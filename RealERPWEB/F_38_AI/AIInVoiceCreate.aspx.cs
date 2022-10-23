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
    public partial class AIInVoiceCreate : System.Web.UI.Page
    {
        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "AI Invoice Create";
                string currentdate = DateTime.Now.ToString("dd-MMM-yyyy");
                this.txtdate.Text = currentdate;
                this.GetCustomerList();
                this.GetProjectList();
            }
        }
        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetCustomerList()
        {
            try
            {
                string comcod = this.GetComdCode();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_ENTRY_CODEBOOK_AI", "GETCUSTOMERLIST", "", "", "", "", "", "");
                if (ds == null)
                    return;
                DataTable dt = ds.Tables[0];
                Session["tblCustlist"] = ds.Tables[0];
                DataView dv2 = dt.DefaultView;
                this.ddlsuplier.DataTextField = "name";
                this.ddlsuplier.DataValueField = "infcod";
                this.ddlsuplier.DataSource = dv2.ToTable();
                this.ddlsuplier.DataBind();

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }


        }

        private void GetProjectList()
        {
            try
            {
                string comcod = this.GetComdCode();
                string client = this.ddlsuplier.SelectedValue.ToString();
                DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "PROJECT_LIST", client, "", "", "", "", "");
                if (ds == null)
                    return;
                this.ddlprojname.DataTextField = "infdesc";
                this.ddlprojname.DataValueField = "infcod";
                this.ddlprojname.DataSource = ds.Tables[0];
                this.ddlprojname.DataBind();               

            }
            catch(Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void ddlsuplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectList();
        }
    }
}