using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using AjaxControlToolkit;
namespace RealERPWEB.F_33_Doc
{
    public partial class ProImgUpload : System.Web.UI.Page
    {
        ProcessAccess UserData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");

                ((Label)this.Master.FindControl("lblTitle")).Text = "PROJECT IMAGE UPLOAD ";

                if (this.ddlPrjName.Items.Count == 0)
                {
                    this.GetProjectName();
                }
            }

        }
        private void GetProjectName()
        {
            Session.Remove("tblpro");
            string comcod = this.GetCompCode();
            DataSet ds1 = UserData.GetTransInfo(comcod, "SP_ENTRY_PRJ_INFO", "GETEXPRJNAME", "%", "", "", "", "", "", "", "", "");
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





    }
}