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

using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class SalesPermissionCost : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                this.GetProjectName();
                this.GetCustomer();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Bill Conformation";
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MM-yyyy");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

            ds1.Dispose();
            this.GetCustomer();
        }
        private void GetCustomer()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtsrchCustomer = "%" + this.txtSrcCustomer.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSE_CLCHOICE", "GETCUSTOMERNAME", pactcode, txtsrchCustomer, "", "", "", "", "", "", "");
            this.ddlCustomer.DataTextField = "custnam";
            this.ddlCustomer.DataValueField = "custid";
            this.ddlCustomer.DataSource = ds1.Tables[0];
            this.ddlCustomer.DataBind();
            ds1.Dispose();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetCustomer();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string userCode = this.ddlCustomer.SelectedValue.ToString();

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_REGISTRATION", "GETSALESPERCOST", ProjectCode, userCode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            Session["tblregcostinfo"] = ds1.Tables[0];
            Session["tbllandinfo"] = ds1.Tables[1];
            this.Data_Bind();

        }

        public void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblregcostinfo"];
            this.grvCostRegistration.DataSource = dt;
            this.grvCostRegistration.DataBind();
            this.Footer_Caculate();
        }

        public void Footer_Caculate()
        {
            DataTable dt = (DataTable)Session["tblregcostinfo"];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            else
            {

                ((Label)grvCostRegistration.FooterRow.FindControl("totalAcamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(acamt)", "")) ? 0.00 : dt.Compute("sum(acamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)grvCostRegistration.FooterRow.FindControl("totalrcvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recamt)", "")) ? 0.00 : dt.Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
            }

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            //string customer = ddlCustomer.SelectedItem.Text;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt1 = (DataTable)Session["tblregcostinfo"];
            DataTable dt2 = (DataTable)Session["tbllandinfo"];



            var list = dt1.DataTableToList<RealEntity.C_24_CC.RptSalesPermissionCost>();
            var list1 = dt2.DataTableToList<RealEntity.C_24_CC.Landinfo>();
            string rcvamnt = list[0].recamt.ToString();
            string acvmnt = list[0].acamt.ToString();
            string flat = list1[0].udesc.ToString();
            //string fsize = list1[0].fsize.ToString();
            string usize = list1[0].usize.ToString();
            string munit = list1[0].munit.ToString();
            //string sdate = list1[0].sdate.ToString();
            string custname = list1[0].customer.ToString();
            string nflat = list1[0].noflat.ToString();
            string totalland = list1[0].totaland.ToString();
            string landunit = list1[0].gunit;
            string landperf = ((Convert.ToDouble(totalland)) / (Convert.ToDouble(nflat))).ToString("#,##0.00");
            string nocar = list1[0].nocar.ToString();

            double rcvTotal = list.Select(l => l.recamt).Sum();
            double actualTotal = list.Select(l => l.acamt).Sum();
            double rester = (actualTotal - rcvTotal);
            LocalReport Rpt1 = new LocalReport();
            //var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.Customer_Dues_info>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptSalesPermissionCost", list, list1, null);
            Rpt1.SetParameters(new ReportParameter("pactdesc", "Project Name : " + ddlProjectName.SelectedItem.Text.Substring(16)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", printFooter));
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("companyadd", comadd));
            Rpt1.SetParameters(new ReportParameter("flatno", flat));
            //Rpt1.SetParameters(new ReportParameter("flatsize", fsize));
            Rpt1.SetParameters(new ReportParameter("unitsize", usize));
            Rpt1.SetParameters(new ReportParameter("land", munit));
            Rpt1.SetParameters(new ReportParameter("totaland", totalland));
            Rpt1.SetParameters(new ReportParameter("landunit", landunit));
            Rpt1.SetParameters(new ReportParameter("landperflat", landperf));

            Rpt1.SetParameters(new ReportParameter("title", "Sales Permission Cost"));
            Rpt1.SetParameters(new ReportParameter("customer", custname));
            Rpt1.SetParameters(new ReportParameter("noflat", nflat));
            Rpt1.SetParameters(new ReportParameter("noofcar", nocar));

            Rpt1.SetParameters(new ReportParameter("InWrd", "Received Amount : " + ASTUtility.Trans(Math.Round(rcvTotal), 2)));
            Rpt1.SetParameters(new ReportParameter("rester", "Rester Amount : " + ASTUtility.Trans(Math.Round(rester), 2)));

            //Rpt1.SetParameters(new ReportParameter("frmdate", "From: "+ frmdate));
            //Rpt1.SetParameters(new ReportParameter("todate", "To: " + todate));
            // Rpt1.SetParameters(new ReportParameter("daterange", DateFT));
            // Rpt1.SetParameters(new ReportParameter("title", "Customer Dues Information"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
    }
}