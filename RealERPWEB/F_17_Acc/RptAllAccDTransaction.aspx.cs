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
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAllAccDTransaction : System.Web.UI.Page
    {

        ProcessAccess MktData = new ProcessAccess();
        public static double OpenBal, Clsbal, Dtdram, Dtcram;

        //double OpenBal = 0, Clsbal = 0;

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


                ((Label)this.Master.FindControl("lblTitle")).Text = "Transaction with Post Dated Cheque";
                //    :(TrMod == "DelTran" ? "DELETED TRANSACTION": "FUND FLOW")));
                //this.Master.Page.Title = "Transaction with Post Dated Cheque";

                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            }



        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            this.ShowCashBook();




        }

        //private void TransactionList() 
        //{
        //    Session.Remove("tranlist");
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
        //    string txtVouType = this.ddlVouchar.SelectedItem.ToString().Trim();
        //    string txtSVoucher = (txtVouType == "ALL Voucher" ? "" : txtVouType) + "%";


        //    string searchinfo = "";

        //    if (this.ddlSrch.SelectedValue != "")
        //    {

        //        if (this.ddlSrch.SelectedValue == "between")
        //        {
        //            searchinfo = "dram between " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmount2.Text.Trim()).ToString();

        //        }

        //        else 
        //        {
        //            searchinfo ="dram " +this.ddlSrch.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmount1.Text.Trim()).ToString();

        //        }
        //    }


        //    DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTTRANSACTIONS", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    DataTable dtr = ds1.Tables[0];
        //    DataTable dtr1 = HiddenSameDate(dtr);
        //    Session["tranlist"] = dtr1;
        //    DataTable tblt03 = (DataTable)Session["tranlist"];
        //    this.gvtranlsit.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        //    //this.dgv3.DataSource = tblt03;
        //    //this.dgv3.DataBind();

        //    this.gvtranlsit.DataSource = dtr1;
        //    this.gvtranlsit.DataBind();
        //    Session["tranlist"] = dtr1;
        //    this.FooterCalculation(dtr1, "gvtranlsit");

        //}

        private void ShowCashBook()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = hst["comcod"].ToString();
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

            //string txtSProject =  "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTCASHBOOKwithPOSTCHQ", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
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
            this.FooterCalculation(dtr1, "gvcashbook");



            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();
            this.FooterCalculation(dtr2, "gvcashbookp");


            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='C'");
            DataTable dtr3 = dvr.ToTable();
            this.gvcashbookDB.DataSource = dvr.ToTable(); ;
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDB");
        }



        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();

            //
            switch (GvName)
            {
                case "gvcashbook":
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                            0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvFBankAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                          0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;


                case "gvcashbookp":
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvCashAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvFBankAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
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



            }


        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;


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
            return dt1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();


            this.PrintCashBook();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }




        }

        private void PrintCashBook()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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

            switch (comcod)
            {

                case "3348":// Credence

                    //rptsl = new RealERPRPT.R_17_Acc.RPTSpecialLedgerRup();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccCashbook1Credence", lst, null, null);
                    break;

                default:

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccCashbook1", lst, null, null);


                    break;


            }
            // Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtrptTitle", "CASH BOOK"));
            Rpt1.SetParameters(new ReportParameter("txtdate", "CASH BOOK"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtdate", "(From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        //private void PrintTransaction() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


        //    DataTable dt = (DataTable)Session["tranlist"];
        //    ReportDocument rptdtlist = new RealERPRPT.R_17_Acc.RptDailyTransaction();
        //    TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rptCname.Text =comnam;
        //    TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")+")";

        //    TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
        //    rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");
        //    TextObject rptcram = rptdtlist.ReportDefinition.ReportObjects["txtcram"] as TextObject;
        //    rptcram.Text = Dtcram.ToString("#,##0;(#,##0); ");

        //    TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptdtlist.SetDataSource(dt);
        //    Session["Report1"] = rptdtlist;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}



        //private void PrintReceiveAndPayment() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    DataTable dt = (DataTable)Session["recandpay"];
        //    ReportDocument rptrandpay = new RealERPRPT.R_17_Acc.RptRecAndPayment();
        //    TextObject rptCname = rptrandpay.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject rpttxtHeaderTitle = rptrandpay.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
        //    rpttxtHeaderTitle.Text =(this.rbtnList1.SelectedIndex==2)?"RECEIPTS & PAYMENTS":"FUND FLOW";
        //    TextObject rptdate = rptrandpay.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")+")";
        //    TextObject rptopenbal = rptrandpay.ReportDefinition.ReportObjects["Openbal"] as TextObject;
        //    rptopenbal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFObal")).Text.Trim();
        //    TextObject rpttopebal = rptrandpay.ReportDefinition.ReportObjects["topenbal"] as TextObject;
        //    rpttopebal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFTRAmt")).Text.Trim();
        //    TextObject rptclsbal = rptrandpay.ReportDefinition.ReportObjects["clsbal"] as TextObject;
        //    rptclsbal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFCbal")).Text.Trim();
        //    TextObject rpttclsbal = rptrandpay.ReportDefinition.ReportObjects["tclsbal"] as TextObject;
        //    rpttclsbal.Text = ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFTPAmt")).Text.Trim();
        //    TextObject txtuserinfo = rptrandpay.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptrandpay.SetDataSource(dt);
        //    Session["Report1"] = rptrandpay;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        //}

        //private void PrintDailyProposal() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


        //    DataTable dt = (DataTable)Session["tranlist"];
        //    ReportDocument rptdtlist = new RealERPRPT.R_17_Acc.RptDailyPayProposal();
        //    TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

        //    TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
        //    rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");


        //    TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptdtlist.SetDataSource(dt);
        //    Session["Report1"] = rptdtlist;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        //}

        //private void PrintDelTransaction() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


        //    DataTable dt = (DataTable)Session["tranlist"];
        //    ReportDocument rptdtlist = new RealERPRPT.R_17_Acc.RptDelDailyTransaction();
        //    TextObject rptCname = rptdtlist.ReportDefinition.ReportObjects["CompName"] as TextObject;
        //    rptCname.Text = comnam;
        //    TextObject rptdate = rptdtlist.ReportDefinition.ReportObjects["date"] as TextObject;
        //    rptdate.Text = "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")";

        //    TextObject rptdram = rptdtlist.ReportDefinition.ReportObjects["txtdram"] as TextObject;
        //    rptdram.Text = Dtdram.ToString("#,##0;(#,##0); ");
        //    TextObject rptcram = rptdtlist.ReportDefinition.ReportObjects["txtcram"] as TextObject;
        //    rptcram.Text = Dtcram.ToString("#,##0;(#,##0); ");

        //    TextObject txtuserinfo = rptdtlist.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptdtlist.SetDataSource(dt);
        //    Session["Report1"] = rptdtlist;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}
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

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.TransactionList();
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
            //this.TransactionList();
        }

        protected void imgbtnSearchVoucherCash_Click(object sender, EventArgs e)
        {
            this.ShowCashBook();
        }

        protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
        {

            //this.lblTo.Visible = (this.ddlSrch.SelectedValue == "between");
            //this.txtAmount2.Visible = (this.ddlSrch.SelectedValue == "between");
        }
        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }
        //protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (rbtnList1.SelectedIndex)
        //    {
        //        case 0:
        //            this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;

        //            break;
        //        case 1:
        //            this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
        //            this.lblPage.Visible = true;
        //            this.ddlpagesize.Visible = true;
        //            this.lblVoucher.Visible = true;
        //            this.ddlVouchar.Visible = true;
        //            break;

        //        case 2:
        //            this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;               
        //            break;
        //        case 3:
        //            this.MultiView1.ActiveViewIndex = rbtnList1.SelectedIndex;
        //            break;
        //        case 4:
        //        case 5:
        //            this.MultiView1.ActiveViewIndex = 4;
        //            break;
        //    }
        //}

    }
}
