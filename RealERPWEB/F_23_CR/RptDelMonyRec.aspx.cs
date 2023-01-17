using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Net.Mail;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_23_CR
{

    public partial class RptDelMonyRec : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtdate.Text = System.DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
                this.txtTodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "TRANSACTION STATEMENT  INFORMATION VIEW/EDIT";
            }



        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void BtnLodGreid_Click(object sender, EventArgs e)
        {
            LodGreidView();
        }

        private void LodGreidView()
        {
            Session.Remove("tblPayment");
            string comcod = this.GetComeCode();
            string fromdate = this.txtdate.Text.Substring(0, 11);
            string otdate = this.txtTodate.Text.Substring(0, 11);
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "DELETEDVOUCHER", fromdate, otdate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDelMonRec.DataSource = null;
                this.gvDelMonRec.DataBind();
                return;
            }
            Session["tblPayment"] = ds1.Tables[0];
            this.Data_Bind();

        }

        protected void gvDelMonRec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDelMonRec.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            this.gvDelMonRec.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDelMonRec.DataSource = (DataTable)Session["tblPayment"];
            this.gvDelMonRec.DataBind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //Session["tblPayment"]

            //Iqbal  Nayan
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
            DataTable dt = (DataTable)Session["tblPayment"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_23_CRR.EClassSales_03.TrnStatInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_23_CR.RptDelMonyRec", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Date", "From " + Convert.ToDateTime(this.txtdate.Text).ToString("dd MMM, yyyy") + " To " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd MMM, yyyy")));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Deleted Money Receipt"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}