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
    public partial class ContentSetupEntry : System.Web.UI.Page
    {
        ProcessAccess AccData = new ProcessAccess();
        ProcessAccess _SMRecord = new ProcessAccess("ASTREALERPMSG");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "CREATE SMS TEMPALTE";

                string smsid = this.Request.QueryString["id"] ?? "";

                if (smsid.Length > 0)
                {
                    this.GetPrevSMSContent();
                }
                this.GetSMSFORNOTIFICATION();
                this.getSMSTags();
            }
           
        }
        private void getSMSTags()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            
            DataSet ds1 = AccData.GetTransInfo("", "SP_ADMIN_SMS_INFO", "GETSMSTAGSLIST", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            rptsmsTags.DataSource = ds1.Tables[0];
            rptsmsTags.DataBind();

            rptsmsEnTags.DataSource = ds1.Tables[0];
            rptsmsEnTags.DataBind();
        }
      
        private void GetPrevSMSContent()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string smsid = this.Request.QueryString["id"]??"";
            DataSet ds1 = _SMRecord.GetTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "GETSMSCONTENTEMPLATE", smsid, "ind", "", "", "", "");

            DataTable dt = ds1.Tables[0];
            //string smsfor = ds1.Tables[0].Rows[0]["smsfor"].ToString();
            //ViewState["smsfor"] = smsfor;
            //this.txtilebn.Text = ds1.Tables[0].Rows[0]["nameban"].ToString();
            this.TxtTitle.Text = ds1.Tables[0].Rows[0]["gdesc"].ToString();
            //this.txtdescBn.Text = ds1.Tables[0].Rows[0]["tempban"].ToString();
            this.txtdesceng.Text = ds1.Tables[0].Rows[0]["smscont"].ToString();
        }

        private void GetSMSFORNOTIFICATION()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();

            DataSet dsconcom = AccData.GetTransInfo(comcod, "SP_ADMIN_SMS_INFO", "GETSMSFORSECTION", "", "", "", "");
            if (dsconcom == null)
            {
                return;
            }
            string smsfor = dsconcom.Tables[0].Rows[0]["code"].ToString();
            string smsor = "";
            string smsid = this.Request.QueryString["Type"] ?? "";
            if (smsid.Length > 0)
            {
                smsor = (string)ViewState["Type"];
            }
            else
            {
                smsor = smsfor;
            }
            this.ddlSMSfor.DataTextField = "typedesc";
            this.ddlSMSfor.DataValueField = "code";
            this.ddlSMSfor.DataSource = dsconcom.Tables[0];
            this.ddlSMSfor.DataBind();
            this.ddlSMSfor.SelectedValue = smsor;
        }
        protected void rptsmsTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rptsmsEnTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string titlebn = this.txtilebn.Text.Trim().ToString();
            string titleen = this.TxtTitle.Text.Trim().ToString();
            string txtdescbn = this.txtdescBn.Text.Trim().ToString();
            string txtdescen = this.txtdesceng.Text.Trim().ToString();
            string smsformat = this.ddlFormat.SelectedValue.Trim().ToString();
            string smsfor = this.ddlSMSfor.SelectedValue.Trim().ToString();

            string smsid = this.Request.QueryString["id"] ?? "";
            string dfor = this.Request.QueryString["dfor"] ?? "";

            string tgcod = (smsid.Length > 0) ? smsid: "";

           bool result = _SMRecord.UpdateTransInfo(comcod, "SP_ENTRY_SMS_MAIL_INFO", "INSERTUPSMINF", tgcod,
                       "", txtdescen, txtdescen, "", "", dfor, "", "", "", "", "", "", "", "");

          // bool result = AccData.UpdateXmlTransInfo("", "SP_ADMIN_SMS_INFO", "INSERTSMSINFORMATION", null, null, null, tgcod, titlebn, titleen, txtdescbn, txtdescen, smsformat, smsfor);

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


              // Response.Redirect("~/F_34_Mgt/CreateTagSetup?Type=");              
            }

            // Response.Redirect("~/Admin/AllViewTemplate");
        }

        protected void lnkTag_Click(object sender, EventArgs e)
        {
            string tempid = this.Request.QueryString["id"].ToString();
            Response.Redirect("~/F_34_Mgt/CreateTagSetup?Type="+"&tid="+ tempid);
        }
    }
}