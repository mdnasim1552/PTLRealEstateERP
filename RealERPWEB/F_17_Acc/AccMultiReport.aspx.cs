using System;
using System.Collections;
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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
using System.Collections.Generic;
namespace RealERPWEB.F_17_Acc
{

    public partial class AccMultiReport : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public double balamt = 0.000000;
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            //rpttype = ledger &

            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = " ";
                ((Label)this.Master.FindControl("lblTitle")).Text = "Accounts Reports View/Print Screen";


                this.Master.Page.Title = "Accounts Reports View/Print Screen";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string mRptType = Request.QueryString["rpttype"].ToString().Trim();
                switch (mRptType)
                {
                    case "ledger":
                        this.GetDataForLedger();
                        break;
                    case "voucher":
                        this.GetDataForVoucher();
                        break;
                    case "schedule":
                        this.GetDataForSchedule();
                        break;
                    case "spledger":
                        this.GetDataForSpLedger();
                        break;
                    case "detailsTB":
                        this.GetDataForDetTB();
                        this.PanelProWork.Visible = true;
                        this.chkcost.Visible = true;
                        break;
                    case "AccRec":
                        this.GetDataAccRec();
                        break;
                    case "RPschedule":
                        this.GetRecPaySchedule();
                        break;
                    case "detailsTBRP":
                        this.GetDataForDetTBRP();
                        break;
                    case "PrjReportRP":
                        this.GetDataForPrjReportRP();
                        break;
                    case "IssPay":
                        this.ShowMonIsuPayment();
                        break;
                    case "spledgerprj":
                        this.GetDataforledgerprj();
                        break;
                    case "spledgerDetials":
                        this.GetspledgerDetials();
                        break;


                }



                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Account Report";
                    string eventdesc = "Show Report: " + mRptType;
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
            switch (mRptType)
            {
                case "ledger":
                    this.PrintLedger();
                    break;
                case "voucher":

                    this.PrintVouLedger();

                    break;
                case "schedule":
                    this.PrintConSchedule();
                    break;
                case "spledger":
                    this.PrintLedgerWithQty();
                    break;
                case "detailsTB":
                    this.RptDetSched();
                    break;
                case "AccRec":
                    this.PrintLedger();
                    break;
                case "IssPay":
                    this.RptMonthlyIsuVsPay();
                    break;
                case "detailsTBRP":
                    this.PrintRPL2Value();
                    break;
                case "PrjReportRP":
                    this.PrintRPProCost();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Report";
                string eventdesc = "Print Report: " + mRptType;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3332":
                case "3101":
                    printinstar = "Innstar";
                    break;

                default:

                    break;


            }
            return printinstar;
        }


        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;

                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;

                case "3330":
                    vouprint = "VocherPrint6";
                    break;

                // case "3101":
                case "3332":
                    vouprint = "VocherPrintIns";
                    break;

                case "3101":
                case "3333":
                    vouprint = "VocherPrintMod";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }



        private void PrintVoucher()
        {
            string vounum = this.Request.QueryString["vounum"].ToString();
            //string curvoudat = this.txt.Text.Substring(0, 11);
            //string vounum = this.txtcurrentvou.Text.Trim().Substring(0, 2) + curvoudat.Substring(7, 4) +
            //          this.txtcurrentvou.Text.Trim().Substring(2, 2) + this.txtCurrntlast6.Text.Trim();

            string paytype = "0"; //this.ChboxPayee.Checked ? "0" : "1";


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('AccPrint.aspx?Type=accVou&vounum=" + vounum + "&paytype=" + paytype
                       + "', target='_blank');</script>";
        }

        private void PrintVouLedger()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.PrintVoucher();

            //switch (comcod)
            //{
            //    case "3101":
            //    //case "3336":
            //    case "3337":
            //        //this.PrintchKSuvastu();
            //        break;
            //    default:

            //        break;
            //}


            //else
            //    this.PrintVoucher();

            //try
            //{
            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string comnam = hst["comnam"].ToString();
            //    // (hst["comadd1"].ToString().Replace("<br />", "\n")).ToString();
            //    string comadd = hst["comadd1"].ToString();
            //    string combranch = hst["combranch"].ToString();
            //    string compname = hst["compname"].ToString();
            //    string username = hst["username"].ToString();
            //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //    string vounum = this.Request.QueryString["vounum"].ToString();
            //    string PrintInstar = this.GetCompInstar();
            //    //string Calltype = (this.rbtnList1.SelectedIndex == 5) ? "PRINTDELETEDVOUCHER01" : "PRINTVOUCHER01";
            //    DataSet _ReportDataSet = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "PRINTVOUCHER01", vounum, PrintInstar, "", "", "", "", "", "", "");
            //    if (_ReportDataSet == null)
            //        return;
            //    DataTable dt = _ReportDataSet.Tables[0];

            //    double dramt, cramt;
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
            //    cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));



            //    if (dramt > 0 && cramt > 0)
            //    {
            //        TAmount = cramt;

            //    }
            //    else if (dramt > 0 && cramt <= 0)
            //    {
            //        TAmount = dramt;
            //    }
            //    else
            //    {
            //        TAmount = cramt;
            //    }

            //    DataTable dt1 = _ReportDataSet.Tables[1];
            //    string Vounum = dt1.Rows[0]["vounum"].ToString();
            //    string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            //    string refnum = dt1.Rows[0]["refnum"].ToString();
            //    string payto = dt1.Rows[0]["payto"].ToString();
            //    string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
            //    string voutype = dt1.Rows[0]["voutyp"].ToString();
            //    string venar = dt1.Rows[0]["venar"].ToString();
            //    string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
            //    string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
            //    string receivedBank = dt1.Rows[0]["banknam"].ToString();
            //    string Type = this.CompanyPrintVou();

            //    ReportDocument rptinfo = new ReportDocument();
            //    if (Type == "VocherPrint")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();
            //        //TextObject txtConverssion = rptinfo.ReportDefinition.ReportObjects["txtConverssion"] as TextObject;
            //        //string curocde = _ReportDataSet.Tables[1].Rows[0]["curcode"].ToString();
            //        //double conrate = Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["conrate"]);
            //        //txtConverssion.Text = (curocde == "001") ? "" : _ReportDataSet.Tables[1].Rows[0]["curdesc"].ToString() + " " + (TAmount * conrate).ToString("#,##0.00;(#,##0.00)") + " (" + "1 SGD" + "=" + conrate.ToString("#,##0.00;(#,##0.00)") + " " + _ReportDataSet.Tables[1].Rows[0]["curdesc"].ToString() + ")";
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;
            //    }
            //    else if (Type == "VocherPrint1")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher1();
            //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";

            //        TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //        txtisunum1.Text = "Issue No: " + Isunum;
            //        TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //        txtPosteddate1.Text = "Entry Date: " + Posteddat;
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;


            //    }
            //    else if (Type == "VocherPrint2")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher2();
            //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //        TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //        txtisunum2.Text = "Issue No: " + Isunum;
            //        TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //        txtPosteddate2.Text = "Entry Date: " + Posteddat;
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;
            //    }
            //    else if (Type == "VocherPrint5")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
            //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //        TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //        txtisunum2.Text = "Issue No: " + Isunum;
            //        TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //        txtPosteddate2.Text = "Entry Date: " + Posteddat;
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;
            //    }

            //    else if (Type == "VocherPrint3")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher3();
            //        TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //        txtisunum2.Text = "Issue No: " + Isunum;
            //        TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //        txtPosteddate2.Text = "Entry Date: " + Posteddat;
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto;  //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;
            //    }

            //    else if (Type == "VocherPrint5")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
            //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //        TextObject txtisunum1 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //        txtisunum1.Text = "Issue No: " + Isunum;
            //        TextObject txtPosteddate1 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //        txtPosteddate1.Text = "Entry Date: " + Posteddat;
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;

            //    }
            //    else if (Type == "VocherPrint6")
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucherBridge();
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;



            //    }

            //    else if (Type == "VocherPrintMod")
            //    {
            //        if (ASTUtility.Left(vounum, 2) == "JV")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //        }




            //        else
            //        {
            //            string vouno = vounum.Substring(0, 2);
            //            string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

            //            if (vouno == "BC" || vouno == "CC")
            //            {
            //                rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli02();
            //                TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
            //                txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
            //                TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //                rpttxtPartyName.Text = (payto == "") ? "" : payto;


            //                TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                naration.Text = venar;

            //                TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
            //                txtporrecieved.Text = paytoorecived;

            //                TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
            //                txtusername.Text = username;

            //                TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //                txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

            //                TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                vounum1.Text = vounum;
            //                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                date.Text = voudat;
            //                TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //                Refnum.Text = refnum;
            //                TextObject txtReceivedBank = rptinfo.ReportDefinition.ReportObjects["txtReceivedBank"] as TextObject;
            //                txtReceivedBank.Text = receivedBank;

            //            }

            //            else
            //            {
            //                rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli03();
            //                TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
            //                txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
            //                TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //                rpttxtPartyName.Text = (payto == "") ? "" : payto;


            //                TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //                naration.Text = venar;

            //                TextObject txtporrecieved = rptinfo.ReportDefinition.ReportObjects["txtporrecieved"] as TextObject;
            //                txtporrecieved.Text = paytoorecived;

            //                TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
            //                txtusername.Text = username;

            //                TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //                txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

            //                TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //                vounum1.Text = vounum;
            //                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //                date.Text = voudat;
            //                TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //                Refnum.Text = refnum;

            //            }




            //        }






            //    }

            //    else if (Type == "VocherPrintIns")
            //    {
            //        if (ASTUtility.Left(vounum, 2) == "JV")
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns();

            //            //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //            //txtCompanyName.Text = comnam;
            //            //TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //            //txtcAdd.Text = comadd;
            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = "Voucher No.: " + vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = "Voucher Date: " + voudat;
            //            TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //            Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = "";// (this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //            //TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
            //            //voutype1.Text = voutype;
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = "Narration: " + venar;
            //        }

            //        else
            //        {
            //            rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherIns02();
            //            TextObject txtDesc = rptinfo.ReportDefinition.ReportObjects["txtDesc"] as TextObject;
            //            txtDesc.Text = dt1.Rows[0]["cactdesc"].ToString();// this.ddlConAccHead.SelectedItem.Text.Substring(13).ToString();
            //            TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //            rpttxtPartyName.Text = (payto == "") ? "" : payto;
            //            TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //            naration.Text = venar;

            //            TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
            //            txtusername.Text = username;

            //            TextObject txtProject = rptinfo.ReportDefinition.ReportObjects["txtProject"] as TextObject;
            //            txtProject.Text = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

            //            TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //            vounum1.Text = vounum;
            //            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //            date.Text = voudat;

            //        }



            //    }


            //    else
            //    {
            //        rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher4();
            //        TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
            //        txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
            //        TextObject txtisunum3 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
            //        txtisunum3.Text = "Issue No: " + Isunum;
            //        TextObject txtPosteddate3 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
            //        txtPosteddate3.Text = "Entry Date: " + Posteddat;
            //        TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //        Refnum.Text = "Cheque/Ref. No.: " + refnum;
            //        TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //        rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
            //        TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //        naration.Text = "Narration: " + venar;
            //        TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //        vounum1.Text = "Voucher No.: " + vounum;
            //        TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //        date.Text = "Voucher Date: " + voudat;

            //    }

            //    //ReportDocument rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher();

            //    TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //    txtCompanyName.Text = comnam;
            //    TextObject txtcAdd = rptinfo.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //    txtcAdd.Text = comadd;
            //    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
            //    //vounum1.Text = "Voucher No.: " + vounum;
            //    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //    //date.Text = "Voucher Date: " + voudat;
            //    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
            //    //Refnum.Text = "Cheque/Ref. No.: " + refnum;

            //    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
            //    //rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto;
            //    TextObject voutype1 = rptinfo.ReportDefinition.ReportObjects["voutype"] as TextObject;
            //    voutype1.Text = voutype;
            //    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
            //    // naration.Text = "Narration: " + venar;
            //    TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //    rpttxtamt.Text = ASTUtility.Trans(Math.Round(TAmount), 2);
            //    TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //    if (ConstantInfo.LogStatus == true)
            //    {
            //        string eventtype = "Voucher Print";
            //        string eventdesc = "Print Voucher";
            //        string eventdesc2 = "Voucher No.: " + vounum;
            //        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //    }

            //    rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
            //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //    rptinfo.SetParameterValue("ComLogo", ComLogo);
            //    Session["Report1"] = rptinfo;
            //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //}
            //catch (Exception ex)
            //{
            //    //((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //}


        }

        private void PrintConSchedule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataSet ds2 = (DataSet)Session["tblreportdata"];
            if (ds2 == null)
                return;

            //RDLC Convert By Parbaz
            var list = ds2.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassAccounts.AccControlSchedule01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptAccConSchedule01", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compname", comnam));
            rpt.SetParameters(new ReportParameter("rptTitle", "ACCOUNTS CONTROL SCHEDULE"));
            rpt.SetParameters(new ReportParameter("rptdate", "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + " )"));
            rpt.SetParameters(new ReportParameter("comLogo", comLogo));
            rpt.SetParameters(new ReportParameter("txtuserinfo", "Print Source :" + username + " , " + session + " , " + printdate));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



        }



        private void PrintRPL2Value()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dtds = (DataTable)ViewState["StoreTable"];
            ReportDocument rptDShedule = new RealERPRPT.R_17_Acc.RptAccRPDetailsSchedule();
            TextObject txtCompany = rptDShedule.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtdate = rptDShedule.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = lblDetRP.Text.Trim();

            TextObject rpttxtAccDesc = rptDShedule.ReportDefinition.ReportObjects["txtAccDesc"] as TextObject;
            rpttxtAccDesc.Text = "Account Description: " + dtds.Rows[0]["actdesc4"].ToString(); ;
            TextObject txtuserinfo = rptDShedule.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rptDShedule.SetDataSource(dtds);

            Session["Report1"] = rptDShedule;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintRPProCost()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dtds = (DataTable)ViewState["StoreTable"];
            ReportDocument rptDShedule = new RealERPRPT.R_17_Acc.RptAccRPDetailsSchedule();
            TextObject txtCompany = rptDShedule.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtdate = rptDShedule.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = lblDuType.Text.Trim();

            TextObject rpttxtAccDesc = rptDShedule.ReportDefinition.ReportObjects["txtAccDesc"] as TextObject;
            rpttxtAccDesc.Text = "Account Description: " + dtds.Rows[0]["actdesc4"].ToString(); ;
            TextObject txtuserinfo = rptDShedule.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rptDShedule.SetDataSource(dtds);

            Session["Report1"] = rptDShedule;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptDetSched()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string level2 = this.ddlRptGroup.SelectedValue.ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //DataSet ds1 = (DataSet)ViewState["StoreTable2"];

            DataTable dtds = (DataTable)ViewState["StoreTable"];

            DataView dv = dtds.DefaultView;
            dv.RowFilter = ("rescode not like '%000'");
            DataTable dt = dv.ToTable();


            var list = new List<RealEntity.C_17_Acc.EClassFinanStatement.DetailsScheduleTB>();

            if (level2 == "Sub-1" || level2 == "Sub-2" || level2 == "Sub-3" || level2 == "Main")
            {

                list = dtds.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.DetailsScheduleTB>();

            }
            else
            {
                list = dt.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.DetailsScheduleTB>();
            }

            // rdlc start

            string desc = "Accounts Details Schedule "; //+ dtds.Rows[0]["actdesc4"].ToString();
            string date1 = "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";

            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptDetailSheduleTB", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Account Details Schedule Report - Details"));
            rpt.SetParameters(new ReportParameter("txtDesc", desc));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            // rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            // rdlc end



            //ReportDocument rptDShedule = new RealERPRPT.R_17_Acc.RptDetailSheduleTB();

            //double opamt; double dramt; double cramt; double clsamt;

            ////((Label)this.grvDTB.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
            ////                    0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ////((Label)this.grvDTB.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
            ////                    0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ////((Label)this.grvDTB.FooterRow.FindControl("lblfCramt")).Text = "<br>" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
            ////                    0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ////((Label)this.grvDTB.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closam)", "")) ?
            ////                    0 : dt.Compute("sum(closam)", ""))).ToString("#,##0;(#,##0); ");

            //if (level2 == "Sub-1" || level2 == "Sub-2" || level2 == "Sub-3" || level2 == "Main")
            //{
            //    opamt = Convert.ToDouble((Convert.IsDBNull(dtds.Compute("sum(opnam)", "")) ?
            //                  0 : dtds.Compute("sum(opnam)", "")));
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dtds.Compute("sum(dram)", "")) ?
            //                      0 : dtds.Compute("sum(dram)", "")));
            //    cramt = Convert.ToDouble((Convert.IsDBNull(dtds.Compute("sum(cram)", "")) ?
            //                   0 : dtds.Compute("sum(cram)", "")));
            //    clsamt = Convert.ToDouble((Convert.IsDBNull(dtds.Compute("sum(closam)", "")) ?
            //                       0 : dtds.Compute("sum(closam)", "")));
            //}
            //else
            //{
            //    opamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
            //                 0 : dt.Compute("sum(opnam)", "")));
            //    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
            //                      0 : dt.Compute("sum(dram)", "")));
            //    cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
            //                   0 : dt.Compute("sum(cram)", "")));
            //    clsamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closam)", "")) ?
            //                       0 : dt.Compute("sum(closam)", "")));
            //}

            //TextObject txtCompany = rptDShedule.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtTitle = rptDShedule.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Account Details Schedule Report - Details";


            //TextObject txtdate = rptDShedule.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";

            //TextObject rpttxtAccDesc = rptDShedule.ReportDefinition.ReportObjects["txtAccDesc"] as TextObject;
            //rpttxtAccDesc.Text = "Account Description: " + dtds.Rows[0]["actdesc4"].ToString(); // Problem

            //TextObject txtopamt = rptDShedule.ReportDefinition.ReportObjects["txtopamt"] as TextObject;
            //txtopamt.Text = opamt.ToString("#,##0;(#,##0); ");
            //TextObject txtdramt = rptDShedule.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            //txtdramt.Text = dramt.ToString("#,##0;(#,##0); ");
            //TextObject txtcramt = rptDShedule.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            //txtcramt.Text = cramt.ToString("#,##0;(#,##0); ");
            //TextObject txtclsamt = rptDShedule.ReportDefinition.ReportObjects["txtclsamt"] as TextObject;
            //txtclsamt.Text = clsamt.ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptDShedule.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Account Details Schedule";
            //    string eventdesc = "Print Schedule";
            //    string eventdesc2 = dtds.Rows[0]["actdesc4"].ToString() + " (From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}

            //rptDShedule.SetDataSource(dtds);

            //Session["Report1"] = rptDShedule;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        //private string ComLedger()
        //{

        //    string comcod = this.GetCompCode();
        //    string comledger = "";
        //    switch (comcod)
        //    {
        //        case "3101":
        //        case "3333":
        //            comledger = "LedgerAlli";
        //            break;

        //        case "3330":
        //            comledger = "LedgerBridge";
        //            break;

        //        default:
        //            comledger = "LedgerGen";
        //            break;




        //    }

        //    return comledger;

        //}



        private string ComLedger()
        {

            string comcod = this.GetCompCode();
            string comledger = "";
            switch (comcod)
            {




                case "3337"://Suvastu
                case "3339"://Tropical
                case "3336"://Suvastu
                case "1103"://Tanvir Constructions Ltd.          
                    comledger = "LedgerSuTroaTanvir";
                    break;

                //case "3101":
                //case "3333":
                //    comledger = "LedgerAlli";
                //    break;


                case "3330":
                    comledger = "LedgerBridge";
                    break;

                case "3344":
                    //case "3336":
                    comledger = "LedgerTerranova";
                    break;


                //case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3309":
                case "3310":
                case "3311":
                    comledger = "LedgerRupayan";
                    break;

                default:
                    comledger = "LedgerGen";
                    break;




            }

            return comledger;

        }

        private void PrintLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["StoreTable"];
            ReportDocument rptstk = new ReportDocument();

            //string Headertitle = (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
            //   : (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
            //   : (this.rbtnLedger.SelectedValue.ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";

            string actdesc = "Accounts Head: " + ((Request.QueryString["rpttype"].ToString() == "AccRec") ? Request.QueryString["actdesc"].ToString() :
                this.LblLgLedgerHead.Text);
            string Headertitle = (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "19") ? "Cash/Bank Book" : (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "29") ? "Cash/Bank Book" : "Account Ledger Report";
            //string daterange = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";


            string daterange = (Request.QueryString["rpttype"].ToString() == "AccRec") ? "As on Date: " + Request.QueryString["Date1"].ToString()
                    : "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " +
                        Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";
            string Resdesc = "";


            if (Request.QueryString["rpttype"].ToString() == "SubLedger")
            {
                Resdesc = "Client Name: " + Request.QueryString["resdesc"].ToString();

            }

            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string comledger = this.ComLedger();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();

            if (comledger == "LedgerSuTroaTanvir") 
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            else if (comledger == "LedgerBridge")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerBridge", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            //else if (comledger == "LedgerAlli")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerAlli();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //}
            else if (comledger == "LedgerTerranova")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerTerra", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }

            else if (comledger == "LedgerRupayan")
            {

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerRup", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }


            else
            {
                string checkby = (comcod == "3340") ? "Checked By" : "Recommended By";
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcheckedby", checkby));




            }
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + actdesc));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", daterange));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new ReportDocument();

            //string comledger = this.ComLedger();
            //string actdesc = "Accounts Head: " + ((Request.QueryString["rpttype"].ToString() == "AccRec") ? Request.QueryString["actdesc"].ToString() :
            //    this.LblLgLedgerHead.Text);
            //string date = (Request.QueryString["rpttype"].ToString() == "AccRec") ? "As on Date: " + Request.QueryString["Date1"].ToString()
            //        : "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " +
            //            Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";
            //if (comledger == "LedgerBridge")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerBridge();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //    rpttxtAccDesc.Text = actdesc;
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = date;
            //}

            //else if (comledger == "LedgerAlli")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerAlli();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = actdesc;
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = date;

            //}

            //else
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedger();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = actdesc;
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = date;

            //}



            //string Resdesc = "";
            //if (Request.QueryString["rpttype"].ToString() == "SubLedger")
            //{
            //    Resdesc = "Client Name: " + Request.QueryString["resdesc"].ToString();

            //}
            //DataTable dt = (DataTable)ViewState["StoreTable"];
            //if (dt == null)
            //    return;
            //string Headertitle = (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "19") ? "Cash/Bank Book" : (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "29") ? "Cash/Bank Book" : "Account Ledger Report";


            //TextObject txtHeadertitle = rptstk.ReportDefinition.ReportObjects["txtHeadertitle"] as TextObject;
            //txtHeadertitle.Text = Headertitle;


            ////TextObject txtSubHeadertitle = rptstk.ReportDefinition.ReportObjects["txtSubHeadertitle"] as TextObject;
            ////txtSubHeadertitle.Text = Headertitle;

            //TextObject txtcompanyname = rptstk.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //txtcompanyname.Text = comnam;





            ////TextObject rpttxtActdesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            ////rpttxtActdesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);

            //TextObject txtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            //txtResDesc.Text = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;




            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //rptstk.SetDataSource((DataTable)ViewState["StoreTable"]);
            ////tring comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                      ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";







        }
        private void RptMonthlyIsuVsPay()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptMonthlyIsuVsPay();
            DataTable dt = (DataTable)Session["tblspledger"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = "grp1='AA'";
            dt = dv.ToTable();

            string isuamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ?
                    0 : dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
            string reconamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reconamt)", "")) ?
                    0 : dt.Compute("sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");


            TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
            txtfdate.Text = " (From " + Convert.ToDateTime(Request.QueryString["frmdate"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(Request.QueryString["todate"].ToString()).ToString("dd-MMM-yyyy") + " )";
            TextObject rpttxtSuporConName = rptsl.ReportDefinition.ReportObjects["txtSuporConName"] as TextObject;
            rpttxtSuporConName.Text = (this.Request.QueryString["rescode"].ToString().Substring(0, 2) == "95") ? "Employee's Name" : (this.Request.QueryString["rescode"].ToString().Substring(0, 2) == "99") ? "Supplier's Name" : "Sub-Contractor's Name";

            TextObject txtIsuAmt = rptsl.ReportDefinition.ReportObjects["txtIsuAmt"] as TextObject;
            txtIsuAmt.Text = isuamt;

            TextObject txtRecAmt = rptsl.ReportDefinition.ReportObjects["txtRecAmt"] as TextObject;
            txtRecAmt.Text = reconamt;

            TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            //string comcod = this.GetComeCode();
            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsl.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintLedgerWithQty()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["StoreTable"];
            //  ReportDocument rptstk = new ReportDocument();

            //string Headertitle = (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "19") ? "Cash/Bank Book"
            //   : (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "29") ? "Cash/Bank Book"
            //   : (this.rbtnLedger.SelectedValue.ToString() == "SubLedger") ? "Subsidary Ledger" : "Ledger";
            string actdesc = "";
            if (Request.QueryString["rpttype"].ToString() == "AccRec")
            {
                actdesc = "Accounts Head: " + Request.QueryString["actdesc"].ToString();
            }
            else
            {
                actdesc = this.lblResName.Text;
            }

            string Headertitle = (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "19") ? "Cash/Bank Book" : (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "29") ? "Cash/Bank Book" : "Account Ledger Report";
            //string daterange = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";


            string daterange = (Request.QueryString["rpttype"].ToString() == "AccRec") ? "As on Date: " + Request.QueryString["Date1"].ToString()
                    : "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " +
                        Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";
            string Resdesc = "";


            if (Request.QueryString["rpttype"].ToString() == "SubLedger")
            {
                Resdesc = "Client Name: " + Request.QueryString["resdesc"].ToString();

            }

            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string comledger = this.ComLedger();
            string acchead = this.lblHeaderName.Text.ToString();

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();
            if (comledger == "LedgerSuTroaTanvir")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            else if (comledger == "LedgerBridge")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerBridge", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }

            //else if (comledger == "LedgerAlli")
            //{
            //    rptstk = new RealERPRPT.R_17_Acc.RptAccLedgerAlli();
            //    TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["txtactdesc"] as TextObject;
            //    rpttxtAccDesc.Text = "Accounts Head: " + this.ddlConAccHead.SelectedItem.ToString().Substring(13);
            //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //    txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";

            //}
            else if (comledger == "LedgerTerranova")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerTerra", lst, null, null);
                Rpt1.EnableExternalImages = true;
            }
            else if (comledger == "LedgerRupayan")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedgerRup", lst, null, null);
                Rpt1.EnableExternalImages = true;

            }
            else
            {
                string checkby = (comcod == "3340") ? "Checked By" : "Recommended By";
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccLedger", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("txtcheckedby", checkby));
                Rpt1.SetParameters(new ReportParameter("txtAccHead", acchead));
            }
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", actdesc));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", daterange));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }
        protected void GetDataForSchedule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 0;
            string mACTCODE1 = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();

            //if (mCOMCOD.Length > 3)
            //    mCOMCOD = (mCOMCOD.Substring(4, 3) == "000" ? mCOMCOD.Substring(0, 3) : mCOMCOD.Substring(4, 3));

            string mLEVEL1 = (ASTUtility.Right(mACTCODE1, 10) == "0000000000" ? "2" :
                (ASTUtility.Right(mACTCODE1, 8) == "00000000" ? "3" : "4"));//this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = "TOPHEAD";// : "NOTOPHEAD");

            //AccReports acb1 = new AccReports();
            //DataSet ds1 = accData.GetAccReportForPrint(mCOMCOD, "SP_REPORT_ACC_CSCH_COMPANY", "CSCH_COMPANY_LEVEL_0" +
            //                ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, mACTCODE1, "", "", "", "");

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", "CSCH_REPORT_LEVEL01_0" + ASTUtility.Right(mLEVEL1, 1),
                          mTRNDAT1, mTRNDAT2, mTOPHEAD1, mACTCODE1, "", "", "", "", "");

            //this.LblSchCompName.Text = Convert.ToString(ds1.Tables[0].Rows[0]["comnam"]);
            this.LblSchReportTitle.Text = "ACCOUNTS CONTROL SCHEDULE - " + mLEVEL1;
            this.LblSchReportPeriod.Text = (mTRNDAT1 == mTRNDAT2) ? ("As On Date: " + mTRNDAT1) : "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            if (ds2.Tables[0].Rows.Count > 0)
            {
                Session["tblreportdata"] = ds2;
            }
            this.gvSchedule.DataSource = ds2.Tables[0];


            this.gvSchedule.DataBind();



            ((Label)this.gvSchedule.FooterRow.FindControl("lblfopnDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(opndram)", "")) ?
                              0 : ds2.Tables[0].Compute("sum(opndram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSchedule.FooterRow.FindControl("lblfopnCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(opncram)", "")) ?
                           0 : ds2.Tables[0].Compute("sum(opncram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSchedule.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(dram)", "")) ?
                           0 : ds2.Tables[0].Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvSchedule.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(cram)", "")) ?
                           0 : ds2.Tables[0].Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            double closdramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(closdram)", "")) ? 0 : ds2.Tables[0].Compute("sum(closdram)", "")));
            double closcramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("sum(closcram)", "")) ? 0 : ds2.Tables[0].Compute("sum(closcram)", "")));
            ((Label)this.gvSchedule.FooterRow.FindControl("lblfcloDramt")).Text = closdramt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvSchedule.FooterRow.FindControl("lblfcloCramt")).Text = closcramt.ToString("#,##0;(#,##0); ");
            ((Label)this.gvSchedule.FooterRow.FindControl("lblfcloNetamt")).Text = (closdramt - closcramt).ToString("#,##0;(#,##0); ");
        }

        protected void GetDataForLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 1;
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();

            string qwithoutopn = Request.QueryString["opnoption"] ?? "";
            string withOutOpn = qwithoutopn.Length > 0 ? qwithoutopn : "";
            //   ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", calltype, actcode, date1, date2, "", Narration, "", ltype, withOutOpn, "");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGER", mACTCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", withOutOpn, "");



            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.LblLgReportTitle.Text = Convert.ToString(ds1.Tables[1].Rows[0]["booknam"]).ToUpper();
            this.LblLgLedgerHead.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString(); // mACTCODE;//this.DDListAccHeadList.SelectedItem.Text.Trim();
            this.LblLgReportPeriod.Text = (mTRNDAT1 == mTRNDAT2) ? ("As On Date: " + mTRNDAT1) : "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            DataTable dt = ds1.Tables[0];
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            ViewState["StoreTable"] = dt;
            this.gvLedger.DataSource = dt;
            this.gvLedger.DataBind();


        }
        protected void GetDataForSpLedger()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 3;
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = Request.QueryString["actcode"].ToString();
            string mRESCODE = Request.QueryString["rescode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            //string withOutOpn = "withoutopening";

            string callType = mACTCODE == mRESCODE ? "ACCOUNTSLEDGERSH" : "ACCOUNTSLEDGERSUB"; 
            string withOutOpn = Request.QueryString["opnoption"].ToString();// Request.QueryString["opnoption"].Length > 0 ? Request.QueryString["opnoption"].ToString() : "withoutopening";

            string spclcode = this.Request.QueryString["spclcode"] ?? "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", callType, mACTCODE, mTRNDAT1, mTRNDAT2, mRESCODE, "", "", "", withOutOpn, spclcode);

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.lblHeaderName.Text = "Accounts Head: " + ds1.Tables[1].Rows[0]["actdesc"].ToString();
            this.lblResName.Text = "Subsidary Head: " + ds1.Tables[1].Rows[0]["resdesc"].ToString();
            this.LblLgResRptPeriod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            DataTable dt = ds1.Tables[0];
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            ViewState["StoreTable"] = dt;
            this.dgv2.DataSource = dt;
            this.dgv2.DataBind();

            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;

                string eventdesc2 = this.lblHeaderName.Text + " " + this.lblResName.Text + " " + this.LblLgResRptPeriod.Text;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }

        protected void GetDataforledgerprj()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 10;
            //string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = Request.QueryString["pactcode"].ToString();
            string mRESCODE = Request.QueryString["rescode"].ToString();
            string mTRNDAT1 = Request.QueryString["frmdate"].ToString();
            string mTRNDAT2 = Request.QueryString["todate"].ToString();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "SPLEDGERPROJECT", mRESCODE, mTRNDAT1, mTRNDAT2, "", mACTCODE, "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.lblledgern.Text = "Accounts Head: " + ds1.Tables[0].Rows[0]["actdesc"].ToString();
            this.lblres.Text = "Subsidary Head: " + ds1.Tables[0].Rows[0]["resdesc"].ToString();
            this.lblperiod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            DataTable dt = ds1.Tables[0];
            this.HiddenSameData(dt);
            ViewState["StoreTable"] = dt;
            this.gvSpledgerprj.DataSource = dt;
            this.gvSpledgerprj.DataBind();
        }

        private void GetspledgerDetials()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Request.QueryString["Date1"].ToString();
            string todate = Request.QueryString["Date2"].ToString();
            string resource = Request.QueryString["rescode"].ToString();
            string withOutOpn = Request.QueryString["opnoption"].ToString();
            // string acthead = this.ddlAccHead.SelectedValue.ToString();

            string withOutnarra = "";


            string acthead = "";

            //foreach (ListItem item in ddlAccHead.Items)
            //{

            //    if (item.Selected)
            //    {
            //        acthead += "actcode like '" + item.Value.Substring(0, 2) + "%' or ";
            //    }
            //}

            acthead = acthead.Length > 0 ? "(" + acthead.Substring(0, acthead.Length - 3) + ")" : acthead;
            // srch1 = this.txtSearch1.Text.Trim() + "'" + " and '" + this.txttoSearch1.Text.Trim();

            //string consolidate = ""; // this.Checkdaywise.Checked ? "Consolidate" : "";
            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_BGDANALYSIS_01", "RPT_ACC_RESOURCELG", resource, frmdate, todate, withOutOpn, acthead, withOutnarra, consolidate, "", "");
            string daywise = this.Request.QueryString["daywise"] ?? "";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, withOutOpn, acthead, withOutnarra, daywise, "", "");

            if (ds1 == null)
            {

                this.gvSpledger.DataSource = null;
                this.gvSpledger.DataBind();
                return;
            }


            DataTable dt = HiddenSameDataSp(ds1.Tables[0]);
            DataTable dt1 = BalCalculationSp(dt);
            Session["tblspledger"] = dt1;



            this.gvSpledger.DataSource = dt1;
            this.gvSpledger.DataBind();
            //Session["Report1"] = gvSpledger;
            //if (dt.Rows.Count > 0)
            //    ((HyperLink)this.gvSpledger.HeaderRow.FindControl("hlbtntbCdataExelsp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        protected void GetDataForDetTB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 4;
            string mACTCODE1 = Request.QueryString["actcode"].ToString() + "%";
            //int? i = null;

            // string mRescode = Request.QueryString["sircode"].ToString();
            string Rescode = (Request.QueryString["sircode"] ?? "") + "%";
            //Rescode = Rescode + "%";
            string spcfcod = (Request.QueryString["spcfcod"] ?? "") + "%";
            //spcfcod=spcfcod+"%"
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            string chk = "";
            if (this.chkcost.Checked)
            {
                chk = "withoutcost";
            }
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB2",
                          mTRNDAT1, mTRNDAT2, mRptGroup, mRptGroup, mACTCODE1, chk, Rescode, spcfcod, "");

            this.LblSchReportTitle.Text = "ACCOUNTS DETAILS SCHEDULE - ";
            this.lblRptPeriod.Text = (mTRNDAT1 == mTRNDAT2) ? ("As On Date: " + mTRNDAT1) : "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            if (ds1.Tables[0].Rows.Count == 0)
            {

                this.LblSchReportTitle5.Text = "";// mACTCODE1;// mACTDESC1;
                ViewState["StoreTable"] = null;
                this.grvDTB.DataSource = null;
                this.grvDTB.DataBind();
                return;

            }

            this.LblSchReportTitle5.Text = "Project Name:  " + ds1.Tables[0].Rows[0]["actdesc4"].ToString();// mACTCODE1;// mACTDESC1;

            lblspclecode.Text = ds1.Tables[1].Rows[0]["spclcode"].ToString();
            ViewState["StoreTable"] = ds1.Tables[0];  //this.HiddenSameData(ds1.Tables[0]);
            this.grvDTB_dataBind();


            string events = hst["events"].ToString();
            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Click " + ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc2 = LblSchReportTitle5.Text;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }

        }

        private void grvDTB_dataBind()
        {
            DataTable dt1 = (DataTable)ViewState["StoreTable"];

            DataTable dt2 = dt1.Copy();

            this.grvDTB.DataSource = this.HiddenSameData(dt2);
            this.grvDTB.DataBind();

            //Accounts Head
            this.grvDTB.Columns[1].Visible = this.Request.QueryString["actcode"].Length <= 2 ? true : false;


            DataView dv = dt1.DefaultView;
            dv.RowFilter = ("rescode like '%000'");
            DataTable dt = dv.ToTable();


            ((Label)this.grvDTB.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                                0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                                0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfCramt")).Text = "<br>" + Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                                0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closam)", "")) ?
                                0 : dt.Compute("sum(closam)", ""))).ToString("#,##0.00;(#,##0.00); ");


            Session["Report1"] = grvDTB;
            ((HyperLink)this.grvDTB.HeaderRow.FindControl("hlbtntbCdataExel1")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        protected void GetDataAccRec()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 5;
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = Request.QueryString["actcode"].ToString();
            string mRESCODE = Request.QueryString["rescode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RECEIVIABLE", "ACCOUNTRECLEDGER_SUB", mACTCODE, mTRNDAT1, mRESCODE, "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.lblAccFec.Text = "Project Name: " + Request.QueryString["actdesc"].ToString();
            this.lblAccRecCustomer.Text = "Resource Name: " + Request.QueryString["resdesc"].ToString();
            this.lblAccleb.Text = "As on Date: " + mTRNDAT1;

            DataTable dt = ds1.Tables[0];
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            ViewState["StoreTable"] = dt;
            this.grvAccRecFin.DataSource = dt;
            this.grvAccRecFin.DataBind();
        }
        protected void GetRecPaySchedule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 6;
            string mACTCODE1 = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string Type = Request.QueryString["Type"].ToString();
            string mTOPHEAD1 = "TOPHEAD";// : "NOTOPHEAD");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", "REPORTRECPAYSCHEDULE",
                          mTRNDAT1, mTRNDAT2, mTOPHEAD1, mACTCODE1, "", Type, "", "", "");

            this.lblRDate.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";
            this.lblRecPayCode.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString().Substring(14);// mACTCODE1;// mACTDESC1;

            this.grvRecPay.DataSource = ds1.Tables[0];
            this.grvRecPay.DataBind();

            ((Label)this.grvRecPay.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(recamt)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvRecPay.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(payamt)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(payamt)", ""))).ToString("#,##0;(#,##0); ");

            //if (Request.QueryString["Type"].ToString() == "R")
            //{
            //    this.grvRecPay.Columns[4].Visible = false;
            //}
            //else
            //{
            //    this.grvRecPay.Columns[3].Visible = false;
            //}
        }
        protected void GetDataForDetTBRP()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 7;
            string mACTCODE1 = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string Type = Request.QueryString["Type"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTRPDETAILTB",
                          mTRNDAT1, mTRNDAT2, "12", "12", mACTCODE1, Type, "", "", "");

            ViewState["StoreTable"] = ds1.Tables[0];
            this.lblDetRP.Text = "(From " + mTRNDAT1 + " To " + mTRNDAT2 + ")";
            this.lblActRp.Text = ds1.Tables[0].Rows[0]["actdesc4"].ToString();// mACTCODE1;// mACTDESC1;

            this.grvDetTbRp.DataSource = ds1.Tables[0];
            this.grvDetTbRp.DataBind();

            ((Label)this.grvDetTbRp.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(recamt)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDetTbRp.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(payamt)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(payamt)", ""))).ToString("#,##0;(#,##0); ");

            //if (Request.QueryString["Type"].ToString() == "R")
            //{
            //    this.grvDetTbRp.Columns[6].Visible = false;
            //}
            //else
            //{
            //    this.grvDetTbRp.Columns[5].Visible = false;
            //}

        }
        protected void GetDataForPrjReportRP()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 8;
            string mACTCODE1 = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string Type = Request.QueryString["Type"].ToString();
            string TopHead = "dfdsf";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "PROJECTREPORT_RP",
                          mTRNDAT1, mTRNDAT2, TopHead, mACTCODE1, "", "12", "", "", Type);


            ViewState["StoreTable"] = ds1.Tables[0];
            this.lblDuType.Text = "(From " + mTRNDAT1 + " To " + mTRNDAT2 + ")";
            this.lblActcodePRJ.Text = ds1.Tables[0].Rows[0]["actdesc"].ToString();// mACTCODE1;// mACTDESC1;

            this.grvPrjRptRP.DataSource = ds1.Tables[0];
            this.grvPrjRptRP.DataBind();

            ((Label)this.grvPrjRptRP.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(recamt)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvPrjRptRP.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(payamt)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(payamt)", ""))).ToString("#,##0;(#,##0); ");

            //if (Request.QueryString["Type"].ToString() == "R")
            //{
            //    this.grvPrjRptRP.Columns[7].Visible = false;
            //}
            //else
            //{
            //    this.grvPrjRptRP.Columns[6].Visible = false;
            //}

        }
        private void ShowMonIsuPayment()
        {

            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Request.QueryString["frmdate"].ToString();
            string todate = Request.QueryString["todate"].ToString();
            string Rescode = Request.QueryString["rescode"].ToString();
            this.MultiView1.ActiveViewIndex = 9;
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "MONISSUEPAYMENT", frmdate, todate, Rescode, "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvMonIsuPay.DataSource = null;
                this.gvMonIsuPay.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tblspledger"] = HiddenSameData(ds2.Tables[0]);

            this.lblDate.Text = "(From " + frmdate + " to " + todate + ")";
            this.lblResDesc.Text = ds2.Tables[1].Rows[0]["sirdesc"].ToString();// mACTCODE1;// mACTDESC1;

            this.gvMonIsuPay.Columns[4].HeaderText = (this.Request.QueryString["rescode"].ToString().Substring(0, 2).ToString() == "95") ? "Employee's Name"
                : (this.Request.QueryString["rescode"].ToString().Substring(0, 2).ToString() == "98") ? "Sub-Contractor's Name" : "Supplier's Name";
            //this.gvMonIsuPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvMonIsuPay.DataSource = (DataTable)Session["tblspledger"];
            this.gvMonIsuPay.DataBind();
            this.FooterCal();

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblspledger"];
            if (dt.Rows.Count == 0)
                return;

            DataView dv = dt.DefaultView;
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
            switch (mRptType)
            {

                case "IssPay":
                    DataTable dt1 = dt.Copy();
                    DataView dv1 = dt1.DefaultView;
                    dv1.RowFilter = "grp1='AA'";
                    dt1 = dv1.ToTable();
                    double isuamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(isuamt)", "")) ? 0 : dt1.Compute("sum(isuamt)", "")));
                    ((Label)this.gvMonIsuPay.FooterRow.FindControl("lgvCrAmt")).Text = isuamt.ToString("#,##0;(#,##0); ");
                    double reconamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(reconamt)", "")) ? 0 : dt1.Compute("sum(reconamt)", "")));
                    ((Label)this.gvMonIsuPay.FooterRow.FindControl("lgvFReconAmt")).Text = reconamt.ToString("#,##0;(#,##0); ");
                    break;

            }

        }
        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            int i;
            switch (mRptType)
            {
                case "ledger":
                case "spledger":


                    for (i = 0; i < dt.Rows.Count - 2; i++)
                    {



                        if ((dt.Rows[i]["vounum"]).ToString().Trim() == "TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "BALANCE" || (dt.Rows[i]["vounum"]).ToString().Trim() == "CURRENT DR/CR")
                            continue;
                        if ((dt.Rows[i]["grp"]).ToString().Trim() == "C")
                            break;

                        if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                        {
                            dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                            cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                            balamt = balamt + (dramt - cramt);
                            dt.Rows[i]["balamt"] = balamt;
                        }
                    }
                    return dt;
                    break;



                default:

                    for (i = 0; i < dt.Rows.Count - 2; i++)
                    {
                        if ((dt.Rows[i]["vounum"]).ToString() == "TOTAL")
                            break;

                        if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                        {
                            dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                            cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                            balamt = balamt + (dramt - cramt);
                            dt.Rows[i]["balamt"] = balamt;
                        }
                    }
                    break;
            }





            //string grp=








            return dt;

        }



        private DataTable HiddenSameDataSp(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string vounum = dt1.Rows[0]["vounum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();

            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["grp"].ToString() == grp)
                {
                    grp = dt1.Rows[j]["grp"].ToString();
                    dt1.Rows[j]["grpdesc"] = "";

                }
                if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum"].ToString() == vounum))
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                    dt1.Rows[j]["vounum"] = "";

                }

                else
                {

                    if (dt1.Rows[j]["actcode"].ToString() == actcode)
                    {

                        dt1.Rows[j]["actdesc"] = "";
                    }

                    if (dt1.Rows[j]["vounum"].ToString() == vounum)
                    {

                        dt1.Rows[j]["vounum"] = "";

                    }
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    vounum = dt1.Rows[j]["vounum"].ToString();
                    grp = dt1.Rows[j]["grp"].ToString();
                }

            }



            return dt1;

        }

        private DataTable BalCalculationSp(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double opnam, dramt, cramt, bbalamt = 0.00;

            string daywise = this.Request.QueryString["daywise"] ?? "";
            bool result = daywise.Length > 0;// this.Checkdaywise.Checked;
            switch (result)
            {
                case true:

                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if ((dr1["vounum"]).ToString().Trim() == "Total:" || (dr1["vounum"]).ToString().Trim() == "Balance:")
                            continue;
                        opnam = Convert.ToDouble(dr1["opam"]);
                        dramt = Convert.ToDouble(dr1["dram"]);
                        cramt = Convert.ToDouble(dr1["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dr1["clsam"] = bbalamt;
                    }


                    break;


                default:
                    string actcode = dt.Rows[0]["actcode"].ToString();
                    //string grp=
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        if ((dt.Rows[i]["actcode"]).ToString().Trim() != actcode)
                        {
                            bbalamt = 0.00;
                        }
                        actcode = dt.Rows[i]["actcode"].ToString();

                        if ((dt.Rows[i]["vounum"]).ToString().Trim() == "SUB TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "Balance:")
                            continue;



                        //if (((dt.Rows[i]["actcode"]).ToString().Trim()).Length == 12)
                        //{
                        opnam = Convert.ToDouble(dt.Rows[i]["opam"]);
                        dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                        cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dt.Rows[i]["clsam"] = bbalamt;
                        //}


                    }

                    break;



            }


            return dt;

        }

        private void HiddenSameDate(DataTable dt1)
        {


            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
            switch (mRptType)
            {

                case "ledger":
                case "detailsTB":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                        {

                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                            //dt1.Rows[j]["refnum"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";

                        }
                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                        }

                        grp = dt1.Rows[j]["grp"].ToString();
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }
                    break;


                case "spledger":
                    int i = 0;
                    string actcode = dt1.Rows[0]["actcode"].ToString();
                    grp = dt1.Rows[0]["grp"].ToString();
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {
                            actcode = dr1["actcode"].ToString();
                            vounum = dr1["vounum"].ToString();
                            grp = dr1["grp"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["grp"].ToString() == grp)
                        {
                            dr1["grpdesc"] = "";
                        }
                        if ((dr1["actcode"].ToString() == actcode) && (dr1["vounum"].ToString() == vounum))
                        {

                            dr1["actdesc"] = "";
                            dr1["vounum"] = "";
                            dr1["voudat1"] = "";

                        }

                        else
                        {

                            if (dr1["actcode"].ToString() == actcode)
                            {

                                dr1["actdesc"] = "";
                            }

                            if (dr1["vounum"].ToString() == vounum)
                            {

                                dr1["vounum"] = "";
                                dr1["voudat1"] = "";


                            }

                        }
                        actcode = dr1["actcode"].ToString();
                        vounum = dr1["vounum"].ToString();
                        grp = dr1["grp"].ToString();
                    }

                    break;



                case "AccRec":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {

                        if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                        {

                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                            //dt1.Rows[j]["refnum"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";

                        }
                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                        }

                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }
                    break;
            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
            int i = 0;
            switch (mRptType)
            {

                case "IssPay":

                    string pactcode1 = dt1.Rows[0]["actcode"].ToString();
                    string rescode = dt1.Rows[0]["rescode"].ToString();


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rescode"].ToString() == rescode)
                            dt1.Rows[j]["resdesc"] = "";
                        if (dt1.Rows[j]["actcode"].ToString() == pactcode1)
                            dt1.Rows[j]["actdesc"] = "";
                        rescode = dt1.Rows[j]["rescode"].ToString();
                        pactcode1 = dt1.Rows[j]["actcode"].ToString();
                    }
                    break;
                case "spledgerprj":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            dt1.Rows[j]["actdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();
                        actcode = dt1.Rows[j]["actcode"].ToString();
                    }
                    break;



                case "spledgerDetials":

                    string vounum = "";
                    actcode = dt1.Rows[0]["actcode"].ToString();
                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }
                        if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum"].ToString() == vounum))
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["vounum"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            {

                                dt1.Rows[j]["actdesc"] = "";
                            }

                            if (dt1.Rows[j]["vounum"].ToString() == vounum)
                            {

                                dt1.Rows[j]["vounum"] = "";

                            }
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            grp = dt1.Rows[j]["grp"].ToString();
                        }

                    }
                    break;



                case "detailsTB":
                    actcode = dt1.Rows[0]["actcode4"].ToString();


                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            actcode = dr1["actcode4"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["actcode4"].ToString() == actcode)
                        {

                            dr1["actdesc4"] = "";

                        }


                        actcode = dr1["actcode4"].ToString();
                    }


                    break;




            }
            return dt1;

        }

        protected void GetDataForVoucher()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 2;
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mVOUNUM = Request.QueryString["vounum"].ToString();
            string mVOUDAT = Request.QueryString["Date1"].ToString();
            string mVTCODE = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", mVOUNUM, mVOUDAT, mVTCODE, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblvou"] = ds1.Tables[0];

            this.LblVUSrinfo1.Visible = (Convert.ToString(ds1.Tables[1].Rows[0]["srinfo"]).Trim().Length > 0);


            this.LblVUVouTitle.Text = Convert.ToString(ds1.Tables[1].Rows[0]["voutyp"]);
            this.LblVUControlDesc.Text = Convert.ToString(ds1.Tables[1].Rows[0]["cactdesc"]);
            this.LblVUControlCode.Text = Convert.ToString(ds1.Tables[1].Rows[0]["cactcode"]);
            this.LblVURefNo.Text = Convert.ToString(ds1.Tables[1].Rows[0]["refnum"]);
            this.LblVUVouDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["voudat"]).ToString("dd-MMM-yyyy ");
            this.LblVUVouNum.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            this.LblVUSrinfo.Text = Convert.ToString(ds1.Tables[1].Rows[0]["srinfo"]).Trim();
            this.LblVUNarration.Text = (ds1.Tables[1].Rows[0]["venar"]).ToString();

            this.gvVoucher.DataSource = ds1.Tables[0];
            double tdram = Convert.ToDouble(ds1.Tables[1].Rows[0]["tdram"]);
            double tcram = Convert.ToDouble(ds1.Tables[1].Rows[0]["tcram"]);
            this.gvVoucher.Columns[8].FooterText = tdram.ToString("#,##0.00;(#,##0.00); ");
            this.gvVoucher.Columns[9].FooterText = tcram.ToString("#,##0.00;(#,##0.00); ");

            this.gvVoucher.DataBind();
            this.LblVUInWord.Text = "Inword: " + ASTUtility.Trans((Convert.ToDouble(ds1.Tables[1].Rows[0]["tdram"]) > 0 ?
                Convert.ToDouble(ds1.Tables[1].Rows[0]["tdram"]) : Convert.ToDouble(ds1.Tables[1].Rows[0]["tcram"])), 2);
        }

        protected void gvSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = ((Label)e.Row.FindControl("lblgvCode")).Text;
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string mACTDESC = ((Label)e.Row.FindControl("gvDesc")).Text;

            string opnoption = "";
            if (ASTUtility.Right(mACTCODE, 4) == "0000")
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            else
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&opnoption=" + opnoption; ;
        }
        protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mVOUNUM = hlink1.Text;
            string mMEMO = (mVOUNUM.Contains("M") ? "MEMO" : "GACC");
            string mTRNDAT1 = ((Label)e.Row.FindControl("lgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                if (mVOUNUM.Contains("M"))
                    hlink1.Text = mVOUNUM.Substring(0, 3) + mVOUNUM.Substring(8, 2) + "-" + mVOUNUM.Substring(10, 4);
                else
                    hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);

            }
        }

        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14 && ASTUtility.Left(mVOUNUM.Trim(), 2) != "PV")
            {
                hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + mVOUNUM;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }
        protected void grvDTB_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            Label opnam = (Label)e.Row.FindControl("lblgvopenamt");
            Label DAmount = (Label)e.Row.FindControl("lblDramt");
            Label CAmount = (Label)e.Row.FindControl("lblgvCramt");
            Label Clsam = (Label)e.Row.FindControl("lblClosingamt");
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = ((Label)e.Row.FindControl("lblgvAccode")).Text;
            string mRESCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string mACTDESC = ((Label)e.Row.FindControl("gvAcDesc")).Text;
            string mREESDESC = ((Label)e.Row.FindControl("gvResDesc")).Text;

            string opnoption = "";

            string mACTHead = mACTCODE.Substring(0, 1);
            switch (mACTHead)
            {
                case "1":
                case "2":

                    break;

                default:
                    opnoption = "withoutopening";
                    break;

            }


            string spclecode = this.lblspclecode.Text.Trim();
            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE + "&spclcode=" + spclecode +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=";

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode4")).ToString();

            if (code == "")
            {
                return;
            }


            else if (((ASTUtility.Right((code), 3) == "000")))
            {
                hlink1.Font.Bold = true;
                //opnam.Font.Bold = true;
                //DAmount.Font.Bold = true;

                //CAmount.Font.Bold = true;
                //Clsam.Font.Bold = true;


                hlink1.Attributes["style"] = "color:maroon;";
                opnam.Attributes["style"] = "color:maroon; font-weight:bold;";
                DAmount.Attributes["style"] = "color:maroon; font-weight:bold;";
                CAmount.Attributes["style"] = "color:maroon; font-weight:bold;";
                Clsam.Attributes["style"] = "color:maroon; font-weight:bold;";

            }

        }
        protected void grvAccRecFin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }
        protected void grvRecPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = ((Label)e.Row.FindControl("lblgvCode")).Text;
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string mACTDESC = ((Label)e.Row.FindControl("gvDesc")).Text;

            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                        "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
        }
        protected void grvDetTbRp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = Request.QueryString["comcod"].ToString();
            string mACTCODE = ((Label)e.Row.FindControl("lblgvAccode")).Text;
            string mRESCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();
            string mACTDESC = ((Label)e.Row.FindControl("gvAcDesc")).Text;
            string mREESDESC = ((Label)e.Row.FindControl("gvResDesc")).Text;

            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&resdesc=" + mREESDESC + "&opnoption=" + "";
        }
        protected void grvPrjRptRP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcode1")).Text;
            string mACTDESC = this.lblActcodePRJ.Text;
            string mRESCODE = ((Label)e.Row.FindControl("lblgvSubcode1")).Text;
            string mREESDESC = ((Label)e.Row.FindControl("gvlblDesc")).Text.Trim();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString().Trim();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString().Trim();

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode1")).ToString().Trim();


            if (code == "")
            {
                return;
            }

            hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&resdesc=" + mREESDESC;
        }


        protected void gvMonIsuPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvAccDesc");
                Label isuamt = (Label)e.Row.FindControl("lgvcramt");
                Label clramt = (Label)e.Row.FindControl("lgvreconamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AB")
                {
                    udesc.Font.Bold = true;
                    isuamt.Font.Bold = true;
                    clramt.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");
                }
            }
        }

        protected void gvSpledgerprj_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string voucher = ((HyperLink)e.Row.FindControl("HLgvvounum")).Text.ToString();
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");

                hlink.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
            }


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GetDataForDetTB();
        }
        protected void gvSpledger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum01");
                Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount01");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount01");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount01");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount01");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB")
                {
                    hlink.Font.Bold = true;
                    //OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvvounum01");
            string voucher = ((HyperLink)e.Row.FindControl("HLgvvounum01")).Text.ToString();
            if (voucher.Trim().Length == 14)
            {
                if (ASTUtility.Left(voucher, 2) == "PV" || ASTUtility.Left(voucher, 2) == "DV")
                {
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
                else
                {
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
            }

        }

    }
}
