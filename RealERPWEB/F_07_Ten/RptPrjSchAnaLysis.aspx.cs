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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_07_Ten
{
    public partial class RptPrjSchAnaLysis : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "SchVsAna") ? "PROJECT SCHEDULE VS ANALYSIS"
                    : (Request.QueryString["Type"].ToString() == "TenVsBgd") ? "Tender Vs. Budget" : "TENDER PROPOSAL";
                this.GetProjectName();
                Request.QueryString["Type"].ToString();
                this.showViewField();



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }
        private void showViewField()
        {
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SchVsAna":
                    break;

                case "TenderProposal":
                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;

                    break;
                case "Benefit":
                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    break;
                case "TenVsBgd":
                    this.lblflrlist.Visible = false;
                    this.ddlFloorListRpt.Visible = false;
                    this.lblRptGroup.Visible = false;
                    this.ddlRptGroup.Visible = false;
                    this.MultiView1.ActiveViewIndex = 3;
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
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS01", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "prjdesc";
            this.ddlProjectName.DataValueField = "prjcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }
        private void GetFloorName()
        {
            string comcod = this.GetCompCode();
            string prjcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS01", "GETFLOORCOD", prjcode, "", "", "", "", "", "", "", "");
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


        protected void imgbtnFindProject_OnClick(object sender, EventArgs e)
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
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SchVsAna":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowPrjSchVsAnalysis();
                    break;

                case "TenderProposal":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowtenderProposal();
                    break;

                case "Benefit":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowtenderBenefit();
                    break;
                case "TenVsBgd":
                    this.showTenVsBudget();
                    break;


            }

        }

        private void showTenVsBudget()
        {
            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string prjcode = ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS", "RPTTENVSBUDGET", prjcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvTenVsBudget.DataSource = null;
                this.gvTenVsBudget.DataBind();
                return;
            }

            Session["tblschdule"] = this.HiddenSameData(ds2.Tables[0]);
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string mrptcod = dt1.Rows[0]["mrptcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["mrptcod"].ToString() == mrptcod)
                    dt1.Rows[j]["mrptdesc"] = "";
                mrptcod = dt1.Rows[j]["mrptcod"].ToString();
            }

            return dt1;
        }
        private void ShowPrjSchVsAnalysis()
        {

            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string prjcode = ddlProjectName.SelectedValue.ToString();
            string flrcode = this.ddlFloorListRpt.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));


            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS01", "RPTSCHEDULEVSANALYSIS", prjcode, flrcode, mRptGroup, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSchedule.DataSource = null;
                this.gvSchedule.DataBind();
                return;
            }

            Session["tblschdule"] = this.HiddenSameData02(ds1.Tables[0]);
            this.LoadGrid();

        }
        DataTable HiddenSameData02(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string flrcod = dt1.Rows[0]["flrcod"].ToString(); ;

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["flrcod"].ToString() == flrcod)
                    dt1.Rows[j]["flrdes"] = "";
                flrcod = dt1.Rows[j]["flrcod"].ToString();

            }
            return dt1;

        }

        private void ShowtenderProposal()
        {

            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string prjcode = ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS01", "RPTTENDERPROPOSAL", prjcode, "000", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvtenprosal.DataSource = null;
                this.gvtenprosal.DataBind();
                return;
            }

            Session["tblschdule"] = ds1.Tables[0];
            this.LoadGrid();


        }

        private void ShowtenderBenefit()
        {

            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string prjcode = ddlProjectName.SelectedValue.ToString();
            DataSet ds2 = tasData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "GETGURVEYRATE", prjcode, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvBenefit.DataSource = null;
                this.gvBenefit.DataBind();
                return;
            }
            else
            {
                this.gvBenefit.DataSource = ds2;
                this.gvBenefit.DataBind();
            }

            Session["tblschdule"] = ds2.Tables[0];
            //this.LoadGrid();
            this.SaveValue();


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetFloorName();
        }

        private void LoadGrid()
        {
            DataTable dt1 = ((DataTable)Session["tblschdule"]).Copy();
            // DataTable tblschdule = (DataTable)Session["tblBenefit"];
            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SchVsAna":
                    this.gvSchedule.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvSchedule.DataSource = dt1;
                    this.gvSchedule.DataBind();
                    this.FooteCalculation();
                    break;


                case "TenderProposal":
                    double amt1, amt2, amt3;
                    DataView dv = ((DataTable)Session["tblschdule"]).DefaultView;
                    DataTable dt;
                    dv.RowFilter = ("rescode not like '%0000%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    this.gvtenprosal.Columns[2].Visible = (amt1 != 0);
                    this.gvtenprosal.Columns[3].Visible = (amt2 != 0);
                    this.gvtenprosal.Columns[4].Visible = (amt3 != 0);
                    this.gvtenprosal.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvtenprosal.DataSource = dt1;
                    this.gvtenprosal.DataBind();
                    break;
                case "Benefit":

                    this.gvBenefit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBenefit.DataSource = dt1;
                    this.gvBenefit.DataBind();
                    this.FooteCalculation();
                    //this.SaveValue();
                    break;
                case "TenVsBgd":

                    this.gvTenVsBudget.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvTenVsBudget.DataSource = dt1;
                    this.gvTenVsBudget.DataBind();
                    this.FooteCalculation();
                    //this.SaveValue();
                    break;

            }
        }


        private void FooteCalculation()
        {
            string Type = Request.QueryString["Type"].ToString();
            DataTable dt = (DataTable)Session["tblschdule"];
            //if (dt.Rows.Count == 0)
            //    return;
            DataTable tblBenefit = (DataTable)Session["tblschdule"];
            int TblRowIndex;

            switch (Type)
            {
                case "SchVsAna":

                    if (dt.Rows.Count == 0)
                        return;
                    double schamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(schamt)", "")) ? 0 : dt.Compute("sum(schamt)", "")));
                    double anaamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(rptamt)", "")) ? 0 : dt.Compute("sum(rptamt)", "")));
                    double bdgamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bdgamt)", "")) ? 0 : dt.Compute("sum(bdgamt)", "")));
                    //double billamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.0 : dt.Compute("sum(billamt)", "")));
                    ((Label)this.gvSchedule.FooterRow.FindControl("lgvFSchamt")).Text = schamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSchedule.FooterRow.FindControl("lgvFAnaamt")).Text = anaamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSchedule.FooterRow.FindControl("lgvFBgdamt")).Text = bdgamt.ToString("#,##0;(#,##0); ");
                    double difamtsastd = schamt - anaamt;
                    //((Label)this.gvSchedule.FooterRow.FindControl("ftlbbillamtAmt")).Text = billamt.ToString("#,##0.00;(#,##0.00); ");
                    this.lbltxtvalDiffamt.Text = (schamt - anaamt).ToString("#,##0;(#,##0); ");
                    this.lbltxtvalDiffamt2.Text = (anaamt - bdgamt).ToString("#,##0;(#,##0); ");
                    this.lbltxtvalDiffper.Text = (anaamt == 0 ? 0.00 : (difamtsastd * 100) / anaamt).ToString("#,##0.00;(#,##0.00); ");
                    break;

                case "Benefit":
                    double billamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.0 : dt.Compute("sum(billamt)", "")));
                    ((Label)this.gvBenefit.FooterRow.FindControl("ftlbbillamtAmt")).Text = billamt.ToString("#,##0.00;(#,##0.00); ");
                    break;
                case "TenVsBgd":



                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("rptcod like '%0000000000%'");
                    dt = dv.ToTable();
                    //if (dt.Rows.Count == 0)
                    //    return;
                    double tenam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tenam)", "")) ? 0.0 : dt.Compute("sum(tenam)", "")));
                    double bgdam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdam)", "")) ? 0.0 : dt.Compute("sum(bgdam)", "")));
                    double diffam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(diffam)", "")) ? 0.0 : dt.Compute("sum(diffam)", "")));
                    double diffram = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(diffram)", "")) ? 0.0 : dt.Compute("sum(diffram)", "")));
                    double toam = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(difftoam)", "")) ? 0.0 : dt.Compute("sum(difftoam)", "")));
                    double difftabam = tenam - bgdam;


                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFtenamtbn")).Text = tenam.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFbgdambn")).Text = bgdam.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFdifatbam")).Text = difftabam.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFdiffambn")).Text = diffam.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFdiffram")).Text = diffram.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFdiffram02")).Text = toam.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvTenVsBudget.FooterRow.FindControl("lgvFtoambn")).Text = toam.ToString("#,##0.00;(#,##0.00); ");


                    break;

            }
        }

        protected void gvTenVsBudget_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTenVsBudget.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void SaveValue()
        {
            DataTable tblBenefit = (DataTable)Session["tblschdule"];
            int TblRowIndex;
            string type = this.Request.QueryString["Type"].ToString();

            double BoQAmt = 0.0;
            double SrvAmt = 0.0;
            switch (type)
            {
                case "Benefit":
                    for (int i = 0; i < this.gvBenefit.Rows.Count; i++)
                    {
                        TblRowIndex = (gvBenefit.PageIndex) * gvBenefit.PageSize + i;
                        double qty = Convert.ToDouble("0" + ((TextBox)this.gvBenefit.Rows[i].FindControl("txtgvSurvQty")).Text.Trim().Replace(",", ""));
                        double rate = Convert.ToDouble("0" + ((TextBox)this.gvBenefit.Rows[i].FindControl("txtgvSurvRete")).Text.Trim().Replace(",", ""));

                        double BoQqty = Convert.ToDouble("0" + ((Label)this.gvBenefit.Rows[i].FindControl("lblgvQty")).Text.Trim().Replace(",", ""));
                        double BoQrate = Convert.ToDouble("0" + ((Label)this.gvBenefit.Rows[i].FindControl("lblgvSRate")).Text.Trim().Replace(",", ""));

                        ((Label)this.gvBenefit.Rows[i].FindControl("lblgvSurvAmt")).Text = (qty * rate).ToString("#,##0.00;(#,##0.00); ");

                        tblBenefit.Rows[TblRowIndex]["sarqty"] = qty;
                        tblBenefit.Rows[TblRowIndex]["sarrate"] = rate;

                        tblBenefit.Rows[TblRowIndex]["saramt"] = qty * rate;

                        SrvAmt += qty * rate;
                        BoQAmt += BoQqty * BoQrate;

                    }
                    if (tblBenefit.Rows.Count == 0)
                        return;

                    double schamt = Convert.ToDouble((Convert.IsDBNull(tblBenefit.Compute("sum(schamt)", "")) ? 0.0 : tblBenefit.Compute("sum(schamt)", "")));
                    double srvamt = Convert.ToDouble((Convert.IsDBNull(tblBenefit.Compute("sum(saramt)", "")) ? 0.0 : tblBenefit.Compute("sum(saramt)", "")));
                    ((Label)this.gvBenefit.FooterRow.FindControl("ftlblSurveyAmt")).Text = srvamt.ToString("#,##0.00;(#,##0.00); ");
                    ((Label)this.gvBenefit.FooterRow.FindControl("ftlblBoqAmt")).Text = schamt.ToString("#,##0.00;(#,##0.00); ");

                    // this.lbltxtvalDiffamt.Text =(schamt-anaamt).ToString("#,##0;(#,##0); ");
                    // this.lbltxtvalDiffper.Text = (((schamt - anaamt) * 100) / schamt).ToString("#,##0.00;(#,##0.00); ");

                    Session["tblschdule"] = tblBenefit;
                    // LoadGrid("gvSubRate");
                    break;


            }
        }

        protected void gvSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSchedule.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }
        protected void gvSubRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBenefit.PageIndex = e.NewPageIndex;
            //this.SaveValue();

        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            string Type = Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SchVsAna":
                    this.PrintSchVsAnalysis();
                    break;


                case "TenderProposal":
                    this.PrintTenderProposal();
                    break;
                case "Benefit":
                    this.printBenefitRpt();
                    break;

            }


        }
        private void PrintSchVsAnalysis()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            ReportDocument rptImp = new RealERPRPT.R_07_Ten.RptSchduleVsAnalysis();
            TextObject txtCompany = rptImp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rpttxtPrjName.Text = "Project: " + projectName;
            TextObject rpttxtDiffrenceAmt = rptImp.ReportDefinition.ReportObjects["txtDiffrenceAmt"] as TextObject;
            rpttxtDiffrenceAmt.Text = this.lbltxtvalDiffamt.Text.Trim();
            TextObject txtBudDiff = rptImp.ReportDefinition.ReportObjects["txtBudDiff"] as TextObject;
            txtBudDiff.Text = this.lbltxtvalDiffamt2.Text.Trim();
            TextObject rpttxtDifferencePer = rptImp.ReportDefinition.ReportObjects["txtDifferencePer"] as TextObject;
            rpttxtDifferencePer.Text = this.lbltxtvalDiffper.Text.Trim();
            DataTable dt = (DataTable)Session["tblschdule"];
            TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptImp.SetDataSource(dt);
            Session["Report1"] = rptImp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //

        }

        private void PrintTenderProposal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);

            DataTable dt = (DataTable)Session["tblschdule"];
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_08_PPlan.BO_Class_Con.RptTenderProposal>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_07_Ten.RptTenderProposal", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Tender Position at a glance"));
            Rpt1.SetParameters(new ReportParameter("projectName", "Project: " + projectName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Submission Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void printBenefitRpt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblschdule"];

            //ReportDocument rptsale = new  RealERPRPT.R_04_Bgd.rptSubConRat();
            ReportDocument rptadj = new RealERPRPT.R_07_Ten.rptTASSurvRate();
            TextObject rptCname = rptadj.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptpactdesc = rptadj.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rptpactdesc.Text = "Project: " + this.ddlProjectName.SelectedItem.Text.Substring(13);

            TextObject txtuserinfo = rptadj.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptadj.SetDataSource(dt);
            Session["Report1"] = rptadj;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvtenprosal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvtenprosal.PageIndex = e.NewPageIndex;
            this.LoadGrid();


        }

        protected void gvTenVsBudget_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lgvtenqtybn = (Label)e.Row.FindControl("lgvtenqtybn");
                Label lgvbgdqtybn = (Label)e.Row.FindControl("lgvbgdqtybn");
                Label lgvtenratbn = (Label)e.Row.FindControl("lgvtenratbn");
                Label lgvbgdratbn = (Label)e.Row.FindControl("lgvbgdratbn");
                Label lgvtenamtbn = (Label)e.Row.FindControl("lgvtenamtbn");
                Label lgvbgdambn = (Label)e.Row.FindControl("lgvbgdambn");
                Label lgvdifqtybn = (Label)e.Row.FindControl("lgvdifqtybn");
                Label lgvdiffram = (Label)e.Row.FindControl("lgvdiffram");
                Label lgvtoambn = (Label)e.Row.FindControl("lgvtoambn");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 10) == "0000000000")
                {
                    lgvtenqtybn.Font.Bold = true;
                    lgvbgdqtybn.Font.Bold = true;
                    lgvtenratbn.Font.Bold = true;
                    lgvbgdratbn.Font.Bold = true;
                    lgvtenamtbn.Font.Bold = true;
                    lgvbgdambn.Font.Bold = true;
                    lgvdifqtybn.Font.Bold = true;
                    lgvdiffram.Font.Bold = true;
                    lgvtoambn.Font.Bold = true;


                }

            }


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
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;


                TableCell cell04 = new TableCell();
                cell04.Text = "Quantity";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 2;

                TableCell cell05 = new TableCell();
                cell05.Text = "Rate";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;

                TableCell cell06 = new TableCell();
                cell06.Text = "Amount";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 2;

                TableCell cell07 = new TableCell();
                cell07.Text = "DIfference";
                cell07.HorizontalAlign = HorizontalAlign.Center;
                cell07.ColumnSpan = 2;

                TableCell cell08 = new TableCell();
                cell08.Text = "";
                cell08.HorizontalAlign = HorizontalAlign.Center;
                cell08.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvrow.Cells.Add(cell06);
                gvrow.Cells.Add(cell07);
                gvrow.Cells.Add(cell08);
                gvTenVsBudget.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gvtenprosal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkdesc = (HyperLink)e.Row.FindControl("hlnkgvWDesc");
                Label amt1 = (Label)e.Row.FindControl("lgvtenamt1");
                Label amt2 = (Label)e.Row.FindControl("lgvtenamt2");
                Label amt3 = (Label)e.Row.FindControl("lgvtenamt3");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();


                if (code == "")
                {
                    return;
                }

                if (code == "6AAAAAAAAAAA" || code == "7AAAAAAAAAAA")
                {
                    string actcode = this.ddlProjectName.SelectedValue.ToString();
                    string actdesc = this.ddlProjectName.SelectedItem.Text;
                    hlnkdesc.NavigateUrl = "~/F_07_Ten/LinkRptPrjSchAnaLysis.aspx?Type=SchVsAna&actcode=" + actcode + "&actdesc=" + actdesc;
                    hlnkdesc.Style.Add("color", "blue");

                }

                else if (code == "7BBBBBBBBBBB")
                {
                    string actcode = this.ddlProjectName.SelectedValue.ToString();
                    string actdesc = this.ddlProjectName.SelectedItem.Text;
                    hlnkdesc.NavigateUrl = "~/F_07_Ten/LinkRptPrjSchAnaLysis.aspx?Type=Benefit&actcode=" + actcode + "&actdesc=" + actdesc;
                    hlnkdesc.Style.Add("color", "blue");

                }



                if (ASTUtility.Right(code, 4) != "0000")
                {

                    hlnkdesc.Font.Bold = true;
                    amt1.Font.Bold = true;
                    amt2.Font.Bold = true;
                    amt3.Font.Bold = true;
                }

            }




        }


    }
}
