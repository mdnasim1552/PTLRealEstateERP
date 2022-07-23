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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpOfficeTimeSetup : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetCompany();
            }
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);
        }
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetSection();
        }
        private void GetSection()
        {
            string comcod = this.GetCompCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string DeptCode = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
            // string txtsection = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETSECTIONNAME", Company, DeptCode, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "actdesc";
            this.ddlSection.DataValueField = "actcode";
            this.ddlSection.DataSource = ds1.Tables[0];
            this.ddlSection.DataBind();
            ds1.Dispose();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnShow.Text == "Ok")
            {
                this.lnkbtnShow.Text = "New";
                this.ddlDepartment.Enabled = false;
                this.ddlSection.Enabled = false;
                this.ddlCompany.Enabled = false;
                this.pnlOfftime.Visible = true;
                this.ShowOffTime();
                return;
            }
            this.lnkbtnShow.Text = "Ok";          
            this.ddlDepartment.Enabled = true;
            this.ddlSection.Enabled = true;
            this.ddlCompany.Enabled = true;

            this.pnlOfftime.Visible = false;
        }

        private void ShowOffTime()
        {
            string comcod = this.GetCompCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETOFFTIME", "", "", "", "", "", "", "", "", "");
            if (ds5 == null)
                return;
            this.ddlOffintimedw.DataTextField = "offintime";
            this.ddlOffintimedw.DataValueField = "offinid";
            this.ddlOffintimedw.DataSource = ds5.Tables[0];
            this.ddlOffintimedw.DataBind();

            this.ddlOffouttimedw.DataTextField = "offouttime";
            this.ddlOffouttimedw.DataValueField = "offoutid";
            this.ddlOffouttimedw.DataSource = ds5.Tables[1];
            this.ddlOffouttimedw.DataBind();

            this.ddlLanintimedw.DataTextField = "lanintime";
            this.ddlLanintimedw.DataValueField = "laninid";
            this.ddlLanintimedw.DataSource = ds5.Tables[2];
            this.ddlLanintimedw.DataBind();

            this.ddlLanouttimedw.DataTextField = "lanouttime";
            this.ddlLanouttimedw.DataValueField = "lanoutid";
            this.ddlLanouttimedw.DataSource = ds5.Tables[3];
            this.ddlLanouttimedw.DataBind();
            ds5.Dispose();
        }
        protected void lnkbtnUpdateOfftime_Click(object sender, EventArgs e)
        {
            this.UpdateOffTime();
        }

        private void UpdateOffTime()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlSection.SelectedValue.ToString();
            string company = this.ddlCompany.SelectedValue.Trim().Substring(0, 2);
            string offinid = this.ddlOffintimedw.SelectedValue.ToString();
            string offintime = this.ddlOffintimedw.SelectedItem.Text;
            string offoutid = this.ddlOffouttimedw.SelectedValue.ToString();
            string offouttime = this.ddlOffouttimedw.SelectedItem.Text;
            string laninid = this.ddlLanintimedw.SelectedValue.ToString();
            string lanintime = this.ddlLanintimedw.SelectedItem.Text;
            string lanoutid = this.ddlLanouttimedw.SelectedValue.ToString();
            string lanouttime = this.ddlLanouttimedw.SelectedItem.Text;
            bool result;
            string msgSuccess = "Updated Successfully";
            string msgFailed = "Data Is Not Updated";
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPOFFTIME", projectcode, company, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgFailed + "');", true);
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, offinid, "01-Jan-1900 " + offintime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgFailed + "');", true);
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, offoutid, "01-Jan-1900 " + offouttime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgFailed + "');", true);
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, laninid, "01-Jan-1900 " + lanintime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgFailed + "');", true);
                return;
            }
            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "UPDATEHREMPOFFTIME", projectcode, lanoutid, "01-Jan-1900 " + lanouttime, company, "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgFailed + "');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msgSuccess + "');", true);
        }
    }
}