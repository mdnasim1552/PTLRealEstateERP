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
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpMisDailyActivities : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        Common ObjCommon = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "ChequeInHand") ? "Cheque In Hand" : (type == "RecPay") ? "Receipt &  payment Information"
                    : (type == "BankPosition") ? "Bank Position Information"
                    : (type == "MasPVsMonPVsExAllPro") ? "MASTER PLAN, MONTHLY PLAN Vs. ACHIEVEMENT- ALL PROJECT"

                    : (type == "TarVsAch") ? "Target Vs Acheivement"
                    : (type == "SoldUnsold") ? "Sold  & Unsold Infromation"
                    : (type == "PDCStatus") ? "PDC Issue Status"
                    : (type == "FeaAllProject") ? "Feasibility Report-All Project(Summary)"
                    : (type == "GPNPALLPRO") ? "GP & NP All Projeect "
                    : (type == "PrjStatus") ? "Project Status"
                    : (type == "MProStatus") ? "Month Wise Project Status"
                    : (type == "PDCSummary") ? "PDC Summary Status"
                    : (type == "RptDayWSale") ? "Day Wise Sales" : (type == "CollSummary") ? "Collection Summary" : "Month Wise Payment-Summary";
                this.SelectView();


            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChequeInHand":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.ShowCollDetails();
                    break;

                case "RecPay":

                    this.MultiView1.ActiveViewIndex = 1;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ReceiptAndPayment();
                    break;

                case "IssuedVsCollect":

                    this.MultiView1.ActiveViewIndex = 6;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowIssuedVsColl();
                    break;

                case "BankPosition":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowBankPosition();
                    break;

                case "MasPVsMonPVsExAllPro":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowMMonPlnVsAchAllPro();
                    break;


                case "TarVsAch":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.gvSalVsColl.Columns[3].Visible = (this.Request.QueryString["Group"] == "Sales");
                    this.gvSalVsColl.Columns[4].Visible = (this.Request.QueryString["Group"] == "Sales");
                    this.gvSalVsColl.Columns[5].Visible = (this.Request.QueryString["Group"] == "Sales");
                    this.gvSalVsColl.Columns[6].Visible = (this.Request.QueryString["Group"] == "Sales");
                    this.gvSalVsColl.Columns[7].Visible = (this.Request.QueryString["Group"] == "Sales");
                    this.gvSalVsColl.Columns[8].Visible = (this.Request.QueryString["Group"] == "Sales");
                    this.gvSalVsColl.Columns[9].Visible = (this.Request.QueryString["Group"] == "Collection");
                    this.gvSalVsColl.Columns[10].Visible = (this.Request.QueryString["Group"] == "Collection");
                    this.gvSalVsColl.Columns[11].Visible = (this.Request.QueryString["Group"] == "Collection");
                    this.gvSalVsColl.Columns[12].Visible = (this.Request.QueryString["Group"] == "Collection");
                    this.gvSalVsColl.Columns[13].Visible = (this.Request.QueryString["Group"] == "Collection");
                    this.gvSalVsColl.Columns[14].Visible = (this.Request.QueryString["Group"] == "Collection");


                    this.ShowTarVsAchievement();
                    break;

                case "SoldUnsold":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.ShowSoldUnsold();
                    break;

                case "PDCStatus":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.ShowPDCIssuest();
                    break;

                case "FeaAllProject":
                    this.lblAsDate.Text = " As On " + this.Request.QueryString["date"].ToString();
                    this.MultiView1.ActiveViewIndex = 8;
                    this.ShowFeaAllProject();
                    break;

                case "GPNPALLPRO":
                    this.lblAsDate.Text = " As On " + this.Request.QueryString["date"].ToString();
                    this.MultiView1.ActiveViewIndex = 9;
                    this.ShowAllProGPNP();
                    break;

                case "PrjStatus":
                    this.lblAsDate.Text = " As On " + this.Request.QueryString["date"].ToString();
                    this.MultiView1.ActiveViewIndex = 10;
                    this.ShowPrjStatus();
                    break;

                case "MProStatus":
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.MultiView1.ActiveViewIndex = 11;
                    this.ShowMonProStatus();
                    break;

                case "PDCSummary":
                    this.MultiView1.ActiveViewIndex = 12;
                    this.lblPage.Visible = false;
                    this.ddlpagesize.Visible = false;
                    this.lblDateRange.Visible = false;
                    this.lblAsDate.Visible = false;
                    this.lblAsDate.Text = "As On " + this.Request.QueryString["Date2"].ToString();
                    this.ShowPDCSummary();
                    break;


                case "RptDayWSale":
                    this.MultiView1.ActiveViewIndex = 13;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.salesStatus();
                    break;


                case "CollSummary":
                    this.MultiView1.ActiveViewIndex = 14;
                    this.lblAsDate.Text = "(From " + this.Request.QueryString["Date1"].ToString() + " To " + this.Request.QueryString["Date2"].ToString() + ")";
                    this.CollectionSummary();
                    break;





            }
        }


        private void ShowCollDetails()
        {
            Session.Remove("tblcollvscl");

            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTCHEQUEINHAND", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCollDet.DataSource = null;
                this.gvCollDet.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();


        }

        private void ReceiptAndPayment()
        {
            Session.Remove("tblcollvscl");

            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string rp = "RP";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "RP_COMPANY_04", frmdate, todate, rp, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblcollvscl"] = ds1.Tables[0];
            Session["recandpayFo"] = ds1.Tables[1];
            Session["recandpayNote"] = ds1.Tables[2];

            this.gvrecandpay.DataSource = ds1.Tables[0];
            this.gvrecandpay.DataBind();
            this.RPNote();

            for (int i = 0; i < gvrecandpay.Rows.Count; i++)
            {
                string recpcode = ((Label)gvrecandpay.Rows[i].FindControl("lblgvrecpcode")).Text.Trim();
                string paycode = ((Label)gvrecandpay.Rows[i].FindControl("lblgvpaycode")).Text.Trim();
                LinkButton lbtn1 = (LinkButton)gvrecandpay.Rows[i].FindControl("btnRecDesc");
                LinkButton lbtn2 = (LinkButton)gvrecandpay.Rows[i].FindControl("btnPayDesc");
                if (lbtn1 != null)
                {
                    if (lbtn1.Text.Trim().Length > 0)
                        lbtn1.CommandArgument = recpcode;
                }
                if (lbtn2 != null)
                {
                    if (lbtn2.Text.Trim().Length > 0)
                        lbtn2.CommandArgument = paycode;
                }
            }

            this.FooterCalculation();
            ds1.Dispose();
            Session["Report1"] = gvrecandpay;
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((HyperLink)this.gvrecandpay.HeaderRow.FindControl("hlbtnRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).NavigateUrl = "~/F_45_GrAcc/LinkGrpAccount.aspx?Type=BalConfirmation&comcod=" + comcod + "&Date1=" + frmdate + "&Date2=" + todate;

            }
        }


        private void ShowIssuedVsColl()
        {

            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "ISSUEDVSCOLLECTION", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblcollvscl"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();




        }
        private void ShowTarVsAchievement()
        {
            try
            {
                Session.Remove("tblcollvscl");
                string comcod = this.Request.QueryString["comcod"].ToString();
                string frmdate = this.Request.QueryString["Date1"].ToString();
                string todate = this.Request.QueryString["Date2"].ToString();
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWISESALVSCOLTAR", frmdate, todate, "", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvSalVsColl.DataSource = null;
                    this.gvSalVsColl.DataBind();
                    return;
                }
                Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
                this.Data_Bind();
                ds1.Dispose();




            }

            catch (Exception ex)
            {


            }




        }





        private void RPNote()
        {
            this.PanelNote.Visible = true;
            DataTable dt = (DataTable)Session["recandpayNote"];
            this.gvbankbal.DataSource = dt;
            this.gvbankbal.DataBind();


            //this.lblPaid.Text = Convert.ToDouble(dt.Rows[0]["payamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblInPaid.Text = Convert.ToDouble(dt.Rows[0]["ipayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblSodPaid.Text = Convert.ToDouble(dt.Rows[0]["sodpayamt"]).ToString("#,##0;(#,##0) ;");
            //this.lblTPaid.Text = Convert.ToDouble(dt.Rows[0]["tpayamt"]).ToString("#,##0;(#,##0) ;");

            //this.lblNet.Text = Convert.ToDouble(dt.Rows[0]["netamt"]).ToString("#,##0;(#,##0) ;");

        }

        private void ShowBankPosition()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTBANKPOSITION", frmdate, todate, "12", "", "", "", "", "", "");
            Session["tblcollvscl"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();




        }


        private void ShowMMonPlnVsAchAllPro()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMASPVSMONPVSEX", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMMPlanVsAch.DataSource = null;
                this.gvMMPlanVsAch.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowSoldUnsold()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "RPTDATEWALLPROINSDUES", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.dgvAccRec03.DataSource = null;
                this.dgvAccRec03.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }

        private void ShowPDCIssuest()
        {

            try
            {
                string comcod = this.Request.QueryString["comcod"].ToString();

                string date = this.Request.QueryString["Date2"].ToString();
                string grp = (this.Request.QueryString["grp"].ToString() == "4") ? "4" : "";
                string acorescode = ASTUtility.Left(this.Request.QueryString["actcode"].ToString(), 2) + "%";
                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_LINKMIS", "RPTGROUPWISEPDCHEQUE", "", date, grp, acorescode, "", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvgrpchqissued.DataSource = null;
                    this.gvgrpchqissued.DataBind();
                    return;
                }
                Session["tblcollvscl"] = ds1.Tables[0];
                this.Data_Bind();



            }
            catch (Exception ex)
            {

            }




        }

        private void ShowFeaAllProject()
        {

            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY", "INSTATALLPRJSUM", date, "consolidate", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvFeaAllPro.DataSource = null;
                this.gvFeaAllPro.DataBind();

                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }


        private void ShowAllProGPNP()
        {


            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            DataSet ds2 = AccData.GetTransInfo(comcod, "SP_REPORT_FEA_PROFEASIBILITY_04", "RPTALLPROCOSTASALE", date, "consolidate", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvgpnp.DataSource = null;
                this.gvgpnp.DataBind();
                return;
            }
            Session["tblcollvscl"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();


        }

        private void ShowPrjStatus()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string date = this.Request.QueryString["date"].ToString();
            string consolidate = "consolidate";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTPROJECTSTATUS", date, consolidate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.grvPrjStatus.DataSource = null;
                this.grvPrjStatus.DataBind();
                return;

            }
            ViewState["tblinterest"] = ds1.Tables[1];
            Session["tblcollvscl"] = HiddenSameData(ds1.Tables[0]);
            this.grvPrjStatus.Columns[10].HeaderText = "Month Of " + Convert.ToDateTime(this.Request.QueryString["date"].ToString()).ToString("MMM yy");
            this.Data_Bind();
        }


        private void ShowMonProStatus()
        {
            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MIS", "RPTMONPROSTATUS", frmdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvMonPorStatus.DataSource = null;
                this.gvMonPorStatus.DataBind();
                return;

            }

            Session["tblcollvscl"] = ds1.Tables[0];
            ViewState["tblresdesc"] = ds1.Tables[1];
            ds1.Dispose();
            this.Data_Bind();
        }

        private void ShowPDCSummary()
        {

            Session.Remove("tblcollvscl");
            string comcod = this.Request.QueryString["comcod"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_GROUP_MIS02", "RPPDCSUMMARY", "", todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvpdc.DataSource = null;
                this.gvpdc.DataBind();
                return;

            }

            Session["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            ds1.Dispose();
            this.Data_Bind();



        }

        private void salesStatus()
        {
            Session.Remove("tblcollvscl");


            string comcod = this.Request.QueryString["comcod"].ToString();
            string PactCode = "%";
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();
            string mRptGroup = "12";
            string steam = "%";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", PactCode, frmdate, todate, mRptGroup, steam, "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }



            Session["tblcollvscl"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void CollectionSummary()
        {

            try
            {
                Session.Remove("tblcollvscl");

                string comcod = this.Request.QueryString["comcod"].ToString();
                string frmdate = this.Request.QueryString["Date1"].ToString();
                string todate = this.Request.QueryString["Date2"].ToString();
                string withrep = "";

                DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_SUM", "RPTDWCOLLECTSTATUS", frmdate, todate, withrep, "", "", "", "", "", "");

                if (ds1.Tables[0].Rows.Count == 0)
                {

                    this.gvrcoll.DataSource = null;
                    this.gvrcoll.DataBind();
                    return;
                }
                Session["tblcollvscl"] = ds1.Tables[0];
                this.gvrcoll.DataSource = ds1.Tables[0];
                this.gvrcoll.DataBind();


            }

            catch (Exception ex)
            {


            }


        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {



                case "ChequeInHand":
                case "MasPVsMonPVsExAllPro":
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                            grp = dt1.Rows[j]["grp"].ToString();
                    }

                    break;





                case "SoldUnsold":
                    string grpdesc = dt1.Rows[0]["grpdesc"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grpdesc"].ToString() == grpdesc)
                        {
                            grpdesc = dt1.Rows[j]["grpdesc"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        else
                        {
                            grpdesc = dt1.Rows[j]["grpdesc"].ToString();
                        }

                    }

                    break;


                case "FeaAllProject":
                    string company = dt1.Rows[0]["company"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                        {
                            company = dt1.Rows[j]["company"].ToString();
                            dt1.Rows[j]["companydesc"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companydesc"] = "";
                            company = dt1.Rows[j]["company"].ToString();
                        }
                    }
                    break;


                case "GPNPALLPRO":
                    string pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {
                            dt1.Rows[j]["pactdesc1"] = "";
                        }


                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();
                    }
                    break;


                case "PrjStatus":

                    string grpps = dt1.Rows[0]["grp"].ToString();
                    pactcode1 = dt1.Rows[0]["pactcode1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grpps && dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                        {

                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["pactdesc1"] = "";
                        }

                        else
                        {
                            if (dt1.Rows[j]["grp"].ToString() == grpps)
                                dt1.Rows[j]["grpdesc"] = "";

                            if (dt1.Rows[j]["pactcode1"].ToString() == pactcode1)
                                dt1.Rows[j]["pactdesc1"] = "";
                        }

                        grpps = dt1.Rows[j]["grp"].ToString();
                        pactcode1 = dt1.Rows[j]["pactcode1"].ToString();

                    }
                    break;

                case "PDCSummary":

                    string pgrp = dt1.Rows[0]["pgrp"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pgrp"].ToString() == pgrp)
                            dt1.Rows[j]["pgrpdesc"] = "";
                        pgrp = dt1.Rows[j]["pgrp"].ToString();
                    }
                    break;

                case "RptDayWSale":
                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                        }

                    }
                    break;


                case "TarVsAch":
                    string mdeptcode = dt1.Rows[0]["mdeptcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["mdeptcode"].ToString() == mdeptcode)
                            dt1.Rows[j]["mdeptname"] = "";

                        mdeptcode = dt1.Rows[j]["mdeptcode"].ToString();

                    }
                    break;

            }



            return dt1;

        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "ChequeInHand":
                    this.gvCollDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvCollDet.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvCollDet.DataBind();
                    this.FooterCalculation();
                    break;

                case "BankPosition":
                    this.gvBankPosition.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvBankPosition.DataBind();
                    Session["Report1"] = gvBankPosition;
                    ((HyperLink)this.gvBankPosition.HeaderRow.FindControl("hlbtnbnkpdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "MasPVsMonPVsExAllPro":

                    this.gvMMPlanVsAch.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvMMPlanVsAch.DataBind();
                    this.FooterCalculation();
                    break;
                case "TarVsAch":
                    this.gvSalVsColl.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvSalVsColl.DataBind();
                    break;

                case "SoldUnsold":
                    this.dgvAccRec03.Columns[17].HeaderText = "Dues Up to " + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("MMM- yyyy");
                    this.dgvAccRec03.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.dgvAccRec03.DataSource = (DataTable)Session["tblcollvscl"];
                    this.dgvAccRec03.DataBind();
                    this.FooterCalculation();
                    break;

                case "IssuedVsCollect":
                    this.gvarecandpay.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvarecandpay.DataBind();
                    this.FooterCalculation();
                    Session["Report1"] = this.gvarecandpay;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvarecandpay.HeaderRow.FindControl("hlbtnacRcvPayCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "PDCStatus":
                    this.gvgrpchqissued.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvgrpchqissued.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvgrpchqissued.DataBind();
                    Session["Report1"] = gvgrpchqissued;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvgrpchqissued.HeaderRow.FindControl("hlbtnbtbCdataExelgp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;


                case "FeaAllProject":

                    this.gvFeaAllPro.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvFeaAllPro.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvFeaAllPro.DataBind();
                    break;


                case "GPNPALLPRO":
                    this.gvgpnp.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvgpnp.DataBind();
                    break;

                case "PrjStatus":
                    this.grvPrjStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvPrjStatus.DataSource = (DataTable)Session["tblcollvscl"];

                    this.grvPrjStatus.DataBind();
                    this.FooterCalculation();
                    Session["Report1"] = grvPrjStatus;
                    ((HyperLink)this.grvPrjStatus.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                case "MProStatus":
                    DataTable dtpname = (DataTable)ViewState["tblresdesc"];
                    int j = 2;
                    for (int i = 0; i < dtpname.Rows.Count; i++)
                    {

                        this.gvMonPorStatus.Columns[j].HeaderText = dtpname.Rows[i]["resdesc"].ToString();
                        j++;
                        if (j == 16)
                            break;


                    }

                    this.gvMonPorStatus.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvMonPorStatus.DataBind();
                    Session["Report1"] = gvMonPorStatus;
                    if (((DataTable)Session["tblcollvscl"]).Rows.Count > 0)
                        ((HyperLink)this.gvMonPorStatus.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    this.FooterCalculation();
                    break;


                case "PDCSummary":

                    this.gvpdc.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvpdc.DataBind();
                    break;

                case "RptDayWSale":
                    this.gvDayWSale.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDayWSale.DataSource = (DataTable)Session["tblcollvscl"];
                    this.gvDayWSale.DataBind();
                    this.FooterCalculation();
                    break;


            }

        }



        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt4;
            DataView dv1;
            switch (type)
            {
                case "ChequeInHand":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("grp='C' and usircode='BBBBAAAAAAAA' ");
                    dt4 = dv1.ToTable();
                    double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                    double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.gvCollDet.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");
                    break;



                case "RecPay":
                    dt1 = (DataTable)Session["recandpayFo"];

                    //dv1=dt.Copy().DefaultView;
                    //dv1.RowFilter = ("recpcode like '%00000000%' or paycode like '%00000000%'");
                    //dt1 = dv1.ToTable();
                    double frecamt = 0, fpayamt1 = 0, netbal;

                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    ((Label)this.gvrecandpay.FooterRow.FindControl("lblgvFrecpam")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    ((Label)this.gvrecandpay.FooterRow.FindControl("lgvFpayam1")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    ((HyperLink)this.gvrecandpay.FooterRow.FindControl("lgvFNetBalance")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;

                case "MasPVsMonPVsExAllPro":

                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFmasPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(masplan)", "")) ? 0.00 :
                        dt1.Compute("sum(masplan)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFmonPlan")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(monplan)", "")) ? 0.00 :
                        dt1.Compute("sum(monplan)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMMPlanVsAch.FooterRow.FindControl("lgvFExecutionpAC")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(excution)", "")) ? 0.00 :
                       dt1.Compute("sum(excution)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "SoldUnsold":
                    dt4 = dt1.Copy();
                    DataView dv = dt1.DefaultView;
                    dv.RowFilter = ("pactcode not like '%AAAA%'");
                    dt4 = dv.ToTable();

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtstkamal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(tstkam)", "")) ?
                     0.00 : dt4.Compute("Sum(tstkam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFususizeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ususize)", "")) ?
                       0.00 : dt4.Compute("Sum(ususize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFusuamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(usamt)", "")) ?
                       0.00 : dt4.Compute("Sum(usamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFusizeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(usize)", "")) ?
                          0.00 : dt4.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFaptcostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(aptcost)", "")) ?
                     0.00 : dt4.Compute("Sum(aptcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFcpaocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cpaocost)", "")) ?
                    0.00 : dt4.Compute("Sum(cpaocost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(tocost)", "")) ?
                    0.00 : dt4.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFatoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(atodues)", "")) ?
                     0.00 : dt4.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtotalduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(todues)", "")) ?
                    0.00 : dt4.Compute("Sum(todues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgFEncashal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(reconamt)", "")) ?
                    0.00 : dt4.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtretamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(retcheque)", "")) ?
                    0.00 : dt4.Compute("Sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtframtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(fcheque)", "")) ?
                    0.00 : dt4.Compute("Sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtpdamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(pcheque)", "")) ?
                    0.00 : dt4.Compute("Sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoreceivedal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ramt)", "")) ?
                    0.00 : dt4.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(bamt)", "")) ?
                    0.00 : dt4.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(pbookam)", "")) ?
                    0.00 : dt4.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(pinsam)", "")) ?
                0.00 : dt4.Compute("Sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cbookam)", "")) ?
                    0.00 : dt4.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cinsam)", "")) ?
                    0.00 : dt4.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoCInstalmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ctodues)", "")) ?
                0.00 : dt4.Compute("Sum(ctodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFvbaamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(vbamt)", "")) ?
                            0.00 : dt4.Compute("Sum(vbamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdelchargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(cdelay)", "")) ?
                0.00 : dt4.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdischargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(discharge)", "")) ?
                0.00 : dt4.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFnettoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("Sum(ntodues)", "")) ?
               0.00 : dt4.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");


                    break;


                case "IssuedVsCollect":
                    frecamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(recpam)", "")) ?
                           0 : dt1.Compute("sum(recpam)", "")));
                    ((Label)this.gvarecandpay.FooterRow.FindControl("lblgvFrecpamac")).Text = frecamt.ToString("#,##0;(#,##0) ;");
                    fpayamt1 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(payam)", "")) ?
                         0 : dt1.Compute("sum(payam)", "")));

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFpayamac")).Text = fpayamt1.ToString("#,##0;(#,##0) ;");
                    netbal = frecamt - fpayamt1;

                    ((Label)this.gvarecandpay.FooterRow.FindControl("lgvFNetBalanceac")).Text = (frecamt - fpayamt1).ToString("#,##0;(#,##0) ;");
                    break;

                case "PDCStatus":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("typesum='TTTT'");
                    dt4 = dv1.ToTable();
                    ((Label)this.gvgrpchqissued.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(payam)", "")) ? 0 : dt4.Compute("sum(payam)", ""))).ToString("#,##0;-#,##0; ");
                    break;


                case "PrjStatus":

                    dv = dt1.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='BBBBAAAAAAAA'");
                    dt4 = dv.ToTable();
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFTSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tsalamt)", "")) ?
                                   0.00 : dt4.Compute("sum(tsalamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFTmonSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(msalamt)", "")) ?
                                   0.00 : dt4.Compute("sum(msalamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFTReSVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(trecamt)", "")) ?
                                           0.00 : dt4.Compute("sum(trecamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFNOI")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(noiamt)", "")) ?
                                           0.00 : dt4.Compute("sum(noiamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFRecamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(recamt)", "")) ?
                                           0.00 : dt4.Compute("sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFBRecSalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(balsalrec)", "")) ?
                                           0.00 : dt4.Compute("sum(balsalrec)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFExpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(texpamt)", "")) ?
                                           0.00 : dt4.Compute("sum(texpamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFPAdvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tpadvamt)", "")) ?
                                           0.00 : dt4.Compute("sum(tpadvamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLCNFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tlcamt)", "")) ?
                                           0.00 : dt4.Compute("sum(tlcamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFOvmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tovamt)", "")) ?
                                           0.00 : dt4.Compute("sum(tovamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFIAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tbankinamt)", "")) ?
                                           0.00 : dt4.Compute("sum(tbankinamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFtExp")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tactamt)", "")) ?
                                           0.00 : dt4.Compute("sum(tactamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLibAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tliamt)", "")) ?
                                           0.00 : dt4.Compute("sum(tliamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(lfrmhoff)", "")) ?
                                           0.00 : dt4.Compute("sum(lfrmhoff)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFLtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(ltohoff)", "")) ?
                                           0.00 : dt4.Compute("sum(ltohoff)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvPrjStatus.FooterRow.FindControl("lgvFRLamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(treloanamt)", "")) ?
                                           0.00 : dt4.Compute("sum(treloanamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "MProStatus":
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r2)", "")) ? 0.00 : dt1.Compute("sum(r2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r3)", "")) ? 0.00 : dt1.Compute("sum(r3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r4)", "")) ? 0.00 : dt1.Compute("sum(r4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r5)", "")) ? 0.00 : dt1.Compute("sum(r5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r6)", "")) ? 0.00 : dt1.Compute("sum(r6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r7)", "")) ? 0.00 : dt1.Compute("sum(r7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r8)", "")) ? 0.00 : dt1.Compute("sum(r8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r9)", "")) ? 0.00 : dt1.Compute("sum(r9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r10)", "")) ? 0.00 : dt1.Compute("sum(r10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r1)", "")) ? 0.00 : dt1.Compute("sum(r11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r12)", "")) ? 0.00 : dt1.Compute("sum(r12)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR13")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r13)", "")) ? 0.00 : dt1.Compute("sum(r13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFR14")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(r14)", "")) ? 0.00 : dt1.Compute("sum(r14)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toramt)", "")) ? 0.00 : dt1.Compute("sum(toramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFtoCollection")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tocollamt)", "")) ? 0.00 : dt1.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPorStatus.FooterRow.FindControl("lgvFnetposition")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ? 0.00 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "RptDayWSale":
                    dv = dt1.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
                    dt4 = dv.ToTable();

                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(tuamt)", "")) ?
                                    0 : dt4.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(suamt)", "")) ?
                                    0 : dt4.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(disamt)", "")) ?
                                    0 : dt4.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;



            }



        }



        protected void gvCollDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvCollDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }


        protected void gvCollDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvudesc");
                Label cashamt = (Label)e.Row.FindControl("lgvcashamt");
                Label chqamt = (Label)e.Row.FindControl("lgvchqamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    udesc.Font.Bold = true;
                    cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");


                }

            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ChequeInHand":

                    break;

                case "RecPay":


                    break;

                case "IssuedVsCollect":


                    break;

                case "BankPosition":

                    break;

                case "MasPVsMonPVsExAllPro":

                    break;


                case "TarVsAch":
                    this.PrintDailSalVsColl();

                    break;

                case "SoldUnsold":

                    break;

                case "PDCStatus":

                    break;

                case "FeaAllProject":

                    break;

                case "GPNPALLPRO":

                    break;

                case "PrjStatus":

                    break;

                case "MProStatus":

                    break;

                case "PDCSummary":

                    break;


                case "RptDayWSale":

                    break;






            }

        }


        private void PrintDailSalVsColl()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblcollvscl"];
            ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptDailySaleVsCollTarget();
            TextObject rptCname = rptsale.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptDate = rptsale.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptDate.Text = "( From " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " to " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " )";

            TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsale.SetDataSource(dt);
            Session["Report1"] = rptsale;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void btnRecDesc_Click(object sender, EventArgs e)
        {


            string recpcode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblcollvscl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "recpcode like('" + recpcode + "')";
            dt = dv1.ToTable();


            string mCOMCOD = this.Request.QueryString["comcod"].ToString();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mACTCODE = dt.Rows[0]["recpcode"].ToString();
            string mACTDESC = dt.Rows[0]["recpdesc"].ToString();
            string lebel2 = dt.Rows[0]["rleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 
            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkAccMultiReport.aspx?rpttype=PrjReportRP&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkAccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkAccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                            mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=R" + "', target='_blank');</script>";
            }


        }
        protected void btnPayDesc_Click(object sender, EventArgs e)
        {
            string paycode = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            DataTable dt = (DataTable)Session["tblcollvscl"];
            DataView dv1 = dt.DefaultView;
            dv1.RowFilter = "paycode like('" + paycode + "')";
            dt = dv1.ToTable();

            string mCOMCOD = this.Request.QueryString["comcod"].ToString();
            string mTRNDAT1 = this.Request.QueryString["Date1"].ToString();
            string mTRNDAT2 = this.Request.QueryString["Date2"].ToString();
            string mACTCODE = dt.Rows[0]["paycode"].ToString();
            string mACTDESC = dt.Rows[0]["paydesc"].ToString();
            string lebel2 = dt.Rows[0]["pleb2"].ToString();
            if (mACTCODE == "")
            {
                return;
            }

            ///---------------------------------//// 

            if (ASTUtility.Left(mACTCODE, 1) == "4")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkAccMultiReport.aspx?rpttype=PrjReportRP&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&actdesc=" + mACTDESC + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
            else if (lebel2 == "")
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkAccMultiReport.aspx?rpttype=RPschedule&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('LinkAccMultiReport.aspx?rpttype=detailsTBRP&comcod=" + mCOMCOD + "&actcode=" +
                                mACTCODE + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&Type=P" + "', target='_blank');</script>";
            }
        }
        protected void gvrecandpay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton HyRecDesc = (LinkButton)e.Row.FindControl("btnRecDesc");
                Label lgvRecAmt = (Label)e.Row.FindControl("lblgvrecpam");

                LinkButton HyPayDesc = (LinkButton)e.Row.FindControl("btnPayDesc");
                Label lgvPayAmt = (Label)e.Row.FindControl("lgvpayam1");


                string code1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recpcode")).ToString();
                string code2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "paycode")).ToString();

                if (code1 == "" && code2 == "")
                {
                    return;
                }

                if (ASTUtility.Right(code1, 8) == "00000000")
                {

                    HyRecDesc.Font.Bold = true;
                    lgvRecAmt.Font.Bold = true;
                }
                if (ASTUtility.Right(code2, 8) == "00000000")
                {
                    HyPayDesc.Font.Bold = true;
                    lgvPayAmt.Font.Bold = true;
                }

            }
        }
        protected void gvBankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label AccDesc = (Label)e.Row.FindControl("lblgvDescbank");
                Label opnbal = (Label)e.Row.FindControl("lblgvopnbal");
                Label opnliabilities = (Label)e.Row.FindControl("lblgvopnliabilities");
                Label Dramtbank = (Label)e.Row.FindControl("lblgvDramtbank");
                Label Cramtbank = (Label)e.Row.FindControl("lblgvCramtbank");
                Label clobalbank = (Label)e.Row.FindControl("lblgvclobalbank");
                Label cloliabilities = (Label)e.Row.FindControl("lblgvcloliabilities");
                Label bankLim = (Label)e.Row.FindControl("lblgvbankLim");
                Label bankBal = (Label)e.Row.FindControl("lblgvbankBal");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();




                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    AccDesc.Font.Bold = true;
                    opnbal.Font.Bold = true;
                    opnliabilities.Font.Bold = true;
                    Dramtbank.Font.Bold = true;
                    Cramtbank.Font.Bold = true;
                    clobalbank.Font.Bold = true;
                    cloliabilities.Font.Bold = true;
                    bankLim.Font.Bold = true;
                    bankBal.Font.Bold = true;
                    //lgvRecAmt.Font.Bold = true;
                    AccDesc.Style.Add("text-align", "right");
                }


            }


        }
        protected void gvMMPlanVsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc1");
            string mPACTCODE = ((Label)e.Row.FindControl("lblgvpactcode")).Text;
            string comcod = this.Request.QueryString["comcod"].ToString();
            string frmdate = this.Request.QueryString["Date1"].ToString();
            string todate = this.Request.QueryString["Date2"].ToString();

            hlink1.NavigateUrl = "~/F_45_GrAcc/LinkGrpImpExeStatus.aspx?Type=MPlnVsEx&comcod=" + comcod + "&pactcode=" + mPACTCODE + "&Date1=" + frmdate + "&Date2=" + todate;
        }
        protected void gvSalVsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartment");
                Label lgvmonsalamt = (Label)e.Row.FindControl("lgvmonsalamt");
                Label lgvmoncollamt = (Label)e.Row.FindControl("lgvmoncollamt");
                Label lgvtsalamt = (Label)e.Row.FindControl("lgvtsalamt");
                Label lgvtcollamt = (Label)e.Row.FindControl("lgvtcollamt");
                Label lgvuatsalamt = (Label)e.Row.FindControl("lgvuatsalamt");
                Label lgvtatsaleamt = (Label)e.Row.FindControl("lgvtatsaleamt");
                Label lgvuatcollamt = (Label)e.Row.FindControl("lgvuatcollamt");
                Label lgvtatcollamt = (Label)e.Row.FindControl("lgvtatcollamt");
                Label lgvpmonsalamt = (Label)e.Row.FindControl("lgvpmonsalamt");
                Label lgvpmoncollamt = (Label)e.Row.FindControl("lgvpmoncollamt");
                Label lgvperontsale = (Label)e.Row.FindControl("lgvperontsale");
                Label lgvperontcoll = (Label)e.Row.FindControl("lgvperontcoll");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvmonsalamt.Font.Bold = true;
                    lgvmoncollamt.Font.Bold = true;
                    lgvtsalamt.Font.Bold = true;
                    lgvtcollamt.Font.Bold = true;
                    lgvtatsaleamt.Font.Bold = true;
                    lgvtatcollamt.Font.Bold = true;
                    lgvuatsalamt.Font.Bold = true;
                    lgvuatcollamt.Font.Bold = true;
                    lgvpmonsalamt.Font.Bold = true;
                    lgvpmoncollamt.Font.Bold = true;
                    lgvperontsale.Font.Bold = true;
                    lgvperontcoll.Font.Bold = true;

                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }

        protected void gvgrpchqissued_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label resdesc = (Label)e.Row.FindControl("lgvresdescgp");
                Label amt = (Label)e.Row.FindControl("lgvpayam");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();


                if (code == "")
                {
                    return;
                }

                else if (code == "TTTT")
                {
                    resdesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    resdesc.Style.Add("text-align", "right");

                }


            }
        }
        protected void dgvAccRec03_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HLgvDesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lgvtstkamal = (Label)e.Row.FindControl("lgvtstkamal");
                Label lgvusunitsizeal = (Label)e.Row.FindControl("lgvusunitsizeal");
                Label lgvusuamtal = (Label)e.Row.FindControl("lgvusuamtal");
                Label lgvunitsizeal = (Label)e.Row.FindControl("lgvunitsizeal");
                Label lgvaptcostal = (Label)e.Row.FindControl("lgvaptcostal");
                Label lgvcpaocostal = (Label)e.Row.FindControl("lgvcpaocostal");
                Label lgvtocsotal = (Label)e.Row.FindControl("lgvtocsotal");
                Label lgvEncashal = (Label)e.Row.FindControl("lgvEncashal");
                Label lgvtretamtal = (Label)e.Row.FindControl("lgvtretamtal");
                Label lgvtframtal = (Label)e.Row.FindControl("lgvtframtal");
                Label lgvtpdamtal = (Label)e.Row.FindControl("lgvtpdamtal");
                Label lgvtotreceivedal = (Label)e.Row.FindControl("lgvtotreceivedal");
                Label lgvtatoduesall = (Label)e.Row.FindControl("lgvtatoduesall");
                Label lgvtotalduesal = (Label)e.Row.FindControl("lgvtotalduesal");
                Label lgvtoduesal = (Label)e.Row.FindControl("lgvtoduesal");
                Label lgvpbduesal = (Label)e.Row.FindControl("lgvpbduesal");
                Label lgvpinsduesall = (Label)e.Row.FindControl("lgvpinsduesall");
                Label lgvCbookingal = (Label)e.Row.FindControl("lgvCbookingal");
                Label lgvCinstallmental = (Label)e.Row.FindControl("lgvCinstallmental");
                Label lgvCoCInstalmental = (Label)e.Row.FindControl("lgvCoCInstalmental");
                Label lgvvbaamtal = (Label)e.Row.FindControl("lgvvbaamtal");
                Label lgvdelchargeal = (Label)e.Row.FindControl("lgvdelchargeal");
                Label lgvdischargeal = (Label)e.Row.FindControl("lgvdischargeal");
                Label lgvnettoduesal = (Label)e.Row.FindControl("lgvnettoduesal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDesc.Font.Bold = true;
                    lgvtstkamal.Font.Bold = true;
                    lgvusunitsizeal.Font.Bold = true;
                    lgvusuamtal.Font.Bold = true;
                    lgvunitsizeal.Font.Bold = true;
                    lgvaptcostal.Font.Bold = true;
                    lgvcpaocostal.Font.Bold = true;
                    lgvtocsotal.Font.Bold = true;
                    lgvEncashal.Font.Bold = true;
                    lgvtretamtal.Font.Bold = true;
                    lgvtframtal.Font.Bold = true;
                    lgvtpdamtal.Font.Bold = true;
                    lgvtotreceivedal.Font.Bold = true;
                    lgvtatoduesall.Font.Bold = true;
                    lgvtotalduesal.Font.Bold = true;
                    lgvtoduesal.Font.Bold = true;
                    lgvpbduesal.Font.Bold = true;
                    lgvpinsduesall.Font.Bold = true;
                    lgvCbookingal.Font.Bold = true;
                    lgvCinstallmental.Font.Bold = true;
                    lgvCoCInstalmental.Font.Bold = true;
                    lgvCbookingal.Font.Bold = true;
                    lgvvbaamtal.Font.Bold = true;
                    lgvdelchargeal.Font.Bold = true;
                    lgvdischargeal.Font.Bold = true;
                    lgvnettoduesal.Font.Bold = true;
                    // actdesc.Style.Add("text-align", "right");


                }

                else
                {
                    string comcod = this.Request.QueryString["comcod"].ToString();
                    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                    string frmdate = this.Request.QueryString["Date1"].ToString();
                    string todate = this.Request.QueryString["Date2"].ToString();
                    HLgvDesc.NavigateUrl = "~/F_45_GrAcc/LinkGrpRptSaleDues.aspx?Type=DuesCollect&comcod=" + comcod + "&pactcode=" + code + "&pactdesc=" + pactdesc + "&Date1=" + frmdate + "&Date2=" + todate;


                }

            }


        }
        protected void dgvAccRec03_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec03.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvFeaAllPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDescfea");
                Label lgvoRev = (Label)e.Row.FindControl("lgvoRev");
                Label logvCost = (Label)e.Row.FindControl("logvCost");
                Label logvmargin = (Label)e.Row.FindControl("logvmargin");
                Label lgvperorCost = (Label)e.Row.FindControl("lgvperorCost");
                Label lgvRev = (Label)e.Row.FindControl("lgvRev");
                Label lgvCost = (Label)e.Row.FindControl("lgvCost");
                HyperLink Profit = (HyperLink)e.Row.FindControl("hlnkgvProfit");
                Label lgvperCost = (Label)e.Row.FindControl("lgvperCost");
                Label lgvPerRev = (Label)e.Row.FindControl("lgvPerRev");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "infcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    lgvoRev.Font.Bold = true;
                    logvCost.Font.Bold = true;
                    logvmargin.Font.Bold = true;
                    lgvperorCost.Font.Bold = true;

                    lgvRev.Font.Bold = true;
                    lgvCost.Font.Bold = true;
                    Profit.Font.Bold = true;
                    lgvperCost.Font.Bold = true;
                    lgvPerRev.Font.Bold = true;


                    actdesc.Style.Add("text-align", "right");


                }
                Profit.Style.Add("color", "blue");
                actdesc.Style.Add("color", "blue");

            }




            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string comcod = this.Request.QueryString["comcod"].ToString();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescfea");
            HyperLink Profit1 = (HyperLink)e.Row.FindControl("hlnkgvProfit");
            string Actcode = ((Label)e.Row.FindControl("lgvInfoCode")).Text;
            string ActDesc = ((Label)e.Row.FindControl("lgvInfodesc")).Text;
            if (ASTUtility.Right(Actcode, 4) == "AAAA")
                return;

            Profit1.NavigateUrl = "~/F_45_GrAcc/LinkGrpRevsiFeasibility.aspx?Type=RevFeaCL&comcod=" + comcod + "&actcode=" + Actcode + "&actdesc=" + ActDesc;
            hlink1.NavigateUrl = "LinkGrpFeaIncomeSt.aspx?comcod=" + comcod + "&actcode=" + Actcode + "&actdesc=" + ActDesc;

        }
        protected void gvgpnp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvProjectgn");
                Label conarea = (Label)e.Row.FindControl("lblgvconarea");
                Label salamt = (Label)e.Row.FindControl("lblgvsaleamt");
                Label conscost = (Label)e.Row.FindControl("lblgvconsct");
                Label binterest = (Label)e.Row.FindControl("lblgvbinterest");
                Label lblgvtconabin = (Label)e.Row.FindControl("lblgvtconabin");
                Label lcost = (Label)e.Row.FindControl("lblgvlcost");
                Label lblgvcoffund = (Label)e.Row.FindControl("lblgvcoffund");
                Label lblgvtocfaland = (Label)e.Row.FindControl("lblgvtocfaland");
                Label adcost = (Label)e.Row.FindControl("lblgvadcost");

                Label tprcost = (Label)e.Row.FindControl("lblgvtprcost");
                Label gprofit = (Label)e.Row.FindControl("lblgvgp");

                Label ovrhead = (Label)e.Row.FindControl("lblgvovrhead");
                Label rfund = (Label)e.Row.FindControl("lblgvrfund");
                Label topcost = (Label)e.Row.FindControl("lblgvtopcost");
                Label tocost = (Label)e.Row.FindControl("lblgvtocost");
                Label nprofit = (Label)e.Row.FindControl("lblgvnp");
                Label peroncost = (Label)e.Row.FindControl("lblgvperoncost");
                Label peronsl = (Label)e.Row.FindControl("lblgvperonsl");
                Label npperoncost = (Label)e.Row.FindControl("lblgvnpperoncost");
                Label vnpperonsl = (Label)e.Row.FindControl("lblgvnpperonsl");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    conarea.Font.Bold = true;
                    salamt.Font.Bold = true;
                    conscost.Font.Bold = true;
                    binterest.Font.Bold = true;
                    lblgvtconabin.Font.Bold = true;
                    lcost.Font.Bold = true;
                    lblgvcoffund.Font.Bold = true;
                    lblgvtocfaland.Font.Bold = true;
                    adcost.Font.Bold = true;
                    tprcost.Font.Bold = true;
                    gprofit.Font.Bold = true;

                    ovrhead.Font.Bold = true;
                    rfund.Font.Bold = true;
                    topcost.Font.Bold = true;
                    nprofit.Font.Bold = true;
                    peroncost.Font.Bold = true;
                    peronsl.Font.Bold = true;
                    npperoncost.Font.Bold = true;
                    vnpperonsl.Font.Bold = true;

                    actdesc.Style.Add("text-align", "right");


                }

            }


        }
        protected void grvPrjStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDescps");
                Label lgvTSVal = (Label)e.Row.FindControl("lgvTSVal");
                Label lgvTmonSVal = (Label)e.Row.FindControl("lgvTmonSVal");
                Label lgvTReSVal = (Label)e.Row.FindControl("lgvTReSVal");
                Label lgvNOI = (Label)e.Row.FindControl("lgvNOI");
                Label lgvRecamt = (Label)e.Row.FindControl("lgvRecamt");
                Label lgvBRecSalamt = (Label)e.Row.FindControl("lgvBRecSalamt");
                Label lgvExpAmt = (Label)e.Row.FindControl("lgvExpAmt");
                Label lgvPAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label lgvLCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label lgvOvmt = (Label)e.Row.FindControl("lgvOvmt");
                Label lgvIAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label lgvtExp = (Label)e.Row.FindControl("lgvtExp");
                Label lgvLibAmt = (Label)e.Row.FindControl("lgvLibAmt");
                Label lgvLframt = (Label)e.Row.FindControl("lgvLframt");
                Label lgvLtoamt = (Label)e.Row.FindControl("lgvLtoamt");
                Label lgvRLamt = (Label)e.Row.FindControl("lgvRLamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string mCOMCOD = this.Request.QueryString["comcod"].ToString();
                string mPACTCODE = ((Label)e.Row.FindControl("lblActcode")).Text;
                string mTRNDAT1 = this.Request.QueryString["date"].ToString();
                //------------------------------//////
                Label actcode = (Label)e.Row.FindControl("lblgvcode");
                HyperLink actdesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                hlink1.NavigateUrl = "LinkProjectCollBrkDown.aspx?Type=IndPrjStDet&comcod=" + mCOMCOD + "&pactcode=" + mPACTCODE + "&Date1=" + mTRNDAT1;

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    hlink1.Font.Bold = true;
                    lgvTSVal.Font.Bold = true;
                    lgvTmonSVal.Font.Bold = true;
                    lgvTReSVal.Font.Bold = true;
                    lgvNOI.Font.Bold = true;
                    lgvRecamt.Font.Bold = true;
                    lgvBRecSalamt.Font.Bold = true;
                    lgvExpAmt.Font.Bold = true;
                    lgvPAdvAmt.Font.Bold = true;
                    lgvLCNFAmt.Font.Bold = true;
                    lgvOvmt.Font.Bold = true;
                    lgvIAmt.Font.Bold = true;
                    lgvtExp.Font.Bold = true;
                    lgvLibAmt.Font.Bold = true;
                    lgvLframt.Font.Bold = true;
                    lgvLtoamt.Font.Bold = true;
                    lgvRLamt.Font.Bold = true;

                    hlink1.Style.Add("text-align", "right");
                }

            }


        }

        protected void gvpdc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HLgvDescpaysum = (HyperLink)e.Row.FindControl("HLgvDescpaysum");
                Label toamtpdc = (Label)e.Row.FindControl("lgvtoamtpdc");
                Label dueam = (Label)e.Row.FindControl("lgvdueam");
                Label pdcam = (Label)e.Row.FindControl("lgvpdc");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pgrp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    toamtpdc.Font.Bold = true;
                    dueam.Font.Bold = true;
                    pdcam.Font.Bold = true;
                    HLgvDescpaysum.Style.Add("text-align", "right");
                }


                else
                {
                    HLgvDescpaysum.Style.Add("color", "blue");
                    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                    HLgvDescpaysum.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PDCStatus&comcod=" + comcod + "&actcode=" + code + "&grp=" + grp + "&Date2=" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");

                }




            }
        }
        protected void gvDayWSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDayWSale.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblgvDPactdesc");
                Label bgdamt = (Label)e.Row.FindControl("lgvDTAmt");
                Label salamt = (Label)e.Row.FindControl("lgvDSAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    salamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }

            }
        }

        protected void gvrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Deptdesc = (Label)e.Row.FindControl("lblgvDepartmentrc");
                Label lgvtocollection = (Label)e.Row.FindControl("lgvtocollectionsum");
                Label lgvinhfchq = (Label)e.Row.FindControl("lgvinhfchq");
                Label lgvinhrchq = (Label)e.Row.FindControl("lgvinhrchq");
                Label lgvchqdep = (Label)e.Row.FindControl("lgvchqdep");
                Label lgvreconamt = (Label)e.Row.FindControl("lgvreconamt");
                Label lgvinhpchq = (Label)e.Row.FindControl("lgvinhpchq");
                Label lgvrepchq = (Label)e.Row.FindControl("lgvrepchq");
                Label lgvncollamt = (Label)e.Row.FindControl("lgvncollamt");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "deptcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 2) == "AA" || ASTUtility.Right(code, 2) == "59")
                {

                    Deptdesc.Font.Bold = true;
                    lgvtocollection.Font.Bold = true;
                    lgvinhfchq.Font.Bold = true;
                    lgvinhrchq.Font.Bold = true;
                    lgvchqdep.Font.Bold = true;
                    lgvreconamt.Font.Bold = true;
                    lgvinhpchq.Font.Bold = true;
                    lgvrepchq.Font.Bold = true;
                    lgvncollamt.Font.Bold = true;

                    Deptdesc.Style.Add("text-align", "right");


                }

            }
        }

        protected void gvbankbal_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label ActDesc = (Label)e.Row.FindControl("lgcActDescbb");
                Label opnam = (Label)e.Row.FindControl("lgvopnambb");
                Label closam = (Label)e.Row.FindControl("lgvclosambb");
                Label balam = (Label)e.Row.FindControl("lgbalambb");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "code")).ToString();


                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "AAA")
                {


                    ActDesc.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    balam.Font.Bold = true;
                    ActDesc.Style.Add("text-align", "right");

                }




            }
        }
    }
}