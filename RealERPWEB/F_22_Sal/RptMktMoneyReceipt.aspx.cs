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
namespace RealERPWEB.F_22_Sal
{
    public partial class RptMktMoneyReceipt : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                this.PrintDupOrOrginal();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MONEY RECEIPT";

            }
            if (this.ddlMRNO.Items.Count == 0)
            {
                this.GetMRNO();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        private void PrintDupOrOrginal()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3301":
                case "1301":
                    //case "3101":
                    this.chkOrginal.Visible = true;
                    break;




            }


        }

        private void GetMRNO()
        {
            Session.Remove("Mrno");

            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comcod = this.GetCompCode();
            //string txtSProject = "%" + this.txtSrcPro.Text + "%";
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string txtSProject = (qgenno.Length == 0 ? ("%" ) : this.Request.QueryString["genno"].ToString()) + "%";

            string type = (this.Request.QueryString["Type"].ToString() == "CustCare" || this.Request.QueryString["Type"].ToString() == "Billing") ? "GETMRNODATEWISEALL" : "GETMRNODATEWISE";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", type, txtSProject, fromdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMRNO.DataTextField = "mrdesc";
            this.ddlMRNO.DataValueField = "mrno";
            this.ddlMRNO.DataSource = ds1.Tables[0];
            this.ddlMRNO.DataBind();
            Session["Mrno"] = ds1.Tables[0];
            this.ddlMRNO_SelectedIndexChanged(null, null);

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetMRNO();
        }




        protected void ddlMRNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mrno = this.ddlMRNO.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["Mrno"];
            DataRow[] dr1 = dt.Select("mrno='" + mrno + "'");
            if (dr1.Length > 0)
            {
                this.lPactCode.Text = dr1[0]["pactcode"].ToString();
                this.lusircode.Text = dr1[0]["usircode"].ToString();
                this.chkOrginal.Checked = Convert.ToBoolean(dr1[0]["oprint"]);
                this.chkOrginal.Enabled = !(Convert.ToBoolean(dr1[0]["oprint"]));
            }

        }
        private string CompanyPrintMR()

        {

            string comcod = this.GetCompCode();
            string mrprint = "";
            switch (comcod)
            {
                case "1301":
                case "3301":
                    //case "3101":
                    mrprint = "MRPrint1";
                    break;


                case "2325":
                case "3325":
                //case "3101":
                    mrprint = "MRPrint2";
                    break;

                //  case "3101":
                case "3335":
                    // case "3101":
                    mrprint = "MRPrint3";
                    break;

                case "3337":
                case "3336":
                    // case "3101":
                    mrprint = "MRPrint4";
                    break;

                case "3339":
                    //case "3101":
                    mrprint = "MRPrint5";
                    break;

                case "3351":
                    //case "3101" :
                    mrprint = "MRPrintWecon";
                    break;

                case "3352":
                //case "3101":
                    mrprint = "MRPrint360";
                    break;

                case "3356":
                    mrprint = "MRPrintIntech";
                    break;

                default:
                    mrprint = "MRPrint";
                    break;
            }
            return mrprint;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.lPactCode.Text.Trim();
            string usircode = this.lusircode.Text.Trim();
            string mrno = this.ddlMRNO.SelectedValue.ToString();
            string mrdate = Convert.ToDateTime(this.lblrecdate.Text).ToString("dd-MMM-yyyy");

            string hostname = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/F_17_Acc/";
            string currentptah = "PrintMoneyReceipt?Type=moneyReceipt&pactcode=" + pactcode + "&usircode=" + usircode + "&mrno=" + mrno + "&mrdate=" + mrdate;
            //string totalpath = hostname + currentptah;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('" + totalpath + "', target='_blank');</script>";
            Response.Redirect("~/F_17_Acc/" + currentptah);




            #region MRPrint
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string UsirCode = this.lusircode.Text.Trim();
            //string PactCode = this.lPactCode.Text.Trim();
            //string mrno = this.ddlMRNO.SelectedValue.ToString();
            //string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string date = Convert.ToDateTime(this.lblrecdate.Text).ToString("dd-MMM-yyyy");

            //if (this.chkAllSchedul.Checked == true)
            //{
            //    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSTALLMANTWITHMRR", PactCode, UsirCode, date, "", "", "", "", "", "");
            //    DataTable dtstatus = ds2.Tables[0];
            //    DataView dv1 = dtstatus.DefaultView;
            //    dv1.RowFilter = "mrno='" + mrno + "'";
            //    dtstatus = dv1.ToTable();

            //    DataSet ds4 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTMONEYRECEIPT", PactCode, UsirCode, mrno, "", "", "", "", "", "");
            //    if (ds4 == null)
            //        return;
            //    DataTable dtrpt = ds4.Tables[0];
            //    string custadd = dtrpt.Rows[0]["custadd"].ToString();
            //    string custid = dtrpt.Rows[0]["usircode"].ToString();
            //    string receptno = dtrpt.Rows[0]["mrno"].ToString();
            //    string project = dtrpt.Rows[0]["pactdesc"].ToString();
            //    string receivdate = Convert.ToDateTime(dtrpt.Rows[0]["mrdate"]).ToString("dd-MMM-yyy");
            //    string refe = dtrpt.Rows[0]["refno"].ToString();
            //    string txtcustName = dtrpt.Rows[0]["custname"].ToString();
            //    string udesc = dtrpt.Rows[0]["udesc"].ToString();
            //    string usize = Convert.ToDouble(dtrpt.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            //    string munit = dtrpt.Rows[0]["munit"].ToString();
            //    string paytype = dtrpt.Rows[0]["paytype"].ToString();
            //    string chqno = dtrpt.Rows[0]["chqno"].ToString();
            //    string bankname = dtrpt.Rows[0]["bankname"].ToString();
            //    string branch = dtrpt.Rows[0]["bbranch"].ToString();
            //    string refno = dtrpt.Rows[0]["refno"].ToString();
            //    string custteam = dtrpt.Rows[0]["custteam"].ToString();
            //    string rmrks = dtrpt.Rows[0]["rmrks"].ToString();

            //    double amount = Convert.ToDouble(((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text);
            //    double disamt = Convert.ToDouble("0" + ((Label)this.grvacc.FooterRow.FindControl("lblgvFdisamt")).Text.Trim());
            //    string amt1t = ASTUtility.Trans(Math.Abs(amount - disamt), 2);

            //    string Typedes = "";
            //    if (paytype == "CHEQUE" || paytype == "P.O")
            //    {
            //        Typedes = paytype + ", " + "No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;
            //    }
            //    else
            //    {
            //        Typedes = paytype;
            //    }

            //    LocalReport Rpt1 = new LocalReport();
            //    var list = dtstatus.DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //    Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptDetailMoneyRecept", list, null, null);
            //    Rpt1.EnableExternalImages = true;
            //    Rpt1.SetParameters(new ReportParameter("txtReceptNo", receptno));
            //    Rpt1.SetParameters(new ReportParameter("txtUnit", udesc + ", " + usize + " " + munit));
            //    Rpt1.SetParameters(new ReportParameter("txtProjectName", project));
            //    Rpt1.SetParameters(new ReportParameter("txtReceivedDate", receivdate));
            //    Rpt1.SetParameters(new ReportParameter("txtRefNo", refno));
            //    Rpt1.SetParameters(new ReportParameter("txtCustID", custid));
            //    Rpt1.SetParameters(new ReportParameter("txtCustFrom", txtcustName));
            //    Rpt1.SetParameters(new ReportParameter("txtCustAdd", custadd));
            //    Rpt1.SetParameters(new ReportParameter("txtCustName1", txtcustName));
            //    Rpt1.SetParameters(new ReportParameter("txtTakaInWord", amt1t));
            //    Rpt1.SetParameters(new ReportParameter("rptTitle", "Money Receipts"));
            //    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            //    Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "Money Receipt Info";
            //        string eventdesc = "Print MR";
            //        string eventdesc2 = receptno;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }


            //    Session["Report1"] = Rpt1;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //}
            //else
            //{
            //    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSTALLMANTWITHMRR", PactCode, UsirCode, date, "", "", "", "", "", "");
            //    DataTable dtstatus = ds2.Tables[0];
            //    DataView dv1 = dtstatus.DefaultView;
            //    dv1.RowFilter = "mrno='" + mrno + "'";
            //    DataTable dtmr = dv1.ToTable();
            //    string Installment = "";
            //    for (int i = 0; i < dtmr.Rows.Count; i++)
            //    {
            //        if (i == 0)
            //        {
            //            if (Convert.ToDouble(dtmr.Rows[i]["schamt"].ToString()) == Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()))
            //                Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
            //            else
            //                if (Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()) < 0)
            //                Installment = Installment + "REFUNDABLE COLLECTION, ";
            //            else
            //                Installment = Installment + dtmr.Rows[i]["gdesc"] + " (Partly), ";

            //        }
            //        else if (dtmr.Rows[i - 1]["gdesc"].ToString().Trim() != dtmr.Rows[i]["gdesc"].ToString().Trim())
            //        {
            //            if (Convert.ToDouble(dtmr.Rows[i]["schamt"].ToString()) == Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()))
            //                Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
            //            else
            //                if (Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()) < 0)
            //                Installment = Installment + "REFUNDABLE COLLECTION, ";
            //            else
            //                Installment = Installment + dtmr.Rows[i]["gdesc"] + " (Partly), ";

            //        }

            //    }
            //    int len = Installment.Length;
            //    Installment = (len == 0) ? "" : ASTUtility.Left(Installment, len - 2);
            //    DataSet ds4 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTMONEYRECEIPT", PactCode, UsirCode, mrno, "", "", "", "", "", "");
            //    if (ds4 == null)
            //        return;
            //    DataTable dtrpt = ds4.Tables[0];
            //    string custadd = dtrpt.Rows[0]["custadd"].ToString();
            //    string custmob = dtrpt.Rows[0]["custmob"].ToString();
            //    string udesc = dtrpt.Rows[0]["udesc"].ToString();
            //    string usize = Convert.ToDouble(dtrpt.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            //    string munit = dtrpt.Rows[0]["munit"].ToString();
            //    string paytype = dtrpt.Rows[0]["paytype"].ToString();
            //    string chqno = dtrpt.Rows[0]["chqno"].ToString();
            //    string bankname = dtrpt.Rows[0]["bankname"].ToString();
            //    string branch = dtrpt.Rows[0]["bbranch"].ToString();
            //    string paidamt = Convert.ToDouble(Convert.ToDouble(((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text)).ToString("#,##0; -#,##0; -");
            //    string refno = dtrpt.Rows[0]["refno"].ToString();
            //    string bookno = dtrpt.Rows[0]["bookno"].ToString();
            //    string custteam = dtrpt.Rows[0]["custteam"].ToString();
            //    string rmrks = dtrpt.Rows[0]["rmrks"].ToString();
            //    string rectype = dtrpt.Rows[0]["rectype"].ToString();
            //    string rectcode = dtrpt.Rows[0]["rectcode"].ToString();


            //    double amt1 = Math.Abs(Convert.ToDouble(Convert.ToDouble(((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text)));
            //    double disamt = Convert.ToDouble("0" + ((Label)this.grvacc.FooterRow.FindControl("lblgvFdisamt")).Text.Trim());
            //    string amt1t = ASTUtility.Trans(amt1 - disamt, 2);
            //    string Typedes = "";
            //    if (paytype == "CHEQUE")
            //    {
            //        Typedes = paytype + ", " + "No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;

            //    }
            //    else if (paytype == "P.O")
            //    {
            //        Typedes = paytype + ", " + "No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;

            //    }
            //    else
            //    {

            //        Typedes = paytype;
            //    }

            //    string Type = this.CompanyPrintMR();
            //    ReportDocument rptMoneyRcpt = new ReportDocument();
            //    LocalReport Rpt1 = new LocalReport();
            //    if (Type == "MRPrint1")
            //    {
            //        if (this.chkOrginal.Checked && this.chkOrginal.Enabled)
            //            this.MoneyReceiptPrint();

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt1", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompAdd", comadd));
            //        Rpt1.SetParameters(new ReportParameter("CompAdd1", comadd));
            //        Rpt1.SetParameters(new ReportParameter("txtmontype1", (rectcode == "54097") ? rectype : (rectcode == "54099") ? rectype : "MONEY RECEIPT"));
            //        Rpt1.SetParameters(new ReportParameter("txtmontype2", (rectcode == "54097") ? rectype : (rectcode == "54099") ? rectype : "MONEY RECEIPT"));
            //        Rpt1.SetParameters(new ReportParameter("txtptintable", (this.chkOrginal.Checked && this.chkOrginal.Enabled) ? "Orginal" : "Duplicate"));
            //        Rpt1.SetParameters(new ReportParameter("txtptintable1", (this.chkOrginal.Checked && this.chkOrginal.Enabled) ? "Orginal" : "Duplicate"));
            //        Rpt1.SetParameters(new ReportParameter("txtrecno1", (rectcode == "54097") ? "Refund No" : (rectcode == "54099") ? "Adjustment No" : "Receipt No"));
            //        Rpt1.SetParameters(new ReportParameter("txtrecno2", (rectcode == "54097") ? "Refund No" : (rectcode == "54099") ? "Adjustment No" : "Receipt No"));
            //        Rpt1.SetParameters(new ReportParameter("txtamttitle1", (rectcode == "54097") ? "Being the amount Refunded" : (rectcode == "54099") ? "Being the amount Adjusted" : "Received with thanks a sum of"));
            //        Rpt1.SetParameters(new ReportParameter("txtamttitle2", (rectcode == "54097") ? "Being the amount Refunded" : (rectcode == "54099") ? "Being the amount Adjusted" : "Received with thanks a sum of"));
            //        Rpt1.SetParameters(new ReportParameter("txtpayorroradajnst1", (rectcode == "54097") ? "Refund Against" : (rectcode == "54099") ? "Adjusted Against" : "Payment Received Against"));
            //        Rpt1.SetParameters(new ReportParameter("txtpayorroradajnst2", (rectcode == "54097") ? "Refund Against" : (rectcode == "54099") ? "Adjusted Against" : "Payment Received Against"));
            //        Rpt1.SetParameters(new ReportParameter("takainword", "Inwords: " + amt1t));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", "Inwords: " + amt1t));
            //        Rpt1.SetParameters(new ReportParameter("txtsignature", (rectcode == "54097") ? "Client Signature" : (rectcode == "54099") ? "Client Signature" : "Prepared By"));
            //        Rpt1.SetParameters(new ReportParameter("txtnote1", (rectcode == "54097") ? "" : (rectcode == "54099") ? "" : "Note: This Money Receipt will be valid Subject to Encashment of the Cheque/DD/Advice/Pay Order"));
            //        Rpt1.SetParameters(new ReportParameter("txtnote2", (rectcode == "54097") ? "" : (rectcode == "54099") ? "" : "Note: This Money Receipt will be valid Subject to Encashment of the Cheque/DD/Advice/Pay Order"));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " /-  "));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " /-  "));
            //        Rpt1.SetParameters(new ReportParameter("paytype", paytype));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", paytype));
            //        Rpt1.SetParameters(new ReportParameter("paydesc", (rectcode == "54097") ? rmrks : (rectcode == "54099") ? rmrks : (rectcode == "54009") ? rectype : Installment));
            //        Rpt1.SetParameters(new ReportParameter("paydesc1", (rectcode == "54097") ? rmrks : (rectcode == "54099") ? rmrks : (rectcode == "54009") ? rectype : Installment));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //    }


            //    // leisure
            //    else if (Type == "MRPrint2")
            //    {

            //        if (this.chkOrginal.Checked && this.chkOrginal.Enabled)
            //            this.MoneyReceiptPrint();

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptLeisure", list, null, null);

            //        amt1t = amt1t.Replace("Only", "");

            //        amt1t = amt1t.Replace("Taka", "");

            //        Rpt1.SetParameters(new ReportParameter("usize", udesc));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd)));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd)));
            //        Rpt1.SetParameters(new ReportParameter("takainword", "BDT. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " " + amt1t + " " + "Only " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", "BDT. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " " + amt1t + " " + "Only " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
            //        //txtuserinfo
            //        //txtcominfo

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //    }

            //    else if (Type == "MRPrint3")
            //    {

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptEdison", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //    }


            //    else if (Type == "MRPrint4")
            //    {

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptSuvastu", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("txtAddress", comadd));
            //        Rpt1.SetParameters(new ReportParameter("txtAddress1", comadd));
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //    }


            //    else if (Type == "MRPrint5")
            //    {

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptTro", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("bookno", bookno));
            //        Rpt1.SetParameters(new ReportParameter("bookno1", bookno));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //    }

            //    else if (Type == "MRPrintWecon")
            //    {

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptWecon", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //    }

            //    else if(Type== "MRPrint360")
            //    {
            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt360", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //    }

            //    else if(Type== "MRPrintIntech")
            //    {
            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptIntech", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t.Replace("Taka", "").Replace("Only", "Taka Only") + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t.Replace("Taka", "").Replace("Only", "Taka Only") + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));


            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //    }

            //    else
            //    {

            //        var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
            //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt", list, null, null);
            //        Rpt1.EnableExternalImages = true;
            //        Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
            //        Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
            //        Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
            //        Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
            //        Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
            //        Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
            //        Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
            //        Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

            //        Session["Report1"] = Rpt1;
            //        ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //    }

            //}
            #endregion
        }

        private void MoneyReceiptPrint()
        {
            string comcod = this.GetCompCode();
            string mrrno = this.ddlMRNO.SelectedValue.ToString().Trim();
            string mPrint = this.chkOrginal.Checked ? "1" : "0";
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSORUPDATEMPRINT", mrrno, mPrint, "", "", "", "", "", "", "", "", "", "", "", "", "");

        }
        protected void lblShow_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblmsg")).Text = " ";

            string comcod = this.GetCompCode();
            string mrno = this.ddlMRNO.SelectedValue.ToString();
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "SHOWMRNO", mrno, "", "", "", "", "", "", "", "");
            DataTable dtstatus = ds2.Tables[0];

            if (dtstatus == null)
                return;

            this.grvacc.DataSource = ds2.Tables[0];
            this.grvacc.DataBind();

            if (dtstatus.Rows.Count > 0)
            {
                this.lblrecdate.Text = Convert.ToDateTime(dtstatus.Rows[0]["mrdate"]).ToString("dd-MMM-yyyy");
                ((Label)this.grvacc.FooterRow.FindControl("txtFTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dtstatus.Compute("Sum(paidamt)", "")) ? 0.00 : dtstatus.Compute("Sum(paidamt)", ""))).ToString("#,##0;-#,##0; ");
                ((Label)this.grvacc.FooterRow.FindControl("lblgvFdisamt")).Text = Convert.ToDouble((Convert.IsDBNull(dtstatus.Compute("Sum(disamt)", "")) ? 0.00 : dtstatus.Compute("Sum(disamt)", ""))).ToString("#,##0;-#,##0; ");

            }
            if (Request.QueryString["Type"].Trim() == "CustCare" || Request.QueryString["Type"].Trim() == "Billing")
            {
                ((LinkButton)this.grvacc.FooterRow.FindControl("lnkDelete")).Visible = false;
            }

        }


        protected void lnkDelete_Click1(object sender, EventArgs e)
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string deletebyid = hst["usrid"].ToString();
                string deleteseson = hst["session"].ToString();
                string deletetrmid = hst["compname"].ToString();
                string comcod = hst["comcod"].ToString();
                string date = System.DateTime.Today.ToString();
                string mrrno = this.ddlMRNO.SelectedValue.ToString().Trim();
                bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEMRRNO", mrrno, deletebyid, deleteseson, deletetrmid, date, "", "", "", "", "", "", "", "", "", "");

                if (result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Money Receipt Deleted!";
                    this.GetMRNO();
                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = MktData.ErrorObject["Msg"].ToString(); ;
                    return;

                }
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Money Receipt Info";
                    string eventdesc = "Delete MR";
                    string eventdesc2 = mrrno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;

            }

        }

    }
}











