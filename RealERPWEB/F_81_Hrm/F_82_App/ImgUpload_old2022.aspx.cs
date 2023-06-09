﻿using System;
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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class ImgUpload : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 1024 * 1024;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                //   this.GetEmployeeName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE IMAGE UPLOAD ";
                this.GetCompanyName();
            }

            if (imgFileUpload.HasFile)
            {
                //if (imgFileUpload.PostedFile != null && imgFileUpload.PostedFile.FileName != "")
                //{

                //    // 10240 KB means 10MB, You can change the value based on your requirement 1024*50(51200)
                //    int imgsize = imgFileUpload.PostedFile.ContentLength;
                //    if (imgFileUpload.PostedFile.ContentLength > 51200)
                //    {

                //        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File Size Less or Equal 50KB')", true);
                //        return;

                //    }
                //}

                try
                {
                    Upload = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName);
                    string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                    string filepath = savelocation;
                    imgFileUpload.PostedFile.SaveAs(savelocation);
                    EmpImg.ImageUrl = "~/Image1/" + Upload;


                    // Session["x"] = "~/Image1/" + Upload;
                    image_file = imgFileUpload.PostedFile.InputStream;
                    size = imgFileUpload.PostedFile.ContentLength;
                    Session["i"] = image_file;
                    Session["s"] = size;
                    //.imgFileUpload.tL
                    // image_file.Close();
                }
                catch (Exception ex)
                {

                }


            }

            if (imgSigFileUpload.HasFile)
            {
                //if (imgSigFileUpload.PostedFile != null && imgSigFileUpload.PostedFile.FileName != "")
                //{

                //    // 10240 KB means 10MB, You can change the value based on your requirement

                //    if (imgSigFileUpload.PostedFile.ContentLength > 51200)
                //    {
                //        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File Size Less or Equal 50KB')", true);
                //        return;

                //    }
                //}

                Upload = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                imgSigFileUpload.PostedFile.SaveAs(savelocation);
                EmpSig.ImageUrl = "~/Image1/" + Upload;
                // Session["x1"] = "~/Image1/" + Upload;
                image_file = imgSigFileUpload.PostedFile.InputStream;
                size = imgSigFileUpload.PostedFile.ContentLength;
                Session["i1"] = image_file;
                Session["s1"] = size;
                // image_file.Close();
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
            string deptcode = (this.ddldepartmentagg.SelectedValue.ToString() == "000000000000") ? "94%" : (this.ddldepartmentagg.SelectedValue.ToString().Substring(0, 2) + "%");
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
            this.ShowImage();
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
                string empid = this.ddlEmpName.SelectedValue.ToString();
                //long[] photo = new long[0];
                //long[] signature = new long[0];

                byte[] photo = new byte[0];
                byte[] signature = new byte[0];

                // Image
                if (Session["i"] != null)
                {
                    image_file = (Stream)Session["i"];
                    size = Convert.ToInt32(Session["s"]);
                    BinaryReader br = new BinaryReader(image_file);
                    photo = br.ReadBytes(size);

                }

                //Signature
                if (Session["i1"] != null)
                {
                    image_file = (Stream)Session["i1"];
                    size = Convert.ToInt32(Session["s1"]);
                    BinaryReader br1 = new BinaryReader(image_file);
                    signature = br1.ReadBytes(size);
                }

                ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
                DataSet ds3 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "EMPID", empid, "", "", "", "", "", "", "", "");
                bool updatPhoto;
                if (ds3.Tables[0].Rows.Count == 0)
                {

                    updatPhoto = HRData.InsertClientPhoto(comcod, empid, photo, signature);
                    if (!updatPhoto)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }







                     ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



                }

                else
                {
                    if (photo.Length > 0 && signature.Length > 0)
                    {
                        updatPhoto = HRData.UpdateClientPhoto(comcod, empid, photo, signature);
                        if (!updatPhoto)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        }

                    }

                    else if (photo.Length > 0)
                    {
                        updatPhoto = HRData.UpdateClientPhotoonly(comcod, empid, photo);
                        if (!updatPhoto)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        }


                    }

                    else if (signature.Length > 0)
                    {
                        updatPhoto = HRData.UpdateClientSignOnly(comcod, empid, signature);
                        if (!updatPhoto)
                        {
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated failed";
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        }

                    }

                     ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                }


                this.ShowImage();


            }

            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }

        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "DELETEUSEIMG", empid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Deleted Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Deleted";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            }

        }

        private void ShowImage()
        {
            Session.Remove("tblEmpimg");
            this.EmpImg.ImageUrl = "";
            this.EmpSig.ImageUrl = "";
            string comcod = this.GetCompCode();
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "SHOWIMG", empid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                return;

            }
            this.Hiddnrl.Value = "";
            Session["tblEmpimg"] = ds1.Tables[0];
            this.EmpImg.ImageUrl = "~/GetImage.aspx?ImgID=ImgEmp";
            this.EmpSig.ImageUrl = "~/GetImage.aspx?ImgID=HREmpSign";

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
            this.ShowImage();
        }
        protected void imgbtnEmpSeach_Click(object sender, EventArgs e)
        {
            //this.ShowValue();
        }

        protected void lnkbtnUpdateEMPImage_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetCompCode();
            string savelocation = Server.MapPath("~") + "\\Image1";
        }
    }
}