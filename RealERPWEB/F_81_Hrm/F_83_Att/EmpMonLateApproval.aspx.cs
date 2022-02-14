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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class EmpMonLateApproval : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                //this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                //this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetCompName();
                this.GetDesignation();
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "MLateAppDay") ? "Monthly Late  Approval Information"
                    : (this.Request.QueryString["Type"].ToString() == "MPunchAppDay") ? "Monthly One Time Punch Approval Information"
                    : (this.Request.QueryString["Type"].ToString() == "MEarlyleave") ? "Monthly Early Leave Approval Information"
                    : (this.Request.QueryString["Type"].ToString() == "LPAproval") ? "Monthly (L.P) Late Approval"
                    : "Monthly Absent Approval";
                this.ViewSaction();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        

        private void ViewSaction()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.MultiView1.ActiveViewIndex = 0;
                    string comcod = this.GetCompCode();
                    this.visibility();
                   
                    switch (comcod)
                    {

                        case "4301"://Sanmer
                        case "3332":                      
                        case "3338":

                            //case "4305"://Rupayan
                            this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            this.txtfrmDate.Text = "26" + this.txtfrmDate.Text.Trim().Substring(2);
                            this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;

                        

                        default:

                            this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                            this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                            // this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            //this.txtfromdate.Text = "26" + this.txtfromdate.Text.Trim().Substring(2);
                            ////this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            //this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            //this.txtfrmDate.Text = "01" + this.txtfrmDate.Text.Trim().Substring(2);
                            //this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;


                    }


                    break;
                case "MPunchAppDay":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    break;

                case "MabsentApp":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.lblfrmDesig.Visible = false;
                    this.ddlfrmDesig.Visible = false;
                    this.lbltoDesig.Visible = false;
                    this.ddlToDesig.Visible = false;
                    break;

                case "MEarlyleave":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;



                case "MabsentApp02":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;

                case "LPAproval":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.txtfrmDate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfrmDate.Text = startdate + this.txtfrmDate.Text.Trim().Substring(2);
                    this.txttoDate.Text = Convert.ToDateTime(this.txtfrmDate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.lblfrmDesig.Visible = false;
                    this.ddlfrmDesig.Visible = false;
                    this.lbltoDesig.Visible = false;
                    this.ddlToDesig.Visible = false;
                    break;
            }
        }

        private void visibility()
        {
            this.lblfrmDesig.Visible = false;
            this.ddlfrmDesig.Visible = false;
            this.lbltoDesig.Visible = false;
            this.ddlToDesig.Visible = false;

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompanyName_SelectedIndexChanged(null, null);
        }

        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            //string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string txtCompanyname = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";


            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }
        private void GetSection()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetCompCode();
            //string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string txtCompanyname = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Department = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETSECTIONDP", txtCompanyname, Department, txtSearchDept, "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "actdesc";
            this.DropCheck1.DataValueField = "actdesc";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();

        }


        private void GetDesignation()
        {

            string comcod = this.GetCompCode();
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


        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }
        protected void ibtnFindCompany_Click(object sender, EventArgs e)
        {
            this.GetCompName();
        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void imgbtnSection_Click(object sender, EventArgs e)
        {
            this.GetSection();
        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.txtfrmDate.Enabled = false;
                this.txttoDate.Enabled = false;
                this.ddlCompanyName.Visible = false;
                this.ddlDepartment.Visible = false;
                this.lblCompanyName.Visible = true;
                this.lblDeptDesc.Visible = true;


                this.lblPage.Visible = true;
                this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
                this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text;
                this.ShowData();
                return;
            }

            this.txtfrmDate.Enabled = true;
            this.txttoDate.Enabled = true;
            this.ddlCompanyName.Visible = true;
            this.ddlDepartment.Visible = true;
            this.lblCompanyName.Visible = false;
            this.lblDeptDesc.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.grvAdjDay.DataSource = null;
            this.grvAdjDay.DataBind();
            this.gvOPunch.DataSource = null;
            this.gvOPunch.DataBind();
            this.gvmapsapp.DataSource = null;
            this.gvmapsapp.DataBind();
            this.lnkbtnShow.Text = "Ok";
            this.lblCompanyName.Text = "";

        }

        private void ShowData()
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.ShowMonthlyLate();
                    break;
                case "MPunchAppDay":
                    this.ShowMPunchAppDay();
                    break;
                case "MabsentApp":
                    this.ShowMabsentApp();
                    break;

                case "MabsentApp02":
                    this.ShowMabsentApp02();
                    break;

                case "MEarlyleave":
                    this.ShowMEarlyLeave();
                    break;

                case "LPAproval":
                    this.ShowLPAproval();
                    break;
            }



        }

        private string selectcomp()
        {
            string comcod = this.GetCompCode();
            string calltype = "";

            switch (comcod)
            {
                // case "3333":
                case "3330":
                // case "3101":
                case "3332":
                    calltype = "EMPDAYADJUSTMENTMAN";
                    break;

                case "3365":
                    calltype = "EMPDAYADJUSTMENTBTI";
                    break;

                    

                default:
                    calltype = "EMPDAYADJUSTMENT";
                    break;
            }
            return calltype;
        }


        private void ShowLPAproval()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            //string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string compname = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 3) == "000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }


            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
           

            string frmdesig = "0399999";
            string todesig = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
                    todesig = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }

            //string calltype = this.selectcomp(); //comcod == "3332" ? "EMPDAYADJUSTMENTMAN" : "EMPDAYADJUSTMENT";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPLATEAPPROVALAFTERONEHOUR", compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, "");
            if (ds2 == null)
            {
                this.gvLPAproval.DataSource = null;
                this.gvLPAproval.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void ShowMonthlyLate()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            //string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string compname = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln)+"%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 3) == "000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }


            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            //string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            //string todesig = this.ddlToDesig.SelectedValue.ToString();

            string frmdesig = "0399999";
            string todesig = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
                    todesig = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }


            string calltype = this.selectcomp(); //comcod == "3332" ? "EMPDAYADJUSTMENTMAN" : "EMPDAYADJUSTMENT";


            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", calltype, compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, "");
            if (ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowMPunchAppDay()
        {

            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            //  string MonthId = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 4) == "0000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";

            string frmdesig = "0399999";
            string todesig = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
                    todesig = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMOPUNCHAPPROVAL", compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, "");
            if (ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowMabsentApp()
        {

            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 4) == "0000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";

            string frmdesig = "0399999";
            string todesig = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
                    todesig = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMONABSADJUSTMENT", compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, "");
            if (ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }


        private void ShowMabsentApp02()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 4) == "0000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";

            string frmdesig = "0399999";
            string todesig = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
                    todesig = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMONABSENTPUNC", compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, "");
            if (ds2 == null)
            {
                this.gvabsapp02.DataSource = null;
                this.gvabsapp02.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }
        private void ShowMEarlyLeave()
        {
            Session.Remove("tblover");
            string comcod = this.GetCompCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 3) == "000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }


            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";

            string frmdesig = "0399999";
            string todesig = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
                    todesig = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }



            //string calltype = this.selectcomp(); //comcod == "3332" ? "EMPDAYADJUSTMENTMAN" : "EMPDAYADJUSTMENT";


            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPEARLYLEAVE", compname, frmdate, todate, deptname, section, Empcode, todesig, frmdesig, "");
            if (ds2 == null)
            {
                this.gvEarlyleave.DataSource = null;
                this.gvEarlyleave.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string company = dt1.Rows[0]["company"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                {

                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                        dt1.Rows[j]["companyname"] = "";
                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";
                }

                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }
            return dt1;

        }

        private void SaveValue()
        {


            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;



            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":

                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {


                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtAdj")).Text.Trim());
                        double txtlvAdj = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtlvAdj")).Text.Trim());

                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        //  double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = dedday;
                        dt.Rows[rowindex]["leaveadj"] = txtlvAdj;


                    }
                    break;


                case "MPunchAppDay":
                    for (int i = 0; i < this.gvOPunch.Rows.Count; i++)
                    {
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.gvOPunch.Rows[i].FindControl("txtpAdj")).Text.Trim());
                        rowindex = (this.gvOPunch.PageSize) * (this.gvOPunch.PageIndex) + i;
                        dt.Rows[rowindex]["pdedday"] = dedday;

                    }
                    break;


                case "MabsentApp":
                    string comcod1 = this.GetCompCode();
                    switch (comcod1)
                    {
                        case "4101":
                        case "4315":
                        case "3365": //BTI


                            for (int i = 0; i < this.gvmapsapp.Rows.Count; i++)
                            {

                                double absday = Convert.ToDouble(((Label)this.gvmapsapp.Rows[i].FindControl("lblgvabsday")).Text.Trim());
                                double aprday = Convert.ToDouble("0" + ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabsaprday")).Text.Trim());
                                double lvadj = Convert.ToDouble("0" + ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabslvadj")).Text.Trim());
                                string reason =  ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabsreason")).Text.Trim();

                                rowindex = (this.gvmapsapp.PageSize) * (this.gvmapsapp.PageIndex) + i;
                                dt.Rows[rowindex]["aprday"] = aprday;
                                dt.Rows[rowindex]["leaveadj"] = lvadj;
                                dt.Rows[rowindex]["reason"] = reason;


                                dt.Rows[rowindex]["dedday"] = (absday - aprday);

                            }
                            break;


       


                        case "4305":

                            for (int i = 0; i < this.gvmapsapp.Rows.Count; i++)
                            {

                                double absday = Convert.ToDouble(((Label)this.gvmapsapp.Rows[i].FindControl("lblgvabsday")).Text.Trim());
                                double aprday = Convert.ToDouble("0" + ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabsaprday")).Text.Trim());

                                rowindex = (this.gvOPunch.PageSize) * (this.gvOPunch.PageIndex) + i;
                                dt.Rows[rowindex]["aprday"] = aprday;
                                dt.Rows[rowindex]["dedday"] = absday - aprday;

                            }

                            break;


                        default:

                            for (int i = 0; i < this.gvmapsapp.Rows.Count; i++)
                            {

                                double absday = Convert.ToDouble(((Label)this.gvmapsapp.Rows[i].FindControl("lblgvabsday")).Text.Trim());
                                double aprday = Convert.ToDouble("0" + ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabsaprday")).Text.Trim());
                                double lvadj = Convert.ToDouble("0" + ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabslvadj")).Text.Trim());
                                string reason = ((TextBox)this.gvmapsapp.Rows[i].FindControl("txtabsreason")).Text.Trim();

                                rowindex = (this.gvmapsapp.PageSize) * (this.gvmapsapp.PageIndex) + i;
                                dt.Rows[rowindex]["aprday"] = aprday;
                                dt.Rows[rowindex]["leaveadj"] = lvadj;
                                dt.Rows[rowindex]["reason"] = reason;


                                dt.Rows[rowindex]["dedday"] = (absday - aprday);

                            }
                            break;
                    }



                    break;


                case "MEarlyleave":
                    for (int i = 0; i < this.gvEarlyleave.Rows.Count; i++)
                    {
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtAdjustday")).Text.Trim());
                        rowindex = (this.gvEarlyleave.PageSize) * (this.gvEarlyleave.PageIndex) + i;
                        dt.Rows[rowindex]["dedday"] = dedday;

                    }




                    break;


                case "LPAproval":

                    for (int i = 0; i < this.gvLPAproval.Rows.Count; i++)
                    {


                        double delayday = Convert.ToDouble("0" + ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtLatedaylp")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtaprdaylp")).Text.Trim());
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtAdjlp")).Text.Trim());
                        double txtlvAdj = Convert.ToDouble("0" + ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtlvAdjlp")).Text.Trim());
                        string reason  =  ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtlpreason")).Text.Trim();


                        rowindex = (this.gvLPAproval.PageSize) * (this.gvLPAproval.PageIndex) + i;
                        //  double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = dedday;
                        dt.Rows[rowindex]["leaveadj"] = txtlvAdj;
                        dt.Rows[rowindex]["reason"] = reason;



                    }
                    break;

            }

            Session["tblover"] = dt;
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblover"];
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "MLateAppDay":
                    this.grvAdjDay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvAdjDay.DataSource = dt;
                    this.grvAdjDay.DataBind();

                    Session["Report1"] = grvAdjDay;
                    if (dt.Rows.Count > 0)
                        ((HyperLink)this.grvAdjDay.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                case "MPunchAppDay":
                    this.gvOPunch.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOPunch.DataSource = dt;
                    this.gvOPunch.DataBind();
                    break;

                case "MabsentApp":
                    this.gvmapsapp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmapsapp.DataSource = dt;
                    this.gvmapsapp.DataBind();
                    break;



                case "MEarlyleave":
                    this.gvEarlyleave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEarlyleave.DataSource = dt;
                    this.gvEarlyleave.DataBind();
                    break;

                case "MabsentApp02":
                    this.gvabsapp02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvabsapp02.DataSource = dt;
                    this.gvabsapp02.DataBind();
                    break;

                case "LPAproval":
                    this.gvLPAproval.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvLPAproval.DataSource = dt;
                    this.gvLPAproval.DataBind();
                    break;






            }






        }

        private string ComCalltype()
        {
            string comcod = this.GetCompCode();
            string Calltype;
            switch (comcod)
            {
                case "3336":
                case "3337":

                    Calltype = "INSERTORUPSUVEMPSALADJST";
                    break;


                default:
                    Calltype = "INSERTORUPEMPSALADJST";
                    break;


            }

            return Calltype;



        }


        protected void btnUpdateDayAdj_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;
            string ComCalltype = this.ComCalltype();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string delday = Convert.ToDouble("0" + dt.Rows[i]["delday"]).ToString();
                string aprday = Convert.ToDouble("0" + dt.Rows[i]["aprday"]).ToString();
                //string dedday = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.grvAdjDay.Items[i].FindControl("txtrptbillamt")).Text.Trim()));
                double dedday = Convert.ToDouble("0" + dt.Rows[i]["dedday"]);
                double leaveadj = Convert.ToDouble("0" + dt.Rows[i]["leaveadj"]);



                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", ComCalltype, monthid, empid, dedday.ToString(), delday, aprday, leaveadj.ToString(), "", "", "", "", "", "", "", "", "");

                if (!result)
                    return;
                //  }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lbtnTotalDay_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnCalCulationSadj_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;

            switch (comcod)
            {
                case "4101":
                case "4315":
                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = redelay / 2;

                    }
                    break;

                case "3333":
                case "3330":
                case "3332":
                case "3339": // Tropical Homes


                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);

                    }


                    break;


                case "3336":


                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = TodeductionDay((int)redelay);

                    }
                    break;



                case "3338":// ACME

                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = TodeductionDayacme((int)redelay);

                    }
                    break;

                case "3365":// BTI



                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;

                        double adjLev = ToAdjustLeaveDayBTI((int)redelay);
                        double adjElLev = ToAdjustLeaveDayBTI((int)redelay);



                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] =  Convert.ToInt32((redelay) / 3);
                        dt.Rows[rowindex]["leaveadj"] = adjLev;
                        dt.Rows[rowindex]["leaveadjel"] = 0;

                    }
                    break;

                default:
                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtLateday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);

                    }
                    break;




            }



            Session["tblover"] = dt;
            this.Data_Bind();
        }

        // Suvastu
        double TodeductionDay(int n)
        {

            int tr, r;
            double sum = 0;
            while (n > 2)
            {
                if (n >= 10)
                {
                    tr = n / 10;
                    r = n % 10;
                    sum = sum + (tr * 3);
                    n = r;
                }
                else if (n >= 8)
                {

                    r = n % 8;
                    sum = sum + 2;
                    n = r;
                }
                else if (n >= 5)
                {

                    r = n % 5;
                    sum = sum + 1;
                    n = r;
                }
                else if (n >= 3)
                {

                    r = n % 3;
                    sum = sum + 1;
                    n = r;
                }



            }
            return sum;

        }

        // ACME
        double TodeductionDayacme(int n)
        {

            int tr, r;
            double sum = 0;
            while (n >= 3)
            {
                if (n >= 10)
                {
                    tr = n / 10;
                    r = n % 10;
                    sum = sum + (tr * 3);
                    n = r;
                }
                else if (n >= 8)
                {

                    r = n % 8;
                    sum = sum + 2;
                    n = r;
                }
                else if (n >= 5)
                {

                    r = n % 5;
                    sum = sum + 1;
                    n = r;
                }
                else if (n >= 3)
                {

                    r = n % 3;
                    sum = sum + 0.5;
                    n = r;
                }



            }
            return sum;

        }


        // ACME
        double ToAdjustLeaveDayBTI(int n)
        {

            
            return n;

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void grvAdjDay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvAdjDay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_83_Att.RptMonthlyLateAttn02();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtMonth"] as TextObject;
            rptftdate.Text = "( From " + fromdate + " To " + todate + " )";
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["tblover"]);
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
            string fromdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");
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
        protected void lbtnTotalP_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void btnUpdatePunch_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string opunday = Convert.ToDouble("0" + dt.Rows[i]["opunchday"]).ToString();
                string paprday = Convert.ToDouble("0" + dt.Rows[i]["paprday"]).ToString();
                string pdedday = Convert.ToDouble("0" + dt.Rows[i]["pdedday"]).ToString();
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPOPUNCH", monthid, empid, opunday, paprday, pdedday, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void lbtnCalCulationPday_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.gvOPunch.Rows.Count; i++)
            {
                double Opunday = Convert.ToDouble("0" + ((Label)this.gvOPunch.Rows[i].FindControl("lblgvPunchDay")).Text.Trim());
                double PAprvday = Convert.ToDouble("0" + ((TextBox)this.gvOPunch.Rows[i].FindControl("txtpaprday")).Text.Trim());
                rowindex = (this.gvOPunch.PageSize) * (this.gvOPunch.PageIndex) + i;
                double redelay = Opunday - PAprvday;
                dt.Rows[rowindex]["paprday"] = PAprvday;
                dt.Rows[rowindex]["pdedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 2);

            }

            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void lbtnTotalabs_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            this.Data_Bind();
        }
        protected void btnUpdateAbsent_Click(object sender, EventArgs e)
        {

            this.SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;

            foreach (DataRow dr1 in dt.Rows)
            {
                string empid = dr1["empid"].ToString();
                string absday = Convert.ToDouble(dr1["absday"]).ToString();
                string aprday = Convert.ToDouble(dr1["aprday"]).ToString();
                string dedday = Convert.ToDouble(dr1["dedday"]).ToString();
                string leaveadj = Convert.ToDouble(dr1["leaveadj"]).ToString();
                string reason =(dr1["reason"]).ToString();


                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPABSENTADJ", monthid, empid, absday, aprday, dedday, leaveadj, reason, "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void grvAdjDay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvEarlyleave_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lbtnTotalELeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void btnUpdateEarly_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;
            ///--------------------------------------------------////////////
            //Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.rpconbilldet.Items[i].FindControl("txtrptbillamt")).Text.Trim()));
            //////----------------------------------------------------------/////////////
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string delday = Convert.ToDouble("0" + dt.Rows[i]["delday"]).ToString();
                string aprday = Convert.ToDouble("0" + dt.Rows[i]["aprday"]).ToString();
                //string dedday = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.grvAdjDay.Items[i].FindControl("txtrptbillamt")).Text.Trim()));
                double dedday = Convert.ToDouble("0" + dt.Rows[i]["dedday"]);
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPELEAVEADJST", monthid, empid, dedday.ToString(), delday, aprday, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lbtnCalEarly_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;

            switch (comcod)
            {
                case "4101":
                case "4315":
                    for (int i = 0; i < this.gvEarlyleave.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtEarlyday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtInformDay")).Text.Trim());
                        rowindex = (this.gvEarlyleave.PageSize) * (this.gvEarlyleave.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = redelay / 2;

                    }
                    break;

                case "3333":
                case "3330":
                case "3332":
                case "3101":

                    for (int i = 0; i < this.gvEarlyleave.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtEarlyday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtInformDay")).Text.Trim());
                        rowindex = (this.gvEarlyleave.PageSize) * (this.gvEarlyleave.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);

                    }

                    break;


                default:

                    for (int i = 0; i < this.gvEarlyleave.Rows.Count; i++)
                    {
                        double delayday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtEarlyday")).Text.Trim());
                        double Aprvday = Convert.ToDouble("0" + ((TextBox)this.gvEarlyleave.Rows[i].FindControl("txtInformDay")).Text.Trim());
                        rowindex = (this.gvEarlyleave.PageSize) * (this.gvEarlyleave.PageIndex) + i;
                        double redelay = delayday - Aprvday;
                        dt.Rows[rowindex]["delday"] = delayday;
                        dt.Rows[rowindex]["aprday"] = Aprvday;
                        dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);

                    }

                    break;








            }



            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void ModalUpdateBtn_Click(object sender, EventArgs e)
        {
            this.lblmsg.Visible = true;
            string comcod = this.GetCompCode();
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            for (int j = 0; j < mgvbreakdown.Rows.Count; j++)
            {


                if (((CheckBox)this.mgvbreakdown.Rows[j].FindControl("chkack")).Checked == true)
                {
                    string lateappsta = (((CheckBox)this.mgvbreakdown.Rows[j].FindControl("chkack")).Checked == true) ? "1" : "0";
                    string empid = Convert.ToString(((Label)this.mgvbreakdown.Rows[j].FindControl("mlgvEmpIdAdj")).Text.Trim());

                    string remarks = Convert.ToString(((TextBox)this.mgvbreakdown.Rows[j].FindControl("mTxtremarks")).Text.Trim());
                    string dayid = Convert.ToDateTime(((Label)this.mgvbreakdown.Rows[j].FindControl("mlblgvlateday")).Text).ToString("yyyyMMdd");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPDATEADJUSTB", dayid, empid, postDat, userid, remarks, lateappsta, "", "", "", "", "");


                    if (!result)
                    {

                        this.lblmsg.Text = "Updated Failed";
                        return;

                    }


                }




            }

            this.lblmsg.Text = "Updated Successfully";
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);


        }
        protected void lblgvdeptandemployeeemp_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            this.lbmodalheading.Text = "Individual Monthly Late Approval Details Information. Date :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = ((Label)this.grvAdjDay.Rows[index].FindControl("lgvEmpIdAdj")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "EMPLATEATTENDETAILSINDIVIDUAL", frmdate, todate, Empcode);
            if (ds2 == null)
            {
                this.mgvbreakdown.DataSource = null;
                this.mgvbreakdown.DataBind();
                return;
            }
            this.mgvbreakdown.DataSource = ds2.Tables[0];
            this.mgvbreakdown.DataBind();
            Session["Report1"] = mgvbreakdown;
            if (ds2.Tables[0].Rows.Count > 0)
                ((HyperLink)this.mgvbreakdown.HeaderRow.FindControl("mhlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void lblgvEarlydept_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.lblearlylvapp.Text = "Individual Monthly Early Leave Approval Details Information. Date :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = ((Label)this.gvEarlyleave.Rows[index].FindControl("lgvEarlyempid")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPEARLYLEAVEAPPATEATTENINDIVIDUAL", frmdate, todate, Empcode);
            if (ds2 == null)
            {
                this.gvearleave.DataSource = null;
                this.gvearleave.DataBind();
                return;
            }
            this.gvearleave.DataSource = ds2.Tables[0];
            this.gvearleave.DataBind();
            Session["Report1"] = gvearleave;
            if (ds2.Tables[0].Rows.Count > 0)
                ((HyperLink)this.gvearleave.HeaderRow.FindControl("mhlbtntbCdataExelp")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "Openearleavemodal();", true);
            Session["tblearlyleave"] = ds2.Tables[0];
        }

        private void SaveGridValue()
        {
            DataTable dt = (DataTable)Session["tblearlyleave"];
            int TblRowIndex;
            for (int i = 0; i < this.gvearleave.Rows.Count; i++)
            {
                string empid = ((Label)this.gvearleave.Rows[i].FindControl("mlgvEmpIdAdjp")).Text.ToString();
                string dayid = ((Label)this.gvearleave.Rows[i].FindControl("mlblgvlatedayp")).Text.ToString();
                string earleavapp = ((CheckBox)this.gvearleave.Rows[i].FindControl("chkack")).Checked.ToString();
                string rmrks = ((TextBox)this.gvearleave.Rows[i].FindControl("mTxtremarksp")).Text.ToString();


                TblRowIndex = (gvearleave.PageIndex) * gvearleave.PageSize + i;
                dt.Rows[TblRowIndex]["empid"] = empid;
                dt.Rows[TblRowIndex]["cdate"] = dayid;
                dt.Rows[TblRowIndex]["earleaveapp"] = earleavapp;
                dt.Rows[TblRowIndex]["rmrks"] = rmrks;
            }
            Session["tblearlyleave"] = dt;

        }
        protected void lnkearapp_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string appdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.SaveGridValue();

            DataTable dt = (DataTable)Session["tblearlyleave"];
            string comcod = this.GetCompCode();

            foreach (DataRow dr1 in dt.Rows)
            {
                string empid = dr1["empid"].ToString();
                string dayid = Convert.ToDateTime(dr1["cdate"].ToString()).ToString("yyyyMMdd");
                string earleaveapp = dr1["earleaveapp"].ToString();
                string rmrks = dr1["rmrks"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "UPDATEEARLEAVEAPP", dayid, empid, earleaveapp, userid, appdate, rmrks, "", "", "", "", "", "", "", "", "");

                if (result == true)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                }

            }


        }
        protected void hlnkgvdeptandemployeeempabs02_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            this.lblabsheading.Text = "Individual Monthly Absent Approval Details Information. Date :" + this.txtfrmDate.Text.ToString() + " To: " + this.txttoDate.Text.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;

            string frmdate = this.txtfrmDate.Text.Trim();
            string todate = this.txttoDate.Text.Trim();
            string Empcode = ((Label)this.gvabsapp02.Rows[index].FindControl("lgvEmpIdabs02")).Text.ToString(); // "%" + this.txtSrcEmployee.Text.Trim() + "%";
            string frmdesig = this.ddlfrmDesig.SelectedValue.ToString();
            string todesig = this.ddlToDesig.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "EMPMONABSENT", frmdate, todate, Empcode);
            if (ds2 == null)
            {
                this.mgvmonabsent.DataSource = null;
                this.mgvmonabsent.DataBind();
                return;
            }
            this.mgvmonabsent.DataSource = ds2.Tables[0];
            this.mgvmonabsent.DataBind();
            Session["Report1"] = mgvbreakdown;
            if (ds2.Tables[0].Rows.Count > 0)
                ((HyperLink)this.mgvmonabsent.HeaderRow.FindControl("mhlbtntbCdataExelabs02")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModalAbs();", true);

        }
        protected void lbntnAbsentApproval_Click(object sender, EventArgs e)
        {

            this.lblmsg.Visible = true;
            string comcod = this.GetCompCode();
            bool result = false;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            foreach (GridViewRow gv1 in mgvmonabsent.Rows)
            {


                if (((CheckBox)gv1.FindControl("lblchkaabs02")).Checked == true)
                {

                    string empid = Convert.ToString(((Label)gv1.FindControl("mlgvEmpIdabs02")).Text.Trim());
                    string remarks = Convert.ToString(((TextBox)gv1.FindControl("lblgvremarks")).Text.Trim());
                    string dayid = Convert.ToDateTime(((Label)gv1.FindControl("lgvabsday")).Text).ToString("yyyyMMdd");
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPDATEOFFTIMEANDDELABSENT", dayid, empid, remarks, "", "", "", "", "", "");


                    if (!result)
                    {

                        this.lblmsg.Text = "Updated Failed";
                        return;

                    }


                }




            }

            this.lblmsg.Text = "Updated Successfully";

        }

        protected void lblgvdeptandemployeeempLP_Click(object sender, EventArgs e)
        {

            

        }

        protected void btnUpdateDayAdjlp_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetCompCode();
            string monthid = Convert.ToDateTime(this.txttoDate.Text.Trim()).ToString("yyyyMM");
            bool result = false;
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string delday = Convert.ToDouble("0" + dt.Rows[i]["delday"]).ToString();
                string aprday = Convert.ToDouble("0" + dt.Rows[i]["aprday"]).ToString();
                //string dedday = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.grvAdjDay.Items[i].FindControl("txtrptbillamt")).Text.Trim()));
                double dedday = Convert.ToDouble("0" + dt.Rows[i]["dedday"]);
                double leaveadj = Convert.ToDouble("0" + dt.Rows[i]["leaveadj"]);
                string reason =  dt.Rows[i]["reason"].ToString();

                


                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPLPAPPROVAL", monthid, empid, dedday.ToString(), delday, aprday, leaveadj.ToString(), reason, "", "", "", "", "", "", "", "");

                if (!result)
                    return;
                //  }
            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void lbtnCalCulationSadjLP_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;


            for (int i = 0; i < this.gvLPAproval.Rows.Count; i++)
            {
                double delayday = Convert.ToDouble("0" + ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtLatedaylp")).Text.Trim());
                double Aprvday = Convert.ToDouble("0" + ((TextBox)this.gvLPAproval.Rows[i].FindControl("txtaprdaylp")).Text.Trim());
                rowindex = (this.gvLPAproval.PageSize) * (this.gvLPAproval.PageIndex) + i;
                double redelay = delayday - Aprvday;
                dt.Rows[rowindex]["delday"] = delayday;
                dt.Rows[rowindex]["aprday"] = Aprvday;
                dt.Rows[rowindex]["dedday"] = Convert.ToDouble(Convert.ToDouble(redelay) / 2);
         

            }

            Session["tblover"] = dt;
            this.Data_Bind();


        }
    }
}