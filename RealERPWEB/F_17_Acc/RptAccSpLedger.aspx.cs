using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using Microsoft.Reporting.WinForms;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptAccSpLedger : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double dramt, cramt, opnamt, clsamt, isuamt, reconamt;
        public double balamt = 0.00;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));


                string title = (Request.QueryString["Type"].ToString().Trim() == "DetailLedger") ? "Special Ledger"
                        : (Request.QueryString["Type"].ToString().Trim() == "SPayment") ? "Supplier Payment Proposal - 01"
                        : (Request.QueryString["Type"].ToString().Trim() == "SPayment02") ? "Supplier Payment Proposal - 02"
                        : (Request.QueryString["Type"].ToString().Trim() == "SubConPay") ? "Sub-Contractor Payment Proposal"
                        : (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier Overall Position"
                        : (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Advanced Summary"
                        : (Request.QueryString["Type"].ToString().Trim() == "IssPay") ? "Issue Vs. Payment(Main Head)"
                        : (Request.QueryString["Type"].ToString().Trim() == "IssPaySum") ? "Issue Vs. Payment Summary(Main Head)"
                        : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? "Overall Position(Supplier & Contractor)" : "Sub-Contractor Overal Position";

                //this.Master.Page.Title = title;

                //((Label)this.Master.FindControl("lblTitle")).Text = title;
                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetResList();
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.txtSrchRes.Text = "";
                if ((Request.QueryString["Type"].ToString().Trim() == "ASPayment") || (Request.QueryString["Type"].ToString().Trim() == "AConPayment") || (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment"))
                {
                    this.lblcontrolAccResCode.Visible = false;
                    this.txtSrchRes.Visible = false;
                    this.ddlConAccResHead.Visible = false;
                    this.ibtnFindRes.Visible = false;
                    this.lblGroup.Visible = true;
                    this.ddlRptGroup.Visible = true;
                    this.lblsearch.Visible = true;
                    this.txtsearch.Visible = true;
                }
                if (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment")
                {
                    this.chkSupplier.Visible = true;
                    this.chkContractor.Visible = true;
                    this.LoadAllSupplier();
                }

                if (Request.QueryString["Type"].ToString().Trim() == "AConPayment")
                {
                    this.chkSupplier.Visible = true;
                    this.chkSupplier.Text = "Without balance";
                }

                if ((Request.QueryString["Type"].ToString().Trim() == "Adv"))
                {
                    this.lblGroup.Visible = true;
                    this.ddlRptGroup.Visible = true;
                }
                if ((Request.QueryString["Type"].ToString().Trim() == "DetailLedger"))
                {
                    this.Checknarration.Visible = true;
                    this.Checkdaywise.Visible = true;
                    string comcod = hst["comcod"].ToString();
                    string events = hst["events"].ToString();

                    if (Convert.ToBoolean(events) == true)
                    {
                        string eventtype = "Click Special Ledger ";
                        string eventdesc = "Click Special Ledger";
                        string eventdesc2 = "";

                        bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



                    }


                }



            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public string GetComCod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }
        private void GetResList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string srchCode = ((Request.QueryString["Type"].ToString().Trim() == "SPayment") ? "99" : (Request.QueryString["Type"].ToString().Trim() == "SPayment02") ? "99"
                  : (Request.QueryString["Type"].ToString().Trim() == "SubConPay") ? "98"
                  : (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "95"
                  : ((Request.QueryString["Type"].ToString().Trim() == "IssPay") || (Request.QueryString["Type"].ToString().Trim() == "IssPaySum")) ? "9[589]" : "") + "%";
            string filter = "%" + this.txtSrchRes.Text.Trim() + "%";
            //  (Request.QueryString["Type"].ToString().Trim() == "DetailLedger") ? "%" : "%" + this.txtSrchRes.Text.Trim() + "%";
            string Calltype = (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "GETADVCODE"
                    : (Request.QueryString["Type"].ToString().Trim() == "DetailLedger") ? "RPTSPLGACCRESLIST"
                    : ((Request.QueryString["Type"].ToString().Trim() == "IssPay") || (Request.QueryString["Type"].ToString().Trim() == "IssPaySum")) ? "GETSUBCOD" : "RPTACCRESLIST";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", Calltype, srchCode, filter, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlConAccResHead.DataTextField = "resdesc1";
            this.ddlConAccResHead.DataValueField = "rescode";
            this.ddlConAccResHead.DataSource = ds1.Tables[0];
            this.ddlConAccResHead.DataBind();


        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string type = Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "DetailLedger":
                    this.PrintDetailLedger();
                    break;

                case "SPayment":
                case "SubConPay":
                    this.PrintSPaymentPro();
                    break;
                case "ASPayment":
                case "AConPayment":
                case "Adv":

                    this.PrintASOrConPayment();
                    break;

                case "SPayment02":
                    this.PrintASOrConPayment02();
                    break;
                case "IssPay":
                    //this.RptMonthlyIsuVsPay();
                    this.RptMonthlyIsuVsPayRdlc();
                    break;
                case "IssPaySum":
                    this.RptMonthlyIsuVsPaySum();
                    break;



                case "ASupConPayment":
                    this.PrintAllSOrConPayment();
                    break;



            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }



        }





        private void PrintDetailLedger()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt1 = (DataTable)Session["tblspledger"];
            if (dt1 == null)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.SpLedger>();
            LocalReport Rpt1 = new LocalReport();

            switch (comcod)
            {
                case "3101":// RHEL
                case "3305":// RHEL
                case "3311":// RHEL(ctg)
                case "3306":// Ratul
                case "3309":// HOlding
                case "2305"://RLDL
                            //rptsl = new RealERPRPT.R_17_Acc.RPTSpecialLedgerRup();
                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedgerRup", lst, null, null);
                    break;

                default:

                    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger", lst, null, null);


                    break;


            }
            // Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSPLedger", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("prjname", "Acc.Desc : " + this.ddlConAccResHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Rptaital", "SPECIAL LEDGER REPORT"));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

          
            string events = hst["events"].ToString();

            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Print Special Ledger ";
                string eventdesc = "Print Special Ledger ";
                string eventdesc2 = "Report Print Head  : "+ this.ddlConAccResHead.SelectedItem.ToString().Substring(13)+ " " +
                    "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") +")" ;

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }
        }





        //private void PrintSPaymentPro()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string Type = this.Request.QueryString["Type"].ToString();
        //    string Head = (Type == "SubConPay") ? "Sub-Contractor Payment Proposal" : "Supplier's Payment Proposal";
        //    ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptSupPayment();
        //    DataTable dt = (DataTable)Session["tblspledger"];

        //    double sdramt = (dt.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + (((Label)this.gvSPayment.FooterRow.FindControl("lgvFDrAmts")).Text));
        //    double scramt = (dt.Rows.Count == 0) ? 0 : Convert.ToDouble("0" + (((Label)this.gvSPayment.FooterRow.FindControl("lgvFCrAmts")).Text));
        //    TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
        //    txtfdate.Text = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";



        //    TextObject txtCompName = rptsl.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
        //    txtCompName.Text = comnam;
        //    TextObject rpttxtHead = rptsl.ReportDefinition.ReportObjects["head"] as TextObject;
        //    rpttxtHead.Text = Head;

        //    TextObject rpttxtAccDesc = rptsl.ReportDefinition.ReportObjects["actdesc"] as TextObject;
        //    rpttxtAccDesc.Text = (Type == "SubConPay") ? "Sub-Contractor :" + this.ddlConAccResHead.SelectedItem.ToString().Substring(13) : "Supplier: " + this.ddlConAccResHead.SelectedItem.ToString().Substring(13);

        //    TextObject rpttxtdramt = rptsl.ReportDefinition.ReportObjects["txtdramt"] as TextObject;
        //    rpttxtdramt.Text = sdramt.ToString("#,##0;(#,##0); ");
        //    TextObject rpttxtcramt = rptsl.ReportDefinition.ReportObjects["txtcramt"] as TextObject;
        //    rpttxtcramt.Text = scramt.ToString("#,##0;(#,##0); ");

        //    TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptsl.SetDataSource((DataTable)Session["tblspledger"]);
        //    //string comcod = this.GetComeCode();
        //    string comcod = GetComCod();
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptsl.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptsl;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //}




        private void PrintSPaymentPro()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Type = this.Request.QueryString["Type"].ToString();
            string Head = (Type == "SubConPay") ? "Sub-Contractor Payment Proposal" : "Supplier's Payment Proposal";

            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd.MM.yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblspledger"];


            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RptSupPayment>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSupPayment", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("actdesc", (Type == "SubConPay") ? "Sub-Contractor :" + this.ddlConAccResHead.SelectedItem.ToString().Substring(13) : "Supplier: " + this.ddlConAccResHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", Head));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }





        //private void PrintASOrConPayment02()
        //{
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string Type = this.Request.QueryString["Type"].ToString();

        //    ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptSupPayment02();
        //    TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
        //    txtfdate.Text = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";


        //    TextObject rpttxtAccDesc = rptsl.ReportDefinition.ReportObjects["actdesc"] as TextObject;
        //    rpttxtAccDesc.Text = "Supplier: " + this.ddlConAccResHead.SelectedItem.ToString().Substring(13);
        //    TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rptsl.SetDataSource((DataTable)Session["tblspledger"]);
        //    //string comcod = this.GetComeCode();
        //    string comcod = GetComCod();
        //    string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
        //    rptsl.SetParameterValue("ComLogo", ComLogo);
        //    Session["Report1"] = rptsl;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        //}



        private void PrintASOrConPayment02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string Type = this.Request.QueryString["Type"].ToString();
            //string Head = (Type == "SubConPay") ? "Sub-Contractor Payment Proposal" : "Supplier's Payment Proposal";

            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd.MM.yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd.MM.yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblspledger"];


            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.RptSupPayment02>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSupPayment02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("actdesc", "Supplier: " + this.ddlConAccResHead.SelectedItem.ToString().Substring(13)));
            Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Supplier's Payment Proposal"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }














        private void PrintASOrConPayment()
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

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblspledger"];
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.AdvancedSummary>();

            if (comcod == "3101" || comcod == "3330")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAllSupPaymentBridge", lst, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAllSupPayment", lst, null, null);
            }
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("date", "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )"));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Advanced Summary"
                  : ((Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's "
                    : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? " Supplier & Sub-Contractor's " : "Sub-Contractor's ") + "Overall Position"));

            Rpt1.SetParameters(new ReportParameter("txtSuporConName", (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Employee's Name"
                    : (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's Name"
                    : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? "Name" : "Sub-Contractor's Name"));

            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod1 = GetComCod();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptsl = new ReportDocument();

            //if (comcod1 == "3101" || comcod1 == "3330")
            //{
            //    rptsl = new RealERPRPT.R_17_Acc.RptAllSupPaymentBridge();
            //}


            //else
            //{
            //    rptsl = new RealERPRPT.R_17_Acc.RptAllSupPayment();

            //}



            //DataTable dt = (DataTable)Session["tblspledger"];

            //TextObject rptCompanyName = rptsl.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rptCompanyName.Text = comnam;



            //TextObject rpttxtHeaderTitle = rptsl.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Advanced Summary"
            //        : ((Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's "
            //        : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? " Supplier & Sub-Contractor's " : "Sub-Contractor's ") + "Overall Position";
            //TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject rpttxtSuporConName = rptsl.ReportDefinition.ReportObjects["txtSuporConName"] as TextObject;
            //rpttxtSuporConName.Text = (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Employee's Name" 
            //        : (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's Name" 
            //        :  (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ?"Name": "Sub-Contractor's Name";



            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            ////string comcod = this.GetComeCode();
            //string comcod = GetComCod();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsl.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void PrintAllSOrConPayment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string value = this.ddlsupplier.SelectedItem.ToString();

            DataTable dt = (DataTable)Session["tblspledger"];

            string date = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            string rpttitle = (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Advanced Summary"
                    : ((Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's "
                    : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? chkSupplier.Checked ? value : chkContractor.Checked ? "Sub-Contractor's " : "Supplier & Sub-Contractor's " : "Sub-Contractor's ") + " Overall Position";
            string rpttxtSuporConName = chkSupplier.Checked ? "Supplier 's Name" : chkContractor.Checked ? "Sub-Contracto 's Name" : "Supplier & Sub-Contractor's Name";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassSupOrConPayment>();


            LocalReport Rpt1 = new LocalReport();

            if (comcod == "3333")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAllSupaConPaymentAlli", lst, null, null);
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptAllSupaConPayment", lst, null, null);
            }

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            Rpt1.SetParameters(new ReportParameter("rpttxtSuporConName", rpttxtSuporConName));

            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptAllSupaConPayment();
            //TextObject rptCompanyName = rptsl.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rptCompanyName.Text = comnam;

            //TextObject rpttxtHeaderTitle = rptsl.ReportDefinition.ReportObjects["txtHeaderTitle"] as TextObject;
            //rpttxtHeaderTitle.Text = (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Advanced Summary"
            //        : ((Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's "
            //        : (Request.QueryString["Type"].ToString ().Trim () == "ASupConPayment") ? chkSupplier.Checked ? value : chkContractor.Checked ? "Sub-Contractor's " : "Supplier & Sub-Contractor's " : "Sub-Contractor's ") + " Overall Position";

            //TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject rpttxtSuporConName = rptsl.ReportDefinition.ReportObjects["txtSuporConName"] as TextObject;
            //rpttxtSuporConName.Text = chkSupplier.Checked ? "Supplier 's Name" : chkContractor.Checked ? "Sub-Contracto 's Name" : "Supplier & Sub-Contractor's Name";

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            ////string comcod = this.GetComeCode();
            //string comcod = GetComCod();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsl.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //         ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";      
        }
        private void RptMonthlyIsuVsPay()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptMonthlyIsuVsPay();
            DataTable dt = (DataTable)Session["tblspledger"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = "grp1='AA'";
            dt = dv.ToTable();

            string isuamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ?
                    0 : dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
            string reconamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reconamt)", "")) ?
                    0 : dt.Compute("sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

            TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            txtCompany.Text = comnam;
            TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
            txtfdate.Text = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            TextObject rpttxtSuporConName = rptsl.ReportDefinition.ReportObjects["txtSuporConName"] as TextObject;
            rpttxtSuporConName.Text = (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "95") ? "Employee's Name" : (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "99") ? "Supplier's Name" : "Sub-Contractor's Name";

            TextObject txtIsuAmt = rptsl.ReportDefinition.ReportObjects["txtIsuAmt"] as TextObject;
            txtIsuAmt.Text = isuamt;

            TextObject txtRecAmt = rptsl.ReportDefinition.ReportObjects["txtRecAmt"] as TextObject;
            txtRecAmt.Text = reconamt;

            TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            //string comcod = this.GetComeCode();
            string comcod = GetComCod();
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptsl.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptsl;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void RptMonthlyIsuVsPayRdlc()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comadd = hst["comadd"].ToString();
            string comcod = GetComCod();

            DataTable dt = (DataTable)Session["tblspledger"];

            DataView dv = dt.DefaultView;
            dv.RowFilter = "grp1='AA'";
            dt = dv.ToTable();

            string isuamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ?
                    0 : dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
            string reconamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reconamt)", "")) ?
                    0 : dt.Compute("sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

            string date = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            string rpttxtSuporConName = (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "95") ? "Employee's Name" : (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "99") ? "Supplier's Name" : "Sub-Contractor's Name";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassRptIssueVsPayment>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthlyIsuVsPay1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Issue Vs Monthly Payment Status"));
            Rpt1.SetParameters(new ReportParameter("rpttxtSuporConName", rpttxtSuporConName));
            Rpt1.SetParameters(new ReportParameter("isuamt", isuamt));
            Rpt1.SetParameters(new ReportParameter("reconamt", reconamt));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





        }




        private void RptMonthlyIsuVsPaySum()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comadd = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = GetComCod();
            DataTable dt = (DataTable)Session["tblspledger"];

            string date = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            string rpttxtSuporConName = (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "95") ? "Employee's Name" : (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "99") ? "Supplier's Name" : "Sub-Contractor's Name";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassRptIssueVsPaymentSummary>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptIsuVsPaySum1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Issue Vs Monthly Payment Status Summary"));
            Rpt1.SetParameters(new ReportParameter("rpttxtSuporConName", rpttxtSuporConName));

            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";





            //ReportDocument rptsl = new RealERPRPT.R_17_Acc.RptMonthlyIsuVsPaySum();
            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtfdate = rptsl.ReportDefinition.ReportObjects["date"] as TextObject;
            //txtfdate.Text = " (From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + " )";
            //TextObject rpttxtSuporConName = rptsl.ReportDefinition.ReportObjects["txtSuporConName"] as TextObject;
            //rpttxtSuporConName.Text = (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "95") ? "Employee's Name" : (this.ddlConAccResHead.SelectedValue.Substring(0, 2) == "99") ? "Supplier's Name" : "Sub-Contractor's Name";

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblspledger"]);
            ////string comcod = this.GetComeCode();
            //string comcod = GetComCod();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptsl.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void ibtnFindRes_Click(object sender, EventArgs e)
        {
            this.GetResList();
        }
        protected void lnkShowLedger_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string type = Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "DetailLedger":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowDetailLedger();
                    break;

                case "SPayment":
                case "SubConPay":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.ShowSPaymentPro();
                    break;

                case "ASPayment":
                case "AConPayment":
                case "Adv":

                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowASupOrConPayment();
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

                case "SPayment02":

                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowSPayment02();
                    break;
                case "IssPay":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowMonIsuPayment();
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;
                case "IssPaySum":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.ShowMonIsuPaymentSum();
                    this.Bind_Data();
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;

                    break;



                case "ASupConPayment":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowAllSupOrConPayment();
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Show Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void ShowDetailLedger()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();
            string withOutOpn = (this.chkwithoutopn.Checked) ? "withoutOpn" : "";
            // string acthead = this.ddlAccHead.SelectedValue.ToString();

            string withOutnarra = (this.Checknarration.Checked) ? "withOutnarra" : "";


            string acthead = "";

            foreach (ListItem item in ddlAccHead.Items)
            {

                if (item.Selected)
                {
                    acthead += "actcode like '" + item.Value.Substring(0, 2) + "%' or ";
                }
            }

            acthead = acthead.Length > 0 ? "(" + acthead.Substring(0, acthead.Length - 3) + ")" : acthead;
            // srch1 = this.txtSearch1.Text.Trim() + "'" + " and '" + this.txttoSearch1.Text.Trim();

            string consolidate = this.Checkdaywise.Checked ? "Consolidate" : "";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTACCRESOURCELG", resource, frmdate, todate, withOutOpn, acthead, withOutnarra, consolidate, "", "");
            if (ds1 == null)
            {

                this.gvSpledger.DataSource = null;
                this.gvSpledger.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            DataTable dt1 = BalCalculation(dt);
            Session["tblspledger"] = dt1;
            this.gvSpledger.DataSource = dt1;
            this.gvSpledger.DataBind();
            Session["Report1"] = gvSpledger;
            if (dt1.Rows.Count > 0)
                ((HyperLink)this.gvSpledger.HeaderRow.FindControl("hlbtntbCdataExelsp")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

          
            string events = hst["events"].ToString();

            if (Convert.ToBoolean(events) == true)
            {
                string eventtype = "Show Data (Special Ledger) ";
                string eventdesc = "Show Data (Special Ledger) ";
                string eventdesc2 = "";

                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);



            }

            //this.FooterCal();


        }


        private DataTable BalCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;
            double opnam, dramt, cramt, bbalamt = 0.00;

            bool result = this.Checkdaywise.Checked;
            switch (result)
            {
                case true:

                    foreach (DataRow dr1 in dt.Rows)
                    {
                        if ((dr1["vounum"]).ToString().Trim() == "CURRENT DR/CR" || (dr1["vounum"]).ToString().Trim() == "Total:" || (dr1["vounum"]).ToString().Trim() == "Balance:")
                            continue;
                        opnam = Convert.ToDouble(dr1["opam"]);
                        dramt = Convert.ToDouble(dr1["dram"]);
                        cramt = Convert.ToDouble(dr1["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dr1["clsam"] = bbalamt;
                    }


                    break;


                default:
                    string actcode = dt.Rows[0]["actcode"].ToString();
                    //string grp=
                    for (int i = 0; i < dt.Rows.Count - 1; i++)
                    {
                        if ((dt.Rows[i]["actcode"]).ToString().Trim() != actcode)
                        {
                            bbalamt = 0.00;
                        }
                        actcode = dt.Rows[i]["actcode"].ToString();

                        if ((dt.Rows[i]["vounum"]).ToString().Trim() == "CURRENT DR/CR" || (dt.Rows[i]["vounum"]).ToString().Trim() == "SUB TOTAL" || (dt.Rows[i]["vounum"]).ToString().Trim() == "Balance:")
                            continue;



                        //if (((dt.Rows[i]["actcode"]).ToString().Trim()).Length == 12)
                        //{
                        opnam = Convert.ToDouble(dt.Rows[i]["opam"]);
                        dramt = Convert.ToDouble(dt.Rows[i]["dram"]);
                        cramt = Convert.ToDouble(dt.Rows[i]["cram"]);
                        bbalamt = bbalamt + (opnam + dramt - cramt);
                        dt.Rows[i]["clsam"] = bbalamt;
                        //}


                    }

                    break;



            }


            return dt;


        }

        private void ShowSPaymentPro()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSUPPLIERPAYMENT", resource, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvSPayment.DataSource = null;
                this.gvSPayment.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds1.Tables[0]);
            Session["tblspledger"] = dt;
            this.gvSPayment.DataSource = dt;
            this.gvSPayment.DataBind();
            this.FooterCal();

        }
        private void


            ShowSPayment02()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string resource = this.ddlConAccResHead.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTSUPPLIERPAYMENT02", resource, frmdate, todate, "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvSPayment02.DataSource = null;
                this.gvSPayment02.DataBind();
                return;
            }

            Session["tblspledger"] = ds1.Tables[0];
            this.gvSPayment02.DataSource = ds1.Tables[0];
            this.gvSPayment02.DataBind();
        }




        private void ShowASupOrConPayment()
        {
            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string SupOrConCode = (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "99"
                : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? "9[89]" : "98";
            string Rescode = ((Request.QueryString["Type"].ToString().Trim() == "Adv") ? this.ddlConAccResHead.SelectedValue.Substring(0, 4).ToString() : SupOrConCode) + "%";
            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));

            string chkwithout = this.chkSupplier.Checked ? "balamt" : "";
            string supsearh = (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "%" + txtsearch.Text + "%" : "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAYMENT", frmdate, todate, Rescode, mRptGroup, chkwithout, "", supsearh, "", "");
            if (ds2 == null)
            {

                this.gvAllSupPay.DataSource = null;
                this.gvAllSupPay.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tblspledger"] = ds2.Tables[0];


            //this.gvAllSupPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvAllSupPay.DataSource = ds2.Tables[0];
            //this.gvAllSupPay.DataBind();
            this.Data_Bind();
            if (dt.Rows.Count > 1)
                ((Label)this.gvAllSupPay.HeaderRow.FindControl("lblheader")).Text = (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's Name"
                    : (Request.QueryString["Type"].ToString().Trim() == "Adv") ? "Employee's Name"
                    : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? " Supplier & Sub-Contractor's Name" : "Sub-Contractor's Name";



            this.SaveValue();

        }

        private void LoadAllSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComCod();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "LOADALLSUPPLIER", "", "", "", "", "", "", "", "", "");
            this.ddlsupplier.DataTextField = "sirdesc";
            this.ddlsupplier.DataValueField = "sircode";
            this.ddlsupplier.DataSource = ds2.Tables[0];
            this.ddlsupplier.DataBind();
        }

        private void ShowAllSupOrConPayment()
        {

            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string SupOrConCode = (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "99"
                : (Request.QueryString["Type"].ToString().Trim() == "ASupConPayment") ? (this.chkContractor.Checked ? "98" : this.chkSupplier.Checked ? "99" : "9[89]") : "98";
            string Rescode = ((Request.QueryString["Type"].ToString().Trim() == "Adv") ? this.ddlConAccResHead.SelectedValue.Substring(0, 4).ToString() : SupOrConCode) + "%";


            string res = this.ddlsupplier.SelectedValue.Substring(0, 4).ToString();
            string Rescodegrp = res.Substring(2, 2).ToString() == "00" ? res.Substring(0, 2).ToString() + "%" : res + "%";



            string mRptGroup = Convert.ToString(this.ddlRptGroup.SelectedIndex);
            mRptGroup = (mRptGroup == "0" ? "2" : (mRptGroup == "1" ? "4" : (mRptGroup == "2" ? "7" : (mRptGroup == "3" ? "9" : "12"))));
            string supplier = this.chkSupplier.Checked ? "Supplier" : this.chkContractor.Checked ? "Contractor" : "All";

            string search = "%" + txtsearch.Text + "%";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAYMENT", frmdate, todate, Rescode, mRptGroup, supplier, Rescodegrp, search, "", "");
            if (ds2 == null)
            {

                this.gvAllSubAconBill.DataSource = null;
                this.gvAllSubAconBill.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tblspledger"] = ds2.Tables[0];
            this.Data_Bind();




            this.SaveValue();


        }
        private void ShowMonIsuPayment()
        {

            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Rescode = this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() + "%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "MONISSUEPAYMENT", frmdate, todate, Rescode, "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvMonIsuPay.DataSource = null;
                this.gvMonIsuPay.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tblspledger"] = HiddenSameData(ds2.Tables[0]);

            this.gvMonIsuPay.Columns[4].HeaderText = (this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() == "95") ? "Employee's Name"
                : (this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() == "98") ? "Sub-Contractor's Name" : "Supplier's Name";
            this.Data_Bind();

            //this.gvMonIsuPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvMonIsuPay.DataSource = ds2.Tables[0];
            //this.gvMonIsuPay.DataBind();

        }
        private void ShowMonIsuPaymentSum()
        {

            Session.Remove("tblspledger");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Rescode = this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() + "%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_PAYMENT", "MONISSUEPAYMENTSUM", frmdate, todate, Rescode, "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvIsuVsPaySum.DataSource = null;
                this.gvIsuVsPaySum.DataBind();
                return;
            }
            DataTable dt = ds2.Tables[0];
            Session["tblspledger"] = ds2.Tables[0];

            this.gvIsuVsPaySum.Columns[2].HeaderText = (this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() == "95") ? "Employee's Name"
                : (this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() == "98") ? "Sub-Contractor's Name" : "Supplier's Name";
            this.Data_Bind();
            //this.gvMonIsuPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvMonIsuPay.DataSource = ds2.Tables[0];
            //this.gvMonIsuPay.DataBind();

        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblspledger"];
            if (dt.Rows.Count == 0)
                return;



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string type = Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {




                case "ASPayment":

                case "Adv":
                    Session["Report1"] = gvAllSupPay;
                    ((HyperLink)this.gvAllSupPay.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFOpnbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnbill)", "")) ?
                                  0 : dt.Compute("sum(opnbill)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFOpnAdv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnadv)", "")) ?
                          0 : dt.Compute("sum(opnadv)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFCrAmtas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFDrAmtas")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                          0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFclsbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsbill)", "")) ?
                           0 : dt.Compute("sum(clsbill)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lgvFclsadv")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsadv)", "")) ?
                          0 : dt.Compute("sum(clsadv)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lblgvFIsuAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ?
                          0 : dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lblgvFmrrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ?
                          0 : dt.Compute("sum(mrramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lblgvFBillAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ?
                          0 : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lblgvFBalDr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isudr)", "")) ?
                          0 : dt.Compute("sum(isudr)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lblgvFBalCr")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isucr)", "")) ?
                          0 : dt.Compute("sum(isucr)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSupPay.FooterRow.FindControl("lblgvFBalNetBl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netbill)", "")) ?
                          0 : dt.Compute("sum(netbill)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "ASupConPayment":
                    Session["Report1"] = gvAllSubAconBill;
                    ((HyperLink)this.gvAllSubAconBill.HeaderRow.FindControl("hlbtntbCdataExelalsasub")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFOpalsasub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                          0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFDrAmtalsasub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                  0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFCrAmtalsasub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                   0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFpostdatedcheque")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ?
             0 : dt.Compute("sum(isuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFPenBill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ?
             0 : dt.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFclsamalsasub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isubal)", "")) ? 0 : dt.Compute("sum(isubal)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvAllSubAconBill.FooterRow.FindControl("lgvFclsOwneramalsasub")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0 : dt.Compute("sum(balamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;





            }




        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string type = Request.QueryString["Type"].ToString().Trim();
            string vounum = dt1.Rows[0]["vounum"].ToString();
            string actcode = dt1.Rows[0]["actcode"].ToString();

            switch (type)
            {

                case "DetailLedger":

                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }
                        if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum"].ToString() == vounum))
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["vounum"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            {

                                dt1.Rows[j]["actdesc"] = "";
                            }

                            //if (dt1.Rows[j]["vounum"].ToString() == vounum)
                            //{

                            //    dt1.Rows[j]["vounum"] = "";

                            //}
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            vounum = dt1.Rows[j]["vounum"].ToString();
                            grp = dt1.Rows[j]["grp"].ToString();
                        }

                    }
                    break;
                case "IssPay":

                    string pactcode1 = dt1.Rows[0]["actcode"].ToString();
                    string rescode = dt1.Rows[0]["rescode"].ToString();


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        //if ((dt1.Rows[j]["rescode"].ToString() == rescode) && (dt1.Rows[j]["actcode"].ToString() == pactcode1))
                        //{
                        //    rescode = dt1.Rows[j]["rescode"].ToString();
                        //    pactcode1 = dt1.Rows[j]["actcode"].ToString();
                        //    dt1.Rows[j]["resdesc"] = "";
                        //    dt1.Rows[j]["actdesc"] = "";
                        //}

                        //else
                        //{
                        if (dt1.Rows[j]["rescode"].ToString() == rescode)
                            dt1.Rows[j]["resdesc"] = "";
                        if (dt1.Rows[j]["actcode"].ToString() == pactcode1)
                            dt1.Rows[j]["actdesc"] = "";
                        rescode = dt1.Rows[j]["rescode"].ToString();
                        pactcode1 = dt1.Rows[j]["actcode"].ToString();
                        //}

                    }
                    break;
                case "IssPaySum":

                    break;
                default:

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if ((dt1.Rows[j]["actcode"].ToString() == actcode) && (dt1.Rows[j]["vounum1"].ToString() == vounum))
                        {
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            vounum = dt1.Rows[j]["vounum1"].ToString();
                            dt1.Rows[j]["actdesc"] = "";
                            dt1.Rows[j]["vounum1"] = "";

                        }

                        else
                        {

                            if (dt1.Rows[j]["actcode"].ToString() == actcode)
                            {

                                dt1.Rows[j]["actdesc"] = "";
                            }

                            if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                            {

                                dt1.Rows[j]["vounum1"] = "";

                            }
                            actcode = dt1.Rows[j]["actcode"].ToString();
                            vounum = dt1.Rows[j]["vounum1"].ToString();
                        }





                    }
                    break;
            }


            return dt1;

        }

        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblspledger"];
            if (dt.Rows.Count == 0)
                return;

            DataView dv = dt.DefaultView;
            string type = Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "DetailLedger":
                    dv.RowFilter = "head1='03CT'";
                    dt = dv.ToTable();
                    ((Label)this.gvSpledger.FooterRow.FindControl("lgvFOpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
                            0 : dt.Compute("sum(opam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpledger.FooterRow.FindControl("lgvFDrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
                          0 : dt.Compute("sum(dram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpledger.FooterRow.FindControl("lgvFCrAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
                           0 : dt.Compute("sum(cram)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpledger.FooterRow.FindControl("lgvFClsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                          0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "SPayment":

                    dv.RowFilter = "head1='03CT'";
                    dt = dv.ToTable();

                    dramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ? 0 : dt.Compute("sum(dram)", "")));
                    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFDrAmts")).Text = dramt.ToString("#,##0;(#,##0); ");
                    cramt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ? 0 : dt.Compute("sum(cram)", "")));
                    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFCrAmts")).Text = cramt.ToString("#,##0;(#,##0); ");
                    balamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(balamt)", "")) ? 0 : dt.Compute("sum(balamt)", "")));
                    ((Label)this.gvSPayment.FooterRow.FindControl("lgvFBalAmts")).Text = balamt.ToString("#,##0;(#,##0); ");

                    break;
                case "IssPay":
                    DataTable dt1 = dt.Copy();
                    DataView dv1 = dt1.DefaultView;
                    dv1.RowFilter = "grp1='AA'";
                    dt1 = dv1.ToTable();
                    isuamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(isuamt)", "")) ? 0 : dt1.Compute("sum(isuamt)", "")));
                    ((Label)this.gvMonIsuPay.FooterRow.FindControl("lgvCrAmt")).Text = isuamt.ToString("#,##0;(#,##0); ");
                    reconamt = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(reconamt)", "")) ? 0 : dt1.Compute("sum(reconamt)", "")));
                    ((Label)this.gvMonIsuPay.FooterRow.FindControl("lgvFReconAmt")).Text = reconamt.ToString("#,##0;(#,##0); ");
                    break;
                case "IssPaySum":
                    isuamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(isuamt)", "")) ? 0 : dt.Compute("sum(isuamt)", "")));
                    ((Label)this.gvIsuVsPaySum.FooterRow.FindControl("lgvCrAmt")).Text = isuamt.ToString("#,##0;(#,##0); ");
                    reconamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(reconamt)", "")) ? 0 : dt.Compute("sum(reconamt)", "")));
                    ((Label)this.gvIsuVsPaySum.FooterRow.FindControl("lgvFReconAmt")).Text = reconamt.ToString("#,##0;(#,##0); ");
                    break;

            }

        }
        protected void gvSpledger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComCod();
            //if (e.Row.RowType != DataControlRowType.DataRow)
            //    return;

            //HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvVounum1");
            //string mCOMCOD = comcod;

            //string mVOUNUM = hlink1.Text;
            //string mTRNDAT1 = ((Label)e.Row.FindControl("lblgvvoudate")).Text;

            //if (mVOUNUM.Trim().Length == 14)
            //{
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=voucher&comcod=" + mCOMCOD + "&vounum=" + mVOUNUM + "&Date1=" + mTRNDAT1;
            //    hlink1.Text = mVOUNUM.Substring(0, 2) + mVOUNUM.Substring(6, 2) + "-" + mVOUNUM.Substring(8, 6);
            //}
        }
        protected void gvAllSupPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvAllSupPay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
            //DataTable dt = (DataTable)Session["tblspledger"];
            ////this.gvAllSupPay.Columns[0].HeaderText = (Request.QueryString["Type"].ToString().Trim() == "ASPayment") ? "Supplier's Name" : "Sub-Contractor's Name";
            //this.gvAllSupPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            //this.gvAllSupPay.DataSource = dt;
            //this.gvAllSupPay.DataBind();
            this.SaveValue();
        }
        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "ASPayment":
                case "AConPayment":
                case "Adv":

                    this.gvAllSupPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAllSupPay.DataSource = (DataTable)Session["tblspledger"];
                    this.gvAllSupPay.DataBind();
                    break;

                case "IssPay":
                    this.gvMonIsuPay.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvMonIsuPay.DataSource = (DataTable)Session["tblspledger"];
                    this.gvMonIsuPay.DataBind();
                    this.FooterCal();
                    break;
                case "IssPaySum":
                    this.gvIsuVsPaySum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvIsuVsPaySum.DataSource = (DataTable)Session["tblspledger"];
                    this.gvIsuVsPaySum.DataBind();
                    this.FooterCal();
                    break;



                case "ASupConPayment":
                    this.gvAllSubAconBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvAllSubAconBill.DataSource = (DataTable)Session["tblspledger"];
                    this.gvAllSubAconBill.DataBind();
                    break;


            }

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "ASPayment":
                case "AConPayment":
                case "Adv":
                    this.Data_Bind();
                    this.SaveValue();
                    break;

                case "IssPay":
                    this.Data_Bind();
                    break;
                case "IssPaySum":
                    this.Data_Bind();
                    break;


                case "ASupConPayment":

                    this.Data_Bind();
                    this.SaveValue();
                    break;



            }


        }
        protected void gvAllSupPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvSupDesc");
            string mCOMCOD = comcod;
            string head = ((HyperLink)e.Row.FindControl("HLgvSupDesc")).Text.ToString();
            string sircod = ((Label)e.Row.FindControl("lblSupCode")).Text;
            string mTRNDAT1 = this.txtDateFrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;

            //if (ASTUtility.Right(mACTCODE, 4) == "0000")
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //else
            hlink1.NavigateUrl = "RptAccSpLedgerDet.aspx?rpttype=DetailLedger&comcod=" + mCOMCOD + "&actcode=" + sircod +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&head=" + head;
        }
        protected void gvSpledger_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("HLgvvounum");
                Label OpAmt = (Label)e.Row.FindControl("lblgvOpAmount");
                Label DrAmt = (Label)e.Row.FindControl("lblgvDrAmount");
                Label CrAmt = (Label)e.Row.FindControl("lblgvCrAmount");
                Label ClAmt = (Label)e.Row.FindControl("lblgvClAmount");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "head1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code.Trim() == "AB")
                {
                    hlink.Font.Bold = true;
                    //OpAmt.Font.Bold = true;
                    DrAmt.Font.Bold = true;
                    CrAmt.Font.Bold = true;
                    ClAmt.Font.Bold = true;
                    hlink.Style.Add("text-align", "right");
                }
            }

            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvvounum");
            string voucher = ((HyperLink)e.Row.FindControl("HLgvvounum")).Text.ToString();
            if (voucher.Trim().Length == 14)
            {
                if (ASTUtility.Left(voucher, 2) == "PV" || ASTUtility.Left(voucher, 2) == "DV")
                {
                    hlink1.NavigateUrl = "RptAccVouher02.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
                else
                {
                    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
                    hlink1.Text = voucher.Substring(0, 2) + voucher.Substring(6, 2) + "-" + voucher.Substring(8, 6);
                }
            }

            //if (voucher.Substring(0,2)=="BC"|| voucher.Substring(0,2)=="BD"|| voucher.Substring(0,2)=="CC"|| voucher.Substring(0,2)=="CD"|| voucher.Substring(0,2)=="JV"|| voucher.Substring(0,2)=="CT")  
            //    hlink1.NavigateUrl = "RptAccVouher.aspx?vounum=" + voucher;
        }
        protected void gvMonIsuPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMonIsuPay.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvMonIsuPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label udesc = (Label)e.Row.FindControl("lgvAccDesc");
                Label isuamt = (Label)e.Row.FindControl("lgvcramt");
                Label clramt = (Label)e.Row.FindControl("lgvreconamt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp1")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AB")
                {
                    udesc.Font.Bold = true;
                    isuamt.Font.Bold = true;
                    clramt.Font.Bold = true;
                    udesc.Style.Add("text-align", "right");
                }
            }
        }
        protected void gvIsuVsPaySum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvIsuVsPaySum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        private void Bind_Data()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string Rescode = this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() + "%";

            string mfDate = "frmdate=" + frmdate;
            string mtDate = "todate=" + todate;
            string mRescode = "rescode=" + Rescode;
            string TString = "javascript:window.showModalDialog('AccMultiReport.aspx?" + "rpttype=IssPay&" + mfDate + "&" + mtDate + "&" + mRescode + "', 'Unit Description', 'dialogHeight:700px;dialogWidth:1200px;status:no')";
            this.lnkShowDet.Attributes.Add("OnClick", TString);


        }
        protected void lnkShowDet_Click(object sender, EventArgs e)
        {
            //this.lnkShowDet.Text = "";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = GetComCod();
            //string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            //string Rescode = this.ddlConAccResHead.SelectedValue.Substring(0, 2).ToString() + "%";

            //hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=IssPay&comcod=" + comcod + "&frmdate=" + frmdate + "&todate=" + todate + "&Rescode=" + Rescode;
        }
        protected void gvAllSubAconBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            HyperLink hlink1 = (HyperLink)e.Row.FindControl("HLgvSupDescalsasub");
            HyperLink HLgvpenbill = (HyperLink)e.Row.FindControl("HLgvpenbill");
            string mCOMCOD = comcod;
            string head = ((HyperLink)e.Row.FindControl("HLgvSupDescalsasub")).Text.ToString();
            string sircod = ((Label)e.Row.FindControl("lblSupCodealsasub")).Text;
            string mTRNDAT1 = this.txtDateFrom.Text;
            string mTRNDAT2 = this.txtDateto.Text;



            //if (ASTUtility.Right(mACTCODE, 4) == "0000")
            //    hlink1.NavigateUrl = "AccMultiReport.aspx?rpttype=schedule&comcod=" + mCOMCOD + "&actcode=" + mACTCODE +
            //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2;
            //else
            hlink1.NavigateUrl = "RptAccSpLedgerDet.aspx?rpttype=DetailLedger&comcod=" + mCOMCOD + "&actcode=" + sircod +
                     "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&head=" + head;

            //HLgvpenbill.NavigateUrl = "RptAccSpLedgerDet.aspx?rpttype=DetailLedger&comcod=" + mCOMCOD + "&actcode=" + sircod +
            //         "&Date1=" + mTRNDAT1 + "&Date2=" + mTRNDAT2 + "&head=" + head;
            //   HyperLink HLgvpenbill = (HyperLink)e.Row.FindControl("HLgvpenbill");
        }
        protected void lbtnacchead_Click(object sender, EventArgs e)
        {
            this.GetAccountHead();
        }
        private void GetAccountHead()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string srchaccounthead = "%" + this.txtsrchacchead.Text.Trim() + "%";
            string reshead = this.ddlConAccResHead.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETACCOUNTSHEAD", srchaccounthead, reshead, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlAccHead.DataTextField = "actdesc";
            this.ddlAccHead.DataValueField = "actcode";
            this.ddlAccHead.DataSource = ds1.Tables[0];
            this.ddlAccHead.DataBind();


        }

        private DataTable HiddenSameData2(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescode = dt1.Rows[0]["ssircode"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ssircode"].ToString() == rescode && dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {

                    dt1.Rows[j]["ssirdesc"] = "";
                    dt1.Rows[j]["pactdesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["ssircode"].ToString() == rescode)
                        dt1.Rows[j]["ssirdesc"] = "";

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";








                }

                rescode = dt1.Rows[j]["ssircode"].ToString();
                pactcode = dt1.Rows[j]["pactcode"].ToString();



            }

            return dt1;
        }
        protected void hlnkLgvpenbill_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetComCod();
            string date = this.txtDateto.Text.Trim();
            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            string concod = ((DataTable)Session["tblspledger"]).Rows[RowIndex]["rescode"].ToString();
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "PANDINGPURBILL", date, concod, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }

            Session["tblpaneing"] = this.HiddenSameData2(ds2.Tables[0]);
            DataTable tbl1 = (DataTable)Session["tblpaneing"];
            dgvPurbill.DataSource = tbl1;
            dgvPurbill.DataBind();
            ((Label)this.dgvPurbill.FooterRow.FindControl("txtTgvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amt)", "")) ?
                      0.00 : tbl1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); - ");

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "loadModal();", true);

        }

        protected void lbtnUpdateDetails_OnClick(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string todate = Convert.ToDateTime(this.txtDateto.Text.Trim()).ToString("dd-MMM-yyyy");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt1 = (DataTable)Session["tblpaneing"];

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EclassPendingBill.ClassPendingBil>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPendingBill", lst, null, null);
            //Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam.ToUpper()));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;

            string printype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "target", "PrintRpt('" + printype + "');", true);


            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl ("DDPrintOpt")).SelectedValue.Trim ().ToString () + "', target='_blank');</script>";


        }
    }
}
