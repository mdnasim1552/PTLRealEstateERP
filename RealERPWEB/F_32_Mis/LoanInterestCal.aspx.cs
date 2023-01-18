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
namespace RealERPWEB.F_32_Mis
{
    public partial class LoanInterestCal : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Loan Interest Calculation Sheet";
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.txtfDat.Text = System.DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                this.txttDat.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS", "GETPROWITHOUTLEV", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc1";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }


        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintLoanCal();

        }

        private void PrintLoanCal()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectcode = this.ddlProjectName.SelectedValue.ToString();
            string projectName = this.ddlProjectName.SelectedItem.Text.Substring(14);


            ReportDocument rptProSummary = new RealERPRPT.R_32_Mis.rptLoanIntCal();
            TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            rpttxtPrjName.Text = "Accounts Head:- " + projectName;
            TextObject rpttxtDate = rptProSummary.ReportDefinition.ReportObjects["date"] as TextObject;
            rpttxtDate.Text = "From: " + this.txtfDat.Text + " To: " + this.txttDat.Text;
            TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Loan Interest Calculation Sheet";
                string eventdesc = "Print Report";
                string eventdesc2 = this.ddlProjectName.SelectedItem.Text.Substring(13); ;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptProSummary.SetDataSource((DataTable)Session["tblData"]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptProSummary.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptProSummary;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.ddlProjectName.SelectedValue.ToString();
            string fdate = this.txtfDat.Text;
            string tdate = this.txttDat.Text;
            string rate = this.TxtIntrstRate.Text.Trim().Replace("%", "");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTINTERCAL", PactCode, fdate, tdate, rate, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvInstCal.DataSource = null;
                this.gvInstCal.DataBind();
                return;
            }

            Session["tblData"] = ds1.Tables[0];
            this.Data_Bind();
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblData"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvInstCal.FooterRow.FindControl("lgvFDays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(daydif)", "")) ?
                                    0 : dt.Compute("sum(daydif)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvInstCal.FooterRow.FindControl("lgvFInst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(intamt)", "")) ?
                                    0 : dt.Compute("sum(intamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblData"];
            this.gvInstCal.DataSource = dt;
            this.gvInstCal.DataBind();
        }

    }
}

