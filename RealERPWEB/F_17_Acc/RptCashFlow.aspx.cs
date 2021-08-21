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
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using RealEntity;
namespace RealERPWEB.F_17_Acc
{

    public partial class RptCashFlow : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static double percent = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                ((Label)this.Master.FindControl("lblTitle")).Text = "Transactions";

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void btnok_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string frmdate = this.txtDateFrom.Text;
            string todate = this.txtDateto.Text;


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "RECEIPTPAYMETCASHFLOW", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvcashflow.DataSource = null;
                this.gvcashflow.DataBind();
                return;
            }



            Session["cashflow"] = ds1.Tables[0];


            this.Data_Bind();



        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["cashflow"];
            this.gvcashflow.DataSource = dt;
            this.gvcashflow.DataBind();
            this.Graph();

        }

        private void Graph()
        {
            DataTable dt = (DataTable)Session["cashflow"];
            var lst = dt.DataTableToList<RealEntity.C_09_PIMP.EClassOrder.EClassCashFlow>();
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);


            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteGraph('" + json + "')", true);

        }
        protected void gvcashflow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            //string mPACTCODE = this.Request.QueryString["pactcode"].ToString();

            string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
            //string mTRNDAT1 = this.Request.QueryString["date"].ToString();

            string mTRNDAT1 = this.txtDateFrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            switch (grp)
            {
                case "A":
                    hlink1.NavigateUrl = "~/F_17_Acc/LinkRptReciptPayment.aspx?Type=receipt&comcod=" + comcod + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                    break;
                case "B":
                    hlink1.NavigateUrl = "~/F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=" + comcod + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                    break;
            }
            //if (grp == "B")
            //{
            //    hlink1.NavigateUrl = "~/F_17_Acc/LinkRptReciptPayment.aspx?Type=payment&comcod=" + comcod + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //    hlink1.NavigateUrl = "~/F_17_Acc/LinkRptReciptPayment.aspx?Type=receipt&comcod=" + comcod + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //}



            // hlink1.NavigateUrl = "~/F_32_Mis/RptLinkImpExeStatus.aspx?Type=BgdAll&comcod=" + comcod + "&pactcode=" + mPACTCODE + "&FlrCode=" + mFlrCode + "&Date1=" + mTRNDAT1;

            //hlink1.NavigateUrl = "~/F_17_Acc/RptReciptPayment.aspx=comcod=" + comcod + "&pactcode=" + mPACTCODE + "&FlrCode=" + mFlrCode + "&Date1=" + mTRNDAT1;

        }
    }
}