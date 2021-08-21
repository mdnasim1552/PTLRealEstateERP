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
namespace RealERPWEB.F_45_GrAcc
{

    public partial class LinkFinncialReports : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {


                string mRepID = Request.QueryString["Type"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = (mRepID == "IS" ? "Income Statement" :
                     (mRepID == "BS" ? "Balance Sheet" : (mRepID == "BE" ? "Budget Vs Expensis" : (mRepID == "PS" ? "Individual Project Cost-Consolidated Report" : (mRepID == "SPC" ? "Specificition Wise Materials" : (mRepID == "SS" ? "Project Status Summary" : (mRepID == "SPBS" ? "Special Balance Sheet" : (mRepID == "IACUR" ? "Income Statement (Acural Basis)" : "Income Statement (Individual Project)"))))))))
                                      + " View/Print Screen";
                this.MultiView1.ActiveViewIndex = ((mRepID == "IS" || mRepID == "IS2") ? 0 : (mRepID == "BS" ? 1 : ((mRepID == "PS" || mRepID == "SS") ? 2 : (mRepID == "BE" ? 3 : (mRepID == "SPC" ? 4 : ((mRepID == "IPRJ" || mRepID == "IACUR") ? 5 : 6))))));
                double day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                this.lblfrmdate.Text = this.Request.QueryString["Date1"].ToString();
                this.lbltodate.Text = this.Request.QueryString["Date2"].ToString();


                if (mRepID == "IS" || mRepID == "IS2")
                {
                    this.lblOpeningDate.Visible = true;
                    this.txtOpeningDate.Visible = true;
                    this.txtOpeningDate.Text = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.DDListLevels.SelectedIndex = 1;
                }

                if (mRepID == "PS" || mRepID == "SS")
                {
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.ImgbtnFindProj_Click(null, null);

                }
                if (mRepID == "BE")
                {
                    this.lblDatefrom.Text = "Date:";
                    this.lbldateto.Visible = false;
                    this.lbltodate.Visible = false;
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.ImgbtnFindProjI_Click(null, null);


                }

                if (mRepID == "SPBS")
                {
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;

                }



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetCompcode()
        {
            return (this.Request.QueryString["comcod"].ToString());
        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }


        private string Company()
        {

            string comcod = this.GetCompcode();
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
        protected void GetIncomeStatement()
        {
            ViewState.Remove("tblAcc");

            string mCOMCOD = this.GetCompcode();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string Opndate = this.txtOpeningDate.Text.Substring(0, 11);
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, "", "", "", "", "");


            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy");




            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();

            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "~/F_45_GrAcc/LinkGrpAccount.aspx?Type=INDetails&comcod=" + mCOMCOD + "&Date1=" + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy"); ;


            ds1.Dispose();
        }

        protected void GetIncomeStatementForPrint()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date1"].ToString();
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            //string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            //DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
            //        ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");

            //if (ds1 == null)
            //    return;

            //mTRNDAT1 = this.txtDatefrom.Text;
            //mTRNDAT2 = this.txtDateto.Text;
            //mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();

            ReportDocument RptIncomeSt = new RealERPRPT.R_17_Acc.RptIncomeSt2();
            RptIncomeSt.SetDataSource((DataTable)ViewState["tblAcc"]);

            TextObject TxtCompName = RptIncomeSt.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            TxtCompName.Text = comnam;

            TextObject TxtRptTitle = RptIncomeSt.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            TxtRptTitle.Text = "INCOME  STATEMENT - " + mLEVEL1;

            TextObject TxtRptPeriod = RptIncomeSt.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            TxtRptPeriod.Text = "(From " + mTRNDAT1 + " to " + mTRNDAT2 + ")";

            TextObject TxtOpening = RptIncomeSt.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            TxtOpening.Text = Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy") + " Taka";

            TextObject TxtClosing = RptIncomeSt.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            TxtClosing.Text = Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy") + " Taka";
            TextObject txtuserinfo = RptIncomeSt.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //--------------------Export to PDF--------------------------------------------------
            Session["Report1"] = RptIncomeSt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void GetBalanceSheetForPrint()
        {




            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString(); ;
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            DataTable dt = (DataTable)ViewState["tblAcc"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>();


            switch (comcod)
            {

                case "3333":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetAlli", list, null, null);
                    break;



                case "3340":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetUddl", list, null, null);
                    break;

                case "3101":
                case "3348":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetCredence", list, null, null);
                    break;

                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetAlli", list, null, null);
                    break;
            }


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
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            ////DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "BSR_COMPANY_0" +
            ////        ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");

            ////if (ds1 == null)
            ////    return;


            //ReportDocument RptBalanceSheet = new RealERPRPT.R_17_Acc.RptBalanceSheet2();
            //RptBalanceSheet.SetDataSource((DataTable)ViewState["tblAcc"]);

            //TextObject TxtCompName = RptBalanceSheet.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            //TxtCompName.Text =comnam;

            //TextObject TxtRptTitle = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptTitle"] as TextObject;
            //TxtRptTitle.Text = "BALANCE  SHEET - " + mLEVEL1;

            //TextObject TxtRptPeriod = RptBalanceSheet.ReportDefinition.ReportObjects["TxtRptPeriod"] as TextObject;
            //TxtRptPeriod.Text = "As on " + mTRNDAT2 + "";

            //TextObject TxtOpening = RptBalanceSheet.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            //TxtOpening.Text = Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy")+" Taka";

            //TextObject TxtClosing = RptBalanceSheet.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            //TxtClosing.Text =  Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy")+" Taka";
            //TextObject txtuserinfo = RptBalanceSheet.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////--------------------Export to PDF--------------------------------------------------
            //Session["Report1"] = RptBalanceSheet;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        protected void GetBalanceSheet()
        {
            ViewState.Remove("tblAcc");

            string mCOMCOD = this.GetCompcode();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string CallType = this.Company();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", CallType +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");


            this.dgvBS.Columns[2].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.Columns[3].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.DataSource = ds1.Tables[0];
            this.dgvBS.DataBind();
            ViewState["tblAcc"] = ds1.Tables[0];

            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvBS.HeaderRow.FindControl("hlbtnDetailsbs")).NavigateUrl = "~/F_45_GrAcc/LinkGrpAccount.aspx?Type=Details&comcod=" + mCOMCOD + "&Date1=" + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");




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
                case "IS2":
                    this.GetIncomeStatementForPrint();
                    break;
                case "BS":
                    this.GetBalanceSheetForPrint();
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

            string comcod = this.GetCompcode(); ;
            string mRepID = Request.QueryString["Type"].ToString();
            switch (mRepID)
            {
                case "IS":
                case "IS2":
                    this.GetIncomeStatement();
                    break;
                case "BS":
                    this.GetBalanceSheet();
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
            //Session.Remove("tblspc");
            //string date1 = this.txtDatefrom.Text.Substring(0, 11); 
            //string actcode = this.ddlProject.SelectedValue.ToString();        
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();

            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "RPTRESSPCPRJWISE",  actcode, date1, "", "", "", "", "", "","");
            //if (ds2 == null)
            //    return;
            //if (ds2.Tables[0].Rows.Count == 0)
            //{
            //    return;
            //}
            //Session["tblspc"] = ds2.Tables[0];
            //this.dgvSPC.DataSource = ds2.Tables[0];
            //this.dgvSPC.DataBind();

            //((Label)this.dgvSPC.FooterRow.FindControl("lblgvfamount")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(trnam)", "")) ?
            //            0.00 : ds2.Tables[0].Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");


        }

        private void GetIncomeIPrj()
        {
            //Session.Remove("tbliprj");
            //string date1 = this.txtDatefrom.Text.Substring(0, 11);
            //string actcode = this.ddlProjectInd.SelectedValue.ToString();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string Type = this.Request.QueryString["RepType"].ToString();

            //string Calltype = ((Type == "IPRJ") ? ((this.rbtnList1.SelectedIndex == 0) ? "RPTINCOMESTATMENTINPRJ" : "RPTINCOMESTATMENTINPRJSUM") : ((this.rbtnList1.SelectedIndex == 0) ? "RPTINCOMESTATMENTACURAL" : "RPTINCOMESTATMENTACURALSUM"));

            //DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", Calltype,"", date1,actcode , "", "","","","","");
            //if (ds2 == null)
            //    return;
            //if (ds2.Tables[0].Rows.Count == 0)
            //{
            //    this.gvIncome.DataSource = null;
            //    this.gvIncome.DataBind();
            //    this.gvInfast.DataSource=null;
            //    this.gvInfast.DataBind();

            //    return;
            //}

            //DataTable dt =HiddenSameData(ds2.Tables[0]);
            // Session["tbliprj"]=dt;
            //DataView dv = dt.DefaultView;
            //dv.RowFilter=("rescode  not like '31%'");

            //this.gvIncome.DataSource = dv.ToTable();
            //this.gvIncome.DataBind();
            //dv = dt.DefaultView;
            //dv.RowFilter = ("rescode like '31%'");
            //this.gvInfast.DataSource = dv.ToTable();
            //this.gvInfast.DataBind();
            //if (dv.ToTable().Rows.Count == 0)
            //    return;
            //((Label)this.gvInfast.FooterRow.FindControl("lgvFTfAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dv.ToTable().Compute("Sum(trnam)", "")) ?
            //            0.00 : dv.ToTable().Compute("Sum(trnam)", ""))).ToString("#,##0;(#,##0); - ");

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string RptType = Request.QueryString["Type"].ToString();
            int j;
            string grpcode;
            switch (RptType)
            {

                case "IS":
                case "IS2":
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
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["BvsE"];

            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccFinalReports();

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
            //txtlevel.Text = "Level : " + this.ddlRptGroupbve.SelectedValue.ToString().Trim() + "";

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlHAccProject.SelectedItem.ToString().Trim().Substring(13, this.ddlHAccProject.SelectedItem.ToString().Trim().Length - 13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintSpcDesc()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tblspc"];
            //if (dt1.Rows.Count == 0)
            //    return;

            // ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptResSpecificition() ;

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "Date: "+Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");

            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProject.SelectedItem.Text.Substring(13); 
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintIncomeIPrj()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tbliprj"];
            //if (dt1.Rows.Count == 0)
            //    return;
            // ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptIncomestIndPrj() ;

            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["RepType"] == "IPRJ") ? "Income Statement (Individual Project)" : "Income Statement (Acural Basis)";
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProjectInd.SelectedItem.Text.Substring(13);
            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);

            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }
        protected void dgvIS_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string comcod = this.GetCompcode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            string mCOMCOD = comcod;
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (code == "3AAAAAAAAAAA")
            {
                hlink1.Style.Add("color", "blue");
                hlink1.NavigateUrl = "~/F_45_GrAcc/LinkGrpAccount.aspx?Type=SalDetails&comcod=" + comcod + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }


        }
        protected void ddlAccProject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void dgvBS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");
            Label lblcode = (Label)e.Row.FindControl("lblgvcode");
            Label clobal = (Label)e.Row.FindControl("lblgvclobal");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            if (code == "")
            {
                return;
            }
            if (code == "1DAAAAAAAAAA" || code == "2IAAAAAAAAAA")
            {
                hlink1.Style.Add("color", "blue");
                lblcode.Style.Add("color", "blue");
                clobal.Style.Add("color", "blue");
                opnamt.Style.Add("color", "blue");
                cuamt.Style.Add("color", "blue");


            }
        }

        protected void GetBudgetVsExpenses()
        {


            string comcod = this.GetCompcode();
            Session.Remove("BvsE");
            string date1 = this.Request.QueryString["Date2"].ToString();
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



            ((Label)this.dgvBE.FooterRow.FindControl("lblftoamt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closam)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(closam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgvBE.FooterRow.FindControl("lblfbgdam")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(bgdam)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(bgdam)", ""))).ToString("#,##0;(#,##0); - ");
            ((Label)this.dgvBE.FooterRow.FindControl("lblftAvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(tavamt)", "")) ?
                    0.00 : ds2.Tables[0].Compute("Sum(tavamt)", ""))).ToString("#,##0;(#,##0); - ");



        }
        protected void GetProjectStatus()
        {
            //Session.Remove("tblPS");
            //DataSet ds2 = this.GetDataForProjectReport();
            //if (ds2 == null)
            //    return;
            //if (ds2.Tables[0].Rows.Count == 0)
            //{
            //    //this.lblmsg.Text = "There is no resource in this accounts.";
            //    //this.lblmsg.ForeColor = System.Drawing.Color.Blue;
            //   return;
            //}
            //Session["tblPS"] = ds2.Tables[0];
            //DataTable dtr = (DataTable)Session["tblPS"];
            ////DataView dvr = new DataView();
            ////dvr = dtr.DefaultView;
            ////dvr.RowFilter = ("grp1 = 'g01'");
            //this.dgvPS.DataSource = dtr;
            //this.dgvPS.DataBind();
            //this.FooterCalculation(dtr, "dgvPS");








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
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////DataTable dt1 = (DataTable)Session["BvsE"];

            //DataSet ds2 = GetDataForProjectReport();
            //if (ds2 == null)
            //    return;

            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccProjectReport1();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = "Individual Project Cost-Consolidated Report";

            ////string dramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closdramt)", "")) ? 0.00 : ds2.Tables[0].Compute("Sum(closdramt)", ""))).ToString("#,##0;(#,##0); - ");
            ////string cramt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(closcramt)", "")) ? 0.00 : ds2.Tables[0].Compute("Sum(closcramt)", ""))).ToString("#,##0;(#,##0); - ");

            ////TextObject txtNetAmt = rptstk.ReportDefinition.ReportObjects["txtNetAmt"] as TextObject;
            ////txtNetAmt.Text = ((Convert.ToDouble(dramt) - Convert.ToDouble(cramt));// == "0") ? "" : (Convert.ToDouble(dramt) - Convert.ToDouble(cramt)).ToString();

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = this.DDListLevels.SelectedValue.ToString().Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptstk.SetDataSource(ds2.Tables[0]);

            //string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void GetProjectStatusForPrint()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ////DataTable dt1 = (DataTable)Session["BvsE"];

            //DataSet ds2 = GetDataForProjectReport();
            //if (ds2 == null)
            //    return;

            // ReportDocument rptstk = new  RealERPRPT.R_17_Acc.RptAccProjectReport();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = this.LblTitle.Text;

            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim()+" )";
            //TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            //txtlevel.Text = this.DDListLevels.SelectedValue.ToString().Trim();
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            //rptstk.SetDataSource(ds2.Tables[0]);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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

            string comcod = this.GetCompcode();

            string filter1 = "%";
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
    }
}
