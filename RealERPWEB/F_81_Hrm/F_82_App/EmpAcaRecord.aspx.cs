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

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            if (this.ddlEmpAcarecord.Items.Count == 0)
                this.Load_CodeBooList();
            //((Label)this.Master.FindControl("lblTitle")).Text = "Academic Degree Title";
            Session["listid"] = "";

            this.ddlEmpAcarecord_SelectedIndexChanged(null,null);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);
                 
            }

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
               
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);

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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message.ToString() + "');", true);

            }
        }
 

        private void ShowInformation()
        {
            string comcod = this.GetCompCode();
            string tempddl1 = (this.ddlEmpAcarecord.SelectedValue.ToString()).Substring(0, 5);
            string txtSearchItem = "%%";
            DataSet ds1 = this.da.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "GETEMPRECORDDETAILS", tempddl1, txtSearchItem, "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();

        }
    

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            clearScreen();
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string listid = ((Label)this.grvacc.Rows[index].FindControl("lbgrcod1")).Text.ToString();
            string lblDesc = ((Label)this.grvacc.Rows[index].FindControl("lbldesc")).Text.ToString();

            this.txtBoxTitle.Text = lblDesc;
            editbyId.Value = listid;
            this.txtBoxTitle.Focus();

            this.lnkAdd.Text = "Update";
        }

        protected void lnkAdd_Click(object sender, EventArgs e)
        {
                
            string Message;            
            string comcod = this.GetCompCode();
            string gdesc = this.txtBoxTitle.Text.Trim();
            string maincode = (this.ddlEmpAcarecord.SelectedValue.ToString()).Substring(0, 5);
            string editedid = editbyId.Value.ToString()??"";

            maincode = (editedid!="") ? editedid : maincode;

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_CODEBOOK", "INOUPEACADEMICRECORDLASTIDWISE", maincode,gdesc, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                clearScreen();
                this.lnkAdd.Text = "Add";

                Message = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                 
            }

            else
            {
                Message = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                clearScreen();
            }

            this.ShowInformation();

        }

        protected void ddlEmpAcarecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowInformation();
            this.grvacc_DataBind();
        }
        private void clearScreen()
        {
            editbyId.Value = "";
            this.txtBoxTitle.Text = "";
        }
    }
}