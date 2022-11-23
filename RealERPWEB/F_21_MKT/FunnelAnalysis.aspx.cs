using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_21_MKT
{
    public partial class FunnelAnalysis : System.Web.UI.Page
    {
        ProcessAccess dbAccess = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "Funnel Analysis";
                this.rbtType.SelectedValue = "0";
                rbtType_SelectedIndexChanged(null, null);
            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }

        protected void rbtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.rbtType.SelectedValue.ToString();
            if (type == "0")
            {
                GetMonthWiseReport();
            }
            else if (type == "1")
            {

            }
            else
            {

            }

        }


        private void GetMonthWiseReport()
        {
            try
            {
                string comcod = this.GetComeCode();
                DataSet ds5 = dbAccess.GetTransInfo(comcod, "SP_REPORT_CRM_MODULE", "GETFUNNELANALYSISREPORT", "", "", "", "", "", "", "", "", "");
                if (ds5 == null)
                {
                    this.gvFunAnaMonths.DataSource = null;
                    this.gvFunAnaMonths.DataBind();
                    return;
                }
                ViewState["tblfunnleMonths"] = ds5.Tables[0];
                this.Data_Bind();
            }
            catch (Exception ex)
            {
                 
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);
            }
        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblfunnleMonths"];
            this.gvFunAnaMonths.DataSource = dt;
            this.gvFunAnaMonths.DataBind();
        }
    }
}