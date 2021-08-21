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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class HREmpOffDays : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                this.GetCompany();
                this.GetProjectName();
                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE OFF DAY'S INFORMATION";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }

        private void GetDepartment()
        {
            if (this.ddlCompany.Items.Count == 0)
                return;
            Hashtable hst = (Hashtable)Session["tblLogin"];

            string comcod = hst["comcod"].ToString();
            string txtCompanyname = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }
        private void GetProjectName()
        {


            string comcod = this.GetComCode();
            //string company = this.ddlCompany.SelectedValue.Substring(0, 2);

            string Department = this.ddlDepartment.SelectedValue.ToString();
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETPROJECTNAME", Department, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "deptname";
            this.ddlProjectName.DataValueField = "deptid";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();

        }

        //protected void lnkbtnOffDay_Click(object sender, EventArgs e)
        //{

        //    if (this.lnkbtnOffDay.Text == "Ok")
        //    {
        //        this.lnkbtnOffDay.Text = "New";
        //        //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
        //        this.ddlProjectName.Visible = false;
        //        //this.lblProjectdesc.Visible = true;
        //        this.PnlEmp.Visible = true;
        //        this.chkoffDays.Visible = true;
        //        this.GetMonth();
        //        this.GetEmployee();
        //    }
        //    else
        //    {
        //        this.lmsg11.Text = "";
        //        this.lnkbtnOffDay.Text = "Ok";
        //        this.chkoffDays.Checked = false;
        //        this.ddlProjectName.Visible = true;
        //        //this.lblProjectdesc.Visible = false;
        //        this.PnlEmp.Visible = false;
        //        this.PnloffDays.Visible = false;
        //        this.chkoffDays.Visible = false;
        //        this.lblPage.Visible = false;
        //        this.ddlpagesize.Visible = false;
        //        this.gvoffday.DataSource = null;
        //        this.gvoffday.DataBind();

        //    }
        //}





        private void GetMonth()
        {

            string comcod = this.GetComCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "mno";
            this.ddlMonth.DataSource = ds2.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.Month.ToString().Trim();


        }
        private void GetEmployee()
        {
            string comcod = this.GetComCode();
            string secname = this.ddlProjectName.SelectedValue.ToString();
            string txtempsrch = "%%";
            string company = this.ddlCompany.SelectedValue.Substring(0, 2).ToString();
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETEMPLOYEENAME", secname, txtempsrch, company, "", "", "", "", "", "");
            this.ddlEmpName.DataTextField = "empname";
            this.ddlEmpName.DataValueField = "empid";
            this.ddlEmpName.DataSource = ds3.Tables[0];
            this.ddlEmpName.DataBind();

        }


        protected void lnkbtnoffShow_Click(object sender, EventArgs e)
        {
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            Session.Remove("tbloffday");
            string comcod = this.GetComCode();

            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00" ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";

            string Section = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string employee = (this.ddlEmpName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlEmpName.SelectedValue.ToString()) + "%";

            string date = Convert.ToDateTime("01-" + this.ddlMonth.SelectedItem.Text.Trim()).ToString("dd-MMM-yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "SHOWEMPOFFDAY", Section, date, employee, Company, Department, "", "", "", "");
            if (ds4 == null)
            {
                this.gvoffday.DataSource = null;
                this.gvoffday.DataBind();
                return;
            }

            Session["tbloffday"] = this.HiddenSameData(ds4.Tables[0]);
            this.LoadGrid();
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string company = dt1.Rows[0]["company"].ToString();
            string secid = dt1.Rows[0]["secid"].ToString();
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
                        dt1.Rows[j]["secton"] = "";
                }


                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }
            return dt1;

        }



        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void chkoffDays_CheckedChanged(object sender, EventArgs e)
        {
            this.PnloffDays.Visible = this.chkoffDays.Checked;
            //((Label)this.Master.FindControl("lblmsg")).Text = "";
            if (this.chkoffDays.Checked)
            {
                GetMonCalender();

            }
        }


        private void GetMonCalender()
        {
            this.chkDate.Items.Clear();
            string comcod = this.GetComCode();
            string Month = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string date = "01-" + Month + "-" + year;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONDATE", date, "", "", "", "", "", "", "", "");

            if (ds4 == null)
            {
                return;
            }
            this.chkDate.DataTextField = "sdate1";
            this.chkDate.DataValueField = "sdate";
            this.chkDate.DataSource = ds4.Tables[0];
            this.chkDate.DataBind();
            DataTable dt = ds4.Tables[0];
        }

        protected void lnkbtnAllUpdate_Click(object sender, EventArgs e)
        {
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;



            string comcod = this.GetComCode();
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00" ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Section = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlProjectName.SelectedValue.ToString()) + "%";
            string employee = (this.ddlEmpName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlEmpName.SelectedValue.ToString()) + "%";
            string reason = this.txtReason.Text.Trim();
            string dStatus = (this.Chkgov.Checked == true) ? "1" : "0";
            for (int i = 0; i < this.chkDate.Items.Count; i++)
            {
                if (this.chkDate.Items[i].Selected)
                {

                    string offdate = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTORUPOFFDAY", Company, Department, Section, employee, offdate, reason, dStatus, "", "", "", "", "", "", "", "");
                    if (!result)
                    {
                        ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Fail ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(0);", true);
                        return;

                    }

                }
            }
            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully ";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);
            this.chkoffDays.Checked = false;
            this.chkoffDays_CheckedChanged(null, null);


        }

        protected void lnkbtnGen_Click(object sender, EventArgs e)
        {
            // Session.Remove("tbloffday"); 
            // string comcod = this.GetComCode();
            // string company = ddlCompany.SelectedValue.Substring(0, 2).ToString();
            // string pactcode = this.ddlProjectName.SelectedValue.ToString();
            // string wkdate = Convert.ToDateTime(this.txtoffdate.Text).ToString("dd-MMM-yyyy");
            // string empid = this.ddlEmpName.SelectedValue.ToString(); 
            // string reason = this.txtReason.Text.Trim();
            // DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GENERATEEMPOFFDAY", pactcode, wkdate, reason, empid, company, "", "", "", "");
            // if (ds3 == null)
            // {
            //     this.gvoffday.DataSource = null;
            //     this.gvoffday.DataBind();
            //     return;

            // }





            // Session["tbloffday"] =ds3.Tables[0];;
            //this.LoadGrid();
            //this.chkoffDays.Checked = false;
            //this.chkoffDays_CheckedChanged(null, null);


        }
        private void LoadGrid()
        {
            this.gvoffday.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvoffday.DataSource = (DataTable)Session["tbloffday"];
            this.gvoffday.DataBind();


        }

        private void SaveValue()
        {
            DataTable dt = (DataTable)Session["tbloffday"];
            int rowindex;
            for (int i = 0; i < this.gvoffday.Rows.Count; i++)
            {
                string date = ((TextBox)this.gvoffday.Rows[i].FindControl("txtgvOffdate")).Text;
                string reason = ((TextBox)this.gvoffday.Rows[i].FindControl("txtgvReason")).Text;
                rowindex = (this.gvoffday.PageIndex) * (this.gvoffday.PageSize) + i;
                dt.Rows[rowindex]["wkdate"] = date;
                dt.Rows[rowindex]["reason"] = reason;
            }
            Session["tbloffday"] = dt;

        }
        protected void gvoffday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.SaveValue();
            this.gvoffday.PageIndex = e.NewPageIndex;
            this.LoadGrid();

        }

        protected void lnkbtnFUpOff_Click(object sender, EventArgs e)
        {
            this.SaveValue();
            ((Label)this.Master.FindControl("lblmsg")).Visible = true;
            DataTable dt = (DataTable)Session["tbloffday"];
            string Company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00" ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
            string Section = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "" : this.ddlCompany.SelectedValue.ToString()) + "%";
            string comcod = this.GetComCode();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string empid = dt.Rows[i]["empid"].ToString() + "%";
                string wkdate = dt.Rows[i]["wkdate"].ToString();
                string reason = dt.Rows[i]["reason"].ToString();
                bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTORUPOFFDAY", Company, Department, Section, empid, wkdate, reason, "", "", "", "", "", "", "", "", "");

            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully ";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "HideLabel(1);", true);



        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvoffday_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = (DataTable)Session["tbloffday"];
            string comcod = this.GetComCode();
            int rowindex = (this.gvoffday.PageSize) * (this.gvoffday.PageIndex) + e.RowIndex;
            string empid = dt.Rows[rowindex]["empid"].ToString();
            string date = Convert.ToDateTime(dt.Rows[rowindex]["wkdate"]).ToString("dd-MMMM-yyyy");
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "DELETEEMPOFFDAY", empid, date, "", "", "", "", "", "", "", "", "", "", "", "", "");


            if (result == true)
            {
                dt.Rows[rowindex].Delete();
            }

            DataView dv = dt.DefaultView;
            //dv.RowFilter = ("empid<>''");
            Session["tbloffday"] = dv.ToTable();
            this.LoadGrid();

        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            if (this.lnkbtnOffDay.Text == "Ok")
            {
                this.lnkbtnOffDay.Text = "New";
                //this.lblProjectdesc.Text = this.ddlProjectName.SelectedItem.Text.Substring(13);
                this.ddlCompany.Enabled = false;
                this.ddlProjectName.Enabled = false;
                //this.lblProjectdesc.Visible = true;
                this.PnlEmp.Visible = true;
                this.chkoffDays.Visible = true;
                this.GetMonth();
                this.GetEmployee();
            }
            else
            {

                this.lnkbtnOffDay.Text = "Ok";
                this.chkoffDays.Checked = false;
                //this.ddlProjectName.Visible = true;
                //this.lblProjectdesc.Visible = false;
                this.ddlCompany.Enabled = true;
                this.ddlProjectName.Enabled = true;
                this.PnlEmp.Visible = false;
                this.PnloffDays.Visible = false;
                this.chkoffDays.Visible = false;
                this.lblPage.Visible = false;
                this.ddlpagesize.Visible = false;
                this.gvoffday.DataSource = null;
                this.gvoffday.DataBind();

            }
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetDepartment();
            //this.GetProjectName();
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void lbtnsrchEmployee_Click(object sender, EventArgs e)
        {
            this.GetEmployee();
        }

        protected void txtoffdate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.GetSection();
            this.GetProjectName();
        }
    }
}

