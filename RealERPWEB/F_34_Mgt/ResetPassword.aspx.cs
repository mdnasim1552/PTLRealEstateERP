using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_34_Mgt
{

    public partial class ResetPassword : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        ProcessAccess User = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        string msg = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //this.lnkPrint.Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                this.Getuser();
                ShowUserInfo();
                Bind_EmpId();
                getHomeMenu();

            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        private void ShowUserInfo()
        {
            Session.Remove("tblUsrinfo");
            string comcod = GetCompCode();
            string SearcUser = "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSER", SearcUser, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            { 
                return;
            }

            Session["tblUsrinfo"] = ds1.Tables[0];
            Session["tblUsrinfo1"] = ds1.Tables[1];
           

        }
        private void Getuser()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string empid = hst["empid"].ToString(); 
            string comcod = this.GetCompCode();
            string mSrchTxt = this.ddlUserList.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETUSERNAME", mSrchTxt, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlUserList.DataTextField = "usrsname";
            this.ddlUserList.DataValueField = "usrid";
            this.ddlUserList.DataSource = ds1.Tables[0];
            this.ddlUserList.DataBind();
            ds1.Dispose();
        }

        private void Bind_EmpId()
        {
            string comcod = this.GetCompCode();
            string empcode = ((DataTable)Session["tblUsrinfo"]).Rows[0]["empid"].ToString();
            string SearchProject = "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddlmEmpId.DataTextField = "empname";
            ddlmEmpId.DataValueField = "empid";
            ddlmEmpId.DataSource = ds1;
            ddlmEmpId.DataBind();
            ddlmEmpId.SelectedValue = empcode;
        }
        private void getHomeMenu()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "HOMEMENULINK", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMenuLink.DataSource = ds1.Tables[0];
            this.ddlMenuLink.DataBind();
            this.ddlMenuLink.DataTextField = "modulename";
            this.ddlMenuLink.DataValueField = "url";
            this.ddlMenuLink.DataBind();
        }
        protected void lbtnSaveUser_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            string usrid = this.txtmUesrId.Text.Trim();
            string usrsname = this.txtmShortName.Text.Trim();
            string usrfname = this.txtmFullName.Text.Trim();
            string usrdesig = this.txtmDesignation.Text.Trim();
            string usrpass = this.txtmPassword.Text.Trim();
            string usrrmrk = this.txtmGraph.Text.Trim();
            string active = this.chkmUserActive.Checked ? "1" : "0";
            string empid = this.ddlmEmpId.SelectedValue.ToString();
            string usermail = this.txtmUserEmail.Text.Trim();
            string webmailpwd = this.txtmWebMailPass.Text.Trim();
            string userRole = this.ddlmUserRole.SelectedValue.ToString();
            string homeurl = this.ddlMenuLink.SelectedValue.ToString();



            usrpass = (usrpass.Length == 0) ? "" : ASTUtility.EncodePassword(usrpass);
            bool result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "UPDATEUSERINFORMATION", usrid, usrsname,
                      usrfname, usrdesig, usrpass, usrrmrk, active, empid, usermail, webmailpwd, userRole, homeurl, "", "", "");


            if (!result)
            {
                msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "New User Created Failed!" + "');", true);
                return;

            }

            msg = "User Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            this.ShowUserInfo();


            string eventtype = "User Updated Successfully";
            string eventdesc = "Update by ID "+ usrsname;
            string eventdesc2 = "User profile Updated,";

            if (ConstantInfo.LogStatus == true)
            {
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            // for notification
            // title  details recvier id
            bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, usrid);
        }


        protected void ddlUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();
            string empcode = this.ddlUserList.SelectedValue.ToString();

            DataSet ds1 = User.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETUSERINFOBYID", empcode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            this.txtmUesrId.Text = ds1.Tables[0].Rows[0]["usrid"].ToString();
            this.txtmShortName.Text = ds1.Tables[0].Rows[0]["usrsname"].ToString();
            this.txtmFullName.Text = ds1.Tables[0].Rows[0]["usrname"].ToString();
            this.txtmDesignation.Text = ds1.Tables[0].Rows[0]["usrdesig"].ToString();
            this.txtmPassword.Text = "";
            string empid = ds1.Tables[0].Rows[0]["empid"].ToString() == "" ? "": ds1.Tables[0].Rows[0]["empid"].ToString();
            if (empid != "")
            {
                this.ddlmEmpId.SelectedValue = empid;

            }

            this.ddlmUserRole.SelectedValue = ds1.Tables[0].Rows[0]["userrole"].ToString();
            this.chkmUserActive.Checked = (ds1.Tables[0].Rows[0]["usractive"].ToString() == "True") ? true : false;
            this.ddlMenuLink.SelectedValue = ds1.Tables[0].Rows[0]["homeurl"].ToString();
        }
    }
}