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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_17_Acc
{
    public partial class RptRealInOutFlow : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;
                this.SelectView();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "RealFlow") ? "Month Wise Real Inflow & Outflow"
                //    : "Month Wise Payment-Summary";

                //this.Master.Page.Title = (type == "RealFlow") ? "Month Wise Real Inflow & Outflow"
                //    : "Month Wise Payment-Summary";



                this.lbtnOk_Click(null, null);


            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }


        private void SelectView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "RealFlow":
                    this.txtfromdate.Text = "01" + (System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy").Substring(2));
                    this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                    this.MultiView1.ActiveViewIndex = 0;
                    break;
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "RealFlow":

                    this.ShowMonFlow();
                    break;




            }
        }

        private void ShowMonFlow()
        {

            ViewState.Remove("tbRealFlow");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txtfromdate.Text.Trim()), Convert.ToDateTime(this.txttodate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP", "MONWINOUTFLOW", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonPayment.DataSource = null;
                this.gvMonPayment.DataBind();
                return;
            }


            ViewState["tbRealFlow"] = this.HiddenSameData(ds1.Tables[0]);
            ViewState["tbRealFlowChart"] = ds1.Tables[1];
            this.Data_Bind();
            this.table();
            this.BindChart();
        }
        private void table()
        {
            DataTable tblt01 = new DataTable();
            tblt01.Columns.Add("month", Type.GetType("System.String"));
            ViewState["tblt01"] = tblt01;
        }
        private void BindChart()
        {
            DataTable dt = (DataTable)ViewState["tbRealFlowChart"];

            DataTable dt1 = (DataTable)ViewState["tblt01"]; ;

            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 0; i < 12; i++)
            {
                if (datefrm > dateto)
                    break;
                DataRow dr3 = dt1.NewRow();
                dr3["month"] = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);
                dt1.Rows.Add(dr3);

            }

            string category = "";
            decimal[] values = new decimal[dt.Rows.Count];
            decimal[] values2 = new decimal[dt.Rows.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                category = category + "," + dt1.Rows[i]["month"].ToString();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //category = category + "," + dt.Rows[i]["grp"].ToString();
                values[i] = Convert.ToDecimal(dt.Rows[i]["inflow"]);
                values2[i] = Convert.ToDecimal(dt.Rows[i]["outflow"]);
            }

            BarChart1.CategoriesAxis = category.Remove(0, 1);
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values, BarColor = "#2fd1f9", Name = "Inflow" });
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = values2, BarColor = "#2fd5g9", Name = "Outflow" });
        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RealFlow":
                    string flow = dt1.Rows[0]["flow"].ToString();
                    string grp = dt1.Rows[0]["grp"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["flow"].ToString() == flow)
                        {
                            flow = dt1.Rows[j]["flow"].ToString();
                            dt1.Rows[j]["flowdesc"] = "";
                        }

                        if (dt1.Rows[j]["grp"].ToString() == grp)
                        {
                            grp = dt1.Rows[j]["grp"].ToString();
                            dt1.Rows[j]["grpdesc"] = "";
                        }

                        flow = dt1.Rows[j]["flow"].ToString();
                        grp = dt1.Rows[j]["grp"].ToString();
                    }

                    break;





            }

            return dt1;

        }


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            DateTime datefrm, dateto;
            DataView dv; DataTable dt;
            switch (type)
            {

                case "RealFlow":
                    dv = ((DataTable)ViewState["tbRealFlow"]).Copy().DefaultView;
                    dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
                    dt = dv.ToTable();
                    amt1 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt1)", "")) ? 0.00 : dt.Compute("sum(amt1)", "")));
                    amt2 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt2)", "")) ? 0.00 : dt.Compute("sum(amt2)", "")));
                    amt3 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt3)", "")) ? 0.00 : dt.Compute("sum(amt3)", "")));
                    amt4 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt4)", "")) ? 0.00 : dt.Compute("sum(amt4)", "")));
                    amt5 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt5)", "")) ? 0.00 : dt.Compute("sum(amt5)", "")));
                    amt6 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt6)", "")) ? 0.00 : dt.Compute("sum(amt6)", "")));
                    amt7 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt7)", "")) ? 0.00 : dt.Compute("sum(amt7)", "")));
                    amt8 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt8)", "")) ? 0.00 : dt.Compute("sum(amt8)", "")));
                    amt9 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt9)", "")) ? 0.00 : dt.Compute("sum(amt9)", "")));
                    amt10 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt10)", "")) ? 0.00 : dt.Compute("sum(amt10)", "")));
                    amt11 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt11)", "")) ? 0.00 : dt.Compute("sum(amt11)", "")));
                    amt12 = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(amt12)", "")) ? 0.00 : dt.Compute("sum(amt12)", "")));

                    this.gvMonPayment.Columns[3].Visible = (amt1 != 0);
                    this.gvMonPayment.Columns[4].Visible = (amt2 != 0);
                    this.gvMonPayment.Columns[5].Visible = (amt3 != 0);
                    this.gvMonPayment.Columns[6].Visible = (amt4 != 0);
                    this.gvMonPayment.Columns[7].Visible = (amt5 != 0);
                    this.gvMonPayment.Columns[8].Visible = (amt6 != 0);
                    this.gvMonPayment.Columns[9].Visible = (amt7 != 0);
                    this.gvMonPayment.Columns[10].Visible = (amt8 != 0);
                    this.gvMonPayment.Columns[11].Visible = (amt9 != 0);
                    this.gvMonPayment.Columns[12].Visible = (amt10 != 0);
                    this.gvMonPayment.Columns[13].Visible = (amt11 != 0);
                    this.gvMonPayment.Columns[14].Visible = (amt12 != 0);



                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvMonPayment.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvMonPayment.DataSource = (DataTable)ViewState["tbRealFlow"];
                    this.gvMonPayment.DataBind();
                    this.FooterCalculation();
                    break;



            }

        }


        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["tbRealFlow"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable dt4;
            DataView dv1;
            switch (type)
            {

                case "RealFlow":
                    dt4 = dt1.Copy();
                    dv1 = dt4.DefaultView;
                    dv1.RowFilter = ("pactcode='9BBBBBAAAAAA'");
                    dt4 = dv1.ToTable();

                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFtoamtmpay")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvMonPayment.FooterRow.FindControl("lgvFamtmpay12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "RealFlow":
                    this.PrintMonRecorPayment();
                    break;
            }


        }

        private void PrintMonRecorPayment()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string title = "Month Wise Real Inflow & Outflow";

            DataTable dt1 = (DataTable)ViewState["tbRealFlow"];
            if (dt1.Rows.Count == 0)
                return;
            var lst = dt1.DataTableToList<RealEntity.C_32_Mis.EClassAcc_03.EClassRealInOutFlow>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptSetupClass1.GetLocalReport("R_32_Mis.RptRealInOutFlow", lst, null, null);

            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            string date = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";
            string txtuserinfo = ASTUtility.Concat(compname, username, printdate);

            Rpt1.SetParameters(new ReportParameter("companyname", comnam));
            Rpt1.SetParameters(new ReportParameter("title", title));
            Rpt1.SetParameters(new ReportParameter("date", date));
            Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto) break;

                Rpt1.SetParameters(new ReportParameter("month" + i.ToString(), datefrm.ToString("MMM yy")));
                datefrm = datefrm.AddMonths(1);

            }

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";



            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptRealFlow();



            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = "Month Wise Real Inflow & Outflow";

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            //DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            //DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (datefrm > dateto)
            //        break;
            //    TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
            //    rpttxth.Text = datefrm.ToString("MMM yy");
            //    datefrm = datefrm.AddMonths(1);

            //}

            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                 ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvMonPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                Label lgvActdescmpay = (Label)e.Row.FindControl("lgvActdescmpay");
                HyperLink toamtmpay = (HyperLink)e.Row.FindControl("hlnkgvtoamtmpay");
                Label lgvamtmpay1 = (Label)e.Row.FindControl("lgvamtmpay1");
                Label lgvamtmpay2 = (Label)e.Row.FindControl("lgvamtmpay2");
                Label lgvamtmpay3 = (Label)e.Row.FindControl("lgvamtmpay3");
                Label lgvamtmpay4 = (Label)e.Row.FindControl("lgvamtmpay4");
                Label lgvamtmpay5 = (Label)e.Row.FindControl("lgvamtmpay5");
                Label lgvamtmpay6 = (Label)e.Row.FindControl("lgvamtmpay6");
                Label lgvamtmpay7 = (Label)e.Row.FindControl("lgvamtmpay7");
                Label lgvamtmpay8 = (Label)e.Row.FindControl("lgvamtmpay8");
                Label lgvamtmpay9 = (Label)e.Row.FindControl("lgvamtmpay9");
                Label lgvamtmpay10 = (Label)e.Row.FindControl("lgvamtmpay10");
                Label lgvamtmpay11 = (Label)e.Row.FindControl("lgvamtmpay11");
                Label lgvamtmpay12 = (Label)e.Row.FindControl("lgvamtmpay12");

                string flow = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "flow")).ToString();
                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    lgvActdescmpay.Font.Bold = true;
                    toamtmpay.Font.Bold = true;
                    lgvamtmpay1.Font.Bold = true;
                    lgvamtmpay2.Font.Bold = true;
                    lgvamtmpay3.Font.Bold = true;
                    lgvamtmpay4.Font.Bold = true;
                    lgvamtmpay5.Font.Bold = true;
                    lgvamtmpay6.Font.Bold = true;
                    lgvamtmpay7.Font.Bold = true;
                    lgvamtmpay8.Font.Bold = true;
                    lgvamtmpay9.Font.Bold = true;
                    lgvamtmpay10.Font.Bold = true;
                    lgvamtmpay11.Font.Bold = true;
                    lgvamtmpay12.Font.Bold = true;
                    lgvActdescmpay.Style.Add("text-align", "right");
                }

                if (flow == "A" && code == "99BBBBAAAAAA")
                {

                    lgvActdescmpay.Style.Add("color", "#009999");
                    toamtmpay.Style.Add("color", "blue");
                    lgvamtmpay1.Style.Add("color", "#009999");
                    lgvamtmpay2.Style.Add("color", "#009999");
                    lgvamtmpay3.Style.Add("color", "#009999");
                    lgvamtmpay4.Style.Add("color", "#009999");
                    lgvamtmpay5.Style.Add("color", "#009999");
                    lgvamtmpay6.Style.Add("color", "#009999");
                    lgvamtmpay7.Style.Add("color", "#009999");
                    lgvamtmpay8.Style.Add("color", "#009999");
                    lgvamtmpay9.Style.Add("color", "#009999");
                    lgvamtmpay10.Style.Add("color", "#009999");
                    lgvamtmpay11.Style.Add("color", "#009999");
                    lgvamtmpay12.Style.Add("color", "#009999");
                    toamtmpay.NavigateUrl = "~/F_17_Acc/LinkInflowAndOutflow.aspx?Type=MonReceipt&Date1=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");




                }
                if (flow == "B" && code == "99BBBBAAAAAA")
                {

                    lgvActdescmpay.Style.Add("color", "#CC33FF");
                    toamtmpay.Style.Add("color", "blue");
                    lgvamtmpay1.Style.Add("color", "#CC33FF");
                    lgvamtmpay2.Style.Add("color", "#CC33FF");
                    lgvamtmpay3.Style.Add("color", "#CC33FF");
                    lgvamtmpay4.Style.Add("color", "#CC33FF");
                    lgvamtmpay5.Style.Add("color", "#CC33FF");
                    lgvamtmpay6.Style.Add("color", "#CC33FF");
                    lgvamtmpay7.Style.Add("color", "#CC33FF");
                    lgvamtmpay8.Style.Add("color", "#CC33FF");
                    lgvamtmpay9.Style.Add("color", "#CC33FF");
                    lgvamtmpay10.Style.Add("color", "#CC33FF");
                    lgvamtmpay11.Style.Add("color", "#CC33FF");
                    lgvamtmpay12.Style.Add("color", "#CC33FF");
                    toamtmpay.NavigateUrl = "~/F_17_Acc/LinkInflowAndOutflow.aspx?Type=MonPaymentDet&Date1=" + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + "&Date2=" + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
                }


            }

        }
    }
}
