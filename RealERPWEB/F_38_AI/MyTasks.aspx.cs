﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_38_AI
{
    public partial class MyTasks : System.Web.UI.Page
    {
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();

        ProcessAccess AIData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                ((Label)this.Master.FindControl("lblTitle")).Text = "My Tasks";

                //DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                //((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                //this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                string type = Request.QueryString["Type"];
                if (type == "MGT")
                {
                    this.mgtenplist.Visible = true;


                }
                this.GetEmployeeName();
                btnMyTasks_SelectedIndexChanged(null, null);

            }
        }


        private void GetEmployeeName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string Uempid = hst["empid"].ToString();
            string comcod = this.GetCompCode();
            string company = "94%";
            string projectName = "%";
            string qtype = Request.QueryString["Type"].ToString();

            string emp = Request.QueryString["EmpID"].ToString() == "" ? "" : Request.QueryString["EmpID"].ToString();
            string txtSEmployee = "%%";
            DataSet ds3 = AIData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETEMPNAME", company, projectName, txtSEmployee, "", "", "", "", "", "");
            if (ds3 == null)
                return;

            Session["tblempname"] = ds3.Tables[0];
            DataTable dt2 = ds3.Tables[0];

            DataView dv2 = dt2.DefaultView;
            this.ddemplist.DataTextField = "empname";
            this.ddemplist.DataValueField = "empid";
            this.ddemplist.DataSource = dv2.ToTable();
            this.ddemplist.DataBind();
            if (emp.Length > 0 && qtype == "MGT")
            {
                this.ddemplist.SelectedValue = emp;

            }
            else
            {
                this.ddemplist.SelectedValue = Uempid;


            }

        }

        protected void btnMyTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("tblassinglist");
            Session.Remove("tbltodaylist");

            string value = this.btnMyTasks.SelectedValue.ToString();
            switch (value)
            {
                case "1":
                    this.MultiView1.ActiveViewIndex = 0;

                    break;
                case "2":
                    this.MultiView1.ActiveViewIndex = 1;
                    GetRecentAssigned();
                    GetTodayDoingJob();
                    GetTodayActivities();
                    break;
                case "3":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
                case "4":
                    this.MultiView1.ActiveViewIndex = 3;
                    break;

            }
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetRecentAssigned()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string empid = this.ddemplist.SelectedValue.ToString();

            string comcod = this.GetCompCode();
            empid = this.Request.QueryString["empid"].ToString() == "" ? empid : this.Request.QueryString["empid"].ToString();
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETMYTASK", empid, "", "", "", "", "");
            if (ds == null)
                return;


            Session["tblassinglist"] = ds.Tables[0];
            this.data_Bind();
        }

        private void GetTodayDoingJob()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();

            string empid = this.ddemplist.SelectedValue.ToString();

            string comcod = this.GetCompCode();
            empid = this.Request.QueryString["empid"].ToString() == "" ? empid : this.Request.QueryString["empid"].ToString();
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETMYTASKPENDING", empid, "", "", "", "", "");
            if (ds == null)
                return;


            Session["tbltodaylist"] = ds.Tables[0];
            this.data_Bind();

        }
        private void GetTodayActivities()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string empid = this.ddemplist.SelectedValue.ToString();


            string comcod = this.GetCompCode();
            empid = this.Request.QueryString["empid"].ToString() == "" ? empid : this.Request.QueryString["empid"].ToString();
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETMYACTIVITIES", empid, "", "", "", "", "");
            if (ds == null)
                return;


            Session["tblActivities"] = ds.Tables[0];
            DataTable dt1 = new DataTable();
            DataView view = new DataView();
            view.Table = ds.Tables[0];
            view.RowFilter = " trackertype='99220'";
            dt1 = view.ToTable();
            Session["tblcompletejob"] = ds.Tables[1];
            this.data_Bind();

        }

        private void data_Bind()
        {

            DataTable tbltodays = (DataTable)Session["tbltodaylist"];
            DataTable tblasing = (DataTable)Session["tblassinglist"];
            DataTable tblActivities = (DataTable)Session["tblActivities"];
            DataTable tblcompletesjob = (DataTable)Session["tblcompletejob"];


            this.gvAssingJob.DataSource = tblasing;
            this.gvAssingJob.DataBind();

            this.gvTodayList.DataSource = tbltodays;
            this.gvTodayList.DataBind();

            this.gvActivities.DataSource = tblActivities;
            this.gvActivities.DataBind();




            this.gv_Completejob.DataSource = tblcompletesjob;
            this.gv_Completejob.DataBind();





        }

        protected void lnkStartJob_Click(object sender, EventArgs e)
        {
            try
            {


                Hashtable hst = (Hashtable)Session["tblLogin"];
                if (hst == null)
                {
                    Response.Redirect("~/PinLog.aspx");
                    return;
                }
                string cdate = System.DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
                string createuser = hst["usrid"].ToString();
                string empid = hst["empid"].ToString();

                string assignuser = this.ddemplist.SelectedValue.ToString();

                string trackertype = "99204";// task wip
                string doneqty = "0";
                string skipqty = "0";
                string remarks = "";
                string returnqty = "0";
                string rejectqty = "0";

                GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
                int RowIndex = gvr.RowIndex;
                int index = this.gvAssingJob.PageSize * this.gvAssingJob.PageIndex + RowIndex;
                string comcod = this.GetCompCode();
                string jobid = ((Label)this.gvAssingJob.Rows[RowIndex].FindControl("lbljobid")).Text.Trim();
                bool resultb = AIData.UpdateTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "INSERTUPDATE_STARTTASK", jobid, assignuser, cdate, trackertype, doneqty, skipqty, remarks, "", returnqty, rejectqty);
                if (!resultb)
                {
                    string msg = "Task Start Fail";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail(" + msg + ");", true);
                    return;
                }
                else
                {
                    string msg = "Task Start";

                    string eventtype = "2";
                    string eventdesc = msg;
                    string eventdesc2 = jobid + ".- Description: " + msg;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord("", ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);


                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    GetRecentAssigned();
                    GetTodayDoingJob();
                }
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }

        protected void HoldCreateNote_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;

            int index = (this.gvTodayList.PageSize * this.gvTodayList.PageIndex) + rowIndex;

            string timeid = ((Label)this.gvTodayList.Rows[index].FindControl("lbltimetaskid")).Text.Trim();
            string empid = ((Label)this.gvTodayList.Rows[index].FindControl("lblempid")).Text.Trim();
            string jobid = ((Label)this.gvTodayList.Rows[index].FindControl("lbljobid")).Text.Trim();
            string taskDesc = ((Label)this.gvTodayList.Rows[index].FindControl("Lbltasktitle")).Text.Trim();
            string roletype = ((Label)this.gvTodayList.Rows[index].FindControl("lblgvroletypecode")).Text.Trim();
            string assign = ((Label)this.gvTodayList.Rows[index].FindControl("lblwrkassignqty")).Text.Trim();
            string pending = ((Label)this.gvTodayList.Rows[index].FindControl("lblwrpkdoneqty")).Text.Trim();
            this.hiddenlabel.Value = jobid;
            this.lblwrkassign.Text = assign;
            this.lblwrkdoneqty.Text = pending;

            if (roletype == "95001")
            {
                this.divRetQty.Visible = false;
                this.divRejQty.Visible = false;
                this.holdreason.Visible = true;
            }


            this.holdstatus.Text = "99215";
            this.notetaskid.Text = timeid;
            this.Mdl_lblempid.Text = empid;
            this.Mdl_jobid.Text = jobid;
            this.lbltaskmodal.Text = "Create Hold Note";
            this.holdreason.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HoldtaskNoteModal();", true);

        }

        protected void SaveNote_ServerClick(object sender, EventArgs e)
        {

            try
            {


                string postdate = System.DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");
                string timeTkerID = this.notetaskid.Text;
                string assignuser = this.Mdl_lblempid.Text;
                string jobid = this.Mdl_jobid.Text;
                string holdreason = this.ddlholdreason.SelectedValue.Trim().ToString();
                string remarks = this.noteDescription.Text;
                string doneqty = this.txtDoneQty.Text;
                string skipqty = this.txtSkippqty.Text == "" ? "0.00" : this.txtSkippqty.Text;
                string trackertype = "";
                string jbdonestts = this.donestatus.Text.ToString().Trim();
                string jbholdstts = this.holdstatus.Text.ToString().Trim();
                string returnqty = this.txtreturnqty.Text.Trim() == "" ? "0.00" : this.txtreturnqty.Text.Trim();
                string rejectqty = this.textrejectqty.Text.Trim() == "" ? "0.00" : this.textrejectqty.Text.Trim();
                if (jbdonestts != "")
                {
                    trackertype = this.donestatus.Text.ToString().Trim();
                }
                else
                {
                    trackertype = this.holdstatus.Text.ToString().Trim();
                }
                string comcod = this.GetCompCode();


                double dnqty = Convert.ToDouble("0" + this.txtDoneQty.Text);

                double assign = Convert.ToDouble("0" + this.lblwrkassign.Text);
                double donqty = Convert.ToDouble("0" + this.lblwrkdoneqty.Text);
                double validtotal = assign- donqty;

                if (validtotal < dnqty)
                {
                    string msg = "Done QTY Greater Then Assign QTY";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                    return;
                }
                //jobid, assignuser, worktime, trackertype, doneqty, skipqty, remarks,holdreason,returnqty,rejectqty

                bool resultb = AIData.UpdateTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "INSERTUPDATE_STARTTASK", jobid, assignuser, postdate, trackertype, doneqty, skipqty, remarks, holdreason, returnqty, rejectqty);

                if (!resultb)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);

                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail();", true);
                    return;
                }
                else
                {
                    ClearNoteFrom();
                    string msg = "Hold Task Note Created: " + remarks;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                    this.GetTodayDoingJob();
                    this.GetTodayActivities();
                    this.data_Bind();

                }
            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }


        }
        private void ClearNoteFrom()
        {
            this.noteDescription.Text = "";
            this.txtDoneQty.Text = "0";
            this.txtSkippqty.Text = "0";
            this.txtreturnqty.Text = "";
            this.textrejectqty.Text = "";
        }

        protected void lnkStartJobByID_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int index = (this.gvTodayList.PageSize * this.gvTodayList.PageIndex) + rowIndex;

            string empid = ((Label)this.gvTodayList.Rows[index].FindControl("lblempid")).Text.Trim();
            string jobid = ((Label)this.gvTodayList.Rows[index].FindControl("lbljobid")).Text.Trim();
            string taskDesc = ((Label)this.gvTodayList.Rows[index].FindControl("Lbltasktitle")).Text.Trim();
            string comcod = this.GetCompCode();
            string trackertype = "99217";
            string postdate = System.DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt");



            bool resultb = AIData.UpdateTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "INSERTUPDATE_STARTTASK", jobid, empid, postdate, trackertype, "0", "0", "Start", "", "0", "0");

            if (!resultb)
            {
                string msg = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail(" + msg + ");", true);
                return;
            }
            else
            {

                string msg = "Task Start ";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
                GetTodayDoingJob();
                this.GetTodayActivities();
                this.data_Bind();

            }


        }

        protected void lnkJObDone_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            int index = (this.gvTodayList.PageSize * this.gvTodayList.PageIndex) + rowIndex;
            string timeid = ((Label)this.gvTodayList.Rows[index].FindControl("lbltimetaskid")).Text.Trim();
            string empid = ((Label)this.gvTodayList.Rows[index].FindControl("lblempid")).Text.Trim();
            string jobid = ((Label)this.gvTodayList.Rows[index].FindControl("lbljobid")).Text.Trim();
            string taskDesc = ((Label)this.gvTodayList.Rows[index].FindControl("Lbltasktitle")).Text.Trim();
            string assign = ((Label)this.gvTodayList.Rows[index].FindControl("lblwrkassignqty")).Text.Trim();
            string pending = ((Label)this.gvTodayList.Rows[index].FindControl("lblwrpkdoneqty")).Text.Trim();
            this.hiddenlabel.Value = jobid;
            this.lblwrkassign.Text = assign;
            this.lblwrkdoneqty.Text = pending;
            this.donestatus.Text = "99220";
            this.lbltaskmodal.Text = "Task Complete Note";
            this.notetaskid.Text = timeid;
            this.Mdl_lblempid.Text = empid;
            this.Mdl_jobid.Text = jobid;
            this.holdreason.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HoldtaskNoteModal();", true);
        }

        protected void ddemplist_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnMyTasks_SelectedIndexChanged(null, null);

        }

        protected void tblworkedit_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
                int index = row.RowIndex;
                string id = ((Label)this.gvActivities.Rows[index].FindControl("lblid")).Text.Trim();
                string title = ((Label)this.gvActivities.Rows[index].FindControl("Lbltasktitle")).Text.Trim();
                string date = ((Label)this.gvActivities.Rows[index].FindControl("tblcreatedate")).Text.Trim();
                string qty = ((Label)this.gvActivities.Rows[index].FindControl("lblacvvelocityqty")).Text.Trim();
                string doneqty = ((Label)this.gvActivities.Rows[index].FindControl("lbldoneqty")).Text.Trim();
                this.lblactiviesid.Text = id;
                this.tbljobname.Text = title;
                this.tblstratdate.Text = date;
                this.tblassignqty.Text = qty;
                this.tbldoneqtyac.Text = doneqty;



                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "activiteseditModal();", true);

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }
        }



        protected void gvActivities_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string type = Request.QueryString["Type"].ToString();

                //int rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                //int index = (this.gvTodayList.PageSize * this.gvTodayList.PageIndex) + rowIndex;

                if (type == "MGT")
                {
                    //this.tblworkedit.Visible = true;
                    LinkButton HyplnkModal = (LinkButton)e.Row.FindControl("tblworkedit");
                    HyplnkModal.Visible = true;


                }
            }

        }

        protected void btlactivitesupdate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string comcod = this.GetCompCode();
                string id = this.lblactiviesid.Text;
                string track = this.ddlstatusupdate.SelectedValue;

                bool result = AIData.UpdateTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "ACTIVEWORKUPDATE", id, track, "", "", "", "", "", "", "", "");

                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('Update Fail..!!');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('Update  Successfully');", true);
                this.GetRecentAssigned();
                this.GetTodayDoingJob();
                this.GetTodayActivities();

            }
            catch (Exception exp)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + exp.Message.ToString() + "');", true);

            }

        }
    }
}