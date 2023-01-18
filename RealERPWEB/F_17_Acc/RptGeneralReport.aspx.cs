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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptGeneralReport : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "General Requistion Report";
                //this.Master.Page.Title = "General Bill Requisition";
                this.txtDatfrom.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDatfrom.Text = "01" + this.txtDatfrom.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtDatfrom.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

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
            this.ShowGenBill();
        }

        private void ShowGenBill()
        {
            Session.Remove("tblconsddetails");
            string comcod = this.GetCompCode();
            string date1 = Convert.ToDateTime(this.txtDatfrom.Text).ToString("dd-MMM-yyyy");
            string date2 = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string appoval = (this.rbtappoval.SelectedIndex == 0) ? "firstapp" : (this.rbtappoval.SelectedIndex == 1) ? "finalapp" : "";
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_BGD", "RPTGENERALREQ", date1, date2, appoval, "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblgenreq"] = ds1.Tables[0]; //ds1.Tables[0];    
            this.Data_Bind();

        }


        private DataTable HiddenSameData(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return dt;

            string pactcode = dt.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (dt.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt.Rows[j]["pactcode"].ToString();
                    dt.Rows[j]["pactdesc"] = "";
                }

                else
                    pactcode = dt.Rows[j]["pactcode"].ToString();
            }

            return dt;
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblgenreq"];
            this.gvgenreq.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvgenreq.DataSource = dt;
            this.gvgenreq.DataBind();
            this.FooterCalculation(dt);
        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)

                return;

            else
            {


                ((Label)this.gvgenreq.FooterRow.FindControl("lgvFreqamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(proamt)", "")) ? 0.00 :
                     dt.Compute("sum(proamt)", ""))).ToString("#,##0;(#,##0); ");
                ((Label)this.gvgenreq.FooterRow.FindControl("lgvFlgvreqamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(appamt)", "")) ? 0.00 :
               dt.Compute("sum(appamt)", ""))).ToString("#,##0;(#,##0); ");

                ((Label)this.gvgenreq.FooterRow.FindControl("lgvFlgvpayamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payment)", "")) ? 0.00 :
               dt.Compute("sum(payment)", ""))).ToString("#,##0;(#,##0); ");



            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            //Iqbal Nayan
            string comcod = this.GetCompCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            LocalReport Rpt1 = new LocalReport();
            DataTable billlist = (DataTable)ViewState["tblgenreq"];

            string header = "General Bill Requisition";
            var tblreq = billlist.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.EClassGenReq>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptGeneralBill", tblreq, null, null);

            string date1 = Convert.ToDateTime(this.txtDatfrom.Text).ToString("dd-MMM-yyyy");
            string date2 = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            string date = "From " + date1 + " To " + date2;
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("print", printFooter));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void gvgenreq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvgenreq.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
    }
}