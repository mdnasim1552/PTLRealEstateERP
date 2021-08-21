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
using RealERPRPT;
namespace RealERPWEB.F_34_Mgt
{
    public partial class UserImage : System.Web.UI.Page
    {
        ProcessAccess UserData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE IMAGE UPLOAD ";

                this.GetUserName();
                this.GetExitsImages();
            }
            if (imgFileUpload.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst == null)
                    return;
                string UserId = this.ddlUserName.SelectedValue.ToString();

                //     string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                bool updatPhoto;
                string uploadFolder = Request.PhysicalApplicationPath + "Upload\\UserImages\\";
                string extension = Path.GetExtension(imgFileUpload.PostedFile.FileName);
                string savelocation = uploadFolder + UserId + extension;

                image_file = imgFileUpload.PostedFile.InputStream;
                size = imgFileUpload.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

                if (size < 125000)
                {
                    string dburl = "~/Upload/UserImages/" + UserId + extension;
                    var filePath = Server.MapPath("~/Upload/UserImages/" + UserId + extension);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }


                    imgFileUpload.SaveAs(savelocation);
                    // ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                    updatPhoto = UserData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSERIMAGES", UserId, dburl, "", "", "", "", "", "", "", "");
                    if (updatPhoto)
                    {
                        this.EmpImg.ImageUrl = dburl;

                        string msg = "Your Porofile Picture Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    }

                    else
                    {
                        string msg = "Profile Picture Updated failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }


                }
                else
                {
                    string msg = "Profile Picture Size Large";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;

                }


            }
            if (imgSigFileUpload.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst == null)
                    return;
                string UserId = this.ddlUserName.SelectedValue.ToString();

                //string UserId = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                bool updatPhoto;
                string uploadFolder = Request.PhysicalApplicationPath + "Upload\\UserImages\\";
                string extension = Path.GetExtension(imgSigFileUpload.PostedFile.FileName);
                string savelocation = uploadFolder + UserId + "_Signature" + extension;

                image_file = imgSigFileUpload.PostedFile.InputStream;
                size = imgSigFileUpload.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

                if (size < 125000)
                {
                    string dburl = "~/Upload/UserImages/" + UserId + "_Signature" + extension;
                    var filePath = Server.MapPath("~/Upload/UserImages/" + UserId + "_Signature" + extension);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }


                    imgSigFileUpload.SaveAs(savelocation);
                    // ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                    updatPhoto = UserData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSERSIGNATURE", UserId, dburl, "", "", "", "", "", "", "", "");
                    if (updatPhoto)
                    {
                        this.EmpSig.ImageUrl = dburl;

                        string msg = "Your Signature Updated Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    }

                    else
                    {
                        string msg = "Signature Updated failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        return;
                    }


                }
                else
                {
                    string msg = "Signature Size Large";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;

                }


            }

            //if (imgSigFileUpload.HasFile)
            //{
            //    if (imgSigFileUpload.PostedFile != null && imgSigFileUpload.PostedFile.FileName != "")
            //    {

            //        // 10240 KB means 10MB, You can change the value based on your requirement

            //        if (imgSigFileUpload.PostedFile.ContentLength > 51200)
            //        {
            //            string msg = "Profile Picture Size Large";
            //            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            //            return;

            //        }
            //    }

            //    Upload = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName);
            //    string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
            //    string filepath = savelocation;
            //    imgSigFileUpload.PostedFile.SaveAs(savelocation);
            //    EmpSig.ImageUrl = "~/Image1/" + Upload;
            //    // Session["x1"] = "~/Image1/" + Upload;
            //    image_file = imgSigFileUpload.PostedFile.InputStream;
            //    size = imgSigFileUpload.PostedFile.ContentLength;
            //    Session["i1"] = image_file;
            //    Session["s1"] = size;
            //    // image_file.Close();
            //}

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

        private void GetExitsImages()
        {
            string comcod = this.GetCompCode();
            string userId = this.ddlUserName.SelectedItem.ToString();
            DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERNAME", userId, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.EmpImg.ImageUrl = ds3.Tables[0].Rows[0]["IMGURL"].ToString();

            this.EmpSig.ImageUrl = ds3.Tables[0].Rows[0]["SIGNURL"].ToString();

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        protected void ibtnUserList_Click(object sender, EventArgs e)
        {
            this.GetUserName();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        //protected void btnCv_Click(object sender, EventArgs e)
        //{
        //    this.PrintEmpAllInfo();
        //}

        private void StartUpLoad()
        {

            //get the file name of the posted image

            string imgName = imgFileUpload.FileName;

            //sets the image path

            string imgPath = "ImageStorage/" + imgName;

            //get the size in bytes that



            int imgSize = imgFileUpload.PostedFile.ContentLength;



            //validates the posted file before saving

            if (imgFileUpload.PostedFile != null && imgFileUpload.PostedFile.FileName != "")
            {

                // 10240 KB means 10MB, You can change the value based on your requirement

                if (imgFileUpload.PostedFile.ContentLength > 10240)
                {

                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);

                }

                else
                {

                    //then save it to the Folder

                    imgFileUpload.SaveAs(Server.MapPath(imgPath));

                    this.EmpImg.ImageUrl = "~/" + imgPath;

                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image saved!')", true);

                }


            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                string comcod = this.GetCompCode();
                string savelocation = Server.MapPath("~") + "\\Image1";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                string UserId = this.ddlUserName.SelectedValue.ToString();
                //long[] photo = new long[0];
                //long[] signature = new long[0];

                byte[] photo = new byte[0];
                byte[] signature = new byte[0];


                image_file = (Stream)Session["i"];
                size = Convert.ToInt32(Session["s"]);
                //Stream fstream = new FileStream(image_file);
                // photo=
                BinaryReader br = new BinaryReader(image_file);
                photo = br.ReadBytes(size);

                //Signature
                if (Session["i1"] != null)
                {
                    image_file = (Stream)Session["i1"];
                    size = Convert.ToInt32(Session["s1"]);
                    BinaryReader br1 = new BinaryReader(image_file);
                    signature = br1.ReadBytes(size);
                }

                DataSet ds3 = UserData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERID", UserId, "", "", "", "", "", "", "", "");
                ProcessAccess UserData01 = new ProcessAccess("ASTREALERPMSG");

                bool updatPhoto;
                if (ds3.Tables[0].Rows.Count == 0)
                {

                    updatPhoto = UserData01.InsertUserPhoto(comcod, UserId, photo, signature);
                    // updatPhoto = HRData.InsertClientPhoto(comcod, empid, photo, signature);
                    //bool updatPhoto = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGE", empid, photo.ToString(), signature.ToString(), "", "", "", "", "", "", "", "", "", "", "", "");
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
                    // updatPhoto = HRData.UpdateClientPhoto(comcod, empid, photo, signature);

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

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }




        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

        }
        protected void ddlUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExitsImages();
        }
    }
}