using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptMonthlyAttnSummary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                this.GetCompany();
                this.SelectType();
                this.SelectDate();
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void SelectDate()
        {
            string comcod = this.GetCompCode();
            DataSet datSetup = compUtility.GetCompUtility();
            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "26" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            if (datSetup == null)
                return;
            switch (comcod)
            {
                case "3330":
                case "3355":
                case "3365":
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpWise":
                    this.PrintMonAttnSumEmpWise();
                    break;

                default:
                    this.PrintMonAttnSumEmpWise();
                    break;

            }
        }
        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                default:
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }
        }       

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();
            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            string comcod = this.GetCompCode();
            string txtCompanyname = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetSectionName();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
        }
        private void GetSectionName()
        {
            string comcod = this.GetCompCode();
            string deptcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", deptcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "EmpWise":
                    if (this.lnkbtnShow.Text == "New")
                    {
                        this.lnkbtnShow.Text = "Ok";
                        this.gvAttnSumEmpWise.DataSource = null;
                        this.gvAttnSumEmpWise.DataBind();
                        return;

                    }

                    this.lnkbtnShow.Text = "New";
                    this.ShowData();
                    break;
            }

        }

        public void ShowData()
        {
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Dept = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE_SUMMARY", "MONTHLY_ATTEN_SUMMARY", fromdate, todate, Company, Dept, section);
            if (ds2 == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('No Data Found');", true);
                return;
            }

            ViewState["tblattenmon"] = ds2.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblattenmon"];
                string type = this.Request.QueryString["Type"].ToString().Trim();
                switch (type)
                {
                    case "EmpWise":
                        this.gvAttnSumEmpWise.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
                        this.gvAttnSumEmpWise.DataSource = dt;
                        this.gvAttnSumEmpWise.DataBind();
                        break;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }

        }

        protected void gvAttnSumEmpWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAttnSumEmpWise.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        private void PrintMonAttnSumEmpWise()
        {
            DataTable dt = (DataTable)ViewState["tblattenmon"];
            if (dt == null)
            {
                Response.Write("<script>alert('Please Click OK of the Page and Press Print to continue!');</script>");
            }
            else
            {
                string comcod = this.GetCompCode();
                string date = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM dd, yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM dd, yyyy");
                string empType = this.ddlCompany.SelectedItem.ToString();
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd"].ToString().Replace("<br />", "\n");
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string monthof = Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yyyy");
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptMonAttnSumEmpWise>().ToList();
                string rptTitle = "Personnel Wise Attendance Summary";
                date = "From Date: " + date + " To Date: " + todate;
                LocalReport rpt1 = new LocalReport();
                rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptMonAttnSumEmpWise", lst, null, null);
                rpt1.EnableExternalImages = true;
                rpt1.SetParameters(new ReportParameter("comnam", comnam));
                rpt1.SetParameters(new ReportParameter("comadd", comadd));
                rpt1.SetParameters(new ReportParameter("rptTitle", rptTitle));
                rpt1.SetParameters(new ReportParameter("date", date));
                rpt1.SetParameters(new ReportParameter("monthof", "Month of "+monthof));
                rpt1.SetParameters(new ReportParameter("footer", ASTUtility.Concat(compname, username, printdate)));
                rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
        }
    }
}