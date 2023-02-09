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
    public partial class EntryHeadOfficeOvhAllocation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Overhead Allocation";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetOpeningDate();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }



        private string GetComcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetOpeningDate()
        {

            string date = "";
            string comcod = this.GetComcod();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS04", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                date = Convert.ToDateTime(System.DateTime.Today).ToString("dd-MMM-yyyy");
                this.txtFromdate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy"); ;
                this.txtTodate.Text = Convert.ToDateTime(this.txtFromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                return;
            }

            date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["ovhodate"]).AddDays(1).ToString("dd-MMM-yyyy");
            this.txtFromdate.Text = Convert.ToDateTime(date).ToString("dd-MMM-yyyy"); ;
            this.txtTodate.Text = Convert.ToDateTime(this.txtFromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            this.txtFromdate.ReadOnly = true;


            ds1.Dispose();


        }



        //private DataTable HiddenSameData(DataTable dt1)
        //{

        //    string pactcode = dt1.Rows[0]["pactcode"].ToString();
        //    string rsircode = dt1.Rows[0]["rsircode"].ToString();

        //    for (int j = 1; j < dt1.Rows.Count; j++)
        //    {
        //        if (dt1.Rows[j]["pactcode"].ToString() == pactcode && dt1.Rows[j]["rsircode"].ToString() == rsircode)
        //        {
        //            pactcode = dt1.Rows[j]["pactcode"].ToString();
        //            rsircode = dt1.Rows[j]["rsircode"].ToString();
        //            dt1.Rows[j]["pactdesc"] = "";
        //            dt1.Rows[j]["rsirdesc"] = "";
        //        }

        //        else
        //        {



        //            if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
        //            {
        //                dt1.Rows[j]["pactdesc"] = "";
        //            }

        //            if (dt1.Rows[j]["rsircode"].ToString() == rsircode)
        //            {
        //                dt1.Rows[j]["rsirdesc"] = "";

        //            }
        //            pactcode = dt1.Rows[j]["pactcode"].ToString();
        //            rsircode = dt1.Rows[j]["rsircode"].ToString();

        //        }

        //    }
        //    return dt1;


        //}



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            //Rdlc
            string date = "Period: " + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            int dateDife = ASTUtility.Datediffday(Convert.ToDateTime(this.txtTodate.Text), Convert.ToDateTime(this.txtFromdate.Text));
            int dateDife1 = dateDife + 1;
            string rpttxtDays = "Days : " + dateDife1.ToString();
            string txtBalance = "Balance as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string txtTotal = "Balane as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string txtDepr = "Depreciatoin as on " + Convert.ToDateTime(this.txtFromdate.Text).AddDays(-1).ToString("dd.MM.yyyy");
            string txtWD = "W.D Values as on " + Convert.ToDateTime(this.txtTodate.Text).ToString("dd.MM.yyyy");
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            DataTable dt = (DataTable)Session["tblDepcost"];

            var lst = dt.DataTableToList<RealEntity.C_29_Fxt.EClassFixedAsset.EClassDepricationCost>();


            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_29_Fxt.RptDepricationCharge", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Fixed Assets Schedule"));
            Rpt1.SetParameters(new ReportParameter("rpttxtDays", rpttxtDays));
            Rpt1.SetParameters(new ReportParameter("txtBalance", txtBalance));
            Rpt1.SetParameters(new ReportParameter("txtTotal", txtTotal));
            Rpt1.SetParameters(new ReportParameter("txtDepr", txtDepr));
            Rpt1.SetParameters(new ReportParameter("txtWD", txtWD));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));




            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";






        }






        protected void grDep_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grDep.PageIndex = e.NewPageIndex;
            this.grDep_DataBind();
        }

        private void grDep_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tbloverhead"];
            this.grDep.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grDep.DataSource = tbl1;
            this.grDep.DataBind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grDep_DataBind();
        }

        protected void lbtnShow_Click(object sender, EventArgs e)
        {
            Session.Remove("tbloverhead");
            this.pnlovhead.Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string frdate = Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_MIS04", "RPTPERHOOVERHEADALLOCATION", frdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.grDep.DataSource = null;
                this.grDep.DataBind();
                return;
            }

            Session["tbloverhead"] = (DataTable)ds1.Tables[0];
            DataTable dt1 = (DataTable)ds1.Tables[1];
            this.grDep_DataBind();

            if (dt1.Rows.Count > 0)
            {
                this.txttotalOv.Text = Convert.ToDouble(dt1.Rows[0]["trnam"]).ToString("#,##0.00;(#,##0.00); ");
            }


            this.FooterRowCal();
        }
        private void FooterRowCal()
        {
            DataTable dt = (DataTable)Session["tbloverhead"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.grDep.FooterRow.FindControl("lgvFcost")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(trnam)", "")) ?
                                   0 : dt.Compute("sum(trnam)", ""))).ToString("#,##0.00;(#,##0.00); ");

            ((Label)this.grDep.FooterRow.FindControl("lgvFPercent")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(percnt)", "")) ?
                                0 : dt.Compute("sum(percnt)", ""))).ToString("#,##0.00;(#,##0.00); ");



            ((HyperLink)this.grDep.FooterRow.FindControl("hlnkgvFdep")).NavigateUrl = "~/F_17_Acc/AccOverHeadJournal.aspx?&Date1=" + Convert.ToDateTime(this.txtFromdate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txtTodate.Text).ToString("dd-MMM-yyyy");
        }


    }
}