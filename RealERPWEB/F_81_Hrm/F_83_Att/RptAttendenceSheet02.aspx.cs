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
using RealERPRDLC;
using RealERPRPT;
using RealEntity;
using System.Drawing;


namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptAttendenceSheet02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetCompany();
                this.GetProjectName();
                this.GetDesignation();
                //this.GetEmpName();
                //this.SectionName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Attendance Information";
                this.SelectDate();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            //if()

            this.PrintMonAttendanceBTI();
        }

        private void SelectDate()
        {
            string comcod = this.GetComCode();
            DataSet datSetup = compUtility.GetCompUtility();
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            if (datSetup == null)
                return;
            switch (comcod)
            {
                case "3365":
                case "3101":
                case "3102":
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
            }
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();

            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds5.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds5.Tables[0];
            this.GetProjectName();
            //ds1.Dispose();

        }

        private void GetProjectName()
        {
            string comcod = this.GetComCode();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //tring Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            //this.GetSectionName();

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private string Calltype()
        {
            string comcod = this.GetComCode();
            string calltype = "";
            switch (comcod)
            {
                case "3101":
                case "3347":

                    calltype = "GROUPNAME";
                    break;

                default:
                    calltype = "GROUPNAME";

                    break;
            }

            return calltype;

        }
        private void GetSectionName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%%";
            string calltype = this.Calltype();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE02", calltype, Company, Department, txtSSec, userid, "", "", "", "", "");
            this.ddlgroup.DataTextField = "attdesc";
            this.ddlgroup.DataValueField = "attgrpcode";
            this.ddlgroup.DataSource = ds2.Tables[0];
            this.ddlgroup.DataBind();

        }
        private void GetDesignation()
        {

            string comcod = this.GetComCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDESIGNATION", "", "", "", "", "", "", "", "", "");
            Session["tbldesig"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlfrmDesig.DataTextField = "designation";
            this.ddlfrmDesig.DataValueField = "desigcod";
            this.ddlfrmDesig.DataSource = ds1.Tables[0];
            this.ddlfrmDesig.DataBind();
            this.ddlfrmDesig.SelectedValue = "0357100";
            this.GetDessignationTo();

        }

        private void GetDessignationTo()
        {

            DataTable dt = (DataTable)Session["tbldesig"];
            //string desigcod = this.ddlfrmDesig.SelectedValue.ToString().Trim();
            //DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "desigcod not in ('" + desigcod + "')";
            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();

        }

        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDessignationTo();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
            //lnkbtnEmp_Click(null, null);
        }
        private string GetComLateAccTime()
        {
            string comcod = this.GetComCode();
            string acclate = "";
            switch (comcod)
            {
                case "3336":
                    acclate = "acclate";
                    break;

                default:
                    break;
            }

            return acclate;
        }
        private void MonthlyAttendance()
        {

            string section22 = this.ddlgroup.SelectedValue.ToString() == "00000000" ? "%%" : this.ddlgroup.SelectedValue.ToString();
            if (this.ddlProjectName.SelectedValue.ToString() == "000000000000")
            {
                string Msg = "Please Select Department";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                return;

            }

            if (section22 == "")
            {
                string Msg = "Please Select Section";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();

            string acclate = this.GetComLateAccTime();

            string section = "";
            //if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            //{
            //    string gp = this.DropCheck1.SelectedValue.Trim();
            //    if (gp.Length > 0)
            //    {
            //        if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
            //            section = "";
            //        else
            //            foreach (ListItem s1 in DropCheck1.Items)
            //            {
            //                if (s1.Selected)
            //                {
            //                    section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
            //                }
            //            }
            //    }
            //}

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE02", "RPTEMPMONTHLYATTN02BTI", frmdate, todate, deptCode, Company, section, todesig, frmdesig, acclate, section22);
            if (ds1 == null)
                return;
            Session["tblallData"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComCode();

            DataTable dt = (DataTable)Session["tblallData"];
            //if (this.rbtnAttStatus.SelectedIndex == 1)
            //{
            int i;
            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

            int tcount;
            tcount = ASTUtility.DatediffTotalDays(dateto, datefrm);
            for (i = 2; i < tcount; i++)
                this.gvMonthlyattSummary.Columns[i].Visible = false;
            int j = 2;
            for (i = 0; i < tcount; i++)
            {
                //if (datefrm > dateto)
                //    break;

                this.gvMonthlyattSummary.Columns[j].Visible = true;
                this.gvMonthlyattSummary.Columns[j].HeaderText = datefrm.ToString("dd") + "<br/>" + datefrm.ToString("dddd").Substring(0, 1);
                //this.gvMonthlyattSummary.Columns[j].HeaderText = datefrm.ToString("dddd").Substring(0,1);
                datefrm = datefrm.AddDays(1);
                j++;

                this.StatusReport.Visible = true;
            }
            this.DelaisAttinfo.Visible = false;

            this.SummaryAttinfo.Visible = true;


            this.gvMonthlyattSummary.DataSource = dt;
            this.gvMonthlyattSummary.DataBind();

            if (dt.Rows.Count == 0)
                return;
            Session["Report1"] = gvMonthlyattSummary;
            ((HyperLink)this.gvMonthlyattSummary.HeaderRow.FindControl("hlbtntbCdataExelSP2")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            //}
            //else
            //{


            //    //if (comcod == "3365")
            //    //{

            //int i;
            //    DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //    DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

            //    int tcount;
            //    tcount = ASTUtility.DatediffTotalDays(dateto, datefrm);
            //    for (i = 2; i < tcount; i++)
            //        this.gvMonthlyAtt.Columns[i].Visible = false;
            //    int j = 2;
            //    for (i = 0; i < tcount; i++)
            //    {
            //        //if (datefrm > dateto)
            //        //    break;

            //        this.gvMonthlyAtt.Columns[j].Visible = true;
            //        this.gvMonthlyAtt.Columns[j].HeaderText = datefrm.ToString("dd") + "<br/>" + datefrm.ToString("dddd").Substring(0, 1);
            //        //this.gvMonthlyattSummary.Columns[j].HeaderText = datefrm.ToString("dddd").Substring(0,1);
            //        datefrm = datefrm.AddDays(1);
            //        j++;

            //        this.StatusReport.Visible = true;
            //    }


            //    this.DelaisAttinfo.Visible = false;

            //    this.SummaryAttinfo.Visible = true;

            //    this.gvMonthlyAtt.DataSource = dt;
            //    this.gvMonthlyAtt.DataBind();
            //}
            //else
            //{

            //    this.DelaisAttinfo.Visible = true;

            //    this.SummaryAttinfo.Visible = false;

            //    this.gvMonthlyAtt.DataSource = dt;
            //    this.gvMonthlyAtt.DataBind();

            //}

            //}
        }
        private void PrintMonAttendanceBTI()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd MMMM yy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM yy");
            string rptMonth = "From " + frmdate + " To " + todate;
            string status = this.statusatt.InnerText.ToString();
            DataTable dt1 = (DataTable)Session["tblallData"];

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
            LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendanceBTI02", list, null, null);




            string printtype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            if (printtype == "EXCEL")
            {
                Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendanceBTI02EXCEL", list, null, null);


            }
            else
            {
                Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendanceBTI02", list, null, null);


            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtMonth", rptMonth));
            Rpt1.SetParameters(new ReportParameter("status", status));
            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 31; i++)
            {
                if (datefrm > dateto)
                    break;

                Rpt1.SetParameters(new ReportParameter("txtDate" + i.ToString(), datefrm.ToString("dd") + "\n" + datefrm.ToString("dddd").Substring(0, 1)));
                datefrm = datefrm.AddDays(1);

            }
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Attendance Statistic"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonAttendance01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();
            string acclate = this.GetComLateAccTime();
            var rptMonth = comcod == "3330" ? "For The Month of " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM, yyyy") : "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy");
            string section = "";

            DataTable dt1 = (DataTable)Session["tblallData"];
            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendance", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtMonth", rptMonth));
            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 31; i++)
            {
                if (datefrm > dateto)
                    break;

                Rpt1.SetParameters(new ReportParameter("txtDate" + i.ToString(), datefrm.ToString("dd")));
                datefrm = datefrm.AddDays(1);

            }
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Attendance Sheet"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.MonthlyAttendance();
        }

        protected void gvMonthlyAtt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonthlyAtt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }

}
