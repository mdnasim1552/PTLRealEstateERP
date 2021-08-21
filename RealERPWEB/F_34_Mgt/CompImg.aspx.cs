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
using System.IO;
using RealEntity;
using RealERPLIB;
namespace RealERPWEB.F_34_Mgt
{
    public partial class CompImg : System.Web.UI.Page
    {
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        ProcessAccess UserData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "COMPANY IMAGE UPLOAD";
                //((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.GetCompanyName();

            }

            if (imgFileUpload.HasFile)
            {

                Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                imgFileUpload.PostedFile.SaveAs(savelocation);
                this.ComImg.ImageUrl = "~/Image1/" + Upload;
                image_file = imgFileUpload.PostedFile.InputStream;
                size = imgFileUpload.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompanyName()
        {


            string comcod = this.GetComeCode();
            string txtUser = "%" + this.txtUserSrc.Text + "%";
            DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMPNAME", txtUser, "", "", "", "", "", "", "", "");
            this.ddlCompName.DataTextField = "comnam";
            this.ddlCompName.DataValueField = "comcod";
            this.ddlCompName.DataSource = ds3.Tables[0];
            this.ddlCompName.DataBind();

        }

        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetComeCode();
            string savelocation = Server.MapPath("~") + "\\Image1";
            string[] filePaths = Directory.GetFiles(savelocation);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            string UserId = this.ddlCompName.SelectedValue.ToString();
            byte[] photo = new byte[0];

            image_file = (Stream)Session["i"];
            size = Convert.ToInt32(Session["s"]);
            BinaryReader br = new BinaryReader(image_file);
            photo = br.ReadBytes(size);

            DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMPID", "", "", "", "", "", "", "", "", "");
            //Session["tblcom"] = ds3;
            bool updatPhoto;
            ProcessAccess UserData01 = new ProcessAccess();
            if (ds3.Tables[0].Rows.Count == 0)
                updatPhoto = UserData01.InsertCompPhoto(comcod, photo);
            else
                updatPhoto = UserData01.UpdateCompPhoto(comcod, photo);

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

            Session.Remove("i");
            Session.Remove("s");
        }
        protected void ibtnUserList_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }
    }
}