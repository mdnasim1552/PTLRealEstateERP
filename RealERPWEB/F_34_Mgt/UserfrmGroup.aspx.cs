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
using System.IO;
namespace RealERPWEB.F_34_Mgt
{
    public partial class UserfrmGroup : System.Web.UI.Page
    {
        ProcessAccess User = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../AcceessError.aspx");

                //----------------udate-20150120---------
                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                ((Label)this.Master.FindControl("lblTitle")).Text = "GROUP USER ";

                this.lblMsg1.Visible = false;
                this.lblMsg.Visible = false;
                this.ShowUserInfo();




                this.GetCompPermission();

            }
            if (fileuploaddropzone.HasFile)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];



                Upload = System.IO.Path.GetFileName(fileuploaddropzone.PostedFile.FileName);
                string savelocation = Server.MapPath("~") + "\\Image1" + "\\" + Upload;
                string filepath = savelocation;
                fileuploaddropzone.PostedFile.SaveAs(savelocation);
                //this.UserImg.ImageUrl = "~/Image1/" + Upload;
                image_file = fileuploaddropzone.PostedFile.InputStream;
                size = fileuploaddropzone.PostedFile.ContentLength;
                Session["i"] = image_file;
                Session["s"] = size;

                string savelocation1 = Server.MapPath("~") + "\\Image1";
                string[] filePaths = Directory.GetFiles(savelocation1);
                foreach (string filePath in filePaths)
                    File.Delete(filePath);

                byte[] photo = new byte[0];
                byte[] signature = new byte[0];

                image_file = (Stream)Session["i"];
                size = Convert.ToInt32(Session["s"]);
                BinaryReader br = new BinaryReader(image_file);
                photo = br.ReadBytes(size);


                var list2 = (List<RealEntity.C_34_Mgt.GetCompany>)ViewState["tblapend"];

                foreach (RealEntity.C_34_Mgt.GetCompany lis in list2)
                {

                    string comcod = lis.comcod.ToString();
                    string UserId = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
                    DataSet ds3 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERID", UserId, "", "", "", "", "", "", "", "");
                    bool updatPhoto;
                    ProcessAccess UserData01 = new ProcessAccess("ASTREALERPMSG");
                    ((Panel)this.Master.FindControl("AlertArea")).Visible = true;
                    if (ds3.Tables[0].Rows.Count == 0)
                    {
                        updatPhoto = UserData01.InsertUserPhoto(comcod, UserId, photo, signature);
                        if (updatPhoto)

                            ((Label)this.Master.FindControl("lblmsg")).Text = "Your Porofile Picture Updated Successfully";

                        else
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Profile Picture Updated failed";
                    }

                    else
                    {
                        updatPhoto = UserData01.UpdateUserPhoto(comcod, UserId, photo, signature);

                        if (updatPhoto)
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Your Porofile Picture Updated Successfully";
                        else
                            ((Label)this.Master.FindControl("lblmsg")).Text = "Profile Picture Updated failed";


                    }
                }
            }
        }

        private void GetCompPermission()
        {
            string comcod = this.GetComeCode();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string usrid = hst["usrid"].ToString();


            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORMASIT", usrid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
            {

                return;
            }


            Session["tblcompper"] = ds2.Tables[0];

        }


        protected void Page_PreInit(object sender, EventArgs e)
        {


        }
        protected void ibtnFindName_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowUserInfo();

        }
        private void getListModulename()
        {

            string comcod = this.ddlCompany.SelectedValue.ToString();
            ProcessAccess ulogin = new ProcessAccess();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULEUPDATE", "", "", "", "", "", "", "", "", "");

            this.ddlModuleName.DataTextField = "modulename";
            this.ddlModuleName.DataValueField = "moduleid";
            this.ddlModuleName.DataSource = ds1.Tables[0];
            this.ddlModuleName.DataBind();
            //ViewState["tblmoduleName"] = ds51.Tables[0];
        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        private void ShowUserInfo()
        {
            ViewState.Remove("tblUsrinfo");
            string comcod = GetComeCode();
            string SearcUser = "%" + this.txtSrcName.Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "GET_GROUP_WISE_USER_INFO", SearcUser, "", "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvUseForm.DataSource = null;
                this.gvUseForm.DataBind();
                return;
            }
            ViewState["tblUsrinfo"] = ds1.Tables[0];
            this.LoadGrid();


        }



        protected void ibtnFindName_Click(object sender, EventArgs e)
        {
            this.ShowUserInfo();

        }
        private void LoadGrid()
        {

            this.gvUseForm.DataSource = (DataTable)ViewState["tblUsrinfo"];
            this.gvUseForm.DataBind();

        }
        protected void gvUseForm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUseForm.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        protected void ibtnSrchCentr_Click(object sender, EventArgs e)
        {

            int rowindex = (int)ViewState["gindex"];
            //string actcode = ((DataTable)Session["CodeBook"]).Rows[rowindex]["actcode"].ToString();
            DropDownList ddl3 = (DropDownList)this.gvUseForm.Rows[rowindex].FindControl("ddlempid");
            string SearchProject = "%" + ((TextBox)gvUseForm.Rows[rowindex].FindControl("txtSrCentrid")).Text.Trim() + "%";
            DataSet ds1 = User.GetTransInfo("", "SP_ENTRY_CODEBOOK", "GETEMPTIDNAME", SearchProject, "", "", "", "", "", "", "", "");
            ddl3.DataTextField = "empname";
            ddl3.DataValueField = "empid";
            ddl3.DataSource = ds1;
            ddl3.DataBind();
        }
        protected void lbtnUserId_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string grpuser = ((LinkButton)gvUseForm.Rows[index].FindControl("lbtnUserId")).Text.Trim().ToString();

            DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_GRPUSER_MGT", "GETGRUSERCOMPANY", grpuser, "", "", "", "", "", "", "", "");
            var list = ds3.Tables[0].DataTableToList<RealEntity.C_34_Mgt.GetCompany>();
            this.ddlCompany.DataTextField = "comname";
            this.ddlCompany.DataValueField = "comcod";
            this.ddlCompany.DataSource = list;
            this.ddlCompany.DataBind();
            string usrid = Convert.ToString(((LinkButton)sender).Text.Trim());
            this.lblusrid.Text = usrid;
            this.lblId.Visible = true;
            this.txtuserid.Visible = true;
            DataTable tbl01 = (DataTable)ViewState["tblUsrinfo"];
            DataRow[] dr1 = tbl01.Select("grpusrid='" + grpuser + "'");
            this.txtuserid.Text = dr1[0]["usrname"].ToString();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        public void GetPermmission()
        {
            this.getListModulename();
            this.ddlModuleName.SelectedValue = "AA";
            Session.Remove("tblusrper");
            this.lblMsg.Text = "";
            this.lblMsg1.Text = "";
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvUseForm.Visible = false;
            this.userDetPan.Visible = false;
            this.MultiView1.ActiveViewIndex = 0;
            string comcod = this.ddlCompany.SelectedValue.ToString();

            string usrid = comcod + ASTUtility.Right(lblusrid.Text.Trim().ToString(), 3).ToString();
            this.lblusrid.Text = comcod + ASTUtility.Right(lblusrid.Text.Trim().ToString(), 3).ToString(); ;
            ///-------------------------///////////


            ///-------------------------///////////
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.lblusrid.Text = usrid;
                Session["tblusrper"] = this.HiddenSameData(ds2.Tables[0]);
            }
            else
            {



                DataTable dt = (DataTable)Session["tblcompper"];
                Session["tblusrper"] = this.HiddenSameData(dt);

            }
            this.ShowPer();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {


            //return dt1;
            if (dt1.Rows.Count == 0)
                return dt1;

            string modulename = dt1.Rows[0]["modulename"].ToString();
            string frmid1 = dt1.Rows[0]["frmid1"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["frmid1"].ToString() == frmid1)
                {
                    dt1.Rows[j]["frmdesc"] = "";

                }


                if (dt1.Rows[j]["modulename"].ToString() == modulename)
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                    dt1.Rows[j]["modulename"] = "";
                }
                else
                {
                    modulename = dt1.Rows[j]["modulename"].ToString();
                }



                frmid1 = dt1.Rows[j]["frmid1"].ToString();


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
                string delete = (((CheckBox)gvPermission.Rows[i].FindControl("delete")).Checked) ? "True" : "False";

                index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                dt.Rows[index]["chkper"] = chkper;
                dt.Rows[index]["entry"] = chkEntry;
                dt.Rows[index]["printable"] = chkPrint;
                dt.Rows[index]["delete"] = delete;

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
            this.lblMsg1.Visible = true;

            this.Session_update();
            string comcod = this.ddlCompany.SelectedValue.ToString();
            string usrid = this.lblusrid.Text;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataTable dt1 = (DataTable)Session["tblusrper"];
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "frmid like '" + modname + "'";
            //DataTable dt1 = dv.ToTable();
            bool result = false;


            result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELETEUSER", usrid, modname,
                        "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (!result)
            {
                this.lblMsg1.Text = User.ErrorObject["Msg"].ToString();
                // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }


            DataView dv = new DataView();
            DataSet ds1 = new DataSet("ds1");
            dv = dt1.DefaultView;
            dv.RowFilter = "chkper=True";
            dt1 = dv.ToTable();
            //    string chkper = dt1.Rows[i]["chkper"
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "tbl1";

            result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUSRPER", ds1, null, null, usrid, "", "", "", "", "", "", "", "",
           "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                this.lblMsg1.Text = User.ErrorObject["Msg"].ToString();
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            else
            {
                this.lblMsg1.Text = "Updated Successfully";
            }



        }
        protected void chkShowall_CheckedChanged(object sender, EventArgs e)
        {
            //this.ddlModName.SelectedValue = "00";

            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
                //this.usrSpcPer();
            }
            else
            {
                this.usrSpcPer();
            }
        }
        private void ShowAllPer()
        {
            DataTable dt = (DataTable)Session["tblcompper"];
            DataTable dt1 = (DataTable)Session["tblusrper"];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string frmname = dt1.Rows[i]["frmname"].ToString().Trim();
                string qrytype = dt1.Rows[i]["qrytype"].ToString().Trim();
                string chkper = dt1.Rows[i]["chkper"].ToString().Trim();
                string entry = dt1.Rows[i]["entry"].ToString().Trim();
                string delete = dt1.Rows[i]["delete"].ToString().Trim();

                string printable = dt1.Rows[i]["printable"].ToString().Trim();
                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;
                    dr1[0]["delete"] = delete;


                }

            }

            Session["tblusrper"] = this.HiddenSameData(dt);
            this.ShowPer();

        }
        private void usrSpcPer()
        {
            string comcod = this.ddlCompany.SelectedValue.ToString();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "SHOWGRPUSERPERFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds4 == null || ds4.Tables[0].Rows.Count == 0)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
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
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("delete")).Checked = true;

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


        private void ShowAllData()
        {
            string comcod = this.ddlCompany.SelectedValue.ToString();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "SHOWGRPUSERPERFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null || ds4.Tables[0].Rows.Count == 0)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);


            DataTable dt = (DataTable)Session["tblcompper"];

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
                string delete = dt1.Rows[i]["delete"].ToString().Trim();

                string confrmqry = frmname + qrytype;
                DataRow[] dr1 = dt2.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {
                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;
                    dr1[0]["delete"] = delete;

                }

            }




            Session["tblusrper"] = this.HiddenSameData(dt2);
            this.ShowPer();






        }
        private void ShowData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "AA" ? "" : this.ddlModuleName.SelectedValue.ToString()) + "%";
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "SHOWMODWISEGRPUSRFORM", usrid, modname, "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }
            Session["tblusrper"] = this.HiddenSameData(ds4.Tables[0]);
            this.ShowPer();
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
                this.lblMsg1.Text = "Updated Failed";
                return;

            }
            this.ShowData();
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



        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvPermission.Rows.Count; i++)
            {
                if (((CheckBox)this.gvPermission.Rows[i].FindControl("chkAll")).Checked)
                {
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPermit")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkEntry")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("chkPrint")).Checked = true;
                    ((CheckBox)this.gvPermission.Rows[i].FindControl("delete")).Checked = true;

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
        protected void chkAllDel_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblusrper"];
            int i, index;
            if (((CheckBox)this.gvPermission.HeaderRow.FindControl("chkAllDel")).Checked)
            {

                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("delete")).Checked = true;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["delete"] = "True";


                }


            }

            else
            {
                for (i = 0; i < this.gvPermission.Rows.Count; i++)
                {

                    ((CheckBox)this.gvPermission.Rows[i].FindControl("delete")).Checked = false;
                    index = (this.gvPermission.PageSize) * (this.gvPermission.PageIndex) + i;
                    dt.Rows[index]["delete"] = "False";


                }

            }

            Session["tblusrper"] = dt;

        }

        protected void lgvusrShorName_Click(object sender, EventArgs e)
        {
            this.userDetPan.Visible = true;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string grpusrid = ((LinkButton)this.gvUseForm.Rows[index].FindControl("lbtnUserId")).Text.ToString();
            DataSet ds1 = User.GetTransInfo("", "SP_UTILITY_GRPUSER_MGT", "GETGRUSERCOMPANY", grpusrid, "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            this.lbluserheading.Text = "<span class='glyphicon glyphicon-user'></span>Name: " + dt.Rows[0]["usrname"].ToString() + ",  Group Id: " + dt.Rows[0]["grpusrid"].ToString();
            this.indUsrinf.DataSource = dt;
            this.indUsrinf.DataBind();
        }
        protected void lbtnNewUser_Click(object sender, EventArgs e)
        {
            ViewState.Remove("tblapend");
            this.LblUpFlag.Text = "NEW";
            gvcompany.DataSource = null;
            gvcompany.DataBind();
            this.gvUseForm.Visible = false;
            this.userDetPan.Visible = false;
            this.MultiView1.ActiveViewIndex = 1;
            this.txtUsr.Text = "";
            this.TxtFullName.Text = "";
            this.TxtDesg.Text = "";
            this.TxtRemark.Text = "";
            this.TxtEmail.Text = "";
            this.GetAllComp();
            this.GetEmployeeName();
            this.CreateList();
            this.GetLastGrpUserId();




        }
        private void CreateList()
        {
            List<RealEntity.C_34_Mgt.GetCompany> list = new List<RealEntity.C_34_Mgt.GetCompany>();
            ViewState["tblapend"] = list;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var list2 = (List<RealEntity.C_34_Mgt.GetCompany>)ViewState["tblapend"];


            string comcod = this.ddlComp.SelectedValue.ToString();
            string comname = this.ddlComp.SelectedItem.ToString();
            string usrsname = this.txtUsr.Text.Trim().ToString();
            string usrname = this.TxtFullName.Text.Trim().ToString();
            string usrdesig = this.TxtDesg.Text.Trim().ToString();
            // string usrrmrk = this.TxtRemark.Text.Trim().ToString();
            string empid = this.ddlHrEmp.SelectedValue.ToString();
            string uRole = this.ddlUserRole.SelectedValue.ToString();
            string usractive = (this.actChkbox.Checked == true) ? "True" : "False";
            var checklist = list2.FindAll(p => p.comcod == comcod);
            if (checklist.Count == 0)
            {
                RealEntity.C_34_Mgt.GetCompany newlist = new RealEntity.C_34_Mgt.GetCompany(comcod, comname, "", usrsname, usrname, usrdesig, "", "", empid, uRole, usractive);
                list2.Add(newlist);
            }
            else
            {
                int i = 0;
                foreach (RealEntity.C_34_Mgt.GetCompany lis in list2)
                {
                    list2[i].usrname = usrname;
                    list2[i].usrsname = usrsname;
                    list2[i].usrdesig = usrdesig;
                    list2[i].urole = uRole;
                    list2[i].empid = empid;

                    i++;
                }
            }
            ViewState["tblapend"] = list2;
            PermitedCOmpBind();
        }
        public void PermitedCOmpBind()
        {
            List<RealEntity.C_34_Mgt.GetCompany> list2 = (List<RealEntity.C_34_Mgt.GetCompany>)ViewState["tblapend"];
            gvcompany.DataSource = list2;
            gvcompany.DataBind();

            //var
            //      newlist = new RealEntity.C_34_Mgt.GetCompany("0000", "--Select Company--", "", "", "", "", "", "", "", "", "");
            //var newlist2 = list2;
            //newlist2.Add(newlist);
            this.ddlPermitedComp.DataTextField = "comname";
            this.ddlPermitedComp.DataValueField = "comcod";
            this.ddlPermitedComp.DataSource = list2;
            this.ddlPermitedComp.DataBind();
            //this.ddlPermitedComp.SelectedValue = "0000";
            // ddlPermitedComp_SelectedIndexChanged(null,null);
        }
        private void GetAllComp()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            //  string comcod = hst["comcod"].ToString();
            DataSet ds = User.GetTransInfo("", "SP_UTILITY_GRPUSER_MGT", "GETCOMPANY", "", "", "", "", "", "", "", "", "");
            var List = ds.Tables[0].DataTableToList<RealEntity.C_34_Mgt.GetCompany>();
            ViewState["compdata"] = List;
            this.ddlComp.DataTextField = "comsnam";
            this.ddlComp.DataValueField = "comcod";
            this.ddlComp.DataSource = ds.Tables[0];
            this.ddlComp.DataBind();

        }
        private void GetEmployeeName()
        {
            DataTable dt1 = new DataTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "GET_EMP_NAME", "", "", "", "", "", "", "", "", "");

            dt1 = ds1.Tables[0].Copy();

            dt1 = dt1.DefaultView.ToTable(true, "sircode", "sirdesc");
            dt1.Rows.Add("", "None");
            //var List = ds.Tables[0].DataTableToList<MFGOBJ.C_34_Mgt.GetCompanyname>();
            //ViewState["compname"] = List;
            this.ddlHrEmp.DataTextField = "sirdesc";
            this.ddlHrEmp.DataValueField = "sircode";
            this.ddlHrEmp.DataSource = dt1;
            this.ddlHrEmp.DataBind();
            this.ddlHrEmp.SelectedValue = "";



            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_GRPUSER_MGT", "GET_USER_ROLE_CODE_INFO", "", "", "", "", "", "", "", "", "");

            ddlUserRole.DataTextField = "roledesc";
            ddlUserRole.DataValueField = "roleid";
            ddlUserRole.DataSource = ds2;
            ddlUserRole.DataBind();
            //ddlUserRole.SelectedValue = "";

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            this.lnkbtnBack_Click(null, null);
        }
        private void GetLastGrpUserId()
        {
            DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_GRPUSER_MGT", "GETLASTGRPUSRID", "", "", "", "", "", "", "", "", "");
            DataTable dt = ds3.Tables[0];
            string mastrcomlastid = ds3.Tables[0].Rows[0]["maxgrusr"].ToString();
            this.Grpusr.Text = mastrcomlastid;

        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {


            this.Save_Value();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            //  string actvstatus = (this.actChkbox.Checked == true) ? "True" : "False";


            var list2 = (List<RealEntity.C_34_Mgt.GetCompany>)ViewState["tblapend"];
            string pass = this.TxtPass.Text.Trim().ToString();
            string mastrcomlastid = this.Grpusr.Text.ToString();
            string usrsname = this.txtUsr.Text.Trim().ToString();

            string flag = this.LblUpFlag.Text.ToString();
            if (flag == "NEW")
            {
                DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_GRPUSER_MGT", "CHECKGRPUSERFORDUPLICATE", usrsname, "", "", "", "", "", "", "", "");
                DataTable dt = ds3.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    this.lblMsg.Visible = true;
                    this.lblMsg.Text = "This User Already Exist";
                    return;
                }

            }


            string usrpass = (pass.Length == 0) ? "" : ASTUtility.EncodePassword(pass);
            // string usrpass = ASTUtility.EncodePassword(usrsname + pass);
            string usrname = this.TxtFullName.Text.Trim().ToString();
            string remark = this.TxtRemark.Text.ToString();
            string desig = this.TxtDesg.Text.Trim().ToString();
            string employee = this.ddlHrEmp.SelectedValue.ToString();
            string uRole = this.ddlUserRole.SelectedValue.ToString();
            string uRoledesc = this.ddlUserRole.SelectedItem.ToString();
            string email = this.TxtEmail.Text.ToString();
            bool result = false;
            foreach (RealEntity.C_34_Mgt.GetCompany lis in list2)
            {
                string userid = lis.comcod + mastrcomlastid.Substring(1);
                string actvstatus = lis.usractive.ToString();
                result = User.UpdateTransInfo(lis.comcod, "SP_UTILITY_GRPUSER_MGT", "INSORUPDATEGRPUSR", userid, usrsname, usrname, desig, usrpass, uRoledesc, actvstatus, employee, mastrcomlastid, uRole, email, "", "", "", "");

            }

            if (result == true)
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Update Successfully";
                this.ShowUserInfo();
                //  ViewState.Remove("tblapend");
                lnkbtnBack_Click(null, null);
            }
            else
            {
                this.lblMsg.Visible = false;
                this.lblMsg.Text = "Update Failed";
                return;
            }
        }
        protected void gvcompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var mrlist = (List<RealEntity.C_34_Mgt.GetCompany>)ViewState["tblapend"];
            mrlist.RemoveAt(e.RowIndex);
            ViewState["tblapend"] = mrlist;
            PermitedCOmpBind();

        }



        protected void EditBtn_Click(object sender, EventArgs e)
        {
            this.userDetPan.Visible = true;
            this.LblUpFlag.Text = "OLD";
            ViewState.Remove("tblapend");
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            int index = row.RowIndex;
            string grpuser = ((LinkButton)gvUseForm.Rows[index].FindControl("lbtnUserId")).Text.Trim().ToString();

            DataSet ds3 = User.GetTransInfo("", "SP_UTILITY_GRPUSER_MGT", "GETGRUSERCOMPANY", grpuser, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            var list = ds3.Tables[0].DataTableToList<RealEntity.C_34_Mgt.GetCompany>();

            ViewState["tblapend"] = list;


            this.gvUseForm.Visible = false;
            this.userDetPan.Visible = false;
            this.MultiView1.ActiveViewIndex = 1;
            this.txtUsr.Text = list[0].usrsname;
            this.TxtFullName.Text = list[0].usrname;
            this.TxtDesg.Text = list[0].usrdesig;
            this.TxtRemark.Text = list[0].usrrmrk;
            this.Grpusr.Text = grpuser;
            this.GetAllComp();
            this.GetEmployeeName();
            if (list[0].empid.Length > 0)
            {
                this.ddlHrEmp.SelectedValue = list[0].empid;
            }

            this.ddlUserRole.SelectedValue = list[0].urole;
            PermitedCOmpBind();

            string url1 = "";
            if (ds3.Tables[0].Rows[0]["usrimg"] != null && ds3.Tables[0].Rows[0]["usrimg"].ToString() != "")
            {

                byte[] imgbyte = (byte[])ds3.Tables[0].Rows[0]["usrimg"];
                url1 = "data:image;base64," + Convert.ToBase64String(imgbyte);
            }
            else
            {
                url1 = "~/Content/Theme/images/avatars/human_avatar.png";
            }
            this.UserImage.ImageUrl = url1;
        }
        private void Save_Value()
        {
            var list2 = (List<RealEntity.C_34_Mgt.GetCompany>)ViewState["tblapend"];

            for (int i = 0; i < this.gvcompany.Rows.Count; i++)
            {
                //double tAmt = Convert.ToDouble("0" + ((Label)this.RepInv.Items[i].FindControl("rlbltAmt")).Text.Trim());

                string usractive = (((CheckBox)this.gvcompany.Rows[i].FindControl("chkActiveSt")).Checked == true) ? "True" : "False";
                list2[i].usractive = usractive;
            }
            ViewState["tblapend"] = list2;

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkShowall.Checked = false;
            GetPermmission();

        }
        protected void GetControlAccCode()
        {

            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string FindProject = "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETMODULELIST", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlConTrolCode.DataTextField = "modulename1";
            this.ddlConTrolCode.DataValueField = "moduleid";
            this.ddlConTrolCode.DataSource = ds1.Tables[0];
            this.ddlConTrolCode.DataBind();
            ds1.Dispose();



        }
        protected void gvProLinkInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string UserName = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            DataTable dt = (DataTable)ViewState["tbLink"];

            string Userid = ((Label)this.gvProLinkInfo.Rows[e.RowIndex].FindControl("lblgvBancCode")).Text.Trim();
            bool result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "DELUSERMODULEPER", UserName, Userid, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProLinkInfo.PageSize) * (this.gvProLinkInfo.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tbLink");
            ViewState["tbLink"] = dv.ToTable();
            this.MenuPermissonBind();


        }



        protected void ddlPermitedComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ProfilePenlTitle.Attributes.Add("class", "clickable small panel-collapsed");
            //this.ProfilePenlTitle.InnerHtml = "<i class='fa fa-plus'></i>";
            //this.ProfilePenl.Attributes.Add("style","display:none");

            //this.PermissionPanel.Attributes.Add("class", "clickable small ");
            //this.PermissionPanel.InnerHtml = "<i class='fa fa-minus'></i>";
            //this.PermissionPnl.Attributes.Add("style", "display:inherit");

            RadioButtonList1_SelectedIndexChanged(null, null);
        }

        private void GetUserModPer()
        {

            ViewState.Remove("tbLink");
            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string UserCode = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETUSERMODULE", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbLink"] = ds1.Tables[0];
            this.MenuPermissonBind();
            ds1.Dispose();

        }
        private void Session_tbltbPreLink_Update()
        {
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvProLinkInfo.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvProLinkInfo.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvProLinkInfo.PageIndex) * this.gvProLinkInfo.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            ViewState["tbLink"] = tbl1;
        }
        protected void lbtnSelectSupl1_Click(object sender, EventArgs e)
        {
            this.Session_tbltbPreLink_Update();
            DataTable tbl1 = (DataTable)ViewState["tbLink"];
            string ProCode = this.ddlConTrolCode.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("moduleid = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["moduleid"] = this.ddlConTrolCode.SelectedValue.ToString();
                dr1["modulename"] = this.ddlConTrolCode.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tbLink"] = tbl1;
            this.MenuPermissonBind();
        }


        protected void lbtnSelectAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbLink"];

            for (int i = 0; i < this.ddlConTrolCode.Items.Count; i++)
            {
                string actcode = this.ddlConTrolCode.Items[i].Value;
                DataRow[] dr = dt.Select("moduleid='" + actcode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["moduleid"] = this.ddlConTrolCode.Items[i].Value;
                    dr1["modulename"] = this.ddlConTrolCode.Items[i].Text;
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);
                }


            }

            ViewState["tbLink"] = dt;
            this.MenuPermissonBind();
        }
        public void MenuPermissonBind()
        {
            DataTable dt = (DataTable)ViewState["tbLink"];
            this.gvProLinkInfo.DataSource = dt;
            this.gvProLinkInfo.DataBind();
        }

        protected void LbtnMenuUpdate_Click(object sender, EventArgs e)
        {

            this.Session_tbltbPreLink_Update();

            bool result = false;


            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string userid = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            DataTable tbl1 = (DataTable)ViewState["tbLink"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string pactcode = tbl1.Rows[i]["moduleid"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                result = User.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTUPDATEPER", userid, pactcode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");


            }

            if (result == true)
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Update Successfully";
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProfilePenlTitle.Attributes.Add("class", "clickable small panel-collapsed");
            this.ProfilePenlTitle.InnerHtml = "<i class='fa fa-plus'></i>";
            this.ProfilePenl.Attributes.Add("style", "display:none");

            this.PermissionPanel.Attributes.Add("class", "clickable small ");
            this.PermissionPanel.InnerHtml = "<i class='fa fa-minus'></i>";
            this.PermissionPnl.Attributes.Add("style", "display:inherit");

            string value = this.RadioButtonList1.SelectedValue.ToString();
            switch (value)
            {
                case "0":
                    this.PanelMenu.Visible = true;
                    this.PanelCashbank.Visible = false;
                    this.Panelroject.Visible = false;
                    this.GetControlAccCode();
                    this.GetUserModPer();
                    this.RadioButtonList1.Items[0].Attributes["class"] = "lblactive blink_me";
                    break;

                case "1":
                    this.PanelMenu.Visible = false;
                    this.PanelCashbank.Visible = true;
                    this.Panelroject.Visible = false;
                    this.GetControlCashbankCode();
                    this.GetCashAndBankPer();
                    this.RadioButtonList1.Items[1].Attributes["class"] = "lblactive blink_me";
                    break;
                case "2":
                    this.PanelMenu.Visible = false;
                    this.PanelCashbank.Visible = false;
                    this.Panelroject.Visible = true;
                    this.Load_Project_Combo();
                    this.Get_Receive_Info();
                    this.RadioButtonList1.Items[2].Attributes["class"] = "lblactive blink_me";
                    break;
            }
        }
        protected void GetControlCashbankCode()
        {

            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string FindProject = "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETCONTROLCODE", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlCashControlCode.DataTextField = "actdesc";
            this.ddlCashControlCode.DataValueField = "actcode";
            this.ddlCashControlCode.DataSource = ds1.Tables[0];
            this.ddlCashControlCode.DataBind();
            ds1.Dispose();



        }
        private void Save_valueCashbank()
        {
            DataTable tbl1 = (DataTable)ViewState["tbcashbank"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvCashbank.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvCashbank.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvCashbank.PageIndex) * this.gvCashbank.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            ViewState["tbcashbank"] = tbl1;
        }
        protected void lbtnCashBnkselect_Click(object sender, EventArgs e)
        {
            this.Save_valueCashbank();
            DataTable tbl1 = (DataTable)ViewState["tbcashbank"];
            string ProCode = this.ddlCashControlCode.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("actcode = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["actcode"] = this.ddlCashControlCode.SelectedValue.ToString();
                dr1["actdesc"] = this.ddlCashControlCode.SelectedItem.Text.Trim().Substring(13);
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            ViewState["tbcashbank"] = tbl1;
            this.Cashbank_DataBind();
        }

        protected void lbtnCashBnkselectall_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["tbcashbank"];

            for (int i = 0; i < this.ddlCashControlCode.Items.Count; i++)
            {
                string actcode = this.ddlCashControlCode.Items[i].Value;
                DataRow[] dr = dt.Select("actcode='" + actcode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["actcode"] = this.ddlCashControlCode.Items[i].Value;
                    dr1["actdesc"] = this.ddlCashControlCode.Items[i].Text.Substring(13);
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);
                }


            }

            ViewState["tbcashbank"] = dt;
            this.Cashbank_DataBind();
        }
        protected void Cashbank_DataBind()
        {
            DataTable tbl1 = (DataTable)ViewState["tbcashbank"];
            this.gvCashbank.DataSource = tbl1;
            this.gvCashbank.DataBind();

        }

        protected void gvCashbank_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            DataTable dt = (DataTable)ViewState["tbcashbank"];
            string usrid = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            string BankCode = ((Label)this.gvCashbank.Rows[e.RowIndex].FindControl("lblgvBancCode")).Text.Trim();
            bool result = User.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "DELETEBANKCODE", usrid, BankCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvCashbank.PageSize) * (this.gvCashbank.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tbcashbank");
            ViewState["tbcashbank"] = dv.ToTable();
            this.Cashbank_DataBind();

        }
        private void GetCashAndBankPer()
        {

            ViewState.Remove("tbcashbank");
            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string UserCode = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_MGT", "GETUSERCANDBANK", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            ViewState["tbcashbank"] = ds1.Tables[0];
            this.Cashbank_DataBind();
            ds1.Dispose();

        }


        protected void LbtnCashBankUpdate_Click(object sender, EventArgs e)
        {
            this.Save_valueCashbank();

            bool result = false;

            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string userid = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();

            DataTable tbl1 = (DataTable)ViewState["tbcashbank"];

            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string pactcode = tbl1.Rows[i]["actcode"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                result = User.UpdateTransInfo(comcod, "SP_ENTRY_MGT", "INSERTUPUCANDBANK", userid, pactcode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = User.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void Load_Project_Combo()
        {

            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string FindProject = "%";
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETPRJLIST", FindProject, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();


        }
        private void Get_Receive_Info()
        {

            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string UserCode = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            DataSet ds1 = User.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "GETPRELINK", UserCode, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            Session["tbPreLink"] = ds1.Tables[0];

            this.Project_DataBind();

        }
        protected void Project_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            this.gvProject.DataSource = tbl1;
            this.gvProject.DataBind();

        }


        protected void gvProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string UserCode = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();
            DataTable dt = (DataTable)Session["tbPreLink"];

            string BankCode = ((Label)this.gvProject.Rows[e.RowIndex].FindControl("lblgvprocode")).Text.Trim();
            bool result = User.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "DELETEPROCODE", UserCode, BankCode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                int rowindex = (this.gvProject.PageSize) * (this.gvProject.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            Session.Remove("tbPreLink");
            Session["tbPreLink"] = dv.ToTable();
            this.Project_DataBind();
        }
        private void Save_value_project()
        {
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            int TblRowIndex2;
            for (int j = 0; j < this.gvProject.Rows.Count; j++)
            {
                string dgvRemarks = ((TextBox)this.gvProject.Rows[j].FindControl("txtgvSuplRemarks")).Text.Trim();

                TblRowIndex2 = (this.gvProject.PageIndex) * this.gvProject.PageSize + j;
                tbl1.Rows[TblRowIndex2]["remarks"] = dgvRemarks;
            }
            Session["tbPreLink"] = tbl1;
        }

        protected void lbtnSuplUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            this.Save_value_project();
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.ddlPermitedComp.SelectedValue.ToString();
            string userid = comcod + ASTUtility.Right(this.Grpusr.Text.Trim(), 3).ToString();


            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            for (int i = 0; i < tbl1.Rows.Count; i++)
            {

                string pactcode = tbl1.Rows[i]["pactcode"].ToString();
                string mRMRKS = tbl1.Rows[i]["remarks"].ToString();

                bool result = User.UpdateTransInfo(comcod, "SP_ENTRY_PURCHASE_04", "INSERTUPDATELINK",
                              userid, pactcode, mRMRKS, "", "", "", "", "", "", "", "", "", "", "", "");
                if (!result)
                {
                    ((Label)this.Master.FindControl("lblmsg")).Text = User.ErrorObject["Msg"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                    return;
                }
            }
          ((Label)this.Master.FindControl("lblmsg")).Text = "Data Updated successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = ((Label)this.Master.FindControl("lblTitle")).Text;
                string eventdesc = "Update Project user Define";
                string eventdesc2 = "";
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
        }

        protected void lbtnSelectAllProject_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tbPreLink"];

            for (int i = 0; i < this.ddlProjectName.Items.Count; i++)
            {
                string ProCode = this.ddlProjectName.Items[i].Value;
                DataRow[] dr = dt.Select("pactcode='" + ProCode + "'");
                if (dr.Length == 0)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["pactcode"] = this.ddlProjectName.Items[i].Value;
                    dr1["pactdesc"] = this.ddlProjectName.Items[i].Text.Substring(13);
                    dr1["remarks"] = "";
                    dt.Rows.Add(dr1);


                }


            }

            Session["tbPreLink"] = dt;
            this.Project_DataBind();
        }

        protected void lbtnSelectProject_Click(object sender, EventArgs e)
        {
            this.Save_value_project();
            DataTable tbl1 = (DataTable)Session["tbPreLink"];
            string ProCode = this.ddlProjectName.SelectedValue.ToString();
            DataRow[] dr2 = tbl1.Select("pactcode = '" + ProCode + "'");
            if (dr2.Length == 0)
            {
                DataRow dr1 = tbl1.NewRow();
                dr1["pactcode"] = this.ddlProjectName.SelectedValue.ToString();
                dr1["pactdesc"] = this.ddlProjectName.SelectedItem.Text.Trim();
                dr1["remarks"] = "";
                tbl1.Rows.Add(dr1);
            }
            Session["tbPreLink"] = tbl1;
            this.Project_DataBind();
        }
    }
}