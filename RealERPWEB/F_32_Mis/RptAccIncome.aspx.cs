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
namespace RealERPWEB.F_32_Mis
{
    public partial class RptAccIncome : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();

                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + date.Substring(2);
                this.txttodate.Text = date;
                this.SelectView();
                ((Label)this.Master.FindControl("lblTitle")).Text = (type == "IncomeMonthly") ? "Income Statement"
                    : "Month Wise Payment-Summary";





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
                case "IncomeMonthly":
                    this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                    this.txtfromdate.Text = Convert.ToDateTime("01-Jan" + (this.txtfromdate.Text.Trim()).Substring(6)).ToString("dd-MMM-yyyy");
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

                case "IncomeMonthly":

                    this.ShowMonIncome();
                    break;
            }
        }



        private void ShowMonIncome()
        {

            ViewState.Remove("tbAccIncome");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");

            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_ACCOUNTS_RP_02", "MONINCOMESTATMENT", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvIncomeMon.DataSource = null;
                this.gvIncomeMon.DataBind();
                return;
            }


            ViewState["tbAccIncome"] = ds1.Tables[0];
            this.Data_Bind();



        }



        //private DataTable HiddenSameData(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return dt1;
        //    string type = this.Request.QueryString["Type"].ToString().Trim();
        //    switch (type)
        //    {

        //        case "IncomeMonthly":
        //            string grpcode = dt1.Rows[0]["grpcode"].ToString();
        //            for (int j = 1; j < dt1.Rows.Count; j++)
        //            {
        //                if (dt1.Rows[j]["grpcode"].ToString() == grpcode)
        //                {
        //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
        //                    dt1.Rows[j]["grpcode"] = "";
        //                }

        //                else
        //                    grpcode = dt1.Rows[j]["grpcode"].ToString();
        //            }

        //            break;





        //    }

        //    return dt1;

        //}


        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            DateTime datefrm, dateto;
            DataView dv; DataTable dt;
            switch (type)
            {


                case "IncomeMonthly":
                    dv = ((DataTable)ViewState["tbAccIncome"]).Copy().DefaultView;
                    //dv.RowFilter = ("pactcode  like '%99BBBBAAAAAA%'");
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

                    this.gvIncomeMon.Columns[4].Visible = (amt1 != 0);
                    this.gvIncomeMon.Columns[5].Visible = (amt2 != 0);
                    this.gvIncomeMon.Columns[6].Visible = (amt3 != 0);
                    this.gvIncomeMon.Columns[7].Visible = (amt4 != 0);
                    this.gvIncomeMon.Columns[8].Visible = (amt5 != 0);
                    this.gvIncomeMon.Columns[9].Visible = (amt6 != 0);
                    this.gvIncomeMon.Columns[10].Visible = (amt7 != 0);
                    this.gvIncomeMon.Columns[11].Visible = (amt8 != 0);
                    this.gvIncomeMon.Columns[12].Visible = (amt9 != 0);
                    this.gvIncomeMon.Columns[13].Visible = (amt10 != 0);
                    this.gvIncomeMon.Columns[14].Visible = (amt11 != 0);
                    this.gvIncomeMon.Columns[15].Visible = (amt12 != 0);



                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 4; i < 16; i++)
                    {
                        if (datefrm > dateto)
                            break;

                        this.gvIncomeMon.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvIncomeMon.DataSource = (DataTable)ViewState["tbAccIncome"];
                    this.gvIncomeMon.DataBind();
                    //this.FooterCalculation();
                    break;



            }

        }


        //private void FooterCalculation()
        //{
        //    DataTable dt1 = (DataTable)ViewState["tbAccIncome"];
        //    if (dt1.Rows.Count == 0)
        //        return;
        //    string type = this.Request.QueryString["Type"].ToString().Trim();
        //    DataTable dt4;
        //    DataView dv1;
        //    switch (type)
        //    {

        //        case "MonPaymentSumm":
        //            dt4 = dt1.Copy();
        //            dv1 = dt4.DefaultView;
        //            dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
        //            dt4 = dv1.ToTable();

        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFtoamtmpaysum")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
        //            ((Label)this.gvIncomeMon.FooterRow.FindControl("lgvFamtmpaysum12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
        //            break;


        //    }



        //}




        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "IncomeMonthly":
                    this.RptIncomeMonthly();
                    break;

            }


        }



        private void RptIncomeMonthly()
        {
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt1 = (DataTable)ViewState["tbAccIncome"];
            //if (dt1.Rows.Count == 0)
            //    return;
            //ReportDocument rptstk = new RealERPRPT.R_17_Acc.RptIncomeMonthly();



            //TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            //txtCompany.Text = comnam;
            //TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            //txtHeader.Text = "Income Statement (Month Wise)";

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
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }




        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }

        protected void gvIncomeMon_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HLgvDescpaysum = (HyperLink)e.Row.FindControl("HLgvDescpaysum");
                Label lgvtoamtmpaysum = (Label)e.Row.FindControl("lgvtoamtmpaysum");
                Label lgvamtmpaysum1 = (Label)e.Row.FindControl("lgvamtmpaysum1");
                Label lgvamtmpaysum2 = (Label)e.Row.FindControl("lgvamtmpaysum2");
                Label lgvamtmpaysum3 = (Label)e.Row.FindControl("lgvamtmpaysum3");
                Label lgvamtmpaysum4 = (Label)e.Row.FindControl("lgvamtmpaysum4");
                Label lgvamtmpaysum5 = (Label)e.Row.FindControl("lgvamtmpaysum5");
                Label lgvamtmpaysum6 = (Label)e.Row.FindControl("lgvamtmpaysum6");
                Label lgvamtmpaysum7 = (Label)e.Row.FindControl("lgvamtmpaysum7");
                Label lgvamtmpaysum8 = (Label)e.Row.FindControl("lgvamtmpaysum8");
                Label lgvamtmpaysum9 = (Label)e.Row.FindControl("lgvamtmpaysum9");
                Label lgvamtmpaysum10 = (Label)e.Row.FindControl("lgvamtmpaysum10");
                Label lgvamtmpaysum11 = (Label)e.Row.FindControl("lgvamtmpaysum11");
                Label lgvamtmpaysum12 = (Label)e.Row.FindControl("lgvamtmpaysum12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();
                //string grp = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grp")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HLgvDescpaysum.Font.Bold = true;
                    lgvtoamtmpaysum.Font.Bold = true;
                    lgvamtmpaysum1.Font.Bold = true;
                    lgvamtmpaysum2.Font.Bold = true;
                    lgvamtmpaysum3.Font.Bold = true;
                    lgvamtmpaysum4.Font.Bold = true;
                    lgvamtmpaysum5.Font.Bold = true;
                    lgvamtmpaysum6.Font.Bold = true;
                    lgvamtmpaysum7.Font.Bold = true;
                    lgvamtmpaysum8.Font.Bold = true;
                    lgvamtmpaysum9.Font.Bold = true;
                    lgvamtmpaysum10.Font.Bold = true;
                    lgvamtmpaysum11.Font.Bold = true;
                    lgvamtmpaysum12.Font.Bold = true;
                    //HLgvDescpaysum.Style.Add("text-align", "right");
                }

                //if (grp == "4" && ASTUtility.Right(code, 10) == "0000000000")
                //{

                //    string pactdesc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactdesc")).ToString();

                //    //HLgvDescpaysum.NavigateUrl = "~/F_32_Mis/LinkMis.aspx?Type=ResCostDet&rescode=" + code + "&resdesc=" + pactdesc + "&frmdate=" + this.txtfromdate.Text.Trim() + "&todate=" + this.txttodate.Text.Trim();

                //}




            }
        }
    }
}
