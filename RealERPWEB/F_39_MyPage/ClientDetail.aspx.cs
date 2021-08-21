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
namespace RealERPWEB.F_39_MyPage
{
    public partial class ClientDetail : System.Web.UI.Page
    {
        ProcessAccess MktData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "Client Datails Information";
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("../AcceessError.aspx");
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetClientName();
                }
                if (this.Request.QueryString["sircode"].Length > 0)
                {
                    this.lbtnOk_Click(null, null);
                }

            }
        }




        private string GetComdCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetClientName()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComdCode();
            //string wcomcod = this.GetWComCode();

            string userid = hst["usrid"].ToString();

            string txtProsCode = (this.Request.QueryString["sircode"].ToString() == "") ? ("%" + this.txtSrcPro.Text.Trim() + "%") : (this.Request.QueryString["sircode"].ToString() + "%");
            string Calltype = (this.Request.QueryString["Type"] == "Client") ? "GETCLIENTINFO" : "GETALLCLIENTINFO";
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", Calltype, txtProsCode, userid, "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "prosdesc";
            this.ddlPrjName.DataValueField = "proscod";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetClientName();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlPrjName.SelectedItem.Text;
                this.ddlPrjName.Visible = false;
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

            this.lblProjectdesc.Text = "";

            this.lblProjectdesc.Visible = false;
            this.gvPersonalInfo.DataSource = null;
            this.gvPersonalInfo.DataBind();
        }

        private void LoadGrid()
        {


            string comcod = this.GetComdCode();
            string proscod = this.ddlPrjName.SelectedValue.ToString();
            DataSet ds1 = MktData.GetTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "CLIENTINFO", proscod, "", "", "", "", "", "", "", "");
            this.gvPersonalInfo.DataSource = ds1.Tables[0];
            this.gvPersonalInfo.DataBind();
            ds1.Dispose();


        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            string comcod = this.GetComdCode();
            string proscod = this.ddlPrjName.SelectedValue.ToString();
            //string Usircode = this.lblCode.Text.Trim();
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                bool result = MktData.UpdateTransInfo(comcod, "dbo_kpi.SP_ENTRY_CLIENT_INFORMATION", "INSERTORUPDATECUSTINF", proscod, "", Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Client Info";
                string eventdesc = "Update Info";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }


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



