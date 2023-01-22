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
    public partial class ShowAllDoc : System.Web.UI.Page
    {
        ProcessAccess DocData = new ProcessAccess("ASITDOC");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetCode();
                this.GetDocument();

            }
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetDocument()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            // string txtSProject = "%" + this.txtSrcDoc.Text + "%";
            string code = this.ddlDocumentType.SelectedValue.ToString().Substring(0, 2) + "%";
            DataSet ds1 = DocData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GETIDDESC", "%%", code, "", "", "", "", "", "", "");
            this.ddlDocumentName.DataTextField = "imgdesc";
            this.ddlDocumentName.DataValueField = "imgid";
            this.ddlDocumentName.DataSource = ds1.Tables[0];
            this.ddlDocumentName.DataBind();
            //this.ddlDocumentType_SelectedIndexChanged(null, null);
        }
        private void GetCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            // string txtSProject = "%" + this.txtSrcType.Text + "%";
            DataSet ds1 = DocData.GetTransInfo(comcod, "SP_ENTRY_DOC", "GETINFOCODE", "%%", "", "", "", "", "", "", "", "");
            this.ddlDocumentType.DataTextField = "gdesc";
            this.ddlDocumentType.DataValueField = "gcod";
            this.ddlDocumentType.DataSource = ds1.Tables[0];
            this.ddlDocumentType.DataBind();
        }
        protected void lnkPrint_Click(object sender, EventArgs e)
        {

        }


        private void ShowReport()
        {
            Session.Remove("tbAllDoc");
            string comcod = this.GetComCode();
            string docName = (this.ddlDocumentName.SelectedValue.ToString() == "000000000") ? "000000000" : this.ddlDocumentName.SelectedValue.ToString();
            string code = this.ddlDocumentType.SelectedValue.ToString().Substring(0, 2) + "%";
            DataSet ds2 = DocData.GetTransInfo(comcod, "SP_ENTRY_DOC", "SHOWDOC", docName, code, "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                this.gvDoc.DataSource = null;
                this.gvDoc.DataBind();
                return;
            }
            Session["tbAllDoc"] = HiddenSameData(ds2.Tables[0]);
            //DataTable dt = ds2.Tables[0];
            this.Data_Bind();



        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string gcod = dt1.Rows[0]["gcod"].ToString();
            string gcod1 = dt1.Rows[0]["gcod1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod1"].ToString() == gcod1)
                {
                    dt1.Rows[j]["gdesc1"] = "";
                }
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                {
                    dt1.Rows[j]["gdesc"] = "";
                }

                gcod = dt1.Rows[j]["gcod"].ToString();
                gcod1 = dt1.Rows[j]["gcod1"].ToString();
            }

            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)Session["tbAllDoc"];
            this.gvDoc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvDoc.DataSource = dt;
            this.gvDoc.DataBind();

        }


        protected void lnkDownload_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string savelocation = Server.MapPath("Docs") + @"\";
                string[] filePaths = Directory.GetFiles(savelocation);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);
                //string fileName = string.Empty;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.gvDoc.Rows[index];
                string ImgID = this.gvDoc.DataKeys[index].Value.ToString();

                DataTable dt = (DataTable)Session["tbAllDoc"];
                DataRow[] dr = dt.Select(" id='" + ImgID + "' ");

                string fileName = dr[0]["filename"].ToString();
                byte[] documentBinary = (byte[])dr[0]["data"];
                FileStream fStream = new FileStream(Server.MapPath("Docs") + @"\" + fileName, FileMode.Create);
                fStream.Write(documentBinary, 0, documentBinary.Length);
                fStream.Close();
                fStream.Dispose();

                Response.Redirect(@"Docs\" + fileName);

                //lbljavascript.Text = @"<script>window.open('~/F_99_Doc/Docs/" + fileName + "', target='_blank');</script>";


            }
        }
        //protected void imgbtnFindDoc_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.GetDocument();
        //}
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowReport();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        //protected void imgbtnFindType_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.GetCode();
        //}
        protected void ddlDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDocument();
        }
    }
}