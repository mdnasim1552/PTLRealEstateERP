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
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{

    public partial class RptReceivedList02 : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");



                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                string comcod =GetCompCode();
                if (comcod == "3348")
                {
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    DateTime nowDate = DateTime.Now;
                    DateTime yearfday = new DateTime(nowDate.Year, 1, 1);
                    DateTime date1 = new DateTime(nowDate.Year, 12, 31);
                    this.txtfrmdate.Text = yearfday.ToString("dd-MMM-yyyy"); 
                    this.txttodate.Text = date1.ToString("dd-MMM-yyyy");
                }
                else
                {
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    string date1 = "01-" + ASTUtility.Right(date, 8);
                    this.txtfrmdate.Text = date1;
                    this.txttodate.Text = Convert.ToDateTime(date1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                }
                string qprjcode = this.Request.QueryString["prjcode"] ?? "";
                string fdate = this.Request.QueryString["Date1"] ?? "";
                string tdate = this.Request.QueryString["Date2"] ?? "";

                this.GetProjectName();
                this.ViewSelection();
                this.NameChange();
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString() == "Receivedlist") ? "Accounts Receivable - 02 Report"
                //  : (this.Request.QueryString["Type"].ToString() == "DuesCollect") ? "Dues Collection Statment Report"
                //  : (this.Request.QueryString["Type"].ToString() == "DuesCollCR") ? "Dues Collection Statment 02 Report"
                //  : (this.Request.QueryString["Type"].ToString() == "yCollectionfc") ? "Yearly Collection Forcasting"
                //  : (this.Request.QueryString["Type"].ToString() == "CurDues") ? "Current Dues"
                //  : (this.Request.QueryString["Type"].ToString() == "ProClientst") ? "Project Wise Client Status" : "Dues Collection -Summary";
                if(qprjcode.Length > 0)
                {
                    this.txtfrmdate.Text = fdate;
                    this.txttodate.Text = tdate;
                    this.lbtnOk_Click(null, null);
                }

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
        private void ViewSelection()
        {
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Receivedlist":
                    this.salestatus.Attributes.Add("class", "d-none col-md-2");
                    this.Paydate.Attributes.Add("class", "mt-3 col-md-2");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DuesCollect":
                case "DuesCollCR":
                case "CurDues":
                    this.salestatus.Attributes.Add("class", "d-none col-md-2");
                    this.Paydate.Attributes.Add("class", "mt-3 col-md-2");
                    this.chkPayDateWise.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "AllProDuesCollect":
                    this.salestatus.Attributes.Add("class", "d-none col-md-2");
                    this.Paydate.Attributes.Add("class", "mt-3 col-md-2");
                    this.prjname.Attributes.Add("class", "d-none col-md-2");
                    this.chkPayDateWise.Visible = true;
                    this.lblProjectname.Visible = false;
                    //this.txtSrcProject.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.DropCheck1.Visible = false;
                    //this.pnlfilter.Visible = false;


                    this.MultiView1.ActiveViewIndex = 2;
                    break;

                case "yCollectionfc":
                    this.prjname.Attributes.Add("class", "d-none col-md-2");
                    this.salestatus.Attributes.Add("class", "d-none col-md-2");
                    this.lblProjectname.Visible = false;
                    //this.txtSrcProject.Visible = false;
                    this.imgbtnFindProject.Visible = false;
                    this.DropCheck1.Visible = false;
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text).AddMonths(12).AddDays(-1).ToString("dd-MMM-yyyy"); ;
                    this.MultiView1.ActiveViewIndex = 3;
                    break;
                case "ProClientst":
                    this.salestatus.Attributes.Add("class", "d-none col-md-2");
                    this.Paydate.Attributes.Add("class", "mt-3 col-md-2");
                    this.MultiView1.ActiveViewIndex = 4;
                    break;
                case "yCollectionDetails":
                    //this.prjname.Attributes.Add("class", "d-none col-md-2");
                    this.MultiView1.ActiveViewIndex = 5;
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
                    this.dgvAccRec02.Columns[5].HeaderText = (ASTUtility.Left(comcod, 1) == "2") ? "Plot Size" : "Flat Size";
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
            string txtSProject = (this.Request.QueryString["prjcode"].ToString().Trim() == "") ? ("%" + this.DropCheck1.SelectedValue + "%") : (this.Request.QueryString["prjcode"].ToString() + "%");

            if (Request.QueryString["Type"].ToString() == "DuesCollectInd")
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string usrid = hst["usrid"].ToString();
                // string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
                DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GET_PURPROJECTNAME", txtSProject, usrid, "", "", "", "", "", "", "");
                this.DropCheck1.DataTextField = "pactdesc";
                this.DropCheck1.DataValueField = "pactcode";
                this.DropCheck1.DataSource = ds1.Tables[0];
                this.DropCheck1.DataBind();
                ds1.Dispose();


            }

            else
            {
                string comcod = this.GetCompCode();
                //string txtSProject = "%" + this.txtSrcProject.Text.Trim() + "%";
                DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");

                this.DropCheck1.DataTextField = "pactdesc";
                this.DropCheck1.DataValueField = "pactcode";
                this.DropCheck1.DataSource = ds1.Tables[0];
                this.DropCheck1.DataBind();

                //this.DropCheck1.DataTextField = "pactdesc";
                //this.DropCheck1.DataValueField = "pactcode";
                //this.DropCheck1.DataSource = ds1.Tables[0];
                //this.DropCheck1.DataBind();
                //ds1.Dispose();

            }






            //string comcod = this.GetCompCode();



            //DataSet ds1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            //this.ddlProjectName.DataTextField = "pactdesc";
            //this.ddlProjectName.DataValueField = "pactcode";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();
            //ds1.Dispose();
            if (this.Request.QueryString["prjcode"].ToString().Trim().Length > 0)
                this.DropCheck1.SelectedValue = this.Request.QueryString["prjcode"].ToString().Trim();

        }
        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();

        }


        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            // Iqbal Nayan
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString();
            switch (type)
            {
                case "Receivedlist":
                    this.PrintReceivedList();
                    break;

                case "DuesCollect":
                case "CurDues":
                    switch (comcod)
                    {
                        case "3336":
                       // case "3101":
                        case "3344":
                        case "3347":
                            this.PrintDuesTerranova();
                            break;
                        default:
                            this.printDuesCollection();
                            break;
                    }
                    break;

                case "AllProDuesCollect":
                    this.PrintAllProDuesCollection();
                    break;
                case "DuesCollCR":
                    this.printDuesCollection02();
                    break;
                case "yCollectionfc":
                    this.PrintYearlyCollection();

                    break;

                case "ProClientst":
                    this.PrintProWiseClientSt();
                    break;
                case "yCollectionDetails":
                    this.PrintYearlyCollectionDetails();

                    break;
            }
        }


        private void PrintDuesTerranova()
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
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string Ftdate = "( Form " + this.txtfrmdate.Text + " To " + this.txttodate.Text + " )";

            string InsDate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yy");

            DataTable dt = (DataTable)Session["tblAccRec"];

            //var lst = dt.DataTableToList< C_23_CRR.EClassSales_03.DueCollStatmentRe>();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.DueCollStatmentRe>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptDuesCollStatsTerranova", lst, null, null);

            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("InsDate", InsDate));

            // string name = this.DropCheck1.Text;

            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ProjectNam", this.DropCheck1.Text));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Dues Collection Statment Report"));
            Rpt1.SetParameters(new ReportParameter("FTdate", Ftdate));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        private void PrintReceivedList()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string date = "( From " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);


            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = this.HiddenSameData((DataTable)Session["tblAccRec"]);
            var lst = dt.DataTableToList<RealEntity.C_22_Sal.Sales_BO.AccountsReceivable2>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptReceivedList", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Account Receivable - 02 "));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Received List Info";
                string eventdesc = "Print Report MR";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



        private void printDuesCollection()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptRcvList = new RealERPRPT.R_22_Sal.RptDuesCollection();
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



            TextObject txtcollectableupto = rptRcvList.ReportDefinition.ReportObjects["txtcollectableupto"] as TextObject;
            txtcollectableupto.Text = "Collectable dues & Overdues up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM yy");

            TextObject txtcollectabledueupto = rptRcvList.ReportDefinition.ReportObjects["txtcollectabledueupto"] as TextObject;
            txtcollectabledueupto.Text = "Colletable Due Amount  Details for " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM yy");


            TextObject rpttxttoduesupto = rptRcvList.ReportDefinition.ReportObjects["txttoduesupto"] as TextObject;
            rpttxttoduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMM-yyyy");
            TextObject rpttxtpredues = rptRcvList.ReportDefinition.ReportObjects["txtpredues"] as TextObject;
            rpttxtpredues.Text = "Previous Due up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            TextObject rpttxtcurrentdues = rptRcvList.ReportDefinition.ReportObjects["txtcurrentdues"] as TextObject;
            rpttxtcurrentdues.Text = "Current  Due " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM-yyyy");



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
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string addate = Convert.ToDateTime(this.txttodate.Text).AddMonths(1).ToString("MMMM-yyyy");

            DataTable dt2 = (DataTable)this.HiddenSameData((DataTable)Session["tblAccRec"]);



            string rptdate = "Monthly Installment Due -  " + Convert.ToDateTime(this.txtfrmdate.Text).ToString("MMMM-yyyy");

            string rpttxtpredues = "Previous Dues up to " + Convert.ToDateTime(this.txtfrmdate.Text).AddDays(-1).ToString("MMM-yyyy");
            string rpttxtcurrentdues = "Current  Due " + Convert.ToDateTime(this.txttodate.Text).ToString("MMMM-yyyy");
            string rptAdvdate = "Advance Up to - " + addate + " - 31.12.2018";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);
            var lst = dt2.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassDuesCollection02>();
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
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tbltosusold"];

            //Rdlc Report
            DataTable dt2 = (DataTable)this.HiddenSameData((DataTable)Session["tblAccRec"]);



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
            var lst = dt2.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.EClassDuesCollection>();

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

        private void PrintProWiseClientSt()
        {
            DataTable dt = (DataTable)this.HiddenSameData((DataTable)Session["tblAccRec"]);
            string projectName = "";
            string pactcode = this.DropCheck1.SelectedValue.ToString();
            if (pactcode == "000000000000")
            {
                projectName = "All Project";
            }
            else
            {
                projectName = dt.Rows[0]["pactdesc"].ToString();
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string ununise = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(unusize)", "")) ? 0.00 : dt.Compute("Sum(unusize)", ""))).ToString("#,##0;(#,##0); ");
            string sunise = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ? 0.00 : dt.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");
            string tusize = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ? 0.00 : dt.Compute("Sum(tsize)", ""))).ToString("#,##0;(#,##0); ");

            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.ProjectWiseClientStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptProClientSt", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Project Wise Client Status"));
            Rpt1.SetParameters(new ReportParameter("txtProjectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unSoldArea", ununise));
            Rpt1.SetParameters(new ReportParameter("soldArea", sunise));
            Rpt1.SetParameters(new ReportParameter("ttlArea", tusize));
            Rpt1.SetParameters(new ReportParameter("regArea", ""));
            Rpt1.SetParameters(new ReportParameter("unRegArea", ""));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                     ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintYearlyCollectionDetails()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comnam = hst["comnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string compname = hst["compname"].ToString();
                string username = hst["username"].ToString();
                string comcod = hst["comcod"].ToString();
                //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());

                string frmdt = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("MMM yy");
                string todt = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM yy");
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                DataTable dt1 = (DataTable)Session["tblyAccRecde"];             

                string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;
                var lst = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassYearlyColletionDetails>();


                LocalReport Rpt1 = new LocalReport();

                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptYearlyCollectionDetails", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("companyname", comnam));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "YEARLY COLLECTION DETAILS"));
                Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
                Rpt1.SetParameters(new ReportParameter("date", "From(" + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text + ")"));
                Rpt1.SetParameters(new ReportParameter("cdate", "(" + frmdt + "-" + todt + ")"));

                for (int i = 1; i <= 12; i++)
                {
                    if (frmdate > todate)
                        break;

                    Rpt1.SetParameters(new ReportParameter("duemonth" + i.ToString(), frmdate.ToString("MMM yy")));
                    frmdate = frmdate.AddMonths(1);

                }


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                    string eventdesc = "Print Report:";
                    string eventdesc2 = "Yearly Collection Details ";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void PrintYearlyCollection()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DateTime frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
            DateTime todate = Convert.ToDateTime(this.txttodate.Text.Trim());

            string frmdt = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("MMM yy");
            string todt = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM yy");
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = new DataTable();
            if (Request.QueryString["Type"]== "yCollectionDetails")
            {
                 dt1 = (DataTable)Session["tblyAccRecde"];

            }
            else
            {
                 dt1 = (DataTable)Session["tblAccRec"];

            }

            string txtuserinfo = "Print Source " + compname + " ,User: " + username + " ,Time: " + printdate;
            var lst = dt1.DataTableToList<RealEntity.C_23_CRR.EClassSalesStatus.EClassYearlyColletionForcasting>();


            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptYearlyCollectionForecasting", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "YEARLY COLLECTION FORCASTING"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("date", "From(" + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text + ")"));
            Rpt1.SetParameters(new ReportParameter("cdate", "("+frmdt+"-"+todt +")"));

            for (int i = 1; i <= 12; i++)
            {
                if (frmdate > todate)
                    break;

                Rpt1.SetParameters(new ReportParameter("duemonth" + i.ToString(), frmdate.ToString("MMM yy")));
                frmdate = frmdate.AddMonths(1);

            }


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Print Report:";
                string eventdesc2 = "Yearly Collection Forcasting ";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }







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
                case "CurDues":
                    this.pnlIndPro.Visible = true;
                    this.ShowDuesCollection();
                    break;

                case "AllProDuesCollect":
                    this.pnlIndProal.Visible = true;
                    this.AllProDuesCollection();
                    break;
                case "yCollectionfc":
                    this.ShowYCollectionfc();
                    break;

                case "ProClientst":
                    this.ShowProWiseClientSt();
                    break;
                case "yCollectionDetails":
                    this.YCollectionDetails();
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
                DataTable dt1 = (DataTable)Session["tblyAccRecde"];
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
                    case "CurDues":

                        this.dgvAccRec02.Columns[13].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
                        this.dgvAccRec02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.dgvAccRec02.DataSource = dt;
                        this.dgvAccRec02.DataBind();
                        Session["Report1"] = dgvAccRec02;
                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.dgvAccRec02.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        this.FooterCalculation();
                        break;


                    case "AllProDuesCollect":
                        this.dgvAccRec03.Columns[15].HeaderText = "Receivable Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
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
                        this.dgvAccRec02.Columns[5].Visible = false;
                        this.dgvAccRec02.Columns[6].Visible = false;
                        this.dgvAccRec02.Columns[7].Visible = false;
                        this.dgvAccRec02.Columns[8].Visible = false;
                        this.dgvAccRec02.Columns[9].Visible = false;
                        this.dgvAccRec02.Columns[10].Visible = false;

                        Session["Report1"] = dgvAccRec02;
                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.dgvAccRec02.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
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

                        Session["Report1"] = gvyCollection;
                        if (dt.Rows.Count > 0)
                            ((HyperLink)this.gvyCollection.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        this.FooterCalculation();
                        break;

                    case "ProClientst":


                        this.gvProClientst.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gvProClientst.DataSource = dt;
                        this.gvProClientst.DataBind();
                        this.FooterCalculation();
                        break;
                    case "yCollectionDetails":

                        double amt01, amt02, amt03, amt04, amt05, amt06, amt07, amt08, amt09, amt010, amt011, amt012;
                        amt01 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam1)", "")) ? 0.00 : dt1.Compute("sum(dueam1)", "")));
                        amt02 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam2)", "")) ? 0.00 : dt1.Compute("sum(dueam2)", "")));
                        amt03 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam3)", "")) ? 0.00 : dt1.Compute("sum(dueam3)", "")));
                        amt04 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam4)", "")) ? 0.00 : dt1.Compute("sum(dueam4)", "")));
                        amt05 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam5)", "")) ? 0.00 : dt1.Compute("sum(dueam5)", "")));
                        amt06 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam6)", "")) ? 0.00 : dt1.Compute("sum(dueam6)", "")));
                        amt07 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam7)", "")) ? 0.00 : dt1.Compute("sum(dueam7)", "")));
                        amt08 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam8)", "")) ? 0.00 : dt1.Compute("sum(dueam8)", "")));
                        amt09 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam9)", "")) ? 0.00 : dt1.Compute("sum(dueam9)", "")));
                        amt010 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam10)", "")) ? 0.00 : dt1.Compute("sum(dueam10)", "")));
                        amt011 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam11)", "")) ? 0.00 : dt1.Compute("sum(dueam11)", "")));
                        amt012 = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(dueam12)", "")) ? 0.00 : dt1.Compute("sum(dueam12)", "")));


                        this.gv_YCollectionDetails.Columns[11].Visible = (amt01 != 0);
                        this.gv_YCollectionDetails.Columns[12].Visible = (amt02 != 0);
                        this.gv_YCollectionDetails.Columns[13].Visible = (amt03 != 0);
                        this.gv_YCollectionDetails.Columns[14].Visible = (amt04 != 0);
                        this.gv_YCollectionDetails.Columns[15].Visible = (amt05 != 0);
                        this.gv_YCollectionDetails.Columns[16].Visible = (amt06 != 0);
                        this.gv_YCollectionDetails.Columns[17].Visible = (amt07 != 0);
                        this.gv_YCollectionDetails.Columns[18].Visible = (amt08 != 0);
                        this.gv_YCollectionDetails.Columns[19].Visible = (amt09 != 0);
                        this.gv_YCollectionDetails.Columns[20].Visible = (amt010 != 0);
                        this.gv_YCollectionDetails.Columns[21].Visible = (amt011 != 0);
                        this.gv_YCollectionDetails.Columns[22].Visible = (amt012 != 0);

                        this.gv_YCollectionDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        DateTime frmdate1 = Convert.ToDateTime(this.txtfrmdate.Text.Trim());
                        DateTime todate1 = Convert.ToDateTime(this.txttodate.Text.Trim());
                        for (int i = 11; i < 23; i++)
                        {
                            if (frmdate1 > todate1)
                                break;

                            this.gv_YCollectionDetails.Columns[i].HeaderText = frmdate1.ToString("MMM yy");
                            frmdate1 = frmdate1.AddMonths(1);

                        }
                        this.gv_YCollectionDetails.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                        this.gv_YCollectionDetails.DataSource = dt1;
                        this.gv_YCollectionDetails.DataBind();
                        this.FooterCalculation();
                        Session["Report1"] = gv_YCollectionDetails;
                        if (dt1.Rows.Count > 0)
                            ((HyperLink)this.gv_YCollectionDetails.HeaderRow.FindControl("hlbtntbdeCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                        break;
                }
            }

            catch (Exception e)
            {
            }



        }

        private void FooterCalculation()
        {
            DataTable dt = new DataTable();
            if(Request.QueryString["Type"]== "yCollectionDetails")
            {
                 dt = (DataTable)Session["tblyAccRecde"];
            }
            else
            {
                 dt = (DataTable)Session["tblAccRec"];
            }           
           
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
                case "CurDues":
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

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFatodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
                       0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtotaldues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todues)", "")) ?
                        0.00 : dt.Compute("Sum(todues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bamt)", "")) ?
                        0.00 : dt.Compute("Sum(bamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pbookam)", "")) ?
                        0.00 : dt.Compute("Sum(pbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpinstallment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pinsam)", "")) ?
                    0.00 : dt.Compute("Sum(pinsam)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFpretodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ptodues)", "")) ?
                        0.00 : dt.Compute("Sum(ptodues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFCbooking")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cbookam)", "")) ?
                        0.00 : dt.Compute("Sum(cbookam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFCinstallment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cinsam)", "")) ?
                        0.00 : dt.Compute("Sum(cinsam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFtoCInstalment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ctodues)", "")) ?
                    0.00 : dt.Compute("Sum(ctodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFvtodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(vtodues)", "")) ?
                    0.00 : dt.Compute("Sum(vtodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFdelcharge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                    0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFdischarge")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(discharge)", "")) ?
                    0.00 : dt.Compute("Sum(discharge)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.dgvAccRec02.FooterRow.FindControl("lgvFnettodues")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
                   0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.dgvAccRec02.FooterRow.FindControl("gvFtoreceived02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                      0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");




                    pactcode = (this.DropCheck1.SelectedValue == "000000000000") ? ""
                        : (this.DropCheck1.SelectedValue.Substring(0, 2) == "18") ? "41" + this.DropCheck1.SelectedValue.Substring(2) : "47" + this.DropCheck1.SelectedValue.Substring(2);


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


                case "ProClientst":


                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFununitsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(unusize)", "")) ?
                        0.00 : dt.Compute("Sum(unusize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFsoldunitsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(usize)", "")) ?
                         0.00 : dt.Compute("Sum(usize)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFtounitsize")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsize)", "")) ?
                             0.00 : dt.Compute("Sum(tsize)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFtocostcst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                        0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFEncashcst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(reconamt)", "")) ?
                        0.00 : dt.Compute("Sum(reconamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFatoduescst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(atodues)", "")) ?
                       0.00 : dt.Compute("Sum(atodues)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFdelchargecst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(cdelay)", "")) ?
                    0.00 : dt.Compute("Sum(cdelay)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFnettoduescst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ntodues)", "")) ?
                   0.00 : dt.Compute("Sum(ntodues)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvProClientst.FooterRow.FindControl("lgvFregiscst")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(stdamt)", "")) ?
                      0.00 : dt.Compute("Sum(stdamt)", ""))).ToString("#,##0;(#,##0); ");

                    break;
                case "yCollectionDetails":
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFtoBgdCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdcost)", "")) ?
                        0.00 : dt.Compute("Sum(bgdcost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFtsoldcalue")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tocost)", "")) ?
                         0.00 : dt.Compute("Sum(tocost)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFtoreceivedyc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(ramt)", "")) ?
                         0.00 : dt.Compute("Sum(ramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFpduesyc")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(pdueam)", "")) ?
                         0.00 : dt.Compute("Sum(pdueam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam1)", "")) ?
                         0.00 : dt.Compute("Sum(dueam1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam2)", "")) ?
                            0.00 : dt.Compute("Sum(dueam2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam3")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam3)", "")) ?
                            0.00 : dt.Compute("Sum(dueam3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam4")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam4)", "")) ?
                            0.00 : dt.Compute("Sum(dueam4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam5")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam5)", "")) ?
                            0.00 : dt.Compute("Sum(dueam5)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam6")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam6)", "")) ?
                            0.00 : dt.Compute("Sum(dueam6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam7")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam7)", "")) ?
                            0.00 : dt.Compute("Sum(dueam7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam8")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam8)", "")) ?
                            0.00 : dt.Compute("Sum(dueam8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam9")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam9)", "")) ?
                            0.00 : dt.Compute("Sum(dueam9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam10")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam10)", "")) ?
                            0.00 : dt.Compute("Sum(dueam10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam11")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam11)", "")) ?
                            0.00 : dt.Compute("Sum(dueam11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFdueam12")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dueam12)", "")) ?
                            0.00 : dt.Compute("Sum(dueam12)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFtdueam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(todueam)", "")) ?
                            0.00 : dt.Compute("Sum(todueam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gv_YCollectionDetails.FooterRow.FindControl("lgvdeFgtdueam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(gtodueam)", "")) ?
                            0.00 : dt.Compute("Sum(gtodueam)", ""))).ToString("#,##0;(#,##0); ");



                    break;

            }







        }



        private void ShowReceivedList()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string ProjectCode = ((this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "18" : this.DropCheck1.SelectedValue.ToString()) + "%";

            string ProjectCode = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                ProjectCode = "18";
            else
                foreach (string s1 in sec)
                    ProjectCode = ProjectCode + s1.Substring(0, 12);

            ProjectCode = ProjectCode + "%";

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


            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "DATEWISERECEIVEDLISTSP", ProjectCode, frmdate, todate, searchinfo, "", "", "", "", "");

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "DATEWISERECEIVEDLISTSPINDI", ProjectCode, frmdate, todate, searchinfo, "", "", "", "", "");

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
            // string ProjectCode =((this.DropCheck1.SelectedValue.ToString()=="000000000000")?"1[38]": this.DropCheck1.SelectedValue.ToString())+"%";


            //string ProjectCode = ((this.DropCheck1.Text.ToString() == "000000000000") ? "1[38]" : this.DropCheck1.Text.ToString()) + "%";


            string ProjectCode1 = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 3) == "000")
                ProjectCode1 = "";
            else
                foreach (string s1 in sec)
                    ProjectCode1 = ProjectCode1 + s1.Substring(0, 12);

            string searchinfo = "";
            string CurDues = (this.Request.QueryString["Type"].ToString() == "CurDues") ? "CurDues" : "";
            string paydate = this.chkPayDateWise.Checked ? "Paydate" : "";
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



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROINSDUES", ProjectCode1, frmdate, todate, searchinfo, CurDues, paydate, "", "", "");

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




            //string comcod = this.GetCompCode();
            //string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string ProjectCode = ((this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "1[38]" : this.DropCheck1.SelectedValue.ToString()) + "%";
            //string searchinfo = "";
            //string CurDues = (this.Request.QueryString["Type"].ToString() == "CurDues") ? "CurDues" : "";
            //string paydate = this.chkPayDateWise.Checked ? "Paydate" : "";
            //if (this.ddlSrchCash.SelectedValue != "")
            //{

            //    if (this.ddlSrchCash.SelectedValue == "between")
            //    {
            //        searchinfo = "(vtodues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

            //    }

            //    else
            //    {
            //        searchinfo = "( vtodues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

            //    }
            //}



            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWISEPROINSDUES", ProjectCode, frmdate, todate, searchinfo, CurDues, paydate, "", "", "");

            //if (ds2 == null)
            //{
            //    this.dgvAccRec02.DataSource = null;
            //    this.dgvAccRec02.DataBind();
            //    return;
            //}
            //Session["tblAccRec"] = this.HiddenSameData(ds2.Tables[0]);
            //ViewState["tbltosusold"] = ds2.Tables[1];
            //this.gvinpro.DataSource = ds2.Tables[1];
            //this.gvinpro.DataBind();
            //this.Data_Bind();
        }


        private void AllProDuesCollection()
        {


            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string searchinfo = "";
            string paydate = this.chkPayDateWise.Checked ? "Paydate" : "";
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



            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWALLPROINSDUES", "", frmdate, todate, searchinfo, "", paydate, "", "", "");

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





            //string comcod = this.GetCompCode();
            //string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string searchinfo = "";
            //string paydate = this.chkPayDateWise.Checked ? "Paydate" : "";
            //if (this.ddlSrchCash.SelectedValue != "")
            //{

            //    if (this.ddlSrchCash.SelectedValue == "between")
            //    {
            //        searchinfo = "(ctodues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

            //    }

            //    else
            //    {
            //        searchinfo = "( ctodues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

            //    }
            //}



            //DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDATEWALLPROINSDUES", "", frmdate, todate, searchinfo, "", paydate, "", "", "");

            //if (ds2 == null)
            //{
            //    this.dgvAccRec03.DataSource = null;
            //    this.dgvAccRec03.DataBind();
            //    return;
            //}
            //Session["tblAccRec"] = HiddenSameData(ds2.Tables[0]);
            //ViewState["tbltosusold"] = ds2.Tables[1];
            //this.gvinproal.DataSource = ds2.Tables[1];
            //this.gvinproal.DataBind();
            //this.Data_Bind();

        }

        private void ShowYCollectionfc()
        {



            Session.Remove("tblAccRec");
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));
            if (mon > 12)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
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
        private void YCollectionDetails()
        {
            try
            {
                Session.Remove("tblyAccRecde");
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.GetCompCode();
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfrmdate.Text.Trim()));

                
                if (mon > 12)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Month Less Than Equal Twelve";
                    return;
                }
                string ProjectCode = ((this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "18" : this.DropCheck1.SelectedValue.ToString()) + "%";
                string frmdate = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
                string searchinfo = "";
                string salesvalue = this.ddlsalestatus.SelectedValue;
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


                DataSet dt1 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTYEARLYCOLLECTIONDETAILS", "", frmdate, todate, searchinfo, ProjectCode, salesvalue, "", "", "");

                if (dt1 == null)
                    return;

                Session["tblyAccRecde"] =  this.HiddenSameData(dt1.Tables[0]); 
                this.Data_Bind();
            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }


        private void ShowProWiseClientSt()
        {

            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //string ProjectCode = ((this.DropCheck1.SelectedValue.ToString() == "000000000000") ? "1[38]" : this.DropCheck1.SelectedValue.ToString()) + "%";
            
            //string searchinfo = "";
            //string CurDues = (this.Request.QueryString["Type"].ToString() == "CurDues") ? "CurDues" : "";
            //if (this.ddlSrchCash.SelectedValue != "")
            //{

            //    if (this.ddlSrchCash.SelectedValue == "between")
            //    {
            //        searchinfo = "(vtodues between " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " and " + Convert.ToDouble("0" + this.txtAmountC2.Text.Trim()).ToString() + " )";

            //    }

            //    else
            //    {
            //        searchinfo = "( vtodues " + this.ddlSrchCash.SelectedValue.ToString() + " " + Convert.ToDouble("0" + this.txtAmountC1.Text.Trim()).ToString() + " )";

            //    }
            //}


            string ProjectCode = "";
            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                ProjectCode = "18";
            else
                foreach (string s1 in sec)
                    ProjectCode = ProjectCode + s1.Substring(0, 12);

            ProjectCode = ProjectCode + "%";

            DataSet ds2 = CustData.GetTransInfo(comcod, "SP_REPORT_SALSMGT03", "RPTPROCLIENTST", ProjectCode, frmdate, todate, "", "", "", "", "", "");

            if (ds2 == null)
            {
                this.gvProClientst.DataSource = null;
                this.gvProClientst.DataBind();
                return;
            }
            Session["tblAccRec"] = HiddenSameData(ds2.Tables[0]);
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
            this.divtocash.Visible = (this.ddlSrchCash.SelectedValue == "between");
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

        protected void gvProClientst_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvProClientst.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void dgvAccRec02_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell cell0 = new TableCell();
                cell0.Text = "Information Part";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 5;
                gvrow.Cells.Add(cell0);

                TableCell cell01 = new TableCell();
                cell01.Text = "Price Part";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 5;
                gvrow.Cells.Add(cell01);

                TableCell cell02 = new TableCell();
                cell02.Text = "Total Rec. & Total Dues Part";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 2;
                gvrow.Cells.Add(cell02);


                TableCell cell03 = new TableCell();
                //this.dgvAccRec02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                cell03.Text = "Collectable dues & Overdues up to " + System.DateTime.Today.ToString("MMM-yyyy");
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 2;
                gvrow.Cells.Add(cell03);

                TableCell cell04 = new TableCell();
                cell04.Text = "Collectable dues & Overdues up to " + System.DateTime.Today.ToString("MMM-yyyy");
                // cell04.Text = "Collectable due Amount Details for "+ Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM- yyyy");
                // cell04.Text = "Collectable due Amount Details for " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM- yyyy");
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 7;
                gvrow.Cells.Add(cell04);

                TableCell cell05 = new TableCell();
                cell05.Text = "Applicable Charge";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 3;
                gvrow.Cells.Add(cell05);

                TableCell cell06 = new TableCell();
                cell06.Text = "Received Details-MIS";
                cell06.HorizontalAlign = HorizontalAlign.Center;
                cell06.ColumnSpan = 5;
                gvrow.Cells.Add(cell06);

                dgvAccRec02.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

        protected void gv_YCollectionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_YCollectionDetails.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvyCollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lgactdescyc = (HyperLink)e.Row.FindControl("lgactdescyc");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                string comcod = this.GetCompCode();
                string frmdate = this.txtfrmdate.Text;
                string todate = this.txttodate.Text;
                lgactdescyc.NavigateUrl = "~/F_23_CR/RptReceivedList02.aspx?Type=yCollectionDetails&prjcode=" + code + "&Date1=" + frmdate + "&Date2=" + todate;

            }
        }
    }
}











