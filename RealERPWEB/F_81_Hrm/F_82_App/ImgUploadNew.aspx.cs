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
using System.Drawing;

namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class ImgUploadNew : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
   
                //   this.GetEmployeeName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE IMAGE UPLOAD ";
                this.GetCompanyName();
            }

          
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            // this.PrintEmpAllInfo();
        }


        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            string txtCompany = "%%"; //(this.Request.QueryString["Type"].ToString().Trim() == "Aggrement") ? this.txtSrcCompanyAgg.Text.Trim() + "%" : this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyAgg.DataTextField = "actdesc";
            this.ddlCompanyAgg.DataValueField = "actcode";
            this.ddlCompanyAgg.DataSource = ds5.Tables[0];
            this.ddlCompanyAgg.DataBind();
            this.GetDepartment();
        }

        private void GetDepartment()
        {
            string comcod = this.GetCompCode();
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string Company = ((this.ddlCompanyAgg.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyAgg.SelectedValue.ToString().Substring(0, 2)) + "%";

            string txtSProject = "%%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentagg.DataTextField = "deptdesc";
            this.ddldepartmentagg.DataValueField = "deptcode";
            this.ddldepartmentagg.DataSource = ds4.Tables[0];
            this.ddldepartmentagg.DataBind();

            this.GetProjectName();
            this.ddlProjectName_SelectedIndexChanged(null, null);
        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            //string type = this.Request.QueryString["Type"].ToString().Trim();
            //string Company = this.ddlCompanyAgg.SelectedValue.ToString().Trim();
            //string deptcode = this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 2) + "%";
            string deptcode = (this.ddldepartmentagg.SelectedValue.ToString() == "000000000000")? "94%" : (this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 2)+ "%");
            // : this.ddlCompany.SelectedValue.ToString().Substring(0, 2);
            string txtSProject = "%%";// ;// (type == "Aggrement") ? (this.txtSrcPro.Text.Trim() + "%") : (this.txtSrcDepartment.Text.Trim() + "%");
                                      //string CallType = (this.Request.QueryString["Type"].ToString().Trim() == "EmpAllInfo") ? "GETPROJECTNAME" : "GETPROJECTNAMEFOT";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");

            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
           // this.GetEmployeeName();
             this.ddlProjectName_SelectedIndexChanged(null, null);
            //this.GetEmpName();
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetEmployeeName()
        {
            string empcode = this.txtSrcEmployee.Text.Trim();

            string comcod = this.GetCompCode();
            
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "94%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            pactcode = (empcode.Length == 0) ? pactcode : "94%";
            empcode = empcode + "%"; // for alwayes search empcode wise 
             
            
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", pactcode, empcode, "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds5.Tables[0];
            this.ddlEmpName.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];

            this.GetComASecSelected(); 
        }

        private void GetComASecSelected()
        {

            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tblemp"];
            DataRow[] dr = dt.Select("empid = '" + empid + "'");
            if (dr.Length > 0)
            {
                this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
                this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
                this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();


            }


        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
            this.GetCompanyName();


        }
        protected void lnkbtnUpdateEMPImage_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0)|| (imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0))
            {
                Guid uid = Guid.NewGuid();
                string fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString()??"";
                string fn2 = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName).ToString()??"";
                string filePath= "Upload/HRM/EmpImg" +"" + empid + fn;
                string filePath2 = "Upload/HRM/signature" + "" + empid + fn2;
                string SaveLocation = Server.MapPath(filePath);
                string SaveLocation2 = Server.MapPath(filePath2);
                string msg = "";
                try
                {
    
                    if (fn!=null || fn != "")
                    {

                        string fileExtention = imgFileUpload.PostedFile.ContentType;
     
                        int fileLenght = imgFileUpload.PostedFile.ContentLength;
                        if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                        {
                            if (fileLenght <= 1048576)
                            {
  
                                DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");
      
                                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                                {
                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);
                                    // Saving image in jpeg format
                                    objImage.Save(SaveLocation);
                                }
                                else
                                {
                                    DataTable dt2 = ds2.Tables[0];
                                    string file1 = dt2.Rows[0]["signurl"].ToString();
                                    FileInfo getFile = new FileInfo(Server.MapPath(file1));
                                    if (getFile.Exists)
                                    {
                                        getFile.Delete();
                                    }

                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);
                                    // Saving image in jpeg format
                                    objImage.Save(SaveLocation);
                                }



                            }
                            else
                            {
                          
                                msg = "Image size cannot be more then 1 MB";
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                            }
                        }
                        else
                        {
                            msg = "Invalid Format!";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        }
                    }


                    if (fn2 != null || fn2 != "")
                    {


                        string fileExtention2 = imgSigFileUpload.PostedFile.ContentType;
                        int fileLenght2 = imgSigFileUpload.PostedFile.ContentLength;
                        if (fileExtention2 == "image/png" || fileExtention2 == "image/jpeg" || fileExtention2 == "image/x-png")
                        {
                            if (fileLenght2 <= 1048576)
                            {

                                DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");
                 
                                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                                {
                                    System.Drawing.Bitmap bmpPostedImage2 = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage2 = ScaleImage(bmpPostedImage2, 81);
                                    // Saving image in jpeg format
                                    objImage2.Save(SaveLocation2);

                                }
                                else
                                {
                                    DataTable dt2 = ds2.Tables[0];
                                    string file2 = dt2.Rows[0]["signurl"].ToString();
                                    FileInfo getFile = new FileInfo(Server.MapPath(file2));
                                    if (getFile.Exists)
                                    {
                                        getFile.Delete();
                                    }
                                    System.Drawing.Bitmap bmpPostedImage2 = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                    System.Drawing.Image objImage2 = ScaleImage(bmpPostedImage2, 81);
                                    // Saving image in jpeg format
                                    objImage2.Save(SaveLocation2);
                                }



                            }
                            else
                            {
                                msg = "Image size cannot be more then 1 MB";
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                            }
                        }
                        else
                        {
                            
                            msg = "Invalid Format";
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                        }
                    }
                    DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGENEW", empid, "", "", filePath, filePath2, "", "", "", "");






                }
                catch (Exception ex)
                {
            


                    msg = "Image size cannot be more then 1 MB";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Error: " + ex.Message + "');", true);
                }

            }



           // updatPhoto = HRData.InsertClientPhoto(comcod, empid, photo, signature);
           // if (!updatPhoto)
           // {
           //     ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
           //     ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
           //     return;
           // }
           //((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
           // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {

        }

        protected void ddldepartmentagg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.GetDepartment();
            GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }
        protected void ddlCompanyAgg_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetComASecSelected();

        }



    }
}