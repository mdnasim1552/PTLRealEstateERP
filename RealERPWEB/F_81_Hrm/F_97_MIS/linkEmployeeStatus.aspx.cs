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
                string qtype = this.Request.QueryString["Type"]??"";
                if(qtype== "late")
                {
                    this.fmdate.Visible = false;
                    this.txtFdate.Visible = false;
                    this.tdate.Visible = false;
                }

                this.GetCompany();
                this.lnkOk_Click(null, null);
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
            this.GetDepartment();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSectionName();
        }

        protected void lnkOk_Click(object sender, EventArgs e)
        {
            string qtype = this.Request.QueryString["Type"].ToString();

            switch (qtype)
            {
                case "late":
                    this.GetLateAttandInfo();
                    break;

                default:
                    break;
            }
        }

        private void GetLateAttandInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            string comcod = this.GetComCode();
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string PCompany = this.ddlCompany.SelectedItem.Text.Trim();
            string frmdate = Convert.ToDateTime(this.txtTdate.Text).ToString("dd-MMM-yyyy");
            string deptCode = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + "%";



            string section = "" ;
            //if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            //{

            //    string gp = this.DropCheck1.SelectedValue.Trim();
            //    if (gp.Length > 0)
            //    {
            //        if (gp.Substring(0, 4).Trim() == "0000" || gp.Trim() == "")
            //            section = "";
            //        else
            //            foreach (ListItem s1 in DropCheck1.Items)
            //            {
            //                if (s1.Selected)
            //                {
            //                    section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + s1.Value.Substring(0, 4);
            //                }

            //            }


            //    }

            //}

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDAILYLATEANDABSENT", frmdate, deptCode, Company, section, "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tbllatattinfo"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbllatattinfo"];
            string qtype = this.Request.QueryString["Type"].ToString();

            switch (qtype)
            {
                case "late":
                    this.gvDailyLateAttn.DataSource = dt;
                    this.gvDailyLateAttn.DataBind();
                    break;

                default:
                    break;
            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void gvDailyLateAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDailyLateAttn.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["section"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                    {
                        dt1.Rows[j]["section"] = "";

                    }

                    secid = dt1.Rows[j]["secid"].ToString();
                }

            }
            return dt1;

        }

      
    }
}