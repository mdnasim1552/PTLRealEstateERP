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
    public partial class RptProPriceTrading : System.Web.UI.Page
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
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            this.PrintTrading();

        }

        private void PrintTrading()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptProSummary = new RealERPRPT.R_22_Sal.RptProPriceTrading();
            TextObject rpttxtPrjName = rptProSummary.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtPrjName.Text = comnam;
            TextObject rpttxtRate1 = rptProSummary.ReportDefinition.ReportObjects["txtrateb"] as TextObject;
            rpttxtRate1.Text = "Bank Int. " + this.TxtIntrstRate.Text + " P.Y.";
            TextObject rpttxtRate2 = rptProSummary.ReportDefinition.ReportObjects["txtrateO"] as TextObject;
            rpttxtRate2.Text = "Overhead " + this.TxtIntrstRate0.Text + " P.Y.";

            TextObject txtuserinfo = rptProSummary.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Product Pricing(Trading)";
                string eventdesc = "Print Report";
                string eventdesc2 = "Product Pricing(Trading)";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptProSummary.SetDataSource((DataTable)Session["tblData"]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptProSummary.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptProSummary;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblData");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string rate1 = this.TxtIntrstRate.Text.Trim().Replace("%", "");
            string rate2 = this.TxtIntrstRate0.Text.Trim().Replace("%", "");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_PURCHASE_04", "RPTPURTRADING", "", rate1, rate2, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRptTrading.DataSource = null;
                this.gvRptTrading.DataBind();
                return;
            }

            Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            this.FooterCalculation();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }
            return dt1;

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblData"];
            if (dt.Rows.Count == 0)
                return;

            //((Label)this.gvRptTrading.FooterRow.FindControl("lgvFDays")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(daydif)", "")) ?
            //                        0 : dt.Compute("sum(daydif)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvRptTrading.FooterRow.FindControl("lgvFInst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(intamt)", "")) ?
            //0 : dt.Compute("sum(intamt)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblData"];
            this.gvRptTrading.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptTrading.DataSource = dt;
            this.gvRptTrading.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvRptTrading_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRptTrading.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}

