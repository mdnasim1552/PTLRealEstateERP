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
    public partial class LinkDuesColl : System.Web.UI.Page
    {
        ProcessAccess SalData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "DoueCollAll") ? "Delay Charge"
                    : (this.Request.QueryString["Type"].ToString() == "SpLedger") ? "Subsidiary Ledger Report" : "";

                this.SelectView();


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (this.Request.QueryString["comcod"].ToString());

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "DoueCollAll":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblDelayCharge.Visible = true;
                    this.lblchqdishonour.Visible = true;
                    this.ShowDelayRate();
                    this.ShowInterest();
                    break;
                case "ClientLedger":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.PrintCleintLedger();
                    break;
                case "CustInvoice":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowInvoice();
                    break;


            }
        }
        private void ShowDelayRate()
        {

            string comcod = this.GetCompCode();

            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETDELAYRATEPERMON", "", "", "", "", "", "", "", "", "");
            this.lblInterPar.Text = Convert.ToDouble(ds2.Tables[0].Rows[0]["inpermonth"]).ToString("#,##0.00;(#,##0.00); ") + "%";
            ds2.Dispose();
            this.CompanyDaleyRate();

        }
        private void CompanyDaleyRate()
        {

            //Interest Per Month(%):
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3338":
                case "3101":
                    this.lblinterest.Text = "Interest Per Year(%):";
                    break;


                default:
                    this.lblinterest.Text = "Interest Per Month(%):";
                    break;



            }


        }

        private void ShowInterest()
        {

            ViewState.Remove("tblinterest");
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();

            string date = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");
            string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTINTEREST", pactcode, custid, frmdate, todate, "", "", "", "", "");


            if (ds2 == null)
            {
                this.gvInterest.DataSource = null;
                this.gvInterest.DataBind();
                this.gvCDHonour.DataSource = null;
                this.gvCDHonour.DataBind();
                this.gvChqnoclin.DataSource = null;
                this.gvChqnoclin.DataBind();
                return;
            }
            this.lblProjectName.Text = ds2.Tables[1].Rows[0]["pactdesc"].ToString();
            this.LblCutName.Text = ds2.Tables[1].Rows[0]["usirdesc"].ToString();
            this.lblDate.Text = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");
            ViewState["tblinterest"] = ds2.Tables[0];
            this.Data_Bind();
            ds2.Dispose();


        }


        private void PrintCleintLedger()
        {




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {
                //case "3101":
                case "3330":
                    this.PrintCleintLedgerBr();
                    break;
                // case "3101":
                case "3336":
                case "3337":

                    this.PrintCleintLedgerSuvastu();
                    break;


                case "3339":
                //case "3101":
                    this.PrintCleintLedgerTropical();
                    break;


                case "3325":
                case "2325":
                    this.PrintCleintLedgerLeisure();
                    break;

                //case "3101":
                case "2305":
                case "3305":
                case "3306":
                case "3311":
                case "3310":
                    this.PrintCleintLedgerRupayan();
                    break;

                case "3348"://Credence
              //  case "3101":
                case "3353":// Manama
                case "3355":// Manama
                    this.PrintCleintLedgerManama();
                    break;


                default:
                    this.PrintCleintLedgergen();
                    break;
            }
        }



        private void PrintCleintLedgerBr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            string custid = Request.QueryString["usircode"].ToString().Trim();
            string Date = Request.QueryString["Date1"].ToString().Trim();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double associafeerec = Convert.ToDouble(ds5.Tables[1].Rows[0]["associarec"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);

            double others = Convert.ToDouble(ds5.Tables[1].Rows[0]["transother"]);
            //Sales Part  
            double totalsales = aprment + carparking + utility;
            double totalreceevable = totalsales + delcharge + modicharge + regisfee + transfee + others;
            double totalrecived = Convert.ToDouble((Convert.IsDBNull(ds5.Tables[0].Compute("Sum(paidamt)", "")) ? 0.00 : ds5.Tables[0].Compute("Sum(paidamt)", "")));
            double balance = totalreceevable - totalrecived;
            // Association part
            double associabal = assciationfee - associafeerec;
            double netbal = balance + associabal;


            // rdlc start

            //string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            //string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            //string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-mmm-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerBridge02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txttoSalevalue", totalsales.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptdelcharge", rptdelcharge));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", totalreceevable.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txttoreceivedvalue", totalrecived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("duebalanceA", balance.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("assciationfee", assciationfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("assciationfeeRecv", associafeerec.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("duebalanceB", associabal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private string ClientCalltype()
        {
            bool result = false;
            string Calltype = "";
            switch (result)
            {
                case true:
                    Calltype = "RPTCLIENTLEDGER";
                    break;

                default:
                    Calltype = "INSTALLMANTWITHMRR";
                    break;


            }


            return Calltype;

        }


        private void PrintCleintLedgerSuvastu()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string Date = this.Request.QueryString["Date1"].ToString();
            string CallType = this.ClientCalltype();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");



            string frmdate = Date;


            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTINTEREST", pactcode, custid, frmdate, frmdate, "", "", "", "", "");

            DataView dv;
            dv = ds2.Tables[0].DefaultView;
            dv.RowFilter = ("grp='A' and interest>0");
            DataTable dt1 = dv.ToTable();

            dv = ds2.Tables[0].DefaultView;
            dv.RowFilter = ("grp='A' and interest<0");
            DataTable dt2 = dv.ToTable();


            double delcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(interest)", "")) ? 0.00
                    : dt1.Compute("Sum(interest)", "")));

            double ebenamt = (Convert.ToDouble((Convert.IsDBNull(dt2.Compute("Sum(interest)", "")) ? 0.00
                     : dt2.Compute("Sum(interest)", "")))) * -1;



            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double asondues = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(asondues)", "")) ? 0.00
                    : tblins.Compute("Sum(asondues)", "")));

            double receivableTotal = treceived - asondues;
            string totalRceeivable = receivableTotal.ToString("#,##0;(#,##0); ");
            // May be Problem
            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            //double reconamt = treceived - (fcheque + retcheque + pcheque);
            double reconamt = treceived - (fcheque + pcheque);

            double netbal = tsalevalue - reconamt;

            string aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            string carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            string utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
            string modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]).ToString("#,##0;(#,##0); ");
            //double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            string regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0); ");
            string assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
            string transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]).ToString("#,##0;(#,##0); ");

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            string txttosalevalue = tsalevalue.ToString("#,##0;(#,##0); ");
            string txttoreceivedvalue = treceived.ToString("#,##0;(#,##0); ");
            string txtfcheque = fcheque.ToString("#,##0;(#,##0); ");
            string txtpcheque = pcheque.ToString("#,##0;(#,##0); ");
            string txtreconamt = reconamt.ToString("#,##0;(#,##0); ");
            string txtnetbalance = netbal.ToString("#,##0;(#,##0); ");

            string rpttxtcompanyname = comnam;
            string rptcompadd = comadd;
            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-mmm-yyyy");

            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy");

            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy");

            string rptapartmentprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            string rptcarparking = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            string rptutility = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
            string rptregistration = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
            string rptdevelopmentcost = Convert.ToDecimal(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
            string rptdelcharge = (delcharge > 0) ? ("delay charge: " + delcharge.ToString("#,##0;(#,##0); ")) : "";
            string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;
            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;
            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptSalClPayDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));

            Rpt1.SetParameters(new ReportParameter("aprment", aprment));
            Rpt1.SetParameters(new ReportParameter("carparking", carparking));
            Rpt1.SetParameters(new ReportParameter("utility", utility));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee));
            Rpt1.SetParameters(new ReportParameter("assciationfee", assciationfee));
            Rpt1.SetParameters(new ReportParameter("transfee", transfee));
            Rpt1.SetParameters(new ReportParameter("rptwelfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rpttoprice", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("txttoSalevalue", txttosalevalue));
            Rpt1.SetParameters(new ReportParameter("txttoreceivedvalue", txttoreceivedvalue));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", txtfcheque));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", txtpcheque));

            Rpt1.SetParameters(new ReportParameter("txtreconamt", txtreconamt));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", txtnetbalance));

            Rpt1.SetParameters(new ReportParameter("rpttxtCompanyName", rpttxtcompanyname));
            Rpt1.SetParameters(new ReportParameter("rptcompadd", rptcompadd));
            Rpt1.SetParameters(new ReportParameter("rptunittype", rptunittype));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));
            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));

            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", rptapartmentprice));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", rptcarparking));
            Rpt1.SetParameters(new ReportParameter("rptUtility", rptutility));
            Rpt1.SetParameters(new ReportParameter("rptregistration", rptregistration));
            Rpt1.SetParameters(new ReportParameter("rptdevelopmentcost", rptdevelopmentcost));

            Rpt1.SetParameters(new ReportParameter("rptdelcharge", rptdelcharge));
            Rpt1.SetParameters(new ReportParameter("txtearlyben", txtearlyben));

            Rpt1.SetParameters(new ReportParameter("txtuserinfo", printFooter));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", totalRceeivable));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintCleintLedgerTropical()
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string Date = this.Request.QueryString["Date1"].ToString();
            string CallType = this.ClientCalltype();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");
            DataTable dt = this.HiddenSameDate2(ds5.Tables[0]);

            string custname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string pactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();
            string udesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string usize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
        //    string unit = ds5.Tables[1].Rows[0]["unit"].ToString();


            //DataTable dt = ((DataTable)Session["tblCustPayment"]).Copy();


            //Discount 

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("dischk=1");
            DataTable dt1 = dv.ToTable();

            string disinfo = "";
            foreach (DataRow dr1 in dt1.Rows)
            {

                disinfo = "MR" + dr1["mrno"] + "-" + dr1["rmrks"] + ", ";


            }


            disinfo = disinfo.Length > 0 ? ("Discount:" + disinfo.Substring(0, disinfo.Length - 2)) : "";


            //var lst = dt.DataTableToList< C_23_CRR.EClassSales_03.DueCollStatmentRe>();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerTropical", lst, null, null);
            //string usircode = this.ddlCustName.SelectedValue.ToString();
            //DataRow[] drc = ((DataTable)ViewState["tblcustomer"]).Select("custid='" + usircode + "'");

          //  string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim();
           // string custname = drc[0]["custname"].ToString();
            //string udesc = drc[0]["udesc"].ToString();
            //string usize = Convert.ToDouble(drc[0]["usize"].ToString()).ToString("#,##0;(#,##0); ") + " " + drc[0]["unit"].ToString();



            //  Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtComName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtcomadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtProject", pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtcustomer", custname));
            Rpt1.SetParameters(new ReportParameter("txtunit", udesc));
            Rpt1.SetParameters(new ReportParameter("txtunitsize", usize));
            Rpt1.SetParameters(new ReportParameter("txtdisinfo", disinfo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //DataTable dt = ((DataTable)Session["tblCustPayment"]).Copy();


            ////Discount 

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("dischk=1");
            //DataTable dt1 = dv.ToTable();

            //string disinfo = "";
            //foreach (DataRow dr1 in dt1.Rows)
            //{

            //    disinfo = "MR" + dr1["mrno"] + "-" + dr1["rmrks"] + ", ";


            //}


            //disinfo = disinfo.Length > 0 ? ("Discount:" + disinfo.Substring(0, disinfo.Length - 2)) : "";


            ////var lst = dt.DataTableToList< C_23_CRR.EClassSales_03.DueCollStatmentRe>();
            //var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>();
            //LocalReport Rpt1 = new LocalReport();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerTropical", lst, null, null);
            //string usircode =this.Request.QueryString["usircode"];
            //DataRow[] drc = ((DataTable)ViewState["tblcustomer"]).Select("custid='" + usircode + "'");

            //string pactdesc = this.ddlProjectName.SelectedItem.Text.Trim();
            //string custname = drc[0]["custname"].ToString();
            //string udesc = drc[0]["udesc"].ToString();
            //string usize = Convert.ToDouble(drc[0]["usize"].ToString()).ToString("#,##0;(#,##0); ") + " " + drc[0]["unit"].ToString();



            ////  Rpt1.EnableExternalImages = true;
            //Rpt1.SetParameters(new ReportParameter("txtComName", comnam));
            //Rpt1.SetParameters(new ReportParameter("txtcomadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("txtProject", pactdesc));
            //Rpt1.SetParameters(new ReportParameter("txtcustomer", custname));
            //Rpt1.SetParameters(new ReportParameter("txtunit", udesc));
            //Rpt1.SetParameters(new ReportParameter("txtunitsize", usize));
            //Rpt1.SetParameters(new ReportParameter("txtdisinfo", disinfo));
            //Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintCleintLedgerLeisure()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string Date = this.Request.QueryString["Date1"].ToString();

            string CallType = this.ClientCalltype();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-mmm-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-mmm-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-mmm-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerLeisure02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }


        private void PrintCleintLedgerRupayan()
        {

            // rdlc start

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string Date = this.Request.QueryString["Dat1"].ToString();

            string CallType = this.ClientCalltype();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string email = ds5.Tables[1].Rows[0]["custemail"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();


            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerRup02", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptCustemail", email));

            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("rptPrjadd", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintCleintLedgerManama()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string Date = this.Request.QueryString["Date1"].ToString();

            string CallType = this.ClientCalltype();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            LocalReport Rpt1 = new LocalReport();
            var lst1 = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassClientLedger>();
            var lst2 = ds5.Tables[4].DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassRevenue>();
            var lst3 = ds5.Tables[5].DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassRevenue>();
            double tobuildingamt = lst2.Sum(l => l.uamt);
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedgerManama", lst1, lst2, lst3);


            DataTable dtsum = ds5.Tables[2];
            //double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            //double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
            //        : tblins.Compute("Sum(paidamt)", "")));

            //// May be Problem
            //double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            //double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            //double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            //double reconamt = treceived - (fcheque + retcheque + pcheque);
            //double netbal = tsalevalue - reconamt;
            string pactdesc = "Project : " + ds5.Tables[1].Rows[0]["projectname"].ToString();





            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam));
            Rpt1.SetParameters(new ReportParameter("compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtprojectname", pactdesc));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Print Date: " + Date));


            Rpt1.SetParameters(new ReportParameter("txtcustcode", ds5.Tables[1].Rows[0]["custcode"].ToString()));
            Rpt1.SetParameters(new ReportParameter("custname", ds5.Tables[1].Rows[0]["name"].ToString()));
            Rpt1.SetParameters(new ReportParameter("CustAdd", ds5.Tables[1].Rows[0]["peraddress"].ToString()));
            Rpt1.SetParameters(new ReportParameter("CustPhone", ds5.Tables[1].Rows[0]["telephone"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtEmail", ds5.Tables[1].Rows[0]["custemail"].ToString()));


            Rpt1.SetParameters(new ReportParameter("txtprojectcode", ds5.Tables[1].Rows[0]["procode"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtproadd", ds5.Tables[1].Rows[0]["proadd"].ToString()));
            Rpt1.SetParameters(new ReportParameter("UnitDesc", ds5.Tables[1].Rows[0]["aptname"].ToString()));
            Rpt1.SetParameters(new ReportParameter("usize", ds5.Tables[1].Rows[0]["aptsize"].ToString()));
            Rpt1.SetParameters(new ReportParameter("Salesteam", ds5.Tables[1].Rows[0]["salesteam"].ToString()));
            Rpt1.SetParameters(new ReportParameter("salesdate", Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("agreementdate", (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Handoverdate", (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy")));

            Rpt1.SetParameters(new ReportParameter("TotalBuildingAmt", tobuildingamt.ToString("#,##0;(#,##0); ")));


            //Rpt1.SetParameters(new ReportParameter("txttoSalevalue", tsalevalue.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txttoreceivedvalue", treceived.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtretcheque", retcheque.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtreconamt", reconamt.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtDevelopedby", ASTUtility.Cominformation()));



            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";












        }

        private void PrintCleintLedgergen()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetCompCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string Date = this.Request.QueryString["Date1"].ToString();

            string CallType = this.ClientCalltype();
            DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedger02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        //private void PrintCleintLedger()
        //{




        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    switch (comcod)
        //    {

        //        case "3101":
        //        case "3330":

        //            this.PrintCleintLedgerBr();
        //            break;



        //        default:
        //            this.PrintCleintLedgergen(); ;
        //            break;
        //    }





        //}

        //private void PrintCleintLedgergen()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string comcod = this.GetCompCode();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string pactcode = Request.QueryString["pactcode"].ToString().Trim();
        //    string custid = Request.QueryString["usircode"].ToString().Trim();

        //    string date = Request.QueryString["Date1"].ToString().Trim();
        //    DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, date, "", "", "", "", "", "");
        //    if (ds5.Tables[0].Rows.Count == 0)
        //        return;
        //    DataTable tblins = this.HiddenSameData(ds5.Tables[0]);

        //    this.LblPrjDesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
        //    this.lblCustName.Text = ds5.Tables[1].Rows[0]["name"].ToString();
        //    this.lblDate1.Text = Request.QueryString["Date1"].ToString().Trim();


        //    ReportDocument rptStatus = new RealERPRPT.R_23_CR.RptClientLedger();
        //    TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rpttxtCompanyName.Text = comnam;
        //    TextObject rptcompadd = rptStatus.ReportDefinition.ReportObjects["compadd"] as TextObject;
        //    rptcompadd.Text = comadd;

        //    TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtDate.Text = "Print Date: " + Request.QueryString["Date1"].ToString().Trim();
        //    TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
        //    rptcustname.Text = ds5.Tables[1].Rows[0]["name"].ToString();
        //    TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
        //    rptCustAdd.Text = ds5.Tables[1].Rows[0]["peraddress"].ToString();
        //    TextObject rptCustPhone = rptStatus.ReportDefinition.ReportObjects["CustPhone"] as TextObject;
        //    rptCustPhone.Text = ds5.Tables[1].Rows[0]["telephone"].ToString();
        //    TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
        //    rptpactdesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
        //    TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["UnitDesc"] as TextObject;
        //    rptUnitDesc.Text = ds5.Tables[1].Rows[0]["aptname"].ToString();
        //    TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
        //    rptUsize.Text = ds5.Tables[1].Rows[0]["aptsize"].ToString();

        //    TextObject rptSalesteam = rptStatus.ReportDefinition.ReportObjects["Salesteam"] as TextObject;
        //    rptSalesteam.Text = ds5.Tables[1].Rows[0]["salesteam"].ToString();
        //    TextObject rptsalesdate = rptStatus.ReportDefinition.ReportObjects["salesdate"] as TextObject;
        //    rptsalesdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
        //    TextObject rptagreementdate = rptStatus.ReportDefinition.ReportObjects["agreementdate"] as TextObject;
        //    rptagreementdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
        //    TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
        //    rptHandoverdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

        //    TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
        //    rptapartmentprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
        //    rptcarparking.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
        //    rptUtility.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptregistration = rptStatus.ReportDefinition.ReportObjects["registration"] as TextObject;
        //    rptregistration.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
        //    TextObject rptdevelopmentcost = rptStatus.ReportDefinition.ReportObjects["developmentcost"] as TextObject;
        //    rptdevelopmentcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptwelfarefund = rptStatus.ReportDefinition.ReportObjects["welfarefund"] as TextObject;
        //    rptwelfarefund.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptOthers = rptStatus.ReportDefinition.ReportObjects["Others"] as TextObject;
        //    rptOthers.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");

        //    TextObject rpttoprice = rptStatus.ReportDefinition.ReportObjects["toprice"] as TextObject;
        //    rpttoprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");

        //    TextObject rptdiscount = rptStatus.ReportDefinition.ReportObjects["discount"] as TextObject;
        //    rptdiscount.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");
        //    //TextObject rptaccost = rptStatus.ReportDefinition.ReportObjects["accost"] as TextObject;
        //    //rptaccost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
        //    //---------
        //    TextObject rptbudgcost = rptStatus.ReportDefinition.ReportObjects["bgdcost"] as TextObject;
        //    rptbudgcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["bgcost"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptcooperative = rptStatus.ReportDefinition.ReportObjects["coopcost"] as TextObject;
        //    rptcooperative.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");


        //    // Summary Total

        //    DataTable dtsum = ds5.Tables[2];
        //    double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
        //    double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
        //            : tblins.Compute("Sum(paidamt)", "")));
        //    // May be Problem
        //    double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
        //    double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
        //    double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
        //    double reconamt = treceived - (fcheque + retcheque + pcheque);
        //    double netbal = tsalevalue - reconamt;

        //    TextObject txttoSalevalue = rptStatus.ReportDefinition.ReportObjects["txttoSalevalue"] as TextObject;
        //    txttoSalevalue.Text = tsalevalue.ToString("#,##0;(#,##0); ");
        //    TextObject txttoreceivedvalue = rptStatus.ReportDefinition.ReportObjects["txttoreceivedvalue"] as TextObject;
        //    txttoreceivedvalue.Text = treceived.ToString("#,##0;(#,##0); ");
        //    TextObject txtfcheque = rptStatus.ReportDefinition.ReportObjects["txtfcheque"] as TextObject;
        //    txtfcheque.Text = fcheque.ToString("#,##0;(#,##0); ");
        //    TextObject txtretcheque = rptStatus.ReportDefinition.ReportObjects["txtretcheque"] as TextObject;
        //    txtretcheque.Text = retcheque.ToString("#,##0;(#,##0); ");
        //    TextObject txtpcheque = rptStatus.ReportDefinition.ReportObjects["txtpcheque"] as TextObject;
        //    txtpcheque.Text = pcheque.ToString("#,##0;(#,##0); ");
        //    TextObject txtreconamt = rptStatus.ReportDefinition.ReportObjects["txtreconamt"] as TextObject;
        //    txtreconamt.Text = reconamt.ToString("#,##0;(#,##0); ");
        //    TextObject txtnetbalance = rptStatus.ReportDefinition.ReportObjects["txtnetbalance"] as TextObject;
        //    txtnetbalance.Text = netbal.ToString("#,##0;(#,##0); ");



        //    TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptStatus.SetDataSource(tblins);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptStatus.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptStatus;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        //}
        //private void PrintCleintLedgerBr()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string comcod = this.GetCompCode();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string pactcode = Request.QueryString["pactcode"].ToString().Trim();
        //    string custid = Request.QueryString["usircode"].ToString().Trim();


        //    string date = Request.QueryString["Date1"].ToString().Trim();
        //    DataSet ds5 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, date, "", "", "", "", "", "");
        //    if (ds5.Tables[0].Rows.Count == 0)
        //        return;

        //    DataTable tblins = this.HiddenSameData(ds5.Tables[0]);



        //    double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
        //    double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
        //    double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
        //    double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
        //    double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
        //    double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
        //    double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
        //    double associafeerec = Convert.ToDouble(ds5.Tables[1].Rows[0]["associarec"]);
        //    double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
        //    double others = Convert.ToDouble(ds5.Tables[1].Rows[0]["transother"]);


        //    //Sales Part  
        //    double totalsales = aprment + carparking + utility;
        //    double totalreceevable = totalsales + delcharge + modicharge + regisfee + transfee + others;
        //    double totalrecived = Convert.ToDouble((Convert.IsDBNull(ds5.Tables[0].Compute("Sum(paidamt)", "")) ? 0.00 : ds5.Tables[0].Compute("Sum(paidamt)", "")));
        //    double balance = totalreceevable - totalrecived;

        //    // Association part
        //    double associabal = assciationfee - associafeerec;
        //    double netbal = balance + associabal;






        //    ReportDocument rptStatus = new ReportDocument();
        //    rptStatus = new RealERPRPT.R_23_CR.RptClientLedgerBridge();
        //    TextObject rptadwrk = rptStatus.ReportDefinition.ReportObjects["rptadwrk"] as TextObject;
        //    rptadwrk.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["adwrk"]).ToString("#,##0;(#,##0); ");

        //    TextObject rptdelchg = rptStatus.ReportDefinition.ReportObjects["rptdelchg"] as TextObject;
        //    rptdelchg.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["delchg"]).ToString("#,##0;(#,##0); ");

        //    TextObject rpttransfee = rptStatus.ReportDefinition.ReportObjects["rpttransfee"] as TextObject;
        //    rpttransfee.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["transfee"]).ToString("#,##0;(#,##0); ");

        //    TextObject rpttransfeeothers = rptStatus.ReportDefinition.ReportObjects["rpttransfeeothers"] as TextObject;
        //    rpttransfeeothers.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["transother"]).ToString("#,##0;(#,##0); ");

        //    TextObject rpttotalsales = rptStatus.ReportDefinition.ReportObjects["rpttotalsales"] as TextObject;
        //    rpttotalsales.Text = totalsales.ToString("#,##0;(#,##0); ");

        //    TextObject rpttotalreceevable = rptStatus.ReportDefinition.ReportObjects["rpttotalreceevable"] as TextObject;
        //    rpttotalreceevable.Text = totalreceevable.ToString("#,##0;(#,##0); ");

        //    TextObject rpttotalrecieved = rptStatus.ReportDefinition.ReportObjects["rpttotalrecieved"] as TextObject;
        //    rpttotalrecieved.Text = totalrecived.ToString("#,##0;(#,##0); ");

        //    TextObject rpttBalance = rptStatus.ReportDefinition.ReportObjects["rpttBalance"] as TextObject;
        //    rpttBalance.Text = balance.ToString("#,##0;(#,##0); ");
        //    TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rpttxtCompanyName.Text = comnam;
        //    TextObject rptcompadd = rptStatus.ReportDefinition.ReportObjects["compadd"] as TextObject;
        //    rptcompadd.Text = comadd;

        //    TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtDate.Text = "Print Date: " + date;
        //    TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
        //    rptcustname.Text = ds5.Tables[1].Rows[0]["name"].ToString();
        //    TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
        //    rptCustAdd.Text = ds5.Tables[1].Rows[0]["peraddress"].ToString();
        //    TextObject rptCustPhone = rptStatus.ReportDefinition.ReportObjects["CustPhone"] as TextObject;
        //    rptCustPhone.Text = ds5.Tables[1].Rows[0]["telephone"].ToString();
        //    TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
        //    rptpactdesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
        //    TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["UnitDesc"] as TextObject;
        //    rptUnitDesc.Text = ds5.Tables[1].Rows[0]["aptname"].ToString();
        //    TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
        //    rptUsize.Text = ds5.Tables[1].Rows[0]["aptsize"].ToString();
        //    TextObject txtproaddress = rptStatus.ReportDefinition.ReportObjects["txtproaddress"] as TextObject;
        //    txtproaddress.Text = ds5.Tables[1].Rows[0]["proadd"].ToString();



        //    TextObject rptSalesteam = rptStatus.ReportDefinition.ReportObjects["Salesteam"] as TextObject;
        //    rptSalesteam.Text = ds5.Tables[1].Rows[0]["salesteam"].ToString();
        //    TextObject rptsalesdate = rptStatus.ReportDefinition.ReportObjects["salesdate"] as TextObject;
        //    rptsalesdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
        //    TextObject rptagreementdate = rptStatus.ReportDefinition.ReportObjects["agreementdate"] as TextObject;
        //    rptagreementdate.Text = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
        //    TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
        //    rptHandoverdate.Text = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-Jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

        //    TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
        //    rptapartmentprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
        //    rptcarparking.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
        //    TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
        //    rptUtility.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");

        //    TextObject rptregistration = rptStatus.ReportDefinition.ReportObjects["registration"] as TextObject;
        //    rptregistration.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
        //    TextObject rptdevelopmentcost = rptStatus.ReportDefinition.ReportObjects["developmentcost"] as TextObject;
        //    rptdevelopmentcost.Text = assciationfee.ToString("#,##0;(#,##0); ");




        //    TextObject txtassociafeereceipt = rptStatus.ReportDefinition.ReportObjects["txtassociafeereceipt"] as TextObject;
        //    txtassociafeereceipt.Text = associafeerec.ToString("#,##0;(#,##0); ");

        //    TextObject txtassociabal = rptStatus.ReportDefinition.ReportObjects["txtassociabal"] as TextObject;
        //    txtassociabal.Text = associabal.ToString("#,##0;(#,##0); ");
        //    TextObject txtnetbalance = rptStatus.ReportDefinition.ReportObjects["txtnetbalance"] as TextObject;
        //    txtnetbalance.Text = netbal.ToString("#,##0;(#,##0); ");




        //    // Summary Total






        //    TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptStatus.SetDataSource(tblins);
        //    //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    //rptStatus.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptStatus;
        //    //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //    //                  this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        //}
        private void ShowInvoice()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            string fromdate = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");


            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTINVOICELETTER", pactcode, custid, fromdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvCustInvoice.DataSource = null;
                this.gvCustInvoice.DataBind();
                return;
            }
            this.lblInprjDesc.Text = ds2.Tables[1].Rows[0]["pactdesc"].ToString();
            this.lblInCustDesc.Text = ds2.Tables[1].Rows[0]["unitdesc"].ToString();
            this.lblInDate.Text = "(From" + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy") + ")";

            ViewState["tblinterest"] = ds2;

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt = (ds2.Tables[0]).Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'A'");
            dt1 = dv.ToTable();
            this.gvCustInvoice.DataSource = dt1;
            this.gvCustInvoice.DataBind();
            this.FooterCalculation(dt1);




            dv = dt.DefaultView;
            dv.RowFilter = ("grp = 'B'");
            dt1 = dv.ToTable();
            this.gvChqnocl.DataSource = dt1;
            this.gvChqnocl.DataBind();
            this.lblchequenotyetcl.Visible = false;
            if (dt1.Rows.Count > 0)
            {
                this.lblchequenotyetcl.Visible = true;
                ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(predue)", "")) ?
                  0.00 : dt1.Compute("Sum(predue)", ""))).ToString("#,##0;(#,##0); ");

            }

        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ClientLedger":

                    string gcod = dt1.Rows[0]["gcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }

                    int torow = dt1.Rows.Count - 1;

                    if (torow > 0)
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {

                            if (j == torow)
                            {
                                double ppaidamt = Convert.ToDouble(dt1.Rows[j - 1]["paidamt"].ToString());
                                if (ppaidamt > 0)
                                {

                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt1"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["paidamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schamt - (paidamt > 0 ? paidamt : 0.00);
                                }
                            }
                            else
                            {

                                double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                                if (npaidamt > 0)
                                {
                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schamt - paidamt;
                                }
                            }
                        }

                    break;

            }
            return dt1;
        }

        private DataTable HiddenSameDate2(DataTable dtable)
        {

            Session.Remove("tblCustPayment");
            string gcod = dtable.Rows[0]["gcod"].ToString();
            string Type = this.Request.QueryString["Type"].ToString();
            DataTable dt1 = dtable;

            switch (Type)
            {
                case "Payment":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }
                    break;

                case "ClPayDetails":
                case "ClientLedger":


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["asondues"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }

                    int torow = dt1.Rows.Count - 1;

                    if (torow > 0)
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {

                            if (j == torow)
                            {
                                double ppaidamt = Convert.ToDouble(dt1.Rows[j - 1]["paidamt"].ToString());

                                DateTime schdate = System.DateTime.Today;
                                //string schdate = System.DateTime.Now;

                                if (dt1.Rows[j - 1]["schdate"].ToString().Trim().Length > 0)
                                {
                                    schdate = Convert.ToDateTime(dt1.Rows[j - 1]["schdate"]);
                                }

                                //string date1 = this.Request.QueryString["Date1"].ToString()?? System.DateTime.Today.ToString("dd-MMM-yyyy");


                                DateTime date =Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()) ;


                                if (ppaidamt > 0)
                                {

                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt1"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["paidamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schdate <= date ? 0.00 : schamt - (paidamt > 0 ? paidamt : 0.00);
                                }
                            }
                            else
                            {

                                double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                                if (npaidamt > 0)
                                {
                                    double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                    dt1.Rows[j]["balamt"] = schamt - paidamt;
                                }


                                else if (npaidamt < 0)
                                {

                                    dt1.Rows[j]["balamt"] = 0.00;


                                }


                            }
                        }

                    break;
            }


            return dt1;
            //Session["tblCustPayment"] = dt1;

        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFPreDue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(predue)", "")) ?
                0.00 : dt.Compute("Sum(predue)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFCurDue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(curdue)", "")) ?
              0.00 : dt.Compute("Sum(curdue)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFDelayCh")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(delayamt)", "")) ?
              0.00 : dt.Compute("Sum(delayamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvCustInvoice.FooterRow.FindControl("lgvFtopayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todue)", "")) ?
              0.00 : dt.Compute("Sum(todue)", ""))).ToString("#,##0;(#,##0); ");


        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblinterest"];

            if ((dt.Rows.Count == 0)) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "DoueCollAll":
                    DataView dv1 = new DataView();
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'A'");
                    this.gvInterest.DataSource = dv1.ToTable();
                    this.gvInterest.DataBind();
                    this.FooterCal(dv1.ToTable());
                    this.lblchqdishonour.Visible = false;
                    this.lblchqnotyetCleared.Visible = false;


                    //Cheque Not yet Cleared

                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'B'");
                    this.gvChqnoclin.DataSource = dv1.ToTable();
                    this.gvChqnoclin.DataBind();
                    if (dv1.ToTable().Rows.Count > 0)
                    {
                        this.lblchqnotyetCleared.Visible = true;
                        ((Label)this.gvChqnoclin.FooterRow.FindControl("lgvFPayamtbuncr")).Text = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("sum(pamount)", "")) ?
                                     0 : dv1.ToTable().Compute("sum(pamount)", ""))).ToString("#,##0;(#,##0); ");
                    }



                    //Dishonour
                    dv1 = dt.DefaultView;
                    dv1.RowFilter = ("grp = 'C'");
                    this.gvCDHonour.DataSource = dv1.ToTable();
                    this.gvCDHonour.DataBind();
                    if (dv1.ToTable().Rows.Count > 0)
                    {
                        this.lblchqdishonour.Visible = true;
                        ((Label)this.gvCDHonour.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(dv1.ToTable().Compute("sum(discharge)", "")) ?
                                     0 : dv1.ToTable().Compute("sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    }

                    break;


                case "CustInvoice":

                    break;

            }
        }
        private void FooterCal(DataTable dt)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "DoueCollAll":
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cinsam)", "")) ?
                                 0 : dt.Compute("sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pamount)", "")) ?
                                          0 : dt.Compute("sum(pamount)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFcumbalamt")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["cumbalance"]).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFinamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ?
                                          0 : dt.Compute("sum(interest)", ""))).ToString("#,##0;(#,##0); ");


                    double tointerest = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(interest)", "")) ? 0 : dt.Compute("sum(interest)", "")));
                    double linterest = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["interest"]);
                    double todue = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["dueamt"]);

                    ((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text = (todue + tointerest - linterest).ToString("#,##0;(#,##0); ");

                    //((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text = Convert.ToDouble(dt.Rows[(dt.Rows.Count) - 1]["dueamt"]).ToString("#,##0;(#,##0); ");

                    break;

            }

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "DoueCollAll":
                    this.PrintInterestAndRegis();
                    break;
                case "ClientLedger":
                    this.PrintCleintLedger();
                    break;
                case "CustInvoice":
                    this.RptCustInvoice();
                    break;
            }
        }

        private void PrintInterestAndRegis()
        {



            //string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            //string frmdate = "01-" + ASTUtility.Right(date, 8);
            //string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            //ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptSalClntInterest();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            //txtAddress.Text = comadd;
            //TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            //txtProjectName.Text = this.ddlProjectName.SelectedItem.Text;

            //// DataTable dt3 = (DataTable)Session["tblchqdishonour"]; ;
            //double cdishonourcharge = 0.00;
            //if (dt1.Rows.Count > 0)
            //    cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ?
            //                 0 : dt1.Compute("sum(discharge)", "")));

            //double insamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text);
            //double paidamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text);
            //double dueamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text);

            //double chqnotyetcl = (this.gvChqnocl.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnocl.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
            ////insamt = insamt - chqnotyetcl;
            //double todueamt = cdishonourcharge + dueamt;
            //TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
            //txtcustname.Text = this.ddlCustName.SelectedItem.Text;
            //TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
            //txtCustaddress.Text = ds2.Tables[0].Rows[0]["custadd"].ToString();
            //TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
            //txtunitdesc.Text = ds2.Tables[0].Rows[0]["udesc"].ToString();
            //TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
            //txtbodyarea.Text = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + "which is as follows:";
            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");
            //TextObject txtdelaycharge = rptstk.ReportDefinition.ReportObjects["txtdelaycharge"] as TextObject;
            //txtdelaycharge.Text = "Delay Charge " + this.txtinpermonth.Text.Trim() + " P. M";

            //TextObject txtcubbalance = rptstk.ReportDefinition.ReportObjects["txtinsdueamt"] as TextObject;
            //txtcubbalance.Text = (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ");
            //TextObject txtchnnotcl = rptstk.ReportDefinition.ReportObjects["txtchnnotcl"] as TextObject;
            //txtchnnotcl.Text = (chqnotyetcl).ToString("#,##0;(#,##0); ");

            ////TextObject txtdueamt = rptstk.ReportDefinition.ReportObjects["txtdueamt"] as TextObject;
            ////txtdueamt.Text = dueamt.ToString("#,##0;(#,##0); ");

            //TextObject txttoDuesamt = rptstk.ReportDefinition.ReportObjects["txttoDuesamt"] as TextObject;
            //txttoDuesamt.Text = todueamt.ToString("#,##0;(#,##0); ");



            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;





            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblinterest"];

            string date = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");
            string frmdate = "01-" + ASTUtility.Right(date, 8);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string custid = this.Request.QueryString["usircode"].ToString();
            DataSet ds2 = SalData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTADDUNIT", pactcode, custid, "", "", "", "", "", "", "");

            // DataTable dt2 = this.GetMarggeTable();
            ReportDocument rptstk = new RealERPRPT.R_22_Sal.RptSalClntInterest();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            txtAddress.Text = comadd;
            TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectName.Text = this.lblProjectName.Text;

            // DataTable dt3 = (DataTable)Session["tblchqdishonour"]; ;
            double cdishonourcharge = 0.00;
            if (dt1.Rows.Count > 0)
                cdishonourcharge = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(discharge)", "")) ?
                             0 : dt1.Compute("sum(discharge)", "")));

            double insamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFinsamt")).Text);
            double paidamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFpayamt")).Text);
            double dueamt = Convert.ToDouble("0" + ((Label)this.gvInterest.FooterRow.FindControl("lgvFdueamt")).Text);

            double chqnotyetcl = (this.gvChqnoclin.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + ((Label)this.gvChqnoclin.FooterRow.FindControl("lgvFPayamtbuncr")).Text);
            //insamt = insamt - chqnotyetcl;
            double todueamt = cdishonourcharge + dueamt;
            TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
            txtcustname.Text = this.LblCutName.Text;
            TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
            txtCustaddress.Text = ds2.Tables[0].Rows[0]["custadd"].ToString();
            TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
            txtunitdesc.Text = ds2.Tables[0].Rows[0]["udesc"].ToString();
            TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
            txtbodyarea.Text = "With reference of the above , kindly be informed that you are payble to us an " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy") + " against your above unit total amount of Tk. " + todueamt.ToString("#,##0;(#,##0); ") + "which is as follows:";
            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "As On " + Convert.ToDateTime(todate).ToString("dd.MM.yyyy");
            TextObject txtdelaycharge = rptstk.ReportDefinition.ReportObjects["txtdelaycharge"] as TextObject;
            txtdelaycharge.Text = "Delay Charge " + this.lblInterPar.Text.Trim() + " P. M";

            TextObject txtcubbalance = rptstk.ReportDefinition.ReportObjects["txtinsdueamt"] as TextObject;
            txtcubbalance.Text = (insamt - paidamt - chqnotyetcl).ToString("#,##0;(#,##0); ");
            TextObject txtchnnotcl = rptstk.ReportDefinition.ReportObjects["txtchnnotcl"] as TextObject;
            txtchnnotcl.Text = (chqnotyetcl).ToString("#,##0;(#,##0); ");

            //TextObject txtdueamt = rptstk.ReportDefinition.ReportObjects["txtdueamt"] as TextObject;
            //txtdueamt.Text = dueamt.ToString("#,##0;(#,##0); ");

            TextObject txttoDuesamt = rptstk.ReportDefinition.ReportObjects["txttoDuesamt"] as TextObject;
            txttoDuesamt.Text = todueamt.ToString("#,##0;(#,##0); ");



            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string comcod = this.GetComeCode();
            //string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void RptCustInvoice()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = (DataSet)ViewState["tblinterest"];
            DataTable dt = ds1.Tables[0];
            double topayment = Math.Round(Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todue)", "")) ?
            0.00 : dt.Compute("Sum(todue)", ""))), 0);
            string Takainword = "Amount in words: " + ASTUtility.Trans(topayment, 2);

            ReportDocument rptstk = new RealERPRPT.R_23_CR.RptCustomerInvoice02();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtAddress = rptstk.ReportDefinition.ReportObjects["txtAddress"] as TextObject;
            txtAddress.Text = comadd;


            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "Date: " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");

            TextObject txtcustname = rptstk.ReportDefinition.ReportObjects["txtcustname"] as TextObject;
            txtcustname.Text = ds1.Tables[1].Rows[0]["custnam"].ToString();
            TextObject txtCustaddress = rptstk.ReportDefinition.ReportObjects["txtCustaddress"] as TextObject;
            txtCustaddress.Text = ds1.Tables[1].Rows[0]["custadd"].ToString();


            TextObject txtProjectName = rptstk.ReportDefinition.ReportObjects["txtProjectName"] as TextObject;
            txtProjectName.Text = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            TextObject txtunitdesc = rptstk.ReportDefinition.ReportObjects["txtunitdesc"] as TextObject;
            txtunitdesc.Text = ds1.Tables[1].Rows[0]["unitdesc"].ToString();


            TextObject txtbodyarea = rptstk.ReportDefinition.ReportObjects["txtbodyarea"] as TextObject;
            txtbodyarea.Text = "According to agreed payment schdule following payments will be due on " + Convert.ToDateTime(this.Request.QueryString["Date1"]).AddDays(7).ToString("dd.MM.yyyy");

            TextObject txtamountinword = rptstk.ReportDefinition.ReportObjects["txtamountinword"] as TextObject;
            txtamountinword.Text = Takainword;

            TextObject txtpaymenttype = rptstk.ReportDefinition.ReportObjects["txtpaymenttype"] as TextObject;
            txtpaymenttype.Text = "Payment: By an Account Payee Cheque in favour of " + comnam;

            TextObject txtforcompany = rptstk.ReportDefinition.ReportObjects["txtforcompany"] as TextObject;
            txtforcompany.Text = "For " + comnam;
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);


            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                               ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void gvCustInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string comcod = this.GetCompCode();
                HyperLink delamt = (HyperLink)e.Row.FindControl("HLgDelamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();
                if (code == "8199995")
                {
                    delamt.Font.Bold = true;

                }
                if (code == "")
                {
                    return;
                }
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgDelamt");
                if (code == "8199995")
                {
                    hlink1.Attributes["style"] = "color:blue; cursor:pointer !importent;";

                    string uPACTCODE = this.Request.QueryString["pactcode"].ToString();
                    string uSIRCODE = this.Request.QueryString["usircode"].ToString();
                    string mTRNDAT1 = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");
                    hlink1.NavigateUrl = "LinkDuesColl.aspx?Type=DoueCollAll&comcod=" + comcod + "&pactcode=" + uPACTCODE + "&usircode=" + uSIRCODE + "&Date1=" + mTRNDAT1;
                }
            }
        }
    }
}