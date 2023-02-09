using System;
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
using RealEntity.C_17_Acc;
namespace RealERPWEB.F_01_LPA
{
    public partial class RptDashBoardLandPro : System.Web.UI.Page
    {
        UserDB_BL objUserService = new UserDB_BL();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "DASHBOARD-LAND PROCUREMENT";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
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
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }




        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //this.lblYearGrp.Visible = true;
            this.lblYear.Visible = true;
            this.lblWeek.Visible = true;
            this.lblMon.Visible = true;
            this.lblGrp.Visible = true;
            //this.BarChart1.Visible = true;
            this.GetYearly();
            this.GetWeekly();
            this.GetMonthly();
            //this.ShowDetails();
        }
        private void GetYearly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly> lst = objUserService.ShowYearLandPur(comcod, CurDate1);
                if (lst == null)
                    return;
                this.gvYearLandPur.DataSource = lst;
                this.gvYearLandPur.DataBind();

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
                List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROWeekly> lst1 = objUserService.ShowWeeklylp(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekLP.DataSource = lst1;
                this.grvWeekLP.DataBind();
                if (lst1.Count == 0)
                    return;

                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt1")).Text = (lst1.Select(p => p.wpamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt2")).Text = (lst1.Select(p => p.wpamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt3")).Text = (lst1.Select(p => p.wpamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt4")).Text = (lst1.Select(p => p.wpamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt4).Sum().ToString("#,##0;(#,##0); ");



                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt1")).Text = (lst1.Select(p => p.wpayamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt2")).Text = (lst1.Select(p => p.wpayamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt3")).Text = (lst1.Select(p => p.wpayamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt4")).Text = (lst1.Select(p => p.wpayamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt4).Sum().ToString("#,##0;(#,##0); ");



                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt2T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt3T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpuramt4T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3 + p.wpamt4)).Sum().ToString("#,##0;(#,##0); ");


                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt2T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt3T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2 + p.wpayamt3)).Sum().ToString("#,##0;(#,##0); ");


                ((Label)this.grvWeekLP.FooterRow.FindControl("lblyFpayamt4T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2 + p.wpayamt3 + p.wpayamt4)).Sum().ToString("#,##0;(#,##0); ");



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
                List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly> lst1 = objUserService.ShowMonthlyLP(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvMonthlylp.DataSource = lst1;
                this.grvMonthlylp.DataBind();
                ViewState["tblMonthly"] = lst1;

                ((Label)this.grvMonthlylp.FooterRow.FindControl("lblFpuramtmon")).Text = lst1.Select(p => p.puram).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlylp.FooterRow.FindControl("lblFpaymntmon")).Text = lst1.Select(p => p.paymntam).Sum().ToString("#,##0;(#,##0); ");

                this.BindChart();
            }
            catch (Exception ex)
            {

            }
        }
        private void BindChartYear()
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly> lst = (List<RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly>)ViewState["tblYearly"];

            int i = 0;
            foreach (RealEntity.C_17_Acc.EClassDB_BO.EClassLPROYearly c1 in lst)
            {
                Chart2.Series[i].Points.Add(new DataPoint(0, (c1.puram / 10000000)));
                Chart2.Series[i].Points.Add(new DataPoint(1, (c1.paymntam / 10000000)));
                i++;
            }

            Chart2.DataSource = lst;
            Chart2.DataBind();


        }
        private void BindChart()
        {
            List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly> lst = (List<RealEntity.C_17_Acc.EClassDB_BO.EClassPROMonthly>)ViewState["tblMonthly"];

            Chart1.Series["Series1"].XValueMember = "yearmon1";
            Chart1.Series["Series1"].YValueMembers = "puram";
            Chart1.Series["Series2"].XValueMember = "yearmon1";
            Chart1.Series["Series2"].YValueMembers = "paymntam";

            Chart1.Series["Series1"].LegendText = "Purchase";
            Chart1.Series["Series2"].LegendText = "Payment";

            Chart1.DataSource = lst;
            Chart1.DataBind();


        }
        private void ShowDetails()
        {
            Session.Remove("cashbank");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string Date = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string fromdate = "01" + Date.Substring(2);
            string todate = Convert.ToDateTime(fromdate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string txtSVoucher = "ALL Voucher";
            string searchinfo = "";
            //string txtSProject =  "%";
            DataSet ds1 = _DataEntry.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_TRANS", "PRINTCASHBOOK", fromdate, todate, txtSVoucher, searchinfo, "", "", "", "", "");
            if (ds1 == null)
                return;

            //For Grouping
            DataTable dtr = new DataTable();

            DataView dv1 = new DataView();
            dv1 = ds1.Tables[0].DefaultView;
            dv1.RowFilter = ("grp1 = 'A' or grp1 = 'B'  or grp1 = 'C'  ");
            dtr = dv1.ToTable();
            this.lblReceiptCash.Visible = true;
            this.lblPaymentCash.Visible = true;
            this.lblDetailsCash.Visible = true;
            Session["cashbank"] = dtr;
            DataView dvr = new DataView();
            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1 = 'A'");
            DataTable dtr1 = HiddenSameDate(dvr.ToTable());
            this.gvcashbook.DataSource = dtr1;
            this.gvcashbook.DataBind();

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            this.FooterCalculation(dtr1, "gvcashbook");
            Session["Report1"] = gvcashbook;
            if (dtr1.Rows.Count > 0)
                ((HyperLink)this.gvcashbook.HeaderRow.FindControl("hlbtnCBdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";



            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='B'");
            DataTable dtr2 = HiddenSameDate(dvr.ToTable());
            this.gvcashbookp.DataSource = dtr2;
            this.gvcashbookp.DataBind();

            if (dtr2.Rows.Count > 0)
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));


            this.FooterCalculation(dtr2, "gvcashbookp");
            Session["Report1"] = gvcashbookp;
            if (dtr2.Rows.Count > 0)
                ((HyperLink)this.gvcashbookp.HeaderRow.FindControl("hlbtnCBPdataExel")).NavigateUrl = "../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";


            dvr = dtr.DefaultView;
            dvr.RowFilter = ("grp1='C'");
            DataTable dtr3 = dvr.ToTable();
            this.gvcashbookDB.DataSource = dvr.ToTable(); ;
            this.gvcashbookDB.DataBind();
            this.FooterCalculation(dtr3, "gvcashbookDB");
        }


        private DataTable HiddenSameDate(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string Date1, vounum;
            int j;
            Date1 = dt1.Rows[0]["voudat1"].ToString();
            vounum = dt1.Rows[0]["vounum1"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["vounum1"].ToString() == vounum)
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                    dt1.Rows[j]["vounum1"] = "";
                    dt1.Rows[j]["voudat1"] = "";
                    dt1.Rows[j]["vounar"] = "";


                }

                else
                {
                    vounum = dt1.Rows[j]["vounum1"].ToString();
                }

            }
            return dt1;

        }
        private void FooterCalculation(DataTable dt, string GvName)
        {
            if (dt.Rows.Count == 0)
                return;
            DataView dv = new DataView();
            double frecamt = 0, fpayamt1 = 0, netbal;
            DataView dv1; DataTable dt1;

            switch (GvName)
            {
                case "gvcashbook":
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvCashAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                            0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbook.FooterRow.FindControl("lgvFBankAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                          0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;


                case "gvcashbookp":
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvCashAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(casham)", "")) ?
                             0 : dt.Compute("sum(casham)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookp.FooterRow.FindControl("lgvFBankAmt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(bankam)", "")) ?
                           0 : dt.Compute("sum(bankam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;

                case "gvcashbookDB":
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFOpening")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(opnam)", "")) ?
                             0 : dt.Compute("sum(opnam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lblgvFrecam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(depam)", "")) ?
                                   0 : dt.Compute("sum(depam)", ""))).ToString("#,##0;(#,##0) ;");

                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFpayam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(payam)", "")) ?
                             0 : dt.Compute("sum(payam)", ""))).ToString("#,##0;(#,##0) ;");
                    ((Label)this.gvcashbookDB.FooterRow.FindControl("lgvFClAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(clsam)", "")) ?
                           0 : dt.Compute("sum(clsam)", ""))).ToString("#,##0;(#,##0) ;");
                    break;
            }


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

        protected void grvWeekLP_RowCreated(object sender, GridViewRowEventArgs e)
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
                cell04.Text = "WEEK-3";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 3;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                grvWeekLP.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}