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
namespace RealERPWEB.F_07_Ten
{
    public partial class LinkRptPrjSchAnaLysis : System.Web.UI.Page
    {
        ProcessAccess tasData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = (Request.QueryString["Type"].ToString() == "SchVsAna") ? "PROJECT SCHEDULE VS ANALYSIS"
                    : "TENDER PROPOSAL";
                //this.lblHeadtitle.Text = (Request.QueryString["Type"].ToString() == "SchVsAna") ? "PROJECT SCHEDULE VS ANALYSIS" : "TENDER PROPOSAL";
                this.lblProject.Text = this.Request.QueryString["actdesc"].ToString();
                this.ShowValue();




            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {

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


            }

        }
        private void ShowPrjSchVsAnalysis()
        {

            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string prjcode = this.Request.QueryString["actcode"].ToString();
            string flrcode = "AAA";
            string mRptGroup = "12";

            DataSet ds1 = tasData.GetTransInfo(comcod, "SP_TAS_REPORT_ANALYSIS01", "RPTSCHEDULEVSANALYSIS", prjcode, flrcode, mRptGroup, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSchedule.DataSource = null;
                this.gvSchedule.DataBind();
                return;
            }

            Session["tblschdule"] = ds1.Tables[0];
            this.LoadGrid();

        }

        private void ShowtenderProposal()
        {




        }

        private void ShowtenderBenefit()
        {

            Session.Remove("tblschdule");
            string comcod = this.GetCompCode();
            string prjcode = this.Request.QueryString["actcode"].ToString();
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
            this.LoadGrid();



        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
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

            }
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

        protected void lbtnPrint_Click(object sender, EventArgs e)
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
            string projectName = this.Request.QueryString["actdesc"].ToString();
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
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            string projectName = this.Request.QueryString["actdesc"].ToString();
            ReportDocument rptImp = new RealERPRPT.R_07_Ten.RptTenderProposal();
            TextObject txtCompany = rptImp.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject rpttxtPrjName = rptImp.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            rpttxtPrjName.Text = "Project: " + projectName;
            TextObject txtDate = rptImp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "Submission Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy");


            DataTable dt = (DataTable)Session["tblschdule"];
            TextObject txtuserinfo = rptImp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptImp.SetDataSource(dt);
            Session["Report1"] = rptImp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
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
            rptpactdesc.Text = "Project: " + this.Request.QueryString["actdesc"].ToString();

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

        protected void gvSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkgvanarate = (HyperLink)e.Row.FindControl("hlnkgvanarate");
                HyperLink hlnkgvBgdrate = (HyperLink)e.Row.FindControl("hlnkgvBgdrate");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptcod")).ToString();


                if (code == "")
                {
                    return;
                }

                string actcode = this.Request.QueryString["actcode"].ToString();
                string lactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string actdesc = this.Request.QueryString["actdesc"].ToString();
                string flrcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "flrcod")).ToString();
                string rptdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rptdesc1")).ToString();
                string flrdes = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "flrdes")).ToString();
                hlnkgvanarate.NavigateUrl = "~/F_07_Ten/LinkBudATenAnaLysis.aspx?Type=TenAnalysis&actcode=" + actcode + "&actdesc=" + actdesc + "&isircode=" + code + "&isirdesc=" + rptdesc + "&flrcod=" + flrcod + "&flrdes=" + flrdes;
                hlnkgvBgdrate.NavigateUrl = "~/F_07_Ten/LinkBudATenAnaLysis.aspx?Type=BudAnalysis&actcode=" + lactcode + "&actdesc=" + actdesc + "&isircode=" + code + "&isirdesc=" + rptdesc + "&flrcod=" + flrcod + "&flrdes=" + flrdes;
                hlnkgvanarate.Style.Add("color", "blue");
                hlnkgvBgdrate.Style.Add("color", "blue");






            }
        }
    }
}