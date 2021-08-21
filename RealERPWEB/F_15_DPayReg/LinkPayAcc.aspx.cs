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
namespace RealERPWEB.F_15_DPayReg
{
    public partial class LinkPayAcc : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "BankPosition02") ? "Bank position"
                    : "Date Wise Sales";
                this.SelectView();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BankPosition02":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblAsDate.Text = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");

                    this.ShowBankPosition();
                    break;

            }
        }

        private void ShowBankPosition()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.Request.QueryString["comcod"];
            string date1 = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION02", date1, "12", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBankPosition02.DataSource = null;
                this.gvBankPosition02.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "BankPosition02":
                    break;

            }

            return dt1;
        }
        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {

                case "BankPosition02":
                    //((Label)this.gvBankPosition02.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                    //                0 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvBankPosition02.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ?
                    //                0 : dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;

            }


        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tbAcc"];

            if ((dt.Rows.Count == 0)) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BankPosition02":
                    this.gvBankPosition02.DataSource = dt;
                    this.gvBankPosition02.DataBind();

                    //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                    //((HyperLink)this.gvBankPosition02.HeaderRow.FindControl("hlbtnbnkpdataExel02")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                    dt.Dispose();
                    Session["Report1"] = gvBankPosition02;
                    ((HyperLink)this.gvBankPosition02.HeaderRow.FindControl("hlbtnbnkpdataExel02")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


                    break;

            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BankPosition02":
                    this.PrintBankPosition02();
                    break;

            }
        }

        private void PrintBankPosition02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tbAcc"];
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.BankPosition>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccBankPosition02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "As On Date : " + Convert.ToDateTime(this.lblAsDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bank Position 02"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}