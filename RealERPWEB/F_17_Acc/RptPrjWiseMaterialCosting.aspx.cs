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
namespace RealERPWEB.F_17_Acc
{

    public partial class RptPrjWiseMaterialCosting : System.Web.UI.Page
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

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Wise Material Costing";
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtFDate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.GetProjectName();

            }

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetProjectName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtSProject = "%%";
            DataSet ds1;

            ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetResource();


        }

        private void GetResource()
        {
            string comcod = this.GetCompCode();
            string SrchResource = "%" + this.txtSrcResource.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_05", "RPTMATRATERESOURCE01", SrchResource, "", "", "", "", "", "", "", "");
            this.ddlResource.DataTextField = "sirdesc";
            this.ddlResource.DataValueField = "sircode";
            this.ddlResource.DataSource = ds1.Tables[0];
            this.ddlResource.DataBind();
            ds1.Dispose();


        }

        protected void imgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnFindResource_Click(object sender, EventArgs e)
        {
            this.GetResource();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {

            Session.Remove("tblprjcosting");
            string comcod = this.GetCompCode();
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string pactcode = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "16" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string ResCode = ((this.ddlResource.SelectedValue.ToString() == "000000000000") ? "01" : this.ddlResource.SelectedValue.ToString()) + "%";

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETPRJWISEMATCOSTING", fromdate, todate, pactcode, ResCode, "", "", "", "", "");
            if (ds1.Tables[0] == null)
            {
                this.gvprjcosting.DataSource = null;
                this.gvprjcosting.DataBind();
                return;
            }
            //DataTable dt01 = ds1.Tables[0].Copy();
            //Session["tblprjcosting01"] = dt01;
            if (ds1.Tables[0].Rows.Count == 0)
                return;
            DataTable dt1 = this.HiddenSameDate(ds1.Tables[0]);
            Session["tblprjcosting"] = ds1.Tables[0];
            this.Data_Bind();
        }


        private DataTable HiddenSameDate(DataTable dt1)
        {

            string actcode = dt1.Rows[0]["actcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["actcode"].ToString() == actcode)
                {
                    actcode = dt1.Rows[j]["actcode"].ToString();
                    dt1.Rows[j]["actdesc"] = "";
                }

                else
                    actcode = dt1.Rows[j]["actcode"].ToString();
            }

            return dt1;
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblprjcosting"];
            this.gvprjcosting.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvprjcosting.DataSource = dt;
            this.gvprjcosting.DataBind();
            this.FooterCalculation();


        }

        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["tblprjcosting"];
            if (dt1.Rows.Count == 0)
                return;
            ((Label)this.gvprjcosting.FooterRow.FindControl("lblFgvamount")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(clsamt)", "")) ?
                               0 : dt1.Compute("sum(clsamt)", ""))).ToString("#,##0;(#,##0); ");

            Session["Report1"] = gvprjcosting;
            ((HyperLink)this.gvprjcosting.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


        }





        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvprjcosting_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvprjcosting.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string fromdate = Convert.ToDateTime(this.txtFDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMMM-yyyy");
            string date1 = "From " + fromdate + " To " + todate;

            DataTable dt = (DataTable)Session["tblprjcosting"];

            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassFinanStatement.PrjWiseMaterialCost>();
            LocalReport rpt = new LocalReport();

            rpt = RptSetupClass1.GetLocalReport("R_17_Acc.RptPrjWiseMaterialCosting", list, null, null);
            rpt.EnableExternalImages = true;
            rpt.SetParameters(new ReportParameter("comName", comnam));
            rpt.SetParameters(new ReportParameter("txtTitle", "Project Wise Material Costing"));
            rpt.SetParameters(new ReportParameter("date1", date1));
            rpt.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            rpt.SetParameters(new ReportParameter("comLogo", ComLogo));

            Session["Report1"] = rpt;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
    }
}

