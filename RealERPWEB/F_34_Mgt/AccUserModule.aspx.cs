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
//using ACCRPT;
namespace RealERPWEB.F_34_Mgt
{
    public partial class AccUserModule : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "User Module Permission";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.Getuser();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void Getuser()
        {
            if (this.lbtnOk.Text == "New")
                return;

            string comcod = this.GetCompCode();
            string mSrchTxt = this.txtUserSearch1.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
        }

        protected void GetControlAccCode()
        {

            string comcod = this.GetCompCode();
            string FindProject = this.txtProSearch.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETMODULELIST", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlConTrolCode.DataTextField = "modulename1";
            this.ddlConTrolCode.DataValueField = "moduleid";
            this.ddlConTrolCode.DataSource = ds1.Tables[0];
            this.ddlConTrolCode.DataBind();
            ds1.Dispose();



        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddlUserList.Enabled = true;

                this.ddlConTrolCode.Enabled = true;

                this.txtProSearch.Text = "";
                this.ddlConTrolCode.Items.Clear();
                this.gvProLinkInfo.DataSource = null;
                this.gvProLinkInfo.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }



            this.ddlUserList.Enabled = false;

            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";

            this.GetUserModPer();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        //protected string GetStdDate(string Date1)
        //{
        //    Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
        //    string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
        //    return Date1;
        //}


        private void GetUserModPer()
        {

            ViewState.Remove("tbLink");
            string comcod = this.GetCompCode();
            string UserCode = this.ddlUserList.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERMODULE", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbLink"] = ds1.Tables[0];
            this.gvProLinkInfo_DataBind();
            ds1.Dispose();

        }
        protected void gvProLinkInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            this.gvProLinkInfo.DataSource = tbl1;
            this.gvProLinkInfo.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvProLinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvProLinkInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvProLinkInfo.PageIndex) * this.gvProLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            ViewState["tbLink"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            string ProCode = this.ddlConTrolCode.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("moduleid = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["moduleid"] = this.ddlConTrolCode.SelectedValue.ToString();
                dr1["modulename"] = this.ddlConTrolCode.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tbLink"] = tbl1;
            this.gvProLinkInfo_DataBind();
        }


        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbLink"];

            for (int i = 0; i < this.ddlConTrolCode.Items.Count; i++)
            {
                string actcode = this.ddlConTrolCode.Items[i].Value;
                DataRow[] dr = dt.Select("moduleid='" + actcode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["moduleid"] = this.ddlConTrolCode.Items[i].Value;
                    dr1["modulename"] = this.ddlConTrolCode.Items[i].Text;
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);
                }


            }

            ViewState["tbLink"] = dt;
            this.gvProLinkInfo_DataBind();
        }


        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            string comcod = this.GetCompCode();
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            string userid = this.ddlUserList.SelectedValue.ToString();
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string pactcode = tbl1.Rows[i]["moduleid"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUPDATEPER", userid, pactcode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = purData.ErrorObject["Msg"].ToString();
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



        protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        {
            this.Getuser();
        }
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetControlAccCode();
        }


        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["tbLink"];
            string UserName = this.ddlUserList.SelectedValue.ToString();
            string Userid = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("lblgvBancCode")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELUSERMODULEPER", UserName, Userid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tbLink");
            ViewState["tbLink"] = dv.ToTable();
            this.gvProLinkInfo_DataBind();


        }
        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string UserName = this.ddlUserList.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELUSERMODULEPERALL", UserName, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            this.GetUserModPer();
        }
    }
}