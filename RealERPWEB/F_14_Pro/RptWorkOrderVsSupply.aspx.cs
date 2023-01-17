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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptWorkOrderVsSupply : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "OrderTk") ? " Order Tracking" : (this.Request.QueryString["Type"] == "OrdVsSup") ? "Purchase Order-Supplier Wise" : "ORDER TRACKING INFORMATION";
                this.ViewSection();

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

        private void ViewSection()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "OrdVsSup":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ChkBalance.Checked = false;
                    this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                    this.GetProjectName();
                    this.GetMaterialName();
                    this.ddlMaterial.Visible = true;
                    this.lblmatrial.Visible = true;
                    this.txtmatreial.Visible = true;
                    this.btnMaterial.Visible = true;
                    this.imgbtnFindSupplier_Click(null, null);
                    break;

                case "OrderTk":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.txtorddate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.ddlMaterial.Visible = false;
                    this.lblmatrial.Visible = false;
                    this.txtmatreial.Visible = false;
                    this.btnMaterial.Visible = false;
                    this.GetOrderList();
                    break;

            }




        }



        private void GetMaterialName()
        {
            ViewState.Remove("tblmat");
            string comcod = this.GetCompCode();
            string serch1 = "%" + this.txtmatreial.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_PURCHASE", "GETMATERIALSNAME", serch1, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMaterial.DataTextField = "rsirdesc";
            this.ddlMaterial.DataValueField = "rsircode";
            this.ddlMaterial.DataSource = ds1.Tables[0];
            this.ddlMaterial.DataBind();
            Session["tblmat"] = ds1.Tables[0];
            ds1.Dispose();



        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAMEFORREQ", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }




        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        private void GetOrderList()
        {

            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtorddate.Text.Trim()).ToString("dd-MMM-yyyy");
            string orderlist = this.txtSrcOrder.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETORDERNO", date, orderlist, "", "", "", "", "", "", "");
            this.ddlOrderList.DataTextField = "orderno1";
            this.ddlOrderList.DataValueField = "orderno";
            this.ddlOrderList.DataSource = ds1.Tables[0];
            this.ddlOrderList.DataBind();
            ds1.Dispose();
        }


        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.LoadData();
        }



        private void LoadData()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            //string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSupCod = (this.ddlSupplierName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSupplierName.SelectedValue.ToString() + "%";
            string balance = (this.ChkBalance.Checked) ? "woz" : "";
            string ddlmatrial = (this.ddlMaterial.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlMaterial.SelectedValue.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "WORKORDER_VS_SUPPLY", txtSupCod, fromdate, todate, pactcode, balance, ddlmatrial, "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;

            this.LoadGv();
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void LoadGv()
        {
            DataTable dt = (DataTable)Session["tblstatus"];

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "OrdVsSup":
                    this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();
                    break;

                case "OrderTk":
                    this.gvOrdertk.DataSource = dt;
                    this.gvOrdertk.DataBind();
                    break;

            }





        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            string pactcode;
            switch (rpt)
            {


                case "OrdVsSup":
                    string ssircode = dt1.Rows[0]["ssircode"].ToString();
                    string orderno = dt1.Rows[0]["orderno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["ssircode"].ToString() == ssircode && dt1.Rows[j]["orderno"].ToString() == orderno)
                        {
                            pactcode = dt1.Rows[j]["ssircode"].ToString();
                            orderno = dt1.Rows[j]["orderno"].ToString();
                            dt1.Rows[j]["ssirdesc"] = "";
                            //dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["orderno"] = "";
                            dt1.Rows[j]["orderdat"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["ssircode"].ToString() == ssircode)
                            {
                                dt1.Rows[j]["ssirdesc"] = "";
                            }

                            if (dt1.Rows[j]["orderno"].ToString() == orderno)
                            {
                                dt1.Rows[j]["orderno"] = "";
                                dt1.Rows[j]["orderdat"] = "";

                            }
                            ssircode = dt1.Rows[j]["ssircode"].ToString();
                            orderno = dt1.Rows[j]["orderno"].ToString();

                        }

                    }

                    break;

                case "OrderTk":
                    pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string rsircode = dt1.Rows[0]["rsircode"].ToString();
                    string spcfcod = dt1.Rows[0]["spcfcod"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        {

                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode && dt1.Rows[j]["spcfcod"].ToString() == spcfcod)
                        {
                            rsircode = dt1.Rows[j]["rsircode"].ToString();
                            spcfcod = dt1.Rows[j]["spcfcod"].ToString();
                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                            dt1.Rows[j]["spcfdesc"] = "";
                            dt1.Rows[j]["ordrqty"] = 0.0000000;
                        }


                        else
                        {

                            if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                            {

                                rsircode = dt1.Rows[j]["rsircode"].ToString();
                                dt1.Rows[j]["rsirdesc"] = "";
                                dt1.Rows[j]["rsirunit"] = "";
                            }


                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            rsircode = dt1.Rows[j]["rsircode"].ToString();

                        }
                    }

                    break;

            }


            return dt1;

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "OrdVsSup":
                    ProjectBasisStatus();
                    break;

                case "OrderTk":
                    this.RptOrderTrakcing();
                    break;

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void ProjectBasisStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus2();
            ReportDocument rptDoc = new RealERPRPT.R_14_Pro.RptWorkOrderVsSupply();
            TextObject rptCname = rptDoc.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtFDate1 = rptDoc.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            txtFDate1.Text = "(From " + fromdate + " To " + todate + ")";

            TextObject txtsupplier = rptDoc.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            txtsupplier.Text = "Supplier: " + this.ddlSupplierName.SelectedItem.Text.Trim().Substring(14);

            TextObject txtuserinfo = rptDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptDoc.SetDataSource(dt1);
            Session["Report1"] = rptDoc;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            this.ChkBalance.Checked = false;
        }

        private void RptOrderTrakcing()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)Session["tblstatus"];


            ReportDocument rptDoc = new RealERPRPT.R_14_Pro.RptOrderTracking();
            TextObject rptCname = rptDoc.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            rptCname.Text = comnam;
            TextObject rpttxtsupplier = rptDoc.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            rpttxtsupplier.Text = dt1.Rows[0]["ssirdesc"].ToString();

            TextObject rpttxtorderno = rptDoc.ReportDefinition.ReportObjects["txtorderno"] as TextObject;
            rpttxtorderno.Text = "Order No: " + dt1.Rows[0]["orderno"].ToString();

            TextObject rpttxtFDate = rptDoc.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            rpttxtFDate.Text = "Date: " + Convert.ToDateTime(dt1.Rows[0]["orderdat"]).ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rptDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptDoc.SetDataSource(dt1);
            Session["Report1"] = rptDoc;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



        }



        //private void RequisitionBasisStatus()
        //{
        //    if (this.lnkbtnOk.Text == "Ok")
        //    {
        //        this.lnkbtnOk_Click(null, null);
        //    }
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comnam = hst["comnam"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();
        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

        //    string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
        //    string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
        //    //string basis = this.rbtnList1.SelectedItem.Text;
        //    DataTable dt1 = (DataTable)Session["tblstatus"];
        //    ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus1();
        //    TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
        //    rptCname.Text = comnam;

        //    TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
        //    txtFDate1.Text = "From " + fromdate + " To " + todate;

        //    //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
        //    //txtTitle.Text = "Work Order Status( "+basis+" )";
        //    TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
        //    txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
        //    rrs1.SetDataSource(dt1);
        //    Session["Report1"] = rrs1;
        //    lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
        //                        this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        //    this.ChkBalance.Checked = false;
        //}



        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGv();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGv();
        }


        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //string mProjCode = this.ddlProject.SelectedValue.ToString();
            string txtSupplier = this.txtSrcSupplier.Text.ToString() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETALLUPLIST", txtSupplier, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblSup"] = ds1.Tables[0];

            this.ddlSupplierName.DataTextField = "ssirdesc";
            this.ddlSupplierName.DataValueField = "ssircode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.DataBind();
        }

        protected void lbtnOrderTk_Click(object sender, EventArgs e)
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string orderno = this.ddlOrderList.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTORDERTRACK", orderno, "", "", "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvOrdertk.DataSource = null;
                this.gvOrdertk.DataBind();
                return;

            }

            Session["tblstatus"] = this.HiddenSameData(ds1.Tables[0]);
            ds1.Dispose();
            this.LoadGv();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Show Order Tracking";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void imgbtnFindOrder_Click(object sender, EventArgs e)
        {
            this.GetOrderList();
        }
        protected void btnMaterial_Click(object sender, EventArgs e)
        {
            this.GetMaterialName();
        }
    }
}