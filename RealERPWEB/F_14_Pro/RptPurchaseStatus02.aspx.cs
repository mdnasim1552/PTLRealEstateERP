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

    public partial class RptPurchaseStatus02 : System.Web.UI.Page
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
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string Type = this.Request.QueryString["Type"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = (Type == "Purchase") ? "Purchase Summary With Opening"
                //    : (Type == "DaywPur") ? "Day Wise Purchase History (Project & Supplier) Wise"
                //    : "";

                //----------------udate-20150120---------
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Summary With Opening";

                this.ShowView();

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private void ShowView()
        {
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.GetProjectName();
                    Panel1.Visible = true;
                    pnldaywpur.Visible = false;
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

                case "DaywPur":
                    string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtFdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                    this.txtTodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                    this.GetProjectName2();
                    Panel1.Visible = false;
                    pnldaywpur.Visible = true;
                    this.MultiView1.ActiveViewIndex = 1;
                    break;

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;

        }

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcProject.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetMaterial();

        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetMaterial()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();
            string txtfindMat = this.txtSrcMaterials.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETMATERIAL", pactcode, txtfindMat, "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "rsirdesc";
            this.DropCheck1.DataValueField = "rsirdesc";
            this.DropCheck1.DataSource = ds1.Tables[0];
            this.DropCheck1.DataBind();
            ds1.Dispose();

        }

        protected void imgbtnFindMat_Click(object sender, ImageClickEventArgs e)
        {
            this.GetMaterial();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.RptDayPurchase();
                    break;

                case "DaywPur":
                    //this.RptDayPurchase();
                    break;


            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Print Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }


        private void RptDayPurchase()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string projectName = this.ddlProjectName.SelectedItem.Text.Substring(13);
            //string Type = Request.QueryString["Type"].ToString();
            //string rpthead = (Type == "ImpPlan" ? "Monthly Implementation Plan" : (Type == "Execution" ? "Work Execution" : "Monthly Plan Vs. Execution"));

            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd MMMM, yyyy");

            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpurchase"];


            var lst = dt.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurchaseSummary02>();

            LocalReport Rpt1 = new LocalReport();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchaseSummary02", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            // Rpt1.SetParameters(new ReportParameter("txtPrjName", "Project Name : " + this.ddlProjectName.SelectedItem.Text.Trim().Substring(13)));
            //Rpt1.SetParameters(new ReportParameter("todate", "(From: " + Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy") + "  To : " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
            Rpt1.SetParameters(new ReportParameter("todate", "As On: " + date));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Purchase  Summary"));
            Rpt1.SetParameters(new ReportParameter("RptFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            //Rpt1.SetParameters(new ReportParameter("ExePer", MaAmt > 0 ? ((ExeAmt * 100) / MaAmt).ToString("#,##0.00;(#,##0.00); ") + "%" : "";));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowValue();

        }
        private void ShowValue()
        {
            string comcod = this.GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.lblPage.Visible = true;
                    this.ddlpagesize.Visible = true;
                    this.ShowPurchase();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        private void ShowPurchase()
        {

            string rsircode = "";
            Session.Remove("tblpurchase");

            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtdate.Text).ToString("dd-MMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";

            string[] sec = this.DropCheck1.Text.Trim().Split(',');
            if (sec[0].Substring(0, 4) == "0000")
                rsircode = "";
            else
            {
                foreach (string s1 in sec)
                {
                    rsircode = rsircode + s1.Substring(0, 12);
                }
            }


            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "RPTPURSUMMARY", date, pactcode, rsircode, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurSum.DataSource = null;
                this.gvPurSum.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();
        }


        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblpurchase"];

            if (dt.Rows.Count == 0) //Problem
                return;

            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "Purchase":
                    this.gvPurSum.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPurSum.DataSource = dt;
                    this.gvPurSum.DataBind();
                    ((Label)this.gvPurSum.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                         0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

                case "DaywPur":

                    this.gvPurStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvPurStatus.DataSource = HiddenSameData(dt);
                    this.gvPurStatus.DataBind();

                    ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
                                         0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ");

                    ((Label)this.gvPurStatus.FooterRow.FindControl("lgvFqty")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(qty)", "")) ?
                                    0 : dt.Compute("sum(qty)", ""))).ToString("#,##0.00;(#,##0.00); ");

                    Session["Report1"] = gvPurStatus;
                    ((HyperLink)this.gvPurStatus.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
                    break;

            }

        }

        private void GetProjectName2()
        {
            string comcod = this.GetCompCode();
            string txtSProject = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETPROJECTNAME02", txtSProject, "", "", "", "", "", "", "", "");
            //this.ddlProjectName2.DataTextField = "pactdesc";
            //this.ddlProjectName2.DataValueField = "pactcode";
            //this.ddlProjectName2.DataSource = ds1.Tables[0];
            //this.ddlProjectName2.DataBind();

            this.chkPrjName.DataTextField = "pactdesc";
            this.chkPrjName.DataValueField = "pactcode";
            this.chkPrjName.DataSource = ds1.Tables[0];
            this.chkPrjName.DataBind();

            this.GetSupplier();

            //drpchkproj

        }
        private void GetSupplier()
        {
            string comcod = this.GetCompCode();
            string pactcode = "000000000000";
            string txtSrchSupplier = "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETSUPPLIER02", pactcode, txtSrchSupplier, "", "", "", "", "", "", "");
            this.chkSupName.DataTextField = "ssirdesc";
            this.chkSupName.DataValueField = "ssircode";
            this.chkSupName.DataSource = ds1.Tables[0];
            this.chkSupName.DataBind();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvPurSum_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurSum.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void imgbtnFindMaterials_Click(object sender, EventArgs e)
        {
            this.GetMaterial();
        }
        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMaterial();
        }

        protected void imgbtnFindProject2_Click(object sender, EventArgs e)
        {
            this.GetProjectName2();
        }

        protected void ltbnOk2_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string rpt = this.Request.QueryString["Type"].ToString().Trim();
            switch (rpt)
            {
                case "DaywPur":
                    this.lblPage2.Visible = true;
                    this.ddlpagesize2.Visible = true;
                    this.ShowSupWisePurchase();
                    break;
            }
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Materials Purchase Status";
                string eventdesc = "Show Report: " + rpt;
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void imgbtnFindSup_Click(object sender, EventArgs e)
        {
            this.GetSupplier();
        }

        protected void ddlpagesize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvPurStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPurStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ddlProjectName2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSupplier();
        }
        private void ShowSupWisePurchase()
        {
            string pactcode1 = "", rsircode = "";
            Session.Remove("tblpurchase");

            string comcod = this.GetCompCode();
            string Fdate = Convert.ToDateTime(this.txtFdate.Text).ToString("dd-MMM-yyyy");
            string Todate = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");

            string gp = this.chkPrjName.SelectedValue.Trim();
            if (gp.Length > 0)
            {
                if (gp.Trim() == "000000000000" || gp.Trim() == "")
                    pactcode1 = "";
                else
                    foreach (ListItem s1 in chkPrjName.Items)
                    {
                        if (s1.Selected)
                        {
                            pactcode1 = pactcode1 + s1.Value.Substring(0, 12);
                        }
                    }

            }

            string gp2 = this.chkSupName.SelectedValue.Trim();
            if (gp2.Length > 0)
            {
                if (gp2.Trim() == "000000000000" || gp2.Trim() == "")
                    rsircode = "";
                else
                    foreach (ListItem s1 in chkSupName.Items)
                    {
                        if (s1.Selected)
                        {
                            rsircode = rsircode + s1.Value.Substring(0, 12);
                        }
                    }

            }

            string pactcode2 = pactcode1.Length == 0 ? "000000000000" : pactcode1;
            string rsircode2 = rsircode.Length == 0 ? "000000000000" : rsircode;

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "RPTSUPINFODATWISE", Fdate, Todate, pactcode2, rsircode2, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvPurStatus.DataSource = null;
                this.gvPurStatus.DataBind();
                return;
            }

            Session["tblpurchase"] = ds1.Tables[0];
            this.LoadGrid();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
            {
                return dt1;
            }
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string mrrno = dt1.Rows[0]["mrrno"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["mrrno"].ToString() == mrrno)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    mrrno = dt1.Rows[j]["mrrno"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                    dt1.Rows[j]["mrrno1"] = "";
                }
                else
                {
                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                    {
                        dt1.Rows[j]["pactdesc"] = "";
                    }
                    if (dt1.Rows[j]["mrrno"].ToString() == mrrno)
                    {
                        dt1.Rows[j]["mrrno1"] = "";
                    }
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    mrrno = dt1.Rows[j]["mrrno"].ToString();

                }
            }
            return dt1;

        }
    }
}