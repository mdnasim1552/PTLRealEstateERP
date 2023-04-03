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

    public partial class InpPlanTopSheet : System.Web.UI.Page
    {

        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Money Receipt Top Sheet";
                string fDate = Convert.ToDateTime(System.DateTime.Today.AddDays(-(DateTime.Today.Day - 1))).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = fDate;
                this.txttodate.Text = Convert.ToDateTime(fDate).AddMonths(1).AddTicks(-1).ToString("dd-MMM-yyyy");
                this.rbtnList1.SelectedIndex = 0;
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
            string refnum = (this.rbtnList1.SelectedIndex == 0) ? "W" : "R";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_IMPEXECUTION", "GETIMPPLNTSHEET", frmdate, todate, refnum, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvWrkWise.DataSource = null;
                this.gvWrkWise.DataBind();
                this.gvMatWise.DataSource = null;
                this.gvMatWise.DataBind();
                return;
            }
            ViewState["tblWrkInfo"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblWrkInfo"];

            string rpt = this.rbtnList1.SelectedIndex.ToString();
            switch (rpt)
            {
                case "0":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.gvWrkWise.DataSource = HiddenSameData(dt);
                    this.gvWrkWise.DataBind();
                    this.FooterCalCulation();
                    break;

                case "1":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.gvMatWise.DataSource = HiddenSameData(dt);
                    this.gvMatWise.DataBind();
                    this.FooterCalCulation();
                    break;

            }


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
        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)ViewState["tblWrkInfo"];
            if (dt.Rows.Count == 0)
                return;
            string rpt = this.rbtnList1.SelectedIndex.ToString();
            switch (rpt)
            {
                case "0":
                    ((Label)this.gvWrkWise.FooterRow.FindControl("lblgvFwrkamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(wrkamt)", "")) ?
            0 : dt.Compute("sum(wrkamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    ((Label)this.gvWrkWise.FooterRow.FindControl("lblgvFmatamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(matamt)", "")) ?
            0 : dt.Compute("sum(matamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    Session["Report1"] = gvWrkWise;
                    //((HyperLink)this.gvWrkWise.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "1":
                    ((Label)this.gvMatWise.FooterRow.FindControl("lblgvFmatamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(matamt)", "")) ?
            0 : dt.Compute("sum(matamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                    Session["Report1"] = gvMatWise;
                    

                    break;

            }


        }
        protected void gvWrkWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //HyperLink hlnkMoneyRcptEdit = (HyperLink)e.Row.FindControl("hlnkMoneyRcptEdit");
                //HyperLink hlnkrcptPrint = (HyperLink)e.Row.FindControl("hlnkMoneyRcptPrint");
                //HyperLink hlnkrcptPrintACK = (HyperLink)e.Row.FindControl("hlnkMoneyRcptPrintACK");
                //LinkButton btnrcptPrint = (LinkButton)e.Row.FindControl("lnkMoneyRcptPrint");
                //string vouno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();


                //string comcod = this.GetCompCode();

                //string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                //string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();
                //string mrdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrdate")).ToString();

                //hlnkMoneyRcptEdit.NavigateUrl = "~/F_23_CR/MktMoneyReceipt?Type=Management&prjcode=" + pactcode + "&usircode=" + usircode + "&genno=" + mrno;
                //hlnkrcptPrintACK.NavigateUrl = "~/F_17_Acc/PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate+ "&rpType=ACK";
                //hlnkrcptPrint.NavigateUrl = "~/F_17_Acc/PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;

            }
        }

        protected void lnkMoneyRcptPrint_Click(object sender, EventArgs e)
        {

            

        }
    }
}