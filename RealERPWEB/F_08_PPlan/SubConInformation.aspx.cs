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
using RealEntity.C_14_Pro;

namespace RealERPWEB.F_08_PPlan
{
    public partial class SubConInformation : System.Web.UI.Page
    {
        UserManPur objUserService = new UserManPur();
        ProcessAccess _DataEntry = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Contains ("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString ().IndexOf ('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                //if ((!ASTUtility.PagePermission (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Substring (0, indexofamp),
                //        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean (hst["permission"]))
                //    Response.Redirect ("~/AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1 (HttpContext.Current.Request.Url.AbsoluteUri.ToString ().Substring (0, indexofamp), (DataSet)Session["tblusrlog"]);

                ((Label)this.Master.FindControl("lblTitle")).Text = "Sub-Contractor DASHBOARD";
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length==0?false:(Convert.ToBoolean(dr1[0]["printable"]));
                this.txtDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");

                this.lbtnOk_Click(null, null);

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string qcomcod = this.Request.QueryString["comcod"] ?? "";
            comcod = qcomcod.Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.lblYear.Visible = true;
            this.lblWeek.Visible = true;
            this.lblMon.Visible = true;
            this.lblGrp.Visible = true;
            this.lblDetails.Visible = true;
            this.lblPayDet.Visible = true;
            this.GetSConYearly();
            this.GetSConMonth();
            this.GetSConWeekly();
            this.GetDayWiseSCon();
            this.GetDayWiseSConPay();
            //this.pnlbtn.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "ExecuteMyGraph();", true);

        }

        private void GetSConYearly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                List<RealEntity.C_14_Pro.EClassPur.EClassYearSCon> list = objUserService.ShowSConYearly(comcod, CurDate);

                if (list == null)
                    return;

                this.grvYearlyPur.DataSource = list;
                this.grvYearlyPur.DataBind();

                ViewState["tblYearly"] = list;
                this.BindChartYear();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindChartYear()
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassYearSCon> list = (List<RealEntity.C_14_Pro.EClassPur.EClassYearSCon>)ViewState["tblYearly"];

            int i = 0;
            foreach (RealEntity.C_14_Pro.EClassPur.EClassYearSCon c1 in list)
            {
                Chart2.Series[i].Points.Add(new DataPoint(0, (c1.tbamt / 10000000)));
                Chart2.Series[i].Points.Add(new DataPoint(1, (c1.tpayamt / 10000000)));
                i++;
            }

            Chart2.DataSource = list;
            Chart2.DataBind();


        }
        private void GetSConMonth()
        {
            try
            {
                string comcod = this.GetCompCode();
                string curDate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
                List<RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon> list2 = objUserService.ShowSConMonth(comcod, curDate);

                if (list2 == null)
                    return;

                this.grvMonthlyPur.DataSource = list2;
                this.grvMonthlyPur.DataBind();
                ViewState["tblMonthly"] = list2;

                ((Label)this.grvMonthlyPur.FooterRow.FindControl("lblyFAmt")).Text = list2.Select(p => p.tcbamt).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvMonthlyPur.FooterRow.FindControl("lblyFtpayamt")).Text = list2.Select(p => p.tcbpayamt).Sum().ToString("#,##0;(#,##0); ");


                this.BindChart();
            }
            catch (Exception ex)
            {
            }
        }
        private void BindChart()
        {
            List<RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon> lst = (List<RealEntity.C_14_Pro.EClassPur.EClassMonthlySCon>)ViewState["tblMonthly"];



            this.c1.Text = Convert.ToDouble(lst[0].tcbamt).ToString();//.ToString("#,##0.00;(#,##0.00);");
            this.s1.Text = Convert.ToDouble(lst[0].tcbpayamt).ToString();//.ToString("#,##0.00;(#,##0.00);");
                                                                         //  this.b1.Text = Convert.ToDouble(lst[0].bal).ToString();//.ToString("#,##0.00;(#,##0.00);");


            this.c2.Text = Convert.ToDouble(lst[1].tcbamt).ToString();
            this.s2.Text = Convert.ToDouble(lst[1].tcbpayamt).ToString();


            this.c3.Text = Convert.ToDouble(lst[2].tcbamt).ToString();
            this.s3.Text = Convert.ToDouble(lst[2].tcbpayamt).ToString();

            this.c4.Text = Convert.ToDouble(lst[3].tcbamt).ToString();
            this.s4.Text = Convert.ToDouble(lst[3].tcbpayamt).ToString();

            this.c5.Text = Convert.ToDouble(lst[4].tcbamt).ToString();
            this.s5.Text = Convert.ToDouble(lst[4].tcbpayamt).ToString();


            this.c6.Text = Convert.ToDouble(lst[5].tcbamt).ToString();
            this.s6.Text = Convert.ToDouble(lst[5].tcbpayamt).ToString();


            this.c7.Text = Convert.ToDouble(lst[6].tcbamt).ToString();
            this.s7.Text = Convert.ToDouble(lst[6].tcbpayamt).ToString();


            this.c8.Text = Convert.ToDouble(lst[7].tcbamt).ToString();
            this.s8.Text = Convert.ToDouble(lst[7].tcbpayamt).ToString();


            this.c9.Text = Convert.ToDouble(lst[8].tcbamt).ToString();
            this.s9.Text = Convert.ToDouble(lst[8].tcbpayamt).ToString();


            this.c10.Text = Convert.ToDouble(lst[9].tcbamt).ToString();
            this.s10.Text = Convert.ToDouble(lst[9].tcbpayamt).ToString();


            this.c11.Text = Convert.ToDouble(lst[10].tcbamt).ToString();
            this.s11.Text = Convert.ToDouble(lst[10].tcbpayamt).ToString();



            this.c12.Text = Convert.ToDouble(lst[11].tcbamt).ToString();
            this.s12.Text = Convert.ToDouble(lst[11].tcbpayamt).ToString();

        }

        private void GetSConWeekly()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_14_Pro.EClassPur.EClassWeekly> lst1 = objUserService.ShowSConWeekly(comcod, CurDate1);
                if (lst1 == null)
                    return;
                this.grvWeekPur.DataSource = lst1;
                this.grvWeekPur.DataBind();

                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt1")).Text = (lst1.Select(p => p.wpamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt2")).Text = (lst1.Select(p => p.wpamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt3")).Text = (lst1.Select(p => p.wpamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt4")).Text = (lst1.Select(p => p.wpamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpamt4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt2T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt3T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFAmt4T")).Text = lst1.Select(p => (p.wpamt1 + p.wpamt2 + p.wpamt3 + p.wpamt4)).Sum().ToString("#,##0;(#,##0); ");


                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt1")).Text = (lst1.Select(p => p.wpayamt1).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt2")).Text = (lst1.Select(p => p.wpayamt2).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt2).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt3")).Text = (lst1.Select(p => p.wpayamt3).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt3).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt4")).Text = (lst1.Select(p => p.wpayamt4).Sum() == 0.00) ? "0.00" : lst1.Select(p => p.wpayamt4).Sum().ToString("#,##0;(#,##0); ");

                //((Label)this.grvWeekSales.FooterRow.FindControl("lblyFAmt1T")).Text = lst1.Select(p => p.wsamt1).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt2T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt3T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2 + p.wpayamt3)).Sum().ToString("#,##0;(#,##0); ");
                ((Label)this.grvWeekPur.FooterRow.FindControl("lblyFPatAmt4T")).Text = lst1.Select(p => (p.wpayamt1 + p.wpayamt2 + p.wpayamt3 + p.wpayamt4)).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }

        private void GetDayWiseSCon()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst = objUserService.ShowSConDayWise(comcod, CurDate1);
                if (lst == null)
                    return;

                ViewState["tblDayWise"] = HiddenSameData(lst);
                this.GvDayWise.DataSource = lst;
                this.GvDayWise.DataBind();
                this.FooterCalculation();

            }
            catch (Exception ex)
            {

            }
        }
        private List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> HiddenSameData(List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string billno = "";
            foreach (RealEntity.C_14_Pro.EClassPur.EClassDayWisePur c1 in lst3)
            {
                if (i == 0)
                {
                    billno = c1.billno;
                    i++;
                    continue;

                }
                else if (c1.billno == billno)
                {
                    c1.billno1 = "";
                    c1.billdate1 = "";
                    c1.pactdesc = "";
                    c1.vounum1 = "";
                    c1.ssirdesc = "";
                }
                billno = c1.billno;

            }

            return lst3;

        }
        private void FooterCalculation()
        {


            List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur> lst3 = (List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePur>)ViewState["tblDayWise"];
            if (lst3.Count == 0)
                return;

            ((Label)this.GvDayWise.FooterRow.FindControl("lblFitmamt")).Text = Convert.ToDouble(lst3.Select(p => p.billamt).Sum()).ToString("#,##0;(#,##0); ");

        }
        private void GetDayWiseSConPay()
        {
            try
            {
                string comcod = this.GetCompCode();
                string CurDate1 = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMM-yyyy");
                List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst = objUserService.ShowSConPayDayWise(comcod, CurDate1);
                if (lst == null)
                    return;

                this.gvDayWisePay.DataSource = HiddenSameData2(lst);
                this.gvDayWisePay.DataBind();
                if (lst.Count < 0)
                    return;
                ((Label)this.gvDayWisePay.FooterRow.FindControl("lblFamount")).Text = lst.Select(p => p.payamt).Sum().ToString("#,##0;(#,##0); ");

            }
            catch (Exception ex)
            {

            }
        }
        private List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> HiddenSameData2(List<RealEntity.C_14_Pro.EClassPur.EClassDayWisePay> lst3)
        {
            if (lst3.Count == 0)
                return lst3;

            int i = 0;
            string vounum = "";
            foreach (RealEntity.C_14_Pro.EClassPur.EClassDayWisePay c1 in lst3)
            {
                if (i == 0)
                {
                    vounum = c1.vounum;
                    i++;
                    continue;

                }
                else if (c1.vounum == vounum)
                {
                    c1.vounum1 = "";
                    c1.voudat = "";
                    c1.pactdesc = "";
                    c1.billno1 = "";
                    c1.cactdesc = "";
                }
                vounum = c1.vounum;

            }

            return lst3;

        }


        protected void grvWeekPur_RowDataBound(object sender, GridViewRowEventArgs e)
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
                grvWeekPur.Controls[0].Controls.AddAt(0, gvrow);
            }
        }

    }
}