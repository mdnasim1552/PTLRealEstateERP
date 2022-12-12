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
using AjaxControlToolkit;
using System.IO;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_14_Pro
{
    public partial class PurBillEntry : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                if (this.Request.QueryString["Type"] == "BillPrint")
                {
                    this.PrintBillConfimation();
                }
                else
                {

                    ((Label)this.Master.FindControl("lblTitle")).Text = "Bill Confirmation";
                    this.txtCurBillDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                    this.txtApprovalDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                    this.txtBillrefDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                    this.txtChequeDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
                    this.txtauditbilldate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    ViewState.Remove("tblproject");
                    this.TblProject();
                    this.txtCurBillDate_CalendarExtender.EndDate = System.DateTime.Today;
                    this.lbtnSupplierList_Click(null, null);
                    string qgenno = this.Request.QueryString["genno"] ?? "";
                    if (qgenno.Length > 0)
                    {
                        if (qgenno.Substring(0, 3) == "PBL")
                        {
                            this.lbtnPrevBillList_Click(null, null);
                            lbtnOk_Click(null, null);
                        }
                    }

                    string type = this.Request.QueryString["Type"] ?? "";
                    if (type == "BillEntryAudit")
                    {
                        this.lblaudit.Visible = true;
                        this.txtauditbilldate.Visible = true;
                    }


                    //string type = this.Request.QueryString["Type"] ?? "";
                    //if (type == "BillEntryAudit")
                    //{


                    //    this.GetBillInfoForAudit();

                    //}
                }


            }
        }

        //private void GetBillInfoForAudit()
        //{
        //    string billno = this.Request.QueryString["billno"];
        //    string type = this.Request.QueryString["Type"];
        //    this.Panel1.Visible = true;
        //    this.Panel2.Visible = true;

        //    this.Get_PurchaseBill_Info();
        //    this.lbtnSupplierList_Click(null, null);
        //    this.GetOrderList();


        //}
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

        private void TblProject()
        {
            if (ViewState["tblproject"] == null)
            {
                DataTable tblproject = new DataTable();
                tblproject.Columns.Add("pactcode", Type.GetType("System.String"));
                tblproject.Columns.Add("pactdesc", Type.GetType("System.String"));
                ViewState["tblproject"] = tblproject;
            }
        }

        //private void GridFieldVisible() 
        //{

        //    ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "BillEdit");
        //    ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnUpdateBill")).Visible = (this.Request.QueryString["InputType"].ToString().Trim() == "BillEdit");

        //}


        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }

        private string CompanyBill()
        {
            string comcod = this.GetCompCode();
            string PrintReq = "";
            switch (comcod)
            {
                //case "3101":
                case "1101": //Rupayan
                case "2305":
                case "3305":
                case "3306":
                case "3307":
                case "3308":
                case "3309":
                case "3310":
                case "3311":

                    PrintReq = "PrintBill02";
                    break;
                //case "3101":
                case "3312":
                case "3315":
                case "3316":
                case "3317": //Assure

                    PrintReq = "PrintBill03";

                    break;
                // case "3336":
                //case "3101":
                case "3330":// Bridge
                    PrintReq = "PrintBill04";

                    break;
                //case "3101":
                case "3332":// Instar
                case "3335":// Edison
                case "1206":// Acme
                case "1207":// Acme
                case "3338":// Acme
                case "3336":// Suvastu
                case "3337"://Suvastu
                    PrintReq = "PrintBill05";
                    break;
                //case "3101":
                case "3333":  // Allaince     
                    PrintReq = "PrintBill06";
                    break;


                //case "3101":
                case "3353":// manama
                    PrintReq = "PrintBill07";

                    break;

                case "3364":// JBS
                    PrintReq = "PrintBill08";

                    break;

                case "3101":
                case "3366":// Lanco
                    PrintReq = "PrintBillLanco";

                    break;

                case "3368":// Lanco
                    PrintReq = "PrintBillFinlay";
                    break;

                case "3370": // 
                    PrintReq = "PrintBillCPDL";
                    break;

                default:
                    PrintReq = "PrintBill05";
                    // PrintReq = "PrintBill04"; //default(before)
                    break;
            }

            return PrintReq;

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            this.PrintBillConfimation();

        }

        private void PrintBillConfimation()
        {
            string printcomreq = this.CompanyBill();

            if (printcomreq == "PrintBill01")
                this.PrintBill01();
            else if (printcomreq == "PrintBill02")
                this.PrintBill02();
            else if (printcomreq == "PrintBill03")
                this.PrintBill03();
            else if (printcomreq == "PrintBill04")
                this.PrintBill04();

            else if (printcomreq == "PrintBill05")
                this.PrintBill05();

            else if (printcomreq == "PrintBill06")
                this.PrintBill06();

            else if (printcomreq == "PrintBill07")
                this.PrintBill07();

            else if (printcomreq == "PrintBill08")
                this.PrintBill08();

            else if (printcomreq == "PrintBillLanco")
                this.PrintBillLanco(); 

            else if (printcomreq == "PrintBillFinlay")
                this.PrintBillFinlay();

            else if (printcomreq == "PrintBillCPDL")
                this.PrintBillCPDL();

            else
                this.PrintBill02();
        }

        private bool isBillFromAcc()
        {
            bool isbillAcc = false;
            if (this.Request.QueryString["Type"] == "BillPrint")
            {
                isbillAcc = true;
            }
            else
            {
                isbillAcc = false;
            }
            return isbillAcc;
        }


        private void PrintBill06()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));


            double security, deduction, penalty, advanced;
            string txtSupName, txtBillid, txtNarration, billref, percntge;
            if (isAccBill)
            {
                security = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(sdamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(sdamt)", "")));
                deduction = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(dedamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(dedamt)", "")));
                penalty = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(penamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(penamt)", "")));
                advanced = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(advamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(advamt)", "")));

                percntge = Convert.ToDouble("0" + ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); 0%");
                txtSupName = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
                txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
            }
            else
            {
                security = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString()));
                deduction = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString()));
                penalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString()));
                advanced = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString()));
                percntge = this.txtpercentage.Text.ToString();
                txtSupName = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
                txtBillid = "Bill No: " + this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                txtNarration = "Narration : " + this.txtBillNarr.Text.Trim();
            }

            // rdlc start
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);

            string txtDepo = Convert.ToDouble(security).ToString("#,##0.00;(#,##0.00); ");
            string txtAdv = Convert.ToDouble(advanced).ToString("#,##0.00;(#,##0.00); ");
            string txtPenalty = Convert.ToDouble(penalty).ToString("#,##0.00;(#,##0.00); ");
            string txtDeduc = Convert.ToDouble(deduction).ToString("#,##0.00;(#,##0.00); ");

            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));
            string inword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);
            string netamt = Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            //
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillAlliInfo", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillno", txtBillid));
            rpt.SetParameters(new ReportParameter("date1", "Date: " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("txtSecurity", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", netamt.ToString()));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;

            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

        }
        private void PrintBill01()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //



            ReportDocument rptstk = new RealERPRPT.R_14_Pro.rptBillInfo();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;
            TextObject rpttxtsupplier = rptstk.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            rpttxtsupplier.Text = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
            TextObject rpttxtbillno = rptstk.ReportDefinition.ReportObjects["billno"] as TextObject;
            rpttxtbillno.Text = "Bill No: " + this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
            TextObject rpttxtdate = rptstk.ReportDefinition.ReportObjects["date"] as TextObject;
            rpttxtdate.Text = "Date: " + CurDate1;






            TextObject rpttxtTotalAmount = rptstk.ReportDefinition.ReportObjects["txtTotalAmount"] as TextObject;
            rpttxtTotalAmount.Text = (amt1 - amt2).ToString("#,##0;(#,##0); ");

            TextObject rpttxtTaka = rptstk.ReportDefinition.ReportObjects["takainword"] as TextObject;
            rpttxtTaka.Text = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            TextObject rpttxtNarration = rptstk.ReportDefinition.ReportObjects["txtNarration"] as TextObject;
            rpttxtNarration.Text = this.txtBillNarr.Text.Trim();

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);


            rptstk.SetDataSource(this.HiddenSameData(ds1.Tables[0]));
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;

            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintBill02()
        {


            ///// start Rdlc

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            //string mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            string mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;


            DataTable dt = ds1.Tables[0];

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            double amt = amt1 - amt2;



            double security = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString()));
            double deduction = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString()));
            double penalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString()));
            double advanced = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString()));
            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));




            //        DataTable dt = ds1.Tables[0];


            //   double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrramt)", "")) ? 0.00 : dt.Compute("Sum(mrramt)", "")));



            //ReportDocument rptstk = new RealERPRPT.R_14_Pro.RptBillConfirmation02();

            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;


            DataTable dt1 = ds1.Tables[2];
            DataTable dtmrref = ds1.Tables[1];
            string mrfno = dtmrref.Rows[0]["mrfno"].ToString();
            string mrfno1 = dtmrref.Rows[0]["mrfno"].ToString();

            for (int i = 1; i < dtmrref.Rows.Count; i++)
            {

                if (dtmrref.Rows[i]["mrfno"].ToString() == mrfno)
                    ;
                else
                {
                    mrfno1 = mrfno1 + ", " + dtmrref.Rows[i]["mrfno"].ToString();

                }

                mrfno = dtmrref.Rows[i]["mrfno"].ToString();



            }

            // rdlc start

            string txtProjName = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtBuildno = dt1.Rows[0]["termsdesc"].ToString();
            string txtFloorno = dt1.Rows[1]["termsdesc"].ToString();
            string txtFlatno = dt1.Rows[2]["termsdesc"].ToString();
            string txtSupName = this.ddlSupList.SelectedItem.Text.Trim();
            string txtBillid = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
            string billrefdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtBillno = this.txtBillRef.Text;
            string chqdate = Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"].ToString()).ToString("dd-MMM-yyyy");
            string txtInword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);


            string txtNarration = "Narration : " + this.txtBillNarr.Text.Trim();
            string txtDepo = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString().Trim()) == 0.00) ? "" : ("Security Deposit (" + this.txtpercentage.Text.Trim() + ") : " + this.txtSDAmount.Text.ToString());
            string txtAdv = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString().Trim()) == 0.00) ? "" : ("Advanced:   " + this.txtAdvanced.Text.ToString().Trim());
            string txtPenalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString().Trim()) == 0.00) ? "" : ("Pelanty:   " + this.txtPenaltyAmount.Text.ToString().Trim());
            string txtDeduc = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString().Trim()) == 0.00) ? "" : ("Deduction:   " + this.txtDedAmount.Text.ToString().Trim());
            string txtNetAmt = "Net Amount : " + Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            string ftReqIn = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string ftReqApp = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ftOrder = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string ftWorkOrd = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string ftMrRecv = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string ftBillConf = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();

            string lblmramt = Convert.ToDouble(((Label)this.gvBillInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text).ToString("#,##0.00;(#,##0.00); ");




            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            //adjusted bill

            //TextObject lblbilldate = rptstk.ReportDefinition.ReportObjects["lblbilldate"] as TextObject;
            //lblbilldate.Text = (this.ddlPayType.SelectedValue == "003") ? "" : "Bill Date";

            //TextObject lblcbilldate = rptstk.ReportDefinition.ReportObjects["lblcbilldate"] as TextObject;
            //lblcbilldate.Text = (this.ddlPayType.SelectedValue == "003") ? "" : ":";

            //TextObject txtbiilrefdate = rptstk.ReportDefinition.ReportObjects["txtbiilrefdate"] as TextObject;
            //txtbiilrefdate.Text = (this.ddlPayType.SelectedValue == "003") ? "Adjustment" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"].ToString()).ToString("dd-MMM-yyyy");

            //TextObject lblchequedate = rptstk.ReportDefinition.ReportObjects["lblchequedate"] as TextObject;
            //lblchequedate.Text = (this.ddlPayType.SelectedValue == "003") ? "" : "Cheque Date";

            //TextObject lblcchequedate = rptstk.ReportDefinition.ReportObjects["lblcchequedate"] as TextObject;
            //lblcchequedate.Text = (this.ddlPayType.SelectedValue == "003") ? "" : ":";


            //TextObject txtbiilchequedate = rptstk.ReportDefinition.ReportObjects["txtbiilchequedate"] as TextObject;
            //txtbiilchequedate.Text = (this.ddlPayType.SelectedValue == "003") ? "" : Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"].ToString()).ToString("dd-MMM-yyyy");


            //

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmation02", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Bill Confirmation Certificate"));
            rpt.SetParameters(new ReportParameter("txtComment", "( Comments On Enclosed Bills )"));
            rpt.SetParameters(new ReportParameter("txtProjName", " : " + txtProjName));
            rpt.SetParameters(new ReportParameter("txtBuildno", " : " + txtBuildno));
            rpt.SetParameters(new ReportParameter("txtFloorno", " : " + txtFloorno));
            rpt.SetParameters(new ReportParameter("txtFlatno", " : " + txtFlatno));
            rpt.SetParameters(new ReportParameter("txtSupName", "Supplier Name  :  " + txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillid", " : " + txtBillid));
            rpt.SetParameters(new ReportParameter("txtBilldate", (this.ddlPayType.SelectedValue == "003") ? "Adjustment" : " : " + billrefdate));
            rpt.SetParameters(new ReportParameter("txtBillno", " : " + txtBillno));
            rpt.SetParameters(new ReportParameter("mprno", "MPR NO  :  " + mrfno1));
            rpt.SetParameters(new ReportParameter("date", " : " + CurDate1));
            rpt.SetParameters(new ReportParameter("chqdate", (this.ddlPayType.SelectedValue == "003") ? "" : " : " + chqdate));
            rpt.SetParameters(new ReportParameter("txtDepo", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", txtNetAmt));
            rpt.SetParameters(new ReportParameter("lblbilldate", (this.ddlPayType.SelectedValue == "003") ? "" : "Bill Date"));
            rpt.SetParameters(new ReportParameter("lblcbilldate", (this.ddlPayType.SelectedValue == "003") ? "" : "Cheque Date"));
            rpt.SetParameters(new ReportParameter("lblmramt", lblmramt));




            rpt.SetParameters(new ReportParameter("txtInword", txtInword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));

            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            // rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            // // rdlc end



        }

        private void PrintBill03()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));

            string txtSupName, txtBillid, txtNarration, billref, lblmramt, percntge;
            double security, deduction, penalty, advanced;

            if (isAccBill)
            {
                security = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(sdamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(sdamt)", "")));
                deduction = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(dedamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(dedamt)", "")));
                penalty = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(penamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(penamt)", "")));
                advanced = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(advamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(advamt)", "")));

                txtSupName = ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
                txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
                percntge = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); 0%");
            }
            else
            {
                security = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString()));
                deduction = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString()));
                penalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString()));
                advanced = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString()));
                percntge = this.txtpercentage.Text.ToString();
                txtSupName = this.ddlSupList.SelectedItem.Text.Trim();
                txtBillid = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                txtNarration = this.txtBillNarr.Text.Trim();
                billref = this.txtBillRef.Text;
                lblmramt = Convert.ToDouble(((Label)this.gvBillInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text).ToString("#,##0.00;(#,##0.00); ");
            }

            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));

            DataTable dt1 = ds1.Tables[2];
            DataTable dtmrref = ds1.Tables[1];
            string mrfno = dtmrref.Rows[0]["mrfno"].ToString();
            string mrfno1 = dtmrref.Rows[0]["mrfno"].ToString();


            for (int i = 1; i < dtmrref.Rows.Count; i++)
            {

                if (dtmrref.Rows[i]["mrfno"].ToString() == mrfno)
                    ;
                else
                {
                    mrfno1 = mrfno1 + ", " + dtmrref.Rows[i]["mrfno"].ToString();

                }

                mrfno = dtmrref.Rows[i]["mrfno"].ToString();
            }


            // rdlc start

            string txtProjName = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtBuildno = dt1.Rows[0]["termsdesc"].ToString();
            string txtFloorno = dt1.Rows[1]["termsdesc"].ToString();
            string txtFlatno = dt1.Rows[2]["termsdesc"].ToString();
            string billrefdate = "Bill Date : " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"].ToString()).ToString("dd-MMM-yyyy");
            string txtBillno = "Bill No : " + billref;
            string txtReqno = mrfno1;
            string chqdate = "Cheque Date : " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"].ToString()).ToString("dd-MMM-yyyy");
            string txtInword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);

            string txtDepo = "Security Deposit (" + percntge + ") : " + Convert.ToDouble(security).ToString("#,##0.00;(#,##0.00); ");
            string txtAdv = "Advanced: " + Convert.ToDouble(advanced).ToString("#,##0.00;(#,##0.00); ");
            string txtPenalty = "Pelanty: " + Convert.ToDouble(penalty).ToString("#,##0.00;(#,##0.00); ");
            string txtDeduc = "Deduction: " + Convert.ToDouble(deduction).ToString("#,##0.00;(#,##0.00); ");
            string txtNetAmt = "Net Amount : " + Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            string ftReqIn = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string ftReqApp = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ftOrder = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string ftWorkOrd = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string ftMrRecv = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string ftBillConf = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


            //string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmation03", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Bill Confirmation Certificate"));
            rpt.SetParameters(new ReportParameter("txtComment", "( Comments On Enclosed Bills )"));
            rpt.SetParameters(new ReportParameter("txtProjName", txtProjName));
            rpt.SetParameters(new ReportParameter("txtBuildno", txtBuildno));
            rpt.SetParameters(new ReportParameter("txtFloorno", txtFloorno));
            rpt.SetParameters(new ReportParameter("txtFlatno", txtFlatno));
            rpt.SetParameters(new ReportParameter("txtSupName", "Supplier Name :  " + txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillid", txtBillid));
            rpt.SetParameters(new ReportParameter("txtBilldate", billrefdate));
            rpt.SetParameters(new ReportParameter("txtBillno", txtBillno));
            rpt.SetParameters(new ReportParameter("txtReqno", txtReqno));
            rpt.SetParameters(new ReportParameter("date", CurDate1));

            rpt.SetParameters(new ReportParameter("chqdate", chqdate));
            rpt.SetParameters(new ReportParameter("txtDepo", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", txtNetAmt));

            rpt.SetParameters(new ReportParameter("txtInword", txtInword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("ftReqIn", ftReqIn));
            rpt.SetParameters(new ReportParameter("ftReqApp", ftReqApp));
            rpt.SetParameters(new ReportParameter("ftOrder", ftOrder));
            rpt.SetParameters(new ReportParameter("ftWorkOrd", ftWorkOrd));
            rpt.SetParameters(new ReportParameter("ftMrRecv", ftMrRecv));
            rpt.SetParameters(new ReportParameter("ftBillConf", ftBillConf));


            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            // rpt.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            // rdlc end




        }

        private void PrintBill04()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));

            //double amt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(mrramt)", "")) ? 0.00 : dt.Compute("Sum(mrramt)", "")));
            double security, deduction, penalty, advanced;
            string txtSupName, txtBillid, txtNarration, billref, percntge;
            if (isAccBill)
            {
                security = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(sdamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(sdamt)", "")));
                deduction = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(dedamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(dedamt)", "")));
                penalty = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(penamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(penamt)", "")));
                advanced = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(advamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(advamt)", "")));
                percntge = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); 0%");
                txtSupName = ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
                txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
            }
            else
            {
                security = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString()));
                deduction = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString()));
                penalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString()));
                advanced = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString()));
                percntge = this.txtpercentage.Text.ToString();

                txtSupName = this.ddlSupList.SelectedItem.Text.Trim();
                txtBillid = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                txtNarration = this.txtBillNarr.Text.Trim();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
            }

            //sdamt dedamt  penamt advamt  percntge
            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));

            //double tamt=this.gvBillInfo.FooterRow
            string pactcode = ds1.Tables[0].Rows[0]["pactcode"].ToString();
            DataTable dt1 = ds1.Tables[2];
            DataTable dtmrref = ds1.Tables[1];
            string mrfno = dtmrref.Rows[0]["mrfno"].ToString();
            string mrfno1 = dtmrref.Rows[0]["mrfno"].ToString();

            //string securitydep = Convert.ToDouble(percntge).ToString("#,##0.00;(#,##0.00); ");
            string lblSecurity = "Less Security Deposite " + "( " + percntge + " )";

            for (int i = 1; i < dtmrref.Rows.Count; i++)
            {
                if (dtmrref.Rows[i]["mrfno"].ToString() != mrfno)
                {
                    mrfno1 = mrfno1 + ", " + dtmrref.Rows[i]["mrfno"].ToString();
                }
                mrfno = dtmrref.Rows[i]["mrfno"].ToString();

            }
            string txtDepo = Convert.ToDouble(security).ToString("#,##0.00;(#,##0.00); ");
            string txtAdv = Convert.ToDouble(advanced).ToString("#,##0.00;(#,##0.00); ");
            string txtPenalty = Convert.ToDouble(penalty).ToString("#,##0.00;(#,##0.00); ");
            string txtDeduc = Convert.ToDouble(deduction).ToString("#,##0.00;(#,##0.00); ");
            string txtNetAmt = Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            string txtProjName = ds1.Tables[1].Rows[0]["pactdesc"].ToString();
            string txtBuildno = dt1.Rows[0]["termsdesc"].ToString();
            string txtFloorno = dt1.Rows[1]["termsdesc"].ToString();
            string txtFlatno = dt1.Rows[2]["termsdesc"].ToString();
            string txtReqno = mrfno1;
            string txtInword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);
            string ftReqIn = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string ftReqApp = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ftOrder = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string ftWorkOrd = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string ftMrRecv = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string ftBillConf = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();

            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            if (pactcode == "160100010025" || pactcode == "160100010027")
            {
                rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmBridgeWithoutLogo", list, null, null);
                rpt.EnableExternalImages = true;
            }
            else
            {
                rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillConfirmationBridge", list, null, null);
                rpt.EnableExternalImages = true;
                rpt.SetParameters(new ReportParameter("compName", comnam));
            }
            rpt.SetParameters(new ReportParameter("txtTitle", "Bill Confirmation Certificate"));
            rpt.SetParameters(new ReportParameter("txtComment", "( Comments On Enclosed Bills )"));
            rpt.SetParameters(new ReportParameter("txtProjName", txtProjName));
            rpt.SetParameters(new ReportParameter("txtBuildno", txtBuildno));
            rpt.SetParameters(new ReportParameter("txtFloorno", txtFloorno));
            rpt.SetParameters(new ReportParameter("txtFlatno", txtFlatno));
            rpt.SetParameters(new ReportParameter("txtSupName", "Supplier Name :  " + txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillid", txtBillid));
            rpt.SetParameters(new ReportParameter("txtBilldate", CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", billref));
            rpt.SetParameters(new ReportParameter("txtReqno", txtReqno));
            rpt.SetParameters(new ReportParameter("txtDepo", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", txtNetAmt));
            rpt.SetParameters(new ReportParameter("txtInword", txtInword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("ftReqIn", ftReqIn));
            rpt.SetParameters(new ReportParameter("ftReqApp", ftReqApp));
            rpt.SetParameters(new ReportParameter("ftOrder", ftOrder));
            rpt.SetParameters(new ReportParameter("ftWorkOrd", ftWorkOrd));
            rpt.SetParameters(new ReportParameter("ftMrRecv", ftMrRecv));
            rpt.SetParameters(new ReportParameter("ftBillConf", ftBillConf));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("lblSecurity", lblSecurity));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }


        }

        private void PrintBill05()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            string txtnar = this.txtBillNarr.Text.Trim();
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //
            // rdlc start
            string inword = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            string refno = this.txtBillRef.Text;
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString();
            string suppname = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string billno = ds1.Tables[1].Rows[0]["billno1"].ToString();
            string narration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();


            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam1"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoInns", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", suppname));
            rpt.SetParameters(new ReportParameter("txtMrfno", " : " + mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", " : " + orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", " : " + refno));
            rpt.SetParameters(new ReportParameter("txtChalan", " : " + chlno));
            rpt.SetParameters(new ReportParameter("txtBilldate", " : " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", " : " + billno));
            rpt.SetParameters(new ReportParameter("txtMrrno", " : " + mrrno));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", narration));
            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            // rdlc end
        }

        private void PrintBillCPDL()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            string txtnar = this.txtBillNarr.Text.Trim();
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //
            // rdlc start
            string inword = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ASTUtility.CustomReqFormat(ds1.Tables[0].Rows[0]["orderno"].ToString());
            string refno = this.txtBillRef.Text;
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string mrrno = ASTUtility.CustomReqFormat(ds1.Tables[0].Rows[0]["mrrno"].ToString());
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString();
            string suppname = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            string billno = ASTUtility.CustomReqFormat(ds1.Tables[1].Rows[0]["billno"].ToString());            
            string mrrref = ds1.Tables[0].Rows[0]["mrrref"].ToString();             
            string narration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();



            string txtMrrdate = ds1.Tables[0].Rows[0]["mrrdate"].ToString();
            string txtReqno = ASTUtility.CustomReqFormat(ds1.Tables[0].Rows[0]["reqno"].ToString());           
            string txtChalandate = ds1.Tables[0].Rows[0]["challandat"].ToString();
            string txtPodate = ds1.Tables[3].Rows[0]["orddat"].ToString();
            string txtBillref = ds1.Tables[1].Rows[0]["billref"].ToString();
            string billdat = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd-MMM-yyyy");


            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam1"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoCPDL", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", suppname));
            rpt.SetParameters(new ReportParameter("txtMrfno", mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", refno));
            rpt.SetParameters(new ReportParameter("txtChalan",chlno));
            rpt.SetParameters(new ReportParameter("txtBilldate", billdat));
            rpt.SetParameters(new ReportParameter("txtBillno",billno));
            rpt.SetParameters(new ReportParameter("txtMrrno",mrrno));
            rpt.SetParameters(new ReportParameter("mrrref", mrrref));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", narration));

            rpt.SetParameters(new ReportParameter("txtMrrdate", txtMrrdate));
            rpt.SetParameters(new ReportParameter("txtReqno", txtReqno));
            rpt.SetParameters(new ReportParameter("txtChalandate", txtChalandate));
            rpt.SetParameters(new ReportParameter("txtPodate", txtPodate));
            rpt.SetParameters(new ReportParameter("txtBillref", txtBillref));

            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            // rdlc end


        }

        private void PrintBill07()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string CurDate1 = "";
            string mBILLNo = "";
            string txtnar = this.txtBillNarr.Text.Trim();
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //
            string suppname, billno, narration, refno;
            if (isAccBill)
            {
                suppname = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                billno = ds1.Tables[1].Rows[0]["billno1"].ToString();
                narration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                refno = ds1.Tables[1].Rows[0]["billref"].ToString();
            }
            else
            {
                suppname = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
                billno = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                narration = this.txtBillNarr.Text.Trim();
                refno = this.txtBillRef.Text;
            }
            // rdlc start
            string inword = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);



            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam1"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoManama", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", suppname));
            rpt.SetParameters(new ReportParameter("txtMrfno", " : " + mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", " : " + orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", " : " + refno));
            rpt.SetParameters(new ReportParameter("txtChalan", " : " + chlno));
            rpt.SetParameters(new ReportParameter("txtBilldate", " : " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", " : " + billno));
            rpt.SetParameters(new ReportParameter("txtMrrno", " : " + mrrno));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", "Narration : " + narration));
            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            // rdlc end




        }

        private void PrintBill08()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string CurDate1 = "";
            string mBILLNo = "";
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            //

            string txtSupName, txtBillid, txtNarration, billref;
            if (isAccBill)
            {
                txtSupName = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
                txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
            }
            else
            {
                txtSupName = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
                txtBillid = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                txtNarration = this.txtBillNarr.Text.Trim();
                billref = this.txtBillRef.Text;
            }
            // rdlc start
            string inword = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);

            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam1"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();
            string rptReqChk = ds1.Tables[3].Rows[0]["checknam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["checkdat"].ToString();


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoJbs", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", txtSupName));
            rpt.SetParameters(new ReportParameter("txtMrfno", " : " + mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", " : " + orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", " : " + billref));
            rpt.SetParameters(new ReportParameter("txtChalan", " : " + chlno));
            rpt.SetParameters(new ReportParameter("txtBilldate", " : " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", " : " + txtBillid));
            rpt.SetParameters(new ReportParameter("txtMrrno", " : " + mrrno));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("rptReqChk", rptReqChk));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            // rdlc end

        }
        private void PrintBillLanco() 
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string CurDate1 = "";
            string mBILLNo = "";
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];
            string mrrref = "", chlnno = "";
            var dr01 = (from DataRow rows in dt.Rows
                        select rows["mrrref"]).Distinct();

            foreach (var rows in dr01)
            {
                mrrref += rows + ", ";
            }

            var dr02 = (from DataRow rows in dt.Rows
                        select rows["chlnno"]).Distinct();

            foreach (var rows in dr02)
            {
                chlnno += rows + ", ";
            }
            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            string totalamt = Convert.ToDouble(amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");
            //

            string txtSupName, txtBillid, txtNarration, billref;
            if (isAccBill)
            {
                txtSupName = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
                txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
            }
            else
            {
                txtSupName = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
                txtBillid = this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                txtNarration = this.txtBillNarr.Text.Trim();
                billref = this.txtBillRef.Text;
            }
            // rdlc start
            string inword = "Taka In Word: " + ASTUtility.Trans((amt1 - amt2), 2);
            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);

            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();
            string rptReqChk = ds1.Tables[3].Rows[0]["checknam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["checkdat"].ToString();


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoLanco", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", txtSupName));
            rpt.SetParameters(new ReportParameter("txtMrfno", mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", billref));
            rpt.SetParameters(new ReportParameter("txtChalan", chlnno.Substring(0, chlnno.Length - 2)));
            rpt.SetParameters(new ReportParameter("txtBilldate", CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", txtBillid));
            rpt.SetParameters(new ReportParameter("txtMrrno", mrrno));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("txtmrrref", mrrref.Substring(0, mrrref.Length - 2)));
            rpt.SetParameters(new ReportParameter("rptReqChk", rptReqChk));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));
            rpt.SetParameters(new ReportParameter("totalamt", totalamt));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            // rdlc end

        }

        private void PrintBillFinlay()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            string txtnar = this.txtBillNarr.Text.Trim();
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));

            double security, deduction, penalty, advanced;
            string txtSupName, percntge;
            if (isAccBill)
            {
                security = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(sdamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(sdamt)", "")));
                deduction = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(dedamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(dedamt)", "")));
                penalty = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(penamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(penamt)", "")));
                advanced = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(advamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(advamt)", "")));

                percntge = Convert.ToDouble("0" + ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); 0%");
                txtSupName = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
            }
            else
            {
                security = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString()));
                deduction = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString()));
                penalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString()));
                advanced = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString()));
                percntge = this.txtpercentage.Text.ToString();
                txtSupName = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
            }

            // rdlc start
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);

            string txtDepo = Convert.ToDouble(security).ToString("#,##0.00;(#,##0.00); ");
            string txtAdv = Convert.ToDouble(advanced).ToString("#,##0.00;(#,##0.00); ");
            string txtPenalty = Convert.ToDouble(penalty).ToString("#,##0.00;(#,##0.00); ");
            string txtDeduc = Convert.ToDouble(deduction).ToString("#,##0.00;(#,##0.00); ");

            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));
            string inword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);
            string netamt = Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            string mrfno = ds1.Tables[1].Rows[0]["mrfno"].ToString();
            string orderno = ds1.Tables[0].Rows[0]["orderno1"].ToString();
            string refno = this.txtBillRef.Text;
            string chlno = ds1.Tables[0].Rows[0]["chlnno"].ToString();
            string billno = ds1.Tables[1].Rows[0]["billno1"].ToString();
            string narration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();


            ////Signing Part

            string reqname = ds1.Tables[3].Rows[0]["reqnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqdat"].ToString();
            string reqapname = ds1.Tables[3].Rows[0]["reqanam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["reqadat"].ToString();
            string ordpro = ds1.Tables[3].Rows[0]["appnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["appdat"].ToString();
            string purchord = ds1.Tables[3].Rows[0]["ordnam1"].ToString() + "\n" + ds1.Tables[3].Rows[0]["orddat"].ToString();
            string recvby = ds1.Tables[3].Rows[0]["mrrnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["mrrdat"].ToString();
            string billname = ds1.Tables[3].Rows[0]["billnam"].ToString() + "\n" + ds1.Tables[3].Rows[0]["billdat"].ToString();


            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoFinlay", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", txtSupName));
            rpt.SetParameters(new ReportParameter("txtMrfno", mrfno));
            rpt.SetParameters(new ReportParameter("txtPono", orderno));
            rpt.SetParameters(new ReportParameter("txtRefno", refno));
            rpt.SetParameters(new ReportParameter("txtChalan", chlno));
            rpt.SetParameters(new ReportParameter("txtBilldate", CurDate1));
            rpt.SetParameters(new ReportParameter("txtBillno", billno));
            rpt.SetParameters(new ReportParameter("txtMrrno", mrrno));
            rpt.SetParameters(new ReportParameter("txtProjectName", projectName));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", narration));
            rpt.SetParameters(new ReportParameter("ftReqIn", reqname));
            rpt.SetParameters(new ReportParameter("ftReqapv", reqapname));
            rpt.SetParameters(new ReportParameter("ftOrdpro", ordpro));
            rpt.SetParameters(new ReportParameter("ftPurchord", purchord));
            rpt.SetParameters(new ReportParameter("ftRecvby", recvby));
            rpt.SetParameters(new ReportParameter("ftBillconf", billname));
            rpt.SetParameters(new ReportParameter("txtSecurity", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", netamt.ToString()));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comlogo", ComLogo));

            Session["Report1"] = rpt;
            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            /*
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string CurDate1 = "";
            string mBILLNo = "";
            bool isAccBill = this.isBillFromAcc();
            if (isAccBill)
            {
                CurDate1 = this.Request.QueryString["Date1"].ToString();
                mBILLNo = this.Request.QueryString["genno"].ToString();
            }
            else
            {
                CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
                mBILLNo = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            }


            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GENPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[0];

            //For Tk In Word

            DataTable td1 = dt.Copy();
            DataTable td2 = dt.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like'019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));
            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));


            double security, deduction, penalty, advanced;
            string txtSupName, txtBillid, txtNarration, billref, percntge;
            if (isAccBill)
            {
                security = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(sdamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(sdamt)", "")));
                deduction = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(dedamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(dedamt)", "")));
                penalty = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(penamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(penamt)", "")));
                advanced = Convert.ToDouble((Convert.IsDBNull(ds1.Tables[1].Compute("Sum(advamt)", "")) ? 0.00 : ds1.Tables[1].Compute("Sum(advamt)", "")));

                percntge = Convert.ToDouble("0" + ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00); 0%");
                txtSupName = "Supplier Name: " + ds1.Tables[1].Rows[0]["ssirdesc"].ToString();
                txtBillid = ds1.Tables[1].Rows[0]["billno1"].ToString();
                txtNarration = "Narration : " + ds1.Tables[1].Rows[0]["billnar"].ToString();
                billref = ds1.Tables[1].Rows[0]["billref"].ToString();
            }
            else
            {
                security = (Convert.ToDouble("0" + this.txtSDAmount.Text.ToString()));
                deduction = (Convert.ToDouble("0" + this.txtDedAmount.Text.ToString()));
                penalty = (Convert.ToDouble("0" + this.txtPenaltyAmount.Text.ToString()));
                advanced = (Convert.ToDouble("0" + this.txtAdvanced.Text.ToString()));
                percntge = this.txtpercentage.Text.ToString();
                txtSupName = "Supplier Name: " + this.ddlSupList.SelectedItem.Text.Trim();
                txtBillid = "Bill No: " + this.lblCurBillNo1.Text.Trim() + this.txtCurBillNo2.Text.Trim();
                txtNarration = "Narration : " + this.txtBillNarr.Text.Trim();
            }

            // rdlc start
            string mrrno = ds1.Tables[0].Rows[0]["mrrno1"].ToString();
            string projectName = "Project Name : " + ds1.Tables[0].Rows[0]["pactdesc"].ToString().Substring(4);

            string txtDepo = Convert.ToDouble(security).ToString("#,##0.00;(#,##0.00); ");
            string txtAdv = Convert.ToDouble(advanced).ToString("#,##0.00;(#,##0.00); ");
            string txtPenalty = Convert.ToDouble(penalty).ToString("#,##0.00;(#,##0.00); ");
            string txtDeduc = Convert.ToDouble(deduction).ToString("#,##0.00;(#,##0.00); ");

            double netAmount = (amt1 - amt2 - (security + deduction + penalty + advanced));
            string inword = "Taka In Word: " + ASTUtility.Trans((netAmount), 2);
            string netamt = Convert.ToDouble(netAmount).ToString("#,##0.00;(#,##0.00); ");

            //
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptBillConfirmation01>();
            LocalReport rpt = new LocalReport();
            rpt = RptSetupClass1.GetLocalReport("R_14_Pro.RptBillInfoFinlay", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("compName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Software Generated Bill"));
            rpt.SetParameters(new ReportParameter("supname", txtSupName));
            rpt.SetParameters(new ReportParameter("txtBillno", txtBillid));
            rpt.SetParameters(new ReportParameter("date1", "Date: " + CurDate1));
            rpt.SetParameters(new ReportParameter("txtInword", inword));
            rpt.SetParameters(new ReportParameter("txtNarration", txtNarration));
            rpt.SetParameters(new ReportParameter("txtSecurity", txtDepo));
            rpt.SetParameters(new ReportParameter("txtAdv", txtAdv));
            rpt.SetParameters(new ReportParameter("txtPenalty", txtPenalty));
            rpt.SetParameters(new ReportParameter("txtDeduc", txtDeduc));
            rpt.SetParameters(new ReportParameter("txtNetAmt", netamt.ToString()));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;

            if (isAccBill)
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
            }
            else
            {
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            */
        }

        private string CompanyLength()
        {
            string comcod = this.GetCompCode();
            string length = "";
            switch (comcod)
            {
                case "3101":
                case "3340":
                    length = "length";
                    break;


                default:
                    length = "";
                    break;
            }

            return length;

        }

        protected void lbtnPrevBillList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string length = this.CompanyLength();
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string qgenno = this.Request.QueryString["genno"] ?? "";
            string SearchPBL = (qgenno.Length == 0 ? "%" : this.Request.QueryString["genno"].ToString()) + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPREVBILLLIST", CurDate1, length, usrid, SearchPBL, "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlPrevBillList.Items.Clear();
            this.ddlPrevBillList.DataTextField = "billno1";
            this.ddlPrevBillList.DataValueField = "billno";
            this.ddlPrevBillList.DataSource = ds1.Tables[0];
            this.ddlPrevBillList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            if (this.lbtnOk.Text == "New")
            {

                //this.lblPrevious.Visible = true;
                //this.txtsrchPrevious.Visible = true;

                this.lbtnPrevBillList.Visible = true;
                this.ddlPrevBillList.Visible = true;
                this.ddlPrevBillList.Items.Clear();
                this.ddlSupList.Enabled = true;
                this.lbtnSupplierList.Enabled = true;
                this.lblCurBillNo1.Text = "PBL" + DateTime.Today.ToString("MM") + "-";
                this.txtCurBillDate.Enabled = true;
                this.txtBillRef.Text = "";


                this.chkCharging.Checked = false;
                this.chkCharging_CheckedChanged(null, null);
                this.ddlMRRList.Items.Clear();
                this.ddlSupList.Items.Clear();
                this.ddlOrderList.Items.Clear();
                ViewState.Remove("tblproject");
                this.ddlProjectName.Items.Clear();

                this.ddlCharge.Items.Clear();
                this.txtPreparedBy.Text = "";
                this.txtApprovedBy.Text = "";
                this.txtBillNarr.Text = "";
                this.txtAccVounum.Text = "";
                this.gvBillInfo.DataSource = null;
                this.gvBillInfo.DataBind();
                this.Panel1.Visible = false;
                this.Panel2.Visible = false;
                // this.txtpercentage.Text = "";
                this.txtSDAmount.Text = "";
                this.txtDedAmount.Text = "";
                this.txtPenaltyAmount.Text = "";
                this.txtAdvanced.Text = "";
                this.lblvalnettotal.Text = "";

                //Session.Remove("tblAttDocs");
                //ListViewEmpAll.DataSource = null;
                //ListViewEmpAll.DataBind();
                //ListViewEmpAll.Items.Clear();

                this.lbtnOk.Text = "Ok";
                return;
            }
            if (comcod == "3336" || comcod == "3337")
            {
                this.datearea.Visible = false;
            }
            //this.lblPrevious.Visible = false;
            //this.txtsrchPrevious.Visible = false;
            this.lbtnPrevBillList.Visible = false;
            this.ddlPrevBillList.Visible = false;
            this.lbtnSupplierList.Enabled = false;
            this.ddlSupList.Enabled = false;
            this.txtCurBillNo2.ReadOnly = true;
            this.Panel1.Visible = true;
            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.Get_PurchaseBill_Info();
            this.GetOrderList();

            this.createTable();
        }



        protected void Session_tblBill_Update()
        {

            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            int TblRowIndex2;

            for (int j = 0; j < this.gvBillInfo.Rows.Count; j++)
            {
                double dgvMRRQty = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvBillInfo.Rows[j].FindControl("lblgvMRRQty")).Text.Trim()));
                double dgvMRRAmt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvBillInfo.Rows[j].FindControl("txtgvMRRAmt")).Text.Trim()));
                double mmrramt = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((Label)this.gvBillInfo.Rows[j].FindControl("lblgvmMRRAmt")).Text.Trim()));
                double dgvMRRRate = (dgvMRRQty > 0) ? dgvMRRAmt / dgvMRRQty : 00;
                TblRowIndex2 = (this.gvBillInfo.PageIndex) * this.gvBillInfo.PageSize + j;

                string mRSIRCODE = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();

                //if (ASTUtility.Left(mRSIRCODE, 7) != "0199999")
                //{

                //    if (mmrramt < dgvMRRAmt)
                //    {
                //        ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                //        ((Label)this.Master.FindControl("lblmsg")).Text = "Management amount must be greater then bill amount";
                //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                //        return;

                //    }





                //}




                //. mmrramt

                tbl1.Rows[TblRowIndex2]["mrrqty"] = dgvMRRQty;
                tbl1.Rows[TblRowIndex2]["mrrrate"] = dgvMRRRate;
                tbl1.Rows[TblRowIndex2]["mrramt"] = dgvMRRAmt;
            }
            ViewState["tblBill"] = tbl1;
        }

        private void GridColoumnVisible()
        {  //Only For Sanmar
            string comcod = this.GetCompCode();
            if (comcod != "3301")
                return;
            if (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry")
            {


                DataTable tbl1 = (DataTable)ViewState["tblBill"];
                int TblRowIndex2;
                for (int j = 0; j < this.gvBillInfo.Rows.Count; j++)
                {

                    TblRowIndex2 = (this.gvBillInfo.PageIndex) * this.gvBillInfo.PageSize + j;
                    string mRSIRCODE = tbl1.Rows[TblRowIndex2]["rsircode"].ToString();
                    if (ASTUtility.Left(mRSIRCODE, 7) != "0199999")
                        ((TextBox)this.gvBillInfo.Rows[j].FindControl("txtgvMRRAmt")).ReadOnly = true;

                }

            }

            //Added



        }

        protected void gvBillInfo_DataBind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            this.gvBillInfo.DataSource = tbl1;
            this.gvBillInfo.DataBind();

            //For Visible Item Serial Manama
            string comcod = GetCompCode();
            if (comcod == "3353" || comcod == "3101")
            {
                this.gvBillInfo.Columns[1].Visible = true;
            }

            this.gvBillInfo.Columns[9].Visible = (this.Request.QueryString["Type"].ToString().Trim() == "BillEdit" && this.lblvalvounum.Text.Trim() == "00000000000000");
            ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnUpdateBill")).Visible = (this.lblvalvounum.Text.Trim() == "00000000000000" || this.lblvalvounum.Text.Trim() == "");
            ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnDeleteBill")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "BillEdit" && this.lblvalvounum.Text.Trim() == "00000000000000");


            //Adeed By Nime

            ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnUpdateBill")).Visible = ((this.Request.QueryString["Type"].ToString().Trim() == "BillEntry") || (this.Request.QueryString["Type"].ToString().Trim() == "BillEdit"));

            ((LinkButton)this.gvBillInfo.FooterRow.FindControl("lbtnFinalUpdateBill")).Visible = ((this.Request.QueryString["Type"].ToString().Trim() == "BillEntryAudit"));



            //End Of Adding

            this.GridColoumnVisible();
            ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Visible = false;
            double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.gvBillInfo.PageSize);
            ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Items.Clear();
            for (int i = 1; i <= TotalPage; i++)
                ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
            if (TotalPage > 1)
                ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).Visible = true;
            ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.gvBillInfo.PageIndex;
            this.lbtnResFooterTotal_Click(null, null);
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            DataView dv = dt1.DefaultView;
            dv.Sort = "grp1, rsircode";
            dt1 = dv.ToTable();
            string rsircode = dt1.Rows[0]["rsircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                {
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
                    dt1.Rows[j]["rsirdesc1"] = "";
                }

                else
                    rsircode = dt1.Rows[j]["rsircode"].ToString();
            }

            return dt1;
        }



        protected void GetBillNo()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string mBILLNo = "NEWBILL";
            if (this.ddlPrevBillList.Items.Count > 0)
                mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();
            if (mBILLNo == "NEWBILL")
            {
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTBILLINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurBillNo1.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.txtCurBillNo2.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                    this.ddlPrevBillList.DataTextField = "maxbillno1";
                    this.ddlPrevBillList.DataValueField = "maxbillno";
                    this.ddlPrevBillList.DataSource = ds1.Tables[0];
                    this.ddlPrevBillList.DataBind();
                }

            }
        }


        protected void Get_PurchaseBill_Info()
        {

            string comcod = this.GetCompCode();
            string CurDate1 = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string mBILLNo = "NEWBILL";

            //string billno = this.Request.QueryString["billno"] ?? "";

            //if (billno.Length > 0)
            //    mBILLNo = billno;
            if (this.ddlPrevBillList.Items.Count > 0)
            {

                this.txtCurBillDate.Enabled = false;
                mBILLNo = this.ddlPrevBillList.SelectedValue.ToString();

            }
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", mBILLNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblBill"] = this.HiddenSameData(ds1.Tables[0]);

            ViewState["tblimages"] = ds1.Tables[2];
            ListViewEmpAll.DataSource = ds1.Tables[2];
            ListViewEmpAll.DataBind();


            this.btnShowimg_Click(null, null);
            if (mBILLNo == "NEWBILL")
            {
                ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLASTBILLINFO", CurDate1, "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    this.lblCurBillNo1.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(0, 6);
                    this.txtCurBillNo2.Text = ds1.Tables[0].Rows[0]["maxbillno1"].ToString().Substring(6, 5);
                }
                return;
            }

            // this.lbtnSupplierList_Click(null, null); // Change Emdad 03.10.2020

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.ddlSupList.DataTextField = "ssirdesc";
                this.ddlSupList.DataValueField = "ssircode";
                this.ddlSupList.DataSource = ds1.Tables[1];
                this.ddlSupList.DataBind();
            }
            this.ddlSupList.SelectedValue = ds1.Tables[1].Rows[0]["ssircode"].ToString();


            this.txtBillRef.Text = ds1.Tables[1].Rows[0]["billref"].ToString();
            this.lblCurBillNo1.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(0, 6);
            this.txtCurBillNo2.Text = ds1.Tables[1].Rows[0]["billno1"].ToString().Substring(6, 5);
            this.txtCurBillDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billdat"]).ToString("dd.MM.yyyy");
            this.txtPreparedBy.Text = ds1.Tables[1].Rows[0]["billbydes"].ToString();
            this.txtApprovedBy.Text = ds1.Tables[1].Rows[0]["appbydes"].ToString();
            this.txtApprovalDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["apprdat"]).ToString("dd.MM.yyyy");
            this.txtBillNarr.Text = ds1.Tables[1].Rows[0]["billnar"].ToString();
            this.txtAccVounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.lblvalvounum.Text = ds1.Tables[1].Rows[0]["vounum"].ToString();
            this.txtBillrefDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["billrefdat"]).ToString("dd.MM.yyyy");
            this.txtChequeDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["chequedat"]).ToString("dd.MM.yyyy");
            this.ddlPayType.SelectedValue = ds1.Tables[1].Rows[0]["paytype"].ToString();
            this.txtpercentage.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["percntge"]).ToString("#,##0.00;(#,##0.00);") + "%";
            this.txtSDAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["sdamt"]).ToString("#,##0.00;(#,##0.00);");
            this.txtDedAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["dedamt"]).ToString("#,##0.00;(#,##0.00);");
            this.txtPenaltyAmount.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["penamt"]).ToString("#,##0.00;(#,##0.00);");
            this.txtAdvanced.Text = Convert.ToDouble(ds1.Tables[1].Rows[0]["advamt"]).ToString("#,##0.00;(#,##0.00);");
            this.ddlPayType_SelectedIndexChanged(null, null);
            this.gvBillInfo_DataBind();



        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlSupList.Items.Clear();
        }
        protected void lbtnSelectRes_Click(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            string mOrderno = this.ddlOrderList.SelectedValue.ToString();

            string mReqno = this.ddlMRRList.SelectedValue.ToString().Substring(0, 14);
            string mMRRNo = this.ddlMRRList.SelectedValue.ToString().Substring(14, 14);
            string mResCode = this.ddlMRRList.SelectedValue.ToString().Substring(28, 12);
            string mSpcfCode = this.ddlMRRList.SelectedValue.ToString().Substring(40, 12);
            string pactcode = this.ddlMRRList.SelectedValue.ToString().Substring(52, 12);
            DataRow[] dr2 = tbl1.Select("pactcode='" + pactcode + "' and  reqno='" + mReqno + "' and orderno='" + mOrderno + "' and mrrno = '" + mMRRNo + "' and rsircode = '" +
                        mResCode + "' and spcfcod = '" + mSpcfCode + "'");


            if (dr2.Length == 0)
            {
                //Advanced

                DataRow[] dro = tbl1.Select("orderno='" + mOrderno + "'");
                if (dro.Length == 0)
                {
                    DataRow[] drodet = ((DataTable)ViewState["tblSupOrd"]).Select("valuefield='" + mOrderno + "'");
                    double advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
                    advamt = advamt + Convert.ToDouble(drodet[0]["advamt"]);
                    this.txtAdvanced.Text = advamt.ToString("#,##0;(#,##0); ");



                }
                DataTable dtorder = (DataTable)ViewState["tblSupOrd"];
                DataRow dr1 = tbl1.NewRow();
                dr1["pactcode"] = pactcode;
                dr1["reqno"] = mReqno;
                dr1["orderno"] = mOrderno;
                dr1["orderno1"] = this.ddlOrderList.SelectedItem.Text.Substring(0, 11);

                dr1["mrrno"] = mMRRNo;
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCode;
                DataTable tbl2 = (DataTable)Session["tblMat"];
                DataRow[] dr3 = tbl2.Select(" pactcode='" + pactcode + "' and reqno='" + mReqno + "' and mrrno = '" + mMRRNo + "' and rsircode = '" +
                        mResCode + "' and spcfcod = '" + mSpcfCode + "'");
                dr1["oissueno"] = dr3[0]["oissueno"];
                dr1["reqno1"] = dr3[0]["reqno1"];
                dr1["mrrno1"] = dr3[0]["mrrno1"];
                dr1["mrrref"] = dr3[0]["mrrref"];
                dr1["chlnno"] = dr3[0]["chlnno"];
                dr1["challandat"] = dr3[0]["challandat"];
                dr1["mrrdat"] = dr3[0]["mrrdat"];
                dr1["pactdesc"] = dr3[0]["pactdesc"];
                dr1["rsirdesc1"] = dr3[0]["rsirdesc1"];
                dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                dr1["rsirunit"] = dr3[0]["rsirunit"];
                dr1["mrrqty"] = dr3[0]["mrrqty"];
                dr1["mrrrate"] = dr3[0]["mrrrate"];
                dr1["mrramt"] = dr3[0]["mrramt"];
                dr1["mmrramt"] = dr3[0]["mrramt"];
                dr1["remrks"] = dr3[0]["mrrnote"];
                dr1["rowid"] = dr3[0]["rowid"];
                dr1["boqrate"] = dr3[0]["boqrate"];
                tbl1.Rows.Add(dr1);
            }


            //Carring FirstTime

            DataRow[] drcar = tbl1.Select("mrrno =''");
            if (drcar.Length == 0)
            {
                DataTable dt = (DataTable)ViewState["tblcar"];
                foreach (DataRow drc in dt.Rows)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["pactcode"] = drc["pactcode"].ToString();

                    dr1["reqno"] = "";
                    dr1["mrrno"] = "";
                    dr1["rsircode"] = drc["rsircode"].ToString();
                    dr1["spcfcod"] = drc["spcfcod"].ToString();
                    dr1["reqno1"] = "";
                    dr1["mrrno1"] = "";
                    dr1["mrrref"] = "";
                    dr1["chlnno"] = "";
                    dr1["challandat"] = "";
                    dr1["pactdesc"] = drc["pactdesc"].ToString();
                    dr1["rsirdesc1"] = drc["rsirdesc"].ToString();
                    dr1["spcfdesc"] = "";
                    dr1["rsirunit"] = "";
                    dr1["mrrqty"] = 0.00;
                    dr1["mrrrate"] = 0.00;
                    dr1["mrramt"] = drc["orderamt"];
                    dr1["mmrramt"] = 0.00;
                    dr1["remrks"] = "";
                    tbl1.Rows.Add(dr1);

                }
            }

            ViewState["tblBill"] = this.HiddenSameData(tbl1);
            this.gvBillInfo_DataBind();

        }

        protected void lbtnSelectMRR_Click(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            DataTable tbl2 = (DataTable)Session["tblMat"];
            string mMRRNo = this.ddlMRRList.SelectedValue.ToString().Substring(14, 14);
            string mOrderno = "";
            DataRow[] dr = tbl1.Select("mrrno = '" + mMRRNo + "'");
            if (dr.Length == 0)
            {
                DataView dv = tbl2.DefaultView;
                dv.RowFilter = ("mrrno='" + mMRRNo + "'");
                tbl2 = dv.ToTable();
                for (int i = 0; i < tbl2.Rows.Count; i++)
                {
                    mOrderno = this.ddlOrderList.SelectedValue.ToString();
                    DataRow[] dro = tbl1.Select("orderno='" + mOrderno + "'");
                    if (dro.Length == 0)
                    {

                        DataRow[] drodet = ((DataTable)ViewState["tblSupOrd"]).Select("valuefield='" + mOrderno + "'");
                        double advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
                        advamt = advamt + Convert.ToDouble(drodet[0]["advamt"]);
                        this.txtAdvanced.Text = advamt.ToString("#,##0;(#,##0); ");

                    }

                    DataRow dr1 = tbl1.NewRow();

                    dr1["pactcode"] = tbl2.Rows[i]["pactcode"].ToString();
                    dr1["reqno"] = tbl2.Rows[i]["reqno"].ToString();
                    dr1["reqno1"] = tbl2.Rows[i]["reqno1"].ToString();
                    dr1["orderno"] = this.ddlOrderList.SelectedValue.ToString();
                    dr1["orderno1"] = this.ddlOrderList.SelectedItem.Text.Substring(0, 11);
                    dr1["oissueno"] = tbl2.Rows[i]["oissueno"].ToString();
                    dr1["mrrno"] = mMRRNo;
                    dr1["mrrno1"] = tbl2.Rows[i]["mrrno1"].ToString();
                    dr1["mrrref"] = tbl2.Rows[i]["mrrref"].ToString();
                    dr1["chlnno"] = tbl2.Rows[i]["chlnno"].ToString();
                    dr1["challandat"] = tbl2.Rows[i]["challandat"].ToString();
                    dr1["mrrdat"] = tbl2.Rows[i]["mrrdat"].ToString();

                    dr1["rsircode"] = tbl2.Rows[i]["rsircode"].ToString();
                    dr1["spcfcod"] = tbl2.Rows[i]["spcfcod"].ToString();
                    dr1["pactdesc"] = tbl2.Rows[i]["pactdesc"].ToString();
                    dr1["rsirdesc1"] = tbl2.Rows[i]["rsirdesc1"].ToString();
                    dr1["spcfdesc"] = tbl2.Rows[i]["spcfdesc"].ToString();
                    dr1["rsirunit"] = tbl2.Rows[i]["rsirunit"].ToString();
                    dr1["mrrqty"] = tbl2.Rows[i]["mrrqty"].ToString();
                    dr1["mrrrate"] = tbl2.Rows[i]["mrrrate"].ToString();
                    dr1["mrramt"] = tbl2.Rows[i]["mrramt"].ToString();
                    dr1["mmrramt"] = tbl2.Rows[i]["mrramt"].ToString();
                    dr1["remrks"] = tbl2.Rows[i]["mrrnote"].ToString();
                    dr1["rowid"] = tbl2.Rows[i]["rowid"].ToString();
                    dr1["boqrate"] = tbl2.Rows[i]["boqrate"].ToString();
                    tbl1.Rows.Add(dr1);
                }
            }



            //Carring FirstTime

            DataRow[] drcar = tbl1.Select("mrrno =''");
            if (drcar.Length == 0)
            {
                DataTable dt = (DataTable)ViewState["tblcar"];
                foreach (DataRow drc in dt.Rows)
                {
                    DataRow dr1 = tbl1.NewRow();
                    dr1["pactcode"] = drc["pactcode"].ToString();

                    dr1["reqno"] = "";
                    dr1["mrrno"] = "";
                    dr1["rsircode"] = drc["rsircode"].ToString();
                    dr1["spcfcod"] = drc["spcfcod"].ToString();
                    dr1["reqno1"] = "";
                    dr1["mrrno1"] = "";
                    dr1["mrrref"] = "";
                    dr1["chlnno"] = "";
                    dr1["challandat"] = "";
                    dr1["pactdesc"] = drc["pactdesc"].ToString();
                    dr1["rsirdesc1"] = drc["rsirdesc"].ToString();
                    dr1["spcfdesc"] = "";
                    dr1["rsirunit"] = "";
                    dr1["mrrqty"] = 0.00;
                    dr1["mrrrate"] = 0.00;
                    dr1["mrramt"] = drc["orderamt"];
                    dr1["mmrramt"] = 0.00;
                    dr1["remrks"] = "";
                    tbl1.Rows.Add(dr1);

                }
            }

            ViewState["tblBill"] = this.HiddenSameData(tbl1);
            this.gvBillInfo_DataBind();
        }


        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {

            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            DataTable tbl2 = (DataTable)Session["tblMat"];
            // string mMRRNo = this.ddlMRRList.SelectedValue.ToString().Substring(14, 14);
            string mOrderno = this.ddlOrderList.SelectedValue.ToString();
            //string[] arrItems = new string[ddlMRRList.Items.Count];
            //if (ddlMRRList.Items.Count > 0)
            //{
            //    for (int i = 0; i < ddlMRRList.Items.Count; i++)
            //    {
            //        arrItems[i] = ddlMRRList.Items[i].Value.ToString();
            //    }
            //}

            foreach (ListItem Item in ddlMRRList.Items)
            {
                string mReqno = Item.Value.Substring(0, 14);
                string mMRRNo = Item.Value.Substring(14, 14);
                string mResCode = Item.Value.Substring(28, 12);
                string mSpcfCode = Item.Value.Substring(40, 12);
                string pactcode = Item.Value.Substring(52, 12);
                DataRow[] dr2 = tbl1.Select("pactcode='" + pactcode + "' and  reqno='" + mReqno + "' and orderno='" + mOrderno + "' and mrrno = '" + mMRRNo + "' and rsircode = '" +
                            mResCode + "' and spcfcod = '" + mSpcfCode + "'");


                if (dr2.Length == 0)
                {
                    //Advanced

                    DataRow[] dro = tbl1.Select("orderno='" + mOrderno + "'");
                    if (dro.Length == 0)
                    {
                        DataRow[] drodet = ((DataTable)ViewState["tblSupOrd"]).Select("valuefield='" + mOrderno + "'");
                        double advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
                        advamt = advamt + Convert.ToDouble(drodet[0]["advamt"]);
                        this.txtAdvanced.Text = advamt.ToString("#,##0;(#,##0); ");



                    }
                    DataTable dtorder = (DataTable)ViewState["tblSupOrd"];
                    DataRow dr1 = tbl1.NewRow();
                    dr1["pactcode"] = pactcode;
                    dr1["reqno"] = mReqno;
                    dr1["orderno"] = mOrderno;
                    dr1["orderno1"] = this.ddlOrderList.SelectedItem.Text.Substring(0, 11);

                    dr1["mrrno"] = mMRRNo;
                    dr1["rsircode"] = mResCode;
                    dr1["spcfcod"] = mSpcfCode;
                    //  DataTable tbl2 = (DataTable)Session["tblMat"];
                    DataRow[] dr3 = tbl2.Select(" pactcode='" + pactcode + "' and reqno='" + mReqno + "' and mrrno = '" + mMRRNo + "' and rsircode = '" +
                            mResCode + "' and spcfcod = '" + mSpcfCode + "'");
                    dr1["oissueno"] = dr3[0]["oissueno"];
                    dr1["reqno1"] = dr3[0]["reqno1"];
                    dr1["mrrno1"] = dr3[0]["mrrno1"];
                    dr1["mrrref"] = dr3[0]["mrrref"];
                    dr1["chlnno"] = dr3[0]["chlnno"];
                    dr1["challandat"] = dr3[0]["challandat"];
                    dr1["mrrdat"] = dr3[0]["mrrdat"];
                    dr1["pactdesc"] = dr3[0]["pactdesc"];
                    dr1["rsirdesc1"] = dr3[0]["rsirdesc1"];
                    dr1["spcfdesc"] = dr3[0]["spcfdesc"];
                    dr1["rsirunit"] = dr3[0]["rsirunit"];
                    dr1["mrrqty"] = dr3[0]["mrrqty"];
                    dr1["mrrrate"] = dr3[0]["mrrrate"];
                    dr1["mrramt"] = dr3[0]["mrramt"];
                    dr1["mmrramt"] = dr3[0]["mrramt"];
                    dr1["remrks"] = dr3[0]["mrrnote"];
                    dr1["rowid"] = dr3[0]["rowid"];
                    dr1["boqrate"] = dr3[0]["boqrate"];
                    tbl1.Rows.Add(dr1);



                }

                DataRow[] drcar = tbl1.Select("mrrno =''");
                if (drcar.Length == 0)
                {
                    DataTable dt = (DataTable)ViewState["tblcar"];
                    foreach (DataRow drc in dt.Rows)
                    {
                        DataRow dr1 = tbl1.NewRow();
                        dr1["pactcode"] = drc["pactcode"].ToString();

                        dr1["reqno"] = "";
                        dr1["mrrno"] = "";
                        dr1["rsircode"] = drc["rsircode"].ToString();
                        dr1["spcfcod"] = drc["spcfcod"].ToString();
                        dr1["reqno1"] = "";
                        dr1["mrrno1"] = "";
                        dr1["mrrref"] = "";
                        dr1["chlnno"] = "";
                        dr1["challandat"] = "";
                        dr1["pactdesc"] = drc["pactdesc"].ToString();
                        dr1["rsirdesc1"] = drc["rsirdesc"].ToString();
                        dr1["spcfdesc"] = "";
                        dr1["rsirunit"] = "";
                        dr1["mrrqty"] = 0.00;
                        dr1["mrrrate"] = 0.00;
                        dr1["mrramt"] = drc["orderamt"];
                        dr1["mmrramt"] = 0.00;
                        dr1["remrks"] = "";
                        dr1["boqrate"] = 0.00;
                        tbl1.Rows.Add(dr1);

                    }


                }



                ViewState["tblBill"] = this.HiddenSameData(tbl1);
                this.gvBillInfo_DataBind();


            }




        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            this.gvBillInfo.PageIndex = ((DropDownList)this.gvBillInfo.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
            this.gvBillInfo_DataBind();
        }
        protected void lbtnResFooterTotal_Click(object sender, EventArgs e)
        {
            double amt1 = 0.00, amt2 = 0.00;
            //this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            DataTable td1 = tbl1.Copy();
            DataTable td2 = tbl1.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like '019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));

            amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));
            ((Label)this.gvBillInfo.FooterRow.FindControl("lblgvFooterTMRRAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvBillInfo.FooterRow.FindControl("lblgvFMMRRAmt")).Text = (amt1 - amt2).ToString("#,##0.00;(#,##0.00); ");//Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(mmrramt)", "")) ? 0.00 : tbl1.Compute("Sum(mmrramt)", ""))).ToString("#,##0;(#,##0); ");





            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            this.lblvalnettotal.Text = ((amt1 - amt2) - (security + deduction + penalty + Advanced)).ToString("#,##0.00;(#,##0.00); ");


        }

        protected void lbtnSupplierList_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblSupOrd");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            string srchoption = (this.Request.QueryString["sircode"].ToString()).Length == 0 ? "%" + this.txtsrchsupplier.Text.Trim() + "%" : this.Request.QueryString["sircode"].ToString() + "%";

            string genno = this.Request.QueryString["genno"].ToString();


            string orderno = (this.Request.QueryString["genno"].ToString()).Length == 0 ? "%%"
                : genno.Substring(0, 3) == "PBL" ? "%%" : this.Request.QueryString["genno"].ToString() + "%";


            string date = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETBILLSUPLIST", srchoption, date, orderno, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tblSupOrd"] = ds1.Tables[0];
            this.ddlSupList.DataTextField = "ssirdesc";
            this.ddlSupList.DataValueField = "ssircode";
            this.ddlSupList.DataSource = ds1.Tables[1];
            this.ddlSupList.DataBind();

        }

        protected void imgSearchOrderno_Click(object sender, EventArgs e)
        {
            this.GetOrderList();

        }


        private void GetOrderList()
        {
            DataTable dt = (DataTable)ViewState["tblSupOrd"];
            string ssircode = this.ddlSupList.SelectedValue.ToString();
            DataView dv = dt.DefaultView;
            string Searchorrefno = this.txtSrchOrderrefno.Text.Trim() + "%";
            dv.RowFilter = "ssircode in('" + ssircode + "')  and textfield like '" + Searchorrefno + "'";
            this.ddlOrderList.DataTextField = "textfield";
            this.ddlOrderList.DataValueField = "valuefield";
            this.ddlOrderList.DataSource = dv.ToTable();
            this.ddlOrderList.DataBind();
            this.ddlOrderList_SelectedIndexChanged(null, null);

        }

        protected void ddlOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMrrList();
        }

        private void GetMrrList()
        {
            ViewState.Remove("tblcar");
            string comcod = this.GetCompCode();
            string mSupCode = this.ddlSupList.SelectedValue.ToString();
            string Ordercod = this.ddlOrderList.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETBILLMRRLIST", mSupCode, Ordercod, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblMat"] = ds1.Tables[0];
            ViewState["tblcar"] = ds1.Tables[1];

            this.ddlMRRList.DataTextField = "textfield";
            this.ddlMRRList.DataValueField = "valuefiled";
            this.ddlMRRList.DataSource = ds1.Tables[0];
            this.ddlMRRList.DataBind();

        }



        protected void chkCharging_CheckedChanged(object sender, EventArgs e)
        {
            this.PnlCharging.Visible = (chkCharging.Checked);

        }


        protected void imgSearchProject_Click(object sender, EventArgs e)
        {

            this.TblProject();
            DataTable dt = (DataTable)ViewState["tblBill"];
            DataTable dt1 = (DataTable)ViewState["tblproject"];

            if (dt.Rows.Count == 0)
            {
                this.ddlProjectName.Items.Clear();
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Pactcode = dt.Rows[i]["pactcode"].ToString();
                DataRow[] dr1 = dt1.Select("pactcode='" + Pactcode + "'");
                if (dr1.Length == 0)
                {
                    DataRow dr2 = dt1.NewRow();
                    dr2["pactcode"] = Pactcode;
                    dr2["pactdesc"] = dt.Rows[i]["pactdesc"].ToString();
                    dt1.Rows.Add(dr2);

                }

            }

            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = dt1;
            this.ddlProjectName.DataBind();

        }
        protected void imgSearchCharge_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETLABOURCHARGE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCharge.DataTextField = "sirdesc";
            this.ddlCharge.DataValueField = "sircode";
            this.ddlCharge.DataSource = ds1.Tables[0];
            this.ddlCharge.DataBind();
            ds1.Dispose();
        }

        protected void lbtnSelect_Click(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            string mProjectCode = this.ddlProjectName.SelectedValue.ToString();
            string mResCode = this.ddlCharge.SelectedValue.ToString();
            string mSpcfCode = "000000000000";
            DataRow[] dr2 = tbl1.Select("pactcode ='" + mProjectCode + "' and rsircode = '" + mResCode + "' and spcfcod = '" + mSpcfCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["pactcode"] = mProjectCode;

                dr1["reqno"] = "";
                dr1["mrrno"] = "";
                dr1["rsircode"] = mResCode;
                dr1["spcfcod"] = mSpcfCode;
                dr1["reqno1"] = "";
                dr1["mrrno1"] = "";
                dr1["mrrref"] = "";
                dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text.Trim();
                dr1["rsirdesc1"] = this.ddlCharge.SelectedItem.Text.Trim();
                dr1["spcfdesc"] = "";
                dr1["rsirunit"] = "";
                dr1["mrrqty"] = 0.00;
                dr1["mrrrate"] = 0.00;
                dr1["mrramt"] = 0.00;
                dr1["mmrramt"] = 0.00;
                dr1["remrks"] = "";
                dr1["boqrate"] = 0.00;
                tbl1.Rows.Add(dr1);
            }



            ViewState["tblBill"] = this.HiddenSameData(tbl1);
            this.gvBillInfo_DataBind();
        }

        protected void lbtnUpdateBill_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.SaveDeposit();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();
            string comcod = this.GetCompCode();
            this.Session_tblBill_Update();

            string mBILLDAT = this.GetStdDate(this.txtCurBillDate.Text.Trim());
            string billrefdat = this.GetStdDate(this.txtBillrefDate.Text.Trim());
            string ChequeDate = this.GetStdDate(this.txtChequeDate.Text.Trim());
            string mSSIRCODE = this.ddlSupList.SelectedValue.ToString().Trim();
            string mBILLUSRID = "";
            string mAPPRUSRID = "";
            string mAPPRDAT = this.GetStdDate(this.txtApprovalDate.Text.Trim());  // DateTime.Today.ToString("dd-MMM-yyyy");
            string mBILLBYDES = this.txtPreparedBy.Text.Trim();
            string mAPPBYDES = this.txtApprovedBy.Text.Trim();
            string mBILLREF = this.txtBillRef.Text.Trim();
            string mBILLNAR = this.txtBillNarr.Text.Trim();
            string mVOUNUM = this.txtAccVounum.Text.Trim();
            string paytype = this.ddlPayType.SelectedValue.ToString();
            string percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim()).ToString();
            string sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim()).ToString();
            string dedamt = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim()).ToString(); ;
            string Penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim()).ToString();
            string advamt = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim()).ToString();

            //added

            DataTable dtuser = (DataTable)Session["UserLog"];
            string aprovedByid;
            string aprvdat;

            switch (comcod)
            {

                // case "3101":
                case "3366"://Lanco
                case "3367"://Epic

                case "3340":
                    aprovedByid = "";
                    aprvdat = "";


                    break;
                default:
                    aprovedByid = usrid;
                    aprvdat = billrefdat;

                    break;



            }


            switch (comcod)
            {
                case "3315":
                case "3316":
                case "3317":
                case "3301":
                    if (mBILLREF == "")
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Please Fill Ref. Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    break;

                default:
                    break;


            }

            DataTable tbl1 = (DataTable)ViewState["tblBill"];


            //switch (comcod)
            //{
            //    case "3301":
            //    case "1301":
            //    case "2301":
            //        break;

            //    default:
            //        foreach (DataRow dr2 in tbl1.Rows)
            //        {

            //            if (Convert.ToDouble(dr2["mmrramt"]) < Convert.ToDouble(dr2["mrramt"]))
            //            {

            //               ((Label)this.Master.FindControl("lblmsg")).Text = "Amount Equal or Below Aproved  Amount";
            //                return;
            //            }

            //        }

            //        break;

            //}



            //For Existing MRR

            if (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry")
            {

                for (int i = 0; i < tbl1.Rows.Count; i++)
                {
                    string mMRRNO = tbl1.Rows[i]["mrrno"].ToString();
                    string Mreqno = tbl1.Rows[i]["reqno"].ToString();
                    string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                    string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                    if (mMRRNO != "")
                    {
                        DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "EXISTINGMRRNO", mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, "", "", "", "", "");
                        if (ds.Tables[0].Rows.Count == 0) continue;
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "MRR No already Existing in this Bill No";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                            return;
                        }
                    }


                }


            }

            mVOUNUM = (mVOUNUM.Length == 0 ? "00000000000000" : mVOUNUM);


            this.lbtnResFooterTotal_Click(null, null);
            if (this.ddlPrevBillList.Items.Count == 0)
                this.GetBillNo();
            string mBILLNO = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();



            bool result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLB",
                           mBILLNO, mBILLDAT, mSSIRCODE, mBILLUSRID, mAPPRUSRID, mAPPRDAT, mBILLBYDES,
                           mAPPBYDES, mBILLREF, mBILLNAR, mVOUNUM, usrid, sessionid, trmid, billrefdat, ChequeDate, paytype, percentage, sdamt, dedamt, Penalty, aprovedByid, aprvdat, "", advamt, "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {



                string mPactcode = tbl1.Rows[i]["pactcode"].ToString();
                string mORDERNO = tbl1.Rows[i]["orderno"].ToString();
                string mMRRNO = tbl1.Rows[i]["mrrno"].ToString();

                if (mMRRNO.Length == 14)
                {
                    bool dcon = ASITUtility02.PurChaseOperation(Convert.ToDateTime(tbl1.Rows[i]["mrrdat"].ToString()), Convert.ToDateTime(mBILLDAT));
                    if (!dcon)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Bill Date is equal or greater MRR Date');", true);
                        return;
                    }
                }



                string Mreqno = tbl1.Rows[i]["reqno"].ToString();
                string mRSIRCODE = tbl1.Rows[i]["rsircode"].ToString();
                string mSPCFCOD = tbl1.Rows[i]["spcfcod"].ToString();
                string mMRRQTY = tbl1.Rows[i]["mrrqty"].ToString();
                string mBILLAMT = tbl1.Rows[i]["mrramt"].ToString();
                if (mORDERNO != "")
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLA", mBILLNO, mORDERNO, mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, mMRRQTY, mBILLAMT, "", "", "", "", "", "", "", "", "", "", "", "");

                else
                    result = purData.UpdateTransInfo2(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLINFO", "PURBILLC", mBILLNO, mPactcode, mRSIRCODE, "000000000000", mBILLAMT, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");



                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }

            //DataTable dttemp = (DataTable)Session["tblAttDocs"];

            //DataSet ds1 = new DataSet("ds1");
            //DataView dv1 = new DataView(dttemp);
            //ds1.Tables.Add(dv1.ToTable());
            //ds1.Tables[0].TableName = "tbl1";

            //bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "BILLXMLATTACHEDDOCUS", ds1, null, null, mBILLNO);

            //if (!resulta)
            //{

            //    //return;
            //}
            //else
            //{
            //    this.btnShowimg_Click(null, null);

            //}



            this.txtCurBillDate.Enabled = false;
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);






            if (hst["compsms"].ToString() == "True")
            {

                switch (comcod)
                {
                    case "3333":
                        break;

                    default:
                        if (this.Request.QueryString["Type"].ToString().Trim() == "BillEntry")
                        {

                            SendSmsProcess sms = new SendSmsProcess();
                            string comnam = hst["comnam"].ToString();
                            string compname = hst["compname"].ToString();
                            string frmname = "AccPurchase.aspx?Type=Entry";


                            string SMSHead = "Ready To Accounts Update, ";



                            string SMSText = comnam + ":\n" + SMSHead + ":\n" + "Supplier: " + this.ddlSupList.SelectedItem.Text.ToString() + "\n" + "Bill No: " + mBILLNO;
                            bool resultsms = sms.SendSmms(SMSText, usrid, frmname);

                        }
                        break;
                }

            }














            //if (ConstantInfo.LogStatus == true)
            //{
            //    string eventtype = "Materials Supply Bill Information";
            //    string eventdesc = "Update Bill";
            //    string eventdesc2 = mBILLNO;
            //    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            //}
        }


        protected void lbtnFinalUpdateBill_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string billno = this.Request.QueryString["genno"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usrid = hst["usrid"].ToString();
            string sessionid = hst["session"].ToString();
            string trmid = hst["compname"].ToString();

            string aprvbyid = usrid;

            //DateTime billdate;
            //billdate = this.txtCurBillDate.Text;  //txtCurBillDate.Text;


            string billdate = this.GetStdDate(this.txtCurBillDate.Text.Trim());

            DateTime billdate1 = Convert.ToDateTime(billdate);
            DateTime auditbilldate1 = Convert.ToDateTime(this.txtauditbilldate.Text);
            string auditbilldate = (this.txtauditbilldate.Text);


            if (billdate1 > auditbilldate1)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Must be Bill Audit Date equal or greater than Bill Date ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                return;
            }


            //  string aprvdat =  System.DateTime.Now.ToString("dd-MMM-yyyy");

            bool result = purData.UpdateTransHREMPInfo3(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEPURBILLAUDIT", "PURBILLB",
                            billno, aprvbyid, auditbilldate, trmid, sessionid, "", "",
                            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }
            else
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }





        }


        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.Session_tblBill_Update();
            gvBillInfo_DataBind();
        }

        protected void gvBillInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblBill"];
            string Billno = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();
            int rowindex = (this.gvBillInfo.PageSize) * (this.gvBillInfo.PageIndex) + e.RowIndex;

            string mORDERNO = dt.Rows[rowindex]["orderno"].ToString();
            string mMRRNO = dt.Rows[rowindex]["mrrno"].ToString();
            string Mreqno = dt.Rows[rowindex]["reqno"].ToString();
            string mRSIRCODE = dt.Rows[rowindex]["rsircode"].ToString();
            string mSPCFCOD = dt.Rows[rowindex]["spcfcod"].ToString();

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELBILLMATERIALS", Billno, mORDERNO, mMRRNO, Mreqno, mRSIRCODE, mSPCFCOD, "", "", "", "", "", "", "", "", "");

            if (result == true)
            {

                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState["tblBill"] = dv.ToTable();
            this.gvBillInfo_DataBind();
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

            bool resulta = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "UPDATEXML01", ds1, null, null, Reqno);

            if (!resulta)
            {

                return false;
            }


            return true;
        }
        protected void lbtnDeleteBill_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string Billno = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "GETPURBILLINFO", Billno, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            bool result2 = this.XmlDataInsert(Billno, ds1);

            if (!result2)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Fail');", true);
                return;

            }




            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "DELETEBILL", Billno, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }


        }

        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblbillrefdate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
            this.txtBillrefDate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
            this.lblchequedate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
            this.txtChequeDate.Visible = (this.ddlPayType.SelectedValue == "003") ? false : true;
        }
        protected void lbtnDepost_Click(object sender, EventArgs e)
        {


            this.SaveDeposit();



            //if (((DataTable)ViewState["tblBill"]).Rows.Count == 0)
            //    return;
            //double amount = Convert.ToDouble((Convert.IsDBNull(((DataTable)ViewState["tblBill"]).Compute("sum(mrramt)", "")) ? 0.00
            //        : ((DataTable)ViewState["tblBill"]).Compute("sum(mrramt)", "")));
            //double percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim());
            //this.txtSDAmount.Text = Convert.ToDouble(amount * percentage * 0.01).ToString("#,#,#0; (#, #,#0);");


            //double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            //double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            //double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            //double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            //this.lblvalnettotal.Text = (amount - (security + deduction + penalty+Advanced)).ToString("#,##0.00;(#,##0.00); ");

        }

        private void SaveDeposit()
        {
            if (((DataTable)ViewState["tblBill"]).Rows.Count == 0)
                return;

            DataTable tbl1 = (DataTable)ViewState["tblBill"];
            DataTable td1 = tbl1.Copy();
            DataTable td2 = tbl1.Copy();
            DataView dv1;
            //Deduction
            dv1 = td2.DefaultView;
            dv1.RowFilter = ("rsircode like '019999902%'");
            td2 = dv1.ToTable();
            // Others
            dv1 = td1.DefaultView;
            dv1.RowFilter = ("rsircode not like '019999902%'");
            td1 = dv1.ToTable();
            double amt2 = (td2.Rows.Count == 0) ? 0.00 : Convert.ToDouble((Convert.IsDBNull(td2.Compute("Sum(mrramt)", "")) ? 0.00 : td2.Compute("Sum(mrramt)", "")));

            double amt1 = Convert.ToDouble((Convert.IsDBNull(td1.Compute("Sum(mrramt)", "")) ? 0.00 : td1.Compute("Sum(mrramt)", "")));




            //double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            //double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            //double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            //double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            //this.lblvalnettotal.Text = ((amt1 - amt2) - (security + deduction + penalty + Advanced)).ToString("#,##0.00;(#,##0.00); ");





            double amount = amt1 - amt2;
            double penalty = Convert.ToDouble("0" + this.txtPenaltyAmount.Text.Trim());
            double netamt = amount - penalty;

            double percentage = Convert.ToDouble("0" + this.txtpercentage.Text.Replace("%", "").Trim());
            double sdamt = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());

            this.txtSDAmount.Text = sdamt > 0 ? sdamt.ToString("#,#,#0.00;(#, #,#0.00); ") : Convert.ToDouble(netamt * percentage * 0.01).ToString("#,#,#0.00;(#, #,#0.00); ");
            double fpercntage = (sdamt > 0) ? (netamt > 0 ? ((Convert.ToDouble(this.txtSDAmount.Text) * 100) / netamt) : 0.00) : percentage;
            this.txtpercentage.Text = fpercntage.ToString("#,#,#0.00;(#, #,#0.00); ") + "%";
            double security = Convert.ToDouble("0" + this.txtSDAmount.Text.Trim());
            double deduction = Convert.ToDouble("0" + this.txtDedAmount.Text.Trim());
            double Advanced = Convert.ToDouble("0" + this.txtAdvanced.Text.Trim());
            this.lblvalnettotal.Text = (amount - (security + deduction + penalty + Advanced)).ToString("#,##0.00;(#,##0.00); ");
        }

        public void createTable()
        {
            DataTable mnuTbl1 = new DataTable();
            // Add Auto Increment Column called ID
            mnuTbl1.Columns.Add(new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                DataType = typeof(System.Int32),
                Unique = true
            });
            mnuTbl1.Columns.Add("billno", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("itemsurl", Type.GetType("System.String"));
            Session["tblAttDocs"] = mnuTbl1;


        }

        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            //Session.Remove("tblAttDocs");
            //string comcod = this.GetCompCode();
            //string Billno = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();


            //    string porderno = this.ddlOrderList.SelectedValue.ToString();
            //    string reqno = this.ddlMRRList.SelectedValue.ToString();

            //    DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "SHOWXMLINFORAMTIONREQABILL", reqno, porderno, "", "", "", "", "", "", "");
            //if (ds == null)
            //{
            //    return;
            //}

            //DataTable tbl1 = ds.Tables[0];
            //ListViewEmpAll.DataSource = tbl1;
            //ListViewEmpAll.DataBind();
            //Session["tblAttDocs"] = tbl1;


        }

        protected void btnDelall_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string billno = ((Label)this.ListViewEmpAll.Items[j].FindControl("billno")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    DataRow dr = dt.Rows[j];
                    dr.Delete();
                    DataSet ds1 = new DataSet("ds1");
                    ds1.Tables.Add(dt);
                    ds1.Tables[0].TableName = "tbl1";

                    bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEBILLIMAGES", ds1, null, null, billno, "", "", "", "", "", "", "", "", "", "", "", "");

                    string subfile = filesname.Remove(0, 2);
                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + subfile);
                        this.lblMesg.Text = " Files Removed ";
                        // this.LoadGrid();
                    }


                }




            }

        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;
                }

            }

        }
        protected void AsyncFileUpload1_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tblimages"];
            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            string billno = "";
            if (AsyncFileUpload1.HasFile)
            {
                billno = this.lblCurBillNo1.Text.Trim().Substring(0, 3) + this.txtCurBillDate.Text.Trim().Substring(6, 4) + this.lblCurBillNo1.Text.Trim().Substring(3, 2) + this.txtCurBillNo2.Text.Trim();

                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/SupBill/") + billno + random + extension);

                Url = "~/Upload/SupBill/" + billno + random + extension;
                //  Url = Url.Substring(0, (Url.Length - 1));
                dt.Rows.Add(comcod, billno, Url);
            }

            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dt);
            ds1.Tables[0].TableName = "tbl1";
            bool result = purData.UpdateXmlTransInfo(comcod, "SP_ENTRY_PURCHASE_03", "UPDATEBILLIMAGES", ds1, null, null, billno, "", "", "", "", "");

            if (result == true)
            {
                this.lblMesg.Text = " Successfully Updated ";
                //  this.LoadGrid();
            }
        }
    }
}
