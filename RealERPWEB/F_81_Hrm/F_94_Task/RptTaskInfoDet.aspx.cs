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
using System.Linq;


//using Newtonsoft.Json;
using ListBox = System.Web.UI.WebControls.ListBox;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_81_Hrm.F_94_Task
{

    public partial class RptTaskInfoDet : System.Web.UI.Page
    {
        ProcessAccess da = new ProcessAccess();
        public List<TimeSpan> list = new List<TimeSpan>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Task";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

                this.MultiView1.ActiveViewIndex = 0;
                getDeptCode();
                getEmpcod();
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
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "DeptList", userid, "%", "", "", "", "", "", "", "", "");
            ddldept1.DataTextField = "deptdesc";
            ddldept1.DataValueField = "deptid";
            ddldept1.DataSource = ds1.Tables[0];
            ddldept1.DataBind();
            //ddlEmp.DataSource = ds1.Tables[0];
            //ddlEmp.DataTextField = "empdesc";
            //ddlEmp.DataValueField = "empid";
            //ddlEmp.DataBind();

        }

        private void getEmpcod()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string userid = hst["usrid"].ToString();
            string deptid = ddldept1.SelectedValue.ToString() + "%";
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "RptEmpOnDeptList", userid, deptid, "", "", "", "", "", "", "", "");
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
            else
            {
                DataRow drA = dt.NewRow();
                drA["comcod"] = comcod;
                drA["taskcode"] = "00000";
                drA["taskdesc"] = "All Task";
                dt.Rows.Add(drA);

            }
            ddltask.DataTextField = "taskdesc";
            ddltask.DataValueField = "taskcode";
            ddltask.DataSource = dt;
            ddltask.DataBind();
            ddltask.SelectedValue = "00000";
        }



        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
           // DataTable dt = (DataTable)Session["tblover"];
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();          
            //string bankname = this.ddlBankName.SelectedItem.Text.Trim();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            // string year = this.txtDate.Text.Substring(0, 4).ToString();
            // string month = ASITUtility03.GetFullMonthName(this.txtDate.Text.Substring(4));
            // string printtype = ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString();
            DataTable dt = (DataTable)Session["allView"];
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_Mgt.EClassHrInterface.EmptaskDesk>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_94_Task.RptTaskInfoDet", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comname));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Task Info Dept" ));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
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
            string deptid = this.ddldept1.SelectedValue.ToString();
            string Empid = (ddlEmp.SelectedValue == "000000000000") ? "%" : ddlEmp.SelectedValue.ToString();
            string taskcode = (ddltask.SelectedValue == "00000" ? "%" : ddltask.SelectedValue.ToString());
            string userid = hst["usrid"].ToString();
            //if (usertype == "admin")
            //{
            //    Empid = "%";
            //}
            //else
            //{
            //    //teamLeader = hst[""].ToString();
            //}
            string empid = ddlEmp.SelectedValue.ToString();
            string ffate = this.txtfmdt1.Text;
            string ttdate = this.txttodt1.Text;
            DataSet ds1 = da.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMP_INTERFACE", "GETTASKASSIGNALLDATA", Empid, deptid, ffate, ttdate, taskcode, userid, "", "", "", "");
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


        protected void gvShowData_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Hashtable hst = (Hashtable)Session["tblLogin"];
                string usertype = hst["usrrmrk"].ToString();
                string fromdate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ftime")).ToString();
                string todate = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "ttime")).ToString();
                DateTime cdate = Convert.ToDateTime(fromdate);
                DateTime tdate = Convert.ToDateTime(todate);
                TimeSpan ts = tdate - cdate;
                Label lnkAct = (Label)e.Row.FindControl("lbldur");
                lnkAct.Text = ts.Hours.ToString() + "Hr" + ts.Minutes.ToString() + "Min";
                list.Add(ts);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //var value = list.Sum(p => p.TotalHours).ToString();
                Label lblamount = (Label)e.Row.FindControl("lblttldur");
                //lblamount.Text = value;//value.Hours.ToString() + "Hr" + value.Minutes.ToString() + "Min";
                //lblamount.Text = "";//value.Hours.ToString() + "Hr" + value.Minutes.ToString() + "Min";


                double value2 = Convert.ToDouble(list.Sum(p => p.TotalMinutes));

                double value3 = value2 == 0 ? 0 : value2 / 60;
                double value4 = value2 == 0 ? 0 : value2 % 60;
                lblamount.Text = value3.ToString("#,##0;(#,##0); ") + "Hr" + value4.ToString("#,##0;(#,##0); ") + "Min";     //value3.Hours.ToString() + "Hr" + value.Minutes.ToString() + "Min";
                 
            }

        }

        protected void gvShowData_DataBind()
        {
            DataTable tbl1 = (DataTable)Session["allView"];
            this.gvShowData.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvShowData.DataSource = tbl1;
            this.gvShowData.DataBind();
            Session["Report1"] = gvShowData;
            ((HyperLink)this.gvShowData.HeaderRow.FindControl("hlbtntbCdataExel")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
        }



        protected void ddldeptcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTask();
            getEmpcod();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvShowData_DataBind();
        }
        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            AllDaata();
        }

    }
    public static class LongExtensions
    {
        public static TimeSpan ToTimeSpan(this long ticks) { return new TimeSpan(ticks); }
    }
}
