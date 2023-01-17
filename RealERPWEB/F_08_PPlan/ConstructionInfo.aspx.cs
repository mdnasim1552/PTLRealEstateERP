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
using RealEntity.C_08_PPlan;

namespace RealERPWEB.F_08_PPlan
{
    public partial class ConstructionInfo : System.Web.UI.Page
    {
        BL_UserManage_Con objUserService = new BL_UserManage_Con();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"])) ;
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Constraction DASHBOARD";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                this.lbtnOk_Click(null, null);
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;


        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblYear.Visible = true;
            this.lblWeek.Visible = true;
            this.lblMon.Visible = true;
            this.lblGrp.Visible = true;
            //this.lblDetails.Visible = true;
            //this.pnlbtn.Visible = true;
            this.GetPurYearly();
            this.GetPurMonth();
            this.GetPurWeekly();
            //this.GetDayWisePur();
            //this.GetDayWisePay();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteMyGraph();", true);
        }

        private void GetPurYearly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                List<RealEntity.C_08_PPlan.BO_Class_Con.EClassYear> list = objUserService.ShowConYearly(comcod, CurDate);

                if (list == null)
                    return;

                this.grvYearlyCon.DataSource = list;
                this.grvYearlyCon.DataBind();

                ViewState["tblYearly"] = list;
                this.BindChartYear();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindChartYear()
        {
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassYear> list = (List<RealEntity.C_08_PPlan.BO_Class_Con.EClassYear>)ViewState["tblYearly"];

            int i = 0;
            foreach (RealEntity.C_08_PPlan.BO_Class_Con.EClassYear c1 in list)
            {
                Chart2.Series[i].Points.Add(new DataPoint(0, (c1.taramt / 10000000)));
                Chart2.Series[i].Points.Add(new DataPoint(1, (c1.examt / 10000000)));
                i++;
            }

            Chart2.DataSource = list;
            Chart2.DataBind();


        }
        private void GetPurMonth()
        {
            try
            {
                string comcod = this.GetCompCode();
                string curDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> list2 = objUserService.ShowConMonth(comcod, curDate);

                if (list2 == null)
                    return;

                this.grvMonthlyCon.DataSource = list2;
                this.grvMonthlyCon.DataBind();
                ViewState["tblMonthly"] = list2;

                ((Label)this.grvMonthlyCon.FooterRow.FindControl("lblyFAmt")).Text = list2.Select(p => p.taramt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlyCon.FooterRow.FindControl("lblyFtpayamt")).Text = list2.Select(p => p.examt).Sum().ToString("#,##0;(#,##0); ");


                this.BindChart();
            }
            catch (Exception ex)
            {
            }
        }
        private void BindChart()
        {
            List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly> lst = (List<RealEntity.C_08_PPlan.BO_Class_Con.EClassMonthly>)ViewState["tblMonthly"];

            //Chart1.Series["Series1"].XValueMember = "ymon";
            //Chart1.Series["Series1"].YValueMembers = "taramt";
            //Chart1.Series["Series2"].XValueMember = "ymon";
            //Chart1.Series["Series2"].YValueMembers = "examt";

            //Chart1.Series["Series1"].LegendText = "Target";
            //Chart1.Series["Series2"].LegendText = "Execution";

            //Chart1.DataSource = lst;
            //Chart1.DataBind();



            this.c1.Text = Convert.ToDouble(lst[0].examt).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.s1.Text = Convert.ToDouble(lst[0].taramt).ToString();//.ToString("#,##0.00;(#,##0.00);");
                                                                      //  this.b1.Text = Convert.ToDouble(lst[0].bal).ToString();//.ToString("#,##0.00;(#,##0.00);");


            this.c2.Text = Convert.ToDouble(lst[1].examt).ToString();
            this.s2.Text = Convert.ToDouble(lst[1].taramt).ToString();


            this.c3.Text = Convert.ToDouble(lst[2].examt).ToString();
            this.s3.Text = Convert.ToDouble(lst[2].taramt).ToString();

            this.c4.Text = Convert.ToDouble(lst[3].examt).ToString();
            this.s4.Text = Convert.ToDouble(lst[3].taramt).ToString();

            this.c5.Text = Convert.ToDouble(lst[4].examt).ToString();
            this.s5.Text = Convert.ToDouble(lst[4].taramt).ToString();


            this.c6.Text = Convert.ToDouble(lst[5].examt).ToString();
            this.s6.Text = Convert.ToDouble(lst[5].taramt).ToString();


            this.c7.Text = Convert.ToDouble(lst[6].examt).ToString();
            this.s7.Text = Convert.ToDouble(lst[6].taramt).ToString();


            this.c8.Text = Convert.ToDouble(lst[7].examt).ToString();
            this.s8.Text = Convert.ToDouble(lst[7].taramt).ToString();


            this.c9.Text = Convert.ToDouble(lst[8].examt).ToString();
            this.s9.Text = Convert.ToDouble(lst[8].taramt).ToString();


            this.c10.Text = Convert.ToDouble(lst[9].examt).ToString();
            this.s10.Text = Convert.ToDouble(lst[9].taramt).ToString();


            this.c11.Text = Convert.ToDouble(lst[10].examt).ToString();
            this.s11.Text = Convert.ToDouble(lst[10].taramt).ToString();



            this.c12.Text = Convert.ToDouble(lst[11].examt).ToString();
            this.s12.Text = Convert.ToDouble(lst[11].taramt).ToString();


        }

        private void GetPurWeekly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_08_PPlan.BO_Class_Con.EClassWeekly> lst1 = objUserService.ShowPurWeekly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekCon.DataSource = lst1;
                this.grvWeekCon.DataBind();

                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wtaramt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wtaramt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wtaramt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wtaramt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wtaramt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wtaramt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wtaramt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wtaramt4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wtaramt1 + p.wtaramt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wtaramt1 + p.wtaramt2 + p.wtaramt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wtaramt1 + p.wtaramt2 + p.wtaramt3 + p.wtaramt4)).Sum().ToString("#,##0;(#,##0); ");


                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt1")).Text = (lst1.Select(p => p.wexamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wexamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt2")).Text = (lst1.Select(p => p.wexamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wexamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt3")).Text = (lst1.Select(p => p.wexamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wexamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt4")).Text = (lst1.Select(p => p.wexamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wexamt4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt2T")).Text = lst1.Select(p => (p.wexamt1 + p.wexamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt3T")).Text = lst1.Select(p => (p.wexamt1 + p.wexamt2 + p.wexamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekCon.FooterRow.FindControl("lblyFPatAmt4T")).Text = lst1.Select(p => (p.wexamt1 + p.wexamt2 + p.wexamt3 + p.wexamt4)).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }

        //private void GetDayWisePur()
        //{
        //    try
        //    {
        //        string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //        List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst = objUserService.ShowPurDayWise(CurDate1);
        //        if (lst == null)
        //            return;

        //        ViewState["tblDayWise"] = HiddenSameData(lst);
        //        this.GvDayWise.DataSource = lst;
        //        this.GvDayWise.DataBind();
        //        this.FooterCalculation();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> HiddenSameData(List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst3)
        //{
        //    if (lst3.Count == 0)
        //        return lst3;

        //    int i = 0;
        //    string billno = "";
        //    foreach (RealEntity.C_14_Pro.EClassPur.EClassDayWisePur c1 in lst3)
        //    {
        //        if (i == 0)
        //        {
        //            billno = c1.billno;
        //            i++;
        //            continue;

        //        }
        //        else if (c1.billno == billno)
        //        {
        //            c1.billno1 = "";
        //            c1.billdate1 = "";
        //            c1.pactdesc = "";
        //            c1.vounum1 = "";
        //            c1.ssirdesc = "";
        //        }
        //        billno = c1.billno;

        //    }

        //    return lst3;

        //}
        //private void FooterCalculation()
        //{


        //    List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst3 = (List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur>)ViewState["tblDayWise"];
        //    if (lst3.Count == 0)
        //        return;

        //    ((Label)this.GvDayWise.FooterRow.FindControl("lblFitmamt")).Text = Convert.ToDouble(lst3.Select(p => p.billamt).Sum()).ToString("#,##0;(#,##0); ");

        //}
        //private void GetDayWisePay()
        //{
        //    try
        //    {
        //        string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
        //        List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst = objUserService.ShowPayDayWise(CurDate1);
        //        if (lst == null)
        //            return;

        //        this.gvDayWisePay.DataSource = HiddenSameData2(lst);
        //        this.gvDayWisePay.DataBind();
        //        if (lst.Count < 0)
        //            return;
        //        ((Label)this.gvDayWisePay.FooterRow.FindControl("lblFamount")).Text = lst.Select(p => p.payamt).Sum().ToString("#,##0;(#,##0); ");

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //private List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> HiddenSameData2(List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst3)
        //{
        //    if (lst3.Count == 0)
        //        return lst3;

        //    int i = 0;
        //    string vounum = "";
        //    foreach (RealEntity.C_14_Pro.EClassPur.EClassDayWisePay c1 in lst3)
        //    {
        //        if (i == 0)
        //        {
        //            vounum = c1.vounum;
        //            i++;
        //            continue;

        //        }
        //        else if (c1.vounum == vounum)
        //        {
        //            c1.vounum1 = "";
        //            c1.voudat = "";
        //            c1.pactdesc = "";
        //            c1.billno1 = "";
        //            c1.cactdesc = "";
        //        }
        //        vounum = c1.vounum;

        //    }

        //    return lst3;

        //}


        protected void grvWeekCon_RowDataBound(object sender, GridViewRowEventArgs e)
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
                grvWeekCon.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

    }
}