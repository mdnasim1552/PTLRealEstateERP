using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
using RealEntity.C_47_Kpi;


namespace RealERPWEB.F_39_MyPage
{
    public partial class RptEmpEvaSheetGen : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        UserManagerMIS objuserMan = new UserManagerMIS();
        public UserManagerKPI objUser = new UserManagerKPI();
        public static string previoudpage = string.Empty;
        BL_EntryKpi obj1 = new BL_EntryKpi();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                previoudpage = this.Request.UrlReferrer.ToString();
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "EvaonProBasis") ? "EVALUATION ON PROJECT"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "EmpEvaluation" ? "EMPLOYEE EVALUATION"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "EmpHistory" ? "INDIVIDUAL HISTORY"
                //    : this.Request.QueryString["Type"].ToString().Trim() == "IndEmpHistory" ? "MONTH WISE EVALUATION DETAILS" : "CLIENT LETTER INFORMATION REPORT";

                this.GetYearMonth();

                this.GetDepartment();





            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
        }

        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void GetYearMonth()
        {
            string comcod = this.Getcomcod();

            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_EMP_KPI_SETUP", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyearmon.DataTextField = "yearmon";
            this.ddlyearmon.DataValueField = "ymon";
            this.ddlyearmon.DataSource = ds1.Tables[0];
            this.ddlyearmon.DataBind();
            this.ddlyearmon.SelectedValue = System.DateTime.Today.ToString("yyyyMM");
            ds1.Dispose();


        }


        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowEmpEvaluation();

        }

        private void GetDepartment()
        {

            List<RealEntity.C_39_MyPage.EClassMIS.EClassDepartment> lst = objuserMan.GetDepartment();

            if (lst.Count == 0)
            {
                this.ddldepartment.DataSource = null;
                this.ddldepartment.DataBind();
                return;
            }

            this.ddldepartment.DataTextField = "deptname";
            this.ddldepartment.DataValueField = "deptcode";
            this.ddldepartment.DataSource = lst;
            this.ddldepartment.DataBind();

        }



        private void ShowEmpEvaluation()
        {

            Session.Remove("tblmis");
            string yearmon = this.ddlyearmon.SelectedValue.ToString();
            string detpcode = this.ddldepartment.SelectedValue.ToString();
            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lst = objuserMan.GetEmployeeEvaGen(yearmon, detpcode);
            if (lst == null)
            {
                this.gvEmpEval.DataSource = null;
                this.gvEmpEval.DataBind();
                return;

            }
            Session["tblmis"] = lst;
            this.Data_Bind();

        }


        private void Data_Bind()
        {

            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lsteva = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen>)Session["tblmis"];
            this.gvEmpEval.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpEval.DataSource = lsteva;
            this.gvEmpEval.DataBind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.Getcomcod();

            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                case "3317":
                case "3318":
                    this.PrintEmpEvSheetGenRup();
                    break;

                default:
                    this.PrintEmpEvaluationGeneral();
                    break;
            }





        }


        private void PrintEmpEvSheetGenRup()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptname = hst["deptname"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen>)Session["tblmis"];

            // Graph

            // double tomark = lst.Select(p => p.tar).Sum();
            // double acmark = lst.Select(p => p.act).Sum();
            // double avgmark=(acmark*100)/tomark;

            //List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph> lstg =new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph>();
            //RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph objgr=new   RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph(100.00, avgmark);
            //lstg.Add(objgr);



            //foreach (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lstgr in lst) 
            //{




            //}




            //DataTable dt = (DataTable)Session["tblmis"];
            ////Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstGrade = obj1.GetGpaList();
            //DataSet ds = MktData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
            //if (ds == null) { return; }
            ////KPI
            //DataSet ds2 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            //if (ds2 == null) { return; }


            //010300101002
            ReportDocument rptAppMonitor = new ReportDocument();
            string dptcode = this.ddldepartment.SelectedValue.ToString();

            if (dptcode == "010300101002")
            {
                rptAppMonitor = new RealERPRPT.R_62_Mis.rptEmpEvaSheetGenRupCons();
            }
            else
            {
                rptAppMonitor = new RealERPRPT.R_62_Mis.rptEmpEvaSheetGenRup();
            }





            TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;

            TextObject txtdept = rptAppMonitor.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdept.Text = deptname;

            int day = 01;
            int mon = Convert.ToInt16(this.ddlyearmon.SelectedValue.Substring(4, 2));
            int year = Convert.ToInt16(this.ddlyearmon.SelectedValue.Substring(0, 4));
            DateTime ydate = new DateTime(year, mon, day);


            TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = "Month: " + ydate.ToString("MMMM-yyyy");
            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////Sub Report
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstGrade);
            // rptAppMonitor.Subreports["rptDeptGraph.rpt"].SetDataSource(lstg);

            rptAppMonitor.SetDataSource(lst);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }
        private void PrintTodaysDisANextApp()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////string Salesteamcode=this.ddlSalesTeam.SelectedValue.ToString();
            //ReportDocument rptAppMonitor = new  RealERPRPT.R_32_Mis.rptEmpEvaSheetGen();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //CompName.Text = comnam;
            //string Date = (this.Request.QueryString["Type"].ToString().Trim() == "Todaysdis") ? "Date: "+this.txtfrmdate.Text.Trim() : " (From "+this.txtfrmdate.Text.Trim()+" To "+this.txttodate.Text.Trim()+")";
            //TextObject rpttxtHeaderTitle = rptAppMonitor.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = this.lblHeadertitle.Text;


            //TextObject txtsalesp = rptAppMonitor.ReportDefinition.ReportObjects["txtsalesp"] as TextObject;
            //txtsalesp.Text = "Executive Name: " + this.ddlSalesTeam.SelectedItem.Text;

            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = Date;


            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptAppMonitor.SetDataSource((DataTable)Session["tbltoapp"]);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintEmpEvaluationGeneral()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptname = hst["deptname"].ToString();
            string deptcode = hst["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen>)Session["tblmis"];

            // Graph

            // double tomark = lst.Select(p => p.tar).Sum();
            // double acmark = lst.Select(p => p.act).Sum();
            // double avgmark=(acmark*100)/tomark;

            //List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph> lstg =new List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph>();
            //RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph objgr=new   RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGenGraph(100.00, avgmark);
            //lstg.Add(objgr);



            //foreach (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEvaGen> lstgr in lst) 
            //{




            //}




            //DataTable dt = (DataTable)Session["tblmis"];
            ////Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstGrade = obj1.GetGpaList();
            //DataSet ds = MktData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
            //if (ds == null) { return; }
            ////KPI
            //DataSet ds2 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            //if (ds2 == null) { return; }
            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptEmpEvaSheetGen();
            TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompName.Text = comnam;

            TextObject txtdept = rptAppMonitor.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdept.Text = deptname;

            int day = 01;
            int mon = Convert.ToInt16(this.ddlyearmon.SelectedValue.Substring(4, 2));
            int year = Convert.ToInt16(this.ddlyearmon.SelectedValue.Substring(0, 4));
            DateTime ydate = new DateTime(year, mon, day);


            TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = "Month: " + ydate.ToString("MMMM-yyyy");
            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////Sub Report
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstGrade);
            // rptAppMonitor.Subreports["rptDeptGraph.rpt"].SetDataSource(lstg);

            rptAppMonitor.SetDataSource(lst);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptAppMonitor.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptAppMonitor;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        private void PrintEmpEvaluation()
        {
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string deptcode = hst["deptcode"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //    List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva> lst = (List<RealEntity.C_39_MyPage.EClassMIS.EmployeeEva>)Session["tblmis"];

            //    Grade
            //    DataSet ds = MktData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTGRADE", "", "", "", "", "");
            //    if (ds == null) { return; }


            //    ReportDocument rptAppMonitor = new  RealERPRPT.R_05_MyPage.rptMonthWiseAllEmpEva02();
            //    TextObject txtCompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //    txtCompName.Text = comnam;
            //    TextObject date = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //    date.Text = "(From " + this.txtfrmdate.Text.ToString() + " To " + this.txttodate.Text.ToString() + ")";
            //    rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(ds.Tables[0]);
            //    rptAppMonitor.SetDataSource(lst);
            //    Session["Report1"] = rptAppMonitor;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }





        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }





        protected void gvEmpEval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkempname = (HyperLink)e.Row.FindControl("hlnkempname");
                Label Index = (Label)e.Row.FindControl("lblgvact");
                LinkButton Gpa = (LinkButton)e.Row.FindControl("btnGpa");
                double Value = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "act"));
                string empid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empid")).ToString();
                string empname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "empname")).ToString();

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
                DateTime date = Convert.ToDateTime(this.ddlyearmon.SelectedValue.Substring(4, 2) + "/" + "01/" + this.ddlyearmon.SelectedValue.Substring(0, 4));
                string edate = date.AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                hlnkempname.NavigateUrl = "~/F_05_MyPage/LinkEmpKpiEntry04.aspx?Type=Mgt&deptcode=" + this.ddldepartment.SelectedValue.ToString() + "&empid=" + empid + "&monedate=" + edate;



            }





        }



        protected void btnGpa_Click(object sender, EventArgs e)
        {


            //string comcod = this.Getcomcod();
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            //DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "SHOWINDKPISTATUS", Empid, YmonID);

            //ViewState["tbModalDataInd"] = HiddenSameData(ds1.Tables[0]);
            //this.Modal_Data_Bind();
            //string radalertscript = "<script language='javascript'>function f(){loadModal(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void btnGraph_Click(object sender, EventArgs e)
        {

            //string comcod = this.Getcomcod();
            //string YmonID = this.ddlyearmon.SelectedValue.ToString();
            //string Empid = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();

            //DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "SHOWEMPPERGRAPH", Empid, YmonID);

            //ViewState["tbModalGraph"] = ds1.Tables[0];
            //this.showChart();
            //string radalertscript = "<script language='javascript'>function f(){loadModal5(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void gvEmpEval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpEval.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void imgsrchdepartment_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
    }
}

