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
using RealERPRDLC;
namespace RealERPWEB.F_24_CC
{
    public partial class RptClientModification : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["WType"].ToString().Trim() == "ReqStatus") ? "REQUISITION STATUS REPORT"
                //    : (this.Request.QueryString["WType"].ToString().Trim() == "ReqAppStatus") ? "REQUISITION APPROVAL STATUS REPORT" : (this.Request.QueryString["WType"].ToString().Trim() == "CliBillApproval") ? "CLIENT MODIFICATION REPORT (Bill Approval)" : "CLIENT MODIFICATION REPORT";


                this.SelectView();
                this.txttodate.Text = Request.QueryString["Date2"].Length > 0 ? Request.QueryString["Date2"].ToString() : System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = Request.QueryString["Date1"].Length > 0 ? Request.QueryString["Date1"].ToString() : System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetProjectName();



            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void SelectView()
        {
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {


                case "CliModfi":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "CliBillApproval":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetProjectName()
         {

            string comcod = this.GetCompCode();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETPROJECTNAME1", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            if (Request.QueryString["prjcode"].Length > 0)
            {
                ddlProjectName.SelectedValue = Request.QueryString["prjcode"].ToString();
                ddlProjectName.Enabled = false;
            }
            this.GetUnitName();


        }

        private void GetUnitName()
        {
            string comcod = this.GetCompCode();
            string pactcode = (this.ddlProjectName.SelectedValue == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString();
            string FintCust = "%" + this.txtSrcCustomer.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETUNITNAME", pactcode, FintCust, "", "", "", "", "", "", "");
            this.ddlCustomer.DataTextField = "custaunit";
            this.ddlCustomer.DataValueField = "usircode";
            this.ddlCustomer.DataSource = ds1.Tables[0];
            this.ddlCustomer.DataBind();



        }


        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void imgbtnFindCustomer_Click(object sender, EventArgs e)
        {
            this.GetUnitName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {


                case "CliModfi":
                    this.RptClientMod();
                    break;

                case "CliBillApproval":
                    this.RptClientModBillApproval();
                    break;


                    


            }




        }

        private void RptClientModBillApproval()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comadd = hst["comadd1"].ToString();
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString(); 
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + comnam + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblstatus"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_24_CC.RptClientModification>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptClientModification", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("frmDate", frmdate));
            Rpt1.SetParameters(new ReportParameter("toDate", todate));
            Rpt1.SetParameters(new ReportParameter("comAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Client Modification Report"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void RptClientMod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["tblstatus"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_23_CRR.EClassCutomer.ClientModification>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_23_CR.RptCliMod", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("ftDate", "Date: " + fromdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Client Modification Report"));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {

                case "CliModfi":
                    this.ShowClientMod();
                    break;

                case "CliBillApproval":
                    this.ShowClientModBillAproval();
                    break;


                    
            }


            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Requsition Status";
                string eventdesc = "Show Report";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void imgbtnFindMod_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {

                case "CliModfi":
                    this.ShowClientMod();
                    break;
            }

        }



        private void ShowClientModBillAproval()
        {

            Session.Remove("tblstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcdoe = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string unitcode = (this.ddlCustomer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlCustomer.SelectedValue.ToString() + "%";

            string adwno = "%" + this.txtSrcRequisition.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMODFINALBILLAPPROVAL", frmdate, todate, pactcdoe, unitcode, adwno, "", "", "", "");
            if (ds1 == null)
            {
                this.gvfbillapproval.DataSource = null;
                this.gvfbillapproval.DataBind();
                return;
            }
            Session["tblstatus"] = this.HiddenSameDate(ds1.Tables[0]);
            this.Data_Bind();
            ds1.Dispose();

        }
        private void ShowClientMod()
        {
            Session.Remove("tblstatus");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string pactcdoe = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string unitcode = (this.ddlCustomer.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlCustomer.SelectedValue.ToString() + "%";

            string adwno = "%" + this.txtSrcRequisition.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "PRINTCLIENTMOD", frmdate, todate, pactcdoe, unitcode, adwno, "", "", "", "");
            if (ds1 == null)
            {
                this.grvRptCliMod.DataSource = null;
                this.grvRptCliMod.DataBind();
                return;
            }
            Session["tblstatus"] = ds1.Tables[0];
            this.Data_Bind();
            ds1.Dispose();

        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblstatus"];
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {


                case "CliModfi":
                    this.grvRptCliMod.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvRptCliMod.DataSource = dt;
                    this.grvRptCliMod.DataBind();
                    this.FooterCalculation();
                    break;

                case "CliBillApproval":
                    this.gvfbillapproval.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvfbillapproval.DataSource = dt;
                    this.gvfbillapproval.DataBind();
                    this.FooterCalculation();
                    Session["Report1"] = gvfbillapproval;
                    ((HyperLink)this.gvfbillapproval.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;


                


            }
        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblstatus"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["WType"].ToString().Trim();
            switch (type)
            {


                case "CliModfi":
                    ((Label)this.grvRptCliMod.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
                                0 : dt1.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvRptCliMod.FooterRow.FindControl("lgvFDisamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(disamt)", "")) ?
                                0 : dt1.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.grvRptCliMod.FooterRow.FindControl("lgvFNetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ?
                                0 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "CliBillApproval":
                    ((Label)this.gvfbillapproval.FooterRow.FindControl("lgvFAmtf")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt)", "")) ?
                                0 : dt1.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvfbillapproval.FooterRow.FindControl("lgvFDisamtf")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(disamt)", "")) ?
                                0 : dt1.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvfbillapproval.FooterRow.FindControl("lgvFNetamtf")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(netamt)", "")) ?
                                0 : dt1.Compute("sum(netamt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }

        }

        private DataTable HiddenSameDate(DataTable dt1)
        {
            string type = this.Request.QueryString["WType"].ToString().Trim();
            string pactcode1 = "";
            switch (type)
            {


                case "CliModfi":
                    pactcode1 = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode1)
                        {
                            pactcode1 = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                        }

                        else
                            pactcode1 = dt1.Rows[j]["pactcode"].ToString();
                    }

                    break;


                case "CliBillApproval":
                     pactcode1 = dt1.Rows[0]["pactcode"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["pactcode"].ToString() == pactcode1)
                        {
                            pactcode1 = dt1.Rows[j]["pactcode"].ToString();
                            dt1.Rows[j]["pactdesc"] = "";
                            dt1.Rows[j]["pactcode"] = "";

                        }

                        else
                            pactcode1 = dt1.Rows[j]["pactcode"].ToString();
                    }

                    break;
            }


            return dt1;

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }


        protected void grvRptCliMod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvRptCliMod.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvfbillapproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvfbillapproval.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetUnitName();
            this.imgbtnFindCustomer_Click(null, null);
        }




        protected void grvRptCliMod_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink gvamt = (HyperLink)e.Row.FindControl("lnkgvamt");
                string adno = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "adno")).ToString();

                if (adno == "")
                {
                    return;
                }

                string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();
                string unitdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "udesc")).ToString();
                string cusname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "cusname")).ToString();
                string cusaunit = cusname + ',' + unitdesc;
                gvamt.NavigateUrl = "~/F_24_CC/LinkCustMaintenWork.aspx?adno=" + adno + "&pactdesc=" + pactdesc + "&unitdesc=" + cusaunit;

            }
        }

       
    }
}