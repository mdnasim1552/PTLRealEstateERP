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


    public partial class AccTrialBalanceAll : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {


                indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                RadioButtonList1.SelectedIndex = 0;
                RadioButtonList1_SelectedIndexChanged(null, null);
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void ImgbtnFindProjind_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string filter = "%";

            // string pactcode = (this.Request.QueryString["Type"].ToString() == "LandPrj") ? "16%" : "4[1-9]%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "GETPROJECTNAME", "", filter, "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProjectInd.DataSource = dt1;
            this.ddlProjectInd.DataTextField = "actdesc1";
            this.ddlProjectInd.DataValueField = "actcode";
            this.ddlProjectInd.DataBind();

            this.ddlProjectInd2.DataSource = dt1;
            this.ddlProjectInd2.DataTextField = "actdesc1";
            this.ddlProjectInd2.DataValueField = "actcode";
            this.ddlProjectInd2.DataBind();
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ViewSection();
            string value = this.RadioButtonList1.SelectedValue.ToString();
            ((Label)this.Master.FindControl("lblTitle")).Text = "";
            switch (value)
            {
                case "Mains":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Trial Balance";
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;
                case "TBConsolidated":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Trial Balance (Consolidated)";
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "Trial02":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Trial Balance (Category Wise)";
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
                case "HOTB":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Head Office Trial Balance";
                    this.RadioButtonList1.Items[3].Attributes["class"] = "lblactive blink_me";
                    break;

                case "PrjTrailBal":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Project Trial Balance";
                    this.RadioButtonList1.Items[4].Attributes["class"] = "lblactive blink_me";
                    this.ImgbtnFindProjind_Click(null, null);
                    break;
                case "TrailBal2":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Trial Balance 2";
                    this.RadioButtonList1.Items[5].Attributes["class"] = "lblactive blink_me";
                    break;
                case "LandPrj":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Trial Balance(WIP)";
                    this.RadioButtonList1.Items[6].Attributes["class"] = "lblactive blink_me";
                    this.ImgbtnFindProjind_Click(null, null);
                    break;
                case "PrjTrailBal3":
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Project Trial Balance -3";
                    this.RadioButtonList1.Items[7].Attributes["class"] = "lblactive blink_me";
                    this.ImgbtnFindProjind_Click(null, null);
                    break;

            }
        }
        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString () : comcod;
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
            string Type = this.RadioButtonList1.SelectedValue.ToString(); //this.Request.QueryString["Type"].ToString().Trim();
            double day; string date;
            switch (Type)
            {

                case "Mains":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefrom.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    if (comcod == "3337" || comcod == "3336" || comcod == "3101")
                    {
                        this.Operningdat();
                    }
                    this.GetchkBalance();

                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "PrjTrailBal":
                case "LandPrj":

                    this.txtDatefromP.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "TrailBal2":
                    this.txtDatefromT2.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "HOTB":
                case "TBConsolidated":
                    this.txtAsDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 3;

                    break;




                case "PrjTrailBal3":
                    date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtDatefromT3.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtDatefromT3.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 4;
                    break;





                case "Trial02":
                    day = Convert.ToInt32(System.DateTime.Today.ToString("dd")) - 1;
                    this.txtDatefromtb.Text = DateTime.Today.AddDays(-day).ToString("dd-MMM-yyyy");
                    this.txtDatetotb.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    if (comcod == "3337" || comcod == "3336" || comcod == "3101")
                    {
                        this.Operningdat();
                    }

                    this.MultiView1.ActiveViewIndex = 5;
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
                string eventdesc = "Show Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
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

            RadioButtonList1_SelectedIndexChanged(null, null);
            string Type = this.RadioButtonList1.SelectedValue.ToString(); //this.Request.QueryString["Type"].ToString().Trim();
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

                case "PrjTrailBal":
                case "LandPrj":

                    break;

                case "TrailBal2":
                    //date1 = this.txtDatefrombank.Text.Substring(0, 11).ToString();
                    //date2 = this.txtDatetobank.Text.Substring(0, 11).ToString();
                    //string levelbank = this.ddlReportLevelBank.SelectedValue.ToString();
                    //ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION", date1, date2, levelbank, "", "", "", "", "", "");
                    break;


                case "HOTB":
                case "TBConsolidated":
                    string date = this.txtAsDate.Text.Substring(0, 11).ToString();
                    string Level = this.ddlReportLevelcon.SelectedValue.ToString();
                    string hotb = (this.RadioButtonList1.SelectedValue.ToString() == "HOTB") ? "HOTB" : "";
                    ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCONTRIALBALANCE", date, Level, hotb, "", "", "", "", "", "");
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


        protected void btnPrjTr_Click(object sender, EventArgs e)
        {
            this.ShowPrjTriBal();
        }
        private void ShowPrjTriBal()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefromP.Text.Substring(0, 11);
            string actcode = (((ASTUtility.Right(this.ddlProjectInd.SelectedValue, 10) == "0000000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 2)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 8) == "00000000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 4)
                : (ASTUtility.Right(this.ddlProjectInd.SelectedValue, 4) == "0000") ? this.ddlProjectInd.SelectedValue.ToString().Substring(0, 8) : this.ddlProjectInd.SelectedValue.ToString()) + "%");

            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = mRptGroup == "0" ? "2" : mRptGroup == "1" ? "4" : mRptGroup == "2" ? "7" : mRptGroup == "3" ? "9" : "12";// (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
            string CallType = (ASTUtility.Left(actcode, 2) == "41") ? "RPT_PROJ_TRIALBAL" : (ASTUtility.Left(actcode, 2) == "16") ? "RPT_PROJ_TRIALBAL" : "RPTPROJTRIALBALHF";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", CallType, "", date1, actcode, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPrjtrbal.DataSource = null;
                this.gvPrjtrbal.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;
            this.gvPrjtrbal.DataSource = dt;
            this.gvPrjtrbal.DataBind();

            Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[2];


            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvPrjtrbal.HeaderRow.FindControl("hlbtntbCdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvPrjtrbal;
            ((HyperLink)this.gvPrjtrbal.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



        }

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
            string Type = this.RadioButtonList1.SelectedValue.ToString(); //this.Request.QueryString["Type"].ToString().Trim();
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
            string Type = this.RadioButtonList1.SelectedValue.ToString(); //this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {
                case "Mains":
                    this.PrintMainTrialBal();
                    break;
                case "PrjTrailBal":
                case "LandPrj":
                    this.RtpPrjTrBal();
                    break;
                case "TrailBal2":
                    this.RtpTrBal2();
                    break;

                case "HOTB":
                case "TBConsolidated":
                    this.PrintConTrialBal();
                    break;

                case "PrjTrailBal3":
                    this.PrintTrailBal3();
                    break;

            }

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).ToString();
                string eventdesc = "Print Report: " + Type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrintTrailBal3()
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Actcode = this.ddlProjectInd.SelectedItem.Text.ToString().Substring(13);
            string Date1 = Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyy");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            string daterange = "(From " + this.txtDatefrom.Text + " To " + this.txttodate.Text + ")";
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];

            string opndram = Convert.ToDouble(dt2.Rows[0]["opndram"]).ToString("##,###,#0; (#,#0); ");
            string opncram = Convert.ToDouble(dt2.Rows[0]["opncram"]).ToString("##,###,#0; (#,#0); ");
            string opnamt = Convert.ToDouble(dt2.Rows[0]["opnam"]).ToString("##,###,#0; (#,#0); ");
            string dramt = Convert.ToDouble(dt2.Rows[0]["dramt"]).ToString("##,###,#0; (#,#0); ");
            string cramt = Convert.ToDouble(dt2.Rows[0]["cramt"]).ToString("##,###,#0; (#,#0); ");
            string clsdram = Convert.ToDouble(dt2.Rows[0]["clsdram"]).ToString("##,###,#0; (#,#0); ");
            string clscram = Convert.ToDouble(dt2.Rows[0]["clscram"]).ToString("##,###,#0; (#,#0); ");
            string clsamt = Convert.ToDouble(dt2.Rows[0]["clsam"]).ToString("##,###,#0; (#,#0); ");


            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.TrailsBal3>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptTrailsBal3", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ProjName", "Project Name:" + Actcode));
            Rpt1.SetParameters(new ReportParameter("FDate", daterange));
            Rpt1.SetParameters(new ReportParameter("RptTital", "Project Trial Balance-3"));
            Rpt1.SetParameters(new ReportParameter("txtopndram", opndram));
            Rpt1.SetParameters(new ReportParameter("txtopncram", opncram));
            Rpt1.SetParameters(new ReportParameter("txtopnam", opnamt));
            Rpt1.SetParameters(new ReportParameter("txtcurdram", dramt));
            Rpt1.SetParameters(new ReportParameter("txtcurcram", cramt));
            Rpt1.SetParameters(new ReportParameter("txtclsdram", clsdram));
            Rpt1.SetParameters(new ReportParameter("txtclscram", clscram));
            Rpt1.SetParameters(new ReportParameter("txtclsam", clsamt));
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
            Rpt1.SetParameters(new ReportParameter("txtHeader", (this.RadioButtonList1.SelectedValue.ToString() == "TBConsolidated") ? "TRIAL BALANCE - " + this.ddlReportLevel.SelectedValue.ToString().Trim() : "TRIAL BALANCE ( Level - " + this.ddlReportLevel.SelectedValue.ToString().Trim() + " )"));
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
            txtHeader.Text = (this.RadioButtonList1.SelectedValue.ToString() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
                : (this.RadioButtonList1.SelectedValue.ToString() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE";



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
            Rpt1.SetParameters(new ReportParameter("Rpttitle", (this.RadioButtonList1.SelectedValue.ToString() == "Mains") ? "TRIAL BALANCE - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1)
                : (this.RadioButtonList1.SelectedValue.ToString() == "TBConsolidated") ? "TRIAL BALANCE (CONSOLIDATED) - " + ASTUtility.Right((this.ddlReportLevelcon.SelectedItem.Text), 1) : "HEAD OFFICE TRIAL BALANCE"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }








        private void RtpPrjTrBal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            switch (comcod)
            {

                case "3332":
                    this.PrintPrjTrBalaceInnstar();
                    break;
                default:
                    this.RtpPrjTrBalGen();
                    break;
            }
        }
        private void PrintPrjTrBalaceInnstar()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_32_Mis.RptProjTrialBalanceInnStar();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtfdate.Text = "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy");
            TextObject txtprojectname = rptstk.ReportDefinition.ReportObjects["txtProjName"] as TextObject;
            txtprojectname.Text = "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString(); // prjsdesc   this.ddlProjectInd.SelectedItem.ToString().Trim().Substring(13);


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource(dt1);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RtpPrjTrBalGen()
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

            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];
            DataTable dt3 = (DataTable)Session["tblPrjname"];
            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.ProjectTrlBal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptProjTrialBalance", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString()));
            Rpt1.SetParameters(new ReportParameter("txtdate", "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Project Trial Balance"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RtpTrBal2()
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

            DataTable dt1 = (DataTable)Session["tblprjtbl"];
            DataTable dt2 = (DataTable)Session["tblFooter"];

            if (dt1.Rows.Count == 0)
                return;

            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.Trialbal02>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_32_Mis.RptTrialBalance2", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            //Rpt1.SetParameters(new ReportParameter("txtprjname", "Project Name: " + (dt3.Rows[0]["prjsdesc"]).ToString())); // commant by iqbal Nayan
            Rpt1.SetParameters(new ReportParameter("txtdate", "As on Date: " + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Trial Balance 2"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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

            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else if (lebel2 == "")
            {
                //int lactcode = Convert.ToInt16(code.Substring(0, 1));
                //string opnoption = lactcode >= 3 ? "withoutOpn" : "";
                string opnoption = "";

                if (ASTUtility.Right(mACTCODE, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&opnoption=" + opnoption;
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
        protected void btmt2_Click(object sender, EventArgs e)
        {
            this.ShowTrailsBal2();
        }
        private void ShowTrailsBal2()
        {
            Session.Remove("tblprjtbl");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date1 = this.txtDatefromT2.Text.Substring(0, 11);
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTTRIALBALANCE2", date1, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.grvTrBal2.DataSource = null;
                this.grvTrBal2.DataBind();
                return;
            }

            DataTable dt = HiddenSameData(ds2.Tables[0]);
            Session["tblprjtbl"] = dt;

            this.grvTrBal2.DataSource = dt;
            this.grvTrBal2.DataBind();

            Session["tblFooter"] = ds2.Tables[1];


            ((Label)this.grvTrBal2.FooterRow.FindControl("lgvFTDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(dram)", "")) ?
                      0.00 : ds2.Tables[1].Compute("Sum(dram)", ""))).ToString("#,##0;(#,##0); - ");

            ((Label)this.grvTrBal2.FooterRow.FindControl("lgvFTCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[1].Compute("Sum(cram)", "")) ?
                     0.00 : ds2.Tables[1].Compute("Sum(cram)", ""))).ToString("#,##0;(#,##0); - ");

        }
        protected void btmt3_Click(object sender, EventArgs e)
        {
            this.ShowTrailsBal3();
        }
        private void ShowTrailsBal3()
        {

            Session.Remove("tblprjtbl");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string date = this.txtDatefromT3.Text.Substring(0, 11);
            string todate = this.txttodate.Text;
            string actcode = this.ddlProjectInd2.SelectedValue.ToString();
            //string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            //mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));

            string mRptGroup = this.chkdetails.Checked ? "Details" : "";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPT_PROJ_TRIALBALALL", date, todate, actcode, mRptGroup, "", "", "", "", "");
            if (ds2 == null)
                return;
            if (ds2.Tables[0].Rows.Count == 0)
            {
                this.gvprjtbal03.DataSource = null;
                this.gvprjtbal03.DataBind();
                return;
            }

            DataTable dt = ds2.Tables[0];
            Session["tblprjtbl"] = this.HiddenSameData(dt);
            Session["tblFooter"] = ds2.Tables[1];
            Session["tblPrjname"] = ds2.Tables[2];


            this.gvprjtbal03.DataSource = dt;
            this.gvprjtbal03.DataBind();





            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
            //((HyperLink)this.gvprjtbal03.HeaderRow.FindControl("hlbtntbCdataExeltp")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            Session["Report1"] = gvprjtbal03;
            ((HyperLink)this.gvprjtbal03.HeaderRow.FindControl("hlbtntbCdataExeltp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFopdbamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opndram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFopCramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opncram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFopnamtp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["opnam"]).ToString("#,##0;(#,##0);");



            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFTDrAmttp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["dramt"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["dramt"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFTCrAmttp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["cramt"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["cramt"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFoClsdbamt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["clsdram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["clsdram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFoClsCramt")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["clscram"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["clscram"]).ToString("#,##0;(#,##0);");
            ((Label)this.gvprjtbal03.FooterRow.FindControl("lgvFClsamtp")).Text = Convert.ToDouble(ds2.Tables[1].Rows[0]["clsam"]) == 0.00 ? "0" : Convert.ToDouble(ds2.Tables[1].Rows[0]["clsam"]).ToString("#,##0;(#,##0);");







        }
        protected void gvPrjtrbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
                mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;

                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");
                    actdesc.Attributes["style"] = "color:maroon;";
                }
                else if (code == "000000000001" || (ASTUtility.Right((code), 3) == "000") && mRptGroup == "12")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");

                }
                if (code == "999999999999" || code == "000000000002" || code == "000000000003" || code == "000000000004")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                    DAmount.Style.Add("text-align", "Right");
                    CAmount.Style.Add("text-align", "Right");
                }
                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }


                //if (e.Row.RowType != DataControlRowType.DataRow)
                //    return;
                string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode1")).ToString();
                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                string Actcode = this.ddlProjectInd.SelectedValue.ToString();
                string Date1 = Convert.ToDateTime(this.txtDatefromP.Text).ToString("dd-MMM-yyy");
                string rescode = ((Label)e.Row.FindControl("lblgvCode")).Text;
                if (ASTUtility.Left(rescode1, 2) == "51")
                {
                    hlink1.NavigateUrl = "~/F_32_Mis/RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + Actcode + "&Date1=" + Date1;
                }

                else if (ASTUtility.Right((code), 3) != "000" && code != "000000000001" && code != "999999999999" && code != "000000000002")
                {
                    hlink1.NavigateUrl = "~/F_32_Mis/RptProjectCollBrkDown.aspx?Type=SpLedger&pactcode=" + Actcode + "&Date1=" + Date1 + "&rescode=" + rescode;
                }




            }


        }
        protected void grvTrBal2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "DIFFERENCE")
                {
                    actdesc.Font.Bold = true;
                    DAmount.Font.Bold = true;
                    CAmount.Font.Bold = true;
                }


            }
        }
        protected void gvprjtbal03_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label rescod = (Label)e.Row.FindControl("lblgvCodetp");
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesctp");
                Label opnam = (Label)e.Row.FindControl("lgvopnamtp");
                Label DAmount = (Label)e.Row.FindControl("lgvAmttp");
                Label CAmount = (Label)e.Row.FindControl("lgvCreamttp");
                Label Clsam = (Label)e.Row.FindControl("lgvClsamtp");

                string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
                mRptGroup = (mRptGroup == "0" ? "4" : (mRptGroup == "1" ? "7" : (mRptGroup == "2" ? "9" : "12")));
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right((code), 10) == "0000000000")
                {

                    actdesc.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    rescod.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    opnam.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    DAmount.Attributes["style"] = opnam.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";
                    Clsam.Attributes["style"] = opnam.Attributes["style"] = "font-weight:bold;text-align:left; color:maroon;";


                }

                else if (code == "000000000001" || ((ASTUtility.Right((code), 3) == "000") && mRptGroup == "12"))
                {
                    actdesc.Font.Bold = true;
                    rescod.Font.Bold = true;
                    opnam.Font.Bold = true;
                    DAmount.Font.Bold = true;

                    CAmount.Font.Bold = true;
                    Clsam.Font.Bold = true;

                    opnam.Style.Add("text-align", "Left");
                    DAmount.Style.Add("text-align", "Left");
                    CAmount.Style.Add("text-align", "Left");
                    Clsam.Style.Add("text-align", "Left");

                }




                if (code == "AAAAAAAAAAAA")
                {
                    actdesc.Style.Add("text-align", "Left");
                }

                if (code == "010000000000")
                {
                    string frmdate = this.txtDatefromT3.Text;
                    string todate = this.txttodate.Text;
                    string actcode = this.ddlProjectInd2.SelectedValue;
                    actdesc.NavigateUrl = "~/F_17_Acc/CashBankposition.aspx?Type=cascredpur&frmdate=" + frmdate + "&todate=" +
                                          todate + "&actcode=" + actcode;
                }
                if (code == "040000000000")
                {
                    string frmdate = this.txtDatefromT3.Text;
                    string todate = this.txttodate.Text;
                    string actcode = this.ddlProjectInd2.SelectedValue;
                    actdesc.NavigateUrl = "~/F_17_Acc/CashBankposition.aspx?Type=labour&frmdate=" + frmdate + "&todate=" +
                                          todate + "&actcode=" + actcode;
                }



                if (code == "111111111111")
                {
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    string comcod = hst["comcod"].ToString();
                    string frmdate = this.txtDatefromT3.Text;
                    string todate = this.txttodate.Text;
                    //  string pactcode = this.ddlProjectInd.SelectedValue;
                    string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                    string witopn = "";

                    actdesc.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?comcod=" + comcod + "&rpttype=ledger&Date1=" + frmdate + "&Date2=" +
                                      todate + "&actcode=" + pactcode + "&opnoption=" + witopn;




                }


                else if (code.Substring(9) != "000")
                {
                    string frmdate = this.txtDatefromT3.Text;
                    string todate = this.txttodate.Text;
                    string pactcode = this.ddlProjectInd2.SelectedValue;
                    string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                    actdesc.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=spledgerprj&frmdate=" + frmdate + "&todate=" +
                                      todate + "&pactcode=" + pactcode + "&rescode=" + rescode;
                }




            }
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Type = this.RadioButtonList1.SelectedValue.ToString(); //this.Request.QueryString["Type"].ToString().Trim();
            string actcode4 = "", grpcode = "";

            switch (Type)
            {
                case "Mains":




                    break;

                case "Trial02":

                    actcode4 = dt1.Rows[0]["actcode4"].ToString();
                    //  string rescode = dt1.Rows[0]["rescode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                            dt1.Rows[j]["actdesc4"] = "";
                        actcode4 = dt1.Rows[j]["actcode4"].ToString();


                    }

                    break;

                case "PrjTrailBal":
                case "LandPrj":
                    grpcode = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
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

                case "TrailBal2":
                case "PrjTrailBal3":
                    string actcode1 = dt1.Rows[0]["actcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode1"].ToString() == actcode1)
                        {
                            actcode1 = dt1.Rows[j]["actcode1"].ToString();
                            dt1.Rows[j]["actdesc1"] = "";
                        }
                        else
                        {
                            actcode1 = dt1.Rows[j]["actcode1"].ToString();
                        }

                    }

                    break;

                case "HOTB":
                case "TBConsolidated":
                    break;


            }



            return dt1;

        }


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

            Session["tblAcc"] = HiddenSameData(ds1.Tables[0]);

            this.gvtrialbalance01.DataSource = ds1.Tables[0];
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
