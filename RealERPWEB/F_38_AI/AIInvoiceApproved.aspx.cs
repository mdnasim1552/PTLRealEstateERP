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

                this.txtfrmdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
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

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }
    }
}