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
namespace RealERPWEB.F_17_Acc
{
    public partial class AccBankLimit : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "BANK LIMIT INFORMATION";
                //this.Master.Page.Title = "BANK LIMIT INFORMATION";


                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                // this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.ShowData();
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
        private void ShowData()
        {

            string comcod = this.GetCompCode();
            string banksearch = "%" + this.txtSrchBankName.Text + "%";
            DataSet ds1 = AccData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETBANKLOANLIMIT", banksearch, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvBankLimit.DataSource = null;
                this.gvBankLimit.DataBind();
                return;
            }
            ViewState["tblbank"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {

            DataTable tbl1 = (DataTable)ViewState["tblbank"];
            this.gvBankLimit.DataSource = tbl1;
            this.gvBankLimit.DataBind();
            this.FooterCalculation(tbl1);
        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvBankLimit.FooterRow.FindControl("lgvFamt")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(bankamt)", "")) ? 0.00
            : dt.Compute("Sum(bankamt)", ""))).ToString("#,##0;(#,##0);  ");
        }

        private void SaveValue()
        {
            DataTable tbl1 = (DataTable)ViewState["tblbank"];
            for (int i = 0; i < this.gvBankLimit.Rows.Count; i++)
            {
                tbl1.Rows[i]["bankamt"] = Convert.ToDouble("0" + ((TextBox)this.gvBankLimit.Rows[i].FindControl("txtgvbankamt")).Text.Trim()).ToString();

            }

            ViewState["tblbank"] = tbl1;

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbTotal_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();

        }
        protected void lbtnFinalUpdate_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            try
            {

                string comcod = this.GetCompCode();
                this.SaveValue();
                DataTable dt1 = (DataTable)ViewState["tblbank"];

                bool result = true;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {


                    string bankcode = dt1.Rows[i]["bankcode"].ToString();
                    double bankam = Convert.ToDouble(dt1.Rows[i]["bankamt"].ToString());
                    if (bankam > 0)

                        result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "INORUPBANKLIMIT", bankcode, bankam.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "");


                    if (result == false)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Errp:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }
        protected void gvBankLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string bankcode = ((Label)this.gvBankLimit.Rows[e.RowIndex].FindControl("lblgvBankCod")).Text.Trim();

            bool result = AccData.UpdateTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "DELETEBANKLIMIT", bankcode, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
                return;

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.ShowData();
        }
    }
}