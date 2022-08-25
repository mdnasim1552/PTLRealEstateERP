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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{

    public partial class EmpStatus02 : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Type = this.Request.QueryString["Type"]??"";
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (Type != "Pabx")
                {
                    int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                    
                    if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                            (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                        Response.Redirect("~/AcceessError.aspx");

                    DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                }
                Session.Remove("tblEmpstatus");                
                this.GetDate();
                this.GetCompany();
                this.SelectView();
                this.GetDesignation();
               
                ((Label)this.Master.FindControl("lblTitle")).Text = (Type=="Pabx")?"List of PABX Information": (Type == "joiningRpt") ? "Joining Report Summary" : (Type == "JoinigdWise") ? "New Joiners List"
                    : (Type == "EmpList") ? "Employee List" : (Type == "TransList") ? "Employee Transfer List"
                    : (Type == "PenEmpCon") ? "Pending Employee Confirmation" : (Type == "SepType") ? "Employee Seperation List Report"
                    : (Type == "EmpHold") ? "Employee Hold List" : (Type == "Manpower") ? "Employee Manpower List"
                    : (Type == "EmpGradeADesig") ? "Grade & Designation Wise  Salary Detail"
                    : (Type == "InActiveEmpList") ? "Inactive Employee List"
                    : (Type == "TotalEmplist") ? "Total Employee List"
                    : "Employee Confirmation";

                if (hst["comcod"].ToString().Substring(0, 1) == "8")
                {
                    this.comlist.Visible = true;
                    this.Company();
                }

                this.lbtnOk_Click(null,null);

            }

        }

        private void GetDate()
        {
            string comcod = this.GetCompCode();

            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //this.txtFdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            //this.txtFdate.Text = startdate + this.txtFdate.Text.Trim().Substring(2);
            //this.txtTdate.Text = Convert.ToDateTime(this.txtFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
                    this.txtFdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtFdate.Text = startdate + this.txtFdate.Text.Trim().Substring(2);
                    this.txtTdate.Text = Convert.ToDateTime(this.txtFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    this.txtFdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtFdate.Text = startdate + this.txtFdate.Text.Trim().Substring(2);
                    this.txtTdate.Text = Convert.ToDateTime(this.txtFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
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
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }

        private void Company()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            this.ddlComName.DataTextField = "comsnam";
            this.ddlComName.DataValueField = "comcod";
            this.ddlComName.DataSource = ds1.Tables[0];
            this.ddlComName.DataBind();

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            switch (type)
            {
                case "joiningRpt":
                    if (comcod == "3365")
                    {
                        this.desFrom.Visible = false;
                        this.desTo.Visible = false;
                    }
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "JoinigdWise":
                    if (comcod == "3365")
                    {
                        this.desFrom.Visible = false;
                        this.desTo.Visible = false;
                    }
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "EmpList":
                    if (comcod == "3365")
                    {
                        this.desFrom.Visible = false;
                        this.desTo.Visible = false;
                    }
                    this.lblfrmdate.Visible = false;
                    this.txtFdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txtTdate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "TransList":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "PenEmpCon":
                case "EmpCon":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "Manpower":
                    this.lblDept.Visible = false;

                    this.divSection.Visible = false;
                    this.lblfrmd.Visible = false;

                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;

                    this.ddlToDesig.Visible = false;
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "SepType":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.SepType.Visible = true;
                    this.GetSepType();
                    break;
                case "EmpHold":
                    this.MultiView1.ActiveViewIndex = 7;
                    break;

                case "EmpGradeADesig":
                    this.lblfrmdate.Visible = false;
                    this.txtFdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txtTdate.Visible = false;
                    //this.lblfrmd.Visible = false;
                    //this.ddlfrmDesig.Visible = false;
                    //this.lbltdeg.Visible = false;
                    //this.ddlToDesig.Visible = false;
                    this.MultiView1.ActiveViewIndex = 8;
                    break;
                case "InActiveEmpList":

                    this.chkbdate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 10;
                    break;
                case "TotalEmplist":
                    if (comcod == "3365")
                    {
                        this.desFrom.Visible = false;
                        this.desTo.Visible = false;
                    }
                    this.lblfrmdate.Visible = false;
                    this.txtFdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txtTdate.Visible = false;
                    this.chkbdate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 11;
                    break;

                case "Pabx":
                    this.lblfrmdate.Visible = false;
                    this.txtFdate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txtTdate.Visible = false;
                    this.chkbdate.Visible = false;
                    this.withBirth.Visible = false;
                    this.MultiView1.ActiveViewIndex = 12;
                    break;

            }
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();

            string txtCompany = "%";
            //string callType = (this.Request.QueryString["Type"].ToString() == "Pabx") ? "GETCOMPANYNAMEW_WPERMISSION" : "GETCOMPANYNAME";


            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);


        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "sectionname";
            this.ddlProjectName.DataValueField = "section";
            this.ddlProjectName.DataSource = ds2.Tables[0];
            this.ddlProjectName.DataBind();


            //
            //string comcod = this.GetCompCode();
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            //string txtSProject = this.txtSrcPro.Text + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "DEPARTMENTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            //this.ddlProjectName.DataTextField = "departmentname";
            //this.ddlProjectName.DataValueField = "department";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();
        }
        private void GetDesignation()
        {

            string comcod = this.GetCompCode();
            string callType = (this.Request.QueryString["Type"].ToString() == "EmpGradeADesig") ? "GRADENAME" : "DESIGNAME";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", callType, "", "", "", "", "", "", "", "", "");
            Session["tbldesig"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlfrmDesig.DataTextField = "designation";
            this.ddlfrmDesig.DataValueField = "desigcod";
            this.ddlfrmDesig.DataSource = ds1.Tables[0];
            this.ddlfrmDesig.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "EmpGradeADesig")
            {
                this.ddlfrmDesig.SelectedValue = "0357000";
            }
            else
            {
                this.ddlfrmDesig.SelectedValue = "0357008";
            }
            this.GetDessignationTo();
        }
        private void GetDessignationTo()
        {

            DataTable dt = (DataTable)Session["tbldesig"];
            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "EmpGradeADesig")
            {
                this.ddlToDesig.SelectedValue = "0311000";
            }
            else
            {
                // this.ddlToDesig.SelectedValue = "0311001";
            }


        }
        private void GetSepType()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETSEAPRATIONTYPE", "", "", "", "", "", "", "", "", "");
            this.ddlSepType.DataTextField = "hrgdesc";
            this.ddlSepType.DataValueField = "hrgcod";
            this.ddlSepType.DataSource = ds1.Tables[0];
            this.ddlSepType.DataBind();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
            //this.GetProjectName();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.ShowData();
                    break;

                case "JoinigdWise":
                    this.GetEmpListJoiningDWise();
                    break;

                case "EmpList":
                    this.GetEmpList();
                    break;
                case "TransList":
                    this.GetTransList();
                    break;
                case "PenEmpCon":
                    this.GetPenConfirmation();
                    break;
                case "EmpCon":
                    this.GetEmpConfirmation();
                    break;
                case "Manpower":
                    this.GetEmpManPower();
                    break;
                case "SepType":
                    this.GetEmpSPList();
                    break;
                case "EmpHold":
                    this.GetEmpHoldList();
                    break;
                case "EmpGradeADesig":
                    this.GetEmpLowHighSal();
                    break;

                case "InActiveEmpList":
                    this.GetInActiveEmpList();
                    break;
                case "TotalEmplist":
                    this.GetTotalEmpList();
                    break;
                case "Pabx":
                    this.GetPabxEmpList(type);
                    break;


            }

        }
        private void ShowData()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "94%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();           
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";                   
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    //pnlDesig.Visible = false;
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }


                    string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETJOINSUMMARY", Fdate, Tdate, Company, Deptid, DesigFrom, DesigTo, "", "", "");
            if (ds4 == null)
            {
                this.grvJoinStat.DataSource = null;
                this.grvJoinStat.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private void GetEmpListJoiningDWise()
        {

            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            // string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                  
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }

            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETEMPLISTJDATEWISE", Company, Deptid, DesigFrom, DesigTo, Fdate, Tdate, "", "", "");
            if (ds4 == null)
            {
                this.gvJoinEmp.DataSource = null;
                this.gvJoinEmp.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }


        private void GetInActiveEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                    
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid

            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTALLINACTIVEEMPLIST", Company, Deptid, DesigFrom, DesigTo, Fdate, Todate, "", "", "");
            if (ds4 == null)
            {
                this.gvinacEmplist.DataSource = null;
                this.gvinacEmplist.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }
        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETALLACTIVEEMP", Company, Deptid, secid, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }
            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private void GetPabxEmpList(string type)
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETALLACTIVEEMP", Company, Deptid, secid, type, "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }
            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private void GetTotalEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                    
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "RPTTOTALEMPLIST", Company, Deptid, DesigFrom, DesigTo, "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvtemplist.DataSource = null;
                this.gvtemplist.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }
        private void GetTransList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                   
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    //pnlDesig.Visible = false;
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid

            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETEMPTRANSFERLIST", Fdate, Tdate, Company, Deptid, DesigFrom, DesigTo, "", "", "");
            if (ds4 == null)
            {
                this.grvTransList.DataSource = null;
                this.grvTransList.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }

        private void GetPenConfirmation()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTPENCONFIRMATION", Company, Deptid, DesigFrom, DesigTo, Fdate, Tdate, "", "", "");
            if (ds4 == null)
            {
                this.gvEmpCon.DataSource = null;
                this.gvEmpCon.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }

        private void GetEmpConfirmation()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTEMPCONFIRMATION", Company, Deptid, DesigFrom, DesigTo, Fdate, Tdate, "", "", "");
            if (ds4 == null)
            {
                this.gvEmpCon.DataSource = null;
                this.gvEmpCon.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }
        private void GetEmpManPower()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTEMPMANPOWER", Company, Fdate, Tdate, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.grvManPwr.DataSource = null;
                this.grvManPwr.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }
        private void GetEmpSPList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid



            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string sptype = (this.ddlSepType.SelectedValue.ToString() == "00000") ? "%" : this.ddlSepType.SelectedValue.ToString() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "SHOWEMPSPLIST", Fdate, Tdate, Company, Deptid, DesigFrom, DesigTo, sptype, "", "");
            if (ds4 == null)
            {
                this.grvEmpSep.DataSource = null;
                this.grvEmpSep.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }


        private void GetEmpHoldList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            switch (comcod)
            {
                case "3365":
                case "3348":
                case "3368":
                case "3366":
                case "3367":
                    DesigFrom = "0399999";
                    DesigTo = "0300001";
                    this.desFrom.Visible = false;
                    this.desTo.Visible = false;
                    break;
                default:
                    this.desFrom.Visible = true;
                    this.desTo.Visible = true;
                    break;
            }
            //emd nahid
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTPEMPHOLDLIST", Company, Deptid, DesigFrom, DesigTo, Fdate, Tdate, "", "", "");
            if (ds4 == null)
            {
                this.gvEmpHold.DataSource = null;
                this.gvEmpHold.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }
        private void GetEmpLowHighSal()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //added nahid
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            //emd nahid
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GRDEGWISESALARYDET", Company, Deptid, DesigFrom, DesigTo, "", "", "", "", "");
            if (ds4 == null)
            {
                this.grvEmpLHSal.DataSource = null;
                this.grvEmpLHSal.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string company, secid;
            switch (type)
            {
                case "joiningRpt":
                    string compcod = dt1.Rows[0]["compcod"].ToString();
                    string deptcod = dt1.Rows[0]["deptcod"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["compcod"].ToString() == compcod)
                        {
                            compcod = dt1.Rows[j]["compcod"].ToString();
                            dt1.Rows[j]["compname"] = "";
                        }


                        else
                        {

                            if (dt1.Rows[j]["deptcod"].ToString() == deptcod)
                            {
                                dt1.Rows[j]["deptname"] = "";

                            }

                            compcod = dt1.Rows[j]["compcod"].ToString();
                            deptcod = dt1.Rows[j]["deptcod"].ToString();
                        }

                    }
                    break;
                case "TotalEmplist":
                case "JoinigdWise":
                case "EmpList":
                case "InActiveEmpList":
                    company = dt1.Rows[0]["company"].ToString();
                    secid = dt1.Rows[0]["secid"].ToString();
                    string depcod = dt1.Rows[0]["depcod"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company )
                        {

                            dt1.Rows[j]["companyname"] = "";
                            
                        }
                        if (dt1.Rows[j]["secid"].ToString() == secid)
                        {
                            dt1.Rows[j]["section"] = "";
                        }
                        if (dt1.Rows[j]["depcod"].ToString() == depcod)
                        {
                            dt1.Rows[j]["deptname"] = "";
                        }
                        //&& dt1.Rows[j]["secid"].ToString() == secid
                        else
                        {
                            //if (dt1.Rows[j]["company"].ToString() == company)
                            //    dt1.Rows[j]["companyname"] = "";

                            //if (dt1.Rows[j]["secid"].ToString() == secid)
                            //    dt1.Rows[j]["section"] = "";
                        }


                        company = dt1.Rows[j]["company"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
                        depcod = dt1.Rows[j]["depcod"].ToString();
                    }

                    break;
                case "TransList":
                    break;


                case "PenEmpCon":
                case "EmpCon":
                case "EmpHold":
                case "EmpGradeADesig":
                    company = dt1.Rows[0]["company"].ToString();


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                            dt1.Rows[j]["companyname"] = "";

                        company = dt1.Rows[j]["company"].ToString();
                    }

                    break;
                case "Manpower":
                    company = dt1.Rows[0]["compcode"].ToString();
                    secid = dt1.Rows[0]["deptcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["compcode"].ToString() == company)
                            dt1.Rows[j]["compname"] = "";

                        if (dt1.Rows[j]["deptcode"].ToString() == secid)
                            dt1.Rows[j]["deptname"] = "";

                        company = dt1.Rows[j]["compcode"].ToString();
                        secid = dt1.Rows[j]["deptcode"].ToString();
                    }

                    break;
                case "SepType":
                    company = dt1.Rows[0]["company"].ToString();
                    secid = dt1.Rows[0]["section"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                            dt1.Rows[j]["compname"] = "";

                        if (dt1.Rows[j]["section"].ToString() == secid)
                            dt1.Rows[j]["secname"] = "";

                        company = dt1.Rows[j]["company"].ToString();
                        secid = dt1.Rows[j]["section"].ToString();
                    }
                    break;


            }

            return dt1;

        }



        private void LoadGrid()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            switch (type)
            {
                case "joiningRpt":
                    this.grvJoinStat.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvJoinStat.DataSource = dt;
                    this.grvJoinStat.DataBind();
                    break;

                case "JoinigdWise":
                    //this.gvJoinEmp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvJoinEmp.DataSource = dt;
                    this.gvJoinEmp.DataBind();
                    Session["Report1"] = gvJoinEmp;
                    if (dt.Rows.Count > 0)
                    {

                        ((Label)this.gvJoinEmp.FooterRow.FindControl("lblgvFsalary")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gssal)", "")) ? 0.00 : dt.Compute("Sum(gssal)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        ((HyperLink)this.gvJoinEmp.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }


                    break;

                case "EmpList":
                    this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpList.DataSource = dt;
                    this.gvEmpList.DataBind();


                    if (dt.Rows.Count > 0)
                    {

                        ((Label)this.gvEmpList.FooterRow.FindControl("lgvFlblgvemplist2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salary)", "")) ? 0.00 : dt.Compute("Sum(salary)", ""))).ToString("#,##0;(#,##0); ");

                        Session["Report1"] = gvEmpList;
                        ((HyperLink)this.gvEmpList.HeaderRow.FindControl("hlbtntbCdataExcelemplist")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }

                    break;
                case "TransList":
                    this.grvTransList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvTransList.DataSource = dt;
                    this.grvTransList.DataBind();
                    break;

                case "PenEmpCon":
                case "EmpCon":
                    this.gvEmpCon.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpCon.DataSource = dt;
                    this.gvEmpCon.DataBind();
                    break;
                case "Manpower":
                    this.grvManPwr.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvManPwr.DataSource = dt;
                    this.grvManPwr.DataBind();
                    break;
                case "SepType":
                    this.grvEmpSep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvEmpSep.DataSource = dt;
                    this.grvEmpSep.DataBind();

                    Session["Report1"] = grvEmpSep;
                    if (dt.Rows.Count > 0)
                    {

                        
                        ((HyperLink)this.grvEmpSep.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }

                    break;
                case "EmpHold":
                    this.gvEmpHold.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpHold.DataSource = dt;
                    this.gvEmpHold.DataBind();
                    break;
                case "EmpGradeADesig":
                    this.grvEmpLHSal.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvEmpLHSal.DataSource = dt;
                    this.grvEmpLHSal.DataBind();
                    break;

                case "InActiveEmpList":
                    this.gvinacEmplist.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvinacEmplist.DataSource = dt;
                    this.gvinacEmplist.DataBind();

                    Session["Report1"] = gvinacEmplist;
                    if (dt.Rows.Count > 0)
                    {


                         ((HyperLink)this.gvinacEmplist.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }


                    break;
                case "TotalEmplist":
                    this.gvtemplist.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtemplist.DataSource = dt;
                    this.gvtemplist.DataBind();

                    break;

                case "Pabx":
                    this.gvPabxInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPabxInfo.DataSource = dt;
                    this.gvPabxInfo.DataBind();

                    break;

            }

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.RptJoiningStatus();
                    break;

                case "JoinigdWise":
                    this.PrintEmpListJoiningDWise();
                    break;
                case "EmpList":
                    this.PrintEmpList();
                    break;
                case "TransList":
                    this.RptTransList();
                    break;


                case "PenEmpCon":
                case "EmpCon":
                    this.RptEmpConfirmation();
                    break;
                case "Manpower":
                    this.RptManPower();
                    break;
                case "SepType":
                    this.RptEmpSPList();
                    break;

                case "EmpHold":
                    this.RptEmpHoldList();
                    break;
                case "EmpGradeADesig":
                    this.RptEmpLowHighSalary();
                    break;
                case "InActiveEmpList":
                    this.PrintInActiveEmpList();
                    break;

                case "TotalEmplist":
                    this.PrintTotalEmpList();
                    break;

                case "Pabx":
                    this.PrintPabxEmpList();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Joining Roport Summary";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        private void PrintPabxEmpList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptPabxInfoList", list, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "List of PABX Information"));           
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptJoiningStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpSepList>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptJoiningStatus", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Joining Report Summary"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintEmpListJoiningDWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptDateWiseJoining();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtfrmdatetodate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintInActiveEmpList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();


            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>();

            LocalReport Rpt1 = new LocalReport();



            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptInactiveEmplists", list, null, null);




            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Resigning Employee List"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintTotalEmpList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RpTotalEmplists", list, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Total Employee List"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEmpList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmployeeInfo>();

            LocalReport Rpt1 = new LocalReport();

            if (chkbdate.Checked)
            {

                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpListBirthDateWise", list, null, null);
            }
            else
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpListJoinDateWise", list, null, null);

            }



            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //  Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", " Employee List"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Period: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptTransList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpTransList>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptTransList", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Transfer List Report"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptEmpConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptDateWiseEmpCon();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtTitle = rptstate.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = (this.Request.QueryString["Type"].ToString() == "PenEmpCon") ? "Pending Employee Confirmation List" : "Employee Confirmation List";
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtfrmdatetodate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptManPower()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptManpower();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptEmpSPList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpSepList>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptEmpSpList", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Separation List"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptEmpHoldList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.BO_ClassEmployee.EmpSepList>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_92_Mgt.RptDateWiseEmpHold", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Hold List"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptEmpLowHighSalary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptGradeADesgSalary();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtDate = rptstate.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtDept"] as TextObject;
            rptftdate.Text = "Department Name: " + this.ddlProjectName.SelectedItem.Text;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void grvJoinStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvJoinStat.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvJoinEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvJoinEmp.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDessignationTo();
        }
        protected void grvTransList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvTransList.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvEmpCon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpCon.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void grvManPwr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvManPwr.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void grvManPwr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label desg = (Label)e.Row.FindControl("lblgvdesignation");
                Label opening = (Label)e.Row.FindControl("lblgvOpening");
                Label joining = (Label)e.Row.FindControl("lblgvJoining");
                Label tin = (Label)e.Row.FindControl("lblgvnotrIn");
                Label tout = (Label)e.Row.FindControl("lblgvnotrout");
                Label dep = (Label)e.Row.FindControl("lblgvDep");
                Label tqty = (Label)e.Row.FindControl("lblgvTotal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desigid")).ToString().Trim();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    desg.Font.Bold = true;
                    opening.Font.Bold = true;
                    joining.Font.Bold = true;
                    tin.Font.Bold = true;
                    tout.Font.Bold = true;
                    dep.Font.Bold = true;
                    tqty.Font.Bold = true;
                    desg.Style.Add("text-align", "right");

                }
            }
        }
        protected void imgBtnSpType_Click(object sender, EventArgs e)
        {
            this.GetSepType();
        }

        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //string nozero = (hrcomln == 4) ? "0000" : "00";

            string txtCompanyname = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept =  "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetProjectName();
        }
        protected void gvEmpHold_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpHold.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void grvEmpLHSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvEmpLHSal.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvgwemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gvinacEmplist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gvtemplist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvtemplist.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void gvPabxInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPabxInfo.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ddlToDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpSPList();
        }
    }
}
