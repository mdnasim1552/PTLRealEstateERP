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
namespace RealERPWEB.F_81_Hrm.F_82_App
{
    public partial class EmpEntryForm : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("~/AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = "Employee Name Entry";

                this.GetCompany();
                GetDepartment();
                this.chkNewEmp.Checked = true;
                this.chkNewEmp_CheckedChanged(null, null);
                this.GetEmpList();

                // this.getLastEmpid();

            }
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private string GetLastUSerID()
        {
            string comcod = this.GetComeCode();
           
            DataSet ds1 = HRData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "GETLASTUSERID", "", "", "", "", "", "", "", "", "");
            string userid = ds1.Tables[0].Rows[0]["userid"].ToString();
            return (userid);
        }
        private void GetCompany()
        {
            string comcod = this.GetComeCode();
            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANY", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompName.DataTextField = "sirdesc";
            this.ddlCompName.DataValueField = "sircode";
            this.ddlCompName.DataSource = ds1.Tables[0];
            this.ddlCompName.DataBind();
            ds1.Dispose();
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
        }

        // img btn click 
        protected void imgbtnComp_Click(object sender, EventArgs e)
        {
            this.GetCompany();
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
            GetEmpList();

        }

        private void getLastEmpid()
        {
            string comcod = this.GetComeCode();
            string compny = ASTUtility.Left(ddlCompName.SelectedValue, 2);
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "LASTEMPID", compny, "", "", "", "", "", "", "", "");
            this.lblEmplastId.Text = ds1.Tables[0].Rows[0]["lastempid"].ToString();
        }

        protected void chkNewEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkNewEmp.Checked)
            {
                this.txtEmpName.Text = "";
            }
        }


        protected void lnkNextbtn_Click(object sender, EventArgs e)
        {
            this.getLastEmpid();
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('EmpEntry01.aspx?Type=Entry&empid=" + this.lblEmplastId.Text + "', target='_self');</script>";
        }

        private void GetDepartment()
        {

            string comcod = this.GetComeCode();
            string txtCompanyname = "94%";
            string txtSearchDept = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "sirdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetComeCode();
            string projectcode = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString() + "%";
            string txtSSec = "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "sectionname";
            this.ddlProjectName.DataValueField = "section";
            this.ddlProjectName.DataSource = ds2.Tables[0];
            this.ddlProjectName.DataBind();

            ddlProjectName_SelectedIndexChanged(null, null);
        }

        private void GetEmpList()
        {

            Session.Remove("tblEmpstatus");
            string comcod = this.GetComeCode();

            string Company = "94%";
            string Deptid = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";

            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GET_ALL_EMP_LIST", Company, Deptid, secid, "", "", "", "", "", "");
            if (ds4 == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }

            Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();

        }
        private void LoadGrid()
        {
            string filtertype = this.ddlfilterby.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["tblEmpstatus"];

            DataView dv = dt.DefaultView;

            if (filtertype == "01")
            {
                dv.RowFilter = "idcardno =''";

            }
            else if (filtertype == "02")
            {
                dv.RowFilter = "idcardno <>''";
            }
            else if (filtertype == "03")
            {
                dv.RowFilter = "genbtn='False' and idcardno <>''"; 
            }
            else if (filtertype == "04")
            {
                dv.RowFilter = "genbtn='True'";
            }

            dt = dv.ToTable();
            this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpList.DataSource = dt;
            this.gvEmpList.DataBind();

        }
        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string company, secid;
            company = dt1.Rows[0]["company"].ToString();
            secid = dt1.Rows[0]["secid"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["secid"].ToString() == secid)
                {

                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                        dt1.Rows[j]["companyname"] = "";

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";
                }


                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }

            return dt1;

        }

        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }



        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProjectName();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpList();
        }

        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetEmployeeform();", true);
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = ((Label)this.gvEmpList.Rows[index].FindControl("lblEmpid")).Text.ToString();
            string empNAme = ((Label)this.gvEmpList.Rows[index].FindControl("lblEmpName")).Text.ToString();

            this.txtEmpName.Text = empNAme;
            this.lblEmplastId.Text = empid;
            this.lnkbtnSave.Text = "Update";
        }

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "GetEmployeeform();", true);
            return;
        }

        protected void hypDelbtn_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string Message;

            string comcod = this.GetComeCode();
            string empid = ((Label)this.gvEmpList.Rows[index].FindControl("lblEmpid")).Text.ToString();
            string empname = ((Label)this.gvEmpList.Rows[index].FindControl("lblEmpName")).Text.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "DELETEEMPLOYEE", empid, "", "", "", "", "", "", "", "");

            if (result != false)
            {
                Message = "Successfully deleted Employee : " + empname;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + Message + "');", true);
            }
            GetEmpList();
        }

        protected void ddlfilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmpList();
        }

        protected void lnkUserGenarate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string Message;
            string msg;
            string comcod = this.GetComeCode();
            string usrid = this.GetLastUSerID();
            string empid = ((Label)this.gvEmpList.Rows[index].FindControl("lblEmpid")).Text.ToString();
            string usrfname = ((Label)this.gvEmpList.Rows[index].FindControl("lblEmpName")).Text.ToString();
            string usrsname = ((Label)this.gvEmpList.Rows[index].FindControl("lblgvcardnoemp")).Text.ToString();
            string usrdesig = ((Label)this.gvEmpList.Rows[index].FindControl("lblgvdesignationemp")).Text.ToString();
            string usrpass = (comcod == "3365"? "123" : comcod == "3354"? "ER@1234%":"123");
            string usrrmrk = "";
            string active ="1";
            usrsname = (comcod == "3365" ? "bti"+ usrsname : comcod == "3354" ? usrsname :usrsname);
            string usermail = "";
            string webmailpwd = "";
            string userRole = "3";
            usrpass = (usrpass.Length == 0) ? "" : ASTUtility.EncodePassword(usrpass);
            bool result = HRData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSORUPDATEUSR", usrid, usrsname,
                      usrfname, usrdesig, usrpass, usrrmrk, active, empid, usermail, webmailpwd, userRole, "UserProfile", "", "", "");
            if (!result)
            {
                msg = HRData.ErrorObject["Msg"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "New User Created Failed!" + "');", true);
                return;

            }

            //user page permission auto 
              result = HRData.UpdateTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "INSERTPAGEPERMISSION_AUTO", usrid, "",
                     "", "", "", "", "", "", "", "", "", "", "", "", "");


            msg = "New User Created Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);

            GetEmpList();


            string eventtype = "User Login From";
            string eventdesc = "Update ID";
            string eventdesc2 = "Your profile Updated,";

            if (ConstantInfo.LogStatus == true)
            {
                bool IsVoucherSaved = CALogRecord.AddLogRecord(comcod, ((Hashtable)Session["tblLogin"]), eventtype, eventdesc, eventdesc2);
            }
            // for notification
            // title  details recvier id
            //bool result2 = UserNotify.SendNotification(eventdesc, eventdesc2, usrid);
             
        }

        protected void gvEmpList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //DateTime startdate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "nstartdate"));
                string  usrsname = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "usrsname"));
                //DateTime today = System.DateTime.Now;
                //if (e.Row.RowIndex > 1)
                //{
                //    Label txtHours = e.Row.FindControl("NoticeDet") as Label;
                //    txtHours.Text = "";

                //}


                //if (today >= startdate && today <= enddate)
                //{

                //    e.Row.FindControl("NoticeDet").Visible = true;
                //}
                //else
                //{
                //    e.Row.FindControl("NoticeDet").Visible = false;
                //}
                if((GetComeCode()=="3365" || GetComeCode() == "3354" ) && usrsname =="")
                {
                    e.Row.FindControl("lnkUserGenarate").Visible = true;

                }


            }
        }
    }
}