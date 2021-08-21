using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Collections.Generic;
using RealERPLIB;
using RealERPRPT;
using RealEntity;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{

    public partial class HrLeaveApprovalForm : System.Web.UI.Page
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "HR Approval Setup";

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                this.GetDepartment();
                //this.GetEmployeeName();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        public void LoadOrderDapp()
        {
            string comcod = GetCompCode();
            string dptName = this.ddldpt.SelectedValue.ToString();
            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "SELECTLEAVEAPP", dptName, "", "", "", "", "", "", "", "");
            DataTable UserInfoTable = ds.Tables[0];
            ViewState["UserInfoTable"] = UserInfoTable;
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void GetDepartment()
        {

            string comcod = GetCompCode();
            string txtCompanyname = "94%";
            string txtSearchDept = "%%";
            DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETALLDEPT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddldpt.DataTextField = "actdesc";
            this.ddldpt.DataValueField = "actcode";
            this.ddldpt.DataSource = ds1.Tables[0];
            this.ddldpt.DataBind();
            ds1.Dispose();
        }
        //protected void ddldpt_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.GetEmployeeName();
        //}

        //private void GetEmployeeName()
        //{
        //    string comcod = GetCompCode();
        //    string mSrchTxt = this.ddldpt.SelectedValue.ToString()+"%";

        //    DataSet ds1 = purData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", mSrchTxt, "%%", "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;

        //    this.ddlEmploye.DataTextField = "empname";
        //    this.ddlEmploye.DataValueField = "empid";
        //    this.ddlEmploye.DataSource = ds1.Tables[0];
        //    this.ddlEmploye.DataBind();
        //    ds1.Dispose();

        //}
        private void Getuser()
        {

            string comcod = GetCompCode();

            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERNAMELIST", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
        }






        protected void Select_Click(object sender, EventArgs e)
        {
            string userName = ddlUserList.SelectedItem.Text;
            string usrid = ddlUserList.SelectedItem.Value;
            string comcod = GetCompCode();

            string centrid = this.ddldpt.SelectedValue.ToString();
            string actdesc = this.ddldpt.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];

            DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
            if (dr.Length == 0)
            {
                DataRow dr1 = UserInfoTable.NewRow();
                dr1["usrid"] = usrid;
                dr1["usrsname"] = userName;
                dr1["centrid"] = centrid;
                dr1["actdesc"] = actdesc;
                dr1["comcod"] = comcod;
                dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                UserInfoTable.Rows.Add(dr1);
            }

            ViewState["UserInfoTable"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }
        protected void SelectAll_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode(); ;
            string centrid = this.ddldpt.SelectedValue.ToString();
            string actdesc = this.ddldpt.SelectedItem.Text;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            for (int i = 0; i < this.ddlUserList.Items.Count; i++)
            {
                string usrid = this.ddlUserList.Items[i].Value;
                string userName = this.ddlUserList.Items[i].Text;
                DataRow[] dr = UserInfoTable.Select("usrid='" + usrid + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = UserInfoTable.NewRow();
                    dr1["usrid"] = usrid;
                    dr1["usrsname"] = userName;
                    dr1["centrid"] = centrid;
                    dr1["actdesc"] = actdesc;
                    dr1["comcod"] = comcod;
                    dr1["slno"] = gvProLinkInfo.Rows.Count + 1;
                    UserInfoTable.Rows.Add(dr1);
                }
            }


            ViewState["UserInfoTable"] = UserInfoTable;
            gvProLinkInfo_DataBind();
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }





        protected void gvProLinkInfo_DataBind()
        {
            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            this.gvProLinkInfo.DataSource = UserInfoTable;
            this.gvProLinkInfo.DataBind();

        }



        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string dpt = this.ddldpt.SelectedValue.ToString();
            string comcod = GetCompCode(); ;

            DataTable UserInfoTable = (DataTable)ViewState["UserInfoTable"];
            if (!IsValid(UserInfoTable))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Invalid Serial No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            if (HasDup(UserInfoTable))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Duplicate Serial No";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }

            bool result = false;
            result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEUSR", dpt, "", "", "", "", "", "", "", "", "", "", "", "");

            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {

                //string userid = UserInfoTable.Rows[i]["usrid"].ToString();
                string userid = ((Label)gvProLinkInfo.Rows[i].FindControl("userid")).Text;
                string slNum = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTLEAVEAPP", slNum, userid, dpt, "", "", "", "", "", "", "", "", "", "", "", "");


            }
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed!"; //purData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
           ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(01;", true);



        }
        public bool IsValid(DataTable UserInfoTable)
        {
            int slNum = 0;
            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                try
                {
                    slNum = Convert.ToInt32(((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text);
                }
                catch (Exception)
                {
                    return false;
                }


            }

            return true;
        }
        public bool IsUnique(DataTable UserInfoTable, string str, int index)
        {

            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                string temp = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                if (str == temp && index != i)
                    return false;
            }

            return true;
        }

        public bool HasDup(DataTable UserInfoTable)
        {
            for (int i = 0; i < UserInfoTable.Rows.Count; i++)
            {
                string temp = ((TextBox)gvProLinkInfo.Rows[i].FindControl("slno")).Text;
                if (!IsUnique(UserInfoTable, temp, i))
                    return true;
            }
            return false;
        }


        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            string comcod = this.GetCompCode();
            string centrid = this.ddldpt.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["UserInfoTable"];
            string userid = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("userid")).Text.Trim();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELLEAVEAPP", userid, centrid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
                ViewState["UserInfoTable"] = dt;
                gvProLinkInfo_DataBind();
            }

        }
        protected void lbtnDeleteAll_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)ViewState["UserInfoTable"];
            string centrid = this.ddldpt.SelectedValue.ToString();
            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELALLLEAVEAPP", centrid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            BindGrid();
        }


        //LinkButton4

        protected void GetcntresByNam(object sender, EventArgs e)
        {
            //GetCentres();
            lbtnOkOrNew.Visible = true;


        }
        protected void GetUserInfo(object sender, EventArgs e)
        {
            if (lbtnOkOrNew.Text == "Ok")
            {
                if (ddldpt.Items.Count > 0)
                {
                    this.Panel2.Visible = true;
                    lbtnOkOrNew.Text = "New";
                    this.LoadOrderDapp();
                    ddldpt.Enabled = false;
                    //ddlEmploye.Enabled = false;

                    BindGrid();
                    this.Getuser();

                }

                else
                {
                    ddldpt.Enabled = true;
                    //ddlEmploye.Enabled = true;
                    this.Panel2.Visible = false;
                }

                return;
            }

            if (lbtnOkOrNew.Text == "New")
            {
                ddldpt.Enabled = true;
                //ddlEmploye.Enabled = true;

                lbtnOkOrNew.Text = "Ok";
                this.Panel2.Visible = false;
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

            }

        }
        protected void BindGrid()
        {
            LoadOrderDapp();
            gvProLinkInfo_DataBind();
        }




    }
}