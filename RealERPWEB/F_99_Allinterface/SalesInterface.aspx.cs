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
using Microsoft.Reporting.WinForms;
using System.IO;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_99_Allinterface
{

    public partial class SalesInterface : System.Web.UI.Page
    {
        //public static string orderno = "", centrid = "", custid = "", orderno1 = "", orderdat = "", Delstatus = "", Delorderno = "", RDsostatus="";
        ProcessAccess accData = new ProcessAccess();
        Common Common = new Common();
        //Xml_BO_Class lst = new Xml_BO_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];

                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError");



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES INTERFACE";//
                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                // this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DateTime date = Convert.ToDateTime("01" + curdate.Substring(2));
                DateTime enddate = date.AddMonths(1).AddDays(-1);


                this.txtfrmdate_CalendarExtender.StartDate = date;
                this.txtfrmdate_CalendarExtender.EndDate = enddate;
                this.txttodate_CalendarExtender.StartDate = date;
                this.txttodate_CalendarExtender.EndDate = enddate;
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.RadioButtonList1.SelectedIndex = 0;
                txtdate_TextChanged(null, null);
                this.GetProjectName();



            }
        }



        private void GetComFromDate()
        {

            string comcod = this.GetCompCode();

            switch (comcod)
            {
                case "3101": //Urban  
                case "3353"://Manama
                case "3355"://Green Wood
                    Hashtable hst = (Hashtable)Session["tblLogin"];
                    this.txtfrmdate.Text = Convert.ToDateTime(hst["opndate"].ToString()).AddDays(1).ToString("dd-MMM-yyyy");

                    break;

                default:
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    break;



            }




        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "3":
                    this.PrintCurrentDues();
                    break;
                case "4":
                    this.PrintOverDues();
                    break;
                case "5":
                    if (comcod == "3368")//finlay
                    {
                        this.PrintAllDues();
                    }
                    else
                    {
                        this.PrintMonthlyColl();
                    }
                   
                    break;
            }
        }


        private void PrintOverDues()
        {
            // Iqbal    Nayan  
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string date = this.txtdate.Text.ToString(); // System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            string DateFT = "(From : " + frmdate + " To: " + todate + ")";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblCustDues"];

            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EclassCurrentDues>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCurrentDues", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("header", "Over Dues"));
            //Rpt1.SetParameters(new ReportParameter("daterange", DateFT));
            // Rpt1.SetParameters(new ReportParameter("RptTitle", "Customer Dues Information"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintCurrentDues()
        {
            // Iqbal    Nayan  
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string date = this.txtdate.Text.ToString(); // System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            string DateFT = "(From : " + frmdate + " To: " + todate + ")";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblCustDues"];

            var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EclassCurrentDues>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptCurrentDues", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("comname", comnam));
            Rpt1.SetParameters(new ReportParameter("header", "Current Dues"));
            //Rpt1.SetParameters(new ReportParameter("daterange", DateFT));
            // Rpt1.SetParameters(new ReportParameter("RptTitle", "Customer Dues Information"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintMonthlyColl()
        {
            // Iqbal    Nayan  
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date = this.txtdate.Text.ToString(); // System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string DateFT = "(From : " + frmdate + " To: " + todate + ")";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            LocalReport Rpt1 = new LocalReport();
            DataTable dt = (DataTable)Session["tblCustDues"];
            var lst = dt.DataTableToList<RealEntity.C_99_AllInterface.Netdues01>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_99_AllInterface.RptNetDuesInfo", lst, null, null);
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("daterange", DateFT));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Net Dues"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



        private void PrintAllDues()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            //string frmdate = Convert.ToDateTime(this.txtfrmDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string date = this.txtdate.Text.ToString(); // System.DateTime.Today.ToString("dd-MMM-yyyy");
            string frmdate = "01" + date.Substring(2);
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string DateFT = "(From : " + frmdate + " To: " + todate + ")";
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            
            DataTable dt = (DataTable)Session["tblCustDues"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("pactcode <> '' ");
            dt = dv.ToTable();
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_99_AllInterface.AllDues>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_99_AllInterface.RptAllDuesInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("daterange", DateFT));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "All Dues Information"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblspledger"];
            //if (dt == null)
            // txtdate_TextChanged(null, null);


        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.txtdate_TextChanged(null, null);


        }
        protected void lnkInteface_Click(object sender, EventArgs e)
        {
            //this.pnlInterf.Visible = true;
            //this.pnlPurchase.Visible = false;
            //this.RadioButtonList1.SelectedIndex = 0;
        }
        protected void lnkRept_Click(object sender, EventArgs e)
        {
            //this.pnlInterf.Visible = false;
            //this.pnlPurchase.Visible = true;
        }
        public string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? comcod;
            comcod = qcomcod.Length > 0 ? qcomcod : comcod;
            return comcod;
        }

        protected void txtdate_TextChanged(object sender, EventArgs e)
        {
            this.Countqty();
            this.SaleRequRpt();
            this.RadioButtonList1_SelectedIndexChanged(null, null);
        }


        private void Countqty()
        {
            string comcod = this.GetCompCode();

            string frdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string endmonth = Convert.ToDateTime(frdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALES_INTERFACE", "SALESDASHBORD", frdate, todate, endmonth, "", "", "", "", "", "");

            DataTable dt = ds1.Tables[0];

            ViewState["tblcount"] = dt;


        }

        private void SaleRequRpt()
        {
            DataTable dt = (DataTable)ViewState["tblcount"];
            if (dt.Rows.Count == 0)
            {
                return;
            }

            string comcod = this.GetCompCode().Substring(0, 3);
            string daywisesales = "";
            string unsoldrent = "";
            switch (comcod)
            {

                case "319"://Rend
                    daywisesales = "Day Wise Rent";
                    unsoldrent = "Unrent Unit";
                    break;
                default:
                    daywisesales = "Day Wise Sales";
                    unsoldrent = "Unsold Unit";
                    break;



            }

            //        comcod, daywise, moneyrcp, unsoldqty, currdus, overdus, duslter

            this.RadioButtonList1.Items[0].Text = "<span class='fa  fa-signal fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["daywise"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + daywisesales + "</span>";
            this.RadioButtonList1.Items[1].Text = "<span class='fa fa-pencil-ruler fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["moneyrcp"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Money Receipt" + "</span>";
            this.RadioButtonList1.Items[2].Text = "<span class='fa fa-pencil-ruler fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["unsoldqty"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + unsoldrent + "</span>";
            this.RadioButtonList1.Items[3].Text = "<span class='fa fa-pencil-ruler fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["currdus"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Current Dues" + "</span>";
            this.RadioButtonList1.Items[4].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["overdus"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class=lbldata2>" + "Over Dues" + "</span>";
            this.RadioButtonList1.Items[5].Text = "<span class='fa fa-calculator fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + Convert.ToDouble(dt.Rows[0]["collcurmon"]).ToString("#,##0;(#,##0); ") + "</span>" + "<span class='lbldata2'>" + "Net Dues" + "</span>";


            //this.RadioButtonList1.Items[6].Text = "<span class='fa fa-credit-card fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<span class='lbldata2'>" + "Money Receipt App" + "</span>";

            //this.RadioButtonList1.Items[6].Text = "<span class='fa fa-credit-card fan'> </span>" + "<br>" + "<span class='lbldata counter'>" + "</span>" + "<a href='../F_17_Acc/MoneyRecptApprov' target='_blank'><span class=lbldata2>" + "Money Receipt Appv" + "</span></a>";


            ////// All Order
            ////DataTable dt = new DataTable();

            ////DataView dv = new DataView();
            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            ////dv.Sort = ("Supcode");
            ////this.Data_Bind("gvProSlInfo", dv.ToTable());


            //////In-process
            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            //////dv.RowFilter = ("sostatus = 'In-process' or  sostatus = 'Request' ");
            ////dv.RowFilter = ("sostatus = 'Request' ");
            ////this.Data_Bind("gvInprocess", dv.ToTable());



            //////Approved
            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            ////dv.RowFilter = ("sostatus = 'In-process' ");
            //////dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'In-process' ");
            ////this.Data_Bind("gvApproved", dv.ToTable());

            //////Ready for Delivery
            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            ////dv.RowFilter = ("sostatus = 'Approved' or sostatus = 'Scanned' or sostatus = 'Scanned Incomplete' ");
            //////dv.RowFilter = ("sostatus = 'Request' or sostatus = 'Approved' or sostatus = 'In-process' or sostatus = 'Scanned' or sostatus = 'Scanned Incomplete' ");
            ////this.Data_Bind("gvReadyDelivery", dv.ToTable());

            //////Ready for Delivery
            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            //////dv.RowFilter = ("sostatus = 'Request' or sostatus = 'Approved' or sostatus = 'In-process' or sostatus = 'Scanned' ");
            //////dv.RowFilter = ("pstatus <> 'OK' ");
            ////dv.RowFilter = ("opdstatus like '%In-process%' or opdstatus like '%Approved%' or opdstatus like '%Scanned%' ");
            ////DataTable dtp = dv.ToTable();
            ////dv = dtp.DefaultView;
            ////dv.RowFilter = ("opdstatus not like '%Pay. Conf%' ");
            ////this.Data_Bind("gvPayConfr", dv.ToTable());

            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            //////dv.RowFilter = ("opdstatus = 'Request' or sostatus = 'Approved' or sostatus = 'In-process' or sostatus = 'Scanned' ");
            //////dv.RowFilter = ("opdstatus like '%Pay. Conf%' or opdstatus like '%Approved%' or opdstatus like '%Scanned%' ");
            ////dv.RowFilter = ("pstatus = 'OK' ");
            ////this.Data_Bind("gvDispatch", dv.ToTable());

            ////dt = ((DataTable)ds1.Tables[0]).Copy();
            ////dv = dt.DefaultView;
            //////dv.RowFilter = ("sostatus = 'Request' or sostatus = 'Approved' or sostatus = 'In-process' or sostatus = 'Scanned' ");
            ////dv.RowFilter = ("opdstatus like '%Dispatch%' ");
            ////this.Data_Bind("gvDeliverd", dv.ToTable());


            ////ViewState["tblInvList"] = HiddenSameData2(((DataTable)ds1.Tables[1]).Copy());
            ////this.Data_Bind();
            //////this.HiddenSameDate(((DataTable)ds1.Tables[1]).Copy());
            ////this.CalculatrGridTotal();
        }
        private DataTable HiddenSameData2(DataTable dt1)
        {
            ////if (dt1.Rows.Count == 0)
            ////    return dt1;

            ////string sactcode = dt1.Rows[0]["sactcode"].ToString();
            ////for (int j = 1; j < dt1.Rows.Count; j++)
            ////{
            ////    if (dt1.Rows[j]["sactcode"].ToString() == sactcode)
            ////    {
            ////        sactcode = dt1.Rows[j]["sactcode"].ToString();
            ////        dt1.Rows[j]["sactdesc"] = "";
            ////    }

            ////    else
            ////    {
            ////        sactcode = dt1.Rows[j]["sactcode"].ToString();
            ////    }

            ////}

            return dt1;
        }





        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";

            string value = this.RadioButtonList1.SelectedValue.ToString();


            switch (value)
            {
                case "0":


                    this.pnlgvDayWSale.Visible = true;
                    this.pnlinprocess.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.pnlReadyDelivery.Visible = false;

                    this.Panelcustcurdues.Visible = false;
                    this.Panelcustduesover.Visible = false;
                    this.Panelcustdues.Visible = false;
                    this.PanelDelivery.Visible = false;
                    this.salesStatus();

                    this.RadioButtonList1.Items[0].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";
                    //this.RadioButtonList1.Items[0].Attributes.Add("class","lblactive");
                    //("class", "hidden");
                    // this.RadioButtonList1.Items[0].Attributes.CssStyle.ToString() = "lblactive";
                    //this.RadioButtonList1.Items[0].Attributes["style"] = "background-color:#13A6A8; font-size:16px; -webkit-border-radius: 10px; -moz-border-radius: 10px; border-radius: 10px;  width:30px;";   

                    break;

                case "1":
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = true;
                    this.PanelApproved.Visible = false;
                    this.pnlReadyDelivery.Visible = false;
                    this.Panelcustcurdues.Visible = false;
                    this.Panelcustduesover.Visible = false;
                    this.Panelcustdues.Visible = false;
                    this.PanelDelivery.Visible = false;
                    this.moneyRecipt();

                    this.RadioButtonList1.Items[1].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    //this.RadioButtonList1.Items[1].Attributes.Add("class", "lblactive");

                    // this.RadioButtonList1.Items[1].Attributes["style"] = "background-color:blue;";      
                    break;
                case "2":
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = false;
                    this.PanelApproved.Visible = true;
                    this.pnlReadyDelivery.Visible = false;
                    this.Panelcustcurdues.Visible = false;
                    this.Panelcustduesover.Visible = false;
                    this.Panelcustdues.Visible = false;
                    this.PanelDelivery.Visible = false;
                    this.Unsoldunit();
                    this.RadioButtonList1.Items[2].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";


                    break;

                case "3":
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.pnlReadyDelivery.Visible = false;
                    this.Panelcustcurdues.Visible = true;
                    this.Panelcustduesover.Visible = false;
                    this.Panelcustdues.Visible = false;
                    this.PanelDelivery.Visible = false;
                    this.ShowCurDues();
                    this.RadioButtonList1.Items[3].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;
                case "4":
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.pnlReadyDelivery.Visible = false;
                    this.Panelcustcurdues.Visible = false;
                    this.Panelcustdues.Visible = false;
                    this.Panelcustduesover.Visible = true;
                    this.PanelDelivery.Visible = false;
                    this.ShowOverDues();
                    this.RadioButtonList1.Items[4].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;
                case "5":
                    this.pnlgvDayWSale.Visible = false;
                    this.pnlinprocess.Visible = false;
                    this.PanelApproved.Visible = false;
                    this.pnlReadyDelivery.Visible = false;
                    this.Panelcustcurdues.Visible = false;
                    this.Panelcustduesover.Visible = false;
                    this.Panelcustdues.Visible = true;
                    this.PanelDelivery.Visible = false;
                    this.ShowDuesAndOverDues();
                    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    break;
                    //case "6":
                    //    this.pnlgvDayWSale.Visible = false;
                    //    this.pnlinprocess.Visible = false;
                    //    this.PanelApproved.Visible = false;
                    //    this.pnlReadyDelivery.Visible = false;
                    //    this.Panelcustcurdues.Visible = false;
                    //    this.Panelcustduesover.Visible = false;
                    //    this.Panelcustdues.Visible = true;
                    //    this.PanelDelivery.Visible = false;
                    //    this.ShowDuesAndOverDues ();
                    //    this.RadioButtonList1.Items[5].Attributes["style"] = "background: #5A5C59; display:block; -webkit-border-radius: 5px;-moz-border-radius: 5px;border-radius: 5px;";

                    //    break;




            }
        }

        private void ShowDuesAndOverDues()
        {

            try
            {
                string comcod = this.GetCompCode();
                string frmdate = Convert.ToDateTime("01" + this.txtdate.Text.Substring(2)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                string ProjectCode = "18%";

                DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGTDUESAOVERDUES", "RPTDUESAOVERDUES", ProjectCode, frmdate, todate, "", "", "", "", "", "", "");
                if (ds2 == null)
                {
                    this.gvcustdues.DataSource = null;
                    this.gvcustdues.DataBind();
                    return;
                }

                Session["tblCustDues"] = this.HiddenSameDataDuesAOveDues(ds2.Tables[0]);
                this.Data_Bind("gvcustdues", (DataTable)Session["tblCustDues"]);
            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);


            }


        }




        private void salesStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = "%";
            string frdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = "12";
            string steam = "%";

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", PactCode, frdate, todate, mRptGroup, steam, "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }



            //this.gvDayWSale.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            // this.gvDayWSale.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            //this.gvDayWSale.DataSource = (DataTable)Session["tblData"];
            //this.gvDayWSale.DataBind();

            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.Sort = ("pactcode");
            this.Data_Bind("gvDayWSale", dv.ToTable());




        }



        private string companytype()
        {
            string comcod = this.GetCompCode();

            string coltype = "";
            switch (comcod)
            {


                case "3305":// RHEL
                case "3311":// RHEL(chittagong)
                case "3306":// Ratul
                case "2305":// Land

                    coltype = "TRANSACTIONSTATEMENT1";
                    break;

                default:
                    coltype = "TRANSACTION_STATEMENT2";
                    break;
            }
            return coltype;
        }

        private void moneyRecipt()
        {
            Session.Remove("DailyTrns");
            string comcod = this.GetCompCode();
            string frdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");

            string pactcode = "%%";//(this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string actual = "";//(this.rbtnList1.SelectedIndex == 2) ? "Actualdate" : "";

            string coltype = this.companytype();

            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", coltype, frdate, todate, pactcode, actual, "", "", "", "", "");
            if (ds1 == null)
            {
                this.grvTrnDatWise.DataSource = null;
                this.grvTrnDatWise.DataBind();
                return;
            }
            Session["DailyTrns"] = (this.rbtnList1.SelectedIndex == 0) ? HiddenSameData(ds1.Tables[0]) : (this.rbtnList1.SelectedIndex == 2) ? HiddenSameData(ds1.Tables[0]) : CollectCurDate(HiddenSameData(ds1.Tables[0]));



            DataTable dt = new DataTable();

            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            // dv.Sort = ("pactcode");
            this.Data_Bind("grvTrnDatWise", dv.ToTable());




        }
        private DataTable CollectCurDate(DataTable dt)
        {
            string frdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");



            DataView dv = dt.DefaultView;
            dv.RowFilter = ("chqdate >= '" + frdate + "' and chqdate<= '" + todate + "'");
            dt = dv.ToTable();
            return dt;

        }
        private void Unsoldunit()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string PactCode = "000000000000";
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string mRptGroup = "12";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTSALSUMMERY", PactCode, date, mRptGroup, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSpayment.DataSource = null;
                this.gvSpayment.DataBind();
                return;
            }


            //// this.gvSpayment.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            ////this.gvSpayment.Columns[1].Visible = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? true : false;
            //Session["tblData"] = HiddenSameData(ds1.Tables[0]);
            //this.gvSpayment.DataSource = (DataTable)Session["tblData"];
            //this.gvSpayment.DataBind();

            DataTable dt = new DataTable();
            DataView dv = new DataView();
            dt = ((DataTable)ds1.Tables[0]).Copy();
            dv = dt.DefaultView;
            dv.RowFilter = ("usqty>=1");
            this.Data_Bind("gvSpayment", dv.ToTable());


            Session["gvSpayment"] = dt;

            // this.FooterCalculation();
        }


        private void ShowCurDues()
        {
            Session.Remove("tblbCustDues");
            Session.Remove("tblCustDues");
            string comcod = this.GetCompCode();

            string frmdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            string ProjectCode = "18%";
            string curdues = "current";
            string overdues = "";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTDUES", ProjectCode, frmdate, todate, curdues, overdues, "", "", "", "");
            if (ds2 == null)
            {
                this.gvcustduescur.DataSource = null;
                this.gvcustduescur.DataBind();
                return;
            }


            DataTable dt = this.HiddenSameDatacurmon(ds2.Tables[0]);
            Session["tblbCustDues"] = dt;
            Session["tblCustDues"] = dt;
            this.Data_Bind("gvcustduescur", (DataTable)Session["tblCustDues"]);


        }
        protected void lnkbtnCurDues_Click(object sender, EventArgs e)
        {
            Session.Remove("tblCustDues");
            DataTable dt = (DataTable)Session["tblbCustDues"];
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text);
            DateTime todate = Convert.ToDateTime(this.txttodate.Text);
            DataView dv = dt.DefaultView;
            if (pactcode == "000000000000")
                dv.RowFilter = ("schdate1>='" + frmdate + "' and schdate1<='" + todate + "'");
            else

                dv.RowFilter = ("pactcode='" + pactcode + "' and  schdate1>='" + frmdate + "' and schdate1<='" + todate + "'");



            Session["tblCustDues"] = dv.ToTable();
            this.Data_Bind("gvcustduescur", (DataTable)Session["tblCustDues"]);
        }

        private void ShowOverDues()
        {
            string comcod = this.GetCompCode();
            //string todate = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            //string frmdate = Convert.ToDateTime("01" + this.txtdate.Text.Substring(2)).ToString("dd-MMM-yyyy");


            string frmdate = "01" + this.txtdate.Text.Trim().Substring(2); //"25-May-2016";
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string ProjectCode = "18%";
            string curdues = "";
            string overdues = "overdues";
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTCUSTDUES", ProjectCode, frmdate, todate, curdues, overdues, "", "", "", "");
            if (ds2 == null)
            {
                this.gvcustduesover.DataSource = null;
                this.gvcustduesover.DataBind();
                return;
            }
            Session["tblCustDues"] = HiddenSameDatacurmon(ds2.Tables[0]);
            this.Data_Bind("gvcustduesover", (DataTable)Session["tblCustDues"]);
        }
        private void ShowmonCOllection()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime("01" + this.txtdate.Text.Substring(2)).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(frmdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string ProjectCode = "18%";

            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_SALSMGTCMONDUESCOLL", "RPTCMONDUESCOLL", ProjectCode, "", frmdate, todate, "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvcustdues.DataSource = null;
                this.gvcustdues.DataBind();
                return;
            }
            Session["tblCustDues"] = this.HiddenSameDataMonCull(ds2.Tables[0]);
            this.Data_Bind("gvcustdues", (DataTable)Session["tblCustDues"]);

            //if (dt1.Rows.Count == 0)
            //    return dt1;
            //string pactcode = dt1.Rows[0]["pactcode"].ToString();
            //string usircode = dt1.Rows[0]["usircode"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
            //    {
            //        pactcode = dt1.Rows[j]["pactcode"].ToString();
            //        usircode = dt1.Rows[j]["usircode"].ToString();
            //        dt1.Rows[j]["pactdesc"] = "";
            //        dt1.Rows[j]["udesc"] = "";
            //        dt1.Rows[j]["custname"] = "";
            //        dt1.Rows[j]["custadd"] = "";
            //        dt1.Rows[j]["cteam"] = "";

            //    }

            //    else
            //    {
            //        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
            //            dt1.Rows[j]["pactdesc"] = "";
            //        else if (dt1.Rows[j]["usircode"].ToString() == usircode)
            //        {
            //            dt1.Rows[j]["udesc"] = "";
            //            dt1.Rows[j]["custname"] = "";
            //            dt1.Rows[j]["custadd"] = "";
            //            dt1.Rows[j]["cteam"] = "";
            //        }


            //        pactcode = dt1.Rows[j]["pactcode"].ToString();
            //        usircode = dt1.Rows[j]["usircode"].ToString();
            //    }

            //}

            //return dt1;

        }



        private DataTable HiddenSameDataDuesAOveDues(DataTable dt1)
        {


            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string usircode = dt1.Rows[0]["usircode"].ToString();
            int i = 0;
            foreach (DataRow dr1 in dt1.Rows)
            {

                if (i == 0)
                {

                    i++;
                    continue;

                }
                else if (dr1["pactcode"].ToString() == pactcode && dr1["usircode"].ToString() == usircode)
                {

                    dr1["pactdesc"] = "";
                    dr1["udesc"] = "";
                    dr1["custname"] = "";
                }

                else
                {

                    if (dr1["pactcode"].ToString() == pactcode)
                        dr1["pactdesc"] = "";

                }
                pactcode = dr1["pactcode"].ToString();
                usircode = dr1["usircode"].ToString();

            }

            return dt1;

        }
        private DataTable HiddenSameDatacurmon(DataTable dt1)
        {




            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string usircode = dt1.Rows[0]["usircode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
                {

                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["udesc"] = "";
                    dt1.Rows[j]["custname"] = "";
                    dt1.Rows[j]["custadd"] = "";
                    dt1.Rows[j]["cteam"] = "";

                }




                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";
                    //else if (dt1.Rows[j]["usircode"].ToString() == usircode)
                    //{
                    //    dt1.Rows[j]["udesc"] = "";
                    //    dt1.Rows[j]["custname"] = "";
                    //    dt1.Rows[j]["custadd"] = "";
                    //    dt1.Rows[j]["cteam"] = "";
                    //}


                    //pactcode = dt1.Rows[j]["pactcode"].ToString();
                    //usircode = dt1.Rows[j]["usircode"].ToString();
                }
                pactcode = dt1.Rows[j]["pactcode"].ToString();
                usircode = dt1.Rows[j]["usircode"].ToString();

            }

            return dt1;

        }


        private DataTable HiddenSameDataMonCull(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string usircode = dt1.Rows[0]["usircode"].ToString();
            string gcod = dt1.Rows[0]["gcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {

                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode && dt1.Rows[j]["gcod"].ToString() == gcod)
                {


                    dt1.Rows[j]["cdueamt"] = 0.00;
                }



                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["usircode"].ToString() == usircode)
                {

                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["udesc"] = "";
                    dt1.Rows[j]["custname"] = "";
                    dt1.Rows[j]["custadd"] = "";
                    dt1.Rows[j]["cteam"] = "";

                }




                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";
                    //else if (dt1.Rows[j]["usircode"].ToString() == usircode)
                    //{
                    //    dt1.Rows[j]["udesc"] = "";
                    //    dt1.Rows[j]["custname"] = "";
                    //    dt1.Rows[j]["custadd"] = "";
                    //    dt1.Rows[j]["cteam"] = "";
                    //}


                    //pactcode = dt1.Rows[j]["pactcode"].ToString();
                    //usircode = dt1.Rows[j]["usircode"].ToString();
                }
                pactcode = dt1.Rows[j]["pactcode"].ToString();
                usircode = dt1.Rows[j]["usircode"].ToString();
                gcod = dt1.Rows[j]["gcod"].ToString();

            }





            return dt1;



        }
        private DataTable HiddenSameData(DataTable dt1)
        {




            if (dt1.Rows.Count == 0)
                return dt1;
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

            return dt1;
        }

        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ////if (e.Row.RowType == DataControlRowType.DataRow)
            ////{
            ////    HyperLink hlink1 = (HyperLink)e.Row.FindControl("HyOrderPrint");
            ////    Hashtable hst = (Hashtable)Session["tblLogin"];
            ////    string comcod = hst["comcod"].ToString();
            ////    string centrid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "centrid")).ToString();
            ////    string orderno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

            ////    hlink1.NavigateUrl = "~/F_23_SaM/Print?Type=OrderPrint&comcod=" + comcod + "&centrid=" + centrid + "&orderno=" + orderno;
            ////}
        }
        protected void grvTrnDatWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lgvCuNamemr = (Label)e.Row.FindControl("lgvCuNamemr");
                Label CollFrm = (Label)e.Row.FindControl("lgvCollFrmmr");
                Label Cashamt = (Label)e.Row.FindControl("lgvCaAmtmr");
                Label chqamt = (Label)e.Row.FindControl("lgvChAmtmr");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();

                if (grp == "")
                {
                    return;
                }
                if (grp == "F" || grp == "G")
                {

                    lgvCuNamemr.Font.Bold = true;
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
        protected void gvSpayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //Label CollFrm = (Label)e.Row.FindControl("lgvCollFrm");
                //Label Cashamt = (Label)e.Row.FindControl("lgvCaAmt");
                //Label chqamt = (Label)e.Row.FindControl("lgvChAmt");

                //string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();
                //string mrno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "mrno")).ToString();

                //if (grp == "")
                //{
                //    return;
                //}
                //if (grp == "F" || grp == "G")
                //{

                //    CollFrm.Font.Bold = true;
                //    Cashamt.Font.Bold = true;
                //    chqamt.Font.Bold = true;
                //}

                //if (mrno == "AAAAAAAAA")
                //{
                //    CollFrm.Font.Bold = true;
                //    Cashamt.Font.Bold = true;
                //    chqamt.Font.Bold = true;
                //}
            }
        }
        private void Data_Bind(string gv, DataTable dt)
        {


            switch (gv)
            {
                case "gvDayWSale":



                    this.gvDayWSale.DataSource = HiddenSameData(dt);
                    this.gvDayWSale.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    DataView dv = dt.Copy().DefaultView;
                    dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
                    DataTable dt2 = dv.ToTable();
                    //DataTable dt = (DataTable)Session["tblData"];

                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tuamt)", "")) ?
                                    0 : dt2.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(suamt)", "")) ?
                                    0 : dt2.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(disamt)", "")) ?
                                    0 : dt2.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");


                    break;

                case "grvTrnDatWise":

                    this.grvTrnDatWise.DataSource = HiddenSameData(dt);
                    this.grvTrnDatWise.DataBind();

                    if (dt.Rows.Count == 0)
                        return;

                    DataTable dt4 = dt.Copy();
                    DataView dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("grp='F' and collfrm1='EEEEE' ");
                    dt4 = dv1.ToTable();
                    double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                    double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                    ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                    ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");


                    break;

                case "gvSpayment":

                    this.gvSpayment.DataSource = HiddenSameData(dt);
                    this.gvSpayment.DataBind();

                    if (dt.Rows.Count == 0)
                        return;



                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTsusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(susize)", "")) ?
                                                       0 : dt.Compute("sum(susize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTusize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usize)", "")) ?
                               0 : dt.Compute("sum(usize)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tqty)", "")) ?
                                       0 : dt.Compute("sum(tqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(tuamt)", "")) ?
                                    0 : dt.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFSqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sqty)", "")) ?
                                    0 : dt.Compute("sum(sqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(suamt)", "")) ?
                                    0 : dt.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFUsqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usqty)", "")) ?
                                    0 : dt.Compute("sum(usqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(usuamt)", "")) ?
                                    0 : dt.Compute("sum(usuamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvSpayment.FooterRow.FindControl("lgvFDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(disamt)", "")) ?
                                    0 : dt.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");



                    break;




                case "gvcustduescur":


                    this.gvcustduescur.DataSource = HiddenSameData(dt);
                    this.gvcustduescur.DataBind();
                    if (dt.Rows.Count == 0)
                        return;


                    ((Label)this.gvcustduescur.FooterRow.FindControl("lgvFDueAmtcur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueamt)", "")) ?
                0.00 : dt.Compute("Sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvcustduescur.FooterRow.FindControl("lgvFcurDueAmtcur")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdueamt)", "")) ?
             0.00 : dt.Compute("Sum(cdueamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;



                case "gvcustduesover":


                    this.gvcustduesover.DataSource = HiddenSameData(dt);
                    this.gvcustduesover.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    ((Label)this.gvcustduesover.FooterRow.FindControl("lgvFDueAmtover")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueamt)", "")) ?
                0.00 : dt.Compute("Sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvcustduesover.FooterRow.FindControl("lgvFcurDueAmtover")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdueamt)", "")) ?
             0.00 : dt.Compute("Sum(cdueamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;


                case "gvcustdues":

                    DataTable dt1 = dt.Copy();
                    this.gvcustdues.DataSource = HiddenSameData(dt);
                    this.gvcustdues.DataBind();
                    if (dt1.Rows.Count == 0)
                        return;


                    ((Label)this.gvcustdues.FooterRow.FindControl("lgvFDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(predues)", "")) ?
                        0.00 : dt1.Compute("Sum(predues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvcustdues.FooterRow.FindControl("lgvFcurDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(curdues)", "")) ?
                     0.00 : dt1.Compute("Sum(curdues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvcustdues.FooterRow.FindControl("lgvFreceivable")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(receivable)", "")) ?
            0.00 : dt1.Compute("Sum(receivable)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvcustdues.FooterRow.FindControl("lgvFpaidamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(recamt)", "")) ?
                     0.00 : dt1.Compute("Sum(recamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvcustdues.FooterRow.FindControl("lgvFnetdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(netdues)", "")) ?
                     0.00 : dt1.Compute("Sum(netdues)", ""))).ToString("#,##0;(#,##0); ");




                    break;

            }




        }

        //protected void gvSpayment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["gvSpayment"];        
        //    this.gvSpayment.PageIndex = e.NewPageIndex;
        //    this.Data_Bind("gvSpayment", dt);
        //}


        protected void gvcustdues_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lgvmrno = (Label)e.Row.FindControl("lgvmrno");


                string recndt = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "recndt")).ToString();

                if (recndt == "")
                {
                    return;
                }
                if (recndt.Length > 0)
                {
                    lgvmrno.Attributes["style"] = "color:red;";

                }


            }

        }
        protected void gvcustduescur_RowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Installment = (Label)e.Row.FindControl("lgvInstallmentover");
                Label duesins = (Label)e.Row.FindControl("lgvDuesover");
                Label duesamt = (Label)e.Row.FindControl("lgvDamtover");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "81AAAAA")
                {

                    Installment.Font.Bold = true;
                    duesins.Font.Bold = true;
                    duesamt.Font.Bold = true;
                    Installment.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvcustduesover_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Installment = (Label)e.Row.FindControl("lgvInstallmentover");
                Label duesins = (Label)e.Row.FindControl("lgvDuesover");
                Label duesamt = (Label)e.Row.FindControl("lgvDamtover");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "81AAAAA")
                {

                    Installment.Font.Bold = true;
                    duesins.Font.Bold = true;
                    duesamt.Font.Bold = true;
                    Installment.Style.Add("text-align", "right");


                }

            }
        }

    }
}