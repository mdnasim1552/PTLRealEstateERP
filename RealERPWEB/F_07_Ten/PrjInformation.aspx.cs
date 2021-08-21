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
namespace RealERPWEB.F_07_Ten
{
    public partial class PrjInformation : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            this.lbtnPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

            if (this.ddlPrjName.Items.Count == 0)
            {
                this.GetProjectName();
            }
        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string txtSProject = "%" + this.txtSrcPro.Text + "%";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_INFO", "GETPRJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "prjdesc1";
            this.ddlPrjName.DataValueField = "prjcod";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, ImageClickEventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlPrjName.SelectedItem.Text;
                this.lblProjectmDesc.Text = this.ddlPrjName.SelectedItem.Text.Substring(13);
                this.ddlPrjName.Visible = false;
                this.lblProjectmDesc.Visible = true;
                this.lblProjectdesc.Visible = true;
                this.LoadGrid();
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ClearScreen();
            }
        }

        private void ClearScreen()
        {
            this.ddlPrjName.Visible = true;
            this.lblProjectmDesc.Text = "";
            this.lblProjectmDesc.Visible = false;
            this.lblProjectdesc.Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lblProjectdesc.Visible = false;
            this.gvPrjInfo.DataSource = null;
            this.gvPrjInfo.DataBind();
        }

        private void LoadGrid()
        {


            string comcod = this.GetComCode();
            string ProjectCode = this.ddlPrjName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_INFO", "PROJECTINFO", ProjectCode, "", "", "", "", "", "", "", "");
            this.gvPrjInfo.DataSource = ds1.Tables[0];
            this.gvPrjInfo.DataBind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string pactcode = this.ddlPrjName.SelectedValue.ToString();

            for (int i = 0; i < this.gvPrjInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPrjInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPrjInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                string Gunit = ((TextBox)this.gvPrjInfo.Rows[i].FindControl("txtResunit")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                bool result = MktData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_INFO", "INSERTORUPDATEPRJINF", pactcode, Gcode, gtype, Gvalue, Gunit,
                    "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                }

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Project Information";
                string eventdesc = "Update Project Information";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }



    }
}



