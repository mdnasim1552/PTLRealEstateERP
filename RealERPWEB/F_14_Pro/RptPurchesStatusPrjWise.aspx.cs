using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRDLC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_14_Pro
{
    public partial class RptPurchesStatusPrjWise : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (dr1.Length == 0)
                //    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();

                if (Request.QueryString["Type"] == "Report")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Status Report";
                    string Date01 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date01, 8);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                }
                else
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Status Details";
                    this.lnkbtnok.Visible = false;
                    string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date, 8);
                    this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                }
                this.GridHidden();
               


                

            }

        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GridHidden()
        {
            string type = Request.QueryString["Type"].ToString();
            if (type == "Report")
            {
                this.GetProjectList();
                this.pnlAllProjectreport.Visible = true;
                this.pnlprojectDetails.Visible = false;

            }
            else
            {
                this.Pagging.Visible = false;
                this.txtfrmdate.Enabled = true;
                this.txttodate.Enabled = true;
                this.ddlprojectname.Enabled = true;
                this.GetProjectList();
                this.GetProjectDetails();
                this.pnlAllProjectreport.Visible = false;
                this.pnlprojectDetails.Visible = true;
            }
        }


        private void GetProjectList()
        {
            string comcod = this.GetCompCode();
            string txtSProject = Request.QueryString["Type"] == "Report"? "%%": Request.QueryString["pactcode"].ToString() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlprojectname.DataTextField = "pactdesc";
            this.ddlprojectname.DataValueField = "pactcode";
            this.ddlprojectname.DataSource = ds1.Tables[0];
            this.ddlprojectname.DataBind();

        }

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetCompCode();
                string formdat = this.txtfrmdate.Text;
                string tomdat = this.txttodate.Text;
                string prjname = this.ddlprojectname.SelectedValue =="000000000000" ? "%%" : this.ddlprojectname.SelectedValue + "%";
                DataSet ds = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "GETPURCHASEPRJWISE", formdat, tomdat, prjname, "", "", "", "");
                if (ds == null)
                    return;

                ViewState["tblpurchasesummary"] = ds.Tables[0];
                this.Load_GridView();




            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }


        private void GetProjectDetails()
        {
            try
            {
                string comcod = this.GetCompCode();
                string formdat = this.txtfrmdate.Text;
                string tomdat = this.txttodate.Text;
                string mRptGroup = "12";
                string pactcode = Request.QueryString["pactcode"].ToString();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS","RPTPURSUMMARY", formdat, tomdat, pactcode, mRptGroup, "", "", "", "", "");

                if (ds1 == null)
                    return;
                ViewState["tblpurchasesummarydetails"]= ds1.Tables[0];
                this.Details_Load();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        private void Details_Load()
        {

            DataTable tbl011 = (DataTable)ViewState["tblpurchasesummarydetails"];
            this.gvPurSum.DataSource = tbl011;
            this.gvPurSum.DataBind();
            //this.FooterCalculationDetails(tbl011);
        }


        //private void FooterCalculationDetails(DataTable tbl011)
        //{
        //    if (tbl011.Rows.Count == 0)
        //        return;
        //    ((Label)this.gv_PurchesSummary.FooterRow.FindControl("lgvFAmtS")).Text = Convert.ToDouble((Convert.IsDBNull(tbl011.Compute("sum(amt)", "")) ? 0.00 :
        //      tbl011.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0); ") ;
        //}

        private void Load_GridView()
        {
            DataTable tbl1 = (DataTable)ViewState["tblpurchasesummary"];
            this.gv_PurchesSummary.DataSource = tbl1;

            this.gv_PurchesSummary.DataBind();
            this.FooterCalculation(tbl1);
        }

        private void FooterCalculation(DataTable tbl1)
        {
            if (tbl1.Rows.Count == 0)
                return;

            ((Label)this.gv_PurchesSummary.FooterRow.FindControl("tblsumAmount")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(amt)", "")) ? 0.00 :
                tbl1.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gv_PurchesSummary.FooterRow.FindControl("tblAmountper")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("sum(percntage)", "")) ? 0.00 :
               tbl1.Compute("sum(percntage)", ""))).ToString("#,##0;(#,##0); ") + " %";
        }


        protected void gv_PurchesSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_PurchesSummary.PageIndex = e.NewPageIndex;
            this.Load_GridView();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = hst["comcod"].ToString();
                string comnam = hst["comnam"].ToString();
                string compname = hst["compname"].ToString();
                string comsnam = hst["comsnam"].ToString();
                string comadd = hst["comadd1"].ToString();
                string comfadd = hst["comadd"].ToString().Replace("<br />", "\n");
                string session = hst["session"].ToString();
                string username = hst["username"].ToString();
                string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
                string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
                string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
                string frmdate =Convert.ToDateTime( this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                DataTable tbl1 = (DataTable)ViewState["tblpurchasesummary"];

                LocalReport Rpt1 = new LocalReport();
                var list = tbl1.DataTableToList<RealEntity.C_14_Pro.EClassPur.RptPurchasePrjwise>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_14_Pro.RptPurchasePrjwiseReport", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comfadd", comfadd));
                Rpt1.SetParameters(new ReportParameter("compname", comnam));
                Rpt1.SetParameters(new ReportParameter("Title", "Purchase Summary Project Wise"));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
                Rpt1.SetParameters(new ReportParameter("DateTime", "Form " + frmdate + " To " + todate));


                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gv_PurchesSummary.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.Load_GridView();
        }

        protected void gv_PurchesSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkbtndetailslink");
               
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString().Trim();
                
                hlink.NavigateUrl = "~/F_14_Pro/RptPurchesStatusPrjWise.aspx?Type=Details&pactcode=" + pactcode;
                
            }
        }
    }
}