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
namespace RealERPWEB.F_46_GrMgtInter
{
    public partial class UserLoginfrm : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");
                this.ShowUserInfo();
                ((Label)this.Master.FindControl("lblTitle")).Text = "USER LOGIN FORM ";
            }
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());


        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

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
            Session["tblUsrinfo"] = ds1.Tables[0];
            this.LoadGrid();


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
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;

            }
            // this.ShowData();

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
                //else
                //{
                //    //((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = false;
                //    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = false;
                //}
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
        protected void ibtnFindName_Click(object sender, EventArgs e)
        {
            this.ShowUserInfo();

        }
        private void LoadGrid()
        {

            this.gvUseForm.DataSource = (DataTable)Session["tblUsrinfo"];
            this.gvUseForm.DataBind();

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }


        protected void gvUseForm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUseForm.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void gvUseForm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvUseForm.EditIndex = -1;
            this.LoadGrid();
        }
        protected void gvUseForm_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string comcod = GetComeCode();
            string usrid = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvuserid")).Text.Trim();
            string usrsname = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtusrShorName")).Text.Trim();
            string usrfname = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtusrFullName")).Text.Trim();
            string usrdesig = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvDesig")).Text.Trim();
            string usrpass = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvpass")).Text.Trim();
            string usrrmrk = ((TextBox)gvUseForm.Rows[e.RowIndex].FindControl("txtgvgvrmrk")).Text.Trim();
            string active = (((CheckBox)gvUseForm.Rows[e.RowIndex].FindControl("chkActive")).Checked) ? "1" : "0";
            //if (usrpass.Length == 0)
            //    return;
            usrpass = ASTUtility.EncodePassword(usrpass);
            bool result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSORUPDATEUSR", usrid, usrsname,
                      usrfname, usrdesig, usrpass, usrrmrk, active, "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;

            }
         ((Label)this.Master.FindControl("lblmsg")).Text = "Successfully Updated";
            this.gvUseForm.EditIndex = -1;
            this.ShowUserInfo();

        }
        protected void gvUseForm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvUseForm.EditIndex = e.NewEditIndex;
            this.LoadGrid();
        }
        protected void lbtnUserId_Click(object sender, EventArgs e)
        {
            Session.Remove("tblusrper");
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            ((Label)this.Master.FindControl("lblmsg")).Text = "";
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvUseForm.Visible = false;
            this.MultiView1.ActiveViewIndex = 0;
            string comcod = this.GetComeCode();
            string usrid = Convert.ToString(((LinkButton)sender).Text.Trim());
            this.lblusrid.Text = usrid;
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            if (ds2.Tables[0].Rows.Count > 0)
                Session["tblusrper"] = HiddenSameData(ds2.Tables[0]);
            else
            {
                DataTable dt = ConstantInfo.WebObjTableGrpMgtInterface();
                Session["tblusrper"] = HiddenSameData(dt);

            }
            this.ShowPer();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string modulename = dt1.Rows[0]["modulename"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["modulename"].ToString() == modulename)
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                    dt1.Rows[j]["modulename"] = "";


                }

                else
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                }
            }



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
            this.chkShowall.Checked = false;
            this.MultiView1.ActiveViewIndex = -1;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.gvUseForm.Visible = true;


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
                index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                dt.Rows[index]["chkper"] = chkper;

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
            this.Session_update();
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            DataTable dt = (DataTable)Session["tblusrper"];
            bool result = false;
            result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSER", usrid, "",
                         "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Failed";
                return;

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string frmname = dt.Rows[i]["frmname"].ToString().Trim();
                string frmid = dt.Rows[i]["frmid"].ToString().Trim();
                string qrytype = dt.Rows[i]["qrytype"].ToString().Trim();
                string description = dt.Rows[i]["dscrption"].ToString().Trim();
                string chkper = dt.Rows[i]["chkper"].ToString().Trim();
                string modulename = dt.Rows[i]["modulename"].ToString().Trim();
                string entry = dt.Rows[i]["entry"].ToString().Trim();
                string printable = dt.Rows[i]["printable"].ToString().Trim();
                if (chkper == "True")
                {
                    result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSRPER", usrid, frmid, frmname,
                         qrytype, description, modulename, entry, printable, "", "", "", "", "", "", "");
                }
            }

           ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";

        }
        protected void chkShowall_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkShowall.Checked)
            {
                this.ShowAllPer();
            }
            else
            {
                this.usrSpcPer();

            }
        }
        private void ShowAllPer()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWMODWISEFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
            DataTable dt = ConstantInfo.WebObjTableGrpMgtInterface();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "frmid like '" + modname + "'";
            DataTable dt2 = dv.ToTable();
            DataTable dt1 = (DataTable)Session["tblusrper"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
                string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
                string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
                string entry = dt1.Rows[i]["entry"].ToString().Trim();
                string printable = dt1.Rows[i]["printable"].ToString().Trim();
                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt2.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;

                }

            }



            Session["tblusrper"] = this.HiddenSameData(dt2);

            //DataTable dt = ConstantInfo.WebObjTableGrpMgtInterface();
            //DataTable dt1 = (DataTable)Session["tblusrper"];

            //for (int i = 0; i < dt1.Rows.Count; i++)
            //{
            //    string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
            //    string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
            //    string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
            //    string confrmqry = frmname + qrytype;
            //    DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + confrmqry + "'");
            //    if (dr1.Length > 0)
            //    {
            //        dr1[0]["chkper"] = chkper;

            //    }

            //}

            //Session["tblusrper"] = HiddenSameData(dt);
            this.ShowPer();

        }
        private void usrSpcPer()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = HiddenSameData(ds4.Tables[0]);
            this.ShowPer();
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
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["chkper"] = "True";
                    dt.Rows[index]["entry"] = "True";
                    dt.Rows[index]["printable"] = "True";

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

                }

            }

            Session["tblusrper"] = dt;
            // this.ShowPer();

        }
    }
}
