using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System.Data;
using System.Configuration;
using System.Collections;

namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class RptSalaryEncashment : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Salary Encashment";
                this.GetCompName();
                this.GetYearMonth();
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];

            this.ddlyearmon.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
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
        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }

        private void SaveValue()
        {

           
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
           



        }
    }
        
}