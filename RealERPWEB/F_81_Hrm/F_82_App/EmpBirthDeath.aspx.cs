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
namespace RealERPWEB.F_81_Hrm.F_82_App
{


    public partial class EmpBirthDeath : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                Session.Remove("tblEmpstatus");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompany();
                this.SelectView();
                this.GetDesignation();
                string Type = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "joiningRpt") ? "Joining Report Summary" : (Type == "JoinigdWise") ? "New Joiners List"
                //    : (Type == "EmpBirthdayList") ? "Employee Birthday" : (Type == "TransList") ? "Employee Transfer List"
                //    : (Type == "PenEmpCon") ? "Pending Employee Confirmation" : (Type == "SepType") ? "Employee Sparation List Report"
                //    : (Type == "EmpHold") ? "Employee Hold List" : (Type == "Manpower") ? "Employee Manpower List"
                //    : (Type == "EmpGradeADesig") ? "Grade & Designation Wise  Salary Detail" : "Employee Confirmation";


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
            return (hst["comcod"].ToString());

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "JoinigdWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "EmpBirthdayList":
                    this.lblfrmdate.Visible = true;
                    this.txtFdate.Visible = true;
                    this.lbltodate.Visible = true;
                    this.txtTdate.Visible = true;
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
                    this.txtSrcPro.Visible = false;
                    this.ibtnFindProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.lblfrmd.Visible = false;
                    this.ddlfrmDesig.Visible = false;
                    this.lbltdeg.Visible = false;
                    this.ddlToDesig.Visible = false;
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "SepType":
                    this.MultiView1.ActiveViewIndex = 6;
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



            }
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();

            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);


        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = this.txtSrcPro.Text + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "DEPARTMENTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "departmentname";
            this.ddlProjectName.DataValueField = "department";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
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
                this.ddlToDesig.SelectedValue = "0311001";
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
            this.GetProjectName();
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

                case "EmpBirthdayList":
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



            }

        }
        private void ShowData()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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

        private void GetEmpList()
        {

            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Tdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "RPTDEATHWISHBIRTHDAY", Company, Deptid, DesigFrom, DesigTo, Fdate, Tdate, "", "", "");
            if (ds4 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }
        private void GetTransList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
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
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string Deptid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string GradeFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string GradeTo = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GRDEGWISESALARYDET", Company, Deptid, GradeFrom, GradeTo, "", "", "", "", "");
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

                case "JoinigdWise":
                case "EmpBirthdayList":
                    //company = dt1.Rows[0]["company"].ToString();
                    //secid = dt1.Rows[0]["secid"].ToString();

                    //   for (int j = 1; j < dt1.Rows.Count; j++)
                    //   {
                    //       if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                    //       {

                    //           dt1.Rows[j]["companyname"] = "";
                    //           dt1.Rows[j]["section"] = "";
                    //       }

                    //       else
                    //       {
                    //           if (dt1.Rows[j]["company"].ToString() == company)
                    //               dt1.Rows[j]["companyname"] = "";                          

                    //           if (dt1.Rows[j]["secid"].ToString() == secid)
                    //               dt1.Rows[j]["secton"] = "";
                    //       }


                    //       company = dt1.Rows[j]["company"].ToString();
                    //       secid = dt1.Rows[j]["secid"].ToString();
                    //   }

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
                    this.gvJoinEmp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvJoinEmp.DataSource = dt;
                    this.gvJoinEmp.DataBind();
                    break;

                case "EmpBirthdayList":
                    this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpList.DataSource = dt;
                    this.gvEmpList.DataBind();
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

            }

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "joiningRpt":
                    this.RptJoiningStatus();
                    break;

                case "JoinigdWise":
                    this.PrintEmpListJoiningDWise();
                    break;
                case "EmpBirthdayList":
                    this.PrintEmpBirthdayList();
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
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Joining Roport Summary";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void RptJoiningStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptJoiningStatus();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEmpListJoiningDWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEmpBirthdayList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");

            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_82_App.RptEmpListJBirthDayWise();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtfrmdatetodate"] as TextObject;
            rptftdate.Text = "Date: " + fromdate + " To " + todate;

            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblEmpstatus"]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;



            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptTransList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptTransList();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptEmpConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptManPower()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptEmpSPList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptEmpSpList();
            DataTable dt = (DataTable)Session["tblEmpstatus"];
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptEmpHoldList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptDateWiseEmpHold();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptEmpLowHighSalary()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
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
        protected void imgBtnSpType_Click(object sender, ImageClickEventArgs e)
        {
            this.GetSepType();
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
    }
}
