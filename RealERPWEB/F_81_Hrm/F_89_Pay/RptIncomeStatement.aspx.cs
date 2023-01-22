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
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptIncomeStatement : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee Income Statement Month Wise";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetCompanyName();
                this.GetDate();
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

            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //this.txtfromdate.Text = startdate + date.Substring(2);
            //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            this.GetDepartment();
            this.ddlCompanyAgg_SelectedIndexChanged(null, null);
        }
        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            //   string type = this.Request.QueryString["Type"].ToString().Trim();
            string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";

            string txtSProject = this.txtsrchdeptagg.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string deptcode = this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 4) + "%";
            string txtSProject = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetEmpName();
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetEmpName()
        {
            string comcod = this.GetComeCode();
            string ProjectCode = (this.txtEmpSrcInfo.Text.Trim().Length > 0) ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string txtSProject = "%" + this.txtEmpSrcInfo.Text + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];
            this.GetComASecSelected();
        }

        private void GetComASecSelected()
        {
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();
            }

        }

        protected void ibtnFindCompanyAgg_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }

        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
            //this.ddlProjectName_SelectedIndexChanged(null,null);
        }

        protected void lbtndeptagg_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
            // this.GetDepartment();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ddlProjectName_SelectedIndexChanged(null, null);
            //this.ddlProjectName_SelectedIndexChanged(null, null);
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();

        }
        protected void ibtnEmpListAllinfo_Click(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();
        }
        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "EMPINDINCOMESTATEMENT", empid, frmdate, todate, "", "", "", "", "", "");
            if (ds == null)
                return;
            Session["tblempsalinfo"] = ds.Tables[0];
            Session["tblempdescinfo"] = ds.Tables[1];

            this.gvpayinfo.DataSource = ds.Tables[0];
            this.gvpayinfo.DataBind();
            this.FooterCalCulation(ds.Tables[0]);

        }
        private void FooterCalCulation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvpayinfo.FooterRow.FindControl("gvFbank")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt)", "")) ? 0.00 : dt.Compute("sum(bankamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvpayinfo.FooterRow.FindControl("gvFbank2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankamt2)", "")) ? 0.00 : dt.Compute("sum(bankamt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvpayinfo.FooterRow.FindControl("gvFcashamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ? 0.00 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvpayinfo.FooterRow.FindControl("gvFtosal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totalsal)", "")) ? 0.00 : dt.Compute("sum(totalsal)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void txtsrchdeptagg_TextChanged(object sender, EventArgs e)
        {

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable empsalary = (DataTable)Session["tblempsalinfo"];
            DataTable empinfo = (DataTable)Session["tblempdescinfo"];
            var emplist = empsalary.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.Incomesalary>();
            var empdesc = empinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.Empinfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptIncomeSatatement", emplist, null, null);
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empdesig", empinfo.Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptname", "Income Statement"));
            Rpt1.SetParameters(new ReportParameter("dept", empinfo.Rows[0]["dept"].ToString()));
            Rpt1.SetParameters(new ReportParameter("date", "From : " + txtfromdate.Text + " To " + txttodate.Text));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}