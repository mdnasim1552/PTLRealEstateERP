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
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{


    public partial class RptOvertimeSalary2 : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Overtime salary Sheet ";
                this.GetCompany();
                this.visibilityBracnh();

                this.GetDate();

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
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            //string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //this.txtfromdate.Text = startdate + date.Substring(2);
            //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
           
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
            
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETDPTLIST_NEW", branch, userid, "", "", "", "", "", "", "");

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }

        private void SectionName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();            
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";   
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETSECTION_LIST", projectcode, userid, "", "", "", "", "", "", "");
          
            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }
        private string GetOverTimeafter10()
        {

            string comcod = this.GetCompCode();
            string overtimeafter10 = "";
            switch (comcod)
            {
                case "3348": //Credence
                case "3101":
                    overtimeafter10 = "acter10hour";
                    break;

                default:
                    break;
            }

            return overtimeafter10;



        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string section = this.ddlSection.SelectedValue.ToString();
            string CompanyName = this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string overtimeafter10 = this.GetOverTimeafter10();

            string calltype = (comcod == "3368" ? "OVERTIMESALARY_FINLAY" : "OVERTIMESALARY");


            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", calltype, frmdate, todate, projectcode, section, CompanyName, overtimeafter10, "", "", "");
            Session["tblOvertime"] = this.HiddenSameData(ds.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string refno = dt1.Rows[0]["refno"].ToString();

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
            return dt1;

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblOvertime"];
            this.gvovertime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvovertime.DataSource = dt;
            this.gvovertime.DataBind();
            this.FooterCal();
        }

        private void FooterCal()
        {


            DataTable dt = (DataTable)Session["tblOvertime"];

            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvovertime.FooterRow.FindControl("lgvFgssal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(gssal)", "")) ? 0.00 :
                dt.Compute("sum(gssal)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFbSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bsal)", "")) ? 0.00 :
                dt.Compute("sum(bsal)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFhrent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(hrent)", "")) ? 0.00 :
               dt.Compute("sum(hrent)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFdailyAllow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(otall)", "")) ? 0.00 :
               dt.Compute("sum(otall)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cven)", "")) ? 0.00 :
               dt.Compute("sum(cven)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFmallow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mallow)", "")) ? 0.00 :
               dt.Compute("sum(mallow)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFOvertime")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ovthour)", "")) ? 0.00 :
               dt.Compute("sum(ovthour)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvovertime.FooterRow.FindControl("lgvFOvtamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ovtamt)", "")) ? 0.00 :
               dt.Compute("sum(ovtamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string todate1 = Convert.ToDateTime(this.txttodate.Text).ToString("MMM, yyyy");
            string subtitle = "Overtime Salary Sheet for the month of " + todate1;
            string userinf = ASTUtility.Concat(comname, username, session, printdate);
            DataTable dt1 = (DataTable)Session["tblOvertime"];

            var list = dt1.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.EmpMonthSummary>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_81_Hrm.R_89_Pay.Overtime", list, null, null);



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

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
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

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void gvovertime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvovertime.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void GetEmployeeOverTime(string empid)
        {


            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string overtimeafter10 = this.GetOverTimeafter10();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "RPTINDEMPOVERTIME", frmdate, todate, empid, overtimeafter10, "", "", "", "", "");
            DataTable dt = this.HiddenSameDatadet(ds1.Tables[0]);
            this.mgvbreakdown.DataSource = dt;
            this.mgvbreakdown.DataBind();

            int overmin = Convert.ToInt32((Convert.IsDBNull(dt.Compute("sum(ovmin)", "")) ? 0.00 : dt.Compute("sum(ovmin)", "")));
            int ovhour = (int)(overmin / 60);
            int rovermin = (int)(overmin % 60);
            rovermin = rovermin > 30 ? 1 : 0;
            ovhour = ovhour + rovermin;

            ((Label)this.mgvbreakdown.FooterRow.FindControl("mlgvFDelday")).Text = Convert.ToDouble(ovhour).ToString("#,##0;(#,##0); ");


            ds1.Dispose();

        }


        private DataTable HiddenSameDatadet(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string grp = dt1.Rows[0]["grp"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    grp = dr1["grp"].ToString();
                    i++;
                    continue;
                }

                if (dr1["grp"].ToString() == grp)
                {

                    dr1["grpdesc"] = "";

                }


                grp = dr1["grp"].ToString();
            }



            return dt1;

        }

        protected void lbtngvSPday_Click(object sender, EventArgs e)
        {

            this.lbmodalheading.Text = "Individual Monthly Over Time Details Information";
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int rownumber = this.gvovertime.PageSize * this.gvovertime.PageIndex + RowIndex;
            string empid = ((DataTable)Session["tblOvertime"]).Rows[RowIndex]["empid"].ToString();

            this.GetEmployeeOverTime(empid);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);
        }
    }
}