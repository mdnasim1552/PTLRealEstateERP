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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
using AjaxControlToolkit;

//using MFGOBJ.C_22_Sal;
namespace RealERPWEB.F_99_Allinterface
{
    public partial class AccountInterface : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        Xml_BO_Class lst = new Xml_BO_Class();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                    (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "ACCOUNTS INTERFACE"; //

                //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");

                this.GetFromDate();
                this.txttoDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");  //Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                this.SaleRequRpt();
                this.RadioButtonList1_SelectedIndexChanged(null, null);
            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void GetFromDate()
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3101": //own 
                case "3333"://Alliance
                case "3354": // Edison
                case "3353"://Manama
                case "3355"://Green Wood
                case "3367"://Epic
                case "3368"://Finlay

                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtfrmdate.Text = Convert.ToDateTime(hst["opndate"].ToString()).AddDays(1).ToString("dd-MMM-yyyy");

                    break;

                default:
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    break;



            }




        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }

        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            // this.pnlInterAcc.Visible = true;
            this.pnlAcc.Visible = false;
        }

        //protected void lnkRept_Click(object sender, EventArgs e)
        //{
        //    //this.pnlInterAcc.Visible = false;
        //    this.pnlAcc.Visible = true;
        //}

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }



        private void SaleRequRpt()
        {
            string comcod = this.GetCompCode();
            string refno = "%" + this.txtrefno.Text.Trim() + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttoDate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", refno, frmdate, "%",
                todate, "", "", "", "", "");
            Session["alltable"] = ds1;
            Session["tblupdatecol"] = ds1.Tables[2];

            //this is side bar menu (Nahid)

            this.RadioButtonList1.Items[0].Text = "Update Sales" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["salupcout"].ToString() + "</span>";
            this.RadioButtonList1.Items[1].Text = "Cheque Deposit" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["adchqdep"].ToString() + "</span>";
            this.RadioButtonList1.Items[2].Text = "Update Collection" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["colupcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[3].Text = "Update Purchase" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["lpurcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[4].Text = "Cont. Bill Update" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["subconcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[5].Text = "Update M.Transfer" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["mtrncount"].ToString() + "</span>";
            this.RadioButtonList1.Items[6].Text = "Update PDC" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["pdccount"].ToString() + "</span>";
            this.RadioButtonList1.Items[7].Text = "Bank Reconcilation" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["bnkrecount"].ToString() + "</span>";
            this.RadioButtonList1.Items[8].Text = "Client Modification" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["adcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[9].Text = "General Bill Update" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["othreq"].ToString() + "</span>";
            this.RadioButtonList1.Items[10].Text = "LSD Update" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["lsdcount"].ToString() + "</span>";
            this.RadioButtonList1.Items[11].Text = "Petty Cash Update" + "<span class='lbldata counter'>" +
                                                  ds1.Tables[18].Rows[0]["pettcycount"].ToString() + "</span>";

            this.RadioButtonList1.Items[12].Text = "Unit Cancellation" + "<span class='lbldata counter'>" +
                                                     ds1.Tables[18].Rows[0]["canunit"].ToString() + "</span>";

            this.RadioButtonList1.Items[13].Text = "Material Conversion" + "<span class='lbldata counter'>" + ds1.Tables[18].Rows[0]["conversion"].ToString() + "</span>";

            this.RadioButtonList1.Items[14].Text = "Transfer Unit" + "<span class='lbldata counter'>" + ds1.Tables[18].Rows[0]["trnunit"].ToString() + "</span>";
            this.RadioButtonList1.Items[15].Text = "Indent Update" + "<span class='lbldata counter'>" + ds1.Tables[18].Rows[0]["indup"].ToString() + "</span>";
            this.RadioButtonList1.Items[16].Text = "Material Issue" + "<span class='lbldata counter'>" + ds1.Tables[18].Rows[0]["issue"].ToString() + "</span>";

            ////Update Sales
            //DataTable dt = new DataTable();
            //DataView dv = new DataView();
            //dt = ((DataTable) ds1.Tables[0]).Copy();
            //this.Data_Bind("gvSalesUpdate", dt);



            //dt = ((DataTable) ds1.Tables[1]).Copy();
            //this.Data_Bind ("gcchqdeposit", dt);


            ////Update Collection
            //dt = ((DataTable) ds1.Tables[2]).Copy();
            //this.Data_Bind("gvCollUpdate", dt);

            ////Purchase
            //dt = ((DataTable) ds1.Tables[3]).Copy();
            //this.Data_Bind("gvPurchase", dt);

            ////Approved
            //dt = ((DataTable) ds1.Tables[4]).Copy();
            //this.Data_Bind("gvConUpdate", dt);
            /////Trasnfer
            //dt = ((DataTable) ds1.Tables[5]).Copy();
            //this.Data_Bind("gvMatTrasfer", dt);

            ////PDC
            //dt = ((DataTable) ds1.Tables[6]).Copy();
            //this.Data_Bind("dgPdc", dt);
            ////Bank Rec
            //dt = ((DataTable) ds1.Tables[7]).Copy();
            //this.Data_Bind("gvBankRec", dt);

            ////Client Modification
            //dt = ((DataTable) ds1.Tables[8]).Copy();
            //this.Data_Bind("gvClientMod", dt);

            ////Other Bill
            //dt = ((DataTable) ds1.Tables[10]).Copy();
            //this.Data_Bind("gvOthBillUp", dt);

            ////LSD
            //dt = ((DataTable) ds1.Tables[11]).Copy();
            //this.Data_Bind("gvlsdUp", dt);

            ////patty Cash
            //dt = ((DataTable)ds1.Tables[12]).Copy();
            //this.Data_Bind("gvpetty", dt);



        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                ((Label)this.Master.FindControl("lblprintstk")).Text = "";
                this.lblprintstkl.Text = "";
                string value = this.RadioButtonList1.SelectedValue.ToString();

                DataSet ds1 = (DataSet)Session["alltable"];

                ////Update Sales
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                switch (value)
                {
                    case "0": //Sales update
                        dt = ((DataTable)ds1.Tables[0]).Copy();
                        this.Data_Bind("gvSalesUpdate", dt);

                        this.pnlUpSales.Visible = true;
                        this.pnlChequeDeposit.Visible = false;
                        // this.pnlMoneyRcptApp.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;

                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;

                        this.RadioButtonList1.Items[0].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;


                    case "1": //Chequd Deposit
                        dt = ((DataTable)ds1.Tables[1]).Copy();
                        this.Data_Bind("gcchqdeposit", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = true;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[1].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;


                    case "2":
                        ////Update Collection
                        dt = ((DataTable)ds1.Tables[2]).Copy();
                        this.Data_Bind("gvCollUpdate", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = true;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[2].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "3": //Purchase update

                        dt = ((DataTable)ds1.Tables[3]).Copy();
                        this.Data_Bind("gvPurchase", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = true;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[3].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "4": //Contractor update

                        dt = ((DataTable)ds1.Tables[4]).Copy();
                        this.Data_Bind("gvConUpdate", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = true;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[4].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "5": //M transfer update               

                        dt = ((DataTable)ds1.Tables[5]).Copy();


                        this.Data_Bind("gvMatTrasfer", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = true;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[5].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;

                    case "6": //Pdc update

                        dt = ((DataTable)ds1.Tables[6]).Copy();
                        this.Data_Bind("dgPdc", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = true;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[6].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "7": //B rec update


                        dt = ((DataTable)ds1.Tables[7]).Copy();
                        this.Data_Bind("gvBankRec", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = true;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[7].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "8":
                        //Client Modification
                        dt = ((DataTable)ds1.Tables[8]).Copy();
                        this.Data_Bind("gvClientMod", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = true;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[8].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "9":

                        dt = ((DataTable)ds1.Tables[10]).Copy();
                        this.Data_Bind("gvOthBillUp", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = true;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[9].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "10":
                        ////LSD
                        dt = ((DataTable)ds1.Tables[11]).Copy();
                        this.Data_Bind("gvlsdUp", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = true;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[10].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;
                    case "11":
                        ////patty Cash
                        dt = ((DataTable)ds1.Tables[12]).Copy();
                        this.Data_Bind("gvpetty", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = true;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.RadioButtonList1.Items[11].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;


                    case "12":
                        ////patty Cash
                        dt = ((DataTable)ds1.Tables[13]).Copy();
                        this.Data_Bind("gvAccUnPosted", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = true;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[12].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;

                    case "13":
                        dt = ((DataTable)ds1.Tables[14]).Copy();
                        this.Data_Bind("grvMatConversion", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = true;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[13].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                        break;


                    case "14":
                        dt = ((DataTable)ds1.Tables[15]).Copy();
                        this.Data_Bind("gvAccUnPostedtrn", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = true;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[14].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                        break;
                    case "15":
                        dt = ((DataTable)ds1.Tables[16]).Copy();
                        this.Data_Bind("gvIndAp", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = true;
                        this.pnlMatIssue.Visible = false;
                        this.RadioButtonList1.Items[15].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
                        break;

                    case "16":
                        dt = ((DataTable)ds1.Tables[17]).Copy();
                        this.Data_Bind("gvMatIssue", dt);
                        this.pnlUpSales.Visible = false;
                        this.pnlChequeDeposit.Visible = false;
                        this.pnlUpColl.Visible = false;
                        this.PanlUpCon.Visible = false;
                        this.pnlPurchase.Visible = false;
                        this.PnlBRec.Visible = false;
                        this.PnlPDC.Visible = false;
                        this.PnlMTras.Visible = false;
                        this.PnlClientMod.Visible = false;
                        this.PnlotherBill.Visible = false;
                        this.pnllsd.Visible = false;
                        this.pnlPatCash.Visible = false;
                        this.pnlUnposted.Visible = false;
                        this.pnlMAtConversion.Visible = false;
                        this.pnltrnUnit.Visible = false;
                        this.PanelIndAp.Visible = false;
                        this.pnlMatIssue.Visible = true;
                        this.RadioButtonList1.Items[16].Attributes["style"] =
                            "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

                        break;
                }

                ds1.Dispose();
            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
        }

        //protected void gvSalesUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string astatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sostatus")).ToString();
        //        //   HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEditIN");
        //        HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

        //        Hashtable hst = (Hashtable)Session["tblLogin"];
        //        string comcod = hst["comcod"].ToString();
        //        string centrid = ""; //Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
        //        string orderno = ""; //Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();



        //        if (astatus != "Approved")
        //        {

        //            hlink2.Font.Bold = true;
        //            hlink2.Style.Add("color", "Red");
        //            hlink2.ToolTip = "Approval";
        //            hlink2.NavigateUrl = "SalesOrderApproval?Type=Ind&centrid=" + centrid + "&orderno=" + orderno;

        //        }
        //    }
        //}

        protected void gvCollUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string chqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();
                hlink1.NavigateUrl = "~/F_17_Acc/AccSales?Type=Entry&prjcode=" + pactcode + "&chqno=" + chqno;
            }
        }

        protected void gvContUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string Date1 = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "billdate")).ToString("dd-MMM-yyyy");



                hlink2.Font.Bold = true;
                hlink2.Style.Add("color", "Blue");
                hlink2.ToolTip = "DO Edit";
                hlink2.NavigateUrl = "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=" + billno + "&Date1=" + Date1;


            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string astatus = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sostatus")).ToString();
            //    HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnEditAp");
            //    HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp2");

            //    Hashtable hst = (Hashtable)Session["tblLogin"];
            //    string comcod = hst["comcod"].ToString();
            //    string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
            //    string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();
            //    LinkButton lbtn1 = (LinkButton)e.Row.FindControl("btnDelRev");

            //    if (astatus == "In-process")
            //    {
            //        hlink1.Font.Bold = true;
            //        hlink1.Style.Add("color", "Red");
            //        hlink2.ToolTip = "Edit";
            //        if (orderno.Length != 0)
            //        {
            //            hlink1.NavigateUrl = "SalesOrder?Type=Edit&centrid=" + centrid + "&orderno=" + orderno;
            //        }
            //    }

            //    if (astatus == "In-process")
            //    {

            //        hlink2.Font.Bold = true;
            //        hlink2.Style.Add("color", "Red");
            //        hlink2.ToolTip = "Approval";
            //        hlink2.NavigateUrl = "SalesOrderApproval?Type=Ind&centrid="+ centrid +"&orderno=" + orderno;

            //    }

            //    else
            //    {
            //        lbtn1.Enabled = false;
            //    }
            //}
        }

        protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnPrintBill");
                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnEditRD");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "billno")).ToString();
                string ssircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ssircode")).ToString();
                string Date1 = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "billdat")).ToString("dd.MM.yyyy");


                hlink2.Font.Bold = true;
                hlink2.Style.Add("color", "Blue");
                hlink2.ToolTip = "DO Edit";

                hlink2.NavigateUrl = "~/F_17_Acc/AccPurchase?Type=Entry&genno=" + billno + "&ssircode=" + ssircode + "&Date1=" + Date1;
                hlink1.NavigateUrl = "~/F_14_Pro/PurBillEntry?Type=BillPrint&genno=" + billno + "&Date1=" + Date1;


            }
        }
        protected void gvOthBillUp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "reqno")).ToString();
                string Date1 = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "reqdat")).ToString("dd-MMM-yyyy");



                hlink2.Font.Bold = true;
                hlink2.Style.Add("color", "Blue");
                hlink2.ToolTip = "DO Edit";
                hlink2.NavigateUrl = "~/F_17_Acc/AccPurchaseOth?Type=Entry&genno=" + billno + "&Date1=" + Date1;


            }



        }
        protected void gvlsdUp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string billno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "lsdno")).ToString();
                string Date1 = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "lsddate")).ToString("dd-MMM-yyyy");



                hlink2.Font.Bold = true;
                hlink2.Style.Add("color", "Blue");
                hlink2.ToolTip = "DO Edit";
                hlink2.NavigateUrl = "~/F_17_Acc/AccDamageAndLost?Type=Entry&genno=" + billno + "&Date1=" + Date1;
            }
        }


        protected void grvMatConversion_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnAppMC");

                string conversionno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "convrno")).ToString();
                string Date1 = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "convrdat")).ToString("dd-MMM-yyyy");

                hlink2.Font.Bold = true;
                hlink2.Style.Add("color", "Blue");
                hlink2.ToolTip = "Update";
                hlink2.NavigateUrl = "~/F_17_Acc/AccMatConversion?Type=Entry&genno=" + conversionno + "&Date1=" + Date1;

            }








        }




        protected void gvpetty_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;


            HyperLink applink = (HyperLink)e.Row.FindControl("HypApprv");

            HyperLink updatelink = (HyperLink)e.Row.FindControl("HypUpdate");

            applink.Font.Bold = true;
            applink.Style.Add("color", "Blue");
            applink.ToolTip = "Approved";

            updatelink.Font.Bold = true;
            updatelink.Style.Add("color", "Blue");
            updatelink.ToolTip = "Update";

            string pcblno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pcblno")).ToString();
            string billdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "billdate")).ToString("dd-MMM-yyyy");
            applink.NavigateUrl = "~/F_17_Acc/AccPettyCashApp?Type=Entry&genno=" + pcblno + "&date=" + billdate;
            updatelink.NavigateUrl = "~/F_17_Acc/AccTopPageUpdate?Type=Entry";



        }







        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            int i = 0;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();

            foreach (DataRow dr1 in dt1.Rows)
            {
                if (i == 0)
                {


                    pactcode = dr1["pactcode"].ToString();
                    i++;
                    continue;
                }

                if (dr1["pactcode"].ToString() == pactcode)
                {

                    dr1["actdesc"] = "";

                }


                pactcode = dr1["pactcode"].ToString();
            }



            return dt1;

        }

        private void Data_Bind(string gv, DataTable dt)
        {

            try
            {
                switch (gv)
                {
                    case "gvSalesUpdate":
                        this.gvSalesUpdate.DataSource = HiddenSameData(dt);
                        this.gvSalesUpdate.DataBind();
                        if (dt.Rows.Count == 0)
                            return;

                        for (int i = 0; i < gvSalesUpdate.Rows.Count; i++)
                        {
                            string pactcode = ((Label)gvSalesUpdate.Rows[i].FindControl("lblgvpactcode")).Text.Trim();
                            string usircode = ((Label)gvSalesUpdate.Rows[i].FindControl("lblgvusircode")).Text.Trim();

                            LinkButton lbtn1 = (LinkButton)gvSalesUpdate.Rows[i].FindControl("lnkbtnPrint");
                            if (lbtn1 != null)
                                if (lbtn1.Text.Trim().Length > 0)
                                    lbtn1.CommandArgument = pactcode + usircode;
                        }

                        break;


                    case "gcchqdeposit":

                        this.gvchqdeposit.DataSource = dt;
                        this.gvchqdeposit.DataBind();
                        if (dt.Rows.Count > 0)
                            ((Label)this.gvchqdeposit.FooterRow.FindControl("lgvFdramt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(paidamt)", ""))
                                    ? 0
                                    : dt.Compute("sum(paidamt)", ""))).ToString("#,##0;(#,##0); ");

                        break;

                    case "gvCollUpdate":
                        this.gvCollUpdate.DataSource = HiddenSameData(dt);
                        this.gvCollUpdate.DataBind();

                        if (dt.Rows.Count > 0)
                            ((Label)this.gvCollUpdate.FooterRow.FindControl("lblFINgvitmamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(cramt)", ""))
                                    ? 0
                                    : dt.Compute("sum(cramt)", ""))).ToString("#,##0.00;(#,##0.00); ");
                        //((Label)this.gvCollUpdate.FooterRow.FindControl("lblINAmtTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cramt)", "")) ?
                        //0 : dt.Compute("sum(cramt)", ""))).ToString("#,##0.00;(#,##0.00);");

                        //((Label)this.gvCollUpdate.FooterRow.FindControl("lblINPTotal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ?
                        //0 : dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00);");
                        //for (int i = 0; i < gvCollUpdate.Rows.Count; i++)
                        //{
                        //    string Centrid = ((Label)gvCollUpdate.Rows[i].FindControl("lblgvINcentrid")).Text.Trim();
                        //    string Orderno = ((Label)gvCollUpdate.Rows[i].FindControl("lgINcorderno")).Text.Trim();

                        //    LinkButton lbtn1 = (LinkButton)gvCollUpdate.Rows[i].FindControl("lnkbtnPrintIN");
                        //    if (lbtn1 != null)
                        //        if (lbtn1.Text.Trim().Length > 0)
                        //            lbtn1.CommandArgument = Centrid + Orderno;

                        //    LinkButton lbtn2 = (LinkButton)gvCollUpdate.Rows[i].FindControl("btnDelOrder");
                        //    if (lbtn2 != null)
                        //        if (lbtn2.Text.Trim().Length > 0)
                        //            lbtn2.CommandArgument = Centrid + Orderno;
                        //}

                        break;
                    case "gvPurchase":

                        this.gvPurchase.DataSource = HiddenSameData(dt);
                        this.gvPurchase.DataBind();

                        Hashtable hst = (Hashtable)Session["tblLogin"];
                        string comcod = hst["comcod"].ToString();
                        if (comcod == "3339")
                        {
                            gvPurchase.Columns[8].Visible = true;
                        }


                        if (dt.Rows.Count > 0)
                        {
                            ((Label)this.gvPurchase.FooterRow.FindControl("lblFPurbillamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(billamt)", ""))
                                    ? 0.00
                                    : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");

                            ((HyperLink)this.gvPurchase.HeaderRow.FindControl("hylbtn")).NavigateUrl =
                                "~/F_17_Acc/AccPurchase?Type=Entry&genno=";
                        }
                        else
                        {

                        }

                        break;

                    case "gvConUpdate":

                        this.gvContUpdate.DataSource = HiddenSameData(dt);
                        this.gvContUpdate.DataBind();
                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvContUpdate.FooterRow.FindControl("lblgvFlcamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(billamt)", ""))
                                    ? 0.00
                                    : dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                            ((HyperLink)this.gvContUpdate.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                                "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }


                        break;
                    case "gvMatTrasfer":

                        this.gvMatTrasfer.DataSource = HiddenSameData(dt);
                        this.gvMatTrasfer.DataBind();

                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.gvMatTrasfer.HeaderRow.FindControl("hyperMattrns")).NavigateUrl =
                                "~/F_17_Acc/AccTransfer?Type=Entry&genno=&Date1=";

                        ((Label)this.gvMatTrasfer.FooterRow.FindControl("lblFchlamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(trsnamt)", ""))
                                    ? 0
                                    : dt.Compute("sum(trsnamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        break;



                    case "dgPdc":
                        this.dgPdc.DataSource = dt;
                        this.dgPdc.DataBind();

                        var result = from r in dt.AsEnumerable()
                                     where r.Field<string>("typesum") != "ZZZZ"
                                     select r;
                        DataTable dtResult = result.CopyToDataTable();

                        if (dtResult.Rows.Count > 0)
                            ((Label)this.dgPdc.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dtResult.Compute("sum(cramt)", ""))
                                    ? 0
                                    : dtResult.Compute("sum(cramt)", ""))).ToString("#,##0;(#,##0); ");


                        return;

                    case "gvBankRec":
                        this.gvBankRec.DataSource = dt;
                        this.gvBankRec.DataBind();






                        return;

                    case "gvClientMod":

                        this.gvClientMod.DataSource = HiddenSameData(dt);
                        this.gvClientMod.DataBind();

                        if (dt.Rows.Count > 0)
                            ((Label)this.gvClientMod.FooterRow.FindControl("lblFsaladjamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(adamt)", ""))
                                    ? 0
                                    : dt.Compute("sum(adamt)", ""))).ToString("#,##0.00;(#,##0.00); ");


                        break;
                    case "gvOthBillUp":

                        this.gvOthBillUp.DataSource = (dt);
                        this.gvOthBillUp.DataBind();
                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvOthBillUp.FooterRow.FindControl("lblgvFreqamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(reqamt)", ""))
                                    ? 0.00
                                    : dt.Compute("sum(reqamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                            //((HyperLink)this.gvOthBillUp.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }


                        break;
                    case "gvlsdUp":

                        this.gvlsdUp.DataSource = HiddenSameData(dt);
                        this.gvlsdUp.DataBind();
                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvlsdUp.FooterRow.FindControl("lblgvFlsdamt")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(lsdamt)", ""))
                                    ? 0.00
                                    : dt.Compute("sum(lsdamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

                            //((HyperLink)this.gvOthBillUp.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }


                        break;
                    case "gvpetty":

                        this.gvpetty.DataSource = (dt);
                        this.gvpetty.DataBind();
                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvpetty.FooterRow.FindControl("lgvFAmount")).Text = Convert.ToDouble(
                                (Convert.IsDBNull(dt.Compute("sum(amount)", ""))
                                    ? 0.00
                                    : dt.Compute("sum(amount)", ""))).ToString("#,##0.00;(#,##0.00); ");

                            //((HyperLink)this.gvOthBillUp.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }


                        break;
                    case "gvAccUnPosted":

                        this.gvAccUnPosted.DataSource = dt;
                        this.gvAccUnPosted.DataBind();



                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvAccUnPosted.FooterRow.FindControl("lblgvFvouamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");

                            //((HyperLink)this.gvOthBillUp.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }


                        break;


                    case "grvMatConversion":
                        this.grvMatConversion.DataSource = dt;
                        this.grvMatConversion.DataBind();
                        break;

                    case "gvAccUnPostedtrn":
                        this.gvAccUnPostedtrn.DataSource = dt;
                        this.gvAccUnPostedtrn.DataBind();



                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvAccUnPostedtrn.FooterRow.FindControl("lblgvFvouamttrn")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");

                            //((HyperLink)this.gvOthBillUp.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }
                        break;

                    case "gvIndAp":
                        this.gvIndAp.DataSource = dt;
                        this.gvIndAp.DataBind();
                        break;

                    case "gvMatIssue":
                        this.gvMatIssue.DataSource = dt;
                        this.gvMatIssue.DataBind();

                        if (dt.Rows.Count > 0)
                        {

                            ((Label)this.gvMatIssue.FooterRow.FindControl("lblgvFIssueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ?
                            0 : dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0);");

                            //((HyperLink)this.gvOthBillUp.HeaderRow.FindControl("hyconlbtn")).NavigateUrl =
                            //    "~/F_17_Acc/AccConBillUpdate?Type=Entry&genno=&Date1=";

                        }
                        break;
                        




                }
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }




        }


        protected void dgPdc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label prodesc = (Label)e.Row.FindControl("lgactdesc");
                Label amt = (Label)e.Row.FindControl("lgvcramt");
                //Label sign = (Label)e.Row.FindControl("gvsign");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "typesum")).ToString().Trim();
                TextBox recondat = (TextBox)e.Row.FindControl("txtgvReconDat");
                CalendarExtender calrecondat = (CalendarExtender)e.Row.FindControl("CalendarExtender_txtgvReconDat");

                if (code == "")
                {
                    return;
                }

                else if (ASTUtility.Right(code, 1) == "Z")
                {
                    prodesc.Font.Bold = true;
                    amt.Font.Bold = true;
                    //sign.Font.Bold = true;
                    prodesc.Style.Add("text-align", "right");
                    recondat.ReadOnly = true;
                    calrecondat.Enabled = false;

                }


            }
        }

        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {


            ////int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            ////this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";

            ////string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            ////string centrid = ASTUtility.Left(code, 12);
            ////string orderno = ASTUtility.Right(code, 14);

            ////try
            ////{

            ////    string comcod = this.GetCompCode();
            ////    Hashtable hst = (Hashtable)Session["tblLogin"];
            ////    string comnam = hst["comnam"].ToString();
            ////    string compname = hst["compname"].ToString();
            ////    string comadd = hst["comadd1"].ToString();
            ////    string username = hst["username"].ToString();
            ////    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");




            ////    DataSet ds2 = accData.GetTransInfo(comcod, "dbo_Sales.SP_REPORT_SALES_INFO", "RPTCUSTINFORMATION", orderno, centrid, "", "", "", "", "", "", "");
            ////    if (ds2 == null)
            ////        return;
            ////    ReportDocument rptSOrder = new ReportDocument();
            ////    //string qType = this.Request.QueryString["Type"].ToString();
            ////    //if (qType == "dNote")
            ////    //{
            ////    //    rptSOrder = new RealERPRPT.R_23_SaM.RptSalDelNoteZelta();
            ////    //    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            ////    //    txtHeader.Text = "DELIVERY NOTE";
            ////    //}
            ////    //else
            ////    //{
            ////    rptSOrder = new RealERPRPT.R_23_SaM.RptSalOrdrZelta();
            ////    TextObject txtHeader = rptSOrder.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            ////    txtHeader.Text = "SALES ORDER";
            ////    // }


            ////    TextObject txtrptcomp = rptSOrder.ReportDefinition.ReportObjects["Company"] as TextObject;
            ////    txtrptcomp.Text = comnam;



            ////    TextObject txtCompAdd = rptSOrder.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            ////    txtCompAdd.Text = comadd;

            ////    TextObject txtsaledate = rptSOrder.ReportDefinition.ReportObjects["txtsaledate"] as TextObject;
            ////    txtsaledate.Text = ds2.Tables[2].Rows[0]["orderdat"].ToString().Trim();

            ////    TextObject txtCust = rptSOrder.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            ////    txtCust.Text = ds2.Tables[2].Rows[0]["name"].ToString().Trim();

            ////    TextObject txtAdd = rptSOrder.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            ////    txtAdd.Text = ds2.Tables[2].Rows[0]["addr"].ToString().Trim();

            ////    TextObject txtPhone = rptSOrder.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            ////    txtPhone.Text = ds2.Tables[2].Rows[0]["phone"].ToString().Trim();

            ////    TextObject txtTrans = rptSOrder.ReportDefinition.ReportObjects["txtTrans"] as TextObject;
            ////    txtTrans.Text = ds2.Tables[0].Rows[0]["courie"].ToString().Trim();

            ////    TextObject txtStore = rptSOrder.ReportDefinition.ReportObjects["txtStore"] as TextObject;
            ////    txtStore.Text = ds2.Tables[2].Rows[0]["storename"].ToString().Trim();


            ////    TextObject txtCode = rptSOrder.ReportDefinition.ReportObjects["txtCode"] as TextObject;
            ////    txtCode.Text = ds2.Tables[2].Rows[0]["sirtdes"].ToString().Trim();

            ////    TextObject txtOrdTime = rptSOrder.ReportDefinition.ReportObjects["txtOrdTime"] as TextObject;
            ////    txtOrdTime.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["posteddat"]).ToString("hh:mm:ss tt").Trim();

            ////    TextObject txtRemarks = rptSOrder.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            ////    txtRemarks.Text = (ds2.Tables[2].Rows[0]["narration"]).ToString().Trim();

            ////    TextObject txtsaleNo = rptSOrder.ReportDefinition.ReportObjects["txtsaleNo"] as TextObject;
            ////    txtsaleNo.Text = orderno;
            ////    //BALANCE 

            ////    DataTable dt = ds2.Tables[0];
            ////    DataTable dt2 = ds2.Tables[1];
            ////    DataTable dt3 = ds2.Tables[2];

            ////    double oStdAmt, Dipsamt, ordAmt, balAmt;
            ////    oStdAmt = Convert.ToDouble((Convert.IsDBNull(dt3.Compute("sum(dues)", "")) ? 0.00 : dt3.Compute("sum(dues)", "")));
            ////    ordAmt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tamount)", "")) ? 0.00 : dt.Compute("sum(tamount)", "")));
            ////    Dipsamt = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(paidamt)", "")) ? 0.00 : dt2.Compute("sum(paidamt)", "")));

            ////    balAmt = (oStdAmt + ordAmt) - Dipsamt;
            ////    //if (qType == "All")
            ////    //{
            ////    TextObject txtOutStdBal = rptSOrder.ReportDefinition.ReportObjects["txtOutStdBal"] as TextObject;
            ////    txtOutStdBal.Text = oStdAmt.ToString("#,##0.00;(#,##0.00);");

            ////    TextObject txtDipositeAmt = rptSOrder.ReportDefinition.ReportObjects["txtDipositeAmt"] as TextObject;
            ////    txtDipositeAmt.Text = Dipsamt.ToString("#,##0.00;(#,##0.00);");

            ////    TextObject txtOrderAmt = rptSOrder.ReportDefinition.ReportObjects["txtOrderAmt"] as TextObject;
            ////    txtOrderAmt.Text = ordAmt.ToString("#,##0.00;(#,##0.00);");

            ////    TextObject txtBalanceAmt = rptSOrder.ReportDefinition.ReportObjects["txtBalanceAmt"] as TextObject;
            ////    txtBalanceAmt.Text = balAmt.ToString("#,##0.00;(#,##0.00);");
            ////    //}

            ////    TextObject txtAppby = rptSOrder.ReportDefinition.ReportObjects["txtAppby"] as TextObject;
            ////    txtAppby.Text = ds2.Tables[2].Rows[0]["appby"].ToString().Trim();

            ////    TextObject txtPreBy = rptSOrder.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            ////    txtPreBy.Text = ds2.Tables[0].Rows[0]["usrname"].ToString().Trim();

            ////    TextObject txtuserinfo = rptSOrder.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////    rptSOrder.SetDataSource(ds2.Tables[0]);

            ////    // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource((DataTable)ViewState["tblpaysch"]);

            ////    // rptSOrder.Subreports["RptSaleOrderPaymentInfo.rpt"].SetDataSource(ds2.Tables[1]);


            ////    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////    rptSOrder.SetParameterValue("ComLogo", ComLogo);

            ////    Session["Report1"] = rptSOrder;

            ////    this.lblprintstkl.Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            ////                 "PDF" + "', target='_blank');</script>";


            ////    Common.LogStatus("Order", "Order Print", "Order No: ", orderno + " - " + centrid);

            ////}
            ////catch (Exception ex)
            ////{

            ////}

        }

        protected void lnkbtnView_Click(object sender, EventArgs e)
        {
            ////int rbtIndex = Convert.ToInt16(this.RadioButtonList1.SelectedIndex);
            ////this.RadioButtonList1.Items[rbtIndex].Attributes["style"] = "background: #189697; display:block; -webkit-border-radius: 30px;-moz-border-radius: 30px;border-radius: 30px;";
            ////string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            ////string centrid = ASTUtility.Left(code, 12);
            ////string Delorderno = ASTUtility.Right(code, 14);

            ////if (Delorderno.Length == 0)
            ////    return;
            ////try
            ////{
            ////    string comcod = this.GetCompCode();
            ////    Hashtable hst = (Hashtable)Session["tblLogin"];
            ////    string comnam = hst["comnam"].ToString();
            ////    string compname = hst["compname"].ToString();
            ////    string comadd = hst["comadd1"].ToString();
            ////    string username = hst["username"].ToString();
            ////    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ////    DataSet ds = accData.GetTransInfo(comcod, "SP_REPORT_CHALLAN", "PRINTDELIVERYCHALLAN", Delorderno, centrid, "", "", "", "", "", "", "");

            ////    double Qty = Convert.ToDouble(ds.Tables[0].Rows[0]["delqty"]);
            ////    //double Vat = Convert.ToDouble((Convert.IsDBNull(ds.Tables[0].Compute("sum(vat)", "")) ? 0.00 : ds.Tables[0].Compute("sum(vat)", "")));

            ////    ReportDocument rptChallan = new RealERPRPT.R_23_SaM.RptDelChallanZelta();

            ////    TextObject txtCompAdd = rptChallan.ReportDefinition.ReportObjects["txtCompAdd"] as TextObject;
            ////    txtCompAdd.Text = comnam + "\n" + "Corporate Office" + "\n" + comadd;
            ////    TextObject txtrptHeader = rptChallan.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            ////    txtrptHeader.Text = "DELIVERY CHALLAN";

            ////    TextObject txtDelNo = rptChallan.ReportDefinition.ReportObjects["txtDelNo"] as TextObject;
            ////    txtDelNo.Text = Delorderno;// "DO" + sdelno.Substring(3);
            ////    TextObject txtChallan = rptChallan.ReportDefinition.ReportObjects["txtChallan"] as TextObject;
            ////    txtChallan.Text = ds.Tables[1].Rows[0]["orderno"].ToString();
            ////    TextObject txtDate = rptChallan.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            ////    txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["memodat"]).ToString("dd-MMM-yyyy");
            ////    //TextObject txtOrder = rptChallan.ReportDefinition.ReportObjects["txtOrder"] as TextObject;
            ////    //txtOrder.Text = (ds.Tables[2].Rows[0]["orderno"].ToString() == "00000000000000") ? "CURRENT SALES" :
            ////    //    ASTUtility.Left(ds.Tables[2].Rows[0]["orderno"].ToString(), 2) + ds.Tables[2].Rows[0]["orderno"].ToString().Substring(7, 2) + "-" + ASTUtility.Right(ds.Tables[2].Rows[0]["orderno"].ToString(), 5); ;

            ////    TextObject txtCust = rptChallan.ReportDefinition.ReportObjects["txtCust"] as TextObject;
            ////    txtCust.Text = ds.Tables[1].Rows[0]["custname"].ToString();
            ////    TextObject txtCustadd = rptChallan.ReportDefinition.ReportObjects["txtAdd"] as TextObject;
            ////    txtCustadd.Text = ds.Tables[1].Rows[0]["custadd"].ToString();
            ////    TextObject txtPhone = rptChallan.ReportDefinition.ReportObjects["txtPhone"] as TextObject;
            ////    txtPhone.Text = ds.Tables[1].Rows[0]["custphone"].ToString();

            ////    TextObject txtBag = rptChallan.ReportDefinition.ReportObjects["txtBag"] as TextObject;
            ////    txtBag.Text = Convert.ToDouble(ds.Tables[1].Rows[0]["bagqty"]).ToString("#,##0;(#,##0);");

            ////    TextObject txtSsirdesc = rptChallan.ReportDefinition.ReportObjects["txtSsirdesc"] as TextObject;
            ////    txtSsirdesc.Text = ds.Tables[1].Rows[0]["ssirdesc"].ToString();

            ////    TextObject txtRemarks = rptChallan.ReportDefinition.ReportObjects["txtRemarks"] as TextObject;
            ////    txtRemarks.Text = ds.Tables[1].Rows[0]["narr"].ToString();

            ////    TextObject txtOrdTime = rptChallan.ReportDefinition.ReportObjects["txtDelTime"] as TextObject;
            ////    txtOrdTime.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["posteddat"].ToString()).ToString("hh:mm:ss tt");
            ////    TextObject txtuserinfo = rptChallan.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            ////    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            ////    rptChallan.SetDataSource(ds.Tables[0]);
            ////    TextObject txtPreBy = rptChallan.ReportDefinition.ReportObjects["txtPreBy"] as TextObject;
            ////    txtPreBy.Text = ds.Tables[1].Rows[0]["username"].ToString();

            ////    TextObject txtDesBy = rptChallan.ReportDefinition.ReportObjects["txtDesBy"] as TextObject;
            ////    txtDesBy.Text = ds.Tables[1].Rows[0]["apusername"].ToString();

            ////    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////    rptChallan.SetParameterValue("ComLogo", ComLogo);

            ////    Session["Report1"] = rptChallan;
            ////    this.lblprintstkl.Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            ////                 "PDF" + "', target='_blank');</script>";
            ////    //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer?PrintOpt=" +
            ////    //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            ////    ///
            ////    if (ConstantInfo.LogStatus == true)
            ////    {
            ////        string eventtype = "Delivery ORDER";
            ////        string eventdesc = "Print Report";
            ////        string eventdesc2 = "Del No : " + Delorderno;
            ////        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            ////    }


            ////}
            ////catch (Exception ex)
            ////{

            ////}
        }

        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string orderno = ASTUtility.Right(code, 14);

            //if (RDsostatus != "Approved")
            //    return;

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERLASTAPPDELETE", centrid,
                orderno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }

        protected void btnDelRev_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();

            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string orderno = ASTUtility.Right(code, 14);
            if (orderno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');",
                    true);
                return;
            }

            DataSet ds = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "CHKFINALAPP", usrid, centrid, "", "",
                "");
            if (ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You Have no Permission');", true);
                return;
            }

            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_APPROVAL", "ORDERAPPDELETE", centrid,
                orderno, "", "", "");

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Order Reverse Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "Order Reverse", "Order No: ", orderno + " - " + centrid);
        }

        protected void btnDelDO_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string usrid = hst["usrid"].ToString();

            string code = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            string centrid = ASTUtility.Left(code, 12);
            string Delorderno = ASTUtility.Right(code, 14);
            if (Delorderno.Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select your item for Delete');",
                    true);
                return;
            }


            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "SHOWDELIVERYORDER", centrid, Delorderno);

            DataSet ds = lst.GetDataSetForXmlDo(ds1, centrid, Delorderno);

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds, null, null, centrid,
                Delorderno);

            if (!resulta)
            {

                return;
            }
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALES_ORDER_02", "DELETEDOLIST", centrid, Delorderno, "",
                "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('DO Delete Successfully');", true);
            }
            Common.LogStatus("Sales Interface", "DO Delete", "DO No: ", Delorderno + " - " + centrid);
        }

        protected void gvSalesUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                // HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnConso");

                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string date = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "schdate")).ToString("dd-MMM-yyyy");
                string schcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "schcode")).ToString();
                string dgno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dgno")).ToString();
                if (dgno == "-1")
                {
                    hlink1.NavigateUrl = "~/F_17_Acc/AccSalJournal?Type=Details&prjcode=" + pactcode + "&usircode=" + usircode + "&Date1=" + date + "&schcode=" + schcode;
                }
                else
                {
                    hlink1.NavigateUrl = "~/F_17_Acc/AccSalJournal?Type=Complaint&DgNo=" + dgno+ "&prjcode=" + pactcode + "&usircode=" + usircode + "&Date1=" + date + "&schcode=" + schcode;
                }
            }

        }

        protected void gvchqdeposit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnAppcdep");
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
                string chqno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();
                hlink1.NavigateUrl = "~/F_17_Acc/AccChqueDeposit?Type=ChquedepEntry&prjcode=" + pactcode + "&chqno=" +
                                     chqno + "&usircode=" + usircode;
            }
        }

        protected void btnDelPurchase_Click(object sender, EventArgs e)
        {
            string url = "PurBillEntry?Type=BillEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string billno = ((Label)this.gvPurchase.Rows[RowIndex].FindControl("lblgvbillno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", billno, "",
                "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result = this.XmlDataInsert(billno, ds1);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELPURBILLINFO", billno, "", "", "",
                "", "", "", "", "", "", "", "", "", "", "");
            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);



        }


        private bool XmlDataInsert(string Reqno, DataSet ds)
        {
            //Log Data
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string usrid = hst["usrid"].ToString();
            string trmnid = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string Date = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            DataSet ds1 = new DataSet("ds1");

            //DataTable dt1 = new DataTable();
            //ds.Tables[2]..Columns.Add("delbyid", typeof(string));
            //ds.Tables[2].Columns.Add("delseson", typeof(string));
            //ds.Tables[2].Columns.Add("deltrmnid", typeof(string));
            //ds.Tables[2].Columns.Add("deldate", typeof(DateTime));
            //ds.Tables[2].Rows[0]["delbyid"] = usrid;
            //ds.Tables[2].Rows[0]["delseson"] = session;
            //ds.Tables[2].Rows[0]["deltrmnid"] = trmnid;
            //ds.Tables[2].Rows[0]["deldate"] = Date;



            DataTable dt1 = new DataTable();
            dt1.Columns.Add("delbyid", typeof(string));
            dt1.Columns.Add("delseson", typeof(string));
            dt1.Columns.Add("deltrmnid", typeof(string));
            dt1.Columns.Add("deldate", typeof(DateTime));

            DataRow dr1 = dt1.NewRow();
            dr1["delbyid"] = usrid;
            dr1["delseson"] = session;
            dr1["deltrmnid"] = trmnid;
            dr1["deldate"] = Date;
            dt1.Rows.Add(dr1);
            dt1.TableName = "tbl1";

            ds1.Merge(dt1);
            ds1.Merge(ds.Tables[0]);
            ds1.Merge(ds.Tables[1]);
            ds1.Tables[0].TableName = "tbl1";
            ds1.Tables[1].TableName = "tbl2";
            ds1.Tables[2].TableName = "tbl3";

            bool resulta = accData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }

        protected void gvMatTrasfer_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnApp");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string trnno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "trnno")).ToString();
                string Date1 = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "trnsdate")).ToString("dd-MMM-yyyy");

                hlink2.Font.Bold = true;
                hlink2.Style.Add("color", "Blue");
                hlink2.ToolTip = "DO Edit";
                hlink2.NavigateUrl = "~/F_17_Acc/AccTransfer?Type=Entry&genno=" + trnno + "&Date1=" + Date1;


            }


        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.PrintSalesJournal();
                    break;
                case "1":
                    this.PrintCheckDeposit();
                    break;

                case "2":
                    this.PrintUpdateCollection();
                    break;

                case "3":
                    this.PrintUpdatePurchase();
                    break;
                case "4":
                    this.PrintBillUpdate();
                    break;

                case "5":
                    this.PrintUpdateMatTrans();
                    break;

                    //case "6":
                    //    this.PrintUpdatePdc ();
                    //    break;
            }

        }

        private void PrintSalesJournal()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["alltable"];
            DataTable dt = ds1.Tables[0];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.SalesJournal>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptSalesJournal", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";



        }

        private void PrintCheckDeposit()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["alltable"];
            DataTable dt = ds1.Tables[1];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptCkhDeposit>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptCkhDeposit", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";
        }

        private void PrintUpdateCollection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["alltable"];
            DataTable dt = ds1.Tables[2];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptUpdateCol>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptUpdateCol", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";
        }

        private void PrintUpdatePurchase()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["alltable"];
            DataTable dt = ds1.Tables[3];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptUpdatePur>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptUpdatePur", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";
        }

        private void PrintBillUpdate()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["alltable"];
            DataTable dt = ds1.Tables[4];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptConBillUp>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptConBillUp", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";
        }

        private void PrintUpdateMatTrans()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            //string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string hostname = hst["hostname"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " +
                                 username + " ,Time: " + printdate;


            DataSet ds1 = (DataSet)Session["alltable"];
            DataTable dt = ds1.Tables[5];


            var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptUpdateMatTrans>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_99_AllInterface.RptUpdateMatTrans", list, null, null);

            Rpt1.SetParameters(new ReportParameter("txtCom", comnam));
            Rpt1.SetParameters(new ReportParameter("txtAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));



            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text =
                @"<script>window.open('../RDLCViewer?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() +
                "', target='_blank');</script>";
        }

        //private void PrintUpdatePdc()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString ();
        //    //string comcod = hst["comcod"].ToString();
        //    string comadd = hst["comadd1"].ToString ();
        //    string compname = hst["compname"].ToString ();
        //    string session = hst["session"].ToString ();
        //    string username = hst["username"].ToString ();
        //    //string hostname = hst["hostname"].ToString();
        //    string printdate = System.DateTime.Now.ToString ("dd.MM.yyyy hh:mm:ss tt");
        //    string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


        //    DataSet ds1 = (DataSet)Session["alltable"];
        //    DataTable dt = ds1.Tables[6];


        //   // var list = dt.DataTableToList<RealEntity.C_99_AllInterface.RptUpdatePdc> ();
        //    LocalReport Rpt1 = new LocalReport ();
        //    //Rpt1 = RDLCAccountSetup.GetLocalReport ("F_99_AllInterface.RptUpdatePdc", list, null, null);

        //    Rpt1.SetParameters (new ReportParameter ("txtCom", comnam));
        //    Rpt1.SetParameters (new ReportParameter ("txtAdd", comadd));
        //    Rpt1.SetParameters (new ReportParameter ("printFooter", printFooter));



        //    Session["Report1"] = Rpt1;
        //    ((Label)this.Master.FindControl ("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer?PrintOpt=" +
        //                ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";
        //}

        protected void btnDelOrder_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblupdatecol"];
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string mrno = ((Label)this.gvCollUpdate.Rows[index].FindControl("lgINmrno")).Text.ToString();
            string chkno = ((Label)this.gvCollUpdate.Rows[index].FindControl("lblgvchqno")).Text.ToString();
            bool result;
            result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "DELETECHQ", mrno, chkno, "", "", "",
                "", "", "", "");
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Delete Successfully');", true);
                dt.Rows[index].Delete();
            }


            Session["tblupdatecol"] = dt;

            this.Data_Bind("gvCollUpdate", dt);
        }



        protected void lnkbtnMAppcdep_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblapprecpt"];
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string mrno = dt.Rows[Rowindex]["mrno"].ToString();
            string chqno = dt.Rows[Rowindex]["chqno"].ToString();

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string appid = hst["usrid"].ToString();
            string appsession = hst["session"].ToString();
            string Terminal = hst["compname"].ToString();


            string appdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            bool result = accData.UpdateTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "INSERTRECPTAPPROVAL", appid, appdate,
               appsession, Terminal, chqno, mrno, "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Update Failed !!!');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Update Successfully !!!.');", true);
            }

            this.RadioButtonList1.SelectedIndex = 0;
        }
        protected void btnDelConBill_Click(object sender, EventArgs e)
        {

            string url = "PurSubConBillFinal?Type=BillEntry";
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataRow[] dr1 = ASTUtility.PagePermission1(url, (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["delete"]))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('You have no permission');", true);
                return;
            }
            int RowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            string billno = ((Label)this.gvContUpdate.Rows[RowIndex].FindControl("lgAPPcorderno")).Text.Trim();


            //accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_INTERFACE", "RPTACCOUNTDASHBOARD", "%", Date, "%", "", "", "", "", "", "");

            //DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", billno, "",
            //    "", "", "", "", "", "", "");
            //if (ds1 == null)
            //    return;


            //bool result = this.XmlDataInsert(billno, ds1);

            //if (!result)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
            //    return;

            //}


            bool resulbill = accData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELCONBILLINFO", billno, "", "", "",
                "", "", "", "", "", "", "", "", "", "", "");
            if (!resulbill)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted  Fail');", true);
                return;
            }
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted Successfully.');", true);



        }
        protected void gvClientMod_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlink1 = (HyperLink)e.Row.FindControl("lnkbtnApp");
                //string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string adno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();
                hlink1.NavigateUrl = "~/F_17_Acc/AccSalesADandDelay?Type=Entry&genno=" + adno;
            }
        }

        protected void gvPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds1 = (DataSet)Session["alltable"];
            this.gvPurchase.PageIndex = e.NewPageIndex;
            DataTable dt = ((DataTable)ds1.Tables[3]).Copy();
            this.Data_Bind("gvPurchase", dt);
        }
        protected void lbtnVoucherApp_Click(object sender, EventArgs e)
        {



            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["alltable"];
            //  DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            ds1.Tables[13].Rows[index]["chkmv"] = "True";


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string vounum = ds1.Tables[13].Rows[index]["vounum"].ToString();

            string ApprovedByid = hst["usrid"].ToString();
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Existing Voucher

            DataSet dse = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");

            if (dse.Tables[0].Rows.Count > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Already Posted";
                return;

            }


            bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                               "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //Session.Remove("alltable");
            //DataView dv = ds1;
            //Session["tblunposted"] = dv.ToTable();
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }


        protected void gvAccUnPosted_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrint");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint?Type=accVou&vounum=" + vounum;

            }



        }


        protected void gvAccUnPostedtrn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkVoucherPrinttrn");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();

                hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint?Type=accVou&vounum=" + vounum;

            }
        }

        protected void gvIndAp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("lnkbtnAppIN");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString();
                string issuedat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issuedat1")).ToString();

                //hlink2.NavigateUrl = "~/F_12_Inv/Material_Issue?Type=Approve&genno=" + issueno + "&prjcode=&sircode=";
                hlink2.NavigateUrl = "~/F_17_Acc/AccIndentUpdate?Type=Entry&genno=" + issueno + "&date=" + issuedat;

            }
        }
        protected void lbtnVoucherApptrn_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataSet ds1 = (DataSet)Session["alltable"];
            //  DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            ds1.Tables[15].Rows[index]["chkmv"] = "True";


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string vounum = ds1.Tables[15].Rows[index]["vounum"].ToString();

            string ApprovedByid = hst["usrid"].ToString();
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Existing Voucher

            DataSet dse = accData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");

            if (dse.Tables[0].Rows.Count > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Already Posted";
                return;

            }


            bool resultb = accData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                               "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //Session.Remove("alltable");
            //DataView dv = ds1;
            //Session["tblunposted"] = dv.ToTable();
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);

        }

        protected void txtgvReconDat_TextChanged(object sender, EventArgs e)
        {
            int index = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;
            string voudat = ((Label)this.dgPdc.Rows[index].FindControl("lgvPVDate")).Text.Trim();
            string recondat = ((TextBox)this.dgPdc.Rows[index].FindControl("txtgvReconDat")).Text.Trim();
            DateTime dtvou = Convert.ToDateTime(voudat);
            DateTime dtrecon = Convert.ToDateTime(recondat);
            if (dtvou > dtrecon)
            {
                this.RiseError("Reconcilation Date Should be larger than Voucher Date");
            }
        }

        private void RiseError(string msg)
        {
            ((Label)this.Master.FindControl("lblmsg")).Text = msg;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        }

        protected void btnokpdc_Click(object sender, EventArgs e)
        {

        }

        protected void gvMatIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink2 = (HyperLink)e.Row.FindControl("hlnkMiasue");

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string issueno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issueno")).ToString();
                string issuedat = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "issuedat1")).ToString();

                //hlink2.NavigateUrl = "~/F_12_Inv/Material_Issue?Type=Approve&genno=" + issueno + "&prjcode=&sircode=";
                hlink2.NavigateUrl = "~/F_17_Acc/AccIssueUpdate?Type=Entry&genno=" + issueno + "&date=" + issuedat;

            }

        }
    }
}