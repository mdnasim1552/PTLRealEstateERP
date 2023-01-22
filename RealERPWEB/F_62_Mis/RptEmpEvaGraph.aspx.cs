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
//using KPIRDLC;
using RealEntity;
using System.Reflection;
//using Microsoft.Reporting.WebForms;
using System.IO;
namespace RealERPWEB.F_62_Mis
{
    public partial class RptEmpEvaGraph : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee KPI Evaluation Graph";

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
            string comcod = this.Getcomcod();
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string deptcode = hst["deptcode"].ToString();
            //List<RealEntity.C_47_Kpi.EClassEmpCode> lst3 = new List<RealEntity.C_47_Kpi.EClassEmpCode>();
            string usrid = "";
            //lst3 = objUser.GetEmpCode("dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETEMPID", srchEmp, usrid,deptcode);
            // Prev. 


            string monid = this.ddlyearmon.SelectedValue.ToString();

            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst3 = objUser.GetEmpCode2(srchEmp, usrid, deptcode, monid);


            this.ddlEmpid.DataTextField = "empname";
            this.ddlEmpid.DataValueField = "empid";
            this.ddlEmpid.DataSource = lst3;
            this.ddlEmpid.DataBind();
        }
        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {
            this.GetEmpList();
        }



        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowEmpData();
        }

        private void ShowEmpData()
        {
            string comcod = this.Getcomcod();
            string Empid = this.ddlEmpid.SelectedValue.ToString();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            DataSet ds = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWEMPPERGRAPH", Empid, YmonID);
            ViewState["tbEmpEvalGrp"] = ds.Tables[0];
            this.showChart();
        }

        private void showChart()
        {
            Panel1.Visible = true;
            Panel2.Visible = true;
            Panel3.Visible = true;
            Panel4.Visible = true;
            Panel5.Visible = true;
            Panel6.Visible = true;
            Panel7.Visible = true;
            Panel8.Visible = true;

            Chart1.Series["Series1"].XValueMember = "mondays";
            Chart1.Series["Series1"].YValueMembers = "tamt1";
            Chart1.Series["Series2"].XValueMember = "mondays";
            Chart1.Series["Series2"].YValueMembers = "amt1";

            Chart1.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart1.DataBind();

            Chart2.Series["Series1"].XValueMember = "mondays";
            Chart2.Series["Series1"].YValueMembers = "tamt2";
            Chart2.Series["Series2"].XValueMember = "mondays";
            Chart2.Series["Series2"].YValueMembers = "amt2";

            Chart2.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart2.DataBind();


            Chart3.Series["Series1"].XValueMember = "mondays";
            Chart3.Series["Series1"].YValueMembers = "tamt3";
            Chart3.Series["Series2"].XValueMember = "mondays";
            Chart3.Series["Series2"].YValueMembers = "amt3";

            Chart3.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart3.DataBind();


            Chart4.Series["Series1"].XValueMember = "mondays";
            Chart4.Series["Series1"].YValueMembers = "tamt4";
            Chart4.Series["Series2"].XValueMember = "mondays";
            Chart4.Series["Series2"].YValueMembers = "amt4";

            Chart4.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart4.DataBind();


            Chart5.Series["Series1"].XValueMember = "mondays";
            Chart5.Series["Series1"].YValueMembers = "tamt5";
            Chart5.Series["Series2"].XValueMember = "mondays";
            Chart5.Series["Series2"].YValueMembers = "amt5";

            Chart5.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart5.DataBind();

            Chart6.Series["Series1"].XValueMember = "mondays";
            Chart6.Series["Series1"].YValueMembers = "tamt6";
            Chart6.Series["Series2"].XValueMember = "mondays";
            Chart6.Series["Series2"].YValueMembers = "amt6";

            Chart6.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart6.DataBind();


            Chart7.Series["Series1"].XValueMember = "mondays";
            Chart7.Series["Series1"].YValueMembers = "tamt7";
            Chart7.Series["Series2"].XValueMember = "mondays";
            Chart7.Series["Series2"].YValueMembers = "amt7";

            Chart7.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart7.DataBind();


            Chart8.Series["Series1"].XValueMember = "mondays";
            Chart8.Series["Series1"].YValueMembers = "tamt8";
            Chart8.Series["Series2"].XValueMember = "mondays";
            Chart8.Series["Series2"].YValueMembers = "amt8";

            Chart8.DataSource = (DataTable)ViewState["tbEmpEvalGrp"];
            Chart8.DataBind();



        }

        protected void lnkprint_Click(object sender, EventArgs e)
        {
            CreateRdlcReport();
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
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        public void CreateRdlcReport()
        {

            //string ReportDllPath = "~/bin/KPIRDLC.dll";
            //string AssemblyPath = "KPIRDLC.R_32_Mis.EmpGrpRt.rdlc";


            //DataTable td = (DataTable)ViewState["tbEmpEvalGrp"];

            //LocalReport rt = new LocalReport();
            ////rt.ReportPath = @"E:\ASITREALKPI\ASITRealKPI\KPIRDLC\R_32_Mis\EmpGrpRt.rdlc";
            //Assembly assembly1 = Assembly.LoadFrom(Server.MapPath(ReportDllPath));
            //Stream stream1 = assembly1.GetManifestResourceStream(AssemblyPath);
            //rt.LoadReportDefinition(stream1);

            //rt.DataSources.Clear();


            //KPIRDLC.R_32_Mis.EmpGrpDs empgrp = new KPIRDLC.R_32_Mis.EmpGrpDs();
            //ReportDataSource ds = new ReportDataSource("EmpGrpDs", (DataTable)ViewState["tbEmpEvalGrp"]);



            //rt.DataSources.Add(ds);
            //SetReportParameters(rt);




            //Session["Report1"] = rt;


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RdlcViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        //public void SetReportParameters(LocalReport rt)
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string EmpName = this.ddlEmpid.SelectedItem.Text.ToString();
        //    string YmonName = this.ddlyearmon.SelectedItem.Text.ToString();
        //    rt.SetParameters(new ReportParameter("EmpName", EmpName));
        //    rt.SetParameters(new ReportParameter("YmonName", YmonName));
        //    rt.SetParameters(new ReportParameter("comnam", comnam));

        //    string compname = hst["compname"].ToString();
        //    rt.SetParameters(new ReportParameter("compname", compname));
        //    string reportname = "Employee KPI Evaluation Graph For The Month of " + this.ddlyearmon.SelectedItem.ToString();
        //    rt.SetParameters(new ReportParameter("reportname", reportname));
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    rt.SetParameters(new ReportParameter("printdate", printdate));
        //    rt.SetParameters(new ReportParameter("comnam", comnam));
        //    string username = hst["username"].ToString();
        //    rt.SetParameters(new ReportParameter("username", username));
        //    string sales = "Monthly Sales. Target Vs Achievement";
        //    string collection = "Monthly Collection. Target Vs Achievement";
        //    string visit = "Monthly Visit. Target Vs Achievement";
        //    string call = "Monthly Call. Target Vs Achievement";
        //    rt.SetParameters(new ReportParameter("sales", sales));
        //    rt.SetParameters(new ReportParameter("collection", collection));
        //    rt.SetParameters(new ReportParameter("visit", visit));
        //    rt.SetParameters(new ReportParameter("call", call));

        //    rt.Refresh();
        //}



    }
}
