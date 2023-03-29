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

namespace RealERPWEB.F_45_GrAcc
{
    public partial class RptAccRecPayment : System.Web.UI.Page
    {
        ProcessAccess GrpData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "GrpRecAndPay") ? "RECEIVE AND PAYAMENT INFORMATIN"
                    : (this.Request.QueryString["Type"].ToString().Trim() == "BankBalance") ? "BANK BALANCE INFORMATIN" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "IncomeStatement") ? "INCOME STATEMENT INFORMATIN" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "BalanceSheet") ? "Statement of Financial Position" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "BalanceDet") ? "BANK BALANCE DETAILS" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "IssueVsCollect") ? "ISSUE VS. COLLECTION" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "CashFlow") ? "Statement of Cash Flow" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "RecAndPayment") ? "Receipts & Payment A/C" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "RecAndPayment01") ? "Receipts & Payment A/C" :

                    (this.Request.QueryString["Type"].ToString().Trim() == "Schedule") ? "Schedule" :
                    (this.Request.QueryString["Type"].ToString().Trim() == "TrialBalance") ? "Trial Balance" : "COST OF FUND OF PROJECTS";

                //this.chkConsolidate.Checked = true;
                this.chkConsolidate_CheckedChanged(null, null);

                //this.chkall.Checked = true;
                this.chkall_CheckedChanged(null, null);
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDateFrom.Text = Convert.ToDateTime("01" + (this.txtDateto.Text.Trim().Substring(3))).ToString("dd-MMM-yyyy");

                this.SectionView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void SectionView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "RecAndPayment01":
                case "RecAndPayment":

                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


                case "Schedule":
                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    //this.chkListGroup.Items[3].Selected = true;
                    this.GetAccountCode();
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "TrialBalance":
                    // this.chkListGroup.Visible = false;
                    //gvGprTB this.chkListGroup.Items[3].Selected = false;
                    //this.chkListGroup.Items[0].Selected = true;
                    //this.chkListGroup.Items[3].Selected = true;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "IncomeStatement":
                    this.lblDateOpening.Visible = true;
                    this.txtDateOpening.Visible = true;
                    this.txtDateOpening.Text = Convert.ToDateTime(this.txtDateFrom.Text.Trim()).AddMonths(-1).ToString("dd-MMM-yyyy");
                    this.chkListGroup.Items[3].Selected = false;
                    //this.chkListGroup.Items[0].Selected = true;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "BalanceSheet":
                    //this.chkListGroup.Items[3].Selected = true;
                    //this.chkListGroup.Items[0].Selected = true;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "BalanceDet":
                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    //this.chkListGroup.Items[3].Selected = true;
                    this.Panelschedule.Visible = false;
                    this.lblDatefrom.Visible = false;
                    this.txtDateFrom.Visible = false;
                    this.lblDateTo.InnerText = "As on Date: ";
                    this.lbltakaInLac.Text = "Select Max 4 Company";
                    this.MultiView1.ActiveViewIndex = 5;
                    break;
                case "CashFlow":
                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    //this.chkListGroup.Items[3].Selected = true;
                    this.Panelschedule.Visible = false;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 6;
                    break;

                case "BankBalance":
                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    //this.chkListGroup.Items[3].Selected = true;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 7;
                    break;


                case "IssueVsCollect":
                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    this.lbltakaInLac.Text = "Select Max 6 Company";
                    this.MultiView1.ActiveViewIndex = 8;
                    break;



                case "PrjTrialBal":
                    this.GetProjectName();
                    this.lblGroup.Visible = false;
                    this.chkListGroup.Visible = false;
                    this.txtDateFrom.Visible = false;
                    this.lblDatefrom.Visible = false;

                    this.MultiView1.ActiveViewIndex = 9;
                    break;
            }
        }


        protected void CallCompanyList()
        {
            string comcod = this.GetCompCode();
            string consolidate = "";
            DataSet ds1 = this.GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "COMPLIST", consolidate, "", "", "", "", "", "", "", "");
            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = "comcod not like '0000%'";
            DataTable dt = dv.ToTable();
            this.ddlComCode.DataTextField = "comsnam";
            this.ddlComCode.DataValueField = "comcod";
            this.ddlComCode.DataSource = dt;
            this.ddlComCode.DataBind();
            this.hdncomcode.Value = dt.Rows.Count == 0 ? "" : dt.Rows[0]["comcod"].ToString();

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.hdncomcode.Value;
            DataSet ds1 = GrpData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", "GETPROJECTNAME", "", "", "", "", "", "", "", "", "");
            DataTable dt1 = ds1.Tables[0];
            this.ddlProject.DataSource = dt1;
            this.ddlProject.DataTextField = "actdesc1";
            this.ddlProject.DataValueField = "actcode";
            this.ddlProject.DataBind();
        }
        private void GetAccountCode()
        {
            string comp1 = "";
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            string findAccCode = this.txtScrchAccCode.Text.Trim() + "%";
            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "GETMAINACCCODE", findAccCode, "", "", "", "", "", "", "", "");
            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataBind();
            ds1.Dispose();


        }


        protected void ibtnFindAccCode_Click(object sender, EventArgs e)
        {

            this.GetAccountCode();

        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "RecAndPayment":

                    this.ShowGrpRecAndPayment();
                    break;


                case "RecAndPayment01":
                    this.ShowGrpRecAndPayment01();
                    break;

                case "IssueVsCollect":
                    this.ShowGrpIssuevsCollection();
                    break;



                case "Schedule":
                    this.ShowSchedule();
                    break;

                case "TrialBalance":
                    this.ShowTrialBalance();
                    break;
                case "IncomeStatement":
                    this.ShowIncomeStatement();
                    break;
                case "BalanceSheet":
                    this.ShowBalanceSheet();
                    break;
                case "BalanceDet":
                    this.ShowBankDet();
                    break;
                case "CashFlow":
                    this.ShowCashFlow();
                    break;


                case "BankBalance":
                    this.ShowBankBalance();
                    break;

                case "PrjTrialBal":
                    this.ShowProjTrialBalance();
                    break;
            }


        }
        private void ShowGrpRecAndPayment()
        {

            Session.Remove("tblrecandpayment");
            this.PnlbStatus.Visible = true;
            string comp1 = "";
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            string grp1 = this.chkListGroup.SelectedValue;
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}


            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "RP_GROUP", date1, date2, grp1, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvGrpRP.DataSource = null;
                this.gvGrpRP.DataBind();
                this.gvGrpRPBS.DataSource = null;
                this.gvGrpRPBS.DataBind();

            }

            Session["tblrecandpayment"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblbs"] = ds1.Tables[2];
            this.Data_Bind();
            ds1.Dispose();
        }

        private void ShowGrpRecAndPayment01()
        {

            Session.Remove("tblrecandpayment");
            this.PnlbStatus.Visible = true;
            string comp1 = "";
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;

            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            string grp1 = this.chkListGroup.SelectedValue;

            //for (int i = 0; i < this.cblCompany.Items.Count; i++)
            //    comp1 += (this.cblCompany.Items[i].Selected ? this.cblCompany.Items[i].Value.ToString() : "");
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}



            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "RP_GROUP01", date1, date2, grp1, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvGrpRP.DataSource = null;
                this.gvGrpRP.DataBind();
                this.gvGrpRPBS.DataSource = null;
                this.gvGrpRPBS.DataBind();

            }
            Session["dstrecpayment"] = ds1.Copy();

            Session["tblrecandpayment"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblbs"] = this.HiddenSameData02(ds1.Tables[2]);
            this.Data_Bind();
            ds1.Dispose();
        }
        private void ShowSchedule()
        {
            Session.Remove("tblrecandpayment");
            string comp1 = "";
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            string grp1 = this.chkListGroup.SelectedValue;
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}
            string bankcode = this.ddlAccHead.SelectedValue.ToString() + "%";
            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "SC_GROUP", date1, date2, grp1, bankcode, "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvGrpCS.DataSource = null;
                this.gvGrpCS.DataBind();
            }

            Session["tblrecandpayment"] = ds1.Tables[0];
            this.Data_Bind();
        }



        private void ShowBankBalance()
        {
            Session.Remove("tblrecandpayment");

            string comp1 = "";
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            string grp1 = this.chkListGroup.SelectedValue;
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}
            string bankcode = this.ddlAccHead.SelectedValue.ToString() + "%";
            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "BB_GROUP", date1, date2, grp1, bankcode, "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvGrpBB.DataSource = null;
                this.gvGrpBB.DataBind();
            }

            Session["tblrecandpayment"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }




        private void ShowTrialBalance()
        {
            Session.Remove("tblrecandpayment");
            string comp1 = "";
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            string grp1 = this.chkListGroup.SelectedValue;
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}
            //string bankcode = (this.ddlAccHead.Items.Count == 0) ? "[12]9%" : this.ddlAccHead.SelectedValue.ToString() + "%";
            string grp1val = (grp1 == "1") ? "2" : (grp1 == "2") ? "4" : (grp1 == "3") ? "8" : "12";
            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "TB_GROUP", date1, date2, grp1val, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvGprTB.DataSource = null;
                this.gvGprTB.DataBind();
            }

            Session["tblrecandpayment"] = ds1.Tables[0];
            this.Data_Bind();
        }


        private void ShowProjTrialBalance()
        {



            Session.Remove("tblrecandpayment");
            string comp1 = "";
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            //string date1 = this.txtDateFrom.Text;
            string date1 = this.txtDateto.Text;
            string grp1 = this.chkListGroup.SelectedValue;
           
            string grp1val = (grp1 == "1") ? "2" : (grp1 == "2") ? "4" : (grp1 == "3") ? "8" : "12";
            string pactcode = this.ddlProject.SelectedValue.ToString();

            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_PROJECTTB", "PROJECTTRIALBALANCE", "", date1, pactcode, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvPrjtrbal.DataSource = null;
                this.gvPrjtrbal.DataBind();
            }

            Session["tblrecandpayment"] = ds1.Tables[0];
            this.Data_Bind();


        }

        private string GetCompanyHeadAllocation()
        {
            string comcod = this.GetCompCode();
            string headallocation = "";
            switch (comcod)
            {
                case "8348":// Credence               
                    headallocation = "Headallocation";
                    break;


                default:

                    break;


            }

            return headallocation;


        }
        private void ShowIncomeStatement()
        {
            Session.Remove("tblrecandpayment");
            string comp1 = "";
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            string grp1 = this.chkListGroup.SelectedValue;
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}
            string grp1val = (grp1 == "1") ? "2" : (grp1 == "2") ? "4" : (grp1 == "3") ? "8" : "12";
            string opndate = this.txtDateOpening.Text.Trim();
            string headallocation = this.GetCompanyHeadAllocation();
            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORT_GRPACC_IS_BS", "RPTGRPINCOMESTMNT", date1, date2, grp1val, opndate, headallocation, "", "", "", "");

            if (ds1 == null)
            {
                this.gvIncomeSt.DataSource = null;
                this.gvIncomeSt.DataBind();
                return;
            }
            //return;
            Session["tblrecandpayment"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["tblrecandpayment"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private void ShowBalanceSheet()
        {
            Session.Remove("tblrecandpayment");
            string comp1 = "";
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");

            string grp1 = this.chkListGroup.SelectedValue;

            string grp1val = (grp1 == "1") ? "2" : (grp1 == "2") ? "4" : (grp1 == "3") ? "8" : "12";
            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORT_GRPACC_IS_BS", "RPTGRPBALANCEWHEET", date1, date2, grp1val, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvBalSheet.DataSource = null;
                this.gvBalSheet.DataBind();
                return;
            }
            Session["tblrecandpayment"] = ds1.Tables[0];
            //Session["tblrecandpayment"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void ShowBankDet()
        {
            Session.Remove("tblrecandpayment");
            string comp1 = "";
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");

            string date2 = this.txtDateto.Text;
            string grp1 = this.chkListGroup.SelectedValue;
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}

            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "BBAL_GROUP", "", date2, grp1, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.grvBankDet.DataSource = null;
                this.grvBankDet.DataBind();
            }

            Session["tblrecandpayment"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void ShowCashFlow()
        {
            Session.Remove("tblrecandpayment");
            string comp1 = "";
            this.PnlbStatuscf.Visible = true;
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");

            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "GRPRPTCASHFLOW", date1, date2, "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.grvCashFlow.DataSource = null;
                this.grvCashFlow.DataBind();
                this.gvGrpCFBS.DataSource = null;
                this.gvGrpCFBS.DataBind();
                return;
            }

            Session["tblrecandpayment"] = HiddenSameData(ds1.Tables[0]);
            ViewState["tblbs"] = ds1.Tables[1];
            this.Data_Bind();
        }
        private void ShowGrpIssuevsCollection()
        {
            Session.Remove("tblrecandpayment");
            this.PnlbStatus.Visible = true;
            string comp1 = "";
            string date1 = this.txtDateFrom.Text;
            string date2 = this.txtDateto.Text;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                comp1 += (this.ddlComCode.Items[i].Selected ? this.ddlComCode.Items[i].Value.ToString() : "");
            string grp1 = this.chkListGroup.SelectedValue;
            //string grp1 = (this.chkListGroup.Items[0].Selected ? "1" : "") + (this.chkListGroup.Items[1].Selected ? "2" : "")
            //            + (this.chkListGroup.Items[2].Selected ? "3" : "") + (this.chkListGroup.Items[3].Selected ? "4" : "");
            //if (grp1.Length == 0)
            //{
            //    this.chkListGroup.Items[3].Selected = true;
            //    grp1 = "4";
            //}


            DataSet ds1 = this.GrpData.GetTransInfo(comp1, "SP_REPORTO_GROUP_ACC_TB_RP", "ISSUEVSCOLLECT_GROUP", date1, date2, grp1, "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvGrpIVsC.DataSource = null;
                this.gvGrpIVsC.DataBind();


            }

            Session["tblrecandpayment"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();



        }

        private DataTable HiddenSameData02(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "RecAndPayment":
                case "RecAndPayment01":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if ((dt1.Rows[j]["grp"].ToString() == grp) && (dt1.Rows[j]["actcode"].ToString() == actcode))
                        {

                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["actdesc"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["grp"].ToString() == grp)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }

                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            {
                                dt1.Rows[j]["actdesc"] = "";

                            }

                            grp = dt1.Rows[j]["grp"].ToString();
                            actcode = dt1.Rows[j]["actcode"].ToString();
                        }


                    }

                    break;




                default:
                    break;





            }

            return dt1;

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "RecAndPayment":
                case "IssueVsCollect":
                    string rpcode = dt1.Rows[0]["rpcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if ((dt1.Rows[j]["rpcode"].ToString() == rpcode))
                        {
                            rpcode = dt1.Rows[j]["rpcode"].ToString();
                            dt1.Rows[j]["rpdesc"] = "";
                        }

                        else
                        {
                            rpcode = dt1.Rows[j]["rpcode"].ToString();
                        }
                    }

                    break;


                case "RecAndPayment01":

                    string rpcod = dt1.Rows[0]["rpcode"].ToString();
                    string actcode = dt1.Rows[0]["actcode"].ToString();


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {

                        if ((dt1.Rows[j]["rpcode"].ToString() == rpcod) && (dt1.Rows[j]["actcode"].ToString() == actcode))
                        {

                            dt1.Rows[j]["rpdesc"] = "";

                        }

                        else
                        {
                            if (dt1.Rows[j]["rpcode"].ToString() == rpcod)
                            {
                                dt1.Rows[j]["rpdesc"] = "";
                            }




                            rpcod = dt1.Rows[j]["rpcode"].ToString();
                            //actcode = dt1.Rows[j]["actcode"].ToString();
                            //actdesc1 = dt1.Rows[j]["actdesc1"].ToString();
                        }


                    }

                    break;

                case "TrialBalance":

                    break;
                case "IncomeStatement":
                    string mgrp = dt1.Rows[0]["mgrp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mgrp"].ToString() == mgrp)
                        {
                            mgrp = dt1.Rows[j]["mgrp"].ToString();
                            dt1.Rows[j]["mgrp"] = "";



                        }

                        else
                        {
                            mgrp = dt1.Rows[j]["mgrp"].ToString();
                        }
                    }

                    break;
                case "BalanceSheet":

                    break;
                case "BalanceDet":

                    break;
                case "CashFlow":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    string grp1 = dt1.Rows[0]["grp1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["grp1"].ToString() == grp1)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            grp1 = dt1.Rows[j]["grp1"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["grpdesc1"] = "";

                        }

                        else
                        {


                            if (dt1.Rows[j]["grp"].ToString() == grp)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grpdesc1"] = "";
                            }

                            grp = dt1.Rows[j]["grp"].ToString();
                            grp1 = dt1.Rows[j]["grp1"].ToString();

                        }

                    }

                    break;

                case "Schedule":
                    break;

                case "BankBalance":
                    string maingrp = dt1.Rows[0]["maingrp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["maingrp"].ToString() == maingrp)
                            dt1.Rows[j]["mgrpdesc"] = "";
                        maingrp = dt1.Rows[j]["maingrp"].ToString();
                    }

                    break;





            }

            return dt1;

        }

        private void Data_Bind()
        {
            int i, j;
            DataTable dt, dt1; DataView dv;
            dt1 = ((DataTable)Session["tblrecandpayment"]).Copy();
            dv = ((DataTable)Session["tblrecandpayment"]).DefaultView;
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8;


            switch (Type)
            {
                case "RecAndPayment":

                    dv.RowFilter = ("grp1 like 'UT%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvGrpRP.Columns[3].Visible = (amt1 != 0);
                    this.gvGrpRP.Columns[4].Visible = (amt2 != 0);
                    this.gvGrpRP.Columns[5].Visible = (amt3 != 0);
                    this.gvGrpRP.Columns[6].Visible = (amt4 != 0);
                    this.gvGrpRP.Columns[7].Visible = (amt5 != 0);
                    this.gvGrpRP.Columns[8].Visible = (amt6 != 0);
                    this.gvGrpRP.Columns[9].Visible = (amt7 != 0);
                    this.gvGrpRP.Columns[10].Visible = (amt8 != 0);




                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGrpRP.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGrpRP.DataSource = dt1;
                    this.gvGrpRP.DataBind();

                    // Bank Balance


                    this.gvGrpRPBS.Columns[3].Visible = (amt1 != 0);
                    this.gvGrpRPBS.Columns[4].Visible = (amt2 != 0);
                    this.gvGrpRPBS.Columns[5].Visible = (amt3 != 0);
                    this.gvGrpRPBS.Columns[6].Visible = (amt4 != 0);
                    this.gvGrpRPBS.Columns[7].Visible = (amt5 != 0);
                    this.gvGrpRPBS.Columns[8].Visible = (amt6 != 0);
                    this.gvGrpRPBS.Columns[9].Visible = (amt7 != 0);
                    this.gvGrpRPBS.Columns[10].Visible = (amt8 != 0);




                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGrpRPBS.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGrpRPBS.DataSource = (DataTable)ViewState["tblbs"];
                    this.gvGrpRPBS.DataBind();
                    break;

                case "RecAndPayment01":


                    dv.RowFilter = ("grp1 like 'UT%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvGrpRP.Columns[3].Visible = (amt1 != 0);
                    this.gvGrpRP.Columns[4].Visible = (amt2 != 0);
                    this.gvGrpRP.Columns[5].Visible = (amt3 != 0);
                    this.gvGrpRP.Columns[6].Visible = (amt4 != 0);
                    this.gvGrpRP.Columns[7].Visible = (amt5 != 0);
                    this.gvGrpRP.Columns[8].Visible = (amt6 != 0);
                    this.gvGrpRP.Columns[9].Visible = (amt7 != 0);
                    this.gvGrpRP.Columns[10].Visible = (amt8 != 0);




                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGrpRP.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGrpRP.DataSource = dt1;
                    this.gvGrpRP.DataBind();

                    // Bank Balance


                    //this.gvGrpRPBS.Columns[3].Visible = (amt1 != 0);
                    //this.gvGrpRPBS.Columns[4].Visible = (amt2 != 0);
                    //this.gvGrpRPBS.Columns[5].Visible = (amt3 != 0);
                    //this.gvGrpRPBS.Columns[6].Visible = (amt4 != 0);
                    //this.gvGrpRPBS.Columns[7].Visible = (amt5 != 0);
                    //this.gvGrpRPBS.Columns[8].Visible = (amt6 != 0);
                    //this.gvGrpRPBS.Columns[9].Visible = (amt7 != 0);
                    //this.gvGrpRPBS.Columns[10].Visible = (amt8 != 0);




                    //j = 3;
                    //for (i = 0; i < this.cblCompany.Items.Count; i++)
                    //{
                    //    if (this.cblCompany.Items[i].Selected)
                    //    {
                    //        this.gvbankstaus2.Columns[j].HeaderText = this.cblCompany.Items[i].Text.Trim();
                    //        j++;
                    //    }
                    //}

                    this.gvbankstaus2.DataSource = (DataTable)ViewState["tblbs"];
                    this.gvbankstaus2.DataBind();
                    break;

                case "Schedule":
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvGrpCS.Columns[3].Visible = (amt1 != 0);
                    this.gvGrpCS.Columns[4].Visible = (amt2 != 0);
                    this.gvGrpCS.Columns[5].Visible = (amt3 != 0);
                    this.gvGrpCS.Columns[6].Visible = (amt4 != 0);
                    this.gvGrpCS.Columns[7].Visible = (amt5 != 0);
                    this.gvGrpCS.Columns[8].Visible = (amt6 != 0);
                    this.gvGrpCS.Columns[9].Visible = (amt7 != 0);
                    this.gvGrpCS.Columns[10].Visible = (amt8 != 0);
                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGrpCS.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGrpCS.DataSource = dt1;
                    this.gvGrpCS.DataBind();
                    this.FooterCalculation(dt1);
                    break;
                case "TrialBalance":

                    dv.RowFilter = ("grp1 like 'TD%' or grp1 like 'TC%'");
                    dt = dv.ToTable();
                    dt = dv.ToTable();

                    dv.RowFilter = ("grp1 <>'TD' and grp1 <>'TC'");
                    dt1 = dv.ToTable();
                    dt1 = dv.ToTable();
                    for (i = 3; i < this.gvGprTB.Columns.Count; i++)
                        this.gvGprTB.Columns[i].Visible = false;
                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGprTB.Columns[j].Visible = true;
                            this.gvGprTB.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGprTB.DataSource = dt1;
                    this.gvGprTB.DataBind();
                    this.FooterCalculation(dt);
                    break;
                case "IncomeStatement":
                    dv.RowFilter = ("actcode not like '%AAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvIncomeSt.Columns[4].Visible = (amt1 != 0);
                    this.gvIncomeSt.Columns[5].Visible = (amt2 != 0);
                    this.gvIncomeSt.Columns[6].Visible = (amt3 != 0);
                    this.gvIncomeSt.Columns[7].Visible = (amt4 != 0);
                    this.gvIncomeSt.Columns[8].Visible = (amt5 != 0);
                    this.gvIncomeSt.Columns[9].Visible = (amt6 != 0);
                    this.gvIncomeSt.Columns[10].Visible = (amt7 != 0);
                    this.gvIncomeSt.Columns[11].Visible = (amt8 != 0);
                    j = 4;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvIncomeSt.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvIncomeSt.DataSource = dt1;
                    this.gvIncomeSt.DataBind();
                    //this.FooterCalculation(dt1);
                    break;
                case "BalanceSheet":
                    dv.RowFilter = ("actcode not like '%AAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvBalSheet.Columns[4].Visible = (amt1 != 0);
                    this.gvBalSheet.Columns[5].Visible = (amt2 != 0);
                    this.gvBalSheet.Columns[6].Visible = (amt3 != 0);
                    this.gvBalSheet.Columns[7].Visible = (amt4 != 0);
                    this.gvBalSheet.Columns[8].Visible = (amt5 != 0);
                    this.gvBalSheet.Columns[9].Visible = (amt6 != 0);
                    this.gvBalSheet.Columns[10].Visible = (amt7 != 0);
                    this.gvBalSheet.Columns[11].Visible = (amt8 != 0);
                    j = 4;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvBalSheet.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvBalSheet.DataSource = dt1;
                    this.gvBalSheet.DataBind();
                    break;
                case "BalanceDet":
                    double bamt01, bamt02, bamt03, bamt04, bamt05, liamt01, liamt02, liamt03, liamt04, liamt05, avamt01, avamt02, avamt03, avamt04, avamt05;
                    dt = dv.ToTable();
                    bamt01 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt01)", "")) ? 0.00 : dt.Compute("sum(bamt01)", "")));
                    liamt01 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt01)", "")) ? 0.00 : dt.Compute("sum(liamt01)", "")));
                    avamt01 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt01)", "")) ? 0.00 : dt.Compute("sum(avamt01)", "")));

                    bamt02 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt02)", "")) ? 0.00 : dt.Compute("sum(bamt02)", "")));
                    liamt02 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt02)", "")) ? 0.00 : dt.Compute("sum(liamt02)", "")));
                    avamt02 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt02)", "")) ? 0.00 : dt.Compute("sum(avamt02)", "")));

                    bamt03 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt03)", "")) ? 0.00 : dt.Compute("sum(bamt03)", "")));
                    liamt03 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt03)", "")) ? 0.00 : dt.Compute("sum(liamt03)", "")));
                    avamt03 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt03)", "")) ? 0.00 : dt.Compute("sum(avamt03)", "")));

                    bamt04 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt04)", "")) ? 0.00 : dt.Compute("sum(bamt04)", "")));
                    liamt04 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt04)", "")) ? 0.00 : dt.Compute("sum(liamt04)", "")));
                    avamt04 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt04)", "")) ? 0.00 : dt.Compute("sum(avamt04)", "")));

                    bamt05 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt05)", "")) ? 0.00 : dt.Compute("sum(bamt05)", "")));
                    liamt05 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt05)", "")) ? 0.00 : dt.Compute("sum(liamt05)", "")));
                    avamt05 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt05)", "")) ? 0.00 : dt.Compute("sum(avamt05)", "")));

                    this.grvBankDet.Columns[3].Visible = (bamt01 != 0);
                    this.grvBankDet.Columns[4].Visible = (liamt01 != 0);
                    this.grvBankDet.Columns[5].Visible = (avamt01 != 0);
                    this.grvBankDet.Columns[9].Visible = (bamt02 != 0);
                    this.grvBankDet.Columns[10].Visible = (liamt02 != 0);
                    this.grvBankDet.Columns[11].Visible = (avamt02 != 0);
                    this.grvBankDet.Columns[12].Visible = (bamt03 != 0);
                    this.grvBankDet.Columns[13].Visible = (liamt03 != 0);
                    this.grvBankDet.Columns[14].Visible = (avamt03 != 0);
                    this.grvBankDet.Columns[15].Visible = (bamt04 != 0);
                    this.grvBankDet.Columns[16].Visible = (liamt04 != 0);
                    this.grvBankDet.Columns[17].Visible = (avamt04 != 0);
                    this.grvBankDet.Columns[18].Visible = (bamt05 != 0);
                    this.grvBankDet.Columns[19].Visible = (liamt05 != 0);
                    this.grvBankDet.Columns[20].Visible = (avamt05 != 0);
                    j = 6;

                    string[] arname = { "Bank Amt", "Bank Liabilities", "Available" };

                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {


                            for (int k = 0; k < 3; k++)
                            {
                                this.grvBankDet.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim() + "<br />" + arname[k];
                                j++;

                            }
                        }
                    }

                    this.grvBankDet.DataSource = dt1;
                    this.grvBankDet.DataBind();
                    this.FooterCalculation(dt1);
                    break;
                case "CashFlow":
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.grvCashFlow.Columns[4].Visible = (amt1 != 0);
                    this.grvCashFlow.Columns[5].Visible = (amt2 != 0);
                    this.grvCashFlow.Columns[6].Visible = (amt3 != 0);
                    this.grvCashFlow.Columns[7].Visible = (amt4 != 0);
                    this.grvCashFlow.Columns[8].Visible = (amt5 != 0);
                    this.grvCashFlow.Columns[9].Visible = (amt6 != 0);
                    this.grvCashFlow.Columns[10].Visible = (amt7 != 0);
                    this.grvCashFlow.Columns[11].Visible = (amt8 != 0);
                    j = 4;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.grvCashFlow.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.grvCashFlow.DataSource = dt1;
                    this.grvCashFlow.DataBind();

                    // Bank Balance


                    //this.gvGrpCFBS.Columns[3].Visible = (amt1 != 0);
                    //this.gvGrpCFBS.Columns[4].Visible = (amt2 != 0);
                    //this.gvGrpCFBS.Columns[5].Visible = (amt3 != 0);
                    //this.gvGrpCFBS.Columns[6].Visible = (amt4 != 0);
                    //this.gvGrpCFBS.Columns[7].Visible = (amt5 != 0);
                    //this.gvGrpCFBS.Columns[8].Visible = (amt6 != 0);
                    //this.gvGrpCFBS.Columns[9].Visible = (amt7 != 0);
                    //this.gvGrpCFBS.Columns[10].Visible = (amt8 != 0);




                    //j = 3;
                    //for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    //{
                    //    if (this.ddlComCode.Items[i].Selected)
                    //    {
                    //        this.gvGrpCFBS.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                    //        j++;
                    //    }
                    //}

                    //this.gvGrpCFBS.DataSource = (DataTable)ViewState["tblbs"];
                    //this.gvGrpCFBS.DataBind();


                    break;

                case "BankBalance":
                    dv.RowFilter = ("actcode='BBBBAAAAAAAA'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvGrpBB.Columns[3].Visible = (amt1 != 0);
                    this.gvGrpBB.Columns[4].Visible = (amt2 != 0);
                    this.gvGrpBB.Columns[5].Visible = (amt3 != 0);
                    this.gvGrpBB.Columns[6].Visible = (amt4 != 0);
                    this.gvGrpBB.Columns[7].Visible = (amt5 != 0);
                    this.gvGrpBB.Columns[8].Visible = (amt6 != 0);
                    this.gvGrpBB.Columns[9].Visible = (amt7 != 0);
                    this.gvGrpBB.Columns[10].Visible = (amt8 != 0);
                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGrpBB.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGrpBB.DataSource = dt1;
                    this.gvGrpBB.DataBind();
                    this.FooterCalculation(dt1);
                    break;


                case "IssueVsCollect":
                    dv.RowFilter = ("grp1 like 'UT%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", "")));
                    this.gvGrpIVsC.Columns[3].Visible = (amt1 != 0);
                    this.gvGrpIVsC.Columns[4].Visible = (amt2 != 0);
                    this.gvGrpIVsC.Columns[5].Visible = (amt3 != 0);
                    this.gvGrpIVsC.Columns[6].Visible = (amt4 != 0);
                    this.gvGrpIVsC.Columns[7].Visible = (amt5 != 0);
                    this.gvGrpIVsC.Columns[8].Visible = (amt6 != 0);
                    this.gvGrpIVsC.Columns[9].Visible = (amt7 != 0);
                    this.gvGrpIVsC.Columns[10].Visible = (amt8 != 0);




                    j = 3;
                    for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    {
                        if (this.ddlComCode.Items[i].Selected)
                        {
                            this.gvGrpIVsC.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim();
                            j++;
                        }
                    }

                    this.gvGrpIVsC.DataSource = dt1;
                    this.gvGrpIVsC.DataBind();
                    break;


                case "PrjTrialBal":

                    //dv.RowFilter = ("grp1 like 'TD%' or grp1 like 'TC%'");
                    //dt = dv.ToTable();
                    //dt = dv.ToTable();

                    //dv.RowFilter = ("grp1 <>'TD' and grp1 <>'TC'");
                    //dt1 = dv.ToTable();
                    //dt1 = dv.ToTable();

                    //for (i = 3; i < this.gvPrjtrbal.Columns.Count; i++)
                    //    this.gvPrjtrbal.Columns[i].Visible = false;
                    //    j = 3;
                    //for (i = 0; i < this.ddlComCode.Items.Count; i++)
                    //{
                    //    if (this.ddlComCode.Items[i].Selected)
                    //    {
                    //        this.gvPrjtrbal.Columns[j].Visible = true;
                          
                    //        j++;
                    //    }
                    //}


                    this.gvPrjtrbal.DataSource = dt1;
                    this.gvPrjtrbal.DataBind();
                    break;

                   
            }

        }

        private void FooterCalculation(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {


                case "Schedule":
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblftotamtcs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totamt)", "")) ? 0.00 : dt.Compute("sum(totamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt01cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt02cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt03cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt04cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt05cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt06cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt07cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpCS.FooterRow.FindControl("lblfamt08cs")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", ""))).ToString("#,##0;(#,##0); ");

                    break;



                case "TrialBalance":
                    double todramt, tocramt, tDramt01, tCramt01, tDramt02, tCramt02, tDramt03, tCramt03, tDramt04, tCramt04, tDramt05, tCramt05, tDramt06, tCramt06, tDramt07, tCramt07, tDramt08, tCramt08;
                    todramt = Convert.ToDouble(dt.Rows[0]["totamt"]);
                    tocramt = Convert.ToDouble(dt.Rows[1]["totamt"]);
                    tDramt01 = Convert.ToDouble(dt.Rows[0]["amt01"]);
                    tCramt01 = Convert.ToDouble(dt.Rows[1]["amt01"]);
                    tDramt02 = Convert.ToDouble(dt.Rows[0]["amt02"]);
                    tCramt02 = Convert.ToDouble(dt.Rows[1]["amt02"]);
                    tDramt03 = Convert.ToDouble(dt.Rows[0]["amt03"]);
                    tCramt03 = Convert.ToDouble(dt.Rows[1]["amt03"]);
                    tDramt04 = Convert.ToDouble(dt.Rows[0]["amt04"]);
                    tCramt04 = Convert.ToDouble(dt.Rows[1]["amt04"]);
                    tDramt05 = Convert.ToDouble(dt.Rows[0]["amt05"]);
                    tCramt05 = Convert.ToDouble(dt.Rows[1]["amt05"]);
                    tDramt06 = Convert.ToDouble(dt.Rows[0]["amt06"]);
                    tCramt06 = Convert.ToDouble(dt.Rows[1]["amt06"]);
                    tDramt07 = Convert.ToDouble(dt.Rows[0]["amt07"]);
                    tCramt07 = Convert.ToDouble(dt.Rows[1]["amt07"]);
                    tDramt08 = Convert.ToDouble(dt.Rows[0]["amt08"]);
                    tCramt08 = Convert.ToDouble(dt.Rows[1]["amt08"]);

                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt")).Text = todramt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt")).Text = tocramt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt01")).Text = tDramt01.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt01")).Text = tCramt01.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt02")).Text = tDramt02.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt02")).Text = tCramt02.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt03")).Text = tDramt03.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt03")).Text = tCramt03.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt04")).Text = tDramt04.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt04")).Text = tCramt04.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt05")).Text = tDramt05.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt05")).Text = tCramt05.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt06")).Text = tDramt06.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt06")).Text = tCramt06.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt07")).Text = tDramt07.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt07")).Text = tCramt07.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotdramt08")).Text = tDramt08.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGprTB.FooterRow.FindControl("lblftotcramt08")).Text = tCramt08.ToString("#,##0;(#,##0); ");
                    break;
                case "IncomeStatement":
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblftotamtIS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totamt)", "")) ? 0.00 : dt.Compute("sum(totamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt01IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt02IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt03IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt04IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt05IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt06IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt07IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    //((Label)this.gvIncomeSt.FooterRow.FindControl("lblfamt08IS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    break;
                case "BalanceDet":
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblftotbamtBD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totbamt)", "")) ? 0.00 : dt.Compute("sum(totbamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblftotliamtBS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totliamt)", "")) ? 0.00 : dt.Compute("sum(totliamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblftotavamtBD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totavamt)", "")) ? 0.00 : dt.Compute("sum(totavamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfbamt01BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt01)", "")) ? 0.00 : dt.Compute("sum(bamt01)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfliamt01BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt01)", "")) ? 0.00 : dt.Compute("sum(liamt01)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfavamt01BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt01)", "")) ? 0.00 : dt.Compute("sum(avamt01)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfbamt02BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt02)", "")) ? 0.00 : dt.Compute("sum(bamt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfliamt02BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt02)", "")) ? 0.00 : dt.Compute("sum(liamt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfavamt02BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt02)", "")) ? 0.00 : dt.Compute("sum(avamt02)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfbamt03BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt03)", "")) ? 0.00 : dt.Compute("sum(bamt03)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfliamt03BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt03)", "")) ? 0.00 : dt.Compute("sum(liamt03)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfavamt03BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt03)", "")) ? 0.00 : dt.Compute("sum(avamt03)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfbamt04BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt04)", "")) ? 0.00 : dt.Compute("sum(bamt04)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfliamt04BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt04)", "")) ? 0.00 : dt.Compute("sum(liamt04)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfavamt04BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt04)", "")) ? 0.00 : dt.Compute("sum(avamt04)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfbamt05BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bamt05)", "")) ? 0.00 : dt.Compute("sum(bamt05)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfliamt05BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(liamt05)", "")) ? 0.00 : dt.Compute("sum(liamt05)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvBankDet.FooterRow.FindControl("lblfavamt05BD")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(avamt05)", "")) ? 0.00 : dt.Compute("sum(avamt05)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "CashFlow":
                    break;

                case "BankBalance":
                    DataTable dt1 = dt.Copy();
                    DataView dv = dt1.DefaultView;
                    dv.RowFilter = ("actcode='BBBBAAAAAAAA'");
                    dt = dv.ToTable();
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblftotamtbb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(totamt)", "")) ? 0.00 : dt.Compute("sum(totamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt01bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt01)", "")) ? 0.00 : dt.Compute("sum(amt01)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt02bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt02)", "")) ? 0.00 : dt.Compute("sum(amt02)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt03bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt03)", "")) ? 0.00 : dt.Compute("sum(amt03)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt04bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt04)", "")) ? 0.00 : dt.Compute("sum(amt04)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt05bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt05)", "")) ? 0.00 : dt.Compute("sum(amt05)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt06bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt06)", "")) ? 0.00 : dt.Compute("sum(amt06)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt07bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt07)", "")) ? 0.00 : dt.Compute("sum(amt07)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvGrpBB.FooterRow.FindControl("lblfamt08bb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt08)", "")) ? 0.00 : dt.Compute("sum(amt08)", ""))).ToString("#,##0;(#,##0); ");

                    break;
            }





        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkall.Checked)
            //{
            //    for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            //    {
            //        ddlComCode.Items[i].Selected = true;

            //    }


            //}

            //else
            //{
            //    for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            //    {
            //        ddlComCode.Items[i].Selected = false;

            //    }

            //}
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "RecAndPayment":

                    this.RptAccRecPay();
                    break;

                case "RecAndPayment01":
                    this.RptAccRecPay01();
                    break;

                case "BankBalance":
                case "Schedule":
                    this.RptBankBalance();
                    break;

                case "TrialBalance":
                    this.RptTrialBalance();
                    break;
                case "IncomeStatement":
                    this.RptIncomeStatement();
                    break;
                case "BalanceSheet":
                    this.RptBalanceSheet();
                    break;
                case "BalanceDet":
                    this.RptBankBalanceDet();
                    break;
                case "CashFlow":
                    this.RptCashFlow();
                    break;

                case "IssueVsCollect":
                    this.RptIssueVsColl();
                    break;
                case "PrjTrialBal":
                    this.PrjTrialBalPrint();
                    break;

            }
        }
        private void RptAccRecPay()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptAccRecPay();
            // ReportDocument subbanst = new RealERPRPT.R_45_GrAcc.RptBankStatus;




            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");


            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }


            //Balance Calculation
            j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtc" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            DataTable dt = (DataTable)ViewState["tblbs"];

            TextObject txttoopnliac = rptstk.ReportDefinition.ReportObjects["txttoopnliac"] as TextObject;
            txttoopnliac.Text = Convert.ToDouble(dt.Rows[0]["totamt"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac1 = rptstk.ReportDefinition.ReportObjects["txtopnliac1"] as TextObject;
            txtopnliac1.Text = Convert.ToDouble(dt.Rows[0]["amt01"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac2 = rptstk.ReportDefinition.ReportObjects["txtopnliac2"] as TextObject;
            txtopnliac2.Text = Convert.ToDouble(dt.Rows[0]["amt02"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac3 = rptstk.ReportDefinition.ReportObjects["txtopnliac3"] as TextObject;
            txtopnliac3.Text = Convert.ToDouble(dt.Rows[0]["amt03"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac4 = rptstk.ReportDefinition.ReportObjects["txtopnliac4"] as TextObject;
            txtopnliac4.Text = Convert.ToDouble(dt.Rows[0]["amt04"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac5 = rptstk.ReportDefinition.ReportObjects["txtopnliac5"] as TextObject;
            txtopnliac5.Text = Convert.ToDouble(dt.Rows[0]["amt05"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac6 = rptstk.ReportDefinition.ReportObjects["txtopnliac6"] as TextObject;
            txtopnliac6.Text = Convert.ToDouble(dt.Rows[0]["amt06"]).ToString("#,##0;(#,##0); ");



            TextObject txttoclslia = rptstk.ReportDefinition.ReportObjects["txttoclslia"] as TextObject;
            txttoclslia.Text = Convert.ToDouble(dt.Rows[1]["totamt"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac1 = rptstk.ReportDefinition.ReportObjects["txtclsliac1"] as TextObject;
            txtclsliac1.Text = Convert.ToDouble(dt.Rows[1]["amt01"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac2 = rptstk.ReportDefinition.ReportObjects["txtclsliac2"] as TextObject;
            txtclsliac2.Text = Convert.ToDouble(dt.Rows[1]["amt02"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac3 = rptstk.ReportDefinition.ReportObjects["txtclsliac3"] as TextObject;
            txtclsliac3.Text = Convert.ToDouble(dt.Rows[1]["amt03"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac4 = rptstk.ReportDefinition.ReportObjects["txtclsliac4"] as TextObject;
            txtclsliac4.Text = Convert.ToDouble(dt.Rows[1]["amt04"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac5 = rptstk.ReportDefinition.ReportObjects["txtclsliac5"] as TextObject;
            txtclsliac5.Text = Convert.ToDouble(dt.Rows[1]["amt05"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac6 = rptstk.ReportDefinition.ReportObjects["txtclsliac6"] as TextObject;
            txtclsliac6.Text = Convert.ToDouble(dt.Rows[1]["amt06"]).ToString("#,##0;(#,##0); ");



            TextObject txttonetlia = rptstk.ReportDefinition.ReportObjects["txttonetlia"] as TextObject;
            txttonetlia.Text = Convert.ToDouble(dt.Rows[2]["totamt"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac1 = rptstk.ReportDefinition.ReportObjects["txtnetliac1"] as TextObject;
            txtnetliac1.Text = Convert.ToDouble(dt.Rows[2]["amt01"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac2 = rptstk.ReportDefinition.ReportObjects["txtnetliac2"] as TextObject;
            txtnetliac2.Text = Convert.ToDouble(dt.Rows[2]["amt02"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac3 = rptstk.ReportDefinition.ReportObjects["txtnetliac3"] as TextObject;
            txtnetliac3.Text = Convert.ToDouble(dt.Rows[2]["amt03"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac4 = rptstk.ReportDefinition.ReportObjects["txtnetliac4"] as TextObject;
            txtnetliac4.Text = Convert.ToDouble(dt.Rows[2]["amt04"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac5 = rptstk.ReportDefinition.ReportObjects["txtnetliac5"] as TextObject;
            txtnetliac5.Text = Convert.ToDouble(dt.Rows[2]["amt05"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac6 = rptstk.ReportDefinition.ReportObjects["txtnetliac6"] as TextObject;
            txtnetliac6.Text = Convert.ToDouble(dt.Rows[2]["amt06"]).ToString("#,##0;(#,##0); ");






            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);




            //
            //j = 1;
            //for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            //{

            //    if (ddlComCode.Items[i].Selected)
            //    {
            //        string header = this.ddlComCode.Items[i].Text.Trim();
            //        TextObject rpttxth = rptstk.Subreports[0].ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
            //        rpttxth.Text = header;
            //        j++;
            //        if (j == 7)
            //            break;
            //    }

            //}
            //rptstk.Subreports[0].SetDataSource((DataTable)ViewState["tblbs"]);

            //string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            // Session["Report1"] = (DataTable)ViewState["tblbs"];
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void RptAccRecPay01()
        {
            string comcod = this.GetCompCode();
            switch (comcod)
            {
                // leisure group
                case "8325":
                case "2325":
                case "3325":
                    this.RptAccRecPay01Rdlc();
                    break;

                default:
                    this.RptAccRecPay01Crystal();
                    break;
            }


        }
        private void RptAccRecPay01Rdlc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = this.GetCompCode();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date1 = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");



            DataSet ds1 = ((DataSet)Session["dstrecpayment"]);
            if (ds1 == null)
                return;

            DataView dv1 = ds1.Tables[0].Copy().DefaultView;
            dv1.RowFilter = ("grp1 = '99' and actcode NOT LIKE '%00000000%'");
            DataTable dt1 = dv1.ToTable();

            //DataView dv2 = ds1.Tables[2].Copy().DefaultView;
            //dv2.RowFilter = ("actcode NOT LIKE '%AAAA%'");
            //DataTable dt2 = dv2.ToTable();

            DataTable dt2 = ds1.Tables[2];

            string comp1 = "";
            string comp2 = "";
            string comp3 = "";

            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    if (i == 0)
                    {
                        comp1 = header;
                    }
                    else if (i == 1)
                    {
                        comp2 = header;

                    }
                    else if (i == 2)
                    {
                        comp3 = header;

                    }
                    else
                    {

                    }

                    j++;
                    if (j == 4)
                        break;
                }

            }



            var list1 = dt1.DataTableToList<RealEntity.C_45_GrAcc.RptGrpMis.RptGrpRecPayment>();
            var list2 = dt2.DataTableToList<RealEntity.C_45_GrAcc.RptGrpMis.RptGrpRecPaymentBank>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_45_GrAcc.RptGrpMisRecPayment", list1, list2, null);
            rpt.EnableExternalImages = true;
            //compname, txtTitle,comAddress
            rpt.SetParameters(new ReportParameter("txtComNam", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Receipt And Payment"));
            rpt.SetParameters(new ReportParameter("comAddress", comadd));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            rpt.SetParameters(new ReportParameter("comp1", comp1));
            rpt.SetParameters(new ReportParameter("comp2", comp2));
            rpt.SetParameters(new ReportParameter("comp3", comp3));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }


        private void RptAccRecPay01Crystal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;

            //string type = this.CompanyRecPayPrint();

            ReportDocument rptstk = rptstk = new RealERPRPT.R_45_GrAcc.RptAccRecPaylei();

            TextObject rptcomname = rptstk.ReportDefinition.ReportObjects["rptcomname"] as TextObject;
            rptcomname.Text = comnam;

            TextObject rptcomadd = rptstk.ReportDefinition.ReportObjects["rptcomadd"] as TextObject;
            rptcomadd.Text = comadd;

            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");


            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }


            DataTable dt = (DataTable)ViewState["tblbs"];



            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            rptstk.Subreports["RptBankStatusLeisure.rpt"].SetDataSource(dt);
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            //Session["Report1"] = rptstk;//(DataTable)ViewState["tblbs"];
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptBankBalance()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptBankBalance();



            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtTitleHeadeer"] as TextObject;
            txtCompany.Text = (this.Request.QueryString["Type"].ToString().Trim() == "BankBalance") ? "Bank Balance Information" : this.ddlAccHead.SelectedItem.Text.Trim();
            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");



            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            // string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptTrialBalance()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptTrialBalance();

            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            // string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptIncomeStatement()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptIncomeSt();

            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            // string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptBalanceSheet()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptBalanceSheet();

            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptBankBalanceDet()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptBankBalanceDet();



            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtTitleHeadeer"] as TextObject;
            //txtCompany.Text = (this.Request.QueryString["Type"].ToString().Trim() == "BankBalance") ? "Bank Balance Information" : this.ddlAccHead.SelectedItem.Text.Trim();
            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "As on Date: " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //string[] arname = { "Bank Amt", "Bank Liabilities", "Available Amt" };

            //for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            //{
            //    if (this.ddlComCode.Items[i].Selected)
            //    {


            //        for (int k = 0; k < 3; k++)
            //        {
            //            this.grvBankDet.Columns[j].HeaderText = this.ddlComCode.Items[i].Text.Trim() + "<br />" + arname[k];
            //            j++;

            //        }
            //    }
            //}


            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {

                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;

                    if (j == 5)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            // string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptCashFlow()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptCashFlow();

            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }


            j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtc" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            DataTable dt = (DataTable)ViewState["tblbs"];

            TextObject txttoopnliac = rptstk.ReportDefinition.ReportObjects["txttoopnliac"] as TextObject;
            txttoopnliac.Text = Convert.ToDouble(dt.Rows[0]["totamt"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac1 = rptstk.ReportDefinition.ReportObjects["txtopnliac1"] as TextObject;
            txtopnliac1.Text = Convert.ToDouble(dt.Rows[0]["amt01"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac2 = rptstk.ReportDefinition.ReportObjects["txtopnliac2"] as TextObject;
            txtopnliac2.Text = Convert.ToDouble(dt.Rows[0]["amt02"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac3 = rptstk.ReportDefinition.ReportObjects["txtopnliac3"] as TextObject;
            txtopnliac3.Text = Convert.ToDouble(dt.Rows[0]["amt03"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac4 = rptstk.ReportDefinition.ReportObjects["txtopnliac4"] as TextObject;
            txtopnliac4.Text = Convert.ToDouble(dt.Rows[0]["amt04"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac5 = rptstk.ReportDefinition.ReportObjects["txtopnliac5"] as TextObject;
            txtopnliac5.Text = Convert.ToDouble(dt.Rows[0]["amt05"]).ToString("#,##0;(#,##0); ");
            TextObject txtopnliac6 = rptstk.ReportDefinition.ReportObjects["txtopnliac6"] as TextObject;
            txtopnliac6.Text = Convert.ToDouble(dt.Rows[0]["amt06"]).ToString("#,##0;(#,##0); ");



            TextObject txttoclslia = rptstk.ReportDefinition.ReportObjects["txttoclslia"] as TextObject;
            txttoclslia.Text = Convert.ToDouble(dt.Rows[1]["totamt"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac1 = rptstk.ReportDefinition.ReportObjects["txtclsliac1"] as TextObject;
            txtclsliac1.Text = Convert.ToDouble(dt.Rows[1]["amt01"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac2 = rptstk.ReportDefinition.ReportObjects["txtclsliac2"] as TextObject;
            txtclsliac2.Text = Convert.ToDouble(dt.Rows[1]["amt02"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac3 = rptstk.ReportDefinition.ReportObjects["txtclsliac3"] as TextObject;
            txtclsliac3.Text = Convert.ToDouble(dt.Rows[1]["amt03"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac4 = rptstk.ReportDefinition.ReportObjects["txtclsliac4"] as TextObject;
            txtclsliac4.Text = Convert.ToDouble(dt.Rows[1]["amt04"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac5 = rptstk.ReportDefinition.ReportObjects["txtclsliac5"] as TextObject;
            txtclsliac5.Text = Convert.ToDouble(dt.Rows[1]["amt05"]).ToString("#,##0;(#,##0); ");
            TextObject txtclsliac6 = rptstk.ReportDefinition.ReportObjects["txtclsliac6"] as TextObject;
            txtclsliac6.Text = Convert.ToDouble(dt.Rows[1]["amt06"]).ToString("#,##0;(#,##0); ");



            TextObject txttonetlia = rptstk.ReportDefinition.ReportObjects["txttonetlia"] as TextObject;
            txttonetlia.Text = Convert.ToDouble(dt.Rows[2]["totamt"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac1 = rptstk.ReportDefinition.ReportObjects["txtnetliac1"] as TextObject;
            txtnetliac1.Text = Convert.ToDouble(dt.Rows[2]["amt01"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac2 = rptstk.ReportDefinition.ReportObjects["txtnetliac2"] as TextObject;
            txtnetliac2.Text = Convert.ToDouble(dt.Rows[2]["amt02"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac3 = rptstk.ReportDefinition.ReportObjects["txtnetliac3"] as TextObject;
            txtnetliac3.Text = Convert.ToDouble(dt.Rows[2]["amt03"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac4 = rptstk.ReportDefinition.ReportObjects["txtnetliac4"] as TextObject;
            txtnetliac4.Text = Convert.ToDouble(dt.Rows[2]["amt04"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac5 = rptstk.ReportDefinition.ReportObjects["txtnetliac5"] as TextObject;
            txtnetliac5.Text = Convert.ToDouble(dt.Rows[2]["amt05"]).ToString("#,##0;(#,##0); ");
            TextObject txtnetliac6 = rptstk.ReportDefinition.ReportObjects["txtnetliac6"] as TextObject;
            txtnetliac6.Text = Convert.ToDouble(dt.Rows[2]["amt06"]).ToString("#,##0;(#,##0); ");


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            // string comcod = this.GetComeCode();
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void RptIssueVsColl()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_45_GrAcc.RptAccIssuevsColl();
            TextObject rptDate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rptDate.Text = "From  " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To  " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");


            int j = 1;
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {

                if (ddlComCode.Items[i].Selected)
                {
                    string header = this.ddlComCode.Items[i].Text.Trim();
                    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtp" + j.ToString()] as TextObject;
                    rpttxth.Text = header;
                    j++;
                    if (j == 7)
                        break;
                }

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            string comcod = this.GetCompCode();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrjTrialBalPrint()
        {
            
               

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt1 = (DataTable)Session["tblrecandpayment"];
            if (dt1.Rows.Count == 0)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_45_GrAcc.RptGrpMis.RptAccRecPayment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_45_GrAcc.RptAccRecPayment", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            for (int i = 0; i < this.ddlComCode.Items.Count; i++)
            {
                if (this.ddlComCode.Items[i].Selected)
                {   
                    Rpt1.SetParameters(new ReportParameter("txtcom"+i,this.ddlComCode.Items[i].Text));
                }
            }
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvGrpRP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label grpcode1 = (Label)e.Row.FindControl("lbgrrpcode");
                // Label actcode = (Label)e.Row.FindControl("lbgraccod");
                HyperLink HLgvDesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label toamt = (Label)e.Row.FindControl("lblgvtotamt");
                Label amt01 = (Label)e.Row.FindControl("lblgvamt01");
                Label amt02 = (Label)e.Row.FindControl("lblgvamt02");
                Label amt03 = (Label)e.Row.FindControl("lblgvamt03");
                Label amt04 = (Label)e.Row.FindControl("lblgvamt04");
                Label amt05 = (Label)e.Row.FindControl("lblgvamt05");
                Label amt06 = (Label)e.Row.FindControl("lblgvamt06");
                Label amt07 = (Label)e.Row.FindControl("lblgvamt07");
                Label amt08 = (Label)e.Row.FindControl("lblgvamt08");

                string grpcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp1")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();

                if (grpcode == "")
                {
                    return;
                }
                if (grpcode == "TT" || grpcode == "UT" || grpcode == "UV" || grpcode == "UW")
                {

                    //  grpcode1.Font.Bold = true;
                    //  actcode.Font.Bold = true;
                    HLgvDesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    HLgvDesc.Style.Add("text-align", "right");

                }
                else if (ASTUtility.Right(actcode, 4) == "0000")
                {

                    HLgvDesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    // HLgvDesc.Style.Add("text-align", "left");

                }

            }
        }

        protected void gvIncomeSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lblgvaccdescIS");
                Label toamt = (Label)e.Row.FindControl("lblgvtotamtIS");
                Label amt01 = (Label)e.Row.FindControl("lblgvamt01IS");
                Label amt02 = (Label)e.Row.FindControl("lblgvamt02IS");
                Label amt03 = (Label)e.Row.FindControl("lblgvamt03IS");
                Label amt04 = (Label)e.Row.FindControl("lblgvamt04IS");
                Label amt05 = (Label)e.Row.FindControl("lblgvamt05IS");
                Label amt06 = (Label)e.Row.FindControl("lblgvamt06IS");
                Label amt07 = (Label)e.Row.FindControl("lblgvamt07IS");
                Label amt08 = (Label)e.Row.FindControl("lblgvamt08IS");

                string grpcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (grpcode == "")
                {
                    return;
                }
                string code = ASTUtility.Right(grpcode, 4);
                if (code == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;


                }

            }
        }
        protected void gvBalSheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label actdesc = (Label)e.Row.FindControl("lblgvaccdescBS");
                Label toamt = (Label)e.Row.FindControl("lblgvtotamtBS");
                LinkButton amt01 = (LinkButton)e.Row.FindControl("lnkbtngvamt01BS");
                Label amt02 = (Label)e.Row.FindControl("lblgvamt02BS");
                Label amt03 = (Label)e.Row.FindControl("lblgvamt03BS");
                Label amt04 = (Label)e.Row.FindControl("lblgvamt04BS");
                Label amt05 = (Label)e.Row.FindControl("lblgvamt05BS");
                Label amt06 = (Label)e.Row.FindControl("lblgvamt06BS");
                Label amt07 = (Label)e.Row.FindControl("lblgvamt07BS");
                Label amt08 = (Label)e.Row.FindControl("lblgvamt08BS");

                string grpcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (grpcode == "")
                {
                    return;
                }
                //&& chkListGroup.Items[0].Selected && chkListGroup.Items[3].Selected
                string code = ASTUtility.Right(grpcode, 4);
                if (code == "0000" && chkListGroup.SelectedValue == "4")
                {
                    actdesc.Attributes["style"] = "color:maroon;font-weight:bold;";
                    toamt.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt01.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt02.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt03.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt04.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt05.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt06.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt07.Attributes["style"] = "color:maroon;font-weight:bold;";
                    amt08.Attributes["style"] = "color:maroon;font-weight:bold;";
                }

                if (grpcode == "1BAAAAAAAAAA" || grpcode == "B9AAAAAAAAAA")
                {
                    actdesc.Attributes["style"] = "color:green;font-weight:bold;";
                    toamt.Attributes["style"] = "color:green;font-weight:bold;";
                    amt01.Attributes["style"] = "color:green;font-weight:bold;";
                    amt02.Attributes["style"] = "color:green;font-weight:bold;";
                    amt03.Attributes["style"] = "color:green;font-weight:bold;";
                    amt04.Attributes["style"] = "color:green;font-weight:bold;";
                    amt05.Attributes["style"] = "color:green;font-weight:bold;";
                    amt06.Attributes["style"] = "color:green;font-weight:bold;";
                    amt07.Attributes["style"] = "color:green;font-weight:bold;";
                    amt08.Attributes["style"] = "color:green;font-weight:bold;";
                }

            }
        }
        protected void grvCashFlow_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lblgvDesc");
                Label toamt = (Label)e.Row.FindControl("lblgvtotamtCF");
                Label amt01 = (Label)e.Row.FindControl("lblgvamt01CF");
                Label amt02 = (Label)e.Row.FindControl("lblgvamt02CF");
                Label amt03 = (Label)e.Row.FindControl("lblgvamt03CF");
                Label amt04 = (Label)e.Row.FindControl("lblgvamt04CF");
                Label amt05 = (Label)e.Row.FindControl("lblgvamt05CF");
                Label amt06 = (Label)e.Row.FindControl("lblgvamt06CF");
                Label amt07 = (Label)e.Row.FindControl("lblgvamt07CF");
                Label amt08 = (Label)e.Row.FindControl("lblgvamt08CF");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    udesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");


                }
                if (ASTUtility.Right(code, 4) == "XXXX")
                {

                    udesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    udesc.Style.Add("color", "red");
                    toamt.Style.Add("color", "red");
                    amt01.Style.Add("color", "red");
                    amt02.Style.Add("color", "red");
                    amt03.Style.Add("color", "red");
                    amt04.Style.Add("color", "red");
                    amt05.Style.Add("color", "red");
                    amt06.Style.Add("color", "red");
                    amt07.Style.Add("color", "red");
                    amt08.Style.Add("color", "red");


                }


            }
        }
        protected void gvGrpBB_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvaccdesc");
                Label totamt = (Label)e.Row.FindControl("lblgvtotamtbb");
                Label amt01 = (Label)e.Row.FindControl("lblgvamt01bb");
                Label amt02 = (Label)e.Row.FindControl("lblgvamt02bb");
                Label amt03 = (Label)e.Row.FindControl("lblgvamt03bb");
                Label amt04 = (Label)e.Row.FindControl("lblgvamt04bb");
                Label amt05 = (Label)e.Row.FindControl("lblgvamt05bb");
                Label amt06 = (Label)e.Row.FindControl("lblgvamt06bb");
                Label amt07 = (Label)e.Row.FindControl("lblgvamt07bb");
                Label amt08 = (Label)e.Row.FindControl("lblgvamt08bb");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    totamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");


                }

            }
        }

        protected void gvGrpIVsC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label grpcode1 = (Label)e.Row.FindControl("lbgrrpcode");
                // Label actcode = (Label)e.Row.FindControl("lbgraccod");
                HyperLink HLgvDesc = (HyperLink)e.Row.FindControl("HLgvDescIVsC");
                Label toamt = (Label)e.Row.FindControl("lblgvtotamtIVsC");
                Label amt01 = (Label)e.Row.FindControl("lblgvamt01IVsC");
                Label amt02 = (Label)e.Row.FindControl("lblgvamt02IVsC");
                Label amt03 = (Label)e.Row.FindControl("lblgvamt03IVsC");
                Label amt04 = (Label)e.Row.FindControl("lblgvamt04IVsC");
                Label amt05 = (Label)e.Row.FindControl("lblgvamt05IVsC");
                Label amt06 = (Label)e.Row.FindControl("lblgvamt06IVsC");
                Label amt07 = (Label)e.Row.FindControl("lblgvamt07IVsC");
                Label amt08 = (Label)e.Row.FindControl("lblgvamt08IVsC");

                string grpcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp1")).ToString();
                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode1")).ToString();

                if (grpcode == "")
                {
                    return;
                }
                if (grpcode == "TT" || grpcode == "UT" || grpcode == "UV" || grpcode == "UW")
                {

                    //  grpcode1.Font.Bold = true;
                    //  actcode.Font.Bold = true;
                    HLgvDesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    HLgvDesc.Style.Add("text-align", "right");

                }
                else if (ASTUtility.Right(actcode, 4) == "0000")
                {

                    HLgvDesc.Font.Bold = true;
                    toamt.Font.Bold = true;
                    amt01.Font.Bold = true;
                    amt02.Font.Bold = true;
                    amt03.Font.Bold = true;
                    amt04.Font.Bold = true;
                    amt05.Font.Bold = true;
                    amt06.Font.Bold = true;
                    amt07.Font.Bold = true;
                    amt08.Font.Bold = true;
                    // HLgvDesc.Style.Add("text-align", "left");

                }

            }
        }
        protected void chkConsolidate_CheckedChanged(object sender, EventArgs e)
        {
            this.CallCompanyList();
            //this.chkall.Checked = false;
            this.chkall_CheckedChanged(null, null);

        }

        protected void gvGprTB_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;





            Label lblgvaccdesc = (Label)e.Row.FindControl("lblgvaccdesc");
            Label lblgvtotamtbb = (Label)e.Row.FindControl("lblgvtotamtbb");
            Label lblgvamt01bb = (Label)e.Row.FindControl("lblgvamt01bb");
            Label lblgvamt02bb = (Label)e.Row.FindControl("lblgvamt02bb");
            Label lblgvamt03bb = (Label)e.Row.FindControl("lblgvamt03bb");
            Label lblgvamt04bb = (Label)e.Row.FindControl("lblgvamt04bb");
            Label lblgvamt05bb = (Label)e.Row.FindControl("lblgvamt05bb");
            Label lblgvamt06bb = (Label)e.Row.FindControl("lblgvamt06bb");
            Label lblgvamt07bb = (Label)e.Row.FindControl("lblgvamt07bb");
            Label lblgvamt08bb = (Label)e.Row.FindControl("lblgvamt08bb");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString().Trim();
            // string lebel2 = Convert.ToString (DataBinder.Eval (e.Row.DataItem, "leb2")).ToString ().Trim ();


            if (code == "")
            {
                return;
            }


            // && chkListGroup.Items[0].Selected && chkListGroup.Items[3].Selected
            if (ASTUtility.Right(code, 4) == "0000" && chkListGroup.SelectedValue == "4")
            {
                lblgvaccdesc.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvtotamtbb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt01bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt02bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt03bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt04bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt05bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt06bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt07bb.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblgvamt08bb.Attributes["style"] = "color:maroon;font-weight:bold;";



            }

            if (ASTUtility.Right(code, 10) == "0000000000" && chkListGroup.SelectedValue == "2" && chkListGroup.SelectedValue == "3")
            {
                lblgvaccdesc.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvtotamtbb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt01bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt02bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt03bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt04bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt05bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt06bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt07bb.Attributes["style"] = "color:green;font-weight:bold;";
                lblgvamt08bb.Attributes["style"] = "color:green;font-weight:bold;";

            }

        }


        protected void gvbankstaus2_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label grpcode1 = (Label)e.Row.FindControl("lbgrrpcode");
                // Label actcode = (Label)e.Row.FindControl("lbgraccod");

                Label actdesc = (Label)e.Row.FindControl("lblgvactdesc");
                Label change = (Label)e.Row.FindControl("lblgvchange");
                Label open = (Label)e.Row.FindControl("lblgvopen");
                Label close = (Label)e.Row.FindControl("lblgvclose");



                string actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();


                if (actcode == "")
                {
                    return;
                }
                if (actcode.Substring(8) == "AAAA")
                {

                    //  grpcode1.Font.Bold = true;
                    //  actcode.Font.Bold = true;
                    actdesc.Font.Bold = true;
                    change.Font.Bold = true;
                    open.Font.Bold = true;
                    close.Font.Bold = true;
                }



            }
        }


        protected void gvPrjtrbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label DAmount = (Label)e.Row.FindControl("lgvAmt");
                Label CAmount = (Label)e.Row.FindControl("lgvCre");

               
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
                else if (code == "000000000001" || (ASTUtility.Right((code), 3) == "000"))
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
                //string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode1")).ToString();
                //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
                //string Actcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString(); ;
                //string Date1 = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyy");
                //string rescode = ((Label)e.Row.FindControl("lblgvCode")).Text;
                //if (ASTUtility.Left(rescode1, 2) == "51")
                //{
                //    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + Actcode + "&Date1=" + Date1;
                //}

                //else if (ASTUtility.Right((code), 3) != "000" && code != "000000000001" && code != "999999999999" && code != "000000000002")
                //{
                //    hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=SpLedger&pactcode=" + Actcode + "&Date1=" + Date1 + "&rescode=" + rescode;
                //}




            }


        }

        protected void gvPrjtrbal_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
               
                
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell1 = new TableCell();
                cell1.Text = "";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                gvrow.Cells.Add(cell1);

                TableCell cell8 = new TableCell();
                cell8.Text = "";
                cell8.HorizontalAlign = HorizontalAlign.Center;
                cell8.ColumnSpan = 1;
                gvrow.Cells.Add(cell8);

                TableCell cell9 = new TableCell();
                cell9.Text = "";
                cell9.HorizontalAlign = HorizontalAlign.Center;
                cell9.ColumnSpan = 1;
                gvrow.Cells.Add(cell9);

                

                TableCell cell2 = new TableCell();

                for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                {
                    if (this.ddlComCode.Items[i].Selected)
                    {
                        cell2.Text = this.ddlComCode.Items[0].Text.Trim();
                        



                        i++;
                    }
                }

               

                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 2;
                gvrow.Cells.Add(cell2);



                //TableCell cell3 = new TableCell();
                //cell3.Text = "Dr.Amount";
                //cell3.HorizontalAlign = HorizontalAlign.Center;
                //cell3.ColumnSpan = 1;
                //cell3.Font.Bold = true;
                //gvrow.Cells.Add(cell3);


                //TableCell cell4 = new TableCell();
                //cell4.Text = "Cr. Amount";
                //cell4.HorizontalAlign = HorizontalAlign.Center;
                //cell4.ColumnSpan = 1;
                //cell4.Font.Bold = true;
                //gvrow.Cells.Add(cell4);




                TableCell cell5 = new TableCell();

                for (int i = 0; i < this.ddlComCode.Items.Count; i++)
                {
                    if (this.ddlComCode.Items[i].Selected)
                    {
                        cell5.Text = this.ddlComCode.Items[1].Text.Trim();
                        
                       i++;
                    }
                }
             
                cell5.HorizontalAlign = HorizontalAlign.Center;
                cell5.ColumnSpan = 2;
                cell5.Font.Bold = true;
                gvrow.Cells.Add(cell5);

                //TableCell cell6 = new TableCell();
                //cell6.Text = "Dr.Amount";
                //cell6.HorizontalAlign = HorizontalAlign.Center;
                //cell6.ColumnSpan = 1;
                //cell6.Font.Bold = true;
                //gvrow.Cells.Add(cell6);

                //TableCell cell7 = new TableCell();
                //cell7.Text = "Cr.Amount";
                //cell7.HorizontalAlign = HorizontalAlign.Center;
                //cell7.ColumnSpan = 1;
                //cell7.Font.Bold = true;
                //gvrow.Cells.Add(cell7);




                gvPrjtrbal.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

    }
}