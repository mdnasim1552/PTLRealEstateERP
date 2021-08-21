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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptDeliveryEfficiency : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Materials Delivery Efficiency Report";

            }

        }

        public string Getcomcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tbDeEffi"];

            ReportDocument rptstate = new RealERPRPT.R_14_Pro.rptDeliveryEfficiency();

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["date"] as TextObject;
            rptftdate.Text = "Date: " + this.txtfromdate.Text + " To " + this.txttodate.Text;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Transaction Statement";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadGrid()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Getcomcod();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "RPTDELIVERYEFF", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRptDelEff.DataSource = null;
                this.gvRptDelEff.DataBind();
                return;
            }

            Session["tbDeEffi"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            this.gvRptDelEff.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvRptDelEff.DataSource = (DataTable)Session["tbDeEffi"];
            this.gvRptDelEff.DataBind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
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

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvRptDelEff_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRptDelEff.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}











