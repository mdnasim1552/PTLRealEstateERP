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

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();


                this.getType();
                this.getDept();
                this.GetYearMonth();
                this.getAllData();
                this.datatype.SelectedIndex = 0;

            }
        }


        private void getAllData()
        {
            Session.Remove("alldata");
            string comcod = this.GetCompCode();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "GETALLDATA", "", "", "", "", "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                this.gvdoc.DataSource = null;
                this.gvdoc.DataBind();
                return;
            }
            Session["alldata"] = ds.Tables[0];
            this.gvdoc.DataSource = ds.Tables[0];
            this.gvdoc.DataBind();
        }

        protected void datatype_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            int index = this.datatype.SelectedIndex;

            DataView dv = new DataView();
            DataTable dt = (DataTable)Session["alldata"];
            if (dt.Rows.Count == 0)
            {
                return;
            }
            switch (index)
            {
                case 0:
                    dv = dt.DefaultView;
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;
                case 1:
                    dv = dt.DefaultView;
                    dv.RowFilter = "gcod='99901'";
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;
                case 2:
                    dv = dt.DefaultView;
                    dv.RowFilter = "gcod='99902'";
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;
                case 3:
                    dv = dt.DefaultView;
                    dv.RowFilter = "gcod='99903'";
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;
                case 4:
                    dv = dt.DefaultView;
                    dv.RowFilter = "gcod='99904'";
                    this.gvdoc.DataSource = dv;
                    this.gvdoc.DataBind();
                    break;
            }

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
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "initextedit();", true);
            string gcod = this.ddlType.SelectedValue.ToString().Substring(0, 5) ?? "";

            switch (gcod)
            {
                case "99901":
                    this.lbltitle.Text = "Title";
                    this.txtsName.Text = this.ddlType.SelectedItem.Text.ToString();
                    this.txtsName.Enabled = false;
                    this.pnlDept.Visible = false;
                    this.pnlMonth.Visible = false;
                    this.pnlTxt.Visible = true;
                    this.pnlFile.Visible = true;
                    this.pnlEditor.Visible = true;

                    break;

                case "99902":
                    this.lbltitle.Text = "Department";
                    this.txtsName.Text = "";
                    this.txtsName.Enabled = true;
                    this.pnlDept.Visible = false;
                    this.pnlMonth.Visible = false;
                    this.pnlTxt.Visible = true;
                    this.pnlFile.Visible = true;
                    break;
                    this.pnlEditor.Visible = true;

                case "99903":
                    this.lbltitle.Text = "Month";
                    this.txtsName.Text = this.ddlMonth.SelectedItem.Text.ToString();
                    this.pnlDept.Visible = false;
                    this.pnlMonth.Visible = true;
                    this.pnlTxt.Visible = false;
                    this.pnlFile.Visible = true;
                    this.pnlEditor.Visible = true;

                    break;



                case "99904":
                    this.lbltitle.Text = "Title";
                    this.txtsName.Text = "";
                    this.txtsName.Enabled = true;
                    this.pnlTxt.Visible = true;
                    this.pnlDept.Visible = false;
                    this.pnlMonth.Visible = false;
                    this.pnlFile.Visible = false;
                    this.pnlEditor.Visible = true;


                    break;
                default:
                    this.lbltitle.Text = "Title";
                    this.txtsName.Text = this.ddlType.SelectedItem.Text.ToString();
                    this.pnlTxt.Visible = true;
                    this.pnlFile.Visible = true;
                    this.pnlEditor.Visible = false;
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
            string title = this.txtsName.Text.ToString() ?? "";
            string remarks = this.txtDetails1.Text;

            string refno = this.ddlDept.SelectedValue.ToString();
            string gcod = this.ddlType.SelectedValue.ToString().Substring(0, 5) ?? "";
            string imgPath = "";
            string msg = "";
            string id = "";


            //validates the posted file before saving  
            if (imgFileUpload.HasFile)
            {
               
                string filePath = imgFileUpload.PostedFile.FileName;
                string filename1 = Path.GetFileName(filePath); // getting the file name of uploaded file  
                string ext = Path.GetExtension(filename1);

                if (ext == ".pdf")
                {

                    string imgName = Guid.NewGuid() + ext;
                    //sets the image path           
                    imgPath = "~/Upload/HRM/Doc/" + imgName;
                    //then save it to the Folder  
                    imgFileUpload.SaveAs(Server.MapPath(imgPath));
                }
                else
                {
                    string msgfail = "Please select pdf file only";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgfail + "');", true);
                    return;
                }
            }

            if (title.Length == 0)
            {
                string msgfail = "Please add Title";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msgfail + "');", true);
                return;
            }

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "UPLOADFILE", "DOCINFB", gcod, title, remarks, "", "", "", "", "");
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }
            else
            {
                string docid = ds.Tables[0].Rows[0]["docid"].ToString();

                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "GETDOCBYID", docid, refno, "", "", "", "", "");

                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                {
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "UPLOADFILE", "DOCINFA", docid, refno, imgPath, "", "", "");
                    msg = "Data Saved Successfully";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    this.refresh();
                }
                else
                {
                    DataTable dt2 = ds2.Tables[0];
                    string filePath = dt2.Rows[0]["imgpath"].ToString();

                    FileInfo file = new FileInfo(Server.MapPath(filePath));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "UPLOADFILE", "DOCINFA", docid, refno, imgPath, "", "", "");
                    if (result)
                    {
                        msg = "Data Saved Successfully";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

                        this.refresh();
                    }



                }





            }


        }

        private void refresh()
        {
   
            this.txtDetails1.Text = "";
            this.getAllData();
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


        protected void btn_remove_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            string msg = "";



            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;

            string filePath = ((Label)this.gvdoc.Rows[index].FindControl("lblimgpath")).Text.ToString();
            FileInfo file = new FileInfo(Server.MapPath(filePath));
            if (file.Exists)
            {
                file.Delete();
            }
            string id = ((Label)this.gvdoc.Rows[index].FindControl("lblid")).Text.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "REMOVEDOc", id, "", "", "", "", "", "");
            if (result)
            {


                msg = "Deleted Successfully";
                this.getAllData();
                this.datatype_SelectedIndexChanged(null, null);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);


            }
            else
            {

                msg = "Delete Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }

        }

        protected void btn_edit_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string id = ((Label)this.gvdoc.Rows[index].FindControl("lblid")).Text.ToString();
            string refno = ((Label)this.gvdoc.Rows[index].FindControl("lblgcod")).Text.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_DOC", "GETDOCBYID", id, refno, "", "", "", "", "");
            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
            {               
                return;
            }
            this.ddlType.SelectedValue = ds2.Tables[0].Rows[0]["gcod"].ToString();
            this.txtsName.Text = ds2.Tables[0].Rows[0]["title"].ToString();
            this.txtDetails1.Text = ds2.Tables[0].Rows[0]["remarks"].ToString();
        }
    }
}