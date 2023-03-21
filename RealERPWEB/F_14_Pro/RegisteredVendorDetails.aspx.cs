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
            this.TxtTermrsConditions.Text= ds.Tables[0].Rows[0]["termscondition"].ToString();
            this.lblExperienced.Text = ds.Tables[0].Rows[0]["experience"].ToString();

            string status = ds.Tables[0].Rows[0]["varify"].ToString();
            if (status != "True")
            {
                this.lbtngvRVLvarify.Visible = true;
                this.lbtnExistingEnlist.Visible = true;
            }
            else
            {
                this.lblEnlisted.Visible = true;

            }

            DataSet ds1 = mktData.GetTransInfo(comcod, "SP_MGT_SCM_PORTAL", "GET_PRE_ASSESMENT_CODE", id);
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[0];
            ViewState["tblAss"] = dt;
            this.gvAssessment.DataSource = dt;
            this.gvAssessment.DataBind();

            ((Label)this.gvAssessment.FooterRow.FindControl("LblFBase")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(baseval)", "")) ?
           0 : dt.Compute("sum(baseval)", ""))).ToString("#,##0.00;(#,##0.00); ");
            ((Label)this.gvAssessment.FooterRow.FindControl("LblFGain")).Text ="Gain:"+ Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(ratio)", "")) ?
           0 : dt.Compute("sum(ratio)", ""))).ToString("#,##0.00;(#,##0.00); ");
        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblAss"];
            int TblRowIndex;
            for (int i = 0; i < this.gvAssessment.Rows.Count; i++)
            {

                string txtdesc = ((Label)this.gvAssessment.Rows[i].FindControl("txtDescription")).Text.Trim();
                string exc = (((CheckBox)gvAssessment.Rows[i].FindControl("lblexec")).Checked) ? "True" : "False";
                string good = (((CheckBox)gvAssessment.Rows[i].FindControl("lblgood")).Checked) ? "True" : "False";
                string avg = (((CheckBox)gvAssessment.Rows[i].FindControl("lblavrg")).Checked) ? "True" : "False";
                string poor = (((CheckBox)gvAssessment.Rows[i].FindControl("lblpoor")).Checked) ? "True" : "False";
                string nill = (((CheckBox)gvAssessment.Rows[i].FindControl("lblnill")).Checked) ? "True" : "False";


                TblRowIndex = (gvAssessment.PageIndex) * gvAssessment.PageSize + i;
               
                dt.Rows[TblRowIndex]["assdesc"] = txtdesc;
                dt.Rows[TblRowIndex]["exc"] = exc;
                dt.Rows[TblRowIndex]["good"] = (exc == "True") ? "False" : good;
                dt.Rows[TblRowIndex]["avrg"] = (exc == "True" || good == "True") ? "False" : avg;
                dt.Rows[TblRowIndex]["poor"] = (exc == "True" || good == "True" || avg == "True") ? "False" : poor;
                dt.Rows[TblRowIndex]["nill"] = (exc == "True" || good == "True" || avg == "True" || poor == "True") ? "False" : nill;



            }
            ViewState["tblAss"] = dt;
        }
        public string GetAssNO()
        {
            string comcod = this.GetComeCode();
       
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Vendorid = ASTUtility.Right("000000000000"+this.Request.QueryString["id"].ToString(),12);

            DataSet ds3 = mktData.GetTransInfo(comcod, "SP_MGT_SCM_PORTAL", "GET_ASSESSMENT_NO", date, Vendorid, "", "", "", "", "", "", "");

            if (ds3 == null)
            {
                return "00000000000000";

            }
            else
            {
                return ds3.Tables[0].Rows[0]["maxsupassno"].ToString();
            }
              
           

        }
        protected void lbtnUpPerAppraisal_OnClick(object sender, EventArgs e)
        {
            try
            {

                string comcod = this.GetComeCode();
       
                
                this.SaveValue();
                DataTable dt = (DataTable)ViewState["tblAss"];
                string empid = ASTUtility.Right("000000000000"+this.Request.QueryString["id"].ToString(),12);
                string matcod ="000000000000";
                string curdate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string assno = GetAssNO();
                string txtref = this.lblcompanyname.Text.Trim();


                bool result = false;
                result = mktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESUPASS", "ASSINFB", assno, empid, matcod, curdate, txtref, "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    return;

                }


                foreach (DataRow dr1 in dt.Rows)
                {

                    string gcod = dr1["asscode"].ToString();
                    string desc = dr1["assdesc"].ToString();
                    string exc = dr1["exc"].ToString();
                    string good = dr1["good"].ToString();
                    string avg = dr1["avrg"].ToString();
                    string poor = dr1["poor"].ToString();
                    string nill = dr1["nill"].ToString();


                    result = mktData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTORUPDATESUPASS", "ASSINFA", assno, gcod, desc, exc, good,
                        avg, poor, nill, "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = mktData.ErrorObject["Msg"].ToString();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }


                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

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
            bool result = mktData.UpdateTransInfo(comcod, "SP_MGT_SCM_PORTAL", "ENLIST_WITH_EXISTING_SUPPLIER", id, suppcod);
            if (result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Successfully updated');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Failed to update');", true);
        }

        protected void LbtnRecalculate_Click(object sender, EventArgs e)
        {
            double achivedmark = 0;
            for (int i = 0; i < this.gvAssessment.Rows.Count; i++)
            {
                double baseval = Convert.ToDouble("0" + ((Label)this.gvAssessment.Rows[i].FindControl("lblbase")).Text.Trim());
                double gainval = 0;
               
                if (((CheckBox)this.gvAssessment.Rows[i].FindControl("lblexec")).Checked == true){
                    gainval = (baseval) *1;
                  }
                else if (((CheckBox)this.gvAssessment.Rows[i].FindControl("lblgood")).Checked == true)
                {
                    gainval = (baseval) * 0.75;
                }
                else if (((CheckBox)this.gvAssessment.Rows[i].FindControl("lblavrg")).Checked == true)
                {
                    gainval = (baseval) * 0.50;
                }
                else if (((CheckBox)this.gvAssessment.Rows[i].FindControl("lblpoor")).Checked == true)
                {
                    gainval = (baseval) * 0.25;
                }
                else if(((CheckBox)this.gvAssessment.Rows[i].FindControl("lblnill")).Checked == true)
                {
                    gainval = (baseval) * 0;
                }
                else
                {
                    gainval = 0;
                }

                achivedmark += gainval;
            }

              ((Label)this.gvAssessment.FooterRow.FindControl("LblFGain")).Text ="Gain: "+ achivedmark.ToString("#,##0.00;(#,##0.00); ");
        }
    }
}