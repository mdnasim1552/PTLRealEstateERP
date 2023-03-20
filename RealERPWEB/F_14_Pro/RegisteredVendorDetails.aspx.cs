using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_14_Pro
{
    public partial class RegisteredVendorDetails : System.Web.UI.Page
    {
        ProcessAccess mktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblTitle")).Text = "Vendor Profile";
                this.Master.Page.Title = "Vendor Profile";
                this.GetVendor();
            }
        }

        private void GetVendor()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string id = Request.QueryString["id"];
            DataSet ds = mktData.GetTransInfo(comcod, "SP_MGT_SCM_PORTAL", "GET_SINGLE_VENDOR_PROFILE", id);
            this.lblcompanyname.Text = ds.Tables[0].Rows[0]["companyname"].ToString();
            this.lblLicenseno.Text = ds.Tables[0].Rows[0]["licenseno"].ToString();
            this.lblConcernName.Text = ds.Tables[0].Rows[0]["username"].ToString();
            this.lbldesignation.Text = ds.Tables[0].Rows[0]["designation"].ToString();
            this.lblmobile.Text = ds.Tables[0].Rows[0]["mobile"].ToString();
            this.lblmobile2.Text = ds.Tables[0].Rows[0]["mobilesec"].ToString();
            this.lblEmail.Text = ds.Tables[0].Rows[0]["email"].ToString();
            this.lblAddress.Text = ds.Tables[0].Rows[0]["compaddress"].ToString();
            this.lblownername.Text = ds.Tables[0].Rows[0]["ownername"].ToString();
            this.lblownernid.Text = ds.Tables[0].Rows[0]["ownernid"].ToString();
            this.lblownertin.Text = ds.Tables[0].Rows[0]["ownertin"].ToString();
            this.lblbankname.Text = ds.Tables[0].Rows[0]["bankname"].ToString();
            this.lblbranch.Text = ds.Tables[0].Rows[0]["branch"].ToString();
            this.lblaccname.Text = ds.Tables[0].Rows[0]["accname"].ToString();
            this.lblaccnumber.Text = ds.Tables[0].Rows[0]["accnumber"].ToString();
            this.lblcomoverview.Text = ds.Tables[0].Rows[0]["comoverview"].ToString();
            this.lblProName.Text = ds.Tables[0].Rows[0]["username"].ToString();
            this.lblProCompName.Text = ds.Tables[0].Rows[0]["companyname"].ToString();
            this.lblVendorId.Text = ds.Tables[0].Rows[0]["vendorid"].ToString();
            this.lbtngvRVLvarify.CommandArgument = ds.Tables[0].Rows[0]["id"].ToString();

            string status = ds.Tables[0].Rows[0]["varify"].ToString();
            if (status != "True")
            {
                this.lbtngvRVLvarify.Visible = true;
                this.lbtnExistingEnlist.Visible = true;
            }
            else
                this.lblEnlisted.Visible = true;

        }

        protected void lbtngvRVLvarify_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string id = linkButton.CommandArgument;
            string varify = "true";

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            bool result = mktData.UpdateTransInfo(comcod, "SP_MGT_REPORT_SCM_PORTAL", "UPDATE_VENDOR_VARIFY_STATUS", id, varify);
            if (result)
            {
                this.GetVendor();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully updated');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to update');", true);
        }

        protected void lbtnExistingEnlist_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = mktData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETMSRSUPLIST", "%", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlSupl2.DataTextField = "ssirdesc1";
            this.ddlSupl2.DataValueField = "ssircode";
            this.ddlSupl2.DataSource = ds1.Tables[0];
            this.ddlSupl2.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenSuppModal();", true);
        }

        protected void lbtnEnlistExisting_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string id = Request.QueryString["id"];
            string suppcod = this.ddlSupl2.SelectedValue;
            bool result = mktData.UpdateTransInfo(comcod, "SP_MGT_REPORT_SCM_PORTAL", "ENLIST_WITH_EXISTING_SUPPLIER", id, suppcod);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully updated');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to update');", true);
        }
    }
}