using RealERPLIB;
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
    public partial class AttnOutOfOffice : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userrole = hst["userrole"].ToString();
                if(userrole !="3")
                {
                    this.GetCompany();
                    this.topPanle.Visible = true;
                }
                this.txtfromdate.Text= System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            }
        }
        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }
        private void GetDptName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDpt.DataTextField = "actdesc";
            this.ddlDpt.DataValueField = "actcode";
            this.ddlDpt.DataSource = ds1.Tables[0];
            this.ddlDpt.DataBind();
            this.ddlDpt_SelectedIndexChanged(null, null);

        }

        private void SectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlDpt.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            this.GetEmpName();

        }

        private void GetEmpName()
        {
            string comcod = this.GetCompCode();
            string ProjectCode = (this.ddlSection.SelectedValue.Trim().Length > 0) ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtSProject = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPAYSLIPEMPNAMEALL", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void getEmpId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();



        }
        protected void btnSaveAttn_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = GetCompCode();
            string usrid = hst["usrid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = this.txtfromdate.Text;
            string userrole= hst["userrole"].ToString();
            string empid = (userrole=="3"? hst["empid"].ToString():this.ddlEmpNameAllInfo.SelectedValue.ToString());
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTOUTOFOFFICEATTENDANCE", usrid, Sessionid, Date, empid, "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                
                string eventtype = "999";
                string eventdesc = this.txtNote.Text.Trim();
                string eventdesc2 = empid;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Save Successfully" + "');", true);
            }

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDptName();
        }

        protected void ddlDpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SectionName();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}