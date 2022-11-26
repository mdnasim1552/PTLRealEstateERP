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
               
                this.txtfrmdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txttodate.Text = dtoday.ToString("dd-MMM-yyyy");               
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
                string res = this.dddSupgrp.SelectedValue.Substring(0, 4).ToString();
                string Rescodegrp = res.Substring(2, 2).ToString() == "00" ? res.Substring(0, 2).ToString() + "%" : res + "%";
               
                DataSet ds1 = MktData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS_SEARCH", "GETSUPLIERPAYMENTACCOUTSDETAILS", frmdate, Rescode, todate, Rescodegrp, "", "", "", "", "");



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
                Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() +" To " + this.txttodate.Text.Trim() + " )" ));

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
                Rpt1.SetParameters(new ReportParameter("printdate", "( From " + this.txtfrmdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )"));

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
                Label lbldiscount = (Label)e.Row.FindControl("lbldiscount");



                
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString().Trim();
                //string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();


                // string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "refno")).ToString();

                if (grp == "")
                {
                    return;
                }



                if (grp == "B" )
                {

                    
                    lblgvOpnamalsasub.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvDrAmountalsasub.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvDrAmountalsasubsd.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvDrAmountalsasubsdtax.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvDrAmountalsasubsdvat.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvDrAmountalsasubNet.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvCrAmtalsasubpay.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lblgvCrAmnetpayable1.Attributes["style"] = "font-weight:bold; color:Navy;";
                    lbldiscount.Attributes["style"] = "font-weight:bold; color:Navy;";

                   
                   
                    lblgvOpnamalsasub.Style.Add("text-align", "right");

                }
                if ( grp == "C" )
                {

                   
          
                    lblgvOpnamalsasub.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasub.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubsd.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubsdtax.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubsdvat.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvDrAmountalsasubNet.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvCrAmtalsasubpay.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lblgvCrAmnetpayable1.Attributes["style"] = "font-weight:bold; color:Orange;";
                    lbldiscount.Attributes["style"] = "font-weight:bold; color:Orange;";



                    lblgvOpnamalsasub.Style.Add("text-align", "right");

                }


                if (grp == "D")
                {

                  

                    lblgvOpnamalsasub.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvDrAmountalsasub.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvDrAmountalsasubsd.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvDrAmountalsasubsdtax.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvDrAmountalsasubsdvat.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvDrAmountalsasubNet.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvCrAmtalsasubpay.Attributes["style"] = "font-weight:bold; color:Green;";
                    lblgvCrAmnetpayable1.Attributes["style"] = "font-weight:bold; color:Green;";
                    lbldiscount.Attributes["style"] = "font-weight:bold; color:Green;";


                    //lgvNagad.Style.Add("text-align", "left");
                    lblgvOpnamalsasub.Style.Add("text-align", "right");

                }



            }
        }

        protected void gvspaysummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblprjName = (Label)e.Row.FindControl("lblprjName");
                Label lblgvCrAmt = (Label)e.Row.FindControl("lblgvCrAmt");
                Label lblgvSdAmt = (Label)e.Row.FindControl("lblgvSdAmt");
                Label lblgvTaxAmt = (Label)e.Row.FindControl("lblgvTaxAmt");
                Label lblgvVatAmt = (Label)e.Row.FindControl("lblgvVatAmt");
                Label lblgvNetAmt = (Label)e.Row.FindControl("lblgvNetAmt");
                Label lblgvPayable = (Label)e.Row.FindControl("lblgvPayable");
                Label lbldiscount = (Label)e.Row.FindControl("lbldiscount");
                Label lblgvRmk = (Label)e.Row.FindControl("lblgvRmk");
               
                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString().Trim();
       
                if (grp == "")
                {
                    return;
                }

                if (grp == "B" )
                {

                   
                    lblprjName.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvCrAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvSdAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvTaxAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvVatAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvNetAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvPayable.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lbldiscount.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";
                    lblgvRmk.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Navy;";



                    lblprjName.Style.Add("text-align", "right");

                }
                if (grp == "C")
                {


                    lblprjName.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvCrAmt.Attributes["style"] = "font-weight:bold; font-size: 15px;  color:Orange;";
                    lblgvSdAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvTaxAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvVatAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvNetAmt.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lblgvPayable.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";
                    lbldiscount.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";

                    lblgvRmk.Attributes["style"] = "font-weight:bold; font-size: 15px; color:Orange;";

                    lblprjName.Style.Add("text-align", "right");

                }

            }
        }
    }
}