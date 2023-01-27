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

namespace RealERPWEB.F_23_CR
{
    public partial class RptPrjWiseClientStatus02 : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string date1 = "01-" + ASTUtility.Right(date, 8);
                this.txtfrmdate.Text = date;


                this.txttodate.Text = Convert.ToDateTime(date1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetProjectName();

                //this.NameChange();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Wise Client Status";


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


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();

            string txtSProject = "%" + this.txtSrcProject.Text + "%";

            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();
            //if (this.Request.QueryString["prjcode"].ToString().Trim().Length > 0)
            //    this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"].ToString().Trim();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.ShowProWiseClientSt();




        }


        private void ShowProWiseClientSt()
        {

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "1[38]" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "RPTPROCLIENTST02", ProjectCode, frmdate, todate, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvProClientst.DataSource = null;
                this.gvProClientst.DataBind();
                return;
            }
            Session["tblprjstatus"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprjstatus"];
            this.gvProClientst.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvProClientst.DataSource = dt;
            this.gvProClientst.DataBind();
            // this.FooterCalculation();


        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblAccRec"];
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFununitsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(unusize)", "")) ?
                0.00 : dt.Compute("Sum(unusize)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFsoldunitsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ?
                 0.00 : dt.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFtounitsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ?
                     0.00 : dt.Compute("Sum(tsize)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFtocostcst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFEncashcst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFatoduescst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
               0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvProClientst.FooterRow.FindControl("lgvFdelchargecst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
            0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvProClientst.FooterRow.FindControl("lgvnettoduescst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
           0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblprjstatus"];

            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EclassPrjClientStatus>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptPrjclientStatus", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("pactdesc", ddlProjectName.SelectedItem.Text.ToString()));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Wise Client Status"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvProClientst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProClientst.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}