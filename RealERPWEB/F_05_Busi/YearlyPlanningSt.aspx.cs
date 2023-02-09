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
namespace RealERPWEB.F_05_Busi
{
    public partial class YearlyPlanningSt : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if ((!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp),
                        (DataSet)Session["tblusrlog"])) && !Convert.ToBoolean(hst["permission"]))
                    Response.Redirect("~/AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Visible = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();
                //((Label)this.Master.FindControl("lblTitle")).Text = (type == "Income") ? "FINANCIAL PERFORMANCE BUDGET (ABP)" : "CASH BUDGET (ABP)";
                this.ViewSection();
                this.GetYear();
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void ViewSection()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "Income":
                case "CBudget":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }


        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            comcod=this.Request.QueryString["comcod"] ?? comcod;
           // comcod = this.Request.QueryString["comcod"]?? this.Request.QueryString["comcod"].ToString() : comcod;
           // comcod = this.Request.QueryString["comcod"].Length > 0 ? this.Request.QueryString["comcod"].ToString() : comcod;
            return comcod;


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

                case "Income":
                case "CBudget":
                    this.YearlyIncome();
                    break;
            }
        }


        private void YearlyIncome()
        {
            string comcod = this.GetCompCode();
            string Year = this.ddlyear.SelectedValue.ToString();
            string CallType = (this.Request.QueryString["Type"] == "Income") ? "RPTYEALRLYBGDINCOME" : "RPTYEALRLYCASHFLOW";
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_REPORT_YEARLYBUDGET", CallType, Year, "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.grvYearltIncome.DataSource = null;
                this.grvYearltIncome.DataBind();
                return;


            }
            ViewState["tblsal"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }


        private void FooterCalCulation()
        {

            DataTable dt = (DataTable)ViewState["tblsal"];
            DataTable dt1 = (DataTable)ViewState["tblsal"];
            if (dt.Rows.Count == 0 || dt1.Rows.Count == 0)
                return;
            DataView dv = dt.DefaultView;
            dv.RowFilter = "rescode in ('02002AAA','02004002','02005001')";
            dt = dv.ToTable();

            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = "rescode in ('02003AAA','02004001','02005002')";
            dt1 = dv.ToTable();
            //Inflow
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lblTotalInflow")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(tamt)", "")) ? 0.00
                : dt.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt1i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt1)", "")) ? 0.00
                : dt.Compute("Sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt2i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt2)", "")) ? 0.00
                : dt.Compute("Sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt3i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt3)", "")) ? 0.00
                : dt.Compute("Sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt4i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt4)", "")) ? 0.00
                : dt.Compute("Sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt5i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt5)", "")) ? 0.00
                : dt.Compute("Sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt6i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt6)", "")) ? 0.00
                : dt.Compute("Sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt7i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt7)", "")) ? 0.00
                : dt.Compute("Sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt8i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt8)", "")) ? 0.00
                : dt.Compute("Sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt9i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt9)", "")) ? 0.00
                : dt.Compute("Sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt10i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt10)", "")) ? 0.00
                : dt.Compute("Sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt11i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt11)", "")) ? 0.00
                : dt.Compute("Sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt12i")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(amt12)", "")) ? 0.00
                : dt.Compute("Sum(amt12)", ""))).ToString("#,##0;(#,##0); ");

            //Out Flow
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lblTotalOutflow")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(tamt)", "")) ? 0.00
              : dt1.Compute("Sum(tamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt1o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt1)", "")) ? 0.00
             : dt1.Compute("Sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt2o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt2)", "")) ? 0.00
             : dt1.Compute("Sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt3o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt3)", "")) ? 0.00
             : dt1.Compute("Sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt4o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt4)", "")) ? 0.00
             : dt1.Compute("Sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt5o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt5)", "")) ? 0.00
             : dt1.Compute("Sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt6o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt6)", "")) ? 0.00
             : dt1.Compute("Sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt7o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt7)", "")) ? 0.00
             : dt1.Compute("Sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt8o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt8)", "")) ? 0.00
             : dt1.Compute("Sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt9o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt9)", "")) ? 0.00
             : dt1.Compute("Sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt10o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt10)", "")) ? 0.00
             : dt1.Compute("Sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt11o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt11)", "")) ? 0.00
             : dt1.Compute("Sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.grvYearltIncome.FooterRow.FindControl("lgvFamt12o")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("Sum(amt12)", "")) ? 0.00
             : dt1.Compute("Sum(amt12)", ""))).ToString("#,##0;(#,##0); ");

        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            DataTable tbl1 = (DataTable)ViewState["tblsal"];
            switch (Type)
            {
                case "CBudget":
                case "Income":
                    this.grvYearltIncome.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.grvYearltIncome.DataSource = tbl1;
                    this.grvYearltIncome.DataBind();
                    this.FooterCalCulation();
                    break;
            }
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            //string grp = dt1.Rows[0]["grp"].ToString();

            switch (type)
            {
                case "Income":
                    //for (int j = 1; j < dt1.Rows.Count; j++)
                    //{
                    //    if (dt1.Rows[j]["grp"].ToString() == grp)
                    //    {
                    //        grp = dt1.Rows[j]["grp"].ToString();
                    //        dt1.Rows[j]["grpdesc"] = "";

                    //    }

                    //    else
                    //    {
                    //        if (dt1.Rows[j]["grp"].ToString() == grp)
                    //        {
                    //            dt1.Rows[j]["grpdesc"] = "";
                    //        }
                    //        grp = dt1.Rows[j]["grp"].ToString();
                    //    }

                    //}
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



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {

                case "Income":
                case "CBudget":
                    this.PrintYearlyIncome();
                    break;
            }

        }
        private void PrintYearlyIncome()
        {
            // IQBAL NAYAN
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
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
            //ReportDocument rptstk = new RealERPRPT.R_05_Busi.RptYePlanIncomeSt();
            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;

            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = (this.Request.QueryString["Type"] == "Income") ? "FINANCIAL PERFORMANCE BUDGET (ABP)" : "CASH BUDGET (ABP)";

            //TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            //txtdate.Text = "For Year " + this.ddlyear.SelectedItem.Text;




            //TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            //rptstk.SetDataSource(dt1);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstk.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstk;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
                if (ASTUtility.Right(code, 2) == "AA")
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
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}