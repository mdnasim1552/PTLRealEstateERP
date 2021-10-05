using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_22_Sal
{
    public partial class ProjectFileDetailsEntry : System.Web.UI.Page
    {
        ProcessAccess accData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project File Details Entry";

                this.GetProject();
                this.ShowProjFileInfo();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }

        private void GetProject()
        {
            string comcod = GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECT", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProject.DataTextField = "pactdesc";
            this.ddlProject.DataValueField = "pactcode";
            this.ddlProject.DataSource = ds1.Tables[0];
            this.ddlProject.DataBind();
        }
        private void ShowProjFileInfo()
        {
            string comcod = GetCompCode();
            DataSet ds1 = accData.GetTransInfo(comcod, "SP_ENTRY_SALSMGT", "GETPROJECTFILEINFO", "", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            Session["tblPrjFileInfo"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];
            this.gvProjFileDet.DataSource = dt;
            this.gvProjFileDet.DataBind();
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblPrjFileInfo"];
            string pactode = this.ddlProject.SelectedValue.ToString();
            string pactdesc = this.ddlProject.SelectedItem.Text.Trim();
            string txtFileNo = this.txtFileNo.Text;
            string txtLocation = this.txtLocation.Text;
            DataRow[] dr = dt.Select("pactcode='" + pactode + "'");
            if(dr.Length==0)
            {
                DataRow dr1 = dt.NewRow();
                dr1["pactcode"] = pactode;
                dr1["pactdesc"] = pactdesc;
                dr1["fileno"] = txtFileNo;
                dr1["location"] = txtLocation;
                dt.Rows.Add(dr1);
            }
            Session["tblPrjFileInfo"] = dt;
            this.Data_Bind();
        }
    }
}