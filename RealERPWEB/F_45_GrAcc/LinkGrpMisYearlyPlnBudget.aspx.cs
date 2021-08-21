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
using RealERPLIB;
using RealERPRPT;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_45_GrAcc
{
    public partial class LinkGrpMisYearlyPlnBudget : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string type = this.Request.QueryString["Type"].ToString().Trim();
                this.lblHeader.Text = (type == "Yearly") ? "Yearly Revenue Target/Cost Entry"
                    : (type == "BgdAmtBasis") ? "Yearly Revenue Target/Cost Report Amount Basis"
                    : (type == "BgdIncome") ? "Yearly Budgeted Income Statement" : "Yearly Revenue Target/Cost Report Qty Basis";
                this.ViewSection();
                this.GetYear();
            }
        }


        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "Yearly":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
                case "BgdAmtBasis":
                    this.MultiView1.ActiveViewIndex = 1;
                    break;
                case "BgdQtyBasis":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "BgdIncome":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

            }


        }
        private string GetCompCode()
        {

            return (this.Request.QueryString["comcod"].ToString());


        }

        private void GetYear()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETYEAR", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlyear.DataTextField = "year1";
            this.ddlyear.DataValueField = "year1";
            this.ddlyear.DataSource = ds1.Tables[0];
            this.ddlyear.DataBind();
            this.ddlyear.SelectedValue = System.DateTime.Today.Year.ToString();
            ds1.Dispose();
        }
        protected void lbtnYearbgd_Click(object sender, EventArgs e)
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            switch (Type)
            {

                case "Yearly":
                    this.YearlyBudget();
                    break;
                case "BgdAmtBasis":
                case "BgdQtyBasis":
                    this.YearlyBgdAmtQty();
                    break;

                case "BgdIncome":
                    this.YearlyIncome();
                    break;
            }
        }

        private void YearlyBudget()
        {
            string comcod = this.GetCompCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "GETYSALBGDINFO", Year, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvySalbgd.DataSource = null;
                this.gvySalbgd.DataBind();
                return;


            }
            ViewState["tblsal"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void YearlyBgdAmtQty()
        {
            string comcod = this.GetCompCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_REPORT_YEARLYBUDGET", "RPTYEALRLYBGDAMT", Year, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvBgdAmt.DataSource = null;
                this.gvBgdAmt.DataBind();
                this.gvBgdQty.DataSource = null;
                this.gvBgdQty.DataBind();
                return;


            }
            ViewState["tblsal"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void YearlyIncome()
        {
            string comcod = this.GetCompCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_REPORT_YEARLYBUDGET", "RPTYEALRLYBGDINCOME", Year, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.grvYearltIncome.DataSource = null;
                this.grvYearltIncome.DataBind();
                return;


            }
            ViewState["tblsal"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            switch (Type)
            {

                case "Yearly":
                    this.gvySalbgd.DataSource = tbl1;
                    this.gvySalbgd.DataBind();
                    break;
                case "BgdAmtBasis":
                    this.gvBgdAmt.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBgdAmt.DataSource = tbl1;
                    this.gvBgdAmt.DataBind();
                    break;

                case "BgdQtyBasis":
                    DataTable dt = tbl1.Copy();
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = ("rescode not like '%AAAA%'");
                    tbl1 = dv.ToTable();
                    this.gvBgdQty.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvBgdQty.DataSource = tbl1;
                    this.gvBgdQty.DataBind();
                    break;
                case "BgdIncome":
                    this.grvYearltIncome.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvYearltIncome.DataSource = tbl1;
                    this.grvYearltIncome.DataBind();
                    break;
            }


        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            string grp = dt1.Rows[0]["grp"].ToString();

            switch (type)
            {
                case "Yearly":
                case "BgdAmtBasis":
                case "BgdQtyBasis":

                    string grp1 = dt1.Rows[0]["grp1"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp && dt1.Rows[j]["grp1"].ToString() == grp1)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            grp1 = dt1.Rows[j]["grp1"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                            dt1.Rows[j]["grpdesc1"] = "";

                        }

                        else
                        {


                            if (dt1.Rows[j]["grp"].ToString() == grp)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }
                            if (dt1.Rows[j]["grp1"].ToString() == grp1)
                            {
                                dt1.Rows[j]["grpdesc1"] = "";
                            }

                            grp = dt1.Rows[j]["grp"].ToString();
                            grp1 = dt1.Rows[j]["grp1"].ToString();

                        }

                    }

                    break;
                case "BgdIncome":
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";

                        }

                        else
                        {
                            if (dt1.Rows[j]["grp"].ToString() == grp)
                            {
                                dt1.Rows[j]["grpdesc"] = "";
                            }
                            grp = dt1.Rows[j]["grp"].ToString();
                        }

                    }
                    break;
            }


            return dt1;

        }


        //private void FooterCalculation(DataTable dt) 
        //{
        //    if (dt.Rows.Count == 0)
        //        return;


        //  string Type = this.Request.QueryString["Type"].ToString().Trim();

        //    switch (Type)
        //    {

        //        case "Yearly":
        //               ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFSaleTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(saleamt)", "")) ? 0.00
        //                   : dt.Compute("Sum(saleamt)", ""))).ToString("#,##0;(#,##0);  ");
        //               ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFCollTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(collamt)", "")) ? 0.00
        //                   : dt.Compute("Sum(collamt)", ""))).ToString("#,##0;(#,##0);  ");
        //               ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFPurTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(puramt)", "")) ? 0.00
        //                     : dt.Compute("Sum(puramt)", ""))).ToString("#,##0;(#,##0);  ");

        //               ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtoSaleTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tsaleamt)", "")) ? 0.00
        //                   : dt.Compute("Sum(tsaleamt)", ""))).ToString("#,##0;(#,##0);  ");

        //               ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtoCollTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tcollamt)", "")) ? 0.00
        //                   : dt.Compute("Sum(tcollamt)", ""))).ToString("#,##0;(#,##0);  ");
        //               ((Label)this.gvySalbgd.FooterRow.FindControl("lgvFtoPurTotalyb")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tpuramt)", "")) ? 0.00
        //                     : dt.Compute("Sum(tpuramt)", ""))).ToString("#,##0;(#,##0);  ");
        //            break;

        //    }







        //}

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblsal"];
            string Type = this.Request.QueryString["Type"].ToString().Trim();

            switch (Type)
            {

                case "Yearly":
                    for (int i = 0; i < this.gvySalbgd.Rows.Count; i++)
                    {

                        dt.Rows[i]["qty1"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty1")).Text.Trim()).ToString();
                        dt.Rows[i]["qty2"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty2")).Text.Trim()).ToString();
                        dt.Rows[i]["qty3"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty3")).Text.Trim()).ToString();
                        dt.Rows[i]["qty4"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty4")).Text.Trim()).ToString();

                        dt.Rows[i]["qty5"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty5")).Text.Trim()).ToString();
                        dt.Rows[i]["qty6"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty6")).Text.Trim()).ToString();
                        dt.Rows[i]["qty7"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty7")).Text.Trim()).ToString();
                        dt.Rows[i]["qty8"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty8")).Text.Trim()).ToString();

                        dt.Rows[i]["qty9"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty9")).Text.Trim()).ToString();
                        dt.Rows[i]["qty10"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty10")).Text.Trim()).ToString();
                        dt.Rows[i]["qty11"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty11")).Text.Trim()).ToString();
                        dt.Rows[i]["qty12"] = Convert.ToDouble("0" + ((TextBox)this.gvySalbgd.Rows[i].FindControl("txtgvqty12")).Text.Trim()).ToString();

                        dt.Rows[i]["tqty"] = Convert.ToDouble(dt.Rows[i]["qty1"]) + Convert.ToDouble(dt.Rows[i]["qty2"]) + Convert.ToDouble(dt.Rows[i]["qty3"])
                      + Convert.ToDouble(dt.Rows[i]["qty4"]) + Convert.ToDouble(dt.Rows[i]["qty5"]) + Convert.ToDouble(dt.Rows[i]["qty6"]) + Convert.ToDouble(dt.Rows[i]["qty7"])
                      + Convert.ToDouble(dt.Rows[i]["qty8"]) + Convert.ToDouble(dt.Rows[i]["qty9"]) + Convert.ToDouble(dt.Rows[i]["qty10"]) + Convert.ToDouble(dt.Rows[i]["qty11"])
                      + Convert.ToDouble(dt.Rows[i]["qty12"]);
                    }

                    break;
            }
            ViewState["tblsal"] = dt;


        }

        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "Yearly":
                    break;
                case "BgdAmtBasis":
                    this.PrintYearlyBgdAmt();
                    break;

                case "BgdQtyBasis":
                    this.PrintYearlyBgdQty();
                    break;
                case "BgdIncome":
                    this.PrintYearlyIncome();
                    break;
            }

        }

        private void PrintYearlyBgdAmt()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblsal"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_05_Busi.RptYearlyBgdAmt();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "For Year " + this.ddlyear.SelectedItem.Text;




            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintYearlyBgdQty()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comcod = hst["comcod"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblsal"];
            DataTable dt = dt1.Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = ("rescode not like '%AAAA%'");
            dt1 = dv.ToTable();

            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_05_Busi.RptYearlyBgdQty();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "For Year " + this.ddlyear.SelectedItem.Text;




            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstk.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstk;
            this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        private void PrintYearlyIncome()
        {
            // IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblsal"];
            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_04_Bgd.EClassBudget.YearlyPlan>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_05_Busi.RptYePlanIncomeSt", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("Foryear", "For Year " + this.ddlyear.SelectedItem.Text));
            Rpt1.SetParameters(new ReportParameter("RptTitle", (this.Request.QueryString["Type"] == "Income") ? "FINANCIAL PERFORMANCE BUDGET (ABP)" : "CASH BUDGET (ABP)"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string comcod = hst["comcod"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblsal"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_05_Busi.RptYearlyIncomeSt();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "For Year " + this.ddlyear.SelectedItem.Text;




            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void lbYearbgdTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnYBgdUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg02.Text = "You have no permission";
            //    return;
            //}
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblsal"];
                string Year = this.ddlyear.SelectedValue.ToString();
                bool result = true;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    int j = 1;

                    string rescode = dt1.Rows[i]["rescode"].ToString();
                    while (j <= 12)
                    {
                        double qty = Convert.ToDouble(dt1.Rows[i]["qty" + j.ToString()].ToString());
                        string monthid = this.ddlyear.SelectedValue.ToString() + ASTUtility.Right("0" + j.ToString(), 2);
                        if (qty != 0)
                        {
                            result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_BUDGET", "INSORUPYSCOLPURBGDINF", Year, monthid, rescode, qty.ToString(), "", "", "", "", "", "", "", "", "", "", "");
                        }
                        j++;
                    }

                    if (result == false)
                    {
                        this.lblmsg02.Text = "Updated Failed";
                        return;
                    }
                    else
                    {
                        this.lblmsg02.Text = "Updated Successfully";
                    }



                }




            }
            catch (Exception ex)
            {
                this.lblmsg02.Text = "Errp:" + ex.Message;
            }

        }
        protected void gvBgdAmt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdAmt.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgdAmt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblDesc");
                Label lblgvTAmt = (Label)e.Row.FindControl("lblgvTAmt");
                Label lblgvAmt1 = (Label)e.Row.FindControl("lblgvAmt1");
                Label lblgvamt2 = (Label)e.Row.FindControl("lblgvamt2");
                Label lblgvamt3 = (Label)e.Row.FindControl("lblgvamt3");
                Label lblgvamt4 = (Label)e.Row.FindControl("lblgvamt4");
                Label lblgvamt5 = (Label)e.Row.FindControl("lblgvamt5");
                Label lblgvamt6 = (Label)e.Row.FindControl("lblgvamt6");
                Label lblgvamt7 = (Label)e.Row.FindControl("lblgvamt7");
                Label lblgvamt8 = (Label)e.Row.FindControl("lblgvamt8");
                Label lblgvamt9 = (Label)e.Row.FindControl("lblgvamt9");
                Label lblgvamt10 = (Label)e.Row.FindControl("lblgvamt10");
                Label lblgvamt11 = (Label)e.Row.FindControl("lblgvamt11");
                Label lblgvamt12 = (Label)e.Row.FindControl("lblgvamt12");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    lblgvTAmt.Font.Bold = true;
                    lblgvAmt1.Font.Bold = true;
                    lblgvamt2.Font.Bold = true;
                    lblgvamt3.Font.Bold = true;
                    lblgvamt4.Font.Bold = true;
                    lblgvamt5.Font.Bold = true;
                    lblgvamt6.Font.Bold = true;
                    lblgvamt7.Font.Bold = true;
                    lblgvamt8.Font.Bold = true;
                    lblgvamt9.Font.Bold = true;
                    lblgvamt10.Font.Bold = true;
                    lblgvamt11.Font.Bold = true;
                    lblgvamt12.Font.Bold = true;

                    actdesc.Style.Add("text-align", "right");
                }


            }
        }
        protected void gvBgdQty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvBgdQty.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void gvBgdQty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblDesc");
                Label lblgvTQty = (Label)e.Row.FindControl("lblgvTQty");
                Label lblgvqty1 = (Label)e.Row.FindControl("lblgvqty1");
                Label lblgvqty2 = (Label)e.Row.FindControl("lblgvqty2");
                Label lblgvqty3 = (Label)e.Row.FindControl("lblgvqty3");
                Label lblgvqty4 = (Label)e.Row.FindControl("lblgvqty4");
                Label lblgvqty5 = (Label)e.Row.FindControl("lblgvqty5");
                Label lblgvqty6 = (Label)e.Row.FindControl("lblgvqty6");
                Label lblgvqty7 = (Label)e.Row.FindControl("lblgvqty7");
                Label lblgvqty8 = (Label)e.Row.FindControl("lblgvqty8");
                Label lblgvqty9 = (Label)e.Row.FindControl("lblgvqty9");
                Label lblgvqty10 = (Label)e.Row.FindControl("lblgvqty10");
                Label lblgvqty11 = (Label)e.Row.FindControl("lblgvqty11");
                Label lblgvqty12 = (Label)e.Row.FindControl("lblgvqty12");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    lblgvTQty.Font.Bold = true;
                    lblgvqty1.Font.Bold = true;
                    lblgvqty2.Font.Bold = true;
                    lblgvqty3.Font.Bold = true;
                    lblgvqty4.Font.Bold = true;
                    lblgvqty5.Font.Bold = true;
                    lblgvqty6.Font.Bold = true;
                    lblgvqty7.Font.Bold = true;
                    lblgvqty8.Font.Bold = true;
                    lblgvqty9.Font.Bold = true;
                    lblgvqty10.Font.Bold = true;
                    lblgvqty11.Font.Bold = true;
                    lblgvqty12.Font.Bold = true;

                    actdesc.Style.Add("text-align", "right");
                }


            }
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void gvySalbgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblDesc");
                Label unit = (Label)e.Row.FindControl("lblunit");
                Label rate = (Label)e.Row.FindControl("lblgvRate");
                Label tQty = (Label)e.Row.FindControl("lblgvTQty");

                TextBox lblgvqty1 = (TextBox)e.Row.FindControl("txtgvqty1");
                TextBox lblgvqty2 = (TextBox)e.Row.FindControl("txtgvqty2");
                TextBox lblgvqty3 = (TextBox)e.Row.FindControl("txtgvqty3");
                TextBox lblgvqty4 = (TextBox)e.Row.FindControl("txtgvqty4");
                TextBox lblgvqty5 = (TextBox)e.Row.FindControl("txtgvqty5");
                TextBox lblgvqty6 = (TextBox)e.Row.FindControl("txtgvqty6");
                TextBox lblgvqty7 = (TextBox)e.Row.FindControl("txtgvqty7");
                TextBox lblgvqty8 = (TextBox)e.Row.FindControl("txtgvqty8");
                TextBox lblgvqty9 = (TextBox)e.Row.FindControl("txtgvqty9");
                TextBox lblgvqty10 = (TextBox)e.Row.FindControl("txtgvqty10");
                TextBox lblgvqty11 = (TextBox)e.Row.FindControl("txtgvqty11");
                TextBox lblgvqty12 = (TextBox)e.Row.FindControl("txtgvqty12");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 3) == "999")
                {
                    //actdesc.BackColor = System.Drawing.Color.Green;
                    unit.BackColor = System.Drawing.Color.Green;
                    rate.BackColor = System.Drawing.Color.Green;
                    tQty.BackColor = System.Drawing.Color.Green;
                    lblgvqty1.BackColor = System.Drawing.Color.Green;
                    lblgvqty2.BackColor = System.Drawing.Color.Green;
                    lblgvqty3.BackColor = System.Drawing.Color.Green;
                    lblgvqty4.BackColor = System.Drawing.Color.Green;
                    lblgvqty5.BackColor = System.Drawing.Color.Green;
                    lblgvqty6.BackColor = System.Drawing.Color.Green;
                    lblgvqty7.BackColor = System.Drawing.Color.Green;
                    lblgvqty8.BackColor = System.Drawing.Color.Green;
                    lblgvqty9.BackColor = System.Drawing.Color.Green;
                    lblgvqty10.BackColor = System.Drawing.Color.Green;
                    lblgvqty11.BackColor = System.Drawing.Color.Green;
                    lblgvqty12.BackColor = System.Drawing.Color.Green;
                    actdesc.Font.Bold = true;
                }


            }
        }

        protected void grvYearltIncome_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label actdesc = (Label)e.Row.FindControl("lblDesc");
                Label lblgvTAmt = (Label)e.Row.FindControl("lblgvTAmt");
                Label lblgvAmt1 = (Label)e.Row.FindControl("lblgvAmt1");
                Label lblgvamt2 = (Label)e.Row.FindControl("lblgvamt2");
                Label lblgvamt3 = (Label)e.Row.FindControl("lblgvamt3");
                Label lblgvamt4 = (Label)e.Row.FindControl("lblgvamt4");
                Label lblgvamt5 = (Label)e.Row.FindControl("lblgvamt5");
                Label lblgvamt6 = (Label)e.Row.FindControl("lblgvamt6");
                Label lblgvamt7 = (Label)e.Row.FindControl("lblgvamt7");
                Label lblgvamt8 = (Label)e.Row.FindControl("lblgvamt8");
                Label lblgvamt9 = (Label)e.Row.FindControl("lblgvamt9");
                Label lblgvamt10 = (Label)e.Row.FindControl("lblgvamt10");
                Label lblgvamt11 = (Label)e.Row.FindControl("lblgvamt11");
                Label lblgvamt12 = (Label)e.Row.FindControl("lblgvamt12");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "rescode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    lblgvTAmt.Font.Bold = true;
                    lblgvAmt1.Font.Bold = true;
                    lblgvamt2.Font.Bold = true;
                    lblgvamt3.Font.Bold = true;
                    lblgvamt4.Font.Bold = true;
                    lblgvamt5.Font.Bold = true;
                    lblgvamt6.Font.Bold = true;
                    lblgvamt7.Font.Bold = true;
                    lblgvamt8.Font.Bold = true;
                    lblgvamt9.Font.Bold = true;
                    lblgvamt10.Font.Bold = true;
                    lblgvamt11.Font.Bold = true;
                    lblgvamt12.Font.Bold = true;

                    actdesc.Style.Add("text-align", "right");
                }


            }
        }

        protected void grvYearltIncome_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvYearltIncome.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}