using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPRDLC;
using RealERPLIB;
using RealERPRPT;
using AjaxControlToolkit;
using Microsoft.Reporting.WinForms;
using System.IO;
namespace RealERPWEB.F_33_Doc
{
    public partial class ProjImgUpload : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        public static int i, j;
        public static string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT IMAGE UPLOAD ";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                //this.btnShowimg_Click(null, null);

                //string qtype = this.Request.QueryString["app"].ToString();

                //if (qtype=="1")
                //{
                //    this.AsyncFileUpload1.Visible = false;
                //    this.imgLoader.Visible = false;
                //    this.btnShowimg.Visible = false;

                //}
                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

            }



        }
        private void GetProjectName()
        {
            Session.Remove("tblpro");
            string comcod = this.GetCompCode();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "GETEXPRJNAME", "%", "", "", "", "", "", "", "", "");
            this.ddlPrjName.DataTextField = "actdesc";
            this.ddlPrjName.DataValueField = "actcode";
            this.ddlPrjName.DataSource = ds1.Tables[0];
            this.ddlPrjName.DataBind();
            Session["tblpro"] = ds1.Tables[0];

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }


        protected void FileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            string comcod = this.GetCompCode();
            string date = Convert.ToDateTime(this.txtDate.Text).ToString("yyyyMMdd");
            string procode = this.ddlPrjName.SelectedValue.ToString(); //ASTUtility.Left(this.lblCurreqno1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurreqno1.Text.Trim().Substring(3, 2) + this.txtCurreqno2.Text.Trim();

            string filename = System.IO.Path.GetFileName(AsyncFileUpload1.FileName);
            // i = i + 1;     

            if (AsyncFileUpload1.HasFile)
            {

                Random rnd = new Random();
                string rndnumber = rnd.Next().ToString();
                string extension = Path.GetExtension(AsyncFileUpload1.PostedFile.FileName);
                AsyncFileUpload1.SaveAs(Server.MapPath("~/Upload/PROJECT/") + rndnumber + extension);

                //  Url = procode + extension;

                Url = rndnumber + extension;
            }
            //Url = Url.Substring(0,(Url.Length-1));

            bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJIMAGEUPLOAD", procode, date, Url, "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {


                //this.lblMesg.Text=" Successfully Updated ";
            }

        }
        protected void btnShowimg_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblimgPath");

            DataTable tbfilePath = new DataTable();
            tbfilePath.Columns.Add("filePath", Type.GetType("System.String"));
            tbfilePath.Columns.Add("filePath1", Type.GetType("System.String"));
            tbfilePath.Columns.Add("supinfo", Type.GetType("System.String"));
            tbfilePath.Columns.Add("procode", Type.GetType("System.String"));
            ViewState["tblimgPath"] = tbfilePath;

            DataTable tbl2 = (DataTable)ViewState["tblimgPath"];
            string comcod = this.GetCompCode();
            // string ssircode = this.ddlBestSupplier.SelectedValue.ToString();

            string procode = this.ddlPrjName.SelectedValue.ToString();//ASTUtility.Left(this.lblCurreqno1.Text.Trim(), 3) + ASTUtility.Right(this.txtCurMSRDate.Text.Trim(), 4) + this.lblCurreqno1.Text.Trim().Substring(3, 2) + this.txtCurreqno2.Text.Trim();

            DataSet ds = purData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "PROJECTIMGESHOW", procode, "", "", "", "", "", "", "", "");
            if (ds == null)
            {
                return;
            }

            DataTable tbl1 = ds.Tables[0];

            ListViewEmpAll.DataSource = tbl1;
            ListViewEmpAll.DataBind();
            ViewState["tblimgPath"] = tbl2;
            this.btnDelall.Visible = true;

            //string Url = "";
            //string supinfo = "";
            //int inc = 0;
            //for (int j = 0; j < tbl1.Rows.Count; j++)
            //{

            //    Url = "~/Upload/PROJECT/" + tbl1.Rows[j]["url"].ToString().Trim();
            //    supinfo = tbl1.Rows[j]["SIRDESC"].ToString().Trim();
            //    DataRow dr1 = tbl2.NewRow();
            //    dr1["filePath"] = tbl1.Rows[j]["url"].ToString().Trim();
            //    dr1["filePath1"] = Url;
            //    dr1["supinfo"] = supinfo;
            //    dr1["procode"] = procode;
            //    tbl2.Rows.Add(dr1);

            //}
            //string path = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath;
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{


            //    supinfo += "<div class='col-md-2 col-sm-2 col-xs-2'>" +
            //                            "<div class='product product-single'>" +
            //                                "<div class='product-thumb'>" +

            //                                    "<img src='" + path + dr["url"].ToString().Replace("~", string.Empty) + "' class='img-responsive img-fluid' alt=''>" +
            //                                "</div>" +

            //                                "<div class='product-body'>" +
            //            // " <h2 class='product-name'>" + dr["itemname"] + "</h2>" +
            //                                    " <h2 class='product-name'><a href='" + path + "DetailsView.aspx?Refid=" + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("")) + "'>" + dr["itemname"] + "</a></h2>" +
            //                                    //" <h4 class='product-price text-primary'>" + dr["sirdesc"] + "</h4>" + /*itemdesc*/
            //                                " </div>" +

            //                            " </div>" +
            //                          " </div>";


            //    inc++;
            //}

            //this.TrendingdataData.InnerHtml = supinfo;





        }
        protected void btnPDFRemove_Click(object sender, EventArgs e)
        {
            string filePath = Server.MapPath("~/Uploads/PDFs/");
            // System.IO.File.Delete(filePath + uploadedPDF);
        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                System.Web.UI.WebControls.Image imgname = (System.Web.UI.WebControls.Image)e.Item.FindControl("GetImg");
                Label imglink = (Label)e.Item.FindControl("ImgLink");
                string extension = Path.GetExtension(imglink.Text.ToString());
                switch (extension)
                {
                    case ".PNG":
                    case ".png":
                    case ".JPEG":
                    case ".JPG":
                    case ".jpg":
                    case ".jpeg":
                    case ".GIF":
                    case ".gif":
                        imgname.ImageUrl = imglink.Text.ToString();
                        break;
                    case ".PDF":
                    case ".pdf":
                        imgname.ImageUrl = "~/Images/pdf.png";
                        break;
                    case ".xls":
                    case ".xlsx":
                        imgname.ImageUrl = "~/Images/excel.svg";
                        break;
                    case ".doc":
                    case ".docx":
                        imgname.ImageUrl = "~/Images/word.png";
                        break;
                }
            }


        }

        protected void btnDelall_OnClick(object sender, EventArgs e)
        {

            string comcod = this.GetCompCode();
            //DataTable dt = (DataTable)ViewState["tblimages"];
            for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            {
                string procode = ((Label)this.ListViewEmpAll.Items[j].FindControl("procode")).Text.ToString();
                string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();
                if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
                {
                    //DataRow dr = dt.Rows[j];
                    //dr.Delete();
                    //DataSet ds1 = new DataSet("ds1");
                    //ds1.Tables.Add(dt);
                    //ds1.Tables[0].TableName = "tbl1";

                    bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "REMOVEPROEJCTIMG", procode, filesname, "", "", "", "", "", "", "", "", "", "", "", "");

                    if (result == true)
                    {
                        string filePath = Server.MapPath("~/");
                        System.IO.File.Delete(filePath + filesname.Replace("~", ""));
                        this.lblMesg.Text = " Files Removed ";
                        //this.ShowProjectFiles();
                    }
                }

            }

            //string comcod = this.GetCompCode();
            //for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            //{
            //    string procode = ((Label)this.ListViewEmpAll.Items[j].FindControl("lblprocode")).Text.ToString();
            //    string filesname = ((Label)this.ListViewEmpAll.Items[j].FindControl("filepath")).Text.ToString();
            //    if (((CheckBox) this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
            //    {
            //        bool result = purData.UpdateTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "REMOVEPROEJCTIMG", procode, filesname, "", "", "", "", "", "", "", "", "", "", "", "");

            //        if (result == true)
            //        {
            //            string filePath = Server.MapPath("~/Upload/");
            //            System.IO.File.Delete(filePath + filesname);

            //            this.lblMesg.Text=" Files Removed ";
            //        }


            //    }




            //}
            this.btnShowimg_Click(null, null);
        }
    }
}