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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_32_Mis
{
    public partial class LinkImpExeStatus : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                string Type = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = Type == "ImpPlan" ? "MONTHLY IMPLEMENTATION PLAN " : Type == "Execution" ? "WORK EXECUTION "
                    : Type == "PlanVSEx" ? "MONTHLY PLAN VS EXECUTION" : Type == "BgdVSEx" ? "BUDGET VS EXECUTION"
                    : Type == "MaPlanVsPlanVsEx" ? "MASTER PLAN, MONTHLY PLAN & EXECUTION"

                    : Type == "DayWiseExecution" ? "DAY WISE EXECUTION"
                    : Type == "ImpPlan02" ? "System Generated Work Plan Vs. Real Plan" : "MATERIALS EVALUTION";







                this.GetProjectName();
                this.ViewSection();

                if (Type == "BgdVSEx" || Type == "MatEva")
                {
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;

                }

                if (Type == "MatEva")
                {
                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;

                }

                if (this.Request.QueryString["pactcode"].ToString().Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }




            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void ViewSection()
        {
            string Type = Request.QueryString["Type"].ToString();

            switch (Type)
            {
                case "ImpPlan":
                case "Execution":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "PlanVSEx":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "BgdVSEx":
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "MatEva":
                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.lbldateto.Visible = false;
                    this.txttodate.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "MaPlanVsPlanVsEx":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;


                case "DayWiseExecution":

                    this.ddlProjectName.SelectedValue = this.Request.QueryString["pactcode"].ToString();
                    this.txtfrmDate.Text = (this.Request.QueryString["Date1"]);
                    this.txttodate.Text = (this.Request.QueryString["Date2"]);

                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 5;
                    this.chkNarration.Visible = true;
                    this.chkNarration.Checked = true;
                    break;



                case "ImpPlan02":
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

            }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);


        }
        private void ShowFloorcode()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETFLOORCOD", pactcode, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataRow dr2 = dt.NewRow();
            dr2["flrcod"] = "000";
            dr2["flrdes"] = "All Floors-Sum";
            DataRow dr3 = dt.NewRow();
            dr3["flrcod"] = "AAA";
            dr3["flrdes"] = "All Floors-Details";
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            DataView dv = dt.DefaultView;
            dv.Sort = "flrcod";
            dt = dv.ToTable();
            this.ddlFloorListRpt.DataTextField = "flrdes";
            this.ddlFloorListRpt.DataValueField = "flrcod";
            this.ddlFloorListRpt.DataSource = dt;
            this.ddlFloorListRpt.DataBind();
            this.ddlFloorListRpt.SelectedValue = "AAA";
        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.ShowValue();

        }

        private void ShowValue()
        {


            this.lbljavascript.Text = "";
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ImpPlan":
                case "Execution":
                    this.ShowImpPlan();
                    break;
                case "PlanVSEx":
                    this.ShowExecution();
                    break;
                case "BgdVSEx":
                    this.ShowBgdVsExecution();
                    break;

                case "MatEva":
                    this.ShowMatEva();
                    break;
                case "MaPlanVsPlanVsEx":
                    this.maplanvaplanes();
                    break;


                case "DayWiseExecution":
                    this.ShowDWiseExecution();
                    break;


                case "ImpPlan02":

                    this.ShowTarVsImp();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetCompCode();
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report  " + Type;
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void ShowImpPlan()
        {
            string Type1 = Request.QueryString["Type"].ToString();
            string calltype = (Type1 == "ImpPlan") ? "RPTIMPPLANSTATUS" : "RPTEXESTATUS";
            Session.Remove("tblplanexe");
            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", calltype, pactcode, frmdate, todate, flrcod, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvImpPlan.DataSource = null;
                this.gvImpPlan.DataBind();
                return;
            }

            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private void ShowTarVsImp()
        {


            Session.Remove("tblplanexe");
            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTTARVSIMP", pactcode, frmdate, todate, flrcod, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvTvsImp.DataSource = null;
                this.gvTvsImp.DataBind();
                return;
            }

            Session["tblplanexe"] = this.HiddenSameData(ds1.Tables[0]);
            this.LoadGrid();

        }
        private void ShowExecution()
        {

            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTPLANVSEXESTATUS", pactcode, frmdate, todate, flrcod, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvPlanVSEx.DataSource = null;
                this.gvPlanVSEx.DataBind();
                return;
            }

            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private void ShowBgdVsExecution()
        {

            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string flrcod = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTBGDVSEXESTATUS", pactcode, date, "", flrcod, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvBgdVsEx.DataSource = null;
                this.gvBgdVsEx.DataBind();
                return;
            }

            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();

        }
        private void ShowMatEva()
        {

            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTMATEVALUTION", pactcode, date, "", "000", mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvMatEva.DataSource = null;
                this.gvMatEva.DataBind();
                return;
            }

            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();


        }

        private void maplanvaplanes()
        {

            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string fdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string floorcode = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string CallType = (ASTUtility.Left(pactcode, 2) == "41") ? "RPTMAPLNVSMPLNVSEXE" : "RPTFINMAPLNVSMPLNVSEXE";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", CallType, pactcode, fdate, todate, floorcode, mRptGroup, "", "", "", "");
            if (ds1 == null)
            {
                this.gvmplanvaexe.DataSource = null;
                this.gvmplanvaexe.DataBind();
                return;
            }

            Session["tblplanexe"] = ds1.Tables[0];
            this.LoadGrid();


        }

        private void ShowDWiseExecution()
        {


            string comcod = this.GetCompCode();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string fdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string floorcode = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            string withnarration = this.chkNarration.Checked ? "withnarration" : "";
            string catagory = "%";
            //mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTDWISEEXECUTION", pactcode, fdate, todate, floorcode, withnarration, catagory, "", "", "");

            //DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "RPTDWISEEXECUTION", pactcode, fdate, todate, floorcode, withnarration, "", "", "", "");
            if (ds1 == null)
            {
                this.gvExecution.DataSource = null;
                this.gvExecution.DataBind();
                return;
            }

            Session["tblplanexe"] = this.HiddenSameData(ds1.Tables[0]);
            this.LoadGrid();


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {


                case "DayWiseExecution":
                    string isuno = dt1.Rows[0]["isuno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["isuno"].ToString() == isuno)
                        {
                            dt1.Rows[j]["isuno1"] = "";
                            dt1.Rows[j]["isudat"] = "";

                        }
                        isuno = dt1.Rows[j]["isuno"].ToString();
                    }
                    break;


                case "ImpPlan02":
                    string rptcod = dt1.Rows[0]["rptcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rptcod"].ToString() == rptcod)
                        {
                            dt1.Rows[j]["rptdesc1"] = "";
                            dt1.Rows[j]["rptunit"] = "";
                        }
                        rptcod = dt1.Rows[j]["rptcod"].ToString();
                    }

                    break;


            }


            return dt1;

        }



        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblplanexe"];
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ImpPlan":
                case "Execution":
                    this.gvImpPlan.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvImpPlan.DataSource = dt;
                    this.gvImpPlan.DataBind();
                    this.FooterCalcul(dt);
                    break;

                case "PlanVSEx":
                    this.gvPlanVSEx.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPlanVSEx.DataSource = dt;
                    this.gvPlanVSEx.DataBind();
                    this.FooterCalcul(dt);
                    break;

                case "BgdVSEx":
                    this.gvBgdVsEx.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBgdVsEx.DataSource = dt;
                    this.gvBgdVsEx.DataBind();
                    this.FooterCalcul(dt);
                    break;

                case "MatEva":
                    this.gvMatEva.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMatEva.DataSource = dt;
                    this.gvMatEva.DataBind();
                    this.FooterCalcul(dt);
                    break;

                case "MaPlanVsPlanVsEx":
                    this.gvmplanvaexe.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvmplanvaexe.DataSource = dt;
                    this.gvmplanvaexe.DataBind();
                    this.FooterCalcul(dt);
                    break;


                case "DayWiseExecution":
                    this.gvExecution.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvExecution.DataSource = dt;
                    this.gvExecution.DataBind();
                    this.FooterCalcul(dt);
                    break;


                case "ImpPlan02":
                    this.gvTvsImp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvTvsImp.DataSource = dt;
                    this.gvTvsImp.DataBind();
                    this.FooterCalcul(dt);
                    break;

            }



        }

        private void FooterCalcul(DataTable dt)
        {
            string Type = Request.QueryString["Type"].ToString();
            if (dt.Rows.Count == 0)
                return;


            switch (Type)
            {


                case "ImpPlan":
                case "Execution":
                    ((Label)this.gvImpPlan.FooterRow.FindControl("lgvFPreAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(preamt)", "")) ?
                                  0 : dt.Compute("sum(preamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvImpPlan.FooterRow.FindControl("lgvFCurAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(curamt)", "")) ?
                                    0 : dt.Compute("sum(curamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvImpPlan.FooterRow.FindControl("lgvFTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(toamt)", "")) ?
                                    0 : dt.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "ImpPlan02":
                    ((Label)this.gvTvsImp.FooterRow.FindControl("lgvFtramttvp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tramt)", "")) ?
                                   0 : dt.Compute("sum(tramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTvsImp.FooterRow.FindControl("lgvFimpAmttvp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(impamt)", "")) ?
                                    0 : dt.Compute("sum(impamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;



                case "PlanVSEx":
                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFPrePlanAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ppreamt)", "")) ?
                                 0 : dt.Compute("sum(ppreamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFPlanAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pcuramt)", "")) ?
                                0 : dt.Compute("sum(pcuramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFToPlanAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ptoamt)", "")) ?
                                0 : dt.Compute("sum(ptoamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFPreExAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(epreamt)", "")) ?
                                    0 : dt.Compute("sum(epreamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFCurExAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ecuramt)", "")) ?
                                    0 : dt.Compute("sum(ecuramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFToExAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(etoamt)", "")) ?
                                    0 : dt.Compute("sum(etoamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPlanVSEx.FooterRow.FindControl("lgvFVtoAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(vtoamt)", "")) ?
                                    0 : dt.Compute("sum(vtoamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "BgdVSEx":

                    double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
                    double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));
                    percent = (bgdamt < 0 ? 0.00 : ((examt * 100) / bgdamt));
                    ((Label)this.gvBgdVsEx.FooterRow.FindControl("lgvFBgdAmt")).Text = bgdamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdVsEx.FooterRow.FindControl("lgvFexAmt")).Text = examt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdVsEx.FooterRow.FindControl("lgvFvaramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(varamt)", "")) ?
                                    0 : dt.Compute("sum(varamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvBgdVsEx.FooterRow.FindControl("lgvFPercent")).Text = percent.ToString("#,##0.00;(#,##0.00); ") + "%";


                    break;

                case "MatEva":

                    ((Label)this.gvMatEva.FooterRow.FindControl("lgvFvaramtm")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(varamt)", "")) ?
                                    0 : dt.Compute("sum(varamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "MaPlanVsPlanVsEx":

                    double MaAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
                    double ExeAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));

                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFmpamt")).Text = MaAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFmonthamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mamt)", "")) ?
                                   0 : dt.Compute("sum(mamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFexeamt")).Text = ExeAmt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvmplanvaexe.FooterRow.FindControl("lgvFexepercentage")).Text = MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";
                    break;



                case "DayWiseExecution":
                    DataTable dt1 = dt.Copy();
                    DataView dv = dt1.DefaultView;
                    dv.RowFilter = ("isircode like  'BBBBAAAAAAAA'");
                    dt1 = dv.ToTable();
                    ((Label)this.gvExecution.FooterRow.FindControl("lgvFIssueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(isuamt)", "")) ? 0
                            : dt1.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



            }

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "ImpPlan":
                case "Execution":
                    PrintImpExe();
                    break;

                case "ImpPlan02":

                    PrintTarVsPlan();
                    break;

                case "PlanVSEx":
                    PrintPlanVsExe();
                    break;

                case "BgdVSEx":
                    PrintBgsdVsExe();
                    break;

                case "MatEva":
                    PrintMatEva();
                    break;
                case "MaPlanVsPlanVsEx":
                    PrintMaPlanVsPlanVsEx();
                    break;

                case "DayWiseExecution":
                    PrintDWiseExecution();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string comcod = this.GetCompCode();
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "Project Name: " + this.ddlProjectName.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrintImpExe()
        {

            DataTable dt = (DataTable)Session["tblplanexe"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string Type = Request.QueryString["Type"].ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassExecution.EmplemanPlan>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptImpExeStatus1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + projectName));
            Rpt1.SetParameters(new ReportParameter("txtdate", " (" + "From  " + frmdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpthead));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string Type = Request.QueryString["Type"].ToString();
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));
            //string frmdate=Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            //ReportDocument rptImp = new RealERPRPT.R_09_PImp.RptImpExeStatus1();

            //TextObject txtheader = rptImp.ReportDefinition.ReportObjects["rpttxtheader"] as TextObject;
            //txtheader.Text = rpthead;
            //TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text =projectName;

            //TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            //txtdat.Text = " (" + "From  " + frmdate + " To " + todate +")";
            //DataTable dt = (DataTable)Session["tblplanexe"];
            //TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptImp.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptImp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptImp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintTarVsPlan()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Type = Request.QueryString["Type"].ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);

            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            ReportDocument rptImp = new RealERPRPT.R_09_PImp.RptTarVsPlan();

            TextObject txtCompanyName = rptImp.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            txtCompanyName.Text = comnam;
            TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = projectName;

            TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
            DataTable dt = (DataTable)Session["tblplanexe"];
            TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptImp.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptImp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptImp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintPlanVsExe()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string Type = Request.QueryString["Type"].ToString();
            string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));
            string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblplanexe"];



            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassExecution.MonthlyPlanVSExecution>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptImpPlanVsStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));

            Rpt1.SetParameters(new ReportParameter("RptTitle", "Monthly Plan VS. Execution"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptImp = new RealERPRPT.R_09_PImp.RptImpPlanVsStatus();

            //TextObject txtheader = rptImp.ReportDefinition.ReportObjects["rpttxtheader"] as TextObject;



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //string Type = Request.QueryString["Type"].ToString();
            //string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));
            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd.MM.yyyy");
            //ReportDocument rptImp = new RealERPRPT.R_09_PImp.RptImpPlanVsStatus();

            //TextObject txtheader = rptImp.ReportDefinition.ReportObjects["rpttxtheader"] as TextObject;
            //txtheader.Text = rpthead;

            //TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject txtdat = rptImp.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            //txtdat.Text = " (" + "From  " + frmdate + " To " + todate + ")";
            //DataTable dt = (DataTable)Session["tblplanexe"];
            //TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptImp.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptImp.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptImp;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBgsdVsExe()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            DataTable dt = (DataTable)Session["tblplanexe"];
            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));
            percent = (bgdamt < 0 ? 0.00 : ((examt * 100) / bgdamt));


            var list = dt.DataTableToList<RealEntity.C_09_PIMP.EClassExecution.BudgetVsExecution>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_09_PIMP.RptBgdVsExe", list, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjectName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtPrjpercent", percent.ToString("#,##0.00;(#,##0.00); ") + "%"));
            Rpt1.SetParameters(new ReportParameter("Date", "Date: " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtrptgrp", "Group: " + this.ddlRptGroup.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("Rptname", "BUDGET VS EXECUTION"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)Session["tblplanexe"];

            //double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            //double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));
            //percent = (bgdamt < 0 ? 0.00 : ((examt * 100) / bgdamt)); 

            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //ReportDocument rptBgdVsEx = new RealERPRPT.R_09_PImp.RptBgdVsExe();
            //TextObject txtCompany = rptBgdVsEx.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject rpttxtPrjName = rptBgdVsEx.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject rpttxtPrjPer = rptBgdVsEx.ReportDefinition.ReportObjects["txtPrjpercent"] as TextObject;
            //rpttxtPrjPer.Text = percent.ToString("#,##0.00;(#,##0.00); ") + "%";
            //TextObject txtgrp = rptBgdVsEx.ReportDefinition.ReportObjects["txtrptgrp"] as TextObject;
            //txtgrp.Text = "Group: " + this.ddlRptGroup.SelectedItem.Text;
            //TextObject txtdat = rptBgdVsEx.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            //txtdat.Text = "Date: " +Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy");

            //TextObject txtuserinfo = rptBgdVsEx.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptBgdVsEx.SetDataSource(dt);
            //Session["Report1"] = rptBgdVsEx;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintMatEva()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            ReportDocument rptMat = new RealERPRPT.R_09_PImp.RptMatEvalution();
            TextObject txtCompany = rptMat.ReportDefinition.ReportObjects["CompName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject rpttxtPrjName = rptMat.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = projectName;
            TextObject txtgrp = rptMat.ReportDefinition.ReportObjects["txtrptgrp"] as TextObject;
            txtgrp.Text = "Group: " + this.ddlRptGroup.SelectedItem.Text;
            TextObject txtdat = rptMat.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            txtdat.Text = "Date: " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd.MM.yyyy");
            DataTable dt = (DataTable)Session["tblplanexe"];
            TextObject txtuserinfo = rptMat.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptMat.SetDataSource(dt);
            Session["Report1"] = rptMat;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintMaPlanVsPlanVsEx()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblplanexe"];
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            ReportDocument rptMat = new RealERPRPT.R_09_PImp.RptMaPlanVsPlanVsEx();
            double MaAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mpamt)", "")) ? 0 : dt.Compute("sum(mpamt)", "")));
            double ExeAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(examt)", "")) ? 0 : dt.Compute("sum(examt)", "")));
            TextObject txtProjectname = rptMat.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectname.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            TextObject txtdat = rptMat.ReportDefinition.ReportObjects["rpttxtdat"] as TextObject;
            txtdat.Text = "(From: " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";
            TextObject rpttxtExePer = rptMat.ReportDefinition.ReportObjects["txtExePer"] as TextObject;
            rpttxtExePer.Text = MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";


            TextObject txtuserinfo = rptMat.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptMat.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptMat.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptMat;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintDWiseExecution()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblplanexe"];
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            ReportDocument rptMat = new RealERPRPT.R_09_PImp.rptDayWiseExecution();

            TextObject txtCompanyName = rptMat.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            txtCompanyName.Text = comnam;
            TextObject txtProjectname = rptMat.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectname.Text = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);

            TextObject txtdat = rptMat.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdat.Text = "(From: " + Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            TextObject rpttxtamt = rptMat.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            rpttxtamt.Text = Convert.ToDouble("0" + ((Label)this.gvExecution.FooterRow.FindControl("lgvFIssueAmt")).Text).ToString("#,##0;(#,##0); ");
            TextObject txtuserinfo = rptMat.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptMat.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptMat.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptMat;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvImpPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvImpPlan.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void gvPlanVSEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPlanVSEx.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowFloorcode();
        }
        protected void gvBgdVsEx_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdVsEx.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvMatEva_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMatEva.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        //.GVFixedHeader { 
        //                  position:relative; 
        //                   top:expression(this.parentNode.parentNode.scrollTop-1);}

        protected void gvmplanvaexe_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmplanvaexe.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvExecution_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvExecution.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvExecution_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // TextBox WrkDesc = (TextBox)e.Row.FindControl("lgcWDescexe");
                Label WrkDesc = (Label)e.Row.FindControl("lgcWDescexe");
                TextBox WrkDescnar = (TextBox)e.Row.FindControl("lgcWDescexenar");
                Label lgvissueAmt = (Label)e.Row.FindControl("lgvissueAmt");

                Label lblgvflrdescexe = (Label)e.Row.FindControl("lblgvflrdescexe");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "isircode")).ToString();

                if (code == "")
                {
                    return;
                }

                WrkDesc.Visible = (code != "AAAAAAAAAAAA" || code != "AAAAAAAAAAAA");
                WrkDescnar.Visible = (code == "AAAAAAAAAAAA");

                if (code == "AAAAAAAAAAAA")
                {

                    lblgvflrdescexe.Font.Bold = true;
                    lblgvflrdescexe.Style.Add("color", "Blue");
                    WrkDesc.Attributes["style"] = "font-weight:bold; color:blue;";
                    WrkDescnar.Attributes["style"] = "font-weight:bold; color:blue;";
                }
                else if (code == "BBBBAAAAAAAA")
                {
                    WrkDesc.Font.Bold = true;
                    lgvissueAmt.Font.Bold = true;
                    WrkDesc.Style.Add("text-align", "right");


                }





            }

        }
        protected void gvTvsImp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvTvsImp.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}

