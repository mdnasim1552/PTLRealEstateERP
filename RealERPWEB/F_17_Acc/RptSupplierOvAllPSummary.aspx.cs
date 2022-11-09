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

namespace RealERPWEB.F_17_Acc
{
    public partial class RptSupplierOvAllPSummary : System.Web.UI.Page

    {
        ProcessAccess MktData = new ProcessAccess();
        static string prevPage = String.Empty;
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                ((Label)this.Master.FindControl("lblTitle")).Text = "Supplier Overall Position Summary";

                var dtoday = System.DateTime.Today;
                this.txttodate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = dtoday.ToString("dd-MMM-yyyy");
                this.SupplierList();
                this.LoadAllSupplier();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }


        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void ibtnFindSupply_OnClick(object sender, EventArgs e)
        {
            this.SupplierList();
        }

        private void LoadAllSupplier()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "LOADALLSUPPLIER", "", "", "", "", "", "", "", "", "");
            this.dddSupgrp.DataTextField = "sirdesc";
            this.dddSupgrp.DataValueField = "sircode";
            this.dddSupgrp.DataSource = ds2.Tables[0];
            this.dddSupgrp.DataBind();
        }


        private void SupplierList()
        {
            string comcod = this.GetComeCode();
            string SrchSupplier = "%%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETSUPPLIER", SrchSupplier, "", "", "", "", "", "", "", "");
            this.ddlSuplist.DataTextField = "resdesc";
            this.ddlSuplist.DataValueField = "rescode";
            this.ddlSuplist.DataSource = ds1.Tables[0];
            this.ddlSuplist.DataBind();
            ViewState["tblSup"] = ds1.Tables[0];
        }



        protected void lnkbtnOk_Click(object sender, EventArgs e)
        {
            string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
            switch (stindex)
            {
                case "0":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.PaymentSummary();

                    break;

                case "1":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.PaymentDetails();
                    break;

            }




        }

        private void PaymentSummary()
        {

            try
            {
                Session.Remove("tblspaysum");
                string comcod = this.GetComeCode();

                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
                string Rescode = this.ddlSuplist.SelectedValue.ToString() == "000000000000" ? "99%" : this.ddlSuplist.SelectedValue.ToString() + "%";

                string res = this.dddSupgrp.SelectedValue.Substring(0, 4).ToString();
                string Rescodegrp = res.Substring(2, 2).ToString() == "00" ? res.Substring(0, 2).ToString() + "%" : res + "%";
                //string mRptGroup = "12";
                // DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "RPTALLSUPPAYMENTSTATUS", frmdate, todate, Rescode, mRptGroup, "", "", "", "", "");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETSUPLIERPAYMENTACCOUTS", Rescode, frmdate, todate, Rescodegrp, "", "", "", "", "");

                if (ds1 == null)
                {
                    this.gvspaysummary.DataSource = null;
                    this.gvspaysummary.DataBind();
                    return;
                }
                Session["tblspaysum"] = ds1.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {

            }

        }

        private void PaymentDetails()
        {

            try
            {
                Session.Remove("tblspaysum");
                string comcod = this.GetComeCode();

                string frmdate = txtfrmdate.Text.ToString();
                string todate = txttodate.Text.ToString();
                string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
                string Rescode = this.ddlSuplist.SelectedValue.ToString() == "000000000000" ? "99%" : this.ddlSuplist.SelectedValue.ToString() + "%";
                //string mRptGroup = "12";
                //string calltype = (stindex == "0" ? "RPTALLSUPPAYMENTSTATUS" : "GETSUPLLIERANDSUBCONTRACTORSATUS");
                //  DataSet ds2 = accData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_SPLG", "RPTALLSUPPAYMENT", frmdate, todate, Rescode, mRptGroup, supplier, Rescodegrp, search, "", "");
                //  DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETSUPLIERPAYMENTDETIAL", Rescode, frmdate, todate, "", "", "", "", "", "");
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETSUPLIERPAYMENTACCOUTSDETAILS", frmdate, Rescode, todate, "", "", "", "", "", "");



                if (ds1 == null)
                {
                    this.gvspaysummary.DataSource = null;
                    this.gvspaysummary.DataBind();
                    return;
                }
                Session["tblspaysum"] = HiddenSameData(ds1.Tables[0]);

                this.Data_Bind();
            }
            catch (Exception ex)
            {

            }

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string rescode = dt1.Rows[0]["rescode"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["rescode"].ToString() == rescode)
                {
                    rescode = dt1.Rows[j]["rescode"].ToString();
                    dt1.Rows[j]["resdesc"] = "";
                }

                else
                {
                    rescode = dt1.Rows[j]["rescode"].ToString();
                }
            }
            return dt1;

        }


        private void Data_Bind()
        {

            string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
            switch (stindex)
            {
                case "0":
                    this.gvspaysummary.DataSource = (DataTable)Session["tblspaysum"];
                    this.gvspaysummary.DataBind();

                    break;

                case "1":
                    this.gvspaymentdetails.DataSource = (DataTable)Session["tblspaysum"];
                    this.gvspaymentdetails.DataBind();
                    break;

            }

        }


        private void lnkPrint_Click(object sender, EventArgs e)
        {
            
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["tblspaysum"];
            string stindex = this.rbtnAtStatus.SelectedIndex.ToString();
            LocalReport Rpt1 = new LocalReport();
            if (stindex == "0")
            {
                var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptSupplierOverAllPSummary>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSupplierOvAllPSummary", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Supplier Overall Summary" ));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " )"));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }
            else
            {
                
                var lst = dt.DataTableToList<RealEntity.C_17_Acc.EClassAccounts.RptSupplierOverAllPSummaryDetails>();
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_17_Acc.RptSupplierOvAllPSummaryDetails", lst, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
                Rpt1.SetParameters(new ReportParameter("comadd", comadd));
                Rpt1.SetParameters(new ReportParameter("RptTitle", "Supplier Overall Details"));
                Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
                Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
                Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " )"));

                Session["Report1"] = Rpt1;
                ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                            ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            }

               

        }

        protected void gvspaymentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblgvOpnamalsasub = (Label)e.Row.FindControl("lblgvOpnamalsasub");
                Label lblgvDrAmountalsasub = (Label)e.Row.FindControl("lblgvDrAmountalsasub");
                Label lblgvDrAmountalsasubsd = (Label)e.Row.FindControl("lblgvDrAmountalsasubsd");
                Label lblgvDrAmountalsasubsdtax = (Label)e.Row.FindControl("lblgvDrAmountalsasubsdtax");
                Label lblgvDrAmountalsasubsdvat = (Label)e.Row.FindControl("lblgvDrAmountalsasubsdvat");
                Label lblgvDrAmountalsasubNet = (Label)e.Row.FindControl("lblgvDrAmountalsasubNet");
                Label lblgvCrAmtalsasubpay = (Label)e.Row.FindControl("lblgvCrAmtalsasubpay");
                Label lblgvCrAmnetpayable1 = (Label)e.Row.FindControl("lblgvCrAmnetpayable1");



                
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString().Trim();
                //string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();


                // string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();

                if (grp == "")
                {
                    return;
                }



                if (grp == "B")
                {

                    //lgvfgrp.Font.Bold = true;
                    //lblgvOpndetails.Font.Bold = true;
                    //lblgvDrAmount.Font.Bold = true;
                    //lblgvCrAmt.Font.Bold = true;
                    //lblgvClsing.Font.Bold = true;
                    //lblprjName.Font.Bold = true;

                    //lblgvOpndetails.Font.Size = 10;
                    //lblgvDrAmount.Font.Size = 10;
                    //lblgvCrAmt.Font.Size = 10;
                    //lblgvClsing.Font.Size = 10;
                    //lblprjName.Font.Size = 10;

                    //lblprjName.Attributes["style"] = "font-weight:bold; color:maroon;";
                    lblgvOpnamalsasub.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasub.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubsd.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubsdtax.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubsdvat.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubNet.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvCrAmtalsasubpay.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvCrAmnetpayable1.Attributes["style"] = "font-weight:bold; color:Orange;";

                   
                    //lgvNagad.Style.Add("text-align", "left");
                    lblgvOpnamalsasub.Style.Add("text-align", "right");

                }


                //if (grp == "D")
                //{

                //    //lgvfgrp.Font.Bold = true;
                //    //lblgvOpndetails.Font.Bold = true;
                //    //lblgvDrAmount.Font.Bold = true;
                //    //lblgvCrAmt.Font.Bold = true;
                //    //lblgvClsing.Font.Bold = true;
                //    //lblprjName.Font.Bold = true;

                //    //lblgvOpndetails.Font.Size = 10;
                //    //lblgvDrAmount.Font.Size = 10;
                //    //lblgvCrAmt.Font.Size = 10;
                //    //lblgvClsing.Font.Size = 10;
                //    //lblprjName.Font.Size = 10;

                //    lblprjName.Attributes["style"] = "font-weight:bold; color:green;";
                //    lblgvOpndetails.Attributes["style"] = "font-weight:bold; color:green;";
                //    lblgvDrAmount.Attributes["style"] = "font-weight:bold; color:green;";
                //    lblgvCrAmt.Attributes["style"] = "font-weight:bold; color:green;";
                //    lblgvClsing.Attributes["style"] = "font-weight:bold; color:green;";


                //    //lgvNagad.Style.Add("text-align", "left");
                //    lblprjName.Style.Add("text-align", "right");

                //}



            }
        }

        
    }
}