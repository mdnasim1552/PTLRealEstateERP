using RealERPLIB;
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
                ((Label)this.Master.FindControl("lblTitle")).Text = "My Tasks";
                btnMyTasks_SelectedIndexChanged(null, null);
            }
        }

        protected void btnMyTasks_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            string empid = hst["empid"].ToString();

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
            string empid = hst["empid"].ToString();

            string comcod = this.GetCompCode();
            empid = this.Request.QueryString["empid"].ToString() == "" ? empid : this.Request.QueryString["empid"].ToString();
            DataSet ds = AIData.GetTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "GETMYTASKPENDING", empid, "", "", "", "", "");
            if (ds == null)
                return;


            Session["tbltodaylist"] = ds.Tables[0];
            this.data_Bind();

        }

        private void data_Bind()
        {

            DataTable tbltodays = (DataTable)Session["tbltodaylist"];
            DataTable tblasing = (DataTable)Session["tblassinglist"];

            this.gvAssingJob.DataSource = tblasing;
            this.gvAssingJob.DataBind();

            this.gvTodayList.DataSource = tbltodays;
            this.gvTodayList.DataBind();


        }

        protected void lnkStartJob_Click(object sender, EventArgs e)
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
            string assignuser = this.Request.QueryString["empid"].ToString() == "" ? empid : this.Request.QueryString["empid"].ToString();

            string trackertype = "99204";// task wip
            string doneqty = "0";
            string skipqty = "0";
            string remarks = "";

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int index = this.gvAssingJob.PageSize * this.gvAssingJob.PageIndex + RowIndex;
            string comcod = this.GetCompCode();
            string jobid = ((Label)this.gvAssingJob.Rows[RowIndex].FindControl("lbljobid")).Text.Trim();
            bool resultb = AIData.UpdateTransInfo(comcod, "dbo_ai.SP_INTERFACE_AI", "INSERTUPDATE_STARTTASK", jobid, assignuser, cdate, trackertype, doneqty, skipqty, remarks);
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
            }
        }
    }
}