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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{

    public partial class AccFinalReports : System.Web.UI.Page
    {
        public static double CAmt, EAmt, BalAmt;
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Final Accounts Reports View/Print Screen
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                //    Response.Redirect("~/AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                string comcod = this.GetCompCode();
                string mRepID = "IS";
                if (Request.QueryString["RepType"] != null)
                    mRepID = Request.QueryString["RepType"].ToString();

                string title = (mRepID == "IS" ? "Income Statement" :
                    (mRepID == "BS" ? "Balance Sheet" : (mRepID == "BE" ? "Budget Vs Expenses(Actual)" :
                    (mRepID == "PS" ? "Project Report" : (mRepID == "SPC" ? "Project Report-Specifition"
                    : (mRepID == "SS" ? "Project Status Summary" : (mRepID == "SPBS" ? "Special Balance Sheet"
                    : (mRepID == "SHEQUITY" ? "Statement Of Share Holder's Equity"
                    : (mRepID == "BSNote" ? "Notes:Balance Sheet"
                    : (mRepID == "PrjIS" ? "Statement Of Comprehensive Income (Project)"

                    : (mRepID == "IACUR" ? "Income Statement (Acural Basis)" : "Income Statement (Individual Project)")))))))))));
                //((Label)this.Master.FindControl("lblTitle")).Text = title;

                //this.Master.Page.Title = title;
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                this.MultiView1.ActiveViewIndex = ((mRepID == "IS" || mRepID == "IS2") ? 0 : (mRepID == "BS" ? 1 : ((mRepID == "PS" || mRepID == "SS") ? 2
                    : (mRepID == "BE" ? 3 : (mRepID == "SPC" ? 4 : ((mRepID == "IPRJ" || mRepID == "IACUR") ? 5 : (mRepID == "SPBS" ? 6 : (mRepID == "PrjIS" ? 10
                    : (mRepID == "LandSt" ? 8 : (mRepID == "BSNote" ? 9 : 7))))))))));

                DateTime opndate = this.GetOpeningDate();
                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DateTime yfdate = Convert.ToDateTime("01-jul-" + Convert.ToDateTime(curdate).AddYears(-1).ToString("yyyy"));
                this.txtDatefrom.Text = (yfdate < opndate) ? opndate.AddDays(1).ToString("dd-MMM-yyyy") : yfdate.ToString("dd-MMM-yyyy");
                this.txtDateto.Text = (yfdate < opndate) ? opndate.AddDays(1).ToString("dd-MMM-yyyy") : yfdate.AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy"); //System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatefrom_CalendarExtender.StartDate = Convert.ToDateTime(opndate.AddDays(1));

                if (mRepID == "IS" || mRepID == "IS2")
                {
                    this.lblOpeningDate.Visible = true;
                    this.txtOpeningDate.Visible = true;
                    // Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtOpeningDate.Text = Convert.ToDateTime(hst["opndate"]).ToString("dd-MMM-yyyy");
                    this.DDListLevels.SelectedIndex = 2;
                    this.DDListLevels.Items.RemoveAt(0);
                    this.DDListLevels.Items.RemoveAt(0);


                }
                else if (mRepID == "BS")
                {
                    if (comcod == "3333")
                    {
                        DateTime currdate = Convert.ToDateTime(System.DateTime.Today.ToString("dd-MMM-yyyy"));
                        this.txtDatefrom.Text = Convert.ToDateTime("01" + currdate.ToString("dd-MMM-yyyy").Substring(3)).ToString("dd-MMM-yyyy");
                        this.txtDateto.Text = Convert.ToDateTime(curdate).ToString("dd-MMM-yyyy");

                        // this.txtDateto.Text = Convert.ToDateTime (this.txtDatefrom.Text).AddMonths(1).AddDays(-1).ToString ("dd-MMM-yyyy");


                    }
                    this.DDListLevels.Items.RemoveAt(0);
                    this.DDListLevels.SelectedIndex = 0;

                }
                else if (mRepID == "PS" || mRepID == "SS")
                {
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.ImgbtnFindProj_Click(null, null);

                }
                else if (mRepID == "BE")
                {
                    this.txtDatefrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lblDatefrom.InnerText = "Date:";
                    this.lbldateto.Visible = false;
                    this.txtDateto.Visible = false;
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.ImgbtnFindProjI_Click(null, null);


                }
                else if (mRepID == "LandSt")
                {
                    this.lblDatefrom.InnerText = "Date:";
                    this.lbldateto.Visible = false;
                    this.txtDateto.Visible = false;
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.imgBtnAccHead_Click(null, null);
                }

                else if (mRepID == "SPBS")
                {
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;

                }

                else if (mRepID == "SPC")
                {
                    this.lblDatefrom.InnerText = "Date:";
                    this.lbldateto.Visible = false;
                    this.txtDateto.Visible = false;
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.ChkTopHead.Visible = false;
                    this.ImgbtnFindProjSpc_Click(null, null);


                }
                else if (mRepID == "IPRJ" || mRepID == "IACUR")
                {
                    this.rbtnList1.SelectedIndex = 0;
                    this.txtDatefrom.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.lblDatefrom.InnerText = "Date:";
                    this.lbldateto.Visible = false;
                    this.txtDateto.Visible = false;
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.Visible = false;
                    this.ChkTopHead.Visible = false;
                    this.ImgbtnFindProjind_Click(null, null);


                }
                else if (mRepID == "SHEQUITY")
                {

                    this.DDListLevels.SelectedValue = "2";

                }
                else if (mRepID == "PrjIS")
                {
                    this.lblOpeningDate.Visible = true;
                    this.txtOpeningDate.Visible = true;
                    // Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtOpeningDate.Text = Convert.ToDateTime(hst["opndate"]).ToString("dd-MMM-yyyy");
                    this.DDListLevels.Visible = false;
                    this.lblrptlbl.Visible = false;
                    this.DDListLevels.SelectedIndex = 3;
                    this.DDListLevels.Items.RemoveAt(0);
                    this.DDListLevels.Items.RemoveAt(0);
                    GetProjectList();
                }
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
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private string Company()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
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
                            //  case "3101"://Asit
                    headallocation = "Headallocation";
                    break;


                default:

                    break;


            }

            return headallocation;


        }
        protected void GetIncomeStatement()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetCompCode();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string Opndate = this.txtOpeningDate.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string headallocation = this.GetCompanyHeadAllocation();

            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "ISR_COMPANY_0" +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, headallocation, "", "", "", "");

            this.dgvIS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.dgvIS.Columns[3].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.dgvIS.DataSource = ds1.Tables[0];
            this.dgvIS.DataBind();
            ds1.Dispose();

            Session["Report1"] = dgvIS;
            ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtntbCdataExel1")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=INDetails&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy"); ;


        }
        private string compIncomest()
        {

            string compIncomest;
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                //case "3333":
                ////case "3101":
                //    compIncomest = "IncomestAlli";
                //    break;

                default:
                    compIncomest = "IncomeGen";
                    break;
            }

            return compIncomest;
        }
        private void GetIncomeStatementRDLCForPrint()
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

        }


        private void GetProjectList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string HeaderCode = (Request.QueryString["RepType"].ToString() == "PS") ? "4%" : "1102%";
            string filter = this.txtSearch.Text.Trim() + "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PROJECT", "GETCONACCHEAD02", HeaderCode, filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlprjlist.DataSource = dt1;
            this.ddlprjlist.DataTextField = "acttdesc";
            this.ddlprjlist.DataValueField = "actcode";
            this.ddlprjlist.DataBind();
        }

        protected void GetIncomeStatementForPrint()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string txtCuramt = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "\n" + " To " + System.Environment.NewLine + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            string txtPreamt = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "\n" + " To " + System.Environment.NewLine + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();


            DataTable dt = (DataTable)ViewState["tblAcc"];

            string lvel = this.DDListLevels.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            // string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3333"://Alliance
                    dv.RowFilter = ("actcode4 not like'%00'");
                    dt = dv.ToTable();

                    break;

                default:

                    break;


            }


            string compIncomest = this.compIncomest();
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.NoteIncoStatement>();

            string mRepID = "";
            string rpType = "";
            if (Request.QueryString["RepType"] != null)
                mRepID = Request.QueryString["RepType"].ToString();
            if (mRepID == "PrjIS")
            {
                rpType = "Project Name: " + this.ddlprjlist.SelectedItem.Text;
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIncomeStSinglePrj", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rpType", rpType));
            }

            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIncomeSt", list, null, null);
            }

            //else
            //{
            //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIncomeSt", list, null, null);

            //}

            Rpt1.EnableExternalImages = true;
            Hashtable reportParm = new Hashtable();
            Rpt1.SetParameters(new ReportParameter("TxtCompName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtCuramt", txtCuramt));
            Rpt1.SetParameters(new ReportParameter("txtPreamt", txtPreamt));
            Rpt1.SetParameters(new ReportParameter("TxtRptPeriod", "For the year ended " + Convert.ToDateTime(this.txtDateto.Text.Substring(0, 11)).ToString("dd MMMM yyyy")));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtdate", System.DateTime.Today.ToString("MMMM dd, yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void GetBalanceSheetForPrint()
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


            switch (comcod)
            {

                case "3333":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetAlli", list, null, null);
                    break;



                case "3340":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetUddl", list, null, null);
                    break;


                case "3348":
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetCredence", list, null, null);
                    break;

                default:
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptBalanceSheetAlli", list, null, null);
                    break;
            }

            // Rpt1.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
            Rpt1.EnableExternalImages = true;
            Hashtable reportParm = new Hashtable();
            Rpt1.SetParameters(new ReportParameter("TxtCompName", comnam));
            Rpt1.SetParameters(new ReportParameter("TxtOpening", Convert.ToDateTime(mTRNDAT1).AddDays(-1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtClosing", Convert.ToDateTime(mTRNDAT2).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("TxtRptPeriod", "As at " + Convert.ToDateTime(mTRNDAT2).ToString("dd MMMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.Refresh();


            //Rpt1.SubreportProcessing.Subreports["RptOrderPaymentAssure.rpt"].SetDataSource(_ReportDataSet.Tables[6]);
            //Rpt1.SetParameters(new ReportParameter("txtdate", System.DateTime.Today.ToString("MMMM dd, yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtuserinfo", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";







        }


        private void localReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {


            DataTable dt = (DataTable)ViewState["tblAcc"];
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.Rptspbalancesheet>();
            ////  List<RealEntity.C_17_Acc.EClassFinanStatement.CashFlowIndirect>)RptDataSet




            e.DataSources.Add(new ReportDataSource("DataSet1", list));




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
            string mCOMCOD = GetCompCode();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string CallType = this.Company();
            string headallocation = this.GetCompanyHeadAllocation();

            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", CallType +
                    ASTUtility.Right(mLEVEL1, 1), mTRNDAT1, mTRNDAT2, mTOPHEAD1, headallocation, "", "", "", "", "");


            this.dgvBS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.Columns[3].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.dgvBS.DataSource = ds1.Tables[0];
            this.dgvBS.DataBind();
            ViewState["tblAcc"] = ds1.Tables[0];

            Session["Report1"] = dgvBS;
            ((HyperLink)this.dgvBS.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.dgvBS.HeaderRow.FindControl("hlbtnDetailsbs")).NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=Details&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

        }

        private void GetNoteBalanceSheet()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetCompCode();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string CallType = this.Company();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "BSRNOTES_COMPANY_04", mTRNDAT1, mTRNDAT2, mTOPHEAD1, "", "", "", "", "", "");



            this.gvbsnotes.Columns[3].HeaderText = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
            this.gvbsnotes.Columns[4].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);

            this.gvbsnotes.DataSource = (DataTable)ViewState["tblAcc"];
            this.gvbsnotes.DataBind();

            Session["Report1"] = gvbsnotes;
            ((HyperLink)this.gvbsnotes.HeaderRow.FindControl("hlbtntbCdataExelbsn")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";




        }
        private void GetSpecialBalanceSheet()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetCompCode();
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
            string comcod = GetCompCode();
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
                    if (this.ddlRptGroupbve.SelectedIndex !=0)
                    {
                        this.ReportBudgetvsExpensesDetails();
                    }
                    else
                    {
                        this.ReportBudgetvsExpenses();
                    }

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

                case "PrjIS":
                    this.GetIncomeStatementForPrint();
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
            string comcod = GetCompCode();
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


                case "BSNote":
                    this.GetNoteBalanceSheet();
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
                case "PrjIS":
                    this.GetProjectWiseIS();
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
            string comcod = GetCompCode();

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
            string comcod = GetCompCode();
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
            string comcod = GetCompCode();
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

            string grpsum;


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
                case "PrjIS":
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


                case "BSNote":

                    string actcode4 = dt1.Rows[0]["actcode4"].ToString();
                    //  string rescode = dt1.Rows[0]["rescode"].ToString();
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                            dt1.Rows[j]["actdesc4"] = "";
                        actcode4 = dt1.Rows[j]["actcode4"].ToString();


                    }
                    break;


                case "IPRJ":
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    //grpsum = Convert.ToDouble(dt1.Rows[0]["grpsum"]).ToString("#,##0.00;(#,##0.00);");
                    for (j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpcode)
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        //if (dt1.Rows[j]["grpsum"].ToString() == grpsum)
                        //{
                        //    grpsum = dt1.Rows[j]["grpsum"].ToString();
                        //    dt1.Rows[j]["grpsum"] = 0.00;

                        //}


                        else
                        {
                            grpcode = dt1.Rows[j]["grp"].ToString();
                            //grpsum = dt1.Rows[j]["grpsum"].ToString();
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

        protected void ReportBudgetvsExpensesDetails()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["BvsE"];
            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.bugdvExpensis>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccFinalReportsDetails", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Level", "Level : " + this.ddlRptGroupbve.SelectedValue.ToString().Trim() + ""));
            Rpt1.SetParameters(new ReportParameter("date", "As on " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + ""));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlHAccProject.SelectedItem.ToString().Trim().Substring(13, this.ddlHAccProject.SelectedItem.ToString().Trim().Length - 13)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budget Vs Expenses Report"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void ReportBudgetvsExpenses()
        {
            // ** ***Iqbal Nayan    
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["BvsE"];
            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.bugdvExpensis>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccFinalReports", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Level", "Level : " + this.ddlRptGroupbve.SelectedValue.ToString().Trim() + ""));
            Rpt1.SetParameters(new ReportParameter("date", "As on " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + ""));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.ddlHAccProject.SelectedItem.ToString().Trim().Substring(13, this.ddlHAccProject.SelectedItem.ToString().Trim().Length - 13)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Budget Vs Expenses Report"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


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
            string txtprojectname = "Project Name: " + this.ddlProject.SelectedItem.Text.Substring(13);
            string txtFDate1 = "Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            DataTable dt1 = (DataTable)Session["tblspc"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptProjectSpecification>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptProjectSpecification", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", "Resource Specification"));
            Rpt1.SetParameters(new ReportParameter("txtprojectname", txtprojectname));


            Rpt1.SetParameters(new ReportParameter("date", txtFDate1));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




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
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }

        private void PrintIncomeIPrj()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comnam"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tbliprj"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.InStIP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIncomestIndPrj", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Projectnam", "Project Name: " + this.ddlProjectInd.SelectedItem.Text.Substring(13))); // commant by iqbal Nayan
            Rpt1.SetParameters(new ReportParameter("txtdate", "Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", (this.Request.QueryString["RepType"] == "IPRJ") ? "Income Statement (Individual Project)" : "Income Statement (Acural Basis)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)Session["tbliprj"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptIncomestIndPrj();

            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["RepType"] == "IPRJ") ? "Income Statement (Individual Project)" : "Income Statement (Acural Basis)";
            //TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            //txtprojectname.Text = "Project Name: " + this.ddlProjectInd.SelectedItem.Text.Substring(13);
            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);

            //string comcod = GetCompCode();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



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
            string comcod = GetCompCode();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Label lblgvopnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobal");
            string mCOMCOD = comcod;
            string mTRNDAT1 = this.txtDatefrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;
            string opndate = this.txtOpeningDate.Text.Trim();

            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();





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
                hlink1.NavigateUrl = "LinkAccount.aspx?Type=IncomeDet02&acgcode=" + code + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&mdesc=" + mACTDESC + "&opndate=" + opndate;




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetCompCode();
            //if (e.Row.RowType != DataControlRowType.DataRow)
            //    return;

            //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            //Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");
            //Label lblgvopnamt = (Label)e.Row.FindControl("lblgvopnamt");
            //Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobal");


            //string mCOMCOD = comcod;
            //string mTRNDAT1 = this.txtDatefrom.Text;
            //string mTRNDAT2 = this.txtDateto.Text;

            //string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            //if (code == "")
            //{
            //    return;
            //}
            //if (code == "3AAAAAAAAAAA")
            //{
            //    hlink1.Style.Add("color", "blue");
            //    hlink1.NavigateUrl = "LinkAccount.aspx?Type=SalDetails&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            //}




            //if (code == "030102AA" || code == "030201AA" || code == "030201CA" || code == "030202AA" || code == "030301AA" || code == "030302AA" || code == "030501AA" || code == "030502AA" || code == "03BAAAAA" || code == "03CAAAAA")
            //{

            //    hlink1.Attributes["style"] = "color:blue; font-weight:bold;";
            //    lblgvcuamt.Attributes["style"] = "color:blue; font-weight:bold;";
            //    lblgvopnamt.Attributes["style"] = "color:blue; font-weight:bold;";
            //    lblgvclobal.Attributes["style"] = "color:blue; font-weight:bold;";

            //}

            //else if (ASTUtility.Right(code, 2) == "00") 
            //{
            //    hlink1.Attributes["style"] = "color:green; font-weight:bold;";
            //    lblgvcuamt.Attributes["style"] = "color:green; font-weight:bold;";
            //    lblgvopnamt.Attributes["style"] = "color:green; font-weight:bold;";
            //    lblgvclobal.Attributes["style"] = "color:green; font-weight:bold;";

            //}

            //else
            //{

            //    hlink1.Attributes["style"] = "color:black;";
            //    lblgvcuamt.Attributes["style"] = "color:black;";
            //    lblgvopnamt.Attributes["style"] = "color:black; ";
            //    lblgvclobal.Attributes["style"] = "color:black;";
            //}


        }

        private string ComBalSheetDet2()
        {
            string comdet = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3340":
                    comdet = "comdet";
                    break;

                default:
                    break;

            }

            return comdet;


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
            string mCOMCOD = GetCompCode();


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();

            /////
            /// HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");


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
                if (code == "02010700" || code == "02010300")   //F_17_Acc/AccFinalReports.aspx?RepType=IS // code == "02010200" 
                {
                    hlink1.NavigateUrl = "LinkAccFinalReports.aspx?RepType=" + "IS" + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");

                }

                else
                {

                    //string combalsheetd
                    string mgrpcode = code.Substring(0, 6);
                    if (mgrpcode == "020302" || mgrpcode == "020305" || mgrpcode == "020306")
                    {

                        string comdet = this.ComBalSheetDet2();

                        if (comdet.Length > 0)
                            hlink1.NavigateUrl = "LinkAccount.aspx?Type=BalanceDet2&acgcode=" + code.Substring(0, 6) + "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim() + "&mdesc=" + mACTDESC;

                        else
                            hlink1.NavigateUrl = "LinkAccount.aspx?Type=BalanceDet&acgcode=" + code.Substring(0, 6) + "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim() + "&mdesc=" + mACTDESC;


                    }

                    else
                    {

                        hlink1.NavigateUrl = "LinkAccount.aspx?Type=BalanceDet&acgcode=" + code.Substring(0, 6) + "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim() + "&mdesc=" + mACTDESC;

                    }




                }



            }

            else if (level == "4")
            {
                if (code.Substring(0, 4) == "2109" || code == "02010701")
                {
                    hlink1.NavigateUrl = "LinkAccFinalReports.aspx?RepType=" + "IS" + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
                }
                else
                {

                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + code +
                            "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim();
                }
            }




        }

        private string GetComCallTypeBVsExpense()
        {
            string calltype = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3338":
                    calltype = "RPTBUDGETVSEXPENSESACME";
                    break;
                default:
                    calltype = "RPTBUDGETVSEXPENSES";
                    break;
            }
            return calltype;
        }


        protected void GetBudgetVsExpenses()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            Session.Remove("BvsE");
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            // string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf";//(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlHAccProject.SelectedValue.ToString();
            string mRptGroup = Convert.ToString(this.ddlRptGroupbve.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            string Calltype = this.GetComCallTypeBVsExpense();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BUDGETVSEX_PROJECT", Calltype,
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
            //if (mRptGroup != "12" || mRptGroup != "9" || mRptGroup != "7" || mRptGroup != "4")
            //{
            //    this.dgvBE.Columns[3].Visible = false;
            //    this.dgvBE.Columns[4].Visible = false;
            //    this.dgvBE.Columns[5].Visible = false;
            //    this.dgvBE.Columns[7].Visible = false;
            //    this.dgvBE.Columns[8].Visible = false;
            //    this.dgvBE.Columns[10].Visible = false;
            //    this.dgvBE.Columns[11].Visible = false;
            //}

            if (mRptGroup == "2" )
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
            Session["Report1"] = dgvBE;
            ((HyperLink)this.dgvBE.HeaderRow.FindControl("hlbtntbCdataExelbe")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }

        protected void GetLandStatus()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
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


                    string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
                    mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
                    switch (mRptGroup)
                    {
                        case "12":
                            DataView dv = dt.Copy().DefaultView;
                            dv.RowFilter = ("subcode1 not like '%000'");
                            dt = dv.ToTable();
                            break;

                        default:
                            break;


                    }


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
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                case "3335":
                case "3336":
                case "3337":
                    this.PrintProStatusEdison();
                    break;
                case "3101":
                    this.PrintProStatus();
                    break;
                default:
                    this.PrintProStatusGen();
                    break;
            }
        }

        private void PrintProStatus()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string prjname = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            string dateft = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            string level = this.ddlResHead.SelectedItem.Text.Trim().Substring(13); //.ToString(); //"Level-" + this.ddlResHead.SelectedValue.Trim().ToString(); //.Substring(13);
            DataTable dt = (DataTable)Session["tblPS"];



            string opnam = ((Label)this.dgvPS.FooterRow.FindControl("lblfopamt")).Text;
            string trnam = ((Label)this.dgvPS.FooterRow.FindControl("lblfcuamt")).Text;
            string closdram = ((Label)this.dgvPS.FooterRow.FindControl("lblfclDrAmt")).Text;
            string closcram = ((Label)this.dgvPS.FooterRow.FindControl("lblfclCramt")).Text;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.PrijectCostN>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccProjectReport01", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + prjname));
            Rpt1.SetParameters(new ReportParameter("txtdate", dateft));
            Rpt1.SetParameters(new ReportParameter("txtlevel", "Resource Name: " + level));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Individual Project Cost-Consolidated Report"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("txtopnam", opnam));
            Rpt1.SetParameters(new ReportParameter("txtcuram", trnam));
            Rpt1.SetParameters(new ReportParameter("txtcldram", closdram));
            Rpt1.SetParameters(new ReportParameter("txtclcram", closcram));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintProStatusEdison()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();
            string prjname = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            string dateft = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            string level = "Level-" + this.DDListLevels.SelectedValue.ToString().Trim();
            DataTable dt = (DataTable)Session["tblPS"];
            DataTable dtf = new DataTable();
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            switch (mRptGroup)
            {
                case "12":
                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("subcode1 not like '%000'");
                    dtf = dv.ToTable();
                    break;
                default:
                    dtf = dt;
                    break;
            }

            string opnam = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(opnam)", "")) ? 0.00 : dtf.Compute("Sum(opnam)", ""))).ToString("#,##0;(#,##0); - ");
            string dram = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(dramt)", "")) ? 0.00 : dtf.Compute("Sum(dramt)", ""))).ToString("#,##0;(#,##0); - ");
            string cram = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(cramt)", "")) ? 0.00 : dtf.Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); - ");
            string closam = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(closamt)", "")) ? 0.00 : dtf.Compute("Sum(closamt)", ""))).ToString("#,##0;(#,##0); - ");

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassProjectReport>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccProjectReport", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", prjname));
            Rpt1.SetParameters(new ReportParameter("txtdate", dateft));
            Rpt1.SetParameters(new ReportParameter("txtlevel", level));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("txtopnam", opnam));
            Rpt1.SetParameters(new ReportParameter("txtdram", dram));
            Rpt1.SetParameters(new ReportParameter("txtcram", cram));
            Rpt1.SetParameters(new ReportParameter("txtclosam", closam));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintProStatusGen()
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
            DataTable dt = ds2.Tables[0].Copy();
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccProjectReport1();

            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Individual Project Cost-Consolidated Report";




            DataTable dtf = new DataTable();

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            switch (mRptGroup)
            {
                case "12":
                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("subcode1 not like '%000'");
                    dtf = dv.ToTable();
                    break;

                default:
                    dtf = dt;
                    break;


            }






            double opamt = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(opnam)", "")) ? 0.00 : dtf.Compute("Sum(opnam)", "")));
            double cuamt = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(trnam)", "")) ? 0.00 : dtf.Compute("Sum(trnam)", "")));
            double dramt = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(dramt)", "")) ? 0.00 : dtf.Compute("Sum(dramt)", "")));
            //string cramt = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(cramt)", "")) ? 0.00 : dtf.Compute("Sum(cramt)", ""))).ToString("#,##0;(#,##0); ");
            double cramt = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(cramt)", "")) ? 0.00 : dtf.Compute("Sum(cramt)", "")));

            double cldramt = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(closdramt)", "")) ? 0.00 : dtf.Compute("Sum(closdramt)", "")));
            double clcramt = Convert.ToDouble((Convert.IsDBNull(dtf.Compute("Sum(closcramt)", "")) ? 0.00 : dtf.Compute("Sum(closcramt)", "")));






            TextObject txtopnamt = rptstk.ReportDefinition.ReportObjects["txtopnamt"] as TextObject;
            txtopnamt.Text = Convert.ToDouble(opamt).ToString("#,##0.00;(#,##0.00); ");

            TextObject txtdramt = rptstk.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
            txtdramt.Text = dramt.ToString("#,##0.00;(#,##0.00); ");

            TextObject txtcramt = rptstk.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
            txtcramt.Text = (cramt).ToString("#,##0.00;(#,##0.00); ");

            TextObject txtclsdramt = rptstk.ReportDefinition.ReportObjects["txtclsdramt"] as TextObject;
            txtclsdramt.Text = (cldramt).ToString("#,##0.00;(#,##0.00); ");

            TextObject txtclscramt = rptstk.ReportDefinition.ReportObjects["txtclscramt"] as TextObject;
            txtclscramt.Text = (clcramt).ToString("#,##0.00;(#,##0.00); ");

            TextObject txtNetAmt = rptstk.ReportDefinition.ReportObjects["txtclsbalamt"] as TextObject;
            txtNetAmt.Text = ((cldramt) - (clcramt)).ToString("#,##0.00;(#,##0.00); ");

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "( From " + this.txtDatefrom.Text.Trim() + " To " + this.txtDateto.Text.Trim() + " )";
            TextObject txtlevel = rptstk.ReportDefinition.ReportObjects["level"] as TextObject;
            txtlevel.Text = this.DDListLevels.SelectedValue.ToString().Trim();
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["projectname"] as TextObject;
            txtprojectname.Text = this.ddlAccProject.SelectedItem.ToString().Trim().Substring(13);
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt);

            string comcod = GetCompCode();
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
            string comcod = GetCompCode();
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
            string comcod = GetCompCode();

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
            string comcod = GetCompCode();
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
            string comcod = GetCompCode();
            string date1 = this.txtDatefrom.Text.Substring(0, 11);
            string date2 = this.txtDateto.Text.Substring(0, 11);
            string TopHead = "dfdsf"; //(this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string actcode = this.ddlAccProject.SelectedValue.ToString() == "000000000000" ? "16%" : this.ddlAccProject.SelectedValue.ToString();
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
            string comcod = GetCompCode();
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
            string comcod = GetCompCode();
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

                else if (ASTUtility.Right((code), 12) == "010000000000")
                {
                    actdesc.Font.Bold = true;
                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "left");
                }

                else if (ASTUtility.Right((code), 12) == "040000000000")
                {
                    actdesc.Font.Bold = true;
                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "left");
                }

                else if (ASTUtility.Right((code), 10) == "BBBBAAAAAA")
                {
                    actdesc.Font.Bold = true;
                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "left");
                }

                if (ASTUtility.Right((code), 4) == "AAAA"  && ASTUtility.Right((code), 10) != "BBBBAAAAAA")
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

            HyperLink description = (HyperLink)e.Row.FindControl("lnkgvdescryptionbe");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode1")).ToString().Trim();

            string projcode = ddlHAccProject.SelectedValue.ToString(); 
             string date = txtDatefrom.Text;



            if (code == "")
            {
                return;
            }
            if (ASTUtility.Right(code, 10) == "0000000000")
            {
                description.Font.Bold = true;

            }
            if (ASTUtility.Right(code, 10) != "0000000000")
            {
                description.NavigateUrl = "~/F_17_Acc/LinkRptBgdvsExpensDetails.aspx?Type=Report&prjcode=" + projcode + "&rsircode=" + code+"&Date="+ date;

            }
        }
        protected void imgBtnAccHead_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();

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

        protected void ddlAccProject_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImgbtnFindRes_Click(null, null);
        }

        protected void gvbsnotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            this.txtOpeningDate.Text = Convert.ToDateTime(this.txtDatefrom.Text).AddYears(-1).ToString("dd-MMM-yyyy");

            string opendat = this.txtOpeningDate.Text;
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDescbsn");
            Label lblcode = (Label)e.Row.FindControl("lblgvcodebsn");
            Label clobal = (Label)e.Row.FindControl("lblgvclobalbsn");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamtbsn");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamtbsn");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetCompCode();


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();

            /////
            /// HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");


            //    string level = this.DDListLevels.SelectedValue.ToString();
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

            else if (code.Length == 12 && (ASTUtility.Left(code, 2) == "18" || ASTUtility.Left(code, 2) == "28" || ASTUtility.Left(code, 4) == "2301" || ASTUtility.Left(code, 4) == "1302" || ASTUtility.Left(code, 4) == "2303"))
            {

                if (rescode == "000000000000")
                {

                    hlink1.Attributes["style"] = "color:green; font-weight:bold;";
                    lblcode.Attributes["style"] = "color:green; font-weight:bold;";
                    clobal.Attributes["style"] = "color:green; font-weight:bold;";
                    opnamt.Attributes["style"] = "color:green; font-weight:bold;";
                    cuamt.Attributes["style"] = "color:green; font-weight:bold;";

                }
                else
                {
                    hlink1.Attributes["style"] = "color:black;";
                    lblcode.Attributes["style"] = "color:black;";
                    clobal.Attributes["style"] = "color:black;";
                    opnamt.Attributes["style"] = "color:black;";
                    cuamt.Attributes["style"] = "color:black; ";

                }


            }



            //else   if (code.Substring(0, 4) == "2109" || code == "02010701")
            //    {
            //        hlink1.NavigateUrl = "LinkAccFinalReports.aspx?RepType=" + "IS" + "&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
            //            + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy");
            //    }

            //    else

            //{

            //        hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + code +
            //                "&Date1=" + this.txtDatefrom.Text.Trim() + "&Date2=" + this.txtDateto.Text.Trim();
            //    }







        }
        protected void dgvPS_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;



            Label lblgvdescryption = (Label)e.Row.FindControl("lblgvdescryption");

            Label lblgvopqty = (Label)e.Row.FindControl("lblgvopqty");
            Label opnam = (Label)e.Row.FindControl("lblgvOpnamt1");
            Label lblgvCuq = (Label)e.Row.FindControl("lblgvCuq");
            Label lblgvCuam = (Label)e.Row.FindControl("lblgvCuam");
            Label lblgvClq = (Label)e.Row.FindControl("lblgvClq");
            Label lblgvClrDrAmt = (Label)e.Row.FindControl("lblgvClrDrAmt");
            Label lblgvClCram = (Label)e.Row.FindControl("lblgvClCram");
            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "subcode1")).ToString();

            if (code == "")
            {
                return;
            }


            else if (((ASTUtility.Right((code), 3) == "000")) && mRptGroup == "12")
            {
                lblgvdescryption.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvopqty.Attributes["style"] = "color:maroon; font-weight:bold;";
                opnam.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvCuq.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvCuam.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvClq.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvClrDrAmt.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblgvClCram.Attributes["style"] = "color:maroon; font-weight:bold;";


            }
        }
        protected void ddlprjlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjectWiseIS();
        }
        public void GetProjectWiseIS()
        {

            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = GetCompCode();
            string mTRNDAT1 = this.txtDatefrom.Text.Substring(0, 11);
            string mTRNDAT2 = this.txtDateto.Text.Substring(0, 11);
            string Opndate = this.txtOpeningDate.Text.Substring(0, 11);
            string mLEVEL1 = this.DDListLevels.SelectedItem.Text.ToString();
            string mTOPHEAD1 = (this.ChkTopHead.Checked == true ? "TOPHEAD" : "NOTOPHEAD");
            string headallocation = this.GetCompanyHeadAllocation();
            string prjname = this.ddlprjlist.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(mCOMCOD, "SP_REPORT_ACCOUNTS_IS_BS_R2", "PRJISR_COMPANY_04", mTRNDAT1, mTRNDAT2, mTOPHEAD1, Opndate, headallocation, prjname, "", "", "");
            if (ds1 == null)
                return;
            this.gvPrjWiseIS.Columns[2].HeaderText = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            this.gvPrjWiseIS.Columns[3].HeaderText = Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.txtDatefrom.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            ViewState["tblAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.gvPrjWiseIS.DataSource = ds1.Tables[0];
            this.gvPrjWiseIS.DataBind();
            ds1.Dispose();

            Session["Report1"] = gvPrjWiseIS;
            ((HyperLink)this.gvPrjWiseIS.HeaderRow.FindControl("hlbtntbCdataExel1")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            if (ds1.Tables[0].Rows.Count > 0)
                ((HyperLink)this.gvPrjWiseIS.HeaderRow.FindControl("hlbtnDetails")).NavigateUrl = "~/F_17_Acc/LinkAccount.aspx?Type=INDetails&Date1=" + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")
                        + "&Date2=" + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + "&opndate=" + Convert.ToDateTime(this.txtOpeningDate.Text).ToString("dd-MMM-yyyy"); ;

        }
    }
}

