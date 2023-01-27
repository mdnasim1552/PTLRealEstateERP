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
    public partial class TradingInterestCal : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProjectName();
                this.GetUnitName();
            }
        }


        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETTRADINGCODE", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private void GetUnitName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSProject = "%" + this.txtScrUnit.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETTRADINGResCODE", pactcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlUnitCode.DataTextField = "sirdesc";
            this.ddlUnitCode.DataValueField = "sircode";
            this.ddlUnitCode.DataSource = ds1.Tables[0];
            this.ddlUnitCode.DataBind();

        }


        protected void imgbtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintTraIntCal();

        }

        private void PrintTraIntCal()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            string UnitName = this.ddlUnitCode.SelectedItem.Text.Substring(13);


            ReportDocument rptProSummary = new RealERPRPT.R_22_Sal.rptTradingIntCal();
            TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = projectName;
            TextObject rpttxtDate = rptProSummary.ReportDefinition.ReportObjects["txtUnitName"] as TextObject;
            rpttxtDate.Text = UnitName;
            TextObject rpttxtRate1 = rptProSummary.ReportDefinition.ReportObjects["txtrateb"] as TextObject;
            rpttxtRate1.Text = "Bank Int. " + this.TxtIntrstRate.Text + " P.Y.";
            TextObject rpttxtRate2 = rptProSummary.ReportDefinition.ReportObjects["txtrateO"] as TextObject;
            rpttxtRate2.Text = "Overhead " + this.TxtIntrstRate2.Text + " P.Y.";

            TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Interest Corfirmation";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptProSummary.SetDataSource((DataTable)Session["tbIntCalTr"]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptProSummary.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptProSummary;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tbIntCalTr");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string rescode = this.ddlUnitCode.SelectedValue.ToString();
            string rate1 = this.TxtIntrstRate.Text.Trim().Replace("%", "");
            string rate2 = this.TxtIntrstRate2.Text.Trim().Replace("%", "");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "SHOWINTBREAKDOWN", "", rate1, rate2, PactCode, rescode, "", "", "", "");
            if (ds1 == null)
            {
                this.gvInstCalTrd.DataSource = null;
                this.gvInstCalTrd.DataBind();
                return;
            }

            Session["tbIntCalTr"] = ds1.Tables[0];
            this.Data_Bind();
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tbIntCalTr"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvInstCalTrd.FooterRow.FindControl("lgvFDays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(daydif)", "")) ?
                                    0 : dt.Compute("sum(daydif)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvInstCalTrd.FooterRow.FindControl("lgvFInst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(intamt)", "")) ?
                                    0 : dt.Compute("sum(intamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvInstCalTrd.FooterRow.FindControl("lgvFOvamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ovamt)", "")) ?
                                    0 : dt.Compute("sum(ovamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tbIntCalTr"];
            this.gvInstCalTrd.DataSource = dt;
            this.gvInstCalTrd.DataBind();
        }

        protected void imgbtnUnit_Click(object sender, ImageClickEventArgs e)
        {
            this.GetUnitName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
    }
}

