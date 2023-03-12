﻿//using Microsoft.Reporting.WebForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_21_MKT
{
    public partial class RptMissFollowup : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                if (this.ddlEmployeeName.Items.Count == 0)
                {
                    this.GetEmployeeName();
                }

                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void Data_Bind()
        {
            DataTable dt = (DataTable)Session["MissFollowUpData"];
            this.grvMissFollowUp.PageSize = 10;
            this.grvMissFollowUp.DataSource = dt;
            this.grvMissFollowUp.DataBind();
        }
        private void GetEmployeeName()
        {
            string comcod = GetComeCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI_ENTRY", "GETEMPLOYEE", "%");
            this.ddlEmployeeName.DataTextField = "empname";
            this.ddlEmployeeName.DataValueField = "empid";
            this.ddlEmployeeName.DataSource = ds1.Tables[0];
            this.ddlEmployeeName.DataBind();
            ds1.Dispose();
        }
        protected void grvMissFollowUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvMissFollowUp.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = GetComeCode();
            string proscode = "8301%";
            string empid = this.ddlEmployeeName.SelectedValue.ToString();
            string date = this.txttodate.Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI_ENTRY", "RPTMISFOLLOWUP", proscode, empid, "93%", date);

            Session["MissFollowUpData"] = ds1.Tables[0];
            ds1.Dispose();
            this.Data_Bind();
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["MissFollowUpData"];

            LocalReport Rpt1 = new LocalReport();

            var lst = dt.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.RptMissFollowup>();

            //Running
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.RptMissFollowup", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Missing Followup Status"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "&embedded=true', target='_blank');</script>";
        }
    }
}