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
using RealEntity.C_47_Kpi;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.IO;
using FastMember;
namespace RealERPWEB.F_47_Kpi
{
    public partial class RptEmpEvaSheet : System.Web.UI.Page
    {
        ProcessAccess KpiData = new ProcessAccess();
        UserManagerKPI Obj = new UserManagerKPI();
        //   private KPIRDLC.R_21_Kpi.RptManager_21_Kpi rptManager_21_Kpi = new KPIRDLC.R_21_Kpi.RptManager_21_Kpi();
        BL_EntryKpi obj1 = new BL_EntryKpi();
        public static string TString = "";
        private string AssemblyPath;
        LocalReport rt = new LocalReport();

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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee KPI Evaluation Sheet";
                this.chktwise.Visible = (this.Request.QueryString["Type"].ToString() == "Mgt");
                this.lblPage.Visible = (this.Request.QueryString["Type"].ToString() == "Mgt");
                this.ddlpagesize.Visible = (this.Request.QueryString["Type"].ToString() == "Mgt");
                this.GetYearMonth();
            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

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
            //string YmonID = (string)Session["YmonID"];
            //string Empid = (string)Session["Empid"];
            //string Grp = (string)Session["Grp"];

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string comcod = this.Getcomcod();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            Session["YmonID"] = YmonID;
            ViewState["curmon"] = this.ddlyearmon.SelectedItem.ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string empid = (this.Request.QueryString["Type"].ToString() == "Mgt") ? "" : hst["usrid"].ToString();
            string teamwgroup = (this.chktwise.Checked) ? "TeamwGroup" : "";
            string deptcode = "9402%";

            DataSet ds = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWKPISTATUSALL", YmonID, "", empid, teamwgroup, deptcode);
            ViewState["tbEmpEval"] = (this.chktwise.Checked) ? HiddenSameDatat(ds.Tables[0]) : ds.Tables[0];
            ViewState["tbGppDes"] = ds.Tables[1];
            this.Data_Bind();
        }
        private DataTable HiddenSameDatat(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string mteamcode = dt1.Rows[0]["mteamcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mteamcode"].ToString() == mteamcode)
                    dt1.Rows[j]["mteamdesc"] = "";
                mteamcode = dt1.Rows[j]["mteamcode"].ToString();
            }
            return dt1;

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dtpname = (DataTable)ViewState["tbGppDes"];
            int j = 3;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvEmpEval.Columns[j].HeaderText = dtpname.Rows[i]["kpigrpdesc"].ToString() + " TARGET";
                j++;
                if (j == 11)
                    break;
            }
            int k = 12;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvEmpEval.Columns[k].HeaderText = dtpname.Rows[i]["kpigrpdesc"].ToString() + " ACTUAL";
                k++;
                if (k == 20)
                    break;
            }


            DataTable dt = (DataTable)ViewState["tbEmpEval"];
            this.gvEmpEval.ShowFooter = (this.Request.QueryString["Type"].ToString() == "Mgt");
            this.gvEmpEval.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpEval.DataSource = dt;
            this.gvEmpEval.DataBind();


            for (int i = 0; i < gvEmpEval.Rows.Count; i++)
            {
                string Empid = ((Label)gvEmpEval.Rows[i].FindControl("lblgvEmpid")).Text.Trim();

                LinkButton lbtn1 = (LinkButton)gvEmpEval.Rows[i].FindControl("btnGpa");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = Empid;
                }
                //LinkButton lbtn2 = (LinkButton)gvEmpEval.Rows[i].FindControl("lblgvEmpname");
                //if (lbtn1 != null)
                //{
                //    if (lbtn2.Text.Trim().Length > 0)
                //        lbtn2.CommandArgument = Empid;
                //}
                //LinkButton lbtn3 = (LinkButton)gvEmpEval.Rows[i].FindControl("btnGraph");
                //if (lbtn3 != null)
                //{
                //    if (lbtn3.Text.Trim().Length > 0)
                //        lbtn3.CommandArgument = Empid;
                //}
                LinkButton lbtnS = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt1");
                if (lbtnS != null)
                {
                    if (lbtnS.Text.Trim().Length > 0)
                        lbtnS.CommandArgument = Empid;
                }
                LinkButton lbtnC = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt2");
                if (lbtnC != null)
                {
                    if (lbtnC.Text.Trim().Length > 0)
                        lbtnC.CommandArgument = Empid;
                }
                LinkButton lbtno = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt3");
                if (lbtno != null)
                {
                    if (lbtno.Text.Trim().Length > 0)
                        lbtno.CommandArgument = Empid;
                }
                LinkButton lblamt4 = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt4");
                if (lblamt4 != null)
                {
                    if (lblamt4.Text.Trim().Length > 0)
                        lblamt4.CommandArgument = Empid;
                }

                LinkButton lblamt5 = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt5");
                if (lblamt5 != null)
                {
                    if (lblamt5.Text.Trim().Length > 0)
                        lblamt5.CommandArgument = Empid;
                }

                LinkButton lblamt6 = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt6");
                if (lblamt6 != null)
                {
                    if (lblamt6.Text.Trim().Length > 0)
                        lblamt6.CommandArgument = Empid;
                }
                LinkButton lblamt7 = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt7");
                if (lblamt7 != null)
                {
                    if (lblamt7.Text.Trim().Length > 0)
                        lblamt7.CommandArgument = Empid;
                }
                LinkButton lblamt8 = (LinkButton)gvEmpEval.Rows[i].FindControl("lblamt8");
                if (lblamt8 != null)
                {
                    if (lblamt8.Text.Trim().Length > 0)
                        lblamt8.CommandArgument = Empid;
                }

            }
            this.FooterCal();
        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)ViewState["tbEmpEval"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt1)", "")) ? 0 : dt.Compute("sum(tamt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt2)", "")) ? 0 : dt.Compute("sum(tamt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt3)", "")) ? 0 : dt.Compute("sum(tamt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt4)", "")) ? 0 : dt.Compute("sum(tamt4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt5)", "")) ? 0 : dt.Compute("sum(tamt5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt6)", "")) ? 0 : dt.Compute("sum(tamt6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt7)", "")) ? 0 : dt.Compute("sum(tamt7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamt8)", "")) ? 0 : dt.Compute("sum(tamt8)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0 : dt.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0 : dt.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0 : dt.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0 : dt.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0 : dt.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0 : dt.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0 : dt.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0 : dt.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void gvEmpEval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkempname = (HyperLink)e.Row.FindControl("hlnkempname");
                //Label Index = (Label)e.Row.FindControl("lbltpar");
                LinkButton Gpa = (LinkButton)e.Row.FindControl("btnGpa");
                HyperLink Index = (HyperLink)e.Row.FindControl("hlnkbtnpar");


                double Value = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "tpar"));

                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string empname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empname")).ToString();
                string yearmon = this.ddlyearmon.SelectedValue.ToString();
                DateTime date = Convert.ToDateTime(yearmon.Substring(4, 2) + "-01-" + yearmon.Substring(0, 4));
                string fromdate = date.ToString("dd-MMM-yyyy");
                string todate = date.AddMonths(1).AddDays(-1).ToString("dd-MMMM-yyyy");

                if (Value >= 100)
                {
                    Index.Style.Add("color", "Green");
                    Gpa.Style.Add("color", "Green");
                }
                else if (Value < 100 && Value >= 80)
                {
                    Index.Style.Add("color", "Navy");
                    Gpa.Style.Add("color", "Navy");
                }
                else if (Value < 80 && Value >= 60)
                {
                    Index.Style.Add("color", "Purple");
                    Gpa.Style.Add("color", "Purple");
                }
                else if (Value < 60 && Value >= 50)
                {
                    Index.Style.Add("color", "Silver");
                    Gpa.Style.Add("color", "Silver");
                }
                else if (Value < 50 && Value >= 40)
                {
                    Index.Style.Add("color", "Lime");
                    Gpa.Style.Add("color", "Lime");
                }
                else if (Value < 40 && Value >= 30)
                {
                    Index.Style.Add("color", "Olive");
                    Gpa.Style.Add("color", "Olive");
                }
                else
                {
                    Index.Style.Add("color", "Red");
                    Gpa.Style.Add("color", "Red");
                }





                hlnkempname.NavigateUrl = "~/F_47_Kpi/RptEmpMonthWiseEva.aspx?Type=Mgt";
                Index.NavigateUrl = "~/F_39_MyPage/EmpKpiEntry04.aspx?Type=Mgt";
                // hlnkempname.NavigateUrl = "~/F_21_Kpi/LinkEmpMonthWiseEva.aspx?empid=" + empid + "&empname=" + empname + "&Date1=" + fromdate + "&Date2=" + todate;

            }



        }

        protected void lnkprint_Click(object sender, EventArgs e)
        {

            if (this.chktwise.Checked)
                this.PrintGroupWiseEmpEva();



            else
                this.PrintEmpEva();
            //this.CreateEmployeeEvaluationReport();

        }

        private void PrintGroupWiseEmpEva()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptname = hst["deptname"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tbEmpEval"];

            ////Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstGrade = obj1.GetGpaList();

            //KPI
            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            if (ds2 == null) { return; }





            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptGrpwiseEmpEva();
            TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;


            TextObject txtdeptname = rptAppMonitor.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdeptname.Text = "Department: " + deptname;

            TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = "Month: " + this.ddlyearmon.SelectedItem.Text;
            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Sub Report
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstGrade);
            rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
            rptAppMonitor.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintEmpEva()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptname = hst["deptname"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tbEmpEval"];

            ////Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstGrade = obj1.GetGpaList();
            //KPI
            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            if (ds2 == null) { return; }
            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptSalesEmpEva();
            TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;

            TextObject txtdeptname = rptAppMonitor.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdeptname.Text = "Department: " + deptname;

            TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = "Month: " + this.ddlyearmon.SelectedItem.Text;
            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Sub Report
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstGrade);
            rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);

            rptAppMonitor.SetDataSource(dt);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        public void CreateEmployeeEvaluationReport()
        {

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //DataTable dt = (DataTable)ViewState["tbGppDes"];
            //string currentMonth = (string)ViewState["curmon"];
            //this.AssemblyPath = "KPIRDLC.R_21_Kpi.EmpEvReport.rdlc";
            //ReportDataSource ds = new ReportDataSource("EmpKpiEvSheet", (DataTable)ViewState["tbEmpEval"]);

            //rt = rptManager_47_Kpi.GetReport(AssemblyPath, ds);
            //rptManager_21_Kpi.EmployeeEvaluationReportParameters(rt, dt, hst, currentMonth);

            //Session["Report1"] = rt;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RdlcViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //rt.DataSources.Add(ds);
            //SetReportParameters(rt);

            //Session["Report1"] = rt;


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RdlcViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        public void SetReportParameters(LocalReport rt)
        {
            string[] headers = new string[13];
            headers[0] = "SL";
            headers[1] = "EMPLOYEE NAME";



            DataTable dtpname = (DataTable)ViewState["tbGppDes"];
            int j = 2;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                headers[j] = dtpname.Rows[i]["kpigrpdesc"].ToString();
                j++;
                if (j == 6)
                    break;


            }
            int k = 6;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                headers[k] = dtpname.Rows[i]["kpigrpdesc"].ToString();
                k++;
                if (k == 13)
                    break;


            }
            headers[10] = "RESULT";
            headers[11] = "GRADE";
            headers[12] = "Graph";
            string[] temp = new String[13];
            temp = headers;
            for (int i = 0; i < headers.Length; i++)
                rt.SetParameters(new ReportParameter("p" + (i + 1).ToString(), headers[i]));


            Hashtable hst = (Hashtable)Session["tblLogin"];

            string compname = hst["compname"].ToString();
            rt.SetParameters(new ReportParameter("compname", compname));
            string reportname = "Employee KPI Evaluation Sheet ";
            rt.SetParameters(new ReportParameter("reportname", reportname));



            string deptname = "Department: " + hst["deptname"].ToString();
            rt.SetParameters(new ReportParameter("deptname", deptname));

            string monthid = "Month: " + this.ddlyearmon.SelectedItem.ToString();
            rt.SetParameters(new ReportParameter("monthid", monthid));


            rt.SetParameters(new ReportParameter("reportname", reportname));
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            rt.SetParameters(new ReportParameter("printdate", printdate));
            string comnam = hst["comnam"].ToString();
            rt.SetParameters(new ReportParameter("comnam", comnam));
            string username = hst["username"].ToString();
            rt.SetParameters(new ReportParameter("username", username));
            rt.Refresh();
        }

        protected void btnGpa_Click(object sender, EventArgs e)
        {


            string comcod = this.Getcomcod();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWINDKPISTATUS", Empid, YmonID);

            ViewState["tbModalDataInd"] = HiddenSameData(ds1.Tables[0]);
            this.Modal_Data_Bind();
            string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }



        private void Modal_Data_Bind()
        {
            DataTable dt1 = (DataTable)ViewState["tbModalDataInd"];
            this.gvEmpIndEva.DataSource = dt1;
            this.gvEmpIndEva.DataBind();

            ((Label)this.gvEmpIndEva.FooterRow.FindControl("lblFKpival")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(stdkpival)", "")) ?
                                    0.00 : dt1.Compute("sum(stdkpival)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvEmpIndEva.FooterRow.FindControl("lblFMparcnt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(mparcnt)", "")) ?
                                    0.00 : dt1.Compute("sum(mparcnt)", ""))).ToString("#,##0.00;(#,##0.00); ");


            string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {
                string empid = dt1.Rows[0]["empid"].ToString();
                for (int j = 1; j < dt1.Rows.Count; j++)
                {
                    if (dt1.Rows[j]["empid"].ToString() == empid)
                    {
                        empid = dt1.Rows[j]["empid"].ToString();
                        dt1.Rows[j]["empname"] = "";
                    }

                    else
                    {
                        empid = dt1.Rows[j]["empid"].ToString();
                    }

                }
            }

            return dt1;

        }
        protected void btnEmp_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();


            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWDAYWISEDATA", Empid, YmonID);

            ViewState["tbModalDataNext"] = ds1.Tables[0];

            this.lblName.Text = "Name: " + ds1.Tables[1].Rows[0]["gdatat"].ToString();
            this.lblDesg.Text = "Designation: " + ds1.Tables[1].Rows[2]["gdatat"].ToString();
            this.lblJoin.Text = "Date of Joining: " + ds1.Tables[1].Rows[1]["gdatad"].ToString();


            this.Modal_2_Bind();
            string radalertscript = "<script language='javascript'>function f(){loadModal4(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        private void Modal_2_Bind()
        {
            DataTable dtpname = (DataTable)ViewState["tbGppDes"];
            int j = 2;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvResMonth.Columns[j].HeaderText = dtpname.Rows[i]["kpigrpdesc"].ToString();
                j++;
                if (j == 6)
                    break;


            }
            int k = 7;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvResMonth.Columns[k].HeaderText = dtpname.Rows[i]["kpigrpdesc"].ToString();
                k++;
                if (k == 11)
                    break;


            }
            //



            DataTable dt1 = (DataTable)ViewState["tbModalDataNext"];
            this.gvResMonth.DataSource = dt1;
            this.gvResMonth.DataBind();

            ((Label)this.gvResMonth.FooterRow.FindControl("lblFtamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tamt1)", "")) ?
                                    0.00 : dt1.Compute("sum(tamt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvResMonth.FooterRow.FindControl("lblFtamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tamt2)", "")) ?
                                    0.00 : dt1.Compute("sum(tamt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvResMonth.FooterRow.FindControl("lblFtamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tamt3)", "")) ?
                                    0.00 : dt1.Compute("sum(tamt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvResMonth.FooterRow.FindControl("lblFtamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tamt4)", "")) ?
                                    0.00 : dt1.Compute("sum(tamt4)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.gvResMonth.FooterRow.FindControl("lblFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ?
                                    0.00 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvResMonth.FooterRow.FindControl("lblFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ?
                                    0.00 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvResMonth.FooterRow.FindControl("lblFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ?
                                    0.00 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvResMonth.FooterRow.FindControl("lblFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ?
                                    0.00 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");


            string radalertscript = "<script language='javascript'>function f(){loadModal4(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void gvResMonth_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 2;

                TableCell cell1 = new TableCell();
                cell1.Text = "TARGET";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 4;
                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;
                TableCell cell2 = new TableCell();
                cell2.Text = "ACTUAL";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 4;

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell03);
                gvResMonth.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void btnGraph_Click(object sender, EventArgs e)
        {

            string comcod = this.Getcomcod();
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "SHOWEMPPERGRAPH", Empid, YmonID);

            ViewState["tbModalGraph"] = ds1.Tables[0];
            this.showChart();
            string radalertscript = "<script language='javascript'>function f(){loadModal5(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        private void showChart()
        {
            Panel1.Visible = true;

            Chart1.Series["Series1"].XValueMember = "mondays";
            Chart1.Series["Series1"].YValueMembers = "indt";
            Chart1.Series["Series2"].XValueMember = "mondays";
            Chart1.Series["Series2"].YValueMembers = "aindt";

            //Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            //Chart1.ChartAreas["ChartArea1"].AxisX.TitleFont= new Font("Sans Serif", 10, FontStyle.Bold);
            //Chart1.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            //Chart1.ChartAreas["ChartArea1"].AxisY.Maximum = 100;

            //Chart1.ChartAreas["ChartArea1"].AxisY.Interval = 20;

            Chart1.DataSource = (DataTable)ViewState["tbModalGraph"];
            Chart1.DataBind();


        }

        protected void gvEmpEval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpEval.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void btnAmt1_Click(object sender, EventArgs e)
        {
            string comcod = this.Getcomcod();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            this.DayWiseSales(Empid);



        }
        private void DayWiseMSales(string Empid)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("DaySales");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();


            List<RealEntity.C_47_Kpi.EClassEmpInf> lst2 = Obj.GetEmpInf(Empid);

            this.lblNameS.Text = "Name: " + lst2[0].gdatat;
            this.lblDesgS.Text = "Designation: " + lst2[2].gdatat;
            this.lblJoinS.Text = "Date of Joining: " + lst2[1].gdatad;


            List<RealEntity.C_47_Kpi.EClassDaySales> lst = Obj.GetDayWiseSales_(YmonID, Empid);

            Session["DaySales"] = lst;
            this.Sales_Bind();


            //try
            //{
            //    string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //    List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise> lst = objUserService.ShowDayWise(CurDate1);
            //    if (lst == null)
            //        return;

            //    ViewState["tblDayWise"] = HiddenSameData(lst);
            //    this.GvDayWise.DataSource = lst;
            //    this.GvDayWise.DataBind();
            //    this.FooterCalculation();

            //}
            //catch (Exception ex)
            //{

            //}


        }

        //private List<Kp.C_22_Sal.EClassSales_02.EClassDayWise> HiddenSameData(List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise> lst3)
        //{
        //    if (lst3.Count == 0)
        //        return lst3;

        //    int i = 0;
        //    string centrid = "";
        //    foreach (RealEntity.C_22_Sal.EClassSales_02.EClassDayWise c1 in lst3)
        //    {
        //        if (i == 0)
        //        {
        //            centrid = c1.centrid;
        //            i++;
        //            continue;

        //        }
        //        else if (c1.centrid == centrid)
        //        {
        //            c1.centrdesc = "";
        //        }
        //        centrid = c1.centrid;

        //    }

        //    return lst3;

        //}
        private void DayWiseSales(string Empid)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("DaySales");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();


            List<RealEntity.C_47_Kpi.EClassEmpInf> lst2 = Obj.GetEmpInf(Empid);

            this.lblNameS.Text = "Name: " + lst2[0].gdatat;
            this.lblDesgS.Text = "Designation: " + lst2[2].gdatat;
            this.lblJoinS.Text = "Date of Joining: " + lst2[1].gdatad;


            List<RealEntity.C_47_Kpi.EClassDaySales> lst = Obj.GetDayWiseSales_(YmonID, Empid);

            Session["DaySales"] = lst;
            this.Sales_Bind();

        }
        private void Sales_Bind()
        {

            List<RealEntity.C_47_Kpi.EClassDaySales> lst = (List<RealEntity.C_47_Kpi.EClassDaySales>)Session["DaySales"];
            this.gvDayWSale.DataSource = lst;
            this.gvDayWSale.DataBind();
            this.FooterCalCulation(lst);
            string radalertscript = "<script language='javascript'>function f(){loadModal_Sales(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }




        protected void lnkprintsales_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            List<RealEntity.C_47_Kpi.EClassDaySales> lst = (List<RealEntity.C_47_Kpi.EClassDaySales>)Session["DaySales"];
            ReportDocument rptsale = new RealERPRPT.R_62_Mis.rptDayWiseSales();
            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtEmpname = rptsale.ReportDefinition.ReportObjects["txtEmpname"] as TextObject;
            txtEmpname.Text = lblNameS.Text;
            TextObject txtDesignation = rptsale.ReportDefinition.ReportObjects["txtDesignation"] as TextObject;
            txtDesignation.Text = lblDesgS.Text;
            TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "Month : " + this.ddlyearmon.SelectedItem.Text;
            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(lst);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + this.DDPrintOptsales.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }


        private void FooterCalCulation(List<RealEntity.C_47_Kpi.EClassDaySales> lst)
        {

            if (lst.Count == 0)
                return;

            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = lst.Select(p => p.tuamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = lst.Select(p => p.suamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = lst.Select(p => p.disamt).Sum().ToString("#,##0;(#,##0); ");

        }

        protected void btnAmt2_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("DaySales");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            List<RealEntity.C_47_Kpi.EClassEmpInf> lst2 = Obj.GetEmpInf(Empid);

            this.lblNameC.Text = "Name: " + lst2[0].gdatat;
            this.lblDesgC.Text = "Designation: " + lst2[2].gdatat;
            this.lblJoinC.Text = "Date of Joining: " + lst2[1].gdatad;


            List<RealEntity.C_47_Kpi.EClassDayCollection> lst = Obj.GetDayWiseColl_(YmonID, Empid);

            Session["DayColl"] = HiddenSameData(lst);
            this.Coll_Bind();
        }
        private List<RealEntity.C_47_Kpi.EClassDayCollection> HiddenSameData(List<RealEntity.C_47_Kpi.EClassDayCollection> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string grp = "";
            foreach (RealEntity.C_47_Kpi.EClassDayCollection c1 in lst3)
            {
                if (i == 0)
                {
                    grp = c1.grp;
                    i++;
                    continue;

                }
                else if (c1.grp == grp)
                {
                    c1.grpdesc = "";
                }
                grp = c1.grp;

            }

            return lst3;

        }
        private void Coll_Bind()
        {

            List<RealEntity.C_47_Kpi.EClassDayCollection> lst = (List<RealEntity.C_47_Kpi.EClassDayCollection>)Session["DayColl"];
            this.grvTrnDatWise.DataSource = lst;
            this.grvTrnDatWise.DataBind();
            this.FooterCalCulation_Col(lst);
            string radalertscript = "<script language='javascript'>function f(){loadModal_Coll(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        protected void lnkprintColl_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            List<RealEntity.C_47_Kpi.EClassDayCollection> lst = (List<RealEntity.C_47_Kpi.EClassDayCollection>)Session["DayColl"];
            ReportDocument rptstate = new RealERPRPT.R_62_Mis.RptTransStatement02();


            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtEmpname = rptstate.ReportDefinition.ReportObjects["txtEmpname"] as TextObject;
            txtEmpname.Text = this.lblNameC.Text;
            TextObject txtDesignation = rptstate.ReportDefinition.ReportObjects["txtDesignation"] as TextObject;
            txtDesignation.Text = this.lblDesgC.Text;
            TextObject rptDate = rptstate.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "Month : " + this.ddlyearmon.SelectedItem.Text;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(lst);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + this.DDPrintOptColl.SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }


        private void FooterCalCulation_Col(List<RealEntity.C_47_Kpi.EClassDayCollection> lst)
        {

            if (lst.Count == 0)
                return;

            ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFCashamt")).Text = lst.Select(p => p.cashamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFChqamt")).Text = lst.Select(p => p.chqamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCDNetTotal")).Text = lst.Select(p => (p.cashamt + p.chqamt)).Sum().ToString("#,##0;(#,##0); ");

        }
        protected void btnAmt3_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("ClientDis");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Grp = "810100601003";
            Session["YmonID"] = YmonID;
            Session["Empid"] = Empid;
            Session["Grp"] = Grp;

            List<RealEntity.C_47_Kpi.EClassClientDis> lst = Obj.GetClientDis_(YmonID, Empid, Grp);
            Session["ClientDis"] = lst;
            this.Dis_Bind();
        }

        protected void btnAmt4_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("ClientDis");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Grp = "810100601004";



            List<RealEntity.C_47_Kpi.EClassClientDis> lst = Obj.GetClientDis_(YmonID, Empid, Grp);
            Session["ClientDis"] = lst;
            this.Dis_Bind();
        }
        private void Dis_Bind()
        {



            List<RealEntity.C_47_Kpi.EClassClientDis> lst = (List<RealEntity.C_47_Kpi.EClassClientDis>)Session["ClientDis"];

            List<RealEntity.C_47_Kpi.EClassClientDis> lst1 = lst.FindAll(p => p.grp == "A");
            List<RealEntity.C_47_Kpi.EClassClientDis> lst2 = lst.FindAll(p => p.grp == "B");
            this.gvClientDisst.DataSource = lst1;
            this.gvClientDisst.DataBind();

            this.gvclientCall.DataSource = lst2;
            this.gvclientCall.DataBind();

            //dv1 = ds1.Tables[0].DefaultView;
            //dv1.RowFilter = ("grp1 = 'A'");
            //dtr = dv1.ToTable();
            //this.lblReceiptCash.Visible = true;
            //this.lblPaymentCash.Visible = false;
            //this.lblDetailsCash.Visible = false;



            string radalertscript = "<script language='javascript'>function f(){loadModalVisit(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void lnkprint2_Click(object sender, EventArgs e)
        {



            //List<RealEntity.C_47_Kpi.EClassClientDis> lst = (List<RealEntity.C_47_Kpi.EClassClientDis>)Session["ClientDis"];


            //string AssemblyPath = "KPIRDLC.R_21_Kpi.ClientDiscussRt.rdlc";
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string currentMonth = (string)ViewState["curmon"];
            //ReportDataSource ds = new ReportDataSource("ClientDiscussDs", lst);
            //rt = rptManager_21_Kpi.GetReport(AssemblyPath, ds);

            //rptManager_21_Kpi.RptClientHistoryParameters(rt, hst, currentMonth);

            //Session["Report1"] = rt;


            //string redirect = "<script>window.open('../RdlcViewer.aspx?PrintOpt=" + DDPrintOpt2.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //Response.Write(redirect);


        }




        protected void lblamt5_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("ClientDis");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Grp = "810100601003";
            Session["YmonID"] = YmonID;
            Session["Empid"] = Empid;
            Session["Grp"] = Grp;

            List<RealEntity.C_47_Kpi.EClassClientDis> lst = Obj.GetClientDis_(YmonID, Empid, Grp);
            Session["ClientDis"] = lst;
            this.Dis_Bind();
        }
        protected void lblamt6_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Session.Remove("ClientDis");
            string YmonID = this.ddlyearmon.SelectedValue.ToString();
            string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string Grp = "810100601003";
            Session["YmonID"] = YmonID;
            Session["Empid"] = Empid;
            Session["Grp"] = Grp;

            List<RealEntity.C_47_Kpi.EClassClientDis> lst = Obj.GetClientDis_(YmonID, Empid, Grp);
            Session["ClientDis"] = lst;
            this.Dis_Bind();
        }
    }
}
