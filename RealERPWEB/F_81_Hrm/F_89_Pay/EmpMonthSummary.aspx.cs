using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpMonthSummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                string type = this.Request.QueryString["Type"].ToString().Trim();

                //((Label)this.Master.FindControl("lblTitle")).Text = type == "salati" ? "AIT purpose salary " : type == "salsumMonth"? "Salary Summary (Month Wise)" :"Monthly Attendance Statement";
                this.GetCompany();
                this.SetDate();

                if(type== "salati")
                {
                    this.ChkAllWithwithout.Visible = true;
                }


                
            }

        }

        private void SetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "8701"://Sanmer
                            //case "4305"://Rupayan
                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string Company = (this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            //string Company = this.CompanyType();
            string txtSProject = this.txtSrcPro.Text + "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, userid, "", "", "", "", "", "");

            //Remove 
            DataView dv = ds1.Tables[0].DefaultView;
            if (comcod=="3315" || comcod == "3316" || comcod == "3101")
            { }
            else
            {
                dv.RowFilter = ("actcode<>'000000000000'");
            }       


            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = dv.ToTable();
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }

        //private string CompanyType()
        //{
        //    string comtype = "";
        //    string comcod = this.GetCompCode();
        //    switch (comcod)
        //    {

        //        //case "3101":
        //        case "3315":
        //        case "3316":
        //            comtype = this.ddlCompany.SelectedValue.ToString().Substring(0, 4) + "%";
        //            break;

        //        default:
        //            comtype = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
        //            break;



        //    }

        //    return comtype;
        //}
        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            this.GetEmployeeName();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            if (type == "salati")
            {
               
                this.ShowATI();
            }

            else if(type == "salsumMonth")
            {
                this.SalSummMonth();
            }
            else
            {
                this.ShowSal();
            }

        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ibtnEmpList_Click(null, null);
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            // this.GetEmployeeName();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            
            DataTable dt = (DataTable)Session["tblpay"];
            if (dt.Rows.Count == 0)
                return;

            string linkType = Request.QueryString["Type"].ToString().Trim();
            switch (linkType)
            {
                case "salary":
                    this.gvempsumm.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvempsumm.DataSource = dt;
                    this.gvempsumm.DataBind();
                    break;

                case "salati":
                    this.gvati.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvati.DataSource = dt;
                    this.gvati.DataBind();
                    this.FooterCalculation();
                    break;

                case "salsumMonth":
                    this.gvsalmonthly.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvsalmonthly.DataSource = dt;
                    this.gvsalmonthly.DataBind();
                    this.FooterCalculation();
                    break;

            }

        }
        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblpay"];
            if (dt.Rows.Count == 0)
                return;
            switch (type)
            {
                case "salary":

                    break;
                case "salati":

                    ((Label)this.gvati.FooterRow.FindControl("lgvFbsal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ?
                                    0 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ?
                                   0 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFcven")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ?
                                   0 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ?
                                   0 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFDailyall")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otallow)", "")) ?
                                   0 : dt.Compute("sum(otallow)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvati.FooterRow.FindControl("lgvFpfund")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ?
                                   0 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvati.FooterRow.FindControl("lgvFgsal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsal)", "")) ?
                                   0 : dt.Compute("sum(gsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFgsal1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsal1)", "")) ?
                                   0 : dt.Compute("sum(gsal1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFbonamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ?
                                  0 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFitax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ?
                                   0 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ?
                                   0 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvati.FooterRow.FindControl("lgvFbankamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ?
                                   0 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "salsumMonth":

                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFbsalm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ?
                                    0 : dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFhrentm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ?
                                   0 : dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFcvenm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ?
                                   0 : dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFmallowm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ?
                                   0 : dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFDailyallm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otallow)", "")) ?
                                   0 : dt.Compute("sum(otallow)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFpfundm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfund)", "")) ?
                                   0 : dt.Compute("sum(pfund)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFgsalm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsal)", "")) ?
                                   0 : dt.Compute("sum(gsal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFgsal1m")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gsal1)", "")) ?
                                   0 : dt.Compute("sum(gsal1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFbonamtm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bonamt)", "")) ?
                                  0 : dt.Compute("sum(bonamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFitaxm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(itax)", "")) ?
                                   0 : dt.Compute("sum(itax)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFcashamtm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ?
                                   0 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvsalmonthly.FooterRow.FindControl("lgvFbankamtm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ?
                                   0 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;
            }

            //this.gvati.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvati.DataSource = dt;
            //this.gvati.DataBind();
            //this.ddlEmplist.DataTextField = "empname";
            //this.ddlEmplist.DataValueField = "empid";
            //this.ddlEmplist.DataSource = dt;
            //this.ddlEmplist.DataBind();

        }

        private void ShowSal()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");


            var ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL01", "PAYROLL_DETAIL06", frmdate, todate, projectcode, section, CompanyName, "", "", "", "");

            if (ds3 == null)
            {
                this.gvempsumm.DataSource = null;
                this.gvempsumm.DataBind();
                return;

            }

            //  DataTable dt = HiddenSameData(ds3.Tables[0]);
            Session["tblpay"] = ds3.Tables[0];

            this.Data_Bind();

        }


        private void SalSummMonth ()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string CompanyName = (this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
            // string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString();// this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString();
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            string empid = (this.ddlEmplist.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmplist.SelectedValue.ToString() + "%";
            string empname = this.ddlEmplist.SelectedItem.ToString();
            var ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETSALARYMONTHWISE", frmdate, todate, projectcode, section, CompanyName, empid, "", "", "");
            if (ds3.Tables[0].Rows.Count == 0)
            {
                this.gvsalmonthly.DataSource = null;
                this.gvsalmonthly.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger('" + empname + "');", true);
                return;

            }
            Session["tblpay"] = ds3.Tables[0];
            //ViewState["taxinf"] = ds3.Tables[1];

            this.Data_Bind();

        }

        private void ShowATI()
        {
            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string CompanyName = (this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompany.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
           // string projectcode = this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString();// this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlSection.SelectedValue.ToString();
            string monthid = Convert.ToDateTime(this.txttodate.Text).ToString("yyyyMM").ToString();
            string dt1 = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string curdate = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            string empid = (this.ddlEmplist.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmplist.SelectedValue.ToString() + "%";
            string empname = this.ddlEmplist.SelectedItem.ToString();
            string chkwoutait = this.ChkAllWithwithout.Checked ? "Length" : "";

            var ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETAITPURSALARY", frmdate, todate, projectcode, section, CompanyName, empid, chkwoutait, "", "");
            if (ds3.Tables[0].Rows.Count == 0)
            {
                this.gvati.DataSource = null;
                this.gvati.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "FnDanger('" + empname + "');", true);
                return;

            }
            Session["tblpay"] = ds3.Tables[0];
            ViewState["taxinf"] = ds3.Tables[1];

            this.Data_Bind();

        }
        private void GetEmployeeName()
        {

            string comcod = this.GetCompCode();
            string pactcode = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string dept = this.ddlProjectName.SelectedValue.ToString();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", "%", pactcode, "%", dept, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dt1 = new DataTable();
            dt1 = ds1.Tables[0].Copy();

            dt1 = dt1.DefaultView.ToTable(true, "empid", "empname");
            dt1.Rows.Add("000000000000", "ALL");

            this.ddlEmplist.DataTextField = "empname";
            this.ddlEmplist.DataValueField = "empid";
            this.ddlEmplist.DataSource = dt1;
            this.ddlEmplist.DataBind();
            this.ddlEmplist.SelectedValue = "000000000000";

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

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            if (type == "salati")
            {
                if (this.CheckAitPrint.Checked == true)
                {
                    this.PrintEMPAITcertificate();
                }
                else
                {
                    this.PrintAIT();
                }

            }

            else if (type == "salsumMonth")
            {
                this.PrintSummaryMonthWise();
            }
            else
            {
                this.PrintSallary();
            }
        }


        protected void PrintSallary()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            DataTable dt = (DataTable)Session["tblpay"];
            if (dt == null)
                return;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lstsum = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EmpMonthSummary>();

            LocalReport rpt1 = new LocalReport();
            rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpMonthSumm", lstsum, null, null);
            rpt1.EnableExternalImages = true;

            rpt1.SetParameters(new ReportParameter("Comname", comnam));
            rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "Monthly Attendance Statement"));
            rpt1.SetParameters(new ReportParameter("date1", ""));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintSummaryMonthWise()
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblpay"];
            if (dt == null)
                return;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lstsum = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>();

            LocalReport rpt1 = new LocalReport();                 
            rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_89_Pay.RptAitPurpose", lstsum, null, null);
            rpt1.EnableExternalImages = true;      
            rpt1.SetParameters(new ReportParameter("Comname", comnam));
            rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "AIT Purpose Salary Statement"));

            //  rpt1.SetParameters(new ReportParameter("date1", ""));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void PrintAIT()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblpay"];
            if (dt == null)
                return;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lstsum = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>();

            LocalReport rpt1 = new LocalReport();

            if (comcod == "3101" || comcod == "3347")
            {

                rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_89_Pay.RptAitPurposePeb", lstsum, null, null);
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("dateRange", "From  : " + frmdate + " To " + todate));

            }

            else
            {
                rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_89_Pay.RptAitPurpose", lstsum, null, null);
                rpt1.EnableExternalImages = true;

            }
           

            rpt1.SetParameters(new ReportParameter("Comname", comnam));
            rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "AIT Purpose Salary Statement"));
            
            //  rpt1.SetParameters(new ReportParameter("date1", ""));

            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintEMPAITcertificate()
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy");
            string empid = this.ddlEmplist.SelectedValue.ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yyyy");
            string daterang = fromdate + " To " + todate;
            DataTable dt = (DataTable)Session["tblpay"];
            DataTable dttax = (DataTable)ViewState["taxinf"];
            if (dt == null)
                return;
            string bankinfo = this.TxBtnInf.Text.ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lstsum = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>();
            var lst2 = dttax.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.aitpurpose>();
            var list = lstsum.FindAll(x => x.empid == empid);
            var taxlist = lst2.FindAll(x => x.empid == empid);
            LocalReport rpt1 = new LocalReport();
            rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpAitCertificate", list, taxlist, null);
            rpt1.EnableExternalImages = true;
            double total = Convert.ToDouble(list[0].bsal) + Convert.ToDouble(list[0].mallow) + Convert.ToDouble(list[0].incent) + Convert.ToDouble(list[0].adv) + Convert.ToDouble(list[0].hrent) + Convert.ToDouble(list[0].cven);
            string inwords = "In Words: (" + ASTUtility.Trans(total, 2) + ")";
            double taxamt = Convert.ToDouble(taxlist.Sum(item => item.itax));
            string incometax = "( TK." + ASTUtility.Trans(taxamt, 2) + ")";
            rpt1.SetParameters(new ReportParameter("Comname", comnam));
            rpt1.SetParameters(new ReportParameter("comaddress", comadd));
            rpt1.SetParameters(new ReportParameter("rpttitle", "TO WHOM IT MAY CONCERN"));
            rpt1.SetParameters(new ReportParameter("empname", list[0].empname.ToString()));
            rpt1.SetParameters(new ReportParameter("designation", list[0].desig.ToString()));
            rpt1.SetParameters(new ReportParameter("date", "Date:" + printdate));
            rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            rpt1.SetParameters(new ReportParameter("inwords", inwords));
            rpt1.SetParameters(new ReportParameter("taxamt", taxamt.ToString()));
            rpt1.SetParameters(new ReportParameter("incometax", incometax));
            rpt1.SetParameters(new ReportParameter("daterang", daterang));
            rpt1.SetParameters(new ReportParameter("bankinfo", bankinfo));
            rpt1.SetParameters(new ReportParameter("author", "MR. XXX" + System.Environment.NewLine + "AGM (Finance & Accounts)"));


            Session["Report1"] = rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}