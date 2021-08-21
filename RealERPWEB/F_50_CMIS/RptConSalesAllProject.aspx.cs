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
namespace RealERPWEB.F_50_CMIS
{
    public partial class RptConSalesAllProject : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "SALES ALL PROJECT";
                string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01-" + ASTUtility.Right(date, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            ((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();
            // string comcod = "7101";
            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }

            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_REPORT_MISCON_SALMGT02", "RPTMONWISESALES", txtdatefrm, txtdateto, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvMonCollect.DataSource = null;
                this.gvMonCollect.DataBind();
                return;
            }


            ViewState["tblcollvscl"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string comcod = dt1.Rows[0]["comcod"].ToString();
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["comcod"].ToString() == comcod && dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    dt1.Rows[j]["comnam"] = "";
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["comcod"].ToString() == comcod)
                        dt1.Rows[j]["comnam"] = "";

                    if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                        dt1.Rows[j]["pactdesc"] = "";
                }

                comcod = dt1.Rows[j]["comcod"].ToString();
                pactcode = dt1.Rows[j]["pactcode"].ToString();

            }

            return dt1;
        }

        private void Data_Bind()
        {

            double amt1, amt2, amt3, amt4, amt5, amt6, amt7, amt8, amt9, amt10, amt11, amt12;
            DateTime datefrm, dateto;
            DataView dv; DataTable dt;
            dv = ((DataTable)ViewState["tblcollvscl"]).Copy().DefaultView;
            dv.RowFilter = ("pactcode  like '%99CCCCAAAAAA%'");
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

            this.gvMonCollect.Columns[3].Visible = (amt1 != 0);
            this.gvMonCollect.Columns[4].Visible = (amt2 != 0);
            this.gvMonCollect.Columns[5].Visible = (amt3 != 0);
            this.gvMonCollect.Columns[6].Visible = (amt4 != 0);
            this.gvMonCollect.Columns[7].Visible = (amt5 != 0);
            this.gvMonCollect.Columns[8].Visible = (amt6 != 0);
            this.gvMonCollect.Columns[9].Visible = (amt7 != 0);
            this.gvMonCollect.Columns[10].Visible = (amt8 != 0);
            this.gvMonCollect.Columns[11].Visible = (amt9 != 0);
            this.gvMonCollect.Columns[12].Visible = (amt10 != 0);
            this.gvMonCollect.Columns[13].Visible = (amt11 != 0);
            this.gvMonCollect.Columns[14].Visible = (amt12 != 0);

            datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 3; i < 15; i++)
            {
                if (datefrm > dateto)
                    break;

                this.gvMonCollect.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            this.gvMonCollect.DataSource = (DataTable)ViewState["tblcollvscl"];
            this.gvMonCollect.DataBind();
            this.FooterCalculation();

        }



        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;

            DataTable dt4;
            DataView dv1;
            dt4 = dt1.Copy();
            dv1 = dt4.DefaultView;
            dv1.RowFilter = ("pactcode='99BBBBAAAAAA'");
            dt4 = dv1.ToTable();

            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(toamt)", "")) ? 0.00 : dt4.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt1)", "")) ? 0.00 : dt4.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt2)", "")) ? 0.00 : dt4.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt3)", "")) ? 0.00 : dt4.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt4)", "")) ? 0.00 : dt4.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt5)", "")) ? 0.00 : dt4.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt6)", "")) ? 0.00 : dt4.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt7)", "")) ? 0.00 : dt4.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt8)", "")) ? 0.00 : dt4.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt9)", "")) ? 0.00 : dt4.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt10)", "")) ? 0.00 : dt4.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt11)", "")) ? 0.00 : dt4.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
            ((Label)this.gvMonCollect.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt4.Compute("sum(amt12)", "")) ? 0.00 : dt4.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");


        }






        protected void gvMonCollect_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink HygvResDesc = (HyperLink)e.Row.FindControl("HygvResDesc");
                Label lgvtoamt = (Label)e.Row.FindControl("lgvtoamt");
                Label lgvamt1 = (Label)e.Row.FindControl("lgvamt1");
                Label lgvamt2 = (Label)e.Row.FindControl("lgvamt2");
                Label lgvamt3 = (Label)e.Row.FindControl("lgvamt3");
                Label lgvamt4 = (Label)e.Row.FindControl("lgvamt4");
                Label lgvamt5 = (Label)e.Row.FindControl("lgvamt5");
                Label lgvamt6 = (Label)e.Row.FindControl("lgvamt6");
                Label lgvamt7 = (Label)e.Row.FindControl("lgvamt7");
                Label lgvamt8 = (Label)e.Row.FindControl("lgvamt8");
                Label lgvamt9 = (Label)e.Row.FindControl("lgvamt9");
                Label lgvamt10 = (Label)e.Row.FindControl("lgvamt10");
                Label lgvamt11 = (Label)e.Row.FindControl("lgvamt11");
                Label lgvamt12 = (Label)e.Row.FindControl("lgvamt12");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pactcode")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    HygvResDesc.Font.Bold = true;
                    lgvtoamt.Font.Bold = true;
                    lgvamt1.Font.Bold = true;
                    lgvamt2.Font.Bold = true;
                    lgvamt3.Font.Bold = true;
                    lgvamt4.Font.Bold = true;
                    lgvamt5.Font.Bold = true;
                    lgvamt6.Font.Bold = true;
                    lgvamt7.Font.Bold = true;
                    lgvamt8.Font.Bold = true;
                    lgvamt9.Font.Bold = true;
                    lgvamt10.Font.Bold = true;
                    lgvamt11.Font.Bold = true;
                    lgvamt12.Font.Bold = true;
                    HygvResDesc.Style.Add("text-align", "right");
                }
                if (Request.QueryString["Type"] == "MonSales")
                {
                    if (ASTUtility.Left(code, 2) == "18" || ASTUtility.Left(code, 2) == "24")
                    {
                        HygvResDesc.NavigateUrl = "LinkAccount.aspx?Type=SalesProj&pactcode=" + code + "&Date1=" + this.txtfromdate.Text.Trim() + "&Date2=" + this.txttodate.Text.Trim();
                    }
                }
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt1 = (DataTable)ViewState["tblcollvscl"];
            if (dt1.Rows.Count == 0)
                return;
            ReportDocument rptstk = new RealERPRPT.R_50_CMIS.RptConMonWiseSales();
            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["companyname"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtHeader = rptstk.ReportDefinition.ReportObjects["txtHeader"] as TextObject;
            txtHeader.Text = "Month Wise Sales";

            TextObject txtdate = rptstk.ReportDefinition.ReportObjects["txtdate"] as TextObject;
            txtdate.Text = "( From " + this.txtfromdate.Text.Trim() + " To " + this.txttodate.Text.Trim() + " )";


            DateTime datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
            DateTime dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
            for (int i = 1; i <= 12; i++)
            {
                if (datefrm > dateto)
                    break;
                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtamt" + i.ToString()] as TextObject;
                rpttxth.Text = datefrm.ToString("MMM yy");
                datefrm = datefrm.AddMonths(1);

            }

            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptstk.SetDataSource(dt1);
            Session["Report1"] = rptstk;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                          ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.Data_Bind();
        }

    }
}