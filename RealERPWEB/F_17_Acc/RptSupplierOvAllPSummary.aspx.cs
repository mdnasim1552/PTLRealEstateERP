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
    public partial class RptSupplierOvAllPSummary : System.Web.UI.Page

    {
        ProcessAccess MktData = new ProcessAccess();
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Overall Position Summary";

                var dtoday = System.DateTime.Today;
                this.txttodate.Text = dtoday.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = new System.DateTime(dtoday.Year, dtoday.Month, 1).ToString("dd-MMM-yyyy");
                this.SupplierList();
               
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

       

        protected void ibtnFindSupply_OnClick(object sender, EventArgs e)
        {
            this.SupplierList();
        }


        private void SupplierList()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETSUPPLIER", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlSuplist.DataTextField = "resdesc";
            this.ddlSuplist.DataValueField = "rescode";
            this.ddlSuplist.DataSource = ds1.Tables[0];
            this.ddlSuplist.DataBind();
            ViewState["tblSup"] = ds1.Tables[0];
        }

       

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
            switch (stindex)
            {
                case "0":
                    this.MultiView1.ActiveViewIndex = 0;
                   this. PaymentSummary();

                    break;

                case "1":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.PaymentDetails();
                    break;

            }


           

        }

        private void PaymentSummary()
        {

            try
            {
                Session.Remove("tblsupinfo");
                string comcod = this.GetComeCode();

                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
                string Rescode = this.ddlSuplist.SelectedValue.ToString() == "000000000000" ? "99%" : this.ddlSuplist.SelectedValue.ToString() + "%";
                string mRptGroup = "12";
                string calltype = (stindex == "0" ? "RPTALLSUPPAYMENTSTATUS" : "GETSUPLLIERANDSUBCONTRACTORSATUS");  
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", calltype, frmdate, todate, Rescode, mRptGroup, "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvspaysummary.DataSource = null;
                    this.gvspaysummary.DataBind();
                    return;
                }
                Session["tblspaysum"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {

            }

        }

        private void PaymentDetails()
        {

            try
            {
                Session.Remove("tblsupinfo");
                string comcod = this.GetComeCode();

                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
                string Rescode = this.ddlSuplist.SelectedValue.ToString() == "000000000000" ? "99%" : this.ddlSuplist.SelectedValue.ToString() + "%";
                string mRptGroup = "12";
                string calltype = (stindex == "0" ? "RPTALLSUPPAYMENTSTATUS" : "GETSUPLLIERANDSUBCONTRACTORSATUS");
                //  DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAYMENT", frmdate, todate, Rescode, mRptGroup, supplier, Rescodegrp, search, "", "");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", calltype, frmdate, todate, Rescode, mRptGroup, "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvspaysummary.DataSource = null;
                    this.gvspaysummary.DataBind();
                    return;
                }
                Session["tblspaysum"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {

            }

        }


        private void Data_Bind()
        {

            string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
            switch (stindex)
            {
                case "0":
                    this.gvspaysummary.DataSource = (DataTable)Session["tblspaysum"];
                    this.gvspaysummary.DataBind();

                    break;

                case "1":
                    this.gvspaymentdetails.DataSource = (DataTable)Session["tblspaysum"];
                    this.gvspaymentdetails.DataBind();
                    break;

            }
           
        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblsupinfo"];
            string stindex = this.rbtnAtStatus.SelectedIndex.ToString();

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptSupAdvanceDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSupAdvanceDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", stindex == "0" ? "Supplier Advance Details" : "Supplier Bill Details"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


    }

}