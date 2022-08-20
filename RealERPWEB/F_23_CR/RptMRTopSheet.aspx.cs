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
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{

    public partial class RptMRTopSheet : System.Web.UI.Page
    {

        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Money Receipt Top Sheet";
                string fDate = Convert.ToDateTime(System.DateTime.Today.AddDays(-(DateTime.Today.Day - 1))).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = fDate;
                this.txttodate.Text = Convert.ToDateTime(fDate).AddMonths(1).AddTicks(-1).ToString("dd-MMM-yyyy");

                this.lbtnOk_Click(null, null);

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void lbtnOk_Click(object sender, EventArgs e) 
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string refnum = "%" + this.txtrefno.Text.Trim() + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONEYRECEIPTTOPSHEET", frmdate, todate, refnum, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvAccMR.DataSource = null;
                this.gvAccMR.DataBind();
                return;
            }
            Session["tblMRInfo"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblMRInfo"];
            this.gvAccMR.DataSource = dt;
            this.gvAccMR.DataBind();
            this.FooterCalCulation();
        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblMRInfo"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvAccMR.FooterRow.FindControl("lblgvFMRAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");
            Session["Report1"] = gvAccMR;
            ((HyperLink)this.gvAccMR.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }
        protected void gvAccMR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlnkMoneyRcptEdit = (HyperLink)e.Row.FindControl("hlnkMoneyRcptEdit");
                HyperLink hlnkPrintMoneyRcpt = (HyperLink)e.Row.FindControl("hlnkMoneyRcptPrint");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();
                string mrdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrdate")).ToString();

                hlnkMoneyRcptEdit.NavigateUrl = "~/F_23_CR/MktMoneyReceipt?Type=Management&prjcode=" + pactcode + "&usircode=" + usircode + "&genno=" + mrno;
                hlnkPrintMoneyRcpt.NavigateUrl = "~/F_17_Acc/PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;

            }
        }
    }
}