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
    public partial class Rptcostingcollection : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //  string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = date;

                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Cost and Sales";
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

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.GerReceColle();
        }

        private void GerReceColle()
        {
            ViewState.Remove("tblRecColle");
            string comcod = this.GetCompCode();
            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MIS01", "RPTPRCOSTACOLLECTION", txtdatefrm, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvRptcodtColle.DataSource = null;
                this.gvRptcodtColle.DataBind();
                return;
            }
            ViewState["tblRecColle"] = ds1.Tables[0];
            this.Data_Bind();
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblRecColle"];
            this.gvRptcodtColle.DataSource = dt;
            this.gvRptcodtColle.DataBind();
            this.FooterCalculationdet();
        }


        private void FooterCalculationdet()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["tblRecColle"];
                if (dt.Rows.Count == 0)
                    return;
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lglbfgvlCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(lcost)", "")) ?
                    0.00 : dt.Compute("Sum(lcost)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblfgvComCon")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(comcost)", "")) ?
                    0.00 : dt.Compute("Sum(comcost)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblgvDesiCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(dcost)", "")) ?
                    0.00 : dt.Compute("Sum(dcost)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblgvfConsCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bgdcons)", "")) ?
                    0.00 : dt.Compute("Sum(bgdcons)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblgvfCostDone")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(conscost)", "")) ?
                    0.00 : dt.Compute("Sum(conscost)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblgvfRemCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(remcons)", "")) ?
                    0.00 : dt.Compute("Sum(remcons)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lglblgvlCost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(saleamt)", "")) ?
                    0.00 : dt.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblgvfCollDone")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(collamt)", "")) ?
                    0.00 : dt.Compute("Sum(collamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvRptcodtColle.FooterRow.FindControl("lblgvfremainColl")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(remcollamt)", "")) ?
                    0.00 : dt.Compute("Sum(remcollamt)", ""))).ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.RptReceCollectionPrint();
        }

        private void RptReceCollectionPrint()
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
            string fromdat = this.txtfromdate.Text.Trim().ToString();
            DataTable dt = (DataTable)ViewState["tblRecColle"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassDB_BO.ProjCostSales>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptProjCostSales", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("date", "As On Date:" + fromdat));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Project Cost and Sales"));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
    }
}
