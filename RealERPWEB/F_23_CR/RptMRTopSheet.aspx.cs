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
                HyperLink hlnkrcptPrint = (HyperLink)e.Row.FindControl("hlnkMoneyRcptPrint");
                LinkButton btnrcptPrint = (LinkButton)e.Row.FindControl("lnkMoneyRcptPrint");
                string comcod = this.GetCompCode();
                switch (comcod)
                {
                    case "3101":
                    case "3368":
                        btnrcptPrint.Visible = true;
                        hlnkrcptPrint.Visible = false;
                        break;
                    default:
                        btnrcptPrint.Visible = false;
                        hlnkrcptPrint.Visible = true;
                        break;
                }
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();
                string mrdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrdate")).ToString();

                hlnkMoneyRcptEdit.NavigateUrl = "~/F_23_CR/MktMoneyReceipt?Type=Management&prjcode=" + pactcode + "&usircode=" + usircode + "&genno=" + mrno;
                hlnkrcptPrint.NavigateUrl = "~/F_17_Acc/PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;

            }
        }

        protected void lnkMoneyRcptPrint_Click(object sender, EventArgs e)
        {

            string PrintOpt = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //LinkButton rcptPrint = ((LinkButton)this.gvAccMR.Rows[index].FindControl("lnkMoneyRcptPrint"));

            string pactcode = ((Label)this.gvAccMR.Rows[index].FindControl("lgvpactcode")).Text.Trim().ToString();
            string usircode = ((Label)this.gvAccMR.Rows[index].FindControl("lgvusircode")).Text.Trim().ToString();
            string mrno = ((Label)this.gvAccMR.Rows[index].FindControl("lblgvMRNo")).Text.Trim().ToString();
            string mrdate = ((Label)this.gvAccMR.Rows[index].FindControl("lgvmrdate")).Text.Trim();

            //rcptPrint.PostBackUrl = "~/F_17_Acc/PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;


            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
            string currentptah = "PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate + "&PrintOpt=" + PrintOpt;
            string totalpath = hostname + currentptah;
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunMoneyReceipt('" + totalpath + "');", true);


         
        }
    }
}