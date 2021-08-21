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
namespace RealERPWEB.F_34_Mgt
{

    public partial class EntryFinResult : System.Web.UI.Page
    {
        ProcessAccess SalesData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ///((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "DAILY SALES & COLLECTION STATUS";
            }
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void lbtnYearbgd_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = SalesData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SHOWYFININCOME", "", "", "", "", "", "", "", "", "");

            if (ds1 == null)
            {
                this.gvFinResult.DataSource = null;
                this.gvFinResult.DataBind();
                return;


            }
            ViewState["tblyincome"] = ds1.Tables[0];
            ViewState["tblyear"] = ds1.Tables[1];
            DataTable dt = ds1.Tables[1];
            int j = 0;
            for (int i = 2; i < 7; i++)
            {

                this.gvFinResult.Columns[i].HeaderText = dt.Rows[j]["yeardesc"].ToString();
                j++;

            }
            this.Data_Bind();
        }
        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblyincome"];
            this.gvFinResult.DataSource = tbl1;
            this.gvFinResult.DataBind();
            //this.FooterCalculation(tbl1);
        }
        //private void FooterCalculation(DataTable dt1)
        //{
        //    if (dt1.Rows.Count == 0)
        //        return;

        //    ((Label)this.gvFinResult.FooterRow.FindControl("lgvFamt1")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt1)", "")) ? 0.00 : dt1.Compute("sum(amt1)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvFinResult.FooterRow.FindControl("lgvFamt2")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt2)", "")) ? 0.00 : dt1.Compute("sum(amt2)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvFinResult.FooterRow.FindControl("lgvFamt3")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt3)", "")) ? 0.00 : dt1.Compute("sum(amt3)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvFinResult.FooterRow.FindControl("lgvFamt4")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt4)", "")) ? 0.00 : dt1.Compute("sum(amt4)", ""))).ToString("#,##0;(#,##0); ");
        //    ((Label)this.gvFinResult.FooterRow.FindControl("lgvFamt5")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(amt5)", "")) ? 0.00 : dt1.Compute("sum(amt5)", ""))).ToString("#,##0;(#,##0); ");


        //}


        private void SaveValue()
        {

            DataTable dt = (DataTable)ViewState["tblyincome"];
            for (int i = 0; i < this.gvFinResult.Rows.Count; i++)
            {


                dt.Rows[i]["amt1"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFinResult.Rows[i].FindControl("txtgvamt1")).Text.Trim()));
                dt.Rows[i]["amt2"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFinResult.Rows[i].FindControl("txtgvamt2")).Text.Trim()));
                dt.Rows[i]["amt3"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFinResult.Rows[i].FindControl("txtgvamt3")).Text.Trim()));
                dt.Rows[i]["amt4"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFinResult.Rows[i].FindControl("txtgvamt4")).Text.Trim()));
                dt.Rows[i]["amt5"] = Convert.ToDouble(ASTUtility.StrPosOrNagative(((TextBox)this.gvFinResult.Rows[i].FindControl("txtgvamt5")).Text.Trim()));

            }

            ViewState["tblyincome"] = dt;



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            ReportDocument rptstk = new RealERPRPT.R_34_Mgt.RptEntryFinalResults();



            TextObject txtCompany = rptstk.ReportDefinition.ReportObjects["txtCompany"] as TextObject;
            txtCompany.Text = comnam;

            TextObject txtTitel = rptstk.ReportDefinition.ReportObjects["txtTitel"] as TextObject;
            txtTitel.Text = "Financial Results (5 Years)";


            DataTable dt = (DataTable)ViewState["tblyear"];
            int j = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                TextObject rpttxth = rptstk.ReportDefinition.ReportObjects["txtDat" + j.ToString()] as TextObject;
                rpttxth.Text = dt.Rows[i]["yeardesc"].ToString();
                j++;

            }


            TextObject txtuserinfo = rptstk.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            rptstk.SetDataSource((DataTable)ViewState["tblyincome"]);
            Session["Report1"] = rptstk;

            //this.lbljavascript.Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        protected void lbtnTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            //if (!Convert.ToBoolean(dr1[0]["entry"]))
            //{
            //    this.lblmsg02.Text = "You have no permission";
            //    return;
            //}
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblyincome"];
                DataTable dt1 = (DataTable)ViewState["tblyear"];

                bool result = true;



                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string year1 = dt1.Rows[i]["year1"].ToString();
                    foreach (DataRow datarow in dt.Rows)
                    {

                        string gcod = datarow["gcod"].ToString();
                        string amt = datarow["amt" + (i + 1).ToString()].ToString();
                        if (ASTUtility.Right(gcod, 2) == "00" || ASTUtility.Right(gcod, 2) == "AA")
                            continue;
                        result = SalesData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSORUPFINRESINF", year1, gcod, amt, "", "", "", "", "", "", "", "", "", "", "", "");


                        if (result == false)
                        {
                            this.lblmsg02.Text = "Updated Failed";
                            return;
                        }
                        else
                        {
                            this.lblmsg02.Text = "Updated Successfully";
                        }

                        //   }

                    }


                }

            }
            catch (Exception ex)
            {
                this.lblmsg02.Text = "Errp:" + ex.Message;
            }
        }
        protected void gvFinResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HyperLink ResDesc = (HyperLink)e.Row.FindControl("HygvResDesc");
                TextBox amt1 = (TextBox)e.Row.FindControl("txtgvamt1");
                TextBox amt2 = (TextBox)e.Row.FindControl("txtgvamt2");
                TextBox amt3 = (TextBox)e.Row.FindControl("txtgvamt3");
                TextBox amt4 = (TextBox)e.Row.FindControl("txtgvamt4");
                TextBox amt5 = (TextBox)e.Row.FindControl("txtgvamt5");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "0000" && ASTUtility.Right(code, 6) == "000000")
                {

                    ResDesc.Font.Bold = true;
                    amt1.ReadOnly = true;
                    amt2.ReadOnly = true;
                    amt3.ReadOnly = true;
                    amt4.ReadOnly = true;
                    amt5.ReadOnly = true;
                    ResDesc.Style.Add("color", "red");
                }
                if (ASTUtility.Right(code, 4) == "0000" && ASTUtility.Right(code, 6) != "000000")
                {

                    ResDesc.Font.Bold = true;
                    amt1.ReadOnly = true;
                    amt2.ReadOnly = true;
                    amt3.ReadOnly = true;
                    amt4.ReadOnly = true;
                    amt5.ReadOnly = true;

                    amt1.Font.Bold = true;
                    amt2.Font.Bold = true;
                    amt3.Font.Bold = true;
                    amt4.Font.Bold = true;
                    amt5.Font.Bold = true;

                    ResDesc.Style.Add("color", "blue");
                    amt1.Style.Add("color", "blue");
                    amt2.Style.Add("color", "blue");
                    amt3.Style.Add("color", "blue");
                    amt4.Style.Add("color", "blue");
                    amt5.Style.Add("color", "blue");
                }
                if (ASTUtility.Right(code, 4) == "AAAA")
                {

                    ResDesc.Font.Bold = true;
                    amt1.ReadOnly = true;
                    amt2.ReadOnly = true;
                    amt3.ReadOnly = true;
                    amt4.ReadOnly = true;
                    amt5.ReadOnly = true;

                    amt1.Font.Bold = true;
                    amt2.Font.Bold = true;
                    amt3.Font.Bold = true;
                    amt4.Font.Bold = true;
                    amt5.Font.Bold = true;

                    ResDesc.Style.Add("text-align", "right");
                    //amt1.Style.Add("color", "blue");
                    //amt2.Style.Add("color", "blue");
                    //amt3.Style.Add("color", "blue");
                    //amt4.Style.Add("color", "blue");
                    //amt5.Style.Add("color", "blue");
                }

            }
        }
    }
}