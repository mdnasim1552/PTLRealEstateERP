using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_34_Mgt
{
    public partial class UserCompPagePrivilegesPtl : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usrid = hst["usrid"].ToString();

                string adminUid = usrid.Substring(4, 3).ToString();
                if (adminUid == "001")
                {
                    ((Label)this.Master.FindControl("lblTitle")).Text = "Company Page Permission ";

                    ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                    ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                    this.ShowUserInfo();

                   
                }
                else
                {
                    Response.Redirect("~/ErrorHandling.aspx?Type=Fake User");
                }




            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            

        }
        protected void ibtnFindName_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowUserInfo();

        }
     
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }



        private void ShowUserInfo()
        {
            Session.Remove("tblUsrinfo");
            string comcod = GetComeCode();
            string SearcUser = "%" + this.txtSrcName.Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSER", SearcUser, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvUseForm.DataSource = null;
                this.gvUseForm.DataBind();
                return;
            }
            DataView dv = ds1.Tables[0].DefaultView;
            dv.RowFilter = string.Format("usrsname = 'ptl'");
            //dataGridView1.DataSource = dv;
            DataTable dt = dv.ToTable();
            Session["tblUsrinfo"] = dt;
            this.LoadGrid();


        }



        protected void ibtnFindName_Click(object sender, EventArgs e)
        {
            this.ShowUserInfo();

        }
        private void LoadGrid()
        {

            this.gvUseForm.DataSource = (DataTable)Session["tblUsrinfo"];
            this.gvUseForm.DataBind();

        }
       

         
        protected void lbtnUserId_Click(object sender, EventArgs e)
        {
            
            Session.Remove("tblusrper");

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvUseForm.Visible = false;
            this.MultiView1.ActiveViewIndex = 0;
            string comcod = this.GetComeCode();
            string usrid = comcod + "001";//Convert.ToString(((LinkButton)sender).Text.Trim());
            this.lblusrid.Text = usrid;
            ///-------------------------///////////
            this.lblId.Visible = true;
            this.txtuserid.Visible = true;
            DataTable tbl01 = (DataTable)Session["tblUsrinfo"];
            DataRow[] dr1 = tbl01.Select("usrid='" + usrid + "'");
            this.txtuserid.Text = dr1[0]["usrname"].ToString();

            string wdtype = this.ddlType.SelectedValue.ToString();

            ///-------------------------///////////
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "GET_MENU_MASTER_MENUPTL", wdtype, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            if (ds2.Tables[0].Rows.Count > 0)
                Session["tblusrper"] = this.HiddenSameData(ds2.Tables[0]);
            else
            {

                DataSet ds3 = User.GetTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "GET_MENU_MASTER_MENUPTL", wdtype, "", "", "", "", "", "", "", "");

                Session["tblusrper"] = this.HiddenSameData(ds3.Tables[0]);

            }

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

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            
            this.MultiView1.ActiveViewIndex = -1;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvUseForm.Visible = true;
            this.lblId.Visible = false;
            this.txtuserid.Visible = false;

            this.LoadGrid();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowPer();
        }
        private void Session_update()
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int index;
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                string chkper = (((CheckBox)gvPermission.Rows[i].FindControl("chkPermit")).Checked) ? "True" : "False";
                string chkEntry = (((CheckBox)gvPermission.Rows[i].FindControl("chkEntry")).Checked) ? "True" : "False";
                string chkPrint = (((CheckBox)gvPermission.Rows[i].FindControl("chkPrint")).Checked) ? "True" : "False";

                index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                dt.Rows[index]["chkper"] = chkper;
                dt.Rows[index]["entry"] = chkEntry;
                dt.Rows[index]["printable"] = chkPrint;

            }
            Session["tblusrper"] = dt;
        }

        protected void gvPermission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.Session_update();
            this.gvPermission.PageIndex = e.NewPageIndex;
            this.ShowPer();
        }
        protected void lbtnUpPer_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            this.Session_update();
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            string modname = "0";
            DataTable dt1 = (DataTable)Session["tblusrper"];

            bool result = false;
            //result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETECOMP_NAHID", usrid, modname,
            //            "", "", "", "", "", "", "", "", "", "", "", "", "");


            //if (!result)
            //{
            //    string msg = User.ErrorObject["Msg"].ToString();
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            //    return;
            //}



            DataView dv = new DataView();
            DataSet ds1 = new DataSet("ds1");
            dv = dt1.DefaultView;
            //dv.RowFilter = "chkper=True";
            dt1 = dv.ToTable();

            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";

            //  result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTCOMPPER", ds1, null, null, "", "", "", "", "", "", "", "", "",
            //"", "", "", "", "", "", "", "", "", "", "");

            result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_ACCESS_PRIVILEGES", "INSERTCOMPPERMISSION", ds1, null, null, modname, "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                string msg = "dd";//User.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;

                //((Label)this.Master.FindControl("lblmsg")).Text = User.ErrorObject["Msg"].ToString();
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                //return;
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

            try
            {
                int i, index;
                if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkAllfrm")).Checked)
                {

                    for (i = 0; i < this.gvPermission.Rows.Count; i++)
                    {
                        ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                        ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                        ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
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
                        index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                        dt.Rows[index]["chkper"] = "False";
                        dt.Rows[index]["entry"] = "False";
                        dt.Rows[index]["printable"] = "False";
                        dt.Rows[index]["delete"] = "False";
                    }

                }

                Session["tblusrper"] = dt;
                // this.ShowPer();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.ToString() + "');", true);
            }


        }

         
        protected void gvPermission_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            

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
                }

            }

        }
        protected void chkallView_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkallView")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "False";


                }

            }

            Session["tblusrper"] = dt;
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

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbtnUserId_Click(null, null);
        }
    }
}