﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using RealEntity;

namespace RealERPWEB.F_46_GrMgtInter
{
    public partial class GrpSalesInformation : System.Web.UI.Page
    {
        UserManSales objUserService = new UserManSales();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES & COLLECTION DASHBOARD " + this.Request.QueryString["Desc"].ToString(); ;
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                //this.txtfromdate.Text = "01" + date.Substring(2);


                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            return (Request.QueryString["comcod"].ToString());

        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //this.lblYearGrp.Visible = true;
            this.lblYear.Visible = true;
            this.lblWeek.Visible = true;
            //this.btnNext.Visible = true;
            this.lblMon.Visible = true;
            this.lblGrp.Visible = true;
            this.lblDetails.Visible = true;
            this.lblColl.Visible = true;
            //this.BarChart1.Visible = true;
            this.GetYearly();
            this.GetWeekly();
            this.GetMonthly();
            this.GetDayWise();
            this.GetDayWiseColl();
        }
        private void GetYearly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_22_Sal.EClassSales_02.EClassYear> lst = objUserService.ShowYearly(comcod, CurDate1);
                if (lst == null)
                    return;
                this.grvYearlySales.DataSource = lst;
                this.grvYearlySales.DataBind();

                ViewState["tblYearly"] = lst;
                this.BindChartYear();
                //((Label)this.grvYearlySales.FooterRow.FindControl("lblgvFinvAmt")).Text = lst.Select(p => p.samt).Sum().ToString("#,##0;(#,##0); ");
                //((Label)this.grvYearlySales.FooterRow.FindControl("lblgvFinvAmt")).Text = lst.Select(p => p.collamt).Sum().ToString("#,##0;(#,##0); ");
            }
            catch (Exception ex)
            {

            }
        }
        private void GetWeekly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_22_Sal.EClassSales_02.EClassWeekly> lst1 = objUserService.ShowWeekly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekSales.DataSource = lst1;
                this.grvWeekSales.DataBind();

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wsamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wsamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wsamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wsamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wsamt4).Sum().ToString("#,##0;(#,##0); ");

                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1")).Text = (lst1.Select(p => p.wcamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2")).Text = (lst1.Select(p => p.wcamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3")).Text = (lst1.Select(p => p.wcamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4")).Text = (lst1.Select(p => p.wcamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wcamt4).Sum().ToString("#,##0;(#,##0); ");


                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wsamt1 + p.wsamt2 + p.wsamt3 + p.wsamt4)).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt1T")).Text = lst1.Select(p => p.wcamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt2T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt3T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekSales.FooterRow.FindControl("lblyFCollamt4T")).Text = lst1.Select(p => (p.wcamt1 + p.wcamt2 + p.wcamt3 + p.wcamt4)).Sum().ToString("#,##0;(#,##0); ");


            }
            catch (Exception ex)
            {

            }
        }
        private void GetMonthly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> lst1 = objUserService.ShowMonthly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvMonthlySales.DataSource = lst1;
                this.grvMonthlySales.DataBind();
                ViewState["tblMonthly"] = lst1;
                if (lst1.Count == 0)
                    return;
                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblyFAmt")).Text = lst1.Select(p => p.ttlsalamt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlySales.FooterRow.FindControl("lblyFCollamt")).Text = lst1.Select(p => p.collamt).Sum().ToString("#,##0;(#,##0); ");

                this.BindChart();
            }
            catch (Exception ex)
            {

            }
        }
        private void BindChartYear()
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassYear> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassYear>)ViewState["tblYearly"];
            if (lst.Count == 0)
                return;

            int i = 0;
            foreach (RealEntity.C_22_Sal.EClassSales_02.EClassYear c1 in lst)
            {
                Chart2.Series[i].Points.Add(new DataPoint(0, (c1.samt / 10000000)));
                Chart2.Series[i].Points.Add(new DataPoint(1, (c1.collamt / 10000000)));
                i++;
            }

            Chart2.DataSource = lst;
            Chart2.DataBind();
            //foreach (Series cs in Chart2.Series)
            //    cs.ChartType = SeriesChartType.StackedColumn;

        }
        private void BindChart()
        {
            List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly> lst = (List<RealEntity.C_22_Sal.EClassSales_02.EClassMonthly>)ViewState["tblMonthly"];

            if (lst.Count == 0)
                return;


            Chart1.Series["Series1"].XValueMember = "yearmon1";
            Chart1.Series["Series1"].YValueMembers = "ttlsalamt";
            Chart1.Series["Series2"].XValueMember = "yearmon1";
            Chart1.Series["Series2"].YValueMembers = "collamt";

            Chart1.Series["Series1"].LegendText = "Sales";
            Chart1.Series["Series2"].LegendText = "Collection";

            Chart1.DataSource = lst;
            Chart1.DataBind();


        }

        private void GetDayWise()
        {
            try
            {
                Session.Remove("tblData");
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                string frdate = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

                DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "RPTDAYWISHSAL", "%", frdate, todate, "12", "%", "", "", "", "");
                if (ds1 == null)
                {
                    this.gvDayWSale.DataSource = null;
                    this.gvDayWSale.DataBind();
                    return;
                }
                //this.gvDayWSale.DataSource = ds1.Tables[0];
                //this.gvDayWSale.DataBind();

                Session["tblData"] = HiddenSameData(ds1.Tables[0]);
                this.gvDayWSale.DataSource = (DataTable)Session["tblData"];
                this.gvDayWSale.DataBind();
                this.FooterCalculation();

            }
            catch (Exception ex)
            {

            }
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
        private void FooterCalculation()
        {

            DataTable dt = (DataTable)Session["tblData"];

            if (dt.Rows.Count == 0)
                return;



            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
            DataTable dt2 = dv.ToTable();
            //DataTable dt = (DataTable)Session["tblData"];

            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tuamt)", "")) ?
                            0 : dt2.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(suamt)", "")) ?
                            0 : dt2.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDDisAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(disamt)", "")) ?
                            0 : dt2.Compute("sum(disamt)", ""))).ToString("#,##0;(#,##0); ");



        }

        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HplgvSAmt = (HyperLink)e.Row.FindControl("HplgvSAmt");


                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetCompCode();
                string fromDtae = this.txtDate.Text;
                string pactcode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                string usircode = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usircode")).ToString();

                HplgvSAmt.NavigateUrl = "~/F_22_Sal/LinkDuesColl.aspx?Type=ClientLedger&comcod=" + comcod + "&pactcode=" + pactcode + "&usircode=" + usircode + "&Date1=" + fromDtae;



                Label actdesc = (Label)e.Row.FindControl("lblgvDPactdesc");
                Label bgdamt = (Label)e.Row.FindControl("lgvDTAmt");
                Label salamt = (Label)e.Row.FindControl("lgvDSAmt");

                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {
                    actdesc.Font.Bold = true;
                    bgdamt.Font.Bold = true;
                    salamt.Font.Bold = true;
                    actdesc.Style.Add("text-align", "right");
                }

            }
        }
        private void GetDayWiseColl()
        {
            try
            {
                string comcod = this.GetCompCode();
                string date = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                string frdate = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                string todate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");

                DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "TRANSACTIONSTATEMENT1", frdate, todate, "%", "", "", "", "", "", "");
                if (ds1 == null)
                {
                    this.grvTrnDatWise.DataSource = null;
                    this.grvTrnDatWise.DataBind();
                    return;
                }
                Session["DailyTrns"] = HiddenSameData(ds1.Tables[0]);


                this.grvTrnDatWise.DataSource = (DataTable)Session["DailyTrns"];
                this.grvTrnDatWise.DataBind();

                if (ds1.Tables[0].Rows.Count == 0)
                    return;

                DataTable dt1 = (DataTable)Session["DailyTrns"];

                DataTable dt4 = dt1.Copy();
                DataView dv1 = dt4.DefaultView;
                dv1.RowFilter = ("grp='F' and collfrm1='EEEEE' ");
                dt4 = dv1.ToTable();
                double cashamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(cashamt)", "")) ? 0 : dt4.Compute("sum(cashamt)", "")));
                double chqamt = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(chqamt)", "")) ? 0 : dt4.Compute("sum(chqamt)", "")));


                ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFCashamt")).Text = cashamt.ToString("#,##0;(#,##0); ");
                ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvFChqamt")).Text = chqamt.ToString("#,##0;(#,##0); ");
                ((Label)this.grvTrnDatWise.FooterRow.FindControl("lgvCDNetTotal")).Text = (cashamt + chqamt).ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }
        protected void grvTrnDatWise_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label CollFrm = (Label)e.Row.FindControl("lgvCollFrm");
                Label Cashamt = (Label)e.Row.FindControl("lgvCaAmt");
                Label chqamt = (Label)e.Row.FindControl("lgvChAmt");

                string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (grp == "")
                {
                    return;
                }
                if (grp == "F" || grp == "G")
                {

                    CollFrm.Font.Bold = true;
                    Cashamt.Font.Bold = true;
                    chqamt.Font.Bold = true;
                }

            }
        }
        private List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl> HiddenSameData2(List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string centrid = "";
            foreach (RealEntity.C_22_Sal.EClassSales_02.EClassDayWiseColl c1 in lst3)
            {
                if (i == 0)
                {
                    centrid = c1.centrid;
                    i++;
                    continue;

                }
                else if (c1.centrid == centrid)
                {
                    c1.centrdesc = "";
                }
                centrid = c1.centrid;

            }

            return lst3;

        }
        private List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise> HiddenSameData(List<RealEntity.C_22_Sal.EClassSales_02.EClassDayWise> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string centrid = "";
            foreach (RealEntity.C_22_Sal.EClassSales_02.EClassDayWise c1 in lst3)
            {
                if (i == 0)
                {
                    centrid = c1.centrid;
                    i++;
                    continue;

                }
                else if (c1.centrid == centrid)
                {
                    c1.centrdesc = "";
                }
                centrid = c1.centrid;

            }

            return lst3;

        }

        protected void grvYearlySales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "A. YEARLY";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.ColumnSpan = 4;

            //    gvrow.Cells.Add(cell01);
            //    grvYearlySales.Controls[0].Controls.AddAt(0, gvrow);
            //}

        }
        protected void grvMonthlySales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //GridViewRow gvRow = e.Row;
            //if (gvRow.RowType == DataControlRowType.Header)
            //{
            //    GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell cell01 = new TableCell();
            //    cell01.Text = "B. MONTHLY";
            //    cell01.HorizontalAlign = HorizontalAlign.Center;
            //    cell01.ColumnSpan = 4;

            //    gvrow.Cells.Add(cell01);
            //    grvMonthlySales.Controls[0].Controls.AddAt(0, gvrow);
            //}
        }
        protected void grvWeekSales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "WEEK-1";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 4;


                TableCell cell02 = new TableCell();
                cell02.Text = "WEEK-2";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 3;


                TableCell cell03 = new TableCell();
                cell03.Text = "WEEK-3";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 3;


                TableCell cell04 = new TableCell();
                cell04.Text = "WEEK-4";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                grvWeekSales.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}