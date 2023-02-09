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
    public partial class MktClientInfo : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
            if (this.ddlProjectName.Items.Count == 0)
            {
                this.GetClientName();

            }
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
            this.Master.Page.Title = dr1[0]["dscrption"].ToString();

            ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
            //((Label)this.Master.FindControl("lblTitle")).Text = "CLIENT INFORMATION ";
        }
        private void GetClientName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtProsCode = "%" + this.txtSrcPro.Text + "%";
            string Calltype = (this.Request.QueryString["Type"] == "Client") ? "getclientinfo" : "getAllclientinfo";
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", Calltype, txtProsCode, userid, "", "", "", "", "", "", "");
            Session["names"] = ds1.Tables[0];
            this.ddlProjectName.DataTextField = "prosdesc";
            this.ddlProjectName.DataValueField = "proscod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetClientName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            if (this.lbtnOk.Text == "Ok")
            {

                this.lbtnOk.Text = "New";

                this.ddlProjectName.Enabled = false;
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
            this.ddlProjectName.Enabled = true;
            this.gvPersonalInfo.DataSource = "";
            this.gvPersonalInfo.DataBind();
            this.lmsg1.Text = "";


        }




        private void LoadGrid()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string proscod = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "CLIENTINFO", proscod, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            ds1.Dispose();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }






        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {

        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            this.lmsg1.Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                this.lmsg1.Text = "You have no permission";
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string proscod = this.ddlProjectName.SelectedValue.ToString();
            //string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                MktData.UpdateTransInfo(comcod, "SP_ENTRY_CLIENT_INFORMATION", "INSERTORUPDATECUSTINF", proscod, "", Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");

            }
            this.lmsg1.Text = "Updated Successfully";
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }


        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dd1 = (DataTable)Session["names"];
            string code = this.ddlProjectName.SelectedValue.ToString();
            DataRow[] dr = dd1.Select("proscod='" + code + "'");

            //((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtgvVal")).Text = "";
            ((TextBox)this.gvPersonalInfo.Rows[0].FindControl("txtgvVal")).Text = dr[0]["descrp"].ToString();

        }
        protected void lbtnPrint_Click1(object sender, EventArgs e)
        {

        }
        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01001")
                {

                    txtgvname.ReadOnly = true;

                }

            }

        }
    }
}

