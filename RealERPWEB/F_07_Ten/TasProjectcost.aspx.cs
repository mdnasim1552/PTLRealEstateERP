using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_07_Ten
{
    public partial class TasProjectcost : System.Web.UI.Page
    {
        ProcessAccess purData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "MARGIN CODEBOOK INFORMATION";

                this.GetProjectName();
            }
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string txtSProject = this.txtSrcProject.Text.Trim() + "%";
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "GETPROJECTNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "prjdesc1";
            this.ddlProjectName.DataValueField = "prjcod";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();



        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }


        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(14);
                this.ddlProjectName.Visible = false;
                this.lblProjectdesc.Visible = true;
                this.ShowView();
                return;

            }

            this.lbtnOk.Text = "Ok";
            this.ddlProjectName.Visible = true;
            this.lblProjectdesc.Visible = false;
            this.MultiView1.ActiveViewIndex = -1;
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.gvprjcost.DataSource = null;
            this.gvprjcost.DataBind();
        }

        private void ShowView()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Margin":
                    this.MultiView1.ActiveViewIndex = 0;
                    this.ShowMargin();
                    break;




            }
        }

        private void ShowMargin()
        {
            Session.Remove("tblprjcost");
            string comcod = this.GetCompCode();
            string ProjectName = this.ddlProjectName.SelectedValue.ToString();
            DataSet ds1 = purData.GetTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "PROJECTMARGIN", ProjectName, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {

                this.gvprjcost.DataSource = null;
                this.gvprjcost.DataBind();
                return;

            }
            Session["tblprjcost"] = ds1.Tables[0];
            this.Data_Bind();

        }
        private void Data_Bind()
        {
            string Type = this.Request.QueryString["Type"].ToString().Trim();
            switch (Type)
            {
                case "Margin":
                    this.gvprjcost.DataSource = (DataTable)Session["tblprjcost"];
                    this.gvprjcost.DataBind();
                    break;

            }

        }


        protected void lUpdatInfo_Click(object sender, EventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                return;
            }

            string comcod = this.GetCompCode();
            string pactcode = this.ddlProjectName.SelectedValue.ToString();

            for (int i = 0; i < this.gvprjcost.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvprjcost.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvprjcost.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Gvalue = ((TextBox)this.gvprjcost.Rows[i].FindControl("txtgvVal")).Text.Trim();


                purData.UpdateTransInfo(comcod, "SP_TAS_ENTRY_PRJ_ANALYSIS", "INSERTORUPPRJMARGIN", pactcode, Gcode, gtype, Gvalue, "", "", "", "", "", "", "", "", "", "", "");

            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
        }
    }
}