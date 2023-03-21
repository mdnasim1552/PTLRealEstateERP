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
                
                ((Label)this.Master.FindControl("lblTitle")).Text = "Project Code Information";
                //  this.ddlProjectList_SelectedIndexChanged(null, null);
                //chkNewProject.Checked = true;
                //this.chkNewProject_CheckedChanged(null, null);
                ////previois
                ///

                this.GeProjectMainCodeFilter();
                this.GeProjectMainCode();
                //GetProjectDetailsCode();
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

        private void GeProjectMainCodeFilter()
        {
            string comcod = this.GetComeCode();
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROMAINCODE", filter, "", "", "", "", "", "", "", "");             
            this.ddlMainFilter.DataSource = ds1.Tables[0];
            this.ddlMainFilter.DataTextField = "actdesc";
            this.ddlMainFilter.DataValueField = "actcode";
            this.ddlMainFilter.DataBind();
            this.GetProjectSubCode1Filter();
            ds1.Dispose();
        }

        private void GetProjectSubCode1Filter()
        {
            string comcod = this.GetComeCode();
            string ProMainCode = this.ddlMainFilter.SelectedValue.ToString().Substring(0, 2);
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE1", ProMainCode, filter, "", "", "", "", "", "", ""); 
            this.ddlSubFilter.DataSource = ds1.Tables[0];
            this.ddlSubFilter.DataTextField = "actdesc";
            this.ddlSubFilter.DataValueField = "actcode";
            this.ddlSubFilter.DataBind();
            this.GetProjectSubCode2Filter();
            ds1.Dispose();

        }
        private void GetProjectSubCode2Filter()
        {
            string comcod = this.GetComeCode();
            string ProSubCode1 = this.ddlSubFilter.SelectedValue.ToString().Substring(0, 4);
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROSUBCODE2", ProSubCode1, filter, "", "", "", "", "", "", "");
             
            this.ddlSubDetailsFilter.DataSource = ds1.Tables[0];
            this.ddlSubDetailsFilter.DataTextField = "actdesc";
            this.ddlSubDetailsFilter.DataValueField = "actcode";
            this.ddlSubDetailsFilter.DataBind(); 

            this.GetProjectDetailsCode();
            ds1.Dispose();

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
            string ProSubCode2 = this.ddlSubDetailsFilter.SelectedValue.ToString().Substring(0, 8);
            string filter = "%%";
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPRODETAILSCODE", ProSubCode2, filter, "", "", "", "", "", "", "");            
            ViewState["tblprolist"] = ds1.Tables[0];
            Data_bind();          
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#newCodeBook", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#newCodeBook').hide();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "newCodebookOpen();", true);
        }
        protected void ddlSub1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState.Remove("pcode");
            this.GetProjectSubCode2();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#newCodeBook", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#newCodeBook').hide();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "newCodebookOpen();", true);
        }

        protected void ddlSub2_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#newCodeBook", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#newCodeBook').hide();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "newCodebookOpen();", true);


        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string Message;
           
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
                Message = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#newCodeBook", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#newCodeBook').hide();", true);
                GetProjectDetailsCode();
            }
            else
            {
                Message = "Sorry, Data Updated Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#newCodeBook", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#newCodeBook').hide();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "newCodebookOpen();", true);


            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {


        }


        protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlProjectList.Items.Count == 0)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            Session.Remove("EmployeeList");
            string procode = this.ddlProjectList.SelectedValue.ToString();
            this.txtProjectName.Text = this.ddlProjectList.SelectedItem.Text.Trim().ToString().Substring(13);
            this.txtShortName.Text = (((DataTable)ViewState["tblprolist"]).Select("actcode='" + procode + "'"))[0]["acttdesc"].ToString();
            string name = txtShortName.Text.ToString();
            this.lblprjname.Text ="User Permission Project Wise: "+ txtShortName.Text.ToString();


            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPRODETAILSCODEIND", procode, "", "", "", "", "", "", "", "");






            this.txtProjectNameBN.Text = ds1.Tables[0].Rows[0]["actdescbn"].ToString();
            isLoadDataEmployeeGv(procode);

            this.lnkBtnPrjDetails.NavigateUrl = "~/F_04_Bgd/PrjInformation?Type=Report&prjcode=";
        }

        private void isLoadDataEmployeeGv(string pCode)
        {

            this.gvEmployeeInfo.DataSource = null;
            this.gvEmployeeInfo.DataBind();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            DataSet ds1 = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERINFLIST", pCode);
            if (ds1.Tables[0].Rows.Count != 0)

                Session["EmployeeList"] = ds1.Tables[0];
            this.Data_bind();
            //ds1.Dispose();
        }



        private void Data_bind()
        {
             

            DataTable dtp = (DataTable)ViewState["tblprolist"];           
            this.gvPrjCode.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPrjCode.DataSource = dtp;
            this.gvPrjCode.DataBind();

            DataTable dt = (DataTable)Session["EmployeeList"];          
            this.gvEmployeeInfo.DataSource = dt;
            this.gvEmployeeInfo.DataBind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.Data_bind();
        }
        protected void btnSaveEmp_Click(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["EmployeeList"];

            for (int i = 0; i < this.gvEmployeeInfo.Rows.Count; i++)
            {
                string userid = ((Label)gvEmployeeInfo.Rows[i].FindControl("lblgvUserId")).Text.ToString();
                string name = ((Label)gvEmployeeInfo.Rows[i].FindControl("lblgvName")).Text.ToString(); ;
                string desig = ((Label)gvEmployeeInfo.Rows[i].FindControl("lblgvDesig")).Text.ToString();
                string usrprm = ((Label)gvEmployeeInfo.Rows[i].FindControl("lblgvPerm")).Text.ToString();
                CheckBox chk = ((CheckBox)gvEmployeeInfo.Rows[i].FindControl("chkPermission"));
                string checkstatus = (chk.Checked == true) ? "True" : "False";
                //string checkstatu1s = (((CheckBox)gvEmployeeInfo.Rows[i].FindControl("CheckPermission")).Checked) ? "True" : "False";

                dt1.Rows[i]["usrid"] = userid;
                dt1.Rows[i]["usrname"] = name;
                dt1.Rows[i]["usrdesig"] = desig;
                dt1.Rows[i]["permission"] = checkstatus;


            }
            Session["EmployeeList"] = dt1;


            if (updateProjectPermission())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Project Permission Updated Successfully!!');", true);
            }

        }



        protected void lnkBtnShow_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string procode = ((Label)this.gvPrjCode.Rows[index].FindControl("Label5")).Text.ToString();

            this.isLoadDataEmployeeGv(procode);
            this.HiddednPactcode.Value = procode;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);
        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            if (((CheckBox)this.gvEmployeeInfo.HeaderRow.FindControl("chkall")).Checked)
            {
                for (i = 0; i < this.gvEmployeeInfo.Rows.Count; i++)
                {
                    ((CheckBox)this.gvEmployeeInfo.Rows[i].FindControl("chkPermission")).Checked = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

                }
            }
            else
            {
                for (i = 0; i < this.gvEmployeeInfo.Rows.Count; i++)
                {
                    //((CheckBox)this.gvEmployeeInfo.Rows[i].FindControl("chkPermission")).Enabled == true
                    if (((Label)gvEmployeeInfo.Rows[i].FindControl("lblgvPerm")).Text.ToString() == "True")
                    {
                        ((CheckBox)this.gvEmployeeInfo.Rows[i].FindControl("chkPermission")).Checked = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);

                        //this.lblgvdeptandemployeeemp_Click(null, null);
                    }
                }
            }
        }


        private bool updateProjectPermission()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string invtype = hst["invtype"].ToString();
            DataTable dt1 = (DataTable)Session["EmployeeList"];
            DataView dv = dt1.DefaultView;
            dv.RowFilter = "permission=True";
            DataSet ds1 = new DataSet("ds1");
            ds1.Tables.Add(dv.ToTable());
            ds1.Tables[0].TableName = "tbl1";
            string procode1 = this.HiddednPactcode.Value.ToString();
            //string procode =(invtype=="15") ? "15" + ASTUtility.Right(procode1, 10) :  "16" + ASTUtility.Right(procode1, 10);
            string procode =  "16" + ASTUtility.Right(procode1, 10);

            string comcod = this.GetComeCode();
            string ss = ds1.GetXml();
            bool result = mgtData.UpdateXmlTransInfo(comcod, "[SP_ENTRY_MGT]", "UPDATEUSERINF", ds1, null, null, procode, "");
            if (!result)
            {
                string msg = mgtData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }

            return true;
        }

        protected void gvPrjCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvPrjCode.PageIndex = e.NewPageIndex;
            this.Data_bind();

        }



        protected void lnknewcodebook_Click(object sender, EventArgs e)
        {
            ViewState.Remove("pcode");
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "newCodebookOpen();", true);
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            ViewState.Remove("pcode");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string comcod = this.GetComeCode();
            string procode = ((Label)this.gvPrjCode.Rows[index].FindControl("Label5")).Text.ToString();
            DataSet ds = mgtData.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETPROJECTBYID", procode, "", "", "", "", "", "", "", "");
            DataTable dt = ds.Tables[0];
            ViewState["pcode"] = dt.Rows[0]["actcode"].ToString(); ;
            this.txtProjectName.Text = dt.Rows[0]["actdesc"].ToString();
            this.txtProjectNameBN.Text = dt.Rows[0]["actdescbn"].ToString();
            this.txtShortName.Text = dt.Rows[0]["acttdesc"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "newCodebookOpen();", true);
        }


        protected void lnkdelete_Click(object sender, EventArgs e)
        {
            string pid = ViewState["projectId"].ToString();


        }

        protected void deleteModal_Click(object sender, EventArgs e)
        {
            ViewState.Remove("projectId");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string procode = ((Label)this.gvPrjCode.Rows[index].FindControl("Label5")).Text.ToString();

            ViewState["projectId"] = procode;

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openDeleteModal();", true);
        }

        protected void gvPrjCode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlink = (HyperLink)e.Row.FindControl("lnkBtnPrjDetails");
                Label lblREFNO = (Label)e.Row.FindControl("Label5");
                string actcode = lblREFNO.Text; 
                hlink.NavigateUrl = "~/F_04_Bgd/PrjInformation?Type=Report&prjcode=16"+ ASTUtility.Right(actcode,10).ToString();
            }

        }

        protected void ddlMainFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectSubCode1Filter();
        }

        protected void ddlSubFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectSubCode2Filter();
          

        }

        protected void ddlSubDetailsFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectDetailsCode();
        }
    }
}