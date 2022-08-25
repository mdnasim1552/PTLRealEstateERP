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
using RealEntity;
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;

namespace RealERPWEB.F_17_Acc
{
    public partial class AccPrint : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        Common Common = new Common();
        public static double TAmount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "PostDatVou":
                    PostVouPrint();
                    break;
                case "accVou":
                    VoucherPrintRDLC();
                    break;

                case "PfaccVou":
                    PfVoucherPrint();
                    break;
                case "AccCheque":
                    PrintCheque();
                    break;

                case "CashSalaryCheque":
                    PrinChequeCashSalary();
                    break;


                case "AccPostDatChq":
                    PrintPostDatedCheque();
                    break;
            }
        }

        private void VoucherPrintRDLC()
        {
            //Nayan Iqbal 
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3336":// Nayan  
                //case "3101":// Nayan 
                case "1111":// Nayan 
                    this.PrintVoucherRDLC();
                    break;
                default:
                    this.printVoucher();
                    break;
            }
        }




        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private string CompanyPrintPostVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                //case "3101":
                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;

                //            case "3101":
                case "3305":
                case "3310":
                    //case "3311":          
                    vouprint = "VocherPrint2";
                    break;

                //case"3101":
                case "3309":
                    vouprint = "VocherPrint3";
                    break;

                //case "3101":
                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;

                //case"3101":
                case "3311":
                    vouprint = "VocherPrint6";
                    break;

                case "3333":
                    vouprint = "VocherPrintAlliance";
                    break;

                //case "3101":
                case "3336":
                case "3337":
                    vouprint = "VocherPrintSuvastu";
                    break;


                case "3339":
                    vouprint = "VocherPrintTropical";
                    break;

                //case "3101":


                case "3349":
                case "3348":
                    vouprint = "VocherPrintCredence";
                    break;

                //case "3101":
                case "3351":
                case "3352":
                case "1205":
                    vouprint = "VoucherPrintP2P";
                    break;

                //case "3101":
                case "3353":
                    vouprint = "VocherPrintManama";
                    break;

                //case "3101":
                case "3325":
                case "2325":
                    vouprint = "VocherPrintLeisure";
                    break;

                //case "3101":
                case "3338":
                    vouprint = "VocherPrintAcme";
                    break;

                case "1103":
                    vouprint = "VocherPrintTanvir";
                    break;

                //case "3101":
                case "3358":
                case "3359":
                case "3360":
                case "3361":
                    vouprint = "VocherPrintEntrust";
                    break;

                case "3101":
                case "3355":
                    vouprint = "VocherPrintGreenwood";
                    break;

                case "3364":
                    vouprint = "VocherPrintJBS";
                    break;

                //case "3101":
                case "3356":
                    vouprint = "VocherPrintIntech";
                    break;
                    
                //case "3101":
                case "3367":
                    vouprint = "VocherPrintEpic";
                    break;

                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }

        private string GetCompInstarPost()
        {


            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3330":// Bridge
                case "2325":// Leisure
                case "3325":// Leisure
                case "1103":// Tanvir
                case "3305":// RHEL
                case "3311":// RHEL(ctg)
                case "3306":// Ratul
                case "3309":// HOlding
                case "3310":// RCU
                case "2305":// land

                case "1108"://  Assure(engineering)
                case "1109"://  Assure(tourism)
                case "3315"://  Assure(Builders)
                case "3316"://  Assure(Development)
                case "3317"://  Assure(Aggro)
                case "3333"://  Alliance
                case "3338"://  ACME
                case "3339"://  Tropical
                case "3340"://  Urban
                case "3101":// Pintech  
                case "3353":// manama  

                case "3357":// Cube   
                case "3367":// Epic    

                    break;



                default:
                    printinstar = "Innstar";
                    break;


            }
            return printinstar;

        }
        private void PostVouPrint()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

                string qcomcod = this.Request["comcod"] ?? "";
                comcod = qcomcod.Length == 0 ? comcod : qcomcod;


                string qcomname = this.Request["comname"] ?? "";
                comnam = qcomname.Length == 0 ? comnam : qcomname;
                string vounum = this.Request.QueryString["vounum"].ToString();

                string PrintInstar = this.GetCompInstarPost();


                //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, "", "", "", "", "", "", "", "");


                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "PRINTVOUCHER01", vounum, PrintInstar, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt = _ReportDataSet.Tables[0];
                DataTable dt1 = _ReportDataSet.Tables[1];
                string VouType = dt1.Rows[0]["voutyp"].ToString();
                if (dt.Rows.Count == 0)
                    return;
                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));

                string payto = dt1.Rows[0]["payto"].ToString();
                string chequeno = dt1.Rows[0]["chequeno"].ToString();
                string cheqdate = Convert.ToDateTime(dt1.Rows[0]["cheqdate"]).ToString("dd-MMM-yyyy");

                string postrmid = dt1.Rows[0]["entryid"].ToString();
                string postuser = dt1.Rows[0]["entryPerson"].ToString();
                //string postseson = dt1.Rows[0]["chequeno"].ToString();
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["entryDate"]).ToString("dd-MMM-yyyy");
                string postdesig = dt1.Rows[0]["entrydesig"].ToString();
                string txtsign1 = postuser + "\n" + postdesig + "\n" + Posteddat;


                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                string Type = this.CompanyPrintPostVou();

                LocalReport Rpt1 = new LocalReport();

                //ReportDocument rptinfo = new ReportDocument();

                if (Type == "VocherPrint")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher", list, null, null);
                    Rpt1.EnableExternalImages = true;
                }             

                else if (Type == "VocherPrintAlliance")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherAlliance", list, null, null);
                    Rpt1.EnableExternalImages = true;

                }
                else if (Type == "VocherPrint1")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPay", (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From"));


                }
                else if (Type == "VocherPrint2")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPay", (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From"));

                }

                else if (Type == "VocherPrint3")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;

                }

                else if(Type== "VocherPrint4")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPay", (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From"));


                }
                else if (Type == "VocherPrint5")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher5", list, null, null);
                    Rpt1.EnableExternalImages = true;


                }
                else if (Type == "VocherPrint6")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher6", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPay", (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From"));



                }

                else if (Type == "VocherPrintLeisure")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherLeisure", list, null, null);
                    Rpt1.EnableExternalImages = true;

                }


                else if (Type == "VocherPrintTropical")
                {


                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherTropical", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", chequeno));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                    Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("chqedate", cheqdate));


                }

                else if (Type == "VocherPrintCredence")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherCredence", list, null, null);
                    Rpt1.EnableExternalImages = true;


                }

                else if (Type == "VocherPrintSuvastu")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherSuvastu", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("refnum", chequeno));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                    Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("chqedate", cheqdate));


                }


                else if (Type == "VoucherPrintP2P")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherP2P", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPayto", (payto == "") ? "" : payto));
                    Rpt1.SetParameters(new ReportParameter("refnum", chequeno));
                    Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));

                }

                else if (Type == "VocherPrintAcme")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherAcme", list, null, null);
                    Rpt1.EnableExternalImages = true;
                }


                else if (Type == "VocherPrintManama")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherManama", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("chqno", "Cheque No : " + chequeno));
                    Rpt1.SetParameters(new ReportParameter("voutype1", " Bank Payment Voucher "));
                    Rpt1.SetParameters(new ReportParameter("txtsign1", txtsign1));

                }

                else if (Type == "VocherPrintTanvir")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherTanvir", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPay", (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From"));

                }

                else if (Type == "VocherPrintEntrust")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherEntrust", list, null, null);
                    Rpt1.EnableExternalImages = true;


                }

                else if (Type == "VocherPrintGreenwood")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherGreenwood", list, null, null);
                    Rpt1.EnableExternalImages = true;

                }

                else if (Type == "VocherPrintIntech")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherIntech", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtSign1", txtsign1));

                }

                else if (Type == "VocherPrintEpic")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucherEpic", list, null, null);
                    Rpt1.EnableExternalImages = true;
                }

                // defult rupayan RLDL
                else
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.PostVoucherPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptBankVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("txtPay", (vounum.Substring(0, 2).ToString() == "PV") ? "Pay To " : "Receive From"));

                }




                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string venar = dt1.Rows[0]["venar"].ToString();


                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("Vounum", (comcod == "3336" || comcod == "3337" || comcod == "3101" || comcod == "3339" || comcod == "1205" || comcod == "3351" || comcod == "3352") ? vounum : "Voucher No : " + vounum));
                Rpt1.SetParameters(new ReportParameter("voudat", (comcod == "3336" || comcod == "3337" || comcod == "3101" || comcod == "3339" || comcod == "1205" || comcod == "3351" || comcod == "3352") ? voudat : "Voucher Date : " + voudat));

                if (comcod == "2305" || comcod == "3305" || comcod == "3306" || comcod == "3309" || comcod == "3310" || comcod == "3311")
                {
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                }

                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                if (comcod == "2325" || comcod == "3325" || comcod == "3355")
                {
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat1(postrmid, postuser, "", Posteddat, compname, username, printdate, "")));
                    Rpt1.SetParameters(new ReportParameter("voutype", "PDC " + VouType));
                }
                else
                {
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
                    Rpt1.SetParameters(new ReportParameter("voutype", VouType));
                }

                //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




                if (ConstantInfo.LogStatus)
                {

                    string eventdesc = "Print Post Dated Voucher";
                    string eventdesc2 = "Voucher: " + vounum + " Dated: " + voudat;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, eventdesc2);


                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }
        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3330":// Bridge
                case "2325":// Leisure
                case "3325":// Leisure
                case "1103":// Tanvir
                case "3305":// RHEL
                case "3311":// RHEL(ctg)
                case "3310":// RHEL(ctg)

                case "3306":// Ratul
                case "3309":// Holding
                case "2305":// land
                case "1108"://  Assure(Engineering)
                case "1109"://  Assure(Tourism)
                case "3315"://  Assure(Builders)
                case "3316"://  Assure(Development)
                case "3317"://  Assure(Aggro)
                case "3353":
                //  case "3101"://  ASIT (Check)

                case "3357"://  Cube Holding
                case "3364"://  JBS 

                //case "3101"://  Pintech 
                case "1102"://  Islam Brothers 
                case "3368"://  Finlay  Properties
                case "3367"://  Epic  Properties

                    break;



                default:
                    printinstar = "Innstar";
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


                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;


                case "3305":
                case "3310":
                    //case "3311":
                    vouprint = "VocherPrint2";
                    break;


                case "3309":
                    vouprint = "VocherPrint3";
                    break;

                //case "3101":
                case "2305": // rupayan land
                    vouprint = "VocherPrint4";
                    break;

                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;

                //case "3101":
                case "3311":
                    vouprint = "VocherPrint6";
                    break;

                //case "3101":
                case "3330":
                    vouprint = "VocherPrintBridge";
                    break;

                case "3332":
                    vouprint = "VocherPrintIns";
                    break;


                case "3333":
                    vouprint = "VocherPrintMod";
                    break;


                case "3336":
                case "3337":
                    vouprint = "VocherPrintSuvastu";
                    break;

                //case "3101":
                case "2325":
                case "3325":
                    vouprint = "VocherPrintLei";
                    break;

                //case "3101":
                case "3353":
                    vouprint = "VoucherPrintManama";
                    break;

                //case "3101":
                case "3357":
                    vouprint = "VoucherPrintCube";
                    break;

                case "1103":// Tanvir
                    vouprint = "VocherPrintTanvir";
                    break;

                //case "3101":
                case "3349":
                case "3348":
                    vouprint = "VocherPrintCredence";
                    break;

                case "1102": // islam brothers 
                    vouprint = "VocherPrintISBL";
                    break;

                case "3368": // Finaly
                    vouprint = "VocherPrintFinlay";
                    break;

                //manama, p2p 
                // Entrust Collection
                //case "3101":
                case "3358":
                case "3359":
                case "3360":
                case "3361":
                    vouprint = "VocherPrintEntrust";
                    break;

                //case "3101":
                case "3364":
                    vouprint = "VocherPrintJBS";
                    break;

                //case "3101":
                case "3356":
                    vouprint = "VocherPrintIntech";
                    break;

                //case "3101":
                case "3367":
                    vouprint = "VocherPrintEpic";
                    break;

                case "3101":
                case "3355":
                    vouprint = "VocherPrintGreenwood";
                    break;

                default:
                    vouprint = "VocherPrintMod";
                    break;
            }
            return vouprint;
        }


        private string GetCompNarration()
        {

            string comcod = this.GetCompCode();
            string ntype = "";
            switch (comcod)
            {
                case "1103":// Tanvir
                case "3353": // manama
                case "3101":// ptl
                    ntype = "ShowNarration";
                    break;

                default:
                    ntype = "";
                    break;
            }
            return ntype;
        }



        private string Getpouaction(string vounum)
        {
            string pounaction = "";
            string comcod = this.GetCompCode();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "GETPOUNACTION", vounum, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return pounaction;
            pounaction = ds1.Tables[0].Rows[0]["pounaction"].ToString();
            return pounaction;

        }
        private void printVoucher()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string vounum = this.Request.QueryString["vounum"].ToString();
                string session = hst["session"].ToString();
                string PrintInstar = this.GetCompInstar();
                string ntype = this.GetCompNarration();

                string pouaction = this.Getpouaction(vounum);
                string Calltype = (pouaction.Length > 0) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";
                //string Calltype =  "PRINTVOUCHER01";
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", Calltype, vounum, PrintInstar, ntype, "", "", "", "", "", "");
                if (_ReportDataSet == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Not Found');", true);
                    return;
                }
                DataTable dt = _ReportDataSet.Tables[0];


                DataTable dts = _ReportDataSet.Tables[2];

                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));

                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                DataTable dt1 = _ReportDataSet.Tables[1];
                string Vounum = dt1.Rows[0]["vounum"].ToString();
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string refnum = dt1.Rows[0]["refnum"].ToString();
                string payto = dt1.Rows[0]["payto"].ToString();
                string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                string Posteddat2 = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd.MM.yyyy hh:mm:ss tt");
                string postuser = dt1.Rows[0]["postuser"].ToString();
                string postseson = dt1.Rows[0]["postseson"].ToString();
                string postrmid = dt1.Rows[0]["postrmid"].ToString();
                string receivedBank = dt1.Rows[0]["banknam"].ToString();

                string aprvdat = Convert.ToDateTime(dt1.Rows[0]["aprvdat"]).ToString("dd-MMM-yyyy");
                string aprvbyid = dt1.Rows[0]["aprvbyid"].ToString();
                string aprvuser = dt1.Rows[0]["aprvuser"].ToString();
                string APRVTRMID = dt1.Rows[0]["APRVTRMID"].ToString();
                string APRVSESON = dt1.Rows[0]["APRVSESON"].ToString();

                string Type = this.CompanyPrintVou();

                string billno = dt.Rows[0]["billno"].ToString();

                //Signnitory 
                string preby = dts.Select("gcod='01003'").Length > 0 ? (dts.Select("gcod='01003'")[0]["gdesc"]).ToString().Trim() : "";
                string Checkby = dts.Select("gcod='01004'").Length > 0 ? (dts.Select("gcod='01004'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvby2 = dts.Select("gcod='01005'").Length > 0 ? (dts.Select("gcod='01005'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvby1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvbyt1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvbyst1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["stdgdesc"]).ToString().Trim() : "";
                string authorizeby = dts.Select("gcod='01010'").Length > 0 ? (dts.Select("gcod='01010'")[0]["gdesc"]).ToString().Trim() : "";
                // string authorizeby = dts.Select("gcod='01010'").Length > 0 ? (dts.Select("gcod='01010'")[0]["gdesc"]).ToString() : "";

                // todo for jbs project location 
                string project = "";
                string pLocation = "";
                string vounum1 = ASTUtility.Left(Vounum, 2) + Vounum.Substring(7, 2) + "-" + ASTUtility.Right(Vounum, 6);
                if (_ReportDataSet.Tables.Count > 3)
                {
                    if (_ReportDataSet.Tables[3].Rows.Count > 0)
                    {
                        project = _ReportDataSet.Tables[3].Rows[0]["pactdesc"].ToString();
                        pLocation = _ReportDataSet.Tables[3].Rows[0]["gdatat"].ToString();
                    }
                }
                //string[] billno1;

                //string[] billno;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // dt1.Rows[j]["useridapp"].ToString() == useridapp

                    if (billno == dt.Rows[i]["billno"].ToString())
                    {

                    }

                    else
                    {
                        billno += dt.Rows[i]["billno"].ToString() + (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
                    }

                }

                int l = billno.Trim().Length;
                billno = (billno.Length == 0) ? "" : billno.Substring(0, l - 1);

                LocalReport Rpt1 = new LocalReport();


                ReportDocument rptinfo = new ReportDocument();

                if (Type == "VocherPrint")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherDefault", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                }

                else if (Type == "VocherPrint1")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                }
                else if (Type == "VocherPrint2")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }

                else if (Type == "VocherPrint3")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }

                else if (Type == "VocherPrint4")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));
                }

                else if (Type == "VocherPrint5")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher5", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }


                else if (Type == "VocherPrint6")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher6", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                }

                else if (Type == "VocherPrintTanvir")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherTanvir", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));


                }

                else if (Type == "VocherPrintBridge")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherBridge", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));

                }

                else if (Type == "VocherPrintISBL")
                {
                    string vouno = vounum.Substring(0, 2);
                    if (vouno == "BC" || vouno == "CC" || vouno == "JV")
                    {
                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherISBL", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                        Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    }
                    else
                    {
                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherISBL02", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                        Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    }

                }

                else if (Type == "VocherPrintFinlay")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                    if (ASTUtility.Left(vounum, 2) == "CD")
                    {
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherFinlay02", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("txtreceivedby", aprvby2));
                    }
                    else
                    {
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherFinlay", list, null, null);
                        Rpt1.EnableExternalImages = true;

                    }
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("voutype", (ASTUtility.Left(vounum, 2) == "CC") ? "Cash Received Voucher" : voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", authorizeby));

                }
                else if (Type == "VocherPrintEpic")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherEpic", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));

                }

                else if (Type == "VocherPrintLei")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher7", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));

                }


                else if (Type == "VocherPrintSuvastu")
                {

                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {

                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherSuvastu", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));

                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

                        if (vouno == "BC" || vouno == "CC")
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherSuvastu02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));

                        }
                        else
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherSuvastu03", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtissuno", vouno == "BD" || vouno == "CD" ? "Payment ID : " + dt1.Rows[0]["isunum"].ToString() : ""));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));


                        }
                    }
                }

                else if (Type == "VocherPrintCredence")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {

                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherCredence01", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("preparedby", postuser));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));

                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc;

                        prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? dt.Rows[0]["actdesc"].ToString() : "Head Office";

                        // prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";


                        if (vouno == "BC" || vouno == "CC")
                        {
                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherCredence02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("preparedby", postuser));

                        }
                        else
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherCredence03", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtissuno", vouno == "BD" || vouno == "CD" ? "Payment ID : " + dt1.Rows[0]["isunum"].ToString() : ""));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("preparedby", postuser));


                        }
                    }

                }

                else if (Type == "VocherPrintJBS")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {

                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherJBS01", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", vounum1));
                        Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("preparedby", postuser));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("txtProject", project));
                        Rpt1.SetParameters(new ReportParameter("txtProjectAdd", pLocation));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc;

                        prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? dt.Rows[0]["actdesc"].ToString() : "Head Office";

                        // prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";



                        if (vouno == "BC" || vouno == "CC")
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherJBS02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("preparedby", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtProject", project));
                            Rpt1.SetParameters(new ReportParameter("txtProjectAdd", pLocation));



                        }
                        else
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherJBS03", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtissuno", vouno == "BD" || vouno == "CD" ? "Payment ID : " + dt1.Rows[0]["isunum"].ToString() : ""));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("preparedby", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtProject", project));
                            Rpt1.SetParameters(new ReportParameter("txtProjectAdd", pLocation));

                        }
                    }

                }

                else if (Type == "VocherPrintMod")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {
                        switch (comcod)
                        {
                            case "1205":// P2P
                            case "3351":// P2P
                            case "3352":// P2P
                                aprvby1 = aprvbyst1;
                                break;
                        }

                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3344") ? aprvby2 : Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));


                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc = "";

                        switch (comcod)
                        {
                            case "3339":

                                // || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") 

                                prjdesc = (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "1301" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16"
                                    || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1902") || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 2) == "29") || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "27"
                                    || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "39" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2201" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2301" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26") ?

                                     (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "1301" ? dt.Rows[0]["actdesc"].ToString().Replace("ADVANCE-", "")

                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" ? dt.Rows[0]["actdesc"].ToString().Replace("WIP-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" ? dt.Rows[0]["actdesc"].ToString().Replace("AR-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "39" ? dt.Rows[0]["actdesc"].ToString().Replace("NOI-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "27" ? dt.Rows[0]["actdesc"].ToString().Replace("NOI-", "")
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") ? dt.Rows[0]["actdesc"].ToString().Replace("CASH-", "")
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1902") ? dt.Rows[0]["pactdesc"].ToString()
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 2) == "29") ? dt.Rows[0]["pactdesc"].ToString()
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2201" ? dt.Rows[0]["actdesc"].ToString().Replace("LOAN-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2301" ? dt.Rows[0]["actdesc"].ToString().Replace("TVS-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? dt.Rows[0]["actdesc"].ToString().Replace("AP-", "") : "")
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "2303") ? dt1.Rows[0]["cactdesc"].ToString().Replace("CASH-", "") : "Head Office";
                                break;




                            default:

                                prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";
                                break;

                        }
                        if (vouno == "BC" || vouno == "CC")
                        {


                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtRecvby", (comcod == "3333") ? "" : "Received By"));
                            Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                            Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3333") ? "" : Checkby));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                            Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            //Rpt1.SetParameters(new ReportParameter("paytoorecived", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));



                        }
                        else
                        {

                            if (vouno == "BD" || vouno == "CD" || vouno == "CT")
                            {

                                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli03", list, null, null);
                                Rpt1.EnableExternalImages = true;
                                Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                                Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                                Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                                Rpt1.SetParameters(new ReportParameter("txtissuno", "Payment ID : " + dt1.Rows[0]["isunum"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                                Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                                Rpt1.SetParameters(new ReportParameter("username", postuser));
                                Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                                Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3333") ? "" : Checkby));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                                Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                                Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                                Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));

                            }

                        }
                    }

                } 
                
                else if (Type == "VocherPrintGreenwood")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {
                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherGreenwood01", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));

                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                        if (vouno == "BC" || vouno == "CC")
                        {
                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherGreenwood02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtRecvby", "Received By"));
                            Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                            Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                            Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            //Rpt1.SetParameters(new ReportParameter("paytoorecived", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));


                        }
                        else
                        {
                          if (vouno == "BD" || vouno == "CD" || vouno == "CT")
                            {
                                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherGreenwood02", list, null, null);
                                Rpt1.EnableExternalImages = true;
                                Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                                Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                                Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                                Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtRecvby", "Received By"));
                                Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                                Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                                Rpt1.SetParameters(new ReportParameter("username", postuser));
                                Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                                Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                                Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                                Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                                Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            }

                        }
                    }

                }

                else if (Type == "VocherPrintIntech")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {
                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherIntech01", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No : " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                        Rpt1.SetParameters(new ReportParameter("username", "Prepared By : " + postuser));
                        Rpt1.SetParameters(new ReportParameter("txtissuno", "Approved By : " + aprvuser));
                        Rpt1.SetParameters(new ReportParameter("txtuserinfo2", ASTUtility.Concat2(postrmid, postuser, postseson, Posteddat2)));

                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";
                        if (vouno == "BC" || vouno == "CC")
                        {
                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherIntech02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("txtRecvby", "Received By"));
                            Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                            Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                            Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            //Rpt1.SetParameters(new ReportParameter("paytoorecived", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("username", "Prepared By : " + postuser));
                            Rpt1.SetParameters(new ReportParameter("txtissuno", "Approved By : " + aprvuser));
                            Rpt1.SetParameters(new ReportParameter("txtuserinfo2", ASTUtility.Concat2(postrmid, postuser, postseson, Posteddat2)));


                        }
                        else
                        {
                            if (vouno == "BD" || vouno == "CD" || vouno == "CT")
                            {
                                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherIntech03", list, null, null);
                                Rpt1.EnableExternalImages = true;
                                Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                                Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                                Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                                Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                                Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                                Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                                Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                                Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                                Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                                Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));
                                Rpt1.SetParameters(new ReportParameter("username", "Prepared By : " + postuser));
                                Rpt1.SetParameters(new ReportParameter("txtissuno", "Approved By : " + aprvuser));
                                Rpt1.SetParameters(new ReportParameter("txtuserinfo2", ASTUtility.Concat2(postrmid, postuser, postseson, Posteddat2)));


                            }

                        }
                    }

                }

                else if (Type == "VoucherPrintManama")
                {
                    string voutype1 = "";

                    string vouno = vounum.Substring(0, 2);
                    if (vouno == "BC")
                    {
                        voutype1 = "Bank Receive Voucher";
                    }
                    else if (vouno == "CC")
                    {
                        voutype1 = "Cash Receive Voucher";
                    }

                    else if (vouno == "JV")
                    {
                        voutype1 = "Journal Voucher";
                    }
                    else
                    {
                        voutype1 = voutype;
                    }

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherManama", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype1));
                    Rpt1.SetParameters(new ReportParameter("venar", venar));
                    Rpt1.SetParameters(new ReportParameter("preparedby", postuser));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));
                }

                else if (Type == "VoucherPrintCube")
                {
                    string voutype1 = "";

                    string vouno = vounum.Substring(0, 2);
                    if (vouno == "BC")
                    {
                        voutype1 = "Bank Receive Voucher";
                    }
                    else if (vouno == "CC")
                    {
                        voutype1 = "Cash Receive Voucher";
                    }

                    else if (vouno == "JV")
                    {
                        voutype1 = "Journal Voucher";
                    }
                    else
                    {
                        voutype1 = voutype;
                    }

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherCube", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype1));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("preparedby", postuser));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));

                }

                else if (Type == "VocherPrintEntrust")
                {

                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {
                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherEntrust01", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", aprvby2));


                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc = "";
                        prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";

                        if (vouno == "BC" || vouno == "CC")
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherEntrust02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtRecvby", (comcod == "3333") ? "" : "Received By"));
                            Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                            Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3333") ? "" : Checkby));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                            Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            //Rpt1.SetParameters(new ReportParameter("paytoorecived", paytoorecived));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));


                        }
                        else
                        {

                            if (vouno == "BD" || vouno == "CD" || vouno == "CT")
                            {

                                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherEntrust03", list, null, null);
                                Rpt1.EnableExternalImages = true;
                                Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                                Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                                Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                                Rpt1.SetParameters(new ReportParameter("txtissuno", "Payment ID : " + dt1.Rows[0]["isunum"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                                Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                                Rpt1.SetParameters(new ReportParameter("username", postuser));
                                Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                                Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3333") ? "" : Checkby));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                                Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                                Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                                Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));

                            }

                        }
                    }
                }

                else
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                }


                string events = hst["events"].ToString();
                if (Convert.ToBoolean(events) == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
                    string eventdesc = "Print Voucher";
                    string eventdesc2 = "Voucher: " + vounum + " Dated: " + voudat;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                }


                //if (ConstantInfo.LogStatus == true)
                //{

                //    string eventdesc = "Print Voucher";
                //    string eventdesc2 = "Voucher: " + vounum + " Dated: " + voudat;
                //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, eventdesc2);


                //}


                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));

                if (comcod == "2325" || comcod == "3325" || comcod == "3353")
                {
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat1(postrmid, postuser, postseson, Posteddat, compname, username, printdate, session)));
                }
                else
                {
                    Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate, session)));
                }

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void PrintVoucherRDLC()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string combranch = hst["combranch"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string vounum = this.Request.QueryString["vounum"].ToString();
            string session = hst["session"].ToString();
            string PrintInstar = this.GetCompInstar();
            string pouaction = this.Getpouaction(vounum);
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string Calltype = (pouaction.Length > 0) ? "PRINTUNPOSTEDVOUCHER01" : "PRINTVOUCHER01";
            //string Calltype =  "PRINTVOUCHER01";
            DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", Calltype, vounum, PrintInstar, "", "", "", "", "", "", "");
            if (_ReportDataSet == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Not Found');", true);
                return;
            }
            DataTable dt = _ReportDataSet.Tables[0];
            DataTable dts = _ReportDataSet.Tables[2];

            double dramt, cramt;
            dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
            cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));

            if (dramt > 0 && cramt > 0)
            {
                TAmount = cramt;

            }
            else if (dramt > 0 && cramt <= 0)
            {
                TAmount = dramt;
            }
            else
            {
                TAmount = cramt;
            }

            DataTable dt1 = _ReportDataSet.Tables[1];
            string Vounum = dt1.Rows[0]["vounum"].ToString();
            string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            string refnum = dt1.Rows[0]["refnum"].ToString();
            string payto = dt1.Rows[0]["payto"].ToString();
            string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
            string voutype = dt1.Rows[0]["voutyp"].ToString();
            string venar = dt1.Rows[0]["venar"].ToString();
            string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
            string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
            string postuser = dt1.Rows[0]["postuser"].ToString();
            string potseson = dt1.Rows[0]["postseson"].ToString();
            string postrmid = dt1.Rows[0]["postrmid"].ToString();
            string receivedBank = dt1.Rows[0]["banknam"].ToString();

            // string Type = this.CompanyPrintVou();

            string billno = dt.Rows[0]["billno"].ToString();


            //Signnitory 

            string preby = dts.Select("gcod='01003'").Length > 0 ? (dts.Select("gcod='01003'")[0]["gdesc"]).ToString() : "";
            string Checkby = dts.Select("gcod='01004'").Length > 0 ? (dts.Select("gcod='01004'")[0]["gdesc"]).ToString() : "";
            string aprvby2 = dts.Select("gcod='01005'").Length > 0 ? (dts.Select("gcod='01005'")[0]["gdesc"]).ToString() : "";
            string aprvby1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["gdesc"]).ToString() : "";

            //string[] billno;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // dt1.Rows[j]["useridapp"].ToString() == useridapp

                if (billno == dt.Rows[i]["billno"].ToString())
                {

                }

                else
                {
                    billno += dt.Rows[i]["billno"].ToString() + (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
                }

            }

            int l = billno.Trim().Length;
            billno = (billno.Length == 0) ? "" : billno.Substring(0, l - 1);

            var AccTrialBl1 = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();
            LocalReport Rpt1 = new LocalReport();

            string vouno = vounum.Substring(0, 2);
            string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
            if (vouno == "JV")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPrintVoucher", AccTrialBl1, null, null);
            }
            else if (vouno == "BD")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPrintVoucher02", AccTrialBl1, null, null);
            }
            else if (vouno == "BC" || vouno == "CC" || vouno == "CD" || vouno == "CT")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPrintVoucher01", AccTrialBl1, null, null);
            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "NAYAN"));
            Rpt1.SetParameters(new ReportParameter("Vounum", Vounum));
            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("Partytype", Partytype));
            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
            Rpt1.SetParameters(new ReportParameter("venar", venar));
            Rpt1.SetParameters(new ReportParameter("Isunum", Isunum));
            Rpt1.SetParameters(new ReportParameter("Posteddat", Posteddat));
            Rpt1.SetParameters(new ReportParameter("postuser", postuser));
            Rpt1.SetParameters(new ReportParameter("potseson", potseson));
            Rpt1.SetParameters(new ReportParameter("postrmid", postrmid));
            Rpt1.SetParameters(new ReportParameter("Project", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
            Rpt1.SetParameters(new ReportParameter("bankCash", dt1.Rows[0]["cactdesc"].ToString()));



            Rpt1.SetParameters(new ReportParameter("receivedBank", receivedBank));
            Rpt1.SetParameters(new ReportParameter("InWrd", "In Words : " + ASTUtility.Trans(Math.Round(TAmount), 2)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private string ComCheckBy()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string checkby = "";
            switch (comcod)
            {

                case "3333":
                    checkby = "";
                    break;
                default:
                    checkby = "Checkby";
                    break;
            }
            return checkby;

        }
        private string CompanyPrintCheque()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string chequeprint = "";
            switch (comcod)
            {
                //case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                    chequeprint = "PrintCheque01";
                    break;


                case "1301":
                case "2301":
                case "3301":
                    chequeprint = "PrintCheque02";
                    break;


                //case "3306":
                //    chequeprint = "PrintCheque03";
                //    break;
                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    chequeprint = "PrintChequeAssure";
                    break;
                case "3344":
                    chequeprint = "PrinChequeTarraNova01";
                    break;

                case "3101":
                case "1103":
                    chequeprint = "PrintCheque01";
                    break;


                default:
                    chequeprint = "PrintCheque01";
                    break;
            }
            return chequeprint;
        }
        private void PrintCheque()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3101":
                case "3337":
                case "3336":
                    this.PrintchKSuvastu();
                    break;

                case "3344":
                    this.PrinChequeTarraNova();
                    break;

                // case "3101":
                case "3305":
                case "3311":
                case "3306":
                case "3309":
                case "2305":
                case "3310":
                    PrinChequeRup();
                    break;

                //case "3101":
                //case "1103":
                //    this.PrinCheque();
                //    break;

                case "1108":
                case "1109":
                case "3315":
                case "3316":
                case "3317":
                    PrinChequeAssure();
                    break;

                //case "3101":
                case "3355":
                    PrinChequeGreenWood();
                    break;

                case "3101":
                case "3368":
                    this.PrintChqFinlay();
                    break;

                default:
                    this.PrinCheque();
                    break;
            }
        }

        private void PrintChqFinlay()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string compName = hst["comnam"].ToString();

                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;

                DataTable dt1 = _ReportDataSet.Tables[0];
                string woutchqdat = "", voudat = "", voudat1 = "";
                if (Request.QueryString.AllKeys.Contains("woutchqdat"))
                {
                    woutchqdat = (this.Request.QueryString["woutchqdat"] == "0") ? "" : "woutchqdat";
                    voudat = woutchqdat.Length > 0 ? "01/01/1900" : Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd/MM/yyyy");
                    voudat1 = woutchqdat.Length > 0 ? "01/01/1900" : Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd/MM/yyyy");
                    if (voudat.Trim() == "01/01/1900")
                    {
                        voudat = "";
                    }
                    if (voudat1.Trim() == "01/01/1900")
                    {
                        voudat1 = "";
                    }
                }
                else
                {
                    voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd/MM/yyyy");
                    voudat1 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd/MM/yyyy");
                }


                string bankcode = dt1.Rows[0]["bnkcode"].ToString();
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string RNaration = _ReportDataSet.Tables[1].Rows[0]["naration"].ToString();
                string Chequeno = _ReportDataSet.Tables[0].Rows[0]["refnum"].ToString();
                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

                string projnam1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? "" : (string)_ReportDataSet.Tables[1].Rows[0]["actdesc"]);
                string projnam2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? "" : (string)_ReportDataSet.Tables[1].Rows[1]["actdesc"]);
                string projnam3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? "" : (string)_ReportDataSet.Tables[1].Rows[2]["actdesc"]);
                string projnam4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? "" : (string)_ReportDataSet.Tables[1].Rows[3]["actdesc"]);
                string projnam5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? "" : (string)_ReportDataSet.Tables[1].Rows[4]["actdesc"]);

                double projamt1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["trnam"]));
                double projamt2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[1]["trnam"]));
                double projamt3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[2]["trnam"]));
                double projamt4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[3]["trnam"]));
                double projamt5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[4]["trnam"]));

                double totalAmount = projamt1 + projamt2 + projamt3 + projamt4 + projamt5;

                string prjDesc = projnam1;

                if (projnam2 != "")
                {
                    prjDesc = String.Join(",", projnam1, projnam2);
                }
                if (projnam3 != "")
                {
                    prjDesc = String.Join(",", projnam1, projnam2, projnam3);
                }
                if (projnam4 != "")
                {
                    prjDesc = String.Join(",", projnam1, projnam2, projnam3, projnam4);
                }
                if (projnam5 != "")
                {
                    prjDesc = String.Join(",", projnam1, projnam2, projnam3, projnam4, projnam5);
                }


                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 6)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["compName"] = compName + ".";
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = "Taka " + wam2;
                hshtbl["amtWord1"] = wam1;
                hshtbl["amt"] = "**" + Convert.ToDouble(amt).ToString("#,##0.00;(#,##0.00); ") + "**";
                hshtbl["date1"] = voudat1;
                hshtbl["naration"] = RNaration.ToUpper();
                hshtbl["Chequeno"] = Chequeno;
                hshtbl["ProjectDesc"] = prjDesc;
                hshtbl["totalAmount"] = Convert.ToDouble(totalAmount).ToString("#,##0;(#,##0); ");

                LocalReport rpt1 = new LocalReport();
                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.PrintChqFinlayBRAC", hshtbl, null, null);


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                string msg = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }

        }
        private void PrinChequeRup()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                //voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";



                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }



                string banktype = dt1.Rows[0]["bnkcode"].ToString();

                ReportDocument rptinfo = new ReportDocument();

                dt1.Rows[0]["acpay"] = value;


                if (Chequeprint == "PrintCheque01")

                    if (banktype == "SBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                    }

                    else if (banktype == "JBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckJBL();

                    }

                    else if (banktype == "MBL" || banktype == "AFL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckMBL();

                    }

                    else if (banktype == "PBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckPBL();

                    }

                    else if (banktype == "SDBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckSDBL();

                    }

                    else if (banktype == "IFIC" || banktype == "BAL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckIFIC();

                    }
                    else
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

                    }


                else if (Chequeprint == "PrintCheque02")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();
                else if (Chequeprint == "PrintChequeAssure")
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();
                else
                    rptinfo = new RealERPRPT.R_17_Acc.PrintCheeque02();

                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = voudat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

                rptinfo.SetDataSource(dt1);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }




        }

        private void PrinChequeAssure()
        {


            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");


                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                // string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("ddMMyyyy");

                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string Chequeprint = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();
                rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();


                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                //voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                //   voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = voudat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = "=" + amt.ToString("#,##0;(#,##0); ") + "/=";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Print Cheque";
                    string eventdesc2 = "Voucher No.: " + vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }




        }

        private void PrinChequeGreenWood()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";



                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 7)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                                          // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";
                LocalReport rpt1 = new LocalReport();
                string banktype = dt1.Rows[0]["bnkcode"].ToString();

                // defult Trust Bank 
                if (banktype == "TBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwood", hshtbl, null, null);
                }
                // Shimanto Bank RptChequeGreenwoodSHBL
                else if (banktype == "SHBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodSHBL", hshtbl, null, null);
                }
                // Shahjalal Islami Bank Ltd RptChequeGreenwoodSHIBL
                else if (banktype == "SHIBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodSHIBL", hshtbl, null, null);
                }
                // Fast Security Bank Ltd RptChequeGreenwoodFSIBL
                else if (banktype == "FSIBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodFSIBL", hshtbl, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwood", hshtbl, null, null);
                }


                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void PrintPostDatedCheque()
        {


            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3344":
                    this.PrinChequePostTarraNova();
                    break;


                case "1108":
                case "1109":
                case "3315":// Assure Builders
                case "3316": //Assure Builders
                    this.PrintChequePostAssure();
                    break;

                //case "3101":
                case "3336":// Assure Builders
                case "3337": //Assure Builders
                    this.PrintChequePostSuvastu();
                    break;



                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3311":
                case "3310":

                    this.RptPostDatChq1(); //Rupayan

                    break;

                //case "3101":// Assure Builders
                case "3355": //Green wood
                    this.PrintChequePostGreenwood();
                    break;


                default:
                    this.PrinChequePost();
                    break;
            }


        }
        private void PrintchKSuvastu()
        {
            //try
            //{
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string vounum = this.Request.QueryString["vounum"].ToString();
            DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
            if (_ReportDataSet == null)
                return;
            DataTable dt1 = _ReportDataSet.Tables[0];
            // string woutchq1 = this.Request.QueryString["woutchqdat"].ToString();

            string woutchqdat = (this.Request.QueryString["woutchqdat"] == "0") ? "" : "woutchqdat";


            string voudat = woutchqdat.Length > 0 ? "01011900" : Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
            string voudat1 = woutchqdat.Length > 0 ? "01011900" : Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd.MM.yyyy");

            if (voudat.Trim() == "01011900")
            {
                voudat = "          ";
            }
            if (voudat1.Trim() == "01.01.1900")
            {
                voudat1 = "";
            }


            string bankcode = dt1.Rows[0]["bnkcode"].ToString();
            string payto = dt1.Rows[0]["payto"].ToString();
            double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
            string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
            int len = amt1.Length;
            string amt2 = amt1.Substring(7, (len - 8));
            string wam1 = string.Empty;
            string wam2 = string.Empty;
            string Chequeprint = this.CompanyPrintCheque();
            string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
            string[] amtdivide = amtWrd1[1].Split(' ');
            string RNaration = _ReportDataSet.Tables[1].Rows[0]["naration"].ToString();
            string Chequeno = _ReportDataSet.Tables[0].Rows[0]["refnum"].ToString();
            string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";



            string projnam1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? "" : (string)_ReportDataSet.Tables[1].Rows[0]["actdesc"]);
            string projnam2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? "" : (string)_ReportDataSet.Tables[1].Rows[1]["actdesc"]);
            string projnam3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? "" : (string)_ReportDataSet.Tables[1].Rows[2]["actdesc"]);
            string projnam4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? "" : (string)_ReportDataSet.Tables[1].Rows[3]["actdesc"]);
            string projnam5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? "" : (string)_ReportDataSet.Tables[1].Rows[4]["actdesc"]);

            double projamt1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["trnam"]));
            double projamt2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[1]["trnam"]));
            double projamt3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[2]["trnam"]));
            double projamt4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[3]["trnam"]));
            double projamt5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[4]["trnam"]));

            // double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);

            double totalAmount = projamt1 + projamt2 + projamt3 + projamt4 + projamt5;

            string prjDesc = projnam1;

            if (projnam2 != "")
            {
                prjDesc = String.Join(",", projnam1, projnam2);
            }
            if (projnam3 != "")
            {
                prjDesc = String.Join(",", projnam1, projnam2, projnam3);
            }
            if (projnam4 != "")
            {
                prjDesc = String.Join(",", projnam1, projnam2, projnam3, projnam4);
            }
            if (projnam5 != "")
            {
                prjDesc = String.Join(",", projnam1, projnam2, projnam3, projnam4, projnam5);
            }


            for (int i = 2; i <= amtdivide.Length - 1; i++)
            {
                if (i == amtdivide.Length)
                {
                    return;
                }
                else if (i > 6)
                {
                    wam1 += " " + amtdivide[i].ToString();
                }
                else
                {
                    wam2 += " " + amtdivide[i].ToString();
                }
            }




            Hashtable hshtbl = new Hashtable();
            hshtbl["bankName"] = "";
            hshtbl["payTo"] = payto;
            hshtbl["acpayee"] = value;
            hshtbl["date"] = voudat;
            hshtbl["amtWord"] = wam2.ToUpper();//.ToUpper();
            hshtbl["amtWord1"] = wam1.ToUpper();//.ToUpper();
                                                // hshtbl["payble"] = value;
            hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";
            hshtbl["projnam1"] = projnam1;
            hshtbl["projnam2"] = projnam2;
            hshtbl["projnam3"] = projnam3;
            hshtbl["projnam4"] = projnam4;
            hshtbl["projnam5"] = projnam5;
            hshtbl["projamt1"] = Convert.ToDouble(projamt1).ToString("#,##0;(#,##0); ");
            hshtbl["projamt2"] = Convert.ToDouble(projamt2).ToString("#,##0;(#,##0); ");
            hshtbl["projamt3"] = Convert.ToDouble(projamt3).ToString("#,##0;(#,##0); ");
            hshtbl["projamt4"] = Convert.ToDouble(projamt4).ToString("#,##0;(#,##0); ");
            hshtbl["projamt5"] = Convert.ToDouble(projamt5).ToString("#,##0;(#,##0); ");
            hshtbl["date1"] = voudat1;
            hshtbl["naration"] = RNaration.ToUpper();
            hshtbl["Chequeno"] = Chequeno;
            hshtbl["ProjectDesc"] = prjDesc;
            hshtbl["totalAmount"] = Convert.ToDouble(totalAmount).ToString("#,##0;(#,##0); ");

            LocalReport rpt1 = new LocalReport();


            //  rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);



            string pbltype = this.Request.QueryString["pbl"].ToString();

            if (pbltype == "1")
            {
                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSuvastuPBL", hshtbl, null, null);
            }
            else if (bankcode == "IBBL")
            {
                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeIBBL", hshtbl, null, null);
            }
            else if (bankcode == "SBL")
            {
                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSBL", hshtbl, null, null);
            }
            else
            {

                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSuvastu", hshtbl, null, null);

                //Commented
                // rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeIBBL", hshtbl, null, null);        


            }

            //rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSBL", hshtbl, null, null);

            Session["Report1"] = rpt1;


            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";


            //}
            //catch (Exception ex)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            //}

        }
        private void PrinCheque()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";



                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 7)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                                          // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";
                LocalReport rpt1 = new LocalReport();

                if (comcod == "3338")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeAcme", hshtbl, null, null);
                }
                else if (comcod == "3353") // cheque manama
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeManama", hshtbl, null, null);
                }
                else if (comcod == "1103") // cheque TCL
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeTCL", hshtbl, null, null);
                }
                else if (comcod == "3348")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheqCredence", hshtbl, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);
                }

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void PrinChequeTarraNova()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString();
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", vounum, "", "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");

                // voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);
                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["tamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

                //if (this.ChBoxCheque.Checked == true)
                //{
                //    string value = this.ChBoxCheque.Text.ToString();
                //}
                //string value = this.ChboxPayee.Checked ? "A/C Payee" : "";
                // double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);

                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 8)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }

                ReportDocument rptinfo = new ReportDocument();
                rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeTarraNova();

                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = voudat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = "= " + amt.ToString("#,##0;(#,##0); ") + "/=";


                rptinfo.SetDataSource(dt1);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }

        private void PrinChequePostTarraNova()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
            string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);
            DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
            if (_ReportDataSet == null)
                return;
            DataTable dt1 = _ReportDataSet.Tables[0];
            double toamt, dramt, cramt;
            string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
            string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
            toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
            string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
            int len = amt1.Length;
            string amt2 = amt1.Substring(7, (len - 8));
            string wam1 = string.Empty;
            string wam2 = string.Empty;
            string Chequeprint = this.CompanyPrintCheque();
            string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
            string[] amtdivide = amtWrd1[1].Split(' ');
            string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

            //if (this.ChBoxCheque.Checked == true)
            //{
            //    string value = this.ChBoxCheque.Text.ToString();
            //}
            //string value = this.ChboxPayee.Checked ? "A/C Payee" : "";
            // double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);

            for (int i = 2; i <= amtdivide.Length - 1; i++)
            {
                if (i == amtdivide.Length)
                {
                    return;
                }
                else if (i > 8)
                {
                    wam1 += " " + amtdivide[i].ToString();
                }
                else
                {
                    wam2 += " " + amtdivide[i].ToString();
                }
            }

            ReportDocument rptinfo = new ReportDocument();
            rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeTarraNova();

            TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            date.Text = voudat;
            TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
            rpttxtpayto.Text = payto;
            TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
            rpttxtamtinword.Text = amt2;
            TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            rpttxtamt.Text = "= " + toamt.ToString("#,##0;(#,##0); ") + "/=";
            rptinfo.SetDataSource(dt1);
            Session["Report1"] = rptinfo;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";






        }

        private void PrintChequePostAssure()
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
                string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];

                string chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                if (chequedat != "01011900")
                {
                    chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " +
                                chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " +
                                chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " +
                                chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);

                }

                else
                {
                    chequedat = "";
                }


                string payto = dt1.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));


                string Chequeprint = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();
                rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeAssure();

                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = chequedat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = "=" + amt.ToString("#,##0;(#,##0); ") + "/=";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Voucher Print";
                    string eventdesc = "Print Cheque";
                    string eventdesc2 = "Voucher No.: " + vounum;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }



        }

        private void RptPostDatChq1()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
                string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);
                string chqwodat = this.Request.QueryString["chqwodat"] == "1" ? "Chequewoutdat" : "";

                //string vounum = this.ddlChqList.SelectedValue.Substring(0, 14);
                //string chqno = this.ddlChqList.SelectedValue.Substring(14);
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                DataTable dt2 = _ReportDataSet.Tables[1];
                //  voudat = voudat.Substring(0, 1) + "   " + voudat.Substring(1, 1) + "   " + voudat.Substring(2, 1) + "   " + voudat.Substring(3, 1) + "   " + voudat.Substring(4, 1) + "   " + voudat.Substring(5, 1) + "   " + voudat.Substring(6, 1) + "   " + voudat.Substring(7, 1);

                string chequedat = "";
                if (chqwodat.Length > 0)
                {

                    chequedat = "01011900";
                }

                else
                {
                    chequedat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");

                }



                if (chequedat != "01011900")
                {


                    chequedat = chequedat.Substring(0, 1) + "   " + chequedat.Substring(1, 1) + "   " + chequedat.Substring(2, 1) + "   " + chequedat.Substring(3, 1) + "   " + chequedat.Substring(4, 1) + "   " + chequedat.Substring(5, 1) + "   " + chequedat.Substring(6, 1) + "   " + chequedat.Substring(7, 1);



                }
                else
                {
                    chequedat = "";
                }
                string payto = dt1.Rows[0]["payto"].ToString();
                string banktype = dt2.Rows[0]["bnkcode"].ToString();

                double amt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));

                string value = (this.Request.QueryString["paytype"] == "1") ? "A/C Payee" : "";

                //string value = ChboxPayee.Checked ? "A/C Payee" : "";
                if (value.Length > 0)
                {
                    _ReportDataSet.Tables[0].Rows[0]["acpay"] = value;
                }
                else
                {
                    _ReportDataSet.Tables[0].Rows[0]["acpay"] = value;
                }

                string type = this.CompanyPrintCheque();
                ReportDocument rptinfo = new ReportDocument();
                if (type == "PrintCheque01")

                    if (banktype == "SBL" || comcod == "3101")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();
                    }

                    else if (banktype == "JBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckJBL();

                    }

                    else if (banktype == "MBL" || banktype == "AFL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckMBL();

                    }

                    else if (banktype == "PBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckPBL();

                    }

                    else if (banktype == "SDBL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckSDBL();




                    }

                    else if (banktype == "IFIC" || banktype == "BAL")
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheckIFIC();

                    }
                    else
                    {
                        rptinfo = new RealERPRPT.R_17_Acc.PrintCheck();

                    }


                TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                date.Text = chequedat;
                TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
                rpttxtpayto.Text = payto;
                TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
                rpttxtamtinword.Text = amt2;
                TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                rpttxtamt.Text = amt.ToString("#,##0;(#,##0); ") + "/=";

                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Post Dated Cheque";
                    string eventdesc = "Print Cheque";
                    string eventdesc2 = vounum + "  " + chqno;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }
                rptinfo.SetDataSource(_ReportDataSet.Tables[0]);
                Session["Report1"] = rptinfo;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_Self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        private void PrintChequePostSuvastu()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();



                string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
                string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);

                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;


                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string voudat1 = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd.MM.yyyy");
                string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
                double toamt, dramt, cramt;

                toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());

                string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

                string RNaration = _ReportDataSet.Tables[1].Rows[0]["naration"].ToString();

                string projnam1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? "" : (string)_ReportDataSet.Tables[1].Rows[0]["actdesc"]);
                string projnam2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? "" : (string)_ReportDataSet.Tables[1].Rows[1]["actdesc"]);
                string projnam3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? "" : (string)_ReportDataSet.Tables[1].Rows[2]["actdesc"]);
                string projnam4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? "" : (string)_ReportDataSet.Tables[1].Rows[3]["actdesc"]);
                string projnam5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? "" : (string)_ReportDataSet.Tables[1].Rows[4]["actdesc"]);

                double projamt1 = ((_ReportDataSet.Tables[1].Rows.Count == 0) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[0]["trnam"]));
                double projamt2 = ((_ReportDataSet.Tables[1].Rows.Count < 2) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[1]["trnam"]));
                double projamt3 = ((_ReportDataSet.Tables[1].Rows.Count < 3) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[2]["trnam"]));
                double projamt4 = ((_ReportDataSet.Tables[1].Rows.Count < 4) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[3]["trnam"]));
                double projamt5 = ((_ReportDataSet.Tables[1].Rows.Count < 5) ? 0 : Convert.ToDouble(_ReportDataSet.Tables[1].Rows[4]["trnam"]));



                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 6)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }

                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                hshtbl["amt"] = toamt.ToString("#,##0;(#,##0); ") + "/-";


                hshtbl["projnam1"] = projnam1;
                hshtbl["projnam2"] = projnam2;
                hshtbl["projnam3"] = projnam3;
                hshtbl["projnam4"] = projnam4;
                hshtbl["projnam5"] = projnam5;
                hshtbl["projamt1"] = Convert.ToDouble(projamt1).ToString("#,##0;(#,##0); ");
                hshtbl["projamt2"] = Convert.ToDouble(projamt2).ToString("#,##0;(#,##0); ");
                hshtbl["projamt3"] = Convert.ToDouble(projamt3).ToString("#,##0;(#,##0); ");
                hshtbl["projamt4"] = Convert.ToDouble(projamt4).ToString("#,##0;(#,##0); ");
                hshtbl["projamt5"] = Convert.ToDouble(projamt5).ToString("#,##0;(#,##0); ");
                hshtbl["date1"] = voudat1;
                hshtbl["naration"] = RNaration;

                LocalReport rpt1 = new LocalReport();

                rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeSuvastu", hshtbl, null, null);

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" + ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }


        }


        private void PrinChequePost()
        {


            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
                string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];


                double toamt, dramt, cramt;
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
                toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";






                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 7)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                                          // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(toamt).ToString("#,##0;(#,##0); ") + "/-";
                LocalReport rpt1 = new LocalReport();

                if (comcod == "3338")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeAcme", hshtbl, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptCheque", hshtbl, null, null);
                }

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }







            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
            //string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);
            //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
            //if (_ReportDataSet == null)
            //    return;
            //DataTable dt1 = _ReportDataSet.Tables[0];
            //double toamt, dramt, cramt;
            //string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
            //string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
            //toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
            //string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
            //int len = amt1.Length;
            //string amt2 = amt1.Substring(7, (len - 8));
            //string wam1 = string.Empty;
            //string wam2 = string.Empty;
            //string Chequeprint = this.CompanyPrintCheque();
            //string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
            //string[] amtdivide = amtWrd1[1].Split(' ');
            //string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

            ////if (this.ChBoxCheque.Checked == true)
            ////{
            ////    string value = this.ChBoxCheque.Text.ToString();
            ////}
            ////string value = this.ChboxPayee.Checked ? "A/C Payee" : "";
            //// double advamt = Convert.ToDouble(dt1.Rows[0]["advamt"]);

            //for (int i = 2; i <= amtdivide.Length - 1; i++)
            //{
            //    if (i == amtdivide.Length)
            //    {
            //        return;
            //    }
            //    else if (i > 8)
            //    {
            //        wam1 += " " + amtdivide[i].ToString();
            //    }
            //    else
            //    {
            //        wam2 += " " + amtdivide[i].ToString();
            //    }
            //}

            //ReportDocument rptinfo = new ReportDocument();
            //rptinfo = new RealERPRPT.R_17_Acc.PrintCheequeTarraNova();

            //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
            //date.Text = voudat;
            //TextObject rpttxtpayto = rptinfo.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
            //rpttxtpayto.Text = payto;
            //TextObject rpttxtamtinword = rptinfo.ReportDefinition.ReportObjects["txtamtinword"] as TextObject;
            //rpttxtamtinword.Text = amt2;
            //TextObject rpttxtamt = rptinfo.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //rpttxtamt.Text = "= " + toamt.ToString("#,##0;(#,##0); ") + "/=";
            //rptinfo.SetDataSource(dt1);
            //Session["Report1"] = rptinfo;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



        }


        private void PrintChequePostGreenwood()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string vounum = this.Request.QueryString["vounum"].ToString().Substring(0, 14);
                string chqno = this.Request.QueryString["vounum"].ToString().Substring(14);
                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_PAYMENT", "RPTPOSTDATCHECK", vounum, chqno, "", "", "", "", "", "", "");
                if (_ReportDataSet == null)
                    return;
                DataTable dt1 = _ReportDataSet.Tables[0];


                double toamt, dramt, cramt;
                string voudat = Convert.ToDateTime(dt1.Rows[0]["chequedat"]).ToString("ddMMyyyy");
                string payto = dt1.Rows[0]["payto"].ToString(); //this.txtRecAndPayto.Text.Trim();
                toamt = Convert.ToDouble(dt1.Rows[0]["trnam"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(toamt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string Chequeprint = this.CompanyPrintCheque();
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(toamt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');
                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";






                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 7)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                                          // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(toamt).ToString("#,##0;(#,##0); ") + "/-";
                LocalReport rpt1 = new LocalReport();

                string banktype = _ReportDataSet.Tables[1].Rows[0]["bnkcode"].ToString();

                // defult Trust Bank 
                if (banktype == "TBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwood", hshtbl, null, null);
                }
                // Shimanto Bank RptChequeGreenwoodSHBL
                else if (banktype == "SHBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodSHBL", hshtbl, null, null);
                }
                // Shahjalal Islami Bank Ltd RptChequeGreenwoodSHIBL
                else if (banktype == "SHIBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodSHIBL", hshtbl, null, null);
                }
                // Fast Security Bank Ltd RptChequeGreenwoodFSIBL
                else if (banktype == "FSIBL")
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodFSIBL", hshtbl, null, null);
                }
                else
                {
                    rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwood", hshtbl, null, null);
                }

                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }

        private void PfVoucherPrint()
        {

            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string combranch = hst["combranch"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string vounum = this.Request.QueryString["vounum"].ToString();
                string session = hst["session"].ToString();
                string PrintInstar = this.GetCompInstar();
                //string pouaction = this.Getpouaction(vounum);
                //string Calltype = (pouaction.Length > 0) ? "PRINTDELETEDVOUCHER01" : "PFPRINTVOUCHER01";
                string Calltype = "PFPRINTVOUCHER01";


                DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACCOUNTS_VOUCHER", Calltype, vounum, PrintInstar, "", "", "", "", "", "", "");
                //
                if (_ReportDataSet == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Not Found');", true);
                    return;
                }
                DataTable dt = _ReportDataSet.Tables[0];


                DataTable dts = _ReportDataSet.Tables[2];

                double dramt, cramt;
                dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Dr)", "")) ? 0.00 : dt.Compute("sum(Dr)", "")));
                cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(Cr)", "")) ? 0.00 : dt.Compute("sum(Cr)", "")));

                if (dramt > 0 && cramt > 0)
                {
                    TAmount = cramt;

                }
                else if (dramt > 0 && cramt <= 0)
                {
                    TAmount = dramt;
                }
                else
                {
                    TAmount = cramt;
                }

                DataTable dt1 = _ReportDataSet.Tables[1];
                string Vounum = dt1.Rows[0]["vounum"].ToString();
                string voudat = Convert.ToDateTime(dt1.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
                string refnum = dt1.Rows[0]["refnum"].ToString();
                string payto = dt1.Rows[0]["payto"].ToString();
                string Partytype = (ASTUtility.Left(vounum, 2) == "BC") ? "Recieved From:" : (ASTUtility.Left(vounum, 2) == "CC") ? "Recieved From:" : "Pay To:";
                string voutype = dt1.Rows[0]["voutyp"].ToString();
                string venar = dt1.Rows[0]["venar"].ToString();
                string Isunum = (dt1.Rows[0]["isunum"]).ToString() == "" ? "" : ASTUtility.Right((dt1.Rows[0]["isunum"]).ToString(), 6);
                string Posteddat = Convert.ToDateTime(dt1.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy");
                string postuser = dt1.Rows[0]["postuser"].ToString();
                string potseson = dt1.Rows[0]["postseson"].ToString();
                string postrmid = dt1.Rows[0]["postrmid"].ToString();
                string receivedBank = "";// dt1.Rows[0]["banknam"].ToString();


                string Type = this.CompanyPrintVou();

                string billno = dt.Rows[0]["billno"].ToString();


                //Signnitory 
                string preby = dts.Select("gcod='01003'").Length > 0 ? (dts.Select("gcod='01003'")[0]["gdesc"]).ToString().Trim() : "";
                string Checkby = dts.Select("gcod='01004'").Length > 0 ? (dts.Select("gcod='01004'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvby2 = dts.Select("gcod='01005'").Length > 0 ? (dts.Select("gcod='01005'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvby1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvbyt1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["gdesc"]).ToString().Trim() : "";
                string aprvbyst1 = dts.Select("gcod='01009'").Length > 0 ? (dts.Select("gcod='01009'")[0]["stdgdesc"]).ToString().Trim() : "";
                string authorizeby = dts.Select("gcod='01010'").Length > 0 ? (dts.Select("gcod='01010'")[0]["gdesc"]).ToString().Trim() : "";








                //string[] billno1;

                //string[] billno;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // dt1.Rows[j]["useridapp"].ToString() == useridapp

                    if (billno == dt.Rows[i]["billno"].ToString())
                    {

                    }

                    else
                    {
                        billno += dt.Rows[i]["billno"].ToString() + (((dt.Rows[i]["billno"].ToString()).Length == 0) ? " " : ", ");
                    }

                }

                int l = billno.Trim().Length;
                billno = (billno.Length == 0) ? "" : billno.Substring(0, l - 1);

                LocalReport Rpt1 = new LocalReport();


                ReportDocument rptinfo = new ReportDocument();

                if (Type == "VocherPrint")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherDefault", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                }




                else if (Type == "VocherPrint1")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher1", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));





                }
                else if (Type == "VocherPrint2")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher2", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));




                }

                else if (Type == "VocherPrint3")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher3", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));



                }

                else if (Type == "VocherPrint4")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));
                }

                else if (Type == "VocherPrint5")
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher5", list, null, null);

                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //Rpt1.EnableExternalImages = true;
                    //Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    //Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    //Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    //Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    //Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    //Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    //Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    //Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    //Rpt1.SetParameters(new ReportParameter("username", postuser));
                    //Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    //Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    //Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    //Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    //Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));


                    //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVoucher5();
                    //TextObject txtComBranch = rptinfo.ReportDefinition.ReportObjects["txtComBranch"] as TextObject;
                    //txtComBranch.Text = (combranch.Length > 0) ? ("Unit: " + combranch) : "";
                    //TextObject txtisunum2 = rptinfo.ReportDefinition.ReportObjects["txtisunum"] as TextObject;
                    //txtisunum2.Text = "Issue No: " + Isunum;
                    //TextObject txtPosteddate2 = rptinfo.ReportDefinition.ReportObjects["entrydate1"] as TextObject;
                    //txtPosteddate2.Text = "Entry Date: " + Posteddat;
                    //TextObject Refnum = rptinfo.ReportDefinition.ReportObjects["Refnum"] as TextObject;
                    //Refnum.Text = "Cheque/Ref. No.: " + refnum;
                    //TextObject rpttxtPartyName = rptinfo.ReportDefinition.ReportObjects["txtPartyName"] as TextObject;
                    //rpttxtPartyName.Text = (payto == "") ? "" : Partytype + " " + payto; //(this.txtPayto.Text.Trim() == "") ? "" : this.lblPayto.Text.Trim() + " " + this.txtPayto.Text.Trim();
                    //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                    //naration.Text = "Narration: " + venar;
                    //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                    //vounum1.Text = "Voucher No.: " + vounum;
                    //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                    //date.Text = "Voucher Date: " + voudat;

                }


                else if (Type == "VocherPrint6")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher6", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));




                }


                else if (Type == "VocherPrintTanvir")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherTanvir", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));




                }

                else if (Type == "VocherPrintBridge")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucherBridge", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));


                }

                else if (Type == "VocherPrintLei")
                {

                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher7", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));





                }


                else if (Type == "VocherPrintSuvastu")
                {

                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {

                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherSuvastu", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));


                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";

                        if (vouno == "BC" || vouno == "CC")
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherSuvastu02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));




                        }
                        else
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherSuvastu03", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtissuno", vouno == "BD" || vouno == "CD" ? "Payment ID : " + dt1.Rows[0]["isunum"].ToString() : ""));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office"));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));





                        }
                    }
                }

                else if (Type == "VocherPrintCredence")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {

                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlliCredence", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));





                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc;

                        prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";



                        if (vouno == "BC" || vouno == "CC")
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli02Credence", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));




                        }
                        else
                        {

                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli03Credence", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtissuno", vouno == "BD" || vouno == "CD" ? "Payment ID : " + dt1.Rows[0]["isunum"].ToString() : ""));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));





                        }
                    }

                }


                else if (Type == "VocherPrintMod")
                {
                    if (ASTUtility.Left(vounum, 2) == "JV")
                    {
                        switch (comcod)
                        {
                            case "1205":// P2P
                            case "3351":// P2P
                            case "3352":// P2P
                                aprvby1 = aprvbyst1;
                                break;

                        }


                        string voutype1 = "";
                        if (comcod == "3353") // for manama
                        {
                            string vouno = vounum.Substring(0, 2);
                            if (vouno == "BC")
                            {
                                voutype1 = "Bank Receive Voucher";
                            }
                            else if (vouno == "CC")
                            {
                                voutype1 = "Cash Receive Voucher";
                            }
                            else
                            {
                                voutype1 = voutype;
                            }
                        }
                        else
                        {
                            voutype1 = voutype;
                        }


                        var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli", list, null, null);
                        Rpt1.EnableExternalImages = true;
                        Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                        Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                        Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                        Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                        Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                        Rpt1.SetParameters(new ReportParameter("username", postuser));
                        Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                        Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3344") ? aprvby2 : Checkby));
                        Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                        Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));


                        /////////////Comment by Parbaz 24.03.2021

                        //rptinfo = new RealERPRPT.R_17_Acc.rptPrintVocherAlli();

                        //TextObject vounum1 = rptinfo.ReportDefinition.ReportObjects["vounum"] as TextObject;
                        //vounum1.Text = "Voucher No.: " + vounum;
                        //TextObject date = rptinfo.ReportDefinition.ReportObjects["date"] as TextObject;
                        //date.Text = "Voucher Date: " + voudat;
                        //TextObject naration = rptinfo.ReportDefinition.ReportObjects["venar"] as TextObject;
                        //naration.Text = "Narration: " + venar;
                        //TextObject txtusername = rptinfo.ReportDefinition.ReportObjects["username"] as TextObject;
                        //txtusername.Text = postuser;

                        //TextObject txtpreby = rptinfo.ReportDefinition.ReportObjects["txtpreby"] as TextObject;
                        //txtpreby.Text = preby;

                        //TextObject txtcheckby = rptinfo.ReportDefinition.ReportObjects["txtcheckby"] as TextObject;
                        //txtcheckby.Text = (comcod == "3344") ? aprvby2 : Checkby;

                        //TextObject txtaprvby1 = rptinfo.ReportDefinition.ReportObjects["txtaprvby1"] as TextObject;
                        //txtaprvby1.Text = aprvby1;


                        //// string comcheckby = comcod == "3340" ? this.ComCheckBy() : "";

                        //TextObject txtauthorizeby = rptinfo.ReportDefinition.ReportObjects["txtauthorizeby"] as TextObject;
                        //txtauthorizeby.Text = authorizeby;

                        ////if (comcod == "3349")//
                        ////{


                        ////    txtpreby.ObjectFormat.EnableSuppress = false;
                        ////    txtcheckby.ObjectFormat.EnableSuppress = false;
                        ////    txtaprvby1.ObjectFormat.EnableSuppress = false;
                        ////    txtauthorizeby.ObjectFormat.EnableSuppress = true;
                        ////}
                        ////else
                        ////{
                        //txtpreby.ObjectFormat.EnableSuppress = preby.Trim().Length == 0;
                        //txtcheckby.ObjectFormat.EnableSuppress = aprvby2.Trim().Length == 0;
                        //txtaprvby1.ObjectFormat.EnableSuppress = aprvby1.Trim().Length == 0;
                        //txtauthorizeby.ObjectFormat.EnableSuppress = authorizeby.Trim().Length == 0;
                        ////}


                    }
                    else
                    {
                        string vouno = vounum.Substring(0, 2);
                        string paytoorecived = (vouno == "BC" || vouno == "CC") ? "Recieved From" : "Pay To";
                        string prjdesc = "";
                        switch (comcod)
                        {
                            case "3339":

                                // || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") 

                                prjdesc = (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "1301" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16"
                                    || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1902") || (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 2) == "29") || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "27"
                                    || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "39" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2201" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2301" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26") ?

                                     (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "1301" ? dt.Rows[0]["actdesc"].ToString().Replace("ADVANCE-", "")

                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" ? dt.Rows[0]["actdesc"].ToString().Replace("WIP-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" ? dt.Rows[0]["actdesc"].ToString().Replace("AR-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "39" ? dt.Rows[0]["actdesc"].ToString().Replace("NOI-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "27" ? dt.Rows[0]["actdesc"].ToString().Replace("NOI-", "")
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1901") ? dt.Rows[0]["actdesc"].ToString().Replace("CASH-", "")
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 4) == "1902") ? dt.Rows[0]["pactdesc"].ToString()
                                    : (ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "0000" && ASTUtility.Left(dt.Rows[0]["actcode"].ToString(), 2) == "29") ? dt.Rows[0]["pactdesc"].ToString()
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2201" ? dt.Rows[0]["actdesc"].ToString().Replace("LOAN-", "")
                                    : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 4) == "2301" ? dt.Rows[0]["actdesc"].ToString().Replace("TVS-", "")
                                     : ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? dt.Rows[0]["actdesc"].ToString().Replace("AP-", "") : "") : "Head Office";
                                break;

                            default:

                                prjdesc = ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "16" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "18" || ASTUtility.Left(dt.Rows[0]["mactcode"].ToString(), 2) == "26" ? "Project" : "Head Office";
                                break;

                        }
                        if (vouno == "BC" || vouno == "CC")
                        {


                            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli02", list, null, null);
                            Rpt1.EnableExternalImages = true;
                            Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                            Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                            Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                            Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                            Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                            Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                            Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                            Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                            Rpt1.SetParameters(new ReportParameter("username", postuser));
                            Rpt1.SetParameters(new ReportParameter("txtRecvby", (comcod == "3333") ? "" : "Received By"));
                            Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                            Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3333") ? "" : Checkby));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                            Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                            Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                            Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                            Rpt1.SetParameters(new ReportParameter("paytoorecived", paytoorecived));






                        }
                        else
                        {

                            if (vouno == "BD" || vouno == "CD" || vouno == "CT")
                            {

                                var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVocherAlli03", list, null, null);
                                Rpt1.EnableExternalImages = true;
                                Rpt1.SetParameters(new ReportParameter("Vounum", vounum));
                                Rpt1.SetParameters(new ReportParameter("voudat", voudat));
                                Rpt1.SetParameters(new ReportParameter("refnum", refnum));
                                Rpt1.SetParameters(new ReportParameter("txtissuno", "Payment ID : " + dt1.Rows[0]["isunum"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtDesc", dt1.Rows[0]["cactdesc"].ToString()));
                                Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : payto));
                                Rpt1.SetParameters(new ReportParameter("txtProject", prjdesc));
                                Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                                Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                                Rpt1.SetParameters(new ReportParameter("username", postuser));
                                Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                                Rpt1.SetParameters(new ReportParameter("txtcheckby", (comcod == "3333") ? "" : Checkby));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                                Rpt1.SetParameters(new ReportParameter("txtaprvby2", aprvby2));
                                Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                                Rpt1.SetParameters(new ReportParameter("txtReceivedBank", receivedBank));
                                Rpt1.SetParameters(new ReportParameter("txtporrecieved", paytoorecived));

                            }




                        }
                    }

                }


                else
                {
                    var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.vouPrint>();

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.rptPrintVoucher4", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    Rpt1.SetParameters(new ReportParameter("Vounum", "Voucher No.: " + vounum));
                    Rpt1.SetParameters(new ReportParameter("voudat", "Voucher Date: " + voudat));
                    Rpt1.SetParameters(new ReportParameter("refnum", "Cheque/Ref. No.: " + refnum));
                    Rpt1.SetParameters(new ReportParameter("txtissuno", "Issue No: " + Isunum));
                    Rpt1.SetParameters(new ReportParameter("txtPartyName", (payto == "") ? "" : Partytype + " " + payto));
                    Rpt1.SetParameters(new ReportParameter("txtComBranch", (combranch.Length > 0) ? ("Unit: " + combranch) : ""));
                    Rpt1.SetParameters(new ReportParameter("voutype", voutype));
                    Rpt1.SetParameters(new ReportParameter("venar", "Narration: " + venar));
                    Rpt1.SetParameters(new ReportParameter("username", postuser));
                    Rpt1.SetParameters(new ReportParameter("txtpreby", preby));
                    Rpt1.SetParameters(new ReportParameter("txtcheckby", Checkby));
                    Rpt1.SetParameters(new ReportParameter("txtaprvby1", aprvby1));
                    Rpt1.SetParameters(new ReportParameter("txtauthorizeby", authorizeby));
                    Rpt1.SetParameters(new ReportParameter("entrydate1", "Entry Date: " + Posteddat));



                }




                if (ConstantInfo.LogStatus == true)
                {

                    string eventdesc = "Print Voucher";
                    string eventdesc2 = "Voucher: " + vounum + " Dated: " + voudat;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, eventdesc2);


                }


                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("InWrd", ASTUtility.Trans(Math.Round(TAmount), 2)));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat1(postrmid, postuser, potseson, Posteddat, compname, username, printdate, session)));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }


        /// BTI Check Print
        /// 
        private void PrinChequeCashSalary()
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();

                string empname = this.Request.QueryString["empname"].ToString();
                string cashamt = this.Request.QueryString["amt"].ToString();
                string ckdate = this.Request.QueryString["ckdate"].ToString();
                string bankcode = this.Request.QueryString["bankcode"].ToString();
                string yearmon = this.Request.QueryString["yearmon"].ToString();
                string daydesc = "Salary" + ASTUtility.Month3digit(Convert.ToInt32(ASTUtility.Right(yearmon, 2))).ToString() + "/ " + ASTUtility.Left(yearmon, 4);

                //DataSet _ReportDataSet = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "PRINTCHECK", "", "", "", "", "", "", "", "", "");
                //if (_ReportDataSet == null)
                //    return;
                //DataTable dt1 = _ReportDataSet.Tables[0];

                string voudat = Convert.ToDateTime(ckdate).ToString("ddMMyyyy");
                string payto = empname;
                double amt = Convert.ToDouble(cashamt);
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);
                int len = amt1.Length;
                string amt2 = amt1.Substring(7, (len - 8));
                string wam1 = string.Empty;
                string wam2 = string.Empty;
                string[] amtWrd1 = ASTUtility.Trans(Math.Round(amt, 0), 2).Split('(', ')');
                string[] amtdivide = amtWrd1[1].Split(' ');

                string value = (this.Request.QueryString["paytype"] == "0") ? "A/C Payee" : "";

                for (int i = 2; i <= amtdivide.Length - 1; i++)
                {
                    if (i == amtdivide.Length)
                    {
                        return;
                    }
                    else if (i > 7)
                    {
                        wam1 += " " + amtdivide[i].ToString();
                    }
                    else
                    {
                        wam2 += " " + amtdivide[i].ToString();
                    }
                }


                Hashtable hshtbl = new Hashtable();
                hshtbl["bankName"] = "";
                hshtbl["payTo"] = payto;
                hshtbl["acpayee"] = value;
                hshtbl["date"] = voudat;
                hshtbl["ckdate"] = ckdate;
                hshtbl["amtWord"] = wam2;//.ToUpper();
                hshtbl["amtWord1"] = wam1;//.ToUpper();
                // hshtbl["payble"] = value;
                hshtbl["amt"] = Convert.ToDouble(amt).ToString("#,##0;(#,##0); ") + "/-";
                hshtbl["prjdesc"] = daydesc;
                LocalReport rpt1 = new LocalReport();
                string banktype = bankcode;
                switch (comcod)
                {
                    case "3365":
                        rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeOneBankBti", hshtbl, null, null);
                        break;
                    default:
                        // defult Trust Bank 
                        if (banktype == "TBL")
                        {
                            rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwood", hshtbl, null, null);
                        }
                        // Shimanto Bank RptChequeGreenwoodSHBL
                        else if (banktype == "SHBL")
                        {
                            rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodSHBL", hshtbl, null, null);
                        }
                        // Shahjalal Islami Bank Ltd RptChequeGreenwoodSHIBL
                        else if (banktype == "SHIBL")
                        {
                            rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodSHIBL", hshtbl, null, null);
                        }
                        // Fast Security Bank Ltd RptChequeGreenwoodFSIBL
                        else if (banktype == "FSIBL")
                        {
                            rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwoodFSIBL", hshtbl, null, null);
                        }
                        else
                        {
                            rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptChequeGreenwood", hshtbl, null, null);
                        }
                        break;

                }
                Session["Report1"] = rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            catch (Exception ex)
            {
                string msg = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }


        }

    }
}
