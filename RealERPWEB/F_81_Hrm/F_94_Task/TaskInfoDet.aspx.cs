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
using System.IO;
using Microsoft.Office.Interop.Excel;
using RealERPLIB;
using RealERPRPT;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using AjaxControlToolkit;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

//using Newtonsoft.Json;
using ListBox = System.Web.UI.WebControls.ListBox;
namespace RealERPWEB.F_81_Hrm.F_94_Task
{
    public partial class TaskInfoDet : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.MultiView1.ActiveViewIndex = 0;
                getDeptCode();
                getTask();
                CreateTable();
                txtfmdt1.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                txttodt1.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
                AllDaata();
            }
        }

        private void getDeptCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string empid = hst["empid"].ToString() + "%";
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETDEPTDATAENTRY", userid, empid, "", "", "", "", "", "", "", "");
            ddldept1.DataTextField = "deptdesc";
            ddldept1.DataValueField = "deptid";
            ddldept1.DataSource = ds1.Tables[1];
            ddldept1.DataBind();
            ddlEmp.DataSource = ds1.Tables[0];
            ddlEmp.DataTextField = "empdesc";
            ddlEmp.DataValueField = "empid";
            ddlEmp.DataBind();

        }


        private void getTask()
        {
            string comcod = this.GetCompCode();
            string deptcode = ddldept1.SelectedValue.ToString();
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETTASK", deptcode, "", "", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];
            if (ds1.Tables[0].Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["comcod"] = comcod;
                dr["taskcode"] = "00000";
                dr["taskdesc"] = "Please Add Task To Continue";
                dt.Rows.Add(dr);

            }
            ddltask.DataTextField = "taskdesc";
            ddltask.DataValueField = "taskcode";
            ddltask.DataSource = dt;
            ddltask.DataBind();
        }

        private void getDDLoc()
        {
            string comcod = this.GetCompCode();
            string deptcode = ddldept1.SelectedValue.ToString();
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTPRCODEDETAIL", "17",
                             "5", "", "", "", "", "", "", "");
            DataTable dt = ds1.Tables[0];

            DataView dv = dt.Copy().DefaultView;
            dv.RowFilter = ("prgcod not like '17000%'");

            ddlfloc.DataTextField = "prgdesc";
            ddlfloc.DataValueField = "prgcod";
            ddlfloc.DataSource = dv.ToTable();
            ddlfloc.DataBind();
            ddltloc.DataTextField = "prgdesc";
            ddltloc.DataValueField = "prgcod";
            ddltloc.DataSource = dv.ToTable();
            ddltloc.DataBind();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }




        private void CreateTable()
        {
            DataTable mnuTbl1 = new DataTable();
            mnuTbl1.Columns.Add("comcod", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("taskcode", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("taskdesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("deptcode", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("deptdesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("assigby", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("bydesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("assigto", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("todesc", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("status", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("entrydat", Type.GetType("System.String"));
            mnuTbl1.Columns.Add("duedat", Type.GetType("System.String"));
            Session["storedata"] = mnuTbl1;

        }
        private void AllDaata()
        {

            string comcod = this.GetCompCode();
            string deptcode = ddldept1.SelectedValue.ToString();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string usertype = hst["usrrmrk"].ToString();

            string Empid = (ddlEmp.SelectedValue == "000000000000") ? "%" : ddlEmp.SelectedValue.ToString();
            //if (usertype == "admin")
            //{
            //    Empid = "%";
            //}
            string empid = ddlEmp.SelectedValue.ToString();
            string ffate = this.txtfmdt1.Text;
            string ttdate = this.txttodt1.Text;
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETTASKASSIGNALLDATA", Empid, "%", ffate, ttdate, "%", "", "", "", "", "");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                gvShowData.DataSource = null;
                gvShowData.DataBind();
            }
            else
            {
                Session["allView"] = ds1.Tables[0];
                gvShowData_DataBind();
            }

        }




        protected void gvShowData_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["allView"];
            this.gvShowData.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvShowData.DataSource = tbl1;
            this.gvShowData.DataBind();

        }


        protected void btnOk_Click(object sender, EventArgs e)
        {
            string time = this.txtdateentry.Text;
            string cdate = Convert.ToDateTime(this.fdate.Text).ToString("dd-MMM-yyyy HH:mm:ss");// Convert.ToDateTime((time + " " + ddlhour.SelectedValue.ToString()
                                                                                                //+ ":" + ddlMmin.SelectedValue.ToString() + " " + ddlslb.SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");

            string tdate = Convert.ToDateTime(this.tdate.Text).ToString("dd-MMM-yyyy HH:mm:ss");// Convert.ToDateTime((time + " " + ddlhourT.SelectedValue.ToString()
                                                                                                //+ ":" + ddlMminT.SelectedValue.ToString() + " " + ddlslbT.SelectedValue.ToString())).ToString("dd-MMM-yyyy HH:mm:ss");

            Hashtable hst = (Hashtable)Session["tblLogin"];
            //this.ddlEmp.SelectedValue.ToString();  
            //if (this.btnOk.Text == "Update")
            //{
            //    empcode = hst["empid"].ToString();
            //}
            string comcod = this.GetCompCode();
            string empcode = hst["empid"].ToString();// this.ddlEmp.SelectedValue.ToString();
            string taskcode = this.ddltask.SelectedValue.ToString();
            string tdesc = this.txttaskdesc.Text;
            string floctn = ddlfloc.SelectedValue.ToString();
            string tloctn = ddltloc.SelectedValue.ToString();
            string ftime = cdate;
            string ttime = tdate;
            string rmk = txtrem.Text;
            string rowid = lblrowid.Text;
            string msg = "";

            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "INSERTUPDATETKDATA", rowid, taskcode, tdesc, floctn, tloctn, ftime, ttime, rmk, empcode, "", "", "", "", "", "");
            if (result == false)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

                msg = "Update Fail";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }
            else
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
                AllDaata();
                this.MultiView1.ActiveViewIndex = 0;
            }

        }
        protected void ddldeptcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTask();

        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvShowData_DataBind();
        }
        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            AllDaata();
        }
        protected void lnkNewTask_Click(object sender, EventArgs e)
        {
            this.btnOk.Text = "Add Records";
            //ddlhour.SelectedValue = "9";
            //ddlhourT.SelectedValue = "9";
            //ddlMmin.SelectedValue = "0";
            //ddlMminT.SelectedValue = "0";
            this.txtdateentry.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            if (this.ddlEmp.SelectedValue.ToString() == "000000000000")
            {
                string msg1 = "Please Select Employee..!!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg1 + "');", true);
                return;
            }
            getTask();
            getDDLoc();
            this.txttaskdesc.Text = "";
            //this.txtffrmtime.Text = "";
            //this.txttotime.Text = "";       
            this.txtrem.Text = "";
            this.lblrowid.Text = "0";
            this.MultiView1.ActiveViewIndex = 1;
        }
        protected void lnkBack_Click(object sender, EventArgs e)
        {
            AllDaata();
            this.MultiView1.ActiveViewIndex = 0;

        }
        protected void gvShowData_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvShowData.Rows[index];



            }

        }
        protected void btnedit_Click(object sender, EventArgs e)
        {
            this.btnOk.Text = "Update";
            getDDLoc();
            GridViewRow row = (GridViewRow)(((LinkButton)sender).Parent.Parent);
            int index = row.RowIndex;
            string lbltaskcode = ((Label)gvShowData.Rows[index].FindControl("lbltaskcode")).Text;
            string fdate = ((Label)gvShowData.Rows[index].FindControl("lbfdate")).Text;
            string lblftime = ((Label)gvShowData.Rows[index].FindControl("lblftime")).Text;
            string lblttime = ((Label)gvShowData.Rows[index].FindControl("lblttime")).Text;
            string lbltdesc = ((Label)gvShowData.Rows[index].FindControl("lbltdesc")).Text;
            string lblfloctn = ((Label)gvShowData.Rows[index].FindControl("lblfloctncode")).Text;
            string lbltloctn = ((Label)gvShowData.Rows[index].FindControl("lbltloctncode")).Text;
            string lblrmk = ((Label)gvShowData.Rows[index].FindControl("lblrmk")).Text;
            string lblrowid = ((Label)gvShowData.Rows[index].FindControl("lblrowidA")).Text;
            string empid = ((Label)gvShowData.Rows[index].FindControl("lblempid")).Text;
            string time = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DateTime cdate = Convert.ToDateTime((fdate + " " + lblftime));

            DateTime tdate = Convert.ToDateTime((fdate + " " + lblttime));
            TimeSpan ts = tdate - cdate;
            lblempid.Text = empid;
            this.ddltask.SelectedValue = lbltaskcode;
            this.txttaskdesc.Text = lbltdesc;
            //this.ddlhour.SelectedValue = lblftime;
            //this.txttotime.Text = lblttime;


            //ddlhour.SelectedValue = (Convert.ToInt16(cdate.Hour) > 12) ? (Convert.ToInt16(cdate.Hour) - 12).ToString() : ((Convert.ToInt16(cdate.Hour) == 0) ? "12" : (cdate.Hour).ToString());
            //ddlhourT.SelectedValue = (Convert.ToInt16(tdate.Hour) > 12) ? (Convert.ToInt16(tdate.Hour) - 12).ToString() : ((Convert.ToInt16(tdate.Hour)==0)? "12":(tdate.Hour).ToString());
            //ddlMmin.SelectedValue = cdate.Minute.ToString();
            //ddlMminT.SelectedValue = tdate.Minute.ToString();
            //ddlslb.SelectedValue = (Convert.ToInt16(cdate.Hour) > 12)?"PM":"AM";
            //ddlslbT.SelectedValue = (Convert.ToInt16(tdate.Hour) > 12) ? "PM" : "AM";

            this.fdate.Text = cdate.ToString("yyyy-MM-ddTHH:mm");
            this.tdate.Text = tdate.ToString("yyyy-MM-ddTHH:mm");

            ddlfloc.SelectedValue = lblfloctn;

            //this.ddlslb.SelectedValue = lblftime.Substring(5, 2);
            ddltloc.SelectedValue = lbltloctn;
            //lblhr.Text = ts.Hours.ToString()+" hours";
            this.txtrem.Text = lblrmk;
            this.txtdateentry.Text = fdate;
            this.lblrowid.Text = lblrowid;
            this.MultiView1.ActiveViewIndex = 1;
        }
        protected void lnkLocCodeBook_Click(object sender, EventArgs e)
        {
            ShowInformation();
            this.MultiView1.ActiveViewIndex = 2;
        }


        protected void grvacc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvacc.EditIndex = e.NewEditIndex;
            this.grvacc_DataBind();

        }
        protected void grvacc_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {


            DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
            if (!Convert.ToBoolean(dr1[0]["entry"]))
            {
                //this.ConfirmMessage.Visible = true;
                ((Label)this.Master.FindControl("lblmsg")).Text = "You have no permission";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                return;
            }
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string gcode1 = ((Label)grvacc.Rows[e.RowIndex].FindControl("lblgrcode")).Text.Trim();
            string gcode2 = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgrcode")).Text.Trim();

            string Desc = ((TextBox)grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string tgcod = gcode1.Substring(0, 2) + gcode2;
            string gdesc = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvDesc")).Text.Trim();
            string gtype = ((TextBox)this.grvacc.Rows[e.RowIndex].FindControl("txtgvttpe")).Text.Trim();

            string Gtype = (gtype.ToString() == "") ? "T" : gtype;
            string msg = "";



            bool result = da.UpdateTransInfo(comcod, "SP_ENTRY_CODEBOOK", "INSERTUPPRINF", tgcod,
                           gdesc, Gtype, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == true)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Updated ";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                msg = "Updated Successfully ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }

            else
            {
                msg = "Update Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            this.ShowInformation();
        }

        protected void grvacc_DataBind()
        {
            try
            {

                DataTable tbl1 = (DataTable)Session["storedata"];
                this.grvacc.DataSource = tbl1;
                this.grvacc.DataBind();
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = false;
                //double TotalPage = Math.Ceiling(tbl1.Rows.Count * 1.00 / this.grvacc.PageSize);
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Clear();
                //for (int i = 1; i <= TotalPage; i++)
                //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Items.Add("Page: " + i.ToString() + " of " + TotalPage.ToString());
                //if (TotalPage > 1)
                //    ((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).Visible = true;
                //((DropDownList)this.grvacc.FooterRow.FindControl("ddlPageNo")).SelectedIndex = this.grvacc.PageIndex;



            }
            catch (Exception ex)
            {
            }

        }

        private void ShowInformation()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string tempddl1 = "17";
            string tempddl2 = "5";
            DataSet ds1 = this.da.GetTransInfo(comcod, "SP_ENTRY_CODEBOOK", "OACCOUNTPRCODEDETAIL", tempddl1,
                            tempddl2, "", "", "", "", "", "", "");
            Session["storedata"] = ds1.Tables[0];

            this.grvacc.EditIndex = -1;
            this.grvacc_DataBind();


        }


        protected void lnkbacktoentry_Click(object sender, EventArgs e)
        {
            getDDLoc();
            this.MultiView1.ActiveViewIndex = 1;
        }
        protected void gvShowData_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usertype = hst["usrrmrk"].ToString();

                //if (usertype != "admin")
                //{
                //    LinkButton lnkedit = (LinkButton)e.Row.FindControl("btnedit");
                //    LinkButton lnkdelete = (LinkButton)e.Row.FindControl("lnkDelete");
                //    lnkedit.Visible = false;
                //    lnkdelete.Visible = false;

                //}
                string fromdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ftime")).ToString();
                string todate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ttime")).ToString();
                DateTime cdate = Convert.ToDateTime(fromdate);
                DateTime tdate = Convert.ToDateTime(todate);
                TimeSpan ts = tdate - cdate;
                Label lnkAct = (Label)e.Row.FindControl("lbldur");
                lnkAct.Text = ts.Hours.ToString() + "Hours" + "</br>" + ts.Minutes.ToString() + "Minutes";
            }
        }
        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)sender).Parent.Parent);
            int index = row.RowIndex;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string msg = "";
            string comcod = hst["comcod"].ToString();
            string lblrowid = ((Label)gvShowData.Rows[index].FindControl("lblrowidA")).Text;
            bool result = da.UpdateTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "DELETETKDATA", lblrowid,
                "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            if (result == true)
            {
                //((Label)this.Master.FindControl("lblmsg")).Text = " Successfully Deleted ";
                //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                msg = " Successfully Deleted ";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }

            else
            {

                msg = "Delete Failed";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
                return;
            }
            AllDaata();

        }
    }
}


