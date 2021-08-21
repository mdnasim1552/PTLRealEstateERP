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
namespace RealERPWEB.F_99_Allinterface
{
    public partial class AddLandInfo : System.Web.UI.Page
    {
        ProcessAccess LandData = new ProcessAccess();

        ProcessAccess feaData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GET_LAND_INFO_DETAILS();
            }

            if (FileUpLoad1.HasFile)
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userid = hst["usrid"].ToString();
                string comcod = this.GetCompCode();
                string filename = System.IO.Path.GetFileName(FileUpLoad1.FileName);
                string Url = "";
                string posteddat = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string gcod = this.lgvGcod.Text.ToString();

                string extension = Path.GetExtension(FileUpLoad1.PostedFile.FileName);
                string random = ASTUtility.RandNumber(1, 99999).ToString();
                FileUpLoad1.SaveAs(Server.MapPath("~/Upload/Lands/") + gcod + "_" + random + extension);
                Url = "~/Upload/Lands/" + gcod + "_" + random + extension;

                DataTable tbl1 = (DataTable)ViewState["tbldata"];
                if (tbl1 == null)
                    return;
                if (tbl1.Rows.Count > 0)
                {


                    DataRow dr = tbl1.Select("gcod=" + gcod).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        dr["imageurl"] = Url;
                    }
                    // this.Data_Bind();
                }


                //   GET_LAND_INFO_DETAILS();
                this.Data_Bind();
            }

        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            return (hst["comcod"].ToString());
        }
        private void GET_LAND_INFO_DETAILS()
        {

            string landcode = this.Request.QueryString["landcode"].ToString();
            string comcod = this.GetCompCode();
            DataSet ds1 = feaData.GetTransInfo(comcod, "SP_LAND_INTERFACE", "GETLANDINFODETAILS", landcode, "", "", "", "", "", "", "", "");

            ViewState["tbldata"] = ds1.Tables[0];
            this.txNote.Text = ds1.Tables[1].Rows[0]["REMARKS"].ToString();
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable tbl1 = (DataTable)ViewState["tbldata"];


            this.gvPersonalInfo.DataSource = tbl1;
            this.gvPersonalInfo.DataBind();
        }

        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");

                LinkButton imgupload = (LinkButton)e.Row.FindControl("ReplaceThumbnail");
                HyperLink hyprrr = (HyperLink)e.Row.FindControl("hyprrr");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "02001" || code == "02003" || code == "02006" || code == "02009" || code == "02012")
                {
                    imgupload.Visible = true;
                    hyprrr.Visible = true;
                    //txtgvname.ReadOnly = true;

                }

            }
        }

        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string landcode = this.Request.QueryString["landcode"].ToString();

            string notes = this.txNote.Text.Trim();
            bool result = false;
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string code = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                Image imgurl = ((Image)this.gvPersonalInfo.Rows[i].FindControl("lblimageurl")) as Image;

                string url = imgurl.ImageUrl;

                result = feaData.UpdateTransInfo(comcod, "SP_LAND_INTERFACE", "INSERTUPDATELANDINFORMATION", landcode, "", code, gtype, Gvalue, url, notes, "", "", "", "", "", "", "", "");
            }

            string msg = "Updated";


            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);


            GET_LAND_INFO_DETAILS();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "Land INFORMATION";
                string eventdesc = "Update Sup Info";
                string eventdesc2 = this.Request.QueryString["custid"].ToString();
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }



        protected void ReplaceThumbnail_Click(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            this.lgvGcod.Text = ((Label)this.gvPersonalInfo.Rows[index].FindControl("lblgvItmCode")).Text.ToString();

            this.lblmodalhead.Text = "Image ";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }
        protected void txtgvVal_TextChanged(object sender, EventArgs e)
        {
            //DataTable tbl1 = (DataTable)Session["tbldata"];
            //TextBox txt = (TextBox)sender;
            //GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;
            //Label lblRs = (Label)gvRow.FindControl("lblgvItmCode");
            //TextBox lblTotalRs = (TextBox)gvRow.FindControl("txtgvVal");
            //string gcod = lblRs.Text;


            //DataRow dr = tbl1.Select("gcod=" + gcod).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
            //if (dr != null)
            //{
            //    dr["gdesc"] = lblTotalRs.Text.Trim();
            //}


        }
    }
}