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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptWorkOrderStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString().Trim();
                ((Label)this.Master.FindControl("lblTitle")).Text = type == "DetailsWorkIOrdStatus" ? "Purchase Order Details" : "WORK ORDER STATUS";
                this.gvVisibility();
                this.rbtnList1.Visible = false;
                this.ChkBalance.Visible = false;
                this.ChkBalance.Checked = false;
                this.rbtnList1.SelectedIndex = 0;
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetProjectName();
                this.GetSupplier();
              
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private void gvVisibility()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DetailsWorkIOrdStatus":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "RequisitionVsOrder":
                    this.MultiView1.ActiveViewIndex = 2;
                    this.rbtnList1.Visible = false;
                    this.rbtnpurtype.Visible = false;

                    break;

            }
        }



        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
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

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "WorkIOrdStatus":
                    this.rbtnList1.Visible = true;
                    this.ChkBalance.Visible = true;
                    this.workorderStatus();
                    break;
                case "DetailsWorkIOrdStatus":
                    this.rbtnpurtype.Visible = true;
                    this.DetworkorderStatus();
                    break;

                case "RequisitionVsOrder":                                   
                    this.GetRequisitionVsOrder();
                    break;


                    

            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Show Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }

        private void workorderStatus()
        {


            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.LoadData();

        }
        private void DetworkorderStatus()
        {


            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.LoadDetailsData();

        }

        private void GetRequisitionVsOrder()
        {


            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.RequisitionVsOrder();

        }
        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtSrchSupplier = "%%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETSUPPLIER", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");

            DataTable dt = ds2.Tables[0];
            DataRow dr1 = dt.NewRow();
            dr1["ssircode"] = "000000000000";
            dr1["ssirdesc"] = "All Suppler";
            dt.Rows.Add(dr1);


            this.ddlSupplier.DataTextField = "ssirdesc";
            this.ddlSupplier.DataValueField = "ssircode";
            this.ddlSupplier.DataSource = dt;
            this.ddlSupplier.DataBind();
            this.ddlSupplier.SelectedValue = "000000000000";
            ds2.Dispose();



        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
     
        private void LoadData()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string balance = (this.ChkBalance.Checked) ? "woz" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "REQSATIONWORKORDERSTATUS", fromdate, todate, pactcode, balance, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqStatus.DataSource = null;
                this.gvReqStatus.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.LoadGrid();
           

        }
      

        private void RequisitionVsOrder()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();      
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString()=="000000000000"? "%" : this.ddlProjectName.SelectedValue.ToString()+"%";           
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTREQUISITIONVSORDER", fromdate, todate, pactcode, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvReqVsOrder.DataSource = null;
                this.gvReqVsOrder.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.LoadGrid();
           // this.FooterCalculation();



        }
        private void LoadDetailsData()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string suppliercode = (this.ddlSupplier.SelectedValue=="000000000000"?"": this.ddlSupplier.SelectedValue.ToString())+"%";
                  
            string ordertype = this.rbtnpurtype.SelectedIndex == 0 ? "002" : this.rbtnpurtype.SelectedIndex == 1 ? "001" : "";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTWORKORDERSTATUS", fromdate, todate, pactcode, ordertype, suppliercode, "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvDeWorkOrdSt.DataSource = null;
                this.gvDeWorkOrdSt.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblstatus"] = dt1;
            this.LoadGrid();
            this.FooterCalculation();
           
        }
        private void LoadGrid()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblstatus"];
            switch (type)
            {
                case "WorkIOrdStatus":

                    this.gvReqStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqStatus.DataSource = dt;
                    this.gvReqStatus.DataBind();
                    break;
                case "DetailsWorkIOrdStatus":
                    this.gvDeWorkOrdSt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvDeWorkOrdSt.DataSource = dt;
                    this.gvDeWorkOrdSt.DataBind();
                    if (dt.Rows.Count == 0)
                        return;
                    Session["Report1"] = gvDeWorkOrdSt;
                    ((HyperLink)this.gvDeWorkOrdSt.HeaderRow.FindControl("hlbtntbCdataExelSP")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

                case "RequisitionVsOrder":
                    this.gvReqVsOrder.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvReqVsOrder.DataSource = dt;
                    this.gvReqVsOrder.DataBind();
                    break;


                    
            }



        }
        private DataTable HiddenSameDate(DataTable dt1)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":

                    if (rbtnList1.SelectedIndex == 1)
                    {
                        return dt1;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        return dt1;
                    }

                    string pactcode = dt1.Rows[0]["pactcode"].ToString();
                    string reqno = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["reqno"].ToString() == reqno)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["reqno1"] = "";
                            dt1.Rows[j]["reqdat1"] = "";
                        }

                        else
                        {



                            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                            {
                                dt1.Rows[j]["pactdesc"] = "";
                            }

                            if (dt1.Rows[j]["reqno"].ToString() == reqno)
                            {
                                dt1.Rows[j]["reqno1"] = "";

                            }
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();

                        }

                    }
                    break;

                case "DetailsWorkIOrdStatus":

                    if (dt1.Rows.Count == 0)
                        return dt1;
                    string Dpactcode = dt1.Rows[0]["pactcode"].ToString();
                    string orderno = dt1.Rows[0]["orderno"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == Dpactcode && dt1.Rows[j]["orderno"].ToString() == orderno)
                        {
                            Dpactcode = dt1.Rows[j]["pactcode"].ToString();
                            orderno = dt1.Rows[j]["orderno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["orderno2"] = "";
                        }

                        else
                        {
                            Dpactcode = dt1.Rows[j]["pactcode"].ToString();
                            orderno = dt1.Rows[j]["orderno"].ToString();
                        }

                    }
                    break;



                case "RequisitionVsOrder":

                    string pactcode1 = dt1.Rows[0]["pactcode"].ToString();
                    string reqno1 = dt1.Rows[0]["reqno"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode1)
                        {
                            pactcode = dt1.Rows[j]["pactcode"].ToString();
                            reqno = dt1.Rows[j]["reqno"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            //dt1.Rows[j]["reqno1"] = "";
                            //dt1.Rows[j]["reqdat1"] = "";
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

        private void FooterCalculation()
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt = (DataTable)Session["tblstatus"];
            if (dt.Rows.Count == 0)
                return;
            switch (type)
            {
                case "WorkIOrdStatus":

                    break;
                case "DetailsWorkIOrdStatus":
                    //((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFSqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                    //                   0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0;(#,##0); ");
                    //((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFUsqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aprovqty)", "")) ?
                    //                0 : dt.Compute("sum(aprovqty)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFUsAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amount)", "")) ?
                                    0 : dt.Compute("sum(amount)", ""))).ToString("#,##0;(#,##0); ");

                    if (ddlProjectName.SelectedValue.ToString() != "000000000000")
                    {
                        ((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFOrderqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordrqty)", "")) ?
                                   0 : dt.Compute("sum(ordrqty)", ""))).ToString("#,##0.00;(#,##0.00); ");

                        ((Label)this.gvDeWorkOrdSt.FooterRow.FindControl("lgvFAppqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(aprovqty)", "")) ?
                                   0 : dt.Compute("sum(aprovqty)", ""))).ToString("#,##0.00;(#,##0.00); ");
                    }
                    break;
            }


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":

                    if (rbtnList1.SelectedIndex == 0)
                    {
                        RequisitionBasisStatus();
                    }
                    else if (rbtnList1.SelectedIndex == 1)
                    {
                        ProjectBasisStatus();
                    }
                    break;

                case "DetailsWorkIOrdStatus":
                    this.rptDetWorOrdStatus();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Work Order Status";
                string eventdesc = "Print Report: " + type;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        private void ProjectBasisStatus()
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk_Click(null, null);
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string basis = this.rbtnList1.SelectedItem.Text;
            string printFooter = ASTUtility.Concat(compname, username, printdate);

            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblstatus"];
            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptWorkOrderStatus>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptWorkOrderStatus2", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date1", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtTitle", "Work Order Status (Material Basis)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string basis = this.rbtnList1.SelectedItem.Text;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus2();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text ="Work Order Status( "+ basis+" )";
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            //Session["Report1"] = rrs1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            this.ChkBalance.Checked = false;
        }

        private void RequisitionBasisStatus()
        {
            if (this.lnkbtnOk.Text == "Ok")
            {
                this.lnkbtnOk_Click(null, null);
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string basis = this.rbtnList1.SelectedItem.Text;
            string printFooter = ASTUtility.Concat(compname, username, printdate);

            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblstatus"];
            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptWorkOrderStatus>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptWorkOrderStatus1", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date1", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtTitle", "Work Order Status (Requisition Basis)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //if (this.lnkbtnOk.Text == "Ok")
            //{
            //    this.lnkbtnOk_Click(null, null);
            //}
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //string basis = this.rbtnList1.SelectedItem.Text;
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus1();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            //TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Work Order Status( "+basis+" )";
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            ////string comcod = this.GetComeCode();
            ////string comcod = hst["comcod"].ToString();
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //this.ChkBalance.Checked = false;
        }


        private void rptDetWorOrdStatus()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            string printFooter = ASTUtility.Concat(compname, username, printdate);

            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblstatus"];
            var lst = dt1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptWorkOrderStatus02>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptWorkOrderStatus3", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("date1", "From " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("txtTitle", "Details Work Order Staus"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd MMMM, yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd MMMM, yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            //ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus3();
            ////TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            ////rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "From " + fromdate + " To " + todate;

            ////TextObject txtTitle = rrs1.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            ////txtTitle.Text = "Work Order Status( " + basis + " )";
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void rbtnList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvReqStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReqStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "WorkIOrdStatus":
                    this.LoadGrid();
                    break;
                case "DetailsWorkIOrdStatus":
                    this.LoadGrid();
                    break;

                case "RequisitionVsOrder":
                    this.LoadGrid();
                    break;
            }

        }


        protected void gvDeWorkOrdSt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvDeWorkOrdSt.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
    }
}