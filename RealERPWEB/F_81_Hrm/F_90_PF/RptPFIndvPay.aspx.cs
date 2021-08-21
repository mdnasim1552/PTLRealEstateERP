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
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_90_PF
{
    public partial class RptPFIndvPay : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Individual Payment Schedule of Provident Fund";


                //   this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetCompanyName();
            }
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
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "EMPINDPAY", empid, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;
            ViewState["tblempinfo"] = ds.Tables[1];
            ViewState["tblemppfinfo"] = ds.Tables[0];

            this.gvpayinfo.DataSource = ds.Tables[0];
            this.gvpayinfo.DataBind();
            this.FooterCalCulation(ds.Tables[0]);

        }
        private void FooterCalCulation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvpayinfo.FooterRow.FindControl("lbltotalpf")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pfamt)", "")) ? 0.00 : dt.Compute("sum(pfamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvpayinfo.FooterRow.FindControl("gvFcontribu")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(contribu)", "")) ? 0.00 : dt.Compute("sum(contribu)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvpayinfo.FooterRow.FindControl("gvFBalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balance)", "")) ? 0.00 : dt.Compute("sum(balance)", ""))).ToString("#,##0.00;(#,##0.00); ");

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
            DataTable Pfinfo = (DataTable)ViewState["tblemppfinfo"];
            DataTable empinfo = (DataTable)ViewState["tblempinfo"];
            var pflist = Pfinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.PaymentScheduleList>();
            var emplist = empinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.Empinfo>();

            if (comcod == "3333")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_90_PF.RptIndvPfAlli", pflist, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
                Rpt1.SetParameters(new ReportParameter("txtconpriod", Convert.ToDouble(empinfo.Rows[0]["years"]).ToString("#,##0;(#,##0") + "  Year  " + Convert.ToDouble(empinfo.Rows[0]["months"]).ToString("#,##0;(#,##0") + "  Month"));
            }


            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));
            Rpt1.SetParameters(new ReportParameter("empdesig", empinfo.Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("rptname", "Individual Payment Schedule of Provident Fund"));
            Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));
            Rpt1.SetParameters(new ReportParameter("pfend", empinfo.Rows[0]["pfend"].ToString()));
            Rpt1.SetParameters(new ReportParameter("dept", empinfo.Rows[0]["dept"].ToString()));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}