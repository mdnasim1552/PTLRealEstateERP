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
namespace RealERPWEB.F_46_GrMgtInter
{
    public partial class RptGrpDailyReport : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtDate.Text = Convert.ToDateTime("01" + date.Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                this.CallCompanyList();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Management Interface";
                //this.chkConsolidate.Checked = true;

                this.lbtnOk_Click(null, null);


            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void CallCompanyList()
        {
            string comcod = this.GetCompCode();
            string CallType = (comcod.Substring(0, 1) == "8") ? "COMPLIST" : "GETSINGLECOM";
            DataSet ds1 = this.MktData.GetTransInfo(comcod, "SP_REPORTO_GROUP_ACC_TB_RP", CallType, "", "", "", "", "", "", "", "", "");
            Session["tbcomp"] = ds1.Tables[0];

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblData");
            DataTable dt = (DataTable)Session["tbcomp"];
            this.lblToDayAc.Visible = true;
            this.PanelAch.Visible = true;
            this.lblSales.Visible = true;
            this.PSales.Visible = true;
            this.lblColl.Visible = true;
            this.PCollection.Visible = true;
            this.lblCollBrk.Visible = true;
            this.lblRecPay.Visible = true;
            this.PanelRec.Visible = true;
            this.lblAvFund.Visible = true;
            this.lblChqisu.Visible = true;
            this.lblRecPayiss.Visible = true;
            this.PanelRecIsu.Visible = true;
            this.lblProcurement.Visible = true;

            this.lblStock.Visible = true;

            this.lblMonProStatus.Visible = true;
            this.lblHrMgt.Visible = true;
            string comp1 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comp1 += dt.Rows[i]["comcod"];
            }

            string frdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


            DataSet ds1 = MktData.GetTransInfo(comp1, "SP_REPORT_GROUP_MIS03", "RPTMGTDAILYACT", frdate, todate, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDayWSale.DataSource = null;
                this.gvDayWSale.DataBind();
                return;
            }

            ViewState["tblData"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["tblDesc"] = ds1.Tables[1];
            ViewState["tblDataGrp"] = ds1.Tables[2];
            this.Data_Bind();
            this.ShowGraph();
        }

        private void Data_Bind()
        {

            DataTable dt = ((DataTable)ViewState["tblData"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();



            // Achiv
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'A'");
            dt1 = dvr.ToTable();
            this.grvToAch.DataSource = dt1;
            this.grvToAch.DataBind();

            // Sales
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'B'");
            dt1 = dvr.ToTable();
            this.gvDayWSale.DataSource = dt1;
            this.gvDayWSale.DataBind();
            // this.FooterCalculation(dt1, "gvDayWSale");   

            //--------Collection-----
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'C'");
            dt1 = dvr.ToTable();
            this.gvCollSt.DataSource = dt1;
            this.gvCollSt.DataBind();


            //--------Collection Break Down-----
            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'D'");
            dt1 = dvr.ToTable();
            this.gvrcoll.DataSource = dt1;
            this.gvrcoll.DataBind();

            // Receipts & Payments

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'E'");
            dt1 = dvr.ToTable();
            this.grvRecPay.DataSource = dt1;
            this.grvRecPay.DataBind();

            // Receipts & Payments

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'F'");
            dt1 = dvr.ToTable();
            this.grvAvFund.DataSource = dt1;
            this.grvAvFund.DataBind();


            ///---------Cheque Issued---------
            DataTable dtpname = (DataTable)ViewState["tblDesc"];
            int j = 2;
            for (int i = 0; i < dtpname.Rows.Count; i++)
            {

                this.gvChqIsu.Columns[j].HeaderText = dtpname.Rows[i]["actdesc"].ToString();
                j++;
                if (j == 8)
                    break;


            }

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'G'");
            dt1 = dvr.ToTable();
            this.gvChqIsu.DataSource = dt1;
            this.gvChqIsu.DataBind();


            //C. Receipts & Payments Issued

            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'H'");
            dt1 = dvr.ToTable();
            this.gvRecPayiss.DataSource = dt1;
            this.gvRecPayiss.DataBind();




            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'I'");
            dt1 = dvr.ToTable();
            this.gvprocure.DataSource = dt1;
            this.gvprocure.DataBind();



            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'J'");
            dt1 = dvr.ToTable();
            //this.gvpstk.Columns[5].HeaderText = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMM- yyyy");
            this.gvpstk.DataSource = dt1;
            this.gvpstk.DataBind();



            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'K'");
            dt1 = dvr.ToTable();
            this.gvmonprost.DataSource = dt1;
            this.gvmonprost.DataBind();



            dvr = dt.DefaultView;
            dvr.RowFilter = ("grp = 'L'");
            dt1 = dvr.ToTable();
            this.gvHremp.DataSource = dt1;
            this.gvHremp.DataBind();


        }


        private void ShowGraph()
        {
            DataTable dt = ((DataTable)ViewState["tblData"]).Copy();
            DataTable dt1 = new DataTable();
            DataView dvr = new DataView();
            double amt1 = 0.00, amt2 = 0.00, amt3 = 0.00, amt4 = 0.00, amt5 = 0.00, amt6 = 0.00, amt7 = 0.00, amt8 = 0.00;

            string category = "";

            decimal[] values = new decimal[1];
            decimal[] values2 = new decimal[1];
            decimal[] values3 = new decimal[1];
            decimal[] values4 = new decimal[1];
            decimal[] values5 = new decimal[1];
            decimal[] values6 = new decimal[1];
            decimal[] values7 = new decimal[1];
            decimal[] values8 = new decimal[1];
            decimal[] values9 = new decimal[1];
            decimal[] values10 = new decimal[1];

            //////---------------------------------------------//////////////////////

            DataRow[] dr = dt.Select("comcod='AAAA'");
            if (dr.Length > 0)
            {

                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'B' and comcod= 'AAAA' ");
                dt1 = dvr.ToTable();
                if (dt1.Rows.Count != 0)
                {

                    amt2 = Convert.ToDouble(dt1.Rows[0]["tosaleamt"]) / 10000000;
                    string tosaleamt = Convert.ToDouble(amt2).ToString("#,##0.00;(#,##0.00);");
                    amt3 = Convert.ToDouble(dt1.Rows[0]["acsaleamt"]) / 10000000;
                    string acsaleamt = Convert.ToDouble(amt3).ToString("#,##0.00;(#,##0.00);");



                    values[0] = Convert.ToDecimal(tosaleamt);
                    values2[0] = Convert.ToDecimal(acsaleamt);

                    BarChart1.CategoriesAxis = category.Remove(0, 0);

                    BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Target As of Today  " });
                    BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values2, BarColor = "red", Name = "Achieve Total" });
                }
                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'C' and comcod= 'AAAA' ");
                dt1 = dvr.ToTable();

                amt2 = Convert.ToDouble(dt1.Rows[0]["tastcollamt"]) / 10000000;
                string tastcollamt = Convert.ToDouble(amt2).ToString("#,##0.00;(#,##0.00);");
                amt3 = Convert.ToDouble(dt1.Rows[0]["accollam"]) / 10000000;
                string accollam = Convert.ToDouble(amt3).ToString("#,##0.00;(#,##0.00);");

                values3[0] = Convert.ToDecimal(tastcollamt);
                values4[0] = Convert.ToDecimal(accollam);

                BarChart2.CategoriesAxis = category.Remove(0, 0);

                BarChart2.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values3, BarColor = "#2fd1f9", Name = "Target As of Today  " });
                BarChart2.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values4, BarColor = "red", Name = "Achieve Total" });

                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'E' and comcod= 'AAAA' ");
                dt1 = dvr.ToTable();

                amt5 = Convert.ToDouble(dt1.Rows[0]["recpam"]) / 10000000;
                string recpam = Convert.ToDouble(amt5).ToString("#,##0.00;(#,##0.00);");
                amt6 = Convert.ToDouble(dt1.Rows[0]["payam"]) / 10000000;
                string payam = Convert.ToDouble(amt6).ToString("#,##0.00;(#,##0.00);");

                values5[0] = Convert.ToDecimal(recpam);
                values6[0] = Convert.ToDecimal(payam);

                BarChart3.CategoriesAxis = category.Remove(0, 0);

                BarChart3.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values5, BarColor = "#2fd1f9", Name = "Receipts  " });
                BarChart3.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values6, BarColor = "red", Name = "Payments" });


                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'H' and comcod= 'AAAA' ");
                dt1 = dvr.ToTable();

                amt7 = Convert.ToDouble(dt1.Rows[0]["recpamis"]) / 10000000;
                string recpamis = Convert.ToDouble(amt7).ToString("#,##0.00;(#,##0.00);");
                amt8 = Convert.ToDouble(dt1.Rows[0]["payamis"]) / 10000000;
                string payamis = Convert.ToDouble(amt8).ToString("#,##0.00;(#,##0.00);");

                values7[0] = Convert.ToDecimal(recpamis);
                values8[0] = Convert.ToDecimal(payamis);

                BarChart4.CategoriesAxis = category.Remove(0, 0);

                BarChart4.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values7, BarColor = "#2fd1f9", Name = "Receipts  " });
                BarChart4.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values8, BarColor = "red", Name = "Issued" });

                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'A' and comcod= 'AAAA' ");
                dt1 = dvr.ToTable();

                amt1 = Convert.ToDouble(dt1.Rows[0]["trec"]) / 10000000;
                string trec = Convert.ToDouble(amt1).ToString("#,##0.00;(#,##0.00);");
                amt2 = Convert.ToDouble(dt1.Rows[0]["tpay"]) / 10000000;
                string tpay = Convert.ToDouble(amt2).ToString("#,##0.00;(#,##0.00);");

                values9[0] = Convert.ToDecimal(trec);
                values10[0] = Convert.ToDecimal(tpay);

                BarChart5.CategoriesAxis = category.Remove(0, 0);

                BarChart5.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values9, BarColor = "#2fd1f9", Name = "Receipts  " });
                BarChart5.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values10, BarColor = "red", Name = "Payments" });
            }


            else
            {

                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'B'");
                dt1 = dvr.ToTable();


                amt2 = Convert.ToDouble(dt1.Rows[0]["tosaleamt"]) / 10000000;
                string tosaleamt = Convert.ToDouble(amt2).ToString("#,##0.00;(#,##0.00);");
                amt3 = Convert.ToDouble(dt1.Rows[0]["acsaleamt"]) / 10000000;
                string acsaleamt = Convert.ToDouble(amt3).ToString("#,##0.00;(#,##0.00);");



                values[0] = Convert.ToDecimal(tosaleamt);
                values2[0] = Convert.ToDecimal(acsaleamt);

                BarChart1.CategoriesAxis = category.Remove(0, 0);

                BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Target As of Today  " });
                BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values2, BarColor = "red", Name = "Achieve Total" });

                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'C'");
                dt1 = dvr.ToTable();

                amt2 = Convert.ToDouble(dt1.Rows[0]["tastcollamt"]) / 10000000;
                string tastcollamt = Convert.ToDouble(amt2).ToString("#,##0.00;(#,##0.00);");
                amt3 = Convert.ToDouble(dt1.Rows[0]["accollam"]) / 10000000;
                string accollam = Convert.ToDouble(amt3).ToString("#,##0.00;(#,##0.00);");

                values3[0] = Convert.ToDecimal(tastcollamt);
                values4[0] = Convert.ToDecimal(accollam);

                BarChart2.CategoriesAxis = category.Remove(0, 0);

                BarChart2.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values3, BarColor = "#2fd1f9", Name = "Target As of Today  " });
                BarChart2.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values4, BarColor = "red", Name = "Achieve Total" });

                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'E'");
                dt1 = dvr.ToTable();

                amt5 = Convert.ToDouble(dt1.Rows[0]["recpam"]) / 10000000;
                string recpam = Convert.ToDouble(amt5).ToString("#,##0.00;(#,##0.00);");
                amt6 = Convert.ToDouble(dt1.Rows[0]["payam"]) / 10000000;
                string payam = Convert.ToDouble(amt6).ToString("#,##0.00;(#,##0.00);");

                values5[0] = Convert.ToDecimal(recpam);
                values6[0] = Convert.ToDecimal(payam);

                BarChart3.CategoriesAxis = category.Remove(0, 0);

                BarChart3.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values5, BarColor = "#2fd1f9", Name = "Receipts  " });
                BarChart3.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values6, BarColor = "red", Name = "Payments" });


                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'H'");
                dt1 = dvr.ToTable();

                amt7 = Convert.ToDouble(dt1.Rows[0]["recpamis"]) / 10000000;
                string recpamis = Convert.ToDouble(amt7).ToString("#,##0.00;(#,##0.00);");
                amt8 = Convert.ToDouble(dt1.Rows[0]["payamis"]) / 10000000;
                string payamis = Convert.ToDouble(amt8).ToString("#,##0.00;(#,##0.00);");

                values7[0] = Convert.ToDecimal(recpamis);
                values8[0] = Convert.ToDecimal(payamis);

                BarChart4.CategoriesAxis = category.Remove(0, 0);

                BarChart4.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values7, BarColor = "#2fd1f9", Name = "Receipts  " });
                BarChart4.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values8, BarColor = "red", Name = "Issued" });

                //////---------------------------------------------//////////////////////
                dvr = dt.DefaultView;
                dvr.RowFilter = ("grp = 'A'");
                dt1 = dvr.ToTable();

                amt1 = Convert.ToDouble(dt1.Rows[0]["trec"]) / 10000000;
                string trec = Convert.ToDouble(amt1).ToString("#,##0.00;(#,##0.00);");
                amt2 = Convert.ToDouble(dt1.Rows[0]["tpay"]) / 10000000;
                string tpay = Convert.ToDouble(amt2).ToString("#,##0.00;(#,##0.00);");

                values9[0] = Convert.ToDecimal(trec);
                values10[0] = Convert.ToDecimal(tpay);

                BarChart5.CategoriesAxis = category.Remove(0, 0);

                BarChart5.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values9, BarColor = "#2fd1f9", Name = "Receipts  " });
                BarChart5.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values10, BarColor = "red", Name = "Payments" });




            }

        }

        //private DataTable HiddenSameData01(DataTable dt1)

        //   {
        //       if (dt1.Rows.Count == 0)
        //           return dt1;

        //       string deptcode = dt1.Rows[0]["deptcode"].ToString();
        //       for (int j = 1; j < dt1.Rows.Count; j++)
        //       {

        //           if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
        //               dt1.Rows[j]["deptname"] = "";


        //           deptcode = dt1.Rows[j]["deptcode"].ToString();
        //       }


        //       return dt1;

        //   }

        private void LinkBound()
        {

            //for (int i = 0; i < gvprocure.Rows.Count; i++)
            //{
            //    string comcod = ((Label)gvprocure.Rows[i].FindControl("lblgvcomcodepro")).Text.Trim();

            //    LinkButton lbtn1 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvreqpro");
            //    LinkButton lbtn2 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvreqapppro");
            //    LinkButton lbtn3 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvorderpro");
            //    LinkButton lbtn4 = (LinkButton)gvprocure.Rows[i].FindControl("lnkgvourchasepro");
            //    LinkButton lbtn5 = (LinkButton)gvprocure.Rows[i].FindControl("hlnkgvbillpro");
            //    if (lbtn1 != null)
            //    {
            //        if (lbtn1.Text.Trim().Length > 0)
            //            lbtn1.CommandArgument = comcod;
            //    }
            //    if (lbtn2 != null)
            //    {
            //        if (lbtn2.Text.Trim().Length > 0)
            //            lbtn2.CommandArgument = comcod;
            //    }

            //    if (lbtn3 != null)
            //    {
            //        if (lbtn3.Text.Trim().Length > 0)
            //            lbtn3.CommandArgument = comcod;
            //    }
            //    if (lbtn4 != null)
            //    {
            //        if (lbtn4.Text.Trim().Length > 0)
            //            lbtn4.CommandArgument = comcod;
            //    }

            //    if (lbtn5 != null)
            //    {
            //        if (lbtn5.Text.Trim().Length > 0)
            //            lbtn5.CommandArgument = comcod;
            //    }


            //}

        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = dt1.Rows[0]["comcod"].ToString();
            string grp = dt1.Rows[0]["grp"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if ((dt1.Rows[j]["comcod"].ToString() == comcod) && (dt1.Rows[j]["grp"].ToString() == grp))
                    dt1.Rows[j]["compname"] = "";




                comcod = dt1.Rows[j]["comcod"].ToString();
                grp = dt1.Rows[j]["grp"].ToString();
            }


            return dt1;

        }

        //private void FooterCalculation(DataTable dt, string GvName)
        //{
        //    if (dt.Rows.Count == 0)
        //        return;
        //    DataView dv = new DataView();
        //    DataTable dt2 = new DataTable();



        //    //DataTable dt = (DataTable)Session["tblData"];




        //    switch (GvName)
        //    {
        //        case "gvDayWSale":
        //             dv = dt.Copy().DefaultView;
        //             dv.RowFilter = ("pactcode='AAAAAAAAAAAA'");
        //             dt2 = dv.ToTable();
        //            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDTAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(tuamt)", "")) ?
        //                    0 : dt2.Compute("sum(tuamt)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvDayWSale.FooterRow.FindControl("lgvFDSAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt2.Compute("sum(suamt)", "")) ?
        //                            0 : dt2.Compute("sum(suamt)", ""))).ToString("#,##0;(#,##0); ");

        //           break;









        //    }


        //}


        protected void gvDayWSale_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label comname = (Label)e.Row.FindControl("lkgvcomnamesale");
                HyperLink tsaleamt = (HyperLink)e.Row.FindControl("hlnkgvtsaleamt");
                Label tosaleamt = (Label)e.Row.FindControl("lgvtosaleamt");
                HyperLink salamt = (HyperLink)e.Row.FindControl("hlnkgvDSAmt");
                Label perotsale = (Label)e.Row.FindControl("lgvperotsale");
                Label dayamt = (Label)e.Row.FindControl("lgvtdayamt");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                double per = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "perotsale"));
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    tsaleamt.Font.Bold = true;
                    tosaleamt.Font.Bold = true;
                    salamt.Font.Bold = true;

                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    dayamt.Style.Add("color", "Green");
                    tsaleamt.Style.Add("color", "blue");
                    salamt.Style.Add("color", "blue");
                    //graph.Style.Add("color", "blue");
                    tsaleamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Sales&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    salamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RptDayWSale&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");



                }
                if (per < 100)
                {
                    perotsale.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    perotsale.ForeColor = System.Drawing.Color.Green;
                }

            }




        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblData"];

            ReportDocument rrs1 = new ReportDocument();
            if (this.chkGrp.Checked == true)
            {
                rrs1 = new RealERPRPT.R_46_GrMgtInter.RptSalesGraph();

                TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                rptCname.Text = comnam;

                TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtFDate1.Text = "(From " + this.txtDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";


                rrs1.SetDataSource((DataTable)ViewState["tblDataGrp"]);
            }
            else
            {
                rrs1 = new RealERPRPT.R_46_GrMgtInter.RptGrpMisDailyAct();
                TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
                rptCname.Text = comnam;

                TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
                txtFDate1.Text = "(From " + this.txtDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";

                //DataTable dtrname = (DataTable)ViewState["tblDesc"];
                //int j = 1;
                //for (int i = 0; i < dtrname.Rows.Count; i++)
                //{

                //    TextObject rpttxth = rrs1.ReportDefinition.ReportObjects["amt" + j.ToString()] as TextObject;
                //    rpttxth.Text = dtrname.Rows[i]["actdesc"].ToString();
                //    j++;
                //    if (j == 7)
                //        break;



                TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
                txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
                rrs1.SetDataSource(dt1);
                //rrs1.Subreports["RptToDay.rpt"].SetDataSource((DataTable)ViewState["tblDataGrp"]);


            }

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rrs1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rrs1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            this.ShowGraph();

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tblData"];
            //ReportDocument rrs1 = new RealERPRPT.R_46_GrMgtInter.RptGrpMisDailyAct();
            //TextObject rptCname = rrs1.ReportDefinition.ReportObjects["txtComName"] as TextObject;
            //rptCname.Text = comnam;

            //TextObject txtFDate1 = rrs1.ReportDefinition.ReportObjects["txtDate"] as TextObject;
            //txtFDate1.Text = "(From " + this.txtDate.Text.Trim() + " To " + this.txttodate.Text.Trim() + ")";
            ////TextObject txtduesupto = rrs1.ReportDefinition.ReportObjects["txtduesupto"] as TextObject;
            ////txtduesupto.Text = "Dues Up to " + Convert.ToDateTime(this.txttodate.Text).ToString("MMM-yyyy");
            //TextObject txtuserinfo = rrs1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rrs1.SetDataSource(dt1);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rrs1.SetParameterValue("ComLogo", ComLogo);

            //Session["Report1"] = rrs1;
            //lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void gvRecPayiss_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Company = (Label)e.Row.FindControl("lblgvCompanycih");
                Label Amt = (Label)e.Row.FindControl("lgvrecamis");
                HyperLink Balamtis = (HyperLink)e.Row.FindControl("hlnkgvbalamis");




                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    Amt.Font.Bold = true;
                    Balamtis.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }

                else
                {

                    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        return;

                    }
                    Balamtis.Style.Add("color", "blue");
                    DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
                    Balamtis.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=IssuedVsCollect&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }

            }





        }


        protected void gvHremp_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("lnkgvcomname");
                Label toemp = (Label)e.Row.FindControl("lgvtoemp");
                Label netsalary = (Label)e.Row.FindControl("lgvnetsalary");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hrcomcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    toemp.Font.Bold = true;
                    netsalary.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkRptMgtInterface.aspx?comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }





            }


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //    Label comname = (Label)e.Row.FindControl("lblgvcomname");
            //    Label toemp = (Label)e.Row.FindControl("lgvtoemp");
            //    Label netsalary = (Label)e.Row.FindControl("lgvnetsalary");



            //    string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "compcod")).ToString();


            //    if (code == "")
            //    {
            //        return;
            //    }
            //    if (ASTUtility.Right(code, 4) == "AAAA")
            //    {


            //        comname.Font.Bold = true;
            //        toemp.Font.Bold = true;
            //        netsalary.Font.Bold = true;
            //        comname.Style.Add("text-align", "right");
            //    }





            //}

        }


        protected void gvBankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink bankposition = (HyperLink)e.Row.FindControl("hlnkgvbankposition");
                Label BankBal = (Label)e.Row.FindControl("lgvbankbalbp");
                Label banklia = (Label)e.Row.FindControl("lblgvbankliabp");
                Label netcbolia = (Label)e.Row.FindControl("lblgvnetcbolia");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                    return;

                if (comcod == "AAAA")
                {
                    bankposition.Font.Bold = true;
                    BankBal.Font.Bold = true;
                    banklia.Font.Bold = true;
                    netcbolia.Font.Bold = true;
                    bankposition.Style.Add("text-align", "right");
                }

                else
                {
                    bankposition.Style.Add("color", "blue");
                    bankposition.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BankPosition&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }



            }





        }



        protected void gvpstk01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamestk01");
                Label tstkamt = (Label)e.Row.FindControl("lgvtstkamt");
                Label soldamtstk = (Label)e.Row.FindControl("lgvsoldamtstk");
                Label unsoldamt = (Label)e.Row.FindControl("lgvunsoldamt");
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    tstkamt.Font.Bold = true;
                    soldamtstk.Font.Bold = true;
                    unsoldamt.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }






            }

        }

        protected void gvpstk_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomname");
                Label stkamt = (Label)e.Row.FindControl("lgvtstkamt");
                Label unsoldamt = (Label)e.Row.FindControl("lgvunsoldamt");

                Label soldamt = (Label)e.Row.FindControl("lgvsoldamt");

                Label toramt = (Label)e.Row.FindControl("lgvtoramt");
                Label atodues = (Label)e.Row.FindControl("lgvatodues");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    stkamt.Font.Bold = true;
                    unsoldamt.Font.Bold = true;
                    soldamt.Font.Bold = true;
                    toramt.Font.Bold = true;
                    atodues.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {
                    comname.Style.Add("color", "blue");
                    string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=SoldUnsold&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }





            }


        }

        protected void gvProjectStatus01_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnameps1");
                Label Sales = (Label)e.Row.FindControl("lgvSales");

                Label toamt = (Label)e.Row.FindControl("lgvtoamtps1");
                Label liaamt = (Label)e.Row.FindControl("lgvliaamt");
                Label netexpenses = (Label)e.Row.FindControl("lgvnetexpenses01");
                Label netloantamt = (Label)e.Row.FindControl("lgvnetloantamt");
                HyperLink prorptwqty = (HyperLink)e.Row.FindControl("hlnkgvprorptwqty");
                HyperLink probgdvsexp = (HyperLink)e.Row.FindControl("hlnkgvprobgdvsexp");
                HyperLink protrdaywise = (HyperLink)e.Row.FindControl("hlnkgvprotrdaywise");




                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {

                    Compname.Font.Bold = true;
                    Sales.Font.Bold = true;

                    toamt.Font.Bold = true;
                    liaamt.Font.Bold = true;
                    netexpenses.Font.Bold = true;
                    netloantamt.Font.Bold = true;
                    Compname.Style.Add("text-align", "right");
                }

                else
                {

                    Compname.Style.Add("color", "blue");
                    prorptwqty.Style.Add("color", "blue");
                    probgdvsexp.Style.Add("color", "blue");
                    protrdaywise.Style.Add("color", "blue");
                    Compname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();
                    prorptwqty.NavigateUrl = "~/F_45_GrAcc/LinkGrpProjReport02.aspx?comcod=" + code + "&date=" + this.txttodate.Text.Trim();
                    probgdvsexp.NavigateUrl = "~/F_45_GrAcc/LinkFinncialReports.aspx?Type=BE&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    protrdaywise.NavigateUrl = "~/F_45_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpProTrans&comcod=" + code;


                    //prorptataglance.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();
                    //proreport.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=PrjStatus&comcod=" + code + "&date=" + this.txttodate.Text.Trim();



                }

            }
        }

        protected void gvProjectStatus02_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnameps2");
                Label ExpAmt = (Label)e.Row.FindControl("lgvExpAmt");
                Label PAdvAmt = (Label)e.Row.FindControl("lgvPAdvAmt");
                Label LCNFAmt = (Label)e.Row.FindControl("lgvLCNFAmt");
                Label Ovmt = (Label)e.Row.FindControl("lgvOvmt");
                Label IAmt = (Label)e.Row.FindControl("lgvIAmt");
                Label texpamt = (Label)e.Row.FindControl("lgvtexamt");
                Label liaamt = (Label)e.Row.FindControl("lgvliaamt");
                Label netexpenses = (Label)e.Row.FindControl("lgvnetexpenses02");





                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {

                    Compname.Font.Bold = true;
                    ExpAmt.Font.Bold = true;
                    PAdvAmt.Font.Bold = true;
                    LCNFAmt.Font.Bold = true;
                    Ovmt.Font.Bold = true;
                    IAmt.Font.Bold = true;
                    texpamt.Font.Bold = true;
                    liaamt.Font.Bold = true;
                    netexpenses.Font.Bold = true;
                    Compname.Style.Add("text-align", "right");
                }



            }

        }



        protected void gvSalevsColl_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamesvscoll");
                Label tsales = (Label)e.Row.FindControl("lgvtsales");
                Label tosales = (Label)e.Row.FindControl("lgvtosales");
                Label acsales = (Label)e.Row.FindControl("lgvacsales");
                Label peronsal = (Label)e.Row.FindControl("lgvperonsal");
                Label tcoll = (Label)e.Row.FindControl("lgvtcoll");
                Label tocoll = (Label)e.Row.FindControl("lgvtocoll");
                Label accoll = (Label)e.Row.FindControl("lgvaccoll");
                Label peroncoll = (Label)e.Row.FindControl("lgvperoncoll");



                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    tsales.Font.Bold = true;
                    tosales.Font.Bold = true;
                    acsales.Font.Bold = true;

                    peronsal.Font.Bold = true;
                    tcoll.Font.Bold = true;
                    tocoll.Font.Bold = true;
                    accoll.Font.Bold = true;
                    peroncoll.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }





            }

        }


        protected void gvMMPlanVsAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink comname = (HyperLink)e.Row.FindControl("hlnkgvcomnamecons");
                Label masterplan = (Label)e.Row.FindControl("lgvmasterplan");
                Label monplan = (Label)e.Row.FindControl("lgvmonplan");
                Label ExecutionpAC = (Label)e.Row.FindControl("lgvExecutionpAC");
                Label PerMasPlan = (Label)e.Row.FindControl("lgvPerMasPlan");
                Label PerMonthPlan = (Label)e.Row.FindControl("lgvPerMonthPlan");
                HyperLink flrwiseprogress = (HyperLink)e.Row.FindControl("hlnkgvflrwiseprogress");
                HyperLink sysgentarget = (HyperLink)e.Row.FindControl("hlnkgvsysgentarget");
                HyperLink ineffect = (HyperLink)e.Row.FindControl("hlnkgvineffect");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {


                    comname.Font.Bold = true;
                    masterplan.Font.Bold = true;
                    monplan.Font.Bold = true;
                    ExecutionpAC.Font.Bold = true;
                    PerMasPlan.Font.Bold = true;
                    PerMonthPlan.Font.Bold = true;


                    comname.Style.Add("text-align", "right");
                }
                else
                {

                    comname.Style.Add("color", "blue");
                    flrwiseprogress.Style.Add("color", "blue");
                    sysgentarget.Style.Add("color", "blue");
                    ineffect.Style.Add("color", "blue");
                    comname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MasPVsMonPVsExAllPro&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    flrwiseprogress.NavigateUrl = "~/F_45_GrAcc/LinkConstruProgress.aspx?comcod=" + code + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    sysgentarget.NavigateUrl = "~/F_45_GrAcc/LinkGrpSysGenTarget.aspx?Type=SymGenTar&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                    ineffect.NavigateUrl = "~/F_45_GrAcc/LinkGrpInflaEffect.aspx?Type=RemainingCost&comcod=" + code + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");


                }





            }

        }

        protected void gvprocure_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label comname = (Label)e.Row.FindControl("lblgvDepartpro");
                HyperLink hlnkgvourchasepro = (HyperLink)e.Row.FindControl("hlnkgvourchasepro");
                Label monplan = (Label)e.Row.FindControl("lgvmonplan");
                Label Execution = (Label)e.Row.FindControl("lgvExecutionpAC");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "AAAA")
                {


                    comname.Font.Bold = true;
                    hlnkgvourchasepro.Font.Bold = true;
                    monplan.Font.Bold = true;
                    Execution.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }



                if (comcod != "AAAA")
                {
                    hlnkgvourchasepro.Style.Add("color", "blue");
                    hlnkgvourchasepro.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=Purchase&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }
            }

        }


        protected void lnkgvreqapppro_Click(object sender, EventArgs e)
        {
            string comcod = Convert.ToString(((LinkButton)sender).CommandArgument).Trim();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('~/F_45_GrAcc/LinkGrpMisPurChase.aspx?Type=ReqAppStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + "', target='_blank');</script>";


        }
        protected void lnkgvorderpro_Click(object sender, EventArgs e)
        {

        }
        protected void lnkgvourchasepro_Click(object sender, EventArgs e)
        {

        }
        protected void hlnkgvbillpro_Click(object sender, EventArgs e)
        {

        }






        protected void gvmonprost_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HyperLink Compname = (HyperLink)e.Row.FindControl("hlnkgvcomnamemps");
                Label Cost = (Label)e.Row.FindControl("lgvcostmps");
                Label collamt = (Label)e.Row.FindControl("lgvcollamtmps");
                Label netposition = (Label)e.Row.FindControl("lgvnetpositionmps");

                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();


                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Compname.Font.Bold = true;
                    Cost.Font.Bold = true;
                    collamt.Font.Bold = true;
                    netposition.Font.Bold = true;

                    Compname.Style.Add("text-align", "right");
                }

                else
                {

                    Compname.Style.Add("color", "blue");
                    Compname.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=MProStatus&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");



                }

            }
        }





        protected void gvCollSt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label comname = (Label)e.Row.FindControl("lkgvcomnameColl");
                HyperLink tcollamt = (HyperLink)e.Row.FindControl("hlnkgvtcollamt");
                Label tosaleamt = (Label)e.Row.FindControl("lgvtosaleamt");
                HyperLink achamt = (HyperLink)e.Row.FindControl("hlnkgvaccollAmt");
                Label dayamt = (Label)e.Row.FindControl("lgvtdayamt");
                Label perotsale = (Label)e.Row.FindControl("lgvperotsale");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                double per = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "perotcoll"));
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {
                    comname.Font.Bold = true;
                    tcollamt.Font.Bold = true;
                    tosaleamt.Font.Bold = true;
                    achamt.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
                else
                {
                    dayamt.Style.Add("color", "Green");
                    tcollamt.Style.Add("color", "blue");
                    achamt.Style.Add("color", "blue");
                    tcollamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=TarVsAch&Group=Collection&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    achamt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=CollSummary&comcod=" + code + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }
                if (per < 100)
                {
                    perotsale.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    perotsale.ForeColor = System.Drawing.Color.Green;
                }

            }



        }
        protected void gvrcoll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label comname = (Label)e.Row.FindControl("lkgvcomnameCollBrk");
                Label collection = (Label)e.Row.FindControl("lgvtocollection");
                Label reconamt = (Label)e.Row.FindControl("lgvreconamt");
                Label chqdep = (Label)e.Row.FindControl("lgvchqdep");
                Label inhrchq = (Label)e.Row.FindControl("lgvinhrchq");
                Label inhfchq = (Label)e.Row.FindControl("lgvinhfchq");
                Label inhpchq = (Label)e.Row.FindControl("lgvinhpchq");
                Label repchq = (Label)e.Row.FindControl("lgvrepchq");
                Label ncollamt = (Label)e.Row.FindControl("lgvncollamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {
                    comname.Font.Bold = true;
                    collection.Font.Bold = true;
                    reconamt.Font.Bold = true;
                    chqdep.Font.Bold = true;
                    inhrchq.Font.Bold = true;
                    inhfchq.Font.Bold = true;
                    inhpchq.Font.Bold = true;
                    repchq.Font.Bold = true;
                    ncollamt.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
            }




        }
        protected void grvRecPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Company = (Label)e.Row.FindControl("lblgvCompanyRecPay");
                Label amount1 = (Label)e.Row.FindControl("lblgvlmrecamt");
                Label amount2 = (Label)e.Row.FindControl("lgvcmrecamt");
                Label amount3 = (Label)e.Row.FindControl("lgvotrecamt");
                Label RecAmt = (Label)e.Row.FindControl("lgvrecpam");
                HyperLink PayAmt = (HyperLink)e.Row.FindControl("hlnkgvpayam");



                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    amount1.Font.Bold = true;
                    amount2.Font.Bold = true;
                    amount3.Font.Bold = true;
                    RecAmt.Font.Bold = true;
                    PayAmt.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }

                else
                {

                    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        return;

                    }
                    amount2.Style.Add("color", "Green");
                    PayAmt.Style.Add("color", "blue");
                    PayAmt.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=RecPay&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

                }

            }




        }
        protected void grvAvFund_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Company = (Label)e.Row.FindControl("lblgvCompanyFund");
                Label ainhfchq = (Label)e.Row.FindControl("hlnkgvainhfchq");
                Label ainhrchq = (Label)e.Row.FindControl("lgvainhrchq");
                Label adepchq = (Label)e.Row.FindControl("lgvadepchq");
                Label arepchq = (Label)e.Row.FindControl("lgvarepchq");
                HyperLink closbal = (HyperLink)e.Row.FindControl("Hypgvclosbal");
                Label bankbal = (Label)e.Row.FindControl("lgvbankbal");
                HyperLink ainhpchq = (HyperLink)e.Row.FindControl("hlnkainhpchq");
                Label tavamt = (Label)e.Row.FindControl("lgvtavamt");




                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    ainhfchq.Font.Bold = true;
                    ainhrchq.Font.Bold = true;
                    adepchq.Font.Bold = true;
                    arepchq.Font.Bold = true;
                    closbal.Font.Bold = true;
                    bankbal.Font.Bold = true;
                    ainhpchq.Font.Bold = true;
                    tavamt.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }

                else
                {

                    DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                    {
                        return;

                    }
                    ainhpchq.Style.Add("color", "blue");
                    closbal.Style.Add("color", "blue");
                    DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);
                    ainhpchq.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=ChequeInHand&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(txtopndate).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                    closbal.NavigateUrl = "~/F_45_GrAcc/LinkGrpMisDailyActivities.aspx?Type=BankPosition&comcod=" + comcod + "&Date1=" + Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }

            }




        }
        protected void gvChqIsu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label comname = (Label)e.Row.FindControl("lblgvCompanyChqIs");
                Label Amt1 = (Label)e.Row.FindControl("lgvamt1");
                Label Amt2 = (Label)e.Row.FindControl("lgvamt2");
                Label Amt3 = (Label)e.Row.FindControl("lgvamt3");
                Label Amt4 = (Label)e.Row.FindControl("lgvamt4");
                Label Amt5 = (Label)e.Row.FindControl("lgvamt5");
                Label Amt6 = (Label)e.Row.FindControl("lgvamt6");
                Label tamt = (Label)e.Row.FindControl("lgvtamt");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();
                if (code == "")
                {
                    return;
                }
                if (code == "AAAA")
                {
                    comname.Font.Bold = true;
                    Amt1.Font.Bold = true;
                    Amt2.Font.Bold = true;
                    Amt3.Font.Bold = true;
                    Amt4.Font.Bold = true;
                    Amt5.Font.Bold = true;
                    Amt6.Font.Bold = true;
                    tamt.Font.Bold = true;
                    comname.Style.Add("text-align", "right");
                }
            }
        }
        protected void grvToAch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Company = (Label)e.Row.FindControl("lkgvcomnameAch");
                Label tsal = (Label)e.Row.FindControl("lgvtsal");
                Label tcoll = (Label)e.Row.FindControl("lgvtcoll");
                Label trec = (Label)e.Row.FindControl("lgvtrec");
                Label tpay = (Label)e.Row.FindControl("lgvtpay");
                Label tcrec = (Label)e.Row.FindControl("lgvtcrec");
                Label tcisu = (Label)e.Row.FindControl("lgvtcisu");




                string comcod = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "comcod")).ToString();

                if (comcod == "")
                {
                    return;
                }
                if (comcod == "AAAA")
                {

                    Company.Font.Bold = true;
                    tsal.Font.Bold = true;
                    tcoll.Font.Bold = true;
                    trec.Font.Bold = true;
                    tpay.Font.Bold = true;
                    tcrec.Font.Bold = true;
                    tcisu.Font.Bold = true;
                    Company.Style.Add("text-align", "right");
                }



            }




        }
        protected void grvToAch_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "SALES & COLLECTION";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 2;
                TableCell cell2 = new TableCell();
                cell2.Text = "HONOURED";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 2;
                TableCell cell3 = new TableCell();
                cell3.Text = "CHEQUE";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 2;



                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                grvToAch.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvDayWSale_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "SALES TARGET";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 2;
                TableCell cell2 = new TableCell();
                cell2.Text = "SALES ACHIEVED";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 3;
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvDayWSale.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void gvCollSt_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "COLLECTION TARGET";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 2;
                TableCell cell2 = new TableCell();
                cell2.Text = "COLLECTION ACHIEVED";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 3;
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvCollSt.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvrcoll_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                TableCell cell2 = new TableCell();
                cell2.Text = "";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 1;
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;
                TableCell cell4 = new TableCell();
                cell4.Text = "IN HAND CHEQUE";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 3;
                TableCell cell5 = new TableCell();
                cell5.Text = "";
                cell5.HorizontalAlign = HorizontalAlign.Center;
                cell5.ColumnSpan = 1;
                TableCell cell6 = new TableCell();
                cell6.Text = "";
                cell6.HorizontalAlign = HorizontalAlign.Center;
                cell6.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvrow.Cells.Add(cell4);
                gvrow.Cells.Add(cell5);
                gvrow.Cells.Add(cell6);
                gvrcoll.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void grvRecPay_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                TableCell cell2 = new TableCell();
                cell2.Text = "RECEIPTS HONOURED";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 4;
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;
                TableCell cell4 = new TableCell();
                cell4.Text = "";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 1;


                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvrow.Cells.Add(cell4);
                grvRecPay.Controls[0].Controls.AddAt(0, gvrow);
            }

        }
        protected void grvAvFund_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "IN HAND CHEQUE";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 4;
                TableCell cell2 = new TableCell();
                cell2.Text = "BANK";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 2;
                TableCell cell3 = new TableCell();
                cell3.Text = "";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 1;
                TableCell cell4 = new TableCell();
                cell4.Text = "";
                cell4.HorizontalAlign = HorizontalAlign.Center;
                cell4.ColumnSpan = 1;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvrow.Cells.Add(cell3);
                gvrow.Cells.Add(cell4);
                grvAvFund.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvRecPayiss_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;

                TableCell cell3 = new TableCell();
                cell3.Text = "CHEQUED";
                cell3.HorizontalAlign = HorizontalAlign.Center;
                cell3.ColumnSpan = 2;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell3);
                gvRecPayiss.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvprocure_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;
                TableCell cell0 = new TableCell();
                cell0.Text = "";
                cell0.HorizontalAlign = HorizontalAlign.Center;
                cell0.ColumnSpan = 1;
                TableCell cell1 = new TableCell();
                cell1.Text = "";
                cell1.HorizontalAlign = HorizontalAlign.Center;
                cell1.ColumnSpan = 1;
                TableCell cell2 = new TableCell();
                cell2.Text = "WORK";
                cell2.HorizontalAlign = HorizontalAlign.Center;
                cell2.ColumnSpan = 2;


                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell0);
                gvrow.Cells.Add(cell1);
                gvrow.Cells.Add(cell2);
                gvprocure.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
    }
}
