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
namespace RealERPWEB.F_14_Pro
{

    public partial class RptPurchaseStatus02 : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type == "Purchase") ? "Purchase Summary With Opening" : "";

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Summary With Opening";

                this.GetProjectName();
                this.ShowView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetMaterial();

        }


        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;





            }



        }




        private void GetMaterial()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = this.txtSrcMaterials.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "rsirdesc";
            this.DropCheck1.DataValueField = "rsirdesc";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();


        }

        protected void imgbtnFindMat_Click(object sender, ImageClickEventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.RptDayPurchase();
                    break;


            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        //private void RptDayPurchase() 
        //{
        //    DataTable dt = (DataTable)Session["tblpurchase"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd MMMM, yyyy");

        //    ReportDocument rptpur = new RealERPRPT.R_14_Pro.RptPurchaseSummary02();
        //    TextObject rptCname = rptpur.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject txtFDate1 = rptpur.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtFDate1.Text = "As On " + date ;
        //    TextObject txtuserinfo = rptpur.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptpur.SetDataSource(dt);
        //    Session["Report1"] = rptpur;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}


        private void RptDayPurchase()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //string Type = Request.QueryString["Type"].ToString();
            //string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));

            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd MMMM, yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpurchase"];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseSummary02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            //Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("todate", "As On: " + date));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Purchase  Summary"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }








        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.ShowValue();

        }
        private void ShowValue()
        {

            string comcod = this.GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowPurchase();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }




        private void ShowPurchase()
        {

            string rsircode = "";


            Session.Remove("tblpurchase");

            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";

            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                rsircode = "";
            else
                foreach (string s1 in sec)
                    rsircode = rsircode + s1.Substring(0, 12);

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "RPTPURSUMMARY", date, pactcode, rsircode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurSum.DataSource = null;
                this.gvPurSum.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();
        }

















        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];

            if ((dt.Rows.Count == 0)) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {


                case "Purchase":
                    this.gvPurSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPurSum.DataSource = dt;
                    this.gvPurSum.DataBind();
                    ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                         0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }






        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurSum.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void imgbtnFindMaterials_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
    }
}