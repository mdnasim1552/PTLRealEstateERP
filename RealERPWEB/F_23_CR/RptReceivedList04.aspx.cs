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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_23_CR
{
    public partial class RptReceivedList04 : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txtfrmdate.Text = date;
                this.txttodate.Text = Convert.ToDateTime("01" + date.Substring(2)).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


                this.ViewSelection();
                this.CompanyColumnVisible();

                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = this.Request.QueryString["Type"] == "AllProDuesCollect" ? "Revenue Status"
                    : this.Request.QueryString["Type"] == "MonthlyColSchedule" ? "Monthly Collection Schedule" : this.Request.QueryString["Type"] == "MonthlyColl" ? "Monthly Collection(Receipt Type)"
                    : this.Request.QueryString["Type"] == "MonthlyColScheduleDet" ? "Monthly Collection (Schedule Vs Actual)" : "Monthly Collection Schedule(Summary)";






            }

            //ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 600;

        }

        private void CompanyColumnVisible()
        {
            try
            {

                string type = this.Request.QueryString["Type"].ToString();

                switch (type)
                {
                    case "AllProDuesCollect":
                        string comcod = this.GetCompCode();
                        switch (comcod)
                        {
                            case "3339":   //Tropical

                                this.dgvAccRec03.Columns[9].Visible = false;
                                this.dgvAccRec03.Columns[10].Visible = false;
                                this.dgvAccRec03.Columns[11].Visible = false;
                                break;

                            default:
                                break;

                        }
                        break;
                }

            }


            catch (Exception ex)
            {

                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }




        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        public string GetCompCode()
        {
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }
        private void GetCatagory()
        {


            string comcod = this.GetCompCode();
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "MONTHLYCALLSCHCATEGORY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlCatatory.Items.Clear();
                return;
            }

            this.ddlCatatory.DataTextField = "CATDESC";
            this.ddlCatatory.DataValueField = "CATCODE";
            this.ddlCatatory.DataSource = ds1.Tables[0];
            this.ddlCatatory.DataBind();
            this.ddlCatatory.SelectedValue = "0300000";


        }


        private void ViewSelection()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {

                case "AllProDuesCollect":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


                case "MonthlyColSchedule":
                    this.divlanguage.Visible = true; 
                    this.ddllang.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "MonthlyColl":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;



                case "MonthlyColScheduleSum":
                    this.divlanguage.Visible = true;
                    this.ddllang.Visible = true;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

                case "MonthlyColScheduleDet":
                    this.GetCatagory();
                    this.lbtnOk_Click(null, null);
                    this.divlanguage.Visible = true;
                    this.ddllang.Visible = true;
                    this.MultiView1.ActiveViewIndex = 4;
                    break;


                case "MonthlyDuesOverDues":
                    this.MultiView1.ActiveViewIndex = 5;
                    break;





            }

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {

                case "AllProDuesCollect":

                    string comcod = this.GetCompCode();

                    switch (comcod)
                    {
                        case "3339":
                        case "3101":
                            this.AllProDuesCollectionTropical();
                            break;

                        default:
                            this.PrintAllProDuesCollection();
                            break;
                    }

                    break;
                case "MonthlyColSchedule":
                    this.PrintMonthlyCollectionSchedule();
                    break;

                case "MonthlyColl":
                    this.PrintonthlyCollectionReceiptType();
                    break;

                case "MonthlyColScheduleSum":
                    this.PrintMonthlyColScheduleSum();
                    break;

                case "MonthlyColScheduleDet":
                    this.PrintMonthlyColScheduleDet();
                    break;
            }


        }

        private void PrintMonthlyColScheduleSum()
        {
            if (this.ddllang.SelectedValue.ToString() == "EN")
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string comcod = hst["comcod"].ToString();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;

                DataTable dt = (DataTable)Session["tblAccRec"];
                var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.MonCollScheSummmay>();
                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptMonCollcScheduleSummaryENG", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "MONTHLY COLLECTION SCHEDULE(SUMMARY)"));
                Rpt1.SetParameters(new ReportParameter("date", "From(" + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text + ")"));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            else
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string comcod = hst["comcod"].ToString();
                string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());




                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;

                DataTable dt = (DataTable)Session["tblAccRec"];

                string tValue = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ?
                    0.00 : dt.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");

                //string bname = ASITUtility02.EngtoBandigit(name);
                string sumValue = ASITUtility02.ToBangla(tValue);


                string frmdatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(frmdate).ToString("dd"))) + "-" + (Convert.ToDateTime(frmdate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(frmdate).ToString("yyyy")));
                string todatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(todate).ToString("dd"))) + "-" + (Convert.ToDateTime(todate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(todate).ToString("yyyy")));

                var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.MonCollScheSummmay>();
                LocalReport Rpt1 = new LocalReport();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptMonCollcScheduleSummaryBAN", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comnam));
                Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
                Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "সর্ব মোট সম্ভাব্য আদায় (এক নজরে)"));
                Rpt1.SetParameters(new ReportParameter("date", "(" + frmdatex + " হইতে " + todatex + ")"));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
                Rpt1.SetParameters(new ReportParameter("sumValue", sumValue));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
        }
        //public string GetMonthName(string name)
        //{
        //    return name.Replace("Jan", "জানুয়ারী").Replace("Feb", "ফেব্রুয়ারী").Replace("Mar", "মার্চ").
        //        Replace("Apr", "এপ্রিল").Replace("May", "মে").Replace("Jun", "জুন").Replace("Jul", "জুলাই").
        //        Replace("Aug", "আগস্ট").Replace("Sep", "সেপ্টেম্বর").Replace("Oct", "অক্টোবর").Replace("Nov", "নভেম্বর").
        //        Replace("Dec", "ডিসেম্বর");

        //}
        //public string GetBanglaNumber(int number)
        //{
        //    return string.Concat(number.ToString().Select(c => (char)('\u09E6' + c - '0')));
        //}
        private void PrintMonthlyColScheduleDet()
        {


            string lang = this.ddllang.SelectedValue.ToString();
            if (lang == "EN")
            {
                this.PrintMonthlyColScheduleDetEnglish();
            }
            else
            {
                this.PrintMonthlyColScheduleDetBangla();

            }


        }

        private void PrintMonthlyColScheduleDetEnglish()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tblAccRec"];
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.MonthlyColScheduleDet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptMonCollcScheduleDetENG", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Expected Collection (As per Schedule)"));
            Rpt1.SetParameters(new ReportParameter("date", "From(" + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text + ")"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintMonthlyColScheduleDetBangla()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;
            string frmdatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(frmdate).ToString("dd"))) + "-" + (Convert.ToDateTime(frmdate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(frmdate).ToString("yyyy")));
            string todatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(todate).ToString("dd"))) + "-" + (Convert.ToDateTime(todate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(todate).ToString("yyyy")));

            DataTable dt = (DataTable)Session["tblAccRec"];
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.MonthlyColScheduleDet>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptMonCollcScheduleDetBAN", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "তফসিল অনুযায়ী সম্ভাব্য আদায়"));
            Rpt1.SetParameters(new ReportParameter("date", "(" + frmdatex + " হইতে " + todatex + ")"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintMonthlyCollectionSchedule()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblAccRec"];

            DataTable dt2 = (DataTable)ViewState["tblproject"];

            string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;
            // var lst = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassYearlyColletionForcasting>();
            var lst = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassMonthlyCollectionSchedule>();
            var lst1 = dt2.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassMonthlyCollectionScheduleProject>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptMonCollcSchedule", lst, lst1, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Collection Schedule"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From(" + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text + ")"));

            for (int i = 1; i <= lst1.Count; i++)
            {


                Rpt1.SetParameters(new ReportParameter("p" + i.ToString(), lst1[i - 1].pactdesc.ToString()));

            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Monthly Collection Schedule ";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


        }





        private void AllProDuesCollectionTropical()
        {

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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string Ftdate = "( From " + this.txtfrmdate.Text + " To " + this.txttodate.Text + " )";



            DataTable dt = ((DataTable)Session["tblAccRec"]).Copy();

            DataView dv = dt.DefaultView;
            dv.RowFilter = ("pactcode not like '%AAAA%'");
            dt = dv.ToTable();


            //var lst = dt.DataTableToList< C_23_CRR.EClassSales_03.DueCollStatmentRe>();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassDuesAOverDues>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptDuesAOverDues", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("txtComName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtfrmatodate", Ftdate));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }




        private void PrintAllProDuesCollection()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptAllProDuesColl02();
            TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtCompName.Text = comnam;

            TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking & Others";
            TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Stock, Sales, Received & Dues Statement -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            rpttxttoduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            rpttxtpredues.Text = "Previous Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            rpttxtcurrentdues.Text = "Current  Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");





            TextObject txtdeptpredues = rptRcvList.ReportDefinition.ReportObjects["txtdeptpredues"] as TextObject;
            txtdeptpredues.Text = "Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            TextObject txtdeptcdues = rptRcvList.ReportDefinition.ReportObjects["txtdeptcdues"] as TextObject;
            txtdeptcdues.Text = "Current  Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            TextObject txtpmdues = rptRcvList.ReportDefinition.ReportObjects["txtpmdues"] as TextObject;
            txtpmdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53001'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53001'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtpcrdues = rptRcvList.ReportDefinition.ReportObjects["txtpcrdues"] as TextObject;
            txtpcrdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53002'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53002'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdealy = rptRcvList.ReportDefinition.ReportObjects["txtdealy"] as TextObject;
            txtdealy.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53003'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53003'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtptodues = rptRcvList.ReportDefinition.ReportObjects["txtptodues"] as TextObject;
            txtptodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='AAAAA'").Length > 0 ? Convert.ToDouble(dt1.Select("code='AAAAA'")[0]["pdues"]).ToString("#,##0;(#,##0);") : "";


            TextObject txtcmdues = rptRcvList.ReportDefinition.ReportObjects["txtcmdues"] as TextObject;
            txtcmdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53001'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53001'")[0]["cdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtccrdues = rptRcvList.ReportDefinition.ReportObjects["txtccrdues"] as TextObject;
            txtccrdues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53002'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53002'")[0]["cdues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtctodues = rptRcvList.ReportDefinition.ReportObjects["txtctodues"] as TextObject;
            txtctodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='AAAAA'").Length > 0 ? Convert.ToDouble(dt1.Select("code='AAAAA'")[0]["cdues"]).ToString("#,##0;(#,##0);") : "";



            TextObject txtmtodues = rptRcvList.ReportDefinition.ReportObjects["txtmtodues"] as TextObject;
            txtmtodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53001'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53001'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtcrtodues = rptRcvList.ReportDefinition.ReportObjects["txtcrtodues"] as TextObject;
            txtcrtodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53002'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53002'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtdtodues = rptRcvList.ReportDefinition.ReportObjects["txtdtodues"] as TextObject;
            txtdtodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='53003'").Length > 0 ? Convert.ToDouble(dt1.Select("code='53003'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtnettodues = rptRcvList.ReportDefinition.ReportObjects["txtnettodues"] as TextObject;
            txtnettodues.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='AAAAA'").Length > 0 ? Convert.ToDouble(dt1.Select("code='AAAAA'")[0]["todues"]).ToString("#,##0;(#,##0);") : "";











            TextObject txtuserinfo = rptRcvList.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptRcvList.SetDataSource(this.HiddenSameData((DataTable)Session["tblAccRec"]));
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Received List Info";
                string eventdesc = "Print Report MR";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptRcvList.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptRcvList;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        private void PrintonthlyCollectionReceiptType()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblAccRec"];

            string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;
            var lst = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.RptMonthlyCollecionReceiptType>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptMonthlyCollReceiptType", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Monthly Collection(Receipt Type)"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From(" + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text + ")"));

            //for (int i = 1; i <= 12; i++)
            //{
            //    if (frmdate > todate)
            //        break;

            //    Rpt1.SetParameters(new ReportParameter("duemonth" + i.ToString(), frmdate.ToString("MMM yy")));
            //    frmdate = frmdate.AddMonths(1);

            //}


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Monthly Collection(Receipt Type) ";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }







        }






        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "AllProDuesCollect":
                    this.AllProDuesCollection();
                    break;


                case "MonthlyColSchedule":
                    this.ShowMonthlCollSchedule();
                    break;

                case "MonthlyColl":
                    this.ShowMonthlCollection();
                    break;

                case "MonthlyColScheduleSum":
                    this.MonthlyColScheduleSum();
                    break;


                case "MonthlyColScheduleDet":
                    this.ShowMonthlyColScheduleDet();
                    break;


                case "MonthlyDuesOverDues":
                    this.ShowMonthlyDuesOverDues();
                    break;
            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "AllProDuesCollect":
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

                case "MonthlyColSchedule":

                    DateTime cdate = Convert.ToDateTime(dt1.Rows[0]["cdate"].ToString());
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (Convert.ToDateTime(dt1.Rows[j]["cdate"].ToString()) == cdate)
                        {

                            dt1.Rows[j]["cdate1"] = "";
                        }

                        cdate = Convert.ToDateTime(dt1.Rows[j]["cdate"].ToString());




                    }

                    break;

                case "MonthlyColScheduleDet":
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

                            dr1["pactdesc"] = "";

                        }


                        pactcode = dr1["pactcode"].ToString();
                    }

                    break;



                case "MonthlyDuesOverDues":
                    i = 0;
                    pactcode = dt1.Rows[0]["pactcode"].ToString();

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

                            dr1["pactdesc"] = "";

                        }


                        pactcode = dr1["pactcode"].ToString();
                    }

                    break;
                default:
                    //string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    //    {
                    //        pactcode = dt1.Rows[j]["pactcode"].ToString();
                    //        dt1.Rows[j]["pactdesc"] = "";
                    //    }

                    //    else
                    //    {
                    //        pactcode = dt1.Rows[j]["pactcode"].ToString();
                    //    }

                    //}
                    break;
            }


            return dt1;
        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAccRec"];
                string comcod = this.GetCompCode();

                string type = this.Request.QueryString["Type"].ToString();
                int i, j;
                //  double p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20;
                //  double r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14;
                switch (type)
                {
                    case "AllProDuesCollect":
                        // this.dgvAccRec03.Columns[17].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
                        this.dgvAccRec03.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec03.DataSource = dt;
                        this.dgvAccRec03.DataBind();

                        if (dt.Rows.Count > 0)
                        {

                            Session["Report1"] = dgvAccRec03;
                            if (dt.Rows.Count > 0)

                                ((HyperLink)this.dgvAccRec03.HeaderRow.FindControl("hlbtntbCdataExel1")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }


                        this.FooterCalculation();
                        break;


                    case "MonthlyColSchedule":
                        for (i = 5; i < this.gvmoncollsch.Columns.Count - 1; i++)
                            this.gvmoncollsch.Columns[i].Visible = false;
                        j = 5;
                        DataTable dtp = (DataTable)ViewState["tblproject"];
                        for (i = 0; i < dtp.Rows.Count; i++)
                        {

                            this.gvmoncollsch.Columns[j].Visible = true;
                            this.gvmoncollsch.Columns[j].HeaderText = dtp.Rows[i]["pactdesc"].ToString();


                            j++;
                        }
                        this.gvmoncollsch.DataSource = dt;
                        this.gvmoncollsch.DataBind();

                        if (dt.Rows.Count > 0)
                        {
                            Session["Report1"] = gvmoncollsch;
                            ((HyperLink)this.gvmoncollsch.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

                        }
                        break;

                    case "MonthlyColl":


                        for (i = 7; i < this.gvmoncoll.Columns.Count - 1; i++)
                            this.gvmoncoll.Columns[i].Visible = false;
                        j = 7;
                        DataTable dtt = (DataTable)ViewState["tblrectype"];
                        for (i = 0; i < dtt.Rows.Count; i++)
                        // for (i = 0; i <= 23; i++)
                        {
                            this.gvmoncoll.Columns[j].Visible = true;
                            this.gvmoncoll.Columns[j].HeaderText = dtt.Rows[i]["recpdesc"].ToString();

                            j++;

                        }



                        this.gvmoncoll.DataSource = dt;
                        this.gvmoncoll.DataBind();
                        break;


                    case "MonthlyColScheduleSum":
                        string langtyp = this.ddllang.SelectedValue.ToString();
                        if (langtyp == "EN")
                        {
                            this.gvmoncollschsum.Visible = true;

                            this.gvmoncollschsumBN.Visible = false;
                            this.gvmoncollschsum.DataSource = dt;
                            this.gvmoncollschsum.DataBind();
                            this.FooterCalculation();
                        }
                        else
                        {
                            this.gvmoncollschsum.Visible = false;
                            this.gvmoncollschsumBN.Visible = true;

                            this.gvmoncollschsumBN.DataSource = dt;
                            this.gvmoncollschsumBN.DataBind();
                            this.FooterCalculation();
                        }




                        break;


                    case "MonthlyColScheduleDet":
                        this.gvmoncolschandac.DataSource = dt;
                        this.gvmoncolschandac.DataBind();

                        if (dt.Rows.Count > 0)
                        {

                            Session["Report1"] = gvmoncolschandac;
                            if (dt.Rows.Count > 0)

                                ((HyperLink)this.gvmoncolschandac.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;

                    case "MonthlyDuesOverDues":
                        this.gvDuesOverdues.DataSource = dt;
                        this.gvDuesOverdues.DataBind();
                        this.FooterCalculation();

                        if (dt.Rows.Count > 0)
                        {

                            Session["Report1"] = gvDuesOverdues;
                            if (dt.Rows.Count > 0)

                                ((HyperLink)this.gvDuesOverdues.HeaderRow.FindControl("hlbtntbCdataduesExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        }
                        break;













                }





            }

            catch (Exception e)
            {
            }



        }

        private void FooterCalculation()
        {
            DataTable dt = ((DataTable)Session["tblAccRec"]).Copy();
            if (dt.Rows.Count == 0)
                return;
            string pactcode = "";
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {




                case "AllProDuesCollect":

                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("pactcode not like '%AAAA%'");
                    dt = dv.ToTable();

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtstkamal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tstkam)", "")) ?
                     0.00 : dt.Compute("Sum(tstkam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                   0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoreceivedal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                  0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFatoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
                    0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtpredues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ptodues)", "")) ?
                       0.00 : dt.Compute("Sum(ptodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                       0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");




                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                   0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");





                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdelchargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                     0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdischargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
                    0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFnettoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
                   0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "MonthlyColScheduleSum":

                    if (ddllang.SelectedValue.ToString() == "EN")
                    {
                        ((Label)this.gvmoncollschsum.FooterRow.FindControl("lblgvFTotalAmount")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ?
                       0.00 : dt.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");
                    }
                    else
                    {
                        string ttlamt = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(schamt)", "")) ?
                            0.00 : dt.Compute("Sum(schamt)", ""))).ToString("#,##0;(#,##0); ");

                        string ttlamount = ASITUtility02.ToBangla(ttlamt);
                        ((Label)this.gvmoncollschsumBN.FooterRow.FindControl("lblgvFTotalAmountbn")).Text = ttlamount;

                    }




                    break;


                case "MonthlyDuesOverDues":


                    ((Label)this.gvDuesOverdues.FooterRow.FindControl("lgvFDuesamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(overdues)", "")) ?
                           0.00 : dt.Compute("Sum(overdues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDuesOverdues.FooterRow.FindControl("lgvFReceived")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(recamt)", "")) ?
                       0.00 : dt.Compute("Sum(recamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDuesOverdues.FooterRow.FindControl("lgvFBalamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(balamt)", "")) ?
                       0.00 : dt.Compute("Sum(balamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDuesOverdues.FooterRow.FindControl("lgvFcduesamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(curdues)", "")) ?
                       0.00 : dt.Compute("Sum(curdues)", ""))).ToString("#,##0;(#,##0); ");





                    break;

            }







        }



        private string GetComDelARet()
        {
            string comDelaRet = "";
            string comcod = this.GetCompCode();
            switch (comcod)
            {

                case "3339":
                case "3101":
                    comDelaRet = "Deladis";
                    break;

                default:
                    break;

            }


            return comDelaRet;
        }



        private void AllProDuesCollection()
        {
            try
            {
                this.pnlIndPro.Visible = true;
                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string searchinfo = "";
                if (this.ddlSrchCash.SelectedValue != "")
                {

                    if (this.ddlSrchCash.SelectedValue == "between")
                    {
                        searchinfo = "(vtodues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                    }

                    else
                    {
                        searchinfo = "( vtodues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                    }
                }

                string comDelaRet = GetComDelARet();


                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT02", "RPTDATEWALLPROINSDUES", "", frmdate, todate, searchinfo, comDelaRet, "", "", "", "");

                if (ds2 == null)
                {
                    this.dgvAccRec03.DataSource = null;
                    this.dgvAccRec03.DataBind();
                    return;
                }
                Session["tblAccRec"] = HiddenSameData(ds2.Tables[0]);
                ViewState["tbltosusold"] = ds2.Tables[1];
                this.ShowSummary();

                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }

            //



        }
        private void ShowSummary()
        {
            DataTable dt = (DataTable)ViewState["tbltosusold"];
            this.gvinpro.Columns[2].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMMM-yyyy");
            this.gvinpro.Columns[3].HeaderText = "Current Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            this.gvinpro.DataSource = dt;
            this.gvinpro.DataBind();
        }

        private void ShowMonthlCollSchedule()
        {
            try
            {

                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONHLYCOLLECTIONDWISE", "", frmdate, todate, "", "", "", "", "", "");

                if (ds2 == null)
                {
                    this.gvmoncollsch.DataSource = null;
                    this.gvmoncollsch.DataBind();
                    return;
                }
                Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
                ViewState["tblproject"] = ds2.Tables[1];

                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }

        }



        private void ShowMonthlCollection()
        {
            try
            {

                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONHLYMRRECTYPEWISE", "", frmdate, todate, "", "", "", "", "", "");

                if (ds2 == null)
                {
                    this.gvmoncoll.DataSource = null;
                    this.gvmoncoll.DataBind();
                    return;
                }
                Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
                ViewState["tblrectype"] = ds2.Tables[1];

                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }


        }



        private void ShowMonthlyDuesOverDues()
        {
            try
            {

                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGTBDUES", "RPTDUESAOVERDUES02", "18%", frmdate, todate, "", "", "", "", "", "");

                if (ds2 == null)
                {
                    this.gvDuesOverdues.DataSource = null;
                    this.gvDuesOverdues.DataBind();
                    return;
                }
                Session["tblAccRec"] = ds2.Tables[0];  // this.HiddenSameData(ds2.Tables[0]);


                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }


        }



        private void MonthlyColScheduleSum()
        {


            try
            {
                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONHLYCOLLSCHSUMMARY", "", frmdate, todate, "", "", "", "", "", "");

                if (ds2 == null)
                {
                    this.gvmoncollschsum.DataSource = null;
                    this.gvmoncollschsum.DataBind();
                    this.gvmoncollschsumBN.DataSource = null;
                    this.gvmoncollschsumBN.DataBind();
                    return;
                }


                DataTable dt = ds2.Tables[0];
                dt.Columns.Add("schamtbn", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    string digit = Convert.ToDouble(row["schamt"]).ToString("#,##0;(#,##0);");
                    string bndigit = ASITUtility02.ToBangla(digit);
                    row["schamtbn"] = bndigit;   // or set it to some other value
                }
                Session["tblAccRec"] = dt;


                this.Data_Bind();


            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }



        }


        private void ShowMonthlyColScheduleDet()
        {


            try
            {

                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                string categoryId = ((this.ddlCatatory.SelectedValue.ToString() == "0000000") ? "" : this.ddlCatatory.SelectedValue.ToString().Substring(0, 2)) + "%";

                DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROCURDUESANDPAY", "18%", frmdate, todate, categoryId, "", "", "", "", "");

                if (ds2 == null)
                {
                    this.gvmoncolschandac.DataSource = null;
                    this.gvmoncolschandac.DataBind();
                    return;
                }


                DataTable dt = ds2.Tables[0];
                dt.Columns.Add("salesdatebn", typeof(string));
                dt.Columns.Add("curduesdatebn", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    DateTime salesdate = Convert.ToDateTime(row["salesdate"].ToString());
                    DateTime curduesdate = Convert.ToDateTime(row["curduesdate"].ToString());
                    string salesdatex = "";
                    string curduesdatex = "";
                    if (Convert.ToDateTime(salesdate).ToString("yyyy") != "1900")
                    {
                        salesdatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(salesdate).ToString("dd"))) + "-" + (Convert.ToDateTime(salesdate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(salesdate).ToString("yyyy")));

                    }
                    if (Convert.ToDateTime(curduesdate).ToString("yyyy") != "1900")
                    {
                        curduesdatex = ASITUtility03.GetMonthName(ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(curduesdate).ToString("dd"))) + "-" + (Convert.ToDateTime(curduesdate).ToString("MMM"))) + "-" + ASITUtility03.GetBanglaNumber(Convert.ToInt16(Convert.ToDateTime(curduesdate).ToString("yyyy")));

                    }


                    row["salesdatebn"] = salesdatex;   // or set it to some other value
                    row["curduesdatebn"] = curduesdatex;   // or set it to some other value
                }






                Session["tblAccRec"] = this.HiddenSameData(dt);
                this.Data_Bind();
            }
            catch (Exception ed)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + ed.Message + "');", true);

            }



        }



        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }


        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.divAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }


        protected void dgvAccRec03_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec03.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void dgvAccRec03_RowDataBound(object sender, GridViewRowEventArgs e)
        {







            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink HLgvDesc = (HyperLink)e.Row.FindControl("HLgvDesc");
                Label lgvtstkamal = (Label)e.Row.FindControl("lgvtstkamal");
                Label lgvtocsotal = (Label)e.Row.FindControl("lgvtocsotal");
                Label lgvtotreceivedal = (Label)e.Row.FindControl("lgvtotreceivedal");
                Label lgvtatoduesall = (Label)e.Row.FindControl("lgvtatoduesall");
                Label lgvtoduesal = (Label)e.Row.FindControl("lgvtoduesal");
                Label lgvdelchargeal = (Label)e.Row.FindControl("lgvdelchargeal");
                Label lgvdischargeal = (Label)e.Row.FindControl("lgvdischargeal");
                Label lgvnettoduesal = (Label)e.Row.FindControl("lgvnettoduesal");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string comcod = this.GetCompCode();
                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDesc.Font.Bold = true;
                    lgvtstkamal.Font.Bold = true;
                    lgvtocsotal.Font.Bold = true;
                    lgvtotreceivedal.Font.Bold = true;
                    lgvtatoduesall.Font.Bold = true;

                    lgvtoduesal.Font.Bold = true;
                    lgvdelchargeal.Font.Bold = true;
                    lgvdischargeal.Font.Bold = true;
                    lgvnettoduesal.Font.Bold = true;



                }

                else
                {
                    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                    string frmdate = this.txtfrmdate.Text;
                    string todate = this.txttodate.Text;
                    HLgvDesc.NavigateUrl = "~/F_23_CR/LinkRptSaleDues.aspx?Type=DuesCollect&comcod=" + comcod + "&pactcode=" + code + "&pactdesc=" + pactdesc + "&Date1=" + frmdate + "&Date2=" + todate;
                    HLgvDesc.Style.Add("color", "blue");

                }

            }


        }

        protected void gvmoncollsch_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label custname = (Label)e.Row.FindControl("lgvcustname");
                Label lgvp1 = (Label)e.Row.FindControl("lgvp1");
                Label lgvp2 = (Label)e.Row.FindControl("lgvp2");
                Label lgvp3 = (Label)e.Row.FindControl("lgvp3");
                Label lgvp4 = (Label)e.Row.FindControl("lgvp4");
                Label lgvp5 = (Label)e.Row.FindControl("lgvp5");
                Label lgvp6 = (Label)e.Row.FindControl("lgvp6");
                Label lgvp7 = (Label)e.Row.FindControl("lgvp7");
                Label lgvp8 = (Label)e.Row.FindControl("lgvp8");
                Label lgvp9 = (Label)e.Row.FindControl("lgvp9");
                Label lgvp10 = (Label)e.Row.FindControl("lgvp10");
                Label lgvp11 = (Label)e.Row.FindControl("lgvp11");
                Label lgvp12 = (Label)e.Row.FindControl("lgvp12");
                Label lgvp13 = (Label)e.Row.FindControl("lgvp13");
                Label lgvp14 = (Label)e.Row.FindControl("lgvp14");
                Label lgvp15 = (Label)e.Row.FindControl("lgvp15");
                Label lgvp16 = (Label)e.Row.FindControl("lgvp16");
                Label lgvp17 = (Label)e.Row.FindControl("lgvp17");
                Label lgvp18 = (Label)e.Row.FindControl("lgvp18");
                Label lgvp19 = (Label)e.Row.FindControl("lgvp19");
                Label lgvp20 = (Label)e.Row.FindControl("lgvp20");
                Label lgvtotal = (Label)e.Row.FindControl("lgvtotal");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    custname.Font.Bold = true;
                    lgvp1.Font.Bold = true;
                    lgvp2.Font.Bold = true;
                    lgvp3.Font.Bold = true;
                    lgvp4.Font.Bold = true;
                    lgvp5.Font.Bold = true;
                    lgvp6.Font.Bold = true;
                    lgvp7.Font.Bold = true;
                    lgvp8.Font.Bold = true;
                    lgvp9.Font.Bold = true;
                    lgvp10.Font.Bold = true;
                    lgvp11.Font.Bold = true;
                    lgvp12.Font.Bold = true;
                    lgvp13.Font.Bold = true;
                    lgvp14.Font.Bold = true;
                    lgvp15.Font.Bold = true;
                    lgvp16.Font.Bold = true;
                    lgvp17.Font.Bold = true;
                    lgvp18.Font.Bold = true;
                    lgvp19.Font.Bold = true;
                    lgvp20.Font.Bold = true;
                    lgvtotal.Font.Bold = true;
                    custname.Style.Add("text-align", "right");
                }


            }
        }
        protected void gvmoncoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label custname = (Label)e.Row.FindControl("lgvcustnamemcoll");
                Label lgvr1 = (Label)e.Row.FindControl("lgvr1");
                Label lgvr2 = (Label)e.Row.FindControl("lgvr2");
                Label lgvr3 = (Label)e.Row.FindControl("lgvr3");
                Label lgvr4 = (Label)e.Row.FindControl("lgvr4");
                Label lgvr5 = (Label)e.Row.FindControl("lgvr5");
                Label lgvr6 = (Label)e.Row.FindControl("lgvr6");
                Label lgvr7 = (Label)e.Row.FindControl("lgvr7");
                Label lgvr8 = (Label)e.Row.FindControl("lgvr8");
                Label lgvr9 = (Label)e.Row.FindControl("lgvr9");
                Label lgvr10 = (Label)e.Row.FindControl("lgvr10");
                Label lgvr11 = (Label)e.Row.FindControl("lgvr11");
                Label lgvr12 = (Label)e.Row.FindControl("lgvr12");
                Label lgvr13 = (Label)e.Row.FindControl("lgvr13");
                Label lgvr14 = (Label)e.Row.FindControl("lgvr14");

                Label lgvtotal = (Label)e.Row.FindControl("lgvtotalmcoll");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    custname.Font.Bold = true;
                    lgvr1.Font.Bold = true;
                    lgvr2.Font.Bold = true;
                    lgvr3.Font.Bold = true;
                    lgvr4.Font.Bold = true;
                    lgvr5.Font.Bold = true;
                    lgvr6.Font.Bold = true;
                    lgvr7.Font.Bold = true;
                    lgvr8.Font.Bold = true;
                    lgvr9.Font.Bold = true;
                    lgvr10.Font.Bold = true;
                    lgvr11.Font.Bold = true;
                    lgvr12.Font.Bold = true;
                    lgvr13.Font.Bold = true;
                    lgvr14.Font.Bold = true;

                    lgvtotal.Font.Bold = true;
                    custname.Style.Add("text-align", "right");
                }

                string comcod = this.GetCompCode();
                if (comcod == "1205" || comcod == "3351" || comcod == "3352")
                {

                }

            }
        }
        protected void gvmoncollschsum_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvmoncolschandac_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {






                Label lblgvcustname = (Label)e.Row.FindControl("lblgvcustname");
                Label lgvapamt = (Label)e.Row.FindControl("lgvapamt");
                Label lgvcarparking = (Label)e.Row.FindControl("lgvcarparking");
                Label lgvutilityaoth = (Label)e.Row.FindControl("lgvutilityaoth");
                Label lgvmodifiaction = (Label)e.Row.FindControl("lgvmodifiaction");
                Label lgvothers = (Label)e.Row.FindControl("lgvothers");
                Label lgvTotalschandac = (Label)e.Row.FindControl("lgvTotalschandac");
                Label lgvrcvamt = (Label)e.Row.FindControl("lgvrcvamt");
                Label lgvbalamt = (Label)e.Row.FindControl("lgvbalamt");
                Label lgvtcurduesschandac = (Label)e.Row.FindControl("lgvtcurduesschandac");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lblgvcustname.Attributes["style"] = "font-weight:bold; text-align:right;";
                    lgvapamt.Font.Bold = true;
                    lgvcarparking.Font.Bold = true;
                    lgvutilityaoth.Font.Bold = true;
                    lgvmodifiaction.Font.Bold = true;
                    lgvothers.Font.Bold = true;

                    lgvTotalschandac.Font.Bold = true;
                    lgvrcvamt.Font.Bold = true;
                    lgvbalamt.Font.Bold = true;
                    lgvtcurduesschandac.Font.Bold = true;



                }



            }


        }
        protected void ddllang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvmoncollschsumBN_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblgvcustname = (Label)e.Row.FindControl("lgvcollschsumamt");
                var valueSet = (Label)e.Row.FindControl("lgvcollschsumamt");
                string digit = Convert.ToDouble(lblgvcustname.Text).ToString("#,##0;(#,##0);");

                //string bname = ASITUtility02.EngtoBandigit(name);
                string bname1 = ASITUtility02.ToBangla(digit);
                valueSet.Text = bname1;
            }
        }
              

        protected void gvmoncollschsum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmoncollschsum.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvDuesOverdues_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDuesOverdues.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvmoncoll_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmoncoll.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvmoncollsch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvmoncollsch.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}











