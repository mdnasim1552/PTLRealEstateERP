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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RpHRtPayroll : System.Web.UI.Page
    {
        static string prevPage = String.Empty;
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        int curd;
        int frdate;
        int mon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                
                this.CommonButton();
                this.GetCompany();
                this.visibilityBracnh();
                //this.GetEmpName();
                this.SelectType();
            }
        }
        private void visibilityBracnh()
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3315":
                case "3347":
                case "3353":
                case "3358":
                    this.divBracnhLsit.Visible = false;
                    this.ddlBranch.Items.Clear();
                    break;

                default:
                    this.divBracnhLsit.Visible = true;
                    break;

            }
        }
        public void CommonButton()
        {

            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            ((CheckBox)this.Master.FindControl("chkBoxN")).Visible = false;
            ((CheckBox)this.Master.FindControl("CheckBox1")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            ((LinkButton)this.Master.FindControl("btnClose")).Visible = true;

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("btnClose")).Click += new EventHandler(btnClose_Click);
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "Salary" || type == "SalResign") 
            {
                ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lnkTotal_Click);
                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lnkFiUpdate_Click);
            }
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetCompCode();
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            switch (type)
            {
                case "Salary":
                case "SalResign":
                    this.MultiView1.ActiveViewIndex = 0;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                    ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
                    this.gndDiv.Visible = true;
                    this.SeachDivForGrid.Visible = true;
                    this.pnlsalops.Visible = true;
                    this.CompanySalary();
                    switch (comcod)
                    {
                        case "4301"://Sanmer
                        case "3332":
                        case "3330":
                        case "3338":
                        case "3336"://Suvastu
                            //case "4305"://Rupayan
                            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            this.txtfromdate.Text = "26" + this.txtfromdate.Text.Trim().Substring(2);
                            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;
                        //case "3101":
                        case "3339":   // Tropical Homes                   
                            string date1 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                            string date2 = "20" + date1.Trim().Substring(2);
                            if (Convert.ToDateTime(date1) >= Convert.ToDateTime(date2))
                            {
                                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            }
                            else
                            {
                                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            }
                            break;
                        default:
                            //string comcod = this.GetComCode();
                            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;
                    }
                    break;

                case "Bonus":

                    this.MultiView1.ActiveViewIndex = 1;
                    this.CompanyBonus();
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.lblfrmdate.Text = "Date:";
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    break;
                case "Payslip":
                    this.MultiView1.ActiveViewIndex = 2;

                    comcod = this.GetCompCode();
                    switch (comcod)
                    {
                        case "4301"://Sanmer
                                    //case "4305"://Rupayan
                            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            this.txtfromdate.Text = "26" + this.txtfromdate.Text.Trim().Substring(2);
                            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;

                        case "3365"://bti
                            this.divEMplist.Visible = true;
                            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-2).ToString("dd-MMM-yyyy");
                            this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;
                        default:
                            this.divEMplist.Visible = true;
                            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                            this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                            break;
                    }
                    break;

                case "Signature":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                case "CashPay":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                case "OvertimeSalary":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    break;
                case "SpecialBonus":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.CompanyBonus();
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                    this.lblfrmdate.Text = "Date:";
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.GetDesignation();
                    break;
            }
        }
        private void GetDesignation()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "DESIGNAME", "", "", "", "", "", "", "", "", "");
            Session["tbldesig"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlfrmDesig.DataTextField = "designation";
            this.ddlfrmDesig.DataValueField = "desigcod";
            this.ddlfrmDesig.DataSource = ds1.Tables[0];
            this.ddlfrmDesig.DataBind();
            this.GetDessignationTo();
        }
        private void GetDessignationTo()
        {
            DataTable dt = (DataTable)Session["tbldesig"];
            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();
        }
        private void CompanySalary()
        {
            this.rbtSalSheet.Visible = false;
            // this.rbtSalSheet.Visible = true; 
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "4101": //foster
                //    this.rbtSalSheet.SelectedIndex = 1;
                //    break;
                //case"3101":
                case "4301": //sanmar
                    this.rbtSalSheet.SelectedIndex = 2;
                    break;

                //case"3101":
                case "4305"://Rupayan
                    this.rbtSalSheet.SelectedIndex = 4;
                    break;
                case "4201"://Multiplan
                    this.rbtSalSheet.SelectedIndex = 3;
                    break;
                case "4302"://Asian TV
                    this.rbtSalSheet.SelectedIndex = 5;
                    break;
                case "4306"://GLG
                    this.rbtSalSheet.SelectedIndex = 6;
                    break;

                //case "3101":
                case "3315"://Assure
                    this.rbtSalSheet.SelectedIndex = 7;
                    break;
                //case "3101":
                case "3325"://Leisu            
                    this.rbtSalSheet.SelectedIndex = 8;
                    break;
                case "3330"://Bridge
                    this.rbtSalSheet.SelectedIndex = 9;
                    break;
                case "3332"://InnStar
                    this.rbtSalSheet.SelectedIndex = 10;
                    break;
                case "3333"://Alliance
                    this.rbtSalSheet.SelectedIndex = 11;
                    break;
                //case"3101":
                case "3338"://Acme
                    this.rbtSalSheet.SelectedIndex = 12;
                    this.rbtnlistsaltypeAddItem();
                    break;
                case "3336"://Suvastu
                    this.rbtSalSheet.SelectedIndex = 13;
                    break;

                case "3339"://Tropical
                    this.rbtSalSheet.SelectedIndex = 14;
                    break;

                //case"3101":
                case "3344"://Terranova
                    this.rbtSalSheet.SelectedIndex = 15;
                    break;
                //case "3101":
                case "3347"://PEB
                    this.rbtSalSheet.SelectedIndex = 16;
                    this.rbtnPayTypeDiv.Visible = true;
                    this.rbtnPayType.Visible = true;
                    break;

                case "3355"://Green Wood
                    this.rbtSalSheet.SelectedIndex = 17;
                    this.rbtnlistsaltypeAddItem();
                    break;

                case "3353"://Manama
                    this.rbtSalSheet.SelectedIndex = 18;
                    this.rbtnPayType.Visible = false;
                    this.rbtnPayTypeDiv.Visible = false;

                    this.chkBangla.Visible = true;
                    this.lblBanglaDiv.Visible = true;
                    break;

                //case "3101":
                case "3354"://Edison Real Estate
                    this.rbtSalSheet.SelectedIndex = 19;
                    break;

                case "3358"://Entrust Real Estate
                    this.rbtSalSheet.SelectedIndex = 20;
                    break;

                //case "3101"://
                case "3364"://JBS
                    this.rbtSalSheet.SelectedIndex = 22;
                    break;

                case "3365"://BTI
               
                    this.rbtSalSheet.SelectedIndex = 21;
                    break;


                case "3101":
                case "3366"://Lanco
                            //case "3101":
                    this.rbtSalSheet.SelectedIndex = 23;
                    break;

                case "3367"://Epic
                    this.rbtSalSheet.SelectedIndex = 25;
                    break;

                case "3368"://Finlay
                    this.rbtSalSheet.SelectedIndex = 24;
                    break;

                default:
                    this.rbtSalSheet.SelectedIndex = 14;
                    break;
            }
        }
        private void rbtnlistsaltypeAddItem()
        {
            string comcod = this.GetCompCode();
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
        private void CompanyBonus()
        {
            string comcod = this.GetCompCode();
            this.rbtlBonSheet.Visible = false;

            switch (comcod)
            {
                case "4102": //foster
                    this.rbtlBonSheet.SelectedIndex = 1;
                    break;
                case "4301": //sanmar
                    this.rbtlBonSheet.SelectedIndex = 2;
                    break;

                case "4201"://Multiplan
                    this.rbtlBonSheet.SelectedIndex = 3;
                    break;

                case "4305"://Rupayan
                    this.rbtlBonSheet.SelectedIndex = 4;
                    break;
                case "3315"://Assure
                    this.rbtlBonSheet.SelectedIndex = 5;
                    break;

                case "3330"://Bridge
                            //case "3101"://Multiplan
                    this.rbtlBonSheet.SelectedIndex = 6;
                    break;

                case "3333":// Allaince
                    this.rbtlBonSheet.SelectedIndex = 7;
                    break;

                //case "3101":
                case "3338":// ACME
                case "3344":// Terranova
                    this.rbtlBonSheet.SelectedIndex = 8;
                    break;


                case "3339"://Tropical
                    this.rbtlBonSheet.SelectedIndex = 10;
                    this.txtafterdays.Text = "90";
                    break;


                case "3347"://PEB STEEL

                    this.rbtlBonSheet.SelectedIndex = 11;
                    this.rbtnPayType.Visible = true;
                    this.rbtnPayTypeDiv.Visible = true;

                    break;

                case "3355"://GreenWood

                    this.rbtlBonSheet.SelectedIndex = 12;
                    this.rbtnPayType.Visible = true;
                    this.rbtnPayTypeDiv.Visible = true;

                    this.rbtnMantype.Visible = true;
                    break;

                case "3365"://BTI
                    this.rbtlBonSheet.SelectedIndex = 13;
                    this.txtafterdays.Text = "90";
                    break;
               
                case "3354"://Edison
                    this.rbtlBonSheet.SelectedIndex = 14;
                    break;


                case "3368":
                    this.rbtlBonSheet.SelectedIndex = 15;
                    break;
                case "3101":
                case "3366":
                    this.rbtlBonSheet.SelectedIndex = 16;
                    break;

                default://Bridge  , Terranova        
                    this.rbtlBonSheet.SelectedIndex = 8;
                    break;
            }
        }
        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "94%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");


            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        private void GetBranch()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtSProject = "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETBRANCH", Company, txtSProject, "", "", "", "", "", "", "");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETBRANCH_NEW", Company, userid, "", "", "", "", "", "", "");

            this.ddlBranch.DataTextField = "actdesc";
            this.ddlBranch.DataValueField = "actcode";
            this.ddlBranch.DataSource = ds1.Tables[0];
            this.ddlBranch.DataBind();
            this.ddlBranch_SelectedIndexChanged(null, null);
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3315":
                case "3347":
                case "3353":
                case "3358":
                    this.divBracnhLsit.Visible = false;
                    this.ddlBranch.Items.Clear();
                    this.GetProjectName();
                    break;

                default:
                    this.GetBranch();
                    break;

            }

           
        }
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string txtSProject = "%%";
            //  DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", branch, txtSProject, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETDPTLIST_NEW", branch, userid, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            // this.SectionName();
        }
        private void SectionName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            //  string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlProjectName.SelectedValue.ToString();
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%%";
            // DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTION_LIST", projectcode, userid, "", "", "", "", "", "", "");

            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            // this.GetEmpName();
            ddlSection_SelectedIndexChanged(null, null);
        }
        private void SectionNameAll()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            // string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%%";
            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", Company, txtSSec, txtSSec, "", "", "", "", "", "", "");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTION_LIST", Company, userid, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            // this.GetEmpName();
            ddlSection_SelectedIndexChanged(null, null);
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empcode = "";
            this.GetEmpName(empcode);
        }


        private void GetEmpName(string empcode)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string ProjectCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? branch : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%");
            string section = this.ddlSection.SelectedValue.ToString() == "000000000000" ? ProjectCode : this.ddlSection.SelectedValue.ToString();

            section = empcode.Length > 0 ? "%%" : section;

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_EMPLIST", section, "%%", userid, "", "", "", "", "", "");
            if (ds5 == null)
                return;

            //  DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPAYSLIPEMPNAMEALL", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            empcode = "";
        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            SectionNameAll();
            string empcode = "all";
            this.GetEmpName(empcode);
        }
        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
            lnkbtnShow_Click(null, null);
        }


        private void GetComASecSelected()
        {
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString().Trim();
            if (empid == "000000000000" || empid == "")
                return;
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.eMpname.InnerText = this.ddlEmpNameAllInfo.SelectedItem.ToString();
                this.eMpDPt.InnerText = dr[0]["dptdesc"].ToString();


                //this.ddlCompany.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                //this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                //// this.ddlProjectName_SelectedIndexChanged(null,null);
                //this.ddlSection.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
            }
            else
            {
                this.eMpname.InnerText = "";
                this.eMpDPt.InnerText = "";

            }
        }
        // this.ddlProjectName_SelectedIndexChanged(null, null);
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.SelectIndex();
        }
        private void SelectIndex()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Salary":
                case "SalResign":
                    this.ShowSal();
                    break;
                case "Bonus":
                    this.ShowBonus();
                    break;

                case "Payslip":
                    this.ShowPaySlip();
                    break;
                case "Signature":
                    this.ShowSignature();
                    break;
                case "CashPay":
                    this.EmpCashPay();
                    break;

                case "OvertimeSalary":
                    this.ShowEmpOvertimeSalary();
                    break;

                case "SpecialBonus":
                    this.ShowSpecialBonus();
                    break;
            }
        }
        private void ShowSal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string saltype = "";
            string type = this.Request.QueryString["Type"].ToString().Trim();
            if (type == "SalResign")
                saltype = "R";
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? CompanyName : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? branch : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%");
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? projectcode : this.ddlSection.SelectedValue.ToString());

            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));
            DataSet ds3;

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SALLOCK", monthid, branch, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                string Msg = "Something Wrong";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
            }
            this.lblComSalLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();

            //13 Suvastu, 
            string CallType = (this.rbtSalSheet.SelectedIndex == 7) ? "PAYROLL_DETAIL06" : (this.rbtSalSheet.SelectedIndex == 8) ? "PAYROLL_DETAIL07" : (this.rbtSalSheet.SelectedIndex == 6) ? "PAYROLL_DETAIL4" : (this.rbtSalSheet.SelectedIndex == 5) ? "PAYROLL_DETAIL3"
                : (this.rbtSalSheet.SelectedIndex == 4) ? "PAYROLL_DETAIL2" : (this.rbtSalSheet.SelectedIndex == 2) ? "PAYROLL_DETAIL1" : (this.rbtSalSheet.SelectedIndex == 9) ? "PAYROLL_DETAIL08" : (this.rbtSalSheet.SelectedIndex == 10) ? "PAYROLL_DETAIL09" : (this.rbtSalSheet.SelectedIndex == 11) ? "PAYROLL_DETAIL10" : (this.rbtSalSheet.SelectedIndex == 12) ? "PAYROLL_DETAIL12" : (this.rbtSalSheet.SelectedIndex == 13) ? "PAYROLL_DETAIL14" :
                    (this.rbtSalSheet.SelectedIndex == 14) ? "PAYROLL_DETAIL16" : (this.rbtSalSheet.SelectedIndex == 15) ? "PAYROLL_DETAIL18" : (this.rbtSalSheet.SelectedIndex == 16) ? "PAYROLL_DETAIL19" : (this.rbtSalSheet.SelectedIndex == 17) ? "PAYROLL_DETAIL20" : (this.rbtSalSheet.SelectedIndex == 18) ? "PAYROLL_DETAIL21" : (this.rbtSalSheet.SelectedIndex == 19) ? "PAYROLL_DETAIL22" : (this.rbtSalSheet.SelectedIndex == 20) ? "PAYROLL_DETAIL23" : (this.rbtSalSheet.SelectedIndex == 21) ? "PAYROLL_DETAIL24" :
                    (this.rbtSalSheet.SelectedIndex == 22) ? "PAYROLL_DETAIL25" : (this.rbtSalSheet.SelectedIndex == 23) ? "PAYROLL_DETAIL26" : (this.rbtSalSheet.SelectedIndex == 24) ? "PAYROLL_DETAIL27" : (this.rbtSalSheet.SelectedIndex == 25) ? "PAYROLL_DETAIL28" : "PAYROLL_DETAIL";
            string ProName = ((this.rbtSalSheet.SelectedIndex == 8) || (this.rbtSalSheet.SelectedIndex == 9) || (this.rbtSalSheet.SelectedIndex == 7) || (this.rbtSalSheet.SelectedIndex == 6) || (this.rbtSalSheet.SelectedIndex == 10) || (this.rbtSalSheet.SelectedIndex == 12) || (this.rbtSalSheet.SelectedIndex == 13) || (this.rbtSalSheet.SelectedIndex == 14) ||
                (this.rbtSalSheet.SelectedIndex == 15) || (this.rbtSalSheet.SelectedIndex == 16) || (this.rbtSalSheet.SelectedIndex == 17) || (this.rbtSalSheet.SelectedIndex == 18) || (this.rbtSalSheet.SelectedIndex == 19) || (this.rbtSalSheet.SelectedIndex == 20) || (this.rbtSalSheet.SelectedIndex == 21) || (this.rbtSalSheet.SelectedIndex == 22) || (this.rbtSalSheet.SelectedIndex == 23) || (this.rbtSalSheet.SelectedIndex == 24) || (this.rbtSalSheet.SelectedIndex == 25)) ? "dbo_hrm.SP_REPORT_PAYROLL03" : (this.rbtSalSheet.SelectedIndex == 5) ? "dbo_hrm.SP_REPORT_PAYROLL01" : ((this.rbtSalSheet.SelectedIndex == 11) || (this.rbtSalSheet.SelectedIndex == 7)) ? "dbo_hrm.SP_REPORT_PAYROLL03" : "dbo_hrm.SP_REPORT_PAYROLL";
            string mantype = "";
            switch (comcod)
            {
                case "3338":
                    mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : "86%";
                    break;
                //case "3101":
                case "3355":
                    mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : (this.rbtnlistsaltype.SelectedIndex == 2) ? "86003%" : "86%";
                    break;
                default:
                    mantype = "86%";
                    break;
            }
            string paytype = "";

            switch (comcod)
            {
                //case "3101":
                case "3347":
                    paytype = (this.rbtnPayType.SelectedIndex == 0) ? "Cash" : (this.rbtnPayType.SelectedIndex == 1) ? "Bank" : (this.rbtnPayType.SelectedIndex == 2) ? "Cheque" : "";
                    break;

                default:
                    paytype = "";
                    break;
            }
            //string exclumgt = "";
            //switch (comcod)
            //{
            //    case "3355":
            //        exclumgt = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" && chkExcluMgt.Checked) ? "exclumgt" : "";
            //        break;

            //    default:
            //        exclumgt = "";
            //        break;  
            //}
            string Calltype1 = (comcod == "3347") ? "RPT_BACSALARY" : (comcod == "3365" ? "RPT_BACSALARYGENBTI" : "RPT_BACSALARYGEN");
            // todo for bangla print
            string language = this.chkBangla.Checked ? "Bangla" : "";
            if (this.lblComSalLock.Text == "True")
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", Calltype1, monthid, projectcode, section, CompanyName, mantype, paytype, saltype, branch, "");
                //if (ds3.Tables[0].Rows.Count == 0)
                //    ds3 = HRData.GetTransInfo(comcod, ProName, CallType, frmdate, todate, projectcode, section, CompanyName, "", "", "", "");
            }
            else
            {
                ds3 = HRData.GetTransInfo(comcod, ProName, CallType, frmdate, todate, projectcode, section, CompanyName, mantype, paytype, language, saltype, branch, userid);
            }
            if (ds3 == null)
            {
                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = dt;
            if (ds3.Tables.Count > 1)
            {
                ViewState["tweekedn"] = ds3.Tables[1];
            }
            this.LoadGrid();
        }
        public int Datediffday1(DateTime dtto, DateTime dtfrm)
        {
            int year, mon, day;
            year = dtto.Year - dtfrm.Year;
            mon = dtto.Month - dtfrm.Month;
            day = dtto.Day - dtfrm.Day;
            if (day < 0)
            {
                day = day + 30;
                mon = mon - 1;
                if (mon < 0)
                {
                    mon = mon + 12;
                    year = year - 1;
                }
            }
            if (mon < 0)
            {

                mon = mon + 12;
                year = year - 1;
            }
            //today = year * 365 + mon * 30 + day;
            return mon;
        }
        private string Companygross()
        {
            string ComGross = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "4305":
                case "4101"://Rupayan
                    ComGross = "Rupayan";
                    break;
                default:
                    ComGross = ""; ;
                    break;
            }
            return ComGross;
        }
        private string companyBonusPayType()
        {
            string bonpaytype = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3355":
                case "3347":
                    bonpaytype = (this.rbtnPayType.SelectedIndex == 0) ? "Cash" : (this.rbtnPayType.SelectedIndex == 1) ? "Bank" : (this.rbtnPayType.SelectedIndex == 2) ? "" : "";
                    break;

                default:
                    bonpaytype = "";
                    break;
            }
            return bonpaytype;
        }
        private void ShowSpecialBonus()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            // string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM").ToString();
            DataSet ds3;
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "BONLOCK", monthid, CompanyName, "", "", "", "", "", "", "");
            //this.lblComBonLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            //string Calltype = (this.rbtlBonSheet.SelectedIndex == 0) ? "EMPBONUS" : (this.rbtlBonSheet.SelectedIndex == 1) ? "EMPBONUS1" : (this.rbtlBonSheet.SelectedIndex == 5) ? "EMPBONUS2" : (this.rbtlBonSheet.SelectedIndex == 2) ? "EMPBONUSSAN" : (this.rbtlBonSheet.SelectedIndex == 6) ? "EMPBONUSBRIDGE" : (this.rbtlBonSheet.SelectedIndex == 7) ? "EMPBONUSALLIANCE"
            //    : (this.rbtlBonSheet.SelectedIndex == 8) ? "EMPBONUSGEN" : (this.rbtlBonSheet.SelectedIndex == 10) ? "EMPBONUSTROPICAL" : (this.rbtlBonSheet.SelectedIndex == 11) ? "EMPBONUSPEBSTEEL" : (this.rbtlBonSheet.SelectedIndex == 12) ? "EMPBONUSGREENWOOD" : "EMPBONUSGEN";
            string afterdays = Convert.ToDouble("0" + this.txtafterdays.Text.Trim()).ToString();
            string comgross = this.Companygross();
            //string bonpaytype = this.companyBonusPayType();
            string DesigFrom = this.ddlfrmDesig.SelectedValue.ToString();
            string DesigTo = this.ddlToDesig.SelectedValue.ToString();
            //string mantype = "";
            ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "EMPSPECIALBONUS", date, projectcode, section, afterdays, CompanyName, comgross, DesigFrom, DesigTo, "");
            if (ds3 == null)
            {
                this.gvsbonus.DataSource = null;
                this.gvsbonus.DataBind();
                return;
            }
            DataTable dt = this.HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = dt;
            this.LoadGrid();
        }
        private void ShowBonus()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            //string compBranch = this.ddlBranch.SelectedValue.ToString();
            //string projectcode = this.ddlProjectName.SelectedValue.ToString();
            //string section = this.ddlSection.SelectedValue.ToString();

            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string compBranch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? CompanyName  : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? compBranch : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%");
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? projectcode : this.ddlSection.SelectedValue.ToString());


            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM").ToString();
            DataSet ds3;
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "BONLOCK", monthid, compBranch, "", "", "", "", "", "", "");
            this.lblComBonLock.Text = (ds1.Tables[0].Rows.Count == 0) ? "False" : Convert.ToBoolean(ds1.Tables[0].Rows[0]["lock"]).ToString();
            string Calltype = (this.rbtlBonSheet.SelectedIndex == 0) ? "EMPBONUS" : (this.rbtlBonSheet.SelectedIndex == 1) ? "EMPBONUS1" : (this.rbtlBonSheet.SelectedIndex == 5) ? "EMPBONUS2" : (this.rbtlBonSheet.SelectedIndex == 2) ? "EMPBONUSSAN" : (this.rbtlBonSheet.SelectedIndex == 6) ? "EMPBONUSBRIDGE" : (this.rbtlBonSheet.SelectedIndex == 7) ? "EMPBONUSALLIANCE"
                : (this.rbtlBonSheet.SelectedIndex == 8) ? "EMPBONUSGEN" : (this.rbtlBonSheet.SelectedIndex == 10) ? "EMPBONUSTROPICAL" : (this.rbtlBonSheet.SelectedIndex == 11) ? "EMPBONUSPEBSTEEL" : (this.rbtlBonSheet.SelectedIndex == 12) ? "EMPBONUSGREENWOOD" : (this.rbtlBonSheet.SelectedIndex == 13) ? "EMPBONUSBTI" : (this.rbtlBonSheet.SelectedIndex == 14) ? "EMPBONUSEDISON" : (this.rbtlBonSheet.SelectedIndex == 15) ? "EMPBONUSFINLAY" : (this.rbtlBonSheet.SelectedIndex == 16) ? "EMPBONUSLANCO": "EMPBONUSGEN";
            string afterdays = Convert.ToDouble("0" + this.txtafterdays.Text.Trim()).ToString();
            string comgross = this.Companygross();
            string bonpaytype = this.companyBonusPayType();
            string mantype = "";
            //mon = this.Datediffday1(Convert.ToDateTime(curdate), Convert.ToDateTime(dt1));
            switch (comcod)
            {
                case "3355":
                    mantype = (this.rbtnMantype.SelectedIndex == 0) ? "86001%" : (this.rbtnMantype.SelectedIndex == 1) ? "86002%" : (this.rbtnMantype.SelectedIndex == 2) ? "86003%" : "86%";
                    break;
                default:
                    mantype = "";
                    break;
            }
            //mon > 3 ||
            if ( (this.lblComBonLock.Text == "True"))
            {



                string CompanyNamelk = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
                 compBranch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? CompanyNamelk : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";               
                string projectcodelk = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? compBranch : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%");
                string sectionlk = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? projectcode : this.ddlSection.SelectedValue.ToString());


                //string CompanyNamelk = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
                //string projectcodelk = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? CompanyNamelk : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
                //string sectionlk = (this.ddlSection.SelectedValue.ToString() == "000000000000" ? projectcodelk : this.ddlSection.SelectedValue.ToString()) + "%";
                

                string Calltype1 = (comcod == "3347") ? "BONSALARYPEB" : "BONSALARY";
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", Calltype1, monthid, projectcodelk, sectionlk, CompanyNamelk, "", "", "", "", "");
                //if (ds3.Tables[0].Rows.Count == 0)
                //    ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", Calltype, date, projectcode, section, afterdays, CompanyName, comgross, "", "", "");
            }
            else
            {
                ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", Calltype, date, projectcode, section, afterdays, CompanyName, comgross, bonpaytype, mantype, compBranch);
            }
            if (ds3 == null)
            {
                this.gvBonus.DataSource = null;
                this.gvBonus.DataBind();
                return;
            }
            DataTable dt = this.HiddenSameData(ds3.Tables[0]);

            //DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();
        }
        private void ShowPaySlip()
        {
            Session.Remove("tblpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string result = "";
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            // string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string empid = ddlEmpNameAllInfo.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlEmpNameAllInfo.SelectedValue.ToString() + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_PAYSLIP", "RPTPAYSLIP", frmdate, todate, projectcode, section, CompanyName, empid, userid, "", "");
            //if (ds3 == null)
            //{
            //    this.gvBonus.DataSource = null;
            //    this.gvBonus.DataBind();
            //    return;
            //}

            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.TakaInWord();
        }
        private void ShowSignature()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SIGNATURESHEET", frmdate, todate, projectcode, section, "", "", "", "", "");
            if (ds3 == null)
                return;
            Session["tblpay"] = ds3.Tables[0];
            ds3.Dispose();
        }
        private void EmpCashPay()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            string CallType = this.CashCallType();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", CallType, frmdate, todate, projectcode, section, CompanyName, "", "", "", "");
            if (ds3 == null)
            {
                this.gvpayroll.DataSource = null;
                this.gvpayroll.DataBind();
                return;
            }
            //DataView dv = ds3.Tables[0].DefaultView;
            //dv.RowFilter = "othded>0";
            // DataTable dt = HiddenSameData(dv.ToTable());
            DataTable dt = HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = dt;
            this.LoadGrid();
        }
        private void ShowEmpOvertimeSalary()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string projectcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 8)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "SP_REPORT_OVRTIMESALARY", "RPTOVRTIMESALARY", frmdate, todate, CompanyName, projectcode, section, "", "", "", "");
            if (ds3 == null)
            {
                this.gvOvertime.DataSource = null;
                this.gvOvertime.DataBind();
                return;
            }
            DataTable dt = ds3.Tables[0];
            Session["tblpay"] = dt;
            this.LoadGrid();
        }
        private string CashCallType()
        {
            string compcod = this.GetCompCode();
            string CallType = "";
            switch (compcod)
            {
                case "4101":
                    break;
                case "4301":
                    CallType = "RPTCASHSALARY";
                    break;
            }
            return CallType;
        }
        private void TakaInWord()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblpay"];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double netpay = Convert.ToDouble(dt.Rows[i]["netpay"]);
                    dt.Rows[i]["aminword"] = ASTUtility.Trans(netpay, 2);
                }
                Session["tblpay"] = dt;
            }
            catch (Exception ex)
            {

            }
        }


        private DataTable ExtractSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string refno = dt1.Rows[0]["refno"].ToString();
            string section = dt1.Rows[0]["section"].ToString();

            string refdesc = dt1.Rows[0]["refdesc"].ToString();
            string sectionname = dt1.Rows[0]["sectionname"].ToString();


            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == section)
                {
                    refno = dt1.Rows[j]["refno"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["refdesc"] = refdesc;
                    dt1.Rows[j]["sectionname"] = sectionname;
                }
                else
                {

                    if (dt1.Rows[j]["section"].ToString() != section)
                    {
                        dt1.Rows[j]["sectionname"] = dt1.Rows[j]["sectionname"];
                    }
                    refno = dt1.Rows[j]["refno"].ToString();
                    section = dt1.Rows[j]["section"].ToString();

                    refdesc = dt1.Rows[j]["refdesc"].ToString();
                    sectionname = dt1.Rows[j]["sectionname"].ToString();
                }
            }

            return dt1;



        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Salary":
                case "Bonus":
                case "SpecialBonus":
                case "Payslip":
                case "Signature":
                case "CashPay":
                    string refno = dt1.Rows[0]["refno"].ToString();
                    string section = dt1.Rows[0]["section"].ToString();
                    switch (comcod)
                    {
                        case "3339":
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["refno"].ToString() == refno)
                                {
                                    dt1.Rows[j]["refdesc"] = "";
                                }
                                refno = dt1.Rows[j]["refno"].ToString();
                            }
                            break;
                        default:
                            for (int j = 1; j < dt1.Rows.Count; j++)
                            {
                                if (dt1.Rows[j]["refno"].ToString() == refno && dt1.Rows[j]["section"].ToString() == section)
                                {
                                    refno = dt1.Rows[j]["refno"].ToString();
                                    section = dt1.Rows[j]["section"].ToString();
                                    dt1.Rows[j]["refdesc"] = "";
                                    dt1.Rows[j]["sectionname"] = "";
                                }
                                else
                                {
                                    if (dt1.Rows[j]["refno"].ToString() == refno)
                                    {
                                        dt1.Rows[j]["refdesc"] = "";
                                    }
                                    if (dt1.Rows[j]["section"].ToString() == section)
                                    {
                                        dt1.Rows[j]["sectionname"] = "";
                                    }
                                    refno = dt1.Rows[j]["refno"].ToString();
                                    section = dt1.Rows[j]["section"].ToString();
                                }
                            }
                            break;
                    }
                    break;
                case "OvertimeSalary":
                    string company = dt1.Rows[0]["company"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                            dt1.Rows[j]["companyname"] = "";
                        company = dt1.Rows[j]["company"].ToString();
                    }
                    break;
            }
            return dt1;
        }
        private void LoadGrid()
        {
            try
            {
                string comcod = this.GetCompCode();
                DataTable dt = (DataTable)Session["tblpay"];

                ////dt = (DataTable)(this.HiddenSameData(Session["tblpay"]));

                //DataTable dt2 = this.HiddenSameData(dt);



                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "Salary":
                    case "SalResign":
                        if (comcod == "3365" || comcod == "3101")
                        {
                            this.gvpayroll.Columns[23].HeaderText = "W.F Fund";
                        }
                        this.gvpayroll.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvpayroll.DataSource = dt;
                        this.gvpayroll.DataBind();
                        if (dt.Rows.Count > 0)
                        {
                            this.gvpayroll.Columns[1].Visible = (this.ddlProjectName.SelectedValue == "000000000000") ? true : false;
                            ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = (this.lblComSalLock.Text == "True") ? true : false;
                        }

                        switch (comcod)
                        {

                            case "3101":
                            case "3365":
                                this.gvpayroll.Columns[11].Visible = false;
                                this.gvpayroll.Columns[12].Visible = false;
                                this.gvpayroll.Columns[13].Visible = false;
                                this.gvpayroll.Columns[14].Visible = false;
                                this.gvpayroll.Columns[15].Visible = false;
                                this.gvpayroll.Columns[16].Visible = false;
                                this.gvpayroll.Columns[17].Visible = false;
                                this.gvpayroll.Columns[18].Visible = false;
                                this.gvpayroll.Columns[20].Visible = false;
                                this.gvpayroll.Columns[31].Visible = false;
                                this.gvpayroll.Columns[41].Visible = false;

                                break;

                            case "3347":

                                this.gvpayroll.Columns[13].Visible = false;
                                this.gvpayroll.Columns[16].Visible = false;
                                this.gvpayroll.Columns[18].Visible = true;
                                this.gvpayroll.Columns[24].Visible = false;
                                this.gvpayroll.Columns[26].Visible = false;
                                this.gvpayroll.Columns[29].Visible = false;
                                this.gvpayroll.Columns[31].Visible = false;
                                this.gvpayroll.Columns[32].Visible = false;
                                this.gvpayroll.Columns[33].Visible = false;
                                this.gvpayroll.Columns[34].Visible = false;
                                this.gvpayroll.Columns[38].Visible = false;
                                this.gvpayroll.Columns[40].Visible = true;
                                break;
                            default:
                                this.gvpayroll.Columns[12].Visible = false;
                                this.gvpayroll.Columns[13].Visible = false;
                                this.gvpayroll.Columns[16].Visible = true;
                                this.gvpayroll.Columns[18].Visible = false;
                                this.gvpayroll.Columns[22].Visible = false;
                                this.gvpayroll.Columns[23].Visible = false;
                                this.gvpayroll.Columns[25].Visible = false;
                                this.gvpayroll.Columns[28].Visible = false;
                                this.gvpayroll.Columns[30].Visible = false;
                                this.gvpayroll.Columns[31].Visible = false;
                                this.gvpayroll.Columns[32].Visible = false;

                                this.gvpayroll.Columns[34].Visible = false;
                                this.gvpayroll.Columns[37].Visible = false;
                                this.gvpayroll.Columns[40].Visible = true;

                                break;
                        }
                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? false : true;
                                ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Enabled = false;
                            }
                        }
                        this.FooterCalculation();
                        //if (mon > 1)
                        //{
                        //    this.gvpayroll.FooterRow.FindControl("lnkFiUpdate").Visible = false;
                        //}
                        //else
                        //    this.gvpayroll.FooterRow.FindControl("lnkFiUpdate").Visible = true;
                        break;
                    case "Bonus":
                        this.gvBonus.Columns[9].HeaderText = (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration(Day)" : "Joining Date";
                        if (comcod == "3365")//BTI
                        {
                            this.gvBonus.Columns[10].HeaderText = "Duration(Year)";
                        }
                        else if (comcod == "3354" || comcod == "3368")//Edison
                        {
                            this.gvBonus.Columns[10].HeaderText = "Duration(Day)";
                            this.gvBonus.Columns[13].HeaderText = "MGT Approved";
                        }




                        this.gvBonus.DataSource = dt;
                        this.gvBonus.DataBind();
                        ((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked = (this.lblComBonLock.Text == "True") ? true : false;

                        if (Request.QueryString["Entry"].ToString() == "Payroll")
                        {
                            ((LinkButton)this.gvBonus.FooterRow.FindControl("lnkUpBonus")).Visible = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? false : true;
                            ((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Enabled = false;
                            this.gvBonus.Columns[1].Visible = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? false : true;
                        }
                        this.FooterCalculation();
                        break;
                    case "CashPay":
                        this.gvcashpay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvcashpay.DataSource = dt;
                        this.gvcashpay.DataBind();
                        this.FooterCalculation();
                        break;
                    case "OvertimeSalary":
                        this.gvOvertime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvOvertime.DataSource = dt;
                        this.gvOvertime.DataBind();
                        this.FooterCalculation();
                        break;
                    case "SpecialBonus":
                        //this.gvsbonus.Columns[8].HeaderText = (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration(Day)" : "Duration(Month)";
                        this.gvsbonus.DataSource = dt;
                        this.gvsbonus.DataBind();
                        if (dt.Rows.Count == 0)
                            return;
                        Session["Report1"] = gvsbonus;
                        ((HyperLink)this.gvsbonus.HeaderRow.FindControl("hlbtntbCdataExelSP")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCELNEW";
                        break;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
                return;
            }
        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblpay"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Salary":
                case "SalResign":
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFarier")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFoth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oth)", "")) ? 0.00 : dt.Compute("sum(oth)", ""))).ToString("#,##0;(#,##00); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFtallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tallow)", "")) ? 0.00 : dt.Compute("sum(tallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFgspay")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay)", "")) ? 0.00 : dt.Compute("sum(gspay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFabsent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absded)", "")) ? 0.00 : dt.Compute("sum(absded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFGratloan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(genloan)", "")) ? 0.00 : dt.Compute("sum(genloan)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFCarlon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(carloan)", "")) ? 0.00 : dt.Compute("sum(carloan)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFMobile")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbillded)", "")) ? 0.00 : dt.Compute("sum(mbillded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFothded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFTransp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(transded)", "")) ? 0.00 : dt.Compute("sum(transded)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFFoods")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(foodal)", "")) ? 0.00 : dt.Compute("sum(foodal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFAreasOth")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othearn)", "")) ? 0.00 : dt.Compute("sum(othearn)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFtded")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFCarallo")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(haircutal)", "")) ? 0.00 : dt.Compute("sum(haircutal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFNetPaySal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gspay1)", "")) ? 0.00 : dt.Compute("sum(gspay1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFpayable")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payables)", "")) ? 0.00 : dt.Compute("sum(payables)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFEranLeav")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(elencash)", "")) ? 0.00 : dt.Compute("sum(elencash)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFnetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbankAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFbankAmt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt2)", "")) ? 0.00 : dt.Compute("sum(bankamt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvpayroll.FooterRow.FindControl("lgvFCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvpayroll;
                    string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");
                    Session["ReportName"] = "Salary_Sheet_" + frmdate;
                    ((HyperLink)this.gvpayroll.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../../RDLCViewer.aspx?PrintOpt=GRIDTOEXCELNEW";
                    break;

                case "Bonus":
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFbSalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFgssalb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFBonusAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFSpBonusAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(spbonamt)", "")) ? 0.00 : dt.Compute("sum(spbonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFtbamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tbamt)", "")) ? 0.00 : dt.Compute("sum(tbamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFbankAmtbon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFbankAmt2bon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt2)", "")) ? 0.00 : dt.Compute("sum(bankamt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBonus.FooterRow.FindControl("lgvFcashAmtbon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    Session["Report1"] = gvBonus;
                    ((HyperLink)this.gvBonus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "CashPay":
                    ((Label)this.gvcashpay.FooterRow.FindControl("lgvFToCahamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "OvertimeSalary":
                    ((Label)this.gvOvertime.FooterRow.FindControl("lgvFoallows")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Salary":
                case "SalResign":
                    this.PrintSal();
                    break;

                case "Bonus":
                    this.PrintEmpBonus();
                    break;
                case "Payslip":
                    this.PrintPaySlip();
                    break;

                case "Signature":
                    this.PrintSignature();
                    break;

                case "CashPay":
                    this.PrintCashPay();
                    break;

                case "OvertimeSalary":
                    this.PrintOvertimeSalary();
                    break;

                case "SpecialBonus":
                    this.PrintEmpSpecialBonus();
                    break;
            }
        }
        private void PrintBonusSheetAcme()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            LocalReport Rpt1 = new LocalReport();
            DataTable dt3 = (DataTable)Session["tblpay"];

            var lst = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();

            double tAmt = lst.Select(p => p.bonamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetAcme", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tAmt, 2)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void BonusSheeetAssure()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            string inword = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet01>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpBonusAssure", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("date1", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", inword));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBonusSheetGreenWood()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            //  double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            LocalReport Rpt1 = new LocalReport();
            DataTable dt3 = (DataTable)Session["tblpay"];
            var lst = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            double tAmt = lst.Select(p => p.bonamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetGreenWood", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tAmt, 2)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBonusSheetTerranova()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            //  double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            LocalReport Rpt1 = new LocalReport();
            DataTable dt3 = (DataTable)Session["tblpay"];
            var lst = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            double tAmt = lst.Select(p => p.bonamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetTerranova", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tAmt, 2)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintBonusSheetPEB()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = this.ddlCompany.SelectedItem.Text;
            string comname = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string paytype = (this.rbtnPayType.SelectedIndex == 0) ? "CASH" : (this.rbtnPayType.SelectedIndex == 1) ? "BANK" : (this.rbtnPayType.SelectedIndex == 2) ? "CHEQUE" : "ALL";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();

            string date1 = bonusType + " BONUS LIST FOR " + frmdate + " FOR " + comnam + " (" + paytype + ") ";
            string date2 = paytype + " PAYMENT FOR " + comnam;
            //  double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            LocalReport Rpt1 = new LocalReport();

            DataTable dt3 = (DataTable)Session["tblpay"];

            var lst = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheetPEB>();

            double tAmt = lst.Select(p => p.bonamt).Sum();

            //pebsteel  factory & driver
            if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9452" || this.ddlProjectName.SelectedValue.Substring(0, 4) == "9454")
            {
                if (this.rbtnPayType.SelectedIndex == 0)
                {
                    // cash 
                    //
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebFactoryCash", lst, null, null);
                }
                else if (this.rbtnPayType.SelectedIndex == 1)
                {
                    // bank 
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebFactoryBank", lst, null, null);
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebFactory", lst, null, null);
                }
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compname", comname));
                Rpt1.SetParameters(new ReportParameter("comadd", "The Alliance Buliding (4th & 5th Floor) "));
                Rpt1.SetParameters(new ReportParameter("comadd2", "63, Progoti Saroni, Baridhara, Dhaka-1212, Bangladesh"));
                Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType));
                Rpt1.SetParameters(new ReportParameter("frmdate", date1));
            }
            // project
            else if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9451")
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebProj", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("compname", comname));
                Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType + " ( " + frmdate + " )"));
                Rpt1.SetParameters(new ReportParameter("frmdate", date2));
            }
            else
            {

                if (this.rbtnPayType.SelectedIndex == 0)
                {
                    // cash 
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebCash", lst, null, null);
                }
                else if (this.rbtnPayType.SelectedIndex == 1)
                {
                    // bank 
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebBank", lst, null, null);
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetPebBank", lst, null, null);
                }

                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("compname", comname));
                Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType + " ( " + frmdate + " )"));
                Rpt1.SetParameters(new ReportParameter("frmdate", date2));
            }

            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tAmt, 2)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintBonusBTI()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = this.ddlCompany.SelectedItem.Text;
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy");

            DataTable dt3 = (DataTable)Session["tblpay"];
            var list = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            double tAmt = list.Select(p => p.bonamt).Sum();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetBTI", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", bonusType + " BONUS/ " + frmdate));
            Rpt1.SetParameters(new ReportParameter("bonusType", "BONUS " + (this.chkBonustype.Checked ? "ADHA" : "FITR") + "(" + frmdate + ")"));
            Rpt1.SetParameters(new ReportParameter("tkInword", "In Word: " + ASTUtility.Trans(tAmt, 2)));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintBonusEdison()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = this.ddlCompany.SelectedItem.Text;
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy").ToUpper();
            string dat2 = ASTUtility.Right(frmdate, 4);

            DataTable dt3 = (DataTable)Session["tblpay"];
            var list = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            double tAmt = list.Select(p => p.bonamt).Sum();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetEdison", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "FESTIVAL BONUS OF " + bonusType +" - "+ frmdate));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("tkInword", "In Word: " + ASTUtility.Trans(tAmt, 2)));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintBonusSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = this.ddlCompany.SelectedItem.Text;
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            //  double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            LocalReport Rpt1 = new LocalReport();
            DataTable dt3 = (DataTable)Session["tblpay"];
            var lst = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            double tAmt = lst.Select(p => p.bonamt).Sum();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheet", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("rptname", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(tAmt, 2)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintSal()
        {
            if (this.chkgrndt.Checked)
            {
                this.PrintGrandTotal();
            }
            else
            {
                this.PrintSalaryAll();
            }


        }

        private void PrintGrandTotal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comnam = hst["comnam"].ToString().ToUpper();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM yyyy");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "RPTSALGRAND", monthid, monthid);
            DataTable dt = ds1.Tables[0];
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryGrandT>();
            LocalReport Rpt1 = new LocalReport();

            if (comcod == "3339" || comcod == "3101")
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryGrndTotalTropical", lst, null, null);
            }
            else
            {
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryGrndT", lst, null, null);
            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Rpt1.SetParameters(new ReportParameter("todate", " Month Of " + todate));
            Rpt1.SetParameters(new ReportParameter("Inword", "In Word: " + ASTUtility.Trans(netpay, 2)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintSalaryAll()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            if (this.rbtSalSheet.SelectedIndex == 0)
            {  //NDE
               //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryDetails();
               //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
               //CompName.Text = comname;
               //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
               //txtccaret.Text = todate;

                //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
                //txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2);
                //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rpcp.SetDataSource(dt);
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rpcp.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rpcp;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

            else if (this.rbtSalSheet.SelectedIndex == 1)
            {
                //Foster
                //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryGross();
                //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
                //CompName.Text = comname;
                //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
                //txtccaret.Text = todate.ToUpper();
                //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
                //txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
                //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rpcp.SetDataSource(dt);
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rpcp.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rpcp;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }

            else if (this.rbtSalSheet.SelectedIndex == 2)
            {
                //Sanmar
                ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryDetails2();
                TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
                CompName.Text = this.ddlCompany.SelectedItem.Text.Trim(); ;
                TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
                txtccaret.Text = "Month: " + todate; ;
                TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
                txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
                TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rpcp.SetDataSource(dt);
                //string comcod = hst["comcod"].ToString();
                string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                rpcp.SetParameterValue("ComLogo", ComLogo);
                Session["Report1"] = rpcp;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }


            else if (this.rbtSalSheet.SelectedIndex == 3)
            {
                //Multiplan
                //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryDetials3();
                //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
                //txtccaret.Text = "Month: " + todate;

                //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
                //txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2); ;
                //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rpcp.SetDataSource(dt);
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rpcp.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rpcp;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }


            else if (this.rbtSalSheet.SelectedIndex == 4)
            {
                //Rupayan


                this.PrintSalRupayanGroup();


            }

            else if (this.rbtSalSheet.SelectedIndex == 5)
            {
                //Asian TV
                //double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayatax)", "")) ? 0.00 : dt.Compute("sum(netpayatax)", "")));
                //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryAsianTV();
                //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
                //CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
                //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
                //txtccaret.Text = "Month: " + todate;
                //TextObject txtl1 = rpcp.ReportDefinition.ReportObjects["txtl1"] as TextObject;
                //txtl1.Text = "Loan Bal. " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yy");
                ////TextObject txtl2 = rpcp.ReportDefinition.ReportObjects["txtl2"] as TextObject;
                ////txtl2.Text = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yy");
                //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
                //txttk.Text = "In Word: " + ASTUtility.Trans(netpayatax, 2); ;
                //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rpcp.SetDataSource(dt);
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rpcp.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rpcp;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            else if (this.rbtSalSheet.SelectedIndex == 6)
            {
                //GreenLand Group
                if (comcod == "4325")
                {
                    this.PrintSalaryLeisure();


                    return;
                }

                //double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
                //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryGLG();
                //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
                //CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
                //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
                //txtccaret.Text = "Month of " + todate;
                //TextObject txtl1 = rpcp.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
                //txtl1.Text = comadd;
                ////TextObject txtl2 = rpcp.ReportDefinition.ReportObjects["txtl2"] as TextObject;
                ////txtl2.Text = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yy");
                //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
                //txttk.Text = "In Word: " + ASTUtility.Trans(netpayatax, 2); ;
                //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                //rpcp.SetDataSource(dt);
                ////string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rpcp.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rpcp;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }

            else if (this.rbtSalSheet.SelectedIndex == 7)
            {


                this.PrintSalAssureGroup();

            }

            else if (this.rbtSalSheet.SelectedIndex == 8)
            {
                this.PrintSalaryLeisure();

            }


            else if (this.rbtSalSheet.SelectedIndex == 9)
            {
                this.PrintSalaryBridge();

            }


            else if (this.rbtSalSheet.SelectedIndex == 10)
            {
                this.PrintSalaryInnStar();

            }
            else if (this.rbtSalSheet.SelectedIndex == 11)
            {
                this.PrintSalaryAlliance();

            }

            else if (this.rbtSalSheet.SelectedIndex == 12)
            {
                this.PrintSalaryAcme();

            }


            else if (this.rbtSalSheet.SelectedIndex == 13)
            {
                if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9405")
                {
                    this.PrintSecuritySalarySuvastu();
                }
                else
                {
                    this.PrintSalarySuvastu();
                }


            }

            else if (this.rbtSalSheet.SelectedIndex == 14)
            {
                this.PrintSalaryTropical();

            }

            else if (this.rbtSalSheet.SelectedIndex == 15)
            {
                this.PrintSalaryTerranova();

            }

            else if (this.rbtSalSheet.SelectedIndex == 16)
            {
                this.PrintSalaryPEB();

            }


            else if (this.rbtSalSheet.SelectedIndex == 17)
            {
                this.PrintSalaryGreenWood();

            }

            else if (this.rbtSalSheet.SelectedIndex == 18)
            {
                if (this.chkBangla.Checked)
                {
                    this.PrintSalaryManamaBn();
                }
                else
                {
                    this.PrintSalaryManama();
                }


            }

            else if (this.rbtSalSheet.SelectedIndex == 19)
            {

                this.PrintSalaryEdisonReal();



            }
            else if (this.rbtSalSheet.SelectedIndex == 20)
            {


                this.PrintSalEntrustGroup();

            }

            else if (this.rbtSalSheet.SelectedIndex == 21)
            {


                this.PrintSalaryBTI();

            }

            else if (this.rbtSalSheet.SelectedIndex == 22)
            {
                this.PrintSalaryJBS();

            }

            else if (this.rbtSalSheet.SelectedIndex == 23)
            {
                this.PrintSalaryLanco();

            }

            else if (this.rbtSalSheet.SelectedIndex == 24)
            {
                this.PrintSalaryFinlay();

            }
            else if (this.rbtSalSheet.SelectedIndex == 25)
            {
                this.PrintSalaryEpic();

            }
            else
                this.PrintSalaryBridge();
        }
        private void PrintSalaryEpic()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            DataTable dtweek = (DataTable)ViewState["tweekedn"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM/ yyyy");

            // for Get Weekend Common for Company Policy not emp offday
            double availday = Convert.ToDouble((Convert.IsDBNull(dtweek.Compute("sum(availday)", "")) ? 0.00 : dtweek.Compute("sum(availday)", "")));
            double wekday = Convert.ToDouble((Convert.IsDBNull(dtweek.Compute("sum(weekend)", "")) ? 0.00 : dtweek.Compute("sum(weekend)", "")));
            double govday = Convert.ToDouble((Convert.IsDBNull(dtweek.Compute("sum(holiday)", "")) ? 0.00 : dtweek.Compute("sum(holiday)", "")));

            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));


            // Bank/Cash Amt 

            double bankamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", "")));
            double cashamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", "")));



            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryEpic", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Statement for the " + "Month of " + todate1)); //+"(" + frmdate + "- " + todate + ")"
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Words: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("availday", availday.ToString()));
            Rpt1.SetParameters(new ReportParameter("wekday", wekday.ToString()));
            Rpt1.SetParameters(new ReportParameter("govday", govday.ToString()));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));




            // Bank/Cash Amt 
            Rpt1.SetParameters(new ReportParameter("bankamt", bankamt.ToString()));
            Rpt1.SetParameters(new ReportParameter("cashamt", cashamt.ToString()));
            Rpt1.SetParameters(new ReportParameter("netpay", netpay.ToString()));

            Rpt1.SetParameters(new ReportParameter("Bankinword", ASTUtility.Trans(bankamt, 2)));
            Rpt1.SetParameters(new ReportParameter("Casinword", ASTUtility.Trans(cashamt, 2)));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintSalaryFinlay()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            DataTable dtweek = (DataTable)ViewState["tweekedn"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM/ yyyy");

            // for Get Weekend Common for Company Policy not emp offday
            double availday = Convert.ToDouble((Convert.IsDBNull(dtweek.Compute("sum(availday)", "")) ? 0.00 : dtweek.Compute("sum(availday)", "")));
            double wekday = Convert.ToDouble((Convert.IsDBNull(dtweek.Compute("sum(weekend)", "")) ? 0.00 : dtweek.Compute("sum(weekend)", "")));
            double govday = Convert.ToDouble((Convert.IsDBNull(dtweek.Compute("sum(holiday)", "")) ? 0.00 : dtweek.Compute("sum(holiday)", "")));

            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));


            // for Footer Summary Details 

            // Earning 
            double grsssal = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 : dt.Compute("sum(gssal)", "")));
            double basic = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 : dt.Compute("sum(bsal)", "")));
            double hrent = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", "")));
            double mallow = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 : dt.Compute("sum(mallow)", "")));
            double dallow = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 : dt.Compute("sum(cven)", "")));
            double transallow = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pickup)", "")) ? 0.00 : dt.Compute("sum(pickup)", "")));
            double tiffin = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(foodal)", "")) ? 0.00 : dt.Compute("sum(foodal)", "")));
            double otamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", "")));
            double arrear = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(arsal)", "")) ? 0.00 : dt.Compute("sum(arsal)", "")));
            double entert = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(entaint)", "")) ? 0.00 : dt.Compute("sum(entaint)", "")));
            double ttalerning = grsssal + tiffin + arrear + otamt;

            // Deduction 
            double trnsded = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(transded)", "")) ? 0.00 : dt.Compute("sum(transded)", "")));
            double absded = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absded)", "")) ? 0.00 : dt.Compute("sum(absded)", "")));

            double advance = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(adv)", "")) ? 0.00 : dt.Compute("sum(adv)", "")));
            double loan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(loanins)", "")) ? 0.00 : dt.Compute("sum(loanins)", "")));
            double advloan = advance + loan;

            double pfamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ? 0.00 : dt.Compute("sum(pfund)", "")));
            double stampcost = 0.00;// Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 : dt.Compute("sum(hrent)", "")));
            double othded = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", "")));
            double taxamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ? 0.00 : dt.Compute("sum(itax)", "")));
            double mbilded = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mbillded)", "")) ? 0.00 : dt.Compute("sum(mbillded)", "")));
            double tdeduc = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tdeduc)", "")) ? 0.00 : dt.Compute("sum(tdeduc)", "")));

            // Bank/Cash Amt 

            double bankamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", "")));
            double cashamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", "")));


            string rptTitle = "Employee Salary Sheet : " + "Month of " + todate1;

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryFinlay", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Salary Sheet : " + "Month of " + todate1)); //+"(" + frmdate + "- " + todate + ")"
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Words: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("availday", availday.ToString()));
            Rpt1.SetParameters(new ReportParameter("wekday", wekday.ToString()));
            Rpt1.SetParameters(new ReportParameter("govday", govday.ToString()));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            // Earning 
            Rpt1.SetParameters(new ReportParameter("basic", basic.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("hrent", hrent.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("mallow", mallow.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("dallow", dallow.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("transallow", transallow.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("tiffin", tiffin.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("otamt", otamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("arrear", arrear.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("entert", entert.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("ttalerning", ttalerning.ToString("#,##0;(#,##0); ")));

            // Deduction 
            Rpt1.SetParameters(new ReportParameter("trnsded", trnsded.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("absded", absded.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("advloan", advloan.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("pfamt", pfamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("stampcost", stampcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("othersamt", othded.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("taxamt", taxamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("mbilded", mbilded.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("tdeduc", tdeduc.ToString("#,##0;(#,##0); ")));


            // Bank/Cash Amt 
            Rpt1.SetParameters(new ReportParameter("bankamt", bankamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("cashamt", cashamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("netpay", netpay.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("Bankinword", ASTUtility.Trans(bankamt, 2)));
            Rpt1.SetParameters(new ReportParameter("Casinword", ASTUtility.Trans(cashamt, 2)));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "&rptTitle=" + rptTitle + "', target='_blank');</script>";
        }

        private void PrintSalaryInnStar()
        {

            //DataTable dt = (DataTable)Session["tblpay"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            //double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            //double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            //ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryInnstar01();
            //TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();
            //TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtccaret.Text = txtfromdate.Text + " To " + txttodate.Text;//"Month of " + todate;
            //TextObject txtl1 = rpcp.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            //txtl1.Text = comadd;
            ////TextObject txtl2 = rpcp.ReportDefinition.ReportObjects["txtl2"] as TextObject;
            ////txtl2.Text = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yy");
            //TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            //txttk.Text = "In Word: " + ASTUtility.Trans(netpayatax, 2); ;
            //TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rpcp.SetDataSource(dt);
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rpcp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rpcp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryBridge()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsBridge", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalaryAlliance()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsAlliance", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryAcme()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsAcme", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintSalaryPEB()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string ddlcomp = this.ddlCompany.SelectedItem.Text;
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy");
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();

            string paytype = (this.rbtnPayType.SelectedIndex == 0) ? "CASH" : (this.rbtnPayType.SelectedIndex == 1) ? "BANK" : (this.rbtnPayType.SelectedIndex == 2) ? "CHEQUE" : "ALL";

            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            // double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", "")));

            ReportDocument rpcp = new ReportDocument();

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();

            // 9401 -head office , 9451- project, 9452- factory
            if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9401")
            {

                if (this.rbtnPayType.SelectedIndex == 0)
                {
                    if (ddlSection.SelectedValue == "940100101020")
                    {
                        if (Convert.ToInt32(monthid) >= 202106)
                        {
                            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeBank", list, null, null);
                        }
                        else
                        {
                            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeBank2", list, null, null);
                        }

                    }

                    else
                    {
                        Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeCash", list, null, null);

                    }


                }


                else if (this.rbtnPayType.SelectedIndex == 1)
                {
                    if (Convert.ToInt32(monthid) >= 202106)
                    {
                        Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeBank", list, null, null);
                    }
                    else
                    {
                        Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeBank2", list, null, null);
                    }
                }

                else if (this.rbtnPayType.SelectedIndex == 2)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOfficeCheque", list, null, null);
                }
                else
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBHeadOffice", list, null, null);
                }

            }

            //  factory report start
            else if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9452")
            {
                if (this.rbtnPayType.SelectedIndex == 0)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactoryCash", list, null, null);

                }
                else if (this.rbtnPayType.SelectedIndex == 1)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactory", list, null, null);
                }


                else
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactory", list, null, null);
                }

            }
            //  factory report end

            // factory driver
            else if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9454")
            {
                if (this.rbtnPayType.SelectedIndex == 0)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactoryCashDriver", list, null, null);
                }

                else if (this.rbtnPayType.SelectedIndex == 1)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactoryDriverBank", list, null, null);

                }

                else
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBFactory", list, null, null);

                }


            }


            else
            {

                if (this.rbtnPayType.SelectedIndex == 0)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBCash", list, null, null);
                }

                else if (this.rbtnPayType.SelectedIndex == 1)
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEBBank", list, null, null);
                }

                else
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsPEB", list, null, null);
                }


            }

            string date1 = "";
            if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9401")
            {
                date1 = "Salary for the month of " + todate1 + " - " + " (" + paytype + ") ";
            }
            else
            {
                date1 = "Salary for the month of " + todate1 + " FOR ( " + ddlcomp + " ) - (" + paytype + ") ";
            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("date", date1));
            //Rpt1.SetParameters(new ReportParameter("date", ddlProjectName.SelectedValue.Substring(0, 4) == "9401" ? "Salary for the month of " + todate1 + " (Head Office)" : "Salary for the month of " + todate1 + " (PID)"));
            //if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "9452")
            //{
            //    Rpt1.SetParameters(new ReportParameter("compName", comname.ToUpper()));
            //    Rpt1.SetParameters(new ReportParameter("date", date1));
            //}
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }

        //private DataTable HideSomeDataPEB(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;

        //    string ohour = dt1.Rows[0]["ohour"].ToString();
        //    string otrate = dt1.Rows[0]["otrate"].ToString();

        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["ohour"].ToString() == "" || Convert.ToDouble(dt1.Rows[j]["ohour"]) == 0.00 )
        //        {
        //            dt1.Rows[j]["otrate"] = "";
        //        }

        //        otrate = dt1.Rows[j]["otrate"].ToString();
        //    }
        //    return dt1;
        //}


        private void PrintSalaryLanco()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            string txtheader = this.ddlProjectName.SelectedValue.Substring(0, 4) == "9471" ? "Salary Sheet (Security Guard)" : "Salary Sheet";
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsLanco", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtHeader2", txtheader));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1));
            Rpt1.SetParameters(new ReportParameter("txtheader", this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "Grand Total - Head Office & All Project" : "Grand Total - " + this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryJBS()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            string txtheader = this.ddlProjectName.SelectedValue.Substring(0, 4) == "9471" ? "Salary Sheet (Security Guard)" : "Salary Sheet";
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsJbs", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtHeader2", txtheader));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1));
            Rpt1.SetParameters(new ReportParameter("txtheader", this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "Grand Total - Head Office & All Project" : "Grand Total - " + this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintSalaryTropical()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            string txtheader = this.ddlProjectName.SelectedValue.Substring(0, 4) == "9471" ? "Salary Sheet (Security Guard)" : "Salary Sheet";
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsTropical", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtHeader2", txtheader));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1));
            Rpt1.SetParameters(new ReportParameter("txtheader", this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "Grand Total - Head Office & All Project" : "Grand Total - " + this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryTerranova()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsTerranova", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSalaryEdisonReal()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsEdisonReal", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintSalaryGreenWood()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsGreenwood", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryManama()
        {
            //RptSalaryDetailsManama02
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsManama02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1));
            Rpt1.SetParameters(new ReportParameter("txtheader", this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "Grand Total - Head Office & All Project" : "Grand Total - " + this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryManamaBn()
        {
            //RptSalaryDetailsManama02
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            string inword = RealERPLIB.Inword.Trans(netpayatax, 2);

            string todatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(todate).ToString("dd"))) + "-" + (Convert.ToDateTime(todate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(todate).ToString("yyyy")));

            // todo for bangla date 
            dt.Columns.Add("joindatebn", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                DateTime joindate = Convert.ToDateTime(row["joindate"].ToString());
                string joindatex = "";
                if (Convert.ToDateTime(joindate).ToString("yyyy") != "1900")
                {
                    joindatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(joindate).ToString("dd"))) + "-" + (Convert.ToDateTime(joindate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(joindate).ToString("yyyy")));
                }
                row["joindatebn"] = joindatex;   // or set it to some other value

            }
            //
            Session["tblpay"] = dt;


            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();


            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsManamaBN", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", "মানামা ডেভেলপমেন্টস লিঃ"));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "বেতনের বিবৃতি : " + todatex));
            //Rpt1.SetParameters(new ReportParameter("txtheader", this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "সর্বমোট - Head Office & All Project" : "সর্বমোট - " + this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtheader", "সর্বমোট পরিমাণ"));
            //Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "কথায় : " + inword));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalarySuvastu()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd \\'MMM");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            //ReportDocument rpcp = new ReportDocument();

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();

            if (this.ddlProjectName.SelectedValue.Substring(0, 4) == "0000") //9401
            {
                //rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryDetailsSuvastuHeadOffice();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsSuvastuHeadOffice", list, null, null);
            }

            else
            {
                //rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptSalaryDetailsSuvastu();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsSuvastu", list, null, null);
            }



            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1 + "(" + frmdate + "- " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSecuritySalarySuvastu()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd \\'MMM");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SecuritySalarySuvstu>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_89_Pay.RptSecuritySalarySuvstu", list, null, null);

            Rpt1.SetParameters(new ReportParameter("comname", comname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("saldate", frmdate));


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
                    this.PrintSalaryHolding();
                    break;

                case "23":
                    this.PrintSalaryRatul();
                    break;

                default:
                    this.PrintSalaryRestRG();
                    break;

            }

        }


        private void PrintSalAssureGroup()
        {
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 4);
            switch (company)
            {
                //case "9400":
                case "9429":
                    this.PrintSecurity();
                    break;

                default:
                    this.PrintSalaryAssure();
                    break;

            }
        }


        private void PrintSecurity()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryAssSecurity01", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month of " + todate));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintSalEntrustGroup()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            string txtheader = this.ddlProjectName.SelectedValue.Substring(0, 4) == "9471" ? "Salary Sheet (Security Guard)" : "Salary Sheet";
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryEntrust", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtHeader2", txtheader));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Statement of Salary : " + "Month of " + todate1));
            Rpt1.SetParameters(new ReportParameter("txtheader", this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "Grand Total - Head Office & All Project" : "Grand Total - " + this.ddlProjectName.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintSalaryBTI()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            string companyname = this.ddlCompany.SelectedItem.Text.Trim();
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            string depcod = this.ddlBranch.SelectedValue.ToString().Substring(0, 4);
            string deptname = this.ddlBranch.SelectedItem.Text.ToString();
            string printtype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();


            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();

            if (printtype == "EXCEL")
            {


                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryHOBTIExcel", list, null, null);
            }
            else
            {

                //9401(head office)
                if (depcod == "9401")
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryHOBTI", list, null, null);
                }
                //9405(Dhaka Project)
                else if (depcod == "9405")
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDPBTI", list, null, null);
                }
                //9455(CTG office)
                else if (depcod == "9455")
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryCTGBTI", list, null, null);
                }
                //9469(SICOL)
                else if (depcod == "9469")
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalarySicolBTI", list, null, null);
                }
                //9470(CP)
                else if (depcod == "9470")
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryCPBTI", list, null, null);
                }
                else
                {
                    Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryBTI", list, null, null);
                }
            }



            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", companyname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtHeader2", deptname + " Salary (Month of " + todate1 + ")"));
            Rpt1.SetParameters(new ReportParameter("rptTitle", ""));
            Rpt1.SetParameters(new ReportParameter("txtheader", "GRAND TOTAL (INCENTIVE + NO. INCENTIVE)"));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("txtYear", Convert.ToDateTime((this.txttodate.Text)).ToString("yyyy")));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, session, printdate)));


            if (printtype == "EXCEL")
            {
                Rpt1.SetParameters(new ReportParameter("depcod", depcod));
            }





            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintSalaryAssure()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryAssure", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month of " + todate));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintSalaryRatul()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayatax)", "")) ? 0.00 : dt.Compute("sum(netpayatax)", "")));

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheetRupayan>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryRatulPro", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Details"));
            Rpt1.SetParameters(new ReportParameter("date", "Month: " + todate));
            Rpt1.SetParameters(new ReportParameter("txtl1", Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("txtl2", Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSalaryHolding()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayatax)", "")) ? 0.00 : dt.Compute("sum(netpayatax)", "")));

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheetRupayan>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetailsRG", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Details"));
            Rpt1.SetParameters(new ReportParameter("date", "Month: " + todate));
            Rpt1.SetParameters(new ReportParameter("txtl1", Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("txtl2", Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintSalaryRestRG()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpayatax)", "")) ? 0.00 : dt.Compute("sum(netpayatax)", "")));

            LocalReport Rpt1 = new LocalReport();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheetRupayan>();

            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryDetails4", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Salary Details"));
            Rpt1.SetParameters(new ReportParameter("date", "Month: " + todate));
            Rpt1.SetParameters(new ReportParameter("txtl1", Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("txtl2", Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yy")));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintSalaryLeisure()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));
            double netpayatax = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", "")));

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSalaryLeisure", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Month of " + todate));
            Rpt1.SetParameters(new ReportParameter("TkInWord", "In Word: " + ASTUtility.Trans(netpayatax, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEmpBonus()
        {
            string comcod = this.GetCompCode();
            if (this.chkSignatureSheet.Checked)
            {
                this.BonusSignatureSheet();
            }
            else
            {

                switch (comcod)
                {
                    case "4305":
                    case "4101":
                        this.BonusPrintRupGroup();
                        break;

                    //case "3101":
                    case "3330"://Bridge   

                        this.BonusSheeetBridge();
                        break;

                    //case "3101":
                    case "3333"://Alliance
                        this.BonusSheeetAlli();
                        break;

                    //case "3101":
                    case "3339"://Tropical
                        this.BonusSheeetTropical();
                        break;


                    case "3338":
                        this.PrintBonusSheetAcme();
                        break;


                    case "3315"://Assure
                        this.BonusSheeetAssure();
                        break;

                    case "3344": //Terranova
                        this.PrintBonusSheetTerranova();
                        break;

                    //case "3101":
                    case "3355": //Greenwood
                        this.PrintBonusSheetGreenWood();
                        break;

                    case "3347": //PEBSteel
                        this.PrintBonusSheetPEB();
                        break;

                    //case "3101":
                    case "3365"://BTI
                        this.PrintBonusBTI();
                        break;

                    case "3101":
                    case "3354"://Edison
                        this.PrintBonusEdison();
                        break;
                    case "3368"://Edison
                        this.PrintBonusFinlay();
                        break;

                    default:
                        this.PrintBonusSheet();
                        break;

                }

            }

        }

        private void PrintBonusFinlay()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = this.ddlCompany.SelectedItem.Text;
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITAR";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM, yyyy").ToUpper();

            string frmdateyear = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyy").ToUpper();
            string frmdatemonth = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM");




            DataTable dt3 = (DataTable)Session["tblpay"];


            DataTable dt4 = this.ExtractSameData(dt3);

            DataView dv = dt4.DefaultView;
            dv.RowFilter = "bonamt > 0";

            DataTable dt5 = new DataTable();
            dt5 = dv.ToTable();



            var list = dt5.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            double tAmt = list.Select(p => p.bonamt).Sum();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSheetFinlay", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bonus Name :  " + bonusType));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("frmdateyear", "Year :  " + frmdateyear));

            Rpt1.SetParameters(new ReportParameter("frmdatemonth", "Month of " + frmdatemonth + " / " + frmdateyear));

            Rpt1.SetParameters(new ReportParameter("tkInword", "In Word: " + ASTUtility.Trans(tAmt, 2)));
            Rpt1.SetParameters(new ReportParameter("compLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintEmpSpecialBonus()

        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            string inword = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet01>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpBonusAssure", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rpttitle", "Speical Purpose Bonus"));
            Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("date1", frmdate));
            Rpt1.SetParameters(new ReportParameter("Inword", inword));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        private void BonusPrintRupGroup()
        {
            string company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            switch (company)
            {

                case "50":
                    this.PrintBonusHolding();
                    break;

                case "23":
                    this.PrintBonusRatul();
                    break;

                default:
                    this.PrintBonusRestRG();
                    break;

            }


        }
        private void BonusSignatureSheet()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusSigSheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "SIGNATURE SHEET OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtWorkingDay", (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration(Day)" : "Duration(Month)"));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void BonusSheeet()
        {

            //DataTable dt = (DataTable)Session["tblpay"];
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comname = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            //frmdate = frmdate.ToUpper();
            //string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            //double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            //ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.RptEmpBonus();
            //TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            //TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            //txtBonType.Text = "FESTIVAL BONUS OF " + bonusType;

            //TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtccaret.Text = frmdate;
            ////TextObject txtduraton = rptempBonus.ReportDefinition.ReportObjects["txtduraton"] as TextObject;
            ////txtduraton.Text = (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration In Day" : "Duration In Month";


            //TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            //txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            //TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptempBonus.SetDataSource(dt);
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptempBonus.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptempBonus;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void BonusSheeetTropical()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusTropical", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("tkInWord", "In Word: " + ASTUtility.Trans(tbonamt, 2)));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void BonusSheeetAlli()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusAlli", list, null, null);

            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("tkInWord", "In Word: " + ASTUtility.Trans(tbonamt, 2)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void BonusSheeetBridge()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptBonusBridge1", list, null, null);

            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "FESTIVAL BONUS OF " + bonusType));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("tkInWord", "In Word: " + ASTUtility.Trans(tbonamt, 2)));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBonusHolding()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.RptEmpBonusRG();
            TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim(); ;

            TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            txtBonType.Text = "FESTIVAL BONUS OF " + bonusType;

            TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = frmdate;
            TextObject txtduraton = rptempBonus.ReportDefinition.ReportObjects["txtduraton"] as TextObject;
            txtduraton.Text = (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration In Day" : "Duration In Month";


            TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempBonus.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempBonus.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempBonus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBonusRatul()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.RptEmpBonusRatulPro();
            TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            txtBonType.Text = "FESTIVAL BONUS OF " + bonusType;

            TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = frmdate;
            TextObject txtduraton = rptempBonus.ReportDefinition.ReportObjects["txtduraton"] as TextObject;
            txtduraton.Text = (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration In Day" : "Duration In Month";


            TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempBonus.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempBonus.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempBonus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintBonusRestRG()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            frmdate = frmdate.ToUpper();
            string bonusType = (this.chkBonustype.Checked) ? " EID-UL-ADHA" : "EID-UL-FITR";
            double tbonamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ? 0.00 : dt.Compute("sum(bonamt)", "")));
            ReportDocument rptempBonus = new RealERPRPT.R_81_Hrm.R_89_Pay.RptEmpBonusRestRG();
            TextObject CompName = rptempBonus.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = this.ddlCompany.SelectedItem.Text.Trim();

            TextObject txtBonType = rptempBonus.ReportDefinition.ReportObjects["txtBonType"] as TextObject;
            txtBonType.Text = "FESTIVAL BONUS OF " + bonusType;

            TextObject txtccaret = rptempBonus.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = frmdate;
            TextObject txtduraton = rptempBonus.ReportDefinition.ReportObjects["txtduraton"] as TextObject;
            txtduraton.Text = (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration In Day" : "Duration In Month";


            TextObject txttk = rptempBonus.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(tbonamt, 2);
            TextObject txtuserinfo = rptempBonus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptempBonus.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptempBonus.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptempBonus;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        public List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            List<T>[] partitions = new List<T>[totalPartitions];

            int maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            int k = 0;

            for (int i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (int j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }


        private void PrintPaySlip()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Inwords = "";
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MMM.yyyy hh:mm:ss tt");
            string month = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yyyy");
            string txtDate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM-yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpay"];
            LocalReport Rpt1 = new LocalReport();

            if (comcod == "4301")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlip1", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtMonth1", Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtMonth2", Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtDate1", Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("txtDate2", Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }
            // rdlc for greenwood
            else if (comcod == "3355")
            {
                double netamt = Convert.ToDouble(dt.Rows[0]["netpay"]);
                string Inword = "In Word: " + ASTUtility.Trans(netamt, 2);
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                LocalReport rpt = new LocalReport();
                rpt = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipGreenwood", list, null, null);
                rpt.EnableExternalImages = true;
                rpt.SetParameters(new ReportParameter("txtCompName", comnam));
                rpt.SetParameters(new ReportParameter("txtTitle", "PAY SLIP"));
                rpt.SetParameters(new ReportParameter("txtDate", "FOR THE MONTH OF " + month));
                rpt.SetParameters(new ReportParameter("txtInword", Inword));
                rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

                Session["Report1"] = rpt;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

            else if (comcod == "3339")
            {
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipTro", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "SALARY PAYMENT VOUCHER"));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }


            else if (comcod == "3347")
            {
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipPEB", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }

            else if (comcod == "3315")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipAssure", list, null, null);

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }

            else if (comcod == "3354")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipEdisonReal", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }

            else if (comcod == "3365")
            {
                string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM-yyyy");
                string txtsign1 = "Md. Saiful Islam\nSenior Executive";
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipBTI", list, null, null);
                Rpt1.EnableExternalImages = true;

                Rpt1.SetParameters(new ReportParameter("printdate", printdate));
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("txtHeader2", "Pay Slip" + todate1 + " (Month of salary disbursement)"));
                Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

            else if (comcod == "3368" || comcod == "3101")
            {

                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();

                //List<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>[] partitionList = Partition<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>(list, 2);


                //var list1 = partitionList[0];
                //var list2 = partitionList[1];



                //Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipFinlay", list, null, null);


                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlipFinlay", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comlogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            }

            else
            {
                // All Pay Slip Except Tropical, Peb Steel
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.SalaryPaySlip>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptPaySlip", list, null, null);

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            }


        }







        private void PrintSignature()
        {
            this.ShowSignature();
            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.BonusSheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptSignatureSheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Signature Sheet"));
            Rpt1.SetParameters(new ReportParameter("txtDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("txtWorkingDay", (this.rbtlBonSheet.SelectedIndex == 2) ? "Duration(Day)" : "Duration(Month)"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCashPay()
        {

            DataTable dt = (DataTable)Session["tblpay"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(othded)", "")) ? 0.00 : dt.Compute("sum(othded)", "")));
            ReportDocument rpcp = new RealERPRPT.R_81_Hrm.R_89_Pay.RptCashPay();
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtccaret = rpcp.ReportDefinition.ReportObjects["date"] as TextObject;
            txtccaret.Text = "Month: " + frmdate; ;
            TextObject txttk = rpcp.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "In Word: " + ASTUtility.Trans(netpay, 2);
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


        private void PrintOvertimeSalary()
        {




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblpay"];
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", "")));
            string date = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_89_Pay.RptOvertimeSalary();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = this.ddlCompany.SelectedItem.Text;
            TextObject txtTitle = rptstate.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Allowance for Holiday/Friday Duties (H/O) - Month Of " + date;
            TextObject txttk = rptstate.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "Amount In Word: " + ASTUtility.Trans(Math.Round(netpay), 2);
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }



        protected void gvpayroll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SaveValue();
            this.gvpayroll.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }

        private void SaveValue()
        {

            int rowindex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];
            int i;
            switch (comcod)
            {

                case "3315":// Assure
                            // case "4101":


                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                        double bonamt = gssal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;

                        double cashamta = Convert.ToDouble(dt.Rows[rowindex]["cashamta"]);
                        string bankacno = dt.Rows[rowindex]["bankacno"].ToString();

                        double bankamt = (bankacno != "") ? (bonamt - cashamta) : 0.000000;
                        double cashamt = (bankacno == "") ? bonamt : cashamta;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                        dt.Rows[rowindex]["bankamt"] = bankamt;
                        dt.Rows[rowindex]["cashamt"] = cashamt;

                    }
                    break;


                case "3347":// Peb steel


                    //case "4305":
                    //case "4315":
                    //case "4101":


                    //    for (int i = 0; i < this.gvBonus.Rows.Count; i++)
                    //    {

                    //        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                    //        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                    //        double bonamt = gssal * 0.01 * perbonus;
                    //        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                    //        dt.Rows[rowindex]["perbon"] = perbonus;
                    //        dt.Rows[rowindex]["bonamt"] = bonamt;
                    //    }
                    //    break;

                    break;

                case "3330":

                    //case "3101":

                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());
                        double bonamt = bsal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        double bankamta = Convert.ToDouble(dt.Rows[rowindex]["bankamta"]);
                        double cashamta = Convert.ToDouble(dt.Rows[rowindex]["cashamta"]);
                        string bankgrp = dt.Rows[rowindex]["bankgrp"].ToString();
                        double bankamt = (bankgrp == "b1") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                        double bankamt2 = (bankgrp == "b2") ? (bonamt - (bankamta + cashamta)) : bankamta;
                        double cashamt = (bankgrp == "") ? bonamt : cashamta;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                        dt.Rows[rowindex]["bankamt"] = bankamt;
                        dt.Rows[rowindex]["bankamt2"] = bankamt2;
                        dt.Rows[rowindex]["cashamt"] = cashamt;

                    }
                    break;

                case "3333":
                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());

                        double bonamt = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("txtgvBonusAmt")).Text.Trim());
                        bonamt = bonamt > 0 ? bonamt : (gssal * 0.01 * perbonus);
                        perbonus = perbonus > 0 ? perbonus : (bonamt / (gssal * 0.01));
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }


                    //for (int i = 0; i < this.gvBonus.Rows.Count; i++)
                    //{

                    //    double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                    //    double gross = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                    //    double bonamt = gross * 0.01 * perbonus;
                    //    rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                    //    //double bankamta = Convert.ToDouble(dt.Rows[rowindex]["bankamta"]);
                    //    //double cashamta = Convert.ToDouble(dt.Rows[rowindex]["cashamta"]);
                    //    //string bankgrp = dt.Rows[rowindex]["bankgrp"].ToString();
                    //    //double bankamt = (bankgrp == "b1") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                    //    //double bankamt2 = (bankgrp == "b2") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                    //    //double cashamt = (bankgrp == "") ? bonamt : cashamta;
                    //    dt.Rows[rowindex]["perbon"] = perbonus;
                    //    dt.Rows[rowindex]["bonamt"] = bonamt;
                    //    dt.Rows[rowindex]["bankamt"] = bankamt;
                    //    dt.Rows[rowindex]["bankamt2"] = bankamt2;
                    //    dt.Rows[rowindex]["cashamt"] = cashamt;

                    //}
                    break;



                case "3338":
                    //case "3101":
                    //case "3101":
                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                        double bonamt = gssal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        double bankamta = Convert.ToDouble(dt.Rows[rowindex]["bankamta"]);
                        double cashamta = Convert.ToDouble(dt.Rows[rowindex]["cashamta"]);
                        string bankgrp = dt.Rows[rowindex]["bankgrp"].ToString();
                        double bankamt = (bankgrp == "b1") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                        double bankamt2 = (bankgrp == "b2") ? (bonamt - (bankamta + cashamta)) : bankamta;
                        double cashamt = (bankgrp == "") ? bonamt : cashamta;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                        dt.Rows[rowindex]["bankamt"] = bankamt;
                        dt.Rows[rowindex]["bankamt2"] = bankamt2;
                        dt.Rows[rowindex]["cashamt"] = cashamt;

                    }
                    break;

                case "3339":
                    // case "3101":

                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());

                        double bonamt = (perbonus == 0 ? 0 : gssal / perbonus);
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }

                    break;


                case "3355":
                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvGsalb")).Text.Trim());
                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());
                        double bonamt = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("txtgvBonusAmt")).Text.Trim());
                        bonamt = gssal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }

                    break;

                case "3354": //Edison Real Estate
                case "3365": //BTI 
                    i = 0;
                    foreach (GridViewRow gv1 in this.gvBonus.Rows)
                    {

                       
                        double bonamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvBonusAmt")).Text.Trim());
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;

                        double bankamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvBankAmtbon")).Text.Trim());
                        double bankamt2 = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvBankAmt2bon")).Text.Trim()); ;
                        //double cashamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvcashAmtbon")).Text.Trim()); ;

                        double cashamt = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)gv1.FindControl("txtgvcashAmtbon")).Text.Trim()));

                        dt.Rows[rowindex]["bonamt"] = bonamt;
                        dt.Rows[rowindex]["bankamt"] = bonamt > 0 ? bankamt : 0.00;
                        dt.Rows[rowindex]["bankamt2"] = bonamt > 0 ? bankamt2 : 0.00;
                        dt.Rows[rowindex]["cashamt"] = bonamt > 0 ? cashamt : 0.00;
                        i++;
                    }
                    break;

                case "3368": //Finlay 
                    i = 0;
                    foreach (GridViewRow gv1 in this.gvBonus.Rows)
                    {


                        double bonamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvBonusAmt")).Text.Trim());
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;

                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());
                        double bankamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvBankAmtbon")).Text.Trim());
                        double bankamt2 = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvBankAmt2bon")).Text.Trim()); ;
                        double cashamt = Convert.ToDouble("0" + ((TextBox)gv1.FindControl("txtgvcashAmtbon")).Text.Trim()); ;


                        double percentbon = (bonamt * 100) / bsal;

                        dt.Rows[rowindex]["perbon"] = percentbon;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                        dt.Rows[rowindex]["bankamt"] = bonamt > 0 ? bankamt : 0.00;
                        dt.Rows[rowindex]["bankamt2"] = bonamt > 0 ? bankamt2 : 0.00;
                        dt.Rows[rowindex]["cashamt"] = bonamt > 0 ? cashamt : 0.00;
                        i++;
                    }
                    break;
                default:
                    for (i = 0; i < this.gvBonus.Rows.Count; i++)
                    {
                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double bsal = Convert.ToDouble("0" + ((Label)this.gvBonus.Rows[i].FindControl("lgvBasicb")).Text.Trim());

                        double bonamt = bsal * 0.01 * perbonus;
                        rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }
                    break;
            }

            Session["tblpay"] = dt;
        }

        private void SaveRrmks()
        {
            int rowindex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];

            for (int i = 0; i < this.gvpayroll.Rows.Count; i++)
            {
                string rmrks = ((TextBox)this.gvpayroll.Rows[i].FindControl("lgvrmrks")).Text.Trim();
                string rmrks2 = ((TextBox)this.gvpayroll.Rows[i].FindControl("lgvrmrks2")).Text.Trim();
                rowindex = (this.gvpayroll.PageSize) * (this.gvpayroll.PageIndex) + i;
                dt.Rows[rowindex]["rmrks"] = rmrks;
                dt.Rows[rowindex]["rmrks2"] = rmrks2;


            }
            Session["tblpay"] = dt;
        }
        private void SaveRrmks2()
        {

            int rowindex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];


            for (int i = 0; i < this.gvBonus.Rows.Count; i++)
            {
                string rmrks = ((TextBox)this.gvBonus.Rows[i].FindControl("txtgvBonusRemarks")).Text.Trim();
                rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + i;
                dt.Rows[rowindex]["rmrks"] = rmrks;
            }

            Session["tblpay"] = dt;
        }

        protected void lnkFiUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (!Convert.ToBoolean(dr6[0]["entry"]))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                string emptype = this.Request.QueryString["Type"] == "SalResign" ? "R" : "";
                this.SaveRrmks();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                DataTable dt = (DataTable)Session["tblpay"];
                string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM");

                bool result = false;

                string mantype = "";
                switch (comcod)
                {
                    case "3338":
                        mantype = (this.rbtnlistsaltype.SelectedIndex == 0) ? "86001%" : (this.rbtnlistsaltype.SelectedIndex == 1) ? "86002%" : "86%";
                        break;
                    default:
                        mantype = "86%";
                        break;
                }


                int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
                // string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);



                string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
                string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";

                string branchdel = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4)) + "%";

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETESALSHEET", monthid, Company, Department, Section, mantype, emptype, branchdel, "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string empid = dt.Rows[i]["empid"].ToString();
                    double dedday = Convert.ToDouble(dt.Rows[i]["dedday"].ToString());

                    //if (dedday > 0)
                    //{
                    //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPEMPSALADJST", monthid, empid, dedday.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");

                    //}


                    string idcard = dt.Rows[i]["idcard"].ToString();
                    // string joindate = dt.Rows[i]["joindate"].ToString();
                    string section = dt.Rows[i]["section"].ToString();
                    string desigid = dt.Rows[i]["desigid"].ToString();
                    string desig = dt.Rows[i]["desig"].ToString();
                    string empname = dt.Rows[i]["empname"].ToString();
                    string refno = dt.Rows[i]["refno"].ToString();
                    string wd = dt.Rows[i]["wd"].ToString();
                    string absday = dt.Rows[i]["absday"].ToString();
                    string wld = dt.Rows[i]["wld"].ToString();
                    string acat = dt.Rows[i]["acat"].ToString();
                    string bsal = dt.Rows[i]["bsal"].ToString();
                    string hrent = dt.Rows[i]["hrent"].ToString();
                    string cven = dt.Rows[i]["cven"].ToString();
                    string mallow = dt.Rows[i]["mallow"].ToString();
                    string arsal = dt.Rows[i]["arsal"].ToString();
                    string pickup = dt.Rows[i]["pickup"].ToString();
                    string fuel = dt.Rows[i]["fuel"].ToString();
                    string entaint = dt.Rows[i]["entaint"].ToString();
                    string mcell = dt.Rows[i]["mcell"].ToString();
                    string incent = dt.Rows[i]["incent"].ToString();
                    string oth = dt.Rows[i]["oth"].ToString();
                    string pfund = dt.Rows[i]["pfund"].ToString();
                    string itax = dt.Rows[i]["itax"].ToString();
                    string adv = dt.Rows[i]["adv"].ToString();
                    string loanins = dt.Rows[i]["loanins"].ToString();
                    string othded = dt.Rows[i]["othded"].ToString();
                    string dallow = dt.Rows[i]["dallow"].ToString();
                    string oallow = dt.Rows[i]["oallow"].ToString();
                    string ohour = dt.Rows[i]["ohour"].ToString();
                    string hallow = dt.Rows[i]["hallow"].ToString();
                    string elallow = dt.Rows[i]["elallow"].ToString();
                    string mbill = dt.Rows[i]["mbill"].ToString();
                    string lwided = dt.Rows[i]["lwided"].ToString();
                    string gssal = dt.Rows[i]["gssal"].ToString();
                    string salpday = dt.Rows[i]["salpday"].ToString();
                    string gspay = dt.Rows[i]["gspay"].ToString();
                    string absded = dt.Rows[i]["absded"].ToString();
                    string tallow = dt.Rows[i]["tallow"].ToString();
                    string tdeduc = dt.Rows[i]["tdeduc"].ToString();
                    string mcadj = dt.Rows[i]["mcadj"].ToString();
                    string sdedamt = dt.Rows[i]["sdedamt"].ToString();
                    string netpay = dt.Rows[i]["netpay"].ToString();
                    string refdesc = dt.Rows[i]["refdesc"].ToString();
                    string sectionname = dt.Rows[i]["sectionname"].ToString();
                    string othallow = dt.Rows[i]["othallow"].ToString();
                    string tptallow = dt.Rows[i]["tptallow"].ToString();
                    string kpi = dt.Rows[i]["kpi"].ToString();
                    string perbon = dt.Rows[i]["perbon"].ToString();
                    string othearn = dt.Rows[i]["othearn"].ToString();
                    string mcallow = dt.Rows[i]["mcallow"].ToString();
                    string teallow = dt.Rows[i]["teallow"].ToString();
                    string thday = dt.Rows[i]["thday"].ToString();
                    string lwpday = dt.Rows[i]["lwpday"].ToString();
                    string arded = dt.Rows[i]["arded"].ToString();
                    string cashamt = dt.Rows[i]["cashamt"].ToString();
                    string bankamt = dt.Rows[i]["bankamt"].ToString();

                    string wjd = dt.Rows[i]["wjd"].ToString();
                    string empcont = dt.Rows[i]["empcont"].ToString();
                    string elftam = dt.Rows[i]["elftam"].ToString();
                    string elfthour = dt.Rows[i]["ellfthour"].ToString();
                    string dalday = dt.Rows[i]["dalday"].ToString();
                    string ddaya10 = dt.Rows[i]["ddaya10"].ToString();
                    string dday10amt = dt.Rows[i]["dday10amt"].ToString();
                    string fallded = dt.Rows[i]["fallded"].ToString();
                    string mbillded = dt.Rows[i]["mbillded"].ToString();
                    string bankamt2 = dt.Rows[i]["bankamt2"].ToString();
                    string wkday = dt.Rows[i]["wkday"].ToString();
                    string govday = dt.Rows[i]["govday"].ToString();
                    string rmrks = dt.Rows[i]["rmrks"].ToString();
                    string haircutal = dt.Rows[i]["haircutal"].ToString();
                    string foodal = dt.Rows[i]["foodal"].ToString();
                    string nfoodal = dt.Rows[i]["nfoodal"].ToString();
                    string otallow = dt.Rows[i]["otall"].ToString();
                    string redamt = dt.Rows[i]["redamt"].ToString();
                    string chequepay = dt.Rows[i]["chequepay"].ToString();
                    string todecashsal = dt.Rows[i]["todecashsal"].ToString();
                    string hardship = dt.Rows[i]["hardship"].ToString();
                    string fine = dt.Rows[i]["fine"].ToString();
                    string cashded = dt.Rows[i]["cashded"].ToString();
                    string tripal = dt.Rows[i]["tripal"].ToString();
                    string absded2 = dt.Rows[i]["absded2"].ToString();
                    string absded3 = dt.Rows[i]["absded3"].ToString();
                    string rmrks2 = dt.Rows[i]["rmrks2"].ToString();
                    string ottotal = dt.Rows[i]["ottotal"].ToString();
                    string finedays = dt.Rows[i]["finedays"].ToString();
                    string lateday = dt.Rows[i]["lateday"].ToString();
                    string latededuc = dt.Rows[i]["latededuc"].ToString();
                    string adjustamt = dt.Rows[i]["adjustamt"].ToString();
                    string transded = dt.Rows[i]["transded"].ToString();
                    string genloan = dt.Rows[i]["genloan"].ToString();
                    string carloan = dt.Rows[i]["carloan"].ToString();
                    string perloan = dt.Rows[i]["perloan"].ToString();
                    string motolon = dt.Rows[i]["motolon"].ToString();
                    string dresslon = dt.Rows[i]["dresslon"].ToString();
                    string msetlon = dt.Rows[i]["msetloan"].ToString();
                    string msclon = dt.Rows[i]["mscloan"].ToString();



                    result = HRData.UpdateTransInfoHRSal(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INUPSALSHEET", monthid, refno, empid, wd, absday, wld, acat, bsal, hrent, cven,
                        mallow, arsal, pickup, fuel, entaint, mcell, incent, oth, pfund, itax, adv, othded, dallow, oallow, ohour, hallow, elallow, mbill, lwided, loanins, gssal, salpday, gspay, absded,
                        tallow, tdeduc, dedday.ToString(), sdedamt, netpay, section, desigid, mcadj, othallow, othearn, mcallow, teallow, thday, lwpday, arded, cashamt, bankamt, wjd, empcont, elftam, elfthour,
                        dalday, ddaya10, dday10amt, fallded, mbillded, bankamt2, wkday, govday, rmrks, tptallow, kpi, perbon, haircutal, foodal, nfoodal, otallow, redamt, chequepay, todecashsal, hardship, fine,
                        cashded, tripal, absded2, absded3, rmrks2, ottotal, finedays, lateday, latededuc, adjustamt, transded, genloan, carloan, perloan, motolon, dresslon, msetlon, msclon, emptype);
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        return;//refdesc
                    }


                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                DataTable dtuser = (DataTable)Session["UserLog"];
                string userid = hst["usrid"].ToString();
                string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                string Terminal = hst["compname"].ToString();
                string Sessionid = hst["session"].ToString();

                string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4));


                //bool islock = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked==true) ? true : false;
                //string Salarylock = islock == false ? "0" : "1";
                string Salarylock = (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked) ? "1" : "0";

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPSALLOCK", monthid, branch, Salarylock, userid, Posteddat, Terminal, Sessionid, "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }



            }

            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


        }




        protected void chkBonus_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlBonus.Visible = this.chkBonus.Checked;
        }
        protected void gvBonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvBonus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }


        protected void lbtnTotalBonus_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.LoadGrid();
        }
        protected void lnkbtnGenBonus_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];
            double perbonus = Convert.ToDouble("0" + this.txtBonusPer.Text);
            switch (comcod)
            {
                case "4305":
                case "4315":
                case "4325":


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double gssal = Convert.ToDouble(dt.Rows[i]["gssal"]);
                        double bonamt = gssal * 0.01 * perbonus;
                        dt.Rows[i]["perbon"] = perbonus;
                        dt.Rows[i]["bonamt"] = bonamt;
                    }

                    break;


                case "3330":
                    // case "3101":


                    foreach (DataRow dr1 in dt.Rows)
                    {

                        //  double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double bsal = Convert.ToDouble(dr1["bsal"].ToString());
                        double bonamt = bsal * 0.01 * perbonus;

                        double bankamta = Convert.ToDouble(dr1["bankamta"].ToString());
                        double cashamta = Convert.ToDouble(dr1["cashamta"].ToString());
                        string bankgrp = dr1["bankgrp"].ToString();
                        double bankamt = (bankgrp == "b1") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                        double bankamt2 = (bankgrp == "b2") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                        double cashamt = (bankgrp == "") ? bonamt : cashamta;
                        dr1["perbon"] = perbonus;
                        dr1["bonamt"] = bonamt;
                        dr1["bankamt"] = bankamt;
                        dr1["bankamt2"] = bankamt2;
                        dr1["cashamt"] = cashamt;

                    }
                    break;


                case "3333":
                    // case "3101":


                    foreach (DataRow dr1 in dt.Rows)
                    {

                        //  double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvBonus.Rows[i].FindControl("lgPerBonus")).Text.Replace("%", "").Trim());
                        double gross = Convert.ToDouble(dr1["gssal"].ToString());
                        double bonamt = gross * 0.01 * perbonus;

                        double bankamta = Convert.ToDouble(dr1["bankamta"].ToString());
                        double cashamta = Convert.ToDouble(dr1["cashamta"].ToString());
                        string bankgrp = dr1["bankgrp"].ToString();
                        double bankamt = (bankgrp == "b1") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                        double bankamt2 = (bankgrp == "b2") ? (bonamt - (bankamta + cashamta)) : 0.000000;
                        double cashamt = (bankgrp == "") ? bonamt : cashamta;
                        dr1["perbon"] = perbonus;
                        dr1["bonamt"] = bonamt;
                        dr1["bankamt"] = bankamt;
                        dr1["bankamt2"] = bankamt2;
                        dr1["cashamt"] = cashamt;

                    }
                    break;





                default:
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double bsal = Convert.ToDouble(dt.Rows[i]["bsal"]);
                        double bonamt = bsal * 0.01 * perbonus;
                        dt.Rows[i]["perbon"] = perbonus;
                        dt.Rows[i]["bonamt"] = bonamt;
                    }

                    break;





            }



            Session["tblpay"] = dt;
            this.LoadGrid();
            this.chkBonus.Checked = false;
            this.chkBonus_CheckedChanged(null, null);

        }
        protected void lnkUpBonus_Click(object sender, EventArgs e)
        {
            DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            string Msg = "";



            if (!Convert.ToBoolean(dr6[0]["entry"]))
            {
                Msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveValue();
            this.SaveRrmks2();
            DataTable dt = (DataTable)Session["tblpay"];


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM");
            string bondate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            bool result = false;
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETEBONSHEET", monthid, Company, Department, Section, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                Msg = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);


                return;
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string perbon = Convert.ToDouble(dt.Rows[i]["perbon"]).ToString();
                string bsal = Convert.ToDouble(dt.Rows[i]["bsal"]).ToString();
                string gssal = Convert.ToDouble(dt.Rows[i]["gssal"]).ToString();
                string section = dt.Rows[i]["section"].ToString();
                string desigid = dt.Rows[i]["desigid"].ToString();
                string duration = Convert.ToDouble(dt.Rows[i]["duration"]).ToString();
                string bonamt = Convert.ToDouble(dt.Rows[i]["bonamt"]).ToString();
                string bankamt = Convert.ToDouble(dt.Rows[i]["bankamt"]).ToString();
                string bankamt2 = Convert.ToDouble(dt.Rows[i]["bankamt2"]).ToString();
                string cashamt = Convert.ToDouble(dt.Rows[i]["cashamt"]).ToString();
                string rmrks = dt.Rows[i]["rmrks"].ToString();
                string spbonamt = Convert.ToDouble(dt.Rows[i]["spbonamt"]).ToString();
                string chkbnkcsh = Convert.ToInt32(dt.Rows[i]["chkbankcash"]).ToString();

                result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPHRBONINF", monthid, empid, perbon, bsal, gssal, bondate, section, desigid, duration, bonamt, bankamt, bankamt2, cashamt, rmrks, spbonamt, chkbnkcsh, "", "", "", "", "");
            }

            Msg = "Updated Success";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);


            string branch = (this.ddlBranch.SelectedValue.ToString() == "000000000000" || this.ddlBranch.SelectedValue.ToString() == "" ? Company : this.ddlBranch.SelectedValue.ToString().Substring(0, 4));
             
            string Bonlock = (((CheckBox)this.gvBonus.FooterRow.FindControl("chkbonLock")).Checked) ? "1" : "0";
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "INORUPBONLOCK", monthid, branch, Bonlock, "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                Msg = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);
                return;
            }

        }

        protected void gvcashpay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvcashpay.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }






        protected void gvOvertime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }


        protected void lnkTotal_Click(object sender, EventArgs e)
        {
            //    tdeduc=a.pfund+a.fallded+a.mbillded+a.othded+a.lwided+a.loanins+a.arded+a.adv+a.itax+a.mbill, 

            //    netpay=a.gspay1-(a.tdeduc+a.sdedamt+a.absded+dday10amt), netpayatax=0.00, a.bankacno,
            //bankamt=(case when a.bankacno<>'' then a.gspay1-(a.tdeduc+a.sdedamt+a.absded+dday10amt+a.cashamt) else 0.00 end), 
            //cashamt=(case when a.bankacno='' then a.gspay1-(a.tdeduc+a.sdedamt+a.absded+dday10amt) else a.cashamt end)


            DataTable dt = (DataTable)Session["tblpay"];

            int rowindex;
            for (int i = 0; i < this.gvpayroll.Rows.Count; i++)
            {

                //double itax = Convert.ToDouble("0" + ((TextBox)this.gvpayroll.Rows[i].FindControl("txtgvitax")).Text.Trim());
                string rmrks = ((TextBox)this.gvpayroll.Rows[i].FindControl("lgvrmrks")).Text.Trim();
                rowindex = (gvpayroll.PageIndex) * gvpayroll.PageSize + i;
                //string bankacno =dt.Rows[rowindex]["bankacno"].ToString();
                //double gspay1 = Convert.ToDouble(dt.Rows[rowindex]["gspay1"]);
                //double pfund = Convert.ToDouble(dt.Rows[rowindex]["pfund"]);
                //double fallded = Convert.ToDouble(dt.Rows[rowindex]["fallded"]);
                //double mbillded = Convert.ToDouble(dt.Rows[rowindex]["mbillded"]);
                //double othded = Convert.ToDouble(dt.Rows[rowindex]["othded"]);
                //double lwided = Convert.ToDouble(dt.Rows[rowindex]["lwided"]);
                //double loanins = Convert.ToDouble(dt.Rows[rowindex]["loanins"]);
                //double arded = Convert.ToDouble(dt.Rows[rowindex]["arded"]);
                //double adv = Convert.ToDouble(dt.Rows[rowindex]["adv"]);
                //double mbill = Convert.ToDouble(dt.Rows[rowindex]["mbill"]);
                //double sdedamt = Convert.ToDouble(dt.Rows[rowindex]["sdedamt"]);
                //double absded = Convert.ToDouble(dt.Rows[rowindex]["absded"]);
                //double dday10amt = Convert.ToDouble(dt.Rows[rowindex]["dday10amt"]);
                //double cashamt = Convert.ToDouble(dt.Rows[rowindex]["cashamt"]);

                //double tdeduc = pfund + fallded + mbillded + othded + lwided + loanins + arded + adv + itax + mbill;
                //double netpay = gspay1 - (tdeduc + sdedamt + absded + dday10amt);



                //double bankamt = (bankacno!="")? (gspay1-(tdeduc+sdedamt+absded+dday10amt+cashamt)):0.000000;
                //cashamt = (bankacno == "") ? (gspay1 - (tdeduc + sdedamt + absded + dday10amt)) : 0.000000 ;
                //dt.Rows[rowindex]["itax"] = itax;
                //dt.Rows[rowindex]["tdeduc"] = tdeduc;
                //dt.Rows[rowindex]["netpay"] = netpay;
                //dt.Rows[rowindex]["bankamt"] = bankamt;
                //dt.Rows[rowindex]["cashamt"] = cashamt;
                dt.Rows[rowindex]["rmrks"] = rmrks;



            }
            Session["tblpay"] = dt;
            this.LoadGrid();




        }
        protected void gvBonus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];


            int rowindex = (this.gvBonus.PageSize) * (this.gvBonus.PageIndex) + e.RowIndex;

            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM");
            string empid = dt.Rows[rowindex]["empid"].ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "DELETEMPBON", monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblpay");
            Session["tblpay"] = dv.ToTable();
            this.LoadGrid();



        }

        protected void gvsbonus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvsbonus.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }

        protected void gvsbonus_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];


            int rowindex = (this.gvsbonus.PageSize) * (this.gvsbonus.PageIndex) + e.RowIndex;

            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM");
            string empid = dt.Rows[rowindex]["empid"].ToString();

            bool result = HRData.UpdateTransInfo(comcod, "[dbo_hrm].[SP_REPORT_PAYROLL]", "DELETEMPSPECIALBON", monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblpay");
            Session["tblpay"] = dv.ToTable();
            this.LoadGrid();


        }


        protected void lnkbtnGenspBonus_Click(object sender, EventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];
            double perbonus = Convert.ToDouble("0" + this.txtspecialbonus.Text);
            switch (comcod)
            {
                case "4305":
                case "3315":
                case "3101":


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double gssal = Convert.ToDouble(dt.Rows[i]["gssal"]);
                        double bonamt = gssal * 0.01 * perbonus;
                        dt.Rows[i]["perbon"] = perbonus;
                        dt.Rows[i]["bonamt"] = bonamt;
                    }

                    break;



                default:
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double bsal = Convert.ToDouble(dt.Rows[i]["bsal"]);
                        double bonamt = bsal * 0.01 * perbonus;
                        dt.Rows[i]["perbon"] = perbonus;
                        dt.Rows[i]["bonamt"] = bonamt;
                    }

                    break;





            }



            Session["tblpay"] = dt;
            this.LoadGrid();
            //this.chkspBonus.Checked = false;
            //this.chkspBonus_CheckedChanged(null, null);

        }




        private void SaveSpecialBonus()
        {

            int rowindex;
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblpay"];

            switch (comcod)
            {

                case "3315":// Assure
                case "3101":

                    for (int i = 0; i < this.gvsbonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvsbonus.Rows[i].FindControl("lgPersbonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvsbonus.Rows[i].FindControl("lgvGsalbsbonus")).Text.Trim());
                        double bonamt = Convert.ToDouble("0" + ((TextBox)this.gvsbonus.Rows[i].FindControl("txtgvsbonusAmt")).Text.Trim());


                        bonamt = bonamt > 0 ? bonamt : (gssal * 0.01 * perbonus);
                        perbonus = perbonus > 0 ? perbonus : (bonamt / (gssal * 0.01));
                        rowindex = (this.gvsbonus.PageSize) * (this.gvsbonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;

                        //double bankamt = (bankacno != "") ? (bonamt - cashamta) : 0.000000;
                        //double cashamt = (bankacno == "") ? bonamt : cashamta;
                        //dt.Rows[rowindex]["perbon"] = perbonus;
                        //dt.Rows[rowindex]["bonamt"] = bonamt;
                        //dt.Rows[rowindex]["bankamt"] = bankamt;
                        //dt.Rows[rowindex]["cashamt"] = cashamt;

                    }
                    break;




                default:

                    for (int i = 0; i < this.gvsbonus.Rows.Count; i++)
                    {

                        double perbonus = Convert.ToDouble("0" + ((TextBox)this.gvsbonus.Rows[i].FindControl("lgPersbonus")).Text.Replace("%", "").Trim());
                        double gssal = Convert.ToDouble("0" + ((Label)this.gvsbonus.Rows[i].FindControl("lgvGsalbsbonus")).Text.Trim());

                        double bsal = Convert.ToDouble("0" + ((Label)this.gvsbonus.Rows[i].FindControl("lgvGsalbsbonus")).Text.Trim());
                        double bonamt = Convert.ToDouble("0" + ((TextBox)this.gvsbonus.Rows[i].FindControl("txtgvsbonusAmt")).Text.Trim());

                        bonamt = bonamt > 0 ? bonamt : (gssal * 0.01 * perbonus);
                        perbonus = perbonus > 0 ? perbonus : (bonamt / (gssal * 0.01));
                        rowindex = (this.gvsbonus.PageSize) * (this.gvsbonus.PageIndex) + i;
                        dt.Rows[rowindex]["perbon"] = perbonus;
                        dt.Rows[rowindex]["bonamt"] = bonamt;
                    }

                    break;





            }

            Session["tblpay"] = dt;



        }

        protected void lnkUpSPBonus_Click(object sender, EventArgs e)
        {
            DataRow[] dr6 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            string Msg = "";
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            if (!Convert.ToBoolean(dr6[0]["entry"]))
            {
                Msg = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);

                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.SaveSpecialBonus();

            DataTable dt = (DataTable)Session["tblpay"];


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln);
            string monthid = Convert.ToDateTime(this.txtfromdate.Text).ToString("yyyyMM");
            string bondate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");

            bool result = false;
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "DELETEHRSPECIALBONSHEET", monthid, Company, Department, Section, "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                Msg = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);


                return;
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                string perbon = Convert.ToDouble(dt.Rows[i]["perbon"]).ToString();
                string bsal = Convert.ToDouble(dt.Rows[i]["bsal"]).ToString();
                string gssal = Convert.ToDouble(dt.Rows[i]["gssal"]).ToString();
                string section = dt.Rows[i]["section"].ToString();
                string desigid = dt.Rows[i]["desigid"].ToString();
                string duration = Convert.ToDouble(dt.Rows[i]["duration"]).ToString();
                string bonamt = Convert.ToDouble(dt.Rows[i]["bonamt"]).ToString();
                string bankamt = Convert.ToDouble(dt.Rows[i]["bankamt"]).ToString();
                string bankamt2 = Convert.ToDouble(dt.Rows[i]["bankamt2"]).ToString();
                string cashamt = Convert.ToDouble(dt.Rows[i]["cashamt"]).ToString();
                string rmrks = dt.Rows[i]["rmrks"].ToString();
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "INSERTORUPHRSPECIALBONINF", monthid, empid, perbon, bsal, gssal, bondate, section, desigid, duration, bonamt, bankamt, bankamt2, cashamt, rmrks, "");



            }

            if (!result)
            {
                Msg = "Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Msg + "');", true);


                return;
            }

            else
            {
                Msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Msg + "');", true);

            }




        }

        protected void lbtnTotalsbonus_Click(object sender, EventArgs e)
        {
            this.SaveSpecialBonus();
            this.LoadGrid();
        }

        protected void gvlnkbtnTotalDed_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblpay"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string empId = dt.Rows[rowIndex]["empid"].ToString();

            this.GetDeductionDetails(empId);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenDedModal();", true);
        }

        private void GetDeductionDetails(string empId)
        {
            string comcod = this.GetCompCode();
            string fromDate = this.txtfromdate.Text;
            string toDate = this.txttodate.Text;

            DataSet ds1 = HRData.GetTransInfo(comcod, "[dbo_hrm].[SP_REPORT_PAYROLL]", "GET_EMPLOYEE_DEDUCTION_INFO", fromDate, toDate, empId, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tbldeddetails"] = ds1.Tables[0];
            this.ShowDedDetails();
        }

        private void ShowDedDetails()
        {
            DataTable dt = (DataTable)ViewState["tbldeddetails"];
            this.gvDedDetails.DataSource = dt;
            this.gvDedDetails.DataBind();

            this.lblmTotDed.Text = Convert.ToDouble(dt.Rows[0]["tdeduc"]).ToString("#,##0;(#,##0); ");
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }
        //Called on input textbox value is changed
        private void SearchText()
        {
            DataTable dt = (DataTable)Session["tblpay"];
            DataView dv = new DataView(dt);
            string SearchExpression = null;
            //check if value is null or not, if not search it
            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                SearchExpression = string.Format("{0} '%{1}%'",
                gvpayroll.SortExpression, txtSearch.Text);
                dv.RowFilter = "idcard like" + SearchExpression;
                gvpayroll.DataSource = dv;
                gvpayroll.DataBind();

            }

            gvpayroll.DataSource = dv;
            gvpayroll.DataBind();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void chkSalaryLock_CheckedChanged(object sender, EventArgs e)
        {


            if (((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked == true)
            {
                ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = true;
                this.lblComSalLock.Text = "True";
            }
            else
            {
                this.lblComSalLock.Text = "False";
                ((CheckBox)this.gvpayroll.FooterRow.FindControl("chkSalaryLock")).Checked = false;
            }

        }
 
    }
}
