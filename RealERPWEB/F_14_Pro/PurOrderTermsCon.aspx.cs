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
    public partial class PurOrderTermsCon : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = dr1.Length == 0 ? false : (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "Puchase Order (Terms & Condition)";
            }
        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }



        protected void lnkok_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.lnkok.Text == "Ok")
                {

                    this.lnkok.Text = "New";
                    this.ShowInformation();
                }
                else
                {

                    this.lnkok.Text = "Ok";

                    this.grvacc.DataSource = null;
                    this.grvacc.DataBind();

                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Information not found!!!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }
        private void ShowInformation()
        {
            string comcod = this.GetComeCode();
            string typecod = this.ddltypecod.SelectedValue;
            DataSet ds1 = da.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "GETTRMSCONDITION", typecod,
                                  "", "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }

        public void grvacc_DataBind()
        {
            try
            {
                DataTable tbl1 = (DataTable)Session["storedata"];
                // this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();

            }
            catch (Exception ex)
            {
            }
        }

        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();
        }


        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {

            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();




        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {

                string comcod = this.GetComeCode();
                string typcod = this.ddltypecod.SelectedValue;
                string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
                string tlblgvrmrk = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("tlblgvrmrk")).Text.Trim();
                string txttrmsub = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txttrmsub")).Text.Trim();
                string trmid = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();


                DataTable tbl1 = (DataTable)Session["storedata"];//check whether it is needed or not
                bool result;
                if (trmid.Length == 3)
                {
                    result = this.da.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_01", "UPDATETRMCON", typcod, trmid, txttrmsub, Desc, tlblgvrmrk, "", "", "", "", "", "", "", "", "", "");
                    this.ShowInformation();
                    if (result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                    }
                    else
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Parent Code Not Found!!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    }
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Term Id Must be 3 digit!!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }


                this.grvacc.EditIndex = -1;
                this.grvacc_DataBind();

            }


            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
        }



    }
}
