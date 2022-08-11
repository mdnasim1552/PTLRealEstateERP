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

    public partial class RptTransactionSt : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");


                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string frmdate = Convert.ToDateTime("01-Jan-" + Convert.ToDateTime(date).ToString("yyyy")).ToString("dd-MMM-yyyy");

                string ifrmdate = this.Request.QueryString["Date1"] ?? frmdate;
                string itodate = this.Request.QueryString["Date2"] ?? date;
                this.txtfromdate.Text = ifrmdate.Length > 0 ? ifrmdate : frmdate;
                this.txttodate.Text = itodate.Length > 0 ? itodate : date;

                this.SelectView();
                this.GetProjectName();
                //this.lblHeader.Text = (type == "TransPrjWise") ? "Daily Transaction(Project Wise) Report" : (type == "ClientStat") ? "Client Status Report"
                //    : (type == "RepChq") ? "Replacement Cheque Report" : (type == "TransSummary") ? "Daily Collection Summary" : "Daily Transaction(Date Wise) Report";

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "TransPrjWise") ? "Daily Transaction(Project Wise) Report" : (type == "ClientStat") ? "Client Status Report"
                    : (type == "RepChq") ? "Replacement Cheque Report"
                    : (type == "TransSummary") ? "Day wise Collection Summary"
                    : (type == "RectypeWise") ? "Client Details Information"
                    : (type == "RectypeWise02") ? "Utility & Other Charges 02"
                    : (type == "Association") ? "Asssoctiation Fee "
                    : (type == "ServiceCharge") ? "Service Charge Top Sheet  "
                    : (type == "ServicePayment") ? "Service Charge Payment Details "
                    : (type == "ServiceCollection") ? "Service Charge Collection Details "
                    : (type == "Modification") ? " Modification Service Charge  "
                    : "Daily Transaction(Date Wise) Report";





            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

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
                case "TransPrjWise":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.rbtnList1.Visible = true;
                    this.rbtnList1.SelectedIndex = 0;
                    break;

                case "TransDateWise":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.rbtnList1.Visible = true;
                    this.rbtnList1.SelectedIndex = 3;
                    break;
                case "ClientStat":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.Label5.Visible = false;
                    this.txtfromdate.Visible = false;
                    this.Label6.Visible = false;
                    this.txttodate.Visible = false;
                    break;
                case "RepChq":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "TransSummary":
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "RectypeWise":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;

                case "Association":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.Label5.Visible = false;
                    this.txtfromdate.Visible = false;
                    break;
                case "ServiceCharge":

                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + (this.txtfromdate.Text.Trim()).Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.MultiView1.ActiveViewIndex = 7;

                    break;

                case "ServicePayment":

                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + (this.txtfromdate.Text.Trim()).Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.MultiView1.ActiveViewIndex = 8;

                    break;

                case "ServiceCollection":

                    this.txtfromdate.Text = System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01" + (this.txtfromdate.Text.Trim()).Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");

                    this.MultiView1.ActiveViewIndex = 9;

                    break;

                case "Modification":
                    this.MultiView1.ActiveViewIndex = 10;
                    this.Label5.Visible = false;
                    this.Label6.Text = "As On Date :";
                    this.txtfromdate.Visible = false;
                    break;
                case "RectypeWise02":
                    this.MultiView1.ActiveViewIndex = 11;
                    break;


            }
        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] + "%" : "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETFIANDONPRONAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName.SelectedValue = this.Request.QueryString["prjcode"].Length > 0 ? this.Request.QueryString["prjcode"] : "000000000000";

        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                //case "TransPrjWise":
                //    this.RptPrjWise();
                //    break;
                case "TransDateWise":
                    this.RptPrjWise();
                    break;
                case "ClientStat":
                    this.RptClientStat();
                    break;
                case "RepChq":
                    this.RptRepChq();
                    break;

                case "TransSummary":
                    this.RptTransSummary();
                    break;
                case "RectypeWise":
                    this.RptRTypeWiseTransaction();
                    break;
                case "Association":
                    this.RptPrintAssociation();
                    break;
                case "ServiceCharge":
                    this.RptServiceCharge();
                    break;

                case "ServicePayment":
                    this.RptServiceChargePayment();
                    break;

                case "ServiceCollection":
                    this.RptServiceChargeCollection();
                    break;

                case "Modification":
                    this.RptModCharge();
                    break;

                case "RectypeWise02":
                    this.RptRTypeWiseTransaction02();
                    break;



            }
        }



        private LocalReport RptCall()
        {

            string comcod = this.GetCompCode();
            //ReportDocument rptstate;
            DataTable dt = (DataTable)Session["DailyTrns"];
            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>();
            switch (comcod)
            {
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3310":
                case "3311":


                    Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptTransStatement02", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    //rptstate =  new RealERPRPT.R_22_Sal.RptTransStatement02();
                    break;
                default:
                    Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptTransStatement02", list, null, null);
                    Rpt1.EnableExternalImages = true;
                    //rptstate = new RealERPRPT.R_22_Sal.RptTransStatement02();
                    //rptstate = new RealERPRPT.R_22_Sal.RptTransStatement02();
                    break;

            }

            return Rpt1;

        }



        private void RptPrjWise()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["DailyTrns"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.ChequeDepositPrint>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_22_Sal.RptTransStatement02", list, null, null);
            Rpt1.EnableExternalImages = true;
            
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Cheque In Hand (Wating For Approval)"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Transaction Statement";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }





        private void RptClientStat()
        {


            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3101":
                case "3330":
                    this.RptClientStatBr();


                    break;
                default:

                    this.RptClientStatGen();

                    break;
            }


        }



        private void RptClientStatBr()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstate = new RealERPRPT.R_22_Sal.RptClientStatBridge();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptPrj = rptstate.ReportDefinition.ReportObjects["txtProName"] as TextObject;
            rptPrj.Text = this.ddlProjectName.SelectedItem.Text;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["DailyTrns"]);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Status Summary";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void RptClientStatGen()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstate = new RealERPRPT.R_22_Sal.RptClientStat();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptPrj = rptstate.ReportDefinition.ReportObjects["txtProName"] as TextObject;
            rptPrj.Text = this.ddlProjectName.SelectedItem.Text;
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["DailyTrns"]);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Status Summary";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptRepChq()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_22_Sal.RptReplacementChq();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;//
            rptCname.Text = comnam;
            TextObject rptHeader = rptstate.ReportDefinition.ReportObjects["TxtHeader"] as TextObject;
            //rptHeader.Text = this.lblHeader.Text;

            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["ftdate"] as TextObject;
            rptftdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["DailyTrns"]);


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Transaction Statement";
                string eventdesc = "Print Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptTransSummary()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt1 = (DataTable)Session["Rcutotalamt"];
            ReportDocument rptstate = new RealERPRPT.R_22_Sal.RptTransactionSummary();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptftdate.Text = "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            TextObject txtRcu = rptstate.ReportDefinition.ReportObjects["txtRcu"] as TextObject;
            txtRcu.Text = comcod == "3305" ? "CR-RCU" : "";

            TextObject txtRHEL = rptstate.ReportDefinition.ReportObjects["txtRHEL"] as TextObject;
            txtRHEL.Text = comcod == "3305" ? "CR-RHEL" : "";

            TextObject txtRHELamt = rptstate.ReportDefinition.ReportObjects["txtRHELamt"] as TextObject;
            txtRHELamt.Text = comcod == "3305" ? Convert.ToDouble(dt1.Rows[0]["cramt2"]).ToString() : "";

            TextObject txtrcuamt = rptstate.ReportDefinition.ReportObjects["txtrcuamt"] as TextObject;
            txtrcuamt.Text = comcod == "3305" ? Convert.ToDouble(dt1.Rows[0]["cramt1"]).ToString() : "";

            int j = 1;
            for (int i = 2; i < this.gvTransSum.Columns.Count; i++)
            {
                TextObject rpttxth = rptstate.ReportDefinition.ReportObjects["txtamt" + j.ToString()] as TextObject;
                rpttxth.Text = this.gvTransSum.Columns[i].HeaderText.ToString();
                j++;

            }


            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["DailyTrns"]);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void RptRTypeWiseTransaction()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["DailyTrns"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.UtilityOtherCharges>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptCustDetailsInfo", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Collection Details Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptPrintAssociation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["DailyTrns"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.AssociationFee>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptCustAssociationFee", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "As On Date :" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Association Fee"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void RptServiceCharge()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["DailyTrns"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_23_CR.RptMonWiseServiceCharge();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Service Charge Top Sheet ";

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private DataTable CollectCurDate(DataTable dt)
        {
            DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
            DateTime todate = Convert.ToDateTime(this.txttodate.Text);
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chqdate >= '" + frmdate + "' and chqdate<= '" + todate + "'");
            dt = dv.ToTable();
            return dt;

        }


        private void RptServiceChargePayment()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["DailyTrns"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_23_CR.RptMonWiseServicePayment();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Service Charge Payment Details";

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void RptServiceChargeCollection()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["DailyTrns"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_23_CR.RptMonWiseServiceCollection();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Service Charge Collection Details";

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptModCharge()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            ReportDocument rptstate = new RealERPRPT.R_23_CR.RptModCharge();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtHeader = rptstate.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Modification Charge ";
            TextObject rptftdate = rptstate.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            rptftdate.Text = "As on Date :" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)Session["DailyTrns"]);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "TransPrjWise":
                    this.LoadGrid();
                    break;
                case "TransDateWise":
                    this.LoadGridDateWise();
                    break;
                case "ClientStat":
                    this.LoadClintStat();
                    break;
                case "RepChq":
                    this.LoadRepChq();
                    break;

                case "TransSummary":
                    this.ShowTransSummary();
                    break;

                case "RectypeWise":
                    this.RTypeWiseTransaction();
                    break;

                case "Association":
                    this.ShowAssociation();
                    break;


                case "ServiceCharge":
                    this.ServiceCharge();
                    break;

                case "ServicePayment":
                    this.ServiceChargePayment();
                    break;


                case "ServiceCollection":
                    this.ServiceChargeCollection();
                    break;

                case "Modification":
                    this.ShowModCharge();
                    break;


                case "RectypeWise02":
                    this.RTypeWiseTransaction02();
                    break;


            }


        }




        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadGrid()
        {

            string comcod = this.GetCompCode();
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "TRANSACTIONSTATEMENT", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDaTrns.DataSource = null;
                this.gvDaTrns.DataBind();
                return;
            }
            Session["DailyTrns"] = (this.rbtnList1.SelectedIndex == 0) ? HiddenSameData(ds1.Tables[0]) : CollectCurDate(HiddenSameData(ds1.Tables[0]));

            //Session["DailyTrns"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private string companytype()
        {
            string comcod = this.GetCompCode();

            string coltype = "";
            switch (comcod)
            {

                //case "3101":// RHEL
                case "3305":// RHEL
                case "3311":// RHEL(chittagong)
                case "3306":// Ratul
                case "2305":// Land
                case "3310":// rcu


                    coltype = "TRANSACTIONSTATEMENT1";
                    break;

                default:
                    coltype = "TRANSACTION_STATEMENT2";
                    break;
            }
            return coltype;
        }
        private void LoadGridDateWise()
        {
            Session.Remove("DailyTrns");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string actual = (this.rbtnList1.SelectedIndex == 2) ? "Actualdate"
                     :(this.rbtnList1.SelectedIndex == 3)?"Reconcliedate" : (this.rbtnList1.SelectedIndex == 4) ? "EntryDate" : (this.rbtnList1.SelectedIndex == 5) ? "Depositeddate" : "";

            string coltype = this.companytype();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", coltype, fromdate, todate, pactcode, actual, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvTrnDatWise.DataSource = null;
                this.grvTrnDatWise.DataBind();
                return;
            }
            Session["DailyTrns"] = (this.rbtnList1.SelectedIndex == 0) ? HiddenSameData(ds1.Tables[0]) : (this.rbtnList1.SelectedIndex == 2) ? HiddenSameData(ds1.Tables[0]) : (this.rbtnList1.SelectedIndex == 3) ? HiddenSameData(ds1.Tables[0]):(this.rbtnList1.SelectedIndex == 4) ? HiddenSameData(ds1.Tables[0]): (this.rbtnList1.SelectedIndex == 5) ? HiddenSameData(ds1.Tables[0]): CollectCurDate(HiddenSameData(ds1.Tables[0]));
            DataTable dt = (DataTable)Session["DailyTrns"];
            this.Data_Bind();

        }
        private void LoadClintStat()
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SALSMGT", "RPTCLIENTSTAT", pactcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDaTrns.DataSource = null;
                this.gvDaTrns.DataBind();
                return;
            }
            Session["DailyTrns"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }
        private void LoadRepChq()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string repno = this.txtSrcMrrNo.Text.Trim();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "REPLACEMENTCHQ", fromdate, todate, pactcode, repno, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvRepChq.DataSource = null;
                this.grvRepChq.DataBind();
                return;
            }
            Session["DailyTrns"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private void ShowTransSummary()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "TRANSSTSUMMARY", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvTransSum.DataSource = null;
                this.gvTransSum.DataBind();
                return;
            }
            DataTable dt = ds1.Tables[1];
            for (int i = 0; i < dt.Rows.Count; i++)
                this.gvTransSum.Columns[i + 2].HeaderText = dt.Rows[i]["colfrmdesc"].ToString();

            Session["DailyTrns"] = ds1.Tables[0];
            Session["Rcutotalamt"] = ds1.Tables[2];

            this.Data_Bind();



            //string comcod = this.GetCompCode();
            //string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "TRANSSTSUMMARY", fromdate, todate, pactcode, "", "", "", "", "", "");
            //if (ds1 == null)
            //{
            //    this.gvTransSum.DataSource = null;
            //    this.gvTransSum.DataBind();
            //    return;
            //}
            //DataTable dt = ds1.Tables[1];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //        this.gvTransSum.Columns[i+2].HeaderText =dt.Rows[i]["colfrmdesc"].ToString();

            //Session["DailyTrns"] = ds1.Tables[0];
            //this.Data_Bind();


        }





        private void RTypeWiseTransaction02()
        {
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTTRANSRTYPEWISE02", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRectypeWise02.DataSource = null;
                this.gvRectypeWise02.DataBind();
                return;
            }


            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();




        }

        private void RTypeWiseTransaction()
        {
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTTRANSRTYPEWISE", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.rpRTypeTrans.DataSource = null;
                this.rpRTypeTrans.DataBind();
                return;
            }


            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();




        }

        private void ShowAssociation()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTTRANSASSOCIATION", todate, pactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvAssociation.DataSource = null;
                this.gvAssociation.DataBind();
                return;
            }

            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["DailyTrns"] = ds1.Tables[0];
            this.Data_Bind();
        }


        private void ServiceCharge()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONWISESERVICETOPSHEET", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSercharge.DataSource = null;
                this.gvSercharge.DataBind();
                return;
            }

            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["DailyTrns"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void ServiceChargePayment()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONWISESERVICECHARGEPAYMENT", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvServicePayment.DataSource = null;
                this.gvServicePayment.DataBind();
                return;
            }

            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["DailyTrns"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void ServiceChargeCollection()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONWISESERVICECHARGECOLLECTION", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvServiceCollection.DataSource = null;
                this.gvServiceCollection.DataBind();
                return;
            }

            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["DailyTrns"] = ds1.Tables[0];
            this.Data_Bind();

        }

        private void ShowModCharge()
        {

            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "25%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT_LETTERINFO", "RPTTRANSMODIFICATION", todate, pactcode, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvModCharge.DataSource = null;
                this.gvModCharge.DataBind();
                return;
            }

            Session["DailyTrns"] = this.HiddenSameData(ds1.Tables[0]);
            //Session["DailyTrns"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "TransPrjWise":
                    this.gvDaTrns.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDaTrns.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvDaTrns.DataBind();
                    this.FooterCalculation();
                    break;
                case "TransDateWise":
                    this.grvTrnDatWise.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvTrnDatWise.DataSource = (DataTable)Session["DailyTrns"];
                    this.grvTrnDatWise.DataBind();
                    this.FooterCalculation();
                    break;
                case "ClientStat":
                    this.grvClientStat.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvClientStat.DataSource = (DataTable)Session["DailyTrns"];
                    this.grvClientStat.DataBind();
                    this.FooterCalculation();
                    break;
                case "RepChq":
                    this.grvRepChq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvRepChq.DataSource = (DataTable)Session["DailyTrns"];
                    this.grvRepChq.DataBind();
                    this.FooterCalculation();
                    break;

                case "TransSummary":
                    this.gvTransSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvTransSum.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvTransSum.DataBind();
                    this.FooterCalculation();
                    break;
                case "RectypeWise":
                    this.rpRTypeTrans.DataSource = (DataTable)Session["DailyTrns"];
                    this.rpRTypeTrans.DataBind();



                    break;

                case "Association":
                    this.gvAssociation.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvAssociation.DataBind();
                    this.FooterCalculation();
                    break;
                case "ServiceCharge":


                    // DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    // DateTime  dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    //for (int i = 7; i < 19; i++)
                    //{
                    //    if (datefrm > dateto)
                    //        break;

                    //    this.gvSercharge.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                    //    datefrm = datefrm.AddMonths(1);

                    //}

                    this.gvSercharge.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvSercharge.DataBind();
                    this.FooterCalculation();
                    break;


                case "ServicePayment":
                    DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 4; i < 16; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvServicePayment.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvServicePayment.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvServicePayment.DataBind();
                    this.FooterCalculation();
                    break;

                case "ServiceCollection":
                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 4; i < 16; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvServiceCollection.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvServiceCollection.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvServiceCollection.DataBind();
                    this.FooterCalculation();
                    break;

                case "Modification":
                    this.gvModCharge.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvModCharge.DataBind();
                    this.FooterCalculation();
                    break;

                case "RectypeWise02":
                    this.gvRectypeWise02.DataSource = (DataTable)Session["DailyTrns"];
                    this.gvRectypeWise02.DataBind();
                    this.FooterCalculation();

                    break;




            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode;
            string mrdate;
            string grp;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Association":
                case "TransPrjWise":
                case "RectypeWise":
                case "RectypeWise02":

                case "ServiceCharge":
                case "ServicePayment":
                case "ServiceCollection":
                case "Modification":

                    pactcode = dt1.Rows[0]["pactcode"].ToString();
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
                case "TransDateWise":

                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                            dt1.Rows[j]["grpdesc"] = "";
                        grp = dt1.Rows[j]["grp"].ToString();
                    }
                    break;

                case "ClientStat":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
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
                case "RepChq":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string orgmrno = dt1.Rows[0]["orgmrno"].ToString();
                    string orgchqno = dt1.Rows[0]["orgchqno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["orgmrno"].ToString() == orgmrno && dt1.Rows[j]["orgchqno"].ToString() == orgchqno && dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {
                            orgmrno = dt1.Rows[j]["orgmrno"].ToString();
                            orgchqno = dt1.Rows[j]["orgchqno"].ToString();
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["orgmrno"] = "";
                            dt1.Rows[j]["orgchqno"] = "";
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["orgamt"] = 0.00;
                        }

                        else
                        {
                            if (dt1.Rows[j]["orgmrno"].ToString() == orgmrno)
                                dt1.Rows[j]["orgmrno"] = "";

                            if (dt1.Rows[j]["orgchqno"].ToString() == orgchqno)
                                dt1.Rows[j]["orgchqno"] = "";

                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                                dt1.Rows[j]["pactdesc"] = "";

                            orgmrno = dt1.Rows[j]["orgmrno"].ToString();
                            orgchqno = dt1.Rows[j]["orgchqno"].ToString();
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                        }

                    }
                    break;
            }

            return dt1;

        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["DailyTrns"];

            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "TransPrjWise":
                    ((Label)this.gvDaTrns.FooterRow.FindControl("lgvCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(cashamt)", "")) ?
                                0 : dt1.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDaTrns.FooterRow.FindControl("lgvCheAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(chqamt)", "")) ?
                               0 : dt1.Compute("sum(chqamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "TransDateWise":


                    DataTable dt4 = dt1.Copy();
                    DataView dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("grp='F' and collfrm1='EEEEE' ");
                    dt4 = dv1.ToTable();
                    double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                    double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                    ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = grvTrnDatWise;
                    ((HyperLink)this.grvTrnDatWise.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


                    //DataTable dt = dt1.Copy();
                    //DataView dv = dt.DefaultView;
                    //dv.RowFilter = ("grp='D'");
                    //dt = dv.ToTable();
                    //((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cashamt)", "")) ?
                    //            0 : dt.Compute("sum(cashamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCheAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(chqamt)", "")) ?
                    //            0 : dt.Compute("sum(chqamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "ClientStat":

                    ((Label)this.grvClientStat.FooterRow.FindControl("lgvFQty")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tqty)", "")) ?
                                0 : dt1.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvClientStat.FooterRow.FindControl("lgvFSoldVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(suamt)", "")) ?
                                0 : dt1.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvClientStat.FooterRow.FindControl("lgvFRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(paidamt)", "")) ?
                                0 : dt1.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvClientStat.FooterRow.FindControl("lgvFReceivable")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(receivable)", "")) ?
                              0 : dt1.Compute("sum(receivable)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvClientStat.FooterRow.FindControl("lgvFReconAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(reconamt)", "")) ?
                                0 : dt1.Compute("sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvClientStat.FooterRow.FindControl("lgvFNReconAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(duereconamt)", "")) ?
                               0 : dt1.Compute("sum(duereconamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;
                case "RepChq":
                    ((Label)this.grvRepChq.FooterRow.FindControl("lgvFOrgCheAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(orgamt)", "")) ?
                                0 : dt1.Compute("sum(orgamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.grvRepChq.FooterRow.FindControl("lgvFRepCheAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(chqamt)", "")) ?
                                    0 : dt1.Compute("sum(chqamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;



                case "TransSummary":
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ?
                                0 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ?
                                0 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ?
                                0 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ?
                                0 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt5)", "")) ?
                                0 : dt1.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt6)", "")) ?
                                0 : dt1.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt7)", "")) ?
                                0 : dt1.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt8)", "")) ?
                                0 : dt1.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt9)", "")) ?
                                0 : dt1.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt10)", "")) ?
                                0 : dt1.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt11)", "")) ?
                                0 : dt1.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt12)", "")) ?
                                0 : dt1.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt13")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt13)", "")) ?
                                0 : dt1.Compute("sum(amt13)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt14")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt14)", "")) ?
                                0 : dt1.Compute("sum(amt14)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt15")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt15)", "")) ?
                                0 : dt1.Compute("sum(amt15)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt16")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt16)", "")) ?
                             0 : dt1.Compute("sum(amt16)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt17")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt17)", "")) ?
                             0 : dt1.Compute("sum(amt17)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt18")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt18)", "")) ?
                             0 : dt1.Compute("sum(amt18)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt19")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt19)", "")) ?
                             0 : dt1.Compute("sum(amt19)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt20")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt20)", "")) ?
                             0 : dt1.Compute("sum(amt20)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt21")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt21)", "")) ?
                             0 : dt1.Compute("sum(amt21)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt22")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt22)", "")) ?
                             0 : dt1.Compute("sum(amt22)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt23")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt23)", "")) ?
                            0 : dt1.Compute("sum(amt23)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt24")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt24)", "")) ?
                            0 : dt1.Compute("sum(amt24)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt25")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt25)", "")) ?
                            0 : dt1.Compute("sum(amt25)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt26")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt26)", "")) ?
                            0 : dt1.Compute("sum(amt26)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt27")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt27)", "")) ?
                            0 : dt1.Compute("sum(amt27)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt28")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt28)", "")) ?
                            0 : dt1.Compute("sum(amt28)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt29")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt29)", "")) ?
                            0 : dt1.Compute("sum(amt29)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt30")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt30)", "")) ?
                            0 : dt1.Compute("sum(amt30)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt31")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt31)", "")) ?
                            0 : dt1.Compute("sum(amt31)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt32")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt32)", "")) ?
                            0 : dt1.Compute("sum(amt32)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFamt33")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt33)", "")) ?
                            0 : dt1.Compute("sum(amt33)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvTransSum.FooterRow.FindControl("lgvFnetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ?
                               0 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");

                    Session["Report1"] = gvTransSum;
                    ((HyperLink)this.gvTransSum.HeaderRow.FindControl("hlbtntbCdataExcel2")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    break;

                case "Association":

                    ((Label)this.gvAssociation.FooterRow.FindControl("lgvFReceipt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(asoreceipt)", "")) ?
                                0 : dt1.Compute("sum(asoreceipt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAssociation.FooterRow.FindControl("lgvFpayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(asopayment)", "")) ?
                                0 : dt1.Compute("sum(asopayment)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAssociation.FooterRow.FindControl("lgvFbalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balance)", "")) ?
                                0 : dt1.Compute("sum(balance)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAssociation.FooterRow.FindControl("lgvFassofee")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(assotarget)", "")) ?
                                0 : dt1.Compute("sum(assotarget)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAssociation.FooterRow.FindControl("lgvFReceivable")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(receivable)", "")) ?
                                0 : dt1.Compute("sum(receivable)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "ServiceCharge":


                    ((Label)this.gvSercharge.FooterRow.FindControl("lgvFopening")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(opnam)", "")) ? 0.00 : dt1.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSercharge.FooterRow.FindControl("lgvFcollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tocollamt)", "")) ? 0.00 : dt1.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSercharge.FooterRow.FindControl("lgvFtoamtmpay")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(topayamt)", "")) ? 0.00 : dt1.Compute("sum(topayamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSercharge.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balamt)", "")) ? 0.00 : dt1.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ? 0.00 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ? 0.00 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ? 0.00 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ? 0.00 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt5)", "")) ? 0.00 : dt1.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt6)", "")) ? 0.00 : dt1.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt7)", "")) ? 0.00 : dt1.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt8)", "")) ? 0.00 : dt1.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt9)", "")) ? 0.00 : dt1.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt10)", "")) ? 0.00 : dt1.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt11)", "")) ? 0.00 : dt1.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvSercharge.FooterRow.FindControl("lgvFamtmpay12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt12)", "")) ? 0.00 : dt1.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "ServicePayment":

                    //((Label)this.gvServicePayment.FooterRow.FindControl("lgvFopening")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(opnam)", "")) ? 0.00 : dt1.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvServicePayment.FooterRow.FindControl("lgvFcollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tocollamt)", "")) ? 0.00 : dt1.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFtoamtmpay01")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(topayamt)", "")) ? 0.00 : dt1.Compute("sum(topayamt)", ""))).ToString("#,##0;(#,##0); ");
                    // ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balamt)", "")) ? 0.00 : dt1.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ? 0.00 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ? 0.00 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ? 0.00 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ? 0.00 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt5)", "")) ? 0.00 : dt1.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt6)", "")) ? 0.00 : dt1.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt7)", "")) ? 0.00 : dt1.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt8)", "")) ? 0.00 : dt1.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt9)", "")) ? 0.00 : dt1.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt10)", "")) ? 0.00 : dt1.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt11)", "")) ? 0.00 : dt1.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServicePayment.FooterRow.FindControl("lgvFamtmpay12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt12)", "")) ? 0.00 : dt1.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "ServiceCollection":

                    // ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFopening")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(opnam)", "")) ? 0.00 : dt1.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFcollamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(tocollamt)", "")) ? 0.00 : dt1.Compute("sum(tocollamt)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFtoamtmpay01")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(topayamt)", "")) ? 0.00 : dt1.Compute("sum(topayamt)", ""))).ToString("#,##0;(#,##0); ");
                    // ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFbalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balamt)", "")) ? 0.00 : dt1.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ? 0.00 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ? 0.00 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ? 0.00 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ? 0.00 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt5)", "")) ? 0.00 : dt1.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt6)", "")) ? 0.00 : dt1.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt7)", "")) ? 0.00 : dt1.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt8)", "")) ? 0.00 : dt1.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt9)", "")) ? 0.00 : dt1.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt10)", "")) ? 0.00 : dt1.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt11)", "")) ? 0.00 : dt1.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvServiceCollection.FooterRow.FindControl("lgvFamtmpay12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt12)", "")) ? 0.00 : dt1.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "Modification":
                    ((Label)this.gvModCharge.FooterRow.FindControl("lgvFModCharge")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(modcharge)", "")) ? 0.00 : dt1.Compute("sum(modcharge)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvModCharge.FooterRow.FindControl("lgvFReceived")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(modreceipt)", "")) ? 0.00 : dt1.Compute("sum(modreceipt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvModCharge.FooterRow.FindControl("lgvFBalance")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balance)", "")) ? 0.00 : dt1.Compute("sum(balance)", ""))).ToString("#,##0;(#,##0); ");

                    break;

                case "RectypeWise02":

                    Session["Report1"] = gvRectypeWise02;
                    ((HyperLink)this.gvRectypeWise02.HeaderRow.FindControl("hlbotherCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                   
                    break;


            }



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvDaTrns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDaTrns.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvTrnDatWise_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvTrnDatWise.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void grvClientStat_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvClientStat.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void grvRepChq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvRepChq.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void grvTrnDatWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label CollFrm = (Label)e.Row.FindControl("lgvCollFrm");
                Label Cashamt = (Label)e.Row.FindControl("lgvCaAmt");
                Label chqamt = (Label)e.Row.FindControl("lgvChAmt");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();

                if (grp == "")
                {
                    return;
                }
                if (grp == "F" || grp == "G")
                {

                    CollFrm.Font.Bold = true;
                    Cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                }

                if (mrno == "AAAAAAAAA")
                {
                    CollFrm.Font.Bold = true;
                    Cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                }
            }
        }

        protected void gvTransSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTransSum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ibtnFindRepNo_Click(object sender, EventArgs e)
        {
            this.LoadRepChq();
        }

        protected void rpRTypeTrans_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {


            string comcod = this.GetCompCode();
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (comcod == "2305" || comcod == "3305" || comcod == "3306" || comcod == "3310" || comcod == "3311" || comcod == "3101")

            
                   ((Label)e.Item.FindControl("lblrputility")).Text = "Solar Panel ";
                ((Label)e.Item.FindControl("lblassocialtion")).Text = "Transfer Fee";



            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                Label actdesc = (Label)e.Item.FindControl("lgcProDescrtype");
                Label lgvUnDestype = (Label)e.Item.FindControl("lgvUnDestype");
                Label salesamt = (Label)e.Item.FindControl("lgvsalesamt");
                Label regavat = (Label)e.Item.FindControl("lgvregavat");
                Label solar = (Label)e.Item.FindControl("lgvsolar");
                Label addwork = (Label)e.Item.FindControl("lgvaddwork");
                Label association = (Label)e.Item.FindControl("lblrpassociation");
                Label societyfee = (Label)e.Item.FindControl("lgvsocietyfee");
                Label sercharge = (Label)e.Item.FindControl("lblrpsercharge");
                Label delaycharge = (Label)e.Item.FindControl("lgvdelaycharge");
                Label others = (Label)e.Item.FindControl("lgvothers");


                string code = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    actdesc.Font.Bold = true;
                    lgvUnDestype.Font.Bold = true;
                    salesamt.Font.Bold = true;
                    regavat.Font.Bold = true;
                    solar.Font.Bold = true;
                    addwork.Font.Bold = true;
                    association.Font.Bold = true;
                    societyfee.Font.Bold = true;
                    sercharge.Font.Bold = true;
                    delaycharge.Font.Bold = true;
                    others.Font.Bold = true;
                    lgvUnDestype.Style.Add("text-align", "right");


                }


                // DataRowView drv = (DataRowView)e.Item.DataItem;

            }



        }
        protected void gvSercharge_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lgvActdescmpay = (Label)e.Row.FindControl("lgvActdescmpay");
                Label lgvopening = (Label)e.Row.FindControl("lgvopening");
                Label lgvnetTotal = (Label)e.Row.FindControl("lgvnetTotal");

                Label lgvtoamtmpay = (Label)e.Row.FindControl("lgvtoamtmpay");
                Label lgvamtmpay1 = (Label)e.Row.FindControl("lgvamtmpay1");
                Label lgvamtmpay2 = (Label)e.Row.FindControl("lgvamtmpay2");
                Label lgvamtmpay3 = (Label)e.Row.FindControl("lgvamtmpay3");
                Label lgvamtmpay4 = (Label)e.Row.FindControl("lgvamtmpay4");
                Label lgvamtmpay5 = (Label)e.Row.FindControl("lgvamtmpay5");
                Label lgvamtmpay6 = (Label)e.Row.FindControl("lgvamtmpay6");
                Label lgvamtmpay7 = (Label)e.Row.FindControl("lgvamtmpay7");
                Label lgvamtmpay8 = (Label)e.Row.FindControl("lgvamtmpay8");
                Label lgvamtmpay9 = (Label)e.Row.FindControl("lgvamtmpay9");
                Label lgvamtmpay10 = (Label)e.Row.FindControl("lgvamtmpay10");
                Label lgvamtmpay11 = (Label)e.Row.FindControl("lgvamtmpay11");
                Label lgvamtmpay12 = (Label)e.Row.FindControl("lgvamtmpay12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActdescmpay.Font.Bold = true;
                    lgvopening.Font.Bold = true;

                    lgvnetTotal.Font.Bold = true;
                    lgvtoamtmpay.Font.Bold = true;
                    lgvamtmpay1.Font.Bold = true;
                    lgvamtmpay2.Font.Bold = true;
                    lgvamtmpay3.Font.Bold = true;
                    lgvamtmpay4.Font.Bold = true;
                    lgvamtmpay5.Font.Bold = true;
                    lgvamtmpay6.Font.Bold = true;
                    lgvamtmpay7.Font.Bold = true;
                    lgvamtmpay8.Font.Bold = true;
                    lgvamtmpay9.Font.Bold = true;
                    lgvamtmpay10.Font.Bold = true;
                    lgvamtmpay11.Font.Bold = true;
                    lgvamtmpay12.Font.Bold = true;
                    lgvActdescmpay.Style.Add("text-align", "right");
                }
                if (this.Request.QueryString["Type"] == "MonPaymentDet" || this.Request.QueryString["Type"] == "MonReceipt")
                {
                    if (ASTUtility.Right(code, 8) == "00000000")
                    {

                        lgvActdescmpay.Font.Bold = true;
                        lgvtoamtmpay.Font.Bold = true;
                        lgvamtmpay1.Font.Bold = true;
                        lgvamtmpay2.Font.Bold = true;
                        lgvamtmpay3.Font.Bold = true;
                        lgvamtmpay4.Font.Bold = true;
                        lgvamtmpay5.Font.Bold = true;
                        lgvamtmpay6.Font.Bold = true;
                        lgvamtmpay7.Font.Bold = true;
                        lgvamtmpay8.Font.Bold = true;
                        lgvamtmpay9.Font.Bold = true;
                        lgvamtmpay10.Font.Bold = true;
                        lgvamtmpay11.Font.Bold = true;
                        lgvamtmpay12.Font.Bold = true;
                        //lgvActdescmpay.Style.Add("text-align", "right");
                    }
                }

            }

        }
        protected void gvAssociation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAssociation.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvSercharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSercharge.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvServicePayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvServicePayment.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvServiceCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvServiceCollection.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvModCharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvModCharge.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvRectypeWise02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string fdate = this.txtfromdate.Text.ToString();
                string tdate = this.txttodate.Text.ToString();

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("gvRectypeWise02Custname");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();


                if (pactcode == "")
                {
                    return;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_24_CC/LinkOtherCollDetials.aspx?&Pactcode=" + pactcode + "&Usircode=" + usircode +
                    "&Date1=" + fdate + "&Date2=" + tdate;
                    hlink1.Target = "_blank";

                }


            }


        }
        protected void gvRectypeWise02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvRectypeWise02.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        private void RptRTypeWiseTransaction02()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");


            //DataTable dt = (DataTable)Session["DailyTrns"];

            //var list = dt.DataTableToList<RealEntity.C_09_PIMP.SubConBill.SubConBillTopSheet>();
            //LocalReport rpt = new LocalReport();
            //rpt = RptSetupClass1.GetLocalReport("R_12_Inv.RptMatTransStatus", list, null, null);
            //rpt.EnableExternalImages = true;
            //rpt.SetParameters(new ReportParameter("txtComNam", comnam));
            //rpt.SetParameters(new ReportParameter("txtProject", ""));
            //rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            //rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            //Session["Report1"] = rpt;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }

}











