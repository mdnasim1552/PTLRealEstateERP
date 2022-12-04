using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Configuration;
using System.Collections;
using RealERPLIB;
using RealERPRDLC;
using RealERPRPT;
namespace RealERPWEB.F_14_Pro
{
    public partial class RptOrderSummary : System.Web.UI.Page
    {
        ProcessAccess BgdData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "PO MRR & Bill Status";
            
                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetSupplierName();
                this.GetProjectName();


            }
        }
        private void GetSupplierName()
        {

            string comcod = this.GetCompCode();
         
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETSUPPLIERlIST", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlSubName.DataTextField = "ssirdesc";
            this.ddlSubName.DataValueField = "ssircode";
            this.ddlSubName.DataSource = ds1.Tables[0];
            this.ddlSubName.DataBind();
          



        }
        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
          
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETPROJECTNAME01", "%%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.ShowBillStatus();

        }

        private void ShowBillStatus()
        {
            //Session.Remove ("tblconsddetails");
            string comcod = this.GetCompCode();
            string PactCode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "16%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            string SupplierName = (this.ddlSubName.SelectedValue.ToString() == "000000000000") ? "99%" : this.ddlSubName.SelectedValue.ToString() + "%";
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETWRKORDERSUMMARY",frmdate, todate, PactCode, SupplierName, "", "", "");
            if (ds1 == null)
                return;
            Session["tblBil"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }
            }
            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblBil"];
            this.gvBillStatus.DataSource = dt;
            this.gvBillStatus.DataBind();
            this.FooterCalculation(dt);

        }
        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;


            ((Label)this.gvBillStatus.FooterRow.FindControl("fgvPOamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ordamt)", "")) ? 0.00 :
                 dt.Compute("sum(ordamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvBillStatus.FooterRow.FindControl("fgvMRRamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(mrramt)", "")) ? 0.00 :
                 dt.Compute("sum(mrramt)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.gvBillStatus.FooterRow.FindControl("fgvInvoice")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(billamt)", "")) ? 0.00 :
                 dt.Compute("sum(billamt)", ""))).ToString("#,##0.00;(#,##0.00); ");

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
     
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;

           

            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;

            DataTable dt = (DataTable)Session["tblBil"];
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassRptPOMRRBillStatus>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptPOMRRBillStatus", list, null, null);

            Rpt1.EnableExternalImages = true;
           

            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "PurChase Status -  Supplier Wise"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lgvpoamt_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string projectcode = ((Label)this.gvBillStatus.Rows[index].FindControl("lblprjcode")).Text.ToString();
            string suppliercode = ((Label)this.gvBillStatus.Rows[index].FindControl("lblsupcode")).Text.ToString();
            DataSet ds1 = BgdData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS02", "GETWRKORDERDETAILS", frmdate, todate, projectcode, suppliercode, "", "", "");
            if (ds1 == null)
                return;
            Session["tblPO"]  = ds1.Tables[0];
            DataTable dt = (DataTable)Session["tblPO"];
            this.gvpobill.DataSource = dt;
            this.gvpobill.DataBind();
          

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenPOamt();", true);
        }
        protected void lgvmrramt_Click(object sender, EventArgs e)
        {

        }

        protected void lgvbillamt_Click(object sender, EventArgs e)
        {

        }

        protected void Close_Click(object sender, EventArgs e)
        {
            this.lbtnOk_OnClick(null,null);

        }
    }
}