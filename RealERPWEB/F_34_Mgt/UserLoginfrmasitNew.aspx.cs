﻿using System;
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
    public partial class UserLoginfrmasitNew : System.Web.UI.Page
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

                    this.getListModulename();
                }
                else
                {
                    Response.Redirect("~/ErrorHandling.aspx?Type=Fake User");
                }




            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);


        }
        protected void ibtnFindName_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowUserInfo();

        }
        private void getListModulename()
        {

            string comcod = this.GetComeCode();
            ProcessAccess ulogin = new ProcessAccess();
            DataSet ds1 = ulogin.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETCOMMODULE_NAHID", "", "", "", "", "", "", "", "", "");

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
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            //DataTable dt = (DataTable)Session["tblUsrinfo"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            string usrid = this.lblusrid.Text;

            // var rptlist = dt.DataTableToList<RealEntity.C_34_Mgt.Userinfo>();

            //LocalReport rptcb1 = new LocalReport();

            // rptcb1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_34_Mgt.UserLoginInfo", rptlist, null, null);

            // rptcb1.SetParameters(new ReportParameter("compname", compname));

            DataSet ds1 = User.GetTransInfo(comcod, "SP_REPORT_LOGSTAUTS", "RPTUSRPRFORM", usrid, "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;


            ReportDocument rptcb1 = new RealERPRPT.R_34_Mgt.RptUsrPerFrm();
            TextObject rptCname = rptcb1.ReportDefinition.ReportObjects["TxtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject rptUname = rptcb1.ReportDefinition.ReportObjects["txtUserName"] as TextObject;
            rptUname.Text = "User Name: " + this.txtuserid.Text;

            TextObject rptdate = rptcb1.ReportDefinition.ReportObjects["date"] as TextObject;
            rptdate.Text = "Date " + System.DateTime.Now.ToString("dd-MMM-yyyy");

            TextObject txtuserinfo = rptcb1.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);
            rptcb1.SetDataSource(ds1.Tables[0]);

            string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            rptcb1.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptcb1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../RptViewer.aspx?PrintOpt=" +
                                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
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
                string msg = "Updated Failed";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            else
            {
                string msg = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }






            this.gvUseForm.EditIndex = -1;
            this.ShowUserInfo();

            if (ConstantInfo.LogStatus == true)
            {
                string eventtype = "User Login From";
                string eventdesc = "Update ID";
                string eventdesc2 = usrsname;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }

        }
        protected void gvUseForm_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvUseForm.EditIndex = e.NewEditIndex;
            this.LoadGrid();
        }
        protected void lbtnUserId_Click(object sender, EventArgs e)
        {
            this.ddlModuleName.SelectedValue = "0";
            Session.Remove("tblusrper");

            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            this.gvUseForm.Visible = false;
            this.MultiView1.ActiveViewIndex = 0;
            string comcod = this.GetComeCode();
            string usrid = comcod+"001";//Convert.ToString(((LinkButton)sender).Text.Trim());
            this.lblusrid.Text = usrid;
            ///-------------------------///////////
            this.lblId.Visible = true;
            this.txtuserid.Visible = true;
            DataTable tbl01 = (DataTable)Session["tblUsrinfo"];
            DataRow[] dr1 = tbl01.Select("usrid='" + usrid + "'");
            this.txtuserid.Text = dr1[0]["usrname"].ToString();

            string wdtype = this.ddlType.SelectedValue.ToString();

            ///-------------------------///////////
            DataSet ds2 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GET_MENU_MASTER_MENU_NAHID", wdtype, "", "", "", "", "", "", "", "");
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

                DataSet ds3 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GET_MENU_MASTER_MENU_NAHID", wdtype, "", "", "", "", "", "", "", "");

                Session["tblusrper"] = this.HiddenSameData(ds3.Tables[0]);

            }
            
            this.ShowPer();

        }
        


        private DataTable HiddenSameData(DataTable dt1)
        {
            //if (dt1.Rows.Count == 0)
            //    return dt1;

            //string modulename = dt1.Rows[0]["modulename"].ToString();
            //string frmid1 = dt1.Rows[0]["frmid1"].ToString();
            //for (int j = 1; j < dt1.Rows.Count; j++)
            //{
            //    if (dt1.Rows[j]["frmid1"].ToString() == frmid1)
            //    { 
            //        dt1.Rows[j]["frmdesc"] = "";

            //    }


            //    if (dt1.Rows[j]["modulename"].ToString() == modulename)
            //    {
            //        modulename = dt1.Rows[j]["modulename"].ToString();
            //        dt1.Rows[j]["modulename"] = "";
            //    }
            //    else 
            //    {
            //        modulename = dt1.Rows[j]["modulename"].ToString();
            //    }



            // frmid1 = dt1.Rows[j]["frmid1"].ToString();


            //}



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
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "0" : this.ddlModuleName.SelectedValue.ToString());
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

            result = User.UpdateXmlTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTCOMPPER_NAHID", ds1, null, null, modname, "", "", "", "", "", "", "", "",
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
        protected void chkShowall_CheckedChanged(object sender, EventArgs e)
        {
            //this.ddlModName.SelectedValue = "00";

            if (this.chkShowall.Checked)
            {
                this.ShowAllData();
                //this.ShowAllPer();
            }
            else
            {
                this.usrSpcPer();
            }
        }
        private void ShowAllPer()
        {

            string coml1digit = this.GetComeCode().Substring(0, 1);
            DataTable dt = coml1digit == "8" ? ConstantInfo.WebObjTableGroupACC() : ConstantInfo.WebObjTable();

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
                DataRow[] dr1 = dt.Select("(frmname+qrytype)='" + confrmqry + "'");
                if (dr1.Length > 0)
                {

                    dr1[0]["chkper"] = chkper;
                    dr1[0]["entry"] = entry;
                    dr1[0]["printable"] = printable;
                    dr1[0]["delete"] = delete;

                    //
                    dr1[0]["chkper"] = chkper;


                }

            }

            Session["tblusrper"] = this.HiddenSameData(dt);
            this.ShowPer();

        }
        private void usrSpcPer()
        {

            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "" : this.ddlModuleName.SelectedValue.ToString());
            string comcod = this.GetComeCode();
            //string usrid = this.lblusrid.Text;
            //this.lblusrid.Text = usrid;
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORMASITUSER", "", "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }


            DataView dv = ds4.Tables[0].DefaultView;
            dv.RowFilter = "frmid like '" + modname + "'";
            DataTable dt2 = dv.ToTable();
            Session["tblusrper"] = this.HiddenSameData(dt2);
            this.ShowPer();
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


        private void ShowAllData()
        {
            string comcod = this.GetComeCode();
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "" : this.ddlModuleName.SelectedValue.ToString());
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORMASITUSER_NAHID", modname, "", "", "", "", "", "", "", "");
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
            string usrid = this.lblusrid.Text;
            this.lblusrid.Text = usrid;
            string modname = (this.ddlModuleName.SelectedValue.Trim() == "0" ? "0" : this.ddlModuleName.SelectedValue.ToString());
            DataSet ds4 = User.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERPERFORMASITUSERACTIVE_NAHID", modname, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvPermission.DataSource = null;
                this.gvPermission.DataBind();
                return;
            }


            DataTable dt2 = ds4.Tables[0];


            Session["tblusrper"] = this.HiddenSameData(dt2);
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
                string msg = "Delete User Fail";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            else
            {
                string msg = "User ID/Passwrod Updated";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
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
                if (menuparentid== "0" || menuparentid == "")
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