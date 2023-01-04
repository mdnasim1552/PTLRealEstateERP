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

namespace RealERPWEB.F_12_Inv
{
    public partial class RptInventoryNet : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date1 = this.Request.QueryString["Date1"];
                string date2 = this.Request.QueryString["Date2"];
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = date1.Length > 0 ? date1 : Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.lbtnOk_Click(null, null);

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];

            this.RtpInventory();
        }
        private void RtpInventory()
        {
            DataTable dt = (DataTable)Session["amtbasis"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;



            if (dt == null)
                return;
            var lst = dt.DataTableToList<RealEntity.C_12_Inv.MatStockIndPro>();

            LocalReport Rpt1 = new LocalReport();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_12_Inv.rptMatStockIndPro", lst, null, null);



            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            //Rpt1.SetParameters(new ReportParameter("StockValuation", "Stock Valuation : " + strstock));

            //Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProName.SelectedItem.Text));

            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            //Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));
            //Rpt1.SetParameters(new ReportParameter("date", "From: " + fdate + " To: " + tdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private string GetCompCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        protected void GetProjectName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCod();
            string serch1 = "%%";
            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETPURPROJECTNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"];
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCod();

            string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string pactcode = this.ddlProjectName.SelectedValue;
            // string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = PurData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTAMTBASISRESPERIODICNET", pactcode, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatStock.DataSource = null;
                this.gvMatStock.DataBind();

                return;

            }

            Session["amtbasis"] = ds1.Tables[0];

            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["amtbasis"];
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMatStock.DataSource = dt;
            this.gvMatStock.DataBind();
            this.FooterCal();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvMatStock.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Data_Bind();
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["amtbasis"];


            ((Label)this.gvMatStock.FooterRow.FindControl("lblactstktf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(actstockamt)", "")) ?
                0.00 : dt.Compute("Sum(actstockamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }
        protected void gvMatStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatStock.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}