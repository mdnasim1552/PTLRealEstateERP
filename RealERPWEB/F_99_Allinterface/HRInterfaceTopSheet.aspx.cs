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
namespace RealERPWEB.F_99_Allinterface
{
    public partial class HRInterfaceTopSheet : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError");

                //DataRow[] dr1 = ASTUtility.PagePermission1 (HttpContext.Current.Request.Url.AbsoluteUri.ToString (), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl ("lnkPrint")).Enabled = (Convert.ToBoolean (dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Salary  360 <sup>0</span>";
                //     : (this.Request.QueryString["Type"].ToString ().Trim () == "BankPayment") ? "BANK PAYMENT INFORMATION"
                //    : (this.Request.QueryString["Type"].ToString ().Trim () == "Holiday") ? "EMPLOYEE HOLIDAY ALLOWANCE"
                //    : (this.Request.QueryString["Type"].ToString ().Trim () == "Mobile") ? "EMPLOYEE MOBILE BILL ALLOWANCE"
                //    : (this.Request.QueryString["Type"].ToString ().Trim () == "Lencashment") ? "LEAVE ENCASHMENT"
                //     : (this.Request.QueryString["Type"].ToString ().Trim () == "OtherDeduction") ? "EMPLOYEE OTHER DEDCUTION"
                //     : (this.Request.QueryString["Type"].ToString ().Trim () == "loan") ? "EMPLOYEE LOAN INFORMATION"
                //     : (this.Request.QueryString["Type"].ToString ().Trim () == "dayadj") ? "Salary Adjustment" : "EMPLOYEE ARREAR INFORMATION";
                this.ViewVisibility();
                this.GetCompName();
                this.GetYearMonth();



            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        //private void GetDepartment()
        //{

        //    string comcod = this.GetComeCode();
        //    string txtDepartment = this.txtSrcDept.Text.Trim() + "%";
        //    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtDepartment, "", "", "", "", "", "", "", "");
        //    this.ddlDeptName.DataTextField = "actdesc";
        //    this.ddlDeptName.DataValueField = "actcode";
        //    this.ddlDeptName.DataSource = ds1.Tables[0];
        //    this.ddlDeptName.DataBind();

        //}

        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }

        private void GetPreYearMonth()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlpreyearmon.DataTextField = "yearmon";
            this.ddlpreyearmon.DataValueField = "ymon";
            this.ddlpreyearmon.DataSource = ds1.Tables[0];
            this.ddlpreyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlpreyearmon.DataBind();

            ds1.Dispose();
        }

        private void GetPreYearMonthother()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlpreyearmonoth.DataTextField = "yearmon";
            this.ddlpreyearmonoth.DataValueField = "ymon";
            this.ddlpreyearmonoth.DataSource = ds1.Tables[0];
            this.ddlpreyearmonoth.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlpreyearmonoth.DataBind();

            ds1.Dispose();
        }



        private void GetCompName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            this.ddlCompanyName_SelectedIndexChanged(null, null);
        }
        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        private void SectionName()
        {

            string comcod = this.GetComeCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }
        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {


            string type = this.ddlType.SelectedValue.ToString();
            switch (type)
            {
                case "Overtime":
                    this.ShowOvertime();
                    break;
                case "BankPayment":
                    this.ShowBankPay();
                    break;
                case "Holiday":
                    this.ShowEmpHolidayInfo();
                    break;

                case "Mobile":
                    this.EmpMobileBill();
                    break;


                case "Lencashment":
                    this.ShowLeaveEncashment();
                    break;

                case "OtherDeduction":
                    this.ShowOtherDeduction();
                    break;

                case "loan":
                    this.ShowEmpLoan();
                    break;
                case "arrear":
                    this.EmpArrearSalary();
                    break;
                case "otherearn":
                    this.OtherEarning();
                    break;
                case "dayadj":
                    this.SalaryDayAdj();
                    break;

            }
        }
        private void ViewVisibility()
        {
            string type = this.ddlType.SelectedValue;
            switch (type)
            {
                case "Overtime":
                    break;
                case "BankPayment":
                    break;

                case "Holiday":
                    break;

                case "Mobile":
                    this.lbldate.Text = "Month Id:";
                    this.rbtnlistsaltype.Visible = true;
                    // this.txtDate.MaxLength = 6;
                    break;

                case "Lencashment":
                    break;

                case "OtherDeduction":

                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    // this.txtDate.MaxLength = 6;
                    break;

                case "loan":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    break;
                case "arrear":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    break;

                case "otherearn":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    break;
                case "dayadj":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    break;
            }


        }

        protected void gvEmpOverTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpOverTime.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            this.GetCompName();

        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.ddlyearmon.Enabled = false;
                this.ddlCompanyName.Visible = false;
                this.ddlDepartment.Visible = false;
                this.ddlSection.Enabled = false;
                this.ddlType.Visible = false;
                this.lblCompanyName.Visible = true;
                this.lblDeptDesc.Visible = true;
                this.lblType.Visible = true;

                //this.lblPage.Visible = true;
                //this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
                this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text;
                this.lblType.Text = this.ddlType.SelectedItem.Text;

                this.SectionView();
                return;
            }
            this.MultiView1.ActiveViewIndex = -1;
            this.ddlyearmon.Enabled = true;
            this.ddlCompanyName.Visible = true;
            this.ddlSection.Enabled = true;
            this.ddlType.Visible = true;
            this.ddlDepartment.Visible = true;
            this.lblCompanyName.Visible = false;
            this.lblDeptDesc.Visible = false;
            this.lblType.Visible = false;
            //this.lblPage.Visible = false;
            //this.ddlpagesize.Visible = false;
            this.gvEmpOverTime.DataSource = null;
            this.gvEmpOverTime.DataBind();
            this.lnkbtnShow.Text = "Ok";
            this.lblCompanyName.Text = "";



        }


        private void SectionView()
        {

            string type = this.ddlType.SelectedValue.ToString();
            switch (type)
            {
                case "Overtime":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowOvertime();
                    break;
                case "BankPayment":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowBankPay();
                    break;
                case "Holiday":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowEmpHolidayInfo();
                    break;

                case "Mobile":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.EmpMobileBill();
                    break;


                case "Lencashment":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowLeaveEncashment();
                    break;

                case "OtherDeduction":
                    this.MultiView1.ActiveViewIndex = 5;

                    this.ShowOtherDeduction();
                    break;

                case "loan":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowEmpLoan();
                    break;
                case "arrear":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.EmpArrearSalary();
                    break;
                case "otherearn":
                    this.MultiView1.ActiveViewIndex = 8;
                    this.OtherEarning();
                    break;
                case "dayadj":
                    this.MultiView1.ActiveViewIndex = 9;
                    this.SalaryDayAdj();
                    break;

            }

        }


        private void ShowOvertime()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPALLOYOVERTIME", deptname, dayid, txtdate, comnam, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvEmpOverTime.DataSource = null;
                this.gvEmpOverTime.DataBind();
                return;
            }
            Session["tblover"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }


        private void ShowBankPay()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.ddlyearmon.Text.Trim()).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "BANKPAYINFO", deptname, todate, comnam, Empcode, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBankPay.DataSource = null;
                this.gvBankPay.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblover"] = dt;
            this.Data_Bind();
        }

        private void ShowEmpHolidayInfo()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";

            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "HOLIDAYEMPALLOYEE", deptname, dayid, txtdate, comnam, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvEmpHoliday.DataSource = null;
                this.gvEmpHoliday.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }
        private void EmpMobileBill()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : "86%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";


            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMOBILEBILLINFO", deptname, MonthId, date, comnam, Empcode, mantype, section, "", "");
            if (ds2 == null)
            {
                this.gvEmpMbill.DataSource = null;
                this.gvEmpMbill.DataBind();
                return;
            }
            Session["tblover"] = HiddenSameData(ds2.Tables[0]);

            this.Data_Bind();


        }
        //txtDate

        private void ShowLeaveEncashment()
        {

            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "LEAVEENCASHMENT", deptname, dayid, txtdate, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpELeave.DataSource = null;
                this.gvEmpELeave.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowOtherDeduction()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHERDEDUCTION", deptname, MonthId, date, comnam, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvEmpOtherded.DataSource = null;
                this.gvEmpOtherded.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void ShowEmpLoan()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPLOANEDUCTION", deptname, MonthId, date, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpOtherded.DataSource = null;
                this.gvEmpOtherded.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void EmpArrearSalary()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPARREARSALARY", compname, MonthId, date, deptname, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvarrear.DataSource = null;
                this.gvarrear.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }

        private void OtherEarning()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHEARNING", compname, MonthId, date, deptname, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvothearn.DataSource = null;
                this.gvothearn.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void SalaryDayAdj()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPDAYADJUSTMENT", compname, MonthId, date, deptname, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.grvAdjDay.DataSource = null;
                this.grvAdjDay.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblover"];
            string type = this.ddlType.SelectedValue;
            switch (type)
            {
                case "Overtime":
                    this.gvEmpOverTime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpOverTime.DataSource = dt;
                    this.gvEmpOverTime.DataBind();
                    this.EnabledOrVissible();
                    this.FooterCalculation();
                    break;

                case "BankPayment":
                    this.gvBankPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBankPay.DataSource = dt;
                    this.gvBankPay.DataBind();
                    if (dt.Rows.Count != 0)
                    {
                        this.FooterCalculation();
                    }

                    break;

                case "Holiday":
                    this.gvEmpHoliday.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpHoliday.DataSource = dt;
                    this.gvEmpHoliday.DataBind();
                    break;

                case "Mobile":
                    this.gvEmpMbill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpMbill.DataSource = dt;
                    this.gvEmpMbill.DataBind();
                    this.FooterCalculation();
                    break;


                case "Lencashment":
                    this.gvEmpELeave.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpELeave.DataSource = dt;
                    this.gvEmpELeave.DataBind();
                    break;

                case "OtherDeduction":
                    this.gvEmpOtherded.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpOtherded.DataSource = dt;
                    this.gvEmpOtherded.DataBind();
                    this.FooterCalculation();
                    break;



                case "loan":
                    this.gvEmploan.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmploan.DataSource = dt;
                    this.gvEmploan.DataBind();
                    this.FooterCalculation();
                    break;

                case "arrear":
                    //this.gvarrear.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvarrear.DataSource = dt;
                    this.gvarrear.DataBind();
                    this.FooterCalculation();
                    break;
                case "otherearn":
                    this.gvothearn.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvothearn.DataSource = dt;
                    if (comcod == "3339" || comcod == "3101")
                    {
                        gvothearn.Columns[9].HeaderText = "Trans/Entr";

                    }
                    this.gvothearn.DataBind();
                    this.FooterCalculation();
                    if (comcod == "3101" || comcod == "3347")
                    {
                        this.gvothearn.Columns[11].Visible = true;
                       
                    }                   
                    break;

                case "dayadj":
                    this.grvAdjDay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvAdjDay.DataSource = dt;
                    this.grvAdjDay.DataBind();
                    this.FooterCalculation();
                    break;


            }



        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.ddlType.SelectedValue;
            switch (type)
            {
                case "Overtime":
                    ((Label)this.gvEmpOverTime.FooterRow.FindControl("lgvFhour")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tohour)", "")) ? 0.00
                        : dt.Compute("sum(tohour)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;
                case "BankPayment":
                    ((Label)this.gvBankPay.FooterRow.FindControl("lgvFBamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00
                            : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "Holiday":
                    break;

                case "Mobile":
                    ((Label)this.gvEmpMbill.FooterRow.FindControl("lgvFMbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbillamt)", "")) ? 0.00
                        : dt.Compute("sum(mbillamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvEmpMbill.FooterRow.FindControl("lgvFMbilllimit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbilllimit)", "")) ? 0.00
                        : dt.Compute("sum(mbilllimit)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvEmpMbill.FooterRow.FindControl("lgvFMbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(intbill)", "")) ? 0.00
                       : dt.Compute("sum(intbill)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    break;


                case "OtherDeduction":
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFleaveded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(lvded)", "")) ? 0.00
                            : dt.Compute("sum(lvded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFTarrearded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arded)", "")) ? 0.00
                           : dt.Compute("sum(arded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFSaladv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(saladv)", "")) ? 0.00
                            : dt.Compute("sum(saladv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFotherded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otherded)", "")) ? 0.00
                            : dt.Compute("sum(otherded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00
                            : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFoterded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fallded)", "")) ? 0.00
                          : dt.Compute("sum(fallded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFotermbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbillded)", "")) ? 0.00
                         : dt.Compute("sum(mbillded)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFoterTransDed")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(transded)", "")) ? 0.00
     : dt.Compute("sum(transded)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "loan":
                    ((Label)this.gvEmploan.FooterRow.FindControl("lblgvFLToamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ? 0.00
                            : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "arrear":
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvFarrearamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aramt)", "")) ? 0.00
                            : dt.Compute("sum(aramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvPFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfamt)", "")) ? 0.00
                            : dt.Compute("sum(pfamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvarrear.FooterRow.FindControl("lgvAPFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tapfamt)", "")) ? 0.00
                            : dt.Compute("sum(tapfamt)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvarrear;
                    ((HyperLink)this.gvarrear.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer?PrintOpt=GRIDTOEXCEL";

                    break;

                case "otherearn":

                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFtpallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tptallow)", "")) ? 0.00
                           : dt.Compute("sum(tptallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFkpi")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(kpi)", "")) ? 0.00
                           : dt.Compute("sum(kpi)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFperbon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(perbon)", "")) ? 0.00
                           : dt.Compute("sum(perbon)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFotherearn")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othearn)", "")) ? 0.00
                           : dt.Compute("sum(othearn)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFtotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalam)", "")) ? 0.00
                            : dt.Compute("sum(totalam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFhaircut")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(haircutal)", "")) ? 0.00
                            : dt.Compute("sum(haircutal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFfood")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(foodal)", "")) ? 0.00
                            : dt.Compute("sum(foodal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFnightfood")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nfoodal)", "")) ? 0.00
                            : dt.Compute("sum(nfoodal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFtakenday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(factualday)", "")) ? 0.00
                          : dt.Compute("sum(factualday)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "dayadj":
                    ((Label)this.grvAdjDay.FooterRow.FindControl("lgvFDelday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dalday)", "")) ? 0.00
                        : dt.Compute("sum(dalday)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAdjDay.FooterRow.FindControl("lgvFAdj")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedday)", "")) ? 0.00
                        : dt.Compute("sum(dedday)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }


        }
        private void EnabledOrVissible()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {

                case "4301":

                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
                    }
                    break;


                case "4101":
                case "4305":

                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = true;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = false;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = false;
                    }

                    break;


                default:
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
                    }
                    break;

            }






        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.ddlType.SelectedValue.ToString();
            switch (type)
            {
                case "BankPayment":
                    this.rptBankPayment();
                    break;

                case "Mobile":
                    this.rptMobileAllowance();
                    break;

            }
        }
        private void rptBankPayment()
        {

            DataTable dt = (DataTable)Session["tbbank"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string companyname = ddlCompanyName.SelectedItem.Text.Trim().Substring(13);
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = this.ddlyearmon.Text.ToString().Trim();//txtdate

            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_84_Lea.rptBankPayment();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = companyname;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Salary for the Month of " + frmdate;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;


                case "3330":
                    vouprint = "VocherPrint6";
                    break;


                case "3332":

                    vouprint = "VocherPrintIns";
                    break;


                //case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;
                case "3101":
                case "3336":
                case "3337":
                    vouprint = "VocherPrintSuvastu";
                    break;



                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }


        private void rptMobileAllowance()
        {
            DataTable dt = ((DataTable)Session["tblover"]).Copy();

            string dptName = ddlDepartment.SelectedItem.Text.Trim().Substring(13);
            string monthid = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("MMM-yyyy");// txtdate


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = new List<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();

            switch (comcod)
            {

                case "3333":// Alliance
                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowAlli", lst, null, null);
                    break;


                case "3339":// Tropical
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("mbillamt>0");
                    dt = dv.ToTable();
                    //this.MobileAllowTropical();
                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowanceTro", lst, null, null);
                    break;


                case "3338": // ACME
                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowAcme", lst, null, null);
                    break;


                case "3315":// Assure

                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowanceAssure", lst, null, null);
                    break;

                default:
                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowance", lst, null, null);
                    break;

            }


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Mobile Allowance"));
            Rpt1.SetParameters(new ReportParameter("txtDpt", "Department : " + dptName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "The Month of " + monthid));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";






            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //switch (comcod)
            //{

            //    case "3333":
            //        this.MobileAllowAlli();
            //        break;

            //    // case "3101":
            //    case "3339":

            //        this.MobileAllowTropical();
            //        break;
            //    case "3101":
            //    case "3338":

            //        this.MobileAllowAcme();
            //        break;

            //    default:
            //        this.GetGeneral();
            //        break;

            //}

        }
        private void MobileAllowAlli()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString ();
            //string comadd = hst["comadd1"].ToString ();
            //string compname = hst["compname"].ToString ();
            //string username = hst["username"].ToString ();
            //string companyname = ddlCompanyName.SelectedItem.Text.Trim ();
            //string dptName = ddlDepartment.SelectedItem.Text.Trim ().Substring (13);
            //string monthid = Convert.ToDateTime (ASTUtility.Right (this.ddlyearmon.Text.Trim (), 2) + "/01/" + this.ddlyearmon.Text.Trim ().Substring (0, 4)).ToString ("MMM-yyyy");// txtdate

            //string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt2 = (DataTable)Session["tblover"];

            //ReportDocument rptMobAll = new ReportDocument ();



            //rptMobAll = new RealERPRPT.R_81_Hrm.R_86_All.rptMobileAllowAlli ();




            //TextObject CompName = rptMobAll.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = companyname.ToUpper ();

            //TextObject rptTxtDptname = rptMobAll.ReportDefinition.ReportObjects["txtDpt"] as TextObject;
            //rptTxtDptname.Text = "Department : " + dptName;

            //TextObject rptTxtDate = rptMobAll.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptTxtDate.Text = "The Month of " + monthid;

            //TextObject txtuserinfo = rptMobAll.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat (compname, username, printdate);

            //rptMobAll.SetDataSource (dt2);

            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptMobAll;

            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../../RptViewer?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        }

        private void MobileAllowTropical()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString ();
            //string comadd = hst["comadd1"].ToString ();
            //string compname = hst["compname"].ToString ();
            //string username = hst["username"].ToString ();
            //string companyname = ddlCompanyName.SelectedItem.Text.Trim ();
            //string dptName = ddlDepartment.SelectedItem.Text.Trim ().Substring (13);
            //string monthid = Convert.ToDateTime (ASTUtility.Right (this.ddlyearmon.Text.Trim (), 2) + "/01/" + this.ddlyearmon.Text.Trim ().Substring (0, 4)).ToString ("MMM-yyyy");// txtdate

            //string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt2 = (DataTable)Session["tblover"];

            //ReportDocument rptMobAll = new ReportDocument ();


            //DataView dv = dt2.DefaultView;
            //dv.RowFilter = ("mbillamt>0");
            //dt2 = dv.ToTable ();

            //rptMobAll = new RealERPRPT.R_81_Hrm.R_86_All.rptMobileAllowanceTro ();



            //TextObject CompName = rptMobAll.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = companyname.ToUpper ();

            //TextObject rptTxtDptname = rptMobAll.ReportDefinition.ReportObjects["txtDpt"] as TextObject;
            //rptTxtDptname.Text = "Department : " + dptName;

            //TextObject rptTxtDate = rptMobAll.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptTxtDate.Text = "The Month of " + monthid;

            //TextObject txtuserinfo = rptMobAll.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat (compname, username, printdate);

            //rptMobAll.SetDataSource (dt2);

            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptMobAll;

            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../../RptViewer?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        }

        private void MobileAllowAcme()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString ();
            //string comadd = hst["comadd1"].ToString ();
            //string compname = hst["compname"].ToString ();
            //string username = hst["username"].ToString ();
            //string companyname = ddlCompanyName.SelectedItem.Text.Trim ();
            //string dptName = ddlDepartment.SelectedItem.Text.Trim ().Substring (13);
            //string monthid = Convert.ToDateTime (ASTUtility.Right (this.ddlyearmon.Text.Trim (), 2) + "/01/" + this.ddlyearmon.Text.Trim ().Substring (0, 4)).ToString ("MMM-yyyy");// txtdate

            //string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt2 = (DataTable)Session["tblover"];

            //ReportDocument rptMobAll = new ReportDocument ();


            //rptMobAll = new RealERPRPT.R_81_Hrm.R_86_All.rptMobileAllowAcme ();





            //TextObject CompName = rptMobAll.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = companyname.ToUpper ();

            //TextObject rptTxtDptname = rptMobAll.ReportDefinition.ReportObjects["txtDpt"] as TextObject;
            //rptTxtDptname.Text = "Department : " + dptName;

            //TextObject rptTxtDate = rptMobAll.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptTxtDate.Text = "The Month of " + monthid;

            //TextObject txtuserinfo = rptMobAll.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat (compname, username, printdate);

            //rptMobAll.SetDataSource (dt2);

            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptMobAll;

            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../../RptViewer?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";

        }
        private void GetGeneral()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString ();
            //string comadd = hst["comadd1"].ToString ();
            //string compname = hst["compname"].ToString ();
            //string username = hst["username"].ToString ();
            //string companyname = ddlCompanyName.SelectedItem.Text.Trim ();
            //string dptName = ddlDepartment.SelectedItem.Text.Trim ().Substring (13);
            //string monthid = Convert.ToDateTime (ASTUtility.Right (this.ddlyearmon.Text.Trim (), 2) + "/01/" + this.ddlyearmon.Text.Trim ().Substring (0, 4)).ToString ("MMM-yyyy");// txtdate

            //string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt2 = (DataTable)Session["tblover"];

            //ReportDocument rptMobAll = new ReportDocument ();


            //rptMobAll = new RealERPRPT.R_81_Hrm.R_86_All.rptMobileAllowance ();





            //TextObject CompName = rptMobAll.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = companyname.ToUpper ();

            //TextObject rptTxtDptname = rptMobAll.ReportDefinition.ReportObjects["txtDpt"] as TextObject;
            //rptTxtDptname.Text = "Department : " + dptName;

            //TextObject rptTxtDate = rptMobAll.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rptTxtDate.Text = "The Month of " + monthid;

            //TextObject txtuserinfo = rptMobAll.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat (compname, username, printdate);

            //rptMobAll.SetDataSource (dt2);

            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptMobAll;

            //((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../../RptViewer?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();


        }
        protected void lTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        private void SaveValue()
        {

            string type = this.ddlType.SelectedValue;
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            switch (type)
            {
                case "Overtime":
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        double fixhour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Text.Trim());
                        double hourly = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Text.Trim());
                        double c1hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Text.Trim());
                        double c2hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Text.Trim());
                        double c3hour = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Text.Trim());

                        double tohour = fixhour + hourly + c1hour + c2hour + c3hour;
                        rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + i;
                        dt.Rows[rowindex]["fixhour"] = fixhour;
                        dt.Rows[rowindex]["hourly"] = hourly;
                        dt.Rows[rowindex]["c1hour"] = c1hour;
                        dt.Rows[rowindex]["c2hour"] = c2hour;
                        dt.Rows[rowindex]["c3hour"] = c3hour;
                        dt.Rows[rowindex]["tohour"] = tohour;

                    }

                    break;

                case "BankPayment":
                    for (int i = 0; i < this.gvBankPay.Rows.Count; i++)
                    {

                        string bankno = ((TextBox)this.gvBankPay.Rows[i].FindControl("lbgvBankSno")).Text.Trim();
                        string acno = ((TextBox)this.gvBankPay.Rows[i].FindControl("lgvBACNo")).Text.Trim();
                        double amount = Convert.ToDouble("0" + ((TextBox)this.gvBankPay.Rows[i].FindControl("lgvAmt")).Text.Trim());
                        string remarks = ((TextBox)this.gvBankPay.Rows[i].FindControl("lgvRemarks")).Text.Trim();
                        rowindex = (this.gvBankPay.PageSize) * (this.gvBankPay.PageIndex) + i;
                        dt.Rows[rowindex]["bankseno"] = bankno;
                        dt.Rows[rowindex]["bankacno"] = acno;
                        dt.Rows[rowindex]["bankamt"] = amount;
                        dt.Rows[rowindex]["remarks"] = remarks;

                    }

                    break;

                case "Holiday":
                    for (int i = 0; i < this.gvEmpHoliday.Rows.Count; i++)
                    {
                        bool chk = ((CheckBox)this.gvEmpHoliday.Rows[i].FindControl("chkHoliday")).Checked;
                        rowindex = (this.gvEmpHoliday.PageSize) * (this.gvEmpHoliday.PageIndex) + i;
                        dt.Rows[rowindex]["hstatus"] = (chk) ? "True" : "False";
                    }

                    break;

                case "Mobile":
                    for (int i = 0; i < this.gvEmpMbill.Rows.Count; i++)
                    {
                        double mbillamt = Convert.ToDouble("0" + ((TextBox)this.gvEmpMbill.Rows[i].FindControl("txtgvMbill")).Text.Trim());
                        double mlimit = Convert.ToDouble("0" + ((TextBox)this.gvEmpMbill.Rows[i].FindControl("txtgvMbilllimit")).Text.Trim());
                        double internet = Convert.ToDouble("0" + ((TextBox)this.gvEmpMbill.Rows[i].FindControl("txtintbill")).Text.Trim());

                        //double balance = Convert.ToDouble(mlimit - mbillamt);

                        rowindex = (this.gvEmpMbill.PageSize) * (this.gvEmpMbill.PageIndex) + i;
                        //dt.Rows[rowindex]["mbillamt"] = mbillamt;
                        //dt.Rows[rowindex]["mbilllimit"] = mlimit;
                        //dt.Rows[rowindex]["balance"] = balance.ToString("#,##0.00;(#,##0.00); ");

                        dt.Rows[rowindex]["mbillamt"] = mbillamt;
                        dt.Rows[rowindex]["mbilllimit"] = mlimit;
                        dt.Rows[rowindex]["intbill"] = internet;


                        //dt.Rows[i]["balance"] = Math.Round(balance, 2);




                    }
                    break;

                case "Lencashment":
                    for (int i = 0; i < this.gvEmpELeave.Rows.Count; i++)
                    {
                        int enclashleave = Convert.ToInt32("0" + ((TextBox)this.gvEmpELeave.Rows[i].FindControl("txtgvEnCleave")).Text.Trim());
                        rowindex = (this.gvEmpELeave.PageSize) * (this.gvEmpELeave.PageIndex) + i;
                        dt.Rows[rowindex]["ecleave"] = enclashleave;
                    }
                    break;
                case "OtherDeduction":
                    for (int i = 0; i < this.gvEmpOtherded.Rows.Count; i++)
                    {

                        double lvded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtgvleaveded")).Text.Trim());
                        double arded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtgvarairded")).Text.Trim());
                        double saladv = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtgvsaladv")).Text.Trim());
                        double otherded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtlgvotherded")).Text.Trim());
                        double mbillded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("gvtxtmbill")).Text.Trim());
                        double fallded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("gvtxtfallow")).Text.Trim());
                        double transded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("gvTransDed")).Text.Trim());


                        double toamt = otherded + lvded + arded + saladv + mbillded + fallded;
                        rowindex = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + i;
                        dt.Rows[rowindex]["lvded"] = lvded;
                        dt.Rows[rowindex]["arded"] = arded;
                        dt.Rows[rowindex]["saladv"] = saladv;
                        dt.Rows[rowindex]["otherded"] = otherded;
                        dt.Rows[rowindex]["mbillded"] = mbillded;
                        dt.Rows[rowindex]["fallded"] = fallded;
                        dt.Rows[rowindex]["transded"] = transded;
                        dt.Rows[rowindex]["toamt"] = toamt;
                    }

                    break;

                case "loan":
                    for (int i = 0; i < this.gvEmploan.Rows.Count; i++)
                    {

                        double cloan = Convert.ToDouble("0" + ((TextBox)this.gvEmploan.Rows[i].FindControl("txtgvcloan")).Text.Trim());
                        double pfloan = Convert.ToDouble("0" + ((TextBox)this.gvEmploan.Rows[i].FindControl("txtgvpfloan")).Text.Trim());

                        double toamt = cloan + pfloan;
                        rowindex = (this.gvEmploan.PageSize) * (this.gvEmploan.PageIndex) + i;
                        dt.Rows[rowindex]["cloan"] = cloan;
                        dt.Rows[rowindex]["pfloan"] = pfloan;
                        dt.Rows[rowindex]["toamt"] = toamt;
                    }

                    break;

                case "arrear":
                    for (int i = 0; i < this.gvarrear.Rows.Count; i++)
                    {
                        double pf = 0.00;
                        double bacic = 0.00;
                        double arrear = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtarrear")).Text.Trim());
                        // if (Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtarrear")).Text.Trim()) != 0.00)
                        //{
                        //    double percent = Convert.ToDouble("0" + (dt.Rows[i]["percnt"]));
                        //    bacic = (arrear * percent) / 100;
                        //    pf = (bacic * 5) / 100;
                        //}
                        rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;

                        dt.Rows[rowindex]["aramt"] = arrear;
                        // dt.Rows[rowindex]["pfamt"] = pf;
                        dt.Rows[rowindex]["tapfamt"] = arrear - pf;
                    }
                    break;


                case "otherearn":
                    for (int i = 0; i < this.gvothearn.Rows.Count; i++)
                    {

                        double tptallow = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvtpallow")).Text.Trim());
                        double kpi = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvkpi")).Text.Trim());
                        double perbon = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txgvperbon")).Text.Trim());
                        double otherearn = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvotherearn")).Text.Trim());
                        double haircutal = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txgvhaircut")).Text.Trim());
                        double foodal = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txgvfood")).Text.Trim());
                        double nfoodal = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txgvnightfood")).Text.Trim());
                        double factualday = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txgvftakenday")).Text.Trim());


                        rowindex = (this.gvothearn.PageSize) * (this.gvothearn.PageIndex) + i;

                        dt.Rows[rowindex]["tptallow"] = tptallow;
                        dt.Rows[rowindex]["kpi"] = kpi;
                        dt.Rows[rowindex]["perbon"] = perbon;
                        dt.Rows[rowindex]["othearn"] = otherearn;
                        dt.Rows[rowindex]["haircutal"] = haircutal;
                        dt.Rows[rowindex]["foodal"] = foodal;
                        dt.Rows[rowindex]["nfoodal"] = nfoodal;
                        dt.Rows[rowindex]["factualday"] = factualday;

                        dt.Rows[rowindex]["totalam"] = tptallow + kpi + perbon + otherearn + haircutal + foodal + nfoodal;

                    }
                    break;
                case "dayadj":
                    for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
                    {
                        double dedday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtAdj")).Text.Trim());
                        rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                        dt.Rows[rowindex]["dedday"] = dedday;

                    }
                    break;

            }
            Session["tblover"] = dt;
        }

        protected void lUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            //bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELEMPOVRTIME", dayid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;
            bool result = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                double fixhour = Convert.ToDouble(dt.Rows[i]["fixhour"]);
                double hourly = Convert.ToDouble(dt.Rows[i]["hourly"]);
                double c1hour = Convert.ToDouble(dt.Rows[i]["c1hour"]);
                double c2hour = Convert.ToDouble(dt.Rows[i]["c2hour"]);
                double c3hour = Convert.ToDouble(dt.Rows[i]["c3hour"]);
                double fixrate = Convert.ToDouble(dt.Rows[i]["fixrate"]);
                double hourrate = Convert.ToDouble(dt.Rows[i]["hourrate"]);
                double c1rate = Convert.ToDouble(dt.Rows[i]["c1rate"]);
                double c2rate = Convert.ToDouble(dt.Rows[i]["c2rate"]);
                double c3rate = Convert.ToDouble(dt.Rows[i]["c3rate"]);


                string fixamt = (fixhour * fixrate).ToString();
                string houramt = (hourly * hourrate).ToString();
                string c1amt = (c1hour * c1rate).ToString();
                string c2amt = (c2hour * c2rate).ToString();
                string c3amt = (c3hour * c3rate).ToString();
                double tohour = Convert.ToDouble(dt.Rows[i]["tohour"]); ;

                if (tohour > 0)
                {

                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEOVRTIME", dayid, empid, gcod, date, fixhour.ToString(), hourly.ToString(), c1hour.ToString(), c2hour.ToString(), c3hour.ToString(), fixamt, houramt, c1amt, c2amt, c3amt, "");
                    if (!result)
                        return;
                }
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }


        protected void gvEmpOverTime_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + e.RowIndex;
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string empid = dt.Rows[rowindex]["empid"].ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELEMPOVRTIME", dayid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }
            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();


        }


        protected void gvEmpHoliday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpHoliday.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ChkAllEmp_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblover"];
            int i, index;
            if (((CheckBox)this.gvEmpHoliday.HeaderRow.FindControl("ChkAllEmp")).Checked)
            {

                for (i = 0; i < this.gvEmpHoliday.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpHoliday.Rows[i].FindControl("chkHoliday")).Checked = true;
                    index = (this.gvEmpHoliday.PageSize) * (this.gvEmpHoliday.PageIndex) + i;
                    dt.Rows[index]["hstatus"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvEmpHoliday.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmpHoliday.Rows[i].FindControl("chkHoliday")).Checked = false;
                    index = (this.gvEmpHoliday.PageSize) * (this.gvEmpHoliday.PageIndex) + i;
                    dt.Rows[index]["hstatus"] = "False";

                }

            }

            Session["tblover"] = dt;
        }
        protected void lUpdateHoliday_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();




            string date = Convert.ToDateTime(this.ddlyearmon.Text.Trim()).ToString("dd-MMM-yyyy");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                string txthstatus = dt.Rows[i]["hstatus"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEHOLIDAY", "", empid, gcod, "", txthstatus, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }
        protected void gvEmpMbill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpMbill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbntUpdateMbill_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                double mbill = Convert.ToDouble(dt.Rows[i]["mbillamt"]);
                double mlimit = Convert.ToDouble(dt.Rows[i]["mbilllimit"]);
                double internetbill = Convert.ToDouble(dt.Rows[i]["intbill"]);




                //if (mlimit > 0)
                //{
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEMBILL", Monthid, empid, gcod, mbill.ToString(), mlimit.ToString(), internetbill.ToString(), "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;

                //}
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lbtnTotalmBill_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnTotalEnLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateEnLeave_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();


            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string gcod = dt.Rows[i]["gcod"].ToString();
                string eleave = dt.Rows[i]["eleave"].ToString();
                int ecleave = Convert.ToInt32(dt.Rows[i]["ecleave"]);
                if (ecleave > 0)
                {

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEELEAVE", dayid, empid, gcod, date, eleave, ecleave.ToString(), "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lbtnTotalOtherDed_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateOtherDed_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();

                string lvded = Convert.ToDouble(dt.Rows[i]["lvded"]).ToString();
                string arded = Convert.ToDouble(dt.Rows[i]["arded"]).ToString();
                string saladv = Convert.ToDouble(dt.Rows[i]["saladv"]).ToString();
                string otherded = Convert.ToDouble(dt.Rows[i]["otherded"]).ToString();
                string mbillded = Convert.ToDouble(dt.Rows[i]["mbillded"]).ToString();
                string fallded = Convert.ToDouble(dt.Rows[i]["fallded"]).ToString();
                string paystatus = Convert.ToDouble(dt.Rows[i]["paystatus"]).ToString(); // Convert.ToDouble(dr1["paystatus"]);
                string fine = dt.Rows[i]["fine"].ToString();
                string finedays = dt.Rows[i]["finedays"].ToString();
                string cashded = dt.Rows[i]["cashded"].ToString();
                string transded = dt.Rows[i]["transded"].ToString();



                double toamt = Convert.ToDouble(dt.Rows[i]["toamt"]);
                double fineday = Convert.ToDouble(dt.Rows[i]["finedays"]);
                if (toamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPOTHERDED", Monthid, empid, lvded, arded, saladv, otherded, mbillded, fallded, paystatus, fine, cashded, finedays, transded, "", "");
                    if (!result)
                        return;
                }
                else if (toamt == 0 && fineday > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPOTHERDED", Monthid, empid, lvded, arded, saladv, otherded, mbillded, fallded, paystatus, fine, cashded, finedays, transded, "", "");
                    if (!result)
                        return;
                }



                //double toamt = Convert.ToDouble (dt.Rows[i]["toamt"]);
                //if (toamt > 0)
                //{
                //    bool result = HRData.UpdateTransInfo (comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPOTHERDED", Monthid, empid, lvded, arded, saladv, otherded, mbillded, fallded, "", "", "", "", "", "", "");
                //    if (!result)
                //        return;

                //}
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void gvEmpOtherded_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmpOtherded.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvEmploan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvEmploan.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnTotalLoan_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateLoan_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();

                string cloan = Convert.ToDouble(dt.Rows[i]["cloan"]).ToString();
                string pfloan = Convert.ToDouble(dt.Rows[i]["pfloan"]).ToString();

                double toamt = Convert.ToDouble(dt.Rows[i]["toamt"]);
                if (toamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPLOANDED", Monthid, empid, cloan, pfloan, "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;

                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string type = this.ddlType.SelectedValue;
            int j;
            switch (type)
            {

                case "Overtime":
                case "Mobile":
                case "OtherDeduction":
                case "Holiday":
                case "Lencashment":
                case "loan":
                case "arrear":
                case "otherearn":
                case "dayadj":
                    secid = dt1.Rows[0]["secid"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["secid"].ToString() == secid)
                        {
                            secid = dt1.Rows[j]["secid"].ToString();
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {
                            secid = dt1.Rows[j]["secid"].ToString();
                        }

                    }

                    break;


                case "BankPayment":
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                        }

                        else
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                        }

                    }
                    break;




            }



            return dt1;

        }
        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();

            DataTable dt = (DataTable)Session["tblover"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string bankno = dt.Rows[i]["bankseno"].ToString();
                string acno = dt.Rows[i]["bankacno"].ToString();
                double amount = Convert.ToDouble(dt.Rows[i]["bankamt"].ToString());
                string remarks = dt.Rows[i]["remarks"].ToString();

                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTBANKPAYINF", empid, acno, bankno, amount.ToString(), remarks, "", "", "", "", "", "", "", "", "", "");

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }


        protected void gvBankPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBankPay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvarrear_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvarrear.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnCalArrear_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

        protected void lbtnTotalArrear_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.gvarrear.Rows.Count; i++)
            {
                double arrear = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtarrear")).Text.Trim());
                double pf = Convert.ToDouble("0" + ((TextBox)this.gvarrear.Rows[i].FindControl("txtPFAmt")).Text.Trim());
                rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;
                dt.Rows[rowindex]["aramt"] = arrear;
                dt.Rows[rowindex]["pfamt"] = pf;
                dt.Rows[rowindex]["tapfamt"] = arrear - pf;
            }
            Session["tblover"] = dt;
            this.Data_Bind();
        }

        protected void lbntUpdateArrear_Click(object sender, EventArgs e)
        {
            this.lbtnTotalArrear_Click(null, null);
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                //string gcod = dt.Rows[i]["gcod"].ToString();
                double arrer = Convert.ToDouble("0" + dt.Rows[i]["aramt"]);
                double pfamt = Convert.ToDouble("0" + dt.Rows[i]["pfamt"]);

                if (arrer > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTARREAR", Monthid, empid, arrer.ToString(), pfamt.ToString(), "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }





        protected void gvEmpMbill_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvEmpMbill.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMBILL", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (result == true)
            {
                int rowindex = (this.gvEmpMbill.PageSize) * (this.gvEmpMbill.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvEmpELeave_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            //string dayid = Convert.ToDateTime(this.ddlyearmon.Text.Trim()).ToString("yyyyMMdd");
            string empid = ((Label)this.gvEmpELeave.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMLEAVE", dayid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (result == true)
            {
                int rowindex = (this.gvEmpELeave.PageSize) * (this.gvEmpELeave.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvEmpOtherded_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvEmpOtherded.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMOTDEC", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (result == true)
            {
                int rowindex = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void gvarrear_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvarrear.Rows[e.RowIndex].FindControl("lgvEmpId")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMARSAL", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Row Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (result == true)
            {
                int rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void gvothearn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvothearn.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnTotalOthEarn_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateOthEarn_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            bool result = false;
            //bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEEMPOTHEARN", Monthid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            //if (!result)
            //    return;

            foreach (DataRow dr in dt.Rows)
            {
                string empid = dr["empid"].ToString();

                string tptallow = Convert.ToDouble(dr["tptallow"]).ToString();
                string kpi = Convert.ToDouble(dr["kpi"]).ToString();
                string perbon = Convert.ToDouble(dr["perbon"]).ToString();
                string othearn = Convert.ToDouble(dr["othearn"]).ToString();
                double totalam = Convert.ToDouble(dr["totalam"]);
                string haircutal = Convert.ToDouble(dr["haircutal"]).ToString();
                string foodal = Convert.ToDouble(dr["foodal"]).ToString();
                string nfoodal = Convert.ToDouble(dr["nfoodal"]).ToString();
                string factualday = Convert.ToDouble(dr["factualday"]).ToString();


                if (totalam > 0)
                {
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTOTHEARN", Monthid, empid, tptallow, kpi, perbon, othearn, haircutal, foodal, nfoodal, factualday, "0", "0", "0", "", "");
                }
            }
            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }



        protected void grvAdjDay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.grvAdjDay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnTotalDay_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void btnUpdateDayAdj_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string monthid = this.ddlyearmon.Text.Trim();
            bool result = false;
            ///--------------------------------------------------////////////
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            //////----------------------------------------------------------/////////////
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string delday = Convert.ToDouble("0" + dt.Rows[i]["dalday"]).ToString();
                string aprday = Convert.ToDouble("0" + dt.Rows[i]["aprday"]).ToString();
                double dedday = Convert.ToDouble("0" + dt.Rows[i]["dedday"]);
                //if (dedday > 0)
                //{
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPEMPSALADJST", monthid, empid, dedday.ToString(), userid, Terminal, Sessionid, Posteddat, delday, aprday, "", "", "", "", "", "");
                if (!result)
                    return;
                //  }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }
        protected void grvAdjDay_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.grvAdjDay.Rows[e.RowIndex].FindControl("lgvEmpIdAdj")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "DELETESALADJST", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (result == true)
            {
                int rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void lbtnCalCulationSadj_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.grvAdjDay.Rows.Count; i++)
            {
                double delayday = Convert.ToDouble("0" + ((Label)this.grvAdjDay.Rows[i].FindControl("lblgvDelday")).Text.Trim());
                double Aprvday = Convert.ToDouble("0" + ((TextBox)this.grvAdjDay.Rows[i].FindControl("txtaprday")).Text.Trim());
                rowindex = (this.grvAdjDay.PageSize) * (this.grvAdjDay.PageIndex) + i;
                double redelay = delayday - Aprvday;
                dt.Rows[rowindex]["aprday"] = Aprvday;
                dt.Rows[rowindex]["dedday"] = Convert.ToInt32(Convert.ToInt32(redelay) / 3);

            }

            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void gvothearn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvothearn.Rows[e.RowIndex].FindControl("lgvEmpIdearn")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMPOTHERN", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Row Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (result == true)
            {
                int rowindex = (this.gvothearn.PageSize) * (this.gvothearn.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void chkcopy_CheckedChanged(object sender, EventArgs e)
        {

            if (this.chkcopy.Checked)
            {
                this.GetPreYearMonth();
            }
            this.pnlCopy.Visible = (this.chkcopy.Checked);
        }
        protected void lbtnCopy_Click(object sender, EventArgs e)
        {



            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlpreyearmon.SelectedValue.ToString();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlpreyearmon.Text.Trim(), 2) + "/01/" + this.ddlpreyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : "86%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMOBILEBILLINFO", deptname, MonthId, date, comnam, Empcode, mantype, section, "", "");
            if (ds2 == null)
            {
                this.gvEmpMbill.DataSource = null;
                this.gvEmpMbill.DataBind();
                return;
            }
            Session["tblover"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            this.chkcopy.Checked = false;
            this.chkcopy_CheckedChanged(null, null);


        }

        protected void lblbtncopyoth_Click(object sender, EventArgs e)
        {
            //
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlpreyearmonoth.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHERDEDUCTION", deptname, MonthId, date, comnam, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvEmpOtherded.DataSource = null;
                this.gvEmpOtherded.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



            //


            this.Data_Bind();
            this.Chkother.Checked = false;
            this.Chkother_CheckedChanged(null, null);
        }
        protected void Chkother_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Chkother.Checked)
            {
                this.GetPreYearMonthother();
            }
            this.Pnlother.Visible = (this.Chkother.Checked);
        }
        protected void btnCopyEarn_Click(object sender, EventArgs e)
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string compname = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlPremEarn.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHEARNING", compname, MonthId, date, deptname, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvothearn.DataSource = null;
                this.gvothearn.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
            this.Data_Bind();
            this.ChkEarn.Checked = false;
            this.ChkEarn_CheckedChanged(null, null);
        }

        private void GetPreYearmEarn()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlPremEarn.DataTextField = "yearmon";
            this.ddlPremEarn.DataValueField = "ymon";
            this.ddlPremEarn.DataSource = ds1.Tables[0];
            this.ddlPremEarn.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlPremEarn.DataBind();

            ds1.Dispose();
        }

        protected void ChkEarn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkEarn.Checked)
            {
                this.GetPreYearmEarn();
            }
            this.PnlEarn.Visible = (this.ChkEarn.Checked);
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }    

       
    }
}