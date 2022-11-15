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

    public partial class RptChequeIssuedList : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "List of  issued Cheque	";
                this.Master.Page.Title = "List of  issued Cheque";
                this.GetBankName();
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }




        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            // Iqbal  Nayan 

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tblCheque"];
            double totalamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnamt)", "")) ? 0.00 : dt.Compute("sum(trnamt)", "")));
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ListIsssuChq>();
            string Bankcode = this.ddlBankName.SelectedValue.ToString();


            switch (comcod)
            {
                case "3330":
                case "3101":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIssuedChequeBridge", lst, null, null);
                    break;

                case "3370":
                    if (Bankcode == "000000000000")
                    {
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIssuedChequeCPALL", lst, null, null);
                    }
                    else
                    {
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIssuedChequeCP", lst, null, null);
                    }
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIssuedCheque", lst, null, null);
                    break;
            }



            //if (comcod == "3330" || comcod == "3101")
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIssuedChequeBridge", lst, null, null);

            //}
            //else
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIssuedCheque", lst, null, null);


            //}


            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));

            if (comcod == "3101" || comcod == "3330")
            {
                Rpt1.SetParameters(new ReportParameter("txtInWord", "Take In Word: " + ASTUtility.Trans(totalamt, 2)));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "  Cheque Requisition Statement"));
            }
            else if(comcod == "3370")
            {
                Rpt1.SetParameters(new ReportParameter("txtInWord", "Take In Word: " + ASTUtility.Trans(totalamt, 2)));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "  Cheque Issuing Register"));
            }
            else
            {
                Rpt1.SetParameters(new ReportParameter("RptTitle", "List Of Issued Cheque"));
            }
            Rpt1.SetParameters(new ReportParameter("Date", "( From " + Convert.ToDateTime(txtfrmdate.Text).ToString("dd-MM-yyyy") + " To " + Convert.ToDateTime(txttodate.Text.Trim()).ToString("dd-MM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("bankn", "Bank Name : " + this.ddlBankName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetBankName()
        {
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSrcBank.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "GETBANKCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1;
            this.ddlBankName.DataBind();
            ds1.Dispose();



        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.ShowData();



        }

        private string companyType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string callType = "";
            switch (comcod)
            {
               
                case "3330": //Bridge Holdling

                    callType = "GETCHEQUEISSUEDBR";
                    break;
                default:
                    callType = "GETCHEQUEISSUED";
                    break;
            }
            return callType;
        }

        private void ShowData()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string Bankcode = (this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBankName.SelectedValue.ToString() + "%";
            string CollType = this.companyType();
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", CollType, frmdate, todate, Bankcode, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvChequelist.DataSource = null;
                this.gvChequelist.DataBind();
                return;
            }


            Session["tblCheque"] = ds2.Tables[0];
            this.Data_Bind();
            if (comcod == "3330")
            {
                this.gvChequelist.Columns[2].Visible = true;

            }


        }

        private void Data_Bind()
        {


            DataTable dt = (DataTable)Session["tblCheque"];

            this.gvChequelist.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvChequelist.DataSource = dt;
            this.gvChequelist.DataBind();
            if (dt.Rows.Count > 0)
            {
                ((Label)this.gvChequelist.FooterRow.FindControl("lgFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnamt)", "")) ?
                            0.00 : dt.Compute("Sum(trnamt)", ""))).ToString("#,##0;(#,##0); ");
            }




        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void gvChequelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvChequelist.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetBankName();
        }
    }
}