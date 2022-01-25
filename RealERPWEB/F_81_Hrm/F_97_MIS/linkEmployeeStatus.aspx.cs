using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_97_MIS
{
    public partial class linkEmployeeStatus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFdate.Text = "01" + date.Substring(2);
                this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompany();
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();             
            return comcod;
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();

            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);


        }
        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";

            string txtCompanyname = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetSectionName();
        }
        private void GetSectionName()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
             
        }
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}