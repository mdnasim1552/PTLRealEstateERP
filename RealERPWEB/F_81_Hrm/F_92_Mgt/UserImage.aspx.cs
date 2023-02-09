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
using System.IO;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class UserImage : System.Web.UI.Page
    {
        ProcessAccess UserData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        //ProcessAccess UserData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "USER IMAGE UPLOAD";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.GetUserName();

            }

            if (imgFileUpload.HasFile)
            {

                Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                imgFileUpload.PostedFile.SaveAs(savelocation);
                this.UserImg.ImageUrl = "~/Image1/" + Upload;
                image_file = imgFileUpload.PostedFile.InputStream;
                size = imgFileUpload.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetUserName()
        {


            string comcod = this.GetCompCode();
            string txtUser = "%" + this.txtUserSrc.Text + "%";
            DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERNAME", txtUser, "", "", "", "", "", "", "", "");
            this.ddlUserName.DataTextField = "usrname";
            this.ddlUserName.DataValueField = "usrid";
            this.ddlUserName.DataSource = ds3.Tables[0];
            this.ddlUserName.DataBind();

        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
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
            string savelocation = Server.MapPath("~") + "\\Image1";
            string[] filePaths = Directory.GetFiles(savelocation);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            string UserId = this.ddlUserName.SelectedValue.ToString();
            byte[] photo = new byte[0];
            byte[] signature = new byte[0];

            image_file = (Stream)Session["i"];
            size = Convert.ToInt32(Session["s"]);
            BinaryReader br = new BinaryReader(image_file);
            photo = br.ReadBytes(size);

            DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERID", UserId, "", "", "", "", "", "", "", "");
            bool updatPhoto;
            ProcessAccess UserData01 = new ProcessAccess("ASTREALERPMSG");
            if (ds3.Tables[0].Rows.Count == 0)
            {
                updatPhoto = UserData01.InsertUserPhoto(comcod, UserId, photo, signature);
                if (updatPhoto)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }

                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }
            }

            else
            {
                updatPhoto = UserData01.UpdateUserPhoto(comcod, UserId, photo, signature);

                if (updatPhoto)
                {

                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }
                else
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                }




            }
        }
        protected void ibtnUserList_Click(object sender, EventArgs e)
        {
            this.GetUserName();
        }
    }
}