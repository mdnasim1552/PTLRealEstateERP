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
namespace RealERPWEB.F_14_Pro
{


    public partial class RptSupplierDueStatus : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                // ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Due Status";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");


                this.GetSupliersName();

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

        private void GetSupliersName()
        {

            string comcod = this.GetComeCode();
            string txtSProject = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "GETSUPORCONNAME02", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlSupplierName.DataTextField = "sirdesc";
            this.ddlSupplierName.DataValueField = "sircode";
            this.ddlSupplierName.DataSource = ds1.Tables[0];
            this.ddlSupplierName.DataBind();

        }

        private void ShowSupDueStatus()
        {

            Session.Remove("tblstatus");
            string comcod = this.GetComeCode();

            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            //string SupCode = (this.ddlSupplierName.SelectedValue.ToString() == "000000000000") ? "99%" : this.ddlSupplierName.SelectedValue.ToString() + "%";

            string SupCode = this.ddlSupplierName.Text.Trim();

            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTBILLINF", SupCode, todate, "", "", "", "", "", "", "");

            //DataTable dt1 = this.HiddenSameData(ds1.Tables[0]);
            Session["tblstatus"] = ds1.Tables[0];
            this.Data_Bind();


        }

        private void Data_Bind()
        {

            this.gvSupDueStatus.DataSource = (DataTable)Session["tblstatus"];
            this.gvSupDueStatus.DataBind();
            this.FooterTotal();

        }



        private void FooterTotal()
        {
            DataTable dt = (DataTable)Session["tblstatus"];

            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvSupDueStatus.FooterRow.FindControl("lgvFgvInvAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                 dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSupDueStatus.FooterRow.FindControl("lgvFInvAmtp")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(paidamt)", "")) ? 0.00 :
                dt.Compute("sum(paidamt)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvSupDueStatus.FooterRow.FindControl("lgvFUnpAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(unpaid)", "")) ? 0.00 :
                dt.Compute("sum(unpaid)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            this.PrintShowSupLwMat();
        }

        private void PrintShowSupLwMat()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

            DataTable dt = (DataTable)Session["tblstatus"];

            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_14_Pro.RptSuppDueStatus>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_14_Pro.RptSupplierDueStatus", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("txtSupName", this.ddlSupplierName.SelectedItem.Text.Substring(13)));
            Rpt1.SetParameters(new ReportParameter("txtDate", todate));

            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetComeCode();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();

            //string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //ReportDocument rptstk = new RealERPRPT.R_14_Pro.RptSupplierDueStatus();

            //TextObject txtComName = rptstk.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //txtComName.Text = comnam;
            //TextObject txtSupName = rptstk.ReportDefinition.ReportObjects["txtSupName"] as TextObject;
            ////txtSupName.Text = SupCode.Substring(12,20);
            //txtSupName.Text = this.ddlSupplierName.SelectedItem.Text.Substring(13);

            ////txtSupName.Text = SupCode.Substring(SupCode.Length - 24);


            //TextObject txtAsonDate = rptstk.ReportDefinition.ReportObjects["txtAsonDate"] as TextObject;
            //txtAsonDate.Text = todate;

            //DataTable dt = (DataTable)Session["tblstatus"];
            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstk.SetDataSource(dt);


            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowSupDueStatus();

        }
        protected void imgbtnFindSupplier_Click(object sender, EventArgs e)
        {
            this.GetSupliersName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void gvWorkOrdHisRes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvSupDueStatus.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
    }
}