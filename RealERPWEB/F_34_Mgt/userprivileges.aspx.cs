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
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_34_Mgt
{
    public partial class userprivileges : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                ////if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                ////    Response.Redirect("../AcceessError.aspx");
                ////DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                //////----------------udate-20150120---------
                ////((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "USER LOGIN FORM ";

                //((Label)this.Master.FindControl("lblmsg")).Visible = false;
                //((Label)this.Master.FindControl("lblmsg")).Visible = false;
                //this.ShowUserInfo();

                this.getListModulename();
                if (this.chkShowall.Checked)
                {

                    //   this.ShowAllData();

                }
                else
                {
                    this.ShowData();
                }

                ////this.ModuleVisible();
                //this.GetCompPermission();
            }
        }

        private void getListModulename()
        {

            string comcod = this.GetComeCode();
            ProcessAccess ulogin = new ProcessAccess();
            string usrid = this.Request.QueryString["Userid"].ToString();

            DataSet ds1 = new DataSet();

            if (this.chkShowall.Checked == true)
            {
                ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE_FOR_PERMISSION_USER", usrid, "", "", "", "", "", "", "", "");
            }
            else
            {
                ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE_FORUSER", usrid, "", "", "", "", "", "", "", "");
            }

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = ds1.Tables[0];
            this.ddlModuleName.DataBind();
            ViewState["tblmoduleName"] = ds1.Tables[0];
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPer();
        }
        protected void chkShowall_CheckedChanged(object sender, EventArgs e)
        {
            this.getListModulename();

            if (this.chkShowall.Checked)
            {

                this.ShowAllData();

            }
            else
            {
                this.ShowData();
            }
        }
        protected void ddlModuleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
            }
            else
            {
                this.ShowData();
            }
        }
        private void ShowAllData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.Request.QueryString["Userid"].ToString();
            string ddlType = (this.ddlType.SelectedValue.Trim() == "0" ? "0" : this.ddlType.SelectedValue.ToString());

            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "0" : this.ddlModuleName.SelectedValue.ToString());
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWPERMISSION_ITEMS_USERALL", ddlType, usrid, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);

            this.ShowPer();
        }

        private void ShowData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.Request.QueryString["Userid"].ToString();

            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "0" : this.ddlModuleName.SelectedValue.ToString());
            string ddlType = (this.ddlType.SelectedValue.Trim() == "0" ? "0" : this.ddlType.SelectedValue.ToString());
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWPERMISSION_ITEMS_USER", usrid, ddlType, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);

            this.ShowPer();
        }
        private DataTable HiddenSameData(DataTable dt1)
        {

            return dt1;

        }
        private void ShowPer()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            this.gvPermission.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvPermission.DataSource = (DataTable)Session["tblusrper"];
            this.gvPermission.DataBind();

        }



        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.Session_update();
            string comcod = this.GetComeCode();
            string usrid = this.Request.QueryString["Userid"].ToString();

            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "0" : this.ddlModuleName.SelectedValue.ToString());
            DataTable dt1 = (DataTable)Session["tblusrper"];

            bool result = false;
            result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSERMENU_NAHID", usrid, modname,
                        "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }




            DataView dv = new DataView();
            DataSet ds1 = new DataSet("ds1");
            dv = dt1.DefaultView;
            dv.RowFilter = "chkper=True";
            dt1 = dv.ToTable();

            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";

            //  result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTCOMPPER", ds1, null, null, "", "", "", "", "", "", "", "", "",
            //"", "", "", "", "", "", "", "", "", "", "");

            result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSERPER_NAHID", ds1, null, null, modname, usrid, "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            else
            {
                string msg = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }






        }
        protected void chkAllfrm_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkAllfrm")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";
                    dt.Rows[index]["entry"] = "True";
                    dt.Rows[index]["printable"] = "True";
                    dt.Rows[index]["delete"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";
                    dt.Rows[index]["entry"] = "False";
                    dt.Rows[index]["printable"] = "False";
                    dt.Rows[index]["delete"] = "False";

                }

            }

            Session["tblusrper"] = dt;


        }
        protected void chkallView_CheckedChanged(object sender, EventArgs e)
        {

            //DataTable dt = (DataTable)Session["tblusrper"];
            //int i, index;
            //if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallView")).Checked)
            //{

            //    for (i = 0; i < this.gvPermission.Rows.Count; i++)
            //    {
            //        ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
            //        index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
            //        dt.Rows[index]["chkper"] = "True";


            //    }


            //}

            //else
            //{
            //    for (i = 0; i < this.gvPermission.Rows.Count; i++)
            //    {
            //        ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
            //        index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
            //        dt.Rows[index]["chkper"] = "False";


            //    }

            //}

            //Session["tblusrper"] = dt;
        }
        protected void chkallEntry_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallEntry")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["entry"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["entry"] = "False";


                }

            }

            Session["tblusrper"] = dt;

        }
        protected void chkallPrint_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallPrint")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["printable"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["printable"] = "False";

                }

            }

            Session["tblusrper"] = dt;

        }
        protected void chkallDelete_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallDelete")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["delete"] = "True";

                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["delete"] = "False";

                }

            }

            Session["tblusrper"] = dt;

        }
        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                if (((CheckBox)this.gvPermission.Rows[i].FindControl("chkAll")).Checked)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkDelete")).Checked = true;
                }
                //else
                //{
                //    //((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                //}
            }

        }
        protected void gvPermission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_update();
            this.gvPermission.PageIndex = e.NewPageIndex;
            this.ShowPer();
        }
        private void Session_update()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            if (dt.Rows.Count < 0)
                return;
            int index;
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvPermission.Rows[i].FindControl("chkPermit")).Checked) ? "True" : "False";
                string chkEntry = (((CheckBox)gvPermission.Rows[i].FindControl("chkEntry")).Checked) ? "True" : "False";
                string chkPrint = (((CheckBox)gvPermission.Rows[i].FindControl("chkPrint")).Checked) ? "True" : "False";
                string chkDelete = (((CheckBox)gvPermission.Rows[i].FindControl("chkDelete")).Checked) ? "True" : "False";

                index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                dt.Rows[index]["chkper"] = chkper;
                dt.Rows[index]["entry"] = chkEntry;
                dt.Rows[index]["printable"] = chkPrint;
                dt.Rows[index]["delete"] = chkDelete;

            }
            Session["tblusrper"] = dt;
        }
        protected void gvPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;

            string frmid = ((Label)this.gvPermission.Rows[e.RowIndex].FindControl("lgvufrmid")).Text.Trim();

            bool result1 = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSER", usrid, frmid,
                            "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result1)
            {
                string msg = User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            else
            {
                string msg = "Delete Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }



            this.ShowData();

        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
            }
            else
            {
                this.ShowData();
            }
        }

        protected void gvPermission_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                CheckBox chper = (CheckBox)e.Row.FindControl("chkPermit");

                string menuparentid = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "menuparentid")).ToString();
                if (menuparentid == "0" || menuparentid == "")
                {
                    e.Row.Style.Add("color", "red");
                    chper.Visible = false;
                }

            }
        }

    }
}