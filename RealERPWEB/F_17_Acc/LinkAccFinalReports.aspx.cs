using System;
using System.Collections;
using System.Collections.Generic;
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
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{

    public partial class LinkAccFinalReports : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {

                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


                string mRepID = "IS";

                this.MultiView1.ActiveViewIndex = ((mRepID == "IS" || mRepID == "IS2") ? 0 : (mRepID == "BS" ? 1 : ((mRepID == "PS" || mRepID == "SS") ? 2
                    : (mRepID == "BE" ? 3 : (mRepID == "SPC" ? 4 : ((mRepID == "IPRJ" || mRepID == "IACUR") ? 5 : (mRepID == "SPC" ? 6 : (mRepID == "LandSt" ? 8 : 7))))))));

                //DateTime opndate = this.GetOpeningDate();

                //string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //DateTime yfdate = Convert.ToDateTime("01-Jan-" + curdate.Substring(7));
                //this.txtDatefrom.Text = (yfdate < opndate) ? opndate.AddDays(1).ToString("dd-MMM-yyyy") : yfdate.ToString("dd-MMM-yyyy");
                //this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                //this.txtDatefrom_CalendarExtender.StartDate = Convert.ToDateTime(opndate.AddDays(1));

                lbtnOk_Click(null, null);

            }


        }
        private DateTime GetOpeningDate()
        {

            string comcod = this.GetCompCode();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return (System.DateTime.Today);
            }

            return (Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]));

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
            return (hst["comcod"].ToString());

        }

        private string Company()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Calltype = "";
            switch (comcod)
            {
                case "2305":
                case "3306":
                    Calltype = "BSR_WIP_COMPANY_0";
                    break;
                default:
                    Calltype = "BSR_COMPANY_0";
                    break;
            }
            return Calltype;
        }


        private string GetCompanyHeadAllocation()
        {
            string comcod = this.GetCompCode();
            string headallocation = "";
            switch (comcod)
            {
                case "3348":// Credence
                case "3101"://Asit
                    headallocation = "Headallocation";
                    break;


                default:

                    break;


            }

            return headallocation;


        }
        protected void lnkbtnISOk_OnClick(object sender, EventArgs e)
        {




            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            //string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();// this.txtDatefrom.Text.Substring(0, 11);
            //string mTRNDAT2 = this.Request.QueryString["Date2"].ToString(); //this.txtDateto.Text.Substring(0, 11);

            string mTRNDAT1 = this.txtfrmdate.Text.Substring(0, 11);
            string mTRNDAT2 = this.txttodate.Text.Substring(0, 11);



            string Opndate = this.Request.QueryString["opndate"].ToString();
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string headallocation = this.GetCompanyHeadAllocation();

            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, headallocation, "", "", "", "");




            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(mTRNDAT1).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(Opndate).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();
            ds1.Dispose();
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=INDetails&Date1=" + mTRNDAT1
                        + "&Date2=" + mTRNDAT2 + "&opndate=" + Opndate;



            //this.GetIncomeStatement();
        }


        protected void GetIncomeStatement()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();// this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString(); //this.txtDateto.Text.Substring(0, 11);

            //string date1 = this.txtfrmdate.Text.Substring(0, 11);
            //string date2 = this.txttodate.Text.Substring(0, 11);

            this.txtfrmdate.Text = mTRNDAT1;
            this.txttodate.Text = mTRNDAT2;



            string Opndate = this.Request.QueryString["opndate"].ToString();
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();

            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string headallocation = this.GetCompanyHeadAllocation();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, headallocation, "", "", "", "");

            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(mTRNDAT1).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(Opndate).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();
            ds1.Dispose();
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=INDetails&Date1=" + mTRNDAT1
                        + "&Date2=" + mTRNDAT2 + "&opndate=" + Opndate;


        }
        private string compIncomest()
        {

            string compIncomest;
            string comcod = this.GetCompCode();
            switch (comcod)
            {


                case "3333":
                case "3101":
                    compIncomest = "IncomestAlli";
                    break;

                default:
                    compIncomest = "IncomeGen";
                    break;





            }

            return compIncomest;
        }
        protected void GetIncomeStatementForPrint()
        {



            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();




            LocalReport Rpt1 = new LocalReport();

            DataTable dt = (DataTable)ViewState["tblAcc"];

            ////DataView dv = dt.DefaultView;
            ////dv.RowFilter = ("actcode4 not like'%00'");
            ////dt = dv.ToTable();



            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.CompIncome>();
            var lst1 = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.CompIncome01>();

            if (comcod == "3101" || comcod == "3333" || comcod == "3335")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccIncomeStAlli", lst, lst1, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccIncomeStAlli", lst, lst1, null);
            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Statement of Comprehensive Income"));
            Rpt1.SetParameters(new ReportParameter("YearEnd", Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("MMMM yyyy")));
            Rpt1.SetParameters(new ReportParameter("Todat", System.DateTime.Today.ToString("MMMM dd, yyyy")));
            Rpt1.SetParameters(new ReportParameter("Curdate", Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Predate", Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy")));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();// this.txtDatefrom.Text.Substring(0, 11);
            //string mTRNDAT2 = this.Request.QueryString["Date2"].ToString(); //this.txtDateto.Text.Substring(0, 11);
            //string Opndate = this.Request.QueryString["opndate"].ToString();
            //string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();



            ////string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            ////string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            ////string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();


            //DataTable dt = (DataTable)ViewState["tblAcc"];

            //DataView dv = dt.DefaultView;
            //dv.RowFilter = ("actcode4 not like'%00'");
            //dt = dv.ToTable();

            ////("right(actcode4,2) <>"00")'" );

            ////DataView dv1 = ds2.Tables[0].DefaultView;
            ////  dv1.RowFilter = ("reqno <>'" + mREQNO + "'");
            ////  DataTable dt = dv1.ToTable();

            ////string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            ////DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
            ////        ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");

            ////if (ds1 == null)
            ////    return;

            ////mTRNDAT1 = this.txtDatefrom.Text;
            ////mTRNDAT2 = this.txtDateto.Text;
            ////mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();

            //ReportDocument RptInSt = new ReportDocument();

            //string compIncomest = this.compIncomest();



            //if (compIncomest == "IncomestAlli")
            //{
            //    RptInSt = new RealERPRPT.R_17_Acc.RptAccIncomeStAlli();
            //}

            //else
            //{
            //    RptInSt = new RealERPRPT.R_17_Acc.RptAccIncomeSt01();

            //}


            //RptInSt.SetDataSource(dt);
            //// RptInSt.SetDataSource((DataTable)ViewState["tblAcc"]);

            //TextObject TxtCompName = RptInSt.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //TxtCompName.Text = comnam;

            ////TextObject TxtRptTitle = RptInSt.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            ////TxtRptTitle.Text = mLEVEL1;

            //TextObject TxtRptPeriod = RptInSt.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            //TxtRptPeriod.Text = "For the year ended " + Convert.ToDateTime(mTRNDAT1).ToString("dd MMMM yyyy");
            //// TxtRptPeriod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            //TextObject txtdate = RptInSt.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = System.DateTime.Today.ToString("MMMM dd, yyyy");


            //TextObject txtCuramt = RptInSt.ReportDefinition.ReportObjects["txtCuramt"] as TextObject; //"\n"+
            //txtCuramt.Text = Convert.ToDateTime(mTRNDAT1).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy");
            //TextObject txtPreamt = RptInSt.ReportDefinition.ReportObjects["txtPreamt"] as TextObject;
            //txtPreamt.Text = Convert.ToDateTime(Opndate).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(mTRNDAT1).ToString("dd-MMM-yyyy");



            ////TextObject txtCuramt = RptInSt.ReportDefinition.ReportObjects["txtCuramt"] as TextObject;
            ////txtCuramt.Text = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy"); ;
            ////TextObject txtPreamt = RptInSt.ReportDefinition.ReportObjects["txtPreamt"] as TextObject;
            ////txtPreamt.Text = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy"); ;


            ////TextObject txtuserinfo = RptInSt.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //RptInSt.SetParameterValue("ComLogo", ComLogo);
            ////--------------------Export to PDF--------------------------------------------------
            //Session["Report1"] = RptInSt;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void GetBalanceSheetForPrint()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            switch (comcod)
            {
                case "3101":
                case "3333":
                    this.GetBalanceSheetForPrintAlli();
                    break;
                default:
                    this.GetBalanceSheetForPrintAlli();
                    break;
            }




        }

        private void GetBalanceSheetForPrintAlli()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string mTRNDAT1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            DataTable dt = (DataTable)ViewState["tblAcc"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetAlli", list, null, null);

            Rpt1.EnableExternalImages = true;
            Hashtable reportParm = new Hashtable();
            Rpt1.SetParameters(new ReportParameter("TxtCompName", comnam));
            Rpt1.SetParameters(new ReportParameter("TxtOpening", Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtClosing", Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtRptPeriod", "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("txtdate", System.DateTime.Today.ToString("MMMM dd, yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mTRNDAT1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            //string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            ////string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            //////DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "BSR_COMPANY_0" +
            //////        ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");

            //////if (ds1 == null)
            //////    return;


            //ReportDocument RptBalanceSheet = new RealERPRPT.R_17_Acc.RptBalanceSheetAlli();
            //RptBalanceSheet.SetDataSource((DataTable)ViewState["tblAcc"]);

            //TextObject TxtCompName = RptBalanceSheet.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //TxtCompName.Text = comnam;

            ////TextObject TxtRptTitle = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            ////TxtRptTitle.Text =  mLEVEL1;

            //TextObject TxtRptPeriod = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            //TxtRptPeriod.Text = "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy");


            //TextObject TxtOpening = RptBalanceSheet.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy");

            //TextObject TxtClosing = RptBalanceSheet.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text = Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy");


            ////TextObject txtrptsysdate = RptBalanceSheet.ReportDefinition.ReportObjects["txtrptsysdate"] as TextObject;
            ////txtrptsysdate.Text = System.DateTime.Today.ToString("MMMM dd, yyyy");

            ////TextObject txtuserinfo = RptBalanceSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //RptBalanceSheet.SetParameterValue("ComLogo", ComLogo);

            ////--------------------Export to PDF--------------------------------------------------
            //Session["Report1"] = RptBalanceSheet;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }






        protected void GetSpeBalnSheetPrint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string mTRNDAT1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            //ReportDocument RptBalanceSheet = new RealERPRPT.R_17_Acc.RptSpeBalnSheet();
            //RptBalanceSheet.SetDataSource((DataTable)ViewState["tblAcc"]);
            //TextObject TxtCompName = RptBalanceSheet.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //TxtCompName.Text = comnam;

            //TextObject TxtRptTitle = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            //TxtRptTitle.Text = "SPECIAL BALANCE  SHEET";

            //TextObject TxtRptPeriod = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            //TxtRptPeriod.Text = "As on " + mTRNDAT2 + "";

            //TextObject TxtOpening = RptBalanceSheet.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy") + " Taka";

            //TextObject TxtClosing = RptBalanceSheet.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text = Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy") + " Taka";
            //TextObject txtuserinfo = RptBalanceSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////--------------------Export to PDF--------------------------------------------------
            //Session["Report1"] = RptBalanceSheet;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        protected void GetBalanceSheet()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string CallType = this.Company();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", CallType +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");


            this.dgvBS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.DataSource = ds1.Tables[0];
            this.dgvBS.DataBind();
            ViewState["tblAcc"] = ds1.Tables[0];
            //hlbtnDetailsbs
            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvBS.HeaderRow.FindControl("hlbtnDetailsbs")).NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=Details&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

        }

        private void GetSpecialBalanceSheet()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTSPBSHEET",
                    mTRNDAT1, mTRNDAT2, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgvSpBS.DataSource = null;
                this.dgvSpBS.DataBind();
                return;

            }

            this.dgvSpBS.Columns[1].HeaderText = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvSpBS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvSpBS.DataSource = (DataTable)ViewState["tblAcc"];
            this.dgvSpBS.DataBind();




        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mRepID = "IS";
            if (Request.QueryString["RepType"] != null)
                mRepID = Request.QueryString["RepType"].ToString();
            switch (mRepID)
            {
                case "IS":
                    this.GetIncomeStatementForPrint();
                    break;
                case "BS":
                    this.GetBalanceSheetForPrint();
                    break;
                case "SPBS":
                    this.GetSpeBalnSheetPrint();
                    break;
                case "PS":
                    this.RptProjectReoprt();
                    break;
                case "SS":
                    this.GetProjectStatusForPrint();
                    break;
                case "BE":
                    this.ReportBudgetvsExpenses();
                    break;
                case "SPC":
                    this.PrintSpcDesc();
                    break;
                case "IPRJ":
                case "IACUR":
                    this.PrintIncomeIPrj();
                    break;
                case "SHEQUITY":
                    this.PrintShareQty();
                    break;
                case "LandSt":
                    this.ReportLandStatus();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + mRepID;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mRepID = "IS";
            if (Request.QueryString["RepType"] != null)
                mRepID = Request.QueryString["RepType"].ToString();
            switch (mRepID)
            {
                case "IS":
                    this.GetIncomeStatement();
                    break;
                case "BS":
                    this.GetBalanceSheet();
                    break;

                case "SPBS":
                    this.GetSpecialBalanceSheet();
                    break;
                case "PS":
                    this.GetProjectStatus();
                    break;
                case "BE":
                    this.GetBudgetVsExpenses();
                    break;

                case "SPC":
                    this.GetSpcDesc();
                    break;
                case "IPRJ":
                case "IACUR":
                    this.GetIncomeIPrj();
                    break;
                case "SHEQUITY":
                    this.SHOWSHAREEQUIT();
                    break;
                case "LandSt":
                    this.GetLandStatus();
                    this.PanelNote.Visible = true;
                    break;
            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report: " + mRepID;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void GetSpcDesc()
        {
            Session.Remove("tblspc");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = this.ddlProject.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTRESSPCPRJWISE", actcode, date1, "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            Session["tblspc"] = ds2.Tables[0];
            this.dgvSPC.DataSource = ds2.Tables[0];
            this.dgvSPC.DataBind();

            ((Label)this.dgvSPC.FooterRow.FindControl("lblgvfamount")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(trnam)", "")) ?
                        0.00 : ds2.Tables[0].Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");


        }

        private void GetIncomeIPrj()
        {
            Session.Remove("tbliprj");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string actcode = this.ddlProjectInd.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Type = this.Request.QueryString["RepType"].ToString();

            string Calltype = ((Type == "IPRJ") ? ((this.rbtnList1.SelectedIndex == 0) ? "RPTINCOMESTATMENTINPRJ" : "RPTINCOMESTATMENTINPRJSUM") : ((this.rbtnList1.SelectedIndex == 0) ? "RPTINCOMESTATMENTACURAL" : "RPTINCOMESTATMENTACURALSUM"));

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", Calltype, "", date1, actcode, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvIncome.DataSource = null;
                this.gvIncome.DataBind();
                this.gvInfast.DataSource = null;
                this.gvInfast.DataBind();

                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tbliprj"] = dt;
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rescode  not like '31%'");

            this.gvIncome.DataSource = dv.ToTable();
            this.gvIncome.DataBind();
            dv = dt.DefaultView;
            dv.RowFilter = ("rescode like '31%'");
            this.gvInfast.DataSource = dv.ToTable();
            this.gvInfast.DataBind();
            if (dv.ToTable().Rows.Count == 0)
                return;
            ((Label)this.gvInfast.FooterRow.FindControl("lgvFTfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("Sum(trnam)", "")) ?
                        0.00 : dv.ToTable().Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");

        }
        private void SHOWSHAREEQUIT()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("tblfinst");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string Level = this.DDListLevels.SelectedValue.ToString();


            string levelval = (Level == "1") ? "2" : (Level == "2") ? "4" : (Level == "3") ? "8" : "12";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTSHAREHOLDEREQUITY", date1, date2, levelval, "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvsequ.DataSource = null;
                this.gvsequ.DataBind();

            }

            DataTable dt = ds2.Tables[0];
            Session["tblfinst"] = dt;

            this.gvsequ.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy");
            this.gvsequ.Columns[5].HeaderText = "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy");

            this.gvsequ.DataSource = dt;
            this.gvsequ.DataBind();

            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFopnamse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFcramtse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cram)", "")) ?
                        0.00 : dt.Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFdramtse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dram)", "")) ?
                        0.00 : dt.Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvsequ.FooterRow.FindControl("lgvFclosamse")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                        0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");





        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string RptType = Request.QueryString["RepType"].ToString();
            int j;
            string grpcode;
            switch (RptType)
            {

                case "IS":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpcode"] = "";



                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }

                    }
                    break;



                case "SPBS":
                    grpcode = dt1.Rows[0]["grpcode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grpcode"].ToString();
                        }

                    }
                    break;

                default:
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                        }

                    }

                    break;

            }


            return dt1;


        }

        protected void ReportBudgetvsExpenses()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["BvsE"];

            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccFinalReports();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            //TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            //txtadress.Text = comadd;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["fdate"] as TextObject;
            //txtfdate.Text = this.txtDatefrom.Text.Trim();
            txtfdate.Text = "As on " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "";

            //TextObject txttdate = rptstk.ReportDefinition.ReportObjects["tdate"] as TextObject;
            //txttdate.Text = this.txtDateto.Text.Trim();

            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = this.ddlRptGroupbve.SelectedValue.ToString().Trim();
            txtlevel.Text = "Level : " + this.ddlRptGroupbve.SelectedValue.ToString().Trim() + "";

            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlHAccProject.SelectedItem.ToString().Trim().Substring(13, this.ddlHAccProject.SelectedItem.ToString().Trim().Length - 13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ReportLandStatus()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["BvsE"];

            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccFinalReportsLand();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            ////TextObject txtadress = rptstk.ReportDefinition.ReportObjects["txtaddress"] as TextObject;
            ////txtadress.Text = comadd;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["fdate"] as TextObject;
            ////txtfdate.Text = this.txtDatefrom.Text.Trim();
            //txtfdate.Text = "As on " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "";

            ////TextObject txttdate = rptstk.ReportDefinition.ReportObjects["tdate"] as TextObject;
            ////txttdate.Text = this.txtDateto.Text.Trim();

            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            ////txtlevel.Text = this.ddlRptGroupbve.SelectedValue.ToString().Trim();
            //txtlevel.Text = "Level : " + this.ddlGrpAcc.SelectedValue.ToString().Trim() + "";

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlAcHead.SelectedItem.ToString().Trim().Substring(13, this.ddlAcHead.SelectedItem.ToString().Trim().Length - 13);


            //////////----------------------------Note////////////////////////////
            //DataTable dt2 = (DataTable)Session["tbNote"];
            //TextObject txtndesc1 = rptstk.ReportDefinition.ReportObjects["txtndesc1"] as TextObject;
            //txtndesc1.Text = dt2.Rows[0]["ndesc"].ToString();
            //TextObject txtndesc2 = rptstk.ReportDefinition.ReportObjects["txtndesc2"] as TextObject;
            //txtndesc2.Text = dt2.Rows[1]["ndesc"].ToString();
            //TextObject txtndesc3 = rptstk.ReportDefinition.ReportObjects["txtndesc3"] as TextObject;
            //txtndesc3.Text = dt2.Rows[2]["ndesc"].ToString();
            //TextObject txtndesc4 = rptstk.ReportDefinition.ReportObjects["txtndesc4"] as TextObject;
            //txtndesc4.Text = dt2.Rows[3]["ndesc"].ToString();

            //TextObject txtqty1 = rptstk.ReportDefinition.ReportObjects["txtqty1"] as TextObject;
            //txtqty1.Text = Convert.ToDouble(dt2.Rows[0]["qty1"]).ToString("#,##0.00;(#,##0.00); - "); 
            //TextObject txtqty2 = rptstk.ReportDefinition.ReportObjects["txtqty2"] as TextObject;
            //txtqty2.Text = Convert.ToDouble(dt2.Rows[1]["qty1"]).ToString("#,##0.00;(#,##0.00); - "); 
            //TextObject txtqty3 = rptstk.ReportDefinition.ReportObjects["txtqty3"] as TextObject;
            //txtqty3.Text = Convert.ToDouble(dt2.Rows[2]["qty1"]).ToString("#,##0.00;(#,##0.00); - ");
            //TextObject txtqty7 = rptstk.ReportDefinition.ReportObjects["txtqty7"] as TextObject;
            //txtqty7.Text = Convert.ToDouble(dt2.Rows[3]["qty1"]).ToString("#,##0.00;(#,##0.00); - "); 

            //TextObject txtqty4 = rptstk.ReportDefinition.ReportObjects["txtqty4"] as TextObject;
            //txtqty4.Text = Convert.ToDouble(dt2.Rows[0]["qty2"]).ToString("#,##0.00;(#,##0.00); - "); 
            //TextObject txtqty5 = rptstk.ReportDefinition.ReportObjects["txtqty5"] as TextObject;
            //txtqty5.Text = Convert.ToDouble(dt2.Rows[1]["qty2"]).ToString("#,##0.00;(#,##0.00); - "); 
            //TextObject txtqty6 = rptstk.ReportDefinition.ReportObjects["txtqty6"] as TextObject;
            //txtqty6.Text = Convert.ToDouble(dt2.Rows[2]["qty2"]).ToString("#,##0.00;(#,##0.00); - ");
            //TextObject txtqty8 = rptstk.ReportDefinition.ReportObjects["txtqty8"] as TextObject;
            //txtqty8.Text = Convert.ToDouble(dt2.Rows[3]["qty2"]).ToString("#,##0.00;(#,##0.00); - "); 



            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintSpcDesc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblspc"];
            if (dt1.Rows.Count == 0)
                return;

            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptResSpecificition();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");

            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintIncomeIPrj()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tbliprj"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptIncomestIndPrj();

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = (this.Request.QueryString["RepType"] == "IPRJ") ? "Income Statement (Individual Project)" : "Income Statement (Acural Basis)";
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = "Project Name: " + this.ddlProjectInd.SelectedItem.Text.Substring(13);
            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);

            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        private void PrintShareQty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblfinst"];
            if (dt1.Rows.Count == 0)
                return;

            var list = dt1.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.IncomeStatementSHE>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptShareQty", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("rptTitle", "Statement Of  Changes In Equity"));
            rpt.SetParameters(new ReportParameter("date", "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy")));
            rpt.SetParameters(new ReportParameter("openingDate", Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd MMMM yyyy")));
            rpt.SetParameters(new ReportParameter("closingDate", "Balance as at " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd MMMM yyyy")));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void dgvIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Label lblgvopnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobal");
            string mCOMCOD = comcod;
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();// this.txtDatefrom.Text;
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString(); //this.txtDateto.Text;

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();

            DateTime date = this.GetOpeningDate();
            string opndate = date.ToString("dd-MMM-yyyy");
            if (code == "")
            {
                return;
            }
            if (code == "3AAAAAAAAAAA")
            {
                hlink1.Style.Add("color", "blue");
                // hlink1.NavigateUrl = "LinkAccount.aspx?Type=SalDetails&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

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

            // Hyper Link
            if (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA")
                hlink1.NavigateUrl = "LinkAccount.aspx?Type=IncomeDet02&acgcode=" + code + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&mdesc=" + mACTDESC + "&opndate=" + this.Request.QueryString["opndate"].ToString();



        }

        protected void dgvBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddYears(-1).ToString("dd-MMM-yyyy");

            string opendat = this.txtOpeningDate.Text;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");
            Label lblcode = (Label)e.Row.FindControl("lblgvcode");
            Label clobal = (Label)e.Row.FindControl("lblgvclobal");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();

            string level = this.DDListLevels.SelectedValue.ToString();
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
            else if (level == "4" && code.Length == 8 && (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA"))
            {
                hlink1.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblcode.Attributes["style"] = "color:maroon; font-weight:bold;";
                clobal.Attributes["style"] = "color:maroon; font-weight:bold;";
                opnamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                cuamt.Attributes["style"] = "color:maroon; font-weight:bold;";

            }
            else if (level == "2")
            {
                if (code == "02010600")   //F_17_Acc/AccFinalReports.aspx?RepType=IS
                {
                    hlink1.NavigateUrl = "AccFinalReports.aspx?RepType=IS=" + mCOMCOD + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");

                }
                else
                {
                    hlink1.NavigateUrl = "LinkAccount.aspx?Type=BalanceDet&acgcode=" + code.Substring(0, 6) + "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim() + "&mdesc=" + mACTDESC;

                }



            }




        }



        protected void GetBudgetVsExpenses()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("BvsE");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            // string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf";//(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlHAccProject.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroupbve.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BUDGETVSEX_PROJECT", "RPTBUDGETVSEXPENSES",
                         date1, "", TopHead, actcode, mRptGroup, "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            Session["BvsE"] = ds2.Tables[0];
            this.dgvBE.DataSource = ds2.Tables[0];
            this.dgvBE.DataBind();
            if (mRptGroup != "12")
            {
                this.dgvBE.Columns[3].Visible = false;
                this.dgvBE.Columns[4].Visible = false;
                this.dgvBE.Columns[5].Visible = false;
                this.dgvBE.Columns[7].Visible = false;
                this.dgvBE.Columns[8].Visible = false;
                this.dgvBE.Columns[10].Visible = false;
                this.dgvBE.Columns[11].Visible = false;
            }
            else
            {
                this.dgvBE.Columns[3].Visible = true;
                this.dgvBE.Columns[4].Visible = true;
                this.dgvBE.Columns[5].Visible = true;
                this.dgvBE.Columns[7].Visible = true;
                this.dgvBE.Columns[8].Visible = true;
                this.dgvBE.Columns[10].Visible = true;
                this.dgvBE.Columns[11].Visible = true;
            }


            ((Label)this.dgvBE.FooterRow.FindControl("lblftoamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closam)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgvBE.FooterRow.FindControl("lblfbgdam")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(bgdam)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(bgdam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgvBE.FooterRow.FindControl("lblftAvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(tavamt)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(tavamt)", ""))).ToString("#,##0;(#,##0); - ");



        }

        protected void GetLandStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            Session.Remove("BvsE");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            // string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf";//(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAcHead.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlGrpAcc.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BUDGETVSEX_PROJECT", "RPTLANDBUDGETVSEXPENSES",
                         date1, "", TopHead, actcode, mRptGroup, "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            Session["BvsE"] = ds2.Tables[0];
            Session["tbNote"] = ds2.Tables[1];
            this.gvlandSt.DataSource = ds2.Tables[0];
            this.gvlandSt.DataBind();

            this.gvNote.DataSource = ds2.Tables[1];
            this.gvNote.DataBind();

            //lblftavqty  lblftToQty lblftbgdqty

            ((Label)this.gvlandSt.FooterRow.FindControl("lblftToQty")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closqty)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(closqty)", ""))).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.gvlandSt.FooterRow.FindControl("lblftbgdqty")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(bgdqty)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(bgdqty)", ""))).ToString("#,##0.00;(#,##0.00); - ");
            ((Label)this.gvlandSt.FooterRow.FindControl("lblftavqty")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(tavqty)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(tavqty)", ""))).ToString("#,##0.00;(#,##0.00); - ");


            ((Label)this.gvlandSt.FooterRow.FindControl("lblftoamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closam)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvlandSt.FooterRow.FindControl("lblfbgdam")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(bgdam)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(bgdam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.gvlandSt.FooterRow.FindControl("lblftAvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(tavamt)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(tavamt)", ""))).ToString("#,##0;(#,##0); - ");



        }
        protected void GetProjectStatus()
        {
            Session.Remove("tblPS");
            DataSet ds2 = this.GetDataForProjectReport();
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                //this.lblmsg.Text = "There is no resource in this accounts.";
                //this.lblmsg.ForeColor = System.Drawing.Color.Blue;
                return;
            }
            Session["tblPS"] = ds2.Tables[0];
            DataTable dtr = (DataTable)Session["tblPS"];
            //DataView dvr = new DataView();
            //dvr = dtr.DefaultView;
            //dvr.RowFilter = ("grp1 = 'g01'");
            this.dgvPS.DataSource = dtr;
            this.dgvPS.DataBind();
            this.FooterCalculation(dtr, "dgvPS");


            //dvr = dtr.DefaultView;
            //dvr.RowFilter = ("grp1='g02'");
            //this.dgvPS31.DataSource = dvr.ToTable();
            //this.dgvPS31.DataBind();
            //this.FooterCalculation(dvr.ToTable(), "dgvPS31");


            //dvr = dtr.DefaultView;
            //dvr.RowFilter=("grp1='g03'");
            //this.dgvPS99.DataSource = dvr.ToTable();
            //this.dgvPS99.DataBind();
            //this.FooterCalculation(dvr.ToTable(), "dgvPS99");







        }
        private void FooterCalculation(DataTable dt, string GvName)
        {
            switch (GvName)
            {
                case "dgvPS":
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.dgvPS.FooterRow.FindControl("lblfopamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                        0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
                    ((Label)this.dgvPS.FooterRow.FindControl("lblfcuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                        0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");
                    ((Label)this.dgvPS.FooterRow.FindControl("lblfclDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closdramt)", "")) ?
                     0.00 : dt.Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); - ");
                    ((Label)this.dgvPS.FooterRow.FindControl("lblfclCramt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closcramt)", "")) ?
                      0.00 : dt.Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); - ");

                    string dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closdramt)", "")) ? 0.00 : dt.Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); 0");
                    string cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closcramt)", "")) ? 0.00 : dt.Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); 0");

                    ((Label)this.dgvPS.FooterRow.FindControl("lblfclBalAmt")).Text = (Convert.ToDouble(dramt) - Convert.ToDouble(cramt)).ToString("#,##0;(#,##0); 0");

                    break;

                    //case "dgvPS31":

                    // if (dt.Rows.Count == 0)
                    //     return;
                    //    ((Label)this.dgvPS31.FooterRow.FindControl("lblfopamt31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                    //       0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
                    //      ((Label)this.dgvPS31.FooterRow.FindControl("lblfcuamt31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                    //       0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");
                    //      ((Label)this.dgvPS31.FooterRow.FindControl("lblfclamt31")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                    //       0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");

                    //        break;

                    // case "dgvPS99":
                    //        if (dt.Rows.Count == 0)
                    //            return;
                    //        DataTable dt1 = (DataTable)Session["tblPS"];
                    //        //((Label)this.dgvPS99.FooterRow.FindControl("lblfopamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(opnam)", "")) ?
                    //        // 0.00 : dt.Compute("Sum(opnam)", ""))).ToString("#,##0.00;(#,##0.00); - ");
                    //        //((Label)this.dgvPS99.FooterRow.FindControl("lblfcuamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trnam)", "")) ?
                    //        //0.00 : dt.Compute("Sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00); - ");
                    //        //((Label)this.dgvPS99.FooterRow.FindControl("lblfclamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(closam)", "")) ?
                    //        //0.00 : dt.Compute("Sum(closam)", ""))).ToString("#,##0.00;(#,##0.00); - ");

                    //        ((Label)this.dgvPS99.FooterRow.FindControl("lblftopamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(opnam)", "")) ?
                    //        0.00 : dt1.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
                    //        ((Label)this.dgvPS99.FooterRow.FindControl("lblftcuamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(trnam)", "")) ?
                    //        0.00 : dt1.Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");
                    //        ((Label)this.dgvPS99.FooterRow.FindControl("lblftclamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(closam)", "")) ?
                    //         0.00 : dt1.Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");




                    //     break;




            }


        }
        protected void RptProjectReoprt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["BvsE"];

            DataSet ds2 = GetDataForProjectReport();
            if (ds2 == null)
                return;

            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccProjectReport1();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Individual Project Cost-Consolidated Report";

            //string dramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closdramt)", "")) ? 0.00 : ds2.Tables[0].Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); - ");
            //string cramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closcramt)", "")) ? 0.00 : ds2.Tables[0].Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); - ");

            //TextObject txtNetAmt = rptstk.ReportDefinition.ReportObjects["txtNetAmt"] as TextObject;
            //txtNetAmt.Text = ((Convert.ToDouble(dramt) - Convert.ToDouble(cramt));// == "0") ? "" : (Convert.ToDouble(dramt) - Convert.ToDouble(cramt)).ToString();

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = this.DDListLevels.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(ds2.Tables[0]);

            string comcod = hst["comcod"].ToString();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void GetProjectStatusForPrint()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["BvsE"];

            DataSet ds2 = GetDataForProjectReport();
            if (ds2 == null)
                return;

            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccProjectReport();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = ((Label)this.Master.FindControl("lblTitle")).Text;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = this.DDListLevels.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rptstk.SetDataSource(ds2.Tables[0]);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ImgbtnFindProj_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string HeaderCode = (Request.QueryString["RepType"].ToString() == "PS") ? "4%" : "1102%";
            string filter = this.txtSearch.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlAccProject.DataSource = dt1;
            this.ddlAccProject.DataTextField = "actdesc1";
            this.ddlAccProject.DataValueField = "actcode";
            this.ddlAccProject.DataBind();
        }
        protected void ImgbtnFindProjI_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string filter1 = "41%";
            string filter2 = this.txtSearchp.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", filter1, filter2, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlHAccProject.DataSource = dt1;
            this.ddlHAccProject.DataTextField = "actdesc1";
            this.ddlHAccProject.DataValueField = "actcode";
            this.ddlHAccProject.DataBind();
        }

        protected void ImgbtnFindRes_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter1 = this.txtSrcRes.Text.Trim() + "%";
            string actcode = this.ddlAccProject.SelectedValue.ToString();

            DataSet ds3 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONRESHEAD01", filter1, actcode, "", "", "", "", "", "", "");
            DataTable dt3 = ds3.Tables[0];
            this.ddlResHead.DataSource = dt3;
            this.ddlResHead.DataTextField = "resdesc1";
            this.ddlResHead.DataValueField = "rescode";
            this.ddlResHead.DataBind();
        }

        private DataSet GetDataForProjectReport()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccProject.SelectedValue.ToString();
            string rescode = this.ddlResHead.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "PROJECT_REPORT_LEVEL", date1, date2, TopHead, actcode, rescode, mRptGroup, "", "", "");

            return ds2;
        }


        protected void DDListLevels_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ChkTopHead_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ImgbtnFindProjSpc_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string filter = this.txtSearchpSpc.Text.Trim() + "%";
            string filter = this.txtSearchpSpc.Text.Trim() + "%";


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", "4[17]%", filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProject.DataSource = dt1;
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataBind();
        }
        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = this.txtSearchpIndp.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", "4[1-2]%", filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();
        }
        protected void gvIncome_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label Amount = (Label)e.Row.FindControl("lgvAmt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }

            }
        }

        protected void dgvPS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void dgvSpBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label Description = (Label)e.Row.FindControl("lblDescription");
                Label lblClosamt = (Label)e.Row.FindControl("lblgvclosamt");
                Label lblOpenamt = (Label)e.Row.FindControl("lblgvopnamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gencode")).ToString();

                if (code == "")
                {
                    return;
                }
                if ((ASTUtility.Right(code, 3) == "000") || (ASTUtility.Right(code, 3) == "AAA"))
                {

                    Description.Font.Bold = true;
                    lblClosamt.Font.Bold = true;
                    lblOpenamt.Font.Bold = true;
                }

            }
        }
        protected void dgvBE_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Label description = (Label)e.Row.FindControl("lblgvdescryptionbe");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode1")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (ASTUtility.Right(code, 10) == "0000000000")
            {
                description.Font.Bold = true;

            }
        }
        protected void imgBtnAccHead_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string filter1 = "%";
            string filter2 = this.txtAccHead.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", filter1, filter2, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlAcHead.DataSource = dt1;
            this.ddlAcHead.DataTextField = "actdesc1";
            this.ddlAcHead.DataValueField = "actcode";
            this.ddlAcHead.DataBind();
        }
        protected void gvlandSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Label description = (Label)e.Row.FindControl("lblgvdescryptionbe");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode1")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (ASTUtility.Right(code, 10) == "0000000000")
            {
                description.Font.Bold = true;

            }
        }
    }
}
