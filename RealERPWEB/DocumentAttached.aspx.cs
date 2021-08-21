using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RealERPLIB;

using System.IO;
using AjaxControlToolkit;
namespace RealERPWEB
{
    public partial class DocumentAttached : System.Web.UI.Page
    {
        ProcessAccess PurData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "Documents Attached";

            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void btnShowimg_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();

            string refno = this.lblrefcode.Text;

            DataSet ds2 = PurData.GetTransInfoNew(comcod, "SP_ENTRY_XML_INFO_01", "XMLGETATTACHEDDOCS", null, null, null, refno);
            if (ds2 == null)
                return;

            ListViewEmpAll.DataSource = ds2.Tables[0];
            ListViewEmpAll.DataBind();
        }

        protected void ListViewEmpAll_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgname = (Image)e.Item.FindControl("GetImg");
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
            //    DataTable dt = (DataTable)Session["tblAttDocs"];
            //    string refno = this.ddlrefno.SelectedValue.ToString();

            //    string comcod = this.GetCompCode();
            //    for (int j = 0; j < this.ListViewEmpAll.Items.Count; j++)
            //    {

            //        string filesname = "Upload/Purchase/" + ((Label)this.ListViewEmpAll.Items[j].FindControl("ImgLink")).Text.ToString();

            //        if (((CheckBox)this.ListViewEmpAll.Items[j].FindControl("ChDel")).Checked == true)
            //        {
            //            string id = ((Label)this.ListViewEmpAll.Items[j].FindControl("id")).Text.ToString();




            //            DataSet ds1 = new DataSet("ds1");
            //            DataView dv1 = new DataView(dt);

            //            dv1.RowFilter = ("id<>" + id);
            //            ds1.Tables.Add(dv1.ToTable());
            //            ds1.Tables[0].TableName = "tbl1";

            //            bool resulta = PurData.UpdateXmlTransInfo(comcod, "SP_ENTRY_XML_INFO_01", "BILLFINALXMLATTACHEDDOCUS", ds1, null, null, refno);

            //            if (!resulta)
            //            {

            //                //return;
            //            }
            //            else
            //            {
            //                //string filePath = Server.MapPath("~/InteriorWEB/");
            //                //System.IO.File.Delete(filePath + filesname);
            //                Session["tblAttDocs"] = ds1.Tables[0];
            //                this.lblmsg.Text = " Files Removed ";

            //            }



            //        }
            //    }


        }



    }
}