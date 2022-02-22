using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Diagnostics;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.IO;
using System.Net.Mail;
using System.Web.Mail;
using RealERPLIB;
using RealERPRPT;
using System.Net;
using EASendMail;
using System.IO;
using System.Drawing;
using AjaxControlToolkit;
using RealEntity;
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class MyLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                this.ShowView();
                this.GetEmpid();

                this.GetCompany();
                ((Label)this.Master.FindControl("lblTitle")).Text = "ONLINE LEAVE APPLICATION";
                this.GetProjectName();
                //this.GetEmpid();
                this.lbtnOk_Click(null, null);

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string userid = hst["usrid"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAMEFL", txtCompany, "", "", "", "", "", "", "", ""); //Edit by Emdad 9.17.2020
                                                                                                                                                    // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        protected void imgbtnEmpSeach_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }

        private void ShowView()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            string comcod = this.GetComeCode();

            ((Label)this.Master.FindControl("lblTitle")).Text = "LEAVE APPLICATION VIEW/EDIT";
            this.rblstapptype.SelectedIndex = 0;
            this.rblstapptype.SelectedIndex = 0;
            this.Label5.Visible = false;
            this.txtdate.Visible = false;
            this.lblPage.Visible = false;
            this.ddlpagesize.Visible = false;
            this.lblEmpCode.Visible = false;
            this.txtEmpSearch.Visible = false;
            this.imgbtnEmpSeach.Visible = false;


        }
        private void GetProjectName()
        {
            DataTable dt = (DataTable)ViewState["tblEmpid"];
            string comcod = this.GetComeCode();
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAMEFL", txtSProject, company, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "deptname";
            this.ddlProjectName.DataValueField = "deptid";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            // ddlProjectName.SelectedValue = dt.Rows[0]["refno"].ToString();

        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            this.ShowLeaveApp();
        }



        private void ShowLeaveApp()
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                this.lblleaveApp.Visible = true;
                this.lblleaveStatus.Visible = true;
                this.lblleaveInformation.Visible = true;
                this.PnlEmp.Visible = true;
                this.Pnlapply.Visible = true;
                this.PnlRmrks.Visible = true;
                this.lbtnOk.Text = "New";
                this.txtaplydate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.txtApprdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.GetLeaveid();
                this.imgbtnlAppEmpSeaarch_Click(null, null);
                //this.imgbtnlFEmpSeaarch_Click(null, null);
            }
            else
            {

                this.lbtnOk.Text = "Ok";
                this.ddlCompany.Enabled = true;
                this.ddlProjectName.Enabled = true;
                this.PnlEmp.Visible = false;
                this.Pnlapply.Visible = false;
                this.PnlRmrks.Visible = false;
                this.lblleaveApp.Visible = false;
                this.lblleaveStatus.Visible = false;
                this.lblleaveInformation.Visible = false;
                this.ddlEmpName.Items.Clear();

                this.lblComPany.Text = "";
                this.lblSection.Text = "";
                this.lblDesignation.Text = "";
                this.lblJoiningDate.Text = "";
                this.txtaplydate.Text = "";
                this.txtLeavLreasons.Text = "";
                this.txtaddofenjoytime.Text = "";
                this.txtLeavRemarks.Text = "";
                this.lmsg11.Text = "";
                this.gvLeaveApp.DataSource = null;
                this.gvLeaveApp.DataBind();
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
            }
        }

        private void ShowValue()
        {
            Session.Remove("YearLeav");

            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string yearid = this.txtdate.Text;
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string empcode = this.txtEmpSearch.Text.Trim() + "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPLEAVE", yearid, pactcode, company, empcode, "", "", "", "", "");
            if (ds4 == null)
            {
                //this.gvLeaveRule.DataSource = null;
                //this.gvLeaveRule.DataBind();
                //return;
            }
            DataTable dt = HiddenSameData(ds4.Tables[0]);
            Session["YearLeav"] = dt;

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["secname"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }

        //private void SaveValue()
        //{
        //    DataTable dt = (DataTable)Session["YearLeav"];
        //    int TblRowIndex;
        //    for (int i = 0; i < this.gvLeaveRule.Rows.Count; i++)
        //    {

        //        string ernleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvel")).Text.Trim()).ToString();
        //        string csleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvcl")).Text.Trim()).ToString();
        //        string skleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvsl")).Text.Trim()).ToString();
        //        string mtleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvml")).Text.Trim()).ToString();
        //        string wpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvWPl")).Text.Trim()).ToString();
        //        string trpleave = Convert.ToDouble("0" + ((TextBox)this.gvLeaveRule.Rows[i].FindControl("txtgvTrL")).Text.Trim()).ToString();
        //        TblRowIndex = (gvLeaveRule.PageIndex) * gvLeaveRule.PageSize + i;

        //        dt.Rows[TblRowIndex]["ernleave"] = ernleave;
        //        dt.Rows[TblRowIndex]["csleave"] = csleave;
        //        dt.Rows[TblRowIndex]["skleave"] = skleave;
        //        dt.Rows[TblRowIndex]["mtleave"] = mtleave;
        //        dt.Rows[TblRowIndex]["wpleave"] = wpleave;
        //        dt.Rows[TblRowIndex]["trpleave"] = trpleave;

        //    }
        //    Session["YearLeav"] = dt;
        //}


        //protected void gvLeaveRule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    this.SaveValue();
        //    this.gvLeaveRule.PageIndex = e.NewPageIndex;
        //    this.LoadGrid();
        //}

        //protected void lnkbtnFUpLeave_Click(object sender, EventArgs e)
        //{
        //   // this.SaveValue();
        //    DataTable dt = (DataTable)Session["YearLeav"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    string comcod = hst["comcod"].ToString();
        //    string yearid = this.txtdate.Text;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string empid = dt.Rows[i]["empid"].ToString();
        //        string ernid = dt.Rows[i]["ernid"].ToString();
        //        string ernleave = dt.Rows[i]["ernleave"].ToString();
        //        string csid = dt.Rows[i]["csid"].ToString();
        //        string csleave = dt.Rows[i]["csleave"].ToString();
        //        string skid = dt.Rows[i]["skid"].ToString();
        //        string skleave = dt.Rows[i]["skleave"].ToString();
        //        string mtid = dt.Rows[i]["mtid"].ToString();
        //        string mtleave = dt.Rows[i]["mtleave"].ToString();
        //        string wpid = dt.Rows[i]["wpid"].ToString();
        //        string wpleave = dt.Rows[i]["wpleave"].ToString();
        //        string trpid = dt.Rows[i]["trpid"].ToString();
        //        string trpleave = dt.Rows[i]["trpleave"].ToString();



        //        bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAV", yearid, empid, ernid, ernleave, csid, csleave, skid, skleave, mtid, mtleave, wpid, wpleave, trpid, trpleave, "");
        //        if (result == false)
        //        {
        //            ((Label)this.Master.FindControl("lblmsg")).Text = "Data Is Not Updated";

        //        }
        //        else
        //        {
        //            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
        //        }

        //    }


        //}
        //protected void lnkbtnGenLeave_Click(object sender, EventArgs e)
        //{
        //    string ernleave = Convert.ToDouble("0" + this.txternleave.Text).ToString();
        //    string csleave = Convert.ToDouble("0" + this.txtcsleave.Text).ToString();
        //    string skleave = Convert.ToDouble("0" + this.txtskleave.Text).ToString();
        //    string mtleave = Convert.ToDouble("0" + this.txtmtleave.Text).ToString();
        //    string wpleave = Convert.ToDouble("0" + this.txtWPayleave.Text).ToString();
        //    string trpleave = Convert.ToDouble("0" + this.txtTrainleave.Text).ToString();
        //    DataTable dt = (DataTable)Session["YearLeav"];
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dt.Rows[i]["ernleave"] = ernleave;
        //        dt.Rows[i]["csleave"] = csleave;
        //        dt.Rows[i]["skleave"] = skleave;
        //        dt.Rows[i]["mtleave"] = mtleave;
        //        dt.Rows[i]["wpleave"] = wpleave;
        //        dt.Rows[i]["trpleave"] = trpleave;

        //    }
        //    Session["YearLeav"] = dt;
        //    this.chkLeave.Checked = false;
        //    this.chkLeave_CheckedChanged(null, null);
        //    this.LoadGrid();



        //}
        //protected void chkLeave_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chkLeave.Checked == true)
        //    {
        //        this.pnlleave.Visible = true;
        //        this.txtcsleave.Text = "";
        //        this.txternleave.Text = "";
        //        this.txtskleave.Text = "";
        //        this.txtmtleave.Text = "";
        //        this.txtWPayleave.Text = "";
        //    }
        //    else
        //    {
        //        this.pnlleave.Visible = false;
        //    }
        //}




        private void GetEmployeeName()
        {

            Session.Remove("tblEmpDesc");
            Session.Remove("tblleave");
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            //string IdCardNo = "%" + this.txtlAppEmpSearch.Text.Trim() + "%";
            string empid = this.Request.QueryString["Type"].ToString() == "User" ? this.lblempid.Text : "%%";
            string deptcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(), 7) + "00000%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETPROJECTWSEMPNAME01", pactcode, company, empid, deptcode, "", "", "", "", "");
            if (ds1 == null)
                return;
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds1.Tables[0];
            this.ddlEmpName.DataBind();

            // this.ddlEmpName.Enabled = false;
            Session["tblEmpDesc"] = ds1.Tables[0];

            this.gvLeaveApp.DataSource = ds1.Tables[1];
            this.gvLeaveApp.DataBind();
            Session["tblleave"] = ds1.Tables[1];
            this.ddlEmpName_SelectedIndexChanged(null, null);
            // Session.Remove("tblEmpDesc");
            // Session.Remove("tblleave");
            // string comcod = this.GetComeCode();
            // string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            // string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            // string IdCardNo = "%" + this.txtlAppEmpSearch.Text.Trim() + "%";
            // //string empid = this.Request.QueryString["Type"].ToString() == "User" ? this.lblempid.Text : "%%";
            // string deptcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" :ASTUtility.Left(this.ddlProjectName.SelectedValue.ToString(),7) + "00000%";

            // DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", pactcode, company, IdCardNo, "", "", "", "", "", "");
            // if (ds1 == null)
            //     return;
            // this.ddlEmpName.DataTextField = "empname";
            // this.ddlEmpName.DataValueField = "empid";
            // this.ddlEmpName.DataSource = ds1.Tables[0];
            // this.ddlEmpName.DataBind();

            //// this.ddlEmpName.Enabled = false;
            // Session["tblEmpDesc"] = ds1.Tables[0];

            // this.gvLeaveApp.DataSource = ds1.Tables[1];
            // this.gvLeaveApp.DataBind();
            // Session["tblleave"] = ds1.Tables[1];
            // this.ddlEmpName_SelectedIndexChanged(null, null);
        }

        private void GetLeaveid()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLEAVEID", "", "", "", "", "", "", "", "", "");
            this.lbltrnleaveid.Text = ds5.Tables[0].Rows[0]["ltrnid"].ToString().Trim();
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowEmppLeave();
            this.EmpLeaveInfo();
            this.RefreshLeave();

        }

        private void RefreshLeave()
        {
            string comcod = this.GetComeCode();
            string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
            string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string IdCardNo = "%" + this.txtlAppEmpSearch.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", pactcode, company, IdCardNo, "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.gvLeaveApp.DataSource = ds1.Tables[1];
            this.gvLeaveApp.DataBind();

        }

        private void ShowEmppLeave()
        {

            this.txtLeavLreasons.Text = "";
            this.txtLeavRemarks.Text = "";
            Session.Remove("tblleavest");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblEmpDesc"];
            DataRow[] dr1 = dt.Select("empid='" + empid + "'");

            if (dr1.Length > 0)
            {
                this.lblComPany.Text = dr1[0]["companyname"].ToString();
                this.lblSection.Text = dr1[0]["section"].ToString();
                this.lblDesignation.Text = dr1[0]["desig"].ToString();
                this.lblJoiningDate.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");


            }

            //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }

            Session["tblleavest"] = ds1.Tables[0];
            this.Data_Bind();

        }


        private void EmpLeaveInfo()
        {
            ViewState.Remove("tblempleaveinfo");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string aplydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVEINFORMATION", empid, aplydat, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                return;
            }
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                this.gvleaveInfo.DataSource = null;
                this.gvleaveInfo.DataBind();
                return;
            }
            string gcod = dt1.Rows[0]["gcod"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["gcod"].ToString() == gcod)
                    dt1.Rows[j]["gdesc"] = "";
                gcod = dt1.Rows[j]["gcod"].ToString();
            }
            ViewState["tblempleaveinfo"] = dt1;
            this.gvleaveInfo.DataSource = dt1;
            this.gvleaveInfo.DataBind();
            ds1.Dispose();
        }

        protected void lnkbtnRef_Click(object sender, EventArgs e)
        {
            this.ShowEmppLeave();
        }
        private void Data_Bind()
        {
            this.gvLeaveStatus.DataSource = (DataTable)Session["tblleavest"];
            this.gvLeaveStatus.DataBind();
        }


        private void SaveLeave()
        {
            this.lblleaveApp.Visible = true;
            DataTable dt = (DataTable)Session["tblleave"];
            DataTable dt1 = (DataTable)Session["tblleavest"];
            for (int i = 0; i < this.gvLeaveApp.Rows.Count; i++)
            {
                //TimeSpan ts = (this.CalExt3.SelectedDate.Value - this.CalExt2.SelectedDate.Value);
                //int leaveday = Convert.ToInt32("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim());
                double leaveday = Convert.ToDouble(ASTUtility.ExprToValue("0" + ((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim()));
                double leaveday1 = Math.Ceiling(leaveday);

                //int leaveday=ASTUtility.StrPosOrNagative(((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvlapplied")).Text.Trim())
                if (leaveday > 0)
                {
                    string stdat = Convert.ToDateTime(((TextBox)this.gvLeaveApp.Rows[i].FindControl("txtgvenjoydt1")).Text.Trim()).ToString("dd-MMM-yyyy");
                    string endat = Convert.ToDateTime(stdat).AddDays(leaveday1 - 1).ToString("dd-MMM-yyyy");
                    dt.Rows[i]["lapplied"] = leaveday;
                    dt.Rows[i]["lenjoydt1"] = stdat;
                    dt.Rows[i]["lenjoydt2"] = endat;
                    double enjleave = Convert.ToDouble(dt1.Rows[i]["ltaken"]);
                    double Clsleave = Convert.ToDouble(dt1.Rows[i]["pbal"]);
                    dt1.Rows[i]["applyday"] = leaveday;
                    dt1.Rows[i]["appday"] = leaveday;
                    dt1.Rows[i]["applydate"] = stdat;
                    dt1.Rows[i]["appdate"] = stdat;
                    //dt1.Rows[i]["todate"] = endat;
                    //dt1.Rows[i]["pbal"] = Convert.ToInt32(dt1.Rows[i]["pbal"]) - leaveday;
                    //dt1.Rows[i]["ltaken"] = Convert.ToInt32(dt1.Rows[i]["ltaken"]) + leaveday;
                    dt1.Rows[i]["balleave"] = Clsleave - leaveday;
                    dt1.Rows[i]["tltakreq"] = leaveday;


                }
            }

            this.gvLeaveApp.DataSource = dt;
            this.gvLeaveApp.DataBind();
            Session["tblleave"] = dt;


            //---------For Status table --------------------------------------

            Session["tblleavest"] = dt1;
            this.Data_Bind();
        }

        //private void GetLveAppEmployeeName()
        //{
        //    Session.Remove("tblEmpDesc");
        //    string comcod = this.GetComeCode();
        //    string company = (this.ddlCompany.SelectedValue.Substring(0, 2).ToString() == "00") ? "%" : this.ddlCompany.SelectedValue.Substring(0, 2).ToString() + "%";
        //    string pactcode = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
        //    //string IdCardNo = "%" + this.txtlFEmpSearch.Text.Trim() + "%";
        //    //string date = Convert.ToDateTime(this.txtformdate.Text).ToString("dd-MMM-yyyy");
        //    string empid = this.Request.QueryString["Type"].ToString() == "User" ? this.lblempid.Text : "%%";

        //    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTWSEMPNAME", company, pactcode, IdCardNo, "", "", "", "", "", "");
        //    if (ds1 == null)
        //        return;
        //    this.ddlEmpNamelApp.DataTextField = "empname";
        //    this.ddlEmpNamelApp.DataValueField = "empid";
        //    this.ddlEmpNamelApp.DataSource = ds1.Tables[0];
        //    this.ddlEmpNamelApp.DataBind();
        //    Session["tblEmpDesc"] = ds1.Tables[0];
        //    this.ddlEmpNamelApp_SelectedIndexChanged(null, null);



        //}


        //protected void ddlEmpNamelApp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session.Remove("tblleavest");

        //    string comcod = this.GetComeCode();
        //    string empid = this.ddlEmpNamelApp.SelectedValue.ToString();
        //    string aplydat = Convert.ToDateTime(this.txtformdate.Text).ToString("dd-MMM-yyyy");

        //    DataTable dt = (DataTable)Session["tblEmpDesc"];
        //    DataRow[] dr1 = dt.Select("empid='" + empid + "'");

        //    if (dr1.Length > 0)
        //    {

        //        this.lblComPanylApp.Text = dr1[0]["companyname"].ToString();
        //        this.lblSectionlApp.Text = dr1[0]["section"].ToString();
        //        this.lblDesignationlApp.Text = dr1[0]["desig"].ToString();
        //        this.lblJoiningDatelApp.Text = Convert.ToDateTime(dr1[0]["joindate"]).ToString("dd-MMM-yyyy");


        //    }


        //    //string calltype = ((this.rblstFormType.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstFormType.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
        //    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, aplydat, "", "", "", "", "", "", "");
        //    if (ds1 == null)
        //    {
        //        this.gvLeaveStatus01.DataSource = null;
        //        this.gvLeaveStatus01.DataBind();
        //        return;
        //    }

        //    this.gvLeaveStatus01.DataSource = ds1.Tables[0];
        //    this.gvLeaveStatus01.DataBind();
        //    Session["tblleavest"] = ds1.Tables[0];


        //}

        protected void lnkbtnPreLeave_Click(object sender, EventArgs e)
        {
            this.PnlPreLeave.Visible = false;
            this.chkPreLeave.Checked = false;
            this.PreLeaveInfo();
            this.chkPreLeave_CheckedChanged(null, null);
        }
        private void PreLeaveInfo()
        {
            Session.Remove("tblleavest");
            DataTable dt = (DataTable)ViewState["tblprelinf"];
            string ltrnid = this.ddlPreLeave.SelectedValue.ToString();
            DataRow[] drp = dt.Select("ltrnid='" + ltrnid + "'");
            if (dt.Rows.Count == 0)
                return;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(drp[0]["strtdat"]).ToString("dd-MMM-yyyy");
            //string calltype = ((this.rblstapptype.SelectedIndex == 0) ? "LEAVESTATUS" : (this.rblstapptype.SelectedIndex == 1) ? "LEAVESTATUS01" : "LEAVESTATUS02");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "LEAVESTATUS02", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.gvLeaveStatus.DataSource = null;
                this.gvLeaveStatus.DataBind();
                return;
            }
            // Session["tblleavest"] = ds1.Tables[0];
            DataTable dt1 = (DataTable)Session["tblleave"];
            DataTable dt2 = ds1.Tables[0];


            string gcod = drp[0]["gcod"].ToString();
            DataRow[] drl = dt1.Select("gcod='" + gcod + "'");
            DataRow[] drls = dt2.Select("gcod='" + gcod + "'");

            //leave-------
            drl[0]["lapplied"] = drp[0]["lapplied"];
            drl[0]["lenjoydt1"] = drp[0]["strtdat"];
            drl[0]["lenjoydt2"] = drp[0]["enddat"];
            //leave status-------
            double leaveday = Convert.ToDouble(drp[0]["lapplied"].ToString());
            double enjleave = Convert.ToDouble(drls[0]["ltaken"]);
            double Clsleave = Convert.ToDouble(drls[0]["pbal"]);
            drls[0]["applyday"] = drp[0]["lapplied"];
            drls[0]["appday"] = drp[0]["lapplied"];
            drls[0]["applydate"] = drp[0]["strtdat"];
            drls[0]["appdate"] = drp[0]["strtdat"];
            // drls[0]["todate"] = drp[0]["strtdat"];

            drls[0]["balleave"] = Clsleave - leaveday;
            drls[0]["tltakreq"] = leaveday;
            //drls[0]["balleave"] = Clsleave - (leaveday + enjleave);
            //drls[0]["tltakreq"] = (leaveday + enjleave);

            Session["tblleave"] = dt1;
            Session["tblleavest"] = dt2;
            //Genral info
            this.lbltrnleaveid.Text = this.ddlPreLeave.SelectedValue.ToString();
            this.txtaplydate.Text = Convert.ToDateTime(drp[0]["aplydat"]).ToString("dd-MMM-yyyy");
            // this.txtApprdate.Text = Convert.ToDateTime(drp[0]["aprdat"]).ToString("dd-MMM-yyyy");
            this.txtLeavLreasons.Text = drp[0]["lreason"].ToString();
            this.txtaddofenjoytime.Text = drp[0]["addlentime"].ToString();
            this.txtLeavRemarks.Text = drp[0]["lrmarks"].ToString();
            this.txtdutiesnameandDesig.Text = drp[0]["denameadesig"].ToString();

            //gvbind
            this.gvLeaveApp.DataSource = dt1;
            this.gvLeaveApp.DataBind();
            this.Data_Bind();
        }


        protected void chkPreLeave_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPreLeave.Checked)
            {
                this.PnlPreLeave.Visible = true;
                this.PreLeaveno();
            }
            else
            {
                this.PnlPreLeave.Visible = false;
            }

        }
        private void PreLeaveno()
        {

            ViewState.Remove("tblprelinf");
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string date = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "PREVIOUSLEAVENO", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
            {
                this.ddlPreLeave.Items.Clear();
                return;
            }
            ViewState["tblprelinf"] = ds1.Tables[0];
            this.ddlPreLeave.DataTextField = "ltrndesc";
            this.ddlPreLeave.DataValueField = "ltrnid";
            this.ddlPreLeave.DataSource = ds1.Tables[0];
            this.ddlPreLeave.DataBind();

        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "User":
                    this.PrintLeaveApprove();
                    break;



            }


        }
        private void PrintLeaveApprove()
        {
            // if (this.rblstapptype.SelectedIndex == 0 || this.rblstapptype.SelectedIndex == 2)
            //{
            //    this.PrintAppform1();

            //}
            //else 
            //{
            //    this.PrintAppform2();

            //}

            string comcod = this.GetComeCode();

            switch (comcod)
            {
                //case "4101":
                //    this.PrintAppform2();
                //    break;
                case "4301":

                    this.PrintAppform2();
                    break;

                case "4101":
                case "4305":
                case "4315":
                    //case "3101":
                    this.PrintAppform13();
                    break;
                default:

                    this.PrintAppform1();
                    break;




            }



        }

        private void PrintAppform1()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("appday>0");
            DataTable dt = dv.ToTable();

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.EmpLeavApp", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("compAdd", comadd));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", this.lbltrnleaveid.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtleaveday", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days"));
            Rpt1.SetParameters(new ReportParameter("txtldatefrm", Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtldateto", Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtlday", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo1", this.lbltrnleaveid.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtEmpName1", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtDesig1", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtReasons", this.txtLeavLreasons.Text));
            Rpt1.SetParameters(new ReportParameter("txttitlelappslip", "Leave Approval Slip"));
            Rpt1.SetParameters(new ReportParameter("txtAppDays", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days " + dt.Rows[0]["gdesc"].ToString() + " from " + Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.EmpLeavApp();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRptCompAdd = rptTest.ReportDefinition.ReportObjects["txtRptCompAdd"] as TextObject;
            //txtRptCompAdd.Text = comadd;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = this.lbltrnleaveid.Text.Trim();
            //TextObject txtRecordNo1 = rptTest.ReportDefinition.ReportObjects["txtRecordNo1"] as TextObject;
            //txtRecordNo1.Text = this.lbltrnleaveid.Text.Trim();
            //TextObject txtleaveday = rptTest.ReportDefinition.ReportObjects["txtleaveday"] as TextObject;
            //txtleaveday.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days";

            //TextObject txtldatefrm = rptTest.ReportDefinition.ReportObjects["txtldatefrm"] as TextObject;
            //txtldatefrm.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            //TextObject txtldateto = rptTest.ReportDefinition.ReportObjects["txtldateto"] as TextObject;
            //txtldateto.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy");
            //TextObject txtlday = rptTest.ReportDefinition.ReportObjects["txtlday"] as TextObject;
            //txtlday.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ");

            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);
            //TextObject txtEmpName1 = rptTest.ReportDefinition.ReportObjects["txtEmpName1"] as TextObject;
            //txtEmpName1.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignation.Text;
            //TextObject txtDesig1 = rptTest.ReportDefinition.ReportObjects["txtDesig1"] as TextObject;
            //txtDesig1.Text = this.lblDesignation.Text;
            ////TextObject txtApprdate = rptTest.ReportDefinition.ReportObjects["txtApprdate"] as TextObject;
            ////txtApprdate.Text = Convert.ToDateTime(this.txtApprdate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");

            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = this.txtLeavLreasons.Text;
            //TextObject txttitlelappslip = rptTest.ReportDefinition.ReportObjects["txttitlelappslip"] as TextObject;
            //txttitlelappslip.Text = "Leave Approval Slip";
            //TextObject txtAppDays = rptTest.ReportDefinition.ReportObjects["txtAppDays"] as TextObject;
            //txtAppDays.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ") + " days " + dt.Rows[0]["gdesc"].ToString() + " from " + Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            //rptTest.SetDataSource(((DataTable)Session["tblleavest"]));

            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptTest.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion

        }
        private void PrintAppform2()
        {

            DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            dv.RowFilter = ("gcod='51001'");
            DataTable dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptHrEmpLeave02", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtRecordNo", this.lbltrnleaveid.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtleavedays", Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtApplydate", Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtlsdate", Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtledate", Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtReasons", this.txtLeavLreasons.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtAddofentime", this.txtaddofenjoytime.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("txtremarks", this.txtLeavRemarks.Text.Trim()));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Leave Application"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave02();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = this.lbltrnleaveid.Text.Trim();
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);

            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignation.Text;
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
            //TextObject txtlsdate = rptTest.ReportDefinition.ReportObjects["txtlsdate"] as TextObject;
            //txtlsdate.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).ToString("dd-MMM-yyyy");
            //TextObject txtledate = rptTest.ReportDefinition.ReportObjects["txtledate"] as TextObject;
            //txtledate.Text = Convert.ToDateTime(dt.Rows[0]["applydate"]).AddDays(Convert.ToInt32(dt.Rows[0]["appday"]) - 1).ToString("dd-MMM-yyyy");
            //TextObject txtleavedays = rptTest.ReportDefinition.ReportObjects["txtleavedays"] as TextObject;
            //txtleavedays.Text = Convert.ToInt32(dt.Rows[0]["appday"]).ToString("#,##0;(#,##0); ");
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = this.txtLeavLreasons.Text.Trim();
            //TextObject txtAddofentime = rptTest.ReportDefinition.ReportObjects["txtAddofentime"] as TextObject;
            //txtAddofentime.Text = this.txtaddofenjoytime.Text.Trim();
            //TextObject rpttxtremarks = rptTest.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //rpttxtremarks.Text = this.txtLeavRemarks.Text.Trim();

            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion
        }


        private void PrintAppform13()
        {
            DataTable dtcal = ((DataTable)Session["tblleavest"]).Copy();
            DataView dv = dtcal.DefaultView;
            dv.RowFilter = ("appday>0");
            DataTable dt = dv.ToTable();
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveAPP>();
            LocalReport Rpt1 = new LocalReport();
            Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.RptHrEmpLeave03", list, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("txtCompanyName", this.lblComPany.Text));
            Rpt1.SetParameters(new ReportParameter("txtEmpName", this.ddlEmpName.SelectedItem.Text.Substring(7)));
            Rpt1.SetParameters(new ReportParameter("txtDesig", this.lblDesignation.Text));
            Rpt1.SetParameters(new ReportParameter("txtDepartment", this.lblSection.Text));
            Rpt1.SetParameters(new ReportParameter("txtJoiningDate", this.lblJoiningDate.Text));
            Rpt1.SetParameters(new ReportParameter("txtReasons", this.txtLeavLreasons.Text));
            Rpt1.SetParameters(new ReportParameter("txtdenameadesig", this.txtdutiesnameandDesig.Text));
            Rpt1.SetParameters(new ReportParameter("txtaveleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbeleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtavcleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbcleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtavsleave", (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("txtbsleave", Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ")));
            Rpt1.SetParameters(new ReportParameter("appday", Convert.ToString(dt.Rows[0]["appday"])));
            Rpt1.SetParameters(new ReportParameter("appdate", Convert.ToDateTime(dt.Rows[0]["appdate"]).ToString("dd-MMM-yyyy")));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "LEAVE APPLICATION FORM(Human Resource Department)"));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

            #region OLD
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave03();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpName.SelectedItem.Text.Substring(7);
            //TextObject txtCompanyName = rptTest.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = this.lblComPany.Text;
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignation.Text;
            //TextObject txtDepartment = rptTest.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            //txtDepartment.Text = this.lblSection.Text;
            //TextObject txtJoiningDate = rptTest.ReportDefinition.ReportObjects["txtJoiningDate"] as TextObject;
            //txtJoiningDate.Text = this.lblJoiningDate.Text;
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = this.txtLeavLreasons.Text;
            //TextObject txtdenameadesig = rptTest.ReportDefinition.ReportObjects["txtdenameadesig"] as TextObject;
            //txtdenameadesig.Text = this.txtdutiesnameandDesig.Text;


            //TextObject txtaveleave = rptTest.ReportDefinition.ReportObjects["txtaveleave"] as TextObject;
            //txtaveleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbeleave = rptTest.ReportDefinition.ReportObjects["txtbeleave"] as TextObject;
            //txtbeleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //TextObject txtavcleave = rptTest.ReportDefinition.ReportObjects["txtavcleave"] as TextObject;
            //txtavcleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbcleave = rptTest.ReportDefinition.ReportObjects["txtbcleave"] as TextObject;
            //txtbcleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //TextObject txtavsleave = rptTest.ReportDefinition.ReportObjects["txtavsleave"] as TextObject;
            //txtavsleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbsleave = rptTest.ReportDefinition.ReportObjects["txtbsleave"] as TextObject;
            //txtbsleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //rptTest.SetDataSource(dtcal);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
            #endregion

        }

        private void PrintLeaveForm()
        {


            string comcod = this.GetComeCode();

            switch (comcod)
            {
                //case "4101":
                //    this.PrintLeaveform2();
                //    break;
                case "4301":
                    //this.PrintLeaveform2();
                    break;
                case "4101":
                case "4305":
                case "4315":
                    //this.PrintLeaveform3();
                    break;
                default:

                    this.PrintLeaveform3();
                    break;

            }




        }
        private void PrintLeaveform1()
        {

            //DataTable dt = ((DataTable)Session["tblleavest"]);
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.EmpLeavApp();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRptCompAdd = rptTest.ReportDefinition.ReportObjects["txtRptCompAdd"] as TextObject;
            //txtRptCompAdd.Text = comadd;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = "";
            //TextObject txtleaveday = rptTest.ReportDefinition.ReportObjects["txtleaveday"] as TextObject;
            //txtleaveday.Text = "";

            //TextObject txtldatefrm = rptTest.ReportDefinition.ReportObjects["txtldatefrm"] as TextObject;
            //txtldatefrm.Text = "";
            //TextObject txtldateto = rptTest.ReportDefinition.ReportObjects["txtldateto"] as TextObject;
            //txtldateto.Text = "";
            //TextObject txtlday = rptTest.ReportDefinition.ReportObjects["txtlday"] as TextObject;
            //txtlday.Text = "";
            //TextObject txtRecordNo1 = rptTest.ReportDefinition.ReportObjects["txtRecordNo1"] as TextObject;
            //txtRecordNo1.Text = "";
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //TextObject txtEmpName1 = rptTest.ReportDefinition.ReportObjects["txtEmpName1"] as TextObject;
            //txtEmpName1.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignationlApp.Text;
            //TextObject txtDesig1 = rptTest.ReportDefinition.ReportObjects["txtDesig1"] as TextObject;
            //txtDesig1.Text = this.lblDesignationlApp.Text;
            //TextObject txtApprdate = rptTest.ReportDefinition.ReportObjects["txtApprdate"] as TextObject;
            //txtApprdate.Text = "";
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = "";
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = "";
            //TextObject txttitlelappslip = rptTest.ReportDefinition.ReportObjects["txttitlelappslip"] as TextObject;
            //txttitlelappslip.Text = "Leave Approval Slip";
            //TextObject txtAppDays = rptTest.ReportDefinition.ReportObjects["txtAppDays"] as TextObject;
            //txtAppDays.Text = "";
            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        private void PrintLeaveform2()
        {
            //DataView dv = ((DataTable)Session["tblleavest"]).DefaultView;
            //dv.RowFilter = ("gcod='51001'");
            //DataTable dt = dv.ToTable();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave02(); ;
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtRecordNo = rptTest.ReportDefinition.ReportObjects["txtRecordNo"] as TextObject;
            //txtRecordNo.Text = "";
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);

            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignationlApp.Text;
            //TextObject txtApplydate = rptTest.ReportDefinition.ReportObjects["txtApplydate"] as TextObject;
            //txtApplydate.Text = "";
            //TextObject txtlsdate = rptTest.ReportDefinition.ReportObjects["txtlsdate"] as TextObject;
            //txtlsdate.Text = "";
            //TextObject txtledate = rptTest.ReportDefinition.ReportObjects["txtledate"] as TextObject;
            //txtledate.Text = "";
            //TextObject txtleavedays = rptTest.ReportDefinition.ReportObjects["txtleavedays"] as TextObject;
            //txtleavedays.Text = "";
            //TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            //rpttxtReasons.Text = "";
            //TextObject txtAddofentime = rptTest.ReportDefinition.ReportObjects["txtAddofentime"] as TextObject;
            //txtAddofentime.Text = "";
            //TextObject rpttxtremarks = rptTest.ReportDefinition.ReportObjects["txtremarks"] as TextObject;
            //rpttxtremarks.Text = "";
            //TextObject txtuserinfo = rptTest.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = ASTUtility.Concat(compname, username, printdate);

            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                   ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        private void PrintLeaveform3()
        {

            //DataTable dtcal = ((DataTable)Session["tblleavest"]).Copy();
            //DataView dv = dtcal.DefaultView;
            //dv.RowFilter = ("gcod='51001'");

            //DataTable dt = dv.ToTable();
            //DataRow[] dr1 = dt.Select("gcod='51001'");
            //dr1[0]["gcod"] = "51008";


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comnam = hst["comnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //ReportDocument rptTest = new RealERPRPT.R_81_Hrm.R_84_Lea.RptHrEmpLeave03();
            //TextObject txtRptComName = rptTest.ReportDefinition.ReportObjects["txtRptComName"] as TextObject;
            //txtRptComName.Text = comnam;
            //TextObject txtEmpName = rptTest.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            //txtEmpName.Text = this.ddlEmpNamelApp.SelectedItem.Text.Substring(7);
            //TextObject txtCompanyName = rptTest.ReportDefinition.ReportObjects["txtCompanyName"] as TextObject;
            //txtCompanyName.Text = this.lblComPanylApp.Text;
            //TextObject txtDesig = rptTest.ReportDefinition.ReportObjects["txtDesig"] as TextObject;
            //txtDesig.Text = this.lblDesignationlApp.Text;
            //TextObject txtDepartment = rptTest.ReportDefinition.ReportObjects["txtDepartment"] as TextObject;
            //txtDepartment.Text = this.lblSectionlApp.Text;
            //TextObject txtJoiningDate = rptTest.ReportDefinition.ReportObjects["txtJoiningDate"] as TextObject;
            //txtJoiningDate.Text = this.lblJoiningDatelApp.Text;


            ////TextObject txtlsdate = rptTest.ReportDefinition.ReportObjects["txtlsdate"] as TextObject;
            ////txtlsdate.Text = "";
            ////TextObject txtledate = rptTest.ReportDefinition.ReportObjects["txtledate"] as TextObject;
            ////txtledate.Text = "";
            ////TextObject txtleavedays = rptTest.ReportDefinition.ReportObjects["txtleavedays"] as TextObject;
            ////txtleavedays.Text = "";

            ////TextObject txtaveleave = rptTest.ReportDefinition.ReportObjects["txtaveleave"] as TextObject;
            ////txtaveleave.Text = "";
            ////TextObject txtbeleave = rptTest.ReportDefinition.ReportObjects["txtbeleave"] as TextObject;
            ////txtbeleave.Text = "";

            ////TextObject txtavcleave = rptTest.ReportDefinition.ReportObjects["txtavcleave"] as TextObject;
            ////txtavcleave.Text = "";
            ////TextObject txtbcleave = rptTest.ReportDefinition.ReportObjects["txtbcleave"] as TextObject;
            ////txtbcleave.Text = "";

            ////TextObject txtavsleave = rptTest.ReportDefinition.ReportObjects["txtavsleave"] as TextObject;
            ////txtavsleave.Text = "";
            ////TextObject txtbsleave = rptTest.ReportDefinition.ReportObjects["txtbsleave"] as TextObject;
            ////txtbsleave.Text = "";
            ////TextObject rpttxtReasons = rptTest.ReportDefinition.ReportObjects["txtReasons"] as TextObject;
            ////rpttxtReasons.Text = "";


            //TextObject txtaveleave = rptTest.ReportDefinition.ReportObjects["txtaveleave"] as TextObject;
            //txtaveleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbeleave = rptTest.ReportDefinition.ReportObjects["txtbeleave"] as TextObject;
            //txtbeleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51001'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //TextObject txtavcleave = rptTest.ReportDefinition.ReportObjects["txtavcleave"] as TextObject;
            //txtavcleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbcleave = rptTest.ReportDefinition.ReportObjects["txtbcleave"] as TextObject;
            //txtbcleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51002'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");

            //TextObject txtavsleave = rptTest.ReportDefinition.ReportObjects["txtavsleave"] as TextObject;
            //txtavsleave.Text = (Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["ltaken"]) + Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["appday"])).ToString("#,##0;(#,##0); ");
            //TextObject txtbsleave = rptTest.ReportDefinition.ReportObjects["txtbsleave"] as TextObject;
            //txtbsleave.Text = Convert.ToDouble((((DataTable)Session["tblleavest"]).Select("gcod='51003'"))[0]["balleave"]).ToString("#,##0;(#,##0); ");





            ////TextObject txtldsdate = rptTest.ReportDefinition.ReportObjects["txtldsdate"] as TextObject;
            ////txtldsdate.Text = "";
            ////TextObject txtldedate = rptTest.ReportDefinition.ReportObjects["txtldedate"] as TextObject;
            ////txtldedate.Text = "";
            ////TextObject txtdleavedays = rptTest.ReportDefinition.ReportObjects["txtdleavedays"] as TextObject;
            ////txtdleavedays.Text = "";

            ////TextObject txtlasdate = rptTest.ReportDefinition.ReportObjects["txtlasdate"] as TextObject;
            ////txtlasdate.Text = "";
            ////TextObject txtlaedate = rptTest.ReportDefinition.ReportObjects["txtlaedate"] as TextObject;
            ////txtlaedate.Text = "";
            ////TextObject txatleavedays = rptTest.ReportDefinition.ReportObjects["txatleavedays"] as TextObject;
            ////txatleavedays.Text = "";

            //rptTest.SetDataSource(dt);
            //Session["Report1"] = rptTest;
            //((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                  ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }

        protected void lnkbtnTotalLeave_Click(object sender, EventArgs e)
        {
            this.SaveLeave();
        }
        protected void lnkbtnUpdateLeave_Click(object sender, EventArgs e)
        {

            this.SaveLeave();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            //Leave Balance Checking
            DataTable dt = (DataTable)Session["tblleave"];
            DataTable dt1 = (DataTable)Session["tblleavest"];
            foreach (DataRow dr1 in dt.Rows)
            {
                string gcod1 = dr1["gcod"].ToString();
                double lappday = Convert.ToDouble(dr1["lapplied"]);

                if (lappday > 0)
                {
                    double pbal = Convert.ToDouble(dt1.Select("gcod='" + gcod1 + "'")[0]["pbal"]);
                    if (pbal < lappday)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "You Applied Balance Cross the limit!!";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;
                    }

                }
            }




            //---------For Status table --------------------------------------






            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string trnid = this.lbltrnleaveid.Text;
            string empid = this.ddlEmpName.SelectedValue.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double lapplied = Convert.ToDouble(dt.Rows[i]["lapplied"]);
                if (lapplied > 0)
                {

                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string frmdate = Convert.ToDateTime(dt.Rows[i]["lenjoydt1"]).ToString("dd-MMM-yyyy");
                    string todate = Convert.ToDateTime(dt.Rows[i]["lenjoydt2"]).ToString("dd-MMM-yyyy");
                    string applydat = Convert.ToDateTime(this.txtaplydate.Text).ToString("dd-MMM-yyyy");
                    //  double lapplied = Convert.ToDouble(dt.Rows[i]["lapplied"]);

                    //  string  lapplied = lapplied < 1.00 ? lapplied : 0.00;

                    string reason = this.txtLeavLreasons.Text.Trim(); ;
                    string addentime = this.txtaddofenjoytime.Text.Trim();
                    string remarks = this.txtLeavRemarks.Text.Trim();
                    string dnameadesig = this.txtdutiesnameandDesig.Text.Trim();

                    string APRdate = "";
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPEMLEAVAPP", trnid, empid, gcod, frmdate, todate, applydat, reason, remarks, APRdate, addentime, dnameadesig, lapplied.ToString(), "", "", "");


                    if (result == true && chkPreLeave.Checked == false)
                    {
                        if (hst["compsms"].ToString() == "True")
                        {

                            switch (comcod)
                            {
                                case "3333":
                                    break;

                                default:
                                    this.SendSms(frmdate, todate);
                                    break;

                            }





                        }

                        else if (hst["compmail"].ToString() == "True")
                        {
                            this.sendmail(frmdate, todate);

                        }



                    }


                }

            }
            this.EmpLeaveInfo();
            this.ShowEmppLeave();
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
        }

        protected void SendSms(string frmdate, string todate)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISER", empid, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;
            DataTable dt = (DataTable)Session["tblleave"];

            //DataRow[] dr = dt.Select("lapplied>0"); 
            double lapplied = Convert.ToDouble(dt.Select("lapplied>0")[0]["lapplied"]);
            string leavedesc = dt.Select("lapplied>0")[0]["gdesc"].ToString();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string phone = (string)ds.Tables[0].Rows[i]["phone"];
                string empname = (string)ds.Tables[1].Rows[0]["name"];
                string empdesig = (string)ds.Tables[1].Rows[0]["desig"];
                string appdate = "";
                if (hst["compsms"].ToString() == "True")
                {
                    SendSmsProcess sms = new SendSmsProcess();
                    string comnam = hst["comnam"].ToString();
                    string compname = hst["compname"].ToString();
                    // string frmname = "PurReqApproval.aspx?Type=RateInput";
                    // string SMSHead = "Leave Applied From : ";
                    string SMSText = leavedesc + " applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig;
                    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
                }
            }
        }



        private void sendmail(string frmdate, string todate)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            var ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETSUPERVISERMAIL", empid, "", "", "", "", "", "", "", "");

            if (ds == null)
                return;
            DataTable dt = (DataTable)Session["tblleave"];

            //DataRow[] dr = dt.Select("lapplied>0"); 
            double lapplied = Convert.ToDouble(dt.Select("lapplied>0")[0]["lapplied"]);
            string leavedesc = dt.Select("lapplied>0")[0]["gdesc"].ToString();


            string idcard = (string)ds.Tables[1].Rows[0]["idcard"];
            string empname = (string)ds.Tables[1].Rows[0]["name"];
            string empdesig = (string)ds.Tables[1].Rows[0]["desig"];
            string deptname = (string)ds.Tables[1].Rows[0]["deptname"];



            string maildescription = "Employee ID : " + idcard + " \n" + "Employee Name : " + empname + "\n" + "Designation : " + empdesig + "\n" +
                "Department Name : " + deptname + "\n" + "Leave Period : " + frmdate + " To " + todate + "\n" + "Leave Duration : " + lapplied +
                "\n" + "Leave Type : " + leavedesc + "\n" + "Leave Reason : " + this.txtLeavLreasons.Text;


            //  string SMSText = leavedesc + " applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig;

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    string mail = (string)ds.Tables[0].Rows[i]["mail"];
            //    string empname = (string)ds.Tables[1].Rows[0]["name"];
            //    string empdesig = (string)ds.Tables[1].Rows[0]["desig"];
            //    string appdate = "";
            //    //if (hst["compsms"].ToString() == "True")
            //    //{
            //    //    SendSmsProcess sms = new SendSmsProcess();
            //    //    string comnam = hst["comnam"].ToString();
            //    //    string compname = hst["compname"].ToString();
            //    //    // string frmname = "PurReqApproval.aspx?Type=RateInput";
            //    //    // string SMSHead = "Leave Applied From : ";
            //    //    string SMSText = leavedesc + " applied from : " + frmdate + " To " + todate + "\n" + "Name: " + empname + " Designation : " + empdesig;
            //    //    bool resultsms = sms.SendSmmsPwd(comcod, SMSText, phone);
            //    //}
            //}   

            ///

            // ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
            DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");

            //SMTP
            string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
            int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());
            string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
            string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
            string mailtousr = ds.Tables[0].Rows[0]["mail"].ToString();


            EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("TryIt");

            //Connection Details 
            SmtpServer oServer = new SmtpServer(hostname);
            oServer.User = frmemail;
            oServer.Password = psssword;
            oServer.Port = portnumber;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oMail.From = frmemail;
            oMail.To = mailtousr;
            oMail.Cc = frmemail;
            // oMail.Subject = subject;


            oMail.HtmlBody = "<html><head></head><body><pre style='max-width:700px;text-align:justify; font-weight: bold;font-size: 14px'>" + "Dear Sir," + "<br/>" + maildescription + "</pre></body></html>";


            try
            {

                oSmtp.SendMail(oServer, oMail);
                ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
            }







        }

        // mail sent for Peb

        //private void SendNormalMail()
        //{
        //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;
        //    string comcod = this.GetComeCode();
        //    string usrid = ((Hashtable)Session["tblLogin"])["usrid"].ToString();
        //    DataSet dssmtpandmail = HRData.GetTransInfo(comcod, "SP_REPORT_SALSMGT", "SMTPPORTANDMAIL", usrid, "", "", "", "", "", "", "", "");


        //  //  string mORDERNO = this.lblCurOrderNo1.Text.Trim().Substring(0, 3) + this.txtCurOrderDate.Text.Trim().Substring(6, 4) + this.lblCurOrderNo1.Text.Trim().Substring(3, 2) + this.txtCurOrderNo2.Text.Trim();

        //   // DataSet ds1 = HRData.GetTransInfo(comcod, "SP_ENTRY_PURCHASE_02", "GETPUREMAIL", mORDERNO, "", "", "", "", "", "", "", "");

        //    string subject = "Work Order";
        //    //SMTP
        //    string hostname = dssmtpandmail.Tables[0].Rows[0]["smtpid"].ToString();
        //    int portnumber = Convert.ToInt32(dssmtpandmail.Tables[0].Rows[0]["portno"].ToString());





        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(hostname, portnumber);
        //    //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //client.EnableSsl = true;
        //    client.EnableSsl = false;
        //    string frmemail = dssmtpandmail.Tables[1].Rows[0]["mailid"].ToString();
        //    string psssword = dssmtpandmail.Tables[1].Rows[0]["mailpass"].ToString();
        //    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(frmemail, psssword);
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = credentials;

        //    ///////////////////////

        //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        //    msg.From = new System.Net.Mail.MailAddress(frmemail);



        //    msg.To.Add(new System.Net.Mail.MailAddress(ds1.Tables[0].Rows[0]["mailid"].ToString()));
        //    msg.Subject = subject;
        //    msg.IsBodyHtml = true;

        //    System.Net.Mail.Attachment attachment;

        //    string apppath = Server.MapPath("~") + "\\SupWorkOreder" + "\\" + mORDERNO + ".pdf"; ;

        //    attachment = new System.Net.Mail.Attachment(apppath);
        //    msg.Attachments.Add(attachment);



        //    msg.Body = string.Format("<html><head></head><body><pre style='max-width:700px;text-align:justify;'>" + "Dear Sir," + "<br/>" + "please find attached file" + "</pre></body></html>");
        //    try
        //    {
        //        client.Send(msg);

        //        ((Label)this.Master.FindControl("lblmsg")).Text = "Your message has been successfully sent.";
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);


        //        //string savelocation = Server.MapPath("~") + "\\SupWorkOreder";
        //        //string[] filePaths = Directory.GetFiles(savelocation);
        //        //foreach (string filePath in filePaths)
        //        //    File.Delete(filePath);

        //    }
        //    catch (Exception ex)
        //    {
        //        ((Label)this.Master.FindControl("lblmsg")).Text = "Error occured while sending your message." + ex.Message;
        //        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
        //    }
        //}


        protected void lbtnDelete_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComeCode();
            string trnid = this.lbltrnleaveid.Text;
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Sucessfully Deleted');", true);



        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "User":
                    break;


            }
        }


        protected void imgbtnlAppEmpSeaarch_Click(object sender, EventArgs e)
        {

            this.GetEmployeeName();

        }
        protected void imgbtnlFEmpSeaarch_Click(object sender, EventArgs e)
        {
            // this.GetLveAppEmployeeName();
        }


        protected void gvleaveInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label description = (Label)e.Row.FindControl("lgvledescription");
                Label lgvleavedays = (Label)e.Row.FindControl("lgvleavedays");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "grpsl")).ToString();

                if (code == "")
                {
                    return;
                }
                if (ASTUtility.Right(code, 4) == "BBBB" || ASTUtility.Right(code, 4) == "CCCC")
                {
                    description.Font.Bold = true;
                    lgvleavedays.Font.Bold = true;
                    description.Style.Add("text-align", "right");


                }

            }
        }
        protected void gvleaveInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)ViewState["tblempleaveinfo"];
            string trnid = ((Label)this.gvleaveInfo.Rows[e.RowIndex].FindControl("lgvltrnleaveid")).Text.Trim();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMLEAVAPP", trnid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Deleted failed');", true);
                return;
            }
            int rowindex = (this.gvleaveInfo.PageSize) * (this.gvleaveInfo.PageIndex) + e.RowIndex;
            dt.Rows[rowindex].Delete();
            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempleaveinfo");
            ViewState["tblempleaveinfo"] = dv.ToTable();
            this.gvleaveInfo.DataSource = dv.ToTable();
            this.gvleaveInfo.DataBind();
        }
        private string GetUserCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["usrid"].ToString());

        }
        private void GetEmpid()
        {
            Session.Remove("tblUsrinfo");
            string comcod = GetComeCode();
            string usrid = GetUserCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWUSERIND", "%%", usrid, "", "", "", "", "", "", "");

            ViewState["tblEmpid"] = ds1.Tables[0];

            if (ds1 == null)
            {
                return;
            }
            this.lblempid.Text = ds1.Tables[0].Rows[0]["empid"].ToString();
        }



        protected void lbtnsendsms_Click(object sender, EventArgs e)
        {


            string comcode = this.GetComeCode();
            string text = "Hello how are you?";
            string mobilenum = "01719862989";
            SendSmsProcess sms = new SendSmsProcess();
            bool result = sms.SendSmmsPwd(comcode, text, mobilenum);

        }



    }
}