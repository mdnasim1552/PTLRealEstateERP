using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace RealERPWEB.F_34_Mgt
{
    public partial class ViewAllTemplate : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetAllTempData();
            }
        }

        private void GetAllTempData()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            DataSet ds1 = AccData.GetTransInfo("", "SP_ADMIN_SMS_INFO", "GETSMSCONTENTEMPLATE", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;
            }
            ViewState["AllTempData"] = ds1.Tables[0];
            this.Data_bind();
        }

        private void Data_bind()
        {
            DataTable dt = (DataTable)ViewState["AllTempData"];

            this.GvTemplate.DataSource = dt;
            this.GvTemplate.DataBind();
        }

        protected void lnkbtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/F_34_Mgt/ContentSetupEntry?Type=");
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.GvTemplate.Rows[index].FindControl("lblSmsId")).Text.ToString();
            Response.Redirect("~/F_34_Mgt/ContentSetupEntry?Type=" + id);
        }

        protected void GvTemplate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTemplate.PageIndex = e.NewPageIndex;
            this.Data_bind();
        }
    }
}