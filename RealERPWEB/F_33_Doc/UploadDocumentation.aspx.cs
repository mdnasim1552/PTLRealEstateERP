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
namespace RealERPWEB.F_33_Doc
{
    public partial class UploadDocumentation : System.Web.UI.Page
    {
     
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //if (dr1.Length == 0)
                //    Response.Redirect("../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();


                this.getType();
                this.getDept();
                this.GetYearMonth();
                this.getAllData();

            }
        }


        private void getAllData()
        {
            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "GETALLDATA", "", "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

            this.gvdoc.DataSource = ds.Tables[0];
            this.gvdoc.DataBind();
        }

        private void getType()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "GETTYPE", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlType.DataTextField = "dtype";
            this.ddlType.DataValueField = "gcod";
            this.ddlType.DataSource = ds1.Tables[0];
            this.ddlType.DataBind();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gcod = this.ddlType.SelectedValue.ToString().Substring(0, 5)??"";
            switch (gcod)
            {
                case "99901":
                    this.lbltitle.Text = "Title";
                    this.txtsName.Text = this.ddlType.SelectedItem.Text.ToString();
                    this.txtsName.Enabled = false;
                    this.pnlDept.Visible = false;
                    this.pnlMonth.Visible = false;
                    this.pnlTxt.Visible = true;
                break;

                case "99902":
                    this.lbltitle.Text = "Department";
                    this.txtsName.Text = this.ddlDept.SelectedItem.Text.ToString();
                    this.pnlDept.Visible = true;
                    this.pnlMonth.Visible = false;
                    //this.pnlTxt.Visible = false;
                    break;

                case "99903":
                    this.lbltitle.Text = "Month";
                    this.txtsName.Text = this.ddlMonth.SelectedItem.Text.ToString();
                    this.pnlDept.Visible = false;
                    this.pnlMonth.Visible = true;
                    //this.pnlTxt.Visible = false;
                    break;
            }


        }

        private void getDept()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "GetDept", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();
        }

        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlMonth.DataTextField = "yearmon";
            this.ddlMonth.DataValueField = "ymon";
            this.ddlMonth.DataSource = ds1.Tables[0];

            this.ddlMonth.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            this.ddlMonth.DataBind();

            ds1.Dispose();
        }
        protected void lnk_save_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string title = this.txtsName.Text.ToString()??"";
            string remarks = this.txtDetails1.Text;
 
            string refno =this.ddlDept.SelectedValue.ToString();
            string gcod = this.ddlType.SelectedValue.ToString().Substring(0, 5) ?? "";
            string imgPath = "";
            string msg = "";

            //validates the posted file before saving  
            if (imgFileUpload.PostedFile != null && imgFileUpload.PostedFile.FileName != "")

            {
                string imgName = Guid.NewGuid()+imgFileUpload.PostedFile.FileName;
                //sets the image path           
                imgPath = "~/Upload/HRM/Doc/" + imgName;
                //then save it to the Folder  
                imgFileUpload.SaveAs(Server.MapPath(imgPath));


             
            }

            DataSet ds  = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "UPLOADFILE", "DOCINFB", gcod, title, remarks, "", "", "", "", "");
            if(ds == null || ds.Tables[0].Rows.Count==0)
            {
                return;
            }
            else
            {
                string docid = ds.Tables[0].Rows[0]["docid"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "UPLOADFILE", "DOCINFA", docid, refno, imgPath, "", "", "");
                msg = "Data Saved Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
              
            }
            this.getAllData();

        }

        protected void download_click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string filePath = ((Label)this.gvdoc.Rows[index].FindControl("lblimgpath")).Text.ToString();

            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                // Clear Rsponse reference  
                Response.Clear();
                // Add header by specifying file name  
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                // Add header for content length  
                Response.AddHeader("Content-Length", file.Length.ToString());
                // Specify content type  
                Response.ContentType = "text/plain";
                // Clearing flush  
                Response.Flush();
                // Transimiting file  
                Response.TransmitFile(file.FullName);
                Response.End();
            }
       
        }

        protected void btn_remove_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string msg = "";
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.gvdoc.Rows[index].FindControl("lblid")).Text.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "REMOVEDOc", id, "", "", "", "", "", "");
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

        protected void download_Click1(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string filePath = ((Label)this.gvdoc.Rows[index].FindControl("lblimgpath")).Text.ToString();
            string msg = "";

            FileInfo file = new FileInfo(Server.MapPath(filePath));
            if (file.Exists)
            {
                // Clear Rsponse reference  
                Response.Clear();
                // Add header by specifying file name  
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                // Add header for content length  
                Response.AddHeader("Content-Length", file.Length.ToString());
                // Specify content type  
                Response.ContentType = "text/plain";
                // Clearing flush  
                Response.Flush();
                // Transimiting file  
                Response.TransmitFile(file.FullName);
                Response.End();
            }
            else
            {
                msg = "Download Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtsName.Text = this.ddlDept.SelectedItem.Text.ToString();
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtsName.Text = this.ddlMonth.SelectedItem.Text.ToString();
        }
    }
}