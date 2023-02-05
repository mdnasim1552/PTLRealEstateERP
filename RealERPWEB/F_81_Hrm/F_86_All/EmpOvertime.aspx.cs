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
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Globalization;

namespace RealERPWEB.F_81_Hrm.F_86_All
{
    public partial class EmpOvertime : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                // this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Overtime") ? "EMPLOYEE  OVERTIME ALLOWANCE "
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "BankPayment") ? "BANK PAYMENT INFORMATION"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "Holiday") ? "EMPLOYEE HOLIDAY ALLOWANCE"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "Mobile") ? "EMPLOYEE MOBILE BILL ALLOWANCE"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "Lencashment") ? "LEAVE ENCASHMENT"
                //    : (this.Request.QueryString["Type"].ToString().Trim() == "salaryencashment") ? "Salary ENCASHMENT"
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "OtherDeduction") ? "EMPLOYEE OTHER DEDCUTION"
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "loan") ? "EMPLOYEE LOAN INFORMATION"
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "dayadj") ? "Salary Adjustment"
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "otherearn") ? "Employee Other Earning"
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "bonusextra") ? "Additional Bonus"
                //     : (this.Request.QueryString["Type"].ToString().Trim() == "SalaryReduction") ? "Salary Reduction" : "EMPLOYEE ARREAR INFORMATION";


                this.ViewVisibility();
                this.GetCompName();
                this.GetYearMonth();
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

       

            }
            //Excel Upload (Deduction Upload)
            if (fileuploadExcel.HasFile)
            {
                try
                {
                    Session.Remove("ExcelData");
                    string connString = "";
                    string StrFileName = string.Empty;
                    if (fileuploadExcel.PostedFile != null && fileuploadExcel.PostedFile.FileName != "")
                    {
                        StrFileName = fileuploadExcel.PostedFile.FileName.Substring(fileuploadExcel.PostedFile.FileName.LastIndexOf("\\") + 1);
                        string StrFileType = fileuploadExcel.PostedFile.ContentType;
                        int IntFileSize = fileuploadExcel.PostedFile.ContentLength;
                        if (IntFileSize <= 0)
                        {
                            return;
                        }
                        else
                        {
                            string savelocation = Server.MapPath("~") + "\\ExcelFile\\";
                            string[] filePaths = Directory.GetFiles(savelocation);
                            foreach (string filePath in filePaths)
                                File.Delete(filePath);
                            fileuploadExcel.PostedFile.SaveAs(Server.MapPath("~") + "\\ExcelFile\\" + StrFileName);
                        }
                    }

                    string strFileType = Path.GetExtension(fileuploadExcel.FileName).ToLower();
                    string apppath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath.ToString();
                    string path = Server.MapPath("~") + ("\\ExcelFile\\" + StrFileName);

                    //Connection String to Excel Workbook
                    if (strFileType.Trim() == ".xls")
                    {
                        connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {

                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                    }

                    string query = "";
                    query = "SELECT * FROM [Sheet1$]";
                    OleDbConnection conn = new OleDbConnection(connString);
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);


                    DataView dv = ds.Tables[0].DefaultView;
                    // dv.RowFilter = ("Card<>''");
                    Session["ExcelData"] = dv.ToTable();
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    string msg = "Please Click Adjust Button";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            if (IsPostBack)
            {
                this.GetCheckBoxStates();

            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkbtnRecalculate_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            this.ddlpreyearmonoth.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            this.ddlpreyearmonoth.DataBind();

            ds1.Dispose();
        }
        

        private void GetPreYearArrear()
        {
            string comcod = this.GetComeCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlprearrear.DataTextField = "yearmon";
            this.ddlprearrear.DataValueField = "ymon";
            this.ddlprearrear.DataSource = ds1.Tables[0];
            this.ddlprearrear.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            this.ddlprearrear.DataBind();

            ds1.Dispose();
        }




        private void GetCompName()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompanyName_SelectedIndexChanged(null, null);
            ds1.Dispose();
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
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            // string txtCompanyname =(this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) =="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
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
            this.SectionName();
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

        private string companyCallType()
        {

            string calltype = "";
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3101":
                    //calltype = "GETDEPARTMENTBYEMP";
                    calltype = "GETDEPARTMENT";
                    break;

                default:
                    calltype = "GETDEPARTMENT";
                    break;

            }
            return calltype;

        }

        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
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
                case "bonusextra":
                    this.AdditionalBonus();
                    break;

                case "salaryencashment":
                    this.ShowSalEncashment();
                    break;
                    
            }
        }

     

        private void ViewVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetComeCode();
            switch (type)
            {
                case "Overtime":
                    ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
                    break;
                case "BankPayment":
                    break;

                case "Holiday":
                    break;

                case "Mobile":
                    this.lbldate.Text = "Month Id:";
                    this.rbtnlistsaltype.Visible = true;
                    this.VisibilityMobileGrid();

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


                    break;

                //this.txtDate_CalendarExtender.Format = "yyyyMM";
                //this.txtDate.MaxLength = 6;

                case "dayadj":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    break;

                case "SalaryReduction":

                    break;

                case "bonusextra":
                    this.ddlyearmon.Text = System.DateTime.Today.ToString("yyyyMM");
                    this.lbldate.Text = "Month Id:";
                    //this.txtDate_CalendarExtender.Format = "yyyyMM";
                    //this.txtDate.MaxLength = 6;
                    break;
                case "salaryencashment":
                    ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
                    break;



            }


        }

        private void VisibilityMobileGrid()
        {

            string comcod = this.GetComeCode();

            switch (comcod)
            {
                case "3315":
                case "3316":
                    this.gvEmpMbill.Columns[7].Visible = false;
                    this.gvEmpMbill.Columns[9].Visible = false;
                    this.gvEmpMbill.Columns[8].HeaderText = "Mobile Bill";
                    break;

                default:
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
               //this.ddlCompanyName.Visible = false;
                //this.ddlDepartment.Visible = false;
                //this.ddlSection.Enabled = false;
                
               // this.lblCompanyName.Visible = true;
                //this.lblDeptDesc.Visible = true;

                //this.lblPage.Visible = true;
                //this.ddlpagesize.Visible = true;
                this.lnkbtnShow.Text = "New";
                //this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
               // this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text;
                this.SectionView();
                return;
            }
            this.MultiView1.ActiveViewIndex = -1;
            this.ddlyearmon.Enabled = true;
            //this.ddlCompanyName.Visible = true;
            //this.ddlDepartment.Visible = true;
            this.ddlSection.Enabled = true;

           // this.lblCompanyName.Visible = false;
            //this.lblDeptDesc.Visible = false;
            //this.lblPage.Visible = false;
            //this.ddlpagesize.Visible = false;
            this.gvEmpOverTime.DataSource = null;
            this.gvEmpOverTime.DataBind();
            this.lnkbtnShow.Text = "Ok";
            //this.lblCompanyName.Text = "";



        }

        private void Visibilitypeb()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3101":
                case "3347":
                    this.btnUploadovrtime.Visible = true;
                    break;

                default:
                    break;

            }
        }

        private void SectionView()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Overtime":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.pnlDedEarnExcel.Visible = true;

                    this.ShowOvertime();
                    this.Visibilitypeb();
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
                    this.pnlDedEarnExcel.Visible = true;
                    this.ShowOtherDeduction();
                    break;

                case "loan":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowEmpLoan();
                    break;
                case "arrear":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.pnlDedEarnExcel.Visible = true;

                    this.EmpArrearSalary();
                    break;
                case "otherearn":
                    this.MultiView1.ActiveViewIndex = 8;
                    this.OtherEarning();
                    this.pnlDedEarnExcel.Visible = true;
                    break;
                case "dayadj":
                    this.MultiView1.ActiveViewIndex = 9;
                    this.SalaryDayAdj();
                    break;

                case "SalaryReduction":
                    this.MultiView1.ActiveViewIndex = 10;
                    this.SalaryReduction();
                    break;
                case "bonusextra":
                    this.MultiView1.ActiveViewIndex = 11;
                    AdditionalBonus();
                    break;

                case "salaryencashment":
                    this.MultiView1.ActiveViewIndex = 12;
                    this.ShowSalEncashment();
                    break;


      

            }

        }

        private string GetComOvertimeCallType()
        {

            string comcod = this.GetComeCode();
            string CallType = "";
            switch (comcod)
            {
                case "3368"://Finlay
                    CallType = "EMPALLOYOVERTIMEFINLAY";
                    break;

                case "3369"://acme ai
                    CallType = "EMPALLOYOVERTIMEACMEAI";

                    break;

                //case "3370"://cpdl
                //    CallType = "EMPALLOYOVERTIMECPDL";

                //    break;

                default:
                    CallType = "EMPALLOYOVERTIME";
                    break;
            
            }


            return CallType;


        }

        private void AdditionalBonus()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPADDITIONALBONUS", compname, MonthId, date, deptname, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                string msg = "No Data Found";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                this.GvAddiBonus.DataSource = null;
                this.GvAddiBonus.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private void ShowOvertime()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            txtdate = Convert.ToDateTime(txtdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string CallType = this.GetComOvertimeCallType();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", CallType, deptname, dayid, txtdate, comnam, Empcode, section, "", "", "");
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

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";


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
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
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
            // string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";


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


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";

            string calltype = comcod == "3365" ? "LEAVEENCASHMENTBTI" : "LEAVEENCASHMENT";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", calltype, deptname, dayid, txtdate, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEmpELeave.DataSource = null;
                this.gvEmpELeave.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowSalEncashment()
        {
            Session.Remove("tblencashment");

            string comcod = this.GetComeCode();


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000" ? "94" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string curr_yr = DateTime.Today.AddYears(-1).ToString("yyyy");
            string dayid = ymon + "01";
            //string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));
            string txtdate ="01-" +"Dec-"  + curr_yr.Substring(0, 4);

            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";

          //  string calltype = comcod == "3365" ? "LVENCASHMENTSALBTI" : "LVENCASHMENTSALBTI";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVE_SUMMARY", "LVENCASHMENTSALBTI", deptname, dayid, txtdate, comnam, Empcode, "", "", "", "");
            if (ds2 == null)
            {
                this.gvEncashment.DataSource = null;
                this.gvEncashment.DataBind();
                return;
            }
            else
            {
                Session["tblencashment"] = this.HiddenSameData(ds2.Tables[0]);
                this.Data_Bind();
            }
           


        }

        private void ShowOtherDeduction()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string field = "";
            string comothdedtype = this.GetCompOtherDeduc();
          

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPOTHERDEDUCTION", deptname, MonthId, date, comnam, Empcode, section, field, comothdedtype);
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



            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPLOANEDUCTION", deptname, MonthId, date, comnam, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvEmploan.DataSource = null;
                this.gvEmploan.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void EmpArrearSalary()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
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

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
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

            if (comcod == "3369")
            {
                this.gvothearn.Columns[13].HeaderText = "Project Bonus";
                this.gvothearn.Columns[5].HeaderText = "Ref Fee";
                this.gvothearn.Columns[12].HeaderText = "Extra Day Amount";


            }

        }
        private void SalaryDayAdj()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();



            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";



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

        private void SalaryReduction()
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();
            string MonthId = this.ddlyearmon.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string compname = this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

            // string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            // string Empcode = "%" + this.txtSrcEmployee.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "SALARYREDUCTION", MonthId, compname, deptname, section, "", "", "", "");
            if (ds2 == null)
            {
                this.gvsalreduction.DataSource = null;
                this.gvsalreduction.DataBind();
                 

                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            string comcod = this.GetComeCode();
          //  DataTable dt = (DataTable)Session["tblover"];
            DataTable dt = ((type == "salaryencashment") ? (DataTable)Session["tblencashment"] : (DataTable)Session["tblover"]);


            switch (type)
            {
                case "Overtime":
                    this.gvEmpOverTime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpOverTime.DataSource = dt;
                    this.gvEmpOverTime.DataBind();
                    this.EnabledOrVissible();
                    this.FooterCalculation();

                    if ( comcod == "3101")//For cpdl
                    {
                        
                        this.gvEmpOverTime.Columns[4].Visible = true;
                        this.gvEmpOverTime.Columns[5].Visible = false;
                        this.gvEmpOverTime.Columns[6].Visible = false;
                        this.gvEmpOverTime.Columns[7].Visible = false;
                        this.gvEmpOverTime.Columns[8].Visible = false;
                        this.gvEmpOverTime.Columns[9].Visible = false;
                        this.gvEmpOverTime.Columns[10].Visible = false;
                        this.gvEmpOverTime.Columns[11].Visible = false;
                        this.gvEmpOverTime.Columns[12].Visible = false;
                        this.gvEmpOverTime.Columns[13].Visible = false;
                        this.gvEmpOverTime.Columns[14].Visible = false;
                        this.gvEmpOverTime.Columns[15].Visible = false;
                        this.gvEmpOverTime.Columns[16].Visible = true;
                    }
                    else if (comcod == "3369")
                    {
                        this.gvEmpOverTime.Columns[3].Visible = true;
                        this.gvEmpOverTime.Columns[4].Visible = true;
                        this.gvEmpOverTime.Columns[5].Visible = true;
                        this.gvEmpOverTime.Columns[6].Visible = true;
                        this.gvEmpOverTime.Columns[7].Visible = true;
                        this.gvEmpOverTime.Columns[8].Visible = true;
                        this.gvEmpOverTime.Columns[9].Visible = false;
                        this.gvEmpOverTime.Columns[10].Visible = false;
                        this.gvEmpOverTime.Columns[11].Visible = false;
                        this.gvEmpOverTime.Columns[12].Visible = false;
                        this.gvEmpOverTime.Columns[13].Visible = false;
                        this.gvEmpOverTime.Columns[14].Visible = false;
                        this.gvEmpOverTime.Columns[15].Visible = false;
                        this.gvEmpOverTime.Columns[16].Visible = false;
                        this.gvEmpOverTime.Columns[17].Visible = false;
                        this.gvEmpOverTime.Columns[18].Visible = false;
                        this.gvEmpOverTime.Columns[19].Visible = false;
                        this.gvEmpOverTime.Columns[20].Visible = false;
                        this.gvEmpOverTime.Columns[21].Visible = true;
                        this.gvEmpOverTime.Columns[22].Visible = true;
                    }

                    else if (comcod == "3370")
                    {
                        this.gvEmpOverTime.Columns[1].Visible = true;
                        this.gvEmpOverTime.Columns[2].Visible = true;


                        this.gvEmpOverTime.Columns[3].Visible = true;
                        this.gvEmpOverTime.Columns[4].Visible = true;
                        this.gvEmpOverTime.Columns[5].Visible = false;
                        this.gvEmpOverTime.Columns[6].Visible = false;
                        this.gvEmpOverTime.Columns[7].Visible = false;
                        this.gvEmpOverTime.Columns[8].Visible = false;
                        this.gvEmpOverTime.Columns[9].Visible = false;
                        this.gvEmpOverTime.Columns[10].Visible = false;
                        this.gvEmpOverTime.Columns[11].Visible = false;
                        this.gvEmpOverTime.Columns[12].Visible = false;
                        this.gvEmpOverTime.Columns[13].Visible = false;
                        this.gvEmpOverTime.Columns[14].Visible = false;
                        this.gvEmpOverTime.Columns[15].Visible = false;
                        this.gvEmpOverTime.Columns[16].Visible = false;
                        this.gvEmpOverTime.Columns[17].Visible = false;
                        this.gvEmpOverTime.Columns[18].Visible = false;
                        this.gvEmpOverTime.Columns[19].Visible = true;
                        this.gvEmpOverTime.Columns[20].Visible = true;
                        this.gvEmpOverTime.Columns[21].Visible = false;
                        this.gvEmpOverTime.Columns[22].Visible = false;
                    }
                    //else if (comcod == "3368")
                    //{
                    //    this.gvEmpOverTime.Columns[1].Visible = true;
                    //    this.gvEmpOverTime.Columns[2].Visible = true;
                    //    this.gvEmpOverTime.Columns[3].Visible = true;
                    //    this.gvEmpOverTime.Columns[4].Visible = true;

                    //    this.gvEmpOverTime.Columns[5].Visible = false;
                    //    this.gvEmpOverTime.Columns[6].Visible = false;
                    //    this.gvEmpOverTime.Columns[7].Visible = false;
                    //    this.gvEmpOverTime.Columns[8].Visible = true;

                    //    this.gvEmpOverTime.Columns[9].Visible = false;
                    //    this.gvEmpOverTime.Columns[10].Visible = false;
                    //    this.gvEmpOverTime.Columns[11].Visible = false;
                    //    this.gvEmpOverTime.Columns[12].Visible = false;
                    //    this.gvEmpOverTime.Columns[13].Visible = true;

                    //    this.gvEmpOverTime.Columns[14].Visible = false;
                    //    this.gvEmpOverTime.Columns[15].Visible = false;
                    //    this.gvEmpOverTime.Columns[16].Visible = false;
                    //    this.gvEmpOverTime.Columns[17].Visible = false;
                    //    this.gvEmpOverTime.Columns[18].Visible = true;

                    //    this.gvEmpOverTime.Columns[19].Visible = false;
                    //    this.gvEmpOverTime.Columns[20].Visible = false;
                    //    this.gvEmpOverTime.Columns[21].Visible = false;
                    //    this.gvEmpOverTime.Columns[22].Visible = false;
                    //}
                    //else
                    //{
                    //    this.gvEmpOverTime.Columns[16].Visible = false;

                    //}




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
                    if (comcod == "3365")//For BTI
                    {
                        this.gvEmpOtherded.Columns[6].Visible = false;
                        this.gvEmpOtherded.Columns[7].Visible = false;
                        this.gvEmpOtherded.Columns[8].Visible = false;
                        this.gvEmpOtherded.Columns[11].Visible = false;
                        this.gvEmpOtherded.Columns[12].Visible = true;
                        this.gvEmpOtherded.Columns[13].Visible = true;
                        this.gvEmpOtherded.Columns[14].Visible = false;
                        this.gvEmpOtherded.Columns[15].Visible = false;
                        this.gvEmpOtherded.Columns[17].Visible = false;
                        this.gvEmpOtherded.Columns[18].Visible = false;
                        this.gvEmpOtherded.Columns[9].Visible = false;
                        this.gvEmpOtherded.Columns[9].HeaderText = "Food";
                        this.gvEmpOtherded.HeaderRow.Cells[13].Text = "Penalty";
                    }
                    if (comcod == "3368")//For Finlay
                    {
                        this.gvEmpOtherded.Columns[6].Visible = false;
                        this.gvEmpOtherded.Columns[7].Visible = false;
                        this.gvEmpOtherded.Columns[8].Visible = true;
                        this.gvEmpOtherded.Columns[9].Visible = false;
                        this.gvEmpOtherded.Columns[10].Visible = true;
                        this.gvEmpOtherded.Columns[11].Visible = false;
                        this.gvEmpOtherded.Columns[12].Visible = true;
                        this.gvEmpOtherded.Columns[13].Visible = false;
                        this.gvEmpOtherded.Columns[14].Visible = false;
                        this.gvEmpOtherded.Columns[15].Visible = false;
                        this.gvEmpOtherded.Columns[17].Visible = false;
                        this.gvEmpOtherded.Columns[18].Visible = false;

                    }
                    if (comcod == "3369")
                    {
                       
                            this.gvothearn.Columns[8].HeaderText = "Penalized";
                            this.gvothearn.Columns[14].HeaderText = "Trainning";
  
                    }
                    if (comcod == "3370" || comcod == "3101")//For Finlay
                    {
                        this.gvEmpOtherded.Columns[6].Visible = false;
                        this.gvEmpOtherded.Columns[7].Visible = false;
                        this.gvEmpOtherded.Columns[8].Visible = false;
                        this.gvEmpOtherded.Columns[9].Visible = true;
                        this.gvEmpOtherded.Columns[10].Visible = true;
                        this.gvEmpOtherded.Columns[11].Visible = false;
                        this.gvEmpOtherded.Columns[12].Visible = true;//otherded
                        this.gvEmpOtherded.Columns[13].Visible = false;
                        this.gvEmpOtherded.Columns[14].Visible = false;
                        this.gvEmpOtherded.Columns[15].Visible = false;
                        this.gvEmpOtherded.Columns[17].Visible = false;
                        this.gvEmpOtherded.Columns[18].Visible = false;
                        this.gvEmpOtherded.Columns[9].HeaderText = "Meal";
                    }


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
                    this.gvothearn.DataBind();
                    this.FooterCalculation();
                    switch (comcod)
                    {
                        case "3339":
                            this.gvothearn.HeaderRow.Cells[9].Text = "Trans/Entr";
                            break;


                        //case "3101":
                        case "3364":
                            this.gvothearn.HeaderRow.Cells[8].Text = "Performance Allowance";
                            this.gvothearn.HeaderRow.Cells[9].Text = "Holiday Allowance";


                            break;

                        case "3365"://For BTI                                   
                            this.gvothearn.HeaderRow.Cells[6].Text = "Earned Leave";
                            this.gvothearn.HeaderRow.Cells[7].Text = "Arear Salary";
                            this.gvothearn.HeaderRow.Cells[8].Text = "Project Visit";
                            //this.gvothearn.HeaderRow.Cells[9].Text = "Car Allowance";
                            this.gvothearn.HeaderRow.Cells[12].Text = "Refund";
                            this.gvothearn.Columns[9].Visible = false;
                            this.gvothearn.Columns[10].Visible = false;
                            this.gvothearn.Columns[14].Visible = false;
                            
                            break;

                        case "3101":
                        case "3366":
                            this.gvothearn.HeaderRow.Cells[7].Text = "Sales Commission";
                            this.gvothearn.HeaderRow.Cells[9].Text = "Lunch Subsidy";
                            break;



                        case "3347":
                            this.gvothearn.Columns[11].Visible = true;
                            this.gvothearn.Columns[14].Visible = true;
                            this.gvothearn.Columns[15].Visible = true;
                            this.gvothearn.Columns[16].Visible = true;
                            break;

                        case "3370"://For CPDL                                   
                            this.gvothearn.HeaderRow.Cells[6].Text = "Earned Leave";
                            this.gvothearn.HeaderRow.Cells[7].Text = "Arear Salary";                            
                            this.gvothearn.HeaderRow.Cells[8].Text = "Bonus";                            
                            this.gvothearn.Columns[6].Visible = true;
                            this.gvothearn.Columns[7].Visible = true;
                            this.gvothearn.Columns[8].Visible = true;

            
                            this.gvothearn.Columns[9].Visible = false;
                            this.gvothearn.Columns[10].Visible = false;
                            this.gvothearn.Columns[12].Visible = false;
                            this.gvothearn.Columns[13].Visible = false;
                            this.gvothearn.Columns[14].Visible = false;
                            this.gvothearn.Columns[15].Visible = false;
                            this.gvothearn.Columns[16].Visible = false;
                            
                            break;

                        default:
                            break;
                    }
                    break;

                case "dayadj":
                    this.grvAdjDay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvAdjDay.DataSource = dt;
                    this.grvAdjDay.DataBind();
                    this.FooterCalculation();
                    break;


                case "SalaryReduction":
                    this.gvsalreduction.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvsalreduction.DataSource = dt;
                    this.gvsalreduction.DataBind();
                    this.FooterCalculation();
                    break;

                case "bonusextra":
                    //this.gvarrear.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.GvAddiBonus.DataSource = dt;
                    this.GvAddiBonus.DataBind();
                    this.FooterCalculation();
                    break;

                case "salaryencashment":
                    this.gvEncashment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEncashment.DataSource = dt;
                    this.gvEncashment.DataBind();
                   // this.FooterCalculation();
                    break;


            }



        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
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
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvfinededuction")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fine)", "")) ? 0.00
                         : dt.Compute("sum(fine)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lbFlgvCashDeduc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashded)", "")) ? 0.00
                        : dt.Compute("sum(cashded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvfinededucdays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finedays)", "")) ? 0.00
                         : dt.Compute("sum(finedays)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvEmpOtherded.FooterRow.FindControl("lblgvFoterTransDed")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(transded)", "")) ? 0.00
                      : dt.Compute("sum(transded)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvEmpOtherded;
                    ((HyperLink)this.gvEmpOtherded.HeaderRow.FindControl("hlbtntbCdataExeldeduct")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

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
                    ((HyperLink)this.gvarrear.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

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

                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFhardship")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hardship)", "")) ? 0.00
                           : dt.Compute("sum(hardship)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvothearn.FooterRow.FindControl("lgvFtripamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tripal)", "")) ? 0.00
                          : dt.Compute("sum(tripal)", ""))).ToString("#,##0;(#,##0); ");


                    //string msg = "Total Other EarningG " + ((Label)this.gvothearn.FooterRow.FindControl("lgvFtotal")).Text;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                    //  this.GetCheckBoxStates();
                    Session["Report1"] = gvothearn;
                    ((HyperLink)this.gvothearn.HeaderRow.FindControl("hlbtntOtherEarnExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "dayadj":
                    ((Label)this.grvAdjDay.FooterRow.FindControl("lgvFDelday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dalday)", "")) ? 0.00
                        : dt.Compute("sum(dalday)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvAdjDay.FooterRow.FindControl("lgvFAdj")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dedday)", "")) ? 0.00
                        : dt.Compute("sum(dedday)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "SalaryReduction":


                    ((Label)this.gvsalreduction.FooterRow.FindControl("lgvFpresal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(grossal)", "")) ? 0.00 : dt.Compute("sum(grossal)", ""))).ToString("#,##0;(#,##0);");
                    ((Label)this.gvsalreduction.FooterRow.FindControl("lgvFredamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(redamt)", "")) ? 0.00 : dt.Compute("sum(redamt)", ""))).ToString("#,##0;(#,##0);");
                    // ((Label)this.gvsalreduction.FooterRow.FindControl("lgvfredamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(finredamt)", "")) ? 0.00 : dt.Compute("sum(finredamt)", ""))).ToString("#,##0;(#,##0);");


                    break;


                case "bonusextra":
                    ((Label)this.GvAddiBonus.FooterRow.FindControl("lgvFAddibonus")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00
                            : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                  
                    Session["Report1"] = gvarrear;
                    ((HyperLink)this.GvAddiBonus.HeaderRow.FindControl("hlbtntbCdataExeladd")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

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


                case "3347":
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {
                       
                        double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
                    }
                    break;
                case "3368"://Finlay
                 

                    
                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {
                        
                        double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
                    }
                    break;
                case "3370":
                case "3101":


                    for (int i = 0; i < this.gvEmpOverTime.Rows.Count; i++)
                    {

                        //double fixhourrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvFixedrate")).Text.Trim());
                        //double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvhourlyrate")).Text.Trim());
                        //double c1rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc1rate")).Text.Trim());
                        //double c2rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc2rate")).Text.Trim());
                        //double c3rate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvc3rate")).Text.Trim());
                        //double fixamt = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("txtgvfixamt")).Text.Trim());

                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixed")).Visible = fixhourrate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourly")).Visible = hourlyrate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc1")).Visible = c1rate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc2")).Visible = c2rate > 0;
                        //((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvc3")).Visible = c3rate > 0;
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
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BankPayment":
                    this.rptBankPayment();
                    break;

                case "Mobile":
                    this.rptMobileAllowance();
                    break;


                case "salaryencashment":
                    this.rptSalEncashment();
                    break;
                    

            }
        }
        private void rptSalEncashment()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string ymon2 = ymon.Substring(4, 2) +"-01"  + "-" + ymon.Substring(0, 4);
            string txtdate = Convert.ToDateTime(ymon2).ToString("MMM-yyyy");

            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            int index = this.btnRadio.SelectedIndex;

            DataTable dt = new DataTable();

            switch (index)
            {
                case 0:
                    dt = (DataTable)Session["tblencashment"];
                    break;
                case 1:
                    dt = (DataTable)Session["tblencashsaved"];
                    break;
            }


            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalEncashment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalEncashment", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Encashment Report -"+ txtdate));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
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

                //case "3101":
                case "3339":// Tropical
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("mbillamt>0");
                    dt = dv.ToTable();
                    //this.MobileAllowTropical();
                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowanceTro", lst, null, null);
                    break;


                case "3338": // ACME
                case "1206"://acme construction
                case "1207"://acme service
                    lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_86_All.EClassAllowance.EClassMobileBill>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_86_All.rptMobileAllowAcme", lst, null, null);
                    break;


                case "3315":// Assure
                    comnam = this.ddlCompanyName.SelectedItem.Text;
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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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

            string comcod = this.GetComeCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            //DataTable dt = (DataTable)Session["tblover"];
            DataTable dt = ((type == "salaryencashment") ? (DataTable)Session["tblencashment"] : (DataTable)Session["tblover"]);

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
                        //double fixamt = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvfixamt")).Text.Trim());
                        double fixamt = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("lblgvfixedamt")).Text.Trim());

                        






                        double fixdaycount = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixedDaycount")).Text.Trim());
                        double dayrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("txtgvdayrate")).Text.Trim());
                        double fixhourcount = Convert.ToDouble("0" + ((TextBox)this.gvEmpOverTime.Rows[i].FindControl("txtgvFixedhour")).Text.Trim());
                        double hourlyrate = Convert.ToDouble("0" + ((Label)this.gvEmpOverTime.Rows[i].FindControl("txtgvhourlyrate")).Text.Trim());
 





                        double tohour = fixhour + hourly + c1hour + c2hour + c3hour;
                        rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEmpOverTime.PageIndex) + i;
                        dt.Rows[rowindex]["fixhour"] = fixhour;
                        dt.Rows[rowindex]["hourly"] = hourly;
                        dt.Rows[rowindex]["c1hour"] = c1hour;
                        dt.Rows[rowindex]["c2hour"] = c2hour;
                        dt.Rows[rowindex]["c3hour"] = c3hour;
                        dt.Rows[rowindex]["tohour"] = tohour;
                        dt.Rows[rowindex]["fixamt"] = fixamt;
                        //if (comcod == "3368")
                        //{

                        //    dt.Rows[rowindex]["fixday"] = fixdaycount;
                        //    dt.Rows[rowindex]["fixhour"] = fixhourcount;
                        //    dt.Rows[rowindex]["totalamt"] = (fixdaycount * dayrate) + (fixhourcount * hourlyrate);
                        //}



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
                        //  string chkcash = ((CheckBox)this.gvEmpOtherded.Rows[i].FindControl("chkCash")).Checked ? "True" : "False";
                        string paystatus = ((DropDownList)this.gvEmpOtherded.Rows[i].FindControl("ddlPaystatus")).SelectedValue.ToString();
                        double fine = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtlgvfineDeduction")).Text.Trim());
                        double finedays = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtlgvfineDeducdays")).Text.Trim());
                        double cashded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("txtlgvCashDeduc")).Text.Trim());
                        double trnsded = Convert.ToDouble("0" + ((TextBox)this.gvEmpOtherded.Rows[i].FindControl("gvTransDed")).Text.Trim());


                        double toamt = otherded + lvded + arded + saladv + mbillded + fallded + cashded + fine + trnsded;
                        rowindex = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + i;
                        dt.Rows[rowindex]["lvded"] = lvded;
                        dt.Rows[rowindex]["arded"] = arded;
                        dt.Rows[rowindex]["saladv"] = saladv;
                        dt.Rows[rowindex]["otherded"] = otherded;
                        dt.Rows[rowindex]["mbillded"] = mbillded;
                        dt.Rows[rowindex]["fallded"] = fallded;
                        dt.Rows[rowindex]["toamt"] = toamt;
                        dt.Rows[rowindex]["cashded"] = cashded;
                        dt.Rows[rowindex]["fine"] = fine;
                        dt.Rows[rowindex]["finedays"] = finedays;
                        dt.Rows[rowindex]["paystatus"] = paystatus;
                        dt.Rows[rowindex]["transded"] = trnsded;

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
                        string chkcash = ((CheckBox)this.gvarrear.Rows[i].FindControl("chkCasharrear")).Checked ? "True" : "False";

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
                        dt.Rows[rowindex]["chkcash"] = chkcash;

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
                        double hardship = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvhardship")).Text.Trim());
                        double tripday = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvtripday")).Text.Trim());
                        double tripal = Convert.ToDouble("0" + ((TextBox)this.gvothearn.Rows[i].FindControl("txtgvtripamt")).Text.Trim());

                        if (comcod == "3347")
                        {
                            foodal = factualday > 0 ? foodal : 0.00;

                        }


                        rowindex = (this.gvothearn.PageSize) * (this.gvothearn.PageIndex) + i;

                        dt.Rows[rowindex]["tptallow"] = tptallow;
                        dt.Rows[rowindex]["kpi"] = kpi;
                        dt.Rows[rowindex]["perbon"] = perbon;
                        dt.Rows[rowindex]["othearn"] = otherearn;
                        dt.Rows[rowindex]["haircutal"] = haircutal;
                        dt.Rows[rowindex]["foodal"] = foodal;
                        dt.Rows[rowindex]["nfoodal"] = nfoodal;
                        dt.Rows[rowindex]["factualday"] = factualday;
                        dt.Rows[rowindex]["hardship"] = hardship;
                        dt.Rows[rowindex]["tripday"] = tripday;
                        dt.Rows[rowindex]["tripal"] = tripal;



                        dt.Rows[rowindex]["totalam"] = tptallow + kpi + perbon + otherearn + haircutal + foodal + nfoodal + hardship + tripal;

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

                case "SalaryReduction":


                    for (int i = 0; i < this.gvsalreduction.Rows.Count; i++)
                    {

                        double grossal = Convert.ToDouble("0" + ((Label)this.gvsalreduction.Rows[i].FindControl("lblgvpreamt")).Text.Trim());
                        double redpercnt = Convert.ToDouble("0" + ((TextBox)this.gvsalreduction.Rows[i].FindControl("txtgvredcpercnt")).Text.Trim());
                        double redamt = Convert.ToDouble("0" + ((TextBox)this.gvsalreduction.Rows[i].FindControl("txtgvredamt")).Text.Trim());

                        rowindex = (gvsalreduction.PageIndex) * gvsalreduction.PageSize + i;


                        redpercnt = redpercnt > 0 ? redpercnt : grossal > 0 ? (redamt * 100) / grossal : 0.00;
                        redamt = redamt > 0 ? redamt : redpercnt > 0 ? (grossal * 0.01 * redpercnt) : 0.00;
                        rowindex = (this.gvsalreduction.PageSize) * (this.gvsalreduction.PageIndex) + i;
                        dt.Rows[rowindex]["grossal"] = grossal;
                        dt.Rows[rowindex]["redpercnt"] = redpercnt;
                        dt.Rows[rowindex]["redamt"] = redamt;



                    }
                    break;

                case "bonusextra":
                    for (int i = 0; i < this.GvAddiBonus.Rows.Count; i++)
                    {
                        double pf = 0.00;
                        double bacic = 0.00;
                        double addbonus = Convert.ToDouble("0" + ((TextBox)this.GvAddiBonus.Rows[i].FindControl("txtaddbonus")).Text.Trim());
                        string paystatus = ((DropDownList)this.GvAddiBonus.Rows[i].FindControl("ddlPaystatusaddi")).SelectedValue.ToString();

                        rowindex = (this.GvAddiBonus.PageSize) * (this.GvAddiBonus.PageIndex) + i;

                        dt.Rows[rowindex]["bonamt"] = addbonus;
                        // dt.Rows[rowindex]["pfamt"] = pf;
                       // dt.Rows[rowindex]["tapfamt"] = arrear - pf;
                        dt.Rows[rowindex]["chkcash"] = paystatus;

                    }
                    break;

                case "salaryencashment":
                   // DataTable dt2 = (DataTable)Session["tblencashment"];

                    for (int i = 0; i < this.gvEncashment.Rows.Count; i++)
                    {

                        rowindex = (this.gvEmpOverTime.PageSize) * (this.gvEncashment.PageIndex) + i;
                        double balleave = Convert.ToDouble("0" + ((TextBox)this.gvEncashment.Rows[i].FindControl("txtballve")).Text.Trim());
                        double salary = Convert.ToDouble("0" + ((Label)this.gvEncashment.Rows[i].FindControl("lblsal")).Text.Trim());
                        double encashamt = salary * 12 / 365 * balleave;
                        dt.Rows[rowindex]["encashamt"] = encashamt;
                        dt.Rows[rowindex]["elencashday"] = balleave;

                    }
                    Session["tblencashment"] = dt;
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
                //double fixhour = Convert.ToDouble(dt.Rows[i]["fixhour"]);
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
                double fixamtx = Convert.ToDouble(dt.Rows[i]["fixamt"]);

                string fixamt = (comcod == "3370" || comcod == "3101") ? fixamtx.ToString() : (fixhour * fixrate).ToString();

                string houramt = (hourly * hourrate).ToString();
                string c1amt = (c1hour * c1rate).ToString();
                string c2amt = (c2hour * c2rate).ToString();
                string c3amt = (c3hour * c3rate).ToString();
                double tohour = Convert.ToDouble(dt.Rows[i]["tohour"]); ;

                double daycount = 0.0;
                double dayrate = 0.0;
                double syshour = 0.0;
                double syshourrate = 0.0;
                double dayamt = 0.0;
                double totalamt = 0.0;

                if ( comcod=="3369")
                {
                    daycount = Convert.ToDouble(dt.Rows[i]["fixday"]);
                    dayrate = Convert.ToDouble(dt.Rows[i]["holidayrate"]);
                    syshour = Convert.ToDouble(dt.Rows[i]["fixhour"]);
                    syshourrate = Convert.ToDouble(dt.Rows[i]["fixrate"]);
                    dayamt = daycount * dayrate;
                    result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEOVRTIME", dayid, empid, gcod, date, fixhour.ToString(), hourly.ToString(), c1hour.ToString(), c2hour.ToString(), c3hour.ToString(), fixamt, houramt, c1amt, c2amt, c3amt, daycount.ToString(), dayrate.ToString(), dayamt.ToString(), "", "", "", "");
                    if (!result)
                        return;
                }
                
                
                
                
                
                else
                {

                    //if (tohour > 0)
                    //{

                        result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEOVRTIME", dayid, empid, gcod, date, fixhour.ToString(), hourly.ToString(), c1hour.ToString(), c2hour.ToString(), c3hour.ToString(), fixamt, houramt, c1amt, c2amt, c3amt, daycount.ToString(), dayrate.ToString(), dayamt.ToString(), "", "", "", "");

                    if (!result)
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + HRData.ErrorObject["Msg"] + "');", true);
                        return;

                    }
                           
                   // }
                    
                    
                    
                    
                    
                    
                    //switch (comcod)                
                    
                    //{
                    //case "3101":
                    //case "3370":
                    //    if (fixamtx > 0)
                    //    {
                    //        result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEOVRTIME", dayid, empid, gcod, date, fixhour.ToString(), hourly.ToString(), c1hour.ToString(), c2hour.ToString(), c3hour.ToString(), fixamt, houramt, c1amt, c2amt, c3amt, daycount.ToString(), dayrate.ToString(), dayamt.ToString(), "", "", "", "");
                    //        if (!result)
                    //            return;
                    //    }
                    //    break;

                    //}

            }


            }
            ShowOvertime();

             msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

        }

        private void lnkbtnRecalculate_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

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
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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
                double ecleave = Convert.ToDouble(dt.Rows[i]["ecleave"]);
                if (ecleave > 0)
                {

                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEELEAVE", dayid, empid, gcod, date, eleave, ecleave.ToString(), "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;
                }
            }

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
        }
        protected void lbtnTotalOtherDed_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbntUpdateOtherDed_Click(object sender, EventArgs e)
        {
            this.Master.FindControl("lblmsg").Visible = true;
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
                //if (toamt > 0)
                //{
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPOTHERDED", Monthid, empid, lvded, arded, saladv, otherded, mbillded, fallded, paystatus, fine, cashded, finedays, transded, "", "");
                    if (!result)
                        return;
                //}
                //else if (toamt == 0 && fineday > 0)
                //{
                //    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPEMPOTHERDED", Monthid, empid, lvded, arded, saladv, otherded, mbillded, fallded, paystatus, fine, cashded, finedays, transded, "", "");
                //    if (!result)
                //        return;
                //}
           }
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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
            string type = this.Request.QueryString["Type"].ToString().Trim();
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
                case "SalaryReduction":
                case "bonusextra":

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
                case "salaryencashment":
                    secid = dt1.Rows[0]["empid"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["empid"].ToString() == secid)
                        {
                            secid = dt1.Rows[j]["empid"].ToString();
                            dt1.Rows[j]["ttlencashamt"] = "0";
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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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

                string chkcash = ((CheckBox)this.gvarrear.Rows[i].FindControl("chkCasharrear")).Checked ? "True" : "False";
                rowindex = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;
                dt.Rows[rowindex]["aramt"] = arrear;
                dt.Rows[rowindex]["pfamt"] = pf;
                dt.Rows[rowindex]["tapfamt"] = arrear - pf;
                dt.Rows[rowindex]["chkcash"] = chkcash;
            }
            Session["tblover"] = dt;
            this.Data_Bind();
        }

        protected void lbntUpdateArrear_Click(object sender, EventArgs e)
        {
            this.Master.FindControl("lblmsg").Visible = true;
            this.lbtnTotalArrear_Click(null, null);
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            string Remarks = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                //string gcod = dt.Rows[i]["gcod"].ToString();
                double arrer = Convert.ToDouble("0" + dt.Rows[i]["aramt"]);
                double pfamt = Convert.ToDouble("0" + dt.Rows[i]["pfamt"]);
                string chkcash = dt.Rows[i]["chkcash"].ToString();
                if (arrer > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTARREAR", Monthid, empid, arrer.ToString(), pfamt.ToString(), chkcash, userid, postDat, trmid, sessionid, Remarks, "", "", "", "", "");
                    if (!result)
                        return;
                }
            }
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

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
            this.Master.FindControl("lblmsg").Visible = true;
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
                string hardship = Convert.ToDouble(dr["hardship"]).ToString();
                string tripday = Convert.ToDouble(dr["tripday"]).ToString();
                string tripal = Convert.ToDouble(dr["tripal"]).ToString();


                //if (totalam > 0)
                //{
                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTOTHEARN", Monthid, empid, tptallow, kpi, perbon, othearn, haircutal, foodal, nfoodal, factualday, hardship, tripday, tripal, "", "");
                if (!result)
                {
                  
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + HRData.ErrorObject["Msg"].ToString() + "');", true);
                    return;
                }
               // }
            }
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            this.Data_Bind();
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
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

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

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
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
            //string comnam = (this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";

            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //string nozero = (hrcomln == 4) ? "0000" : "00";
            //string txtCompanyname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string comnam = this.ddlCompanyName.SelectedValue.ToString().Substring(0, hrcomln) + "%";


            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
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
        private string GetCompOtherDeduc()
        {
            string comothdedtype = "";
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3365"://BTI
                    comothdedtype = "comothdedtype";
                    break;

                default:
                    break;




            }
            return comothdedtype;



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
            string field = "";
            for (int i=0;  i<chkfield.Items.Count; i++)
            {

                if (chkfield.Items[i].Selected)
                {
                    if (chkfield.Items[i].Value.ToString() == "000")
                    {
                        field = "";
                        break;
                    }

                    else
                    {
                        field = field+ chkfield.Items[i].Value.ToString()+",";


                    }
                
                
                }

               

                //if (item.SelectedValue == "000")
                //{

                //    break;
                //}

                //else
                //{ 


                //}

            }
            field = field.Length > 0 ? field.Substring(0, field.Length - 1) : field;
            string comothdedtype = this.GetCompOtherDeduc();
            string monthid = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "COPYEMPOTHERDEDUCTION", deptname, MonthId, date, comnam, Empcode, section, field, comothdedtype, monthid);
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
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string MonthId = this.ddlPremEarn.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string field = "";
            for (int i = 0; i < this.chkotherearn.Items.Count; i++)
            {

                if (chkotherearn.Items[i].Selected)
                {
                    if (chkotherearn.Items[i].Value.ToString() == "000")
                    {
                        field = "";
                        break;
                    }

                    else
                    {
                        field = field + chkotherearn.Items[i].Value.ToString() + ",";


                    }


                }
            }
            field = field.Length > 0 ? field.Substring(0, field.Length - 1) : field;
            string comothdedtype = this.GetCompOtherDeduc();
            string monthid = this.ddlyearmon.SelectedValue.ToString();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "COPYEMPOTHEARNING", compname, MonthId, date, deptname, Empcode, section, field, comothdedtype, monthid);
            if (ds2 == null)
            {
                this.gvothearn.DataSource = null;
                this.gvothearn.DataBind();
                return;
            }
            Session["tblover"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
           
            this.ChkEarn.Checked = false;
            this.PnlEarn.Visible = (this.ChkEarn.Checked);
            // this.ChkEarn_CheckedChanged(null, null);
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
            this.ddlPremEarn.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
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
            this.Data_Bind();
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void gvsalreduction_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvsalreduction.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnTotalsred_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnRound_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];

            int TblRowIndex;
            for (int i = 0; i < this.gvsalreduction.Rows.Count; i++)
            {

                double redamt = Convert.ToDouble("0" + ((TextBox)this.gvsalreduction.Rows[i].FindControl("txtgvredamt")).Text.Trim());
                TblRowIndex = (gvsalreduction.PageIndex) * gvsalreduction.PageSize + i;
                dt.Rows[TblRowIndex]["redamt"] = redamt;
            }
            Session["tblover"] = dt;
            this.Data_Bind();

        }
        protected void lbtnPutSameValue_Click(object sender, EventArgs e)
        {


            DataTable dt = (DataTable)Session["tblover"];
            double redpercnt = Convert.ToDouble(dt.Rows[0]["redpercnt"]);
            for (int i = 1; i < dt.Rows.Count; i++)
            {

                double grossal = Convert.ToDouble(dt.Rows[i]["grossal"]);
                dt.Rows[i]["redpercnt"] = redpercnt;
                dt.Rows[i]["redamt"] = grossal * 0.01 * redpercnt;

            }
            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void lnkFiUpdatesred_Click(object sender, EventArgs e)
        {



            this.SaveValue();
            DataTable dt = (DataTable)Session["tblover"];
            string comcod = this.GetComeCode();
            string ymon = this.ddlyearmon.SelectedValue.ToString();

            bool result = false;
            foreach (DataRow dr1 in dt.Rows)
            {
                string empid = dr1["empid"].ToString();
                string grossal = dr1["grossal"].ToString();
                string redpercnt = dr1["redpercnt"].ToString();
                string redamt = dr1["redamt"].ToString();




                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATESALREDUCTION", ymon, empid, grossal, redpercnt, redamt, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {

                    msg = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                    return;
                }

            }

            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
        }


        //protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        //{
        //     DataTable dt = (DataTable)Session["tblover"];
        //    int i,index;
        //    if (((CheckBox)this.gvEmpOtherded.HeaderRow.FindControl("chkAllfrm")).Checked)
        //    {

        //        for (i = 0; i < gvEmpOtherded.Rows.Count; i++)
        //        {
        //            ((CheckBox)this.gvEmpOtherded.Rows[i].FindControl("chkCash")).Checked = true;
        //            index = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + i;
        //            dt.Rows[index]["chkcash"] = "True";


        //        }


        //    }



        //    else
        //    {
        //        for (i = 0; i < gvEmpOtherded.Rows.Count; i++)
        //        {
        //            ((CheckBox)this.gvEmpOtherded.Rows[i].FindControl("chkCash")).Checked = false;
        //            index = (this.gvEmpOtherded.PageSize) * (this.gvEmpOtherded.PageIndex) + i;
        //            dt.Rows[index]["saltrn"] = "False";

        //        }

        //    }

        //    Session["tblover"] = dt;
        //}


        protected void lnkarrearcopy_Click(object sender, EventArgs e)
        {

            Session.Remove("tblover");
            string comcod = this.GetComeCode();


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlprearrear.SelectedValue.ToString();
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
        protected void chkarrearcopy_CheckedChanged(object sender, EventArgs e)
        {

            if (this.chkarrearcopy.Checked)
            {
                this.GetPreYearArrear();
            }
            this.Pnlarrer.Visible = (this.chkarrearcopy.Checked);

        }
        protected void chkAllArrearfrm_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblover"];
            int i, index;
            if (((CheckBox)this.gvarrear.HeaderRow.FindControl("chkAllArrearfrm")).Checked)
            {

                for (i = 0; i < gvarrear.Rows.Count; i++)
                {
                    ((CheckBox)this.gvarrear.Rows[i].FindControl("chkCash")).Checked = true;
                    index = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;
                    dt.Rows[index]["chkcash"] = "True";
                }
            }
            else
            {
                for (i = 0; i < gvarrear.Rows.Count; i++)
                {
                    ((CheckBox)this.gvarrear.Rows[i].FindControl("chkCash")).Checked = false;
                    index = (this.gvarrear.PageSize) * (this.gvarrear.PageIndex) + i;
                    dt.Rows[index]["saltrn"] = "False";
                }
            }
            Session["tblover"] = dt;

        }
        protected void gvEmpOtherded_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            string mCOMCOD = comcod;

            string lblpaystatus = ((Label)e.Row.FindControl("lblpaystatus")).Text;
            DropDownList ddlPaystatus = ((DropDownList)e.Row.FindControl("ddlPaystatus"));
            // string lblpaystatus1 = ((Label)e.Row.FindControl("paystatus1")).Text;

            if (lblpaystatus == "1")
            {
                ddlPaystatus.SelectedValue = "1";
            }

            else if (lblpaystatus == "2")
            {
                ddlPaystatus.SelectedValue = "2";
            }

            else
            {
                ddlPaystatus.SelectedValue = "0";
            }

        }
        protected void btnUploadovrtime_Click(object sender, EventArgs e)
        {
            Session.Remove("tblover");
            string comcod = this.GetComeCode();


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string comnam = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon;// +"01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            txtdate = Convert.ToDateTime(txtdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPlOYEE_OVERTIMEUPLAOD", "GETEMPLOYEOVRTIMEUPLOAD", deptname, dayid, txtdate, comnam, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvEmpOverTime.DataSource = null;
                this.gvEmpOverTime.DataBind();
                return;
            }
            Session["tblover"] = HiddenSameData(ds2.Tables[0]);

            this.Data_Bind();
            this.lTotal_Click(null, null);
            //this.SaveValue();

        }


        private void GetCheckBoxStates()
        {
            if (gvothearn.HeaderRow != null)
            {
                CheckBox chksl = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chksl");


                CheckBox chkCol0 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol0");
                CheckBox chkCol1 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol1");
                CheckBox chkCol2 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol2");
                CheckBox chkCol3 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol3");
                CheckBox chkCol4 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol4");
                CheckBox chkCol5 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol5");
                CheckBox chkCol6 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol6");
                CheckBox chkCol7 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol7");
                CheckBox chkCol8 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol8");
                CheckBox chkCol9 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol9");
                CheckBox chkCol10 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol10");
                CheckBox chkCol11 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol11");
                CheckBox chkCol12 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol12");
                //CheckBox chkCol13 = (CheckBox)gvothearn.HeaderRow.Cells[0].FindControl("chkCol13");

                ArrayList arr;
                if (ViewState["States"] == null)
                {
                    arr = new ArrayList();
                }
                else
                {
                    arr = (ArrayList)ViewState["States"];
                }

                arr.Add(chkCol0.Checked);
                arr.Add(chkCol1.Checked);
                arr.Add(chkCol2.Checked);
                arr.Add(chkCol3.Checked);
                arr.Add(chkCol4.Checked);
                arr.Add(chkCol5.Checked);
                arr.Add(chkCol6.Checked);
                arr.Add(chkCol7.Checked);
                arr.Add(chkCol8.Checked);
                arr.Add(chkCol9.Checked);
                arr.Add(chkCol10.Checked);
                arr.Add(chkCol11.Checked);
                arr.Add(chkCol12.Checked);

                ViewState["States"] = arr;
            }

        }

        private void Data_Binds()
        {
            DataTable dt = (DataTable)Session["tblover"];
            this.gvothearn.DataSource = dt;
            this.gvothearn.DataBind();
        }
        protected void btnExportOtherEarnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Data_Binds();
                Response.Clear();
                Response.Buffer = true;

                Response.AddHeader("content-disposition", "attachment;filename=Clientlist.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvothearn.AllowPaging = false;

                //GridView1.DataBind();
                gvothearn.HeaderRow.Style.Add("background-color", "#FFFFFF");
                gvothearn.HeaderRow.Style.Add("color", "#FFFFFF");
                gvothearn.HeaderRow.Style.Add("vertical-align", "middle");
                gvothearn.HeaderRow.Cells[4].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[5].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[6].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[7].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[8].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[9].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[10].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[11].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[12].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[12].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[14].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[15].Style.Add("background-color", "green");
                gvothearn.HeaderRow.Cells[16].Style.Add("background-color", "green");
                //gvothearn.HeaderRow.Cells[13].Style.Add("background-color", "green");
                //gvothearn.HeaderRow.Cells[14].Style.Add("background-color", "green");

                ArrayList arr = (ArrayList)ViewState["States"];

                gvothearn.HeaderRow.Cells[4].Visible = Convert.ToBoolean(arr[0]);
                gvothearn.HeaderRow.Cells[5].Visible = Convert.ToBoolean(arr[1]);
                gvothearn.HeaderRow.Cells[6].Visible = Convert.ToBoolean(arr[2]);
                gvothearn.HeaderRow.Cells[7].Visible = Convert.ToBoolean(arr[3]);
                gvothearn.HeaderRow.Cells[8].Visible = Convert.ToBoolean(arr[4]);
                gvothearn.HeaderRow.Cells[9].Visible = Convert.ToBoolean(arr[5]);
                gvothearn.HeaderRow.Cells[10].Visible = Convert.ToBoolean(arr[6]);
                gvothearn.HeaderRow.Cells[11].Visible = Convert.ToBoolean(arr[7]);
                gvothearn.HeaderRow.Cells[12].Visible = Convert.ToBoolean(arr[8]);
                gvothearn.HeaderRow.Cells[13].Visible = Convert.ToBoolean(arr[9]);
                gvothearn.HeaderRow.Cells[14].Visible = Convert.ToBoolean(arr[10]);
                gvothearn.HeaderRow.Cells[15].Visible = Convert.ToBoolean(arr[11]);
                gvothearn.HeaderRow.Cells[16].Visible = Convert.ToBoolean(arr[12]);
                //gvothearn.HeaderRow.Cells[13].Visible = Convert.ToBoolean(arr[13]);
                //gvothearn.HeaderRow.Cells[14].Visible = Convert.ToBoolean(arr[14]);

                gvothearn.HeaderRow.Cells[4].FindControl("chkCol0").Visible = false; // company 4
                gvothearn.HeaderRow.Cells[5].FindControl("chkCol1").Visible = false; // fullname 5
                gvothearn.HeaderRow.Cells[6].FindControl("chkCol2").Visible = false; // email 6
                gvothearn.HeaderRow.Cells[7].FindControl("chkCol3").Visible = false; // mobile 7
                gvothearn.HeaderRow.Cells[8].FindControl("chkCol4").Visible = false; // username 8
                gvothearn.HeaderRow.Cells[9].FindControl("chkCol5").Visible = false; // password -9
                gvothearn.HeaderRow.Cells[10].FindControl("chkCol6").Visible = false; // registration 13
                gvothearn.HeaderRow.Cells[11].FindControl("chkCol7").Visible = false; // active date 14
                gvothearn.HeaderRow.Cells[12].FindControl("chkCol8").Visible = false;  //expire 15 
                gvothearn.HeaderRow.Cells[12].FindControl("chkCol9").Visible = false; // user type-16
                gvothearn.HeaderRow.Cells[14].FindControl("chkCol10").Visible = false; // reaming -17
                gvothearn.HeaderRow.Cells[15].FindControl("chkCol11").Visible = false; // user role -18
                gvothearn.HeaderRow.Cells[16].FindControl("chkCol12").Visible = false; // status -19
                                                                                       //gvothearn.HeaderRow.Cells[13].FindControl("chkCol13").Visible = false; // Action -20

                for (int i = 0; i < gvothearn.Rows.Count; i++)
                {
                    GridViewRow row = gvothearn.Rows[i];
                    row.Cells[4].Visible = Convert.ToBoolean(arr[0]);
                    row.Cells[5].Visible = Convert.ToBoolean(arr[1]);
                    row.Cells[6].Visible = Convert.ToBoolean(arr[2]);
                    row.Cells[7].Visible = Convert.ToBoolean(arr[3]);
                    row.Cells[8].Visible = Convert.ToBoolean(arr[4]);
                    row.Cells[9].Visible = Convert.ToBoolean(arr[5]);
                    row.Cells[10].Visible = Convert.ToBoolean(arr[6]);
                    row.Cells[11].Visible = Convert.ToBoolean(arr[7]);
                    row.Cells[12].Visible = Convert.ToBoolean(arr[8]);
                    row.Cells[12].Visible = Convert.ToBoolean(arr[9]);
                    row.Cells[14].Visible = Convert.ToBoolean(arr[10]);
                    row.Cells[15].Visible = Convert.ToBoolean(arr[11]);
                    row.Cells[16].Visible = Convert.ToBoolean(arr[12]);
                    //row.Cells[13].Visible = Convert.ToBoolean(arr[13]);
                    //row.Cells[14].Visible = Convert.ToBoolean(arr[14]);

                    row.BackColor = System.Drawing.Color.White;
                    row.Attributes.Add("class", "textmode");
                    if (i % 2 != 0)
                    {
                        row.Cells[4].Style.Add("background-color", "#C2D69B");
                        row.Cells[5].Style.Add("background-color", "#C2D69B");
                        row.Cells[6].Style.Add("background-color", "#C2D69B");
                        row.Cells[7].Style.Add("background-color", "#C2D69B");
                        row.Cells[8].Style.Add("background-color", "#C2D69B");
                        row.Cells[9].Style.Add("background-color", "#C2D69B");
                        row.Cells[10].Style.Add("background-color", "#C2D69B");
                        row.Cells[11].Style.Add("background-color", "#C2D69B");
                        row.Cells[12].Style.Add("background-color", "#C2D69B");
                        row.Cells[12].Style.Add("background-color", "#C2D69B");
                        row.Cells[14].Style.Add("background-color", "#C2D69B");
                        row.Cells[15].Style.Add("background-color", "#C2D69B");
                        row.Cells[16].Style.Add("background-color", "#C2D69B");
                        //row.Cells[13].Style.Add("background-color", "#C2D69B");
                        //row.Cells[14].Style.Add("background-color", "#C2D69B");

                    }
                }             
                gvothearn.RenderControl(hw);
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void lbtnDedorOtherEernExcelAdjust_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();

            bool isAllValid = true;
            DataTable dt = (DataTable)Session["ExcelData"];
            int rowCount = 0;

            DataTable dt1 = (DataTable)Session["tblover"];
            if (dt.Rows.Count == 0 || dt1.Rows.Count == 0)
            {
                return;
            }
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "OtherDeduction": 
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Card = dt.Rows[i]["Card"].ToString();
                        // string Adv_Deduction = dt.Rows[i]["Adv_Deduction"].ToString().Length == 0 ? "0" : dt.Rows[i]["Adv_Deduction"].ToString();
                        string Mobile_Bill = "0.00"; //dt.Rows[i]["Mobile_Bill"].ToString().Length == 0 ? "0" : dt.Rows[i]["Mobile_Bill"].ToString();
                        string Other_Deduction = "0.00"; //(comcod=="3370"? "0" : dt.Rows[i]["Other_Deduction"].ToString().Length == 0 ? "0" : dt.Rows[i]["Other_Deduction"].ToString());
                        string Transport ="0.00";//dt.Rows[i]["Transport"].ToString().Length == 0 ? "0" : dt.Rows[i]["Transport"].ToString();
                        string Penalty = "0.00"; //(comcod == "3370" ? "0" : dt.Rows[i]["Penalty"].ToString().Length == 0 ? "0" : dt.Rows[i]["Penalty"].ToString());

                        string Meal = "0.00";// (comcod == "3370" ? dt.Rows[i]["Meal"].ToString().Length == 0 ? "0" : dt.Rows[i]["Meal"].ToString(): "0");

                        switch (comcod)
                        {
                            case "3370":

                                Mobile_Bill = dt.Rows[i]["Mobile_Bill"].ToString().Length == 0 ? "0" : dt.Rows[i]["Mobile_Bill"].ToString();
                                Meal = dt.Rows[i]["Meal"].ToString().Length == 0 ? "0" : dt.Rows[i]["Meal"].ToString() ;
                                Other_Deduction = dt.Rows[i]["Other_Deduction"].ToString().Length == 0 ? "0" : dt.Rows[i]["Other_Deduction"].ToString();

                                break;
                            default:
                                Mobile_Bill = dt.Rows[i]["Mobile_Bill"].ToString().Length == 0 ? "0" : dt.Rows[i]["Mobile_Bill"].ToString();
                                Other_Deduction = dt.Rows[i]["Other_Deduction"].ToString().Length == 0 ? "0" : dt.Rows[i]["Other_Deduction"].ToString();
                                Penalty = dt.Rows[i]["Penalty"].ToString().Length == 0 ? "0" : dt.Rows[i]["Penalty"].ToString();
                                break;
                        }





                        if (Card.Length == 0)
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }
                        // Check Adv_Deduction is Number or not.
                        if (!IsNuoDecimal(Mobile_Bill))
                        {
                            dt.Rows[i]["Mobile_Bill"] = 0.00;
                        }
                        // Check Other_Deduction is Number or not.
                        if (!IsNuoDecimal(Other_Deduction))
                        {
                            dt.Rows[i]["Other_Deduction"] = 0.00;
                        }
                        // Check Transport is Number or not.
                        if (!IsNuoDecimal(Transport))
                        {
                            dt.Rows[i]["Transport"] = 0.00;
                        }

                        // Check Penalty is Number or not.
                        if (!IsNuoDecimal(Penalty))
                        {
                            dt.Rows[i]["Penalty"] = 0.00;
                        }

                        // Check Penalty is Number or not.
                        if (!IsNuoDecimal(Meal))
                        {
                            dt.Rows[i]["Meal"] = 0.00;
                        }


                        dt.AcceptChanges();
                        isAllValid = true;
                    }
                    if (isAllValid)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            DataRow[] rows = dt.Select("Card ='" + dt1.Rows[i]["idcardno"] + "'");

                            if (rows.Length > 0)
                            {
                                double Mobile_Bill = 0.00;
                                double otherded = 0.00;
                                double Penalty = 0.00;
                                double Meal = 0.00;

                                switch (comcod)
                                {
                                    case "3370":
                                        Mobile_Bill = Convert.ToDouble("0" + (rows[0]["Mobile_Bill"]));
                                        Meal = Convert.ToDouble("0" + (rows[0]["Meal"]));
                                        otherded = Convert.ToDouble("0" + (rows[0]["Other_Deduction"]));

                                        break;
                                    default:
                                        Mobile_Bill = Convert.ToDouble("0" + (rows[0]["Mobile_Bill"]));
                                        otherded = Convert.ToDouble("0" + (rows[0]["Other_Deduction"]));
                                        Penalty = Convert.ToDouble("0" + (rows[0]["Penalty"])); 

                                        break;
                                }


                              //  double Mobile_Bill = Convert.ToDouble("0" + (rows[0]["Mobile_Bill"]));
                              ////  double transded = 0.00;// Convert.ToDouble("0" + (rows[0]["Transport"]));
                              //  double otherded = (comcod == "3370" ? 0 : Convert.ToDouble("0" + (rows[0]["Other_Deduction"])));
                              //  double Penalty = (comcod == "3370" ? 0 : Convert.ToDouble("0" + (rows[0]["Penalty"])));
                              //  double Meal = (comcod == "3370" ? Convert.ToDouble("0" + (rows[0]["Meal"])) : 0);

                                double ttlamt = Mobile_Bill + otherded+ Penalty+ Meal;
                                dt1.Rows[i]["mbillded"] = Mobile_Bill;
                               // dt1.Rows[i]["transded"] = transded;
                                dt1.Rows[i]["otherded"] = otherded;
                                dt1.Rows[i]["fallded"] = Meal;
                                dt1.Rows[i]["fine"] = Penalty;
                                dt1.Rows[i]["toamt"] = ttlamt;
                                rowCount++;
                                dt1.AcceptChanges();
                            }
                        }
                    }
                    break;
                case "otherearn":
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Earned_Leave = "0.00";
                        string Arear_Salary = "0.00";
                        string Project_Visit = "0.00";
                        string Car_Allow = "0.00";
                        string Fooding = "0.00";
                        string Refund = "0.00";
                        string Others = "0.00";
                        string Bonus = "0.00";
                        string Dress_Bill = "0.00";
                        switch (comcod)
                        {
                            case "3365":
                                 Earned_Leave = dt.Rows[i]["Earned_Leave"].ToString().Length == 0 ? "0" : dt.Rows[i]["Earned_Leave"].ToString();
                                 Arear_Salary = dt.Rows[i]["Arear_Salary"].ToString().Length == 0 ? "0" : dt.Rows[i]["Arear_Salary"].ToString();
                                 Project_Visit = dt.Rows[i]["Project_Visit"].ToString().Length == 0 ? "0" : dt.Rows[i]["Project_Visit"].ToString();
                                 //Car_Allow = dt.Rows[i]["Car_Allow"].ToString().Length == 0 ? "0" : dt.Rows[i]["Car_Allow"].ToString();                                 
                                 Refund = dt.Rows[i]["Refund"].ToString().Length == 0 ? "0" : dt.Rows[i]["Refund"].ToString();
                                 Others = dt.Rows[i]["Others"].ToString().Length == 0 ? "0" : dt.Rows[i]["Others"].ToString();                               
                                break;
                            case "3370":
                                Earned_Leave = dt.Rows[i]["Earned_Leave"].ToString().Length == 0 ? "0" : dt.Rows[i]["Earned_Leave"].ToString();
                                Arear_Salary = dt.Rows[i]["Arear_Salary"].ToString().Length == 0 ? "0" : dt.Rows[i]["Arear_Salary"].ToString();
                                Bonus = dt.Rows[i]["Bonus"].ToString().Length == 0 ? "0" : dt.Rows[i]["Bonus"].ToString();
                                
                                break;
                            default:
                                 Earned_Leave = dt.Rows[i]["Earned_Leave"].ToString().Length == 0 ? "0" : dt.Rows[i]["Earned_Leave"].ToString();
                                 Arear_Salary = dt.Rows[i]["Arear_Salary"].ToString().Length == 0 ? "0" : dt.Rows[i]["Arear_Salary"].ToString();
                                 Project_Visit = dt.Rows[i]["Project_Visit"].ToString().Length == 0 ? "0" : dt.Rows[i]["Project_Visit"].ToString();
                                 Car_Allow = dt.Rows[i]["Car_Allow"].ToString().Length == 0 ? "0" : dt.Rows[i]["Car_Allow"].ToString();
                                 Fooding = dt.Rows[i]["Fooding"].ToString().Length == 0 ? "0" : dt.Rows[i]["Fooding"].ToString();
                                 Refund = dt.Rows[i]["Refund"].ToString().Length == 0 ? "0" : dt.Rows[i]["Refund"].ToString();
                                 Others = dt.Rows[i]["Others"].ToString().Length == 0 ? "0" : dt.Rows[i]["Others"].ToString();
                                 Dress_Bill = dt.Rows[i]["Dress_Bill"].ToString().Length == 0 ? "0" : dt.Rows[i]["Dress_Bill"].ToString();
                                break;
                        }
                        string Card = dt.Rows[i]["Card"].ToString();
                      
                        if (Card.Length == 0)
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }

                        if (!IsNuoDecimal(Earned_Leave))
                        {
                            dt.Rows[i]["Earned_Leave"] = 0.00;
                        }

                        if (!IsNuoDecimal(Arear_Salary))
                        {
                            dt.Rows[i]["Arear_Salary"] = 0.00;
                        }

                        if (!IsNuoDecimal(Project_Visit))
                        {
                            dt.Rows[i]["Project_Visit"] = 0.00;
                        }

                        if (!IsNuoDecimal(Car_Allow))
                        {
                            dt.Rows[i]["Car_Allow"] = 0.00;
                        }
                        if (!IsNuoDecimal(Fooding))
                        {
                            dt.Rows[i]["Fooding"] = 0.00;
                        }
                        if (!IsNuoDecimal(Refund))
                        {
                            dt.Rows[i]["Refund"] = 0.00;
                        }
                        if (!IsNuoDecimal(Others))
                        {
                            dt.Rows[i]["Others"] = 0.00;
                        }
                        if (!IsNuoDecimal(Dress_Bill))
                        {
                            dt.Rows[i]["Dress_Bill"] = 0.00;
                        }
                        if (!IsNuoDecimal(Bonus))
                        {
                            dt.Rows[i]["Bonus"] = 0.00;
                        }

                        dt.AcceptChanges();
                        isAllValid = true;

                    }
                    if (isAllValid)
                    {

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            DataRow[] rows = dt.Select("Card ='" + dt1.Rows[i]["idcardno"] + "'");

                            if (rows.Length > 0)
                            {
                                double Earned_Leave = 0.00;
                                double Arear_Salary = 0.00;
                                double Project_Visit = 0.00;
                                double Car_Allow = 0.00;
                                double Fooding = 0.00;
                                double Others = 0.00;
                                double Refund = 0.00;
                                double Dress_Bill = 0.00;
                               


                                switch (comcod)
                                {
                                    case "3365":

                                         Earned_Leave = Convert.ToDouble("0" + rows[0]["Earned_Leave"]);
                                         Arear_Salary = Convert.ToDouble("0" + rows[0]["Arear_Salary"]);
                                         Project_Visit = Convert.ToDouble("0" + rows[0]["Project_Visit"]);

                                         Car_Allow = Convert.ToDouble("0" + rows[0]["Car_Allow"]);                                       
                                         Others = Convert.ToDouble("0" + rows[0]["Others"]);
                                         Refund = Convert.ToDouble("0" + rows[0]["Refund"]);                                      

                                        break;
                                    case "3370":

                                        Earned_Leave = Convert.ToDouble("0" + rows[0]["Earned_Leave"]);
                                        Arear_Salary = Convert.ToDouble("0" + rows[0]["Arear_Salary"]);
                                        Project_Visit = Convert.ToDouble("0" + rows[0]["Bonus"]);                                        


                                        break;

                                    default:
                                         Earned_Leave = Convert.ToDouble("0" + rows[0]["Earned_Leave"]);
                                         Arear_Salary = Convert.ToDouble("0" + rows[0]["Arear_Salary"]);
                                         Project_Visit = Convert.ToDouble("0" + rows[0]["Project_Visit"]);
                                         Car_Allow = Convert.ToDouble("0" + rows[0]["Car_Allow"]);
                                         Fooding = Convert.ToDouble("0" + rows[0]["Fooding"]);
                                         Others = Convert.ToDouble("0" + rows[0]["Others"]);
                                         Refund = Convert.ToDouble("0" + rows[0]["Refund"]);
                                         Dress_Bill = Convert.ToDouble("0" + rows[0]["Dress_Bill"]);
                                        break;
                                }
                                dt1.Rows[i]["tptallow"] = Earned_Leave;
                                dt1.Rows[i]["kpi"] = Arear_Salary;
                                dt1.Rows[i]["perbon"] = Project_Visit;

                                dt1.Rows[i]["haircutal"] = Car_Allow;
                                dt1.Rows[i]["foodal"] = Fooding;
                                dt1.Rows[i]["othearn"] = Others;
                                dt1.Rows[i]["nfoodal"] = Refund;
                                dt1.Rows[i]["hardship"] = Dress_Bill;
                                rowCount++;
                                dt1.AcceptChanges();

                            }
                        }
                    }
                    break;

                case "Overtime":
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string fixamt = "0.00";
                        
                        switch (comcod)
                        {
                            case "3370":
                                fixamt = dt.Rows[i]["fixamt"].ToString().Length == 0 ? "0" : dt.Rows[i]["fixamt"].ToString();                               
                                break;
                           
                            default:
                                fixamt = "0.00";                               
                                break;
                        }
                        string Card = dt.Rows[i]["Card"].ToString();

                        if (Card.Length == 0)
                        {
                            dt.Rows.RemoveAt(i);
                            continue;
                        }

                        if (!IsNuoDecimal(fixamt))
                        {
                            dt.Rows[i]["fixamt"] = 0.00;
                        }

                        dt.AcceptChanges();
                        isAllValid = true;

                    }
                    if (isAllValid)
                    {

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            DataRow[] rows = dt.Select("Card ='" + dt1.Rows[i]["idcardno"] + "'");

                            if (rows.Length > 0)
                            {
                                double fixamt = 0.00;
                                 
                                switch (comcod)
                                {
                                    case "3370":
                                        fixamt = Convert.ToDouble("0" + rows[0]["fixamt"]);
                                        break;
                                    
                                    default:
                                        fixamt = 0.00;                                        
                                        break;
                                }
                                dt1.Rows[i]["fixamt"] = fixamt;
                                
                                rowCount++;
                                dt1.AcceptChanges();

                            }
                        }
                    }
                    break;
            }

            Session["tblover"] = dt1;
            this.Data_Bind();

            string msg = "Total Row Adjust : " + rowCount;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

        }

        private bool IsNumber(string value)
        {
            return value.All(char.IsDigit);
        }
        private bool IsNuoDecimal(string value)
        {
            Regex regexLetter = new Regex(@"^[+-] ? ([0 - 9] +\.?[0 - 9]*|\.[0 - 9]+)+$");
            return !(regexLetter.IsMatch(value));
        }
        private bool IsLetter(string value)
        {
            Regex regexLetter = new Regex(@"^[a-zA-Z]+$");
            return regexLetter.IsMatch(value);
        }
        private bool IsDate(string value)
        {
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(value);
            //Verify whether entered date is Valid date.
            DateTime dt;
            isValid = DateTime.TryParseExact(value, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            return isValid;
        }
        protected void GvAddiBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.GvAddiBonus.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void GvAddiBonus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataTable dt = (DataTable)Session["tblover"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.GvAddiBonus.Rows[e.RowIndex].FindControl("lgvEmpIdadd")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEMPADDBONUS", Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            msg = "Deleted Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            if (result == true)
            {
                int rowindex = (this.GvAddiBonus.PageSize) * (this.GvAddiBonus.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblover");
            Session["tblover"] = dv.ToTable();
            this.Data_Bind();
        }
        protected void lbtnTotalAddi_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblover"];
            int rowindex;
            for (int i = 0; i < this.GvAddiBonus.Rows.Count; i++)
            {
                double addbonus = Convert.ToDouble("0" + ((TextBox)this.GvAddiBonus.Rows[i].FindControl("txtaddbonus")).Text.Trim());
                string paystatus = ((DropDownList)this.GvAddiBonus.Rows[i].FindControl("ddlPaystatusaddi")).SelectedValue.ToString();

                rowindex = (this.GvAddiBonus.PageSize) * (this.GvAddiBonus.PageIndex) + i;

                dt.Rows[rowindex]["bonamt"] = addbonus;
                // dt.Rows[rowindex]["pfamt"] = pf;
                // dt.Rows[rowindex]["tapfamt"] = arrear - pf;
                dt.Rows[rowindex]["chkcash"] = paystatus;
            }
            Session["tblover"] = dt;
            this.Data_Bind();
        }
        protected void lbntUpdateAddition_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Master.FindControl("lblmsg").Visible = true;
            ///this.lbtnTotalArrear_Click(null, null);
            DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetComeCode();
            string Monthid = this.ddlyearmon.Text.Trim();
            string Remarks = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                //string gcod = dt.Rows[i]["gcod"].ToString();
                double bonamt = Convert.ToDouble("0" + dt.Rows[i]["bonamt"]);           
                string chkcash = dt.Rows[i]["chkcash"].ToString();
                if (bonamt > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTADDBONUS", Monthid, empid, bonamt.ToString(), chkcash, userid, postDat, trmid, sessionid, Remarks, "", "", "", "", "");
                    if (!result)
                        return;
                }
            }
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
        }

        protected void GvAddiBonus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            string mCOMCOD = comcod;

            string lblpaystatus = ((Label)e.Row.FindControl("lblpaystatusaddi")).Text;
            DropDownList ddlPaystatus = ((DropDownList)e.Row.FindControl("ddlPaystatusaddi"));
            // string lblpaystatus1 = ((Label)e.Row.FindControl("paystatus1")).Text;

            if (lblpaystatus == "1")
            {
                ddlPaystatus.SelectedValue = "1";
            }
            else
            {
                ddlPaystatus.SelectedValue = "0";
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void lnksyshour_Click(object sender, EventArgs e)
        {
           GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
           int index = row.RowIndex;
           string empid= ((Label)this.gvEmpOverTime.Rows[index].FindControl("lblempid")).Text.ToString();
            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            txtdate = Convert.ToDateTime(txtdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            DataSet ds1 = HRData.GetTransInfo(GetCompCode(), "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETOTDETAILS", empid, dayid, txtdate, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {

                this.gvotDetails.DataSource = null;
                this.gvotDetails.DataBind();
                return;
            }

            DataTable dt = ds1.Tables[0];

            this.gvotDetails.DataSource = dt;
            this.gvotDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenPayslipModal();", true);

        }

        protected void lnksysdaycount_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = ((Label)this.gvEmpOverTime.Rows[index].FindControl("lblempid")).Text.ToString();

            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            txtdate = Convert.ToDateTime(txtdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            DataSet ds1 = HRData.GetTransInfo(GetCompCode(), "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETOTDETAILS", empid, dayid, txtdate, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {

                this.gvotDetails.DataSource = null;
                this.gvotDetails.DataBind();
                return;
            }

            DataTable dt = ds1.Tables[0];

            this.gvotDetails.DataSource = dt;
            this.gvotDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "otdetails();", true);
        }

        protected void gvEmpOverTime_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string type = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            int index = gvr.RowIndex;
            string empid = ((Label)this.gvEmpOverTime.Rows[index].FindControl("lblempid")).Text.ToString();

            string ymon = this.ddlyearmon.SelectedValue.ToString();
            string dayid = ymon + "01";
            string txtdate = ASTUtility.DateFormat("01." + ymon.Substring(4, 2) + "." + ymon.Substring(0, 4));

            txtdate = Convert.ToDateTime(txtdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string calltype = callType();

            DataSet ds1 = HRData.GetTransInfo(GetCompCode(), "dbo_hrm.SP_ENTRY_EMPLOYEE01", calltype, empid,type, dayid, txtdate, "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {

                this.gvotDetails.DataSource = null;
                this.gvotDetails.DataBind();
                return;
            }

            DataTable dt = ds1.Tables[0];
            this.gvotDetails.DataSource = dt;
            this.gvotDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "otdetails();", true);
        }

        public string callType()
        {
            string comcod = GetComeCode();
            string calltype = "";
            switch (comcod)
            {
                //finalay
                case "3368":
                    calltype= "GETOTDETAILSFINLAY";
                    break;

                    //acmeai
                case "3369":
                    calltype = "GETOTDETAILSACMEAI";
                    break;

                //cpdl
                case "3370":
                    calltype = "GETOTDETAILSCPDL";
                    break;

                default:
                    calltype = "GETOTDETAILSFINLAY";
                    break;
            }
            return calltype;
        }

        protected void lnkencashUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            this.SaveValue();
            DataTable dt = (DataTable)Session["tblencashment"];
            string comcod = this.GetComeCode();
            string yearMon = this.ddlyearmon.SelectedValue.ToString();
            string encashMon = this.ddlyearmon.SelectedValue.ToString();


            bool result = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //a.comcod, a.empid, a.salary, a.fdate, a.tdate, a.duration, a.dueelve,  enjoyday=a.lvenjoyed, balleave=a.dueelve-a.lvenjoyed
                // comcod,empid,idcard=idcardno,empname,deptid,refno=secid,desig,doj,frmdat,todat,servlen,ttlv,foragm='',avail=enjlv,elencashday=eleave,presal=format(presal,'N0'), encashamt
                string rowid = dt.Rows[i]["rowid"].ToString();
                string empid = dt.Rows[i]["empid"].ToString();
                string fdate = dt.Rows[i]["frmdat"].ToString();
                string tdate = dt.Rows[i]["todat"].ToString();


                double salary = Convert.ToDouble(dt.Rows[i]["presal"]);
                double duration = Convert.ToDouble(dt.Rows[i]["servlen"]);
                double dueelve = Convert.ToDouble(dt.Rows[i]["ttlv"]);
                double enjoyday = Convert.ToDouble(dt.Rows[i]["avail"]);
                double balleave = Convert.ToDouble(dt.Rows[i]["elencashday"]);
                double servday = Convert.ToDouble(dt.Rows[i]["servday"]);
                string day = Convert.ToDateTime(dt.Rows[i]["doj"]).ToString("dd");
   
   

                if ((servday < 365.00))
                {
                    if (Convert.ToInt32(day) > 25)
                    {

                        encashMon = Convert.ToDateTime(dt.Rows[i]["doj"]).AddMonths(1).AddYears(1).ToString("yyyyMM");
                    }
                    else
                    {
                        encashMon = Convert.ToDateTime(dt.Rows[i]["doj"]).AddYears(1).ToString("yyyyMM");
                    }

                }
                else
                {
                    encashMon = yearMon;
                }



                result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_REPORT_LEAVE_SUMMARY", "INSERTUPDATENCASHMENT", empid,rowid, fdate, tdate, salary.ToString(), duration.ToString(), dueelve.ToString(), enjoyday.ToString(), balleave.ToString(), yearMon,encashMon, servday.ToString(), "","","","","","","","","");
                    if (!result)
                        return;

                }



            ShowSalEncashment();
            msg = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
        }

        protected void savedEncash_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblencashment"];
            DataView dv = dt.DefaultView;
          dv.RowFilter = "issaved = '" + 1 + "'";
   
        }

        protected void btnRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.btnRadio.SelectedIndex;


            DataView dv = new DataView();
            DataTable dt = (DataTable)Session["tblencashment"];


            switch (index)
            {
                case 0:

                    dv = dt.DefaultView;
        
                    this.gvEncashment.DataSource = dv;
                 this.gvEncashment.DataBind();
                    break;

                case 1:
                    dv = dt.DefaultView;
                    dv.RowFilter = "issaved = '" + 1 + "'";
                    this.gvEncashment.DataSource = dv;
                    this.gvEncashment.DataBind();
                    Session["tblencashsaved"] = dv.Table;
                    break;




            }


            }
    }


}