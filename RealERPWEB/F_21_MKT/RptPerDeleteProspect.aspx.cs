using Microsoft.Reporting.WinForms;
using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class RptPerDeleteProspect : System.Web.UI.Page
    {
        ProcessAccess accessData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length==0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string txtDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01-"+Convert.ToDateTime(txtDate).ToString("MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

                this.lnkbtnOk_Click(null, null);
            }            
        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string txtFrmDate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string txtToDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = accessData.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "PER_DELETE_PROSPECT", txtFrmDate, txtToDate, "", "", "", "", "", "", "", "");
            if (ds1==null)
                return;

            ViewState["tblperdelpros"] = ds1.Tables[0];
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblperdelpros"];
            this.gvPerDelProspect.DataSource=dt;
            this.gvPerDelProspect.DataBind();

            if (gvPerDelProspect.Rows.Count > 0)
            {
                Session["Report1"] = gvPerDelProspect;
                ((HyperLink)this.gvPerDelProspect.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }

        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvPerDelProspect.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.Data_Bind();
        }

        protected void gvPerDelProspect_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPerDelProspect.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string session = hst["session"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string compLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)ViewState["tblperdelpros"];
            string txtDate = "( From "+ Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " +Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + " )";

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_21_Mkt.ECRMClientInfo.RptProspectWorking>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_21_MKT.RptProspectWorking", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("compLogo", compLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "WORKING REPORT"));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate, session)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_self');</script>";
        }
    }
}