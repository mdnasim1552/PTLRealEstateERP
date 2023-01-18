using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_23_CR
{
    public partial class RptCustomerInvoice : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                //this.lblHeadtitle.Text = (this.Request.QueryString["Type"].ToString() == "ClLedger") ? "Client Ledger Report" : "CUSTOMER PAYMENT STATUS";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                //((Label)this.Master.FindControl("lblTitle")).Text = "Customer Invoice ";




            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        private void GetCustomerName()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETCUSTOMERNAME", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlCustName.DataTextField = "custnam";
            this.ddlCustName.DataValueField = "custid";
            this.ddlCustName.DataSource = ds2.Tables[0];
            this.ddlCustName.DataBind();

        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomerName();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetCustomerName();
        }


        //


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.lbtnOk.Text = "Ok";
                this.gvCustInvoice.DataSource = null;
                this.gvCustInvoice.DataBind();
                return;
            }

            this.lbtnOk.Text = "New";
            this.ShowInvoice();

        }

        private void ShowInvoice()
        {
            string comcod = this.GetComeCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString() + "%";
            string custid = this.ddlCustName.SelectedValue.ToString() + "%";
            string frmdate = "01" + this.txtDate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            //string ProjectCode = "18%";
            string curdues = "current";
            string overdues = "";
            string Date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds2 = purData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "RPTCURRENTINVOINCE", pactcode, custid, frmdate, todate, curdues, overdues, "", "", "");


            if (ds2 == null)
            {
                this.gvCustInvoice.DataSource = null;
                this.gvCustInvoice.DataBind();
                return;
            }

            Session["tblCustPayment"] = ds2.Tables[0];
            Session["tblCustadd"] = ds2.Tables[1];
            Session["tbldecharge"] = ds2.Tables[2];
            // this.HiddenSameDate2(ds2.Tables[0]);
            // this.HiddenSameDate2(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblCustPayment"];

            this.gvCustInvoice.DataSource = dt;
            this.gvCustInvoice.DataBind();
            this.FooterCalculation();

        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblCustPayment"];

            if (dt.Rows.Count == null)
            {
                return;
            }


            ((Label)this.gvCustInvoice.FooterRow.FindControl("lfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueamt)", "")) ? 0.00
                    : dt.Compute("Sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblCustPayment"];
            DataTable dt1 = (DataTable)Session["tblCustadd"];
            DataTable dt2 = (DataTable)Session["tbldecharge"];

            double decharge = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(interest)", "")) ? 0.00
                      : dt2.Compute("Sum(interest)", "")));
            double currentdues = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueamt)", "")) ? 0.00
                      : dt.Compute("Sum(dueamt)", "")));




            string aptdesc = dt1.Rows[0]["udesc"].ToString();
            string custadd = dt1.Rows[0]["udesc"].ToString();
            string custmobile = dt1.Rows[0]["custmobile"].ToString();
            string prjadd = dt1.Rows[0]["prjadd"].ToString();
            // string cudues = Convert.ToDouble(dt1.Rows[0]["cudues"]).ToString("#,##0.00;(#,##0.00); ");
            string cuduesdat = Convert.ToDateTime(dt1.Rows[0]["cudate"]).ToString("dd-MMM-yyyy");
            string custname = dt1.Rows[0]["custname"].ToString();

            double totalamt = Convert.ToDouble((decharge + currentdues).ToString("#,##0;(#,##0); "));

            string invdate = txtDate.Text;

            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.CustInovoce>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptCustInvoice", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("prjname", "Project " + ddlProjectName.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("custname", custname));
            Rpt1.SetParameters(new ReportParameter("custmobile", "Mobile : " + custmobile));
            Rpt1.SetParameters(new ReportParameter("custadd", custadd));
            Rpt1.SetParameters(new ReportParameter("aptdesc", "Apt : " + aptdesc));
            Rpt1.SetParameters(new ReportParameter("prjadd", "Address : " + prjadd));
            Rpt1.SetParameters(new ReportParameter("cudues", (decharge + currentdues).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("inword", "In Word : " + ASTUtility.Trans(totalamt, 2)));

            Rpt1.SetParameters(new ReportParameter("decharge", decharge.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("cudate", "Due Date : " + cuduesdat));
            Rpt1.SetParameters(new ReportParameter("invdate", "Invoice Date : " + invdate));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}