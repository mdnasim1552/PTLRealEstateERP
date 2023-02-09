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
namespace RealERPWEB.F_22_Sal
{
    public partial class SaleYearlySalAndColl : System.Web.UI.Page
    {
        ProcessAccess SaleData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                string type = this.Request.QueryString["Type"].ToString().Trim();

                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-11).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + (this.txtfromdate.Text.Trim()).Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.SelectView();
                this.lblHeader.Text = (type == "YearlySalesTar") ? "Yearly Sales Target" : (type == "YearlyCollTar") ? "Yearly Sales Target"

                    : "";
            }
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
                case "YearlySalesTar":
                case "YearlyCollTar":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;

            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";
                this.txtfromdate.Enabled = false;
                this.txttodate.Enabled = false;
                this.ShowView();
                return;


            }

            this.lbtnOk.Text = "Ok";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.txtfromdate.Enabled = true;
            this.txttodate.Enabled = true;
            this.gvSalAndColl.DataSource = null;
            this.gvSalAndColl.DataBind();




        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "YearlySalesTar":
                case "YearlyCollTar":
                    this.ShowSalesACollTar();
                    break;





            }

        }

        private void ShowSalesACollTar()
        {

            ViewState.Remove("tblcollvscl");
            string comcod = this.GetCompCode();

            int mon = ASTUtility.Datediff(Convert.ToDateTime(this.txttodate.Text.Trim()), Convert.ToDateTime(this.txtfromdate.Text.Trim()));
            if (mon > 12)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Month Less Than Equal Twelve');", true);
                return;


            }



            string txtdatefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim()).ToString("dd-MMM-yyyy");
            string txtdateto = Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("dd-MMM-yyyy");
            string sale = (this.Request.QueryString["Type"] == "YearlySalesTar") ? "sale" : "";
            DataSet ds1 = SaleData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT03", "GETYSALEACOLLTAR", txtdatefrm, txtdateto, sale, "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvSalAndColl.DataSource = null;
                this.gvSalAndColl.DataBind();
                return;
            }


            ViewState["tblysalacoll"] = ds1.Tables[0];
            this.Data_Bind();

        }







        private void Data_Bind()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            DateTime datefrm, dateto;
            switch (type)
            {
                case "YearlySalesTar":
                case "YearlyCollTar":
                    datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                    dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                    for (int i = 3; i < 15; i++)
                    {
                        if (datefrm > dateto)
                            break;
                        this.gvSalAndColl.Columns[i].HeaderText = datefrm.ToString("MMM yy");
                        datefrm = datefrm.AddMonths(1);

                    }

                    this.gvSalAndColl.DataSource = (DataTable)ViewState["tblysalacoll"];
                    this.gvSalAndColl.DataBind();
                    this.FooterCalculation();
                    break;
            }

        }


        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)ViewState["tblysalacoll"];
            if (dt1.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "YearlySalesTar":
                case "YearlyCollTar":
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFtoamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(toamt)", "")) ? 0.00 : dt1.Compute("sum(toamt)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ? 0.00 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ? 0.00 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ? 0.00 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ? 0.00 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt5)", "")) ? 0.00 : dt1.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt6")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt6)", "")) ? 0.00 : dt1.Compute("sum(amt6)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt7")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt7)", "")) ? 0.00 : dt1.Compute("sum(amt7)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt8")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt8)", "")) ? 0.00 : dt1.Compute("sum(amt8)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt9")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt9)", "")) ? 0.00 : dt1.Compute("sum(amt9)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt10")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt10)", "")) ? 0.00 : dt1.Compute("sum(amt10)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt11")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt11)", "")) ? 0.00 : dt1.Compute("sum(amt11)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvSalAndColl.FooterRow.FindControl("lgvFamt12")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt12)", "")) ? 0.00 : dt1.Compute("sum(amt12)", ""))).ToString("#,##0;(#,##0); ");
                    break;


            }



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblysalacoll"];
            for (int i = 0; i < this.gvSalAndColl.Rows.Count; i++)
            {
                dt.Rows[i]["amt1"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt1")).Text.Trim()).ToString();
                dt.Rows[i]["amt2"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt2")).Text.Trim()).ToString();
                dt.Rows[i]["amt3"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt3")).Text.Trim()).ToString();
                dt.Rows[i]["amt4"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt4")).Text.Trim()).ToString();

                dt.Rows[i]["amt5"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt5")).Text.Trim()).ToString();
                dt.Rows[i]["amt6"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt6")).Text.Trim()).ToString();
                dt.Rows[i]["amt7"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt7")).Text.Trim()).ToString();
                dt.Rows[i]["amt8"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt8")).Text.Trim()).ToString();

                dt.Rows[i]["amt9"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt9")).Text.Trim()).ToString();
                dt.Rows[i]["amt10"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt10")).Text.Trim()).ToString();
                dt.Rows[i]["amt11"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt11")).Text.Trim()).ToString();
                dt.Rows[i]["amt12"] = Convert.ToDouble("0" + ((TextBox)this.gvSalAndColl.Rows[i].FindControl("txtgvamt12")).Text.Trim()).ToString();

                dt.Rows[i]["toamt"] = Convert.ToDouble(dt.Rows[i]["amt1"]) + Convert.ToDouble(dt.Rows[i]["amt2"]) + Convert.ToDouble(dt.Rows[i]["amt3"])
                       + Convert.ToDouble(dt.Rows[i]["amt4"]) + Convert.ToDouble(dt.Rows[i]["amt5"]) + Convert.ToDouble(dt.Rows[i]["amt6"]) + Convert.ToDouble(dt.Rows[i]["amt7"])
                       + Convert.ToDouble(dt.Rows[i]["amt8"]) + Convert.ToDouble(dt.Rows[i]["amt9"]) + Convert.ToDouble(dt.Rows[i]["amt10"]) + Convert.ToDouble(dt.Rows[i]["amt11"])
                       + Convert.ToDouble(dt.Rows[i]["amt12"]);
            }

            ViewState["tblysalacoll"] = dt;
            this.Data_Bind();


        }
        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblysalacoll"];
                //DataRow[] dr = new DataRow();
                bool result = true;
                DateTime datefrm, dateto;
                datefrm = Convert.ToDateTime(this.txtfromdate.Text.Trim());
                dateto = Convert.ToDateTime(this.txttodate.Text.Trim());
                string CallType = (this.Request.QueryString["Type"] == "YearlySalesTar") ? "INSORUPYSALE" : "INSORUPYCOLL";


                for (int i = 0; i < 12; i++)
                {
                    if (datefrm > dateto)
                        break;

                    string yearmon = datefrm.ToString("yyyyMM");
                    foreach (DataRow datarow in dt.Rows)
                    {


                        string deptcode = datarow["deptcode"].ToString();
                        //for (int j = 1; j <= 12; j++)
                        //{

                        string amt = datarow["amt" + (i + 1).ToString()].ToString();
                        result = SaleData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT03", CallType, yearmon, deptcode, amt, "", "", "", "", "", "", "", "", "", "", "", "");


                        if (result == false)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                            return;
                        }
                        else
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        }

                        //   }

                    }
                    datefrm = datefrm.AddMonths(1);

                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
            }
        }
    }
}
