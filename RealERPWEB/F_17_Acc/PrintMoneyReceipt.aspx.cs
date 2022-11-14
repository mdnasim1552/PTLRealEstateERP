using Microsoft.Reporting.WinForms;
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
                    // case "3101":
                    mrprint = "MRPrint5";
                    break;

                case "3351":
                    // case "3101":
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
                case "3101":
                case "3370":
                    // case "3101":
                    mrprint = "MRPrintCPDL";
                    break;

                //Finlay
                case "3368":
                    mrprint = "MRPrintFinlay";
                    break;
                
                case "1206":
                case "1207":
                case "3338":
                    mrprint = "MRPrintAcme";
                    break;


                default:
                    mrprint = "MRPrint";
                    break;
            }
            return mrprint;
        }

        private string GetComInsrecType()
        {
            string irectype = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3368": //Finlay
                    irectype = "rectypewise";
                    break;

                default:
                    irectype = "";
                    break;
            }
            return irectype;

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
            string curDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string usirCode = this.Request.QueryString["usircode"].ToString();
            string pactCode = this.Request.QueryString["pactcode"].ToString();
            string mrno = this.Request.QueryString["mrno"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mrDate = Convert.ToDateTime(this.Request.QueryString["mrdate"].ToString()).ToString("dd-MMM-yyyy");
            //string PrintOpt=this.Request.QueryString["PrintOpt"].ToString() ?? "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSTALLMANTWITHMRR", pactCode, usirCode, mrDate, "", "", "", "", "", "");
            DataTable dtstatus = ds2.Tables[0];
            DataView dv1 = dtstatus.DefaultView;
            dv1.RowFilter = "mrno='" + mrno + "'";
            DataTable dtmr = dv1.ToTable();
            string Installment = "";
            string Installment2 = "";
            bool isMoneyRecpt = false;
            bool isPartial = false;

            string insrectype = GetComInsrecType();
            string instpart2 = "";

            switch (comcod)
            {
                case "3101":
                case "1205":
                case "3351":
                case "3352":
                case "3356":

                    for (int i = 0; i < dtmr.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (Convert.ToDouble(dtmr.Rows[i]["schamt"].ToString()) == Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()))
                            {
                                if ((ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81001") || (ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81002"))
                                {
                                    instpart2 = instpart2 + dtmr.Rows[i]["gdesc"] + ", ";
                                }
                                else
                                {
                                    Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
                                }

                            }

                            else if (Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()) < 0)
                            {
                                if ((ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81001") || (ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81002"))
                                {
                                    instpart2 = instpart2 + "REFUNDABLE COLLECTION, ";
                                }
                                else
                                {
                                    Installment = Installment + "REFUNDABLE COLLECTION, ";
                                }

                            }

                            else
                            {
                                if ((ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81001") || (ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81002"))
                                {
                                    instpart2 = instpart2 + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                                }
                                else
                                {
                                    Installment = Installment + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                                }

                            }


                        }

                        else if (dtmr.Rows[i - 1]["gdesc"].ToString().Trim() != dtmr.Rows[i]["gdesc"].ToString().Trim())
                        {
                            if (Convert.ToDouble(dtmr.Rows[i]["schamt"].ToString()) == Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()))
                            {
                                if ((ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81001") || (ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81002"))
                                {
                                    instpart2 = instpart2 + dtmr.Rows[i]["gdesc"] + ", ";
                                }
                                else
                                {
                                    Installment = Installment + dtmr.Rows[i]["gdesc"] + ", ";
                                    isMoneyRecpt = true;
                                }

                            }

                            else if (Convert.ToDouble(dtmr.Rows[i]["paidamt"].ToString()) < 0)
                            {
                                if ((ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81001") || (ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81002"))
                                {
                                    instpart2 = instpart2 + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                                }
                                else
                                {
                                    Installment = Installment + "REFUNDABLE COLLECTION, ";

                                }
                            }
                            else
                            {

                                if ((ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81001") || (ASTUtility.Left(dtmr.Rows[i]["gcod"].ToString(), 5) == "81002"))
                                {
                                    instpart2 = instpart2 + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                                }
                                else
                                {
                                    Installment = Installment + dtmr.Rows[i]["gdesc"] + " (Partly), ";
                                    isPartial = true;
                                }


                            }

                        }

                    }
                    break;



                case "3368":

                    //MR (1 Row Existed), MR(More than one row existed)
                    string prectype = "";
                    int count = dtmr.Rows.Count;
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

                            //If DataRow is 1, then insert  installment in  insdesc 

                            if (dtmr.Rows.Count == 1)
                                dtmr.Rows[i]["insdesc"] = (Installment.Length == 0) ? "" : ASTUtility.Left(Installment, Installment.Length - 2);


                        }

                        else if (prectype == dtmr.Rows[i]["rectype"].ToString().Trim())
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
                        else
                        {
                            Installment = (Installment.Length == 0) ? "" : ASTUtility.Left(Installment, Installment.Length - 2);

                            //Full Schedule
                            if (isMoneyRecpt)
                            {
                                string part1 = ASTUtility.Left(Installment, 4);
                                string part2 = isPartial == true ? ASTUtility.Right(Installment, 25) : ASTUtility.Right(Installment, 16);
                                Installment = part1 + " - " + part2;
                            }

                            dtmr.Rows[i - 1]["insdesc"] = Installment;
                            Installment = "";
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

                        //if Row More than 1 and then insert  installment in  insdesc 

                        if (i != 0 && i == count - 1)
                        {

                            //Full Schedule
                            if (Installment.Contains(","))
                            {
                                Installment = (Installment.Length == 0) ? "" : ASTUtility.Left(Installment, Installment.Length - 2);
                            }

                            if (isMoneyRecpt)
                            {
                                string part1 = ASTUtility.Left(Installment, 4);
                                string part2 = isPartial == true ? ASTUtility.Right(Installment, 25) : ASTUtility.Right(Installment, 16);
                                Installment = part1 + " - " + part2;
                            }
                            dtmr.Rows[i]["insdesc"] = Installment;
                        }


                        prectype = dtmr.Rows[i]["rectype"].ToString().Trim();

                    }
                    break;
                
                
                default:
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

                    break;
            }

            //. DataTable dt = dtmr;


            int len = Installment.Length;
            Installment = (len == 0) ? "" : ASTUtility.Left(Installment, len - 2);

            Installment2 = (len == 0) ? "" : ASTUtility.Left(Installment, len - 2);
            if (IsAutoLengthDesc())
            {
                if (isMoneyRecpt)
                {
                    string part1 = ASTUtility.Left(Installment2, 4);
                    string part2 = isPartial == true ? ASTUtility.Right(Installment2, 25) : ASTUtility.Right(Installment2, 16);
                    Installment = instpart2 + part1 + " - " + part2;
                }
                else
                {
                    Installment = Installment2;
                }
                //  Installment = (Installment.Length == 0) ? "" : ASTUtility.Left(Installment, Installment.Length - 2);

            }

            //Installment2 = (len == 0) ? "" : ASTUtility.Left(Installment, len - 2);
            //switch (comcod)
            //{
            //    case "3101":
            //    case "1205":
            //    case "3351":
            //    case "3352":
            //    case "3356":
            //    case "3325":
            //    case "2325":
            //        if (isMoneyRecpt)
            //        {
            //            string part1 = ASTUtility.Left(Installment2, 4);
            //            string part2 = isPartial == true ? ASTUtility.Right(Installment2, 25) : ASTUtility.Right(Installment2, 16);
            //            Installment = part1 + " - " + part2;
            //        }
            //        else
            //        {
            //            Installment = Installment2;
            //        }

            //        break;

            //    default:
            //        Installment = Installment2;
            //        break;
            //}
            //Installment = (Installment.Length == 0) ? "" : ASTUtility.Left(Installment, Installment.Length - 2);


            DataSet ds4 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPORTMONEYRECEIPT", pactCode, usirCode, mrno, "", "", "", "", "", "");
            if (ds4 == null || ds4.Tables[0].Rows.Count == 0)
                return;

            if (insrectype.Length > 0)
            {
                foreach (DataRow dr in ds4.Tables[0].Rows)
                {
                    string mrectpe = dr["rectcode"].ToString();
                    DataView dv = dtmr.DefaultView;
                    dv.RowFilter = ("rectype='" + mrectpe + "'");
                    DataTable dt1 = dv.ToTable();
                    int lrow = dt1.Rows.Count;
                    dr["insdesc"] = dt1.Rows[lrow - 1]["insdesc"].ToString();

                }
            }




            //islandowner
            DataTable dtrpt = ds4.Tables[0];
            string custname = dtrpt.Rows[0]["custname"].ToString();
            string custadd = dtrpt.Rows[0]["custadd"].ToString();
            string custmob = dtrpt.Rows[0]["custmob"].ToString();
            string udesc = dtrpt.Rows[0]["udesc"].ToString();
            string project = dtrpt.Rows[0]["islandowner"].ToString() == "True" ? dtrpt.Rows[0]["pactdesc"].ToString() + " (L/O Part)" : dtrpt.Rows[0]["pactdesc"].ToString();
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
            string parking = dtrpt.Rows[0]["parking"].ToString();
            string benefname = dtrpt.Rows[0]["benefname"].ToString().Length == 0 ? "" : ("Beneficiary:  " + dtrpt.Rows[0]["benefname"].ToString());

           

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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("Parking", parking));

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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
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

            else if (Type == "MRPrintCPDL")
            {
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                string currentdate = DateTime.Now.ToString("dd-MMM-yyyy");
                string vounum = dtrpt.Rows[0]["vounum"].ToString();
                if (vounum == "00000000000000")
                {
                    //Title=Acknowledgement Slip

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptAcknowledgementSlipCPDL", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                    Rpt1.SetParameters(new ReportParameter("Title", "PAYMENT ACKNOWLEDGEMENT "));
                    Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                    Rpt1.SetParameters(new ReportParameter("currentdate", currentdate));
                    Rpt1.SetParameters(new ReportParameter("CompAdd", comadd));
                    Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                    Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                    Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                    Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                    Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                    Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                    Rpt1.SetParameters(new ReportParameter("aptno", udesc));
                    Rpt1.SetParameters(new ReportParameter("usize", usize));
                    Rpt1.SetParameters(new ReportParameter("munit", munit));
                    Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                    Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                    Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                    Rpt1.SetParameters(new ReportParameter("takainword", amt1t.Replace("Taka", "").Replace("Only", "Taka Only")));
                    Rpt1.SetParameters(new ReportParameter("As", ((Installment == "") ? rectype : Installment)));
                    Rpt1.SetParameters(new ReportParameter("takainword1", amt1t.Replace("Taka", "").Replace("Only", "Taka Only") + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                    Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                    Rpt1.SetParameters(new ReportParameter("chqno", chqno));
                    Rpt1.SetParameters(new ReportParameter("bank", bankname));
                    Rpt1.SetParameters(new ReportParameter("branch", branch));
                    Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                    Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                    Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("footer1", "Original money Receipt will be provided after encashment the PO/DD/cross cheque in favor of CPDL."));
                    Rpt1.SetParameters(new ReportParameter("footer2", "Thanking you"));
                    Rpt1.SetParameters(new ReportParameter("notes", "Note: This is a system generated document and does not require physical signature."));


                    Session["Report1"] = Rpt1;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "&Title=Acknowledgement Slip', target='_self');</script>";
                }
                else
                {

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptCPDL", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("CompName", comnam));
                    Rpt1.SetParameters(new ReportParameter("CompName1", comnam));
                    Rpt1.SetParameters(new ReportParameter("CompAdd", comadd));
                    Rpt1.SetParameters(new ReportParameter("CustAdd", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                    Rpt1.SetParameters(new ReportParameter("CustAdd1", (custmob == "") ? custadd : (custadd + ", " + "Mobile: " + custmob)));
                    Rpt1.SetParameters(new ReportParameter("custteam", "Received by: " + custteam));
                    Rpt1.SetParameters(new ReportParameter("custteam1", "Received by: " + custteam));
                    Rpt1.SetParameters(new ReportParameter("rmrks", "Remarks: " + rmrks));
                    Rpt1.SetParameters(new ReportParameter("rmrks1", "Remarks: " + rmrks));
                    Rpt1.SetParameters(new ReportParameter("aptno", udesc));
                    Rpt1.SetParameters(new ReportParameter("usize", usize));
                    Rpt1.SetParameters(new ReportParameter("munit", munit));
                    Rpt1.SetParameters(new ReportParameter("usize1", udesc + ", " + usize + " " + munit));
                    Rpt1.SetParameters(new ReportParameter("amount", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                    Rpt1.SetParameters(new ReportParameter("amount1", "TK. " + Convert.ToDouble(paidamt).ToString("#,##0;(#,##0)")));
                    Rpt1.SetParameters(new ReportParameter("takainword", amt1t.Replace("Taka", "").Replace("Only", "Taka Only")));
                    Rpt1.SetParameters(new ReportParameter("As", ((Installment == "") ? rectype : Installment)));
                    Rpt1.SetParameters(new ReportParameter("takainword1", amt1t.Replace("Taka", "").Replace("Only", "Taka Only") + " " + "AS " + ((Installment == "") ? rectype : Installment)));
                    Rpt1.SetParameters(new ReportParameter("paytype", Typedes));
                    Rpt1.SetParameters(new ReportParameter("chqno", chqno));
                    Rpt1.SetParameters(new ReportParameter("bank", bankname));
                    Rpt1.SetParameters(new ReportParameter("branch", branch));
                    Rpt1.SetParameters(new ReportParameter("paytype1", Typedes));
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo1", ASTUtility.Concat(compname, username, printdate)));
                    Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                    Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
                    Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                    Rpt1.SetParameters(new ReportParameter("footer", "Note : This is a system generated receipt and does not require any physical signature"));

                    Session["Report1"] = Rpt1;
                    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
                }

            }

            else if (Type == "MRPrintFinlay")
            {
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();

                string paytype1 = "";
                string amt22 = amt1t.Replace("(", "").Replace("Taka", "").Replace(")", "").Trim();

                string pertype = ((Installment == "") ? rectype : Installment);
                if (paytype == "CHEQUE" || paytype == "P.O")
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptFinlay", list, null, null);
                    paytype1 = "CHEQUE/" + "\n" + "PO";
                    //Rpt1.SetParameters(new ReportParameter("takainword1", pertype));
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptFinlay2", list, null, null);
                    paytype1 = "CASH" + "/Adj" + "/Bank Trans";

                }

                Rpt1.EnableExternalImages = true;


                Rpt1.SetParameters(new ReportParameter("txtbenefname", benefname));
                Rpt1.SetParameters(new ReportParameter("txtDate", mrDate));
                Rpt1.SetParameters(new ReportParameter("txtDate1", curDate));
                Rpt1.SetParameters(new ReportParameter("mrno", "MR" + mrno));
                Rpt1.SetParameters(new ReportParameter("mrno1", "MR" + mrno));
                Rpt1.SetParameters(new ReportParameter("custname", custname));
                Rpt1.SetParameters(new ReportParameter("custname1", custname));
                Rpt1.SetParameters(new ReportParameter("CustAdd", custadd));
                Rpt1.SetParameters(new ReportParameter("CustAdd1", custadd));
                Rpt1.SetParameters(new ReportParameter("custmob", custmob));
                Rpt1.SetParameters(new ReportParameter("custmob1", custmob));
                Rpt1.SetParameters(new ReportParameter("project", project));
                Rpt1.SetParameters(new ReportParameter("project1", project));
                Rpt1.SetParameters(new ReportParameter("unit", udesc));
                Rpt1.SetParameters(new ReportParameter("unit1", udesc));
                Rpt1.SetParameters(new ReportParameter("amount", Convert.ToDouble(paidamt).ToString("#,##0.00;(#,##0.00) ")));
                Rpt1.SetParameters(new ReportParameter("amount1", Convert.ToDouble(paidamt).ToString("#,##0.00;(#,##0.00) ")));
                Rpt1.SetParameters(new ReportParameter("takainword", amt22));
                Rpt1.SetParameters(new ReportParameter("takainword1", amt22));
                Rpt1.SetParameters(new ReportParameter("txtperpose", pertype));


                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("paytype", paytype1));

                string PrintOpt = Request.QueryString.AllKeys.Contains("PrintOpt") ? this.Request.QueryString["PrintOpt"].ToString() : "";
                PrintOpt = PrintOpt.Length > 0 ? PrintOpt : ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();

                Session["Report1"] = Rpt1;
                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" + PrintOpt + "', target='_self');</script>";
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" + PrintOpt + "', target='_self');</script>";
            }

            else if (Type== "MRPrintAcme")
            {
               
                var list = ds4.Tables[0].DataTableToList<RealEntity.C_22_Sal.Sales_BO.CustomerMoneyrecipt>();
                if (comcod == "1207")
                {
                    DataTable dtservice = ds4.Tables[1];
                    string custid = dtservice.Rows[0]["customerid"].ToString();
                    string workdesc = dtservice.Rows[0]["workdesc"].ToString();
                    string quotid = dtservice.Rows[0]["quotid"].ToString();
                    string paddress = dtservice.Rows[0]["paddress"].ToString();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptAcme02", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("custid", custid));
                    Rpt1.SetParameters(new ReportParameter("workdesc", workdesc));
                    Rpt1.SetParameters(new ReportParameter("paddress", paddress));
                }
                else
                {
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptMoneyReceiptAcme", list, null, null);
                    Rpt1.EnableExternalImages = true;
                }

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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }else
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
                Rpt1.SetParameters(new ReportParameter("txtcominfo", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("txtcominfo1", ASTUtility.ComInfoWithoutNumber()));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
        }

        private bool IsAutoLengthDesc()
        {
            bool isDesc = false;
            string comcod = GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "1205": // p2p
                case "3351": // p2p
                case "3352": // p2p
                case "3356": // intech
                case "3325": // leisure
                case "2325": // leisure
                    isDesc = true;
                    break;

                default:
                    isDesc = false;
                    break;

            }
            return isDesc;
        }
    }
}