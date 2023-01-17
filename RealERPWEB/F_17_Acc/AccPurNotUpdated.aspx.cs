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
using RealERPRDLC;
namespace RealERPWEB.F_17_Acc
{
    public partial class AccPurNotUpdated : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
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
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Purchase Not Yet Updated";
                //this.Master.Page.Title = "Purchase Not Yet Updated";
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
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;
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


            DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_PURCHASE01", "PANDINGPURBILL", date, concod, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count == 0)
            {
                return;
            }

            Session["tblpaneing"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();
        }


        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)Session["tblpaneing"];
            dgvPurbill.DataSource = tbl1;
            dgvPurbill.DataBind();
            ((Label)this.dgvPurbill.FooterRow.FindControl("txtTgvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(tbl1.Compute("Sum(amt)", "")) ?
                      0.00 : tbl1.Compute("Sum(amt)", ""))).ToString("#,##0;(#,##0); - ");

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescode = dt1.Rows[0]["ssircode"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["ssircode"].ToString() == rescode && dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {

                    dt1.Rows[j]["ssirdesc"] = "";
                    dt1.Rows[j]["pactdesc"] = "";

                }

                else
                {
                    if (dt1.Rows[j]["ssircode"].ToString() == rescode)
                        dt1.Rows[j]["ssirdesc"] = "";

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";








                }

                rescode = dt1.Rows[j]["ssircode"].ToString();
                pactcode = dt1.Rows[j]["pactcode"].ToString();



            }

            return dt1;
        }





        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string curvoudat = this.txtdate.Text.Substring(0, 11);
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblpaneing"];
            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.PurchaseNotYetUpdated>();

            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptPurNotUpdated", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtDate", "As on Date: " + (Convert.ToDateTime(this.txtdate.Text)).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Purchase Not Yet Updated"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void imgbtnFindAccount_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
            string filter = "%" + this.txtserceacc.Text + "%";
            string ctype = "99%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODETRAN", "GETSUPSUBCOD", filter, ctype, "", "", "", "", "", "", "");
            this.ddlSubCon.DataSource = ds1.Tables[0];
            this.ddlSubCon.DataTextField = "sirdesc1";
            this.ddlSubCon.DataValueField = "sircode";
            this.ddlSubCon.DataBind();
            this.ddlSubCon.SelectedValue = "000000000000";
        }
    }
}