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
    public partial class LinkRptBgdvsExpensDetails : System.Web.UI.Page
    { 


          ProcessAccess MktData = new ProcessAccess();
    protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Budget Vs Expense (Actual) Details ";

                //this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtFDate.Text = "01" + this.txtFDate.Text.Trim().Substring(2);
                //this.txttodate.Text = Convert.ToDateTime(this.txtFDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.GetData();


            }

        }

        private void GetData()
        {

            Session.Remove("tblresource");
            string comcod = this.GetCompCode();


            string date = this.Request.QueryString["Date"].ToString();
            string rsircode = this.Request.QueryString["rsircode"].ToString();
            string prjcode = this.Request.QueryString["prjcode"].ToString();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BUDGETVSEX_PROJECT", "RPTBUDGETVSEXPENSESDETAILS", prjcode, rsircode, date, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdvsExpense.DataSource = null;
                this.gvBgdvsExpense.DataBind();
                return;
            }
            DataTable dt = this.HiddenSameData(ds1.Tables[0]);

            Session["tblresource"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;

            string rescode = dt1.Rows[0]["rescode"].ToString();
            

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rescode"].ToString() == rescode)
                {
                    rescode = dt1.Rows[j]["rescode"].ToString();

                    dt1.Rows[j]["resdesc"] = "";

                }

                else
                {


                    rescode = dt1.Rows[j]["rescode"].ToString();
                }
            }


            return dt1;
        }
        private void Data_Bind()
        {
            
            this.gvBgdvsExpense.DataSource = (DataTable)Session["tblresource"];
            this.gvBgdvsExpense.DataBind();

            this.FooterCalculation();


        }



        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblresource"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvBgdvsExpense.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnqty)", "")) ?
                                 0 : dt.Compute("sum(trnqty)", ""))).ToString("#,##0.00; #,##0.00; ");
            ((Label)this.gvBgdvsExpense.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ?
                         0 : dt.Compute("sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            
            Session["Report1"] = gvBgdvsExpense;
            ((HyperLink)this.gvBgdvsExpense.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }





        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        protected void gvBgdvsExpense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("lblgvactDesc");
            string mCOMCOD = comcod;


            string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString().Trim();

            //F_17_Acc / RptAccVouher.aspx ? vounum = BD202103000029

            if (vounum == "")
                return;

            else
            {
                hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + vounum;              
                hlink1.Target = "_blank";

            }


        }











        //    //    //lgvNagad.Style.Add("text-align", "left");
        //    //    lgvResDescd.Style.Add("text-align", "right");

        //}

        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

        //    string temp = this.ddlBankName.SelectedItem.ToString();
        //    string title = temp.Substring(13, temp.Length - 13);

        //    DataTable dt = (DataTable)Session["tblbankledger"];

        //    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.MonthWiseBankLedger>();
        //    LocalReport rpt = new LocalReport();
        //    rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthWiseBankLedger", list, null, null);
        //    rpt.EnableExternalImages = true;
        //    rpt.SetParameters(new ReportParameter("comName", comnam));
        //    rpt.SetParameters(new ReportParameter("txtTitle", "Month Wise Bank Ledger For " + title));
        //    rpt.SetParameters(new ReportParameter("date1", "From " + fromdate + " To " + todate));
        //    rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
        //    rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

        //    Session["Report1"] = rpt;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
        //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}








    }
}



