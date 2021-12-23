using RealERPLIB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_34_Mgt
{
    public partial class CreateTagSetup : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "TAG Entry Form";

                this.GetTaginfo();
                string id = this.Request.QueryString["Type"] ?? "";
                if (id.Length > 0)
                {
                    this.GetPreviousType();
                }
            }
        }

        private void GetPreviousType()
        {
            string id = this.Request.QueryString["Type"] ?? "";
            DataSet ds1 = AccData.GetTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "GETTAGINFO", id);
            if (ds1 == null)
            {
                return;
            }
          
            this.TxtTag.Text = ds1.Tables[0].Rows[0]["tagname"].ToString();
        
        }

        protected void lnkSave_Click1(object sender, EventArgs e)
        {
            string tagname = this.TxtTag.Text.ToString();
            string pid = this.Request.QueryString["Type"] ?? "";
            string id = (pid.Length > 0) ? pid.ToString() : "";

            bool result = AccData.UpdateTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "INSERTUPDATETAGINFO", id, tagname);
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                this.TxtTag.Text = "";
             
                this.GetTaginfo();
                string tagid = this.Request.QueryString["Type"] ?? "";
                if (tagid.Length > 0)
                {
                    Response.Redirect("~/F_34_Mgt/CreateTagSetup?Type=");
                }
            }

        }

        private void GetTaginfo()
        {

            DataSet ds1 = AccData.GetTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "GETTAGINFO");
            if (ds1 == null)
            {
                this.GvTagSetup.DataSource = null;
                this.GvTagSetup.DataBind();
                return;
            }
            ViewState["tbltaginfo"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tbltaginfo"];

            this.GvTagSetup.DataSource = dt;
            this.GvTagSetup.DataBind();
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.GvTagSetup.Rows[index].FindControl("lblTagid")).Text.ToString();
         
            Response.Redirect("~/F_34_Mgt/CreateTagSetup?Type=" + id);
        }

        protected void GvTagSetup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GvTagSetup.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}