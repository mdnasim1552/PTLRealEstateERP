using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_14_Pro
{
    public partial class RegisteredVendorList : System.Web.UI.Page
    {
        ProcessAccess mktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                string Date01 = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfrmdate.Text = "01-" + ASTUtility.Right(Date01, 8);
                this.txttodate.Text = Convert.ToDateTime(this.txtfrmdate.Text.Trim()).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetRegistredVendorList();
            }
        }

        private void GetRegistredVendorList()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds = mktData.GetTransInfo(comcod, "SP_MGT_REPORT_SCM_PORTAL", "GET_REGISTERED_VENDOR_LIST");
            this.gvRegVendorList.DataSource = ds.Tables[0];
            this.gvRegVendorList.DataBind();
        }

        protected void lbtngvRVLvarify_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string varify = linkButton.Text.ToLower().Trim();
            string id = linkButton.CommandArgument;
            varify = (varify == "false") ? "true" : "false";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            bool result = mktData.UpdateTransInfo(comcod, "SP_MGT_REPORT_SCM_PORTAL", "UPDATE_VENDOR_VARIFY_STATUS", id, varify);
            if (result)
            {
                this.GetRegistredVendorList();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully updated');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to update');", true);
        }

        protected void gvRegVendorList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton btn = (LinkButton)(e.Row.FindControl("lbtngvRVLvarify"));
                btn.CssClass = btn.Text == "True" ? "btn btn-sm btn-success" : "btn btn-sm btn-warning";
            }
        }
    }
}

