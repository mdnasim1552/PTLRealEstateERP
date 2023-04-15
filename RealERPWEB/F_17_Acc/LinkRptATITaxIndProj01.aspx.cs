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
namespace RealERPWEB.F_17_Acc
{

    public partial class LinkRptATITaxIndProj01 : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "TDS VDS SD Deduction Supplier Wise  Details";
                this.Master.Page.Title = "TDS VDS SD Deduction Supplier Wise  Details";
                string Date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.LoadSupplierData();
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

        private void LoadSupplierData()
        {
            string comcod = this.GetCompCode();
            string pactcode = this.Request.QueryString["pactcode"].ToString();
            string rescode = this.Request.QueryString["rescode"].ToString();
            string ssircode = this.Request.QueryString["ssircode"].ToString();
            string frmdate = this.Request.QueryString["frmdate"].ToString();
            string todate = this.Request.QueryString["todate"].ToString();


            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTINDSUPPTVSDETAILS", pactcode, rescode, frmdate, todate, ssircode, "", "", "", "");
            if (ds1 == null)
            {
                this.gvsupdetails.DataSource = null;
                this.gvsupdetails.DataBind();
                return;
            }

            Session["tblsupdetails"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            string rescode = dt1.Rows[0]["rescode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                if (dt1.Rows[j]["rescode"].ToString() == rescode)
                {
                    rescode = dt1.Rows[j]["rescode"].ToString();
                    dt1.Rows[j]["resdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    rescode = dt1.Rows[j]["rescode"].ToString();
                }

            }

            return dt1;
        }


        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblsupdetails"];
            this.gvsupdetails.DataSource = dt;
            this.gvsupdetails.DataBind();
            this.FooterCalculations();


        }

        private void FooterCalculations()
        {
            DataTable dt = (DataTable)Session["tblsupdetails"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvsupdetails.FooterRow.FindControl("lgvFopen")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opam)", "")) ?
            0 : dt.Compute("sum(opam)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvsupdetails.FooterRow.FindControl("lgvFPayment")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cram)", "")) ?
            0 : dt.Compute("sum(cram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvsupdetails.FooterRow.FindControl("lgvFDeposit")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dram)", "")) ?
            0 : dt.Compute("sum(dram)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvsupdetails.FooterRow.FindControl("lgvFNetamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netamt)", "")) ?
             0 : dt.Compute("sum(netamt)", ""))).ToString("#,##0.00;(#,##0.00); ");



            Session["Report1"] = gvsupdetails;
            ((HyperLink)this.gvsupdetails.HeaderRow.FindControl("hlbtntbCdataExcel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";

        }

        ///F_17_Acc/RptAccVouher.aspx?vounum=BD202110000022



        protected void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string session = hst["session"].ToString();

            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt1 = (DataTable)Session["tblsupdetails"];

            //string projname = "Project Name: " + ddlProj.SelectedItem.Text.Substring(13);
            //string date = "Date: " + Convert.ToDateTime(this.txtDateFrom.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txtDateto.Text).ToString("dd-MMM-yyyy");
            string rpttitle = "TDS VDS SD Deduction Supplier/Subcontractor Wise Details";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            var lst = dt1.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.TdsVdsSdDeducSubjWise>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.LinkRptATITaxIndProj01", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", rpttitle));
            //Rpt1.SetParameters(new ReportParameter("projname", projname));
            //Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void gvsupdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlnkvounum = (HyperLink)e.Row.FindControl("hlnkvounum");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                hlnkvounum.NavigateUrl = "~/F_17_Acc/RptAccVouher.aspx?vounum=" + vounum;
            }

        }
    }
}