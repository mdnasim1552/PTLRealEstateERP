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
using RealERPRDLC;
namespace RealERPWEB.F_22_Sal
{
    public partial class RptBookingDues : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.ViewSelection();
                this.NameChange();
                //this.lblHeader.Text = (this.Request.QueryString["Type"].ToString() == "Receivedlist") ? "Accounts Receivable - 02 Report"
                //    : (this.Request.QueryString["Type"].ToString() == "DuesCollect") ? "Dues Collection Statment Report"
                //    : (this.Request.QueryString["Type"].ToString() == "DuesCollCR") ? "Dues Collection Statment 02 Report"
                //    : (this.Request.QueryString["Type"].ToString() == "yCollectionfc") ? "Yearly Collection Forcasting" : "Dues Collection -Summary";
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length==0?false:(Convert.ToBoolean(dr1[0]["printable"]));

                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled=(Convert.ToBoolean(dr1[0]["printable"]));

                //((Label)this.Master.FindControl("lblTitle")).Text = "Booking Dues";


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
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
        }


        private void ViewSelection()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Receivedlist":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DuesCollect":
                case "DuesCollCR":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "AllProDuesCollect":

                    this.lblProjectname.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "yCollectionfc":
                    this.lblProjectname.Visible = false;
                    this.txtSrcProject.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.ddlProjectName.Visible = false;
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy"); ;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
            }

        }
        private void NameChange()
        {

            string type = this.Request.QueryString["Type"].ToString();
            string comcod = this.GetCompCode();
            switch (type)
            {


                case "DuesCollect":
                case "DuesCollCR":
                    this.dgvAccRec02.Columns[4].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
                    this.dgvAccRec02.Columns[6].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Cost" : "Apartment Cost";
                    this.dgvAccRec02.Columns[7].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
                    break;
                case "AllProDuesCollect":
                    this.dgvAccRec03.Columns[2].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
                    this.dgvAccRec03.Columns[4].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Land Cost" : "Apartment Cost";
                    this.dgvAccRec03.Columns[5].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
                    break;
            }



        }
        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            ds1.Dispose();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Receivedlist":
                    this.PrintReceivedList();
                    break;

                case "DuesCollect":
                    this.printDuesCollection();
                    break;

                case "AllProDuesCollect":
                    this.PrintAllProDuesCollection();
                    break;
                case "DuesCollCR":
                    this.printDuesCollection02();
                    break;
                case "yCollectionfc":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
            }


        }


        private void PrintReceivedList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptReceivedlist();
            TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "( From " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
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
            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptRcvList.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptRcvList;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        private void printDuesCollection()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            //DataTable dt = (DataTable)Session["tblAccRec"];
            //DataTable dt1 = (DataTable)ViewState["tbltosusold"];




            //LocalReport Rpt1 = new LocalReport();
            //var lst = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.RptBookingtDues>();
            //Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptBookingtDues", lst, null, null);
            //Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("Date", "Current  Due " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM-yyyy")));
            //Rpt1.SetParameters(new ReportParameter("txtsoldqty", (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size"));
            //Rpt1.SetParameters(new ReportParameter("txtaptcost", (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price"));
            //Rpt1.SetParameters(new ReportParameter("txtparking", (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking"));
            //Rpt1.SetParameters(new ReportParameter("txtsoldqty", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["unumber"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtunsoldqty", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["unumber"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txttoqty", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["unumber"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtsoldsize", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["usize"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtunsoldsize", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["usize"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txttosize", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["usize"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtsoldrate", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["rate"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtunsoldrate", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["rate"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txttorate", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["rate"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtsoldamt", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["amount"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtunsoldamt", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["amount"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txttoamt", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["amount"]).ToString("#,##0;(#,##0);") : ""));
            //Rpt1.SetParameters(new ReportParameter("txtsoldpercnt", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : ""));
            //Rpt1.SetParameters(new ReportParameter("txtunsoldpercnt", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : ""));
            //Rpt1.SetParameters(new ReportParameter("txttopercnt", (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : ""));

            //Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            //Session["Report1"] = Rpt1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
            //            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";









            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptBookingtDues();
            DataTable dt1 = (DataTable)ViewState["tbltosusold"];
            TextObject rpttxtCompName = rptRcvList.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rpttxtCompName.Text = comnam;



            TextObject txtsize = rptRcvList.ReportDefinition.ReportObjects["txtsize"] as TextObject;
            txtsize.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            TextObject txtaptcost = rptRcvList.ReportDefinition.ReportObjects["txtaptcost"] as TextObject;
            txtaptcost.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            TextObject txtparking = rptRcvList.ReportDefinition.ReportObjects["txtparking"] as TextObject;
            txtparking.Text = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
            TextObject rptdate = rptRcvList.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Monthly Installment Due -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");

            //TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            //rpttxttoduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            //TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            //rpttxtpredues.Text = "Previous Due up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            //TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            //rpttxtcurrentdues.Text = "Current  Due " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM-yyyy");



            TextObject txtsoldqty = rptRcvList.ReportDefinition.ReportObjects["txtsoldqty"] as TextObject;
            txtsoldqty.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldqty = rptRcvList.ReportDefinition.ReportObjects["txtunsoldqty"] as TextObject;
            txtunsoldqty.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttoqty = rptRcvList.ReportDefinition.ReportObjects["txttoqty"] as TextObject;
            txttoqty.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";


            TextObject txtsoldsize = rptRcvList.ReportDefinition.ReportObjects["txtsoldsize"] as TextObject;
            txtsoldsize.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldsize = rptRcvList.ReportDefinition.ReportObjects["txtunsoldsize"] as TextObject;
            txtunsoldsize.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttosize = rptRcvList.ReportDefinition.ReportObjects["txttosize"] as TextObject;
            txttosize.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtsoldrate = rptRcvList.ReportDefinition.ReportObjects["txtsoldrate"] as TextObject;
            txtsoldrate.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldrate = rptRcvList.ReportDefinition.ReportObjects["txtunsoldrate"] as TextObject;
            txtunsoldrate.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttorate = rptRcvList.ReportDefinition.ReportObjects["txttorate"] as TextObject;
            txttorate.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtsoldamt = rptRcvList.ReportDefinition.ReportObjects["txtsoldamt"] as TextObject;
            txtsoldamt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtunsoldamt = rptRcvList.ReportDefinition.ReportObjects["txtunsoldamt"] as TextObject;
            txtunsoldamt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            TextObject txttoamt = rptRcvList.ReportDefinition.ReportObjects["txttoamt"] as TextObject;
            txttoamt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            TextObject txtsoldpercnt = rptRcvList.ReportDefinition.ReportObjects["txtsoldpercnt"] as TextObject;
            txtsoldpercnt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";

            TextObject txtunsoldpercnt = rptRcvList.ReportDefinition.ReportObjects["txtunsoldpercnt"] as TextObject;
            txtunsoldpercnt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";

            TextObject txttopercnt = rptRcvList.ReportDefinition.ReportObjects["txttopercnt"] as TextObject;
            txttopercnt.Text = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";





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
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";




        }




        private void printDuesCollection02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)this.HiddenSameData((DataTable)Session["tblAccRec"]);
            string rptdate = "Monthly Installment Due -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");

            string rpttxtpredues = "Previous Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            string rpttxtcurrentdues = "Current  Due " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM-yyyy");
            string rptAdvdate = "Receivable Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassDuesCollection02>();
            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptDuesCollection02", lst, null, null);


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", rptdate));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Rpt1.SetParameters(new ReportParameter("rpttxtpredues", rpttxtpredues));
            Rpt1.SetParameters(new ReportParameter("rpttxtcurrentdues", rpttxtcurrentdues));
            Rpt1.SetParameters(new ReportParameter("rptAdvdate", rptAdvdate));

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

            string txtsize = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
            string txtaptcost = (ASTUtility.Left(comcod, 1) == "2") ? "Land Price" : "Apartment Price";
            string txtparking = (ASTUtility.Left(comcod, 1) == "2") ? "Development Cost" : "Car Parking";
            string rptdate = "Monthly Dues Collection Summary -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            string rpttxttoduesupto = "Receivable Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            string rpttxtpredues = "Previous Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            string rpttxtcurrentdues = "Current  Dues " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");
            string txtsoldqty = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";

            string txtunsoldqty = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";
            string txttoqty = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["unumber"]).ToString("#,##0;(#,##0);") : "";

            string txtsoldsize = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";

            string txtunsoldsize = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";
            string txttosize = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["usize"]).ToString("#,##0;(#,##0);") : "";
            string txtsoldrate = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";
            string txtunsoldrate = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";
            string txttorate = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["rate"]).ToString("#,##0;(#,##0);") : "";
            string txtsoldamt = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            string txtunsoldamt = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            string txttoamt = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["amount"]).ToString("#,##0;(#,##0);") : "";

            string txtsoldpercnt = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='01'").Length > 0 ? Convert.ToDouble(dt1.Select("code='01'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";

            string txtunsoldpercnt = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='02'").Length > 0 ? Convert.ToDouble(dt1.Select("code='02'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";
            string txttopercnt = (dt1.Rows.Count == 0) ? "" : dt1.Select("code='03'").Length > 0 ? Convert.ToDouble(dt1.Select("code='03'")[0]["percnt"]).ToString("#,##0.00;(#,##0.00);") + "%" : "";

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassDuesCollection>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptAllProDuesCollection", lst, null, null);


            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("date", rptdate));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("txtsize", txtsize));
            Rpt1.SetParameters(new ReportParameter("txtaptcost", txtaptcost));
            Rpt1.SetParameters(new ReportParameter("txtparking", txtparking));
            Rpt1.SetParameters(new ReportParameter("rpttxttoduesupto", rpttxttoduesupto));
            Rpt1.SetParameters(new ReportParameter("rpttxtpredues", rpttxtpredues));
            Rpt1.SetParameters(new ReportParameter("rpttxtcurrentdues", rpttxtcurrentdues));
            Rpt1.SetParameters(new ReportParameter("txtsoldqty", txtsoldqty));

            Rpt1.SetParameters(new ReportParameter("txtunsoldqty", txtunsoldqty));
            Rpt1.SetParameters(new ReportParameter("txttoqty", txttoqty));
            Rpt1.SetParameters(new ReportParameter("txtsoldsize", txtsoldsize));
            Rpt1.SetParameters(new ReportParameter("txtunsoldsize", txtunsoldsize));
            Rpt1.SetParameters(new ReportParameter("txttosize", txttosize));
            Rpt1.SetParameters(new ReportParameter("txtsoldrate", txtsoldrate));
            Rpt1.SetParameters(new ReportParameter("txtunsoldrate", txtunsoldrate));
            Rpt1.SetParameters(new ReportParameter("txttorate", txttorate));
            Rpt1.SetParameters(new ReportParameter("txtsoldamt", txtsoldamt));
            Rpt1.SetParameters(new ReportParameter("txtunsoldamt", txtunsoldamt));
            Rpt1.SetParameters(new ReportParameter("txttoamt", txttoamt));
            Rpt1.SetParameters(new ReportParameter("txtsoldpercnt", txtsoldpercnt));
            Rpt1.SetParameters(new ReportParameter("txtunsoldpercnt", txtunsoldpercnt));
            Rpt1.SetParameters(new ReportParameter("txttopercnt", txttopercnt));

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Received List Info";
                string eventdesc = "Print Report MR";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {


            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Receivedlist":
                    this.ShowReceivedList();
                    break;

                case "DuesCollect":
                case "DuesCollCR":
                    //this.pnlIndPro.Visible = true;
                    this.ShowDuesCollection();
                    break;

                case "AllProDuesCollect":
                    //this.pnlIndProal.Visible = true;
                    this.AllProDuesCollection();
                    break;
                case "yCollectionfc":
                    this.ShowYCollectionfc();
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
                default:
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
            }


            return dt1;
        }
        private void Data_Bind()
        {
            try
            {
                DataTable dt = (DataTable)Session["tblAccRec"];
                string type = this.Request.QueryString["Type"].ToString();
                switch (type)
                {
                    case "Receivedlist":
                        this.dgvAccRec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec.DataSource = dt;
                        this.dgvAccRec.DataBind();

                        this.FooterCalculation();
                        break;

                    case "DuesCollect":

                        this.dgvAccRec02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec02.DataSource = dt;
                        this.dgvAccRec02.DataBind();
                        this.FooterCalculation();
                        break;


                    case "AllProDuesCollect":
                        this.dgvAccRec03.Columns[17].HeaderText = "Receivable Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
                        this.dgvAccRec03.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec03.DataSource = dt;
                        this.dgvAccRec03.DataBind();
                        this.FooterCalculation();
                        break;
                    case "DuesCollCR":
                        this.dgvAccRec02.Columns[17].HeaderText = "Receivable Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
                        this.dgvAccRec02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec02.DataSource = dt;
                        this.dgvAccRec02.DataBind();
                        this.dgvAccRec02.Columns[4].Visible = false;
                        this.dgvAccRec02.Columns[5].Visible = false;
                        this.dgvAccRec02.Columns[6].Visible = false;
                        this.dgvAccRec02.Columns[7].Visible = false;
                        this.dgvAccRec02.Columns[8].Visible = false;
                        this.dgvAccRec02.Columns[9].Visible = false;
                        this.FooterCalculation();
                        break;

                    case "yCollectionfc":
                        double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
                        amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam1)", "")) ? 0.00 : dt.Compute("sum(dueam1)", "")));
                        amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam2)", "")) ? 0.00 : dt.Compute("sum(dueam2)", "")));
                        amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam3)", "")) ? 0.00 : dt.Compute("sum(dueam3)", "")));
                        amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam4)", "")) ? 0.00 : dt.Compute("sum(dueam4)", "")));
                        amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam5)", "")) ? 0.00 : dt.Compute("sum(dueam5)", "")));
                        amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam6)", "")) ? 0.00 : dt.Compute("sum(dueam6)", "")));
                        amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam7)", "")) ? 0.00 : dt.Compute("sum(dueam7)", "")));
                        amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam8)", "")) ? 0.00 : dt.Compute("sum(dueam8)", "")));
                        amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam9)", "")) ? 0.00 : dt.Compute("sum(dueam9)", "")));
                        amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam10)", "")) ? 0.00 : dt.Compute("sum(dueam10)", "")));
                        amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam11)", "")) ? 0.00 : dt.Compute("sum(dueam11)", "")));
                        amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dueam12)", "")) ? 0.00 : dt.Compute("sum(dueam12)", "")));


                        this.gvyCollection.Columns[6].Visible = (amt1 != 0);
                        this.gvyCollection.Columns[7].Visible = (amt2 != 0);
                        this.gvyCollection.Columns[8].Visible = (amt3 != 0);
                        this.gvyCollection.Columns[9].Visible = (amt4 != 0);
                        this.gvyCollection.Columns[10].Visible = (amt5 != 0);
                        this.gvyCollection.Columns[11].Visible = (amt6 != 0);
                        this.gvyCollection.Columns[12].Visible = (amt7 != 0);
                        this.gvyCollection.Columns[13].Visible = (amt8 != 0);
                        this.gvyCollection.Columns[14].Visible = (amt9 != 0);
                        this.gvyCollection.Columns[15].Visible = (amt10 != 0);
                        this.gvyCollection.Columns[16].Visible = (amt11 != 0);
                        this.gvyCollection.Columns[17].Visible = (amt12 != 0);

                        this.gvyCollection.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                        DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());
                        for (int i = 6; i < 18; i++)
                        {
                            if (frmdate > todate)
                                break;

                            this.gvyCollection.Columns[i].HeaderText = frmdate.ToString("MMM yy");
                            frmdate = frmdate.AddMonths(1);

                        }


                        this.gvyCollection.DataSource = dt;
                        this.gvyCollection.DataBind();
                        this.FooterCalculation();
                        break;
                }





            }

            catch (Exception e)
            {
            }



        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblAccRec"];
            if (dt.Rows.Count == 0)
                return;
            string pactcode = "";
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Receivedlist":
                    ((Label)this.dgvAccRec.FooterRow.FindControl("lgvAcAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(uamt)", "")) ?
                         0.00 : dt.Compute("Sum(uamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec.FooterRow.FindControl("lgvRecAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                        0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec.FooterRow.FindControl("lgvBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                        0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec.FooterRow.FindControl("lgvFDueAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueamt)", "")) ?
                        0.00 : dt.Compute("Sum(dueamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec.FooterRow.FindControl("lgvFcurinsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(procolam)", "")) ?
                        0.00 : dt.Compute("Sum(procolam)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "DuesCollect":
                case "DuesCollCR":

                    double usize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ? 0.00 : dt.Compute("Sum(usize)", "")));
                    double aptccost = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aptcost)", "")) ? 0.00 : dt.Compute("Sum(aptcost)", "")));
                    double avgrate = (usize == 0) ? 0.00 : (aptccost / usize);
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFunitsize")).Text = usize.ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFavgrate")).Text = avgrate.ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFaptcost")).Text = aptccost.ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFcpcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cpcost)", "")) ?
                        0.00 : dt.Compute("Sum(cpcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFutilitycost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utltycost)", "")) ?
                        0.00 : dt.Compute("Sum(utltycost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFothcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(othcost)", "")) ?
                    0.00 : dt.Compute("Sum(othcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtocost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                        0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");



                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgFEncash")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                        0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtretamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(retcheque)", "")) ?
                        0.00 : dt.Compute("Sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtframt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(fcheque)", "")) ?
                        0.00 : dt.Compute("Sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtpdamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pcheque)", "")) ?
                        0.00 : dt.Compute("Sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((HyperLink)this.dgvAccRec02.FooterRow.FindControl("hlnkgvFtoreceived")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                        0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pbookam)", "")) ?
                        0.00 : dt.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFCbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ?
                        0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtbdues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tbdues)", "")) ?
                    0.00 : dt.Compute("Sum(tbdues)", ""))).ToString("#,##0;(#,##0); ");



                    pactcode = (this.ddlProjectName.SelectedValue == "000000000000") ? ""
                        : (this.ddlProjectName.SelectedValue.Substring(0, 2) == "18") ? "41" + this.ddlProjectName.SelectedValue.Substring(2)
                        : "47" + this.ddlProjectName.SelectedValue.Substring(2);


                    ((HyperLink)this.dgvAccRec02.FooterRow.FindControl("hlnkgvFtoreceived")).NavigateUrl = "~/F_32_Mis/RptProjectCollBrkDown.aspx?Type=PrjCol&pactcode=" + pactcode + "&Date1=" + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                    break;
                case "AllProDuesCollect":

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFaptcostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(aptcost)", "")) ?
                    0.00 : dt.Compute("Sum(aptcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFcpcostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cpcost)", "")) ?
                   0.00 : dt.Compute("Sum(cpcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFutilitycostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(utltycost)", "")) ?
                   0.00 : dt.Compute("Sum(utltycost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFothcostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(othcost)", "")) ?
               0.00 : dt.Compute("Sum(othcost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtocostal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                   0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFatoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
                    0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtotalduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todues)", "")) ?
                   0.00 : dt.Compute("Sum(todues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgFEncashal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                   0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtretamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(retcheque)", "")) ?
                   0.00 : dt.Compute("Sum(retcheque)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtframtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(fcheque)", "")) ?
                   0.00 : dt.Compute("Sum(fcheque)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtpdamtal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pcheque)", "")) ?
                   0.00 : dt.Compute("Sum(pcheque)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoreceivedal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                    0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                   0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");


                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pbookam)", "")) ?
                   0.00 : dt.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pinsam)", "")) ?
               0.00 : dt.Compute("Sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFpretoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ptodues)", "")) ?
                   0.00 : dt.Compute("Sum(ptodues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCbookingal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ?
                   0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFCinstallmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cinsam)", "")) ?
                   0.00 : dt.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFtoCInstalmental")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ctodues)", "")) ?
               0.00 : dt.Compute("Sum(ctodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFvtoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(vtodues)", "")) ?
               0.00 : dt.Compute("Sum(vtodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdelchargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                 0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFdischargeal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
                    0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec03.FooterRow.FindControl("lgvFnettoduesal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
                  0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");


                    break;


                case "yCollectionfc":
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFtoBgdCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdcost)", "")) ?
                        0.00 : dt.Compute("Sum(bgdcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFtsoldcalue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                         0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFtoreceivedyc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                         0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFpduesyc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pdueam)", "")) ?
                         0.00 : dt.Compute("Sum(pdueam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam1)", "")) ?
                         0.00 : dt.Compute("Sum(dueam1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam2)", "")) ?
                            0.00 : dt.Compute("Sum(dueam2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam3)", "")) ?
                            0.00 : dt.Compute("Sum(dueam3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam4)", "")) ?
                            0.00 : dt.Compute("Sum(dueam4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam5)", "")) ?
                            0.00 : dt.Compute("Sum(dueam5)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam6)", "")) ?
                            0.00 : dt.Compute("Sum(dueam6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam7)", "")) ?
                            0.00 : dt.Compute("Sum(dueam7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam8)", "")) ?
                            0.00 : dt.Compute("Sum(dueam8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam9)", "")) ?
                            0.00 : dt.Compute("Sum(dueam9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam10)", "")) ?
                            0.00 : dt.Compute("Sum(dueam10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam11)", "")) ?
                            0.00 : dt.Compute("Sum(dueam11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFdueam12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam12)", "")) ?
                            0.00 : dt.Compute("Sum(dueam12)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFtdueam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todueam)", "")) ?
                            0.00 : dt.Compute("Sum(todueam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvyCollection.FooterRow.FindControl("lgvFgtdueam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gtodueam)", "")) ?
                            0.00 : dt.Compute("Sum(gtodueam)", ""))).ToString("#,##0;(#,##0); ");





                    break;
            }







        }



        private void ShowReceivedList()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "18" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(dueamt between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( dueamt " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "DATEWISERECEIVEDLISTSP", ProjectCode, frmdate, todate, searchinfo, "", "", "", "", "");

            if (ds2 == null)
            {
                this.dgvAccRec.DataSource = null;
                this.dgvAccRec.DataBind();
                return;
            }
            Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();



        }


        private void ShowDuesCollection()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string ProjectCode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "1[38]" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(tbdues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( tbdues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGTBDUES", "RPTDATEWISEPROINSDUES", ProjectCode, frmdate, todate, searchinfo, "", "", "", "", "");

            if (ds2 == null)
            {
                this.dgvAccRec02.DataSource = null;
                this.dgvAccRec02.DataBind();
                return;
            }
            Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            ViewState["tbltosusold"] = ds2.Tables[1];
            this.gvinpro.DataSource = ds2.Tables[1];
            this.gvinpro.DataBind();
            this.Data_Bind();








        }


        private void AllProDuesCollection()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(ctodues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( ctodues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWALLPROINSDUES", "", frmdate, todate, searchinfo, "", "", "", "", "");

            if (ds2 == null)
            {
                this.dgvAccRec03.DataSource = null;
                this.dgvAccRec03.DataBind();
                return;
            }
            Session["tblAccRec"] = HiddenSameData(ds2.Tables[0]);
            ViewState["tbltosusold"] = ds2.Tables[1];
            this.gvinproal.DataSource = ds2.Tables[1];
            this.gvinproal.DataBind();
            this.Data_Bind();


            //



        }

        private void ShowYCollectionfc()
        {



            Session.Remove("tblAccRec");
            string comcod = this.GetCompCode();
            //((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
            if (mon > 12)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                return;
            }



            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string searchinfo = "";
            if (this.ddlSrchCash.SelectedValue != "")
            {

                if (this.ddlSrchCash.SelectedValue == "between")
                {
                    searchinfo = "(dueamt between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

                }

                else
                {
                    searchinfo = "( dueamt " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

                }
            }


            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTYCOLLECTFORCASTING", "", frmdate, todate, searchinfo, "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvyCollection.DataSource = null;
                this.gvyCollection.DataBind();
                return;
            }
            Session["tblAccRec"] = ds2.Tables[0];
            this.Data_Bind();






        }


        protected void dgvAccRec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAccRec.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }




        protected void ddlSrchCash_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblToCash.Visible = (this.ddlSrchCash.SelectedValue == "between");
            this.txtAmountC2.Visible = (this.ddlSrchCash.SelectedValue == "between");
        }
        protected void dgvAccRec02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec02.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void dgvAccRec03_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.dgvAccRec03.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvyCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvyCollection.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

    }
}











