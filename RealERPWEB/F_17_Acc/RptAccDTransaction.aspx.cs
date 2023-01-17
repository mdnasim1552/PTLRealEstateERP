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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccDTransaction : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;
        public static int indexofamp;

        //double OpenBal = 0, Clsbal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
                int index1 = (url.Contains("&")) ? url.IndexOf('&') : url.Length;
                int index2 = (url.Contains("&")) ? url.Substring(index1 + 1).IndexOf('&') : 0;

                int indexofamp = index1 + (index2 > 0 ? index2 + 1 : index2);

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.RbtnVisibility();
                this.GetAccCode();
                string comcod = GetCompCode();
                string date1 = this.Request.QueryString["Date1"];
                string date2 = this.Request.QueryString["Date2"];
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = date1.Length > 0 ? date1 : "01" + date.Substring(2);
                this.txttodate.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.txtfrmdat.Text = date1.Length > 0 ? date1 : "01" + date.Substring(2);
                this.txttodat.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.chkwitransfer.Checked = true;
                if (comcod == "1103")
                {
                    this.chkwitransfer.Checked = false;
                }
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void RbtnVisibility()
        {
            string TrMode = this.Request.QueryString["TrMod"].ToString();
            switch (TrMode)
            {
                case "DTran":
                    this.rbtnList1.SelectedIndex = 0;
                    //rbtnList1.Items.Remove("Daily Transaction");
                    //rbtnList1.Items.Remove("Deleted Transaction");
                    //rbtnList1.Items.Remove("Fund Flow");
                    //rbtnList1.Items.Remove("Receipts & Payment");
                    //rbtnList1.Items.Remove("Issued Vs. Collection");
                    //rbtnList1.Items.Remove("Project Transaction");
                    //rbtnList1.Items.Remove("Day Book");
                    //rbtnList1.Items.Remove("Receipt Payment");
                    this.rbtnList1.Visible = false;

                    break;

                case "DelTran":
                    this.rbtnList1.SelectedIndex = 3;
                    this.rbtnList1.Visible = false;
                    break;
                case "RecPay":
                    this.rbtnList1.SelectedIndex = 4;
                    this.rbtnList1.Visible = false;
                    break;

                case "Fflow":
                    this.rbtnList1.SelectedIndex = 5;
                    this.rbtnList1.Visible = false;
                    break;

                case "IssuedVsCollect":
                    this.rbtnList1.SelectedIndex = 6;
                    this.rbtnList1.Visible = false;
                    break;

                case "ProTrans":
                    this.rbtnList1.SelectedIndex = 7;
                    this.rbtnList1.Visible = false;
                    break;

                case "RecPayprj":
                    this.rbtnList1.SelectedIndex = 9;
                    //this.lblGroupRP.Visible = true;
                    //this.rbtnGroupRP.Visible = true;            
                    //this.lblproj.Visible = true;
                    //this.btnproj.Visible = true;
                    //this.ddlproj.Visible = true;
                    this.rbtnList1.Visible = false;
                    this.mainfiledset.Visible = false;
                    this.LoadProj();

                    //this.GetAccCode();
                    break;
                case "RecPay02":
                    this.rbtnList1.SelectedIndex = 10;
                    this.rbtnList1.Visible = false;
                    break;

                case "RecPayprj02":
                    this.rbtnList1.SelectedIndex = 11;
                    this.rbtnList1.Visible = false;
                    this.mainfiledset.Visible = false;
                    this.LoadProj02();
                    break;

                case "RecPay03"://Receipt & Payment (Customized) Rupayan
                    this.rbtnList1.SelectedIndex = 12;
                    this.rbtnList1.Visible = false;
                    break;
            }

            this.rbtnList1_SelectedIndexChanged(null, null);

        }


        private void GetAccCode()
        {

            string TrMode = this.Request.QueryString["TrMod"].ToString();
            string comcod = this.GetCompCode();
            string filter = "%" + this.txtAccSearch.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "GETACCHEAD", filter, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataTextField = "actdesc1";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataBind();
            if (TrMode == "ProTrans" && Request.QueryString["prjcode"].Length > 0)
            {
                ddlAccHead.SelectedValue = Request.QueryString["prjcode"].ToString();
                ddlAccHead.Enabled = false;
            }
            ds1.Dispose();
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.ShowCashBook();

                    break;
                case 1:
                    this.TransactionList();

                    break;

                case 2:
                    this.ShowDailyProposal();
                    break;
                case 3:

                    this.ShowDeletedtransaction();

                    break;
                case 4:
                case 5:


                    this.ReceiptAndPayment();
                    break;

                case 6:
                    this.ShowIssuedVsColl();
                    break;

                case 7:
                    this.ShowpProTransaction();
                    break;

                case 9:
                    this.ReceiptAndPaymentproj();
                    break;

                case 10:
                    this.ReceiptAndPaymentDet();
                    break;

                case 12:
                    this.ReceiptAndPaymentDetCustomized();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblprintstk")).Text;
                string eventdesc = "Show Report";
                string eventdesc2 = rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }




        private void TransactionList()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            this.Paneltovoucherno.Visible = true;
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";


            string searchinfo = "";

            if (this.ddlSrch.SelectedValue != "")
            {

                if (this.ddlSrch.SelectedValue == "between")
                {
                    searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();
                }
                else
                {
                    searchinfo = "dram " + this.ddlSrch.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString();
                }
            }

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSACTIONS", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            Session["tranlist"] = dtr1;
            DataTable tblt03 = (DataTable)Session["tranlist"];
            // this.gvtranlsit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvtranlsit.DataSource = dtr1;
            this.gvtranlsit.DataBind();
            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvtranlsit");

            this.lbltoCashVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[0]["tonum"]).ToString("#,##0; (#,##0); ");
            this.lbltoBankVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[1]["tonum"]).ToString("#,##0; (#,##0); ");
            this.lbltoContraVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[2]["tonum"]).ToString("#,##0; (#,##0); ");
            this.lbltoJournalVoucher.Text = Convert.ToDouble(ds1.Tables[2].Rows[3]["tonum"]).ToString("#,##0; (#,##0); ");
            Session["Report1"] = gvtranlsit;
            ((HyperLink)this.gvtranlsit.HeaderRow.FindControl("hlbtnbtbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        private void ShowCashBook()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();
            string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType);
            string searchinfo = "";

            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(srcham between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( srcham " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }

            string withoutrans = this.chkwitransfer.Checked ? "withouttrans" : "";

            //string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTCASHBOOK", fromdate, todate, txtSVoucher, searchinfo, withoutrans, "", "", "", "");
            if (ds1 == null)
                return;

            //For Grouping
            DataTable dtr = new DataTable();
            string RptGroup = this.rbtnGroup.SelectedItem.Text.Trim();
            DataView dv1 = new DataView();
            switch (RptGroup)
            {
                case "Receipt":
                    dv1 = ds1.Tables[0].DefaultView;
                    dv1.RowFilter = ("grp1 = 'A'");
                    dtr = dv1.ToTable();
                    this.lblReceiptCash.Visible = true;
                    this.lblPaymentCash.Visible = false;
                    this.lblDetailsCash.Visible = false;

                    break;
                case "Payment":
                    dv1 = ds1.Tables[0].DefaultView;
                    dv1.RowFilter = ("grp1 = 'B'");
                    dtr = dv1.ToTable();
                    this.lblReceiptCash.Visible = false;
                    this.lblPaymentCash.Visible = true;
                    this.lblDetailsCash.Visible = false;
                    break;

                case "Both":
                    dv1 = ds1.Tables[0].DefaultView;
                    dv1.RowFilter = (this.ddlVoucharCash.SelectedValue == "ALL Voucher") ? ("grp1 = 'A' or grp1 = 'B'  or grp1 = 'C'  ") : ("grp1 = 'A' or grp1 = 'B' ");
                    dtr = dv1.ToTable();
                    this.lblReceiptCash.Visible = true;
                    this.lblPaymentCash.Visible = true;
                    this.lblDetailsCash.Visible = (this.ddlVoucharCash.SelectedValue == "ALL Voucher");
                    break;
            }

            /////////

            Session["cashbank"] = dtr;




            DataView dvr = new DataView();
            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (RptGroup == "Receipt")
            {
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
            }
            this.FooterCalculation(dtr1, "gvcashbook");
            Session["Report1"] = gvcashbook;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();

            if (RptGroup == "Payment")
            {
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
            }


            this.FooterCalculation(dtr2, "gvcashbookp");
            Session["Report1"] = gvcashbookp;
            if (dtr2.Rows.Count > 0)
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='C'");
            DataTable dtr3 = dvr.ToTable();
            this.gvcashbookDB.DataSource = dvr.ToTable(); ;
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDB");
        }

        private string ComReePayCallType()
        {

            string comcod = this.GetCompCode();
            string Calltype = "";
            switch (comcod)
            {
                case "3333":
                    Calltype = "RPCOMPANY02_04";
                    break;
                default:

                    Calltype = (this.rbtnGroupRP.SelectedIndex == 0 || this.rbtnGroupRP.SelectedIndex == 1) ? "RPCOMPANYCORB_04" : "RP_COMPANY_04";
                   
                    break;

            }
            return Calltype;
        }







        private void ReceiptAndPaymentproj()
        {
            this.banksts.Visible = true;
            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfrmdat.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodat.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string rp = (this.Request.QueryString["TrMod"] == "RecPayprj") ? "RP" : "";
            string CBorBoth = (this.rbtncashbank.SelectedIndex == 0) ? "C" : (this.rbtncashbank.SelectedIndex == 1) ? "B" : "";
            string net = this.cknet.Checked ? "Net" : "";
            string projcode = this.ddlproject.SelectedValue.ToString();
            //string Calltype=this.ComReePayCallType();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "RPCOMPANY04_04", fromdate, todate, rp, CBorBoth, net, projcode, "", "", "");
            if (ds1 == null)
                return;

            Session["recandpay"] = this.HiddenSameDate(ds1.Tables[0]);
            Session["recandpayFo"] = ds1.Tables[1];
            ViewState["recandpayNote"] = ds1.Tables[2];

            this.gvRecptPayment.DataSource = ds1.Tables[0];
            this.gvRecptPayment.DataBind();
            this.RPNote1();

            for (int i = 0; i < gvRecptPayment.Rows.Count; i++)
            {
                string recpcode = ((Label)gvRecptPayment.Rows[i].FindControl("lblgvrecpcodep")).Text.Trim();
                string paycode = ((Label)gvRecptPayment.Rows[i].FindControl("lblgvpaycodep")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvRecptPayment.Rows[i].FindControl("btnRecDescp");
                LinkButton lbtn2 = (LinkButton)gvRecptPayment.Rows[i].FindControl("btnPayDescp");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.gvRecptPayment.HeaderRow.FindControl("hlbtnRcvPayCdataExelp")).Enabled = (dr1.Length == 0) ? false : (Convert.ToBoolean(dr1[0]["printable"]));

            this.FooterCalculation(ds1.Tables[0], "gvRecptPayment");
            ds1.Dispose();
            Session["Report1"] = gvRecptPayment;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.gvRecptPayment.HeaderRow.FindControl("hlbtnRcvPayCdataExelp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

            }
        }
        private void ReceiptAndPayment()
        {
            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string rp = (this.Request.QueryString["TrMod"] == "RecPay") ? "RP" : "";
            string CBorBoth = (this.rbtnGroupRP.SelectedIndex == 0) ? "C" : (this.rbtnGroupRP.SelectedIndex == 1) ? "B" : "";
            string net = this.chknet.Checked ? "Net" : "";
            string Calltype = this.ComReePayCallType();

          

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", Calltype, fromdate, todate, rp, CBorBoth, net, "", "", "", "");
            if (ds1 == null)
                return;

            Session["recandpay"] = this.HiddenSameDate(ds1.Tables[0]);
            Session["recandpayFo"] = ds1.Tables[1];
            ViewState["recandpayNote"] = ds1.Tables[2];

            this.gvrecandpay.DataSource = ds1.Tables[0];
            this.gvrecandpay.DataBind();
            this.RPNote();

            for (int i = 0; i < gvrecandpay.Rows.Count; i++)
            {
                string recpcode = ((Label)gvrecandpay.Rows[i].FindControl("lblgvrecpcode")).Text.Trim();
                string paycode = ((Label)gvrecandpay.Rows[i].FindControl("lblgvpaycode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvrecandpay.Rows[i].FindControl("btnRecDesc");
                LinkButton lbtn2 = (LinkButton)gvrecandpay.Rows[i].FindControl("btnPayDesc");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            if (ds1.Tables[0].Rows.Count > 0)
            {

                if (comcod == "3354")// only edison realerp
                {
                    ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).Enabled = true;
                }
                else
                {
                    ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                }
            }


            this.FooterCalculation(ds1.Tables[0], "gvrecandpay");
            ds1.Dispose();
            Session["Report1"] = gvrecandpay;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

            }
        }
        private void RPNote()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3356":
                case "3357":
                    this.PanelNote.Visible = false;
                    break;
                default:
                    this.PanelNote.Visible = true;
                    break;
            }
            DataTable dt = (DataTable)ViewState["recandpayNote"];
            this.gvbankbal.DataSource = dt;
            this.gvbankbal.DataBind();

            //this.lblPaid.Text = Convert.ToDouble(dt.Rows[0]["payamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblInPaid.Text = Convert.ToDouble(dt.Rows[0]["ipayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblSodPaid.Text = Convert.ToDouble(dt.Rows[0]["sodpayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblTPaid.Text = Convert.ToDouble(dt.Rows[0]["tpayamt"]).ToString("#,##0;(#,##0) ;");

            //this.lblNet.Text = Convert.ToDouble(dt.Rows[0]["netamt"]).ToString("#,##0;(#,##0) ;");

        }

        private void RPNote1()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3356":
                case "3357":
                    this.PanelNote.Visible = false;
                    break;
                default:
                    this.PanelNote.Visible = true;
                    break;
            }
            DataTable dt = (DataTable)ViewState["recandpayNote"];
            this.gvbankbal1.DataSource = dt;
            this.gvbankbal1.DataBind();

            //this.lblPaid.Text = Convert.ToDouble(dt.Rows[0]["payamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblInPaid.Text = Convert.ToDouble(dt.Rows[0]["ipayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblSodPaid.Text = Convert.ToDouble(dt.Rows[0]["sodpayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblTPaid.Text = Convert.ToDouble(dt.Rows[0]["tpayamt"]).ToString("#,##0;(#,##0) ;");

            //this.lblNet.Text = Convert.ToDouble(dt.Rows[0]["netamt"]).ToString("#,##0;(#,##0) ;");

        }
        private void ShowIssuedVsColl()
        {

            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "ISSUEDVSCOLLECTION", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["recandpay"] = ds1.Tables[0]; ;
            this.gvarecandpay.DataSource = ds1.Tables[0];
            this.gvarecandpay.DataBind();
            this.FooterCalculation(ds1.Tables[0], "gvarecandpay");
            ds1.Dispose();
            Session["Report1"] = this.gvarecandpay;
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.gvarecandpay.HeaderRow.FindControl("hlbtnacRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }

        private void ShowDailyProposal()
        {

            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSPROPOSAL", fromdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            this.gvDailPayPro.DataSource = dtr1;
            this.gvDailPayPro.DataBind();
            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvDailPayPro");


        }

        private void ShowDeletedtransaction()
        {
            Session.Remove("tranlist");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtSVoucher = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTDELTRANSACTIONS", fromdate, todate, txtSVoucher, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            DataTable dtr = ds1.Tables[0];
            DataTable dtr1 = HiddenSameDate(dtr);
            this.gvdtranlsit.DataSource = dtr1;
            this.gvdtranlsit.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvdtranlsit.HeaderRow.FindControl("hlbtnbtbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["tranlist"] = dtr1;
            this.FooterCalculation(dtr1, "gvdtranlsit");



        }


        private void ShowpProTransaction()
        {
            Session.Remove("tranlist");

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlAccHead.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "RPTPROTRANSACTION", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dtr = this.HiddenSameDate(ds1.Tables[0]);
            Session["tranlist"] = dtr;
            this.gvPtotranlsit.DataSource = dtr;
            this.gvPtotranlsit.DataBind();
            this.FooterCalculation(dtr, "gvPtotranlsit");
            Session["Report1"] = gvPtotranlsit;
            if (dtr.Rows.Count > 0)
                ((HyperLink)this.gvPtotranlsit.HeaderRow.FindControl("hlbtnbtbCdataExelp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }

        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            double frecamt = 0, fpayamt1 = 0, netbal;
            DataView dv1; DataTable dt1;
            double cashamt = 0.00, bankamt = 0.00, netamt = 0.00;
            switch (GvName)
            {
                case "gvcashbook":

                    cashamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ? 0 : dt.Compute("sum(casham)", "")));
                    bankamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                         0 : dt.Compute("sum(bankam)", "")));
                    netamt = cashamt + bankamt;
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvCashAmt")).Text = cashamt.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvFBankAmt")).Text = bankamt.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvnetTotal")).Text = netamt.ToString("#,##0;(#,##0) ;");




                    break;


                case "gvcashbookp":

                    cashamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ? 0 : dt.Compute("sum(casham)", "")));
                    bankamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                         0 : dt.Compute("sum(bankam)", "")));
                    netamt = cashamt + bankamt;


                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvCashAmt1")).Text = cashamt.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvFBankAmt1")).Text = bankamt.ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvnetCashaBankAmt")).Text = netamt.ToString("#,##0;(#,##0) ;");



                    break;

                case "gvcashbookDB":
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                             0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFClAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                           0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;

                case "gvtranlsit":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");
                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("lgvFDram")).Text);
                    Dtcram = Convert.ToDouble("0" + ((Label)this.gvtranlsit.FooterRow.FindControl("txtgvFCram")).Text);
                    break;


                case "gvdtranlsit":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvdtranlsit.FooterRow.FindControl("lgvdFDram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvdtranlsit.FooterRow.FindControl("txtgvdFCram")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");
                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvdtranlsit.FooterRow.FindControl("lgvdFDram")).Text);
                    Dtcram = Convert.ToDouble("0" + ((Label)this.gvdtranlsit.FooterRow.FindControl("txtgvdFCram")).Text);
                    break;



                case "gvrecandpay":

                    dv1 = dt.Copy().DefaultView;
                    dv1.RowFilter = ("recpcode like '%BBBBAAAAAAAA%' or paycode like '%BBBBAAAAAAAA%'");
                    dt1 = dv1.ToTable();
                    //dt1 = (DataTable)Session["recandpayFo"];

                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    //((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = netbal.ToString("#,##0;(#,##0) ;");

                    ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = netbal.ToString("#,##0;(#,##0) ;");







                    //dv1=dt.Copy().DefaultView;
                    //dv1.RowFilter = ("recpcode like '%00000000%' or paycode like '%00000000%'");
                    //dt1 = dv1.ToTable();
                    //dt1 = (DataTable)Session["recandpayFo"];

                    //frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                    //       0 : dt1.Compute("sum(recpam)", "")));
                    //((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    //fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                    //     0 : dt1.Compute("sum(payam)", "")));

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    //netbal = frecamt - fpayamt1;

                    //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;

                case "gvRecptPayment":

                    dv1 = dt.Copy().DefaultView;
                    dv1.RowFilter = ("recpcode like '%BBBBAAAAAAAA%' or paycode like '%BBBBAAAAAAAA%'");
                    dt1 = dv1.ToTable();
                    //dt1 = (DataTable)Session["recandpayFo"];

                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    //((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = netbal.ToString("#,##0;(#,##0) ;");

                    ((HyperLink)this.gvRecptPayment.FooterRow.FindControl("lgvFNetBalancep")).Text = netbal.ToString("#,##0;(#,##0) ;");







                    //dv1=dt.Copy().DefaultView;
                    //dv1.RowFilter = ("recpcode like '%00000000%' or paycode like '%00000000%'");
                    //dt1 = dv1.ToTable();
                    //dt1 = (DataTable)Session["recandpayFo"];

                    //frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                    //       0 : dt1.Compute("sum(recpam)", "")));
                    //((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    //fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                    //     0 : dt1.Compute("sum(payam)", "")));

                    //((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    //netbal = frecamt - fpayamt1;

                    //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;





                case "gvDailPayPro":
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acrescode not in('" + "  Total Amt:" + "')");
                    dt = dv.ToTable();
                    ((Label)this.gvDailPayPro.FooterRow.FindControl("lgvFDramPPro")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                             0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");


                    Dtdram = Convert.ToDouble("0" + ((Label)this.gvDailPayPro.FooterRow.FindControl("lgvFDramPPro")).Text);
                    break;



                case "gvarecandpay":
                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(recpam)", "")) ?
                           0 : dt.Compute("sum(recpam)", "")));
                    ((Label)this.gvarecandpay.FooterRow.FindControl("lblgvFrecpamac")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                         0 : dt.Compute("sum(payam)", "")));

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFpayamac")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFNetBalanceac")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;


                case "gvPtotranlsit":

                    ((Label)this.gvPtotranlsit.FooterRow.FindControl("lgvFDramp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                           0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvPtotranlsit.FooterRow.FindControl("lgvFCramp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0) ;");

                    break;




            }


        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;

            string grp1 = dt1.Rows[0]["grp1"].ToString();
            switch (this.rbtnList1.SelectedIndex)
            {

                case 0:
                    Date1 = dt1.Rows[0]["voudat1"].ToString();
                    vounum = dt1.Rows[0]["vounum1"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                        {
                            vounum = dt1.Rows[j]["vounum1"].ToString();
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                            dt1.Rows[j]["vounar"] = "";


                        }

                        else
                        {
                            vounum = dt1.Rows[j]["vounum1"].ToString();
                        }

                    }
                    break;

                case 7:              

                        Date1 = dt1.Rows[0]["voudat"].ToString();
                        vounum = dt1.Rows[0]["vounum"].ToString();
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["voudat"].ToString() == Date1 && dt1.Rows[j]["vounum"].ToString() == vounum)
                            {
                                dt1.Rows[j]["vounum1"] = "";
                                dt1.Rows[j]["voudat1"] = "";
                            }

                            else
                            {
                                if (dt1.Rows[j]["vounum"].ToString() == vounum)
                                    dt1.Rows[j]["vounum1"] = "";

                                if (dt1.Rows[j]["voudat"].ToString() == Date1)
                                    dt1.Rows[j]["voudat1"] = "";
                            }

                            Date1 = dt1.Rows[j]["voudat"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();

                        }
                    break;


                case 4:
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grprpdesc"] = "";
                                dt1.Rows[j]["grppaydesc"] = "";
                            }


                            grp1 = dt1.Rows[j]["grp1"].ToString();


                        }
                    break;

                case 8:                        
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grprpdesc"] = "";
                                dt1.Rows[j]["grppaydesc"] = "";
                            }


                            grp1 = dt1.Rows[j]["grp1"].ToString();


                        }
                    break;

                case 9:
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grprpdesc"] = "";
                                dt1.Rows[j]["grppaydesc"] = "";
                            }


                            grp1 = dt1.Rows[j]["grp1"].ToString();


                        }
                    break;

                case 10:
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grprpdesc"] = "";
                                dt1.Rows[j]["grppaydesc"] = "";
                            }
                            grp1 = dt1.Rows[j]["grp1"].ToString();
                        }
                    break;

                case 11:
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grprpdesc"] = "";
                                dt1.Rows[j]["grppaydesc"] = "";
                            }
                            grp1 = dt1.Rows[j]["grp1"].ToString();
                        }
                    break;

                case 12:
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grprpdesc"] = "";
                                dt1.Rows[j]["grppaydesc"] = "";
                            }
                            grp1 = dt1.Rows[j]["grp1"].ToString();
                        }
                    break;

                default:
                    Date1 = dt1.Rows[0]["voudat1"].ToString();
                        vounum = dt1.Rows[0]["vounum1"].ToString();
                        for (j = 1; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                            {
                                vounum = dt1.Rows[j]["vounum1"].ToString();
                                dt1.Rows[j]["vounum1"] = "";
                                dt1.Rows[j]["voudat1"] = "";


                            }

                            else
                            {
                                vounum = dt1.Rows[j]["vounum1"].ToString();
                            }

                        }

                    break;
                   
            }
            return dt1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();

            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.PrintCashBook();

                    break;
                case 1:
                    this.PrintTransaction();
                    break;
                case 2:
                    this.PrintDailyProposal();
                    break;
                case 3:
                    this.PrintDelTransaction();
                    break;
                case 4:
                case 5:
                    this.PrintReceiveAndPayment();
                    break;
                case 6:
                    this.PrintIssuedVsCollection();
                    break;
                case 7:
                    this.PrintProTransaction();
                    break;
                case 9:
                    this.PrintReceiveAndPaymentProj();
                    break;
                case 10:
                    switch (comcod)
                    {

                        case "3348":
                            this.PrintReceiveAndPayment01Credence();
                            break;

                        //case "3101":
                        case "3358":
                        case "3359":
                        case "3360":
                        case "3361":
                            this.PrintReceiveAndPaymentEnt();
                            break;

                        case "3101":
                        case "3357":
                            this.PrintReceiveAndPaymentCube();
                            break;

                        default:
                            this.PrintReceiveAndPayment01();
                            break;
                    }
                    break;
                case 11:
                    this.ProjectwiseReceiptandPaymentDetails();
                    break;
                case 12://Rupayan
                    this.PrintRecAndPayCustomized();
                    break;


                    //this.PrintReceiveAndPayment01 ();
                    //break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblprintstk")).ToString();
                string eventdesc = "Print Report";
                string eventdesc2 = rbtnList1.SelectedItem.ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void LoadProj()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PROJECTDESC", "", "", "", "", "", "", "", "", "");
            this.ddlproject.DataTextField = "actdesc";
            this.ddlproject.DataValueField = "actcode";
            this.ddlproject.DataSource = ds1.Tables[0];
            this.ddlproject.DataBind();
        }

        private void LoadProj02()
        {

            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtfrmdat2.Text = date1.Length > 0 ? date1 : "01" + date.Substring(2);
            this.txttodat2.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PROJECTDESC", "", "", "", "", "", "", "", "", "");
            this.ddlproject2.DataTextField = "actdesc";
            this.ddlproject2.DataValueField = "actcode";
            this.ddlproject2.DataSource = ds1.Tables[0];
            this.ddlproject2.DataBind();
        }



        private void PrintCashBook()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = ASTUtility.Concat(compname, username, session, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;


            DataTable dt = HiddenSameDate((DataTable)Session["cashbank"]);
            if (dt == null)
                return;
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.AccCashBankBook1>();
            LocalReport Rpt1 = new LocalReport();
            string Tittle = "";

            switch (comcod)
            {

                case "3348":// Credence
                    Tittle = "CASH BOOK";
                    //rptsl = new RealERPRPT.R_17_Acc.RPTSpecialLedgerRup();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccCashbook1Credence", lst, null, null);
                    break;

                default:

                    string booktittle = this.ddlVoucharCash.SelectedValue.ToString();
                    switch (booktittle)
                    {
                        case "C":
                            Tittle = "CASH BOOK";
                            break;

                        case "B":
                            Tittle = "BANK BOOK";
                            break;

                        default:
                            Tittle = "CASH & BANK BOOK";
                            break;



                    }

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccCashbook1", lst, null, null);


                    break;


            }
            // Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtrptTitle", Tittle));
           // Rpt1.SetParameters(new ReportParameter("txtdate", "CASH BOOK"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtdate", "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void PrintTransaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tranlist"];

            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyTransaction", list, null, null);
            Rpt2.SetParameters(new ReportParameter("compName", comnam));
            Rpt2.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt2.SetParameters(new ReportParameter("txtDrAmt", Dtdram.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("txtCrAmt", Dtcram.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("rptTitle", "TRANSACTION LIST"));
            Rpt2.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintReceiveAndPayment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();

            DataTable dt = (DataTable)Session["recandpay"];
            DataTable dt1 = (DataTable)Session["recandpayFo"];
            DataTable dt2 = (DataTable)ViewState["recandpayNote"];


            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RescPayment0>();
            var lst1 = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RescPayment01>();
            var lst2 = dt2.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RescPayment02>();

            LocalReport Rpt2 = new LocalReport();
            switch (comcod)
            {
                case "3333":
                    Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptRecAndPaymentAlli", lst, lst1, lst2);
                    break;

                case "3101":
                case "3357": //cube
                case "3356": // intech
                    Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptBankBalance02Cube", lst, lst1, lst2);
                    break;

                default:
                    Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptBankBalance02", lst, lst1, lst2);
                    break;
            }

            Rpt2.EnableExternalImages = true;
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("Ftdate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt2.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt2.SetParameters(new ReportParameter("NetInDercInCash", ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text.Trim()));
            Rpt2.SetParameters(new ReportParameter("RptTitle", (this.Request.QueryString["TrMod"] == "RecPay") ? "RECEIPTS & PAYMENTS" : "FUND FLOW"));
            Rpt2.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void PrintReceiveAndPaymentProj()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string proj = this.ddlproject.SelectedItem.Text.ToString().Substring(4);
            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();

            string frmdate1 = Convert.ToDateTime(this.txtfrmdat.Text).ToString("dd-MMM-yyyy");
            string todate1 = Convert.ToDateTime(this.txttodat.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["recandpay"];
            DataTable dt1 = (DataTable)Session["recandpayFo"];
            DataTable dt2 = (DataTable)ViewState["recandpayNote"];


            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RescPayment0>();
            var lst1 = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RescPayment01>();
            var lst2 = dt2.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RescPayment02>();

            LocalReport Rpt2 = new LocalReport();

            Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptRecAndPaymentProj", lst, lst1, lst2);
            Rpt2.EnableExternalImages = true;
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("Ftdate", "(From " + frmdate1 + " To " + todate1 + ")"));
            Rpt2.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt2.SetParameters(new ReportParameter("NetInDercInCash", ((HyperLink)this.gvRecptPayment.FooterRow.FindControl("lgvFNetBalancep")).Text.Trim()));
            Rpt2.SetParameters(new ReportParameter("proj", proj));
            Rpt2.SetParameters(new ReportParameter("RptTitle", (this.Request.QueryString["TrMod"] == "RecPayprj") ? "RECEIPTS & PAYMENTS" : "FUND FLOW"));
            Rpt2.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintReceiveAndPayment01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();

            string Ftdate = "(From " + this.txtfromdate.Text + " To " + this.txttodate.Text + ")";

            DataTable dt = (DataTable)Session["recandpay"];
            DataTable dt1 = (DataTable)Session["recandpayFo"];
            DataTable dt2 = (DataTable)ViewState["recandpayNote"];


            double TotoRes = Convert.ToDouble(dt1.Rows[0]["recpam"]);
            double TotoPay = Convert.ToDouble(dt1.Rows[0]["payam"]);
            double NetAmt = TotoRes - TotoPay;
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptRecAndPayment", lst, null, null);
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("Ftdate", Ftdate));
            Rpt2.SetParameters(new ReportParameter("TotoRes", TotoRes.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("TotoPay", TotoPay.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("NetAmt", NetAmt.ToString("#,##0;(#,##0); ")));

            //  Rpt2.SetParameters(new ReportParameter("VouType", "Voucher Type: " + txtVouType));

            Rpt2.SetParameters(new ReportParameter("RptTitle", "RECEIPTS & PAYMENT"));
            Rpt2.SetParameters(new ReportParameter("txtuserinfo", "Print Source :" + username + " , " + session + " , " + printdate));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintRecAndPayCustomized()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Ftdate = "(From " + this.txtfromdate.Text + " To " + this.txttodate.Text + ")";

            DataTable dt = (DataTable)Session["recandpay"];

            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptRecAndPayCustomized", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtDate", Ftdate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Receipts & Payment Report"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Print Source :" + username + " , " + session + " , " + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintReceiveAndPaymentCube()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();

            string Ftdate = "(From " + this.txtfromdate.Text + " To " + this.txttodate.Text + ")";

            DataTable dt = (DataTable)Session["recandpay"];
            DataTable dt1 = (DataTable)Session["recandpayFo"];
            DataTable dt2 = (DataTable)ViewState["recandpayNote"];


            double TotoRes = Convert.ToDouble(dt1.Rows[0]["recpam"]);
            double TotoPay = Convert.ToDouble(dt1.Rows[0]["payam"]);
            double NetAmt = TotoRes - TotoPay;
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptRecAndPaymentCube", lst, null, null);
            Rpt2.EnableExternalImages = true;
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("Ftdate", Ftdate));
            Rpt2.SetParameters(new ReportParameter("TotoRes", TotoRes.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("TotoPay", TotoPay.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("NetAmt", NetAmt.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //  Rpt2.SetParameters(new ReportParameter("VouType", "Voucher Type: " + txtVouType));

            Rpt2.SetParameters(new ReportParameter("RptTitle", "RECEIPTS & PAYMENT"));
            Rpt2.SetParameters(new ReportParameter("txtuserinfo", "Print Source :" + username + " , " + session + " , " + printdate));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintReceiveAndPayment01Credence()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();

            string Ftdate = "(From " + this.txtfromdate.Text + " To " + this.txttodate.Text + ")";

            DataTable dt = (DataTable)Session["recandpay"];
            DataTable dt1 = (DataTable)Session["recandpayFo"];
            DataTable dt2 = (DataTable)ViewState["recandpayNote"];


            double TotoRes = Convert.ToDouble(dt1.Rows[0]["recpam"]);
            double TotoPay = Convert.ToDouble(dt1.Rows[0]["payam"]);
            double NetAmt = TotoRes - TotoPay;
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptRecAndPaymentCredence", lst, null, null);
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("Ftdate", Ftdate));

            Rpt2.SetParameters(new ReportParameter("TotoRes", TotoRes.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("TotoPay", TotoPay.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("NetAmt", NetAmt.ToString("#,##0;(#,##0); ")));

            //  Rpt2.SetParameters(new ReportParameter("VouType", "Voucher Type: " + txtVouType));

            Rpt2.SetParameters(new ReportParameter("RptTitle", "RECEIPTS & PAYMENT"));
            Rpt2.SetParameters(new ReportParameter("txtuserinfo", "Print Source :" + username + " , " + session + " , " + printdate));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintReceiveAndPaymentEnt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Todate = System.DateTime.Now.ToString("dd-MMM-yyyy");

            string txtVouType = this.ddlVoucharCash.SelectedValue.ToString().Trim();

            string Ftdate = "(From " + this.txtfromdate.Text + " To " + this.txttodate.Text + ")";

            DataTable dt = (DataTable)Session["recandpay"];
            DataTable dt1 = (DataTable)Session["recandpayFo"];
            DataTable dt2 = (DataTable)ViewState["recandpayNote"];


            double TotoRes = Convert.ToDouble(dt1.Rows[0]["recpam"]);
            double TotoPay = Convert.ToDouble(dt1.Rows[0]["payam"]);
            double NetAmt = TotoRes - TotoPay;
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ReceptPayment>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RDLCAccountSetup.GetLocalReport("R_17_Acc.RptRecAndPaymentEntrust", lst, null, null);
            Rpt2.SetParameters(new ReportParameter("comnam", comnam));
            Rpt2.SetParameters(new ReportParameter("comadd", comadd));
            Rpt2.SetParameters(new ReportParameter("Ftdate", Ftdate));

            Rpt2.SetParameters(new ReportParameter("TotoRes", TotoRes.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("TotoPay", TotoPay.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("NetAmt", NetAmt.ToString("#,##0;(#,##0); ")));

            //  Rpt2.SetParameters(new ReportParameter("VouType", "Voucher Type: " + txtVouType));

            Rpt2.SetParameters(new ReportParameter("RptTitle", "RECEIPTS & PAYMENT"));
            Rpt2.SetParameters(new ReportParameter("txtuserinfo", "Print Source :" + username + " , " + session + " , " + printdate));
            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private  void ProjectwiseReceiptandPaymentDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["recandpay"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PWRPDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptProjectwiseReceptsandPayment", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printdate", printdate));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("date", "From  " + Convert.ToDateTime(this.txtfrmdat2.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txttodat2.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Project Wise Recepits & Payments Details"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintIssuedVsCollection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["recandpay"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.IssuedVsColl>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptRecAndPaymentActual", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("NetBalanceac", ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFNetBalanceac")).Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "RECEIPTS & PAYMENTS(ACTUAL)"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintDailyProposal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tranlist"];
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyPayProposal", list, null, null);
            Rpt2.SetParameters(new ReportParameter("compName", comnam));
            Rpt2.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt2.SetParameters(new ReportParameter("txtDrAmt", Dtdram.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("rptTitle", "Payment Proposal"));
            Rpt2.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintDelTransaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tranlist"];
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.EClassTranList>();
            LocalReport Rpt2 = new LocalReport();
            Rpt2 = RptSetupClass1.GetLocalReport("R_17_Acc.RptDelDailyTransaction", list, null, null);
            Rpt2.SetParameters(new ReportParameter("compName", comnam));
            Rpt2.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt2.SetParameters(new ReportParameter("txtDrAmt", Dtdram.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("txtCrAmt", Dtcram.ToString("#,##0;(#,##0); ")));
            Rpt2.SetParameters(new ReportParameter("rptTitle", "DELETED TRANSACTION LIST"));
            Rpt2.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt2;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintProTransaction()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tranlist"];


            string date = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

            string txtprojectname = "Date Wise Transaction - " + this.ddlAccHead.SelectedItem.Text.Trim().Substring(13);


            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptTransactionList>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptDailyProjectTransaction", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtprojectname", txtprojectname));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void gvtranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvAcRsCode");
                Label acresdesc = (Label)e.Row.FindControl("lblgvAcRsDesc");
                Label lbldram = (Label)e.Row.FindControl("lgvDram");
                Label lblcramt = (Label)e.Row.FindControl("txtgvCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;
                    //lblcramt.Style.Add("text-transform", "initcap");



                }

            }
        }
        protected void gvDailPayPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvAcRsCodePPro");
                Label acresdesc = (Label)e.Row.FindControl("lblgvAcRsDescPPro");
                Label lbldram = (Label)e.Row.FindControl("lgvDramPPro");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;


                }

            }



        }
        protected void gvdtranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label acrescode = (Label)e.Row.FindControl("lblgvdAcRsCode");
                Label acresdesc = (Label)e.Row.FindControl("lblgvdAcRsDesc");
                Label lbldram = (Label)e.Row.FindControl("lgvdDram");
                Label lblcramt = (Label)e.Row.FindControl("txtgvdCram");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "acrescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "  Total Amt:")
                {

                    acrescode.Font.Bold = true;
                    acresdesc.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;

                }

            }
        }
        protected void gvtranlsit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.SessionUpdate2();
            this.gvtranlsit.PageIndex = e.NewPageIndex;
            this.TransactionList();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TransactionList();
            //this.gvtranlsit_DataBind();
        }
        //protected void dgv3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.SessionUpdate2();
        //    this.gvtranlsit.PageIndex = e.NewPageIndex;
        //    this.gvtranlsit_DataBind();
        //}
        protected void gvdtranlsit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void imgbtnSearchVoucher_Click(object sender, EventArgs e)
        {
            this.TransactionList();
        }

        protected void imgbtnSearchVoucherCash_Click(object sender, EventArgs e)
        {
            this.ShowCashBook();
        }

        protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblTo.Visible = (this.ddlSrch.SelectedValue == "between");
            this.txtAmount2.Visible = (this.ddlSrch.SelectedValue == "between");
        }
        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }
        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbtnList1.SelectedIndex)
            {
                case 0:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;

                    break;
                case 1:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;

                    this.lblVoucher.Visible = true;
                    this.ddlVouchar.Visible = true;
                    break;

                case 2:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    break;
                case 3:
                    this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
                    break;
                case 4:
                case 5:
                    this.MultiView1.ActiveViewIndex = 4;
                    break;

                case 6:
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case 7:
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case 8:
                    this.MultiView1.ActiveViewIndex = 6;
                    break;
                case 9:
                    this.MultiView1.ActiveViewIndex = 8;
                    break;
                case 10:
                    this.MultiView1.ActiveViewIndex = 9;
                    break;

                case 11:
                    this.MultiView1.ActiveViewIndex = 10;
                    break;
                case 12:
                    this.MultiView1.ActiveViewIndex = 11;
                    break;
            }
        }

        protected void btnRecDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string recpcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["recandpay"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "recpcode like('" + recpcode + "')";
            dt = dv1.ToTable();

            string mCOMCOD = comcod;

            string mTRNDAT1;
            string mTRNDAT2;

            string TrMod = this.Request.QueryString["TrMod"];
            if (TrMod == "RecPayprj")
            {
                mTRNDAT1 = this.txtfrmdat.Text;
                mTRNDAT2 = this.txttodat.Text;
            }
            else
            {
                mTRNDAT1 = this.txtfromdate.Text;
                mTRNDAT2 = this.txttodate.Text;
            }

            string mACTCODE = dt.Rows[0]["recpcode"].ToString();
            string mACTDESC = dt.Rows[0]["recpdesc"].ToString();
            string lebel2 = dt.Rows[0]["rleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 
            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {


                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            //else if (lebel2 == "")
            //{
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
            //                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            //}
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }



        }

        private void ReceiptAndPaymentDet()
        {
            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string rp = "RP";
            string CBorBoth = (this.rbtnlistrp02.SelectedIndex == 0) ? "C" : (this.rbtnlistrp02.SelectedIndex == 1) ? "B" : "";

            string CallType = (this.rbtnlistrp02.SelectedIndex == 0 || this.rbtnlistrp02.SelectedIndex == 1) ? "RPTRECEIPTPAYMENTCASHORBANK" : "RPTRECEIPTPAYMENT";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", CallType, fromdate, todate, rp, CBorBoth, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["recandpay"] = this.HiddenSameDate(ds1.Tables[0]);
            Session["recandpayFo"] = ds1.Tables[1];
            ViewState["recandpayNote"] = ds1.Tables[2];

            this.gvrecandpay02.DataSource = ds1.Tables[0];
            this.gvrecandpay02.DataBind();
            // this.RPNote();

            for (int i = 0; i < gvrecandpay02.Rows.Count; i++)
            {
                string recpcode = ((Label)gvrecandpay02.Rows[i].FindControl("lblgvrecpcoderp02")).Text.Trim();
                string paycode = ((Label)gvrecandpay02.Rows[i].FindControl("lblgvpaycoderp02")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvrecandpay02.Rows[i].FindControl("btnRecDescrp02");
                LinkButton lbtn2 = (LinkButton)gvrecandpay02.Rows[i].FindControl("btnPayDescrp02");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }


            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (ds1.Tables[0].Rows.Count > 0)
            //    ((HyperLink)this.gvrecandpay02.HeaderRow.FindControl("hlbtnRcvPayCdataExelrp02")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));


            Session["Report1"] = gvrecandpay02;
            if (ds1.Tables[0].Rows.Count > 0)
            {


                ((HyperLink)this.gvrecandpay02.HeaderRow.FindControl("hlbtnRcvPayCdataExelrp02")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            }
        }

        private void ReceiptAndPaymentDetCustomized()
        {
            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string rp = "RP";
            string CBorBoth = "";            
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RPTRECPAYCUSTOMIZED", fromdate, todate, rp, CBorBoth, "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["recandpay"] = this.HiddenSameDate(ds1.Tables[0]);
            //Session["recandpayFo"] = ds1.Tables[1];
            //ViewState["recandpayNote"] = ds1.Tables[2];

            this.gvRecPayCustomized.DataSource = ds1.Tables[0];
            this.gvRecPayCustomized.DataBind();        

            for (int i = 0; i < gvRecPayCustomized.Rows.Count; i++)
            {
                string recpcode = ((Label)gvRecPayCustomized.Rows[i].FindControl("lblgvrecpcoderp03")).Text.Trim();
                string paycode = ((Label)gvRecPayCustomized.Rows[i].FindControl("lblgvpaycoderp03")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvRecPayCustomized.Rows[i].FindControl("btnRecDescrp03");
                LinkButton lbtn2 = (LinkButton)gvRecPayCustomized.Rows[i].FindControl("btnPayDescrp03");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }

            Session["Report1"] = gvRecPayCustomized;
            if (ds1.Tables[0].Rows.Count > 0)
            {

                ((HyperLink)this.gvRecPayCustomized.HeaderRow.FindControl("hlbtnRcvPayCdataExelrp03")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            }
        }
        protected void btnPayDesc_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string paycode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["recandpay"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "paycode like('" + paycode + "')";
            dt = dv1.ToTable();
            string mCOMCOD = comcod;
            string mTRNDAT1;
            string mTRNDAT2;

            string TrMod = this.Request.QueryString["TrMod"];
            if (TrMod == "RecPayprj")
            {
                mTRNDAT1 = this.txtfrmdat.Text;
                mTRNDAT2 = this.txttodat.Text;
            }
            else
            {
                mTRNDAT1 = this.txtfromdate.Text;
                mTRNDAT2 = this.txttodate.Text;
            }

            string mACTCODE = dt.Rows[0]["paycode"].ToString();
            string mACTDESC = dt.Rows[0]["paydesc"].ToString();
            string lebel2 = dt.Rows[0]["pleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 

            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {


                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=PrjReportRP&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
        }


        protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        {

            this.GetAccCode();

        }
        protected void gvrecandpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btnRecDesc");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrecpam");

                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btnPayDesc");
                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayam1");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

            }


        }

        protected void gvRecptPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btnRecDescp");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrecpamp");

                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btnPayDescp");
                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayam1p");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

            }


        }
        protected void gvbankbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbb");
                Label opnam = (Label)e.Row.FindControl("lgvopnambb");
                Label closam = (Label)e.Row.FindControl("lgvclosambb");
                HyperLink balam = (HyperLink)e.Row.FindControl("hlnkgvbalambb");

                //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();
                double netbal = Convert.ToDouble((DataBinder.Eval(e.Row.DataItem, "netbal")).ToString());


                if (code == "")
                {
                    return;
                }


                if (ASTUtility.Right(code, 3) == "AAA")
                {


                    ActDesc.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    balam.Font.Bold = true;
                    ActDesc.Style.Add("text-align", "right");

                }
                if (code == "003AAA" && netbal != 0.00)
                {
                    balam.Font.Bold = true;
                    balam.Style.Add("color", "blue");
                    balam.NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;


                }





            }
        }

        protected void gvbankbal1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbbp");
                Label opnam = (Label)e.Row.FindControl("lgvopnambbp");
                Label closam = (Label)e.Row.FindControl("lgvclosambbp");
                HyperLink balam = (HyperLink)e.Row.FindControl("hlnkgvbalambbp");

                //((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();
                double netbal = Convert.ToDouble((DataBinder.Eval(e.Row.DataItem, "netbal")).ToString());


                if (code == "")
                {
                    return;
                }


                if (ASTUtility.Right(code, 3) == "AAA")
                {


                    ActDesc.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    balam.Font.Bold = true;
                    ActDesc.Style.Add("text-align", "right");

                }
                if (code == "003AAA" && netbal != 0.00)
                {
                    balam.Font.Bold = true;
                    balam.Style.Add("color", "blue");
                    balam.NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=BalConfirmation&Date1=" + this.txtfromdate.Text + "&Date2=" + this.txttodate.Text;


                }





            }
        }

        protected void gvPtotranlsit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT2 = ((Label)e.Row.FindControl("lblgvvnump1")).Text;
            if (mTRNDAT2.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + mTRNDAT2;

            }


        }

        protected void lbtnshow2_Click(object sender, EventArgs e)
        {
            ReceiptAndPaymentproj02();

        }
        private void ReceiptAndPaymentproj02()
        {
            Session.Remove("recandpay");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfrmdat2.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodat2.Text).ToString("dd-MMM-yyyy");
            string comcod = GetCompCode();
            string rp = "RP";
            string CBorBoth = (this.rbtncashbank2.SelectedIndex == 0) ? "C" : (this.rbtncashbank2.SelectedIndex == 1) ? "B" : "";
            string prjcode = this.ddlproject2.SelectedValue.ToString();
            string CallType = (this.rbtncashbank2.SelectedIndex == 0 || this.rbtncashbank2.SelectedIndex == 1) ? "RPTRECEIPTPAYMENTCASHORBANKPRJWISE" : "RPTRECEIPTPAYMENTPRJWISE";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", CallType, fromdate, todate, rp, CBorBoth, prjcode, "", "", "", "");
            if (ds1 == null)
                return;
            Session["recandpay"] = this.HiddenSameDate(ds1.Tables[0]);
            Session["recandpayFo"] = ds1.Tables[1];
            ViewState["recandpayNote"] = ds1.Tables[2];

            this.gvrecandpay03.DataSource = ds1.Tables[0];
            this.gvrecandpay03.DataBind();

            for (int i = 0; i < gvrecandpay03.Rows.Count; i++)
            {
                string recpcode = ((Label)gvrecandpay03.Rows[i].FindControl("lblgvrp2recpcode")).Text.Trim();
                string paycode = ((Label)gvrecandpay03.Rows[i].FindControl("lblgvrp2paycode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvrecandpay03.Rows[i].FindControl("btngvrp2recpdesc");
                LinkButton lbtn2 = (LinkButton)gvrecandpay03.Rows[i].FindControl("btngvrp2paydesc");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }
            Session["Report1"] = gvrecandpay03;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.gvrecandpay03.HeaderRow.FindControl("btngvrp2ept2excel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }



        protected void btngvrp2recpdesc_Click(object sender, EventArgs e)
        {

        }

        protected void btngvrp2paydesc_Click(object sender, EventArgs e)
        {

        }

        protected void gvrecandpay03_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btngvrp2recpdesc");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrp2recpam");
                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btngvrp2paydesc");
                Label lgvPayAmt = (Label)e.Row.FindControl("lblgvrp2payam");

                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

                if (ASTUtility.Left(code1, 2) == "OP" || ASTUtility.Left(code1, 2) == "RP" || ASTUtility.Left(code1, 2) == "CL")
                {
                    HyRecDesc.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    lgvRecAmt.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    HyPayDesc.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    lgvPayAmt.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                }

            }
        }

        protected void gvRecPayCustomized_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btnRecDescrp03");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrecpamrp03");
                Label lblgvtorecam = (Label)e.Row.FindControl("lblgvtorecpamrp03");
                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btnPayDescrp03");

                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayamrp03");
                Label lgvtopayam = (Label)e.Row.FindControl("lgvtopayamrp03");
                


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 3) == "000" || ASTUtility.Right(code1, 3) == "AAA")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                    lblgvtorecam.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 3) == "000" || ASTUtility.Right(code1, 3) == "AAA")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                    lgvtopayam.Font.Bold = true;
                }

                if (ASTUtility.Left(code1, 2) == "OP" || ASTUtility.Left(code1, 2) == "RP" || ASTUtility.Left(code1, 2) == "CL")
                {
                    HyRecDesc.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    lgvRecAmt.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    HyPayDesc.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    lgvPayAmt.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                }

            }
        }

        protected void gvrecandpay02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btnRecDescrp02");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrecpamrp02");

                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btnPayDescrp02");

                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayamrp02");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000" || ASTUtility.Right(code1, 8) == "AAAAAAAA")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

                if (ASTUtility.Left(code1, 2) == "OP" || ASTUtility.Left(code1, 2) == "RP" || ASTUtility.Left(code1, 2) == "CL")
                {
                    HyRecDesc.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    lgvRecAmt.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    HyPayDesc.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                    lgvPayAmt.Attributes["style"] = "font-weight:bold;color:green;background:yellow";
                }

            }
        }

        //protected void IbtnSearchAcc_Click(object sender, EventArgs e)
        //{
        //    this.GetAccCode();
        //}

        protected void btnRecDescrp02_Click(object sender, EventArgs e)
        {

            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mACTDESC = ((DataTable)Session["recandpay"]).Rows[index]["recpdesc"].ToString();
            string mRESCODE = ((DataTable)Session["recandpay"]).Rows[index]["recpcode"].ToString();
            string mACTCODE = ((DataTable)Session["recandpay"]).Rows[index]["recpcode2"].ToString();
            string mTRNDAT1 = this.txtfromdate.Text;
            string mTRNDAT2 = this.txttodate.Text;
            string mCOMCOD = this.GetCompCode();


            string opnoption = "withoutopening";

            //if(ASTUtility.Left(mACTCODE,2)=="19" || ASTUtility.Left(mACTCODE, 2) == "29")
            //{
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE  + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=" + opnoption + "', target='_blank');</script>";

            //}

            if (mRESCODE != "000000000000")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=" + opnoption + "', target='_blank');</script>";
            }



        }
        protected void btnPayDescrp02_Click(object sender, EventArgs e)
        {

            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            int index = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string mACTDESC = ((DataTable)Session["recandpay"]).Rows[index]["paydesc"].ToString();
            string mRESCODE = ((DataTable)Session["recandpay"]).Rows[index]["paycode"].ToString();
            string mACTCODE = ((DataTable)Session["recandpay"]).Rows[index]["paycode2"].ToString();
            string mTRNDAT1 = this.txtfromdate.Text;
            string mTRNDAT2 = this.txttodate.Text;
            string mCOMCOD = this.GetCompCode();


            string opnoption = "withoutopening";

            if (mRESCODE != "000000000000")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=" + opnoption + "', target='_blank');</script>";
            }

            //if (mRESCODE != "000000000000")
            //{
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE + "&actdesc=" + mACTDESC + "&Date1=" + fromdate + "&Date2=" + todate + "&opnoption=" + opnoption + "', target='_blank');</script>";
            //}



        }
    }


}



