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
//using  RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_39_MyPage
{
    public partial class RptEmpKpiRes : System.Web.UI.Page
    {
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee KPI Result";

                this.GetYearMonth();
                this.GetEmpList();
            }
        }

        private void GetYearMonth()
        {
            string comcod = this.Getcomcod();

            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


        }
        private void GetEmpList()
        {
            if (this.lnkok.Text == "New")
                return;
            //-----------Get Person List ---------------//
            UserManagerKPI objUser = new UserManagerKPI();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetHRcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text;
            string usrid = "";
            List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            string deptcode = hst["deptcode"].ToString();
            lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, usrid, deptcode);

            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
        }


        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private string GetHRcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["hcomcod"].ToString());
        }



        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowEmpData();
        }

        private void ShowEmpData()
        {
            string comcod = this.GetHRcomcod();
            UserManagerMyPage objUser = new UserManagerMyPage();
            string empid = this.ddlEmpid.SelectedValue.ToString();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();

            List<RealEntity.C_39_MyPage.EClassShowKPIData> lst3 = new List<RealEntity.C_39_MyPage.EClassShowKPIData>();
            lst3 = objUser.ShowKPIData("SP_REPORT_EMP_KPI", "SHOWINDKPISTATUS", empid, YmonID);



            Session["tbEmpResult"] = lst3;
            this.Data_Bind();
        }
        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //    {

        //    }
        //    else
        //    {
        //        string empid = dt1.Rows[0]["empid"].ToString();
        //        for (int j = 1; j < dt1.Rows.Count; j++)
        //        {
        //            if (dt1.Rows[j]["empid"].ToString() == empid)
        //            {
        //                empid = dt1.Rows[j]["empid"].ToString();
        //                dt1.Rows[j]["empname"] = "";
        //            }

        //            else
        //            {
        //                empid = dt1.Rows[j]["empid"].ToString();
        //            }

        //        }
        //    }

        //    return dt1;

        //}

        private void Data_Bind()
        {
            List<RealEntity.C_39_MyPage.EClassShowKPIData> lst = (List<RealEntity.C_39_MyPage.EClassShowKPIData>)Session["tbEmpResult"];
            this.gvEmpKpiResult.DataSource = lst;
            this.gvEmpKpiResult.DataBind();
            this.FooterCal();
            //for (int i = 0; i < gvStdKpi.Rows.Count; i++)
            //{
            //    string Empid = ((Label)gvStdKpi.Rows[i].FindControl("lblgvEmpid")).Text.Trim();
            //    string Grp = ((Label)gvStdKpi.Rows[i].FindControl("lblgrp")).Text.Trim();
            //    LinkButton lbtn1 = (LinkButton)gvStdKpi.Rows[i].FindControl("btnGrp");
            //    if (lbtn1 != null)
            //    {
            //        if (lbtn1.Text.Trim().Length > 0)
            //            lbtn1.CommandArgument = Empid + Grp;
            //    }
            //}

        }
        private void FooterCal()
        {
            List<RealEntity.C_39_MyPage.EClassShowKPIData> lst = (List<RealEntity.C_39_MyPage.EClassShowKPIData>)Session["tbEmpResult"];
            if (lst.Count == 0)
                return;

            ((Label)this.gvEmpKpiResult.FooterRow.FindControl("lblFKpival")).Text = Convert.ToDouble(lst.Select(p => p.stdkpival).Sum()).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpKpiResult.FooterRow.FindControl("lblFMparcnt")).Text = Convert.ToDouble(lst.Select(p => p.mparcnt).Sum()).ToString("#,##0.00;(#,##0.00); ");
        }


        protected void lnkprint_Click(object sender, EventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string comcod = this.Getcomcod();
            //string clientcod = this.ddlClientList.SelectedValue.ToString();
            //DataSet dset1 = this.KpiData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "RPTCLIENTCOMUCATION", clientcod, "", "", "", "", "", "", "", "");
            //DataTable dtab1 = dset1.Tables[0];
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_21_Mkt.RptTodaysDisAndNextApp();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = this.ddlSalesTeam.SelectedItem.Text;
            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //TextObject txtclientname = rptAppMonitor.ReportDefinition.ReportObjects["txtclientname"] as TextObject;
            //txtclientname.Text = this.ddlClientList.SelectedItem.Text;
            //rptAppMonitor.SetDataSource(dtab1);
            //Session["Report1"] = rptAppMonitor;
            //this.lblprint.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }


    }
}
