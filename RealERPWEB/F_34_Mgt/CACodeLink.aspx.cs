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

namespace RealERPWEB.F_34_Mgt
{
    public partial class CACodeLink : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "Chart of Accounts Permission";
                this.tbnRadio.SelectedIndex = 0;
                this.Getuser();
            }

        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void Load_Project_Combo()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string FindProject = this.tbnRadio.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETCALIST", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProjectName.DataTextField = "cadesc";
            this.ddlProjectName.DataValueField = "cacode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }
        private void Getuser()
        {
            if (this.lbtnOk.Text == "New")
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mSrchTxt = "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "New")
            {

                this.ddlUserList.Enabled = true;

                this.ddlProjectName.Enabled = true;

                //this.txtProSearch.Text = "";
                this.ddlProjectName.Items.Clear();
                this.gvCALinkInfo.DataSource = null;
                this.gvCALinkInfo.DataBind();
                this.Panel2.Visible = false;
                this.lbtnOk.Text = "Ok";
                return;
            }



            this.ddlUserList.Enabled = false;

            this.Panel2.Visible = true;
            this.lbtnOk.Text = "New";
            this.tbnRadio_SelectedIndexChanged(null, null);
            this.Get_Receive_Info();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }

        protected string GetStdDate(string Date1)
        {
            Date1 = (Date1.Trim().Length == 0 ? DateTime.Today.ToString("dd.MM.yyyy") : Date1);
            string[] moth1 = { "XXX", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            Date1 = Date1.Substring(0, 2) + "-" + moth1[Convert.ToInt32(Date1.Substring(3, 2))] + "-" + Date1.Substring(6, 4);
            return Date1;
        }



        private void Get_Receive_Info()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string UserCode = this.ddlUserList.SelectedValue.ToString();
            string cType = this.tbnRadio.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "GETCALINK", UserCode, cType, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbPreLink"] = ds1.Tables[0];

            this.gvCALinkInfo_DataBind();

        }
        protected void gvCALinkInfo_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            this.gvCALinkInfo.DataSource = tbl1;
            this.gvCALinkInfo.DataBind();

        }

        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvCALinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvCALinkInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvCALinkInfo.PageIndex) * this.gvCALinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            Session["tbPreLink"] = tbl1;
        }

        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            string ProCode = this.ddlProjectName.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("cacode = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["cacode"] = this.ddlProjectName.SelectedValue.ToString();
                dr1["cadesc"] = this.ddlProjectName.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            Session["tbPreLink"] = tbl1;
            this.gvCALinkInfo_DataBind();
        }

        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            this.Session_tbltbPreLink_Update();
            string userid = this.ddlUserList.SelectedValue.ToString();
            string cType = this.tbnRadio.SelectedValue.ToString();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string cacode = tbl1.Rows[i]["cacode"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "INSERTUPDATECALINK",
                              userid, cacode, mRMRKS, cType, "", "", "", "", "", "", "", "", "", "", "");
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

        //protected void ImgbtnFindUser1_Click(object sender, EventArgs e)
        //{
        //    this.Getuser();
        //}
        protected void ImgbtnFindProject_Click(object sender, EventArgs e)
        {
            this.Load_Project_Combo();
        }

        protected void gvCALinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tbPreLink"];
            string UserName = this.ddlUserList.SelectedValue.ToString();
            string BankCode = ((Label)this.gvCALinkInfo.Rows[e.RowIndex].FindControl("lblgvprocode")).Text.Trim();
            string cType = this.tbnRadio.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK02", "DELETECACODE", UserName, BankCode, cType, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvCALinkInfo.PageSize) * (this.gvCALinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tbPreLink");
            Session["tbPreLink"] = dv.ToTable();
            this.gvCALinkInfo_DataBind();

        }
        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbPreLink"];

            for (int i = 0; i < this.ddlProjectName.Items.Count; i++)
            {
                string ProCode = this.ddlProjectName.Items[i].Value;
                DataRow[] dr = dt.Select("cacode='" + ProCode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["cacode"] = this.ddlProjectName.Items[i].Value;
                    dr1["cadesc"] = this.ddlProjectName.Items[i].Text.Substring(3);
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);


                }


            }

            Session["tbPreLink"] = dt;
            this.gvCALinkInfo_DataBind();
        }
        protected void tbnRadio_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.tbnRadio.SelectedIndex == 0)
            {
                this.ImgbtnFindProject.Text = "Accounts Code";

            }
            else
            {
                this.ImgbtnFindProject.Text = "Resource Code";


            }
            this.ImgbtnFindProject_Click(null, null);
            this.Get_Receive_Info();
        }
    }
}
