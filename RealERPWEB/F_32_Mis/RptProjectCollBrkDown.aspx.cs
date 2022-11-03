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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_32_Mis
{ 

    
public partial class RptProjectCollBrkDown : System.Web.UI.Page
    {
        ProcessAccess MisData = new ProcessAccess();
        public double balamt = 0.000000;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "ClLedger") ? "Client Ledger Report" :
                    (this.Request.QueryString["Type"].ToString() == "SpLedger") ? "Subsidiary Ledger Report" : "PROJECT WISE COLLECTION BREAK DOWN REPORT";
                this.SelectView();


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowReport();
                    break;

                case "ClientLedger":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.PrintCleintLedger();
                    break;
                case "IndPrjStDet":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowIndPrjDet();
                    break;
                case "SpLedger":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowSPLedger();
                    break;
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    this.PrjWiseCollBrekDown();
                    break;

                case "ClientLedger":
                    this.PrintCleintLedger();
                    break;
                case "IndPrjStDet":
                    this.RptIndPrjStDet();
                    break;
                case "SpLedger":
                    this.RptSPLedger();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report";
                string eventdesc2 = this.Request.QueryString["Type"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }
        private void PrjWiseCollBrekDown()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.lblActDesc.Text.Trim();
            ReportDocument rpcp = new ReportDocument();
            if (ASTUtility.Left(Request.QueryString["pactcode"].ToString().Trim(), 2) == "41")
            {
                rpcp = new RealERPRPT.R_32_Mis.RptPrjWiseBrkDown();
            }
            else
            {
                rpcp = new RealERPRPT.R_32_Mis.RptPrjWiseBrkDown1();
            }
            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = "Project Name: " + prjname;
            TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "As on Date: " + Request.QueryString["Date1"].ToString().Trim(); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = this.lblHeadtitle.Text;
            //    string eventdesc = "Print Report";
            //    //string eventdesc2 = this.rbtnList1.SelectedItem.ToString();
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, "");
            //}

            rpcp.SetDataSource((DataTable)Session["tblPrjData"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private string ClientCalltype()
        {
            bool result = false;
            string Calltype = "";
            switch (result)
            {
                case true:
                    Calltype = "RPTCLIENTLEDGER";
                    break;

                default:
                    Calltype = "INSTALLMANTWITHMRR";
                    break;


            }


            return Calltype;

        }

        private void PrintCleintLedger()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string comcod = this.GetComCode();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string custid = this.Request.QueryString["usircode"].ToString();
            string pactcode = this.Request.QueryString["pactcode"].ToString();

            string Date = Convert.ToDateTime(this.Request.QueryString["Date1"]).ToString("dd-MMM-yyyy");

            string CallType = this.ClientCalltype();
            DataSet ds5 = MisData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", CallType, pactcode, custid, Date, "", "", "", "", "", "");

            DataTable tblins = this.HiddenSameDate2(ds5.Tables[0]);

            double aprment = Convert.ToDouble(ds5.Tables[1].Rows[0]["aptprice"]);
            double carparking = Convert.ToDouble(ds5.Tables[1].Rows[0]["carparking"]);
            double utility = Convert.ToDouble(ds5.Tables[1].Rows[0]["utility"]);
            double modicharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["adwrk"]);
            double delcharge = Convert.ToDouble(ds5.Tables[1].Rows[0]["delchg"]);
            double regisfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["regavat"]);
            double assciationfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["devcharge"]);
            double transfee = Convert.ToDouble(ds5.Tables[1].Rows[0]["transfee"]);
            double bgcost = Convert.ToDouble(ds5.Tables[1].Rows[0]["bgcost"]);
            //double proadd = Convert.ToDouble(ds5.Tables[1].Rows[0]["proadd"]);




            // rdlc start

            string rptwelfarefund = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            string rptothers = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");
            string rpttoprice = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            string rptcooperative = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");
            string rptdiscount = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");


            DataTable dtsum = ds5.Tables[2];
            double tsalevalue = Convert.ToDouble(ds5.Tables[1].Rows[0]["acprice"]);
            double treceived = Convert.ToDouble((Convert.IsDBNull(tblins.Compute("Sum(paidamt)", "")) ? 0.00
                    : tblins.Compute("Sum(paidamt)", "")));

            double fcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["fcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["fcheque"]) : 0.00;
            double retcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["retcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["retcheque"]) : 0.00;
            double pcheque = (dtsum.Rows.Count == 0) ? 0 : (Convert.ToDouble(dtsum.Rows[0]["pcheque"]) > 0) ? Convert.ToDouble(dtsum.Rows[0]["pcheque"]) : 0.00;
            double reconamt = treceived - (fcheque + retcheque + pcheque);
            double netbal = tsalevalue - reconamt;



            string rptunittype = ds5.Tables[1].Rows[0]["unittype"].ToString();
            string txtdate = "Print date: " + Date;
            string rptcustname = ds5.Tables[1].Rows[0]["name"].ToString();
            string rptcustadd = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            string rptcustphone = ds5.Tables[1].Rows[0]["telephone"].ToString();
            string rptpactdesc = ds5.Tables[1].Rows[0]["projectname"].ToString();
            string projadd = ds5.Tables[1].Rows[0]["proadd"].ToString();

            string rptunitdesc = ds5.Tables[1].Rows[0]["aptname"].ToString();
            string rptusize = ds5.Tables[1].Rows[0]["aptsize"].ToString();
            string rptsalesteam = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            string rptsalesdate = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            string rptagreementdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            string rpthandoverdate = (Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy") == "01-jan-1900") ? "" : Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            string rptdelcharge = (delcharge > 0) ? delcharge.ToString("#,##0;(#,##0); ") : "";

            //string txtearlyben = (ebenamt > 0) ? ("early benefit: " + ebenamt.ToString("#,##0;(#,##0); ")) : ""; ;

            string printFooter = ASTUtility.Concat(compname, username, printdate);
            string comlogo = new Uri(Server.MapPath(@"~\image\logo" + comcod + ".jpg")).AbsoluteUri;

            var lst = tblins.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassClientPayDetails>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptClientLedger02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("ComLogo", comlogo));
            Rpt1.SetParameters(new ReportParameter("CompName", comnam));
            Rpt1.SetParameters(new ReportParameter("Compadd", comadd));
            Rpt1.SetParameters(new ReportParameter("title", "Client Ledger"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtdate));

            Rpt1.SetParameters(new ReportParameter("rptcustname", rptcustname));
            Rpt1.SetParameters(new ReportParameter("rptCustAdd", rptcustadd));
            Rpt1.SetParameters(new ReportParameter("rptCustPhone", rptcustphone));
            Rpt1.SetParameters(new ReportParameter("rptpactdesc", rptpactdesc));
            Rpt1.SetParameters(new ReportParameter("projAddress", projadd));
            Rpt1.SetParameters(new ReportParameter("rptUnitDesc", rptunitdesc));
            Rpt1.SetParameters(new ReportParameter("rptUsize", rptusize));
            Rpt1.SetParameters(new ReportParameter("rptSalesteam", rptsalesteam));
            Rpt1.SetParameters(new ReportParameter("rptsalesdate", rptsalesdate));
            Rpt1.SetParameters(new ReportParameter("rptagreementdate", rptagreementdate));
            Rpt1.SetParameters(new ReportParameter("rptHandoverdate", rpthandoverdate));

            Rpt1.SetParameters(new ReportParameter("txtbgcost", bgcost.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptapartmentprice", aprment.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcarparking", carparking.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptUtility", utility.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("rptcooperative", rptcooperative));
            Rpt1.SetParameters(new ReportParameter("regisfee", regisfee.ToString("#,##0;(#,##0); ")));
            //Rpt1.SetParameters(new ReportParameter("transfee", transfee.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("modicharge", modicharge.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("welfarefund", rptwelfarefund));
            Rpt1.SetParameters(new ReportParameter("rptOthers", rptothers));
            Rpt1.SetParameters(new ReportParameter("receivableTotal", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("lessdisamt", rptdiscount));

            Rpt1.SetParameters(new ReportParameter("txttovalueamt", rpttoprice));
            Rpt1.SetParameters(new ReportParameter("txttoreceived", treceived.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtfcheque", fcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtrcheque", retcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtpcheque", pcheque.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetencash", reconamt.ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtnetbalance", netbal.ToString("#,##0;(#,##0); ")));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                    ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            //string custid = Request.QueryString["usircode"].ToString().Trim();
            //string Date = Request.QueryString["Date1"].ToString().Trim();
            //DataSet ds5 = MisData.GetTransInfo(comcod, "SP_REPORT_SALSMGT01", "INSTALLMANTWITHMRR", pactcode, custid, Date, "", "", "", "", "", "");
            //if (ds5.Tables[0].Rows.Count == 0)
            //    return;
            //DataTable tblins = this.HiddenSameData(ds5.Tables[0]);

            //this.LblPrjDesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
            //this.lblCustName.Text = ds5.Tables[1].Rows[0]["name"].ToString(); 
            //this.lblDate1.Text =Request.QueryString["Date1"].ToString().Trim();


            //ReportDocument rptStatus = new RealERPRPT.R_23_CR.RptClientLedger();
            //TextObject rpttxtCompanyName = rptStatus.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rpttxtCompanyName.Text = comnam;
            //TextObject rptcompadd = rptStatus.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //rptcompadd.Text = comadd;

            //TextObject txtDate = rptStatus.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtDate.Text = "Print Date: " + Request.QueryString["Date1"].ToString().Trim();
            //TextObject rptcustname = rptStatus.ReportDefinition.ReportObjects["custname"] as TextObject;
            //rptcustname.Text = ds5.Tables[1].Rows[0]["name"].ToString();
            //TextObject rptCustAdd = rptStatus.ReportDefinition.ReportObjects["CustAdd"] as TextObject;
            //rptCustAdd.Text = ds5.Tables[1].Rows[0]["peraddress"].ToString();
            //TextObject rptCustPhone = rptStatus.ReportDefinition.ReportObjects["CustPhone"] as TextObject;
            //rptCustPhone.Text = ds5.Tables[1].Rows[0]["telephone"].ToString();
            //TextObject rptpactdesc = rptStatus.ReportDefinition.ReportObjects["pactdesc"] as TextObject;
            //rptpactdesc.Text = ds5.Tables[1].Rows[0]["projectname"].ToString();
            //TextObject rptUnitDesc = rptStatus.ReportDefinition.ReportObjects["UnitDesc"] as TextObject;
            //rptUnitDesc.Text = ds5.Tables[1].Rows[0]["aptname"].ToString();
            //TextObject rptUsize = rptStatus.ReportDefinition.ReportObjects["usize"] as TextObject;
            //rptUsize.Text = ds5.Tables[1].Rows[0]["aptsize"].ToString();

            //TextObject rptSalesteam = rptStatus.ReportDefinition.ReportObjects["Salesteam"] as TextObject;
            //rptSalesteam.Text = ds5.Tables[1].Rows[0]["salesteam"].ToString();
            //TextObject rptsalesdate = rptStatus.ReportDefinition.ReportObjects["salesdate"] as TextObject;
            //rptsalesdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["saledate"]).ToString("dd-MMM-yyyy");
            //TextObject rptagreementdate = rptStatus.ReportDefinition.ReportObjects["agreementdate"] as TextObject;
            //rptagreementdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["agdate"]).ToString("dd-MMM-yyyy");
            //TextObject rptHandoverdate = rptStatus.ReportDefinition.ReportObjects["Handoverdate"] as TextObject;
            //rptHandoverdate.Text = Convert.ToDateTime(ds5.Tables[1].Rows[0]["hoverdate"]).ToString("dd-MMM-yyyy");

            //TextObject rptapartmentprice = rptStatus.ReportDefinition.ReportObjects["apartmentprice"] as TextObject;
            //rptapartmentprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["aptprice"]).ToString("#,##0;(#,##0); ");
            //TextObject rptcarparking = rptStatus.ReportDefinition.ReportObjects["carparking"] as TextObject;
            //rptcarparking.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["carparking"]).ToString("#,##0;(#,##0); ");
            //TextObject rptUtility = rptStatus.ReportDefinition.ReportObjects["Utility"] as TextObject;
            //rptUtility.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["utility"]).ToString("#,##0;(#,##0); ");
            //TextObject rptregistration = rptStatus.ReportDefinition.ReportObjects["registration"] as TextObject;
            //rptregistration.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["regavat"]).ToString("#,##0;(#,##0);");
            //TextObject rptdevelopmentcost = rptStatus.ReportDefinition.ReportObjects["developmentcost"] as TextObject;
            //rptdevelopmentcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["devcharge"]).ToString("#,##0;(#,##0); ");
            //TextObject rptwelfarefund = rptStatus.ReportDefinition.ReportObjects["welfarefund"] as TextObject;
            //rptwelfarefund.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["wefund"]).ToString("#,##0;(#,##0); ");
            //TextObject rptOthers = rptStatus.ReportDefinition.ReportObjects["Others"] as TextObject;
            //rptOthers.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["others"]).ToString("#,##0;(#,##0); ");

            //TextObject rpttoprice = rptStatus.ReportDefinition.ReportObjects["toprice"] as TextObject;
            //rpttoprice.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");

            //TextObject rptdiscount = rptStatus.ReportDefinition.ReportObjects["discount"] as TextObject;
            //rptdiscount.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["discount"]).ToString("#,##0;(#,##0); ");
            ////TextObject rptaccost = rptStatus.ReportDefinition.ReportObjects["accost"] as TextObject;
            ////rptaccost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["acprice"]).ToString("#,##0;(#,##0); ");
            ////---------
            //TextObject rptbudgcost = rptStatus.ReportDefinition.ReportObjects["bgdcost"] as TextObject;
            //rptbudgcost.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["bgcost"]).ToString("#,##0;(#,##0); ");
            //TextObject rptcooperative = rptStatus.ReportDefinition.ReportObjects["coopcost"] as TextObject;
            //rptcooperative.Text = Convert.ToDecimal(ds5.Tables[1].Rows[0]["coorcost"]).ToString("#,##0;(#,##0); ");

            //TextObject txtuserinfo = rptStatus.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptStatus.SetDataSource(tblins);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptStatus.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptStatus;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void RptIndPrjStDet()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string prjname = this.lblIndPrjDesc.Text.Trim();
            ReportDocument rpcp = new RealERPRPT.R_32_Mis.RptIndProjDet();

            TextObject CompName = rpcp.ReportDefinition.ReportObjects["CompName"] as TextObject;
            CompName.Text = comname;
            TextObject txtPrjName = rpcp.ReportDefinition.ReportObjects["txtPrjName"] as TextObject;
            txtPrjName.Text = "Project Name: " + prjname;
            TextObject txtDate = rpcp.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtDate.Text = "As on Date: " + Request.QueryString["Date1"].ToString().Trim(); ;
            TextObject txtuserinfo = rpcp.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rpcp.SetDataSource((DataTable)Session["tblPrjData"]);
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rpcp.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rpcp;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void RptSPLedger()
        {



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblPrjData"];
            ReportDocument rptstk = new ReportDocument();
            string Headertitle = "Subsidary Ledger";
            string daterange = "Date: " + this.lblLGDate.Text;
            string Resdesc = this.lblLGResDesc.Text;
            Resdesc = (Resdesc.Length == 0) ? "" : "Details Head: " + Resdesc;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AccLedger1>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAccSLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;

            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("txtHeadertitle", Headertitle));
            Rpt1.SetParameters(new ReportParameter("prjname", "Accounts Head: " + this.lblLGPrjDesc.Text));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", userinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", daterange));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("resdes", Resdesc));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void ShowReport()
        {
            Session.Remove("tblPrjData");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            this.lblDate.Text = Request.QueryString["Date1"].ToString().Trim();
            string date = Request.QueryString["Date1"].ToString().Trim();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTCLIENTSTAT", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvPrjWiseCollBrkDown.DataSource = null;
                this.gvPrjWiseCollBrkDown.DataBind();
                return;
            }
            this.lblActDesc.Text = ds2.Tables[1].Rows[0]["acttdesc"].ToString();
            Session["tblPrjData"] = ds2.Tables[0];
            this.Data_Bind();
        }
        private void ShowIndPrjDet()
        {
            Session.Remove("tblPrjData");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            this.lblIndDate.Text = Request.QueryString["Date1"].ToString().Trim();
            string date = Request.QueryString["Date1"].ToString().Trim();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_PROJECT_STATUS", "RPTPRJSTATUSDETAILS", pactcode, date, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvIndPrjDet.DataSource = null;
                this.gvIndPrjDet.DataBind();
                return;
            }
            this.lblIndPrjDesc.Text = ds2.Tables[1].Rows[0]["actdesc"].ToString();
            Session["tblPrjData"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }
        private string GetDate()
        {
            string comcod = this.GetComCode();
            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TB", "GETOPDATE", "", "", "", "", "", "", "", "", "");
            string date1 = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]).ToString("dd-MMM-yyyy");
            return date1;

        }
        private void ShowSPLedger()
        {
            Session.Remove("tblPrjData");
            string comcod = this.GetComCode();
            string pactcode = Request.QueryString["pactcode"].ToString().Trim();
            this.lblLGDate.Text = Request.QueryString["Date1"].ToString().Trim();
            string date1 = this.GetDate();
            string date2 = Request.QueryString["Date1"].ToString().Trim();
            string rescode = Request.QueryString["rescode"].ToString().Trim();
            string spcfcode = "%";
//20210822 dev by uzzal
            pactcode = ASTUtility.Left(rescode, 2) == "97" ? "23" + ASTUtility.Right(pactcode, 10)
                : ASTUtility.Left(rescode, 2) == "95" ? "13" + ASTUtility.Right(pactcode, 10)
                : (ASTUtility.Left(rescode, 2) == "98" || ASTUtility.Left(rescode, 2) == "99") ? "26" + ASTUtility.Right(pactcode, 10) 
                : pactcode;


            DataSet ds2 = MisData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_LG", "ACCOUNTSLEDGERSUB", pactcode, date1, date2, rescode, "", "", "", "", spcfcode);
            if (ds2 == null)
            {

                this.gvSPLedger.DataSource = null;
                this.gvSPLedger.DataBind();
                return;
            }
            this.lblLGPrjDesc.Text = ds2.Tables[1].Rows[0]["actdesc"].ToString();
            this.lblLGResDesc.Text = ds2.Tables[1].Rows[0]["resdesc"].ToString();
            this.BalCalculation(ds2.Tables[0]);
            Session["tblPrjData"] = HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    this.gvPrjWiseCollBrkDown.DataSource = (DataTable)Session["tblPrjData"];
                    this.gvPrjWiseCollBrkDown.DataBind();
                    if (ASTUtility.Left(Request.QueryString["pactcode"].ToString().Trim(), 2) == "47")
                    {
                        this.gvPrjWiseCollBrkDown.Columns[2].Visible = false;
                        this.gvPrjWiseCollBrkDown.Columns[1].HeaderText = "Customer Name";
                    }
                    this.FooterCalculation();
                    break;

                case "ClientLedger":

                    break;
                case "IndPrjStDet":
                    this.gvIndPrjDet.DataSource = (DataTable)Session["tblPrjData"];
                    this.gvIndPrjDet.DataBind();
                    break;
                case "SpLedger":
                    this.gvSPLedger.DataSource = (DataTable)Session["tblPrjData"];
                    this.gvSPLedger.DataBind();
                    break;
            }

        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string grp;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type) //---------------
            {
                case "PrjCol":
                    //string gcod = dt1.Rows[0]["gcod"].ToString();

                    //    for (int j = 1; j < dt1.Rows.Count; j++)
                    //    {
                    //        if (dt1.Rows[j]["gcod"].ToString() == gcod)
                    //        {
                    //            gcod = dt1.Rows[j]["gcod"].ToString();
                    //            dt1.Rows[j]["gcod"] = "";
                    //            dt1.Rows[j]["gdesc"] = "";
                    //            dt1.Rows[j]["pactcode"] = "";
                    //            dt1.Rows[j]["usircode"] = "";
                    //            dt1.Rows[j]["schamt"] = 0;
                    //            dt1.Rows[j]["schdate"] = "";
                    //        }

                    //        else
                    //        {
                    //            gcod = dt1.Rows[j]["gcod"].ToString();
                    //        }

                    //    }
                    break;

                case "ClientLedger":
                    string gcod = dt1.Rows[0]["gcod"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)

                    {
                        if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                            dt1.Rows[j]["gcod"] = "";
                            dt1.Rows[j]["gdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";
                            dt1.Rows[j]["usircode"] = "";
                            dt1.Rows[j]["schamt"] = 0;
                            dt1.Rows[j]["schdate"] = "";
                        }

                        else
                        {
                            gcod = dt1.Rows[j]["gcod"].ToString();
                        }

                    }

                    int torow = dt1.Rows.Count - 1;

                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {

                        if (j == torow)
                            continue;
                        else
                        {

                            double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                            if (npaidamt > 0)

                            {
                                double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                                dt1.Rows[j]["balamt"] = schamt - paidamt;
                            }
                        }
                    }


                    break;
                case "IndPrjStDet":
                    grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }
                        grp = dt1.Rows[j]["grp"].ToString();


                    }
                    break;
                case "SpLedger":
                    grp = dt1.Rows[0]["grp"].ToString();
                    string Date1 = dt1.Rows[0]["voudat1"].ToString();
                    string vounum = dt1.Rows[0]["vounum1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                        {

                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                            //dt1.Rows[j]["refnum"] = "";
                        }

                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "TOTAL")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";

                        }
                        if (dt1.Rows[j]["vounum1"].ToString().Trim() == "BALANCE")
                        {
                            dt1.Rows[j]["vounum1"] = "";
                            dt1.Rows[j]["voudat1"] = "";
                        }

                        grp = dt1.Rows[j]["grp"].ToString();
                        vounum = dt1.Rows[j]["vounum1"].ToString();
                    }


                    break;

            }



            return dt1;


        }

        private DataTable HiddenSameDate2(DataTable dtable)
        {

            Session.Remove("tblCustPayment03");
            string gcod = dtable.Rows[0]["gcod"].ToString();
            DataTable dt1 = dtable;

            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "grp like 'AA' ";
            dt1 = dv1.ToTable();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                    dt1.Rows[j]["gcod"] = "";
                    dt1.Rows[j]["gdesc"] = "";
                    dt1.Rows[j]["pactcode"] = "";
                    dt1.Rows[j]["usircode"] = "";
                    dt1.Rows[j]["schamt"] = 0;
                    dt1.Rows[j]["asondues"] = 0;
                    dt1.Rows[j]["schdate"] = "01-Jan-1900";
                    dt1.Rows[j]["intdesc"] = "";
                }

                //else if (dt1.Rows[j]["grp"].ToString() == "AA")
                //{
                //    dt1.Rows[j]["intdesc"] = "";
                //}

                else
                {
                    gcod = dt1.Rows[j]["gcod"].ToString();
                }

            }

            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["grp"].ToString() == "AA" && dt1.Rows[j]["gcod"].ToString() == gcod)
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //        dt1.Rows[j]["gcod"] = "";
            //        dt1.Rows[j]["gdesc"] = "";
            //        dt1.Rows[j]["pactcode"] = "";
            //        dt1.Rows[j]["usircode"] = "";
            //        dt1.Rows[j]["schamt"] = 0;
            //        dt1.Rows[j]["asondues"] = 0;
            //        dt1.Rows[j]["schdate"] = "01-Jan-1900";
            //    }
            //    else if (dt1.Rows[j]["grp"].ToString() == "AA")
            //    {
            //        dt1.Rows[j]["intdesc"] = "";
            //    }

            //    else
            //    {
            //        gcod = dt1.Rows[j]["gcod"].ToString();
            //    }

            //}

            int torow = dt1.Rows.Count - 1;

            if (torow > 0)
                for (int j = 0; j < dt1.Rows.Count; j++)
                {

                    if (j == torow)
                    {
                        double ppaidamt = Convert.ToDouble(dt1.Rows[j - 1]["paidamt"].ToString());

                        DateTime schdate = System.DateTime.Today;
                        //string schdate = System.DateTime.Now;

                        if (dt1.Rows[j - 1]["schdate"].ToString().Trim().Length > 0)
                        {
                            schdate = Convert.ToDateTime(dt1.Rows[j - 1]["schdate"]);
                        }


                        DateTime date = Convert.ToDateTime(this.Request.QueryString["Date1"].ToString());


                        if (ppaidamt > 0)
                        {

                            double schamt = Convert.ToDouble(dt1.Rows[j]["schamt1"].ToString());
                            double paidamt = Convert.ToDouble(dt1.Rows[j]["paidamt"].ToString());
                            dt1.Rows[j]["balamt"] = schdate <= date ? 0.00 : schamt - (paidamt > 0 ? paidamt : 0.00);
                        }
                    }
                    else
                    {

                        double npaidamt = Convert.ToDouble(dt1.Rows[j + 1]["paidamt"].ToString());
                        if (npaidamt > 0)
                        {
                            double schamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                            double paidamt = Convert.ToDouble(dt1.Rows[j]["schamt"].ToString());
                            dt1.Rows[j]["balamt"] = schamt - paidamt;
                        }


                        else if (npaidamt < 0)
                        {

                            dt1.Rows[j]["balamt"] = 0.00;


                        }


                    }
                }


            return dt1;
            //Session["tblCustPayment"] = dt1;

        }

        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double dramt, cramt;
            for (int i = 0; i < dt.Rows.Count - 2; i++)
            {


                if ((dt.Rows[i]["vounum"]).ToString() == "TOTAL")
                    break;

                if (((dt.Rows[i]["cactcode"]).ToString().Trim()).Length == 12)
                {
                    dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                    cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                    balamt = balamt + (dramt - cramt);
                    dt.Rows[i]["balamt"] = balamt;
                }
            }
            return dt;


        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblPrjData"];
            if (dt.Rows.Count == 0)
                return;

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "PrjCol":
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFSaVal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsalamt)", "")) ? 0.00
                         : dt.Compute("Sum(tsalamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgFClrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tclramt)", "")) ? 0.00
                        : dt.Compute("Sum(tclramt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFtretamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(retcheque)", "")) ?
                                            0.00 : dt.Compute("sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFtframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(fcheque)", "")) ?
                                            0.00 : dt.Compute("sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFtpdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pcheque)", "")) ?
                                           0.00 : dt.Compute("sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFAmtrep")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(trecev)", "")) ? 0.00
                        : dt.Compute("Sum(trecev)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFNOI")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(noiamt)", "")) ? 0.00
                        : dt.Compute("Sum(noiamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFStdAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stdamt)", "")) ? 0.00
                        : dt.Compute("Sum(stdamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFcuamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cuamt)", "")) ? 0.00
                        : dt.Compute("Sum(cuamt)", ""))).ToString("#,##0;(#,##0);  ");
                    ((Label)this.gvPrjWiseCollBrkDown.FooterRow.FindControl("lgvFTamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tamt)", "")) ? 0.00
                        : dt.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0);  ");
                    break;

                case "ClientLedger":

                    break;
                case "IndPrjStDet":

                    break;
                case "SpLedger":
                    break;
            }

        }

        protected void gvPrjWiseCollBrkDown_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string rescode1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();
            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvDesc");
            HyperLink hlink2 = (HyperLink)e.Row.FindControl("HLgvDesc1");
            //Label actcode = (Label)e.Row.FindControl("lblgvActcode");
            //Label usircode = (Label)e.Row.FindControl("lblgvUsircod");
            string Date1 = this.lblDate.Text;
            string actcode = ((Label)e.Row.FindControl("lblgvActcode")).Text;
            string usircode = ((Label)e.Row.FindControl("lblgvUsircod")).Text;
            if (ASTUtility.Left(rescode1, 1) == "6")
            {
                hlink1.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=ClientLedger&pactcode=" + actcode + "&usircode=" + usircode + "&Date1=" + Date1;
            }
            if (ASTUtility.Left(rescode1, 1) == "5")
            {
                hlink2.NavigateUrl = "RptProjectCollBrkDown.aspx?Type=ClientLedger&pactcode=" + actcode + "&usircode=" + usircode + "&Date1=" + Date1;
            }
        }
        protected void gvIndPrjDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actcode = (Label)e.Row.FindControl("lblgvCode");
                Label actdesc = (Label)e.Row.FindControl("lgcActDesc");
                Label Amount = (Label)e.Row.FindControl("lgvAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Left((code), 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;

                    Amount.Font.Bold = true;
                    actdesc.Style.Add("text-align", "Right");
                    Amount.Style.Add("text-align", "Right");
                    actdesc.Style.Add("color", "Blue");
                    Amount.Style.Add("color", "Blue");

                }
                if (ASTUtility.Right((code), 4) == "AAAA")
                {
                    actcode.Visible = false;
                    Amount.Font.Bold = true;
                }


            }
        }
        protected void gvSPLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            string mCOMCOD = comcod;


            HyperLink hlinkbill = (HyperLink)e.Row.FindControl("HLgvRemarks");
            string mVOUNUM = hlink1.Text;
            string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;
            string billno = hlinkbill.Text;

            if (mVOUNUM.Trim().Length == 14 && ASTUtility.Left(mVOUNUM.Trim(), 2) != "PV")
            {
                //hlink1.NavigateUrl = "~/F_17_Acc/AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
                hlink1.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + mVOUNUM;
                hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);



            }

            if (billno == "") ;
            else if (billno.Length > 3 && billno.Substring(0, 3) == "PBL")
            {
                hlinkbill.NavigateUrl = "LinkMis.aspx?Type=BillConfirmation&billno=" + billno;
            }

        }
    }
}