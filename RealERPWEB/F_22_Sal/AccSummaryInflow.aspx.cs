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
using System.IO;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
using RealEntity.C_22_Sal;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_22_Sal
{
    public partial class AccSummaryInflow : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
        SalesInvoice_BL GetCompinf = new SalesInvoice_BL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project Summary Infow ";
                this.GetCompcodeERP();
                this.GetProjectNameERP();
                lbtnOk_Click(null, null);
            }


        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
        }

        protected void ddlcomplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectNameERP();

        }

        private void GetCompcodeERP()
        {
            try
            {
                string comcod = this.GetCompCode();
                string calltype = "GETERPCOMCODERP";

                var lst = GetCompinf.GetAllCompData(comcod, calltype);
                this.ddlcomplist.DataTextField = "compname";
                this.ddlcomplist.DataValueField = "comcod";
                this.ddlcomplist.DataSource = lst;
                this.ddlcomplist.DataBind();

                if (comcod == "3348")
                {
                    this.ddlcomplist.SelectedValue = comcod;
                    this.ddlcomplist.Enabled = false;
                }
                 
            }
            catch (Exception)
            {
            }
        }
        private void GetProjectNameERP()
        {
            try
            {
                string comcod = this.ddlcomplist.SelectedValue.ToString();

                string calltype = "GETERPPRJLIST_MAP";

                var lst = GetCompinf.GetProjectList(comcod, calltype);


                this.ddlProjectName.DataTextField = "actdesc";
                this.ddlProjectName.DataValueField = "actcode";
                this.ddlProjectName.DataSource = lst;
                this.ddlProjectName.DataBind();
            }
            catch (Exception)
            {
            }

        }
        private string GetCompCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetPrjCustomerERP();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.ddlcomplist.SelectedValue.ToString();
            //string prjcod = this.ddlProjectName.SelectedValue.ToString();
            string prjcod = this.ddlProjectName.SelectedValue.ToString() == "0" ? "18%" : this.ddlProjectName.SelectedValue.ToString();
            string calltype = "";

            if (comcod == "3348")
            {
                calltype = "GETSUMMARYOFINFLOW_CRD";
            }
            else
            {
                calltype = "GETSUMMARYOFINFLOW";
            }


            string tdate = this.txtDatefrom.Text.ToString();
            var lst = GetCompinf.GetERPSumOFInflow(comcod, prjcod, calltype);
            if (lst.Count <= 0)
            {
                return;
            }

            Session["tblList"] = lst;

            this.grvacc.DataSource = lst;
            this.grvacc.DataBind();



            ((Label)this.grvacc.FooterRow.FindControl("lgvtpauamt")).Text = (lst.Select(p => p.uamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.uamt).Sum().ToString("#,##0;(#,##0); "); ;
            ((Label)this.grvacc.FooterRow.FindControl("lgvtpaidamt")).Text = (lst.Select(p => p.paidamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.paidamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvacc.FooterRow.FindControl("lgvtrecvbale")).Text = (lst.Select(p => p.recvbale).Sum() == 0.00) ? "0.00" : lst.Select(p => p.recvbale).Sum().ToString("#,##0;(#,##0); ");

            ((Label)this.grvacc.FooterRow.FindControl("lgvttaaccuamt")).Text = (lst.Select(p => p.accuamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.accuamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvacc.FooterRow.FindControl("lgvttaccpaidamt")).Text = (lst.Select(p => p.accpaidamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.accpaidamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvacc.FooterRow.FindControl("lgvttaccrecvbale")).Text = (lst.Select(p => p.accrecvbale).Sum() == 0.00) ? "0.00" : lst.Select(p => p.accrecvbale).Sum().ToString("#,##0;(#,##0); ");


            ((Label)this.grvacc.FooterRow.FindControl("lgvttlsolamt")).Text = (lst.Select(p => p.ttlsolamt).Sum() == 0.00) ? "0.00" : lst.Select(p => p.ttlsolamt).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvacc.FooterRow.FindControl("lgvttsolrecv")).Text = (lst.Select(p => p.ttsolrecv).Sum() == 0.00) ? "0.00" : lst.Select(p => p.ttsolrecv).Sum().ToString("#,##0;(#,##0); ");
            ((Label)this.grvacc.FooterRow.FindControl("lgvttldue")).Text = (lst.Select(p => p.ttldue).Sum() == 0.00) ? "0.00" : lst.Select(p => p.ttldue).Sum().ToString("#,##0;(#,##0); ");


        }
        private void GetPrjCustomerERP()
        {
            try
            {
                //string comcod = this.ddlcomplist.SelectedValue.ToString();
                //string prjcod = this.ddlProjectName.SelectedValue.ToString();
                //string calltype = "GETSUMMARYOFINFLOW";
                //var lst = GetCompinf.GetERPCustList(comcod, prjcod, calltype);

                //this.ddlcustomerlist.DataTextField = "custdesc";
                //this.ddlcustomerlist.DataValueField = "usircode";
                //this.ddlcustomerlist.DataSource = lst;
                //this.ddlcustomerlist.DataBind();
            }
            catch (Exception)
            {
            }

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
            string date = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string session = hst["session"].ToString();
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;


            string projName = this.ddlProjectName.SelectedItem.ToString();
            string txtTitle = hst["username"].ToString();
            //DataTable dt = (DataTable)Session["billstatus"];
            //var rptlist = dt.DataTableToList<RealEntity.C_22_Sal.EClassSales_02.EclassSunOfFlow>();

            var dtlist = (List<RealEntity.C_22_Sal.EClassSales_02.EclassSunOfFlow>)Session["tblList"];

            LocalReport Rpt1a = new LocalReport();
            Rpt1a = RealERPRDLC.RptSetupClass1.GetLocalReport("R_22_Sal.RptAccSummaryInflow", dtlist, null, null);
            Rpt1a.SetParameters(new ReportParameter("comname", comnam));
            Rpt1a.SetParameters(new ReportParameter("txtComAddress", comadd));
            Rpt1a.SetParameters(new ReportParameter("txtProjName", projName));
            Rpt1a.SetParameters(new ReportParameter("txtTitle", "SUMMARAY OF INFLOW"));
            Rpt1a.SetParameters(new ReportParameter("txtDate", "Date : " + date));
            Rpt1a.SetParameters(new ReportParameter("printFooter", printFooter));

            Session["Report1"] = Rpt1a;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                       ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}