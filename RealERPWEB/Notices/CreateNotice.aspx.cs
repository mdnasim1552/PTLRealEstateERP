using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.Notices
{
    public partial class CreateNotice : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.GetDepartment();
                this.GetUser();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Create Notices";
                this.txtStartDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy h: mm tt");
                this.txtEndDate.Text = System.DateTime.Today.AddDays(30).ToString("dd-MMM-yyyy h: mm tt");
            }
        }

        private void GetUser()
        {
            try
            {
                string comcod = GetcompCode();
                string departmentID = this.ddlDepartment.SelectedValue.ToString();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GETNOTICEDATA", departmentID, "", "", "", "", "", "", "", "", "");
                if (ds1 == null)
                    return;

                DataTable dt = ds1.Tables[1];
                this.listboxUser.DataTextField = "usrname";
                this.listboxUser.DataValueField = "usrid";
                this.listboxUser.DataSource = dt;
                this.listboxUser.DataBind();
            }

            catch (Exception ex)
            {

            }
        }

        private string GetcompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetDepartment()
        {
            try
            {
                string comcod = GetcompCode();
                DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GETNOTICEDATA", "", "", "", "", "", "", "","","","");
                if (ds1 == null)
                    return;

                DataTable dt = ds1.Tables[0];
                this.ddlDepartment.DataTextField = "sirdesc";
                this.ddlDepartment.DataValueField = "sircode";
                this.ddlDepartment.DataSource = dt;
                this.ddlDepartment.DataBind();
                ds1.Dispose();
            }
           
            catch(Exception ex)
            {
               
            }
        }

        protected void lbtnSaveNotice_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetcompCode();
            string noCreatdBy = hst["usrid"].ToString();
            string noticeTitle = this.txtNoticeTitle.Text.Trim();
            string noticeDesc = this.txtnoticeDesc.InnerText.Trim();
            string noCrateDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string txtdeapartment = this.ddlDepartment.SelectedValue.ToString();
            string txtStartDate = this.txtStartDate.Text.ToString();
            string txtEndDate = this.txtEndDate.Text.ToString();
            string txtUser = "";
            string msg = "";
            foreach (ListItem listUser in listboxUser.Items)
            {
                if(listUser.Selected)
                {
                    txtUser += listUser.Value.ToString();
                   
                }
            }


            string imgPath = "";
        ;



            //validates the posted file before saving  
            if (attachFile.HasFile)
            {

                string filePath = attachFile.PostedFile.FileName;
                string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file  
                string ext = Path.GetExtension(filename1);

                if (ext == ".pdf")
                {

                    string imgName = Guid.NewGuid() + ext;
                    //sets the image path           
                    imgPath = "~/Upload/HRM/Doc/" + imgName;
                    //then save it to the Folder  
                    attachFile.SaveAs(Server.MapPath(imgPath));
                }
                else
                {
                    string msgfail = "Please select pdf file only";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgfail + "');", true);
                    return;
                }
            }



            bool result = purData.UpdateTransInfo(comcod, "SP_REPORT_NOTICE", "INSERTUPDATENOTICE", noticeTitle, noticeDesc, noCrateDate, txtUser, txtdeapartment, noCreatdBy, txtStartDate, txtEndDate, "Notice", imgPath, "", "", "", "", "");
            if (!result)
            {
                msg = "Update Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

            }
            else
            {
                msg = "Data Updated successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            }
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = GetcompCode();
            string departmentID = this.ddlDepartment.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_REPORT_NOTICE", "GETNOTICEDATA", departmentID, "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            DataTable dt = ds1.Tables[1];
            this.listboxUser.DataTextField = "usrname";
            this.listboxUser.DataValueField = "usrid";
            this.listboxUser.DataSource = dt;
            this.listboxUser.DataBind();
            
        }
    }
}