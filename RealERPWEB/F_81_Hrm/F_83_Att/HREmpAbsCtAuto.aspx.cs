﻿using System;
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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{

    public partial class HREmpAbsCtAuto : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                // this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.lblfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Abscount Count (Automatic)";



                //  this.ViewVisibility();
                this.GetCompName();
                this.GetYearMonth();

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

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


        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            this.ShowAbsCount();
            //if (this.lnkbtnShow.Text == "Ok")
            //{
            //    //this.lnkbtnShow.Text = "New";
            //   // this.ddlyearmon.Enabled = false;
            //    //this.ddlCompanyName.Visible = false;
            //    //this.ddlDepartment.Visible = false;
            //   // this.ddlSection.Enabled = false;

            //    //this.lblCompanyName.Visible = true;
            //    //this.lblDeptDesc.Visible = true;

            //    //this.lblPage.Visible = true;
            //    //this.ddlpagesize.Visible = true;

            //    this.lblCompanyName.Text = this.ddlCompanyName.SelectedItem.Text;
            //    this.lblDeptDesc.Text = this.ddlDepartment.SelectedItem.Text;

            //    return;
            //}

            //this.lnkbtnShow.Text = "Ok";
            //this.lblCompanyName.Text = "";
            //this.ddlyearmon.Enabled = false;
            //this.ddlCompanyName.Visible = true;
            //this.ddlDepartment.Visible = true;
            //this.ddlSection.Enabled = true;

            //this.lblCompanyName.Visible = false;
            //this.lblDeptDesc.Visible = false;
            ////this.lblPage.Visible = false;
            ////this.ddlpagesize.Visible = false;
            //this.gvabscount.DataSource = null;
            //this.gvabscount.DataBind();




        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;

            int j;

            secid = dt1.Rows[0]["secid"].ToString();
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

            }

            return dt1;

        }

        private void ShowAbsCount()
        {
            Session.Remove("tblabscount");
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string MonthId = this.ddlyearmon.Text.Trim();
            string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode = this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPMANUALABSENTS", compname, MonthId, date, deptname, Empcode, section, "", "", "");
            if (ds2 == null)
            {
                this.gvabscount.DataSource = null;
                this.gvabscount.DataBind();
                return;
            }
            Session["tblabscount"] = this.HiddenSameData(ds2.Tables[0]);
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



            ((Label)this.gvabscount.FooterRow.FindControl("lgvFabsday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(absday)", "")) ? 0.00
                        : dt.Compute("sum(absday)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            this.GetCompName();

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


        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
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

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }


        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
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


        protected void gvabscount_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowAbsCount();

        }
        private void SaveValue()
        {

            DataTable dt = (DataTable)Session["tblabscount"];
            int rowindex;

            for (int i = 0; i < this.gvabscount.Rows.Count; i++)
            {

                double absday = Convert.ToDouble("0" + ((TextBox)this.gvabscount.Rows[i].FindControl("txtabsday")).Text.Trim());
                rowindex = (this.gvabscount.PageSize) * (this.gvabscount.PageIndex) + i;
                dt.Rows[rowindex]["absday"] = absday;
            }

            Session["tblabscount"] = dt;
        }
        protected void lbntUpdateAbs_Click(object sender, EventArgs e)
        {
            this.Master.FindControl("lblmsg").Visible = true;
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblabscount"];

            //log Entry

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = userid;
            string Posttrmid = Terminal;
            string Posteddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string Monthid = this.ddlyearmon.Text.Trim();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString();
                double absday = Convert.ToDouble(dt.Rows[i]["absday"]);

                if (absday > 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTORUPDATEABSAUTO", Monthid, empid, absday.ToString(), PostedByid, Posttrmid, Posteddat, "", "", "", "", "", "", "", "", "");
                    if (!result)
                        return;

                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }

        protected void lbtnabscountdelete_Click(object sender, EventArgs e)
        {

            int rownum = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblabscount"];
            string Monthid = this.ddlyearmon.Text.Trim();
            string empid = ((Label)this.gvabscount.Rows[rownum].FindControl("lgvEmpId")).Text.Trim();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "DELETEABSENTAUTO",
                       Monthid, empid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[rownum].Delete();
            }

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Fail !!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }




            DataView dv = dt.DefaultView;
            Session.Remove("tblabscount");
            Session["tblabscount"] = dv.ToTable();
            this.Data_Bind();

        }
        protected void gvabscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvabscount.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();


        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void chkactocopy_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void lnkautocopy_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnCalAbsCount_Click(object sender, EventArgs e)
        {

        }


        protected void lbtnTotalAbsCount_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }

    }
}