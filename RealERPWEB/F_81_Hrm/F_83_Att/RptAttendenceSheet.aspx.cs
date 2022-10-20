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
    public partial class RptAttendenceSheet : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetCompany();
                this.GetProjectName();
                this.GetDesignation();
                this.GetEmpName();
                //this.SectionName();
                ((Label)this.Master.FindControl("lblTitle")).Text ="Employee Attendance Information";
                this.SelectDate();
                this.hideOptions();
                string comcod = this.GetComCode();
                if (comcod == "3365")
                {
                    this.rbtnAttStatus.SelectedIndex = 1;
                }
                else
                {
                    this.rbtnAttStatus.SelectedIndex = 0;
                }
                }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        private void hideOptions()
        {
            string comcod = this.GetComCode();
            this.rbtnAtten.SelectedIndex = 3;
            this.lnkbtnEmp.Visible = comcod=="3347"?false:true;
            this.ddlEmpName.Visible = comcod == "3347" ? false : true;
            this.empListPnl.Visible = comcod == "3347" ? false : true;
        }
        private void SelectDate()
        {
            string comcod = this.GetComCode();
            DataSet datSetup = compUtility.GetCompUtility();
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "26" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            if (datSetup == null)
                return;
            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
                    this.txtfromdate.Text= System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    break;

                default:
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
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
            if (ds5 == null)
                return;
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
        private void PanelVisivility()
        {
            int rdbutton = this.rbtnAtten.SelectedIndex;
            switch (rdbutton)
            {
                case 0:
                    this.pnlAttnLog.Visible = true;
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;                  
                    this.PnlSection.Visible = false;         
                    break;
                case 1:
                    this.pnldailyatt.Visible = true;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;                                      
                    this.PnlSection.Visible = true;
                    break;
                case 2:
                    this.pnlempstatus.Visible = true;
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;               
                    this.PnlSection.Visible = false;
                    break;
                case 3:
                    this.pnlmonthlyatt.Visible = true;
                    this.pnldailyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;
                    this.isStatusType.Visible = true;
                    this.empListPnl.Visible = true; 
                    this.PnlSection.Visible = true;
                    break;
                case 4:
                    this.pnlemplateatt.Visible = true;
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;                  
                    this.PnlSection.Visible = true;
                    break;
                case 5:
                    this.pnlempstatusLate.Visible = true;
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlAttnLog.Visible = false;                 
                    this.PnlSection.Visible = false;
                    break;
                case 6:
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;                  
                    this.PnlSection.Visible = false;
                    break;
                case 7:
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;                  
                    this.PnlSection.Visible = true;
                    break;
                case 8:
                    this.pnldailyatt.Visible = false;
                    this.pnlmonthlyatt.Visible = false;
                    this.pnlemplateatt.Visible = false;
                    this.pnlempstatus.Visible = false;
                    this.pnlempstatusLate.Visible = false;
                    this.pnlAttnLog.Visible = false;                 
                    this.PnlSection.Visible = true;
                    break;
                default:
                    break;
            }
        }
        private string Calltype()
        {
            string comcod = this.GetComCode();
            string calltype = "";
            switch (comcod)
            {       
                case "3347":
                    calltype = "SECTIONNAMEDP01";
                    break;          
                default:
                    calltype = "SECTIONNAMEDP";                  
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
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", calltype, Company, Department, txtSSec, userid, "", "", "", "", "");
            this.DropCheck1.DataTextField = "sectionname";
            this.DropCheck1.DataValueField = "sectionname";
            this.DropCheck1.DataSource = ds2.Tables[0];
            this.DropCheck1.DataBind();
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
        private void GetEmpName()
        {
            string comcod = this.GetComCode();
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string projectName = ((ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : ddlProjectName.SelectedValue.ToString().Substring(0, 8)) + "%";
 
            string txtSEmployee = "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, projectName, txtSEmployee, "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
        }
        private void GetEmpNameResign()
        {
            string comcod = this.GetComCode();
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string projectName = (ddlProjectName.SelectedValue.ToString() == "000000000000") ? company : ddlProjectName.SelectedValue.ToString().Substring(0, 8) + "%" ;

            string isResign = this.isResignChekcbox.Checked == true ? "true" : "";
             
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETRESIGNEMPLIST", company, projectName, "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
            lnkbtnEmp_Click(null,null);        
        }      
        protected void rbtnAtten_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lnkbtnEmp.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 5) || (this.rbtnAtten.SelectedIndex == 6);
            //this.txtSrcEmpName.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 5) || (this.rbtnAtten.SelectedIndex == 6);
           // this.imgbtnEmpName.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 5 || (this.rbtnAtten.SelectedIndex == 6));
            this.empListPnl.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 3) || (this.rbtnAtten.SelectedIndex == 5 || (this.rbtnAtten.SelectedIndex == 6));
            this.ddlEmpName.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 5 || (this.rbtnAtten.SelectedIndex == 6));
            this.lblfrmdate.Text = ((this.rbtnAtten.SelectedIndex == 1 || this.rbtnAtten.SelectedIndex == 7) ? "Date:" : "From:");
            this.txtfromdate.Text = (this.rbtnAtten.SelectedIndex == 1 ? System.DateTime.Today.ToString("dd-MMM-yyyy") : this.txtfromdate.Text.Trim());
            this.lbltodate.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 3) || (this.rbtnAtten.SelectedIndex == 4 || (this.rbtnAtten.SelectedIndex == 5) || (this.rbtnAtten.SelectedIndex == 6));
            this.txttodate.Visible = (this.rbtnAtten.SelectedIndex == 0) || (this.rbtnAtten.SelectedIndex == 2) || (this.rbtnAtten.SelectedIndex == 3) || (this.rbtnAtten.SelectedIndex == 4) || (this.rbtnAtten.SelectedIndex == 5) || (this.rbtnAtten.SelectedIndex == 6);
            this.isStatusType.Visible = (this.rbtnAtten.SelectedIndex == 3);
            this.RbtnAttanTypeDiv.Visible = (this.rbtnAtten.SelectedIndex == 3);
            this.PanelVisivility();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.PanelVisivility();
            int index = this.rbtnAtten.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.GetAttendncLogData();
                    break;
                case 1:
                    this.DailyAttendance();
                    break;
                case 2:
                    this.EmployeeStatus();
                    break;
                case 3:
                    this.MonthlyAttendance();
                    break;
                case 4:
                    this.MonthLateAtt();
                    break;
                case 5:
                    this.EmployeeStatusLate();
                    break;
                case 6:
                    this.DailyAttendanceCHL();
                    break;
                default:
                    break;
            }
        }
        private void DailyAttendanceCHL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            // string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string Actime = this.GetComLateAccTime();
            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_EMP_DAILY_ATTENDANCE_CHL", frmdate, deptCode, Company, section, Actime, "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblallDataCHL"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void GetAttendncLogData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTENDENCELOG", txtfromdate, txttodate, empid, "", "", "", "", "", "");
            if (ds==null)
                return;
            Session["tblallData"] = ds.Tables[0];
            this.Data_Bind();
        }

        private void MonthLateAtt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            //string prjcode = this.ddlProjectName.SelectedValue.ToString();
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYLATEATTN", frmdate, todate, deptCode, Company, section, "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblallData"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void EmployeeStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            ///string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string Actime = this.GetComLateAccTime();

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, Actime, "", "", "", "", "");
            if (ds4 == null)
                return;
            Session["tblallData"] = ds4.Tables[0];
            this.Data_Bind();
        }
        private void EmployeeStatusLate()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPLATESTATUS", frmdate, todate, empid, "", "", "", "", "", "");
            if (ds5 == null)
                return;
            Session["tblallData"] = ds5.Tables[0];
            this.Data_Bind();
        }
        private void MonthlyAttendance()
        {
            string empid = (this.ddlEmpName.SelectedValue.ToString() == "") ? "%" : this.ddlEmpName.SelectedValue.ToString(); 
            string section22 = this.DropCheck1.SelectedValue.Trim();
            string section = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln)+"%";
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? Company : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();
            string acclate = this.GetComLateAccTime();
            if (empid == "%")
            {
                if (this.ddlProjectName.SelectedValue.ToString() == "000000000000" && comcod != "3315" && comcod != "3365")
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
                if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
                {
                    string gp = this.DropCheck1.SelectedValue.Trim();
                    if (gp.Length > 0)
                    {
                        if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                            section = "";
                        else
                            foreach (ListItem s1 in DropCheck1.Items)
                            {
                                if (s1.Selected)
                                {
                                    section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                                }
                            }
                    }
                }
            }

            string isResign = this.isResignChekcbox.Checked == true ?"True": "";
            string isAttnType = this.RbtnAttanType.SelectedValue == "" ?"%": this.RbtnAttanType.SelectedValue.ToString()+"%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYATTN02", frmdate, todate, deptCode, Company, section, todesig, frmdesig, acclate, empid, isResign, isAttnType);
            if (ds1 == null)
                return;
            Session["tblallData"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void DailyAttendance()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);

            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            // string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";

            string Actime = this.GetComLateAccTime();

            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN", frmdate, deptCode, Company, section, Actime, "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblallData"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComCode();

            DataTable dt = (DataTable)Session["tblallData"];

            int index = this.rbtnAtten.SelectedIndex;
            switch (index)
            {
                case 0:
                    this.gvAttnLog.DataSource = dt;
                    this.gvAttnLog.DataBind();
                    break;
                case 1:
                    this.gvdailyatt.DataSource = dt;
                    this.gvdailyatt.DataBind(); 
                    break;
                case 2:
                    this.gvEmpStatus.DataSource = dt; 
                    this.gvEmpStatus.DataBind(); 
                    break;

                case 3:
                    if (this.rbtnAttStatus.SelectedIndex==1)
                    {
                        int i;
                        DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                        DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

                        int tcount;
                        tcount = ASTUtility.DatediffTotalDays(dateto, datefrm);
                        for (i = 2; i < tcount; i++)
                            this.gvMonthlyattSummary.Columns[i].Visible = false;
                        int j = 2;
                        for (i = 0; i <tcount; i++)
                        {
                            //if (datefrm > dateto)
                            //    break;

                            this.gvMonthlyattSummary.Columns[j].Visible = true;
                            this.gvMonthlyattSummary.Columns[j].HeaderText = datefrm.ToString("dd") + "<br/>"+ datefrm.ToString("dddd").Substring(0, 1);
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
                    }
                    else
                    {                       
                        if (comcod == "3365")
                        {
                            int i;
                            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());

                            int tcount;
                            tcount = ASTUtility.DatediffTotalDays(dateto, datefrm);
                            for (i = 2; i < tcount; i++)
                                this.gvMonthlyAtt.Columns[i].Visible = false;
                            int j = 2;
                            for (i = 0; i < tcount; i++)
                            {
                                //if (datefrm > dateto)
                                //    break;

                                this.gvMonthlyAtt.Columns[j].Visible = true;
                                this.gvMonthlyAtt.Columns[j].HeaderText = datefrm.ToString("dd") + "<br/>" + datefrm.ToString("dddd").Substring(0, 1);
                                //this.gvMonthlyattSummary.Columns[j].HeaderText = datefrm.ToString("dddd").Substring(0,1);
                                datefrm = datefrm.AddDays(1);
                                j++;

                                this.StatusReport.Visible = true;
                            }
                            this.DelaisAttinfo.Visible = true;
                            this.SummaryAttinfo.Visible = false;
                            this.gvMonthlyAtt.DataSource = dt;
                            this.gvMonthlyAtt.DataBind();
                        }
                        else
                        {
                            this.DelaisAttinfo.Visible = true;
                            this.SummaryAttinfo.Visible = false;
                            this.gvMonthlyAtt.DataSource = dt;
                            this.gvMonthlyAtt.DataBind();
                        }

                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvMonthlyAtt;
                        ((HyperLink)this.gvMonthlyAtt.HeaderRow.FindControl("hlbtntbCdataExelSP")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    }
                    break;

                case 4:
                    this.gvemplateatt.DataSource = dt;
                    this.gvemplateatt.DataBind();
                    break;

                case 5:
                    this.gvempstatusLate.DataSource = dt;
                    this.gvempstatusLate.DataBind();
                    break;

                default:
                    break;

            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string rbtnindex = this.rbtnAtten.SelectedIndex.ToString();
            switch (rbtnindex)
            {

                case "0":
                    this.PritEmpAttndencLog();
                    break;
                case "1":
                    this.PritDailyEmpAttndenc();
                    break;
                case "2":
                    this.PrintEmpAttnIdWise();
                    break;
                case "3":
                    this.PrintMonthlyAttn();
                    break;
                case "4":
                    this.PrintMonthlyLateAtten();
                    break;
                case "5":
                    this.PrintEmpStatusLate();
                    break;
                case "6":
                    this.PrintEmpStatusEarly();
                    break;
                case "7":
                    this.DailyEarlyLeave();
                    break;
                case "8":
                    this.DailyLate();
                    break;
            }
        }
        private void PrintDailyAttendanceCHL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string rptDt = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");


            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";

            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {
                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPT_EMP_DAILY_ATTENDANCE_CHL", frmdate, deptCode, Company, section, "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                return;
            }

            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLDayWize>();
            LocalReport rpt = new LocalReport();
            rpt = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptDailyAttendenceCHL", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("date1", rptDt));
            rpt.SetParameters(new ReportParameter("txtTitle", "Daily Attendence Group wize"));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PritEmpAttndencLog()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string txtfromdate = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txttodate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string rptDt = "Date( From: " + txtfromdate + " To: " + txttodate + " )";

            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTENDENCELOG", txtfromdate, txttodate, empid, "", "", "", "", "", "");
            if (ds==null)
                return;

            string depart = ds.Tables[0].Rows[0]["depname"].ToString();
            string designation = "Designation: " + ds.Tables[0].Rows[0]["desg"].ToString(); ;
            string empname = ds.Tables[0].Rows[0]["empname"].ToString();
            var list = ds.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.EmpAttendncLog>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptAttnLog", list, null, null);
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            rpt1.SetParameters(new ReportParameter("depart", "Department:" + depart));
            rpt1.SetParameters(new ReportParameter("designation", designation));
            rpt1.SetParameters(new ReportParameter("reprtdate", rptDt));
            rpt1.SetParameters(new ReportParameter("EmpNam", " Employee Name: " + empname));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Attendance Log"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PritDailyEmpAttndenc()
        {
            string comcod = this.GetComCode();
            switch (comcod)
            {
                //case"3101":
                //case "4305":// Rupayan
                //    this.PrintDailyAttendance02();
                //    break;

                case "3101":// CHL
                case "3348":// CHL
                    this.PrintDailyAttendanceCHL();
                    break;

                default:
                    this.PrintDailyAttendance01();
                    // this.PrintDailyAttendance02();
                    break;
            }
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
        private void PrintDailyAttendance01()
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
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string Actime = this.GetComLateAccTime();
            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {
                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }
                        }
                }
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN", frmdate, deptCode, Company, section, Actime, "", "", "", "");
            if (ds1 == null)
                return;

            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptDailyAllEmpAttn", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txtDate", Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Employee Attendance"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptcb1 = new RealERPRPT.R_81_Hrm.R_83_Att.RptDailyAllEmpAttn();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = PCompany;
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintDailyAttendance02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = GetComCode();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 4) == "0000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + s1.Value.Substring(0, 4);
                            }

                        }
                }
            }
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPDAILYATTN02", frmdate, deptid, Company, section, "", "", "", "", "");
            if (ds1 == null)
                return;


            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptDailyAllEmpAttn02", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txtDate", Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Employee Attendance"));
            Rpt1.SetParameters(new ReportParameter("txttotalemp", Convert.ToDouble(ds1.Tables[1].Rows[0]["temployee"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtabsent", Convert.ToDouble(ds1.Tables[1].Rows[0]["absemp"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtleave", Convert.ToDouble(ds1.Tables[1].Rows[0]["leaveemp"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpresent", Convert.ToDouble(ds1.Tables[1].Rows[0]["presntemp"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtresign", Convert.ToDouble(ds1.Tables[1].Rows[0]["resignemp"]).ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("txtlate5min", Convert.ToDouble(ds1.Tables[1].Rows[0]["lw5min"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtlate6to10min", Convert.ToDouble(ds1.Tables[1].Rows[0]["l6to10"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtlate11minon", Convert.ToDouble(ds1.Tables[1].Rows[0]["l11toup"]).ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("txteleft", Convert.ToDouble(ds1.Tables[1].Rows[0]["eleft"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtoutw30mi", Convert.ToDouble(ds1.Tables[1].Rows[0]["outw30mi"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtoutw31to60mi", Convert.ToDouble(ds1.Tables[1].Rows[0]["outw31to60mi"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtoutw61to90mi", Convert.ToDouble(ds1.Tables[1].Rows[0]["outw61to90mi"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtoutw91toabove", Convert.ToDouble(ds1.Tables[1].Rows[0]["outw91toabove"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptcb1 = new RealERPRPT.R_81_Hrm.R_83_Att.RptDailyAllEmpAttn02();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = PCompany;
            //TextObject txttotalemp = rptcb1.ReportDefinition.ReportObjects["txttotalemp"] as TextObject;
            //txttotalemp.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["temployee"]).ToString("#,##0;(#,##0); ");
            //TextObject txtabsent = rptcb1.ReportDefinition.ReportObjects["txtabsent"] as TextObject;
            //txtabsent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["absemp"]).ToString("#,##0;(#,##0); ");
            //TextObject txtleave = rptcb1.ReportDefinition.ReportObjects["txtleave"] as TextObject;
            //txtleave.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["leaveemp"]).ToString("#,##0;(#,##0); ");
            //TextObject txtpresent = rptcb1.ReportDefinition.ReportObjects["txtpresent"] as TextObject;
            //txtpresent.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["presntemp"]).ToString("#,##0;(#,##0); ");
            //TextObject txtresign = rptcb1.ReportDefinition.ReportObjects["txtresign"] as TextObject;
            //txtresign.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["resignemp"]).ToString("#,##0;(#,##0); ");

            //TextObject txtlate5min = rptcb1.ReportDefinition.ReportObjects["txtlate5min"] as TextObject;
            //txtlate5min.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["lw5min"]).ToString("#,##0;(#,##0); ");
            //TextObject txtlate6to10min = rptcb1.ReportDefinition.ReportObjects["txtlate6to10min"] as TextObject;
            //txtlate6to10min.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l6to10"]).ToString("#,##0;(#,##0); ");
            //TextObject txtlate11minon = rptcb1.ReportDefinition.ReportObjects["txtlate11minon"] as TextObject;
            //txtlate11minon.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["l11toup"]).ToString("#,##0;(#,##0); ");

            //TextObject txteleft = rptcb1.ReportDefinition.ReportObjects["txteleft"] as TextObject;
            //txteleft.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["eleft"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw30mi = rptcb1.ReportDefinition.ReportObjects["txtoutw30mi"] as TextObject;
            //txtoutw30mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw30mi"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw31to60mi = rptcb1.ReportDefinition.ReportObjects["txtoutw31to60mi"] as TextObject;
            //txtoutw31to60mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw31to60mi"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw61to90mi = rptcb1.ReportDefinition.ReportObjects["txtoutw61to90mi"] as TextObject;
            //txtoutw61to90mi.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw61to90mi"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoutw91toabove = rptcb1.ReportDefinition.ReportObjects["txtoutw91toabove"] as TextObject;
            //txtoutw91toabove.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["outw91toabove"]).ToString("#,##0;(#,##0); ");
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //rptdate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd MMMM,yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);


            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion

        }


        private void PrintMonthlyAttn()
        {

            string comcod = this.GetComCode();
            switch (comcod)
            {

                //case "3101":
                case "3333":
                    this.PrintMonAttendanceAlli();
                    break;

                //case"3101":
                case "3325":
                case "2325":
                    this.PrintMonAttendanceLBL();
                    break;

                //BTI   
               //case "3101":
                case "3365":
                    this.PrintMonAttendanceBTI();
                    break;
                case "3101":
                case "3367":
                    this.PrintMonAttendanceEpic();
                    break;

                default:
                    this.PrintMonAttendance01();
                    break;
            }

        }
        //create by (robi)
        public void PrintMonAttendanceEpic()
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

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.RptMntAttenReport>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendanceEPIC", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("txtMonth", rptMonth));
            Rpt1.SetParameters(new ReportParameter("status", status));
            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 31; i++)
            //{
            //    if (datefrm > dateto)
            //        break;

            //    Rpt1.SetParameters(new ReportParameter("txtDate" + i.ToString(), datefrm.ToString("dd") + "\n" + datefrm.ToString("dddd").Substring(0, 1)));
            //    datefrm = datefrm.AddDays(1);

            //}
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Attendance Statistic-Epic Corporate Office"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO"+comcod+".jpg")).AbsoluteUri;
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd MMMM yy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM yy");
            string rptMonth = "From " + frmdate + " To "+ todate;
            string status = this.statusatt.InnerText.ToString();
            DataTable dt1 = (DataTable)Session["tblallData"];

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendanceBTI", list, null, null);
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

                Rpt1.SetParameters(new ReportParameter("txtDate" + i.ToString(), datefrm.ToString("dd")+"\n"+datefrm.ToString("dddd").Substring(0, 1)));
                datefrm = datefrm.AddDays(1);

            }
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Daily Attendance Statistic"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintMonAttendanceAlli()
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
            //string section = "";
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

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYATTN02", frmdate, todate, deptCode, Company, section, todesig, frmdesig, acclate, "");
            //if (ds1 == null)
            //    return;
            DataTable dt1 = (DataTable)Session["tblallData"];

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
            //var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttendanceAlli", list, null, null);
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
        private void PrintMonAttendance01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            //string empid = (this.ddlEmpName.SelectedValue.ToString() == "") ? "%" : this.ddlEmpName.SelectedValue.ToString();
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            //string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            //string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            //string todesig = this.ddlToDesig.SelectedValue.ToString();
            //string acclate = this.GetComLateAccTime();
            var rptMonth = comcod == "3330" ? "For The Month of " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM, yyyy") : "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy");

            DataTable dt1 = (DataTable)Session["tblallData"];

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
            //var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpMnthAttn>();
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

        private void PrintMonAttendanceLBL()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.GetComCode();
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            //string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            //string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + "%";
            //string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            //string todesig = this.ddlToDesig.SelectedValue.ToString();



            //string section = "";
            //if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            //{
            //    //string[] sec = this.DropCheck1.Text.Trim().Split(',');

            //    //if (sec[0].Substring(0, 3) == "000")
            //    //    section = "";
            //    //else
            //    //    foreach (string s1 in sec)
            //    //        section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            //    string gp = this.DropCheck1.SelectedValue.Trim();
            //    if (gp.Length > 0)
            //    {
            //        if (gp.Substring(0,3).Trim() == "000" || gp.Trim() == "")
            //            section = "";
            //        else
            //            foreach (ListItem s1 in DropCheck1.Items)
            //            {
            //                if (s1.Selected)
            //                {
            //                    section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0,3);
            //                }

            //            }


            //    }

            //}

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYATTN02", frmdate, todate, deptCode, Company, section, todesig, frmdesig, "", "");
            //if (ds1 == null)
            //    return;

            //ReportDocument rptcb1 = new RealERPRPT.R_81_Hrm.R_83_Att.RptMonAttendancelbl();


            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = PCompany;
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtMonth"] as TextObject;
            //rptdate.Text = "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy");

            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());


            //for (int i = 1; i <= 31; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptcb1.ReportDefinition.ReportObjects["txtdate" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("dd");
            //    datefrm = datefrm.AddDays(1);

            //}


            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        private void PrintMonAttendance02()
        {
            #region not used
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.GetComCode();
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            //string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            //string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + "%";



            //string section = "";
            //if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            //{
            //    //string[] sec = this.DropCheck1.Text.Trim().Split(',');

            //    //if (sec[0].Substring(0, 4) == "0000")
            //    //    section = "";
            //    //else
            //    //    foreach (string s1 in sec)
            //    //        section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            //    string gp = this.DropCheck1.SelectedValue.Trim();
            //    if (gp.Length > 0)
            //    {
            //        if (gp.Substring(0,4).Trim() == "0000" || gp.Trim() == "")
            //            section = "";
            //        else
            //            foreach (ListItem s1 in DropCheck1.Items)
            //            {
            //                if (s1.Selected)
            //                {
            //                    section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + s1.Value.Substring(0, 4);
            //                }

            //            }


            //    }

            //}
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYATTN", frmdate, todate, deptCode, Company, "", "", "", "", "");
            //if (ds1 == null)
            //    return;
            //ReportDocument rptcb1 = new RealERPRPT.R_81_Hrm.R_83_Att.rptTotalAttndc();
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = PCompany;
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtMonth"] as TextObject;
            //rptdate.Text = "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #endregion
        }

        private void PrintEmpAttnIdWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string Actime = this.GetComLateAccTime();

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPATTNIDWISE", frmdate, todate, empid, Actime, "", "", "", "", "");

            #region Old
            //DataTable dtdailyiemp = ds4.Tables[0];
            //int sum = 0, Ovtsum = 0;
            //string hour, minute, ovthour, ovtminute;
            //for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            //{
            //    sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            //    Ovtsum += Convert.ToInt32(dtdailyiemp.Rows[i]["ovtminitotal"]);

            //}
            //hour = Convert.ToInt32(sum / 60).ToString();
            //minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            //ovthour = Convert.ToInt32(Ovtsum / 60).ToString();
            //ovtminute = ASTUtility.Right((Convert.ToString("00" + (Ovtsum % 60))), 2);

            //string TotalHour = hour + ":" + minute;
            //string TotalOvtHour = ovthour + ":" + ovtminute;


            //ReportDocument rptTest = new ReportDocument();
            //if (comcod == "3347")
            //{



            //    rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptDailyAttnEmpPEB();
            //    TextObject txtrptTotalOvtHour = rptTest.ReportDefinition.ReportObjects["txtOvtHour"] as TextObject;
            //    txtrptTotalOvtHour.Text = TotalOvtHour;

            //}
            //else
            //{
            //    rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptDailyAttnEmp();

            //}




            //rptTest.SetDataSource(ds4.Tables[0]);
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = PCompany;

            //TextObject txttowrkday = rptTest.ReportDefinition.ReportObjects["txttowrkday"] as TextObject;
            //txttowrkday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            //TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            //txttolateday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            //TextObject txttoleaveday = rptTest.ReportDefinition.ReportObjects["txttoleaveday"] as TextObject;
            //txttoleaveday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            //TextObject txtoabsday = rptTest.ReportDefinition.ReportObjects["txtoabsday"] as TextObject;
            //txtoabsday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            //TextObject txtohday = rptTest.ReportDefinition.ReportObjects["txtohday"] as TextObject;
            //txtohday.Text = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");


            //TextObject txtrptTotalHour = rptTest.ReportDefinition.ReportObjects["txtTHour"] as TextObject;
            //txtrptTotalHour.Text = TotalHour;
            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Session["Report1"] = rptTest;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #endregion
            var empName = ds4.Tables[0].Rows[0]["empnam"].ToString();
            var empDsg = ds4.Tables[0].Rows[0]["empdsg"].ToString();
            var empDept = ds4.Tables[0].Rows[0]["empdept"].ToString();
            var StdIn = ds4.Tables[0].Rows[0]["stdtimein"].ToString();
            var StdOut = ds4.Tables[0].Rows[0]["stdtimeout"].ToString();
            var list = ds4.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.EmpAttnIdWise>();
            LocalReport rpt1 = new LocalReport();

            if (comcod == "3354" || comcod=="3101")
            {
                rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptNewEmpStatusEdi", list, null, null);
            }
            else
            {
                rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptNewEmpStatus", list, null, null);

            }
            DataTable dtdailyiemp = ds4.Tables[0];
            int sum = 0;
            string hour, minute;
            for (int i = 0; i < dtdailyiemp.Rows.Count; i++)
            {
                sum += Convert.ToInt32(dtdailyiemp.Rows[i]["actualattnminute"]);
            }
            hour = Convert.ToInt32(sum / 60).ToString();
            minute = ASTUtility.Right((Convert.ToString("00" + (sum % 60))), 2);
            string TotalHour = hour + ":" + minute;

            //string empnam = ds.Tables[0].Rows[0]["empnam"].ToString();


            string idcardno = list[0].idcardno.ToString();

            string empdsg = list[0].empdsg.ToString();
            string empdept = list[0].empdept.ToString();
            string empnam = list[0].empnam.ToString();
            string stdtimein = Convert.ToDateTime(list[0].stdtimein).ToString("hh:mm tt");
            string stdtimeout = Convert.ToDateTime(list[0].stdtimeout).ToString("hh:mm tt");


            string wday = Convert.ToDouble(ds4.Tables[1].Rows[0]["twrkday"]).ToString("#,##0;(#,##0); ");
            string laday = Convert.ToDouble(ds4.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            string leday = Convert.ToDouble(ds4.Tables[1].Rows[0]["tlvday"]).ToString("#,##0;(#,##0); ");
            string abday = Convert.ToDouble(ds4.Tables[1].Rows[0]["tabsday"]).ToString("#,##0;(#,##0); ");
            string hday = Convert.ToDouble(ds4.Tables[1].Rows[0]["thday"]).ToString("#,##0;(#,##0); ");
            string earlyleav = Convert.ToDouble(ds4.Tables[1].Rows[0]["terlyday"]).ToString("#,##0;(#,##0); ");
            
            rpt1.SetParameters(new ReportParameter("comnam", comnam));
            //rpt1.SetParameters(new ReportParameter("RptTitle", rptDt));
            rpt1.SetParameters(new ReportParameter("empname", empnam));
            rpt1.SetParameters(new ReportParameter("idcardno", idcardno));
            rpt1.SetParameters(new ReportParameter("empdsg", empdsg));
            rpt1.SetParameters(new ReportParameter("empdept", empdept));

            rpt1.SetParameters(new ReportParameter("stdtimein", stdtimein));
            rpt1.SetParameters(new ReportParameter("stdtimeout", stdtimeout));

            rpt1.SetParameters(new ReportParameter("wday", wday));
            rpt1.SetParameters(new ReportParameter("laday", laday));
            rpt1.SetParameters(new ReportParameter("leday", leday));
            rpt1.SetParameters(new ReportParameter("abday", abday));
            rpt1.SetParameters(new ReportParameter("hday", hday));
            rpt1.SetParameters(new ReportParameter("earlyleav", earlyleav));
            rpt1.SetParameters(new ReportParameter("TotalHour", TotalHour));

            rpt1.SetParameters(new ReportParameter("RptTitle", "Individual Attendance Summary Report"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonthlyLateAtten()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }

                        }


                }

            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPMONTHLYLATEATTN", frmdate, todate, deptCode, Company, section, "", "", "", "");
            if (ds1 == null)
                return;

            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EMDailyAttendenceClassCHL.DailyAttenCHLGroupWize>();
            LocalReport Rpt1 = new LocalReport();

            //ReportDocument rptcb1 = new ReportDocument();

            if (comcod == "3330") //3330
            {
                Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptHRMonthlyLateSumBR", list, null, null);
                //rptcb1 = new RealERPRPT.R_81_Hrm.R_83_Att.RptHRMonthlyLateSumBR();
            }
            else
            {
                Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptHRMonthlyLateSum", list, null, null);
                //rptcb1 = new RealERPRPT.R_81_Hrm.R_83_Att.RptHRMonthlyLateSum();

            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txtMonth", comcod == "3330" ? "Month of " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM, yyyy") : "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Late Attendance"));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = PCompany;
            ////rptdate.Text = comcod == "3330" ? "For The Month of " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM, yyyy") : "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy");
            //TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["txtMonth"] as TextObject;
            //rptdate.Text = comcod == "3330" ? "Month of " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM, yyyy") : "For The Month of " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM, yyyy");   //"Month " + Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("MMMM,yyyy");
            //TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptcb1.SetDataSource(ds1.Tables[0]);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptcb1.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptcb1;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }


        private void PrintEmpStatusLate()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPLATESTATUS", frmdate, todate, empid, "", "", "", "", "", "");
            DataTable dtdailyiemp = ds5.Tables[0];

            var list = ds5.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.EmpSatausLate>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.rptMonthyLateAttnEmp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txttolateday", (ds5.Tables[1].Rows.Count == 0) ? "0" : Convert.ToDouble(ds5.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Late Status"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptMonthyLateAttnEmp();
            //rptTest.SetDataSource(ds5.Tables[0]);
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = PCompany;


            //TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            //txttolateday.Text =  (ds5.Tables[1].Rows.Count==0)?"0":Convert.ToDouble(ds5.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Session["Report1"] = rptTest;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }

        private void PrintEmpStatusEarly()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "EMPSTATUSEARLY", frmdate, todate, empid, "", "", "", "", "", "");
            DataTable dtdailyiemp = ds6.Tables[0];

            var list = ds6.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.EmpSatausLate>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.rptMonthyEarlyLeaveEmp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", PCompany));
            Rpt1.SetParameters(new ReportParameter("txttolateday", (ds6.Tables[1].Rows.Count == 0) ? "0" : Convert.ToDouble(ds6.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Early Leave Status"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.rptMonthyEarlyLeaveEmp();
            //rptTest.SetDataSource(ds6.Tables[0]);
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = PCompany;

            //TextObject txttolateday = rptTest.ReportDefinition.ReportObjects["txttolateday"] as TextObject;
            //txttolateday.Text = Convert.ToDouble(ds6.Tables[1].Rows[0]["tLday"]).ToString("#,##0;(#,##0); ");
            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Session["Report1"] = rptTest;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }



        private void DailyEarlyLeave()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 3).Trim() == "000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + s1.Value.Substring(0, 3);
                            }

                        }


                }

            }

            DataSet ds6 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTDAILYEARLYLEAVE", date, Company, deptCode, section, "", "", "", "");
            DataTable dtdailyiemp = ds6.Tables[0];

            var list = ds6.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.DailyLate>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptAttendenceSheetEarly", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Early Leave Status"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_83_Att.RptAttendenceSheetEarly();
            //rptTest.SetDataSource(ds6.Tables[0]);
            //TextObject CompName = rptTest.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txttitle = rptTest.ReportDefinition.ReportObjects["txttitle"] as TextObject;
            //txttitle.Text = "	Early Leave Status";
            //TextObject cdate = rptTest.ReportDefinition.ReportObjects["cdate"] as TextObject;
            //cdate.Text = date;


            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Session["Report1"] = rptTest;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }


        private void DailyLate()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comadd = hst["comadd1"].ToString();
            string rptDt = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + "%";



            string section = "";
            if ((this.ddlProjectName.SelectedValue.ToString() != "000000000000"))
            {

                string gp = this.DropCheck1.SelectedValue.Trim();
                if (gp.Length > 0)
                {
                    if (gp.Substring(0, 4).Trim() == "0000" || gp.Trim() == "")
                        section = "";
                    else
                        foreach (ListItem s1 in DropCheck1.Items)
                        {
                            if (s1.Selected)
                            {
                                section = section + this.ddlProjectName.SelectedValue.ToString().Substring(0, 8) + s1.Value.Substring(0, 4);
                            }

                        }
                }

            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDAILYLATEANDABSENT", frmdate, deptCode, Company, section, "", "", "", "", "");
            if (ds1 == null)
                return;

            //var list = (List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.DailyLate>)ds1.Tables[0];
            var list = ds1.Tables[0].DataTableToList<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.DailyLate>();
            // string ComLogo = new Uri (Server.MapPath (@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport rpt1 = new LocalReport();

            rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_83_Att.RptDailyLateAtt", list, null, null);

            rpt1.SetParameters(new ReportParameter("comnam", comnam));

            rpt1.SetParameters(new ReportParameter("reprtdate", "Date: " + rptDt));
            rpt1.SetParameters(new ReportParameter("RptTitle", "Daily Late Attendence Information"));
            rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
            rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }

        protected void gvMonthlyAtt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonthlyAtt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvdailyatt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {

                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell01 = new TableCell();
                cell01.Text = "Sl";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.RowSpan = 2;
                gvrow.Cells.Add(cell01);


                TableCell cell02 = new TableCell();
                cell02.Text = "Emp ID";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.RowSpan = 2;
                gvrow.Cells.Add(cell02);

                TableCell cell03 = new TableCell();
                cell03.Text = "Card No";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.RowSpan = 2;
                gvrow.Cells.Add(cell03);


                TableCell cell04 = new TableCell();
                cell04.Text = "Name";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.RowSpan = 2;
                gvrow.Cells.Add(cell04);


                TableCell cell05 = new TableCell();
                cell05.Text = "Designation";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.RowSpan = 2;
                gvrow.Cells.Add(cell05);

                TableCell cell06 = new TableCell();
                cell06.Text = "Office Time";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.Attributes["style"] = "font-weight:bold;";
                cell06.ColumnSpan = 2;
                gvrow.Cells.Add(cell06);


                TableCell cell07 = new TableCell();
                cell07.Text = "Actual Time";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.Attributes["style"] = "font-weight:bold;";
                cell07.ColumnSpan = 2;
                gvrow.Cells.Add(cell07);

                TableCell cell08 = new TableCell();
                cell08.Text = "Late";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.RowSpan = 2;
                gvrow.Cells.Add(cell08);


                TableCell cell09 = new TableCell();
                cell09.Text = "Early Leave";
                cell09.HorizontalAlign = HorizontalAlign.Center;
                cell09.RowSpan = 2;
                gvrow.Cells.Add(cell09);

                TableCell cell10 = new TableCell();
                cell10.Text = "Absent";
                cell10.HorizontalAlign = HorizontalAlign.Center;
                cell10.RowSpan = 2;
                gvrow.Cells.Add(cell10);

                gvdailyatt.Controls[0].Controls.AddAt(0, gvrow);
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[09].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;

            }
        }

        protected void gvMonthlyattSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonthlyattSummary.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvAttnLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAttnLog.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lnkbtnEmp_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void DropCheck1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }

        protected void isResignChekcbox_CheckedChanged(object sender, EventArgs e)
        {
            if (isResignChekcbox.Checked == true)
            {
                this.GetEmpNameResign();
            }
            else
            {
                this.GetEmpName();
            }
           
        }
    }
}



