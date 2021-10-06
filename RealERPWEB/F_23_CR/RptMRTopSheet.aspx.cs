﻿using System;
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
using RealERPRDLC;
namespace RealERPWEB.F_23_CR
{

    public partial class RptMRTopSheet : System.Web.UI.Page
    {

        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "Money Receipt Top Sheet";
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.lbtnOk_Click(null, null);

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
            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            string comcod = this.GetCompCode();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string refnum = "%" + this.txtrefno.Text.Trim() + "%";

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTMONEYRECEIPTTOPSHEET", frmdate, todate, refnum, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvAccVoucher.DataSource = null;
                this.gvAccVoucher.DataBind();
                return;
            }

            Session["tblunposted"] = ds1.Tables[0];
            Session["tblvoucount"] = ds1.Tables[1];
            Session["tblusrvoucount"] = ds1.Tables[2];


            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblunposted"];
            this.gvAccVoucher.DataSource = dt;
            this.gvAccVoucher.DataBind();
            this.FooterCalCulation();
        }

        private void FooterCalCulation()
        {
            DataTable dt = (DataTable)Session["tblunposted"];
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvAccVoucher.FooterRow.FindControl("lblgvFvouamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt)", "")) ?
            0 : dt.Compute("sum(amt)", ""))).ToString("#,##0;(#,##0);");
            Session["Report1"] = gvAccVoucher;
            ((HyperLink)this.gvAccVoucher.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



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
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string userinfo = ASTUtility.Concat(compname, username, printdate);
            DataTable dt = (DataTable)Session["tblunposted"];
            DataTable dt2 = (DataTable)Session["tblusrvoucount"];

            var list = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.VoutopSheet>();
            var list1 = dt2.DataTableToList<RealEntity.C_17_Acc.EClassAccVoucher.VouTopSheetSum>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_17_Acc.RptVoucherTopSheet", list, list1, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "All Voucher Top Sheet"));
            Rpt1.SetParameters(new ReportParameter("txtDate", "From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy")));
            //Rpt1.SetParameters(new ReportParameter("vouType", "Type: " + vouType));
            //Rpt1.SetParameters(new ReportParameter("vouPDC", this.lbltoPdcVoucher.Text));
            //Rpt1.SetParameters(new ReportParameter("vouCash", this.lbltoCashVoucher.Text));
            //Rpt1.SetParameters(new ReportParameter("vouBank", this.lbltoBankVoucher.Text));
            //Rpt1.SetParameters(new ReportParameter("vouContra", this.lbltoContraVoucher.Text));
            //Rpt1.SetParameters(new ReportParameter("vouJournal", this.lbltoJournalVoucher.Text));
            //Rpt1.SetParameters(new ReportParameter("vouTotal", this.lbltotalvoucher.Text));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", userinfo));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewerWin.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void gvAccVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink hlink1 = (HyperLink)e.Row.FindControl("hlnkVoucherEdit");
                HyperLink hlnkPrintVoucher = (HyperLink)e.Row.FindControl("hlnkMoneyRcptPrint");
                string vounum = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "vounum")).ToString();
                string vounum1 = vounum.Substring(0, 2);

                string cquepbl = (this.checkpb.Checked) == false ? "0" : "1";
                string woutchqdat = (this.withoutchqdate.Checked) ? "1" : "0";

                if (this.checkpb.Checked == true)
                {
                    hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                    hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;                   
                }

                else
                {
                    hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                    hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;                    
                }


            }
        }

        protected void lnkbtnPrintRD_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string vounum = dt.Rows[index]["vounum"].ToString();

        }

        private string CompanyPrintVou()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string vouprint = "";
            switch (comcod)
            {

                case "2305":
                    vouprint = "VocherPrint4";
                    break;

                case "3306":
                case "3307":
                case "3308":
                    vouprint = "VocherPrint1";
                    break;
                case "3305":
                case "3310":
                case "3311":
                    vouprint = "VocherPrint2";
                    break;
                case "3309":
                    vouprint = "VocherPrint3";
                    break;

                case "3315":
                case "3316":
                case "3317":
                    vouprint = "VocherPrint5";
                    break;

                case "3330":
                    vouprint = "VocherPrint6";
                    break;

                case "3101":
                case "3332":
                    vouprint = "VocherPrintIns";
                    break;
                default:
                    vouprint = "VocherPrint";
                    break;
            }
            return vouprint;
        }
        private string GetCompInstar()
        {

            string comcod = this.GetCompCode();
            string printinstar = "";
            switch (comcod)
            {
                case "3332":
                case "3101":
                    printinstar = "Innstar";
                    break;

                default:

                    break;


            }
            return printinstar;
        }

        protected void lbtnVoucherApp_Click(object sender, EventArgs e)
        {

            ((Label)this.Master.FindControl("lblprintstk")).Text = "";
            DataTable dt = (DataTable)Session["tblunposted"];
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            dt.Rows[index]["chkmv"] = "True";


            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string vounum = dt.Rows[index]["vounum"].ToString();

            string ApprovedByid = hst["usrid"].ToString();
            string Approvedtrmid = hst["compname"].ToString();
            string ApprovedSession = hst["session"].ToString();
            string Approvedddat = System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");


            // Existing Voucher

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETEXISTPOSTEDVOUCHER", vounum, "", "", "", "", "", "", "", "");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Already Posted";
                return;
            }


            bool resultb = AccData.UpdateTransInfo2(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "UPUNPOSTEDVOUCHER", vounum, ApprovedByid, Approvedtrmid, ApprovedSession, Approvedddat,
                               "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!resultb)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = AccData.ErrorObject["Msg"].ToString();
                return;
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            Session.Remove("tblunposted");
            DataView dv = dt.DefaultView;
            Session["tblunposted"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void lbtnDeleteVoucher_Click(object sender, EventArgs e)
        {

            //((Label)this.Master.FindControl("lblprintstk")).Text = "";
            //DataTable dt = (DataTable)Session["tblunposted"];
            //GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            //int index = row.RowIndex;

            //((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = this.GetCompCode();
            //string userid = hst["usrid"].ToString();
            //string Terminal = hst["trmid"].ToString();
            //string Sessionid = hst["session"].ToString();
            //string vounum = dt.Rows[index]["vounum"].ToString();
            //bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEVOUCHERUNPOSTED", vounum, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            //if (result == false)
            //{
            //    ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Deleted";
            //    return;

            //}

            //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            //this.Data_Bind();


        }

        protected void gvAccVoucher_Sorting(object sender, GridViewSortEventArgs e)
        {

            //    Session["tblunposted"]
            string sortingDirection = string.Empty;
            if (direction == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                direction = System.Web.UI.WebControls.SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                direction = System.Web.UI.WebControls.SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataTable dt = (DataTable)Session["tblunposted"];

            DataView sortedView = new DataView(dt);
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            // Session["SortedView"] = sortedView;
            gvAccVoucher.DataSource = sortedView;
            gvAccVoucher.DataBind();
        }

        public System.Web.UI.WebControls.SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = System.Web.UI.WebControls.SortDirection.Ascending;
                }
                return (System.Web.UI.WebControls.SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }
        protected void withoutchqdate_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow gv1 in gvAccVoucher.Rows)
            {
                string vounum = ((Label)gv1.FindControl("lblvounumh")).Text.Trim();
                HyperLink hlink1 = (HyperLink)gv1.FindControl("hlnkVoucherEdit");
                HyperLink hlnkPrintVoucher = (HyperLink)gv1.FindControl("hlnkVoucherPrint");
                HyperLink hlnkChequePrint = (HyperLink)gv1.FindControl("hlnkChequePrint");
                string vounum1 = vounum.Substring(0, 2);
                string cquepbl = (this.checkpb.Checked) == false ? "0" : "1";
                string woutchqdat = (this.withoutchqdate.Checked) ? "1" : "0";

                hlink1.NavigateUrl = "~/F_17_Acc/GeneralAccounts.aspx?Mod=Management&vounum=" + vounum;
                hlnkPrintVoucher.NavigateUrl = "~/F_17_Acc/AccPrint.aspx?Type=accVou&vounum=" + vounum;               
            }

        }
    }
}