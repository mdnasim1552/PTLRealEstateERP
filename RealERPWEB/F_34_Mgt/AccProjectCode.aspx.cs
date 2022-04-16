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
using RealERPLIB;
using RealERPRPT;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
namespace RealERPWEB.F_34_Mgt
{
    public partial class AccProjectCode : System.Web.UI.Page
    {
        ProcessAccess mgtData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.GeProjectMainCode();
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Code Information";
                //  this.ddlProjectList_SelectedIndexChanged(null, null);
                //chkNewProject.Checked = true;
                //this.chkNewProject_CheckedChanged(null, null);
                ////previois
                GetProjectDetailsCode();
            }
        }

        public string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        public string GetEmpID()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string Empid = (hst["empid"].ToString() == "") ? "%" : hst["empid"].ToString();
            return (Empid);

        }

        private void GeProjectMainCode()
        {


            string comcod = this.GetComeCode();
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROMAINCODE", filter, "", "", "", "", "", "", "", "");
            this.ddlMainCode.DataSource = ds1.Tables[0];
            this.ddlMainCode.DataTextField = "actdesc";
            this.ddlMainCode.DataValueField = "actcode";
            this.ddlMainCode.DataBind();
            this.GetProjectSubCode1();
            ds1.Dispose();

        }

        private void GetProjectSubCode1()
        {
            string comcod = this.GetComeCode();
            string ProMainCode = this.ddlMainCode.SelectedValue.ToString().Substring(0, 2);
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE1", ProMainCode, filter, "", "", "", "", "", "", "");
            this.ddlSub1.DataSource = ds1.Tables[0];
            this.ddlSub1.DataTextField = "actdesc";
            this.ddlSub1.DataValueField = "actcode";
            this.ddlSub1.DataBind();
            this.GetProjectSubCode2();
            ds1.Dispose();

        }

        private void GetProjectSubCode2()
        {
            string comcod = this.GetComeCode();
            string ProSubCode1 = this.ddlSub1.SelectedValue.ToString().Substring(0, 4);
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE2", ProSubCode1, filter, "", "", "", "", "", "", "");
            this.ddlSub2.DataSource = ds1.Tables[0];
            this.ddlSub2.DataTextField = "actdesc";
            this.ddlSub2.DataValueField = "actcode";
            this.ddlSub2.DataBind();
            this.GetProjectDetailsCode();
            ds1.Dispose();

        }

        private void GetProjectDetailsCode()
        {
            ViewState.Remove("tblprolist");
            string comcod = this.GetComeCode();
            string ProSubCode2 = this.ddlSub2.SelectedValue.ToString().Substring(0, 8);
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPRODETAILSCODE", ProSubCode2, filter, "", "", "", "", "", "", "");

            ViewState["tblprolist"] = ds1.Tables[0];
            ds1.Dispose();
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            DataTable dt = (DataTable)ViewState["tblprolist"];
            this.gvPrjCode.DataSource = dt;
            this.gvPrjCode.DataBind();
        }



        protected void chkNewProject_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.chkNewProject.Checked==true)
            //{
            //    this.ddlProjectList.Items.Clear(); 
            //    this.txtShortName.Text = "";
            //    this.txtProjectName.Text = "";
            //    this.txtProjectNameBN.Text = "";

            //    prvProjt.Visible = false;
            //}
            //else 
            //{
            //    prvProjt.Visible = true;

            //    GetProjectDetailsCode();
            //}

        }
        protected void imgbtnMainCode_Click(object sender, EventArgs e)
        {
            this.GeProjectMainCode();
        }
        protected void ingbtnSub1_Click(object sender, EventArgs e)
        {
            this.GetProjectSubCode1();

        }
        protected void imgbtnSub2_Click(object sender, EventArgs e)
        {
            this.GetProjectSubCode2();

        }
        protected void mgbtnPreDetails_Click(object sender, EventArgs e)
        {
            //if (!(this.chkNewProject.Checked))
            //    this.GetProjectDetailsCode();

        }
        protected void ddlMainCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectSubCode1();
        }
        protected void ddlSub1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectSubCode2();
        }

        protected void ddlSub2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectDetailsCode();

        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string SubCode2 = this.ddlSub2.SelectedValue.ToString().Trim().Substring(0, 8);
            string ProjectName = this.txtProjectName.Text;
            string ProjectNameBN = this.txtProjectNameBN.Text;
            string ShortName = this.txtShortName.Text.Trim();
            bool result = true;

            string pcode = "";
            if (ViewState["pcode"] != null && !ViewState["pcode"].Equals("-1"))
            {
                pcode = ViewState["pcode"].ToString() ?? "";

                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "UPDATEPROJECT", pcode, ProjectName, ShortName, userid, ProjectNameBN, "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                result = mgtData.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTPROJECT", SubCode2, ProjectName, ShortName, userid, ProjectNameBN, "", "", "", "", "", "", "", "", "", "");
            }
            if (result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                //this.txtProjectName.Text = "";
                //this.txtShortName.Text = "";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Sorry, Data Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //this.lblmsg.ForeColor
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }
    }
         
}