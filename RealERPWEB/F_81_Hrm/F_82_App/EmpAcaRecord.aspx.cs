using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpAcaRecord : System.Web.UI.Page

    {
        ProcessAccess da = new ProcessAccess();
        //static string tempddl1 = "", tempddl2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
            if (this.ddlEmpAcarecord.Items.Count == 0)
                this.Load_CodeBooList();

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void Load_CodeBooList()
        {

            try
            {

                //string comcod = this.GetCompCode();
                DataSet dsone = this.da.GetTransInfo("", "dbo_hrm.SP_ENTRY_CODEBOOK", "GETEMPRECORDCODE", "", "", "", "", "", "", "", "", "");
                this.ddlEmpAcarecord.DataTextField = "subdesc";
                this.ddlEmpAcarecord.DataValueField = "subcode";
                this.ddlEmpAcarecord.DataSource = dsone.Tables[0];
                this.ddlEmpAcarecord.DataBind();
            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error:" + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
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

            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            string comcod = this.GetCompCode();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim().Replace("-", "");
            if (gcode2.Length != 6)
                return;

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();

            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string maincode = (tgcod.Substring(0, 5));
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INOUPEACADEMICRECORD", maincode, tgcod,
                           gdesc, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
            this.grvacc.EditIndex = -1;
            this.ShowInformation();
            this.grvacc_DataBind();
        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                for (int i = 1; i <= TotalPage; i++)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                if (TotalPage > 1)
                    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.grvacc.PageIndex = ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex;
                this.grvacc_DataBind();
            }
            catch (Exception ex)
            {
            }
        }


        protected void lnkok_Click(object sender, EventArgs e)
        {



            if (this.lnkok.Text == "Ok")
            {
                string comcod = this.GetCompCode();
                this.lnkok.Text = "New";
                this.LblBookName.Text = "Code Book:";
                this.lblddlOEmpAcarecord.Text = this.ddlEmpAcarecord.SelectedItem.Text.Trim();
                this.ddlEmpAcarecord.Visible = false;
                this.lblddlOEmpAcarecord.Visible = true;
                this.ShowInformation();
            }

            else
            {

                this.lnkok.Text = "Ok";
                ((Label)this.Master.FindControl("lblmsg")).Text = "";
                this.LblBookName.Text = "Select Code Book:";
                this.lblddlOEmpAcarecord.Visible = false;
                this.ddlEmpAcarecord.Visible = true;
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();

            }


        }

        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlEmpAcarecord.SelectedValue.ToString()).Substring(0, 5);
            string txtSearchItem = "%" + this.txtAcademicSrc.Text.Trim() + "%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETEMPRECORDDETAILS", tempddl1, txtSearchItem, "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }
        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
    }
}