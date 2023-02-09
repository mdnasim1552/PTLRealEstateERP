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

    public partial class AccPendSubconBill : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Pending Contractor Bill";
                //this.Master.Page.Title = "Pending Contractor Bill";
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.imgbtnFindAccount_Click(null, null);
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            string date = this.txtdate.Text.Substring(0, 11);
            string concod = "";
            string suncan = this.ddlSubCon.SelectedValue.Trim().ToString();
            if (suncan == "000000000000")
            {
                concod = "%%";
            }
            else { concod = suncan; }


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "PANDINGCONBILL", date, concod, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }
            DataTable tbl1 = (DataTable)ds2.Tables[0];
            dgvConbill.DataSource = tbl1;
            dgvConbill.DataBind();

            ((Label)this.dgvConbill.FooterRow.FindControl("txtTgvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(ds2.Tables[0].Compute("Sum(amt)", "")) ?
                      0.00 : ds2.Tables[0].Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); - ");
            Session["tblpaneing"] = (DataTable)ds2.Tables[0];
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string curvoudat = this.txtdate.Text.Substring(0, 11);

            DataTable dt = (DataTable)Session["tblpaneing"];

            string date = "As on Date: " + (Convert.ToDateTime(this.txtdate.Text)).ToString("dd-MMM-yyyy");

            string rpttitle = "Pending Sub-Contractor Bill";

            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.EClassPendingConBill>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptPendingConBill", lst, null, null);
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






            //ReportDocument rptinfo = new ReportDocument();
            //rptinfo = new RealERPRPT.R_17_Acc.RptPendContractorBill();


            //TextObject txtCompanyName = rptinfo.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompanyName.Text = comnam;

            //TextObject date = rptinfo.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //date.Text = "As on Date: " + (Convert.ToDateTime(this.txtdate.Text)).ToString("dd-MMM-yyyy");

            //TextObject txtuserinfo = rptinfo.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptinfo.SetDataSource((DataTable)Session["tblpaneing"]);

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptinfo.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptinfo;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string filter = "%" + this.txtserceacc.Text + "%";
            string ctype = "98%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODETRAN", "GETSUPSUBCOD", filter, ctype, "", "", "", "", "", "", "");
            this.ddlSubCon.DataSource = ds1.Tables[0];
            this.ddlSubCon.DataTextField = "sirdesc1";
            this.ddlSubCon.DataValueField = "sircode";
            this.ddlSubCon.DataBind();
        }
    }
}