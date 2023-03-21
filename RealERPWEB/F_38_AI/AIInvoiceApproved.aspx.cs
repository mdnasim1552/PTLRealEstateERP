using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class AIInvoiceApproved : System.Web.UI.Page
    {
        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");
               
                ((Label)this.Master.FindControl("lblTitle")).Text = "AI Invoice Aproved";

                this.txtfrmdate.Text = Request.QueryString["Date"];
                this.tblinvo.Text = Request.QueryString["Invono"];
               

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        

        protected void lnkbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                this.pnlupdate.Visible = true;
                string tblinvo = (this.tblinvo.Text)+"%";
                this.ibtnvounu_Click(null, null);

                DataSet ds2 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETAIVOUNUM", tblinvo, "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }
                ViewState["tblt01"]= ds2.Tables[0];

                this.GetData_Bound();
              




            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
        private void GetData_Bound()
        {
            DataTable dt = (DataTable)ViewState["tblt01"];
            this.gvInvoApp.DataSource = dt;
            this.gvInvoApp.DataBind();
            this.FooterCalculation(dt);

        }

        private void FooterCalculation(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;

            ((Label)this.gvInvoApp.FooterRow.FindControl("tbldrsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(dramount)", "")) ? 0.00 :
                dt.Compute("sum(dramount)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvInvoApp.FooterRow.FindControl("tblcrsum")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(cramount)", "")) ? 0.00 :
                dt.Compute("sum(cramount)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }



        protected void ibtnvounu_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;

                string comcod = this.GetCompCode();

                DataSet ds2 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETOPENINGDATE", "", "", "", "", "", "", "", "", "");
                if (ds2.Tables[0].Rows.Count == 0)
                {
                    return;

                }

                DateTime txtopndate = Convert.ToDateTime(ds2.Tables[0].Rows[0]["voudat"]);

                if (txtopndate >= Convert.ToDateTime(this.txtfrmdate.Text.Trim().Substring(0, 11)))
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Voucher Date Must  Be Greater then Opening Date";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;

                }

                string VNo3 = "JV";
                string entrydate = this.txtfrmdate.Text.Substring(0, 11).Trim();
                DataSet ds4 = AIData.GetTransInfo(comcod, "SP_ENTRY_ACCOUNTS_VOUCHER", "GETNEWVOUCHER", entrydate, VNo3, "", "", "", "", "", "", "");
                DataTable dt4 = ds4.Tables[0];
                string cvno1 = dt4.Rows[0]["couvounum"].ToString().Substring(0, 8);
                this.txtvounum1.Text = cvno1.Substring(0, 2) + cvno1.Substring(6, 2) + "-";
                this.txtvounum2.Text = dt4.Rows[0]["couvounum"].ToString().Substring(8);

            }
            catch (Exception exp)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}