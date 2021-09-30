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
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{
    public partial class SupplierPaySlipPrint : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString();
            switch (Type)
            {
                case "SupPay":
                    this.PrintSupplierPaySlip();
                    break;

                case "SubContractor":
                    this.PrintSubContractor();
                    break;

            }


        }


        private void PrintSubContractor()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd1"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = this.Request.QueryString["frmdat"].ToString();
            string todate = this.Request.QueryString["todat"].ToString();
            string vouno = this.Request.QueryString["Vouno"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_SUBCONTRACTOR", "RPTSUBCONTRACTORPAYSLIP", fromdate, todate, vouno, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            string printdat = System.DateTime.Today.ToString("dd-MM-yyyy");
            string chequedat = Convert.ToDateTime(dt.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
            string cheqeno = dt.Rows[0]["refnum"].ToString();
            string bankname = dt.Rows[0]["actdesc"].ToString();
            string pactname = dt.Rows[0]["cactdesc"].ToString().Replace("AP-", "");
            //string payto = dt.Rows[0]["payto"].ToString();


            //string pactname1 =( dt.Rows.Count>1)?dt.Rows[1]["cactdesc"].ToString():"";
            //string pactname2 = (dt.Rows.Count > 2) ? dt.Rows[2]["cactdesc"].ToString() : "";

            string pactname1 = (dt.Rows.Count > 1) ? dt.Rows[1]["cactdesc"].ToString().Replace("AP-", "") : "";
            string pactname2 = (dt.Rows.Count > 2) ? dt.Rows[2]["cactdesc"].ToString().Replace("AP-", "") : "";
            string pactname3 = (dt.Rows.Count > 3) ? dt.Rows[3]["cactdesc"].ToString().Replace("AP-", "") : "";
            string pactname4 = (dt.Rows.Count > 4) ? dt.Rows[4]["cactdesc"].ToString().Replace("AP-", "") : "";
            string pactname5 = (dt.Rows.Count > 5) ? dt.Rows[5]["cactdesc"].ToString().Replace("AP-", "") : "";



            // string pactname2 = (dt.Rows.Count==2)? dt.Rows[2]["cactdesc"].ToString():"";     


            string naration = dt.Rows[0]["nofwork"].ToString();
            string subcontractor = dt.Rows[0]["resdesc"].ToString();
            string payto = dt.Rows[0]["payto"].ToString();
            double amt = Convert.ToDouble(dt.Rows[0]["trnamt"].ToString());
            double amt1 = (dt.Rows.Count > 1) ? Convert.ToDouble(dt.Rows[1]["trnamt"].ToString()) : 0.00;
            double amt2 = (dt.Rows.Count > 2) ? Convert.ToDouble(dt.Rows[2]["trnamt"].ToString()) : 0.00;
            double amt3 = (dt.Rows.Count > 3) ? Convert.ToDouble(dt.Rows[3]["trnamt"].ToString()) : 0.00;
            double amt4 = (dt.Rows.Count > 4) ? Convert.ToDouble(dt.Rows[4]["trnamt"].ToString()) : 0.00;

            double amt5 = (dt.Rows.Count > 5) ? Convert.ToDouble(dt.Rows[5]["trnamt"].ToString()) : 0.00;


            double totalamt = amt + amt1 + amt2 + amt3 + amt4 + amt5;
            string totalamt1 = ASTUtility.Trans(Math.Round(totalamt), 2);




            //Rdlc
            string totalamtstr = totalamt.ToString("#,##0.00;(#,##0.00);");
            string amtstr = amt.ToString("#,##0.00;(#,##0.00);");
            string amt1str = amt1.ToString("#,##0.00;(#,##0.00);");
            string amt2str = amt2.ToString("#,##0.00;(#,##0.00);");
            string amt3str = amt3.ToString("#,##0.00;(#,##0.00);");
            string amt4str = amt4.ToString("#,##0.00;(#,##0.00);");
            string amt5str = amt5.ToString("#,##0.00;(#,##0.00);");

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            string txtMode = (cheqeno.Length > 0) ? "Paid By Cheque" : "";
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPaySlipSubContractor", null, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "SUB-CONTRACTOR PAYMENT SLIP "));

            Rpt1.SetParameters(new ReportParameter("cheqeno", cheqeno));
            Rpt1.SetParameters(new ReportParameter("cheqedat", chequedat));
            Rpt1.SetParameters(new ReportParameter("payto", payto));
            Rpt1.SetParameters(new ReportParameter("pactname", pactname));

            Rpt1.SetParameters(new ReportParameter("pactname1", pactname1));
            Rpt1.SetParameters(new ReportParameter("pactname2", pactname2));
            Rpt1.SetParameters(new ReportParameter("pactname3", pactname3));
            Rpt1.SetParameters(new ReportParameter("pactname4", pactname4));
            Rpt1.SetParameters(new ReportParameter("pactname5", pactname5));



            Rpt1.SetParameters(new ReportParameter("bankname", bankname));
            Rpt1.SetParameters(new ReportParameter("amt", amtstr));
            Rpt1.SetParameters(new ReportParameter("amt1", amt1str));
            Rpt1.SetParameters(new ReportParameter("amt2", amt2str));
            Rpt1.SetParameters(new ReportParameter("amt3", amt3str));
            Rpt1.SetParameters(new ReportParameter("amt4", amt4str));
            Rpt1.SetParameters(new ReportParameter("amt5", amt5str));

            Rpt1.SetParameters(new ReportParameter("tamt", totalamtstr));
            Rpt1.SetParameters(new ReportParameter("txtMode", txtMode));
            Rpt1.SetParameters(new ReportParameter("narration", naration));
            Rpt1.SetParameters(new ReportParameter("inword", totalamt1));
            Rpt1.SetParameters(new ReportParameter("paidby", ""));
            Rpt1.SetParameters(new ReportParameter("txtdate", printdat));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";




            //ReportDocument rptvou = new RealERPRPT.R_17_Acc.RptPaySlipSubContractor();
            //TextObject txtCompanyName = rptvou.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = comnam;
            //TextObject txtcomadd = rptvou.ReportDefinition.ReportObjects["compadd"] as TextObject;
            //txtcomadd.Text = comadd;
            //TextObject txtdate = rptvou.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = printdat;

            //TextObject txtChequedate = rptvou.ReportDefinition.ReportObjects["txtChequedate"] as TextObject;
            //txtChequedate.Text = chequedat;

            //TextObject txtChequeno = rptvou.ReportDefinition.ReportObjects["txtChequeno"] as TextObject;
            //txtChequeno.Text = cheqeno;

            //TextObject txtMode = rptvou.ReportDefinition.ReportObjects["txtMode"] as TextObject;
            //txtMode.Text = (cheqeno.Length>0)?"Paid By Cheque":"";

            //TextObject txtSupplier = rptvou.ReportDefinition.ReportObjects["txtSupplier"] as TextObject;
            //txtSupplier.Text = subcontractor;

            //TextObject txtpayto = rptvou.ReportDefinition.ReportObjects["txtpayto"] as TextObject;
            //txtpayto.Text = payto;
            //TextObject txtbankname = rptvou.ReportDefinition.ReportObjects["txtbankname"] as TextObject;
            //txtbankname.Text = bankname;
            //TextObject txtPactName = rptvou.ReportDefinition.ReportObjects["txtPactName"] as TextObject;
            //txtPactName.Text = pactname;


            ////

            //TextObject txtPactName1 = rptvou.ReportDefinition.ReportObjects["txtPactName1"] as TextObject;
            //txtPactName1.Text = (pactname1.Length > 0) ? pactname1  : "";
            //rptvou.ReportDefinition.Sections["ReportHeaderSection5"].SectionFormat.EnableSuppress = (pactname1.Length > 0) ? false : true;

            //TextObject txtPactName2 = rptvou.ReportDefinition.ReportObjects["txtPactName2"] as TextObject;
            //txtPactName2.Text = (pactname2.Length > 0) ? pactname2 : "";
            //rptvou.ReportDefinition.Sections["ReportHeaderSection4"].SectionFormat.EnableSuppress = (pactname2.Length > 0) ? false : true;


            //TextObject txtPactName3 = rptvou.ReportDefinition.ReportObjects["txtPactName3"] as TextObject;
            //txtPactName3.Text = (pactname3.Length > 0) ? pactname3 : "";
            //rptvou.ReportDefinition.Sections["ReportHeaderSection3"].SectionFormat.EnableSuppress = (pactname3.Length > 0) ? false : true;


            //TextObject txtPactName4 = rptvou.ReportDefinition.ReportObjects["txtPactName4"] as TextObject;
            //txtPactName4.Text = (pactname4.Length > 0) ? pactname4 : "";
            //rptvou.ReportDefinition.Sections["ReportHeaderSection2"].SectionFormat.EnableSuppress = (pactname4.Length > 0) ? false : true;


            //TextObject txtPactName5 = rptvou.ReportDefinition.ReportObjects["txtPactName5"] as TextObject;
            //txtPactName5.Text = (pactname5.Length > 0) ? pactname5 : "";
            //rptvou.ReportDefinition.Sections["ReportHeaderSection6"].SectionFormat.EnableSuppress = (pactname5.Length > 0) ? false : true;


            //TextObject txtnaration = rptvou.ReportDefinition.ReportObjects["txtnaration"] as TextObject;
            //txtnaration.Text = naration;


            //TextObject txtamt = rptvou.ReportDefinition.ReportObjects["txtamt"] as TextObject;
            //txtamt.Text = amt.ToString("#,##0.00;(#,##0.00); ");

            //TextObject txtamt1 = rptvou.ReportDefinition.ReportObjects["txtamt1"] as TextObject;
            //txtamt1.Text = amt1.ToString("#,##0.00;(#,##0.00); ");

            //TextObject txtamt2 = rptvou.ReportDefinition.ReportObjects["txtamt2"] as TextObject;
            //txtamt2.Text = amt2.ToString("#,##0.00;(#,##0.00); ");


            //TextObject txtamt3 = rptvou.ReportDefinition.ReportObjects["txtamt3"] as TextObject;
            //txtamt3.Text = amt3.ToString("#,##0.00;(#,##0.00); ");

            //TextObject txtamt4 = rptvou.ReportDefinition.ReportObjects["txtamt4"] as TextObject;
            //txtamt4.Text = amt4.ToString("#,##0.00;(#,##0.00); ");
            //TextObject txtamt5 = rptvou.ReportDefinition.ReportObjects["txtamt5"] as TextObject;
            //txtamt5.Text = amt5.ToString("#,##0.00;(#,##0.00); ");


            //TextObject txtamttolal = rptvou.ReportDefinition.ReportObjects["txttamt"] as TextObject;
            //txtamttolal.Text = totalamt.ToString("#,##0.00;(#,##0.00; ");

            //TextObject txtInword = rptvou.ReportDefinition.ReportObjects["txtInword"] as TextObject;
            //txtInword.Text = totalamt1;
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptvou.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptvou;

            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";

        }
        private void PrintSupplierPaySlip()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comadd = hst["comadd1"].ToString();
                string username = hst["username"].ToString();
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string fromdate = this.Request.QueryString["frmdat"].ToString();
                string todate = this.Request.QueryString["todat"].ToString();
                string vouno = this.Request.QueryString["Vouno"].ToString() + "%";

                DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_VOUCHER", "RPTSUPPLIERPAYSLIP", fromdate, todate, vouno, "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                DataTable dt = ds1.Tables[0];
                string printdat = System.DateTime.Today.ToString("dd-MM-yyyy");
                string chequedat = Convert.ToDateTime(dt.Rows[0]["chequedat"]).ToString("dd-MM-yyyy");
                string cheqeno = dt.Rows[0]["refnum"].ToString();
                string pactname = dt.Rows[0]["cactdesc"].ToString();
                string bankname = dt.Rows[0]["actdesc"].ToString();
                string naration = dt.Rows[0]["vounar"].ToString();
                string payto = dt.Rows[0]["payto"].ToString();
                double amt = Convert.ToDouble(dt.Rows[0]["trnamt"].ToString());
                string amt1 = ASTUtility.Trans(Math.Round(amt), 2);

                string amtstr = amt.ToString("#,##0.00;(#,##0.00);");
                //Rdlc
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
                string txtMode = (cheqeno.Length > 0) ? "Paid By Cheque" : "";
                LocalReport Rpt1 = new LocalReport();
                var list = new  List<RealEntity.C_32_Mis.EClassAcc_03.CollectionBrackDown>(); 

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPaySlipSupplier", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "SUPPLIER PAYMENT SLIP"));
                Rpt1.SetParameters(new ReportParameter("txtDate", printdat));
                Rpt1.SetParameters(new ReportParameter("cheqDate", chequedat));
                Rpt1.SetParameters(new ReportParameter("cheqNo", cheqeno));
                Rpt1.SetParameters(new ReportParameter("suppName", payto));
                Rpt1.SetParameters(new ReportParameter("pactName", pactname));
                Rpt1.SetParameters(new ReportParameter("txtAmt", amtstr));
                Rpt1.SetParameters(new ReportParameter("txtMode", txtMode));
                Rpt1.SetParameters(new ReportParameter("txtInWord", amt1));
                Rpt1.SetParameters(new ReportParameter("comLogo", ComLogo));

                //Rpt1.SetParameters(new ReportParameter("bankname", bankname));
                //Rpt1.SetParameters(new ReportParameter("amt", amtstr));
                //Rpt1.SetParameters(new ReportParameter("narration", naration));
                //Rpt1.SetParameters(new ReportParameter("paidby", ""));
                //Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



                //ReportDocument rptvou = new RealERPRPT.R_17_Acc.RptPaySlipSupplier();
                //TextObject txtCompanyName = rptvou.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
                //txtCompanyName.Text = comnam;
                //TextObject txtcomadd = rptvou.ReportDefinition.ReportObjects["compadd"] as TextObject;
                //txtcomadd.Text = comadd;
                //TextObject txtdate = rptvou.ReportDefinition.ReportObjects["txtdate"] as TextObject;
                //txtdate.Text = printdat;
                //TextObject txtChequedate = rptvou.ReportDefinition.ReportObjects["txtChequedate"] as TextObject;
                //txtChequedate.Text = chequedat;
                //TextObject txtChequeno = rptvou.ReportDefinition.ReportObjects["txtChequeno"] as TextObject;
                //txtChequeno.Text = cheqeno;
                //TextObject txtbankname = rptvou.ReportDefinition.ReportObjects["txtbankname"] as TextObject;
                //txtbankname.Text = bankname;
                //TextObject txtMode = rptvou.ReportDefinition.ReportObjects["txtMode"] as TextObject;
                //txtMode.Text = (cheqeno.Length > 0) ? "Paid By Cheque" : "";
                //TextObject txtSupplier = rptvou.ReportDefinition.ReportObjects["txtSupplier"] as TextObject;
                //txtSupplier.Text = payto;
                //TextObject txtPactName = rptvou.ReportDefinition.ReportObjects["txtPactName"] as TextObject;
                //txtPactName.Text = pactname;
                //TextObject txtnaration = rptvou.ReportDefinition.ReportObjects["txtnaration"] as TextObject;
                //txtnaration.Text = naration;
                //TextObject txtamt = rptvou.ReportDefinition.ReportObjects["txtamt"] as TextObject;
                //txtamt.Text = amt.ToString("#,##0.00;(#,##0.00); ");
                //TextObject txtamt2 = rptvou.ReportDefinition.ReportObjects["txtamt2"] as TextObject;
                //txtamt2.Text = amt.ToString("#,##0.00;(#,##0.00); ");

                //TextObject txtInword = rptvou.ReportDefinition.ReportObjects["txtInword"] as TextObject;
                //txtInword.Text = amt1;

                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                //rptvou.SetParameterValue("ComLogo", ComLogo);
                //Session["Report1"] = rptvou;

                //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                //                         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";



            }
            catch (Exception ex)
            {
                string msg = "Error:" + ex.Message;
            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


    }
}