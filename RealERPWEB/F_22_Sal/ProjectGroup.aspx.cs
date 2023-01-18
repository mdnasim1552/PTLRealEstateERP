using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
namespace RealERPWEB.F_22_Sal
{
    public partial class ProjectGroup : System.Web.UI.Page
    {
        ProcessAccess _access = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetProjectGroup();
                this.GetProject();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetProjectGroup()
        {
            string comcod = this.GetCompCode();
            DataSet ds = _access.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "LOADPROJECTGROUP");
            this.ddlprjgroup.DataTextField = "gdesc";
            this.ddlprjgroup.DataValueField = "gcod";
            this.ddlprjgroup.DataSource = ds.Tables[0];
            this.ddlprjgroup.DataBind();
            this.ddlprjgroup_SelectedIndexChanged(null, null);

        }

        private void GetProject()
        {
            string comcod = this.GetCompCode();

            string prjgrp = ddlprjgroup.SelectedValue.ToString();
            prjgrp = ASTUtility.Left(prjgrp, 2) == "78" ? "length" : "";
            DataSet ds = _access.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "LOADPROJECT", prjgrp);
            this.DropCheck1.DataTextField = "actdesc";
            this.DropCheck1.DataValueField = "actcode";
            this.DropCheck1.DataSource = ds.Tables[0];
            this.DropCheck1.DataBind();
            Session["tblproj"] = ds.Tables[0];
        }

        private void LoadProjectGrpWise()
        {
            string comcod = this.GetCompCode();
            string prjgrp = this.ddlprjgroup.SelectedValue;
            DataSet ds = _access.GetTransInfo(comcod, "SP_ENTRY_SALSMGT02", "GETPROJECT", prjgrp);
            Session["tblproject"] = ds.Tables[0];
            Data_Bind();
        }

        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblproject"];
            this.gvProject.DataSource = dt;
            this.gvProject.DataBind();
        }

        protected void lnkOk_OnClick(object sender, EventArgs e)
        {
            this.LoadProjectGrpWise();
        }

        protected void lnk_add_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblproject"];
            DataTable dt1 = (DataTable)Session["tblproj"];
            string actcode = "";
            string actdesc = "";

            string[] gp = this.DropCheck1.Text.Trim().Split(',');
            if (gp.Length > 0)
            {

                foreach (string s1 in gp)
                {
                    actcode = s1.ToString();
                    if (dt.Select("actcode='" + actcode + "'").Length == 0)
                    {
                        actdesc = dt1.Select("actcode='" + actcode + "'")[0]["actdesc"].ToString();
                        DataRow dr1 = dt.NewRow();
                        dr1["actcode"] = actcode;
                        dr1["actdesc"] = actdesc;
                        dt.Rows.Add(dr1);
                        Session["tblproject"] = dt;
                        Data_Bind();
                    }


                }


            }
        }

        protected void lnk_update_OnClick(object sender, EventArgs e)
        {
            string comcod = this.GetCompCode();
            DataTable dt = (DataTable)Session["tblproject"];
            DataSet ds1 = new DataSet("ds1");
            ds1.Merge(dt);
            ds1.Tables[0].TableName = "tbl1";
            string prjgrp = this.ddlprjgroup.SelectedValue;
            bool result = _access.UpdateXmlTransInfo(comcod, "SP_ENTRY_SALSMGT", "UPDATEALLPRJ", ds1, null, null, prjgrp);

            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Successfully";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Update Failed";
            }



        }

        protected void lnkdel_OnClick(object sender, EventArgs e)
        {
            int Rowindex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            string actcode = ((Label)this.gvProject.Rows[Rowindex].FindControl("lblactcode")).Text.Trim().ToString();//Another Way
            string comcod = this.GetCompCode();

            bool result = _access.UpdateTransInfo(comcod, "SP_ENTRY_SALSMGT", "DELETEPRJ", actcode, "", "", "");
            if (result == true)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Successfully";
            }

            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Delete Failed";
            }

            this.LoadProjectGrpWise();

        }
        protected void ddlprjgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProject();
        }
    }
}