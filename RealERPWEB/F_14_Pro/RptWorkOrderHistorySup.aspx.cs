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
namespace RealERPWEB.F_14_Pro
{
    public partial class RptWorkOrderHistorySup : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"] == "WorkOrdHisSup") ? "Work Order History Supplier"
                //    : (this.Request.QueryString["Type"] == "WorkOrdHisRes") ? "Work Order History Resource" : (this.Request.QueryString["Type"] == "OrderVsSupplier") ? "Purchase Order Vs Supplier" : "";
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");

                this.ViewSection();
                this.GetSupliersName();
                string type = Request.QueryString["Type"];
                if(type== "OrderVsSupplier")
                {
                    this.GetProjectList();
                }
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
                case "WorkOrdHisSup":
                    this.MultiView1.ActiveViewIndex = 0;


                    break;

                case "WorkOrdHisRes":
                    this.GetResource();
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

                case "OrderVsSupplier":
                    this.project.Visible = true;
                    this.MultiView1.ActiveViewIndex = 2;
                    break;

            }

        }

        private void GetSupliersName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETSUPPLIERlIST", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSupplierName.DataTextField = "ssirdesc";
            this.ddlSupplierName.DataValueField = "ssircode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.DataBind();

        }
        private void GetResource()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtResCode.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETRESOURCELIST", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlResoName.DataTextField = "sirdesc";
            this.ddlResoName.DataValueField = "sircode";
            this.ddlResoName.DataSource = ds1.Tables[0];
            this.ddlResoName.DataBind();

        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupliersName();
        }
        protected void ImgbtnSrchRes_Click(object sender, EventArgs e)
        {
            this.GetResource();

        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetSupliersName();
        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "WorkOrdHisSup":
                    this.ShowWorkHisSup();


                    break;

                case "WorkOrdHisRes":
                    this.ShowWorkHisRes();
                    break;

                case "OrderVsSupplier":
                    this.ShowOrderVsSupplier();
                    break;

            }

        }
        private void ShowWorkHisSup()
        {

            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            //string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string SupCode = ((this.ddlSupplierName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSupplierName.SelectedValue.ToString()) + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "RPTSUPWORKORDER", SupCode, fromdate, todate, "", "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.WorkOrdHisSup.DataSource = null;
                this.WorkOrdHisSup.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;

            this.Data_Bind();


        }
        private void ShowWorkHisRes()
        {
            Session.Remove("tblstatus");
            string comcod = this.GetCompCode();
            //string basis = this.rbtnList1.SelectedItem.Text;
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string SupCode = ((this.ddlSupplierName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSupplierName.SelectedValue.ToString()) + "%";
            string Rescode = ((this.ddlResoName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlResoName.SelectedValue.ToString()) + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "RPTRESWORKORDER", SupCode, fromdate, todate, Rescode, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvWorkOrdHisRes.DataSource = null;
                this.gvWorkOrdHisRes.DataBind();
                return;

            }
            DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = dt1;

            this.Data_Bind();

        }


       
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            DataTable dt1 =(DataTable)Session["tblordersupllier"];
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "WorkOrdHisSup":
                    this.WorkOrdHisSup.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.WorkOrdHisSup.DataSource = dt;
                    this.WorkOrdHisSup.DataBind();
                    this.FooterCalCulation();
                    break;

                case "WorkOrdHisRes":
                    this.gvWorkOrdHisRes.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvWorkOrdHisRes.DataSource = dt;
                    this.gvWorkOrdHisRes.DataBind();
                    break;
                case "OrderVsSupplier":
                    this.gv_OrderVsSupplier.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gv_OrderVsSupplier.DataSource = dt1;
                    this.gv_OrderVsSupplier.DataBind();

                    break;

            }

        }

        private void FooterCalCulation()
        {

            DataTable dt = ((DataTable)Session["tblstatus"]).Copy();
            if (dt.Rows.Count == 0)
                return;


            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "WorkOrdHisSup":


                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("orderno ='PORBBBBAAAAAAA'");
                    DataTable dt1 = dv.ToTable();

                    ((Label)this.WorkOrdHisSup.FooterRow.FindControl("lblgvFPurOrdCos")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(ordramt)", "")) ?
                                  0 : dt1.Compute("sum(ordramt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.WorkOrdHisSup.FooterRow.FindControl("lgvFBalAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(billamt)", "")) ?
                                  0 : dt1.Compute("sum(billamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.WorkOrdHisSup.FooterRow.FindControl("lgvFAcPamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(acpaidamt)", "")) ?
                                  0 : dt1.Compute("sum(acpaidamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.WorkOrdHisSup.FooterRow.FindControl("lgvFBlamtPO")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balbonpo)", "")) ?
                                 0 : dt1.Compute("sum(balbonpo)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.WorkOrdHisSup.FooterRow.FindControl("lgvFBalAmBasDelv")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balbonmrr)", "")) ?
                                 0 : dt1.Compute("sum(balbonmrr)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.WorkOrdHisSup.FooterRow.FindControl("lgvFBalAmBasUnpad")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(balbonbill)", "")) ?
                                 0 : dt1.Compute("sum(balbonbill)", ""))).ToString("#,##0;(#,##0); ");

                    break;


                case "WorkOrdHisRes":

                    break;



            }



        }
        private void ShowOrderVsSupplier()
        {
            try
            {
                string comcod = this.GetCompCode();
                string prjname = ((this.ddlprojectname.SelectedValue.ToString() == "000000000000") ? "16" : this.ddlprojectname.SelectedValue.ToString())+"%";
                string supplier = ((this.ddlSupplierName.SelectedValue.ToString()== "000000000000")?"99": this.ddlSupplierName.SelectedValue.ToString())+"%";
                string fromdate = this.txtFDate.Text;
                string todate = this.txttodate.Text;
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GET_MTRRECV_HISTORY", prjname, fromdate, todate, supplier, "", "", "", "", "");
                if (ds1 == null)
                    return;

                Session["tblordersupllier"] = this.HiddenSameData(ds1.Tables[0]); ;
                this.OrderSupplierDataBound();
               

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void OrderSupplierDataBound()
        {
            DataTable dt = (DataTable)Session["tblordersupllier"];
            this.gv_OrderVsSupplier.DataSource = dt;
            this.gv_OrderVsSupplier.DataBind();
        }

        private void GetProjectList()
        {
            try
            {


                string comcod = this.GetCompCode();
                string txtSProject = "%16%";
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
                this.ddlprojectname.DataTextField = "pactdesc";
                this.ddlprojectname.DataValueField = "pactcode";
                this.ddlprojectname.DataSource = ds1.Tables[0];
                this.ddlprojectname.DataBind();

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            string rsircode = "";
            switch (rpt)
            {


                case "WorkOrdHisSup":

                    string orderno = dt1.Rows[0]["orderno"].ToString();
                    rsircode = dt1.Rows[0]["rsircode"].ToString();
                    string ssircode = dt1.Rows[0]["ssircode"].ToString();

                    for (int i = 1; i < dt1.Rows.Count; i++)
                    {

                        if (dt1.Rows[i]["ssircode"].ToString() == ssircode)
                            dt1.Rows[i]["ssirdesc"] = "";

                        ssircode = dt1.Rows[i]["ssircode"].ToString();

                    }


                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["orderno"].ToString() == orderno && dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["orderno1"] = "";
                            dt1.Rows[j]["orderdat1"] = "";
                            dt1.Rows[j]["rsirdesc"] = "";
                            dt1.Rows[j]["rsirunit"] = "";
                            dt1.Rows[j]["ordrrate"] = 0.00;
                            dt1.Rows[j]["ordrqty"] = 0.00;
                            dt1.Rows[j]["ordramt"] = 0.00;
                        }

                        else
                        {

                            if (dt1.Rows[j]["orderno"].ToString() == orderno)
                            {
                                dt1.Rows[j]["orderno1"] = "";
                                dt1.Rows[j]["orderdat1"] = "";
                            }

                            //if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                            //{
                            //    dt1.Rows[j]["ordrrate"] = 0.00;
                            //    dt1.Rows[j]["ordrqty"] = 0.00;
                            //    dt1.Rows[j]["ordramt"] = 0.00;

                            //}


                        }

                        orderno = dt1.Rows[j]["orderno"].ToString();
                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }

                    break;

                case "WorkOrdHisRes":


                    rsircode = dt1.Rows[0]["rsircode"].ToString();

                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
                        {

                            dt1.Rows[j]["rsirunit"] = "";
                            dt1.Rows[j]["rsirdesc"] = "";

                        }






                        rsircode = dt1.Rows[j]["rsircode"].ToString();

                    }


                    break;
                case "OrderVsSupplier":

                    string projname = dt1.Rows[0]["pactdesc"].ToString();
                    int k = 0;
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        if ( k == 0)
                        {


                            projname = dr1["pactdesc"].ToString();
                            k++;
                            continue;
                        }

                        if (dr1["pactdesc"].ToString() == projname)
                        {

                            dr1["pactdesc"] = "";


                        }


                        projname = dr1["pactdesc"].ToString();
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
                case "WorkOrdHisSup":
                    WorkOrdSuplHisory();
                    break;

                case "WorkOrdHisRes":
                    WorkOrdHisoryResource();
                    break;
            }


        }

        private void WorkOrdSuplHisory()
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
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblstatus"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptWorkOrdHisSup>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptWorkOrderSupHistory", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + fromdate + " To " + todate + ")"));
            Rpt1.SetParameters(new ReportParameter("txtSupplier", "Supplier: " + this.ddlSupplierName.SelectedItem.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Work Order - Supplier History"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //string basis = this.rbtnList1.SelectedItem.Text;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            ////ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus2();
            //ReportDocument rptDoc = new RealERPRPT.R_14_Pro.RptWorkOrderSupHistory();
            //TextObject rptCname = rptDoc.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rptDoc.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "(From " + fromdate + " To " + todate + ")";

            //TextObject txtsupplier = rptDoc.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            //txtsupplier.Text = "Supplier: " + this.ddlSupplierName.SelectedItem.Text.Trim();

            //TextObject txtuserinfo = rptDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptDoc.SetDataSource(dt1);
            //Session["Report1"] = rptDoc;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        private void WorkOrdHisoryResource()
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
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            DataTable dt = (DataTable)Session["tblstatus"];

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptWorkOrdHisResource>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptWorkOrdHisoryResource", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            //Rpt1.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + fromdate + " To " + todate + ")"));

            Rpt1.SetParameters(new ReportParameter("rptTitle", "Work Order - History Resource"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ////string basis = this.rbtnList1.SelectedItem.Text;
            //string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //DataTable dt1 = (DataTable)Session["tblstatus"];
            ////ReportDocument rrs1 = new RealERPRPT.R_14_Pro.RptWorkOrderStatus2();
            //ReportDocument rptDoc = new RealERPRPT.R_14_Pro.RptWorkOrdHisoryResource();
            //TextObject rptCname = rptDoc.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rptDoc.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "(From " + fromdate + " To " + todate + ")";

            ////TextObject txtsupplier = rptDoc.ReportDefinition.ReportObjects["txtsupplier"] as TextObject;
            ////txtsupplier.Text = "Supplier: " + this.ddlSupplierName.SelectedItem.Text.Trim();

            //TextObject txtuserinfo = rptDoc.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptDoc.SetDataSource(dt1);
            //Session["Report1"] = rptDoc;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }




        protected void WorkOrdHisSup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.WorkOrdHisSup.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
           
           
           
                this.OrderSupplierDataBound();
           
        }



        protected void gvWorkOrdHisRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvWorkOrdHisRes.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
        protected void gvWorkOrdHisRes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.ColumnSpan = 1;

            //    TableCell cell02 = new TableCell();
            //    cell02.Text = "";
            //    cell02.HorizontalAlign = HorizontalAlign.Center;
            //    cell02.ColumnSpan = 1;

            //    TableCell cell03 = new TableCell();
            //    cell03.Text = "";
            //    cell03.HorizontalAlign = HorizontalAlign.Center;
            //    cell03.ColumnSpan = 1;

            //    TableCell cell04 = new TableCell();
            //    cell04.Text = "";
            //    cell04.HorizontalAlign = HorizontalAlign.Center;
            //    cell04.ColumnSpan = 1;

            //    TableCell cell05 = new TableCell();
            //    cell05.Text = "";
            //    cell05.HorizontalAlign = HorizontalAlign.Center;
            //    cell05.ColumnSpan = 1;

            //    TableCell cell06 = new TableCell();
            //    cell06.Text = "";
            //    cell06.HorizontalAlign = HorizontalAlign.Center;
            //    cell06.ColumnSpan = 1;

            //    TableCell cell07 = new TableCell();
            //    cell07.Text = "";
            //    cell07.HorizontalAlign = HorizontalAlign.Center;
            //    cell07.ColumnSpan = 1;

            //    TableCell cell08 = new TableCell();
            //    cell08.Text = "";
            //    cell08.HorizontalAlign = HorizontalAlign.Center;
            //    cell08.ColumnSpan = 1;
            //    TableCell cell09 = new TableCell();
            //    cell09.Text = "";
            //    cell09.HorizontalAlign = HorizontalAlign.Center;
            //    cell09.ColumnSpan = 1;

            //    TableCell cell10 = new TableCell();
            //    cell10.Text = "Order";
            //    cell10.HorizontalAlign = HorizontalAlign.Center;
            //    cell10.ColumnSpan = 2;




            //    TableCell cell0 = new TableCell();
            //    cell0.Text = "-->     <--";
            //    cell0.HorizontalAlign = HorizontalAlign.Center;
            //    cell0.ColumnSpan = 2;
            //    TableCell cell1 = new TableCell();
            //    cell1.Text = "";
            //    cell1.HorizontalAlign = HorizontalAlign.Center;
            //    cell1.ColumnSpan = 1;
            //    TableCell cell2 = new TableCell();
            //    cell2.Text = "Control";
            //    cell2.HorizontalAlign = HorizontalAlign.Center;
            //    cell2.ColumnSpan = 3;
            //    TableCell cell3 = new TableCell();
            //    cell3.Text = "Commercial Part";
            //    cell3.HorizontalAlign = HorizontalAlign.Center;
            //    cell3.ColumnSpan = 2;
            //    TableCell cell4 = new TableCell();
            //    cell4.Text = "Time Part";
            //    cell4.HorizontalAlign = HorizontalAlign.Center;
            //    cell4.ColumnSpan = 3;
            //    TableCell cell5 = new TableCell();
            //    cell5.Text = "Calander";
            //    cell5.HorizontalAlign = HorizontalAlign.Center;
            //    cell5.ColumnSpan = 2;
            //    TableCell cell6 = new TableCell();
            //    cell6.Text = "";
            //    cell6.HorizontalAlign = HorizontalAlign.Center;
            //    cell6.ColumnSpan = 2;

            //    gvrow.Cells.Add(cell01);
            //    gvrow.Cells.Add(cell0);
            //    gvrow.Cells.Add(cell1);
            //    gvrow.Cells.Add(cell2);
            //    gvrow.Cells.Add(cell3);
            //    gvrow.Cells.Add(cell4);
            //    gvrow.Cells.Add(cell5);
            //    gvrow.Cells.Add(cell6);
            //    gvDailyPro.Controls[0].Controls.AddAt(0, gvrow);
            // }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label orderno = (Label)e.Row.FindControl("lblgvordnores");
                Label Ordamt = (Label)e.Row.FindControl("lblgvPurOrdamtres");
                Label DeliveredAmt = (Label)e.Row.FindControl("lblgvDeliveredAmtres");
                Label BalAmt = (Label)e.Row.FindControl("lblgvBalAmt");
                Label AcPamt = (Label)e.Row.FindControl("lblgvAcPamtres");
                Label orddbalamt = (Label)e.Row.FindControl("lblgvorddbalamtres");
                Label mrrbalamt = (Label)e.Row.FindControl("lblgvmrrbalamtres");
                Label billbalPamt = (Label)e.Row.FindControl("lblgvbillbalPamtres");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "PORAAAAAAAAAAA")
                {

                    orderno.Font.Bold = true;
                    Ordamt.Font.Bold = true;
                    DeliveredAmt.Font.Bold = true;
                    BalAmt.Font.Bold = true;
                    AcPamt.Font.Bold = true;
                    orddbalamt.Font.Bold = true;
                    mrrbalamt.Font.Bold = true;
                    billbalPamt.Font.Bold = true;
                    orderno.Style.Add("text-align", "right");


                }

            }
        }


        protected void WorkOrdHisSup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label orderno = (Label)e.Row.FindControl("lblgvordno");
                Label Ordamt = (Label)e.Row.FindControl("lblgvPurOrdCos");
                Label DeliveredAmt = (Label)e.Row.FindControl("lblgvDeliveredAmt");
                Label BalAmt = (Label)e.Row.FindControl("lblgvBalAmt");
                Label AcPamt = (Label)e.Row.FindControl("lblgvAcPamt");
                Label BlamtPO = (Label)e.Row.FindControl("lblgvBlamtPO");
                Label BalAmBasDel = (Label)e.Row.FindControl("lblgvBalAmBasDelv");
                Label BalAmBaunpaid = (Label)e.Row.FindControl("lblgvBalAmBasUnpad");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "orderno")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "PORAAAAAAAAAAA" || code == "PORBBBBAAAAAAA")
                {

                    orderno.Font.Bold = true;
                    Ordamt.Font.Bold = true;
                    DeliveredAmt.Font.Bold = true;
                    BalAmt.Font.Bold = true;
                    AcPamt.Font.Bold = true;
                    BlamtPO.Font.Bold = true;
                    BalAmBasDel.Font.Bold = true;
                    BalAmBaunpaid.Font.Bold = true;
                    orderno.Style.Add("text-align", "right");


                }

            }

        }

        protected void gv_OrderVsSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_OrderVsSupplier.PageIndex = e.NewPageIndex;
            OrderSupplierDataBound();
        }
    }
}