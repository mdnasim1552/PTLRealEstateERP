﻿using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_17_Acc
{
    public partial class PrintMoneyReceipt : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = "";
            if (this.Request.QueryString["Type"].ToString() != "")
            {
                Type = this.Request.QueryString["Type"].ToString();
            }
            switch (Type)
            {
                case "moneyReceipt":
                    this.PrintMoneyReceiptAll();
                    break;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

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
                case "3101":
                    mrprint = "MRPrintWecon";
                    break;

                case "3352":
                    //case "3101":
                    mrprint = "MRPrint360";
                    break;

                case "3356":
               // case "3101":
                    mrprint = "MRPrintIntech";
                    break;

                default:
                    mrprint = "MRPrint";
                    break;
            }
            return mrprint;
        }
        private void PrintMoneyReceiptAll()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string usirCode = this.Request.QueryString["usircode"].ToString();
            string pactCode = this.Request.QueryString["pactcode"].ToString();
            string mrno = this.Request.QueryString["mrno"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mrDate = Convert.ToDateTime(this.Request.QueryString["mrdate"].ToString()).ToString("dd-MMM-yyyy");

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSTALLMANTWITHMRR", pactCode, usirCode, mrDate, "", "", "", "", "", "");
            DataTable dtstatus = ds2.Tables[0];
            DataView dv1 = dtstatus.DefaultView;
            dv1.RowFilter = "mrno='" + mrno + "'";
            DataTable dtmr = dv1.ToTable();
            string Installment = "";
            string Installment2 = "";
            bool isMoneyRecpt=false;
            bool isPartial = false;
            for (int i = 0; i < dtmr.Rows.Count; i++)
            {
                if (i == 0)
                {
                    if (Convert.ToDouble(dtmr.Rows[i]["schamt"].ToString()) == Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()))
                    {
                        Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
                    }

                    else if (Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()) < 0)
                    {
                        Installment = Installment + "REFUNDABLE COLLECTION, ";
                    }

                    else
                    {
                        Installment = Installment + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                    }
                        

                }
                else if (dtmr.Rows[i - 1]["gdesc"].ToString().Trim() != dtmr.Rows[i]["gdesc"].ToString().Trim())
                {
                    if (Convert.ToDouble(dtmr.Rows[i]["schamt"].ToString()) == Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()))
                    {
                        Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
                        isMoneyRecpt = true;
                    }

                    else if (Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()) < 0)
                    {
                        Installment = Installment + "REFUNDABLE COLLECTION, ";
                    }
                    else
                    {
                        Installment = Installment + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                        isPartial = true;
                    }                        

                }

            }
            int len = Installment.Length;
            Installment2 = (len == 0) ? "" : ASTUtility.Left(Installment, len - 2);

            switch (comcod)
            {
                case "3101":
                case "3356":
                case "3325":
                case "2325":
                    if (isMoneyRecpt)
                    {
                        string part1 = ASTUtility.Left(Installment2, 4);
                        string part2 = isPartial==true ? ASTUtility.Right(Installment2, 25) : ASTUtility.Right(Installment2, 16);
                        Installment = part1 + " - " + part2;
                    }
                    else
                    {
                        Installment = Installment2;
                    }

                    break;
                default:
                    Installment = Installment2;
                    break;
            }

            DataSet ds4 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTMONEYRECEIPT", pactCode, usirCode, mrno, "", "", "", "", "", "");
            if (ds4 == null)
                return;
            DataTable dtrpt = ds4.Tables[0];
            string custadd = dtrpt.Rows[0]["custadd"].ToString();
            string custmob = dtrpt.Rows[0]["custmob"].ToString();
            string udesc = dtrpt.Rows[0]["udesc"].ToString();
            string usize = Convert.ToDouble(dtrpt.Rows[0]["usize"]).ToString("#,##0;(#,##0); -");
            string munit = dtrpt.Rows[0]["munit"].ToString();
            string paytype = dtrpt.Rows[0]["paytype"].ToString();
            string chqno = dtrpt.Rows[0]["chqno"].ToString();
            string bankname = dtrpt.Rows[0]["bankname"].ToString();
            string branch = dtrpt.Rows[0]["bbranch"].ToString();
            string paidamt = Convert.ToDouble((Convert.IsDBNull(dtrpt.Compute("Sum(paidamt)", "")) ? 0.00 : dtrpt.Compute("Sum(paidamt)", ""))).ToString("#,##0;-#,##0; ");
            string refno = dtrpt.Rows[0]["refno"].ToString();
            string bookno = dtrpt.Rows[0]["bookno"].ToString();
            string custteam = dtrpt.Rows[0]["custteam"].ToString();
            string rmrks = dtrpt.Rows[0]["rmrks"].ToString();
            string rectype = dtrpt.Rows[0]["rectype"].ToString();
            string rectcode = dtrpt.Rows[0]["rectcode"].ToString();


            double amt1 = Convert.ToDouble((Convert.IsDBNull(dtrpt.Compute("Sum(paidamt)", "")) ? 0.00 : dtrpt.Compute("Sum(paidamt)", "")));
            string amt1t = ASTUtility.Trans(amt1, 2);
            string Typedes = "";
            if (paytype == "CHEQUE")
            {
                Typedes = paytype + ", " + "No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;

            }
            else if (paytype == "P.O")
            {
                Typedes = paytype + ", " + "No: " + chqno + ", Bank: " + bankname + ", Branch: " + branch;

            }
            else
            {

                Typedes = paytype;
            }

            string Type = this.CompanyPrintMR();
            LocalReport Rpt1 = new LocalReport();
            if (Type == "MRPrint1")
            {
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt1", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CompAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("CompAdd1", comadd));
                Rpt1.SetParameters(new ReportParameter("txtmontype1", (rectcode == "54097") ? rectype : (rectcode == "54099") ? rectype : "MONEY RECEIPT"));
                Rpt1.SetParameters(new ReportParameter("txtmontype2", (rectcode == "54097") ? rectype : (rectcode == "54099") ? rectype : "MONEY RECEIPT"));
                Rpt1.SetParameters(new ReportParameter("txtptintable", "Orginal"));
                Rpt1.SetParameters(new ReportParameter("txtptintable1", "Orginal"));
                Rpt1.SetParameters(new ReportParameter("txtrecno1", (rectcode == "54097") ? "Refund No" : (rectcode == "54099") ? "Adjustment No" : "Receipt No"));
                Rpt1.SetParameters(new ReportParameter("txtrecno2", (rectcode == "54097") ? "Refund No" : (rectcode == "54099") ? "Adjustment No" : "Receipt No"));
                Rpt1.SetParameters(new ReportParameter("txtamttitle1", (rectcode == "54097") ? "Being the amount Refunded" : (rectcode == "54099") ? "Being the amount Adjusted" : "Received with thanks a sum of"));
                Rpt1.SetParameters(new ReportParameter("txtamttitle2", (rectcode == "54097") ? "Being the amount Refunded" : (rectcode == "54099") ? "Being the amount Adjusted" : "Received with thanks a sum of"));
                Rpt1.SetParameters(new ReportParameter("txtpayorroradajnst1", (rectcode == "54097") ? "Refund Against" : (rectcode == "54099") ? "Adjusted Against" : "Payment Received Against"));
                Rpt1.SetParameters(new ReportParameter("txtpayorroradajnst2", (rectcode == "54097") ? "Refund Against" : (rectcode == "54099") ? "Adjusted Against" : "Payment Received Against"));
                Rpt1.SetParameters(new ReportParameter("takainword", "Inwords: " + amt1t));
                Rpt1.SetParameters(new ReportParameter("takainword1", "Inwords: " + amt1t));
                Rpt1.SetParameters(new ReportParameter("txtsignature", (rectcode == "54097") ? "Client Signature" : (rectcode == "54099") ? "Client Signature" : "Prepared By"));
                Rpt1.SetParameters(new ReportParameter("txtnote1", (rectcode == "54097") ? "" : (rectcode == "54099") ? "" : "Note: This Money Receipt will be valid Subject to Encashment of the Cheque/DD/Advice/Pay Order"));
                Rpt1.SetParameters(new ReportParameter("txtnote2", (rectcode == "54097") ? "" : (rectcode == "54099") ? "" : "Note: This Money Receipt will be valid Subject to Encashment of the Cheque/DD/Advice/Pay Order"));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " /-  "));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " /-  "));
                Rpt1.SetParameters(new ReportParameter("paytype", paytype));
                Rpt1.SetParameters(new ReportParameter("paytype1", paytype));
                Rpt1.SetParameters(new ReportParameter("paydesc", (rectcode == "54097") ? rmrks : (rectcode == "54099") ? rmrks : (rectcode == "54009") ? rectype : Installment));
                Rpt1.SetParameters(new ReportParameter("paydesc1", (rectcode == "54097") ? rmrks : (rectcode == "54099") ? rmrks : (rectcode == "54009") ? rectype : Installment));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            }

            else if (Type == "MRPrint2")
            {
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptLeisure", list, null, null);

                amt1t = amt1t.Replace("Only", "");

                amt1t = amt1t.Replace("Taka", "");

                Rpt1.SetParameters(new ReportParameter("usize", udesc));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc));
                Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd)));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd)));
                Rpt1.SetParameters(new ReportParameter("takainword", "BDT. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " " + amt1t + " " + "Only " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", "BDT. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0);") + " " + amt1t + " " + "Only " + "AS " + ((Installment == "") ? rectype : Installment)));

                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }

            else if (Type == "MRPrint3")
            {

                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptEdison", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }


            else if (Type == "MRPrint4")
            {

                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptSuvastu", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("txtAddress", comadd));
                Rpt1.SetParameters(new ReportParameter("txtAddress1", comadd));
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }

            else if (Type == "MRPrint5")
            {

                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptTro", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("bookno", bookno));
                Rpt1.SetParameters(new ReportParameter("bookno1", bookno));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }

            else if (Type == "MRPrintWecon")
            {

                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptWecon", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }

            else if (Type == "MRPrint360")
            {
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt360", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }

            else if (Type == "MRPrintIntech")
            {
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptIntech", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t.Replace("Taka", "").Replace("Only", "Taka Only") + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t.Replace("Taka", "").Replace("Only", "Taka Only") + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }

            else
            {

                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceipt", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                Rpt1.SetParameters(new ReportParameter("usize", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt1t + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.Cominformation()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
        }
    }
}