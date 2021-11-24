using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_99_Allinterface
{
    public partial class CRMDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
            //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
            //    Response.Redirect("~/AcceessError.aspx");

            ((Label)this.Master.FindControl("lblTitle")).Text = "CRM Dashboard";

            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtfodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }

        protected void ddlEmpid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}