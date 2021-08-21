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
using RealERPWEB.Service;

namespace RealERPWEB.F_39_MyPage
{

    public partial class LinkIndGraph : System.Web.UI.Page
    {

        public UserService objuser = new UserService();
        UserManagerKPI objUser = new UserManagerKPI();
        ProcessAccess KpiData = new ProcessAccess();
        public static string TString = "";

        // UserManagerKPI objUser = new UserManagerKPI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee KPI Graph";

                this.GetEmpInfo();




                this.ShowView();
                this.lnkok_Click(null, null);
            }
        }
        private void GetEmpInfo()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";
            string srchEmp = "%";
            string yearmon = this.Request.QueryString["ymon"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = objUser.GetEmpCode2(srchEmp, userid, deptcode, yearmon);
            Session["tblemployee"] = lstemp;
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));


            this.lblvalempname.Text = lstemp1[0].empname1;
            this.lblvaldepartment.Text = lstemp1[0].deptname;
            this.lblvaldesignation.Text = lstemp1[0].desg;
            this.lblvaljoiningdate.Text = Convert.ToDateTime(lstemp1[0].joindat).ToString("dd-MMM-yyyy");
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }

        private void ShowView()
        {


            string dept = this.Request.QueryString["dept"].ToString();
            int ind = this.ddlyearmon.SelectedIndex;
            if (ind == 0)
                this.MultiView1.ActiveViewIndex = 0;
            else if (dept == "Sales" && ind == 1)
                this.MultiView1.ActiveViewIndex = 1;

            else if (dept == "Collection" && ind == 1)
                this.MultiView1.ActiveViewIndex = 2;

            else if ((dept == "Others" || dept == "Legal") && ind == 1)
                this.MultiView1.ActiveViewIndex = 3;

            else if (dept == "Sales" && ind == 2)
                this.MultiView1.ActiveViewIndex = 4;

            else if (dept == "Collection" && ind == 2)
                this.MultiView1.ActiveViewIndex = 5;





            else
                this.MultiView1.ActiveViewIndex = 6;

            //switch (dept)
            //{
            //    case "Salse":
            //        this.MultiView1.ActiveViewIndex = 0;
            //        break;

            //    case "Collection":
            //        this.MultiView1.ActiveViewIndex = 1;
            //        break;

            //    default:
            //        this.MultiView1.ActiveViewIndex = 2;
            //        break;




            //}


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




            string dept = this.Request.QueryString["dept"].ToString();
            int ind = this.ddlyearmon.SelectedIndex;

            if (this.ddlyearmon.SelectedIndex == 0)
            {

                this.MultiView1.ActiveViewIndex = 0;
                this.ShowEmpMonGrap();
            }

            else if (dept == "Sales" && ind == 1)
            {

                this.MultiView1.ActiveViewIndex = 1;
                this.ShowEmpYearlyGraphSales();
            }

            else if (dept == "Collection" && ind == 1)

            {

                this.MultiView1.ActiveViewIndex = 2;
                this.ShowEmpYearlyGraphSales();
            }






            else if ((dept == "Others" || dept == "Legal") && ind == 1)
            {

                this.MultiView1.ActiveViewIndex = 3;
                this.ShowEmpYearlyGraphGen();
            }

            else if (dept == "Sales" && ind == 2)
            {

                this.MultiView1.ActiveViewIndex = 4;
                this.ShowEmpYearlyEmpDetailshSales();
            }

            else if (dept == "Collection" && ind == 2)
            {
                this.MultiView1.ActiveViewIndex = 5;
            }



            else
            {

                this.ShowEmpYearlyEmpDetailsgen();
                this.MultiView1.ActiveViewIndex = 6;
            }
        }

        private void ShowEmpMonGrap()
        {



            string comcod = this.Getcomcod();
            string Empid = this.Request.QueryString["empid"].ToString();
            string ymon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string Date = Convert.ToDateTime(ymon.Substring(4, 2) + "-" + "01-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string CallType = (deptcode.Substring(0, 4) == "9402") ? "GRAPHWKWISESALES" : (deptcode.Substring(0, 4) == "9409") ? "GRAPHWKWISELEG" : "GRAPHDAYWISE";
            DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI03", CallType, deptcode, ymon, Empid, Date);
            ViewState["tblweeklygp"] = ds1.Tables[0];

            Chart1.Series["Series1"].XValueMember = "grpdesc";
            Chart1.Series["Series1"].YValueMembers = "wtamt";
            Chart1.Series["Series2"].XValueMember = "grpdesc";
            Chart1.Series["Series2"].YValueMembers = "wacamt";
            Chart1.Series["Series1"].LegendText = "Target";
            Chart1.Series["Series2"].LegendText = "Actual";
            Chart1.DataSource = ds1.Tables[0];
            Chart1.DataBind();
        }


        private void ShowEmpYearlyGraphSales()
        {



            string comcod = this.Getcomcod();
            string Empid = this.Request.QueryString["empid"].ToString();
            string ymon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string Date = Convert.ToDateTime(ymon.Substring(4, 2) + "-" + "01-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string frmdate = Convert.ToDateTime("01-Jan-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(frmdate).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = objuser.GetEmpMonEva(Empid, frmdate, todate);
            Session["tblempmoneva"] = lst;
            this.Data_Bind();
            this.FooterCalCulation();
            this.ShowMonChartSales();




        }

        private void ShowEmpYearlyGraphGen()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = this.Request.QueryString["empid"].ToString();
            string ymon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string Date = Convert.ToDateTime(ymon.Substring(4, 2) + "-" + "01-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string frmdate = Convert.ToDateTime("01-Jan-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(frmdate).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");


            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = objuser.GetEmpMonEvagen(Empid, frmdate, todate);
            // string CallType = (deptcode == "010100101001") ? "GRAPHMONWISESALES" : "GRAPHMONWISE";
            // DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI03", CallType, Empid, frmdate, todate);
            Session["tblempmoneva"] = lst;
            this.Data_Bind();
            this.FooterCalCulation();
            this.ShowMonChartgen();




        }
        private void ShowEmpYearlyEmpDetailshSales()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = this.Request.QueryString["empid"].ToString();
            string ymon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string Date = Convert.ToDateTime(ymon.Substring(4, 2) + "-" + "01-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string frmdate = Convert.ToDateTime("01-Jan-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(frmdate).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = objuser.GetEmpMonEva(Empid, frmdate, todate);
            Session["tblempmoneva"] = lst;
            this.Data_Bind();
            this.FooterCalCulation();
            this.ShowMonChartLineSales();



        }
        private void ShowEmpYearlyEmpDetailsgen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string Empid = this.Request.QueryString["empid"].ToString();
            string ymon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string Date = Convert.ToDateTime(ymon.Substring(4, 2) + "-" + "01-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string frmdate = Convert.ToDateTime("01-Jan-" + ymon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(frmdate).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy");


            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = objuser.GetEmpMonEvagen(Empid, frmdate, todate);
            // string CallType = (deptcode == "010100101001") ? "GRAPHMONWISESALES" : "GRAPHMONWISE";
            // DataSet ds1 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI03", CallType, Empid, frmdate, todate);
            Session["tblempmoneva"] = lst;
            this.Data_Bind();
            this.FooterCalCulation();
            this.ShowMonChartLinegen();



        }
        private void Data_Bind()
        {




            string dept = this.Request.QueryString["dept"].ToString();
            switch (dept)
            {
                case "Sales":
                    List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];
                    if (this.ddlyearmon.SelectedIndex == 1)
                    {
                        this.gvEmpEval.DataSource = lst;
                        this.gvEmpEval.DataBind();
                    }

                    else
                    {
                        this.gvEmpEvalavg.DataSource = lst;
                        this.gvEmpEvalavg.DataBind();
                    }

                    break;

                case "Collection":
                    break;
                case "Others":
                case "Legal":

                    List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lstgen = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>)Session["tblempmoneva"];
                    if (this.ddlyearmon.SelectedIndex == 1)
                    {
                        this.gvempevagen.DataSource = lstgen;
                        this.gvempevagen.DataBind();
                    }
                    else
                    {
                        this.gvempevagenavg.DataSource = lstgen;
                        this.gvempevagenavg.DataBind();
                    }

                    break;


            }



        }


        private void FooterCalCulation()
        {

            string dept = this.Request.QueryString["dept"].ToString();
            switch (dept)
            {
                case "Sales":
                    List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];
                    if (lst.Count == 0)
                        return;

                    double tsales, tcoll, toffer, tvisit, tcall, toth, asales, acoll, aoffer, avisit, acall, aoth;
                    tsales = Convert.ToDouble(lst.Select(p => p.tamt1).Sum());
                    tcoll = Convert.ToDouble(lst.Select(p => p.tamt2).Sum());
                    toffer = Convert.ToDouble(lst.Select(p => p.tamt3).Sum());
                    tvisit = Convert.ToDouble(lst.Select(p => p.tamt4).Sum());
                    tcall = Convert.ToDouble(lst.Select(p => p.tamt5).Sum());
                    toth = Convert.ToDouble(lst.Select(p => p.tamt6).Sum());
                    asales = Convert.ToDouble(lst.Select(p => p.amt1).Sum());
                    acoll = Convert.ToDouble(lst.Select(p => p.amt2).Sum());
                    aoffer = Convert.ToDouble(lst.Select(p => p.amt3).Sum());
                    avisit = Convert.ToDouble(lst.Select(p => p.amt4).Sum());
                    acall = Convert.ToDouble(lst.Select(p => p.amt5).Sum());
                    aoth = Convert.ToDouble(lst.Select(p => p.amt6).Sum());
                    double selper = (tsales == 0) ? 0.00 : ((asales / tsales) * 100);
                    double collper = (tcoll == 0) ? 0.00 : ((acoll / tcoll) * 100);
                    double offper = (toffer == 0) ? 0.00 : ((aoffer / toffer) * 100);
                    double visitper = (tvisit == 0) ? 0.00 : ((avisit / tvisit) * 100);
                    double callper = (tcall == 0) ? 0.00 : ((acall / tcall) * 100);
                    double othper = (toth == 0) ? 0.00 : ((aoth / toth) * 100);
                    if (this.ddlyearmon.SelectedIndex == 1)
                    {
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt1")).Text = Convert.ToDouble(tsales).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt2")).Text = Convert.ToDouble(tcoll).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt3")).Text = Convert.ToDouble(toffer).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt4")).Text = Convert.ToDouble(tvisit).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt5")).Text = Convert.ToDouble(tcall).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFtamt6")).Text = Convert.ToDouble(toth).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt1")).Text = Convert.ToDouble(asales).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt2")).Text = Convert.ToDouble(acoll).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt3")).Text = Convert.ToDouble(aoffer).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt4")).Text = Convert.ToDouble(avisit).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt5")).Text = Convert.ToDouble(acall).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFamt6")).Text = Convert.ToDouble(aoth).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFpertamt1")).Text = Convert.ToDouble(selper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFpertamt2")).Text = Convert.ToDouble(collper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFpertamt3")).Text = Convert.ToDouble(offper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFpertamt4")).Text = Convert.ToDouble(visitper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFpertamt5")).Text = Convert.ToDouble(callper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEval.FooterRow.FindControl("lblFpertamt6")).Text = Convert.ToDouble(othper).ToString("#,##0.00;(#,##0.00); ");
                    }
                    else
                    {
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFtamtavg1")).Text = Convert.ToDouble(tsales).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFtamtavg2")).Text = Convert.ToDouble(tcoll).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFtamtavg3")).Text = Convert.ToDouble(toffer).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFtamtavg4")).Text = Convert.ToDouble(tvisit).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFtamtavg5")).Text = Convert.ToDouble(tcall).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFtamtavg6")).Text = Convert.ToDouble(toth).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFamtavg1")).Text = Convert.ToDouble(asales).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFamtavg2")).Text = Convert.ToDouble(acoll).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFamtavg3")).Text = Convert.ToDouble(aoffer).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFamtavg4")).Text = Convert.ToDouble(avisit).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFamtavg5")).Text = Convert.ToDouble(acall).ToString("#,##0;(#,##0); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFamtavg6")).Text = Convert.ToDouble(aoth).ToString("#,##0;(#,##0); ");

                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFpertamtavg1")).Text = Convert.ToDouble(selper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFpertamtavg2")).Text = Convert.ToDouble(collper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFpertamtavg3")).Text = Convert.ToDouble(offper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFpertamtavg4")).Text = Convert.ToDouble(visitper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFpertamtavg5")).Text = Convert.ToDouble(callper).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvEmpEvalavg.FooterRow.FindControl("lblFpertamtavg6")).Text = Convert.ToDouble(othper).ToString("#,##0.00;(#,##0.00); ");

                    }






                    break;

                case "Collection":
                    break;
                case "Others":
                    List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lstgen = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>)Session["tblempmoneva"];
                    if (lstgen.Count == 0)
                        return;

                    double ttarget, tactual;
                    ttarget = Convert.ToDouble(lstgen.Select(p => p.tmark).Sum());
                    tactual = Convert.ToDouble(lstgen.Select(p => p.acmark).Sum());
                    double perontarget = (ttarget == 0) ? 0.00 : ((tactual / ttarget) * 100);
                    if (this.ddlyearmon.SelectedIndex == 1)
                    {
                        ((Label)this.gvempevagen.FooterRow.FindControl("lblgvftarget")).Text = Convert.ToDouble(ttarget).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvempevagen.FooterRow.FindControl("lblgvfactual")).Text = Convert.ToDouble(tactual).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvempevagen.FooterRow.FindControl("lblFperontar")).Text = Convert.ToDouble(perontarget).ToString("#,##0.00;(#,##0.00); ");
                    }
                    else
                    {
                        ((Label)this.gvempevagenavg.FooterRow.FindControl("lblgvftargetavg")).Text = Convert.ToDouble(ttarget).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvempevagenavg.FooterRow.FindControl("lblgvfactualavg")).Text = Convert.ToDouble(tactual).ToString("#,##0.00;(#,##0.00); ");
                        ((Label)this.gvempevagenavg.FooterRow.FindControl("lblFperontaravg")).Text = Convert.ToDouble(perontarget).ToString("#,##0.00;(#,##0.00); ");

                    }

                    break;


            }






        }
        private void ShowMonChartSales()
        {

            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];
            Chartmonsales.Series["Series1"].XValueMember = "yearmon";
            Chartmonsales.Series["Series1"].YValueMembers = "tmark";
            Chartmonsales.Series["Series2"].XValueMember = "yearmon";
            Chartmonsales.Series["Series2"].YValueMembers = "tper";

            Chartmonsales.Series["Series1"].LegendText = "Target";
            Chartmonsales.Series["Series2"].LegendText = "Actual";

            //Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            //Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            Chartmonsales.DataSource = lst;
            Chartmonsales.DataBind();

        }
        private void ShowMonChartgen()
        {

            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>)Session["tblempmoneva"];
            this.Chartmonoth.Series["Series1"].XValueMember = "yearmon";
            this.Chartmonoth.Series["Series1"].YValueMembers = "tmark";
            this.Chartmonoth.Series["Series2"].XValueMember = "yearmon";
            this.Chartmonoth.Series["Series2"].YValueMembers = "acmark";

            this.Chartmonoth.Series["Series1"].LegendText = "Target";
            this.Chartmonoth.Series["Series2"].LegendText = "Actual";
            this.Chartmonoth.DataSource = lst;
            this.Chartmonoth.DataBind();
        }

        private void ShowMonChartLineSales()
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];
            this.chrtlinegraphsales.Series["Series1"].XValueMember = "yearmon";
            this.chrtlinegraphsales.Series["Series1"].YValueMembers = "tmark";
            this.chrtlinegraphsales.Series["Series2"].XValueMember = "yearmon";
            this.chrtlinegraphsales.Series["Series2"].YValueMembers = "tper";
            this.chrtlinegraphsales.Series["Series3"].XValueMember = "yearmon";
            this.chrtlinegraphsales.Series["Series3"].YValueMembers = "avgmark";

            this.chrtlinegraphsales.Series["Series1"].LegendText = "Target";
            this.chrtlinegraphsales.Series["Series2"].LegendText = "Actual";
            this.chrtlinegraphsales.Series["Series3"].LegendText = "Average";
            this.chrtlinegraphsales.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            this.chrtlinegraphsales.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            this.chrtlinegraphsales.DataSource = lst;
            this.chrtlinegraphsales.DataBind();


        }

        private void ShowMonChartLinegen()
        {
            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>)Session["tblempmoneva"];
            this.chrtlinegraphoth.Series["Series1"].XValueMember = "yearmon";
            this.chrtlinegraphoth.Series["Series1"].YValueMembers = "tmark";
            this.chrtlinegraphoth.Series["Series2"].XValueMember = "yearmon";
            this.chrtlinegraphoth.Series["Series2"].YValueMembers = "acmark";
            this.chrtlinegraphoth.Series["Series3"].XValueMember = "yearmon";
            this.chrtlinegraphoth.Series["Series3"].YValueMembers = "avgmark";

            this.chrtlinegraphoth.Series["Series1"].LegendText = "Target";
            this.chrtlinegraphoth.Series["Series2"].LegendText = "Actual";
            this.chrtlinegraphoth.Series["Series3"].LegendText = "Average";
            this.chrtlinegraphoth.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            this.chrtlinegraphoth.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            this.chrtlinegraphoth.DataSource = lst;
            this.chrtlinegraphoth.DataBind();

        }



        protected void gvEmpEval_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lnkprint_Click(object sender, EventArgs e)
        {

            string dept = this.Request.QueryString["dept"].ToString();
            int ind = this.ddlyearmon.SelectedIndex;

            if (dept == "Sales" && ind == 1)
            {
                this.PrintMonEvaluationSales();

            }

            else if (dept == "Sales" && ind == 2)
            {
                this.PrintMonEvaluationSaleslineGraph();

            }

            else if ((dept == "Sales" || dept == "Collection" || dept == "Legal" || dept == "Others") && ind == 0)
            {
                this.printWeeklyGraph();

            }

            else if (dept == "Collection" && ind == 1)
            {

            }

            else if ((dept == "Others" || dept == "Legal") && ind == 1)
            {
                this.PrintMonEvaluationGen();

            }


            else if ((dept == "Others" || dept == "Legal") && ind == 2)
            {
                this.PrintMonEvaluationGenLine();

            }
            else
            {

            }
        }

        private void PrintMonEvaluationSales()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString().Substring(0, 4) + "%";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];

            string yearmon = this.Request.QueryString["ymon"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];



            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));
            string frmdate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            //Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstgrade = objUser.GetGpaList();


            //KPI
            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            if (ds2 == null) { return; }


            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptMonthWiseEmpEva();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;

            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindat).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";


            TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            CompName02.Text = comnam;

            TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            txtdate02.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";

            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Sub Report
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstgrade);
            rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
            rptAppMonitor.SetDataSource(lst);

            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintMonEvaluationSaleslineGraph()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];
            string yearmon = this.Request.QueryString["ymon"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));
            string frmdate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            //Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstgrade = objUser.GetGpaList();


            //KPI
            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            if (ds2 == null) { return; }


            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptGraphwiseline();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;

            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindat).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";


            TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            CompName02.Text = comnam;

            TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            txtdate02.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";

            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Sub Report
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstgrade);
            rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
            rptAppMonitor.SetDataSource(lst);

            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        private void PrintMonEvaluationGen()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string yearmon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();



            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>)Session["tblempmoneva"];
            string empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));


            //Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstgrade = objUser.GetGpaList();


            //KPI
            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            if (ds2 == null) { return; }







            string frmdate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");



            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptMonthWiseEmpEvaGen();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;
            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Salary:       , Benifits:     ";


            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + frmdate + " To " + todate + ")";

            TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            CompName02.Text = comnam;

            TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            txtdate02.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";


            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstgrade);
            rptAppMonitor.SetDataSource(lst);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string deptcode = hst["deptcode"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            //List<RealEntity.C_47_Kpi.EClassEmployeeMonEva> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEva>)Session["tblempmoneva"];
            //string userid = (this.Request.QueryString["Type"] == "Client") ? hst["usrid"].ToString() : "";

            //string srchEmp = "%";

            //string empid = this.Request.QueryString["empid"].ToString();
            //List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = objUser.GetEmpCode2(srchEmp, userid, deptcode, yearmon);
            //List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));


            ////Grade
            //List<RealEntity.C_47_Kpi.GradeWise> lstgrade = objUser.GetGpaList();


            ////KPI
            //DataSet ds2 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            //if (ds2 == null) { return; }


            //ReportDocument rptAppMonitor = new  RealERPRPT.R_05_MyPage.rptMonthWiseEmpEva02();
            //TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //CompName.Text = comnam;

            //TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            //txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindat).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";

            //TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";


            //TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            //CompName02.Text = comnam;

            //TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            //txtdate02.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";

            //TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////Sub Report
            //rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstgrade);
            //rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
            //rptAppMonitor.SetDataSource(lst);

            //Session["Report1"] = rptAppMonitor;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintMonEvaluationGenLine()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string yearmon = this.Request.QueryString["ymon"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();



            List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen> lst = (List<RealEntity.C_47_Kpi.EClassEmployeeMonEvagen>)Session["tblempmoneva"];
            string empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));


            //Grade
            List<RealEntity.C_47_Kpi.GradeWise> lstgrade = objUser.GetGpaList();


            //KPI
            DataSet ds2 = KpiData.GetTransInfo(comcod, "dbo_kpi.SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            if (ds2 == null) { return; }







            string frmdate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");



            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptMonthWiseEmpEvaLgraphGen();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;
            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Salary:       , Benifits:     ";


            //TextObject txtDesignation = rptAppMonitor.ReportDefinition.ReportObjects["txtdesignation"] as TextObject;
            //txtDesignation.Text = "Designation: " + (((DataTable)Session["tblsaleteam"]).Select("teamcode='" + Salesteamcode + "'"))[0]["designtion"];

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + frmdate + " To " + todate + ")";

            TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            CompName02.Text = comnam;

            TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            txtdate02.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";

            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstgrade);
            rptAppMonitor.SetDataSource(lst);
            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }

        private void printWeeklyGraph()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptcode = this.Request.QueryString["deptcode"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");



            DataTable dt = (DataTable)ViewState["tblweeklygp"];

            string yearmon = this.Request.QueryString["ymon"].ToString();
            string empid = this.Request.QueryString["empid"].ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)Session["tblemployee"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lstemp1 = lstemp.FindAll((p => p.empid == empid));
            string frmdate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(yearmon.Substring(4, 2) + "-" + "01-" + yearmon.Substring(0, 4)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            //Grade
            //  List<RealEntity.C_47_Kpi.GradeWise> lstgrade = objUser.GetGpaList();


            //KPI
            // DataSet ds2 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI", "RPTKPI", deptcode, "", "", "", "");
            // if (ds2 == null) { return; }


            ReportDocument rptAppMonitor = new RealERPRPT.R_62_Mis.rptWeeklyGraph();
            TextObject CompName = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;

            TextObject txtempname = rptAppMonitor.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = "Name: " + lstemp1[0].empname1 + ", " + "Designation: " + lstemp1[0].desg + ", Joinning Date: " + Convert.ToDateTime(lstemp1[0].joindat).ToString("dd-MMM-yyyy") + ", Salary:       , Benifits:     ";

            TextObject txtdate = rptAppMonitor.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";


            //TextObject CompName02 = rptAppMonitor.ReportDefinition.ReportObjects["txtCompName02"] as TextObject;
            //CompName02.Text = comnam;

            //TextObject txtdate02 = rptAppMonitor.ReportDefinition.ReportObjects["txtdate02"] as TextObject;
            //txtdate02.Text = " (From " + frmdate.Trim() + " To " + todate.Trim() + ")";

            TextObject txtuserinfo = rptAppMonitor.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //Sub Report
            // rptAppMonitor.Subreports["rptGradeSystem.rpt"].SetDataSource(lstgrade);
            //rptAppMonitor.Subreports["rptKPIIndex.rpt"].SetDataSource(ds2.Tables[0]);
            rptAppMonitor.SetDataSource(dt);

            Session["Report1"] = rptAppMonitor;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void gvEmpEval_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "Target";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 6;
                cell03.Attributes["style"] = "font-weight:bold;font-size:20px;";
                //cell03.Style.Add("font-weight", "bold");
                //cell03.Style.Add("font-size", "14px");



                TableCell cell04 = new TableCell();
                cell04.Text = "Actual";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 6;
                cell04.Attributes["style"] = "font-weight:bold;font-size:20px;";


                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;



                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvEmpEval.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gvEmpEvalavg_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "Target";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 6;
                cell03.Attributes["style"] = "font-weight:bold;font-size:20px;";
                //cell03.Style.Add("font-weight", "bold");
                //cell03.Style.Add("font-size", "14px");



                TableCell cell04 = new TableCell();
                cell04.Text = "Actual";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 6;
                cell04.Attributes["style"] = "font-weight:bold;font-size:20px;";


                TableCell cell05 = new TableCell();
                cell05.Text = "";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 1;

                TableCell cell06 = new TableCell();
                cell06.Text = "";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 1;

                TableCell cell07 = new TableCell();
                cell07.Text = "";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 1;

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvEmpEvalavg.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}
