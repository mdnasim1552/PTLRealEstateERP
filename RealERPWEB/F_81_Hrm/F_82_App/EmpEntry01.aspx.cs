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
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using AjaxControlToolkit;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntry011 : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        string Upload = "";
        int size = 0;
        System.IO.Stream image_file = null;
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                //if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                //    Response.Redirect("~/AcceessError.aspx");
                ////if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                ////    Response.Redirect("../../AcceessError.aspx");
                this.GetInformation();
                this.GetEmployeeName();

                //this.GetEmployeeName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE PERSONAL INFORMATION";
                this.getLastCardNo();
                this.lblLastCardNo.Visible = true;
                CommonButton();
            }
        }
        public void CommonButton()
        {
            //((Panel)this.Master.FindControl("pnlbtn")).Visible = true;
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Visible = true;
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Text = "Agreement";
            //((LinkButton)this.Master.FindControl("lnkbtnLedger")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnHisprice")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnTranList")).Visible = false;
            //((Panel)this.Master.FindControl("pilleftDvi")).Visible = false;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Visible = true;
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Text = "Aggrement";
            // ((LinkButton)this.Master.FindControl("lnkbtnAdd")).Visible = false;

            //((LinkButton)this.Master.FindControl("lnkbtnEdit")).Visible = false;
            //((LinkButton)this.Master.FindControl("lnkbtnDelete")).Visible = false;
            //((LinkButton)this.Master.FindControl("btnClose")).Visible = false;
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lUpdatPerInfo_Click);
            ((LinkButton)this.Master.FindControl("lnkbtnNew")).Click += new EventHandler(lnkbtnAgreement);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        private void lnkbtnAgreement(object sender, EventArgs e)
        {
            string empid = this.ddlEmpName.SelectedValue.ToString();
            Response.Redirect("HREmpEntry?Type=Aggrement&Empid=" + empid);
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void getLastCardNo()
        {

            string comcod = this.GetComeCode();
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETLASTCARDNO", "", "", "", "", "", "", "", "", "");
            this.lblLastCardNo.Text = "Last Card Number :- " + ds5.Tables[0].Rows[0]["lastCard"].ToString().Trim();
        }
        private void GetEmployeeName()
        {
            Session.Remove("tblempname");
            string comcod = this.GetComeCode();
            string txtSProject = (this.Request.QueryString["empid"] != "") ? "%" + this.Request.QueryString["empid"].ToString() + "%" : "%%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, "", "", "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            //this.ddlEmpName.SelectedValue = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            Session["tblempname"] = ds3.Tables[0];
            ds3.Dispose();
            this.ddlEmpName.SelectedValue = (this.Request.QueryString["empid"] == "") ? this.ddlEmpName.Items[0].Value : this.Request.QueryString["empid"].ToString();
            this.SelectView();
        }
        private void GetInformation()
        {
            string comcod = this.GetComeCode();
            string txtinformation = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETINFORMATION", txtinformation, "", "", "", "", "", "", "", "");
            this.ddlInformation.DataTextField = "infodesc";
            this.ddlInformation.DataValueField = "infoid";
            this.ddlInformation.DataSource = ds3.Tables[0];
            this.ddlInformation.DataBind();
        }
        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlInformation_SelectedIndexChanged(null, null);
        }
        protected void ddlInformation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectView();
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            // if (this.lbtnOk.Text == "Ok")
            this.GetEmployeeName();
        }
        protected void ibtnInformation_Click(object sender, EventArgs e)
        {
            this.GetInformation();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            //if (this.lbtnOk.Text == "Ok")
            //{
            //    this.ddlEmpName.Visible = false;
            //    this.ddlInformation.Visible = false;
            //    //this.lbtnOk.Text = "New";
            //    this.SelectView();
            //    return;
            //}
            //this.ddlEmpName.Visible = true;
            //this.ddlInformation.Visible = true;
            ////this.lbtnOk.Text = "Ok";
            //this.MultiView1.ActiveViewIndex = -1;       
            //this.lblmsg.Text = "";
        }
        private void SelectView()
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            this.lblLastCardNo.Visible = false;
            switch (infoid)
            {
                case "01":

                    this.MultiView1.ActiveViewIndex = 0;
                    this.GetBldMeReFes();
                    this.GetSupervisorName();
                    this.ShowPersonalInformation();
                    this.lblLastCardNo.Visible = true;
                    this.addOcupation.Visible = false;

                    this.UploadCV.Visible = true;
                    this.FileUploadControl.Visible = true;
                    this.lblUploadCV.Visible = true;

                    this.btnUpload.Visible = true;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;
                    break;

                case "10":
                    this.MultiView1.ActiveViewIndex = 1;
                    this.GetAcaDemicDegree();
                    this.ShowDegree();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = true;

                    break;

                case "13":

                    this.MultiView1.ActiveViewIndex = 2;
                    this.ShowEmpRecord();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;

                case "14":
                    this.MultiView1.ActiveViewIndex = 3;
                    this.ShowEmpPosition();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;

                case "15":
                    this.MultiView1.ActiveViewIndex = 5;
                    this.ShowJobRespon();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;


                case "16":
                    this.MultiView1.ActiveViewIndex = 4;
                    this.ShowReferecne();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;
                case "20":
                case "21":
                case "26":
                case "27":
                    this.MultiView1.ActiveViewIndex = 6;
                    this.ShowParentDT();
                    this.ShowLastDegree();
                    this.addOcupation.Visible = true;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;

                    break;

                case "30":
                    this.MultiView1.ActiveViewIndex = 7;
                    this.ShowCTCDetails();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                    break;
                case "31":
                    this.MultiView1.ActiveViewIndex = 8;
                    this.ShowSalaryDetails();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                    break;
                case "37":
                    this.MultiView1.ActiveViewIndex = 9;
                    this.ShowNomineeInfo();
                    this.addOcupation.Visible = false;
                    ((LinkButton)this.Master.FindControl("lnkbtnSave")).Visible = false;
                    break;


            }
        }
        private void GetBldMeReFes()
        {


            string comcod = this.GetComeCode();
            Session.Remove("tblbmrf");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETBLDMEREFES", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblbmrf"] = ds2.Tables[0];



        }
        private void GetSupervisorName()
        {

            Session.Remove("tblsppname");
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", txtSProject, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;

            DataRow row = ds3.Tables[0].NewRow();  // NewRow();
            row["empid"] = "000000000000";
            row["empname"] = "None";
            ds3.Tables[0].Rows.Add(row);
            DataView dv = ds3.Tables[0].DefaultView;
            dv.Sort = "empid";
            Session["tblsppname"] = dv.ToTable();

            ds3.Dispose();



        }
        private void GetAcaDemicDegree()
        {
            string comcod = this.GetComeCode();
            Session.Remove("tblacadeg");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETACADEMICDEGREE", "", "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            Session["tblacadeg"] = ds2;
            ddlDegreeList.DataTextField = "gdesc";
            ddlDegreeList.DataValueField = "gcod";
            ddlDegreeList.DataSource = ds2.Tables[0];
            ddlDegreeList.DataBind();
            ds2.Dispose();
            ddlDegreeList_SelectedIndexChanged(null, null);

        }
        private void ShowPersonalInformation()
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPERSONALINFO", empid, "", "", "", "", "", "", "", "");
            if (ds2 == null)
                return;
            DataTable dt;
            DataTable dt2;
            DataView dv2;
            DataView dv3;

            dv2 = ds2.Tables[0].DefaultView;
            dv2.RowFilter = "gcod >= '01001' and gcod <= 01020";
            dt = dv2.ToTable();

            dv3 = ds2.Tables[0].DefaultView;
            dv3.RowFilter = "gcod >= '01021' and gcod <= 99999";
            dt2 = dv3.ToTable();


            Session["UserLog"] = ds2.Tables[1];
            DataRow[] dr = dt.Select("gcod='01002'");
            dr[0]["gdesc1"] = (((DataTable)Session["tblempname"]).Select("empid='" + empid + "'"))[0]["empname1"];
            DataTable dt1 = (DataTable)Session["tblbmrf"];
            DataView dv1;

            //first step
            this.gvPersonalInfo.DataSource = dt;
            this.gvPersonalInfo.DataBind();
            //secnond step
            this.gvPersonalInfo2.DataSource = dt2;
            this.gvPersonalInfo2.DataBind();



            DropDownList ddlgval;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string Gcode = dt.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {
                    case "01006": //Confirmation Date
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '85%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                       
                        string provcode = (comcod == "3365" || comcod == "3101")  ? "85006" : comcod == "3354"? "85099" : "85001";

                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = gdesc1 == "" ? provcode : gdesc1;
                        ddlval_SelectedIndexChanged(null,null);
                        break;

                    case "01009": //Blood Group
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '90%'");
                        dv1.Sort = "slno desc";
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01010": //Relationship Status 
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '92%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;
                    case "01011":// Religion
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '95%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01012": // Festival
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '97%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01013": // Nationality
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '98%'");
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01003": // Datetime
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        string Joindate = (gdesc1 == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = Joindate;
                        break;


                    case "01008":

                         gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false; 
                        Joindate = (gdesc1 == "") ? "" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = Joindate; 
                        break;

                    case "01007":
                        gdesc1 = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;
                        Joindate = (gdesc1 == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text = Joindate; 
                        break;


                    default:
                        ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;

                        break;

                }

            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                string Gcode = dt2.Rows[i]["gcod"].ToString();

                switch (Gcode)
                {

                    case "01023": // Sex
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '99%'");
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01025": // Sex
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '96%'");
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01994": // Grade
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '34%'");
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01995": // Service Location
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '28%' or gcod like '29%'");
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        string gdesc1 = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        ddlgval = ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = gdesc1 == "" ? "29001" : ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01996": // Supper Location


                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "empname";
                        ddlgval.DataValueField = "empid";

                        ddlgval.DataSource = (DataTable)Session["tblsppname"];
                        //ddlgval.SelectedIndex =-1;

                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;


                    case "01997": // Grade
                        //if (comcod == "3354")
                        //{
                        //    dv1 = dt1.DefaultView;
                        //    dv1.RowFilter = ("gcod like '03%'");
                        //}
                        //else
                        //{
                        //    dv1 = dt1.DefaultView;
                        //    dv1.RowFilter = ("gcod like '86%'");
                        //}
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("gcod like '86%'");


                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ddlgval = ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval"));
                        ddlgval.DataTextField = "gdesc";
                        ddlgval.DataValueField = "gcod";
                        ddlgval.DataSource = dv1.ToTable();
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim();
                        break;

                    case "01999":
                        ((Panel)this.gvPersonalInfo2.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Visible = false;
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo2.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;

                        break;


                    default:
                        ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Visible = false;
                        ((Panel)this.gvPersonalInfo2.Rows[i].FindControl("Panegrd")).Visible = false;
                        ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Items.Clear();
                        ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Visible = false;
                        ((LinkButton)this.gvPersonalInfo2.Rows[i].FindControl("ibtngrdEmpList")).Visible = false;

                        break;

                }

            }

            DataSet copSetup = compUtility.GetCompUtility();
            bool langbang = copSetup.Tables[0].Rows.Count == 0 ? false : Convert.ToBoolean(copSetup.Tables[0].Rows[0]["LANG_BANG"]);

            this.gvPersonalInfo.Columns[6].Visible = langbang; // for Bangla column
            this.gvPersonalInfo2.Columns[6].Visible = langbang; // for Bangla column

        }
        private void ShowDegree()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPACADEGREE", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvDegree.DataSource = null;
                this.gvDegree.DataBind();
                return;

            }
            Session["tblempAcaRecord"] = ds3.Tables[0];

            DataTable dt = ds3.Tables[0];
            this.gvDegree.DataSource = ds3.Tables[0];
            this.gvDegree.DataBind();
            DataSet ds1 = (DataSet)Session["tblacadeg"];

            DropDownList ddldegree, ddlAcadegree, ddlMajorSubj, ddlresult, ddlpyear;
            TextBox txtRsultDt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Degree
                string Gcode = dt.Rows[i]["gcod"].ToString();
                ddldegree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree"));
                ddldegree.DataTextField = "gdesc";
                ddldegree.DataValueField = "gcod";
                ddldegree.DataSource = ds1.Tables[0];
                ddldegree.DataBind();
                ddldegree.SelectedValue = Gcode;


                if (Gcode == "10007" || Gcode == "10008")
                {
                    ddlAcadegree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                    ddlAcadegree.Visible = false;
                }
                else
                {
                    //Academic Degree
                    DataTable dt1 = ds1.Tables[1].Copy();
                    DataView dv1 = dt1.DefaultView;
                    dv1.RowFilter = ("maincode='99999' or maincode='" + Gcode + "'");

                    string subcode = dt.Rows[i]["subcode"].ToString();
                    ddlAcadegree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                    ddlAcadegree.DataTextField = "subdesc";
                    ddlAcadegree.DataValueField = "subcode";
                    ddlAcadegree.DataSource = dv1.ToTable();
                    ddlAcadegree.DataBind();
                    ddlAcadegree.SelectedValue = subcode;

                }


                if (Gcode == "10007" || Gcode == "10008")
                {
                    ddlMajorSubj = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlMajorSubj"));
                    ddlMajorSubj.Visible = false;
                    txtRsultDt = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvEngMedium"));

                    txtRsultDt.Visible = true;
                    txtRsultDt.Text = ds3.Tables[0].Rows[i]["majsubcode"].ToString();
                }

                else
                {
                    //Major Subject
                    string majsubcode = dt.Rows[i]["majsubcode"].ToString();
                    ddlMajorSubj = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlMajorSubj"));
                    ddlMajorSubj.DataTextField = "gdesc";
                    ddlMajorSubj.DataValueField = "gcod";
                    ddlMajorSubj.DataSource = ds1.Tables[3];
                    ddlMajorSubj.DataBind();
                    ddlMajorSubj.SelectedValue = majsubcode;
                }




                //Result

                string resultcode = dt.Rows[i]["resultcode"].ToString();
                ddlresult = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlResult"));
                ddlresult.DataTextField = "gdesc";
                ddlresult.DataValueField = "gcod";
                ddlresult.DataSource = ds1.Tables[2];
                ddlresult.DataBind();
                ddlresult.SelectedValue = resultcode;


                //Passing Year
                int year = (dt.Rows[i]["pyear"].ToString() == "") ? Convert.ToInt32(System.DateTime.Today.ToString("yyyy")) : Convert.ToInt32("0" + dt.Rows[i]["pyear"].ToString());

                List<string> passyear = ASITUtility02.pasyear(year - 75, year + 20);
                ddlpyear = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlPassingYear"));
                foreach (string pass in passyear)
                    ddlpyear.Items.Add(pass);
                ddlpyear.SelectedValue = year.ToString();




            }

            // this.ddlDegree_SelectedIndexChanged(null, null);
            this.ddlResult_SelectedIndexChanged(null, null);

        }
        private void ShowMajorSub()
        {

        }
        private void ShowEmpRecord()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPRECORD", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvEmpRec.DataSource = null;
                this.gvEmpRec.DataBind();
                return;

            }
            this.gvEmpRec.DataSource = ds3.Tables[0];
            this.gvEmpRec.DataBind();
        }
        private void ShowEmpPosition()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPOSITION", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvAssocia.DataSource = null;
                this.gvAssocia.DataBind();
                return;

            }
            this.gvAssocia.DataSource = ds3.Tables[0];
            this.gvAssocia.DataBind();



        }
        private void ShowReferecne()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPREF", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
            {
                this.gvRef.DataSource = null;
                this.gvRef.DataBind();
                return;

            }
            this.gvRef.DataSource = ds3.Tables[0];
            this.gvRef.DataBind();
        }
        private void ShowParentDT()
        {
            string comcod = this.GetComeCode();
            string type = this.ddlInformation.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPPARENTDT", empid, type, "", "", "", "", "", "", "");
            if (ds == null)
            {
                this.gvFamilyInfo.DataSource = null;
                this.gvFamilyInfo.DataBind();
                return;

            }
            this.gvFamilyInfo.DataSource = ds.Tables[0];
            this.gvFamilyInfo.DataBind();



            this.gvFamilyInfo.Columns[4].Visible = this.ddlInformation.SelectedValue == "26";
            Session["tblFamilydt"] = ds.Tables[0];
        }
        private void ShowCTCDetails()
        {
            string comcod = this.GetComeCode();
            string type = this.ddlInformation.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPCTCDT", empid, type, "", "", "", "", "", "", "");
            if (ds == null)
            {
                this.gvCTCDetails.DataSource = null;
                this.gvCTCDetails.DataBind();
                return;

            }
            Session["tblctdet"] = ds.Tables[0];
            this.gvCTCDetails.DataSource = ds.Tables[0];
            this.gvCTCDetails.DataBind();

        }
        private void ShowSalaryDetails()
        {
            string comcod = this.GetComeCode();
            string type = this.ddlInformation.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPSALDT", empid, type, "", "", "", "", "", "", "");
            if (ds == null)
            {
                this.gvSALDetails.DataSource = null;
                this.gvSALDetails.DataBind();
                return;

            }
            Session["tblsaldet"] = ds.Tables[0];
            this.gvSALDetails.DataSource = ds.Tables[0];
            this.gvSALDetails.DataBind();

        }

        private void ShowJobRespon()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPJOBRESPONSIBILITES", empid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.grvJobRespo.DataSource = null;
                this.grvJobRespo.DataBind();

                return;

            }
            this.grvJobRespo.DataSource = ds4.Tables[0];
            this.grvJobRespo.DataBind();

        }


        private void ShowNomineeInfo()
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPNOMINEEINFORMATION", empid, "", "", "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.GvNominee.DataSource = null;
                this.GvNominee.DataBind();

                return;

            }
            this.GvNominee.DataSource = ds4.Tables[0];
            this.GvNominee.DataBind();

        }
        protected void lUpdatPerInfo_Click(object sender, EventArgs e)
        {
            string infoid = this.ddlInformation.SelectedValue.ToString();
            switch (infoid)
            {
                case "01":
                    lUpdatPerInfo_Click1(null, null);
                    break;
                case "10":
                    lUpdateDegree_Click(null, null);
                    break;
            }
        }
        protected void lUpdatPerInfo_Click1(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            //string Gvalue="";
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string empname = ((TextBox)this.gvPersonalInfo.Rows[1].FindControl("txtgvVal")).Text.Trim();
            //Log Entry

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                //new nahid by 20220126

                if (Gcode == "01001")
                {
                    string Gvalue = (Gcode == "01001") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    if (Gvalue.Length == 0)
                    {
                        string errMsg = "Please Put ID CARD Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    DataSet copSetup = compUtility.GetCompUtility();
                    if (copSetup == null)
                        return;
                    int idCardLength = copSetup.Tables[0].Rows.Count == 0 ? 0 : Convert.ToInt32(copSetup.Tables[0].Rows[0]["hr_idcardlen"]);
                    if (Gvalue.Length != idCardLength && idCardLength != 0)
                    {
                        string errMsg = "Please Put " + idCardLength + " Digit ID CARD Number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + errMsg + "');", true);
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");

                    }

                    //// for duplicate value 
                    ///
                    ////
                    ///////////----------------------------------------
                    DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETIDCARDNO", Gvalue, "", "", "", "", "", "", "", "");
                    if (ds2.Tables[0].Rows.Count == 0)
                        ;
                    else
                    {
                        DataView dv1 = ds2.Tables[0].DefaultView;
                        dv1.RowFilter = ("empid <>'" + empid + "'");
                        DataTable dt = dv1.ToTable();
                        if (dt.Rows.Count == 0)
                            ;
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Found Duplicate ID CARD No" + "');", true);

                            return;
                        }
                    }
                    ///////////////----------------------------------------

                }

                if (Gcode == "01003")
                {
                    string Gvalue = (Gcode == "01001") ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                    if (Gvalue.Length == 0)
                    {
                        // int x = 1;
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.Color.Red;
                        //  value = value+x;
                    }
                    else
                    {
                        ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgcResDesc1")).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000");
                    }

                }

            }

            DataTable dtuser = (DataTable)Session["UserLog"];
            string tblPostedByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postedbyid"].ToString();
            string tblPostedtrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postrmid"].ToString();
            string tblPostedSession = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["postseson"].ToString();
            string tblPosteddat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["posteddat"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            //string tblEditByid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editbyid"].ToString();
            //string tblEditDat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : Convert.ToDateTime(dtuser.Rows[0]["editdat"]).ToString("dd-MMM-yyyy");
            //string tblEdittrmid = (dtuser.Rows.Count == 0) ? "" : dtuser.Rows[0]["editrmid"].ToString();

            string userid = hst["usrid"].ToString();
            string Terminal = hst["compname"].ToString();
            string Sessionid = hst["session"].ToString();
            string PostedByid = (tblPostedByid == "") ? userid : tblPostedByid;
            string Posttrmid = (tblPostedtrmid == "") ? Terminal : tblPostedtrmid;
            string PostSession = (tblPostedSession == "") ? Sessionid : tblPostedSession;
            string Posteddat = (tblPosteddat == "01-Jan-1900") ? System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : tblPosteddat;
            string EditByid = (dtuser.Rows.Count == 0) ? "" : userid;
            string Editdat = (dtuser.Rows.Count == 0) ? "01-Jan-1900" : System.DateTime.Today.ToString("dd-MMM-yyyy");
            string Editrmid = (dtuser.Rows.Count == 0) ? "" : Terminal;

            bool result = HRData.UpdateTransInfo2(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPLINF", empid, empname, PostedByid, PostSession, Posttrmid, Posteddat,
                    EditByid, Editdat, Editrmid, "", "", "", "", "", "", "", "", "", "", "", "");

            if (result == false)
                return;

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lgvgval")).Text.Trim();
                string gvalueBn = ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvValBn")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

           

                    if (Gcode == "01003" || Gcode == "01007")
                    {
                
                        Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy") : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                    }

                if (Gcode == "01008")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-Jan-1900" : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();

                }


                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "",
                            "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", gvalueBn);


                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated Fail" + "');", true);
                    return;
                }

            }

            for (int i = 0; i < this.gvPersonalInfo2.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo2.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvPersonalInfo2.Rows[i].FindControl("lgvgval")).Text.Trim();
                string gvalueBn = ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvValBn")).Text.Trim();
                string Gvalue = (((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).Items.Count == 0) ? ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvVal")).Text.Trim() : ((DropDownList)this.gvPersonalInfo2.Rows[i].FindControl("ddlval")).SelectedValue.ToString();

                if (Gcode == "01999")
                {
                    Gvalue = (((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? "01-jan-1900" : ((TextBox)this.gvPersonalInfo2.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                }

               else if (Gcode == "01050")
                {
                    Gvalue = Gvalue.Length == 0 ? "0" : Gvalue;
                }

                Gvalue = (gtype == "D") ? ASTUtility.DateFormat(Gvalue) : Gvalue;
                result = HRData.UpdateTransInfo01(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Gvalue, "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "",
                            "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", gvalueBn);
                if (!result)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated Fail" + "');", true);
                    return;
                }

            }
            this.getLastCardNo();
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);
        }
        protected void lUpdateDegree_Click(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {

                //string Maincode = ((DropDownList)this.gvDegree.Rows[i].FindControl ("ddlDegree")).Text.Trim ();

                string Gcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree")).SelectedValue.ToString();
                string gtype = ((Label)this.gvDegree.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Degree = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree")).SelectedItem.Text.Trim();
                string Group = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree")).SelectedValue.ToString();
                string Institute = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvInstituee")).Text.Trim();
                string Result = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlResult")).SelectedValue.ToString();
                string PassisnYr = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlPassingYear")).SelectedValue.ToString();
                string marksorgrade = Convert.ToDouble("0" + ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text.Trim()).ToString();
                string Scale = Convert.ToDouble("0" + ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text.Trim()).ToString();

                string majsub = "";
                string majsubcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlMajorSubj")).SelectedValue.ToString();
                string resultdt = ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvEngMedium")).Text.Trim();

                if (Gcode == "10007" || Gcode == "10008")
                {
                    majsub = resultdt;
                }
                else
                {
                    majsub = majsubcode;

                }



                if (Gcode != "99999" && Group != "999999999")
                    HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Degree, "", Group, Institute, Result, PassisnYr, "0", "", "0", "0", "0", "0", "0", "0", "", "", "", marksorgrade, Scale, "0", majsub, "01-jan-1900", "01-jan-1900", "", "", "", "");


            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);




        }
        protected void lUpdateEmprecord_Click(object sender, EventArgs e)
        {


            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvEmpRec.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvEmpRec.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvEmpRec.Rows[i].FindControl("lgvgval")).Text.Trim();
                string ComName = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgcvCompany")).Text.Trim();
                string Desig = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvDesig")).Text.Trim();

                string frmduration = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvesDurationfrm")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvesDurationfrm")).Text.Trim()).ToString("dd-MMM-yyy");
                string toDuration = ((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvesDurationto")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)this.gvEmpRec.Rows[i].FindControl("txtgvesDurationto")).Text.Trim()).ToString("dd-MMM-yyy");

                if (ComName.Length > 0)
                    HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, ComName, "", Desig, "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", frmduration, toDuration, "", "", "", "");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);



        }
        protected void lUpdateEmpAssocia_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvAssocia.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvAssocia.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvAssocia.Rows[i].FindControl("lgvgval")).Text.Trim();
                string OrgName = ((TextBox)this.gvAssocia.Rows[i].FindControl("txtgcvOrgName")).Text.Trim();
                string Position = ((TextBox)this.gvAssocia.Rows[i].FindControl("txtgvPostion")).Text.Trim();


                if (OrgName.Length > 0)
                    HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, OrgName, "", Position, "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "");

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);



        }
        protected void lUpdateRef_Click(object sender, EventArgs e)
        {

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            for (int i = 0; i < this.gvRef.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvRef.Rows[i].FindControl("lblgvItmCode")).Text.Trim();
                string gtype = ((Label)this.gvRef.Rows[i].FindControl("lgvgval")).Text.Trim();
                string Name = ((TextBox)this.gvRef.Rows[i].FindControl("txtgcvName")).Text.Trim();
                string OrgName = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvOrgname")).Text.Trim();
                string Designation = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvDesig")).Text.Trim();
                string Phone = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvPhone")).Text.Trim();
                string Mobile = ((TextBox)this.gvRef.Rows[i].FindControl("txtgvMobile")).Text.Trim();
                if (Name.Length > 0)
                    HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcode, gtype, Name, "", OrgName, Designation, Phone, Mobile, "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", "", "", "", "");

            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);

        }
        protected void gvPersonalInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtgvname = (TextBox)e.Row.FindControl("txtgvVal");
                TextBox txtgvValBn = (TextBox)e.Row.FindControl("txtgvValBn");


                string code = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gcod")).ToString();

                if (code == "")
                {
                    return;
                }
                if (code == "01002")
                {

                    txtgvname.ReadOnly = true;
                    //txtgvValBn.ReadOnly = false;
                    //txtgvValBn.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "gdesc1")).ToString();
                }

                //txtgvValBn.Text = "";

            }


        }
        protected void lbtnUpdateImg_Click(object sender, EventArgs e)
        {

        }
        protected void ddlval_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();

            string Joindate = "";
            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "01003": //Join Date

                        Joindate = (((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim() == "") ? System.DateTime.Today.ToString("dd-MMM-yyyy")
                            : ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvdVal")).Text.Trim();
                        //Joindate = ASTUtility.DateFormat(Joindate) ;
                        // ((TextBox)this.gvPersonalInfo.Rows[i].FindControl("txtgvVal")).Text = Joindate;

                        break;


                    case "01006": //Confirmation Date
                        string value = ((DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval")).SelectedItem.Text.Trim();
                        if (value == "None")
                            continue;

                        int monyear = (value.Contains("Month")) ? Convert.ToInt32(ASTUtility.Left(value, 2)) : (12 * Convert.ToInt32(ASTUtility.Left(value, 2)));
                        string ConDate = Convert.ToDateTime(ASTUtility.DateFormat(Joindate)).AddMonths(monyear).ToString("dd-MMM-yyyy");
                        
                        if(value== "0 Days")
                        {
                            ConDate = Joindate;

                        }
                         
                        ((TextBox)this.gvPersonalInfo.Rows[i + 1].FindControl("txtgvdVal")).Text = ConDate;
                        
                        
 

                        break;
                }


            }


        }
        protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlgval;
            DataSet ds1 = (DataSet)Session["tblacadeg"];
            DataTable dt1 = ds1.Tables[1].Copy();
            DataView dv1;
            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string Maincode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlDegree")).Text.Trim();
                string subcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree")).Text.Trim();



                switch (Maincode)
                {

                    case "10001": //SSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = subcode;



                        this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = false;
                        break;



                    case "10002": //HSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = false;
                        ddlgval.SelectedValue = subcode;

                        break;


                    case "10003": //Diploma
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = false;
                        ddlgval.SelectedValue = subcode;

                        break;


                    case "10004": //BSC

                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = subcode;
                        //this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = true;                   

                        break;

                    case "10005": //MSC
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = subcode;
                        // this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = true;
                        break;


                    case "10006": //Doctoral
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999' or maincode='" + Maincode + "'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        ddlgval.SelectedValue = subcode;
                        //this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = true;
                        break;

                    case "10007":
                    case "10008":
                        ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlMajorSubj")).Visible = false;
                        ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvEngMedium")).Visible = true;

                        break;

                    case "99999": //Doctoral
                        dv1 = dt1.DefaultView;
                        dv1.RowFilter = ("maincode='99999'");
                        ddlgval = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree"));
                        ddlgval.DataTextField = "subdesc";
                        ddlgval.DataValueField = "subcode";
                        ddlgval.DataSource = dt1;
                        ddlgval.DataBind();
                        // this.gvDegree.Rows[i].FindControl("ddlinstitute").Visible = true;
                        break;
                }


            }
        }
        protected void ddlResult_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string resultcode = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlResult")).SelectedValue.ToString();



                switch (resultcode)
                {
                    case "17001":  //Marks System
                    case "17002":
                    case "17003":
                    case "17004":
                    case "17005":
                    case "17006":
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Text = "Marks(%) :";
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = true;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = true;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = false;
                        break;


                    //case "17004":   //Grade System
                    //    //((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text = "";
                    //    //((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                    //    ((Label)this.gvDegree.Rows[i].FindControl ("lblgvMarks")).Text = "CGPA :";
                    //    ((Label)this.gvDegree.Rows[i].FindControl ("lblgvMarks")).Visible = true;
                    //    ((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvmarkorgrade")).Visible = true;
                    //    ((Label)this.gvDegree.Rows[i].FindControl ("lblgvScale")).Visible = true;
                    //    ((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvScale")).Visible = true;
                    //    break;

                    //case "17005": //Apeared
                    //case "17006":
                    //    //((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvmarkorgrade")).Text = "";
                    //    //((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvScale")).Text = "CGPA :";
                    //    //((Label)this.gvDegree.Rows[i].FindControl ("lblgvMarks")).Visible = true;
                    //    //((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvmarkorgrade")).Visible = true;
                    //    //((Label)this.gvDegree.Rows[i].FindControl ("lblgvScale")).Visible = true;
                    //    //((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvScale")).Visible = true;


                    //     ((Label)this.gvDegree.Rows[i].FindControl ("lblgvMarks")).Text = "CGPA :";
                    //    ((Label)this.gvDegree.Rows[i].FindControl ("lblgvMarks")).Visible = true;
                    //    ((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvmarkorgrade")).Visible = true;
                    //    ((Label)this.gvDegree.Rows[i].FindControl ("lblgvScale")).Visible = true;
                    //    ((TextBox)this.gvDegree.Rows[i].FindControl ("txtgvScale")).Visible = true;
                    //    break;

                    case "17007":// grade 
                    case "17008":
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Text = "CGPA :";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = true;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = true;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = true;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = true;
                        break;

                    case "99999": //Apeared
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Text = "";
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Text = "";
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvMarks")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvmarkorgrade")).Visible = false;
                        ((Label)this.gvDegree.Rows[i].FindControl("lblgvScale")).Visible = false;
                        ((TextBox)this.gvDegree.Rows[i].FindControl("txtgvScale")).Visible = false;
                        break;



                }


            }
        }
        protected void ibtngrdEmpList_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.gvPersonalInfo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.gvPersonalInfo.Rows[i].FindControl("lblgvItmCode")).Text.Trim();

                switch (Gcode)
                {
                    case "01996": //Supper Visor


                        string comcod = this.GetComeCode();
                        DropDownList ddl2 = (DropDownList)this.gvPersonalInfo.Rows[i].FindControl("ddlval");
                        string Searchemp = "%%";
                        DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPTIDNAME", Searchemp, "", "", "", "", "", "", "", "");

                        //ddl2.AppendDataBoundItems = true;
                        ddl2.DataTextField = "empname";
                        ddl2.DataValueField = "empid";

                        ddl2.DataSource = ds3.Tables[0];
                        ddl2.DataBind();
                        ds3.Dispose();
                        break;
                }


            }
        }
        protected void gvDegree_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblempAcaRecord"];
            string comcod = this.GetComeCode();

            string empid = this.ddlEmpName.SelectedValue.ToString();
            string Gcode = ((DropDownList)this.gvDegree.Rows[e.RowIndex].FindControl("ddlDegree")).SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPDEGREEDELETE", empid, Gcode, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {
                int rowindex = (this.gvDegree.PageSize) * (this.gvDegree.PageIndex) + e.RowIndex;
                dt.Rows[rowindex].Delete();

            }

            DataView dv = dt.DefaultView;
            ViewState.Remove("tblempAcaRecord");
            ViewState["tblempAcaRecord"] = dv.ToTable();

            this.gvDegree.DataBind();
            this.ShowDegree();

        }
        protected void lUpdateJobRes_Click(object sender, EventArgs e)
        {
            //  ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPJOBRESPONDEL", empid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated fail" + "');", true);
                return;
            }
            for (int i = 0; i < this.grvJobRespo.Rows.Count; i++)
            {
                string Gcode = ((Label)this.grvJobRespo.Rows[i].FindControl("lblgvItmCode1")).Text.Trim();
                // string gtype = ((Label)this.grvJobRespo.Rows[i].FindControl("lgvgval1")).Text.Trim();
                string jobRespons = ((TextBox)this.grvJobRespo.Rows[i].FindControl("txtgcvJobRes")).Text.Trim();

                if (jobRespons.Length > 0)
                    HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPJOBRESPONINSUPDATE", empid, Gcode, jobRespons, "", "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900");
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);
        }
        protected void ddlAcadegree_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.gvDegree.Rows.Count; i++)
            {
                string ExamDegre = ((DropDownList)this.gvDegree.Rows[i].FindControl("ddlAcadegree")).SelectedValue.ToString();

                switch (ExamDegre)
                {
                    case "":
                        this.getMajorSubject();
                        break;

                }

            }

        }
        private void getMajorSubject()
        {

        }
        protected void gvEmpRec_RowCreated(object sender, GridViewRowEventArgs e)
        {


            GridViewRow gvRow = e.Row;
            if (gvRow.RowType == DataControlRowType.Header)
            {
                GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell01 = new TableCell();
                cell01.Text = "";
                cell01.HorizontalAlign = HorizontalAlign.Center;
                cell01.ColumnSpan = 1;

                TableCell cell02 = new TableCell();
                cell02.Text = "";
                cell02.HorizontalAlign = HorizontalAlign.Center;
                cell02.ColumnSpan = 1;

                TableCell cell03 = new TableCell();
                cell03.Text = "";
                cell03.HorizontalAlign = HorizontalAlign.Center;
                cell03.ColumnSpan = 1;


                TableCell cell04 = new TableCell();
                cell04.Text = "";
                cell04.HorizontalAlign = HorizontalAlign.Center;
                cell04.ColumnSpan = 1;



                TableCell cell05 = new TableCell();
                cell05.Text = "SERVICE DURATION";
                cell05.HorizontalAlign = HorizontalAlign.Center;
                cell05.ColumnSpan = 2;
                cell05.Attributes["style"] = "font-weight:bold;";

                gvrow.Cells.Add(cell01);
                gvrow.Cells.Add(cell02);
                gvrow.Cells.Add(cell03);
                gvrow.Cells.Add(cell04);
                gvrow.Cells.Add(cell05);
                gvEmpRec.Controls[0].Controls.AddAt(0, gvrow);
            }
        }
        protected void gvFamilyInfo_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private void ShowLastDegree()
        {
            string comcod = this.GetComeCode();
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPDEGREOCU", "", "", "", "", "", "", "", "");

            //DropDownList ddl1 = (DropDownList)e.Row.FindControl ("ddllastDegree");
            //DropDownList ddl2 = (DropDownList)e.Row.FindControl ("ddlOcupation");
            DataTable dt = (DataTable)Session["tblFamilydt"];
            DropDownList ddl1, ddl2;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl1 = ((DropDownList)this.gvFamilyInfo.Rows[i].FindControl("ddllastDegree"));
                ddl2 = ((DropDownList)this.gvFamilyInfo.Rows[i].FindControl("ddlOcupation"));

                string lastdeg = dt.Rows[i]["educode"].ToString();
                ddl1.DataTextField = "degdes";
                ddl1.DataValueField = "degcod";
                ddl1.DataSource = ds.Tables[0];
                ddl1.DataBind();
                ddl1.SelectedValue = lastdeg;


                string ocu = dt.Rows[i]["ocupation"].ToString();
                ddl2.DataTextField = "ocudes";
                ddl2.DataValueField = "ocucod";

                ddl2.DataSource = ds.Tables[1];
                ddl2.DataBind();
                ddl2.SelectedValue = ocu;

            }





        }
        protected void lUpdateFamilyInfo_OnClick(object sender, EventArgs e)
        {
            //    ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            foreach (GridViewRow gvr1 in this.gvFamilyInfo.Rows)
            {
                string Gcod = ((Label)gvr1.FindControl("lblgvFamilyCode")).Text.Trim();
                string gtype = ((Label)gvr1.FindControl("lgvgvalfam")).Text.Trim();
                string lastdegree = ((DropDownList)gvr1.FindControl("ddllastDegree")).SelectedValue.ToString();
                string name = ((TextBox)gvr1.FindControl("lblgvEmpname")).Text.Trim();
                string age = ((TextBox)gvr1.FindControl("txtgvAge")).Text.Trim();
                string org = ((TextBox)gvr1.FindControl("txtorg")).Text.Trim();
                string ocupation = ((DropDownList)gvr1.FindControl("ddlOcupation")).SelectedValue.ToString();


                if (name.Length > 0)
                {
                    bool result = HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTORUPDATEHREMPDLINF", empid, Gcod, gtype, name, "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900", lastdegree, ocupation, org, age);
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated fail" + "');", true);



                    }

                }

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);


        }
        protected void lUpdateCtcInfo_OnClick(object sender, EventArgs e)
        {
            // ((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = (DataTable)Session["tblctdet"];
            ds1.Merge(dt1);
            ds1.Tables[0].TableName = "tbl1";
            string gcod = dt1.Rows[0]["gcod"].ToString();
            string particular = dt1.Rows[0]["particular"].ToString();
            bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSUPDATECTCDT", ds1, null, null, empid, gcod, particular);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated fail" + "');", true);

                return;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);


        }
        protected void lTotalClick_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblctdet"];
            int i = 0;
            foreach (GridViewRow gvr in gvCTCDetails.Rows)
            {
                string partc = ((TextBox)gvr.FindControl("txtgvParticulars")).Text.Trim();
                double salamt = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvsal")).Text.Trim());
                double trnsamt = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvtrnsamt")).Text.Trim());
                double bonus = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvbonus")).Text.Trim());
                double pfamt = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvpfamt")).Text.Trim());
                double mobal = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvmobamt")).Text.Trim());
                string lastupdt = ((TextBox)gvr.FindControl("txtgvlastupd")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)gvr.FindControl("txtgvlastupd")).Text.Trim()).ToString("dd-MMM-yyyy");
                double ctct = salamt + trnsamt + bonus - pfamt + mobal;
                ((TextBox)gvr.FindControl("txtgvtotalamt")).Text = ctct.ToString();
                dt.Rows[i]["particular"] = partc;
                dt.Rows[i]["salamt"] = salamt;
                dt.Rows[i]["trnsamt"] = trnsamt;
                dt.Rows[i]["bonus"] = bonus;
                dt.Rows[i]["pfamt"] = pfamt;
                dt.Rows[i]["mobalw"] = mobal;
                dt.Rows[i]["ctctotal"] = ctct;
                dt.Rows[i]["lastupd"] = lastupdt;
                i++;


            }
        }
        protected void lUpdateSalInfo_OnClick(object sender, EventArgs e)
        {
            //((Label)this.Master.FindControl("lblmsg")).Visible = true;

            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();

            DataSet ds1 = new DataSet("ds1");
            DataTable dt1 = (DataTable)Session["tblsaldet"];
            ds1.Merge(dt1);
            ds1.Tables[0].TableName = "tbl1";
            string gcod = dt1.Rows[0]["gcod"].ToString();
            string sal = "Salary";

            bool result = HRData.UpdateXmlTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSUPDATESALDT", ds1, null, null, empid, gcod, sal);

            if (!result)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated fail" + "');", true);


            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);


        }
        protected void lTotalClickSal_OnClick(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["tblsaldet"];
            int i = 0;
            foreach (GridViewRow gvr in gvSALDetails.Rows)
            {

                double strtsal = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvstrtsal")).Text.Trim());
                double presntsal = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvprstsal")).Text.Trim());
                double avgincr = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvavgincr")).Text.Trim());
                double avgincrper = Convert.ToDouble("0" + ((TextBox)gvr.FindControl("txtgvavgincrper")).Text.Trim());

                string lastupdt = ((TextBox)gvr.FindControl("txtgvlastupdt")).Text.Trim() == "" ? "01-Jan-1900" : Convert.ToDateTime(((TextBox)gvr.FindControl("txtgvlastupdt")).Text.Trim()).ToString("dd-MMM-yyyy");
                double saldif = presntsal - strtsal;
                ((TextBox)gvr.FindControl("txtgvsaldiff")).Text = saldif.ToString();
                dt.Rows[i]["strtsal"] = strtsal;
                dt.Rows[i]["presntsal"] = presntsal;
                dt.Rows[i]["avgincr"] = avgincr;
                dt.Rows[i]["avgincrper"] = avgincrper;
                dt.Rows[i]["saldif"] = saldif;
                dt.Rows[i]["lastupdt"] = lastupdt;
                i++;


            }
        }
        protected void gvFamilyInfo_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = (DataTable)Session["tblFamilydt"];
            string comcod = this.GetComeCode();

            string empid = this.ddlEmpName.SelectedValue.ToString();
            string Gcode = ((Label)this.gvFamilyInfo.Rows[e.RowIndex].FindControl("lblgvFamilyCode")).Text.ToString();

            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEFAMILYDT", empid, Gcode, "", "", "", "", "", "", "", "", "", "", "", "", "");

            //DataView dv = dt.DefaultView;
            //ViewState.Remove ("tblempAcaRecord");
            //ViewState["tblempAcaRecord"] = dv.ToTable ();

            this.gvFamilyInfo.DataBind();

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                //try
                //{
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    String extension = System.IO.Path.GetExtension(filename);
                    switch (extension)
                    {
                        case ".doc":
                        case ".docx":
                        case ".PDF":
                        case ".pdf":
                            break;
                        default:
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Updated fail" + "');", true);
                            return;
                            break;
                    }
                    string comcod = this.GetCompCode();
                    string empcode = ddlEmpName.SelectedValue.ToString();
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "INSERTUPDATCV", empcode, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "File Update fail !!!" + "');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "File Update Succesfull !!!" + "');", true);
                    }
                    FileUploadControl.SaveAs(Server.MapPath("~") + ("\\CV\\" + filename));

                    //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                //}
                //catch (Exception ex)
                //{

 

                //}
            }
        }
        protected void gvPersonalInfo2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetEmployeeform();", true);
            return;
        }
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string empdept = "9301";//this.ddlDept.SelectedValue.ToString().Trim().Substring(0, 9);
            string empname = this.txtEmpName.Text;
            string empcode = this.lblEmplastId.Text;
            string Message;
            bool result = true;
            if (this.txtEmpName.Text.Length < 1)
            {
                Message = "Employee name can't be empty!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
                return;
            }
            if (empcode.Length > 0)
            {
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "UPDATEEMPNAME", empcode, empname, "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                // result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPNAME", empdept, empname, "", "", "", "", "", "", "", "", "", "", "", "", "");
                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "INSERTEMPNAMELASTIDWISE", empdept, empname, "", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            if (result)
            {
                Message = "Successfully Added Employee : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
                this.txtEmpName.Text = "";
                this.lblEmplastId.Text = "";
            }
            else
            {
                Message = "Sorry, Data Updated Fail : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + Message + "');", true);
            }
            this.GetEmployeeName();
        }

        protected void ddlDegreeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string degree = this.ddlDegreeList.SelectedValue.ToString();
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPACADEGREE", empid, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            DataSet ds1 = (DataSet)Session["tblacadeg"];
            //Academic Degree
            DataTable dt1 = ds1.Tables[1].Copy();
            DataView dv1 = dt1.DefaultView;
            dv1.RowFilter = ("maincode='99999' or maincode='" + degree + "'");
            this.ddlAcadegreeList.DataTextField = "subdesc";
            this.ddlAcadegreeList.DataValueField = "subcode";
            this.ddlAcadegreeList.DataSource = dv1.ToTable();
            this.ddlAcadegreeList.DataBind();
            ddlAcadegreeList_SelectedIndexChanged(null, null);
        }

        protected void ddlAcadegreeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string accdegree = this.ddlDegreeList.SelectedValue.ToString();
            DataSet ds1 = (DataSet)Session["tblacadeg"];
            DataTable dt1 = ds1.Tables[3].Copy();

            ddlMajorSubjList.DataTextField = "gdesc";
            ddlMajorSubjList.DataValueField = "gcod";
            ddlMajorSubjList.DataSource = dt1;
            ddlMajorSubjList.DataBind();
            ddlMajorSubjList_SelectedIndexChanged(null, null);
        }



        protected void ddlMajorSubjList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds1 = (DataSet)Session["tblacadeg"];
            DataTable dt1 = ds1.Tables[2].Copy();

            ddlResultList.DataTextField = "gdesc";
            ddlResultList.DataValueField = "gcod";
            ddlResultList.DataSource = dt1;
            ddlResultList.DataBind();
        }
        protected void ddlResultList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void lUpdateInfoNomi_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            for (int i = 0; i < this.GvNominee.Rows.Count; i++)
            {
                string Gcode = ((Label)this.GvNominee.Rows[i].FindControl("lblCode")).Text.Trim();
                // string gtype = ((Label)this.grvJobRespo.Rows[i].FindControl("lgvgval1")).Text.Trim();
                string description = ((TextBox)this.GvNominee.Rows[i].FindControl("txtgvNomi")).Text.Trim();        
                HRData.UpdateTransHREMPInfo3(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "EMPJOBRESPONINSUPDATE", empid, Gcode, description, "", "", "", "", "", "", "0", "", "0", "0", "0", "0", "0", "0", "", "", "", "0", "0", "0", "", "01-jan-1900", "01-jan-1900");
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Updated Successfully" + "');", true);
        }
    }
}