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
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class HREmpConfirmation : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                 
                DataSet datSetup = compUtility.GetCompUtility();
                if (datSetup == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                    return;
                }
                string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
                this.txtfrmdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = startdate + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");





                this.GetCompany();
                this.GetDepartName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE CONFIRM INFORMATION";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                //string confrm = this.Request.QueryString["chk"].ToString();
                //if (confrm == "confirm")
                //{
                //    this.chkconfrmdt.Checked = true;
                //}

                this.lnkbtnShow_Click(null, null);
            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0]; ;
            this.ddlCompany_SelectedIndexChanged(null, null);

        }

        private void GetDepartName()
        {
            string comcod = this.GetComeCode();
            string company = this.ddlCompany.SelectedValue.Substring(0, 2).ToString();
            string txtDeptname = this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETPROJECTNAME", company, txtDeptname, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            try
            {
                Session.Remove("tblMrr");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
                string DeptCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9) + "%";
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string cnfrm = this.chkconfrmdt.Checked ? "confirm" : "";

                DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "EMPCONFIRM", company, DeptCode, frmdate, todate, cnfrm, "", "", "", "");
                if (ds1 == null)
                {
                    this.dgvEmpCon.DataSource = null;
                    this.dgvEmpCon.DataBind();
                    return;
                }
                Session["tblMrr"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error :" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        private void Data_Bind()
        {
            if (this.chkconfrmdt.Checked)
            {
                this.dgvEmpCon.Columns[8].Visible = false;
                this.dgvEmpCon.Columns[9].Visible = false;

            }


            this.dgvEmpCon.DataSource = (DataTable)Session["tblMrr"];
            this.dgvEmpCon.DataBind();
            DataTable dt1 = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgvEmpCon.Rows.Count; i++)
            {
                string empid = ((Label)dgvEmpCon.Rows[i].FindControl("lgvEmID")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)dgvEmpCon.Rows[i].FindControl("lbok");
                if (lbtn1 != null)
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = empid;
            }



        }
        private void CheckValue()
        {
            DataTable dt = (DataTable)Session["tblMrr"];
            for (int i = 0; i < this.dgvEmpCon.Rows.Count; i++)
            {
                string chkmr = (((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked) ? "True" : "False";
                dt.Rows[i]["chkmv"] = chkmr;
                ((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Enabled = (((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
                ((LinkButton)this.dgvEmpCon.Rows[i].FindControl("lbok")).Enabled = (((CheckBox)this.dgvEmpCon.Rows[i].FindControl("chkvmrno")).Checked) ? false : true;
            }
            Session["tblMrr"] = dt;

        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartName();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lbok_Click(object sender, EventArgs e)
        {
            this.CheckValue();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string postDat = System.DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string empid = code.Substring(0, 12).ToString();
            DataTable dt = (DataTable)Session["tblMrr"];
            DataRow[] dr = dt.Select(" empid='" + empid + "' ");
            string id = dr[0]["empid"].ToString();
            string Remarks = dr[0]["remarks"].ToString();
            string Chk = dr[0]["chkmv"].ToString();
            if (Chk == "False")
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Please Check CheckBox";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            try
            {

                bool resultpa = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPCONFIRM", id, Remarks, userid, postDat, trmid, sessionid,
                                "", "", "", "", "", "", "", "", "");

                if (!resultpa)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = HRData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }

             ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully.";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
    }
}