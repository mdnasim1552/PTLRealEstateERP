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
namespace RealERPWEB.F_32_Mis
{
    public partial class ProjectSummary : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Summary -At a glance";

                this.GetProjectName();

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
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }


        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintProSummary();

        }

        private void PrintProSummary()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblprosummary"];

            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptProSummary", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtcompName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtprojectName", this.ddlProjectName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtdate", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", ((Label)this.Master.FindControl("lblTitle")).Text));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //ReportDocument rptProSummary = new RealERPRPT.R_32_Mis.RptProSummary();
            //TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject rpttxtDate = rptProSummary.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text ="Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Project Summary - At a glance";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptProSummary.SetDataSource(dt);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptProSummary.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptProSummary;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ClearScreen();
            Session.Remove("tblprosummary");
            this.pnlgrv.Visible = true;
            string comcod = this.GetCompCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "PRODETAILSINFO", projectcode, "", "", "", "", "", "", "", "");

            if (ds == null) return;
            Session["tblprosummary"] = ds.Tables[0];

            DataTable dt = ds.Tables[0];
            DataTable dt1 = new DataTable();
            DataView dv1;

            dv1 = dt.DefaultView;
            dv1.RowFilter = ("grp = 'AA'");
            dt1 = dv1.ToTable();
            this.gv01.DataSource = dt1;
            this.gv01.DataBind();
            if (dt1.Rows.Count > 0)
                this.lblgrp1.Text = dt1.Rows[0]["grpdesc"].ToString();

            //dv1 = dt.DefaultView;
            //dv1.RowFilter = ("grp = 'BB'");
            //dt1 = dv1.ToTable();
            //this.gv02.DataSource = dt1;
            //this.gv02.DataBind();
            //if (dt1.Rows.Count > 0)
            //    this.lblgrp2.Text = dt1.Rows[0]["grpdesc"].ToString();

            //dv1 = dt.DefaultView;
            //dv1.RowFilter = ("grp = 'CC'");
            //dt1 = dv1.ToTable();
            //this.gv03.DataSource = dt1;
            //this.gv03.DataBind();
            //if (dt1.Rows.Count > 0)
            //    this.lblgrp3.Text = dt1.Rows[0]["grpdesc"].ToString();


            //dv1 = dt.DefaultView;
            //dv1.RowFilter = ("grp = 'DD'");
            //dt1 = dv1.ToTable();
            //this.gv04.DataSource = dt1;
            //this.gv04.DataBind();
            //if (dt1.Rows.Count > 0)
            //    this.lblgrp4.Text = dt1.Rows[0]["grpdesc"].ToString();

            dv1 = dt.DefaultView;
            dv1.RowFilter = ("grp = 'EE'");
            dt1 = dv1.ToTable();
            this.gv05.DataSource = dt1;
            this.gv05.DataBind();
            if (dt1.Rows.Count > 0)
                this.lblgrp5.Text = dt1.Rows[0]["grpdesc"].ToString();


        }

        private void ClearScreen()
        {
            this.lblgrp1.Text = "";
            this.lblgrp2.Text = "";

            this.lblgrp5.Text = "";
        }
        protected void gv01_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvBgdam = (HyperLink)e.Row.FindControl("hlnkgvBgdam");
                HyperLink hlnkgvacam = (HyperLink)e.Row.FindControl("hlnkgvacam");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string gval = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gval")).ToString().Trim();

                if (grp == "")
                {
                    return;
                }
                if (grp == "AA" && gval == "8")
                {

                    string actcode = this.ddlProjectName.SelectedValue.ToString();
                    hlnkgvBgdam.NavigateUrl = "~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk&prjcode=" + actcode;
                    hlnkgvBgdam.Attributes["style"] = "color:blue;";

                    hlnkgvacam.NavigateUrl = "~/F_32_Mis/ProjTrialBalanc.aspx?Type=PrjTrailBal&prjcode=" + actcode;
                    hlnkgvacam.Attributes["style"] = "color:blue;";


                }
                else
                {
                    hlnkgvBgdam.Attributes["style"] = "color:black;";
                    hlnkgvacam.Attributes["style"] = "color:black;";

                }
                //if (torevnue == 0.00 && ASTUtility.Right(code, 4) != "AAAA")

            }

        }
    }
}

