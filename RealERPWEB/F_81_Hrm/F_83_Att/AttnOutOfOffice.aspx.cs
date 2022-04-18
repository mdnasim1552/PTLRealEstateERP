﻿using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class AttnOutOfOffice : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                ((Label)this.Master.FindControl("lblTitle")).Text = "Online Attendance";

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string userrole = hst["userrole"].ToString();
                if(userrole != "3")
                {
                    this.GetCompany();
                    this.topPanle.Visible = true;
                    string comcod = GetCompCode();
                    if (comcod == "3365" || comcod == "3101")
                    {
                        this.GetEmpName();
                        this.WorkComments.Visible = false;
                        this.ReasonType.Visible = false;

                    }

                }
                else
                {
                    string comcod = GetCompCode();
                    if (comcod == "3365"||comcod=="3101")
                    {
                        this.GetEmpName();
                        this.ShowEmp.Visible = true;
                        this.ReasonType.Visible = false;
                        this.WorkComments.Visible = false;
                    }
                    else
                    {
                        this.ShowEmp.Visible = false;
                    }
                    lblCurrentDate.Text= "Current Time: "+ System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                }
                
                
                this.txtfromdate.Text= System.DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                this.GetEmpAttandance();
            }
        }

        private void GetEmpAttandance()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();            
            string userrole = hst["userrole"].ToString();
            string empid = (userrole == "3" ? hst["empid"].ToString() : this.ddlEmpNameAllInfo.SelectedValue.ToString());
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "GETATTANDANCEINFOINDIVIDUAL", empid, "", "", "", "", "", "", "", "");
            if (ds5 == null)
            {
                return;
            }

            if (comcod == "3365" || comcod == "3101")
            {
                this.btnSaveAttn.Text = "Save";
            }
            else
            {
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    this.btnSaveAttn.Text = "punch Out";
                }
                else
                {
                    this.btnSaveAttn.Text = "punch In";
                }
            }
        }

        private void GetCompany()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetCompCode();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }
        private void GetDptName()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;


            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string txtSProject = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDpt.DataTextField = "actdesc";
            this.ddlDpt.DataValueField = "actcode";
            this.ddlDpt.DataSource = ds1.Tables[0];
            this.ddlDpt.DataBind();
            this.ddlDpt_SelectedIndexChanged(null, null);

        }

        private void SectionName()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlDpt.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
            this.GetEmpName();
        }

        private void GetEmpName()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userrole = hst["userrole"].ToString();
            string empid = hst["empid"].ToString();

            string comcod = this.GetCompCode();
            string ProjectCode = (this.ddlSection.SelectedValue.Trim().Length > 0) ? "%" : this.ddlSection.SelectedValue.ToString() + "%";
            string txtSProject = "%%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETEMPLOYEEINOUTLIST", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmpNameAllInfo.DataTextField = "empname";
            this.ddlEmpNameAllInfo.DataValueField = "empid";
            this.ddlEmpNameAllInfo.DataSource = ds5.Tables[0];
            this.ddlEmpNameAllInfo.DataBind();
            if (userrole == "3")
            {
                this.ddlEmpNameAllInfo.SelectedValue = empid;
                this.ddlEmpNameAllInfo.Enabled=false;

            }
            ViewState["tblemp"] = ds5.Tables[0];

        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void getEmpId()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = GetCompCode();
        }
        protected void btnSaveAttn_Click(object sender, EventArgs e)
        {
            string comcod = GetCompCode();
            switch (comcod)
            {
                case "3101":
                case "3365":
                    this.InsertUpdateAttBti();
                    break;

                default:

                    this.InsertUpdateOutofoffice();
                    break;
            }         
        }

        private void InsertUpdateAttBti()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = this.GetCompCode();
            string date = System.DateTime.Now.ToString() ;
           
            string dayid = Convert.ToDateTime(date).ToString("yyyyMMdd");

            //result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "DELETEOFFTIME", dayid, "", "", "", "", "", "", "", "", "", "", "", "", "", "");


            //string absent = dt.Rows[i]["absnt"].ToString().Trim();
            //string leave = dt.Rows[i]["leave"].ToString().Trim();
            //if ((absent != "A") && (leave != "L"))
            //{
                string userrole = hst["userrole"].ToString();
                string empid = this.ddlEmpNameAllInfo.SelectedValue.ToString();
                 if(empid== "000000000000")
                 {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Please Enter Idcard No." + "');", true);
                return;
                 }
                
                string hrempid = (userrole == "3" ? hst["empid"].ToString() : this.ddlEmpNameAllInfo.SelectedValue.ToString());
                string machid = "01";
                string idcardno =this.ddlEmpNameAllInfo.SelectedItem.Text.ToString().Substring(0,5);
                string intime = date;
              
                string outtime = Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + " " + "17:00:00";
                string offintime =  Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + " " + "9:00:00";
                string offoutime = Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + " " + "17:00:00";
                string lnintime = Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + " " + "13:00:00";
                string lnoutime = Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + " " + "14:00:00";
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTORUPEMPOFFTIMEAUTO", dayid, empid, machid, idcardno, intime, outtime, offintime, offoutime, lnintime, lnoutime, "", "", "", "", "");
                if (result == true)
                {

                    string eventtype = "999";
                    string eventdesc = this.txtNote.Text.Trim();
                    string eventdesc2 = empid;
                    bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Save Successfully" + "');", true);
                }


            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Updated Successfully');", true);
        }

        private void InsertUpdateOutofoffice()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = GetCompCode();
            string usrid = hst["usrid"].ToString();
            string Sessionid = hst["session"].ToString();
            string Date = this.txtfromdate.Text;
            string userrole = hst["userrole"].ToString();
            string empid = (userrole == "3" ? hst["empid"].ToString() : this.ddlEmpNameAllInfo.SelectedValue.ToString());
            string reason = this.ddlReson.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_ATTENDENCE", "INSERTOUTOFOFFICEATTENDANCE", usrid, Sessionid, Date, empid, reason, "", "", "", "", "", "", "", "", "", "");
            if (result == true)
            {

                string eventtype = "999";
                string eventdesc = this.txtNote.Text.Trim();
                string eventdesc2 = empid;
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + "Data Save Successfully" + "');", true);
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDptName();
        }

        protected void ddlDpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            SectionName();
        }

        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEmpNameAllInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpAttandance();
        }
    }
}