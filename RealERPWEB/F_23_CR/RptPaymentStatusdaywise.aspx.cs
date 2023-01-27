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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_23_CR
{
    public partial class RptPaymentStatusdaywise : System.Web.UI.Page
    {
        ProcessAccess ImpleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Payment Status (Date Wise)";
                //string date1 = this.Request.QueryString["Date1"];
                //string date2 = this.Request.QueryString["Date2"];
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
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
        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string Srchpactcode = "%%";
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETPROJECTNAME", Srchpactcode, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "pactdesc";
            this.ddlPrjName.DataValueField = "pactcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();


        }

        protected void ddlPrjName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }
        private void GetCustomerName()
        {
            ViewState.Remove("tblcustomer");

            string comcod = this.GetComeCode();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            string islandowner = this.Request.QueryString["Type"] == "LOClLedger" ? "1" : "0";
            DataSet ds2 = ImpleData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, islandowner, "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();
            ViewState["tblcustomer"] = ds2.Tables[0];
            ds2.Dispose();

        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }
        public void LoadData()
        {

            this.ShowPaymentStatus();

        }
        public void ShowPaymentStatus()
        {
            string comcod = this.GetComeCode();

            // string frmdate = this.txtFDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string frmdate = this.txtFDate.Text.Trim();
            string pactcode = this.ddlPrjName.SelectedValue.ToString() == "000000000000" ? "18" + "%" : this.ddlPrjName.SelectedValue.ToString() + "%";
            string usircode = this.ddlCustName.SelectedValue.ToString();
            DataSet ds1 = ImpleData.GetTransInfo(comcod, "SP_REPORT_COLLECTIONMGT", "GETPAYMENTSATATUS", pactcode, frmdate, todate, usircode, "", "", "", "");
            if (ds1 == null)
            {

                this.gvpaystatus.DataSource = null;
                this.gvpaystatus.DataBind();

                return;

            }

            // DataTable dt=this.HiddenSamaData(ds1.Tables[0])

            ViewState["prjcoll"] = ds1.Tables[0];
            ViewState["prjdesc"] = ds1.Tables[1];
            ViewState["prjcust"] = ds1.Tables[2];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["prjcoll"];

            this.gvpaystatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvpaystatus.DataSource = dt;
            this.gvpaystatus.DataBind();
            this.FooterCal();



        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadData();

        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["prjcoll"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvpaystatus.FooterRow.FindControl("lgvFPaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(paidamt)", "")) ?
       0.00 : dt.Compute("Sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
           
            string prjname = this.ddlPrjName.SelectedItem.Text.Trim();
           
            DataTable dt2 = (DataTable)ViewState["prjcust"];
            string custname = dt2.Rows[0]["custname"].ToString();
            string udesc = dt2.Rows[0]["udesc"].ToString();
            string mobileno = dt2.Rows[0]["mobileno"].ToString();
            string preaddress = dt2.Rows[0]["preaddress"].ToString();

            LocalReport Rpt1 = new LocalReport();
           
                DataTable dt = (DataTable)ViewState["prjcoll"];
                DataTable dt1 = (DataTable)ViewState["prjdesc"];


                var list = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales.PaymentStatusReconcile>();
                var list1 = dt1.DataTableToList<RealEntity.C_22_Sal.EClassSales.PaymentStatusRevenue>();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptPaymentSystem", list, list1, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Payment Status"));
          


            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("custname", custname));
            Rpt1.SetParameters(new ReportParameter("udesc", udesc));
            Rpt1.SetParameters(new ReportParameter("mobileno", mobileno));
            Rpt1.SetParameters(new ReportParameter("frmto", "( "+frmdate +" to " +todate+" )"));
            Rpt1.SetParameters(new ReportParameter("prjname", prjname));
            Rpt1.SetParameters(new ReportParameter("preaddress", preaddress));

            Rpt1.SetParameters(new ReportParameter("printdate", "Print Date : " + printdate));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

    }
}