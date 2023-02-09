using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_22_Sal
{
    public partial class ProjectFileDetailsEntry : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Project File Details Entry";

                this.GetProject();
                this.ShowProjFileInfo();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetProject()
        {
            string comcod = GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECT", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProject.DataTextField = "pactdesc";
            this.ddlProject.DataValueField = "pactcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
            this.ShowProjFileInfo();
        }
        private void ShowProjFileInfo()
        {
            string comcod = GetCompCode();
            string pactcode = this.ddlProject.SelectedValue.ToString();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTFILEINFO", pactcode, "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblPrjFileInfo"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];
            this.gvProjFileDet.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue.ToString());
            this.gvProjFileDet.DataSource = dt;
            this.gvProjFileDet.DataBind();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];
            string pactode = this.ddlProject.SelectedValue.ToString();
            string pactdesc = this.ddlProject.SelectedItem.Text.Trim();
            string txtFileNo = this.txtFileNo.Text.Trim();
            string txtLocation = this.txtLocation.Text;
            DataRow[] dr = dt.Select("pactcode='" + pactode + "' and fileno='"+ txtFileNo + "'");
            if(dr.Length==0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["pactcode"] = pactode;
                dr1["pactdesc"] = pactdesc;
                dr1["fileno"] = txtFileNo;
                dr1["location"] = txtLocation;
                dt.Rows.Add(dr1);
            }
            Session["tblPrjFileInfo"] = dt;
            this.Data_Bind();
        }

        protected void lbtngvDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];
            string comcod = GetCompCode();
            int gvRowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int rowIndex = (this.gvProjFileDet.PageSize) * (this.gvProjFileDet.PageIndex) + gvRowIndex;
            string pactID = dt.Rows[rowIndex]["id"].ToString();
            bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEPROJFILEINFO", pactID, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                dt.Rows[rowIndex].Delete();
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data Deleted successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tblPrjFileInfo");
            Session["tblPrjFileInfo"] = dv.ToTable();
            this.Data_Bind();
        }

        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }


            string comcod = this.GetCompCode();
            this.SaveValue();
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string pactcode = dt.Rows[i]["pactcode"].ToString();
                string fileNo = dt.Rows[i]["fileno"].ToString();
                string location = dt.Rows[i]["location"].ToString();
                bool result = accData.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "INSERTUPDATEPROJFILEINFO", pactcode, fileNo, location, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = accData.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveValue();
            this.Data_Bind();
        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];
            int row;
            foreach(GridViewRow gvr in gvProjFileDet.Rows)
            {
                row = (this.gvProjFileDet.PageSize) * (this.gvProjFileDet.PageIndex) + gvr.RowIndex;
                string txtFileNo = ((TextBox)gvr.FindControl("txtgvFileNo")).Text.ToString();
                string txtLocation = ((TextBox)gvr.FindControl("txtgvLocation")).Text.ToString();
                dt.Rows[row]["fileno"] = txtFileNo;
                dt.Rows[row]["location"] = txtLocation;
                
            }
            Session["tblPrjFileInfo"] = dt;
        }

        protected void gvProjFileDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvProjFileDet.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if(this.lbtnOk.Text.Trim() == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.pnlAdd.Visible = true;
                this.pnlGrid.Visible = true;
                this.lblFileNo.Visible = true;
                this.txtFileNo.Visible = true;
                this.lblLocation.Visible = true;
                this.txtLocation.Visible = true;
                this.lbtnAdd.Visible = true;
                this.ShowProjFileInfo();
            }
            else
            {
                this.pnlGrid.Visible = false;
                this.pnlAdd.Visible = false;
                this.lblFileNo.Visible = false;
                this.txtFileNo.Visible = false;
                this.lblLocation.Visible = false;
                this.txtLocation.Visible = false;
                this.lbtnAdd.Visible = false;
                this.lbtnOk.Text = "Ok";
                this.ShowProjFileInfo();
            }
        }
    }
}