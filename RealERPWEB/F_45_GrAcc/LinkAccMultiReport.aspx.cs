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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_45_GrAcc
{

    public partial class LinkAccMultiReport : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            //rpttype = ledger &


            ((Label)this.Master.FindControl("lblTitle")).Text = "Accounts Reports View/Print Screen";

            if (this.lblRptType.Text.Length == 0)
            {

                string comcod = Request.QueryString["comcod"].ToString();
                string mRptType = Request.QueryString["rpttype"].ToString().Trim();
                this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
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
                    case "FxtAsset":
                        this.ShowFxtAsset();
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

        }

        private string GetCompcode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {


            string comcod = Request.QueryString["comcod"].ToString();
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
            switch (mRptType)
            {
                case "ledger":
                    this.PrintLedger();
                    break;
                case "voucher":

                    break;
                case "schedule":

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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dtds = (DataTable)ViewState["StoreTable"];
            ReportDocument rptDShedule = new RealERPRPT.R_17_Acc.RptDetailSheduleTB();
            TextObject txtCompany = rptDShedule.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtTitle = rptDShedule.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Account Details Schedule Report - Details";


            TextObject txtdate = rptDShedule.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";

            TextObject rpttxtAccDesc = rptDShedule.ReportDefinition.ReportObjects["txtAccDesc"] as TextObject;
            rpttxtAccDesc.Text = "Account Description: " + dtds.Rows[0]["actdesc4"].ToString();
            TextObject txtuserinfo = rptDShedule.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Account Details Schedule";
                string eventdesc = "Print Schedule";
                string eventdesc2 = dtds.Rows[0]["actdesc4"].ToString() + " (From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            rptDShedule.SetDataSource(dtds);

            Session["Report1"] = rptDShedule;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private string ComLedger()
        {

            string comcod = this.GetCompcode();
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


                case "3101":
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
            DataTable dt = (DataTable)Session["StoreTable"];
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
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccLedger();
            //string Resdesc = "";
            //if (Request.QueryString["rpttype"].ToString() == "AccRec")
            //{
            //    Resdesc = "Client Name: " + Request.QueryString["resdesc"].ToString();

            //}
            //DataTable dt = (DataTable)ViewState["StoreTable"];
            //if (dt == null)
            //    return;
            //string Headertitle = (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "19") ? "Cash/Bank Book" : (this.Request.QueryString["actcode"].ToString().Substring(0, 2) == "29") ? "Cash/Bank Book" : "Account Ledger Report";

            //TextObject txtcompanyname = rptstk.ReportDefinition.ReportObjects["txtcompanyname"] as TextObject;
            //txtcompanyname.Text = comnam;


            //TextObject txtHeadertitle = rptstk.ReportDefinition.ReportObjects["txtHeadertitle"] as TextObject;
            //txtHeadertitle.Text = Headertitle;



            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = (Request.QueryString["rpttype"].ToString() == "AccRec") ? "As on Date: " + Request.QueryString["Date1"].ToString()
            //        : "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " +
            //            Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";

            //TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //rpttxtAccDesc.Text = "Accounts Head: " + ((Request.QueryString["rpttype"].ToString() == "AccRec") ? Request.QueryString["actdesc"].ToString() : this.LblLgLedgerHead.Text);

            ////TextObject rpttxtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            ////rpttxtResDesc.Text = Resdesc;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["StoreTable"]);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            DataTable dt = (DataTable)Session["StoreTable"];
            ReportDocument rptstk = new ReportDocument();
            string Headertitle = "Subsidary Ledger";

            string Resdesc = Request.QueryString["resdesc"].ToString();
            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            string comledger = this.ComLedger();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccSLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + Request.QueryString["actdesc"].ToString()));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //RealERPRPT.R_17_Acc.RptAccSLedger rptstk = new RealERPRPT.R_17_Acc.RptAccSLedger();
            //string Resdesc = "Subsidiary Head: " + Request.QueryString["resdesc"].ToString();
            //DataTable dt = (DataTable)ViewState["StoreTable"];
            //if (dt == null)
            //    return;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + " )";
            //TextObject rpttxtAccDesc = rptstk.ReportDefinition.ReportObjects["actdesc"] as TextObject;
            //rpttxtAccDesc.Text = "Accounts Head: " + Request.QueryString["actdesc"].ToString();
            //TextObject rpttxtResDesc = rptstk.ReportDefinition.ReportObjects["txtResDesc"] as TextObject;
            //rpttxtResDesc.Text = Resdesc;
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource((DataTable)ViewState["StoreTable"]);
            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void GetDataForSchedule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Request.QueryString["comcod"].ToString();
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

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_CSCH", "CSCH_REPORT_LEVEL_0" + ASTUtility.Right(mLEVEL1, 1),
                          mTRNDAT1, mTRNDAT2, mTOPHEAD1, mACTCODE1, "", "", "", "", "");

            //this.LblSchCompName.Text = Convert.ToString(ds1.Tables[0].Rows[0]["comnam"]);
            this.LblSchReportTitle.Text = "ACCOUNTS CONTROL SCHEDULE - " + mLEVEL1;
            this.LblSchReportPeriod.Text = (mTRNDAT1 == mTRNDAT2) ? ("As On Date: " + mTRNDAT1) : "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";
            this.LblSchReportTitle2.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString();// mACTCODE1;// mACTDESC1;

            this.gvSchedule.DataSource = ds1.Tables[0];
            this.gvSchedule.Columns[2].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0.00;(#,##0.00); -");
            this.gvSchedule.Columns[2].FooterText = this.gvSchedule.Columns[2].FooterText + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0.00;(#,##0.00); -");
            this.gvSchedule.Columns[3].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0.00;(#,##0.00); ") + "<br> ";
            this.gvSchedule.Columns[4].FooterText = " <br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0.00;(#,##0.00); ");
            this.gvSchedule.Columns[5].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0.00;(#,##0.00); -");
            this.gvSchedule.Columns[5].FooterText = this.gvSchedule.Columns[5].FooterText + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0.00;(#,##0.00); -");
            this.gvSchedule.DataBind();
        }

        protected void GetDataForLedger()
        {


            this.MultiView1.ActiveViewIndex = 1;
            string comcod = Request.QueryString["comcod"].ToString();
            string mACTCODE = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGER", mACTCODE, mTRNDAT1, mTRNDAT2, "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.LblLgReportTitle.Text = Convert.ToString(ds1.Tables[1].Rows[0]["booknam"]).ToUpper();
            this.LblLgLedgerHead.Text = Request.QueryString["actdesc"].ToString(); // mACTCODE;//this.DDListAccHeadList.SelectedItem.Text.Trim();
            this.LblLgReportPeriod.Text = (mTRNDAT1 == mTRNDAT2) ? ("As On Date: " + mTRNDAT1) : "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            DataTable dt = ds1.Tables[0];
            this.HiddenSameDate(dt);
            ViewState["StoreTable"] = dt;
            this.gvLedger.DataSource = dt;
            this.gvLedger.DataBind();


        }
        protected void GetDataForSpLedger()
        {

            this.MultiView1.ActiveViewIndex = 3;
            string comcod = Request.QueryString["comcod"].ToString();
            string mACTCODE = Request.QueryString["actcode"].ToString();
            string mRESCODE = Request.QueryString["rescode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", mACTCODE, mTRNDAT1, mTRNDAT2, mRESCODE, "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.lblHeaderName.Text = "Project Name: " + ds1.Tables[1].Rows[0]["actdesc"].ToString();
            this.lblResName.Text = "Resource Name: " + ds1.Tables[1].Rows[0]["resdesc"].ToString();
            this.LblLgResRptPeriod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            DataTable dt = ds1.Tables[0];
            this.BalCalculation(dt);
            this.HiddenSameDate(dt);
            ViewState["StoreTable"] = dt;
            this.dgv2.DataSource = dt;
            this.dgv2.DataBind();
        }
        protected void GetDataForDetTB()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Request.QueryString["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 4;
            string mACTCODE1 = Request.QueryString["actcode"].ToString();
            string mTRNDAT1 = Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = Request.QueryString["Date2"].ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB2",
                          mTRNDAT1, mTRNDAT2, "12", "12", mACTCODE1, "", "", "", "");

            this.LblSchReportTitle.Text = "ACCOUNTS DETAILS SCHEDULE - ";
            this.lblRptPeriod.Text = (mTRNDAT1 == mTRNDAT2) ? ("As On Date: " + mTRNDAT1) : "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";
            this.LblSchReportTitle5.Text = ds1.Tables[0].Rows[0]["actdesc4"].ToString();// mACTCODE1;// mACTDESC1;
            ViewState["StoreTable"] = ds1.Tables[0];
            this.grvDTB.DataSource = ds1.Tables[0];
            this.grvDTB.DataBind();
            ((Label)this.grvDTB.FooterRow.FindControl("lblfopnamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(opnam)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(dram)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfCramt")).Text = "<br>" + Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(cram)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvDTB.FooterRow.FindControl("lblfcloamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[0].Compute("sum(closam)", "")) ?
                                0 : ds1.Tables[0].Compute("sum(closam)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void GetDataAccRec()
        {

            string comcod = Request.QueryString["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 5;
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
            string comcod = Request.QueryString["comcod"].ToString();
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
            string comcod = Request.QueryString["comcod"].ToString();
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
            string comcod = Request.QueryString["comcod"].ToString();
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

            string comcod = Request.QueryString["comcod"].ToString();
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
        private void ShowFxtAsset()
        {

            Session.Remove("tblspledger");

            string comcod = Request.QueryString["comcod"].ToString();
            string frmdate = Request.QueryString["Date1"].ToString();
            string todate = Request.QueryString["Date2"].ToString();
            this.MultiView1.ActiveViewIndex = 10;
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "FXTASSETINFO", frmdate, todate, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvFxtAsset.DataSource = null;
                this.gvFxtAsset.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tblspledger"] = HiddenSameData(ds2.Tables[0]);

            this.lblFdate.Text = Request.QueryString["Date1"].ToString();
            this.lblTDate.Text = Request.QueryString["Date2"].ToString();

            this.gvFxtAsset.DataSource = (DataTable)Session["tblspledger"];
            this.gvFxtAsset.DataBind();
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
                case "FxtAsset":
                    ((Label)this.gvFxtAsset.FooterRow.FindControl("lgvFOpDramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opndram)", "")) ?
                       0.00 : dt.Compute("Sum(opndram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFxtAsset.FooterRow.FindControl("lgvFOpCramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opncram)", "")) ?
                        0.00 : dt.Compute("Sum(opncram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFxtAsset.FooterRow.FindControl("lgvFDramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dram)", "")) ?
                        0.00 : dt.Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFxtAsset.FooterRow.FindControl("lgvFCramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cram)", "")) ?
                        0.00 : dt.Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvFxtAsset.FooterRow.FindControl("lgvFClsamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                           0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }

        }
        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;

            for (int i = 0; i < dt.Rows.Count - 2; i++)
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
            return dt;

        }
        private void HiddenSameDate(DataTable dt1)
        {


            string Date1 = dt1.Rows[0]["voudat1"].ToString();
            string vounum = dt1.Rows[0]["vounum1"].ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = Request.QueryString["comcod"].ToString();
            string mRptType = Request.QueryString["rpttype"].ToString().Trim();
            this.lblRptType.Text = (mRptType == null ? "NoReport" : mRptType);
            switch (mRptType)
            {

                case "ledger":
                case "spledger":
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
            switch (mRptType)
            {

                case "IssPay":
                case "FxtAsset":
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


            }
            return dt1;

        }

        protected void GetDataForVoucher()
        {

            string comcod = Request.QueryString["comcod"].ToString();
            this.MultiView1.ActiveViewIndex = 2;
            string mVOUNUM = Request.QueryString["vounum"].ToString();
            string mVOUDAT = Request.QueryString["Date1"].ToString();
            string mVTCODE = "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "EDITVOUCHER", mVOUNUM, mVOUDAT, mVTCODE, "", "", "", "", "", "");

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

            if (ASTUtility.Right(mACTCODE, 4) == "0000")
                hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            else
                hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
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
                hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                if (mVOUNUM.Contains("M"))
                    hlink1.Text = mVOUNUM.Substring(0, 3) + mVOUNUM.Substring(8, 2) + "-" + mVOUNUM.Substring(10, 4);
                else
                    hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);

            }
        }

        protected void dgv2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = Request.QueryString["comcod"].ToString(); ;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14 && ASTUtility.Left(mVOUNUM.Trim(), 2) != "PV")
            {
                hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            }
        }
        protected void grvDTB_RowDataBound(object sender, GridViewRowEventArgs e)
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

            hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //if (ASTUtility.Right(mACTCODE, 4) == "0000")
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //else
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //"&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
        }
        protected void grvAccRecFin_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = Request.QueryString["comcod"].ToString(); ;

            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            if (mVOUNUM.Trim().Length == 14)
            {
                hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
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

            hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
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

            hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&resdesc=" + mREESDESC;
        }
        protected void grvPrjRptRP_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = Request.QueryString["comcod"].ToString();
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

            hlink1.NavigateUrl = "LinkAccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + mRESCODE +
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
    }
}
