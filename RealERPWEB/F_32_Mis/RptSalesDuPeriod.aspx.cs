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
namespace RealERPWEB.F_32_Mis
{


    public partial class RptSalesDuPeriod : System.Web.UI.Page
    {
        ProcessAccess prjData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "SALES DURING THE PERIOD";
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblcost");
            string comcod = this.GetComeCode();

            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = prjData.GetTransInfo(comcod, "SP_REPORT_ACCOUNT_SALEDP", "RPTSALEDPERIOD", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvsaldperiod.DataSource = null;
                this.gvsaldperiod.DataBind();
                return;

            }

            ViewState["tblcost"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void Data_Bind()
        {
            this.gvsaldperiod.DataSource = (DataTable)ViewState["tblcost"];
            this.gvsaldperiod.DataBind();
            this.FooteCalculation();

        }

        private void FooteCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblcost"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvsaldperiod.FooterRow.FindControl("lgvFExpenditure")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ?
                                     0.00 : dt.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsaldperiod.FooterRow.FindControl("lgvFSales")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salam)", "")) ?
                                    0.00 : dt.Compute("sum(salam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvsaldperiod.FooterRow.FindControl("lgvFMaramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(maram)", "")) ?
                                    0.00 : dt.Compute("sum(maram)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblcost"];
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptsaldperiod = new RealERPRPT.R_32_Mis.RptSalesDuringPeriod();
            TextObject rptdate = rptsaldperiod.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptdate.Text = "(From " + frmdate + " To " + todate + ")";
            TextObject txtuserinfo = rptsaldperiod.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsaldperiod.SetDataSource(dt);
            string comcod = hst["comcod"].ToString();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Sales During the Peroid";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsaldperiod.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsaldperiod;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}