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
using RealEntity;
using RealEntity.C_47_Kpi;
namespace RealERPWEB.F_39_MyPage
{
    public partial class EmpKpiEntry04 : System.Web.UI.Page
    {
        UserManagerKPI objUser = new UserManagerKPI();
        BL_EntryKpi obj1 = new BL_EntryKpi();
        ProcessAccess KpiData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "KPI ENTRY";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string deptcode = "9402%";

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.lbtnAddClient.Visible = (deptcode == "010100101001");
                this.GetIniEmpList();
                this.ShowGpa();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkprint_Click);

        }
        protected void lnkprint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string deptname = hst["deptname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string empid = this.ddlemplist.SelectedValue.ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)ViewState["tblgetEmpList"];
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst1 = lst.FindAll(p => p.empid == empid);
            string date = "Date :- " + this.txtdate.Text.Trim();
            string empname = "Name :- " + lst1[0].empname1.ToString().Trim();
            string dpt = "Department :- " + lst1[0].deptname.ToString().Trim();
            string desg = "Designation:- " + lst1[0].desg.ToString().Trim();
            string jdate = "Joining Date:- " + Convert.ToDateTime(lst1[0].joindat).ToString("dd-MMM-yyyy");
            string lbltarget = "100";
            string lblAch = this.lblAch.Text.Trim();
            string lblPer = this.lblPer.Text.Trim();
            string lblGrade = this.lblGrade.Text.Trim();


            ReportDocument rptKpiinfo = new RealERPRPT.R_39_MyPage.rptEmpDetails();

            TextObject CompName = rptKpiinfo.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            CompName.Text = comnam;
            TextObject txtDate = rptKpiinfo.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = date;
            TextObject txtempname = rptKpiinfo.ReportDefinition.ReportObjects["txtempname"] as TextObject;
            txtempname.Text = empname;
            TextObject txtdeptname = rptKpiinfo.ReportDefinition.ReportObjects["txtdeptname"] as TextObject;
            txtdeptname.Text = dpt;
            TextObject txtdesg = rptKpiinfo.ReportDefinition.ReportObjects["txtDesg"] as TextObject;
            txtdesg.Text = desg;
            TextObject txtjoindate = rptKpiinfo.ReportDefinition.ReportObjects["txtjoindate"] as TextObject;
            txtjoindate.Text = jdate;
            TextObject txtTarget = rptKpiinfo.ReportDefinition.ReportObjects["txtTarget"] as TextObject;
            txtTarget.Text = lbltarget;
            TextObject txtAchived = rptKpiinfo.ReportDefinition.ReportObjects["txtAchived"] as TextObject;
            txtAchived.Text = lblAch;
            TextObject txtAchivedin = rptKpiinfo.ReportDefinition.ReportObjects["txtAchivedin"] as TextObject;
            txtAchivedin.Text = lblPer;
            TextObject txtGrade = rptKpiinfo.ReportDefinition.ReportObjects["txtGrade"] as TextObject;
            txtGrade.Text = lblGrade;


            List<RealEntity.C_47_Kpi.EClassShowEmpData> lst2 = (List<RealEntity.C_47_Kpi.EClassShowEmpData>)ViewState["tblempworklist"];
            List<RealEntity.C_47_Kpi.GradeWise> lst3 = (List<RealEntity.C_47_Kpi.GradeWise>)ViewState["tblGpa"];


            TextObject txtuserinfo = rptKpiinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptKpiinfo.Subreports["rptEmpDetailslist.rpt"].SetDataSource(lst2);
            rptKpiinfo.Subreports["rptGradeSystem.rpt"].SetDataSource(lst3);

            rptKpiinfo.SetDataSource(lst2);

            Session["Report1"] = rptKpiinfo;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }
        private string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetIniEmpList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string type = this.Request.QueryString["Type"];
            string userid = (type == "Sales" || type == "CR" || type == "General" || type == "Legal") ? hst["usrid"].ToString() : "";

            string deptcode = "9402%";
            string srchEmp = "%" + this.txtSrchSalesTeam.Text.Trim() + "%";
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMM");
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst3 = objUser.GetEmpCode2(srchEmp, userid, deptcode, Date);
            ViewState["tblgetEmpList"] = lst3;
            this.GetEmpList();


        }

        private void GetEmpList()

        {
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)ViewState["tblgetEmpList"];
            string srchEmp = this.txtSrchSalesTeam.Text.Trim();
            if (srchEmp.Length > 0)
            {

                IEnumerable<RealEntity.C_47_Kpi.EClassEmpCode2> ProjectQuery = (from employee in lst
                                                                                where employee.empname1.ToUpper().Contains(srchEmp.ToUpper())
                                                                                orderby employee.empid ascending
                                                                                select employee);
                this.ddlemplist.DataTextField = "empname";
                this.ddlemplist.DataValueField = "empid";
                this.ddlemplist.DataSource = ProjectQuery.ToList();
                this.ddlemplist.DataBind();

            }

            else
            {
                this.ddlemplist.DataTextField = "empname";
                this.ddlemplist.DataValueField = "empid";
                this.ddlemplist.DataSource = lst;
                this.ddlemplist.DataBind();
            }

            string Empid = this.ddlemplist.SelectedValue.ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst1 = lst.FindAll((p => p.empid == Empid));
            if (lst1.Count == 0)
            {
                this.lblEmpinf.Text = "";
                return;
            }
            this.lblEmpinf.Text = "Department :-" + lst1[0].deptname + "<br>" + "Designation:- " + lst1[0].desg + "<br>" + "Joining Date:- " + Convert.ToDateTime(lst1[0].joindat).ToString("dd-MMM-yyyy");
            this.showGvEmpwlist();
            this.EmpEvalSheet();
        }


        private void ShowGpa()
        {

            string comcod = Getcomcod();
            List<RealEntity.C_47_Kpi.GradeWise> lst3 = obj1.GetGpaList();


            this.grvGpa.DataSource = lst3;

            this.grvGpa.DataBind();

            ViewState["tblGpa"] = lst3;
        }

        protected void imgSearchSalesTeam_Click(object sender, EventArgs e)
        {

            string sysdate = System.DateTime.Today.ToString("yyyyMM");
            string curdate = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMM");

            if (sysdate == curdate)
            {
                this.GetEmpList();
            }
            else
                this.GetIniEmpList();

        }
        protected void lnkok_Click(object sender, EventArgs e)
        {

            this.showGvEmpwlist();
            this.EmpEvalSheet();
        }

        private void showGvEmpwlist()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = "9402%";

            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)ViewState["tblgetEmpList"];
            string comcod = Getcomcod();
            string Empid = this.ddlemplist.SelectedValue.ToString();
            string MonthID = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMM");


            // : (deptcode == "010200101002") ? "SHOWEMPEVALLEGAL" 

            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string CallType = ((deptcode == "010100101001") || (deptcode.Substring(0, 4) == "9402")) ? "SHOWEMPDATALISTSALSE" : (deptcode == "010200101002") ? "SHOWEMPDETAILSLEGAL" : "SHOWEMPDATALIST";


            List<RealEntity.C_47_Kpi.EClassShowEmpData> lst3 = objUser.GetEmpWorklist(CallType, MonthID, Empid, Date);


            ViewState["tblempworklist"] = lst3;


            this.gvEmpWlist.DataSource = HiddenSameData(lst3);
            this.gvEmpWlist.DataBind();
            if (lst3.Count == 0)
                return;

            ((Label)this.gvEmpWlist.FooterRow.FindControl("lblgvFwmarks")).Text = lst3.Select(p => p.marks).Sum().ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvEmpWlist.FooterRow.FindControl("lblgvFacmarks")).Text = lst3.Select(p => p.acmarks).Sum().ToString("#,##0.00;(#,##0.00); ");

        }


        private void EmpEvalSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string deptcode = "9402%";
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)ViewState["tblgetEmpList"];
            string comcod = Getcomcod();
            string Empid = this.ddlemplist.SelectedValue.ToString();
            string MonthID = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMM"); ;
            // string DptCode = (((List<RealEntity.C_47_Kpi.EClassEmpCode2>)ViewState["tblgetEmpList"]).FindAll(p => p.empid == Empid))[0].deptcode;

            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string CallType = (deptcode == "010100101001") ? "SHOWEMPEVALSALES" : (deptcode == "010200101002") ? "SHOWEMPEVALLEGAL" : "SHOWEMPEVAL";


            List<RealEntity.C_47_Kpi.EClassShowEval> lst3 = objUser.GetEmpEval(CallType, MonthID, Empid, Date);
            if (lst3.Count == 0)
            {

                this.lblAch.Text = "";
                this.lblPer.Text = "";
                this.lblGrade.Text = "";
                return;

            }

            this.lblAch.Text = lst3[0].actmark.ToString("#,##0.00;(#,##0.00); ");
            this.lblPer.Text = lst3[0].acpar.ToString("#,##0.00;(#,##0.00); ") + " %";
            this.lblGrade.Text = lst3[0].grade;



            string value = this.lblGrade.Text.ToString();

            if (value == "Outstanding")
            {
                lblGrade.Attributes["style"] = "color:#842DCE;";

            }
            else if (value == "Excellent")
            {
                lblGrade.Attributes["style"] = "color:maroon;";

            }
            else if (value == "Very Good")
            {
                lblGrade.Attributes["style"] = "color:blue;";

            }
            else if (value == "Good")
            {
                lblGrade.Attributes["style"] = "color:green;";

            }
            else if (value == "Average")
            {
                lblGrade.Attributes["style"] = "color:#7D1B7E;";

            }
            else if (value == "Below Average")
            {
                lblGrade.Attributes["style"] = "color:magenta;";

            }
            else if (value == "Non-Performer")
            {
                lblGrade.Attributes["style"] = "color:red;";

            }






        }
        private List<RealEntity.C_47_Kpi.EClassShowEmpData> HiddenSameData(List<RealEntity.C_47_Kpi.EClassShowEmpData> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string actcode1 = "";
            foreach (RealEntity.C_47_Kpi.EClassShowEmpData c1 in lst3)
            {
                if (i == 0)
                {
                    actcode1 = c1.actcode1;
                    i++;
                    continue;

                }
                else if (c1.actcode1 == actcode1)
                {
                    c1.actdesc1 = "";
                }
                actcode1 = c1.actcode1;

            }

            return lst3;

        }

        protected void ddlemplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst = (List<RealEntity.C_47_Kpi.EClassEmpCode2>)ViewState["tblgetEmpList"];
            string Empid = this.ddlemplist.SelectedValue.ToString();
            List<RealEntity.C_47_Kpi.EClassEmpCode2> lst1 = lst.FindAll((p => p.empid == Empid));
            this.lblEmpinf.Text = "Department :-" + lst1[0].deptname + "<br>" + "Designation:- " + lst1[0].desg + "<br>" + "Joining Date:- " + Convert.ToDateTime(lst1[0].joindat).ToString("dd-MMM-yyyy");
            this.showGvEmpwlist();
            this.EmpEvalSheet();
        }

        protected void gvEmpWlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label mdesc = (Label)e.Row.FindControl("lblppercent");
                HyperLink hlnkgvwrkdesc = (HyperLink)e.Row.FindControl("hlnkgvwrkdesc");
                string kpigrp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();



                double ppercent = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ppercent"));

                if (kpigrp == "810100601003" || kpigrp == "810100601004" || kpigrp == "810100601005")
                {
                    hlnkgvwrkdesc.Style.Add("color", "blue");
                    string comcod = ASTUtility.Left(this.Getcomcod(), 2);
                    if (comcod == "7" || comcod == "8")
                        hlnkgvwrkdesc.NavigateUrl = "~/F_05_MyPage/LinkEmpKpiEntry.aspx?Type=Mgt&&empid=" + this.ddlemplist.SelectedValue.ToString() + "&kpigrp=" + kpigrp;
                    else
                        hlnkgvwrkdesc.NavigateUrl = "~/F_05_MyPage/LinkEmpKpimfEntry.aspx?Type=Mgt&&empid=" + this.ddlemplist.SelectedValue.ToString() + "&kpigrp=" + kpigrp;

                }
                else if (kpigrp == "810100603001" || kpigrp == "810100603002" || kpigrp == "810100603003" || kpigrp == "810100603004" || kpigrp == "810100603005" || kpigrp == "810100603006" || kpigrp == "810100603901")
                {

                    hlnkgvwrkdesc.Style.Add("color", "blue");

                    hlnkgvwrkdesc.NavigateUrl = "~/F_05_MyPage/LinkEmpKpiEntryLeg.aspx?Type=Mgt&&empid=" + this.ddlemplist.SelectedValue.ToString() + "&kpigrp=" + kpigrp;

                }

                if (ppercent == 0)
                {
                    return;
                }

                if (ppercent < 50)
                {
                    mdesc.Style.Add("color", "red");
                }


            }
        }
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            string Empid = this.ddlemplist.SelectedValue.ToString();
            string DayID = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMMdd");
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('EmpKpiEntry04Det.aspx?dayid=" + DayID + "&empid=" + Empid + "', target='_self');</script>";
        }
        protected void lblGrp_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string Empid = this.ddlemplist.SelectedValue.ToString();

            string ymon = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMM");
            string DptCode = hst["deptcode"].ToString();
            string dept = (DptCode == "010100101001") ? "Sales" : (DptCode == "010100101002") ? "Collection" : (DptCode == "010200101002") ? "Legal" : "Others";


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkIndGraph.aspx?dept=" + dept + "&deptcode=" + DptCode + "&ymon=" + ymon + "&empid=" + Empid + "', target='_self');</script>";

            //DataSet ds1 = KpiData.GetTransInfo(comcod, "SP_REPORT_EMP_KPI03", "SHOWGPAPHWITHDATA", DptCode, DayID, Empid, Date);

            //ViewState["tbModalGraph"] = ds1.Tables[0];
            ////this.showChart();
            //string radalertscript = "<script language='javascript'>function f(){loadModal7(); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        protected void lbtnFresh_Click(object sender, EventArgs e)
        {
            this.showGvEmpwlist();
            this.EmpEvalSheet();
        }
        protected void lblPerformance_Click(object sender, EventArgs e)
        {
            string comcod = this.Getcomcod();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = this.ddlemplist.SelectedValue.ToString();
            string DptCode = hst["deptcode"].ToString();
            string dept = (DptCode == "010100101001") ? "Sales" : (DptCode == "010100101002") ? "Collection" : (DptCode == "010200101002") ? "Legal" : "Others";
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy"); ;
            ((LinkButton)this.Master.FindControl("lnkPrint")).Text = "";

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_21_Kpi/LinkEmpMonthWiseEvaDet.aspx?Type=Mgt&dept=" + dept + "&date=" + date + "&empid=" + Empid + "', target='blank');</script>";
        }
        protected void lnkbtnmonsummary_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.Getcomcod();
            string Empid = this.ddlemplist.SelectedValue.ToString();

            string ymon = Convert.ToDateTime(this.txtdate.Text).ToString("yyyyMM");
            string DptCode = hst["deptcode"].ToString();
            string dept = (DptCode == "010100101001") ? "Sales" : (DptCode == "010100101002") ? "Collection" : "Others";
            if (dept == "010100101001")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_21_Kpi/RptEmpMonthWiseEva.aspx?Type=Mgt', target='_blank');</script>";
            }
            else if (dept == "010100101002")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_05_MyPage/RptMIS02.aspx?Type=EmpEvaluation', target='_blank');</script>";

            }

            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_05_MyPage/RptMIS02.aspx?Type=EmpEvaluation', target='_blank');</script>";

            }



        }
        protected void lbtnAddClient_Click(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Text = "";
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../F_05_MyPage/ClientCodeBook.aspx', target='blank');</script>";
        }
        protected void grvGpa_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Index = (Label)e.Row.FindControl("lgvgdesc");
                Label gpa = (Label)e.Row.FindControl("lgvgrade");

                string value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrange")).ToString();

                if (value == "100 Or Above")
                {
                    gpa.Attributes["style"] = "color:#842DCE;";

                }
                else if (value == "80 To 99")
                {
                    gpa.Attributes["style"] = "color:maroon;";

                }
                else if (value == "70 To 79")
                {
                    gpa.Attributes["style"] = "color:blue;";

                }
                else if (value == "60 To 69")
                {
                    gpa.Attributes["style"] = "color:green;";

                }
                else if (value == "40 To 59")
                {
                    gpa.Attributes["style"] = "color:#7D1B7E;";

                }
                else if (value == "30 To 39")
                {
                    gpa.Attributes["style"] = "color:magenta;";

                }
                else if (value == "Below 30")
                {
                    gpa.Attributes["style"] = "color:red;";

                }
            }

        }
    }
}