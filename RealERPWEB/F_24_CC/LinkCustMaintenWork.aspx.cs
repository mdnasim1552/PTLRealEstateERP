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
namespace RealERPWEB.F_24_CC
{
    public partial class LinkCustMaintenWork : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        public static bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ShowAdWork();


            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void ShowAdWork()
        {

            Session.Remove("tbladwork");
            string comcod = this.GetCompCode();

            string mAdNo = this.Request.QueryString["adno"].ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETADINFO", mAdNo, "",
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tbladwork"] = ds1.Tables[0];
            this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["adno1"].ToString().Substring(6, 5);
            this.lblDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["addate"]).ToString("dd-MMM-yyyy");
            this.lblProjectdesc.Text = this.Request.QueryString["pactdesc"].ToString();
            this.lblUnitName.Text = this.Request.QueryString["unitdesc"].ToString();
            this.Data_DataBind();



        }
        private void Data_DataBind()
        {

            this.gvAddWork.DataSource = (DataTable)Session["tbladwork"];
            this.gvAddWork.DataBind();
            this.FooterCalculation((DataTable)Session["tbladwork"]);

        }

        private void FooterCalculation(DataTable dt)
        {
            ((Label)this.gvAddWork.FooterRow.FindControl("lgvFAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ? 0.00 :
                 dt.Compute("sum(amt)", ""))).ToString("#,##0.00;(#,##0.00); ");


        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string projectName = this.Request.QueryString["pactdesc"].ToString();

            string unitName = this.Request.QueryString["unitdesc"].ToString();

            DataTable dt = (DataTable)Session["tbladwork"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_24_CC.EClassAddwork.AddWorkCus>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_24_CC.RptMaintenanceWrk", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "CLIENT'S MODIFICATION"));
            Rpt1.SetParameters(new ReportParameter("projectName", projectName));
            Rpt1.SetParameters(new ReportParameter("unitName", unitName));
            Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + Convert.ToDateTime(this.lblDate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtAddNo", "Modification No: " + this.lblCurNo1.Text.ToString().Trim() + "-" + this.lblCurNo2.Text.ToString().Trim()));
            Rpt1.SetParameters(new ReportParameter("txtNarration", ""));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }


        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void ibtnFindUnitName_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}