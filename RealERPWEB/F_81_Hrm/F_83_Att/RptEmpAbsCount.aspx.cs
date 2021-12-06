﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpAbsCount : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE ABSENT COUNT LIST";
                DateTime curdate = System.DateTime.Today;
                DateTime frmdate = Convert.ToDateTime("01" + curdate.ToString("dd-MMM-yyyy").Substring(2));
                DateTime todate = Convert.ToDateTime(frmdate.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy"));
                this.txtfodate.Text = frmdate.ToString("dd-MMM-yyyy");
                this.txttodate.Text = todate.ToString("dd-MMM-yyyy");
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
            //this.ddlyearmon.DataTextField = "yearmon";
            //this.ddlyearmon.DataValueField = "ymon";
            //this.ddlyearmon.DataSource = ds1.Tables[0];

            //this.ddlyearmon.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            //this.ddlyearmon.DataBind();
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
            string txtCompany =  "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlcomp.DataTextField = "actdesc";
            this.ddlcomp.DataValueField = "actcode";
            this.ddlcomp.DataSource = ds1.Tables[0];
            this.ddlcomp.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlcomp_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }

        protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            if (this.ddlcomp.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlcomp.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string txtCompanyname = (this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            // string txtCompanyname =(this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) =="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept =  "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddldept.DataTextField = "actdesc";
            this.ddldept.DataValueField = "actcode";
            this.ddldept.DataSource = ds1.Tables[0];
            this.ddldept.DataBind();
            this.ddldept_SelectedIndexChanged(null, null);
        }

        protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }

        private void SectionName()
        {

            string comcod = this.GetComeCode();
            string projectcode = this.ddldept.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlsec.DataTextField = "sectionname";
            this.ddlsec.DataValueField = "section";
            this.ddlsec.DataSource = ds2.Tables[0];
            this.ddlsec.DataBind();

        }
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        private void GetEmpName()
        {
            string comcod = this.GetComeCode();
            string ProjectCode = this.ddlsec.SelectedValue.ToString() + "%";
            string txtSProject = "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPAYSLIPEMPNAMEALL", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmp.DataTextField = "empname";
            this.ddlEmp.DataValueField = "empid";
            this.ddlEmp.DataSource = ds5.Tables[0];
            this.ddlEmp.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];

        }

        private void ShowAbsCount()
        {
            Session.Remove("tblabscount");
            string comcod = this.GetComeCode();
            string fromdate = this.txtfodate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlcomp.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            //string compname = (this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddldept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddldept.SelectedValue.ToString() + "%";
            string section = (this.ddlsec.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlsec.SelectedValue.ToString() + "%";
            string Empcode = (this.ddlEmp.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmp.SelectedValue.ToString() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPABSCOUNTINFO", Empcode, fromdate, tdate, deptname, section);
            if (ds2 == null)
            {
                this.gvabscount.DataSource = null;
                this.gvabscount.DataBind();
                return;
            }
            Session["tblabscount"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowAbsCount();
        }
        protected void gvabscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvabscount.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblabscount"];
            this.gvabscount.DataSource = dt;
            this.gvabscount.DataBind();
            this.FooterCal();
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblabscount"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvabscount.FooterRow.FindControl("lgvFabsday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(abscount)", "")) ? 0.00
                        : dt.Compute("sum(abscount)", ""))).ToString("#,##0;(#,##0); ");


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string deptcode;

            int j;

            secid = dt1.Rows[0]["secid"].ToString();
            deptcode = dt1.Rows[0]["deptcode"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptname"] = "";
                }

                else
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                }

            }

            return dt1;

        }

        protected void lbltotalabs_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = ((Label)this.gvabscount.Rows[index].FindControl("lgvEmpId")).Text.ToString();
            string comcod = this.GetComeCode();
            string fromdate = this.txtfodate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPLOYEEABSENTDETAILSCOUNT", empid, fromdate, todate);
            if (ds2 == null)
            {
                this.GvEmpDetails.DataSource = null;
                this.GvEmpDetails.DataBind();
                return;
            }
            this.GvEmpDetails.DataSource = this.HiddenSameDataDetails(ds2.Tables[0]);
            this.GvEmpDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalDeails();", true);
            
        }


        private DataTable HiddenSameDataDetails(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string deptcode;
            string design;
            string empid;

            int j;

            secid = dt1.Rows[0]["secid"].ToString();
            deptcode = dt1.Rows[0]["deptcode"].ToString();
            empid = dt1.Rows[0]["empid"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptname"] = "";
                }

                else
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                }

                if (dt1.Rows[j]["empid"].ToString() == empid)
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                    dt1.Rows[j]["empname"] = "";
                    dt1.Rows[j]["desig"] = "";
                }

                else
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                }

            }

            return dt1;

        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvabscount.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.Data_Bind();

        }
    }
}