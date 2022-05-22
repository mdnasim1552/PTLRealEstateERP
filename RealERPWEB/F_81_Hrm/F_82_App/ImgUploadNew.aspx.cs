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
using CrystalDecisions.ReportSource;
using System.IO;
using RealERPLIB;
using RealERPRPT;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
                this.getAllData();
            }


        }

        private void getAllData()
        {
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            string comcod = this.GetCompCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvimg.DataSource = null;
                this.gvimg.DataBind();
                return;
            }

            this.gvimg.DataSource = ds.Tables[0];
            this.gvimg.DataBind();
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

            //this.GetComASecSelected();
        }

        //private void GetComASecSelected()
        //{

        //    string empid = this.ddlEmpName.SelectedValue.ToString();
        //    DataTable dt = (DataTable)ViewState["tblemp"];
        //    DataRow[] dr = dt.Select("empid = '" + empid + "'");
        //    if (dr.Length > 0)
        //    {
        //        this.ddlCompanyAgg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["companycode"].ToString();
        //        this.ddldepartmentagg.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["deptcode"].ToString();
        //        this.ddlProjectName.SelectedValue = ((DataTable)ViewState["tblemp"]).Select("empid='" + empid + "'")[0]["refno"].ToString();


        //    }


        //}
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            this.GetEmployeeName();
            this.GetCompanyName();


        }
        protected void lnkbtnUpdateEMPImage_Click(object sender, EventArgs e)
        {
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");

            string comcod = this.GetCompCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string filePath = "";
            string filePath2 = "";
            string msg = "";
            string fileExtention = "";
            int fileLenght = 0;


            //check image selected or not
            if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0) || (imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString() ?? "";
                string fn2 = System.IO.Path.GetFileName(imgSigFileUpload.PostedFile.FileName).ToString() ?? "";
                //check image
                if ((imgFileUpload.PostedFile != null) && (imgFileUpload.PostedFile.ContentLength > 0) && (fn != null || fn != ""))
                {
                    Guid uid = Guid.NewGuid();
                    fileExtention = imgFileUpload.PostedFile.ContentType;
                    fileLenght = imgFileUpload.PostedFile.ContentLength;
                    fn = System.IO.Path.GetFileName(imgFileUpload.PostedFile.FileName).ToString() ?? "";
                    filePath = "~/Upload/HRM/EmpImg/" + empid + fn;
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                    {
                        if (fileLenght <= 1048576)
                        {
                            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");

                            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                            {
                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage = ScaleImage(bmpPostedImage);

                                objImage.Save(Server.MapPath(filePath), ImageFormat.Jpeg);



                            }
                            else
                            {
                                DataTable dt2 = ds2.Tables[0];
                                string file1 = dt2.Rows[0]["imgurl"].ToString();
                                if (fn2 == null || fn2 == "")
                                {
                                    filePath2 = dt2.Rows[0]["signurl"].ToString();
                                }

                                FileInfo getFile = new FileInfo(Server.MapPath(file1));

                                if (getFile.Exists)
                                {
                                    getFile.Delete();
                                }

                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage = ScaleImage(bmpPostedImage);
                                // Saving image in jpeg format
                                objImage.Save(Server.MapPath(filePath), ImageFormat.Jpeg);

                            }


                        }
                    }

                }

                //check signature
                if ((imgSigFileUpload.PostedFile != null) && (imgSigFileUpload.PostedFile.ContentLength > 0) && (fn2 != null || fn2 != ""))
                {

                    Guid uid = Guid.NewGuid();
                    filePath2 = "~/Upload/HRM/signature/" + empid + fn2;

                    fileExtention = imgSigFileUpload.PostedFile.ContentType;
                    fileLenght = imgSigFileUpload.PostedFile.ContentLength;
                    if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                    {
                        if (fileLenght <= 1048576)
                        {
                            DataSet ds2 = HRData.GetTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "GETEMPBYID", empid, "", "", "", "", "", "", "", "");

                            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                            {
                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgSigFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage1 = ScaleImage2(bmpPostedImage);
                                // Saving image in jpeg format
                                objImage1.Save(Server.MapPath(filePath2), ImageFormat.Jpeg);


                            }
                            else
                            {
                                DataTable dt2 = ds2.Tables[0];
                                string file1 = dt2.Rows[0]["signurl"].ToString();
                                if (fn == null || fn == "")
                                {
                                    filePath = dt2.Rows[0]["imgurl"].ToString();
                                }

                                FileInfo getFile = new FileInfo(Server.MapPath(file1));
                                if (getFile.Exists)
                                {
                                    getFile.Delete();
                                }

                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(imgSigFileUpload.PostedFile.InputStream);
                                System.Drawing.Image objImage1 = ScaleImage2(bmpPostedImage);
                                // Saving image in jpeg format
                                objImage1.Save(Server.MapPath(filePath2), ImageFormat.Jpeg);

                            }


                        }
                    }

                }

                bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "INSERTUPDATEIMAGENEW", empid, "", "", filePath, filePath2, "", "", "", "", "");
                if (result)
                {
                    msg = "Image Uploaded Successfull";
                    this.getAllData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                }


            }
            else
            {
                msg = "Upload Failed!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Error: " + msg + "');", true);
            }




        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image)
        {
            //var ratio = (double)maxHeight / image.Height;
            var newWidth = 300;
            var newHeight = 300;
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }


        public static System.Drawing.Image ScaleImage2(System.Drawing.Image image)
        {

            var newWidth = 300;
            var newHeight = 80;
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        protected void btn_remove_Click1(object sender, EventArgs e)
        {
            ProcessAccess HRData = new ProcessAccess("ASITHRMIMG");
            string comcod = this.GetCompCode();
            string msg = "";
            string filepath = "";
            string filepath2 = "";



            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            filepath = ((Label)this.gvimg.Rows[index].FindControl("lblimg")).Text.ToString();
            filepath2 = ((Label)this.gvimg.Rows[index].FindControl("lblsign")).Text.ToString();
            FileInfo file = new FileInfo(Server.MapPath(filepath));
            if (file.Exists)
            {
                file.Delete();
            }

            FileInfo file2 = new FileInfo(Server.MapPath(filepath2));
            if (file2.Exists)
            {
                file2.Delete();
            }

            string empid = ((Label)this.gvimg.Rows[index].FindControl("lblid")).Text.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "SP_ENTRY_EMPLOYEEIMG", "REMOVEDATA", empid, "", "", "", "", "");
            if (result)
            {


                msg = "Deleted Successfully";
                this.getAllData();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);


            }
            else
            {

                msg = "Delete Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }

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
            //this.GetComASecSelected();
            this.getAllData();

        }

        protected void empSrc_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", "94%", "%", "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds5.Tables[0];
            this.ddlEmpName.DataBind();
        }
    }
}