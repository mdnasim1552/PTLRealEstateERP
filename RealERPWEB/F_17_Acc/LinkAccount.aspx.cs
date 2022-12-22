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
    public partial class LinkAccount : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string title = (this.Request.QueryString["Type"].ToString() == "BalConfirmation") ? "Balance Confirmation Information"
                  : (this.Request.QueryString["Type"].ToString() == "Details") ? "Details of Balance Sheet"
                  : (this.Request.QueryString["Type"].ToString() == "INDetails") ? "Details of Income  Statement" : "Date Wise Sales";
                ((Label)this.Master.FindControl("lblTitle")).Text = title;

                this.Master.Page.Title = title;
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
            return (hst["comcod"].ToString());

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "BalConfirmation":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.txtfrmdate.Text = this.Request.QueryString["Date1"];
                    this.txttodate.Text = this.Request.QueryString["Date2"];
                    this.ShowBalConfirmation();
                    break;
                case "SalesProj":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.sfrDate.Text = this.Request.QueryString["Date1"];
                    this.stDate.Text = this.Request.QueryString["Date2"];
                    this.salesStatus();
                    break;
                case "SalDetails":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.lblSFrmDate.Text = this.Request.QueryString["Date1"];
                    this.lblSTrmDate.Text = this.Request.QueryString["Date2"];
                    this.ShowSalesDetails();
                    break;


                case "Details":
                    this.MultiView1.ActiveViewIndex = 3;
                    //this.lblSFrmDate.Text = this.Request.QueryString["Date1"];
                    //this.lblSTrmDate.Text = this.Request.QueryString["Date2"];
                    this.DetailBalance();
                    break;



                case "INDetails":
                    this.MultiView1.ActiveViewIndex = 4;

                    this.IncomeDetails();
                    break;



                case "BalanceDet":
                    this.MultiView1.ActiveViewIndex = 5;

                    this.txtfdatebdet.Text = this.Request.QueryString["Date1"];
                    this.txttodatebdet.Text = this.Request.QueryString["Date2"];
                    this.BalanceSheetDet();
                    break;



                case "IncomeDet02":
                    this.MultiView1.ActiveViewIndex = 6;

                    this.lblfrmdateisd.Text = this.Request.QueryString["Date1"];
                    this.lbltodateisd.Text = this.Request.QueryString["Date2"];
                    this.lblvalopndate.Text = this.Request.QueryString["opndate"];
                    this.IncomeStDet();
                    break;



                case "BalanceDet2":
                    this.MultiView1.ActiveViewIndex = 7;

                    this.txtfdatebdet2.Text = this.Request.QueryString["Date1"];
                    this.txttodatebdet2.Text = this.Request.QueryString["Date2"];
                    this.BalanceSheetDet2();
                    break;





            }
        }

        private void ShowBalConfirmation()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTCASHANDBANKBAL", date1, date2, "12", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvCABankBal.DataSource = null;
                this.gvCABankBal.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void salesStatus()
        {
            Session.Remove("tbAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = this.Request.QueryString["pactcode"];
            string frdate = this.Request.QueryString["Date1"];
            string todate = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", PactCode, frdate, todate, "12", "%", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }

            this.lblPrijDesc.Text = ds1.Tables[0].Rows[0]["pactdesc"].ToString();
            ViewState["tbAcc"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void ShowSalesDetails()
        {
            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "RPTSALESDETAILS", date1, date2, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvSalDet.DataSource = null;
                this.grvSalDet.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = ds1.Tables[0];
            this.Data_Bind();



        }

        private void DetailBalance()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];
            string levelmain = "12";
            string leveldetails = "12";
            string status = this.Request.QueryString["Type"];
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILSTB", date1, date2, levelmain, leveldetails, status, "", "", "", "");



            if (ds1 == null)
            {
                this.gvDetails.DataSource = null;
                this.gvDetails.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void IncomeDetails()
        {

            ViewState.Remove("tbAcc");
            string comcod = this.GetCompCode();
            string date1 = this.Request.QueryString["Date1"];
            string date2 = this.Request.QueryString["Date2"];
            string opndate = this.Request.QueryString["opndate"];
            string levelmain = "12";
            string leveldetails = "12";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "RPTDETAILINST", date1, date2, levelmain, leveldetails, opndate, "", "", "", "");



            if (ds1 == null)
            {
                this.gvInDetails.DataSource = null;
                this.gvInDetails.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();



        }

        private void BalanceSheetDet()
        {



            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acgcode = this.Request.QueryString["acgcode"].ToString() + "%";
            string date1 = this.txtfdatebdet.Text.Substring(0, 11);
            string date2 = this.txttodatebdet.Text.Substring(0, 11);
            string chk = "";
            if (this.chkcost.Checked)
            {
                chk = "withoutcost";
            }
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTBALSHEETDET", date1, date2, acgcode, chk, "", "", "", "", "");



            if (ds1 == null)
            {
                this.dgvBSDet.DataSource = null;
                this.dgvBSDet.DataBind();

                return;
            }
            DataTable dt = ds1.Tables[0];
            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            ds1.Dispose();
            this.Data_Bind();



        }


        private void BalanceSheetDet2()
        {
            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acgcode = this.Request.QueryString["acgcode"].ToString() + "%";
            string date1 = this.txtfdatebdet2.Text.Substring(0, 11);
            string date2 = this.txttodatebdet2.Text.Substring(0, 11);
            string chk = "";
            if (this.chkcostadjdet2.Checked)
            {
                chk = "withoutcost";
            }
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTBALSHEETDET2", date1, date2, acgcode, chk, "", "", "", "", "");



            if (ds1 == null)
            {
                this.dgvbsdet2.DataSource = null;
                this.dgvbsdet2.DataBind();

                return;
            }

            ViewState["tbAcc"] = this.HiddenSameData(ds1.Tables[0]);
            ds1.Dispose();
            this.Data_Bind();




        }


        private void IncomeStDet()
        {


            ViewState.Remove("tblAcc");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string acgcode = this.Request.QueryString["acgcode"].ToString() + "%";
            string date1 = this.lblfrmdateisd.Text.Substring(0, 11);
            string date2 = this.lbltodateisd.Text.Substring(0, 11);
            string opndate = this.lblvalopndate.Text.Substring(0, 11);
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_IS_BS_R2", "RPTINCOMESTDET", date1, date2, opndate, acgcode, "", "", "", "", "");



            if (ds1 == null)
            {
                this.gvistdet.DataSource = null;
                this.gvistdet.DataBind();

                return;
            }

            ViewState["tbAcc"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();
                    }
                    break;

                case "SalesProj":
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

                case "Details":
                case "TBDetails":
                case "INDetails":

                    string actcode4 = dt1.Rows[0]["actcode4"].ToString();

                    if (dt1.Rows[0]["rescode4"].ToString().Substring(0, 4) == "0000")
                        dt1.Rows[0]["rescode4"] = "";

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                        {
                            actcode4 = dt1.Rows[j]["actcode4"].ToString();
                            dt1.Rows[j]["actdesc4"] = "";
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



                case "BalanceDet2":

                    actcode4 = dt1.Rows[0]["actcode4"].ToString();
                    //  string rescode = dt1.Rows[0]["rescode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["actcode4"].ToString() == actcode4)
                            dt1.Rows[j]["actdesc4"] = "";
                        actcode4 = dt1.Rows[j]["actcode4"].ToString();


                    }

                    break;
            }

            return dt1;
        }
        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            if (dt.Rows.Count == 0)
                return;

            switch (type)
            {

                case "SalesProj":
                    //((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                    //                0 : dt.Compute("sum(tuamt)", ""))).TlgvFditemoString("#,##0;(#,##0); ");
                    //((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ?
                    //                0 : dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ?
                    //                0 : dt.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;
                case "SalDetails":
                    ((Label)this.grvSalDet.FooterRow.FindControl("lgvFsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salamt)", "")) ?
                                    0 : dt.Compute("sum(salamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvSalDet.FooterRow.FindControl("lgvFaAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(agamt)", "")) ?
                                    0 : dt.Compute("sum(agamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvSalDet.FooterRow.FindControl("lgvFNAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ?
                                    0 : dt.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
            }
        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tbAcc"];

            if ((dt.Rows.Count == 0)) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":
                    this.gvCABankBal.DataSource = dt;
                    this.gvCABankBal.DataBind();
                    break;
                case "SalesProj":

                    this.gvDayWSale.DataSource = dt;
                    this.gvDayWSale.DataBind();

                    this.FooterCalculation();
                    break;
                case "SalDetails":
                    this.grvSalDet.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvSalDet.DataSource = dt;
                    this.grvSalDet.DataBind();
                    this.FooterCalculation();
                    break;

                case "Details":

                    this.gvDetails.Columns[3].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.gvDetails.Columns[4].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.gvDetails.DataSource = dt;
                    this.gvDetails.DataBind();

                    Session["Report1"] = gvDetails;
                    ((HyperLink)this.gvDetails.HeaderRow.FindControl("hlbtnCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


                    break;

                case "INDetails":

                    this.gvInDetails.Columns[3].HeaderText = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date2"].ToString()).ToString("dd-MMM-yyyy");
                    this.gvInDetails.Columns[4].HeaderText = Convert.ToDateTime(this.Request.QueryString["opndate"].ToString()).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.Request.QueryString["Date1"].ToString()).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.gvInDetails.DataSource = dt;
                    this.gvInDetails.DataBind();
                    break;

                case "BalanceDet":

                    this.dgvBSDet.Columns[2].HeaderText = Convert.ToDateTime(this.txtfdatebdet.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.dgvBSDet.Columns[5].HeaderText = Convert.ToDateTime(this.txttodatebdet.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.dgvBSDet.DataSource = dt;
                    this.dgvBSDet.DataBind();
                    break;




                case "BalanceDet2":

                    this.dgvbsdet2.Columns[2].HeaderText = Convert.ToDateTime(this.txtfdatebdet2.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.dgvbsdet2.Columns[5].HeaderText = Convert.ToDateTime(this.txttodatebdet2.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.dgvbsdet2.DataSource = dt;
                    this.dgvbsdet2.DataBind();
                    break;




                case "IncomeDet02":



                    this.gvistdet.Columns[2].HeaderText = Convert.ToDateTime(this.lblfrmdateisd.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.lbltodateisd.Text).ToString("dd-MMM-yyyy");
                    this.gvistdet.Columns[3].HeaderText = Convert.ToDateTime(this.lblvalopndate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.lblfrmdateisd.Text).AddDays(-1).ToString("dd-MMM-yyyy");


                    //this.dgvBSDet.Columns[2].HeaderText = Convert.ToDateTime(this.lbltodatebdet.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    //this.dgvBSDet.Columns[3].HeaderText = Convert.ToDateTime(this.lblfdatebdet.Text).ToString("dd-MMM-yyyy") + "<br />" + "Taka";
                    this.gvistdet.DataSource = dt;
                    this.gvistdet.DataBind();
                    break;



            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {

                case "BalConfirmation":
                    this.PrintBalConfirmation();
                    break;
                case "SalesProj":
                    this.rptDayWSale();
                    break;
                case "SalDetails":
                    this.RptSalesDetails();
                    break;

                case "Details":
                    this.PrintDetBS();
                    break;
                case "INDetails":
                    this.PrintDetIncome();
                    break;
                case "BalanceDet":
                    //if (comcod == "3333" || comcod == "3101")
                    //{
                    //    this.PrintBalanceSheetDetAl();
                    //}
                    //else
                    //{
                    //    this.PrintBalanceSheetDet();
                    //}
                    this.PrintBalanceSheetDetAl();

                    break;


                case "IncomeDet02":
                    this.PrintIncomeStatDet();
                    break;




            }
        }


        private void PrintBalanceSheetDet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBalanceSheet();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfdatebdet.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodatebdet.Text).ToString("dd-MMM-yyyy") + " )";

            TextObject txtHefrmdate = rptstk.ReportDefinition.ReportObjects["txtHefrmdate"] as TextObject;
            txtHefrmdate.Text = Convert.ToDateTime(this.txtfdatebdet.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";

            TextObject txtHetodate = rptstk.ReportDefinition.ReportObjects["txtHetodate"] as TextObject;
            txtHetodate.Text = Convert.ToDateTime(this.txttodatebdet.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";
            TextObject txtHead = rptstk.ReportDefinition.ReportObjects["txtHead"] as TextObject;
            txtHead.Text = this.Request.QueryString["mdesc"].ToString();//dt.Rows[0]["actdesc4"].ToString();


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintBalanceSheetDetAl()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBalanceSheetAle();
            DataTable dt = (DataTable)ViewState["tbAcc"];

            TextObject txtcomname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtcomname.Text = comnam;

            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfdatebdet.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodatebdet.Text).ToString("dd-MMM-yyyy") + " )";

            TextObject txtHefrmdate = rptstk.ReportDefinition.ReportObjects["txtHefrmdate"] as TextObject;
            txtHefrmdate.Text = Convert.ToDateTime(this.txtfdatebdet.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";

            TextObject txtHetodate = rptstk.ReportDefinition.ReportObjects["txtHetodate"] as TextObject;
            txtHetodate.Text = Convert.ToDateTime(this.txttodatebdet.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";
            TextObject txtHead = rptstk.ReportDefinition.ReportObjects["txtHead"] as TextObject;
            txtHead.Text = this.Request.QueryString["mdesc"].ToString();//dt.Rows[0]["actdesc4"].ToString();


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintIncomeStatDet()
        {
            // Iqbal Nayan
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
            string fdate01 = (Convert.ToDateTime(this.lblfrmdateisd.Text).ToString("dd-MMM-yyyy"));
            string tdate = (Convert.ToDateTime(this.lbltodateisd.Text).ToString("dd-MMM-yyyy"));
            string FTDate = "(From " + fdate01 + " To " + tdate + " )";

            string drdate = Convert.ToDateTime(this.lblfrmdateisd.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.lbltodateisd.Text).ToString("dd-MMM-yyyy");

            string crdate = Convert.ToDateTime(this.lblvalopndate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.lblfrmdateisd.Text).AddDays(-1).ToString("dd-MMM-yyyy");


            //this.gvistdet.Columns[2].HeaderText = Convert.ToDateTime(this.lblfrmdateisd.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.lbltodateisd.Text).ToString("dd-MMM-yyyy");
            //this.gvistdet.Columns[3].HeaderText = Convert.ToDateTime(this.lblvalopndate.Text).ToString("dd-MMM-yyyy") + "<br />" + "To " + "<br / >" + Convert.ToDateTime(this.lblfrmdateisd.Text).AddDays(-1).ToString("dd-MMM-yyyy");

            //  string FTDate = "(From " + Convert.ToDateTime(this.lblfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.lbltodate.Text).ToString("dd-MMM-yyyy") + " )";



            string fDate = Convert.ToDateTime(this.lbltodateisd.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";
            string sDate = Convert.ToDateTime(this.lblfrmdateisd.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            string Rpttitle = dt.Rows[0]["actdesc1"].ToString();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.GeneralAdminOverH>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptGeneralAdminOverhad", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("Cdate", FTDate));
            Rpt1.SetParameters(new ReportParameter("fDate", drdate));
            Rpt1.SetParameters(new ReportParameter("sDate", crdate));
            Rpt1.SetParameters(new ReportParameter("RptTitle", Rpttitle));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        //private void PrintIncomeStatDet() 
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBalanceSheet();
        //    DataTable dt = (DataTable)ViewState["tbAcc"];
        //    TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
        //    rpttxtcompanyname.Text = comnam;
        //    TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
        //    txtfdate.Text = "(From " + Convert.ToDateTime(this.lblfdatebdet.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.lbltodatebdet.Text).ToString("dd-MMM-yyyy") + " )";

        //    TextObject txtHefrmdate = rptstk.ReportDefinition.ReportObjects["txtHefrmdate"] as TextObject;
        //    txtHefrmdate.Text = Convert.ToDateTime(this.lblfdatebdet.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";

        //    TextObject txtHetodate = rptstk.ReportDefinition.ReportObjects["txtHetodate"] as TextObject;
        //    txtHetodate.Text = Convert.ToDateTime(this.lbltodatebdet.Text).ToString("dd-MMM-yyyy") + "\n" + "Taka";
        //    TextObject txtHead = rptstk.ReportDefinition.ReportObjects["txtHead"] as TextObject;
        //    txtHead.Text = this.Request.QueryString["mdesc"].ToString();//dt.Rows[0]["actdesc4"].ToString();


        //    TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptstk.SetDataSource(dt);
        //    Session["Report1"] = rptstk;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}
        protected void PrintBalConfirmation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptAccBalConfirmation();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            rpttxtcompanyname.Text = comnam;
            TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtfdate.Text = "(From " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void rptDayWSale()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //ReportDocument rptsale = new RealERPRPT.R_22_Sal.rptDayWiseSales();
            //TextObject rptCname = rptsale.ReportDefinition.ReportObjects["CompName"] as TextObject;
            //rptCname.Text = comnam;
            //TextObject rptCode = rptsale.ReportDefinition.ReportObjects["CodeDesc"] as TextObject;
            //rptCode.Text = "Level: Details";
            //TextObject rptDate = rptsale.ReportDefinition.ReportObjects["date"] as TextObject;
            //rptDate.Text = "From : " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To:" + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");
            //TextObject txtuserinfo = rptsale.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsale.SetDataSource(dt);
            //Session["Report1"] = rptsale;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void RptSalesDetails()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptstk = new RealERPRPT.R_15_Acc.RptSalesDetails();
            //DataTable dt = (DataTable)ViewState["tbAcc"];
            //TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //rpttxtcompanyname.Text = comnam;
            //TextObject txtfdate = rptstk.ReportDefinition.ReportObjects["TxtDate"] as TextObject;
            //txtfdate.Text = "(From " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " )";
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void PrintDetBS()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptDetAccBalanceSheet();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;   //
            rpttxtcompanyname.Text = comnam;

            TextObject rpttxtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;   //
            rpttxtHeader.Text = "Details of Balance Sheet";

            TextObject TxtOpening = rptstk.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            TxtOpening.Text = Convert.ToDateTime(this.Request.QueryString["Date1"]).AddDays(-1).ToString("dd-MMM-yyyy") + " Taka";

            TextObject TxtClosing = rptstk.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            TxtClosing.Text = Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy") + " Taka";


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void PrintDetIncome()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptDetAccBalanceSheet();
            DataTable dt = (DataTable)ViewState["tbAcc"];
            TextObject rpttxtcompanyname = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;   //txtHeader
            rpttxtcompanyname.Text = comnam;

            TextObject rpttxtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;   //
            rpttxtHeader.Text = "Details of Income Statment";

            TextObject TxtOpening = rptstk.ReportDefinition.ReportObjects["TxtOpening"] as TextObject;
            TxtOpening.Text = Convert.ToDateTime(this.Request.QueryString["opndate"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date1"]).AddDays(-1).ToString("dd-MMM-yyyy");

            TextObject TxtClosing = rptstk.ReportDefinition.ReportObjects["TxtClosing"] as TextObject;
            TxtClosing.Text = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.Request.QueryString["Date2"]).ToString("dd-MMM-yyyy");


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);

            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvCABankBal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink description = (HyperLink)e.Row.FindControl("HLgvDescbankcb");
                Label opnam = (Label)e.Row.FindControl("lblgvopnamcb");
                Label closam = (Label)e.Row.FindControl("lblgvclosamcb");
                Label netbal = (Label)e.Row.FindControl("lblgvnetbalcb");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    description.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    netbal.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }
                else if (ASTUtility.Right(code, 4) == "0000")
                {

                    description.Font.Bold = true;
                    opnam.Font.Bold = true;
                    closam.Font.Bold = true;
                    netbal.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }


            }
        }

        protected void gvDayWSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDayWSale.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvSalDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvSalDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lblgvdescriptiond");
                Label lblopnamt = (Label)e.Row.FindControl("lblgvopnamtd");
                Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamt");

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
                    lblgvcuamt.Font.Bold = true;

                    lblgvclobal.Font.Bold = true;

                }
            }
        }
        protected void gvInDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lblgvdescriptionind");
                Label lblopnamt = (Label)e.Row.FindControl("lblgvopnamtind");
                Label lblgvcuamt = (Label)e.Row.FindControl("lblgvcuamtind");
                Label lblgvclobal = (Label)e.Row.FindControl("lblgvclobalind");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "00000000AAAA")
                {

                    description.Font.Bold = true;
                    lblopnamt.Font.Bold = true;
                    lblgvcuamt.Font.Bold = true;

                    lblgvclobal.Font.Bold = true;

                }
            }
        }
        protected void dgvBSDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDesc");
            Label lblcode = (Label)e.Row.FindControl("lblgvcode");
            Label clobal = (Label)e.Row.FindControl("lblgvclobal");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamt");
            //Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();
            string mTRNDAT1 = this.txtfdatebdet.Text;
            string mTRNDAT2 = this.txttodatebdet.Text;

            if (code == "")
            {
                return;
            }


            if (code.Length == 8 && (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA"))
            {
                hlink1.Attributes["style"] = "color:maroon; font-weight:bold;";
                lblcode.Attributes["style"] = "color:maroon; font-weight:bold;";
                clobal.Attributes["style"] = "color:maroon; font-weight:bold;";
                opnamt.Attributes["style"] = "color:maroon; font-weight:bold;";
                //cuamt.Attributes["style"] = "color:maroon; font-weight:bold;";

            }
















            if (code == "")
            {
                return;
            }





            if (ASTUtility.Left(code, 1) == "4")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + code + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else if (lebel2 == "2")
            {
                if (ASTUtility.Right(code, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            else
            {
                if (ASTUtility.Right(code, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=";  //+ "&actdesc=" + mACTDESC


            }


        }
        protected void gvistdet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvISDesc");
            Label lblcode = (Label)e.Row.FindControl("lblgvcodeisd");
            Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamt");
            Label clobal = (Label)e.Row.FindControl("lblgvclobal");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();
            string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();
            string mTRNDAT1 = this.lblfrmdateisd.Text;
            string mTRNDAT2 = this.lbltodateisd.Text;

            if (code == "")
            {
                return;
            }


            if (code.Length == 8 && (ASTUtility.Right(code, 2) != "00" && ASTUtility.Right(code, 2) != "AA"))
            {
                hlink1.Attributes["style"] = "color:maroon; font-weight:bold; font-size:12px;";
                lblcode.Attributes["style"] = "color:maroon; font-weight:bold;font-size:12px;";
                clobal.Attributes["style"] = "color:maroon; font-weight:bold; font-size:12px;";
                opnamt.Attributes["style"] = "color:maroon; font-weight:bold; font-size:12px;";
                cuamt.Attributes["style"] = "color:maroon; font-weight:bold; font-size:12px;";

            }


            if (ASTUtility.Right(code, 4) == "AAAA")
            {


                hlink1.Attributes["style"] = "color:#FC2A6A; font-weight:bold; font-size:14px;";
                lblcode.Attributes["style"] = "color:#FC2A6A; font-weight:bold font-size:14px;";
                clobal.Attributes["style"] = "color:#FC2A6A; font-weight:bold; font-size:14px;";
                opnamt.Attributes["style"] = "color:#FC2A6A; font-weight:bold; font-size:14px;";
                cuamt.Attributes["style"] = "color:#FC2A6A; font-weight:bold; font-size:14px;";



            }
















            if (code == "")
            {
                return;
            }





            if (ASTUtility.Left(code, 1) == "4")
            {
                hlink1.NavigateUrl = "AccProjectReports.aspx?actcode=" + code + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }
            if (lebel2 == "")
            {

                int lactcode = Convert.ToInt16(code.Substring(0, 1));
                string opnoption = lactcode >= 3 ? "withoutOpn" : "";
                // string opnoption="";

                if (ASTUtility.Right(code, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else

                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=ledger&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&actdesc=" + mACTDESC + "&opnoption=" + opnoption;
            }
            else
            {
                if (ASTUtility.Right(code, 4) == "0000")
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
                else
                    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + code +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }
        }

        protected void lbtnUpdatePCDate_OnClick(object sender, EventArgs e)
        {
            this.BalanceSheetDet();
        }

        protected void lbtnbaldet2_Click(object sender, EventArgs e)
        {
            this.BalanceSheetDet2();
        }
        protected void dgvbsdet2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string mCOMCOD = hst["comcod"].ToString();

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvBSDescbdet2");
            Label lblcode = (Label)e.Row.FindControl("lblgvcodebdet2");
            Label opnamt = (Label)e.Row.FindControl("lblgvopnamtbdet2");
            Label lbldram = (Label)e.Row.FindControl("lblgvdramtbdet2");
            Label lblcram = (Label)e.Row.FindControl("lblgvcramtbdet2");
            Label clobal = (Label)e.Row.FindControl("lblgvclobalbdet2");


            //Label cuamt = (Label)e.Row.FindControl("lblgvcuamt");


            string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();
            string rescode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString().Trim();
            string spcfcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "spcfcod")).ToString().Trim();
            string mACTCODE = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actcode4")).ToString().Trim();

            string mACTDESC = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "actdesc4")).ToString().Trim();
            //  string lebel2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "leb2")).ToString().Trim();
            string mTRNDAT1 = this.txtfdatebdet2.Text;
            string mTRNDAT2 = this.txttodatebdet2.Text;

            if (code == "")
            {
                return;
            }


            if (code.Length == 8)
            {
                hlink1.Attributes["style"] = "color:blue;font-weight:bold;";
                lblcode.Attributes["style"] = "color:blue;font-weight:bold;";
                opnamt.Attributes["style"] = "color:blue;font-weight:bold;";
                lbldram.Attributes["style"] = "color:blue;font-weight:bold;";
                lblcram.Attributes["style"] = "color:blue;font-weight:bold;";
                clobal.Attributes["style"] = "color:blue;font-weight:bold;";





            }

            else if (ASTUtility.Right(code, 4) == "0000")
            {
                hlink1.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblcode.Attributes["style"] = "color:maroon;font-weight:bold;";
                opnamt.Attributes["style"] = "color:maroon;font-weight:bold;";
                lbldram.Attributes["style"] = "color:maroon;font-weight:bold;";
                lblcram.Attributes["style"] = "color:maroon;font-weight:bold;";
                clobal.Attributes["style"] = "color:maroon;font-weight:bold;";





            }





            string sactcode = mACTCODE.Trim().Length == 8 ? mACTCODE : ((ASTUtility.Right(mACTCODE, 10) == "0000000000") ? mACTCODE.Substring(0, 2) : (ASTUtility.Right(mACTCODE, 8) == "00000000") ? mACTCODE.Substring(0, 4) : (ASTUtility.Right(mACTCODE, 4) == "0000") ? mACTCODE.Substring(0, 8) : mACTCODE);


            if ((code.Substring(0, 2) != "18" && code.Substring(0, 2) != "26" && code.Substring(0, 2) != "25") && (ASTUtility.Right(rescode, 4) == "0000"))
            {




                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + sactcode +
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

                hlink1.NavigateUrl = "RptAccAITVATASDAllSup.aspx?Type=Report&sircode=" + rescode + "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;

            }

            else if (ASTUtility.Right(rescode, 4) != "0000")
            {


                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=spledgerDetials&comcod=" + mCOMCOD + "&rescode=" + rescode +
                 "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&opnoption=&daywise=daywise";



            }

            else
            {


                hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=detailsTB&comcod=" + mCOMCOD + "&actcode=" + sactcode +
                         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            }


        }




        protected void lnkbtnIncdateOk_Click(object sender, EventArgs e)
        {
            this.IncomeStDet();
        }
    }
}