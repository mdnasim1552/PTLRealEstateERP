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
using RealERPRPT;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptTarVsAchievement : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "SalesTarVsAch" ? "Sales Target Vs. Achievement " : type == "CollTarVsAch" ? "Collection Target Vs. Achievement "
                     : type == "ConTarVsAch" ? "Construction Target Vs. Achievement "
                     : type == "ConPlan" ? "Construction Target Vs. Achievement "
                     : type == "LPPlan" ? "Land Procurement Target Vs. Achievement " : type == "ProPlan" ? "Profit Planing " : "HR Information");


                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.ViewSection();
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            }


        }
        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void ViewSection()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalesTarVsAch":

                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "CollTarVsAch":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "HRInfo":
                    this.lblfrmdate.Visible = false;
                    this.txtDate.Visible = false;
                    this.lbltodate.Visible = false;
                    this.txttodate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "ConTarVsAch":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "ConPlan":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "LPPlan":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "ProPlan":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;





            }
        }



        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalesTarVsAch":
                    this.ShowSalTarVsAch();
                    break;

                case "CollTarVsAch":
                    this.ShowCollTarVsAch();
                    break;


                case "HRInfo":
                    this.ShowHrInfo();
                    break;

                case "ConTarVsAch":
                    this.ShowConTarVsAch();
                    break;
                case "ConPlan":
                    this.ShowConPlan();
                    break;

                case "LPPlan":
                    this.ShowLPPlan();
                    break;

                case "ProPlan":
                    this.ShowProPlan();
                    break;

            }





        }

        private void ShowSalTarVsAch()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTSALESPLANING", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalTvsAch.DataSource = null;
                this.gvSalTvsAch.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void ShowCollTarVsAch()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTCOLLPLANING", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvrcoll.DataSource = null;
                this.gvrcoll.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();


        }
        private void ShowHrInfo()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTHRINFO", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvHremp.DataSource = null;
                this.gvHremp.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();



        }
        private void ShowConTarVsAch()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTCONSTRUCPLANING", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMMPlanVsAch.DataSource = null;
                this.gvMMPlanVsAch.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();


        }

        private void ShowConPlan()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTCONSTRUCPLANING", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConTvsAch.DataSource = null;
                this.gvConTvsAch.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void ShowLPPlan()
        {

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTLANDPROCUREMENT", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLPTvsAch.DataSource = null;
                this.gvLPTvsAch.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void ShowProPlan()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS03", "RPTPROFITPLANING", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvProPlan.DataSource = null;
                this.gvProPlan.DataBind();
                return;
            }

            ViewState["tblData"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {


            DataTable dt = (DataTable)ViewState["tblData"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "SalesTarVsAch":
                    this.gvSalTvsAch.DataSource = dt;
                    this.gvSalTvsAch.DataBind();
                    break;

                case "CollTarVsAch":
                    this.gvrcoll.DataSource = dt;
                    this.gvrcoll.DataBind();
                    break;


                case "HRInfo":
                    this.gvHremp.DataSource = dt;
                    this.gvHremp.DataBind();
                    break;

                case "ConTarVsAch":
                    this.gvMMPlanVsAch.DataSource = dt;
                    this.gvMMPlanVsAch.DataBind();
                    break;
                case "ConPlan":
                    this.gvConTvsAch.DataSource = dt;
                    this.gvConTvsAch.DataBind();
                    break;

                case "LPPlan":
                    this.gvLPTvsAch.DataSource = dt;
                    this.gvLPTvsAch.DataBind();
                    break;
                case "ProPlan":

                    this.gvProPlan.DataSource = dt;
                    this.gvProPlan.DataBind();
                    break;
            }






        }

        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblData"];

            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {

                case "SalesTarVsAch":
                    //((Label)this.gvparking.FooterRow.FindControl("lgvftoqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tqty)", "")) ?
                    //                 0 : dt1.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "CollTarVsAch":

                    break;





            }


        }







        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void gvSalTvsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //Label comname = (Label)e.Row.FindControl("lkgvcomnamesale");
                HyperLink tsaleamt = (HyperLink)e.Row.FindControl("hlnkgvtsaleamt");

                Label tatosaleamt = (Label)e.Row.FindControl("lgvtatosaleamt");
                HyperLink salamt = (HyperLink)e.Row.FindControl("hlnkgvDSAmt");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraph");
                Label perotsal = (Label)e.Row.FindControl("lgvperotsal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    //comname.Font.Bold = true;
                    tsaleamt.Font.Bold = true;
                    tatosaleamt.Font.Bold = true;
                    salamt.Font.Bold = true;
                    perotsal.Font.Bold = true;
                    //comname.Style.Add("text-align", "right");
                }
                else
                {

                    string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double ttargetamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvtsaleamt")).Text);
                    string ttargetamt1 = ((ttargetamt == 0) ? 0 : ttargetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                    //double targetamt = Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).Text);
                    //string targetamt1 = ((targetamt == 0) ? 0 : targetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double acsalamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvDSAmt")).Text);
                    string acsalamt1 = ((acsalamt == 0) ? 0 : acsalamt / 1000000).ToString("#,##0.00;(#,##0.00);");

                    //Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).ToString()).ToString("#,##0;(#,##0);");
                    tsaleamt.Style.Add("color", "blue");
                    salamt.Style.Add("color", "blue");
                    graph.Style.Add("color", "blue");
                    tsaleamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Sales&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    salamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptDayWSale&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&ttargetamt=" + ttargetamt1 + "&acsalamt=" + acsalamt1;



                }

            }
        }
        protected void gvrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                HyperLink tcollamt = (HyperLink)e.Row.FindControl("hlnkgvtcollamt");
                Label tatocollamt = (Label)e.Row.FindControl("lgvtatocollamt");
                HyperLink achamt = (HyperLink)e.Row.FindControl("hlnkgvaccollAmt");

                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphcoll");
                Label perotcoll = (Label)e.Row.FindControl("lgvperotcoll");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {



                    tcollamt.Font.Bold = true;
                    tatocollamt.Font.Bold = true;
                    achamt.Font.Bold = true;
                    perotcoll.Font.Bold = true;

                }
                else
                {

                    string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double ttargetamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvtcollamt")).Text);
                    string ttargetamt1 = ((ttargetamt == 0) ? 0 : ttargetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double targetamt = Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatocollamt")).Text);
                    string targetamt1 = ((targetamt == 0) ? 0 : targetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                    double accollamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvaccollAmt")).Text);
                    string accollamt1 = ((accollamt == 0) ? 0 : accollamt / 1000000).ToString("#,##0.00;(#,##0.00);");


                    tcollamt.Style.Add("color", "blue");
                    achamt.Style.Add("color", "blue");
                    graph.Style.Add("color", "blue");
                    tcollamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Collection&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    achamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=CollSummary&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&ttargetamt=" + ttargetamt1 + "&acsalamt=" + accollamt1;



                }

            }

        }
        protected void gvHremp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("lnkgvcomname");
                Label toemp = (Label)e.Row.FindControl("lgvtoemp");
                Label netsalary = (Label)e.Row.FindControl("lgvnetsalary");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrcomcod")).ToString();

                if (code == "")
                {
                    return;
                }

                comname.Font.Bold = true;
                toemp.Font.Bold = true;
                netsalary.Font.Bold = true;
                comname.Style.Add("color", "blue");
                comname.NavigateUrl = "~/F_45_GrAcc/LinkRptMgtInterface.aspx?comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");






            }

        }

        protected void gvMMPlanVsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamecons");
                Label masterplan = (Label)e.Row.FindControl("lgvmasterplan");
                Label monplan = (Label)e.Row.FindControl("lgvmonplan");
                Label ExecutionpAC = (Label)e.Row.FindControl("lgvExecutionpAC");
                Label PerMasPlan = (Label)e.Row.FindControl("lgvPerMasPlan");
                Label PerMonthPlan = (Label)e.Row.FindControl("lgvPerMonthPlan");
                HyperLink flrwiseprogress = (HyperLink)e.Row.FindControl("hlnkgvflrwiseprogress");
                HyperLink sysgentarget = (HyperLink)e.Row.FindControl("hlnkgvsysgentarget");
                HyperLink ineffect = (HyperLink)e.Row.FindControl("hlnkgvineffect");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    masterplan.Font.Bold = true;
                    monplan.Font.Bold = true;
                    ExecutionpAC.Font.Bold = true;
                    PerMasPlan.Font.Bold = true;
                    PerMonthPlan.Font.Bold = true;


                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    flrwiseprogress.Style.Add("color", "blue");
                    sysgentarget.Style.Add("color", "blue");
                    ineffect.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    flrwiseprogress.NavigateUrl = "~/F_45_GrAcc/LinkConstruProgress.aspx?comcod=" + code + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    sysgentarget.NavigateUrl = "~/F_45_GrAcc/LinkGrpSysGenTarget.aspx?Type=SymGenTar&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                    ineffect.NavigateUrl = "~/F_45_GrAcc/LinkGrpInflaEffect.aspx?Type=RemainingCost&comcod=" + code + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }





            }
        }
        protected void gvConTvsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //Label comname = (Label)e.Row.FindControl("lkgvcomnamesale");
                HyperLink monplan = (HyperLink)e.Row.FindControl("hlnkgvmonplancs");
                HyperLink monach = (HyperLink)e.Row.FindControl("hlnkgvmonachcs");
                HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphcs");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }


                string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
                double ttargetamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvmonplancs")).Text);
                string ttargetamt1 = ((ttargetamt == 0) ? 0 : ttargetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                //double targetamt = Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).Text);
                //string targetamt1 = ((targetamt == 0) ? 0 : targetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
                double acsalamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvmonachcs")).Text);
                string acsalamt1 = ((acsalamt == 0) ? 0 : acsalamt / 1000000).ToString("#,##0.00;(#,##0.00);");

                //Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).ToString()).ToString("#,##0;(#,##0);");
                monplan.Style.Add("color", "blue");
                monach.Style.Add("color", "blue");
                graph.Style.Add("color", "blue");
                monplan.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                monach.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&ttargetamt=" + ttargetamt1 + "&acsalamt=" + acsalamt1;




            }

        }
        protected void gvLPTvsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink monplan = (HyperLink)e.Row.FindControl("hlnkgvmonplanlp");
            HyperLink monach = (HyperLink)e.Row.FindControl("hlnkgvmonachlp");
            HyperLink graph = (HyperLink)e.Row.FindControl("hlnkgvgraphlp");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
            if (code == "")
            {
                return;
            }


            string Capacity = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "capacity")) / 1000000).ToString("#,##0.00;(#,##0.00);");
            string Masbgd = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "masbgd")) / 1000000).ToString("#,##0.00;(#,##0.00);");
            string Bep = (Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "bep")) / 1000000).ToString("#,##0.00;(#,##0.00);");
            double ttargetamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvmonplanlp")).Text);
            string ttargetamt1 = ((ttargetamt == 0) ? 0 : ttargetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
            //double targetamt = Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).Text);
            //string targetamt1 = ((targetamt == 0) ? 0 : targetamt / 1000000).ToString("#,##0.00;(#,##0.00);");
            double acsalamt = Convert.ToDouble("0" + ((HyperLink)e.Row.FindControl("hlnkgvmonachlp")).Text);
            string acsalamt1 = ((acsalamt == 0) ? 0 : acsalamt / 1000000).ToString("#,##0.00;(#,##0.00);");

            //Convert.ToDouble("0" + ((Label)e.Row.FindControl("lgvtatosaleamt")).ToString()).ToString("#,##0;(#,##0);");
            monplan.Style.Add("color", "blue");
            monach.Style.Add("color", "blue");
            graph.Style.Add("color", "blue");
            // monplan.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //monach.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            graph.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisGraph.aspx?comcod=" + code + "&capacity=" + Capacity + "&masbgd=" + Masbgd + "&bep=" + Bep + "&ttargetamt=" + ttargetamt1 + "&acsalamt=" + acsalamt1;




        }
        protected void gvProPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label resdesc = (Label)e.Row.FindControl("lgvresdesc");
                HyperLink bgdamt = (HyperLink)e.Row.FindControl("lnkgvbgdamt");
                HyperLink acamt = (HyperLink)e.Row.FindControl("lnkgvacamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {


                    resdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    acamt.Font.Bold = true;
                    resdesc.Style.Add("text-align", "right");

                }

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "02AAAAAA")
                {
                    bgdamt.Style.Add("color", "blue");
                    bgdamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisYearlyPlnBudget.aspx?Type=BgdIncome&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }
                else if (code == "02000000")
                {
                    acamt.Style.Add("color", "blue");
                    acamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisProjectStatus.aspx?Type=MProStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }

                else if (code == "03AAAAAA")
                {
                    acamt.Style.Add("color", "blue");
                    acamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=GPNPALLPRO&comcod=" + comcod + "&date=" + this.txttodate.Text.Trim();


                }












            }
        }
    }
}

