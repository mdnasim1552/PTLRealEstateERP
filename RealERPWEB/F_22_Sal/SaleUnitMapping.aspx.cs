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
//using RealERPRPT;
namespace RealERPWEB.F_22_Sal
{
    public partial class SaleUnitMapping : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Sale Unit Mapping";

                this.ShowInformation();
                this.GetACGCode();

                // this.GetCode();
            }
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetACGCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
          
            DataSet dsone = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETUNITTYPE","", "", "", "", "", "", "");
            ViewState["tblgencode"] = dsone.Tables[0];
            dsone.Dispose();

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
            ViewState["gindex"] = e.NewEditIndex;
            int rowindex = (grvacc.PageSize) * (this.grvacc.PageIndex) + e.NewEditIndex;          
            string unittype = ((DataTable)Session["storedata"]).Rows[rowindex]["unittype"].ToString();
            DropDownList ddlteam = (DropDownList)this.grvacc.Rows[e.NewEditIndex].FindControl("ddlteam");
            ddlteam.DataTextField = "acgdesc";
            ddlteam.DataValueField = "acgcode";
            ddlteam.DataSource = (DataTable)ViewState["tblgencode"];
            ddlteam.SelectedValue = unittype;
            ddlteam.DataBind();
             //((Label)this.gvCodeBook.Rows[e.NewEditIndex].FindControl("lblgvProName")).Text.Trim();

        }

        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            

            string actcode = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgvactcode")).Text.Trim().Replace("-", "");
            string acgcode = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlteam")).SelectedValue.ToString();
            string acgdesc = ((DropDownList)this.grvacc.Rows[e.RowIndex].FindControl("ddlteam")).SelectedItem.ToString();
            DataTable tbl1 = (DataTable)Session["storedata"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int Index = grvacc.PageSize * grvacc.PageIndex + e.RowIndex;
            tbl1.Rows[Index]["sircode"] = actcode;
            tbl1.Rows[Index]["unitdesc"] = acgdesc;
            Session["storedata"] = tbl1;
            this.grvacc.EditIndex = -1;
            bool result = this.accData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "UPDATEUNITTYPE", actcode, acgcode, "", "", "", "", "", "", "", "",
                "", "", "", "", "");


            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Accounts CodeBook";
                    string eventdesc = "Update CodeBook";
                    string eventdesc2 = actcode;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

                this.ShowInformation();
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.grvacc_DataBind();
        }

        protected void grvacc_DataBind()
        {
            this.grvacc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.grvacc.DataSource = (DataTable)Session["storedata"]; ;
            this.grvacc.DataBind();
        }

        protected void ddlPageNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        private void ShowInformation()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = this.accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "SALEUNITMAPPING","", "", "", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                this.grvacc.DataSource = null;
                this.grvacc.DataBind();
                return;
            }
            Session["storedata"] = ds1.Tables[0];
            this.grvacc_DataBind();
        }

        protected void ibtnSrch_Click(object sender, EventArgs e)
        {
            this.ShowInformation();
        }
        protected void grvacc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grvacc.PageIndex = e.NewPageIndex;
            this.grvacc_DataBind();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.grvacc_DataBind();
        }
        protected void ibtnSrchProject_Click(object sender, ImageClickEventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            int rowindex = (int)ViewState["gindex"];

            DropDownList ddl2 = (DropDownList)this.grvacc.Rows[rowindex].FindControl("ddlProCode");
            string SearchProject = "%" + ((TextBox)grvacc.Rows[rowindex].FindControl("txtSerachProject")).Text.Trim() + "%";
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETPROJECT", SearchProject, "", "", "", "", "", "", "", "");
            ddl2.DataTextField = "actdesc1";
            ddl2.DataValueField = "actcode";
            ddl2.DataSource = ds1;
            ddl2.DataBind();

        }

        protected void ibtnSrchProject_Click1(object sender, EventArgs e)
        {

        }

        protected void ibtnSrchteam_Click(object sender, EventArgs e)
        {

        }

    }
}
