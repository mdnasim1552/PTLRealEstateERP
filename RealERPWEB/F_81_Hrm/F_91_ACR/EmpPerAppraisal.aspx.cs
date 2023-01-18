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
using Microsoft.Reporting.WinForms;
using RealERPLIB;
using RealERPRPT;
namespace RealERPWEB.F_81_Hrm.F_91_ACR
{
    public partial class EmpPerAppraisal : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));

                string type = this.Request.QueryString["Type"].ToString();

                //((Label)this.Master.FindControl("lblTitle")).Text = type == "Edit" ? "EMPLOYEE PERFORMANCE APPRAISAL" : "EMPLOYEE PERFORMANCE APPRAISAL";
                this.txtCurDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                //this.GetLastPerNumber();
                //this.GetEmployeeName();
                this.GetCompanyName();
            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void GetCompanyName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();


            string txtCompany = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompanyApr.DataTextField = "actdesc";
            this.ddlCompanyApr.DataValueField = "actcode";
            this.ddlCompanyApr.DataSource = ds5.Tables[0];
            this.ddlCompanyApr.DataBind();
            this.GetDepartment();
            this.ddlCompanyApr_SelectedIndexChanged(null, null);
        }


        private void GetDepartment()
        {
            string comcod = this.GetComeCode();
            //   string type = this.Request.QueryString["Type"].ToString().Trim();
            string Company = ((this.ddlCompanyApr.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompanyApr.SelectedValue.ToString().Substring(0, 2)) + "%";

            string txtSProject = "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETDEPTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            this.ddldepartmentapr.DataTextField = "deptdesc";
            this.ddldepartmentapr.DataValueField = "deptcode";
            this.ddldepartmentapr.DataSource = ds4.Tables[0];
            this.ddldepartmentapr.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string deptcode = this.ddldepartmentapr.SelectedValue.ToString().Substring(0, 4) + "%";
            string txtSProject = "%";
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPROJECTNAME", deptcode, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds4.Tables[0];
            this.ddlProjectName.DataBind();
            this.GetEmployeeName();
        }

        private void GetEmployeeName()
        {
            string comcod = this.GetComeCode();
            string txtSProject = "%";
            string ProjectCode = this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETEMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname1";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();
            ds3.Dispose();

        }

        //private void GetEmpName()
        //{
        //    string comcod = this.GetComeCode();
        //    string ProjectCode = (this.txtEmpSrc.Text.Trim().Length > 0) ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
        //    string txtSProject = "%" + this.txtEmpSrc.Text + "%";
        //    DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPREMPNAME", ProjectCode, txtSProject, "", "", "", "", "", "", "");
        //    this.ddlEmpName.DataTextField = "empname";
        //    this.ddlEmpName.DataValueField = "empid";
        //    this.ddlEmpName.DataSource = ds5.Tables[0];
        //    this.ddlEmpName.DataBind();
        //    ViewState["tblemp"] = ds5.Tables[0];
        //}


        private void CompanyType()
        {
            string comcod = this.GetComeCode();

            if (comcod == "3338")
            {
                gvPerAppraisal.FindControl("list1").Visible = true;
                gvPerAppraisal.FindControl("list2").Visible = true;
                gvPerAppraisal.FindControl("list3").Visible = true;
                gvPerAppraisal.FindControl("list4").Visible = true;
                gvPerAppraisal.FindControl("list5").Visible = true;
                gvPerAppraisal.FindControl("list6").Visible = true;
                gvPerAppraisal.FindControl("list7").Visible = true;
                gvPerAppraisal.FindControl("list8").Visible = true;
                gvPerAppraisal.FindControl("list9").Visible = true;
                gvPerAppraisal.FindControl("list10").Visible = true;

            }


        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        protected void GetPerNumber()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
                mPerNo = this.ddlPreList.SelectedValue.ToString();

            string mProDAT = this.txtCurDate.Text.Trim();
            if (mPerNo == "NEWPER")
            {
                DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTPERNO", mProDAT,
                       "", "", "", "", "", "", "", "");
                if (ds2 == null)
                    return;
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    this.lblCurNo1.Text = ds2.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
                    this.lblCurNo2.Text = ds2.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6, 5);
                    this.ddlPreList.DataTextField = "maxperno1";
                    this.ddlPreList.DataValueField = "maxperno";
                    this.ddlPreList.DataSource = ds2.Tables[0];
                    this.ddlPreList.DataBind();
                }
            }
        }

        private void GetLastPerNumber()
        {
            string comcod = this.GetComeCode();
            string date = this.txtCurDate.Text;
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTPERNO", date, "", "", "", "", "", "", "", "");
            if (ds3 == null)
                return;
            this.lblCurNo1.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
            this.lblCurNo2.Text = ds3.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6);
        }
        protected void ibtnEmpList_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
                this.GetEmployeeName();
        }
        protected void ibtnPreList_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string sectionid = this.ddlProjectName.SelectedValue.ToString();
            string empid = this.ddlEmpName.SelectedValue.ToString();
            string empid1 = sectionid == "000000000000" ? "%%" : "%" + empid + "%";

            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPREPERNO", curdate, empid1, "", "", "", "", "", "", "");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                this.lbtnshow.Visible = true;
            }
            this.ddlPreList.DataTextField = "perno1";
            this.ddlPreList.DataValueField = "perno";
            this.ddlPreList.DataSource = ds2.Tables[0];
            this.ddlPreList.DataBind();
            ds2.Dispose();

        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            if (this.lbtnOk.Text == "Ok")
            {
                this.lbtnOk.Text = "New";
                if (this.ddlEmpName.Items.Count > 0)
                {
                    this.ddlEmpName.Visible = false;
                    this.ddlEmpName.Enabled = false;
                    this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim().Substring(13);
                    this.lblEmpname.Visible = true;
                    this.lblprelist.Visible = false;
                    this.ibtnPreList.Visible = false;
                    this.ddlPreList.Visible = false;
                    this.ShowPerformance();
                    return;
                }

                this.ddlEmpName.Visible = true;
                this.ddlEmpName.Enabled = true;
                this.lblprelist.Visible = false;
                this.ibtnPreList.Visible = false;
                this.ddlPreList.Visible = false;
                this.lbtnshow.Visible = false;
                return;
            }
            else
            {
                this.lbtnOk.Text = "Ok";
                this.ddlEmpName.Items.Clear();
                this.ddlPreList.Items.Clear();
                this.gvPerAppraisal.DataSource = null;
                this.gvPerAppraisal.DataBind();
                this.ddlEmpName.Visible = true;
                this.lblEmpname.Visible = false;
                this.ddlEmpName.Enabled = true;
                this.lblprelist.Visible = true;
                this.ibtnPreList.Visible = true;
                this.ddlPreList.Visible = true;
                this.txtCurDate.Enabled = true;
                this.lblCurNo1.Text = "";
                this.lblCurNo2.Text = "";
                this.ddlEmpName.DataSource = null;
                this.ddlEmpName.DataBind();
                this.GetProjectName();
            }


        }

        private void ShowPerformance()
        {
            ViewState.Remove("tblper");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mPerNo = this.ddlPreList.SelectedValue.ToString();
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPERINFO", mPerNo, CurDate1,
                          "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblper"] = ds1.Tables[0];


            //if (mPerNo == "NEWPER")
            //{
            //    ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETLASTPERNO", CurDate1, "", "", "", "", "", "", "", "");
            //    if (ds1 == null)
            //        return;
            //    if (ds1.Tables[0].Rows.Count > 0)
            //    {
            //        this.lblCurNo1.Text = ds1.Tables[0].Rows[0]["maxperno1"].ToString().Substring(0, 6);
            //        this.lblCurNo2.Text = ds1.Tables[0].Rows[0]["maxperno1"].ToString().Substring(6, 5);
            //    }
            //    return;
            //}

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["perdate"]).ToString("dd-MMM-yyyy");
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.ddlEmpName.SelectedValue = ds1.Tables[1].Rows[0]["empid"].ToString();
                this.lblEmpname.Text = this.ddlEmpName.SelectedItem.Text.Trim();

            }
            //lblgvsgval5

            this.Data_DataBind();


        }

        private void hideGridColumn()
        {
            string comcod = this.GetComeCode();
            if (comcod == "3101" || comcod == "3348" || comcod == "3355")
            {
                this.gvPerAppraisal.Columns[6].Visible = false;
                this.gvPerAppraisal.Columns[7].Visible = false;
                this.gvPerAppraisal.Columns[8].Visible = false;
                this.gvPerAppraisal.Columns[9].Visible = false;
                this.gvPerAppraisal.Columns[10].Visible = false;
                this.gvPerAppraisal.Columns[11].Visible = false;

                for (int i = 0; i < this.gvPerAppraisal.Rows.Count; i++)
                {


                    ((Label)gvPerAppraisal.Rows[i].FindControl("lblgvDescription")).Width = new Unit(300);
                    ((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval1")).Width = new Unit(150);
                    ((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval2")).Width = new Unit(150);
                    ((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval3")).Width = new Unit(150);
                    ((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval4")).Width = new Unit(150);
                    ((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval5")).Width = new Unit(150);

                }
            }

        }


        private void Data_DataBind()
        {

            DataTable dt = (DataTable)ViewState["tblper"];
            this.gvPerAppraisal.DataSource = (DataTable)ViewState["tblper"];
            this.gvPerAppraisal.DataBind();
            this.hideGridColumn();
            if(dt.Rows.Count>0)
            {
                ((LinkButton)this.gvPerAppraisal.FooterRow.FindControl("btndeleteApprisal")).Visible = (this.Request.QueryString["Type"].ToString().Trim() == "Edit");

            }



            //for (int i = 0; i < this.gvPerAppraisal.Rows.Count; i++)
            //{
            //    string sgval1 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval1")).Checked) ? "True" : "False";
            //    string sgval2 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval2")).Checked) ? "True" : "False";
            //    string sgval3 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval3")).Checked) ? "True" : "False";
            //    string sgval4 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval4")).Checked) ? "True" : "False";

            //    CheckBox list1 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list1");
            //    CheckBox list2 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list2");
            //    CheckBox list3 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list3");
            //    CheckBox list4 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list4");
            //    CheckBox list5 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list5");

            //    CheckBox list6 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list6");
            //    CheckBox list7 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list7");
            //    CheckBox list8 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list8");
            //    CheckBox list9 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list9");
            //    CheckBox list10 = (CheckBox)this.gvPerAppraisal.Rows[i].FindControl("list10");




            //    if (Convert.ToBoolean(sgval1) == true)
            //    {
            //        if (Convert.ToInt16(dt.Rows[i]["mark"]) == 1)
            //        {
            //            list1 .Checked= true;
            //        }

            //        else if (Convert.ToInt16(dt.Rows[i]["mark"]) == 2)
            //        {
            //            list2.Checked = true;

            //        }

            //        else if (Convert.ToInt16(dt.Rows[i]["mark"]) == 3)
            //        {
            //            list3.Checked = true;

            //        }


            //    }

            //    else if (Convert.ToBoolean(sgval2) == true)
            //    {
            //        if (Convert.ToInt16(dt.Rows[i]["mark1"]) == 4)
            //        {
            //            list4.Checked = true;
            //        }

            //        else if (Convert.ToInt16(dt.Rows[i]["mark1"]) == 5)
            //        {
            //            list5.Checked = true;

            //        }

            //        else if (Convert.ToInt16(dt.Rows[i]["mark1"]) == 6)
            //        {
            //            list6.Checked = true;

            //        }


            //    }

            //    else if (Convert.ToBoolean(sgval3) == true)
            //    {
            //        if (Convert.ToInt16(dt.Rows[i]["mark2"]) == 7)
            //        {
            //            list7.Checked = true;
            //        }

            //        else if (Convert.ToInt16(dt.Rows[i]["mark2"]) == 8)
            //        {
            //            list8.Checked = true;

            //        }




            //    }

            //    else if (Convert.ToBoolean(sgval4) == true)
            //    {
            //        if (Convert.ToInt16(dt.Rows[i]["mark3"]) == 9)
            //        {
            //            list9.Checked = true;
            //        }

            //        else if (Convert.ToInt16(dt.Rows[i]["mark3"]) == 10)
            //        {
            //            list10.Checked = true;

            //        }




            //    }

            //}

            //string  mark = ((RadioButtonList)gvPerAppraisal.Rows[i].FindControl("list2")).SelectedValue.ToString();   


        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)ViewState["tblper"];
            for (int i = 0; i < this.gvPerAppraisal.Rows.Count; i++)
            {
                string sgval1 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval1")).Checked) ? "True" : "False";
                string sgval2 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval2")).Checked) ? "True" : "False";
                string sgval3 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval3")).Checked) ? "True" : "False";
                string sgval4 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval4")).Checked) ? "True" : "False";
                string sgval5 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval5")).Checked) ? "True" : "False";
                string sgval6 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval6")).Checked) ? "True" : "False";
                string sgval7 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval7")).Checked) ? "True" : "False";
                string sgval8 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval8")).Checked) ? "True" : "False";
                string sgval9 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval9")).Checked) ? "True" : "False";
                string sgval10 = (((CheckBox)gvPerAppraisal.Rows[i].FindControl("lblgvsgval10")).Checked) ? "True" : "False";



                dt.Rows[i]["sgval1"] = sgval1;
                dt.Rows[i]["sgval2"] = sgval2;
                dt.Rows[i]["sgval3"] = sgval3;
                dt.Rows[i]["sgval4"] = sgval4;
                dt.Rows[i]["sgval5"] = sgval5;
                dt.Rows[i]["sgval6"] = sgval6;
                dt.Rows[i]["sgval7"] = sgval7;
                dt.Rows[i]["sgval8"] = sgval8;
                dt.Rows[i]["sgval9"] = sgval9;
                dt.Rows[i]["sgval10"] = sgval10;


            }
            Session["tblper"] = dt;



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComeCode();
            switch (comcod)
            {
                case "3101":
                case "3348":
                    this.RptPrintAppEng();
                    break;
                case "3338":
                    this.RptPrintAppBangla();
                    break;
            }

        }

        private void RptPrintAppEng()
        {
            //string comcod = this.GetComeCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string mPerNo = this.ddlPreList.SelectedValue.ToString();
            //DataSet ds1 = HRData.GetTransInfo (comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEEVAINFO", mPerNo, "", "", "", "", "", "", "", "");
            //DataTable dt = ds1.Tables[0];
            //if (dt == null)
            //{
            //    return;
            //}



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string mPerNo = this.ddlPreList.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEACRINFO", mPerNo, "", "", "", "", "", "", "", "");

            if (ds1.Tables[1].Rows.Count == 0)
            {
                return;
            }
            DataTable dt = ds1.Tables[1];

            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_91_ACR.RptEmpPerAppraisal();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtRefno = rptstate.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            txtRefno.Text = "Ref: " + dt.Rows[0]["refno"].ToString();
            TextObject txtPerdate = rptstate.ReportDefinition.ReportObjects["txtPerdate"] as TextObject;
            txtPerdate.Text = "Date: " + Convert.ToDateTime(dt.Rows[0]["perdate"]).ToString("dd-MMM-yyyy");

            TextObject txtEmpName = rptstate.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            txtEmpName.Text = dt.Rows[0]["empname"].ToString();
            TextObject txtEmpIdcard = rptstate.ReportDefinition.ReportObjects["txtEmpIdcard"] as TextObject;
            txtEmpIdcard.Text = dt.Rows[0]["empidcardno"].ToString();
            TextObject txtEmpDesig = rptstate.ReportDefinition.ReportObjects["txtEmpDesig"] as TextObject;
            txtEmpDesig.Text = dt.Rows[0]["empdesig"].ToString();
            TextObject txtEmpjoindate = rptstate.ReportDefinition.ReportObjects["txtEmpjoindate"] as TextObject;
            txtEmpjoindate.Text = Convert.ToDateTime(dt.Rows[0]["empjoindate"]).ToString("dd-MMM-yyyy");
            TextObject txtEmpEvaperiod = rptstate.ReportDefinition.ReportObjects["txtEmpEvaperiod"] as TextObject;
            txtEmpEvaperiod.Text = dt.Rows[0]["evaperiod"].ToString();
            TextObject txtEmpPreSal = rptstate.ReportDefinition.ReportObjects["txtEmpPreSal"] as TextObject;
            txtEmpPreSal.Text = Convert.ToDouble(dt.Rows[0]["empgssal"]).ToString("#,##0;(#,##0); ");
            TextObject txtEmpLasIncamADate = rptstate.ReportDefinition.ReportObjects["txtEmpLasIncamADate"] as TextObject;
            txtEmpLasIncamADate.Text = dt.Rows[0]["incamtadate"].ToString();


            TextObject txtSupName = rptstate.ReportDefinition.ReportObjects["txtSupName"] as TextObject;
            txtSupName.Text = dt.Rows[0]["sname"].ToString();
            TextObject txtSupIdcard = rptstate.ReportDefinition.ReportObjects["txtSupIdcard"] as TextObject;
            txtSupIdcard.Text = dt.Rows[0]["sidcardno"].ToString();
            TextObject txtSupDesig = rptstate.ReportDefinition.ReportObjects["txtSupDesig"] as TextObject;
            txtSupDesig.Text = dt.Rows[0]["sdesig"].ToString();
            TextObject txtSupSection = rptstate.ReportDefinition.ReportObjects["txtSupSection"] as TextObject;
            txtSupSection.Text = dt.Rows[0]["ssection"].ToString();
            TextObject txtSupSerlength = rptstate.ReportDefinition.ReportObjects["txtSupSerlength"] as TextObject;
            txtSupSerlength.Text = dt.Rows[0]["sserlength"].ToString();
            TextObject txtSupPreSal = rptstate.ReportDefinition.ReportObjects["txtSupPreSal"] as TextObject;
            txtSupPreSal.Text = Convert.ToDouble(dt.Rows[0]["sgssal"]).ToString("#,##0;(#,##0); ");

            rptstate.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void RptPrintAppBangla()
        {
            //string comcod = this.GetComeCode();
            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string comsnam = hst["comsnam"].ToString();
            //string comadd = hst["comadd1"].ToString();
            //string session = hst["session"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            //string mPerNo = this.ddlPreList.SelectedValue.ToString();
            //DataSet ds1 = HRData.GetTransInfo (comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEEVAINFO", mPerNo, "", "", "", "", "", "", "", "");
            //DataTable dt = ds1.Tables[0];
            //if (dt == null)
            //{
            //    return;
            //}



            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string mPerNo = this.ddlPreList.SelectedValue.ToString();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_EMPLOYEE_ACR", "RPTEMPLOYEEACRINFO", mPerNo, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            DataTable dt = ds1.Tables[1];
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_91_ACR.RptEmpPerAppBangla();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;

            TextObject txtRefno = rptstate.ReportDefinition.ReportObjects["txtRefno"] as TextObject;
            txtRefno.Text = "Ref: " + dt.Rows[0]["refno"].ToString();
            TextObject txtPerdate = rptstate.ReportDefinition.ReportObjects["txtPerdate"] as TextObject;
            txtPerdate.Text = "Date: " + Convert.ToDateTime(dt.Rows[0]["perdate"]).ToString("dd-MMM-yyyy");

            TextObject txtEmpName = rptstate.ReportDefinition.ReportObjects["txtEmpName"] as TextObject;
            txtEmpName.Text = dt.Rows[0]["empname"].ToString();
            TextObject txtEmpIdcard = rptstate.ReportDefinition.ReportObjects["txtEmpIdcard"] as TextObject;
            txtEmpIdcard.Text = dt.Rows[0]["empidcardno"].ToString();
            TextObject txtEmpDesig = rptstate.ReportDefinition.ReportObjects["txtEmpDesig"] as TextObject;
            txtEmpDesig.Text = dt.Rows[0]["empdesig"].ToString();
            TextObject txtEmpjoindate = rptstate.ReportDefinition.ReportObjects["txtEmpjoindate"] as TextObject;
            txtEmpjoindate.Text = Convert.ToDateTime(dt.Rows[0]["empjoindate"]).ToString("dd-MMM-yyyy");
            TextObject txtEmpEvaperiod = rptstate.ReportDefinition.ReportObjects["txtEmpEvaperiod"] as TextObject;
            txtEmpEvaperiod.Text = dt.Rows[0]["evaperiod"].ToString();
            TextObject txtEmpPreSal = rptstate.ReportDefinition.ReportObjects["txtEmpPreSal"] as TextObject;
            txtEmpPreSal.Text = Convert.ToDouble(dt.Rows[0]["empgssal"]).ToString("#,##0;(#,##0); ");
            TextObject txtEmpLasIncamADate = rptstate.ReportDefinition.ReportObjects["txtEmpLasIncamADate"] as TextObject;
            txtEmpLasIncamADate.Text = dt.Rows[0]["incamtadate"].ToString();


            TextObject txtSupName = rptstate.ReportDefinition.ReportObjects["txtSupName"] as TextObject;
            txtSupName.Text = dt.Rows[0]["sname"].ToString();
            TextObject txtSupIdcard = rptstate.ReportDefinition.ReportObjects["txtSupIdcard"] as TextObject;
            txtSupIdcard.Text = dt.Rows[0]["sidcardno"].ToString();
            TextObject txtSupDesig = rptstate.ReportDefinition.ReportObjects["txtSupDesig"] as TextObject;
            txtSupDesig.Text = dt.Rows[0]["sdesig"].ToString();
            TextObject txtSupSection = rptstate.ReportDefinition.ReportObjects["txtSupSection"] as TextObject;
            txtSupSection.Text = dt.Rows[0]["ssection"].ToString();
            TextObject txtSupSerlength = rptstate.ReportDefinition.ReportObjects["txtSupSerlength"] as TextObject;
            txtSupSerlength.Text = dt.Rows[0]["sserlength"].ToString();
            TextObject txtSupPreSal = rptstate.ReportDefinition.ReportObjects["txtSupPreSal"] as TextObject;
            txtSupPreSal.Text = Convert.ToDouble(dt.Rows[0]["sgssal"]).ToString("#,##0;(#,##0); ");

            rptstate.SetDataSource(ds1.Tables[0]);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }
        protected void lbtnUpPerAppraisal_Click(object sender, EventArgs e)
        {
            try
            {
                ((Label)this.Master.FindControl("lblmsg")).Visible = true;
                this.SaveValue();
                string comcod = this.GetComeCode();
                DataTable dt = (DataTable)ViewState["tblper"];

                //if (comcod == "3338")
                //{
                //    lblMessage.Text = "";
                //    int count = 0;
                //    foreach (GridViewRow row in GridLeaveApproved.Rows)
                //    {
                //        CheckBox ch = (CheckBox)row.FindControl("CheckBox1");
                //        if (ch.Checked)
                //        {
                //            count++;
                //            if (count > 1)
                //            {
                //                lblMessage.Text = "Please select only one row";
                //                return;
                //            }
                //        }
                //    }
                //}





                if (this.ddlPreList.Items.Count == 0)
                    this.GetPerNumber();
                string empid = this.ddlEmpName.SelectedValue.ToString();
                string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
                string prono = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
                string refno = this.txtrefno.Text.Trim();
                string narration = this.txtNarr.Text.Trim();

                bool result = false;


                if (this.Request.QueryString["Type"].ToString() == "Entry")
                {
                    DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "CHKPERFORMANCENO", empid, curdate,
                                         "", "", "", "", "", "", "");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data is already exists !!!');", true);
                        // ((Label)this.Master.FindControl("lblmsg")).Text = "Data is already exists";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

                        return;

                    }

                }




                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEPERB", prono, empid, curdate, refno, narration,
                  "", "", "", "", "", "", "", "", "", "");
                if (!result)
                    return;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string gcod = dt.Rows[i]["gcod"].ToString();
                    string sgcod = "";
                    for (int j = 1; j <= 10; j++)
                    {
                        sgcod = Convert.ToString("0" + j);
                        if (sgcod == "010")
                        {
                            sgcod = sgcod.Substring(1, 2);

                        }
                        bool chkgval = Convert.ToBoolean(dt.Rows[i]["sgval" + j.ToString()]);
                        if (chkgval)
                            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEPERA", prono, gcod, sgcod, "",
                         "", "", "", "", "", "", "", "", "", "", "");
                        if (!result)
                            return;
                        if (chkgval)
                            break;
                    }


                }
                ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }
            catch (Exception ex)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            }


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    string gcod = dt.Rows[i]["gcod"].ToString();
            //    string sgcod = "";
            //    string mark="";
            // for (int j = 1; j <=; j++) 
            //{

            //     sgcod =Convert.ToString("0"+j);
            //      if (sgcod=="01")
            //      {
            //          if(((CheckBox)gvPerAppraisal.Rows[i].FindControl("list1")).Checked)
            //          {
            //              mark= Convert.ToDouble(1).ToString();
            //          }

            //          else if(((CheckBox)gvPerAppraisal.Rows[i].FindControl("list2")).Checked)
            //          {
            //              mark= Convert.ToDouble(2).ToString();
            //          }
            //          else if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list3")).Checked)
            //          {
            //              mark= Convert.ToDouble(3).ToString();

            //          }


            //      }
            //      else if(sgcod=="02")
            //      {

            //          if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list4")).Checked)
            //          {
            //              mark = Convert.ToDouble(4).ToString();
            //          }

            //          else if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list5")).Checked)
            //          {
            //              mark = Convert.ToDouble(5).ToString();
            //          }
            //          else if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list6")).Checked)
            //          {
            //              mark = Convert.ToDouble(6).ToString();

            //          }

            //      }

            //      else if (sgcod == "03")
            //      {


            //          if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list7")).Checked)
            //          {
            //              mark = Convert.ToDouble(7).ToString();
            //          }

            //          else if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list8")).Checked)
            //          {
            //              mark = Convert.ToDouble(8).ToString();
            //          }


            //      }
            //      else if (sgcod == "04")
            //      {
            //          if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list9")).Checked)
            //          {
            //              mark = Convert.ToDouble(9).ToString();
            //          }

            //          else if (((CheckBox)gvPerAppraisal.Rows[i].FindControl("list10")).Checked)
            //          {
            //              mark = Convert.ToDouble(10).ToString();
            //          }


            //      }

            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            string gcod = dt.Rows[i]["gcod"].ToString();
            //            string sgcod = "";
            //            for (int j = 1; j <= 4; j++)
            //            {
            //                sgcod = Convert.ToString("0" + j);
            //                bool chkgval = Convert.ToBoolean(dt.Rows[i]["sgval" + j.ToString()]);
            //                if (chkgval)
            //                    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEPERA", prono, gcod, sgcod, "",
            //                 "", "", "", "", "", "", "", "", "", "", "");
            //                if (!result)
            //                    return;
            //                if (chkgval)
            //                    break;
            //            }



            //           bool chkgval=Convert.ToBoolean( dt.Rows[i]["sgval"+j.ToString()]);  
            //         if(chkgval)
            //             result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "INSORUPDATEPERA", prono, gcod, sgcod, mark,
            //          "", "", "", "", "", "", "", "", "", "","");
            //        if (!result)
            //            return;
            //        if (chkgval)
            //            break;
            //        }


            //    }
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            //}
            //catch (Exception ex)
            //{
            // ((Label)this.Master.FindControl("lblmsg")).Text = "Error: " + ex.Message;
            // ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);

            //}

        }
        protected void btndeleteApprisal_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            string comcod = this.GetComeCode();
            string curdate = Convert.ToDateTime(this.txtCurDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string prono = this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + curdate.Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "DELETEAPPRAISAL", prono, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Data Deleted successfully');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);

            }

        }

        protected void ibtnFindCompanyApr_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }
        protected void ddlCompanyApr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void lnkbtnSerOk_Click(object sender, EventArgs e)
        {

        }
        protected void lbtndeptapr_Click(object sender, EventArgs e)
        {

        }


        protected void ddldepartmentapr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.ddlProjectName_SelectedIndexChanged(null, null);
            this.GetProjectName();
        }
        protected void lbtnshow_Click(object sender, EventArgs e)
        {
            this.ShowPrevPerformance();
        }
        private void ShowPrevPerformance()
        {
            ViewState.Remove("tblper");
            string comcod = this.GetComeCode();
            string CurDate1 = this.txtCurDate.Text.Trim();
            string mPerNo = "NEWPER";
            if (this.ddlPreList.Items.Count > 0)
            {
                this.txtCurDate.Enabled = false;
                mPerNo = this.ddlPreList.SelectedValue.ToString();
            }

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ACR_EMPLOYEE", "GETPERINFO", mPerNo, CurDate1, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;

            ViewState["tblper"] = ds1.Tables[0];

            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.lblCurNo1.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(0, 6);
                this.lblCurNo2.Text = ds1.Tables[1].Rows[0]["perno1"].ToString().Substring(6, 5);
                this.txtCurDate.Text = Convert.ToDateTime(ds1.Tables[1].Rows[0]["perdate"]).ToString("dd-MMM-yyyy");
                this.txtrefno.Text = ds1.Tables[1].Rows[0]["refno"].ToString();
                this.txtNarr.Text = ds1.Tables[1].Rows[0]["rmks"].ToString();

                this.ddlEmpName.Visible = false;
                this.lblEmpname.Visible = true;
                this.lblEmpname.Text = ds1.Tables[1].Rows[0]["empname"].ToString();
                this.lbtnshow.Visible = false;
                this.lbtnOk.Text = "New";
            }
            this.Data_DataBind();
        }
    }
}