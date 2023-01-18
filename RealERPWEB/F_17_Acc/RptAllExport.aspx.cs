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
namespace RealERPWEB.F_17_Acc
{



    public partial class RptAllExport : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string title = (this.Request.QueryString["Type"].ToString().Trim() == "Mains") ? "Trial Balance"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "Trial02") ? "Trial Balance Category Wise"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "Details") ? "Notes to the Financial  Statement"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "TBDetails") ? "Details of Trial Balance"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "INDetails") ? "Details Of Income Statement"

                   : (this.Request.QueryString["Type"].ToString().Trim() == "HOTB") ? "Head Office Trial Balance"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "Accounts Trial Balance (Consolidated)"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "BDetails2") ? "Notes:Financial Position"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "INDetails2") ? "Notes:Income Statement"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "INDetails2") ? "Notes:Income Statement"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "BankPosition02") ? "Bank Position 02"
                   : (this.Request.QueryString["Type"].ToString().Trim() == "BalConfirmation") ? "Balance Confirmation" : "Bank Position ";

                //((Label)this.Master.FindControl("lblTitle")).Text = title;

                //this.Master.Page.Title = title;
                indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));




                if (ConstantInfo.LogStatus)
                {
                    string comcod = this.GetComcod();
                    string eventdesc = "View " + ((Label)this.Master.FindControl("lblTitle")).Text;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), "", eventdesc, "");


                }

                this.ViewSection();
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }
        public void Operningdat()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string datepart;
            DataSet ds4 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            DataTable dt4 = ds4.Tables[0];
            if (dt4.Rows.Count == 0)
            {
                datepart = "";
            }
            else
            {
                datepart = Convert.ToDateTime(dt4.Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            }
            if (datepart == "")
            {
                this.txtDatefrom.Text = datepart.ToString();
                this.txtDatefrom.Enabled = true;
            }
            else
            {
                this.txtDatefrom.Text = datepart;
                //this.txtDatefrom.Enabled = false;
            }
        }

        private void GetchkBalance()
        {
            string comcod = this.GetComcod();

            switch (comcod)
            {

                case "3315":
                case "3316":
                case "3317":
                case "3101":
                    this.chknetbalance.Checked = true;
                    break;

                default:
                    break;

            }

        }
        private void ViewSection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            double day;
            switch (Type)
            {

                case "Mains":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    if (comcod == "3337" || comcod == "3336" || comcod == "3101" || comcod == "1103")
                    {
                        this.Operningdat();
                    }
                    this.GetchkBalance();

                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":
                    string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefromd.Text = Convert.ToDateTime("01-Jan-" + curdate.Substring(7)).ToString("dd-MMM-yyyy");
                    this.txtDatetod.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "BankPosition":
                    string date1 = this.Request.QueryString["Date1"];
                    string date2 = this.Request.QueryString["Date2"];
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefrombank.Text = date1.Length > 0 ? date1 : "01" + date.Substring(2);//DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetobank.Text = date2.Length > 0 ? date2 : System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lnkbtnBankPosition_Click(null, null);

                    break;
                //  case "HOTB":
                //day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                //this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                //this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.lblreportlevel.Visible = false;
                //this.ddlReportLevel.Visible = false;
                //this.MultiView1.ActiveViewIndex = 0;
                //    break;
                case "HOTB":
                case "TBConsolidated":
                    this.txtAsDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;

                    break;

                case "BankPosition02":
                    this.txtAsDateb.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 4;
                    break;


                case "BalConfirmation":

                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrombankcb.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetobankcb.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 5;
                    break;


                case "BDetails2":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrombdet2.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetobdet2.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "INDetails2":
                    curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefromisdet2.Text = Convert.ToDateTime("01-Jan-" + curdate.Substring(7)).ToString("dd-MMM-yyyy");
                    this.txtDatetoisdet2.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefromisdet2.Text).AddYears(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 7;
                    break;

                case "Trial02":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefromtb.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetotb.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    if (comcod == "3337" || comcod == "3336" || comcod == "3101")
                    {
                        this.Operningdat();
                    }

                    this.MultiView1.ActiveViewIndex = 8;
                    break;
            }
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
            //    string eventdesc = "Show Report: " + Type;
            //    string eventdesc2 = "";
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }


        private string calltype()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string Calltype = "";
            switch (comcod)
            {
                case "3101":

                    Calltype = "REPORT_TRIALBALANCE_COMPANYUDDL";
                    break;
                default:
                    Calltype = "TB_COMPANY_0";
                    break;
            }
            return Calltype;
        }
        private DataSet GetDataForReport()
        {


            string Type = this.Request.QueryString["Type"].ToString().Trim();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            DataSet ds1 = new DataSet();
            string date1, date2;

            switch (Type)
            {
                case "Mains":
                    date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
                    date2 = this.txtDateto.Text.Substring(0, 11).ToString();
                    string level = this.ddlReportLevel.SelectedValue.ToString();
                    string CallType = (this.chknetbalance.Checked ? "TBNET_COMPANY_0" : "TB_COMPANY_0") + level;
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", CallType, date1, date2, "", "", "", "", "", "", "");
                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":

                    date1 = this.txtDatefromd.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetod.Text.Substring(0, 11).ToString();
                    string levelmain = this.ddlacclevel.SelectedValue.ToString();
                    string leveldetails = this.ddlReportLevelDetails.SelectedValue.ToString();
                    string status = this.Request.QueryString["Type"];
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB", date1, date2, levelmain, leveldetails, status, "", "", "", "");
                    break;

                case "BankPosition":
                    date1 = this.txtDatefrombank.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetobank.Text.Substring(0, 11).ToString();
                    string levelbank = this.ddlReportLevelBank.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION", date1, date2, levelbank, "", "", "", "", "", "");
                    break;

                // case "HOTB":
                //date1 = this.txtDatefrom.Text.Substring(0, 11).ToString();
                //date2 = this.txtDateto.Text.Substring(0, 11).ToString();
                //ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "TBHEADOFFICE", date1, date2, "", "", "", "", "", "", "");
                // break;

                case "HOTB":
                case "TBConsolidated":
                    string date = this.txtAsDate.Text.Substring(0, 11).ToString();
                    string Level = this.ddlReportLevelcon.SelectedValue.ToString();
                    string hotb = (this.Request.QueryString["Type"].ToString().Trim() == "HOTB") ? "HOTB" : "";
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCONTRIALBALANCE", date, Level, hotb, "", "", "", "", "", "");
                    break;

                case "BankPosition02":
                    date = this.txtAsDateb.Text.Substring(0, 11).ToString();
                    string levelbank02 = this.ddlReportLevelbk02.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION02", date, levelbank02, "", "", "", "", "", "", "");
                    break;

                case "BalConfirmation":
                    date1 = this.txtDatefrombankcb.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetobankcb.Text.Substring(0, 11).ToString();
                    string levelcandbank = this.ddlReportLevelBankcb.SelectedValue.ToString();
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCASHANDBANKBAL", date1, date2, levelcandbank, "", "", "", "", "", "");
                    break;

                case "BDetails2":
                    string mTOPHEAD1 = "NOTOPHEAD";
                    date1 = this.txtDatefrombdet2.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetobdet2.Text.Substring(0, 11).ToString();

                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBALSHEETDETAILS02", date1, date2, mTOPHEAD1, "", "", "", "", "", "");
                    break;


                case "Trial02":
                    date1 = this.txtDatefromtb.Text.Substring(0, 11).ToString();
                    date2 = this.txtDatetotb.Text.Substring(0, 11).ToString();
                    // string level = this.ddlReportLevel.SelectedValue.ToString();
                    //string Calltype = this.calltype();
                    string leveltb2 = this.ddlReportLeveltb2.SelectedValue.ToString();

                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "REPORT_TRIALBALANCE_COMPANYUDDL_0" + leveltb2, date1, date2, "", "", "", "", "", "", "");
                    break;
            }
            return ds1;
        }

        //private DataTable HiddenSameData(DataTable dt1)
        //{

        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    string Type = Request.QueryString["Type"].ToString();
        //    int j;

        //    switch (Type)
        //    {







        //        case "Mains":

        //            string actcode4 = dt1.Rows[0]["actcode4"].ToString();
        //            //  string rescode = dt1.Rows[0]["rescode"].ToString();
        //            for (j = 1; j < dt1.Rows.Count; j++)
        //            {
        //                if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
        //                    dt1.Rows[j]["actdesc4"] = "";
        //                actcode4 = dt1.Rows[j]["actcode4"].ToString();


        //            }
        //            break;




        //    }

        //    return dt1;


        //}

        protected void lnkok_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.dgv1.DataSource = null;
                this.dgv1.DataBind();
                return;

            }

            this.dgv1.DataSource = ds1.Tables[0];
            this.dgv1.DataBind();
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            if (Type == "HOTB")
            {
                this.dgv1.Columns[11].Visible = false;
            }

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

            ((Label)this.dgv1.FooterRow.FindControl("lblfopndramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            ((Label)this.dgv1.FooterRow.FindControl("lblfopncramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            //this.dgv1.Columns[2].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0.00;(#,##0.00); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfDramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfCramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfclodramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfclocramt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.dgv1.FooterRow.FindControl("lblfnetamt")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            Session["Report1"] = dgv1;
            ((HyperLink)this.dgv1.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "Mains":

                    this.PrintMainTrialBal();
                    break;
                case "Details":
                case "TBDetails":
                case "INDetails":
                    this.PrintDetailsTrialBal();
                    break;
                case "BankPosition":
                    this.RptPrintBankPosition();
                    break;

                case "HOTB":
                case "TBConsolidated":
                    this.PrintConTrialBal();
                    break;

                case "BankPosition02":
                    PrintBankPosition02();
                    break;
                case "BalConfirmation":
                    PrintBalConfirmation();
                    break;

                case "BDetails2":
                    PrintRptNoteSheet();
                    break;
                case "INDetails2":
                    PrintRptIncomStatement();
                    break;

                case "Trial02":
                    this.PrintMainTrialBal02();
                    // this.PrintTrialBalRDLC();
                    break;

            }


            if (ConstantInfo.LogStatus)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrintRptIncomStatement()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromDate = this.txtDatefromisdet2.Text;
            string toDate = this.txtDatetoisdet2.Text;
            string Opdate1 = this.txtOpeningDate.Text;
            string Opdatep = this.txtOpeningDate.Text;
            string Cdate = fromDate + " TO " + toDate;
            string Opdate = Opdatep + " TO " + toDate;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            // ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);

            DataTable dt = (DataTable)ViewState["tblAcc"];

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.NoteIncoStatement>();

            LocalReport Rpt1 = new LocalReport();

            //DataTable dt1 = (DataTable)ViewState["VAproved"];
            //DataTable dt = new DataTable();
            //DataView dv;

            //dv = dt1.DefaultView;
            //dv.RowFilter = ("issued = 'E' ");
            //dt = dv.ToTable();


            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptNoteIncoStatement", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("Cdate", Cdate));
            Rpt1.SetParameters(new ReportParameter("Opdate", Opdate));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Notes:Comprehensive Income"));
            Rpt1.SetParameters(new ReportParameter("txtfootet", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintRptIncomStatement01()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mTRNDAT1 = this.txtDatefrombdet2.Text.Trim();
            string mTRNDAT2 = this.txtDatetobdet2.Text.Trim(); //.Substring(0, 11);  


            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.FinancialPosition02>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptNoteSheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("TxtOpening", Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtClosing", Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Notes Of Financial Position"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintRptNoteSheet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mTRNDAT1 = this.txtDatefrombdet2.Text.Trim();
            string mTRNDAT2 = this.txtDatetobdet2.Text.Trim(); //.Substring(0, 11);  


            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var list = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.FinancialPosition02>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptNoteSheet", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("TxtOpening", Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtClosing", Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Notes Of Financial Position"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private string ComTrialbalance()
        {
            string comtrialbal = "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            switch (comcod)
            {
                case "3336":
                case "3101":
                case "3305":
                case "3306":
                case "3309":
                case "2305":
                case "2306":
                case "3310":
                case "3311":

                    comtrialbal = "PrintTrialBalRDLC";
                    break;

                default:
                    break;
            }
            return comtrialbal;
        }


        private void PrintMainTrialBal()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            var AccTrialBl1 = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccTrialBl1>();
            LocalReport Rpt1 = new LocalReport();
            //Hashtable reportParm = new Hashtable();
            string opndram = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");
            string opncram = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            string dram = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            string cram = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            string closdram = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            string closcram = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            string closam = Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptTrialBl1", AccTrialBl1, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtopndram", opndram));
            Rpt1.SetParameters(new ReportParameter("txtopncram", opncram));
            Rpt1.SetParameters(new ReportParameter("txtdram", dram));
            Rpt1.SetParameters(new ReportParameter("txtcram", cram));
            Rpt1.SetParameters(new ReportParameter("txtclosdram", closdram));
            Rpt1.SetParameters(new ReportParameter("txtcloscram", closcram));
            Rpt1.SetParameters(new ReportParameter("txtnetam", closam));
            Rpt1.SetParameters(new ReportParameter("txtHeader", (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE - " + this.ddlReportLevel.SelectedValue.ToString().Trim() : "TRIAL BALANCE ( Level - " + this.ddlReportLevel.SelectedValue.ToString().Trim() + " )"));
            Rpt1.SetParameters(new ReportParameter("txtdate", "( For The Period From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintConTrialBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();

            switch (comcod)
            {

                case "3101":
                case "3332":

                    this.PrintConTrialBalInnstar();
                    break;

                default:

                    this.PrintConTrialBalGen();
                    break;

            }


        }

        private void PrintConTrialBalInnstar()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;


            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccConTrialBalanceInstar();



            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;
            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = (this.Request.QueryString["Type"].ToString().Trim() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
                : (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE";



            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "As On Date:  " + Convert.ToDateTime(this.txtAsDate.Text.Trim()).ToString("dd-MMM-yyyy");



            //TextObject txtclosdramt = rptstk.ReportDefinition.ReportObjects["txtclosdramt"] as TextObject;
            //txtclosdramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtcloscramt = rptstk.ReportDefinition.ReportObjects["txtcloscramt"] as TextObject;
            //txtcloscramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetdramt = rptstk.ReportDefinition.ReportObjects["txtnetdramt"] as TextObject;
            txtnetdramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            TextObject txtnetcramt = rptstk.ReportDefinition.ReportObjects["txtnetcramt"] as TextObject;
            txtnetcramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(ds1.Tables[0]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintConTrialBalGen()
        {

            // Iqbal Nayan
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

            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            LocalReport Rpt1 = new LocalReport();
            //DataTable dt = (DataTable)Session["tblDetails"];
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.TrialHeadOf>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccConTrialBalance", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Rpt1.SetParameters(new ReportParameter("closdram", Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("closcram", Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("netdram", Convert.ToDouble(ds1.Tables[1].Rows[0]["netdram"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("netcram", Convert.ToDouble(ds1.Tables[1].Rows[0]["netcram"]).ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("date", "As On Date:  " + Convert.ToDateTime(this.txtAsDate.Text.Trim()).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", (this.Request.QueryString["Type"].ToString().Trim() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
                : (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComcod();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataSet ds1 = this.GetDataForReport();
            //if (ds1 == null)
            //    return;


            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccConTrialBalance();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"].ToString().Trim() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
            //    : (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE";
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "As On Date:  " + Convert.ToDateTime(this.txtAsDate.Text.Trim()).ToString("dd-MMM-yyyy");
            //TextObject txtclosdramt = rptstk.ReportDefinition.ReportObjects["txtclosdramt"] as TextObject;
            //txtclosdramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            //TextObject txtcloscramt = rptstk.ReportDefinition.ReportObjects["txtcloscramt"] as TextObject;
            //txtcloscramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            //TextObject txtnetdramt = rptstk.ReportDefinition.ReportObjects["txtnetdramt"] as TextObject;
            //txtnetdramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netdram"]).ToString("#,##0;(#,##0); ");
            //TextObject txtnetcramt = rptstk.ReportDefinition.ReportObjects["txtnetcramt"] as TextObject;
            //txtnetcramt.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netcram"]).ToString("#,##0;(#,##0); ");
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }



        //private void PrintDetailsTrialBal()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string comsnam = hst["comsnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string session = hst["session"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;




        //    DataSet ds1 = this.GetDataForReport();
        //    if (ds1 == null)
        //        return;

        //    if (ds1.Tables[0].Rows.Count == 0)
        //        return;
        //    string qType = this.Request.QueryString["Type"].ToString();
        //    var DtlsofBlShet = ds1.Tables[0].DataTableToList<BDACCRDLC.R_17_Acc.AccRptList1.DtlOfBalanceSheet1>();
        //    LocalReport rpt1 = null;
        //    Hashtable reportParm = new Hashtable();
        //    reportParm["companyname"] = comnam.ToUpper();
        //    reportParm["Header"] = (qType == "INDetails") ? "DETAILS OF INCOME STATEMENT" : (qType == "Details") ? "DETAILS OF BALANCE SHEET" : "TRIAL BALANCE - " + this.ddlReportLevelDetails.SelectedItem.ToString().Trim();
        //    reportParm["date"] = "(From " + Convert.ToDateTime(this.txtDatefromd.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetod.Text.Trim()).ToString("dd-MMM-yyyy") + ")";
        //    reportParm["txtuserinfo"] = "Print Source :" + hostname + " , " + username + " , " + session + " , " + printdate;

        //    rpt1 = BDACCRptSetup1.GetLocalReport("R_17_Acc.RptAccDetTrialBalance", DtlsofBlShet, reportParm, null);

        //    Session["Report1"] = Rpt1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
        //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}

        //string comcod = this.GetComeCode();
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string comsnam = hst["comsnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string session = hst["session"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

        //    LocalReport Rpt1 = new LocalReport();
        //    DataTable Pfinfo = (DataTable) ViewState["tblemppfinfo"];
        //    DataTable empinfo = (DataTable)ViewState["tblempinfo"];
        //    var pflist = Pfinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.PaymentScheduleList>();
        //    var emplist = empinfo.DataTableToList<RealEntity.C_81_Hrm.IndvPf.Empinfo>();

        //    if(comcod=="3101"||comcod=="3333")
        //    {
        //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_90_PF.RptIndvPfAlli", pflist, null, null);
        //    }
        //    else
        //    {
        //        Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIndvPf", pflist, null, null);
        //    }


        //    Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
        //    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
        //    Rpt1.SetParameters(new ReportParameter("compname", comnam));
        //    Rpt1.SetParameters(new ReportParameter("empname", empinfo.Rows[0]["name"].ToString()));
        //    Rpt1.SetParameters(new ReportParameter("empid", empinfo.Rows[0]["idcard"].ToString()));
        //    Rpt1.SetParameters(new ReportParameter("empdesig", empinfo.Rows[0]["desig"].ToString()));
        //    Rpt1.SetParameters(new ReportParameter("rptname", "Individual Payment Schedule of Provident Fund"));
        //    Rpt1.SetParameters(new ReportParameter("pfstart", empinfo.Rows[0]["pfstart"].ToString()));
        //    Rpt1.SetParameters(new ReportParameter("pfend", empinfo.Rows[0]["pfend"].ToString()));
        //    Rpt1.SetParameters(new ReportParameter("dept", empinfo.Rows[0]["dept"].ToString()));


        //    Session["Report1"] = Rpt1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
        //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        private void PrintDetailsTrialBal()
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
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var lst = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.TrialBalDetails>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccDetTrialBalance", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("TxtOpening", Convert.ToDateTime(this.txtDatefromd.Text).AddDays(-1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtClosing", Convert.ToDateTime(this.txtDatetod.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("txtrptposition", (Type == "Details") ? "Financial Position" : (Type == "INDetails") ? "Comprehensive Income" : "Financial Position"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "As at " + Convert.ToDateTime(this.txtDatetod.Text).ToString("dd MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", (Type == "Details") ? "Notes to the Financial  Statement" : (Type == "INDetails") ? "Notes to the Financial  Statement" : ""));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //if (ds1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;

            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccDetTrialBalance();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;
            //string Type = this.Request.QueryString["Type"].ToString().Trim();
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (Type == "Details") ? "Notes to the Financial  Statement" : (Type == "INDetails") ? "Notes to the Financial  Statement" : "";

            //TextObject txtrptposition = rptstk.ReportDefinition.ReportObjects["txtrptposition"] as TextObject;
            //txtrptposition.Text = (Type == "Details") ? "Financial Position" : (Type == "INDetails") ? "Comprehensive Income" : "Financial Position";

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "As at " + Convert.ToDateTime(this.txtDatetod.Text).ToString("dd MMMM, yyyy");

            //TextObject TxtOpening = rptstk.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(this.txtDatefromd.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            //TextObject TxtClosing = rptstk.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text = Convert.ToDateTime(this.txtDatetod.Text).ToString("dd-MMM-yyyy");


            ////TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["opeingname1"] as TextObject;
            ////txtopeingname1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            ////TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["opeingname2"] as TextObject;
            ////txtopeingname2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");

            ////TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["dramount"] as TextObject;
            ////txtdramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); "); ;

            ////TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["cramount"] as TextObject;
            ////txtcramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            ////TextObject txtclosingamount1 = rptstk.ReportDefinition.ReportObjects["closingamount1"] as TextObject;
            ////txtclosingamount1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            ////TextObject txtclosingamount2 = rptstk.ReportDefinition.ReportObjects["closingamount2"] as TextObject;
            ////txtclosingamount2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); "); ;


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(this.HiddenSameData(ds1.Tables[0]));
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void RptPrintBankPosition()
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
            DataTable dt = (DataTable)Session["tblBankPosition"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.BankPosition>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBankPosition", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDatefrombank.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetobank.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Bank Position"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintBankPosition02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblBankPosition"];
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.BankPosition>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccBankPosition02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "As On Date : " + Convert.ToDateTime(this.txtAsDateb.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Bank Position 02"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBalConfirmation()
        {

            // Iqbal Nayan
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
            DataTable dt = (DataTable)Session["tblBankPosition"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccTrialBal001>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccBalConfirmation", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDatefrombankcb.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetobankcb.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Bank Confirmation"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintMainTrialBal02()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string frmdate = Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            var AccTrialBl1 = ds1.Tables[0].DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccTrialBl1>();
            LocalReport Rpt1 = new LocalReport();
            //Hashtable reportParm = new Hashtable();
            string opndram = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");
            string opncram = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            string dram = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            string cram = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            string closdram = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            string closcram = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            string closam = Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptTrialBl1", AccTrialBl1, null, null);
            Rpt1.SetParameters(new ReportParameter("companyname", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtopndram", opndram));
            Rpt1.SetParameters(new ReportParameter("txtopncram", opncram));
            Rpt1.SetParameters(new ReportParameter("txtdram", dram));
            Rpt1.SetParameters(new ReportParameter("txtcram", cram));
            Rpt1.SetParameters(new ReportParameter("txtclosdram", closdram));
            Rpt1.SetParameters(new ReportParameter("txtcloscram", closcram));
            Rpt1.SetParameters(new ReportParameter("txtnetam", closam));
            Rpt1.SetParameters(new ReportParameter("txtHeader", (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE - " + this.ddlReportLevel.SelectedValue.ToString().Trim() : "TRIAL BALANCE ( Level - " + this.ddlReportLevel.SelectedValue.ToString().Trim() + " )"));
            Rpt1.SetParameters(new ReportParameter("txtdate", "( For The Period From " + Convert.ToDateTime(this.txtDatefrom.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("frmdate", frmdate));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComcod();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataSet ds1 = this.GetDataForReport();
            //if (ds1 == null)
            //    return;

            //if (ds1.Tables[0].Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new ReportDocument();
            //rptstk = new RealERPRPT.R_17_Acc.RptAccTrialBalance();


            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"].ToString().Trim() == "TBConsolidated") ? "TRIAL BALANCE - " + this.ddlReportLeveltb2.SelectedValue.ToString().Trim() : "TRIAL BALANCE";
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.txtDatefromtb.Text.Trim()).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatetotb.Text.Trim()).ToString("dd-MMM-yyyy") + ")";


            //TextObject txtopeingname1 = rptstk.ReportDefinition.ReportObjects["txtopndram"] as TextObject;
            //txtopeingname1.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtopeingname2 = rptstk.ReportDefinition.ReportObjects["txtopncram"] as TextObject;
            //txtopeingname2.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtdramount = rptstk.ReportDefinition.ReportObjects["txtdram"] as TextObject;
            //txtdramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtcramount = rptstk.ReportDefinition.ReportObjects["txtcram"] as TextObject;
            //txtcramount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); "); ;

            //TextObject txtclosdram = rptstk.ReportDefinition.ReportObjects["txtclosdram"] as TextObject;
            //txtclosdram.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtcloscram = rptstk.ReportDefinition.ReportObjects["txtcloscram"] as TextObject;
            //txtcloscram.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");

            //TextObject txtclosam = rptstk.ReportDefinition.ReportObjects["txtclosam"] as TextObject;
            //txtclosam.Text = (comcod == "9201") ? Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0.00;(#,##0.00); ") : Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");


            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(ds1.Tables[0]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void dgv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcode")).Text;
            string mACTDESC = ((Label)e.Row.FindControl("lblgvAcDesc")).Text;



            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcode");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
            Label lblgvopndramt = (Label)e.Row.FindControl("lblgvopndramt");
            Label lblgvopncramt = (Label)e.Row.FindControl("lblgvopncramt");
            Label lblgvDramt = (Label)e.Row.FindControl("lblgvDramt");
            Label lblgvCramt = (Label)e.Row.FindControl("lblgvCramt");
            Label lblgvclodramt = (Label)e.Row.FindControl("lblgvclodramt");
            Label lblgvclocramt = (Label)e.Row.FindControl("lblgvclocramt");
            Label lblgvnetamt = (Label)e.Row.FindControl("lblgvnetamt");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();




            if (code == "")
            {
                return;
            }

            string level = this.ddlReportLevel.SelectedValue.ToString();

            if (ASTUtility.Right(code, 4) == "0000" && level == "4")
            {
                actcode.Attributes["style"] = "color:maroon;font-weight:bold;";
                actdesc.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvopndramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvopncramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvDramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvCramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvclodramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvclocramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvnetamt.Attributes["style"] = "color:maroon;font-weight:bold;";



            }
            ///---------------------------------//// 

            //if (ASTUtility.Left(mACTCODE, 1) == "4")
            //{
            //    hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //}
            if (lebel2 == "")
            {
                //int lactcode = Convert.ToInt16(code.Substring(0, 1));
                //string opnoption = lactcode >= 3 ? "withoutOpn" : "";
                string opnoption = "";

                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    //hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                    //     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&opnoption=" + opnoption;

                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                            "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=" + opnoption;
            }
            else
            {
                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }








        }
        protected void lnkDetailsok_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvDetails.DataSource = null;
                this.gvDetails.DataBind();
                return;
            }

            // this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDetails.DataSource = HiddenSameData(ds1.Tables[0]);
            this.gvDetails.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ///((HyperLink)this.gvDetails.HeaderRow.FindControl("hlbtnCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            // (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp)

            Session["DetTrBal"] = ds1.Tables[0];
            // Session["DetTBal"] = ds1.Tables[1];
            //this.FooterCal();
        }
        private void FooterCal()
        {

            DataTable dt = (DataTable)Session["DetTrBal"];

            DataView dv = dt.Copy().DefaultView;

            dv.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and opnam >'0'");
            DataTable dtopdr = dv.ToTable();
            double opdr = Convert.ToDouble((Convert.IsDBNull(dtopdr.Compute("sum(opnam)", "")) ? 0 : dtopdr.Compute("sum(opnam)", "")));

            dv.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and opnam <'0'");
            DataTable dtopcr = dv.ToTable();
            double opcr = Convert.ToDouble((Convert.IsDBNull(dtopcr.Compute("sum(opnam)", "")) ? 0 : dtopcr.Compute("sum(opnam)", "")));

            DataView dv1 = dt.Copy().DefaultView;

            dv1.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and closam >'0'");
            DataTable dtcldr = dv1.ToTable();
            double cldr = Convert.ToDouble((Convert.IsDBNull(dtcldr.Compute("sum(closam)", "")) ? 0 : dtcldr.Compute("sum(closam)", "")));

            dv1.RowFilter = ("(rescode like '%AAAA%' or rescode like '%BBBB%') and closam <'0'");
            DataTable dtclcr = dv1.ToTable();
            double clcr = Convert.ToDouble((Convert.IsDBNull(dtclcr.Compute("sum(closam)", "")) ? 0 : dtclcr.Compute("sum(closam)", "")));

            ((Label)this.gvDetails.FooterRow.FindControl("lblfopDes")).Text = "Dr. " + "<br>" + "Cr. ";
            ((Label)this.gvDetails.FooterRow.FindControl("lblfopnamtd")).Text = opdr.ToString("#,##0;(#,##0); ") + "<br>" + opcr.ToString("#,##0;(#,##0); ");

            ((Label)this.gvDetails.FooterRow.FindControl("lblfcloamtd")).Text = cldr.ToString("#,##0;(#,##0); ") + "<br>" + clcr.ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvDetails;
            ((HyperLink)this.gvDetails.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lblgvdescriptiond");
                Label lblopnamt = (Label)e.Row.FindControl("lblgvopnamtd");
                Label lbldram = (Label)e.Row.FindControl("lblgvDramtd");
                Label lblcramt = (Label)e.Row.FindControl("lblgvCramtd");
                Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobald");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "00000000AAAA")
                {

                    description.Font.Bold = true;
                    lblopnamt.Font.Bold = true;
                    lbldram.Font.Bold = true;
                    lblcramt.Font.Bold = true;
                    lblgvclobal.Font.Bold = true;

                }
            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            string actcode4 = "";
            string rescode = "";


            switch (Type)
            {
                case "Mains":




                    break;

                case "Trial02":

                    actcode4 = dt1.Rows[0]["actcode4"].ToString();
                    rescode = dt1.Rows[0]["rescode"].ToString();
                    int i = 0;
                    ////  string rescode = dt1.Rows[0]["rescode"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                    //        dt1.Rows[j]["actdesc4"] = "";
                    //    actcode4 = dt1.Rows[j]["actcode4"].ToString();


                    //}




                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if (i == 0)
                        {


                            actcode4 = dr1["actcode4"].ToString();
                            i++;
                            continue;
                        }

                        if (dr1["actcode4"].ToString() == actcode4 && dr1["rescode"].ToString() == rescode)
                        {


                            dr1["actdesc4"] = "";
                            dr1["resdesc"] = "";

                        }

                        else
                        {


                            if (dr1["actcode4"].ToString() == actcode4)

                            {
                                dr1["actdesc4"] = "";

                            }



                            if (dr1["rescode"].ToString() == rescode)

                            {
                                dr1["resdesc"] = "";

                            }




                        }


                        actcode4 = dr1["actcode4"].ToString();
                        rescode = dr1["rescode"].ToString();
                    }

                    break;

                case "Details":
                case "TBDetails":
                case "INDetails":
                    actcode4 = dt1.Rows[0]["actcode4"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();
                            dt1.Rows[j]["actdesc4"] = "";
                            dt1.Rows[j]["actnotes"] = "";
                            dt1.Rows[j]["actcode4"] = "";

                        }

                        else
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();

                        }

                        if (dt1.Rows[j]["rescode4"].ToString().Substring(0, 4) == "0000")
                            dt1.Rows[j]["rescode4"] = "";
                    }

                    break;

                case "BankPosition":


                    break;

                case "HOTB":
                case "TBConsolidated":
                    break;

                case "BankPosition02":
                    break;


                case "BalConfirmation":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();
                    }
                    break;
            }



            return dt1;

        }

        protected void lnkbtnBankPosition_Click(object sender, EventArgs e)
        {
            Session.Remove("tblBankPosition");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvBankPosition.DataSource = null;
                this.gvBankPosition.DataBind();
                return;
            }
            Session["tblBankPosition"] = ds1.Tables[0];
            this.gvBankPosition.DataSource = ds1.Tables[0];
            this.gvBankPosition.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvBankPosition.HeaderRow.FindControl("hlbtnbnkpdataExel")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));

            ds1.Dispose();
            Session["Report1"] = gvBankPosition;
            ((HyperLink)this.gvBankPosition.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }



        //protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.GetDataForReport();
        //    this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        //    DataTable dt = (DataTable)Session["DetTrBal"];
        //    gvDetails.DataSource = dt;
        //    gvDetails.DataBind();
        //    this.SaveValue();
        //}
        //protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    //this.GetDataForReport();
        //    gvDetails.PageIndex = e.NewPageIndex;
        //    this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
        //    DataTable dt = (DataTable)Session["DetTrBal"];
        //    gvDetails.DataSource = dt;
        //    gvDetails.DataBind();
        //    this.SaveValue();
        //}
        protected void lbtnCdataExel_Click(object sender, EventArgs e)
        {


        }
        protected void lnkTrialBalCon_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvtbcon.DataSource = null;
                this.gvtbcon.DataBind();
                return;

            }

            this.gvtbcon.DataSource = ds1.Tables[0];
            this.gvtbcon.DataBind();


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvtbcon.HeaderRow.FindControl("hlbtntbCdataExelcon")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            DataTable dt = ds1.Tables[0];

            //((Label)this.gvtbcon.FooterRow.FindControl("lblfClosDramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closdram)", "")) ?
            //                             0 : dt.Compute("sum(closdram)", ""))).ToString("#,##0;(#,##0); ");

            //((Label)this.gvtbcon.FooterRow.FindControl("lblfClosCramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(closcram)", "")) ?
            //                             0 : dt.Compute("sum(closcram)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvtbcon.FooterRow.FindControl("lblfnetdramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netdram)", "")) ?
            //                             0 : dt.Compute("sum(netdram)", ""))).ToString("#,##0;(#,##0); ");
            //((Label)this.gvtbcon.FooterRow.FindControl("lblfnetcramtcon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netcram)", "")) ?
            //                             0 : dt.Compute("sum(netcram)", ""))).ToString("#,##0;(#,##0); ");


            ((Label)this.gvtbcon.FooterRow.FindControl("lblfClosDramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbcon.FooterRow.FindControl("lblfClosCramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbcon.FooterRow.FindControl("lblfnetdramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtbcon.FooterRow.FindControl("lblfnetcramtcon")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["netcram"]).ToString("#,##0;(#,##0); ");


            Session["Report1"] = gvtbcon;
            ((HyperLink)this.gvtbcon.HeaderRow.FindControl("hlbtntbCdataExelcon")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void gvtbcon_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesccon");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcodecon")).Text;
            string mACTDESC = ((HyperLink)e.Row.FindControl("HLgvDesccon")).Text;
            string mTRNDAT1 = this.txtAsDate.Text;
            string mTRNDAT2 = this.txtAsDate.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcodecon");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesccon");

            string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpcode")).ToString().Trim();
            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();


            if (code == "")
            {
                return;
            }


            if (grp == "A")
            {
                Label closdram = (Label)e.Row.FindControl("lblgClosDramtcon");
                Label closcramt = (Label)e.Row.FindControl("lblgvClosCramtcon");
                Label netdramt = (Label)e.Row.FindControl("lblgvnetdramtcon");
                Label netcramt = (Label)e.Row.FindControl("lblgvnetcramtcon");
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                closdram.Font.Bold = true;
                closcramt.Font.Bold = true;
                netdramt.Font.Bold = true;
                netcramt.Font.Bold = true;
                //actdesc.Style.Add("text-align", "right");

            }
            else if (grp == "B" && code == "AAAAAAAAAAAA")
            {

                Label closdram = (Label)e.Row.FindControl("lblgClosDramtcon");
                Label closcramt = (Label)e.Row.FindControl("lblgvClosCramtcon");
                Label netdramt = (Label)e.Row.FindControl("lblgvnetdramtcon");
                Label netcramt = (Label)e.Row.FindControl("lblgvnetcramtcon");
                actcode.Font.Bold = true;
                actdesc.Font.Bold = true;
                closdram.Font.Bold = true;
                closcramt.Font.Bold = true;
                netdramt.Font.Bold = true;
                netcramt.Font.Bold = true;

            }
            ///---------------------------------//// 

            if (grp == "B" && ASTUtility.Left(mACTCODE, 1) == "4")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else if (grp == "B" && lebel2 == "")
            {

                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC;
            }
            else
            {
                if (grp == "B" && ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }











            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesccon");
            //    Label dramt = (Label)e.Row.FindControl("lblgvDramtcon");
            //    Label cramt = (Label)e.Row.FindControl("lblgvCramtcon");
            //    Label closdramt = (Label)e.Row.FindControl("lblgvclodramtcon");
            //    Label closcramt = (Label)e.Row.FindControl("lblgvclocramtcon");
            //    string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpcode")).ToString();

            //    if (grp == "")
            //    {
            //        return;
            //    }
            //    if (grp == "A")
            //    {

            //        actdesc.Font.Bold = true;
            //        dramt.Font.Bold = true;
            //        cramt.Font.Bold = true;
            //        closdramt.Font.Bold = true;
            //        closcramt.Font.Bold = true;
            //       // actdesc.Style.Add("text-align", "right");


            //    }

            //}
        }
        protected void lnkBankPosition02_Click(object sender, EventArgs e)
        {

            Session.Remove("tblBankPosition");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvBankPosition02.DataSource = null;
                this.gvBankPosition02.DataBind();
                return;
            }
            Session["tblBankPosition"] = ds1.Tables[0];
            this.gvBankPosition02.DataSource = ds1.Tables[0];
            this.gvBankPosition02.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvBankPosition02.HeaderRow.FindControl("hlbtnbnkpdataExel02")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ds1.Dispose();
            Session["Report1"] = gvBankPosition;
            ((HyperLink)this.gvBankPosition02.HeaderRow.FindControl("hlbtnbnkpdataExel02")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }


        protected void lnkbtnCashBankBal_Click(object sender, EventArgs e)
        {
            Session.Remove("tblBankPosition");
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvCABankBal.DataSource = null;
                this.gvCABankBal.DataBind();
                return;
            }
            Session["tblBankPosition"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["tblBankPosition"] = ds1.Tables[0];
            this.gvCABankBal.DataSource = (DataTable)Session["tblBankPosition"];
            this.gvCABankBal.DataBind();

            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0,indexofamp), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvCABankBal.HeaderRow.FindControl("hlbtnbnkpdataExelcb")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            ds1.Dispose();
            Session["Report1"] = gvCABankBal;
            ((HyperLink)this.gvCABankBal.HeaderRow.FindControl("hlbtnbnkpdataExelcb")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }


        protected void gvCABankBal_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink description = (HyperLink)e.Row.FindControl("HLgvDescbankcb");
                Label netbal = (Label)e.Row.FindControl("lblgvnetbalcb");
                Label opnbalcb = (Label)e.Row.FindControl("lblgvopnamcb");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcb");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    netbal.Font.Bold = true;
                    opnbalcb.Font.Bold = true;
                    closam.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }
                else if (ASTUtility.Right(code, 4) == "0000")
                {

                    description.Font.Bold = true;
                    opnbalcb.Font.Bold = true;
                    closam.Font.Bold = true;
                    description.Style.Add("text-align", "left");


                }


            }

        }
        protected void dgvBSdet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDescbdet");
            Label lblcode = (Label)e.Row.FindControl("lblgvcodebdet");
            Label clobal = (Label)e.Row.FindControl("lblgvclobalbdet");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamtbdet");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamtbdet");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();


            if (code == "")
            {
                return;
            }
            if (code == "01DAAAAA" || code == "02IAAAAA")
            {
                hlink1.Style.Add("color", "green");
                lblcode.Style.Add("color", "green");
                clobal.Style.Add("color", "green");
                opnamt.Style.Add("color", "green");
                cuamt.Style.Add("color", "green");
                hlink1.Style.Add("font-weight", "bolder");
                lblcode.Style.Add("font-weight", "bolder");
                clobal.Style.Add("font-weight", "bolder");
                opnamt.Style.Add("font-weight", "bolder");
                cuamt.Style.Add("font-weight", "bolder");
            }
            else if (code == "01010000" || code == "01020000" || code == "02010000" || code == "02020000" || code == "02030000")
            {
                hlink1.Style.Add("color", "blue");
                lblcode.Style.Add("color", "blue");
                clobal.Style.Add("color", "blue");
                opnamt.Style.Add("color", "blue");
                cuamt.Style.Add("color", "blue");
            }
            else if (code.Length == 8 && (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA"))
            {
                hlink1.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblcode.Attributes["style"] = "color:maroon; font-weight:bold;";
                clobal.Attributes["style"] = "color:maroon; font-weight:bold;";
                opnamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                cuamt.Attributes["style"] = "color:maroon; font-weight:bold;";

            }
        }
        protected void lnkokbdet2_Click(object sender, EventArgs e)
        {
            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.dgvBSdet.DataSource = null;
                this.dgvBSdet.DataBind();
                return;
            }

            // this.gvDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.dgvBSdet.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatetobdet2.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBSdet.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrombdet2.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBSdet.DataSource = HiddenSameData(ds1.Tables[0]);
            this.dgvBSdet.DataBind();





            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvDetails.HeaderRow.FindControl("hlbtnCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["DetTrBal"] = ds1.Tables[0];
        }
        protected void lnkokisdet2_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetComcod();
            string mTRNDAT1 = this.txtDatefromisdet2.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDatetoisdet2.Text.Substring(0, 11);
            string Opndate = this.txtOpeningDate.Text.Substring(0, 11);



            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_TB", "RPTINCOMESTDETAILS02", mTRNDAT1, mTRNDAT2, "", Opndate, "", "", "", "", "");

            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefromisdet2.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatetoisdet2.Text).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefromisdet2.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();
            ds1.Dispose();



        }
        protected void dgvIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Label lblgvopnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobal");


            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (code == "3AAAAAAAAAAA")
            {
                hlink1.Style.Add("color", "blue");
                hlink1.NavigateUrl = "LinkAccount.aspx?Type=SalDetails&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }




            if (code == "030102AA" || code == "030201AA" || code == "030201CA" || code == "030202AA" || code == "030301AA" || code == "030302AA" || code == "030501AA" || code == "030502AA" || code == "03BAAAAA" || code == "03CAAAAA")
            {

                hlink1.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvcuamt.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvopnamt.Attributes["style"] = "color:blue; font-weight:bold;";
                lblgvclobal.Attributes["style"] = "color:blue; font-weight:bold;";

            }

            else if (ASTUtility.Right(code, 2) == "00")
            {
                hlink1.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvcuamt.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvopnamt.Attributes["style"] = "color:green; font-weight:bold;";
                lblgvclobal.Attributes["style"] = "color:green; font-weight:bold;";

            }

            else
            {

                hlink1.Attributes["style"] = "color:black;";
                lblgvcuamt.Attributes["style"] = "color:black;";
                lblgvopnamt.Attributes["style"] = "color:black; ";
                lblgvclobal.Attributes["style"] = "color:black;";
            }
        }

        protected void gvBankPosition_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescbank");
            Label lblgvopnbal = (Label)e.Row.FindControl("lblgvopnbal");
            Label lblgvclobalbank = (Label)e.Row.FindControl("lblgvclobalbank");
            //Label lblgvclobal = (Label)e.Row.FindControl ("lblgvclobal");

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();

            if (code == "1901AAAAAAAA" || code == "1902AAAAAAAA" || code == "2202AAAAAAAA" || code == "2901AAAAAAAA" || code == "2902AAAAAAAA" || code == "AAAABBBBAAAA")
            {

                hlink1.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvopnbal.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvclobalbank.Attributes["style"] = "color:maroon; font-weight:bold;";


            }
        }
        //protected void dgv1_RowCreated(object sender, GridViewRowEventArgs e)
        //{

        //    GridViewRow gvRow = e.Row;
        //    if (gvRow.RowType == DataControlRowType.Header)
        //    {


        //        GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

        //        //  gvrow.Cells.Remove(TableCell [0]);

        //        TableCell cell01 = new TableCell();
        //        cell01.Text = "Sl.No.";
        //        cell01.HorizontalAlign = HorizontalAlign.Center;
        //        cell01.RowSpan = 2;
        //        gvrow.Cells.Add(cell01);



        //        TableCell cell02 = new TableCell();
        //        cell02.Text = "Code";
        //        cell02.HorizontalAlign = HorizontalAlign.Center;
        //        cell02.RowSpan = 2;
        //        gvrow.Cells.Add(cell02);

        //        TableCell cell03 = new TableCell();
        //        cell03.Text = "Description Of Accounts";
        //        cell03.HorizontalAlign = HorizontalAlign.Center;
        //        cell03.ColumnSpan = 2;
        //        gvrow.Cells.Add(cell03);


        //        //TableCell cell04 = new TableCell();
        //        //cell04.Text = "";
        //        //cell04.HorizontalAlign = HorizontalAlign.Center;
        //        //cell04.RowSpan = 1;
        //        //gvrow.Cells.Add(cell04);


        //        //TableCell cell05 = new TableCell();
        //        //cell05.Text = "Opening";
        //        //cell05.HorizontalAlign = HorizontalAlign.Center;
        //        //cell05.Attributes["style"] = "font-weight:bold;";
        //        //cell05.ColumnSpan = 2;
        //        //gvrow.Cells.Add(cell05);





        //        //TableCell cell08 = new TableCell();
        //        //cell08.Text = "Dr. Amount";
        //        //cell08.HorizontalAlign = HorizontalAlign.Center;
        //        //cell08.RowSpan = 2;
        //        //gvrow.Cells.Add(cell08);



        //        //TableCell cell09a = new TableCell();
        //        //cell09a.Text = "Cr. Amount";
        //        //cell09a.HorizontalAlign = HorizontalAlign.Center;
        //        //cell09a.RowSpan = 2;
        //        //gvrow.Cells.Add(cell09a);


        //        //TableCell cell09 = new TableCell();
        //        //cell09.Text = "Closing";
        //        //cell09.HorizontalAlign = HorizontalAlign.Center;
        //        //cell09.Attributes["style"] = "font-weight:bold;";
        //        //cell09.ColumnSpan = 2;
        //        //gvrow.Cells.Add(cell09);







        //        //TableCell cell11 = new TableCell();
        //        //cell11.Text = "Net Amt";
        //        //cell11.HorizontalAlign = HorizontalAlign.Center;
        //        //cell11.Attributes["style"] = "font-weight:bold;";
        //        //cell11.ColumnSpan = 1;
        //        //gvrow.Cells.Add(cell11);

        //        //TableCell cell12 = new TableCell();
        //        //cell12.Text = "";
        //        //cell12.HorizontalAlign = HorizontalAlign.Center;
        //        //cell12.Attributes["style"] = "font-weight:bold;";
        //        //cell12.ColumnSpan = 1;
        //        //gvrow.Cells.Add(cell11);
        //       dgv1.Controls[0].Controls.AddAt(0, gvrow);
        //    }
        //}

        protected void lnkTrial02_Click(object sender, EventArgs e)
        {


            DataSet ds1 = this.GetDataForReport();
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.gvtrialbalance01.DataSource = null;
                this.gvtrialbalance01.DataBind();
                return;

            }

            Session["tbltb"] = HiddenSameData(ds1.Tables[0]);

            this.gvtrialbalance01.DataSource = (DataTable)Session["tbltb"];
            this.gvtrialbalance01.DataBind();
            //string Type = this.Request.QueryString["Type"].ToString().Trim();
            //if (Type == "HOTB")
            //{
            //    this.dgv1.Columns[11].Visible = false;
            //}

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            ((HyperLink)this.gvtrialbalance01.HeaderRow.FindControl("hlbtntbCdataExel01")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));









            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfopndramt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0); ");

            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfopncramt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0); ");
            //this.dgv1.Columns[2].FooterText = Convert.ToDouble(ds1.Tables[1].Rows[0]["opndram"]).ToString("#,##0.00;(#,##0.00); ") + "<br>" + Convert.ToDouble(ds1.Tables[1].Rows[0]["opncram"]).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfDramt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfCramt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["cram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfclodramt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closdram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfclocramt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closcram"]).ToString("#,##0;(#,##0); ");
            ((Label)this.gvtrialbalance01.FooterRow.FindControl("lblfnetamt01")).Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["closam"]).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvtrialbalance01;
            ((HyperLink)this.gvtrialbalance01.HeaderRow.FindControl("hlbtntbCdataExel01")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }
        protected void gvtrialbalance01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComcod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc01");
            string mCOMCOD = comcod;
            string mACTCODE = ((Label)e.Row.FindControl("lblgvcode01")).Text;
            string mACTDESC = ((Label)e.Row.FindControl("lblgvAcDesc01")).Text;




            string mTRNDAT1 = this.txtDatefromtb.Text;
            string mTRNDAT2 = this.txtDatetotb.Text;
            //------------------------------//////
            Label actcode = (Label)e.Row.FindControl("lblgvcode01");
            HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc01");
            Label lblgvopndramt = (Label)e.Row.FindControl("lblgvopndramt01");
            Label lblgvopncramt = (Label)e.Row.FindControl("lblgvopncramt01");
            Label lblgvDramt = (Label)e.Row.FindControl("lblgvDramt01");
            Label lblgvCramt = (Label)e.Row.FindControl("lblgvCramt01");
            Label lblgvclodramt = (Label)e.Row.FindControl("lblgvclodramt01");
            Label lblgvclocramt = (Label)e.Row.FindControl("lblgvclocramt01");
            Label lblgvnetamt = (Label)e.Row.FindControl("lblgvnetamt01");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString().Trim();
            string spcfcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString().Trim();


            string opnoption = "withoutopening";

            // string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();




            if (code == "")
            {
                return;
            }

            // string level = this.ddlReportLevel.SelectedValue.ToString();

            if (ASTUtility.Right(code, 4) == "0000")
            {
                actcode.Attributes["style"] = "color:maroon;font-weight:bold;";
                actdesc.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvopndramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvopncramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvDramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvCramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvclodramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvclocramt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvnetamt.Attributes["style"] = "color:maroon;font-weight:bold;";



            }


            if ((code.Substring(0, 2) != "18" && code.Substring(0, 2) != "26" && code.Substring(0, 2) != "25") && (ASTUtility.Right(rescode, 4) == "0000"))
            {



                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }


            else if (code.Substring(0, 2) == "18")
            {
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&rescode=" + rescode +
                "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=";

            }


            else if (code.Substring(0, 2) == "25" && ASTUtility.Right(code, 4) != "0000")  // STD
            {

                string sircode = "[5-6][1-2]";
                spcfcod = rescode;
                mACTCODE = mACTCODE.Substring(0, 2);
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&sircode=" + sircode + "&spcfcod=" + spcfcod +
                      "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }


            else if (code.Substring(0, 2) == "26" && ASTUtility.Right(code, 4) != "0000")
            {

                string sircode = ((rescode.Substring(0, 2) == "99") ? "99" : (rescode.Substring(0, 2) == "98") ? "98" : "[0-9][1-7]");

                mACTCODE = mACTCODE.Substring(0, 2);
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE + "&sircode=" + sircode +
                      "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            // && ASTUtility.Right(spcfcode, 4) != "0000"
            else if (code.Substring(0, 4) == "2301")
            {
                if (this.ddlReportLeveltb2.SelectedValue.ToString() == "4")
                    hlink1.NavigateUrl = "RptlinkAccAITVATASD.aspx?frmdate=" + mTRNDAT1 + "&todate=" + mTRNDAT2 + "&rescode=" + spcfcod + "&subcode=" + rescode;
                else
                    hlink1.NavigateUrl = "RptAccAITVATASDAllSup.aspx?Type=Report&sircode=" + rescode + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }

            else if (ASTUtility.Right(rescode, 4) != "0000")
            {


                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledgerDetials&comcod=" + mCOMCOD + "&rescode=" + rescode +
                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=&daywise=daywise";



            }

            else
            {
                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }







        }
        protected void chknetbalance_CheckedChanged(object sender, EventArgs e)
        {
            this.lblnetbalance.Text = (this.chknetbalance.Checked) ? "Net Balance" : "Gross Balance";
        }
    }
}
