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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalSummary02 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                getBank();

                this.GetMonth();
                this.GetCompany();
                // this.GetDepartment(); 
                this.SelectType();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE SALARY SUMMARY INFORMATION ";

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "Disbursement") ? "Summary of Disbursement" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "TopSalary") ? "Salary Top Sheet" : (this.Request.QueryString["Type"].ToString().Trim() == "TopSheetPID") ? "Salary Top Sheet (Project)" :
                    "EMPLOYEE SALARY SUMMARY INFORMATION ";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                GetEmployeeName();
   
                


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            this.ddlfrmDesig.SelectedValue = "0345001";
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
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SelectType()
        {
            string comcod = this.GetComeCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

                case "CashSalary":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetDesignation();
                    this.GetDessignationTo();

                    switch (comcod)
                    {
                        case "3355":
                            this.rbtnlistsaltypeAddItem();
                            break;
                        case "3354":
                            this.PnlDesign.Visible = true;
                            break;
                        case "3365": // BTI 
                            this.PnlDesign.Visible = false;
                            break;
                    }
                    break;


                case "SalLACA":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "RPTENVELOP":
                    this.lnkbtnShow.Visible = false;
                    break;

                case "CashBonus":
                    this.chkBonustype.Visible = true;
                    this.MultiView1.ActiveViewIndex = 3;
                    this.GetDesignation();
                    this.GetDessignationTo();
                    switch (comcod)
                    {
                        case "3101":
                        case "3354"://Edison Real Estate
                            this.PnlDesign.Visible = true;
                            break;
                    }
                    break;

                case "BonusSummary":
                    this.chkBonustype.Visible = true;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case "BonPaySlip":
                    this.chkBonustype.Visible = true;
                    this.lnkbtnShow.Visible = false;
                    break;

                case "Disbursement":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;

                    break;
                case "NagadSalary":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "TopSalary":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

                case "TopSheetFactory":
                    this.MultiView1.ActiveViewIndex = 8;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

                case "TopSheetPID":
                    this.MultiView1.ActiveViewIndex = 9;
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

            }



        }

        private void rbtnlistsaltypeAddItem()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3355":
                    this.rbtnlistsaltype.Visible = true;
                    this.rbtnlistsaltype.Items.Add(new ListItem("Management", "1"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("Acting Management", "2"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("General Employee", "3"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("All", "4"));
                    this.rbtnlistsaltype.SelectedIndex = 3;
                    break;

                //default acme
                default:
                    this.rbtnlistsaltype.Visible = true;
                    this.rbtnlistsaltype.Items.Add(new ListItem("Management", "1"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("Non Management", "2"));
                    this.rbtnlistsaltype.Items.Add(new ListItem("Both", "3"));
                    this.rbtnlistsaltype.SelectedIndex = 0;
                    break;
            }

        }


        private void GetMonth()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4301":
                case "4305":
                    this.txtfMonth.Text = System.DateTime.Today.ToString("yyyyMM");
                    break;

                default:
                    this.txtfMonth.Text = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
                    break;


            }


        }

        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();

            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.GetDepartment();
            ds1.Dispose();

        }

        private void GetDepartment()
        {

            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDepartName.DataTextField = "actdesc";
            this.ddlDepartName.DataValueField = "actcode";
            this.ddlDepartName.DataSource = ds1.Tables[0];
            this.ddlDepartName.DataBind();
            this.SectionName();
            ds1.Dispose();

        }
        private void SectionName()
        {

            string comcod = this.GetComeCode();
            string projectcode = this.ddlDepartName.SelectedValue.ToString();
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            ds2.Dispose();

        }




        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddlDepartName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }


        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.ShowSalSummary();
                    break;

                case "CashSalary":
                    GetBankName();
                    this.ShowCashSalary();

                    break;
                case "SalLACA":
                    this.ShowSalaLACA();
                    break;
                case "CashBonus":
                    this.ShowCashBonous();
                    break;

                case "BonusSummary":
                    this.ShowBonousSummary();
                    break;

                case "Disbursement":
                    this.ShowDisbursement();
                    break;


                case "NagadSalary":
                    this.ShowNagadSalary();
                    break;

                case "TopSalary":
                    this.ShowTopsalary();
                    break;


                case "TopSheetFactory":
                    this.ShowFatorySummary();
                    break;

                case "TopSheetPID":
                    this.ShowTopSheetPIDSummary();
                    break;






            }
        }

        private void ShowFatorySummary()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            //string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string length = (this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) == "9454" ? "Length" : "");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "SHOWTOPSALARYSHEETFACTORY", month, Company, Department, section, length, "", "", "", "");

            if (ds3 == null)
            {
                this.gvtopsheetfactory.DataSource = null;
                this.gvtopsheetfactory.DataBind();
                return;

            }

            Session["topsalaryfactory"] = ds3.Tables[0];

            this.Data_Bind();
        }


        private void ShowTopSheetPIDSummary()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            //string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString().Substring(0, 9) + "%";


            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTSALARYTOPSHEETPID", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvtopsheetpid.DataSource = null;
                this.gvtopsheetpid.DataBind();
                return;

            }



            Session["topsheetpid"] = ds3.Tables[0];
            Session["sumnagadcashpid"] = ds3.Tables[1];


            this.Data_Bind();

        }
        private void ShowTopsalary()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "SHOWTOPSALARYSHEET", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvtopsalary.DataSource = null;
                this.gvtopsalary.DataBind();
                return;

            }



            Session["topsalary"] = ds3.Tables[0];

            this.Data_Bind();



        }
        private void ShowDisbursement()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "SHOWDISBURSEMENT", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvdisbursement.DataSource = null;
                this.gvdisbursement.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);

            Session["tblSalSum"] = dt;
            Session["tblDisburseSumm"] = ds3.Tables[1];

            this.Data_Bind();

        }
        private void ShowSalSummary()
        {

            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTSALARYSUMMARY", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();

        }

        private void ShowCashSalary()
        {

            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string DesigFrom = "0399999";
            string DesigTo = "0311001";

            string exclumgt = "";
            string mantype = "";

            switch (comcod)
            {
                case "3354":
                    DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
                    DesigTo = this.ddlToDesig.SelectedValue.ToString();
                    break;
                case "3355":
                    //exclumgt = (this.ddlSection.SelectedValue.ToString() == "000000000000" && chkExcluMgt.Checked) ? "exclumgt" : "";
                    mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : (this.rbtnlistsaltype.SelectedIndex == 2) ? "86003%" : "86%";
                    break;

                default:
                     DesigFrom = "0399999";
                     DesigTo = "0311001";
                    exclumgt = "";
                    break;


            }
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTCASHSALARY", month, Company, Department, section, exclumgt, mantype, DesigFrom, DesigTo, "");

            if (ds3 == null)
            {
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();


        }

        //ShowSalaLACR();
        private void ShowSalaLACA()
        {

            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            // string Company = this.ddlCompany.SelectedValue.Substring(0, 2);
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTSALLACR", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvLACA.DataSource = null;
                this.gvLACA.DataBind();
                return;
            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();
        }

        private void ShowCashBonous()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString().Substring(0,9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            string DesigFrom = "";
            string DesigTo = "";

            switch (comcod)
            {
                case "3354"://Edison Real Estate
                    DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
                    DesigTo = this.ddlToDesig.SelectedValue.ToString();
                    break;
              
                default:
                    DesigFrom = "";
                    DesigTo = "";
                    break;


            }

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTCASHBONOUS", month, Company, Department, section, DesigFrom, DesigTo, "", "", "");

            if (ds3 == null)
            {
                this.gvSalSum.DataSource = null;
                this.gvSalSum.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();




        }

        private void ShowBonousSummary()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            //string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString().Substring(0, 9) + "%";

            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            // string CallType = (comcod == "4305") ? "RPTBONSUMMARYRG" : "RPTBONSUMMARY";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTBONSUMMARY", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvBonus.DataSource = null;
                this.gvBonus.DataBind();
                return;

            }

            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblSalSum"] = dt;
            this.Data_Bind();
        }


        private void ShowNagadSalary()
        {

            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTCASHSALARYWNAGADACC", month, Company, Department, section, "", "", "", "", "");

            if (ds3 == null)
            {
                this.gvnsalary.DataSource = null;
                this.gvnsalary.DataBind();
                return;

            }
            Session["tblSalSum"] = ds3.Tables[0];
            this.Data_Bind();


        }



        private DataTable HiddenSameData(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string refno = dt1.Rows[0]["refno"].ToString();

            switch (type)

            {
                case "Disbursement":

                    string section = dt1.Rows[0]["section"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["section"].ToString() == section)
                        {

                            section = dt1.Rows[j]["section"].ToString();
                            dt1.Rows[j]["sectionname"] = "";
                        }

                        else
                        {
                            section = dt1.Rows[j]["section"].ToString();
                        }
                    }
                    break;




                default:

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["refno"].ToString() == refno)
                        {
                            refno = dt1.Rows[j]["refno"].ToString();
                            dt1.Rows[j]["refdesc"] = "";
                        }
                        else
                        {
                            refno = dt1.Rows[j]["refno"].ToString();
                        }


                    }

                    break;
            }

            return dt1;


        }

        private void Data_Bind()
        {
            string comcod = this.GetComeCode();

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.gvSalSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSalSum.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvSalSum.DataBind();
                    this.FooterCalculation((DataTable)Session["tblSalSum"]);


                    break;

                case "CashSalary":
                    DataTable dt1 = (DataTable)Session["tblSalSum"];
                    DataTable tbnk = (DataTable)Session["tblbank"];


                  
                    this.gvcashpay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvcashpay.DataSource = dt1;
                    this.gvcashpay.DataBind();
                    if (this.gvcashpay.Rows.Count > 0)
                    {
                        this.FooterCalculation((DataTable)Session["tblSalSum"]);
                        //Session["Report1"] = gvcashpay;
                        //((HyperLink)this.gvcashpay.HeaderRow.FindControl("hlbtntbCdataExcel22")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    }
                    if ((comcod=="3365")|| comcod=="3101")
                    {
                        this.gvcashpay.Columns[7].Visible = true;
                        this.gvcashpay.Columns[8].Visible = true;
                        this.gvcashpay.Columns[9].Visible = true;

                        DropDownList ddlBankList; TextBox ckdate;
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            ckdate = ((TextBox)this.gvcashpay.Rows[j].FindControl("txtckDate"));
                            ddlBankList = ((DropDownList)this.gvcashpay.Rows[j].FindControl("ddlBankList"));
                            ddlBankList.DataTextField = "actdesc";
                            ddlBankList.DataValueField = "bankcode";
                            ddlBankList.DataSource = tbnk;
                            ddlBankList.DataBind();
                            ddlBankList.Items.Insert(0, new ListItem("--Please Select--", ""));

                            ckdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                        }
                    }
                    else
                    {
                        this.gvcashpay.Columns[7].Visible = false;
                        this.gvcashpay.Columns[8].Visible = false;
                        this.gvcashpay.Columns[9].Visible = false;
                    }

                    


                    break;


                case "SalLACA":
                    this.gvLACA.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvLACA.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvLACA.DataBind();
                    this.FooterCalculation((DataTable)Session["tblSalSum"]);
                    break;

                case "CashBonus":
                    this.gvBonus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBonus.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvBonus.DataBind();
                    this.FooterCalculation((DataTable)Session["tblSalSum"]);
                    break;

                case "BonusSummary":
                    this.gvBonusSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBonusSum.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvBonusSum.DataBind();
                    this.FooterCalculation((DataTable)Session["tblSalSum"]);
                    break;

                case "Disbursement":
                    this.gvdisbursement.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvdisbursement.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvdisbursement.DataBind();
                    this.DibusmentSummary();
                    this.FooterCalculation((DataTable)Session["tblSalSum"]);
                    break;


                case "NagadSalary":

                    this.gvnsalary.DataSource = (DataTable)Session["tblSalSum"];
                    this.gvnsalary.DataBind();
                    this.FooterCalculation((DataTable)Session["tblSalSum"]);

                    Session["Report1"] = gvnsalary;
                    ((HyperLink)this.gvnsalary.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "TopSalary":
                    this.gvtopsalary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtopsalary.DataSource = (DataTable)Session["topsalary"];
                    this.gvtopsalary.DataBind();
                    Session["Report1"] = gvtopsalary;
                    ((HyperLink)this.gvtopsalary.HeaderRow.FindControl("hlbtntTopSheetExcel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;
                //this.FooterCalculation((DataTable)Session["tblSalSum"]);


                case "TopSheetFactory":
                    this.gvtopsheetfactory.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtopsheetfactory.DataSource = (DataTable)Session["topsalaryfactory"];
                    this.gvtopsheetfactory.DataBind();
                    Session["Report1"] = gvtopsheetfactory;
                    ((HyperLink)this.gvtopsheetfactory.HeaderRow.FindControl("hlbtntTopSheetExcelFactory")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "TopSheetPID":
                    this.gvtopsheetpid.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtopsheetpid.DataSource = (DataTable)Session["topsheetpid"];
                    this.gvtopsheetpid.DataBind();
                    this.SumPidNagadCash();
                    this.FooterCalculation((DataTable)Session["topsheetpid"]);

                    Session["Report1"] = gvtopsheetpid;
                    ((HyperLink)this.gvtopsheetpid.HeaderRow.FindControl("hlbtntTopSheetExcelpid")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

            }


        }


        private void DibusmentSummary()
        {
            DataTable dt2 = (DataTable)Session["tblDisburseSumm"];

            if (dt2.Rows.Count == 0)
                return;

            this.gvdisbursummary.DataSource = dt2;
            this.gvdisbursummary.DataBind();

        }


        private void SumPidNagadCash()
        {
            DataTable dt2 = (DataTable)Session["sumnagadcashpid"];

            if (dt2.Rows.Count == 0)
                return;

            this.gvnagadcashpid.DataSource = dt2;
            this.gvnagadcashpid.DataBind();

        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":

                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFbSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFarier")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oth)", "")) ? 0.00 : dt.Compute("sum(oth)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFtallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tallow)", "")) ? 0.00 : dt.Compute("sum(tallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFgssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFgspay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFtded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalSum.FooterRow.FindControl("lgvFnetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "CashSalary":
                    ((Label)this.gvcashpay.FooterRow.FindControl("lgvFTNetmtcash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    Session["Report1"] = gvcashpay;
                    //((HyperLink)this.gvcashpay.HeaderRow.FindControl("hlbtntbCdataExcel22")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    ((HyperLink)this.gvcashpay.HeaderRow.FindControl("hlbtntbCdataExcel22")).NavigateUrl = "../../RDLCViewer.aspx?PrintOpt=GRIDTOEXCELNEW";






                    break;
                case "SalLACA":
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFothallo")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othallow)", "")) ? 0.00 : dt.Compute("sum(othallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFothearn")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othearn)", "")) ? 0.00 : dt.Compute("sum(othearn)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFtax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFcelladj")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mcadj)", "")) ? 0.00 : dt.Compute("sum(mcadj)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFcellbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mcell)", "")) ? 0.00 : dt.Compute("sum(mcell)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFadvance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##00); ");

                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFloan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(loanins)", "")) ? 0.00 : dt.Compute("sum(loanins)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvLACA.FooterRow.FindControl("lgvFarrear")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSalSum.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSalSum.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSalSum.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSalSum.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSalSum.FooterRow.FindControl("lgvFtded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSalSum.FooterRow.FindControl("lgvFnetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "CashBonus":
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFbSalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFgssalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFBonusAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "BonusSummary":
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFNoEmpsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nofemployee)", "")) ? 0.00 : dt.Compute("sum(nofemployee)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFbSalbsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFgssalbsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFBonusAmtbsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFBankamtsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonusSum.FooterRow.FindControl("lgvFCashamtsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "Disbursement":
                    ((Label)this.gvdisbursement.FooterRow.FindControl("lgvPrincidisF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvdisbursement.FooterRow.FindControl("lgvCashoutfeeF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashoutfee)", "")) ? 0.00 : dt.Compute("sum(cashoutfee)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvdisbursement.FooterRow.FindControl("lgvNagadF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashoutfeenag)", "")) ? 0.00 : dt.Compute("sum(cashoutfeenag)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvdisbursement.FooterRow.FindControl("lgdisburseF")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disbuamt)", "")) ? 0.00 : dt.Compute("sum(disbuamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;



                case "NagadSalary":
                    ((Label)this.gvnsalary.FooterRow.FindControl("lgvFTNetmtnagad")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "TopSheetPID":
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFmanpowerpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nofemployee)", "")) ? 0.00 : dt.Compute("sum(nofemployee)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFgrossalpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFarrearpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFfooding")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(foodal)", "")) ? 0.00 : dt.Compute("sum(foodal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFOT")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ottotal)", "")) ? 0.00 : dt.Compute("sum(ottotal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFarrearotherpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toother)", "")) ? 0.00 : dt.Compute("sum(toother)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFDeductionpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFNagadpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(nagpercommi)", "")) ? 0.00 : dt.Compute("sum(nagpercommi)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvtopsheetpid.FooterRow.FindControl("lgvFNetPaymentpid")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayable)", "")) ? 0.00 : dt.Compute("sum(netpayable)", ""))).ToString("#,##0;(#,##0); ");

                    break;




            }







        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalSummary":
                    this.PrintSalSummary();
                    break;

                case "CashSalary":
                    this.PrintCashSalary();
                    break;
                case "SalLACA":
                    this.PrintSalLACA();
                    break;

                case "RPTENVELOP":
                    this.PrintEnvelop();
                    break;

                case "CashBonus":
                    this.PrintCashBonous();
                    break;
                case "BonusSummary":
                    this.PrintBonSummary();
                    break;

                case "BonPaySlip":
                    this.PrintBonPaySlip();
                    break;
                case "Disbursement":
                    this.PrintDisbursement();
                    break;

                case "NagadSalary":
                    this.PrintNagadSalary();
                    break;

                case "TopSheetPID":
                    this.PrintTopSheetSalaryPid();
                    break;




            }



        }

        private void PrintNagadSalary()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            // string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalaryStatementNagad>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalStatNagad", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comname));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Statement(Cash/Nagad)"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "Salary Statement (Cash)  for the Month of : " + date));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Salary Statement (Cash/Nagad) ";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }





            //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptCashPay02();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtccaret.Text = "Salary Statement (Cash)  for the Month of : " + date; ;
            //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            //txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);

            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintDisbursement()
        {

            //Sanmar

            this.PrintDis();



        }

        private void PrintDis()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy");
            string subtitle = "SUMMARY OF DISBURSEMENT ";//+ todate1 ;
            string userinf = ASTUtility.Concat(comname, username, session, printdate);
            DataTable dt1 = (DataTable)Session["tblSalSum"];
            DataTable dt2 = (DataTable)Session["tblDisburseSumm"];

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.Disbursement>();
            var list2 = dt2.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.DisbursementSummary>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalDisbursement", list, list2, null);



            Rpt1.SetParameters(new ReportParameter("comnam", comname));
            Rpt1.SetParameters(new ReportParameter("subtitle", subtitle));
            Rpt1.SetParameters(new ReportParameter("userinf", userinf));
            //Rpt1.SetParameters(new ReportParameter("bodytxt", bodytxt));
            //Rpt1.SetParameters(new ReportParameter("bodynxt", bodynxt));
            //Rpt1.SetParameters(new ReportParameter("gret", gret));

            //Rpt1.SetParameters(new ReportParameter("compname", comnam));
            //Rpt1.SetParameters(new ReportParameter("sub", "Subject : Confirmation Of Services."));
            //Rpt1.SetParameters(new ReportParameter("hr", "HR Executive"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvSalSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSalSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }


        private void PrintTopSheetSalaryPid()
        {

            DataTable dt = (DataTable)Session["topsheetpid"];
            DataTable dt2 = (DataTable)Session["sumnagadcashpid"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string todate1 = Convert.ToDateTime(date).AddDays(24).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(date).AddDays(24).ToString("dd \\'MMM");
            string frmdate = Convert.ToDateTime(todate1).AddMonths(-1).AddDays(1).ToString("dd \\'MMM");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSummaryPEB", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Month of " + Convert.ToDateTime(date).ToString("MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Summary of Salary & Allowance for PID Worker"));
            Rpt1.SetParameters(new ReportParameter("txtNagad", Convert.ToDouble(dt2.Rows[0]["netpayable"].ToString()).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtCash", Convert.ToDouble(dt2.Rows[1]["netpayable"].ToString()).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtTotal", Convert.ToDouble(dt2.Rows[2]["netpayable"].ToString()).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSalSummary()
        {



            //Sanmar

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4101":
                case "4305":
                    this.PrintSalRupayanGroup();
                    break;

                case "4301":
                    this.PrintSalSumSanmar();
                    break;

                case "3330":

                    this.PrintSalSumBridge();
                    break;           
                case "3333":
                    this.PrintSalSumAlli();
                    break;

                case "3368":
                    PrintSalSumFinlay();
                    break;

                default:
                    this.PrintSalSum();
                    break;
            }
        }

        private void PrintSalSumFinlay()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSummaryFinlay", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Summary Sheet of Salary for the " + "Month of: " + date)); //+"(" + frmdate + "- " + todate + ")"
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalRupayanGroup()
        {

            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            switch (company)
            {

                case "50":
                    this.PrintSalSumHolding();
                    break;


                case "23":
                    this.PrintSalSumRatul();
                    break;



                default:
                    this.PrintSalSumRestRG();
                    break;

            }
        }
        private void PrintSalSumHolding()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = this.GetComeCode();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSumRG();

            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Summary Salary Sheet for the Month of : " + date; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalSumRestRG()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = this.GetComeCode();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSumRup();

            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Summary Salary Sheet for the Month of : " + date; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalSumRatul()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = this.GetComeCode();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSumRup();

            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Summary Salary Sheet for the Month of : " + date; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        //private void PrintSalSumRupayan() 
        //{

        //    DataTable dt = (DataTable)Session["tblSalSum"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comname = hst["comnam"].ToString();
        //    string comcod = this.GetComeCode();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
        //    date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
        //    double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
        //    ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSumRup();

        //    TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
        //    TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
        //    txtccaret.Text = "Summary Salary Sheet for the Month of : " + date; ;
        //    TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
        //    txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
        //    TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    rpcp.SetDataSource(dt);
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rpcp.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rpcp;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}

        private void PrintSalSumSanmar()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSummary2();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Summary Salary Sheet for the Month of : " + date; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalSum()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSummary2", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("Comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Summary Salary Sheet"));
            Rpt1.SetParameters(new ReportParameter("date", "Month of " + date));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        //private void PrintSalSumPEB()
        //{
        //    DataTable dt = (DataTable)Session["tblSalSum"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = this.GetComeCode();
        //    string comname = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
        //    //  date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
        //    string todate1 = Convert.ToDateTime(date).AddDays(24).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(date).AddDays(24).ToString("dd \\'MMM");
        //    string frmdate = Convert.ToDateTime(todate1).AddMonths(-1).AddDays(1).ToString("dd \\'MMM");


        //    ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalSummaryPEB();
        //    TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
        //    TextObject txtdate = rpcp.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        //    txtdate.Text = "Month of " + Convert.ToDateTime(date).ToString("MMMM, yyyy") + "(" + frmdate + "- " + todate + ")";
        //    TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

        //    rpcp.SetDataSource(dt);
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rpcp.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rpcp;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
        //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}
        private void PrintSalSumBridge()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            //  date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            string todate1 = Convert.ToDateTime(date).AddDays(24).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(date).AddDays(24).ToString("dd \\'MMM");
            string frmdate = Convert.ToDateTime(todate1).AddMonths(-1).AddDays(1).ToString("dd \\'MMM");


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSummaryBridge", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("Comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Salary Status -Current Month VS Last Month"));
            Rpt1.SetParameters(new ReportParameter("date", "Month of " + Convert.ToDateTime(date).ToString("MMMM, yyyy") + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }



        private void PrintSalSumAlli()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            //  date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            string todate1 = Convert.ToDateTime(date).AddDays(24).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(date).AddDays(24).ToString("dd \\'MMM");
            string frmdate = Convert.ToDateTime(todate1).AddMonths(-1).AddDays(1).ToString("dd \\'MMM");


            ReportDocument rptstk = new ReportDocument();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.SalSummary2>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalSummaryAlliance", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("Comname", comname));
            Rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Salary Status -Current Month VS Last Month"));
            Rpt1.SetParameters(new ReportParameter("date", "Month of " + Convert.ToDateTime(date).ToString("MMMM, yyyy") + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintCashSalary()
        {
            //Sanmar
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3315":
                case "4101":
                    this.PrintCashSalaryAssure();
                    break;

                //case "3101":
                case "3355":
                    this.PrintCashSalaryGreenWood();
                    break;

                case "3354":
                    this.PrintCashSalaryEdison();
                    break;

                //case "3101":
                case "3368":
                    this.PrintCashSalaryFinlay();
                    break;

                default:
                    this.PrintCashSalarygen();
                    break;

            }
        }

        private void PrintCashSalaryFinlay()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string year = this.txtfMonth.Text.Substring(0, 4).ToString();
            string month = ASITUtility03.GetFullMonthName(this.txtfMonth.Text.Substring(4));
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptCashPay02Finlay", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Cash Forwarding Report"));
            Rpt1.SetParameters(new ReportParameter("txtMonth", month));
            Rpt1.SetParameters(new ReportParameter("txtYear", year));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintCashSalaryEdison()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptCashPay02Edison", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Statement (Cash)"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Salary Statement (Cash)  for the Month of : " + date));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCashSalaryGreenWood()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet2.RptCashPay02>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptCashPay02GreenWood", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Statement (Cash)"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Salary Statement (Cash)  for the Month of : " + date));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintCashSalaryAssure()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptCashPay02Assure();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Salary Statement (Cash)  for the Month of : " + date; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);

            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintCashSalarygen()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptCashPay02();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Salary Statement (Cash)  for the Month of : " + date; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rpcp.SetDataSource(dt);

            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintSalLACA()
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4315":
                case "4101":
                case "3101":
                    this.PPrintSalLACAGAssure();
                    break;



                default:
                    this.PrintSalLACAGen();
                    break;




            }


        }

        private void PPrintSalLACAGAssure()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalLACAAssure", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Loan/Advance/Cell Phone/Arrear Data Sheet"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Month : " + date));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalLACAGen()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            date = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalLACA", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Loan/Advance/Cell Phone/Arrear Data Sheet"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Month : " + date));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEnvelop()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString(); ;
            string month = this.txtfMonth.Text.Trim();
            string Company = this.ddlCompany.SelectedValue.Substring(0, 2);
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "RPTEMPENVELOP", month, Company, Department, section, "", "", "", "", "");


            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptEnvelop();

            TextObject CompAdd = rpcp.ReportDefinition.ReportObjects["CompAddress"] as TextObject;
            CompAdd.Text = comadd;
            rpcp.SetDataSource(ds3.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintCashBonous()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            string dat2 = ASTUtility.Right(frmdate, 4);

            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>();
            LocalReport Rpt1 = new LocalReport();
            switch (comcod)
            {
                case "3354":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpBonusEdison", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;
                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpBonus", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    break;
            }           

            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("bonusType", "FESTIVAL BONUS OF " + bonusType + " (Cash)"));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("txtDuration", "Duration In Month"));
            Rpt1.SetParameters(new ReportParameter("tkInWord", "In Word: " + ASTUtility.Trans(tbonamt, 2)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintBonSummary()
        {

            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "4305":
                case "4101":
                    this.PrintBonusSumRupGroup();
                    break;

                case "3330":
                    //case "3101":
                    this.PrintComBonSummaryBridge();
                    break;

                case "3333":
                    //case "3101":
                    this.PrintComBonSummaryAlli();
                    break;


                case "3315":
                case "3101":
                    this.PrintComBonSummaryAssure();
                    break;

                //case "3101":
                case "3347":
                    this.PrintComBonSummaryPEB();
                    break;
                //default:
                //    this.PrintComBonSummary();
                //    break;

                default:
                    this.PrintComBonSummaryBridge();
                    break;
            }






        }


        private void PrintComBonSummaryAlli()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSummaryAlli", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS SUMMARY OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tbonamt, 2)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintComBonSummaryBridge()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSummaryBridge", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("comlogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS SUMMARY OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tbonamt, 2)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintComBonSummaryPEB()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comnam = this.ddlCompany.SelectedItem.Text.Trim();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));


            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSummaryPEB", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compname", comnam));
            rpt.SetParameters(new ReportParameter("comadd", comadd));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));
            rpt.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS SUMMARY OF " + bonusType));
            rpt.SetParameters(new ReportParameter("frmdate", frmdate));
            rpt.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tbonamt, 2)));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintComBonSummaryAssure()
        {

            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.BonusSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.rptBonousSummaryAssure", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS SUMMARY OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tbonamt, 2)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintBonusSumRupGroup()
        {
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            switch (company)
            {

                case "50":
                    this.PrintBonusSumHolding();
                    break;

                case "23":
                    this.PrintBonusSumRatul();
                    break;

                default:
                    this.PrintBonusSumRestRG();
                    break;

            }


        }

        private void PrintBonusSumHolding()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.rptBonousSummaryRG();
            TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            txtBonType.Text = "FESTIVAL BONUS SUMMARY OF " + bonusType;

            TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = frmdate;
            TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempBonus.SetDataSource(dt);


            Session["Report1"] = rptempBonus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBonusSumRatul()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.rptBonousSummaryRatulPro();
            TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            txtBonType.Text = "FESTIVAL BONUS SUMMARY OF " + bonusType;

            TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = frmdate;
            TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempBonus.SetDataSource(dt);


            Session["Report1"] = rptempBonus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBonusSumRestRG()
        {
            DataTable dt = (DataTable)Session["tblSalSum"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = this.GetStdDate("01." + ASTUtility.Right(this.txtfMonth.Text, 2) + "." + this.txtfMonth.Text.Substring(0, 4));
            string frmdate = Convert.ToDateTime(date).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.rptBonousSummaryRestRG();
            TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            txtBonType.Text = "FESTIVAL BONUS SUMMARY OF " + bonusType;

            TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = frmdate;
            TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempBonus.SetDataSource(dt);


            Session["Report1"] = rptempBonus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBonPaySlip()
        {
            string comcod = this.GetComeCode();
            string month = this.txtfMonth.Text.Trim();
            string Company = this.ddlCompany.SelectedValue.Substring(0, 2);
            string Department = (this.ddlDepartName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_PAYSLIP", "RPTBONPAYSLIP", month, Company, Department, section, bonusType, "", "", "", "");

            if (ds3 == null)
            {
                return;

            }

            ReportDocument rptBonPaySlip = new RealERPRPT.R_81_Hrm.R_89_Pay.RptBonPaySlip();
            rptBonPaySlip.SetDataSource(ds3.Tables[0]);
            Session["Report1"] = rptBonPaySlip;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        protected void gvcashpay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcashpay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvLACA_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLACA.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBonus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvBonusSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBonusSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvdisbursement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvdisbursement.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void gvtopsalary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvperticular = (Label)e.Row.FindControl("lgvperticular");
                Label lgvmanpower = (Label)e.Row.FindControl("lgvmanpower");
                Label lgvgrossal = (Label)e.Row.FindControl("lgvgrossal");
                Label lgvarrear = (Label)e.Row.FindControl("lgvarrear");
                Label lgvDeduction = (Label)e.Row.FindControl("lgvDeduction");
                Label lgvNagad = (Label)e.Row.FindControl("lgvNagad");
                Label lgvNetPayment = (Label)e.Row.FindControl("lgvNetPayment");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();

                if (code == "")
                {
                    return;
                }


                if (code == "940100101EEE")
                {

                    lgvperticular.Font.Bold = true;
                    lgvmanpower.Font.Bold = true;
                    lgvgrossal.Font.Bold = true;
                    lgvarrear.Font.Bold = true;
                    lgvDeduction.Font.Bold = true;
                    lgvNagad.Font.Bold = true;
                    lgvNetPayment.Font.Bold = true;
                    lgvperticular.Font.Size = 12;
                    lgvmanpower.Font.Size = 12;
                    lgvgrossal.Font.Size = 12;
                    lgvarrear.Font.Size = 12;
                    lgvDeduction.Font.Size = 12;
                    lgvNagad.Font.Size = 12;
                    lgvNetPayment.Font.Size = 12;

                    e.Row.BackColor = System.Drawing.Color.Orange;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon; ";

                    lgvNagad.Style.Add("text-align", "left");
                    lgvNetPayment.Style.Add("text-align", "right");

                }


                else if (code == "940100101AAA" || code == "940100101DDD")
                {
                    lgvperticular.Font.Bold = true;
                    lgvmanpower.Font.Bold = true;
                    lgvgrossal.Font.Bold = true;
                    lgvarrear.Font.Bold = true;
                    lgvDeduction.Font.Bold = true;
                    lgvNagad.Font.Bold = true;
                    lgvNetPayment.Font.Bold = true;

                    lgvperticular.Font.Size = 12;
                    lgvmanpower.Font.Size = 12;
                    lgvgrossal.Font.Size = 12;
                    lgvarrear.Font.Size = 12;
                    lgvDeduction.Font.Size = 12;
                    lgvNagad.Font.Size = 12;
                    lgvNetPayment.Font.Size = 12;

                    e.Row.BackColor = System.Drawing.Color.Aqua;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                    lgvNagad.Style.Add("text-align", "left");
                    lgvNetPayment.Style.Add("text-align", "right");

                }



                else if (code == "940100101FFF")
                {
                    lgvperticular.Font.Bold = true;
                    lgvmanpower.Font.Bold = true;
                    lgvgrossal.Font.Bold = true;
                    lgvarrear.Font.Bold = true;
                    lgvDeduction.Font.Bold = true;
                    lgvNagad.Font.Bold = true;
                    lgvNetPayment.Font.Bold = true;

                    lgvperticular.Font.Size = 12;
                    lgvmanpower.Font.Size = 12;
                    lgvgrossal.Font.Size = 12;
                    lgvarrear.Font.Size = 12;
                    lgvDeduction.Font.Size = 12;
                    lgvNagad.Font.Size = 12;
                    lgvNetPayment.Font.Size = 12;



                    e.Row.BackColor = System.Drawing.Color.HotPink;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                    lgvNagad.Style.Add("text-align", "left");
                    lgvNetPayment.Style.Add("text-align", "right");

                }

                else if (code == "940100101RRR")
                {
                    lgvperticular.Font.Bold = true;
                    lgvmanpower.Font.Bold = true;
                    lgvgrossal.Font.Bold = true;
                    lgvarrear.Font.Bold = true;
                    lgvDeduction.Font.Bold = true;
                    lgvNagad.Font.Bold = true;
                    lgvNetPayment.Font.Bold = true;
                    lgvperticular.Font.Size = 12;
                    lgvmanpower.Font.Size = 12;
                    lgvgrossal.Font.Size = 12;
                    lgvarrear.Font.Size = 12;
                    lgvDeduction.Font.Size = 12;
                    lgvNagad.Font.Size = 12;
                    lgvNetPayment.Font.Size = 12;

                    e.Row.BackColor = System.Drawing.Color.Orange;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                    lgvNagad.Style.Add("text-align", "left");
                    lgvNetPayment.Style.Add("text-align", "right");

                }

                else if (code == "940100101YYY")
                {
                    lgvperticular.Font.Bold = true;
                    lgvmanpower.Font.Bold = true;
                    lgvgrossal.Font.Bold = true;
                    lgvarrear.Font.Bold = true;
                    lgvDeduction.Font.Bold = true;
                    lgvNagad.Font.Bold = true;
                    lgvNetPayment.Font.Bold = true;
                    lgvperticular.Font.Size = 12;
                    lgvmanpower.Font.Size = 12;
                    lgvgrossal.Font.Size = 12;
                    lgvarrear.Font.Size = 12;
                    lgvDeduction.Font.Size = 12;
                    lgvNagad.Font.Size = 12;
                    lgvNetPayment.Font.Size = 12;

                    e.Row.BackColor = System.Drawing.Color.Orange;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                    lgvNagad.Style.Add("text-align", "left");
                    lgvNetPayment.Style.Add("text-align", "right");


                }


                else if (code == "940100101CCC")
                {
                    lgvperticular.Font.Bold = true;
                    lgvmanpower.Font.Bold = true;
                    lgvgrossal.Font.Bold = true;
                    lgvarrear.Font.Bold = true;
                    lgvDeduction.Font.Bold = true;
                    lgvNagad.Font.Bold = true;
                    lgvNetPayment.Font.Bold = true;

                    lgvperticular.Font.Size = 12;
                    lgvmanpower.Font.Size = 12;
                    lgvgrossal.Font.Size = 12;
                    lgvarrear.Font.Size = 12;
                    lgvDeduction.Font.Size = 12;
                    lgvNagad.Font.Size = 12;
                    lgvNetPayment.Font.Size = 12;



                    e.Row.BackColor = System.Drawing.Color.AliceBlue;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                    lgvNagad.Style.Add("text-align", "left");
                    lgvNetPayment.Style.Add("text-align", "right");

                }
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }


        protected void gvtopsheetfactory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvfgrp = (Label)e.Row.FindControl("lgvfgrp");
                Label lgvfacperticular = (Label)e.Row.FindControl("lgvfacperticular");
                Label lgvfaccashamt = (Label)e.Row.FindControl("lgvfaccashamt");
                Label lgvfbanksalary = (Label)e.Row.FindControl("lgvfbanksalary");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string descrip = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "descrip")).ToString();

                // string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();

                if (grp == "")
                {
                    return;
                }


                //if (  grp == "C")
                //{

                //    //lgvfgrp.Font.Bold = true;
                //    lgvfacperticular.Font.Bold = true;
                //    lgvfaccashamt.Font.Bold = true;
                //    lgvfbanksalary.Font.Bold = true;

                //    lgvfacperticular.Font.Size = 12;
                //    lgvfaccashamt.Font.Size = 12;
                //    lgvfbanksalary.Font.Size = 12;

                //    //e.Row.BackColor = System.Drawing.Color.Aqua;
                //    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                //    lgvfacperticular.Attributes["style"] = "font-weight:bold; color:black; ";
                //    lgvfaccashamt.Attributes["style"] = "font-weight:bold; color:black;";
                //    lgvfbanksalary.Attributes["style"] = "font-weight:bold; color:black;";




                //    //lgvNagad.Style.Add("text-align", "left");
                //    //lgvNetPayment.Style.Add("text-align", "right");

                //}

                if (grp == "B" || grp == "C" || grp == "E" || grp == "F" || grp == "G" || grp == "H" || grp == "I" || grp == "J" || grp == "K" || grp == "L" || grp == "O" || grp == "P" || grp == "Q" || grp == "R")
                {

                    //lgvfgrp.Font.Bold = true;
                    lgvfacperticular.Font.Bold = true;
                    lgvfaccashamt.Font.Bold = true;
                    lgvfbanksalary.Font.Bold = true;

                    lgvfacperticular.Font.Size = 10;
                    lgvfaccashamt.Font.Size = 10;
                    lgvfbanksalary.Font.Size = 10;

                    //e.Row.BackColor = System.Drawing.Color.Aqua;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvfacperticular.Attributes["style"] = "font-weight:bold; color:black; ";
                    lgvfaccashamt.Attributes["style"] = "font-weight:bold; color:black;";
                    lgvfbanksalary.Attributes["style"] = "font-weight:bold; color:black;";




                    //lgvNagad.Style.Add("text-align", "left");
                    //lgvNetPayment.Style.Add("text-align", "right");

                }



                else if (grp == "A" || grp == "D" || grp == "M" || grp == "N")
                {

                    //lgvfgrp.Font.Bold = true;
                    lgvfacperticular.Font.Bold = true;
                    lgvfaccashamt.Font.Bold = true;
                    lgvfbanksalary.Font.Bold = true;

                    lgvfacperticular.Font.Size = 12;
                    lgvfaccashamt.Font.Size = 12;
                    lgvfbanksalary.Font.Size = 12;

                    //e.Row.BackColor = System.Drawing.Color.Aqua;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvfacperticular.Attributes["style"] = "font-weight:bold; color:black;background:CornflowerBlue";
                    lgvfaccashamt.Attributes["style"] = "font-weight:bold; color:black;background:CornflowerBlue";
                    lgvfbanksalary.Attributes["style"] = "font-weight:bold; color:black;background:CornflowerBlue";




                    //lgvNagad.Style.Add("text-align", "left");
                    //lgvNetPayment.Style.Add("text-align", "right");

                }

                else if (grp == "S")
                {

                    //lgvfgrp.Font.Bold = true;
                    lgvfacperticular.Font.Bold = true;
                    lgvfaccashamt.Font.Bold = true;
                    lgvfbanksalary.Font.Bold = true;

                    lgvfacperticular.Font.Size = 12;
                    lgvfaccashamt.Font.Size = 12;
                    lgvfbanksalary.Font.Size = 12;

                    //e.Row.BackColor = System.Drawing.Color.Aqua;
                    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                    lgvfacperticular.Attributes["style"] = "font-weight:bold; color:black; background:Aquamarine";
                    lgvfaccashamt.Attributes["style"] = "font-weight:bold; color:black;background:Aquamarine";
                    lgvfbanksalary.Attributes["style"] = "font-weight:bold; color:black;background:Aquamarine";




                    //lgvNagad.Style.Add("text-align", "left");
                    //lgvNetPayment.Style.Add("text-align", "right");

                }

                //else if (code == "940100101AAA" || code == "940100101DDD")
                //{
                //    lgvperticular.Font.Bold = true;
                //    lgvmanpower.Font.Bold = true;
                //    lgvgrossal.Font.Bold = true;
                //    lgvarrear.Font.Bold = true;
                //    lgvDeduction.Font.Bold = true;
                //    lgvNagad.Font.Bold = true;
                //    lgvNetPayment.Font.Bold = true;

                //    lgvperticular.Font.Size = 12;
                //    lgvmanpower.Font.Size = 12;
                //    lgvgrossal.Font.Size = 12;
                //    lgvarrear.Font.Size = 12;
                //    lgvDeduction.Font.Size = 12;
                //    lgvNagad.Font.Size = 12;
                //    lgvNetPayment.Font.Size = 12;

                //    e.Row.BackColor = System.Drawing.Color.Aqua;
                //    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                //    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                //    lgvNagad.Style.Add("text-align", "left");
                //    lgvNetPayment.Style.Add("text-align", "right");

                //}



                //else if (code == "940100101FFF")
                //{
                //    lgvperticular.Font.Bold = true;
                //    lgvmanpower.Font.Bold = true;
                //    lgvgrossal.Font.Bold = true;
                //    lgvarrear.Font.Bold = true;
                //    lgvDeduction.Font.Bold = true;
                //    lgvNagad.Font.Bold = true;
                //    lgvNetPayment.Font.Bold = true;

                //    lgvperticular.Font.Size = 12;
                //    lgvmanpower.Font.Size = 12;
                //    lgvgrossal.Font.Size = 12;
                //    lgvarrear.Font.Size = 12;
                //    lgvDeduction.Font.Size = 12;
                //    lgvNagad.Font.Size = 12;
                //    lgvNetPayment.Font.Size = 12;



                //    e.Row.BackColor = System.Drawing.Color.HotPink;
                //    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                //    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                //    lgvNagad.Style.Add("text-align", "left");
                //    lgvNetPayment.Style.Add("text-align", "right");

                //}

                //else if (code == "940100101RRR")
                //{
                //    lgvperticular.Font.Bold = true;
                //    lgvmanpower.Font.Bold = true;
                //    lgvgrossal.Font.Bold = true;
                //    lgvarrear.Font.Bold = true;
                //    lgvDeduction.Font.Bold = true;
                //    lgvNagad.Font.Bold = true;
                //    lgvNetPayment.Font.Bold = true;
                //    lgvperticular.Font.Size = 12;
                //    lgvmanpower.Font.Size = 12;
                //    lgvgrossal.Font.Size = 12;
                //    lgvarrear.Font.Size = 12;
                //    lgvDeduction.Font.Size = 12;
                //    lgvNagad.Font.Size = 12;
                //    lgvNetPayment.Font.Size = 12;

                //    e.Row.BackColor = System.Drawing.Color.Orange;
                //    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                //    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                //    lgvNagad.Style.Add("text-align", "left");
                //    lgvNetPayment.Style.Add("text-align", "right");

                //}

                //else if (code == "940100101YYY")
                //{
                //    lgvperticular.Font.Bold = true;
                //    lgvmanpower.Font.Bold = true;
                //    lgvgrossal.Font.Bold = true;
                //    lgvarrear.Font.Bold = true;
                //    lgvDeduction.Font.Bold = true;
                //    lgvNagad.Font.Bold = true;
                //    lgvNetPayment.Font.Bold = true;
                //    lgvperticular.Font.Size = 12;
                //    lgvmanpower.Font.Size = 12;
                //    lgvgrossal.Font.Size = 12;
                //    lgvarrear.Font.Size = 12;
                //    lgvDeduction.Font.Size = 12;
                //    lgvNagad.Font.Size = 12;
                //    lgvNetPayment.Font.Size = 12;

                //    e.Row.BackColor = System.Drawing.Color.Orange;
                //    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                //    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                //    lgvNagad.Style.Add("text-align", "left");
                //    lgvNetPayment.Style.Add("text-align", "right");


                //}


                //else if (code == "940100101CCC")
                //{
                //    lgvperticular.Font.Bold = true;
                //    lgvmanpower.Font.Bold = true;
                //    lgvgrossal.Font.Bold = true;
                //    lgvarrear.Font.Bold = true;
                //    lgvDeduction.Font.Bold = true;
                //    lgvNagad.Font.Bold = true;
                //    lgvNetPayment.Font.Bold = true;

                //    lgvperticular.Font.Size = 12;
                //    lgvmanpower.Font.Size = 12;
                //    lgvgrossal.Font.Size = 12;
                //    lgvarrear.Font.Size = 12;
                //    lgvDeduction.Font.Size = 12;
                //    lgvNagad.Font.Size = 12;
                //    lgvNetPayment.Font.Size = 12;



                //    e.Row.BackColor = System.Drawing.Color.AliceBlue;
                //    //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='green'");
                //    lgvperticular.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvmanpower.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvgrossal.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvarrear.Attributes["style"] = "font-weight:bold; color:maroon;";
                //    lgvDeduction.Attributes["style"] = "font-weight:bold; color:maroon;";

                //    lgvNagad.Style.Add("text-align", "left");
                //    lgvNetPayment.Style.Add("text-align", "right");

                //}
            }

        }


        protected void chekPrint_Click(object sender, EventArgs e)
        {
            string msg;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{

            //    msg = "You have no permission";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            //    return;
            //}

            try
            {
                this.cardNo.Value = "";
                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                int index = this.gvcashpay.PageSize * this.gvcashpay.PageIndex + RowIndex;
                string card = ((Label)this.gvcashpay.Rows[index].FindControl("lgIdCardcash")).Text.ToString();
                string empname = ((Label)this.gvcashpay.Rows[index].FindControl("lgvndesigcash")).Text.ToString();
                this.ddlBank.Items.Clear();
                this.ddlcheque.Items.Clear();
                this.cardNo.Value = card;
                GetBankName();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
            }


            catch (Exception ex)
            {

                msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);



            }
        }

        private void GetBankName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            string bankcode = "1902%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETBANKNAME", txtSProject, bankcode, "", "", "", "", "", "", "");
            this.ddlBank.DataTextField = "actdesc";
            this.ddlBank.DataValueField = "bankcode";
            this.ddlBank.DataSource = ds1.Tables[0];
            this.ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("--Please Select--", ""));
            Session["tblbank"] = ds1.Tables[0];



        }

        protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string bankcode = this.ddlBank.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "TOPCHEQUE", bankcode, "", "", "", "", "", "", "", "");
            this.ddlcheque.DataTextField = "chequeno";
            this.ddlcheque.DataValueField = "chequeno";
            this.ddlcheque.DataSource = ds1.Tables[0];

            this.ddlcheque.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }

        protected void btnUpdateChekNumber_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string chekno = this.ddlcheque.SelectedValue.ToString();
            string bankcode = this.ddlBank.SelectedValue.ToString();
            string cardno = this.cardNo.Value;
            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTMONTHLYCHECKGIVEN", bankcode, chekno, "", "", "", "", "", "", "");

        }

        protected void btnPrintCheck_Click(object sender, EventArgs e)
        {
            string msg;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{

            //    msg = "You have no permission";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            //    return;
            //}

            try
            {

                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                int index = this.gvcashpay.PageSize * this.gvcashpay.PageIndex + RowIndex;

                string yearmon = this.txtfMonth.Text.ToString();
                LinkButton linkBtn =((LinkButton)this.gvcashpay.Rows[index].FindControl("btnPrintCheck"));
                //double  amt = Convert.ToDouble(((Label)this.gvcashpay.Rows[index].FindControl("lgvnetamtcash")).Text);

                double amt = 0;
                string card = ((Label)this.gvcashpay.Rows[index].FindControl("lgIdCardcash")).Text.ToString();
                string empname = ((Label)this.gvcashpay.Rows[index].FindControl("lblempname")).Text.ToString();
                string bankcode = ((DropDownList)this.gvcashpay.Rows[index].FindControl("ddlBankList")).SelectedValue.ToString();
                string ckdate = ((TextBox)this.gvcashpay.Rows[index].FindControl("txtckDate")).Text.ToString();

                int i;
                for(i= 0; i < gvcashpay.Rows.Count; i++ ){

                    if(((CheckBox)this.gvcashpay.Rows[i].FindControl("chkMerge")).Checked == true)
                    {
                        double  mergeamt = Convert.ToDouble(((Label)this.gvcashpay.Rows[i].FindControl("lgvnetamtcash")).Text);

                        amt += mergeamt;

                        ((CheckBox)this.gvcashpay.Rows[i].FindControl("chkMerge")).Checked = false;



                    }
                }


              


                if (bankcode.Length != 12)
                {
                    msg = "Please Select Bank Name, Emp Name: " + empname;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }
                else
                {
                    //string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
                    //string currentptah = "AccPrint.aspx?Type=CashSalaryCheque&empname=" + empname + "&amt=" + amt.ToString() + "&ckdate=" + ckdate + "&bankcode=" + bankcode + "&yearmon=" + yearmon;
                    //string totalpath = hostname + currentptah;
                    ////string totalpath = "~/F_17_Acc/AccPrint.aspx?Type=CashSalaryCheque&empname=" + empname + "&amt=" + amt.ToString() + "&ckdate=" + ckdate + "&bankcode=" + bankcode + "&yearmon=" + yearmon;
                    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";


                    //Response.Write("<script>window.open ('~/F_17_Acc/AccPrint.aspx?Type=CashSalaryCheque&empname=" + empname + "&amt=" + amt + "&ckdate=" + ckdate + "','_blank');</script>");
                    Response.Redirect("~/F_17_Acc/AccPrint.aspx?Type=CashSalaryCheque&empname=" + empname + "&amt=" + amt.ToString() + "&ckdate=" + ckdate + "&bankcode=" + bankcode + "&yearmon=" + yearmon);
                }
            }


            catch (Exception ex)
            {
                msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }
        }



        //rakib
        protected void chkMergeAll_CheckedChanged(object sender, EventArgs e)
        {

            int i, index;
            if (((CheckBox)this.gvcashpay.HeaderRow.FindControl("chkMergeAll")).Checked)
            {

                for (i = 0; i < this.gvcashpay.Rows.Count; i++)
                {

                    ((CheckBox)this.gvcashpay.Rows[i].FindControl("chkMerge")).Checked = true;
                    index = (this.gvcashpay.PageSize) * (this.gvcashpay.PageIndex) + i;
         


                }


            }

            else
            {
                for (i = 0; i < this.gvcashpay.Rows.Count; i++)
                {

                    ((CheckBox)this.gvcashpay.Rows[i].FindControl("chkMerge")).Checked = false;
                    index = (this.gvcashpay.PageSize) * (this.gvcashpay.PageIndex) + i;



                }

            }



        }
        private void GetEmployeeName()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();
                int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                string compcode = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
                string deptcode = (this.ddlDepartName.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlDepartName.SelectedValue.ToString().Substring(0, 9) + "%";
                string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

                string txtSProject = "%";
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETEMPLOYEENAME", compcode, deptcode, Section, txtSProject, "", "", "", "", "");
                Session["tblempdsg"] = ds3.Tables[0];
                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = ds3.Tables[0];
                this.ddlEmployee.DataBind();

            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }


        }

        protected void OpenChckPrintModal(object sender, EventArgs e)
        {

            double amt = 0;


            int i;
            for (i = 0; i < gvcashpay.Rows.Count; i++)
            {

                if (((CheckBox)this.gvcashpay.Rows[i].FindControl("chkMerge")).Checked == true)
                {
                    double mergeamt = Convert.ToDouble(((Label)this.gvcashpay.Rows[i].FindControl("lgvnetamtcash")).Text);

                    amt += mergeamt;

                    ((CheckBox)this.gvcashpay.Rows[i].FindControl("chkMerge")).Checked = false;



                }
            }
            this.ttlamt.Text = amt.ToString();
            this.getBank();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openChckPrint();", true);
        }



        private void getBank()
        {
            DataTable tbnk = (DataTable)Session["tblbank"];
            ddlBankModal.DataTextField = "actdesc";
            ddlBankModal.DataValueField = "bankcode";
            ddlBankModal.DataSource = tbnk;
            ddlBankModal.DataBind();
            ddlBankModal.Items.Insert(0, new ListItem("--Please Select--", ""));
        }

        protected void lnkchckPrintModal_Click(object sender, EventArgs e)
        {
            string msg="";
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{

            //    msg = "You have no permission";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            //    return;
            //}

            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();


                string yearmon = this.txtfMonth.Text.ToString();
                string amt = this.ttlamt.Text.ToString();
                
                string empname = this.ddlEmployee.SelectedItem.Text.ToString().Remove(0,7);
                string bankcode = ddlBankModal.SelectedValue.ToString();
                string ckdate = this.txtchckdate.Text.ToString();


                if (bankcode.Length != 12)
                {
                    msg = "Please Select Bank Name, Emp Name: " + empname;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }
                else
                {

                    Response.Redirect("~/F_17_Acc/AccPrint.aspx?Type=CashSalaryCheque&empname=" + empname + "&amt=" + amt + "&ckdate=" + ckdate + "&bankcode=" + bankcode + "&yearmon=" + yearmon);
                }
            }


            catch (Exception ex)
            {
                msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}