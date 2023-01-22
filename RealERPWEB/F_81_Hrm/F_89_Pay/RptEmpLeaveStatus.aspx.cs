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
    public partial class RptEmpLeaveStatus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        int curd;
        int frdate;
        int mon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee Salary Sheet and Leave Status";

                this.GetCompany();
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01-Jan" + (this.txtfromdate.Text.Trim()).Substring(6)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RptEmpSalAndLeaveStatus();
        }
        private void RptEmpSalAndLeaveStatus()
        {

            DataTable dt = (DataTable)Session["tblEmpLevSta"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_89_Pay.SalarySheet.RptSalarySheet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_89_Pay.RptEmpLeaveStatus", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", this.ddlCompany.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Salary Sheet And Leave Status"));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }

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

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.showData();
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

        private void showData()
        {
            Session.Remove("tblEmpLevSta");
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string projectcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string CompanyName = (this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "SALARYANDLEAVESTATUS", frmdate, todate, projectcode, section, CompanyName, "", "", "", "");

            DataTable dt = HiddenSameData(ds.Tables[0]);
            Session["tblEmpLevSta"] = dt;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblEmpLevSta"];
            this.gvEmpSalLevSta.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpSalLevSta.DataSource = dt;
            this.gvEmpSalLevSta.DataBind();
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string refno = dt1.Rows[0]["refno1"].ToString();
            string section = dt1.Rows[0]["section"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["refno1"].ToString() == refno && dt1.Rows[j]["section"].ToString() == section)
                {
                    refno = dt1.Rows[j]["refno1"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["refdesc"] = "";
                    dt1.Rows[j]["sectionname"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["refno1"].ToString() == refno)
                    {
                        dt1.Rows[j]["refdesc"] = "";
                    }

                    if (dt1.Rows[j]["section"].ToString() == section)
                    {
                        dt1.Rows[j]["sectionname"] = "";

                    }
                    refno = dt1.Rows[j]["refno1"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                }

            }

            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Data_Bind();
        }
        protected void gvEmpSalLevSta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpSalLevSta.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}




