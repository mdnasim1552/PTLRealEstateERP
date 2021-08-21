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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkConstruProgress : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00, bgdpercent = 0.00, bgdexepercent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                this.GetProjectName();
                this.lblAsDate.Text = this.Request.QueryString["Date2"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Constraction Progress";

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetProjectName()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

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
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.Pnlnote.Visible = true;
            this.ShowConProgress();
        }



        private void ShowConProgress()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string pactcode = ddlProjectName.SelectedValue.ToString();
            string toDate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTCONPROGRAM", pactcode, toDate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvConPro.DataSource = null;
                this.gvConPro.DataBind();
                return;
            }

            Session["tblConPro"] = ds1.Tables[0];
            this.LoadGrid();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Floor Wise Construction Progress";
                string eventdesc = "Show Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblConPro"];
            this.gvConPro.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvConPro.DataSource = dt;
            this.gvConPro.DataBind();
            this.FooterCalcul(dt);
        }

        private void FooterCalcul(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;


            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ? 0 : dt.Compute("sum(mplanat)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));

            percent = (mplan == 0 ? 0.00 : ((examt * 100) / mplan));
            bgdpercent = (bgdamt == 0 ? 0.00 : ((mplan * 100) / bgdamt));
            bgdexepercent = (bgdamt == 0 ? 0.00 : ((examt * 100) / bgdamt));
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFBgdAmt")).Text = bgdamt.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFMasPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplan)", "")) ?
                                0 : dt.Compute("sum(mplan)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = examt.ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFMPlanastoday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplanat)", "")) ?
                                0 : dt.Compute("sum(mplanat)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvConPro.FooterRow.FindControl("lgvFexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ?
                                0 : dt.Compute("sum(eamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(leamt)", "")) ?
                         0 : dt.Compute("sum(leamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFWorkP")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(perwork)", "")) ?
                                0 : dt.Compute("sum(perwork)", ""))).ToString("#,##0.00;(#,##0.00); ") + "%";
            ((Label)this.gvConPro.FooterRow.FindControl("lgvFPercent")).Text = percent.ToString("#,##0.00;(#,##0.00); ") + "%";

            this.lPercentonbgd.Text = bgdpercent.ToString("#,##0.00;(#,##0.00); ") + "%";
            this.lPercentonbgdexe.Text = bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + "%";

            string comcod = this.Request.QueryString["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string frmdate = Convert.ToDateTime("01" + this.lblAsDate.Text.Substring(2)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.lblAsDate.Text).ToString("dd-MMM-yyyy");


            ((HyperLink)this.gvConPro.FooterRow.FindControl("hlnkgvFlessexAmt")).NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ImpPlan02&comcod=" + comcod + "&Pactcode=" + pactcode + "&Pactdesc=" + pactdesc + "&Date1=" + frmdate + "&Date2=" + todate;


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintBgsdVsExe();

        }

        private void PrintBgsdVsExe()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblConPro"];
            double bgdamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bgdamt)", "")) ? 0 : dt.Compute("sum(bgdamt)", "")));
            double mplan = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mplan)", "")) ? 0 : dt.Compute("sum(mplan)", "")));
            double examt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(eamt)", "")) ? 0 : dt.Compute("sum(eamt)", "")));
            if (mplan != 0)
            {
                percent = (mplan < 0 ? 0.00 : ((examt * 100) / mplan));
            }
            else
            {
                percent = 0.00;
            }
            if (bgdamt != 0)
            {
                bgdpercent = (bgdamt < 0 ? 0.00 : ((examt * 100) / bgdamt));
            }
            else
            {
                bgdpercent = 0.00;
            }

            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.CatWiseConProgress>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptConProgram", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Floor Wise Construction Progress"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("date", "As On " + this.Request.QueryString["Date2"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtPercent", percent.ToString("#,##0.00;(#,##0.00); ") + " %"));
            Rpt1.SetParameters(new ReportParameter("txtBPercent", bgdpercent.ToString("#,##0.00;(#,##0.00); ") + " %"));
            Rpt1.SetParameters(new ReportParameter("txtBExePrcent", bgdexepercent.ToString("#,##0.00;(#,##0.00); ") + " %"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //ReportDocument rptConPro = new RealERPRPT.R_32_Mis.RptConProgram();
            //TextObject rpttxtPrjName = rptConPro.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            //rpttxtPrjName.Text = projectName;
            //TextObject rpttxtDate = rptConPro.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
            //TextObject rpttxtpercent = rptConPro.ReportDefinition.ReportObjects["txtpercent"] as TextObject;
            //rpttxtpercent.Text = percent.ToString("#,##0.00;(#,##0.00); ") + " %";
            //TextObject rpttxBtpercent = rptConPro.ReportDefinition.ReportObjects["txtbpercent"] as TextObject;
            //rpttxBtpercent.Text = bgdpercent.ToString("#,##0.00;(#,##0.00); ") + " %";
            //TextObject txtuserinfo = rptConPro.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptConPro.SetDataSource(dt);

            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Floor Wise Construction Progress";
            //    string eventdesc = "Print Report";
            //    string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptConPro.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptConPro;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }


        protected void gvConPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvConPro.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvConPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");

            string mPACTCODE = this.ddlProjectName.SelectedValue.ToString();
            string mFlrCode = ((Label)e.Row.FindControl("lblgvflrCode")).Text;
            //string mPACTDESC = ((Label)e.Row.FindControl("Actdesc")).Text;
            string mTRNDAT1 = this.Request.QueryString["Date2"].ToString();
            string comcod = this.Request.QueryString["comcod"].ToString();
            hlink1.NavigateUrl = "~/F_32_Mis/RptLinkImpExeStatus.aspx?Type=BgdAll&comcod=" + comcod + "&pactcode=" + mPACTCODE + "&FlrCode=" + mFlrCode + "&Date1=" + mTRNDAT1;
        }
    }
}

