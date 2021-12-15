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
    public partial class SMSTypeEntry : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "SMS For Type Entry";
                this.GetSMSfor();
                string id = this.Request.QueryString["Type"]??"";
                if (id.Length > 0)
                {
                    this.GetPreviousType();
                }
            }
        }

        private void GetPreviousType()
        {
            string id = this.Request.QueryString["Type"] ?? "";
            DataSet ds1 = AccData.GetTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "GETSMSFORINFO", id);
            if (ds1 == null)
            {
                return;
            }
            this.Txtsmsfor.Text = ds1.Tables[0].Rows[0]["typedesc"].ToString();
            this.txtCode.Text = ds1.Tables[0].Rows[0]["code"].ToString();
            this.txtCode.Enabled = false;

        }

        private void GetSMSfor()
        {

            DataSet ds1 = AccData.GetTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "GETSMSFORINFO");
            if (ds1 == null)
            {
                this.GvSpecification.DataSource = null;
                this.GvSpecification.DataBind();
                return;
            }
            ViewState["tblsmsfor"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblsmsfor"];

            this.GvSpecification.DataSource = dt;
            this.GvSpecification.DataBind();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

            string smsfor = this.Txtsmsfor.Text.ToString();
            string code = this.txtCode.Text.ToString();
            string pid= this.Request.QueryString["Type"] ?? "";
            string id = (pid.Length > 0) ? pid.ToString() : "";
            bool result = AccData.UpdateTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "INSERTUPDATESMSFORCODE", id, code, smsfor );

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
                this.txtCode.Text = "";
                this.Txtsmsfor.Text = "";
                this.GetSMSfor();
                string Code = this.Request.QueryString["Code"] ?? "";
                if (code.Length > 0)
                {
                    Response.Redirect("~/F_34_Mgt/SMSTypeEntry?Type=");
                }

            }

        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.GvSpecification.Rows[index].FindControl("lblid")).Text.ToString();
            string code = ((Label)this.GvSpecification.Rows[index].FindControl("lblcode")).Text.ToString();
            Response.Redirect("~/F_34_Mgt/SMSTypeEntry?Type=" + id+"&Code="+code);

        }


        [System.Web.Services.WebMethod]
        public static string Checkcode(string code)
        {
            string retval = "";
            ProcessAccess _DataEntry = new ProcessAccess();

            DataSet ds = _DataEntry.GetTransInfo("", "SP_TAS_ENTRY_CODEBOOK", "GETCODECHECK", code);
            if (ds == null)
            {
                return "true";
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                retval = "true";
            }
            else
            {
                retval = "false";
            }

            return retval;
        }        
        protected void GvSpecification_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GvSpecification.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
    }
}