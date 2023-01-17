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
namespace RealERPWEB.F_23_CR
{
    public partial class DishonourCheque : System.Web.UI.Page
    {
        ProcessAccess CustData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.txtfromdate.Text = System.DateTime.Today.AddDays(-30).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = Convert.ToDateTime("01" + this.txtfromdate.Text.Trim().Substring(2)).ToString("dd-MMM-yyyy");
                this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetProjectName();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "DISHONOUR CHEQUE INFORMATION";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;


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

        private void GetProjectName()
        {

            string comcod = this.GetCompCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTNAMEDIS", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "pactdesc";
            this.ddlProjectName.DataValueField = "pactcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.Substring(0, 12).ToString();
            string fromdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string cheno = this.txtSrcChequeNo.Text.Trim() + "%";
            DataSet ds1 = CustData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "TRANSACTIONSTATEMENT", fromdate, todate, pactcode, cheno, "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvDhonuurcheque.DataSource = null;
                this.gvDhonuurcheque.DataBind();
                return;
            }

            Session["DailyTrns"] = HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["DailyTrns"];
            int TblRowIndex;
            for (int i = 0; i < this.gvDhonuurcheque.Rows.Count; i++)
            {
                string Dishdate = (((TextBox)this.gvDhonuurcheque.Rows[i].FindControl("txtgvdhonuurDat")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvDhonuurcheque.Rows[i].FindControl("txtgvdhonuurDat")).Text.Trim();
                TblRowIndex = (gvDhonuurcheque.PageIndex) * gvDhonuurcheque.PageSize + i;
                dt.Rows[TblRowIndex]["dishdat"] = Dishdate;

            }
            Session["DailyTrns"] = dt;

        }


        private void Data_Bind()
        {
            this.gvDhonuurcheque.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDhonuurcheque.DataSource = (DataTable)Session["DailyTrns"];
            this.gvDhonuurcheque.DataBind();
            this.FooterCalculation();
        }




        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string pactcode = dt1.Rows[0]["pactcode"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["pactcode"].ToString() == pactcode)
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                    dt1.Rows[j]["pactdesc"] = "";
                }

                else
                {
                    pactcode = dt1.Rows[j]["pactcode"].ToString();
                }

            }

            return dt1;
        }
        private void FooterCalculation()
        {
            DataTable dt1 = (DataTable)Session["DailyTrns"];
            if (dt1.Rows.Count == 0)
                return;

            ((Label)this.gvDhonuurcheque.FooterRow.FindControl("lgvCheAmt")).Text = Convert.ToDouble((Convert.IsDBNull(dt1.Compute("sum(chqamt)", "")) ?
                                0 : dt1.Compute("sum(chqamt)", ""))).ToString("#,##0;(#,##0); ");

        }

        protected void gvDhonuurcheque_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            gvDhonuurcheque.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.SaveValue();
            DataTable tbl2 = (DataTable)Session["DailyTrns"];
            string comcod = this.GetCompCode();
            bool result = false;
            for (int i = 0; i < tbl2.Rows.Count; i++)
            {
                string pactcode = tbl2.Rows[i]["pactcode"].ToString();
                string usircode = tbl2.Rows[i]["usircode"].ToString();
                string mrno = tbl2.Rows[i]["mrno"].ToString();
                string chqno = tbl2.Rows[i]["chqno"].ToString().Trim();
                string Disdate = tbl2.Rows[i]["dishdat"].ToString().Trim();


                result = CustData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEDISHDATE", pactcode, usircode, mrno, chqno, Disdate, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = CustData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        }
    }
}