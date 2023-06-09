﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt

{
    public partial class PrintLeaveInterface : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ApplyPrint":
                    this.PrinEmpApplication();
                    break;

                default:
                    break;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        public void PrinEmpApplication()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string empid = this.Request.QueryString["empid"].ToString();
            string date = this.Request.QueryString["strtdat"].ToString();
            string ltrnid = this.Request.QueryString["LeaveId"].ToString();
            this.PreLeaveno();
            DataTable dt = (DataTable)ViewState["tblprelinf"];
            DataRow[] drp = dt.Select("ltrnid='" + ltrnid + "'");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_INTERFACE", "LEAVEPrintApplication", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }

            DataTable dt1 = ds1.Tables[1];
            DataTable dt2 = ds1.Tables[0];

            string gcod = drp[0]["gcod"].ToString();
            DataRow[] drl = dt1.Select("gcod='" + gcod + "'");
            DataRow[] drls = dt2.Select("gcod='" + gcod + "'");

            //leave-------
            drl[0]["lapplied"] = drp[0]["lapplied"];
            drl[0]["lenjoydt1"] = drp[0]["strtdat"];
            drl[0]["lenjoydt2"] = drp[0]["enddat"];
            //leave status-------
            double leaveday = Convert.ToDouble(drp[0]["lapplied"].ToString());
            double enjleave = Convert.ToDouble(drls[0]["ltaken"]);
            double Clsleave = Convert.ToDouble(drls[0]["pbal"]);
            string startdate = Convert.ToDateTime(drp[0]["strtdat"]).ToString("dd-MMM-yyyy");
            string  endate = Convert.ToDateTime(drp[0]["enddat"]).ToString("dd-MMM-yyyy");
            drls[0]["applyday"] = drp[0]["lapplied"];
            drls[0]["appday"] = drp[0]["lapplied"];
            drls[0]["applydate"] = drp[0]["strtdat"];
            drls[0]["appdate"] = drp[0]["strtdat"];
            drls[0]["balleave"] = Clsleave - leaveday;
            drls[0]["tltakreq"] = leaveday;

            Session["tblleave"] = dt1;
            Session["tblleavest"] = dt2;


            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("appday>0");
            DataTable dt3 = dv.ToTable();

            var list = dt3.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.EmpLeavApp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", this.Request.QueryString["LeaveId"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtleaveday", Convert.ToInt32(dt3.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days"));
            Rpt1.SetParameters(new ReportParameter("txtldatefrm", startdate));
            Rpt1.SetParameters(new ReportParameter("txtldateto", endate)); 
            Rpt1.SetParameters(new ReportParameter("txtlday", Convert.ToInt32(dt3.Rows[0]["appday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo1", this.Request.QueryString["LeaveId"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", ds1.Tables[2].Rows[0]["empname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtEmpName1", ds1.Tables[2].Rows[0]["empname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtDesig", ds1.Tables[2].Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtDesig1", ds1.Tables[2].Rows[0]["desig"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", Convert.ToDateTime(dt3.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtReasons", ds1.Tables[3].Rows[0]["lreason"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txttitlelappslip", "Leave Approval Slip"));
            Rpt1.SetParameters(new ReportParameter("txtAppDays", Convert.ToInt32(dt3.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days " + dt3.Rows[0]["gdesc"].ToString() + " from " + Convert.ToDateTime(dt3.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }

        private void PreLeaveno()
        {
            ViewState.Remove("tblprelinf");
            string comcod = this.GetCompCode();
            string empid = this.Request.QueryString["empid"].ToString();
            string date = this.Request.QueryString["strtdat"].ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "PREVIOUSLEAVENO", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblprelinf"] = ds1.Tables[0];

        }

    }
}