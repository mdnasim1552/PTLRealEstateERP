using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptMonthSupllBill : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        public static double dramt, cramt, opnamt, clsamt, balamt, isuamt, reconamt;
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

                //((Label)this.Master.FindControl("lblTitle")).Text = "Monthly Supplier Bill";
                //this.Master.Page.Title = "Monthly Supplier Bill";
                this.txtDateFrom.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtDateto.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            DataTable dt = (DataTable)Session["tblsupbill"];

            string date = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";
            string rpttitle = "Monthly Supplier Bill";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptMontlySupplierBill>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptMonthlySuppBill", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));

            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


            //ReportDocument rptsl = new RealERPRPT.R_17_Acc.rptMonthSuppbill();
            //TextObject txtCompany = rptsl.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtRptHead = rptsl.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtRptHead.Text = "Monthly Supplier Bill";


            //TextObject txtdate = rptsl.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "(From " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy") + ")";

            //TextObject txtuserinfo = rptsl.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptsl.SetDataSource((DataTable)Session["tblsupbill"]);

            //Session["Report1"] = rptsl;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkbtnok2_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.showMonthSuppInfo();
        }
        private void showMonthSuppInfo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frmdate = Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETMONTHSUPPBILL", frmdate, todate, "", "", "", "", "", "", "");

            Session["tblsupbill"] = ds2.Tables[0];
            this.Data_Bind();



        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblsupbill"];
            this.grvMSuppBill.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue);
            this.grvMSuppBill.DataSource = dt;
            this.grvMSuppBill.DataBind();
            ((Label)this.grvMSuppBill.FooterRow.FindControl("lblttlBill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(sbillamt)", "")) ?
                           0 : dt.Compute("sum(sbillamt)", ""))).ToString("#,##0;(#,##0); ");

            ((Label)this.grvMSuppBill.FooterRow.FindControl("lblttlTax")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ttaxamt)", "")) ?
                           0 : dt.Compute("sum(ttaxamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvMSuppBill.FooterRow.FindControl("lblFpbillamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(pbillamt)", "")) ?
                           0 : dt.Compute("sum(pbillamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvMSuppBill.FooterRow.FindControl("lblttlNetbill")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netbill)", "")) ?
                           0 : dt.Compute("sum(netbill)", ""))).ToString("#,##0;(#,##0); ");

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void grvMSuppBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvMSuppBill.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


    }
}
