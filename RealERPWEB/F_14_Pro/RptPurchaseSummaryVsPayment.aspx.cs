﻿using System;
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
    public partial class RptPurchaseSummaryVsPayment : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
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
                //((Label)this.Master.FindControl("lblTitle")).Text = "Purshase Summary vs Payment";

                this.txtfrmdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01" + this.txtfrmdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            }
           

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        protected void lbtnOk_OnClick(object sender, EventArgs e)
        {
            this.ShowPurVsPay();

        }

        private void ShowPurVsPay()
        {
            //Session.Remove ("tblconsddetails");
            string comcod = this.GetCompCode();

            string frmdate = Convert.ToDateTime(this.txtfrmdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_REQ_STATUS", "RPTPURSUMMARYVSPAYMENT", frmdate, todate, "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tblpursum"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tblpursum"];
            this.gvpurvspay.DataSource = dt;
            this.gvpurvspay.DataBind();
            

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

            DataTable dt = (DataTable)Session["tblpursum"];
            var list = dt.DataTableToList<RealEntity.C_14_Pro.EClassPayment.EclassPurchasesummaryvspayment>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RDLCAccountSetup.GetLocalReport("R_14_Pro.RptPurSumVsPay", list, null, null);

            Rpt1.EnableExternalImages = true;


            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "SUMMARY OF PURCHASE"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}