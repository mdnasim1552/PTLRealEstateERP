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
    public partial class RptRequsitionStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["WType"].ToString().Trim() == "ReqStatus") ? "REQUISITION STATUS REPORT"
                //    : (this.Request.QueryString["WType"].ToString().Trim() == "ReqAppStatus") ? "Requisition Status(Approved Date)" : "CLIENT MODIFICATION REPORT";


                this.SelectView();
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                if (this.ddlProjectName.Items.Count == 0)
                {
                    this.GetProjectName();

                }

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ChkBalance.Checked = false;
                    this.rbtnList1.SelectedIndex = 0;
                    this.CheckReqApp.Visible = true;
                    break;

                case "CliModfi":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ChkBalance.Visible = false;
                    this.rbtnList1.Visible = false;
                    this.lblReq.Text = "ADW No";
                    break;

                case "ReqAppStatus":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ChkBalance.Visible = false;
                    this.rbtnList1.Visible = false;

                    break;
            }
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1;
            string geprj = this.Request.QueryString["WType"].ToString();
            if (geprj == "ReqStatus" || geprj == "ReqAppStatus")
            {
                ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
                this.ddlProjectName.DataTextField = "pactdesc";
                this.ddlProjectName.DataValueField = "pactcode";
                this.ddlProjectName.DataSource = ds1.Tables[0];
                this.ddlProjectName.DataBind();
            }
            else
            {
                ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETPROJECTNAME1", txtSProject, "", "", "", "", "", "", "", "");
                this.ddlProjectName.DataTextField = "actdesc";
                this.ddlProjectName.DataValueField = "actcode";
                this.ddlProjectName.DataSource = ds1.Tables[0];
                this.ddlProjectName.DataBind();
            }

        }
        private void GetResource()
        {
            string comcod = this.GetCompCode();
            string SrchResource = "%" + this.txtSrcResource.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETRESOURCE", SrchResource, "", "", "", "", "", "", "", "");
            this.ddlResource.DataTextField = "sirdesc";
            this.ddlResource.DataValueField = "sircode";
            this.ddlResource.DataSource = ds1.Tables[0];
            this.ddlResource.DataBind();
            ds1.Dispose();


        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindResource_Click(object sender, EventArgs e)
        {
            this.GetResource();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":
                    if (rbtnList1.SelectedIndex == 0)
                    {
                        RequisitionBasisStatus();
                    }
                    else if (rbtnList1.SelectedIndex == 1)
                    {
                        ProjectBasisStatus();
                    }
                    break;

                case "CliModfi":
                    this.RptClientMod();
                    break;

                case "ReqAppStatus":
                    this.PrintReqAppStatus();
                    break;
            }


            if (ConstantInfo.LogStatus == true)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string eventtype = "Requsition Status";
                string eventdesc = "Print Report";
                string eventdesc2 = rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        //private void ProjectBasisStatus()
        //{
        //    if (this.lnkbtnOk.Text == "Ok")
        //    {
        //        this.lnkbtnOk_Click(null, null);
        //    }
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
        //    DataTable dt1 = (DataTable)Session["tblstatus"];
        //    ReportDocument rrs2 = new RealERPRPT.R_14_Pro.RptRequisitionStatus2();
        //    TextObject rptCname = rrs2.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject txtFDate1 = rrs2.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtFDate1.Text = "From " + fromdate + " To " + todate;

        //    TextObject txtuserinfo = rrs2.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rrs2.SetDataSource(dt1);
        //    Session["Report1"] = rrs2;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //    this.ChkBalance.Checked = false;
        //}


        private void ProjectBasisStatus()
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
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt1 = (DataTable)Session["tblstatus"];

            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptRequisitionStatus2>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptRequisitionStatus2", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Requisition Status -Requisition Basis"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }







        //private void RequisitionBasisStatus()
        //{
        //    if (this.lnkbtnOk.Text == "Ok")
        //    {
        //        this.lnkbtnOk_Click(null, null);
        //    }
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

        //    DataTable dt1 =(DataTable)Session["tblstatus"];
        //    ReportDocument rrs1 = new  RealERPRPT.R_14_Pro.RptRequisitionStatus1();
        //    TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtFDate1.Text ="From "+fromdate+ " To "+todate;
        //    TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rrs1.SetDataSource(dt1);
        //    Session["Report1"] = rrs1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //    this.ChkBalance.Checked = false;

        //}


        private void RequisitionBasisStatus()
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
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt1 = (DataTable)Session["tblstatus"];

            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptRequisitionStatus1>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptRequisitionStatus1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Requisition Status -Requisition Basis"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        //private void PrintReqAppStatus()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");

        //    DataTable dt1 = (DataTable)Session["tblstatus"];
        //    ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptReqAppStatus();
        //    TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtFDate1.Text = "From " + fromdate + " To " + todate;
        //    TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rrs1.SetDataSource(dt1);
        //    Session["Report1"] = rrs1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        //}

        private void PrintReqAppStatus()
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
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt1 = (DataTable)Session["tblstatus"];

            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptReqAppStatus>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptReqAppStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Requisition Status Information"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        private void RptClientMod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_23_CR.RptCliMod();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":
                    this.LoadData();
                    break;
                case "CliModfi":
                    this.ShowClientMod();
                    break;

                case "ReqAppStatus":
                    this.ShowReqApproved();
                    break;
            }


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Requsition Status";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void imgbtnFindRequiSition_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":
                    this.LoadData();
                    break;
                case "CliModfi":
                    this.ShowClientMod();
                    break;

                case "ReqAppStatus":
                    this.ShowReqApproved();
                    break;
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        private void LoadData()
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string balance = (this.ChkBalance.Checked) ? "woz" : "";
            string Reqno = "%" + this.txtSrcRequisition.Text.Trim() + "%";
            string ResCode = ((this.ddlResource.SelectedValue.ToString() == "000000000000") ? "" : this.ddlResource.SelectedValue.ToString()) + "%";
            string ReqAndApprove = this.CheckReqApp.Checked ? "ReqAndApprove" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONSTATUS", fromdate, todate, pactcode, balance, Reqno, ResCode, ReqAndApprove, "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;
            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.Data_Bind();
        }
        private void ShowReqApproved()
        {

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string Reqno = this.txtSrcRequisition.Text.Trim() + "%";
            string ResCode = ((this.ddlResource.SelectedValue.ToString() == "000000000000") ? "" : this.ddlResource.SelectedValue.ToString()) + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQAPPSTATUS", fromdate, todate, pactcode, Reqno, ResCode, "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatusAp.DataSource = null;
                this.gvReqStatusAp.DataBind();
                return;
            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.Data_Bind();



        }
        private void ShowClientMod()
        {
            Session.Remove("tblstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcdoe = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string adwno = "%" + this.txtSrcRequisition.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMOD", frmdate, todate, pactcdoe, adwno, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvRptCliMod.DataSource = null;
                this.grvRptCliMod.DataBind();
                return;
            }
            Session["tblstatus"] = this.HiddenSameDate(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":
                    this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();

                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFApQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(areqty)", "")) ? 0.00 : dt.Compute("sum(areqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFordrqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ? 0.00 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFRecqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrrqty)", "")) ? 0.00 : dt.Compute("sum(mrrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvReqStatus.FooterRow.FindControl("lblgvFBalqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balqty)", "")) ? 0.00 : dt.Compute("sum(balqty)", ""))).ToString("#,##0.00;(#,##0.00); ");



                    break;

                case "CliModfi":
                    this.grvRptCliMod.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvRptCliMod.DataSource = dt;
                    this.grvRptCliMod.DataBind();
                    this.FooterCalculation();
                    break;

                case "ReqAppStatus":
                    this.gvReqStatusAp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatusAp.DataSource = dt;
                    this.gvReqStatusAp.DataBind();
                    this.FooterCalculation();
                    break;
            }
        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblstatus"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":

                    break;

                case "CliModfi":
                    ((Label)this.grvRptCliMod.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
                                0 : dt1.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "ReqAppStatus":
                    ((Label)this.gvReqStatusAp.FooterRow.FindControl("lgvreqapAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(reqamt)", "")) ?
                               0 : dt1.Compute("sum(reqamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }

        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {
                case "ReqStatus":
                case "ReqAppStatus":

                    if (rbtnList1.SelectedIndex == 1)
                    {
                        return dt1;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        return dt1;
                    }

                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string reqno = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();

                            dt1.Rows[j]["mrfno"] = "";
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat1"] = "";
                            dt1.Rows[j]["eusrname"] = "";
                            dt1.Rows[j]["ausrname"] = "";
                            dt1.Rows[j]["aprvdat"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["reqno"].ToString() == reqno)
                            {
                                dt1.Rows[j]["reqno1"] = "";
                                dt1.Rows[j]["mrfno"] = "";

                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();

                        }

                    }
                    break;

                case "CliModfi":
                    string pactcode1 = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode1)
                        {
                            pactcode1 = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                            pactcode1 = dt1.Rows[j]["pactcode"].ToString();
                    }

                    break;
            }


            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void grvRptCliMod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvRptCliMod.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvReqStatusAp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatusAp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

    }
}