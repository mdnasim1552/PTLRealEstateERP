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
namespace RealERPWEB.F_15_DPayReg
{
    public partial class RptOnlinePayment : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.SelectView();
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;
                this.lblHeader.Text = (type == "ChqSign") ? "Chequed Signature Report"
                    : (type == "RptPayApp") ? "Payment Approval Report" : (type == "RptPayRecPro") ? "" : "Payment Record";


            }

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetBankName()
        {
            string comcod = this.GetCompCode();
            string SearchBank = "%" + this.txtSerchBank.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "GETBANKCODE", SearchBank, "", "", "", "", "", "", "", "");
            this.ddlBankName.DataTextField = "actdesc";
            this.ddlBankName.DataValueField = "actcode";
            this.ddlBankName.DataSource = ds1;
            this.ddlBankName.DataBind();
            ds1.Dispose();



        }


        protected void ibtnSrchBank_Click(object sender, ImageClickEventArgs e)
        {
            this.GetBankName();
        }
        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChqSign":
                    this.GetBankName();
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "RptPayApp":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblChqNo.Text = "Ref no. ";
                    break;
                case "RptPayRec":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblChqNo.Text = "Ref no. ";
                    break;
                case "RptPayRecPro":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.lblChqNo.Text = "Ref no.2 ";
                    break;

            }
        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChqSign":
                    this.PrintChequeSign();
                    break;
                case "RptPayApp":
                    this.PrintChequeApproval();
                    break;
                case "RptPayRec":

                    break;

                case "RptPayRecPro":
                    this.PrintChequePro();
                    break;

            }
        }

        private void PrintChequeSign()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptAccOnlineChqSign();
            DataTable dt = (DataTable)ViewState["tbChqSign"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtdate.Text = "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintChequeApproval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptAccOnlinePay();
            DataTable dt = (DataTable)ViewState["tbChqSign"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtdate.Text = "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintChequePro()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptPaymentApproval();
            DataTable dt = (DataTable)ViewState["tbChqSign"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtdate.Text = "From: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To: " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChqSign":
                    this.ShowChqSign();
                    break;
                case "RptPayApp":
                    this.ShowChqApproval();
                    break;
                case "RptPayRec":
                    this.ShowChqRecord();
                    break;


                case "RptPayRecPro":
                    this.ShowChqPayRecPro();
                    break;

            }
        }


        private void ShowChqSign()
        {

            ViewState.Remove("tbChqSign");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";
            string Bankcode = ((this.ddlBankName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlBankName.SelectedValue.ToString());
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "RPTCHQSIGNSHEET", frmdate, todate, chequeno, Bankcode, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvChqSign.DataSource = null;
                this.gvChqSign.DataBind();
                return;
            }
            this.gvChqSign.Columns[1].Visible = (this.ddlBankName.SelectedValue == "000000000000") ? true : false;
            ViewState["tbChqSign"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ShowChqApproval()
        {

            ViewState.Remove("tbChqSign");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "RPTCHEQUEAPP", frmdate, todate, chequeno, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayment.DataSource = null;
                this.gvPayment.DataBind();
                return;
            }
            //this.gvChqSign.Columns[1].Visible = (this.ddlBankName.SelectedValue == "000000000000") ? true : false;
            ViewState["tbChqSign"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ShowChqRecord()
        {

            ViewState.Remove("tbChqSign");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "RPTPAYRECORD", frmdate, todate, chequeno, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayRec.DataSource = null;
                this.gvPayRec.DataBind();
                return;
            }
            ViewState["tbChqSign"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ShowChqPayRecPro()
        {

            ViewState.Remove("tbChqSign");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string chequeno = "%" + this.txtSrchChequeno.Text.Trim() + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "RPTAPPROVED", frmdate, todate, chequeno, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPayapp.DataSource = null;
                this.gvPayapp.DataBind();
                return;
            }
            ViewState["tbChqSign"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();


        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)ViewState["tbChqSign"];
            switch (type)
            {

                case "ChqSign":
                    this.gvChqSign.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvChqSign.DataSource = dt;
                    this.gvChqSign.DataBind();
                    this.FooterCalculation();
                    break;
                case "RptPayApp":
                    this.gvPayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPayment.DataSource = dt;
                    this.gvPayment.DataBind();
                    this.FooterCalculation();
                    break;
                case "RptPayRec":
                    this.gvPayRec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPayRec.DataSource = dt;
                    this.gvPayRec.DataBind();
                    this.FooterCalculation();
                    break;

                case "RptPayRecPro":
                    this.gvPayapp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPayapp.DataSource = dt;
                    this.gvPayapp.DataBind();

                    break;
            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string useridapp = dt1.Rows[0]["useridapp"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();
            switch (type)
            {
                case "ChqSign":

                    string cactcode = dt1.Rows[0]["cactcode"].ToString();
                    string vounum = dt1.Rows[0]["vounum"].ToString();
                    string rescode = dt1.Rows[0]["rescode"].ToString();
                    string isunum = dt1.Rows[0]["isunum"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();
                            dt1.Rows[j]["usrdesig"] = "";
                        }

                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                        }
                        if (dt1.Rows[j]["cactcode"].ToString() == cactcode)
                        {
                            actcode = dt1.Rows[j]["cactcode"].ToString();
                            dt1.Rows[j]["cactdesc"] = "";
                        }
                        if (dt1.Rows[j]["vounum"].ToString() == vounum)
                        {
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat"] = "";
                            dt1.Rows[j]["vernar"] = "";
                            dt1.Rows[j]["payto"] = "";
                            dt1.Rows[j]["trnrmrk"] = "";
                            dt1.Rows[j]["appamt"] = 0.00;

                        }
                        if (dt1.Rows[j]["rescode"].ToString() == rescode)
                        {
                            dt1.Rows[j]["resdesc"] = "";
                        }
                        if (dt1.Rows[j]["isunum"].ToString() == isunum)
                        {
                            dt1.Rows[j]["isunum"] = "";
                        }
                        useridapp = dt1.Rows[j]["useridapp"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                        cactcode = dt1.Rows[j]["cactcode"].ToString();
                        vounum = dt1.Rows[j]["vounum"].ToString();
                        rescode = dt1.Rows[j]["rescode"].ToString();
                        isunum = dt1.Rows[j]["isunum"].ToString();
                    }
                    break;
                case "RptPayApp":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp && dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["usrdesig"] = "";
                            dt1.Rows[j]["actdesc"] = "";
                        }
                        if (dt1.Rows[j]["useridapp"].ToString() == useridapp)
                        {
                            useridapp = dt1.Rows[j]["useridapp"].ToString();

                            dt1.Rows[j]["usrdesig"] = "";

                        }

                        //if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        //{
                        //    actcode = dt1.Rows[j]["actcode"].ToString();
                        //    dt1.Rows[j]["actdesc"] = "";
                        //}

                        useridapp = dt1.Rows[j]["useridapp"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }
                    break;
                case "RptPayRec":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                        }

                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }
                    break;
            }

            return dt1;

        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["tbChqSign"];

            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChqSign":
                    ((Label)this.gvChqSign.FooterRow.FindControl("lblFaTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(appamt)", "")) ?
                               0 : dt1.Compute("sum(appamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvChqSign.FooterRow.FindControl("lblFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(trnam)", "")) ?
                                0 : dt1.Compute("sum(trnam)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "RptPayApp":
                    ((Label)this.gvPayment.FooterRow.FindControl("lblFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt)", "")) ? 0.00 : dt1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");
                    ((Label)this.gvPayment.FooterRow.FindControl("lblFApamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(apamt)", "")) ? 0.00 : dt1.Compute("Sum(apamt)", ""))).ToString("#,##0;(#,##0); -");
                    ((Label)this.gvPayment.FooterRow.FindControl("lblFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(balamt)", "")) ? 0.00 : dt1.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); -");
                    break;
                case "RptPayRec":
                    ((Label)this.gvPayRec.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt)", "")) ? 0.00 : dt1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");
                    ((Label)this.gvPayRec.FooterRow.FindControl("txtFAdvTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(advamt)", "")) ? 0.00 : dt1.Compute("Sum(advamt)", ""))).ToString("#,##0;(#,##0); -");
                    ((Label)this.gvPayRec.FooterRow.FindControl("txtFNetTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(netamt)", "")) ? 0.00 : dt1.Compute("Sum(netamt)", ""))).ToString("#,##0;(#,##0); -");
                    break;
                case "RptPayRecPro":
                    //((Label)this.gvPayRec.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt)", "")) ? 0.00 : dt1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); -");

                    break;


            }



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvChqdep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvChqSign.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void imgSearchCheque_Click(object sender, ImageClickEventArgs e)
        {
            this.lbtnOk_Click(null, null);
        }



        protected void gvChqdep_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            this.gvChqSign.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }



        protected void gvPayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPayment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}











