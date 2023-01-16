using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{

    public partial class RptMonthWiseBankLedger : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Month Wise Bank Ledger ";

                this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = "01" + this.txtFDate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtFDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetBankName();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }



        private void GetBankName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            string bankcode = "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETBANKNAME", txtSProject, bankcode, "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "bankName";
            this.ddlBankName.DataValueField = "bankcode";
            this.ddlBankName.DataSource = ds1.Tables[0];
            this.ddlBankName.DataBind();



        }

        protected void imgbtnFindBank_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("tblbankledger");
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtFDate.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }

            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string bankcode = this.ddlBankName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "RPTLEDGERMONTHWISE", fromdate, todate, bankcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBankLedger.DataSource = null;
                this.gvBankLedger.DataBind();
                return;
            }
            // DataTable dt = this.HiddenSameData(ds1.Tables[0]);

            Session["tblbankledger"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;

            string actcode = dt1.Rows[0]["actcode"].ToString();
            string mrescode = dt1.Rows[0]["mrescode"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode && dt1.Rows[j]["mrescode"].ToString() == mrescode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    mrescode = dt1.Rows[j]["mrescode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["mresdesc"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {
                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["mrescode"].ToString() == mrescode)
                    {
                        dt1.Rows[j]["mresdesc"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    mrescode = dt1.Rows[j]["mrescode"].ToString();
                }
            }


            return dt1;
        }
        private void Data_Bind()
        {
            //this.prjcost.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvBankLedger.DataSource = (DataTable)Session["tblbankledger"];
            this.gvBankLedger.DataBind();
            ((HyperLink)this.gvBankLedger.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            //fthis.FooterCalculation();


        }



        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblbankledger"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvBankLedger.FooterRow.FindControl("lgvFopnamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                 0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;-#,##0; ");
            ((Label)this.gvBankLedger.FooterRow.FindControl("lgvFdebitamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trndr)", "")) ?
                         0 : dt.Compute("sum(trndr)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBankLedger.FooterRow.FindControl("lgvFcreditamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trncr)", "")) ?
                         0 : dt.Compute("sum(trncr)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvBankLedger.FooterRow.FindControl("lgvFclsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsamt)", "")) ?
                         0 : dt.Compute("sum(clsamt)", ""))).ToString("#,##0;-#,##0; ");

            Session["Report1"] = gvBankLedger;

        }





        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }







        protected void gvBankLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvactDesc");
            string mCOMCOD = comcod;


            string monthid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "monthid")).ToString().Trim();
            string fromdate = Convert.ToDateTime(monthid).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(fromdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            string bankcode = this.ddlBankName.SelectedValue.ToString();
            string opnoption = "";

            if (monthid == "")
                return;

            else
            {
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + bankcode +
                 "&Date1=" + fromdate + "&Date2=" + todate + "&opnoption=" + opnoption;
                hlink1.Target = "_blank";

            }




            //    //lgvNagad.Style.Add("text-align", "left");
            //    lgvResDescd.Style.Add("text-align", "right");

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string temp = this.ddlBankName.SelectedItem.ToString();
            string title = temp.Substring(13, temp.Length - 13);

            DataTable dt = (DataTable)Session["tblbankledger"];

            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.MonthWiseBankLedger>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthWiseBankLedger", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Month Wise Bank Ledger For " + title));
            rpt.SetParameters(new ReportParameter("date1", "From " + fromdate + " To " + todate));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }








    }
}


