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
namespace RealERPWEB.F_81_Hrm.F_87_Tra
{
    public partial class HREmpTransferReport : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.txtFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE TRANSFER INFORMATION";
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
            this.getEmployeeInfo();
        }

        private void getEmployeeInfo()
        {
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtFromDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtToDate.Text).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "[dbo_hrm].[SP_REPORT_EMPLOYEE_TRANSFER]", "GETEMPLOYEETRANSFER", frmdate, todate, "");
            if (ds1 == null)
            {
                this.gvEmpInfo.DataSource = null;
                this.gvEmpInfo.DataBind();
                return;
            }

            Session["tblemptransinfo"] = ds1.Tables[0];
            this.Data_Bind();
        }

        protected void Data_Bind()
        {
            this.gvEmpInfo.DataSource = (DataTable)Session["tblemptransinfo"];
            this.gvEmpInfo.DataBind();
        }


        protected void gvEmpInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvEmpInfo.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable tbl1 = (DataTable)Session["tblemptransinfo"];
            this.gvEmpInfo.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpInfo.DataSource = tbl1;
            this.gvEmpInfo.DataBind();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comadd = hst["comadd1"].ToString();

            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string fdate = Convert.ToDateTime(this.txtFromDate.Text.ToString()).ToString("dd-MMMM-yyyy");
            string todate = Convert.ToDateTime(this.txtToDate.Text.ToString()).ToString("dd-MMMM-yyyy");


            DataTable dt = (DataTable)Session["tblemptransinfo"];

            var prjList = dt.DataTableToList<RealEntity.C_81_Hrm.C_87_Tra.EmployeeTransInfo>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_87_Tra.RptHREmpTransferReport", prjList, null, null);
            Rpt1.SetParameters(new ReportParameter("compname", comnam));
            Rpt1.SetParameters(new ReportParameter("Rpttitle", "Employee Transfer Information"));
            Rpt1.SetParameters(new ReportParameter("date1", "From " + fdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, comnam, printdate)));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

    }

}
