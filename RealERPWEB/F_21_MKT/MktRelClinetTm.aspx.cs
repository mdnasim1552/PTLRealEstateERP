using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_21_MKT
{
    public partial class MktRelClinetTm : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GetMarketingTeam();
                this.GetCleint();
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            }

        }

        private void GetMarketingTeam()
        {
            string comcod = this.GetComeCode();
            string txtsrchteam = this.txtSrcteam.Text.Trim() + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETMARKETTEAM", txtsrchteam, "", "", "", "", "", "", "", "");
            this.ddlMarketingteam.DataTextField = "teamdesc";
            this.ddlMarketingteam.DataValueField = "teamcode";
            this.ddlMarketingteam.DataSource = ds1.Tables[0];
            this.ddlMarketingteam.DataBind();
        }
        private void GetCleint()
        {
            string comcod = this.GetComeCode();
            string txtsrchteam = this.txtSrcclient.Text.Trim() + "%";
            DataSet ds2 = MktData.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "GETCLEINT", txtsrchteam, "", "", "", "", "", "", "", "");
            this.ddlCleint.DataTextField = "prosdesc";
            this.ddlCleint.DataValueField = "proscod";
            this.ddlCleint.DataSource = ds2.Tables[0];
            this.ddlCleint.DataBind();
        }
        private string GetComeCode()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ibtnFindMteam_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetMarketingTeam();

        }
        protected void ibtnFindClient_Click(object sender, ImageClickEventArgs e)
        {
            this.GetCleint();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblMktteam.Text = this.ddlMarketingteam.SelectedItem.Text.Trim();
                this.ddlMarketingteam.Visible = false;
                this.lblMktteam.Visible = true;
                this.pnlclient.Visible = true;
                this.lblpagesize.Visible = true;
                this.ddlpagesize.Visible = true;
                this.GetCleint();
                this.ShowInformation();

                return;
            }
            this.lbtnOk.Text = "Ok";
            this.lblMktteam.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.gvCleint.DataSource = null;
            this.gvCleint.DataBind();
            this.ddlMarketingteam.Visible = true;
            this.lblMktteam.Visible = false;
            this.pnlclient.Visible = false;
            this.lblpagesize.Visible = false;
            this.ddlpagesize.Visible = false;


        }

        private void ShowInformation()
        {
            Session.Remove("tblmteam");
            string comcod = this.GetComeCode();
            string MTeam = this.ddlMarketingteam.SelectedValue.ToString();
            DataSet ds3 = MktData.GetTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "SHOWCLEINTFORTM", MTeam, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvCleint.DataSource = null;
                this.gvCleint.DataBind();
                return;

            }

            Session["tblmteam"] = ds3.Tables[0];
            this.LoadGrid();


        }

        private void LoadGrid()
        {
            this.gvCleint.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvCleint.DataSource = (DataTable)Session["tblmteam"];
            this.gvCleint.DataBind();


        }



        protected void lbtnSelect_Click(object sender, EventArgs e)
        {

            string clientcode = this.ddlCleint.SelectedValue.ToString();

            DataTable dt = (DataTable)Session["tblmteam"];
            DataRow[] dr1 = dt.Select("proscod = '" + clientcode + "'");
            if (dr1.Length > 0)
            {
                return;

            }

            DataRow dr2 = dt.NewRow();
            dr2["proscod"] = clientcode;
            dr2["prosdesc"] = this.ddlCleint.SelectedItem.Text.Trim();
            dt.Rows.Add(dr2);
            Session["tblmteam"] = dt;
            this.LoadGrid();
        }
        protected void gvCleint_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCleint.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            try
            {

                string comcod = this.GetComeCode();
                string teamcode = this.ddlMarketingteam.SelectedValue.ToString();
                DataTable dt = (DataTable)Session["tblmteam"];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string clientcode = dt.Rows[i]["proscod"].ToString();
                    bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "UPDATEPROSINF", clientcode, teamcode, "", "",
                         "", "", "", "", "", "", "", "", "", "", "");
                }
             ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                if (ConstantInfo.LogStatus == true)
                {
                    string eventtype = "Client Team";
                    string eventdesc = "Update Team";
                    string eventdesc2 = "";
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                }

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }

        }

        protected void gvCleint_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblmteam"];
            string clientcode = ((Label)this.gvCleint.Rows[e.RowIndex].FindControl("lgcClientCode")).Text.Trim();
            bool result = MktData.UpdateTransInfo(comcod, "SP_ENTRY_MKT_TEAM", "UPDATEPROSINF", clientcode, "", "", "",
                       "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvCleint.PageSize) * (this.gvCleint.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session["tblmteam"] = dv.ToTable();
            this.GetCleint();
            this.LoadGrid();

        }
    }
}
