using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRDLC;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_15_DPayReg
{

    public partial class BillRegInterface : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));




                ((Label)this.Master.FindControl("lblTitle")).Text = "Bill Register";//
                                                                                    //this.txtFDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                //this.SaleRequRpt();

                txtdate_TextChanged(null, null);
                this.totalCount();

                //this.RadioButtonList1_SelectedIndexChanged(null, null);

            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3333":
                    this.Timer1.Interval = 10000; // 15 Secnod
                    break;

                case "3336":
                    this.Timer1.Interval = 300000; // 5 Min
                    break;



                default:
                    this.Timer1.Interval = 30000; //30 Second
                    break;



            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? comcod;
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.BillRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            this.BillRequRpt();
        }

        private void totalCount()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string ttsrch = "%";
            string searchbill = "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT", "GETBILLNO", ttsrch, searchbill, "", "", "", "", "", "", "");
            Session["BillAmt"] = ds1.Tables[0];
            this.lblbill.Text = (ds1.Tables[1].Rows.Count == 0) ? "0" : Convert.ToDouble(ds1.Tables[1].Rows[0]["billno"]).ToString("#,##0;(#,##0); ");// ds1.Tables[1].Rows[0]["billno"].ToString();
            this.lblpanding.Text = (ds1.Tables[2].Rows.Count == 0) ? "0" : Convert.ToDouble(ds1.Tables[2].Rows[0]["amt"]).ToString("#,##0;(#,##0); ");


        }

        private void BillRequRpt()

        {

            //DataTable dt = (DataTable)Session["BillAmt"];
            string comcod = this.GetCompCode();
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_ONLINE_PAYMENT_02", "RPTCHQREGSHEET", Date, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            string ftype = this.getForwordtype();

            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["billqty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Payment Proposal" + "</span>";

            this.RadioButtonList1.Items[1].Text = "<span class='fa  fa-cog fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["recmqty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Checked" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa  fa-cog fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["forward"].ToString() + "</span>" + "<span class='lbldata2'>" + ftype + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-balance-scale fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["appqty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Approval" + "</span>";

            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-hourglass-end fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["issueqty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Cheque Issue" + "</span>";
            this.RadioButtonList1.Items[5].Text = "<span class='fa fa-hourglass-end fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["chqsign"].ToString() + "</span>" + "<span class='lbldata2'>" + "Cheque Signature Sheet" + "</span>";
            this.RadioButtonList1.Items[6].Text = "<span class='fa  fa-check fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["cprty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Pay To Party" + "</span>";

            //this.RadioButtonList1.Items[5].Text = "<span class='fa fa-hourglass-end fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["issueqty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Cheque Issue" + "</span>";
            //this.RadioButtonList1.Items[6].Text = "<span class='fa fa-hourglass-end fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["chqsign"].ToString() + "</span>" + "<span class='lbldata2'>" + "Cheque Signature Sheet" + "</span>";
            //this.RadioButtonList1.Items[7].Text = "<span class='fa  fa-check fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + ds1.Tables[3].Rows[0]["cprty"].ToString() + "</span>" + "<span class='lbldata2'>" + "Pay To Party" + "</span>";




            //switch (comcod)
            //{

            //    case "3335":

            //        this.RadioButtonList1.Items.RemoveAt(6);
            //        this.RadioButtonList1.Items.RemoveAt(5);
            //        break;

            //    default:
            //        break;
            //}


            //  this.RadioButtonList1.Items[1].Text = "<table><tr><th rowspan='2' style='width:50px;background:#19F2C7 none repeat scroll 0 0;border: medium none;height: 20px;'><span class='fa  fa-cog fan'></span></th>" + "<td  style=' padding-top: 2px; font-size: 20px; height:24px;width: 133px;background:#fff none repeat scroll 0 0;border: medium none;color:#000;'>" + ds1.Tables[1].Rows[0]["recmqty"].ToString() + "</td>" + "</tr>" + "<tr>" + "<td  style='line-height:28px; height:23px;width: 133px; background:#9CFFEB none repeat scroll 0 0; color:#000; border: medium none;text-align: center;font-size: 11px;'>" + "Recommendation" + "</td></tr></table>";
            // this.RadioButtonList1.Items[2].Text = "<table><tr><th rowspan='2' style='width:50px;background:#C3E574 none repeat scroll 0 0;border: medium none;height: 20px;'><span class='fa fa-balance-scale fan'></span></th>" + "<td  style=' padding-top: 2px; font-size: 20px; height:24px;width: 133px;background:#fff none repeat scroll 0 0;border: medium none;color:#000;'>" + ds1.Tables[1].Rows[0]["appqty"].ToString() + "</td>" + "</tr>" + "<tr>" + "<td  style='line-height:28px; height:23px;width: 133px;background:#E7FCB9 none repeat scroll 0 0; color:#000; border: medium none;text-align: center;'>" + "Approval" + "</td></tr></table>";
            //this.RadioButtonList1.Items[3].Text = "<table><tr><th rowspan='2' style='width:50px;background:#FCC970 none repeat scroll 0 0;border: medium none;height: 20px;'><span class='fa fa-hourglass-end fan'></span></th>" + "<td  style=' padding-top: 2px; font-size: 20px; height:24px;width: 133px;background:#fff none repeat scroll 0 0;border: medium none;color:#000;'>" + ds1.Tables[1].Rows[0]["issueqty"].ToString() + "</td>" + "</tr>" + "<tr>" + "<td  style='line-height:28px; height:23px;width: 133px; background:#FFE1AB none repeat scroll 0 0; color:#000; border: medium none;text-align: center;'>" + "Cheque Issue" + "</td></tr></table>";
            //  this.RadioButtonList1.Items[4].Text = "<table><tr><th rowspan='2' style='width:50px;background:#BC3C2F none repeat scroll 0 0;border: medium none;height: 20px;'><span class='fa  fa-check fan'></span></th>" + "<td  style=' padding-top: 2px; font-size: 20px; height:24px;width: 133px;background:#fff none repeat scroll 0 0;border: medium none;color:#000;'>" + ds1.Tables[1].Rows[0]["compqty"].ToString() + "</td>" + "</tr>" + "<tr>" + "<td  style='line-height:28px; height:23px;width: 133px;background:#FCACA5 none repeat scroll 0 0; color:#000; border: medium none;text-align: center;'>" + "Total Completed" + "</td></tr></table>";


            // All Order

            Session["tlbbill"] = ds1.Tables[0];

            DataTable dt = new DataTable();
            DataView dv;
            //Total Bill
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            this.Data_Bind("gvBillInfo", dv.ToTable());

            ////Recommendation
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("rec = 'B' ");
            this.Data_Bind("grvRecm", dv.ToTable());
            ViewState["grvRecmBill"] = dt;
            //Forward

            ////Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("forward = 'C' ");
            this.Data_Bind("gvforward", dv.ToTable());
            ViewState["ForwardBill"] = dt;
            ////Approved
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("approved = 'D' ");
            this.Data_Bind("grvApproved", dv.ToTable());

            ViewState["VAproved"] = dt;

            ////Issued
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("issued = 'E' ");
            this.Data_Bind("grvIssued", dv.ToTable());

            ////pnlChequeSign
            dt = ((DataTable)ds1.Tables[1]).Copy();
            dv = dt.DefaultView;
            ViewState["ChequeSign"] = dv.ToTable();

            // dv.RowFilter = ("issued = 'D' ");
            this.Data_Bind("gvChequeSign", dv.ToTable());


            ////Complited
            dt = ((DataTable)ds1.Tables[2]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("CQPAYTPPARTY = 'False' ");
            ViewState["tblcqkpary"] = dv.ToTable();
            this.Data_Bind("grvComp", dv.ToTable());

        }

        private string getForwordtype()
        {
            string comcod = this.GetCompCode();
            string ftype = "";

            switch (comcod)
            {
                //case "3101":
                case "3355":
                    ftype = "Verified";
                    break;

                default:
                    ftype = "Forward";
                    break;
            }
            return ftype;
        }
        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {
                case "gvBillInfo":
                    this.gvBillInfo.DataSource = dt;
                    this.gvBillInfo.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.gvBillInfo.FooterRow.FindControl("lblFTotal")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "grvRecm":
                    this.grvRecm.DataSource = dt;
                    this.grvRecm.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.grvRecm.FooterRow.FindControl("lblFTotal")).Text =
               Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
               0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "gvforward":
                    this.gvforward.DataSource = dt;
                    this.gvforward.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.gvforward.FooterRow.FindControl("lblFTotalfr")).Text =
               Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
               0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "grvApproved":
                    this.grvApproved.DataSource = dt;
                    this.grvApproved.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.grvApproved.FooterRow.FindControl("lblFTotal")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt)", "")) ?
                0.00 : dt.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "grvIssued":
                    this.grvIssued.DataSource = dt;
                    this.grvIssued.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.grvIssued.FooterRow.FindControl("lblFTotal")).Text =
                Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(issuedamt)", "")) ?
                0.00 : dt.Compute("Sum(issuedamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "gvChequeSign":
                    this.gvChequeSign.DataSource = dt;
                    this.gvChequeSign.DataBind();
                    break;

                case "grvComp":
                    this.grvComp.DataSource = dt;
                    this.grvComp.DataBind();
                    break;
            }
        }


        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            this.lblprintstkl.Text = "";
            string value = this.RadioButtonList1.SelectedValue.ToString();

            switch (value)
            {
                case "0":


                    this.pnlBillInfo.Visible = true;
                    this.PnlRecm.Visible = false;
                    this.pnlforward.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlComp.Visible = false;
                    this.pnlChequeSign.Visible = false;
                    this.RadioButtonList1.Items[0].Attributes.Add("class", "lblactive");


                    break;

                case "1":
                    this.pnlBillInfo.Visible = false;
                    this.PnlRecm.Visible = true;
                    this.pnlforward.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.PnlComp.Visible = false;
                    this.pnlChequeSign.Visible = false;

                    this.RadioButtonList1.Items[1].Attributes.Add("class", "lblactive");
                    break;
                case "2":
                    this.pnlBillInfo.Visible = false;
                    this.PnlRecm.Visible = false;
                    this.pnlforward.Visible = true;
                    this.PanelApproved.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.pnlChequeSign.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[2].Attributes.Add("class", "lblactive");


                    break;

                case "3":
                    this.pnlBillInfo.Visible = false;
                    this.PnlRecm.Visible = false;
                    this.pnlforward.Visible = false;
                    this.PanelApproved.Visible = true;
                    this.PnlIssued.Visible = false;
                    this.pnlChequeSign.Visible = false;
                    this.PnlComp.Visible = false;
                    this.RadioButtonList1.Items[3].Attributes.Add("class", "lblactive");

                    break;
                case "4":
                    this.pnlBillInfo.Visible = false;
                    this.PnlRecm.Visible = false;
                    this.pnlforward.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.PnlIssued.Visible = true;
                    this.PnlComp.Visible = false;
                    this.pnlChequeSign.Visible = false;
                    this.RadioButtonList1.Items[4].Attributes.Add("class", "lblactive");

                    break;

                case "5":
                    this.pnlBillInfo.Visible = false;
                    this.PnlRecm.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.pnlChequeSign.Visible = true;
                    this.PnlComp.Visible = false;
                    this.pnlforward.Visible = false;

                    this.RadioButtonList1.Items[5].Attributes.Add("class", "lblactive");

                    break;

                case "6":
                    this.pnlBillInfo.Visible = false;
                    this.PnlRecm.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.PnlIssued.Visible = false;
                    this.pnlChequeSign.Visible = false;
                    this.PnlComp.Visible = true;
                    this.pnlforward.Visible = false;

                    this.RadioButtonList1.Items[6].Attributes.Add("class", "lblactive");

                    break;

                    //case "7":

                    //    this.pnlBillInfo.Visible = false;
                    //    this.PnlRecm.Visible = false;
                    //    this.PanelApproved.Visible = false;
                    //    this.PnlIssued.Visible = false;
                    //    this.pnlChequeSign.Visible = false;
                    //    this.PnlComp.Visible = false;
                    //    this.pnlforward.Visible = false;
                    //    this.pnlApprovedCancel.Visible = true;

                    //    this.RadioButtonList1.Items[6].Attributes.Add("class", "lblactive");

                    //    break;
            }

        }


        protected void grvRecm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("HyInprReqno11");



                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string payid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum")).ToString();
                //string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;

                hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa?Type=ChequeReady" + "&payid=" + payid;
                hlink3.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk";

            }
        }
        protected void gvforward_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrintfr");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntryfr");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lbgvreqnofr");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string payid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum")).ToString();
                //string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;

                hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa?Type=ChequeApproval" + "&payid=" + payid;

                hlink3.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk";

            }
        }
        protected void grvApproved_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("lbgvreqno");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string payid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "slnum")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;
                //hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa?Type=ChequeApproval";
                hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentApp?Type=ChequePayment&comcod=" + comcod + "&payid=" + payid;
                hlink3.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk";


            }
        }

        protected void gvAppCancel_RowDataBound(object sender, EventArgs e)
        {








        }





        protected void grvIssued_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");
                HyperLink hlink3 = (HyperLink)e.Row.FindControl("hypreno2");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = GetCompCode();


                string pymdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "appisedate")).ToString();
                string slnum = DataBinder.Eval(e.Row.DataItem, "slnum").ToString();

                string actcode = DataBinder.Eval(e.Row.DataItem, "actcode").ToString();
                string reqno = DataBinder.Eval(e.Row.DataItem, "reqno").ToString();
                string billno = DataBinder.Eval(e.Row.DataItem, "billno").ToString();


                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                //string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;
                //if (comcod == "3339")
                //{
                //    hlink2.NavigateUrl = "~/F_15_DPayReg/ChequeSignSheet?Type=Acc&slnum=";
                //}
                //else
                //{
                //    hlink2.NavigateUrl = "~/F_15_DPayReg/ChequeSignSheet?Type=Acc&slnum=" + slnum;
                //}

                hlink1.NavigateUrl = "~/F_15_DPayReg/ChequeSignSheet?Type=Acc&slnum=" + slnum + "&actcode=" + actcode + "&reqno=" + reqno + "&billno=" + billno;

                hlink2.NavigateUrl = "~/F_15_DPayReg/ChequeSignSheet?Type=Acc&slnum=" + slnum;

                hlink3.NavigateUrl = "~/F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk&comcod=&Date1=&Date2=";

            }
        }
        protected void grvComp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyInprPrint");
                //HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEntry");

                //Hashtable hst = (Hashtable)Session["tblLogin"];
                //string comcod = GetCompCode();
                //string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
                //string recvno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recvno")).ToString();
                //string imesimeno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mimei")).ToString();

                //hlink1.NavigateUrl = "~/F_20_Service/Ser_Print?Type=ProReceived&comcod=" + comcod + "&centrid=" + centrid + "&recvno=" + recvno + "&imesimeno=" + imesimeno;

                //hlink2.NavigateUrl = "~/F_15_Acc/AccSales";

            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "2":
                    this.PrintForward();
                    break;
                case "3":
                    this.PrintApproved();
                    break;

                case "4":
                    this.PrintChIssue();
                    break;
                case "5":

                    this.PrintChequeSign();
                    break;

                case "6":
                    this.PrintPaytoParty();
                    break;
                default:
                    //this.PrintApproved();
                    break;
            }
        }

        private void PrintForward()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Cdate = "Date: " + this.txtdate.Text.ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            LocalReport Rpt1 = new LocalReport();

            DataTable dt1 = (DataTable)ViewState["ForwardBill"];
            DataTable dt = new DataTable();
            DataView dv;

            dv = dt1.DefaultView;
            dv.RowFilter = ("forward = 'C' ");
            dt = dv.ToTable();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillForward>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_15_DPayReg.RptBillForward", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Cdate", Cdate));
            Rpt1.SetParameters(new ReportParameter("rptname", "Bill Forward"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintApproved()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Cdate = "Date: " + this.txtdate.Text.ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            LocalReport Rpt1 = new LocalReport();

            DataTable dt1 = (DataTable)ViewState["VAproved"];
            DataTable dt = new DataTable();
            DataView dv;

            dv = dt1.DefaultView;

            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    dv.RowFilter = ("rec = 'B' ");
                    break;
                case "2":
                    dv.RowFilter = ("forward = 'C' ");
                    break;
                case "3":
                    dv.RowFilter = ("approved = 'D' ");
                    break;

                    //case "4":
                    //    dv.RowFilter = ("issued = 'E' ");
                    //    break;
            }

            dt = dv.ToTable();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillAproved01>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_15_DPayReg.RptBillAproved", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Cdate", Cdate));
            Rpt1.SetParameters(new ReportParameter("rptname", "Approval"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintChIssue()
        {
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Cdate = "Date: " + this.txtdate.Text.ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            LocalReport Rpt1 = new LocalReport();

            DataTable dt1 = (DataTable)ViewState["VAproved"];
            DataTable dt = new DataTable();
            DataView dv;

            dv = dt1.DefaultView;
            dv.RowFilter = ("issued = 'E' ");
            dt = dv.ToTable();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillAproved01>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_15_DPayReg.RptBillIssue", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Cdate", Cdate));
            Rpt1.SetParameters(new ReportParameter("rptname", "Cheque Issue"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintChequeSign()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptChequeSignatureSheet();
            DataTable dt = (DataTable)ViewState["ChequeSign"];
            //ViewState["ChequeSign"] = dv.ToTable();  
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxttxtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            rpttxttxtTitle.Text = "Cheque Signature Sheet";
            TextObject rpttxtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtDate.Text = "Date : " + Date;
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtdate.Text = "Date: "+ System.DateTime.Today.ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintPaytoParty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");


            ReportDocument rptstk = new RealERPRPT.R_15_DPayReg.RptChequeSignatureSheet();
            DataTable dt = (DataTable)ViewState["tblcqkpary"];
            //ViewState["ChequeSign"] = dv.ToTable();  
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject rpttxttxtTitle = rptstk.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            rpttxttxtTitle.Text = "Pay To Party";
            TextObject rpttxtDate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtDate.Text = "Date : " + Date;
            //TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //rpttxtdate.Text = "Date: "+ System.DateTime.Today.ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            lbtnOk_Click(null, null);
        }


        private void Session_update()
        {
            DataTable dt = (DataTable)ViewState["tblcqkpary"];
            int index;
            for (int i = 0; i < this.grvComp.Rows.Count; i++)
            {
                string chkper = (((CheckBox)grvComp.Rows[i].FindControl("chkActive")).Checked) ? "True" : "False";

                index = (this.grvComp.PageSize) * (this.grvComp.PageIndex) + i;
                dt.Rows[index]["CQPAYTPPARTY"] = chkper;

            }
            ViewState["tblcqkpary"] = dt;
        }

        protected void lbtnUpdate11_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            this.Session_update();

            DataTable dt1 = (DataTable)ViewState["tblcqkpary"];

            bool result = false;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string slnum = dt1.Rows[i]["slnum"].ToString().Trim();
                string chkper = dt1.Rows[i]["CQPAYTPPARTY"].ToString().Trim();

                if (chkper == "True")
                {
                    result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "UPDATECQPAYPARTY", slnum, chkper, "", "", "", "", "", "", "", "", "", "", "", "");
                }

            }
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

        }


        protected void gvBillInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypBill = (HyperLink)e.Row.FindControl("hypBill");
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();

                string custbill = ASTUtility.Left(billno, 3).ToString();

                if (custbill == "PBL")
                {
                    hypBill.NavigateUrl = "F_14_Pro/RptPurchaseStatus?Type=Purchase&Rpt=PurBilltk&genno=" + billno;

                }
                else if (custbill == "CBL")
                {


                    hypBill.NavigateUrl = "F_09_PImp/PurSubConBillFinal?Type=BillEdit&genno=" + billno;

                }

                else
                {

                }





                // hlnkempname.NavigateUrl = "~/F_21_Kpi/LinkEmpMonthWiseEva?empid=" + empid + "&empname=" + empname + "&Date1=" + fromdate + "&Date2=" + todate;

            }



        }


        protected void grvRecm_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            DataTable dt = (DataTable)Session["tlbbill"];

            string slno = ((Label)this.grvRecm.Rows[e.RowIndex].FindControl("lblgvreqno1payma")).Text.Trim();
            string billno = ((Label)this.grvRecm.Rows[e.RowIndex].FindControl("lbgvbillno")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "DELETEBILLNO", slno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                int rowindex = (this.grvRecm.PageSize) * (this.grvRecm.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                DataView dv = dt.DefaultView;
                dv.RowFilter = ("rec = 'B' ");
                dt = dv.ToTable();
                this.grvRecm.DataSource = dt;
                this.grvRecm.DataBind();
                //this.lTotalPayment_Click(null, null);


            }
        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.grvComp.HeaderRow.FindControl("chkAll")).Checked)
            {

                for (i = 0; i < this.grvComp.Rows.Count; i++)
                {
                    ((CheckBox)this.grvComp.Rows[i].FindControl("chkActive")).Checked = true;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.grvComp.Rows.Count; i++)
                {
                    ((CheckBox)this.grvComp.Rows[i].FindControl("chkActive")).Checked = false;
                    //index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    //dt.Rows[i]["chkper"] = "False";

                }

            }
        }
        protected void lnkbtnMerge_Click(object sender, EventArgs e)
        {
            // string []paymentid=new string[100];
            int i = 0;
            string mergeslnum = "";
            foreach (GridViewRow gv1 in grvIssued.Rows)
            {
                string chkemerge = ((CheckBox)gv1.FindControl("chkmerge")).Checked ? "True" : "False";
                string slnum = ((Label)gv1.FindControl("lblgvreqno12")).Text.Trim();
                if (chkemerge == "True")
                {
                    mergeslnum += slnum + ",";
                    // paymentid[i++] = slnum;
                }
            }

            mergeslnum = mergeslnum.Length > 0 ? mergeslnum.Trim().Substring(0, mergeslnum.Length - 1) : "";
            string[] paymentid = mergeslnum.Split(',');

            if (paymentid.Length > 0)
            {
                string mslnum = "";
                string comcod = this.GetCompCode();
                foreach (string slnum in paymentid)
                {


                    if (i == 0)
                    {
                        mslnum = slnum;
                        i++;
                    }




                    bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "MERGESLNUM", slnum, mslnum, "", "", "", "", "", "", "", "", "", "", "", "", "");
                    if (!result)
                    {

                        ((Label)this.Master.FindControl("lblmsg")).Text = "Merge Failed.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                        return;

                    }

                }

                ((Label)this.Master.FindControl("lblmsg")).Text = "Merge .Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.BillRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);

            }

            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Please select Item";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }


        protected void lnkbtnPayIdAppCancel_Click(object sender, EventArgs e)
        {

        }
        protected void lnkbtnPayId_Click(object sender, EventArgs e)
        {

            // string []paymentid=new string[100];
            string comcod = this.GetCompCode();
            int i = 0;
            string appslnum = "";
            foreach (GridViewRow gv1 in grvApproved.Rows)
            {
                string chkemerge = ((CheckBox)gv1.FindControl("chkpayid")).Checked ? "True" : "False";
                string slnum = ((Label)gv1.FindControl("lblpayidapp")).Text.Trim();
                if (chkemerge == "True")
                {
                    appslnum += slnum;
                    // paymentid[i++] = slnum;
                }
            }

            //  appslnum = appslnum.Length > 0 ? appslnum.Trim().Substring(0, appslnum.Length - 1) : "";


            string url = "AccOnlinePaymentApp?Type=ChequePayment&comcod=" + comcod + "&payid=" + appslnum;

            //((LinkButton)this.grvApproved.FooterRow.FindControl("lnkbtnPayId")).PostBackUrl = url;









            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunApprovedBill('" + url + "');", true);


            //string url = "~/F_15_DPayReg/AccOnlinePaymentApp?Type=ChequePayment&comcod="  + comcod + "&payid=" + appslnum;
            //Response.Redirect(url);

            //Page.redi

            //hlink1.NavigateUrl = "~/F_99_Allinterface/PuchasePrint?Type=BillPrint&comcod=" + comcod + "&billno=" + billno;

            //hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa?Type=ChequeReady" + "&payid=" + payid; 
            //string[] paymentid = mergeslnum.Split(',');

            //if (paymentid.Length > 0)
            //{
            //    string mslnum = "";
            //    string comcod = this.GetCompCode();
            //    foreach (string slnum in paymentid)
            //    {


            //        if (i == 0)
            //        {
            //            mslnum = slnum;
            //            i++;
            //        }




            //        bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "MERGESLNUM", slnum, mslnum, "", "", "", "", "", "", "", "", "", "", "", "", "");
            //        if (!result)
            //        {

            //            ((Label)this.Master.FindControl("lblmsg")).Text = "Merge Failed.";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //            return;

            //        }

            //    }

            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Merge .Successfully";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            //    this.BillRequRpt();
            //    this.RadioButtonList1_SelectedIndexChanged(null, null);

            //}

            //else
            //{

            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Please select Item";
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //}

        }
        protected void btnDelCheque_Click(object sender, EventArgs e)
        {
            string url = "ChequeSignSheet?Type=Acc";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;

            string slno = ((Label)this.grvIssued.Rows[rowindex].FindControl("lblgvreqno12")).Text.Trim();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETECHEQUEISSUE", slno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.BillRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('delete  Fail');", true);

            }

        }



        protected void btnDelApproval_Click(object sender, EventArgs e)
        {

            string url = "AccOnlinePaymentApp?Type=ChequePayment";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;

            string slno = ((Label)this.grvIssued.Rows[rowindex].FindControl("lblgvreqno12")).Text.Trim();


            //string actcode = ((Label)this.grvIssued.Rows[rowindex].FindControl("lblgvactcode")).Text.Trim();

            string billno = ((Label)this.grvIssued.Rows[rowindex].FindControl("lbgvbillno")).Text.Trim();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEBILLAPPROVAL", slno, billno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.BillRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('delete  Fail');", true);

            }



        }



        protected void btnDelForward_Click(object sender, EventArgs e)
        {
            string url = "AccOnlinePaymentRa?Type=ChequeApproval";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;

            string slno = ((Label)this.grvApproved.Rows[rowindex].FindControl("lblpayidapp")).Text.Trim();


            //string actcode = ((Label)this.grvIssued.Rows[rowindex].FindControl("lblgvactcode")).Text.Trim();

            string billno = ((Label)this.grvApproved.Rows[rowindex].FindControl("lbgvbillno")).Text.Trim();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEBILLFORWARD", slno, billno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.BillRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('delete  Fail');", true);

            }


        }


        protected void btnDelChecked_Click(object sender, EventArgs e)
        {
            string url = "AccOnlinePaymentRa?Type=ChequeApproval";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;

            string slno = ((Label)this.gvforward.Rows[rowindex].FindControl("lblgvreqno1fr")).Text.Trim();


            //string actcode = ((Label)this.grvIssued.Rows[rowindex].FindControl("lblgvactcode")).Text.Trim();

            string billno = ((Label)this.gvforward.Rows[rowindex].FindControl("lbgvbillnofr")).Text.Trim();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEBILLCHECKED", slno, billno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.BillRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('delete  Fail');", true);

            }


        }




        protected void btnDelProposal_Click(object sender, EventArgs e)
        {
            string url = "AccOnlinePaymentRa?Type=ChequeReady";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int rowindex = gvr.RowIndex;

            string slno = ((Label)this.grvRecm.Rows[rowindex].FindControl("lblgvreqno1payma")).Text.Trim();


            //string actcode = ((Label)this.grvIssued.Rows[rowindex].FindControl("lblgvactcode")).Text.Trim();

            string billno = ((Label)this.grvRecm.Rows[rowindex].FindControl("lbgvbillno")).Text.Trim();

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_ONLINE_PAYMENT", "DELETEPAYPROPOSAL", slno, billno, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                this.BillRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);


            }

            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('delete  Fail');", true);

            }






        }



        protected void lnkbtnChekedId_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string appslnum = "";
            foreach (GridViewRow gv1 in grvRecm.Rows)
            {
                string chkemerge = ((CheckBox)gv1.FindControl("chkCheckid")).Checked ? "True" : "False";
                string slnum = ((Label)gv1.FindControl("lblgvreqno1payma")).Text.Trim();
                if (chkemerge == "True")
                {
                    appslnum += slnum;
                }
            }
            //hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa?Type=ChequeReady" + "&payid=" + payid; 
            string url = "AccOnlinePaymentRa?Type=ChequeReady&comcod=" + comcod + "&payid=" + appslnum;

            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunCheckedBill('" + url + "');", true);

        }
        protected void lnkbtnForword_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string appslnum = "";
            foreach (GridViewRow gv1 in gvforward.Rows)
            {
                string chkemerge = ((CheckBox)gv1.FindControl("chkforwordid")).Checked ? "True" : "False";
                string slnum = ((Label)gv1.FindControl("lblgvreqno1fr")).Text.Trim();
                if (chkemerge == "True")
                {
                    appslnum += slnum;
                }
            }
            //   hlink2.NavigateUrl = "~/F_15_DPayReg/AccOnlinePaymentRa?Type=ChequeApproval" + "&payid=" + payid; 
            //string url = "AccOnlinePaymentRa?Type= ChequeApproval&comcod=" + comcod + "&payid=" + appslnum;
            string url = "AccOnlinePaymentRa?Type=ChequeApproval" + "&payid=" + appslnum;

            ScriptManager.RegisterStartupScript(this, GetType(), "target", "FunForwordBill('" + url + "');", true);

        }


        protected void chkAllforwordid_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["ForwardBill"];
            int i, index;
            if (((CheckBox)this.gvforward.HeaderRow.FindControl("chkAllforwordid")).Checked)
            {

                for (i = 0; i < this.gvforward.Rows.Count; i++)
                {
                    ((CheckBox)this.gvforward.Rows[i].FindControl("chkforwordid")).Checked = true;
                    index = (this.gvforward.PageSize) * (this.gvforward.PageIndex) + i;
                    dt.Rows[index]["chkmerge"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvforward.Rows.Count; i++)
                {
                    ((CheckBox)this.gvforward.Rows[i].FindControl("chkforwordid")).Checked = false;
                    index = (this.gvforward.PageSize) * (this.gvforward.PageIndex) + i;
                    dt.Rows[index]["chkmerge"] = "False";


                }

            }

            Session["ForwardBill"] = dt;
        }
        protected void chkAllCheckid_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["grvRecmBill"];
            int i, index;
            if (((CheckBox)this.grvRecm.HeaderRow.FindControl("chkAllCheckid")).Checked)
            {

                for (i = 0; i < this.grvRecm.Rows.Count; i++)
                {
                    ((CheckBox)this.grvRecm.Rows[i].FindControl("chkCheckid")).Checked = true;
                    index = (this.grvRecm.PageSize) * (this.grvRecm.PageIndex) + i;
                    dt.Rows[index]["chkmerge"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.grvRecm.Rows.Count; i++)
                {
                    ((CheckBox)this.grvRecm.Rows[i].FindControl("chkCheckid")).Checked = false;
                    index = (this.grvRecm.PageSize) * (this.grvRecm.PageIndex) + i;
                    dt.Rows[index]["chkmerge"] = "False";


                }

            }

            Session["grvRecmBill"] = dt;
        }


        protected void chkAllpayid_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["VAproved"];
            int i, index;
            if (((CheckBox)this.grvApproved.HeaderRow.FindControl("chkAllpayid")).Checked)
            {

                for (i = 0; i < this.grvApproved.Rows.Count; i++)
                {
                    ((CheckBox)this.grvApproved.Rows[i].FindControl("chkpayid")).Checked = true;
                    index = (this.grvApproved.PageSize) * (this.grvApproved.PageIndex) + i;
                    dt.Rows[index]["chkmerge"] = "True";
                }

            }

            else
            {
                for (i = 0; i < this.grvApproved.Rows.Count; i++)
                {
                    ((CheckBox)this.grvApproved.Rows[i].FindControl("chkpayid")).Checked = false;
                    index = (this.grvApproved.PageSize) * (this.grvApproved.PageIndex) + i;
                    dt.Rows[index]["chkmerge"] = "False";
                }

            }

            Session["VAproved"] = dt;
        }
    }
}