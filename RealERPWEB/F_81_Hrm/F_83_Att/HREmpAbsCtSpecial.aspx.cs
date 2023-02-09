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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{

    public partial class HREmpAbsCtSpecial : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                if (dr1.Length == 0)
                    Response.Redirect("../AcceessError.aspx");
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();


                //  this.ViewVisibility();
                this.GetCompName();
                this.GetYearMonth();

                ((Label)this.Master.FindControl("lblmsg")).Visible = false;

            }


        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private string Getdatestart()
        {
            DataSet datSetup = compUtility.GetCompUtility();

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            return startdate;
        }

        private void GetYearMonth()
        {
            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "GETMONTHFORABS", "", "", "", "", "", "", "", "", "");
            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "yearmon";
            this.ddlMonth.DataSource = ds1.Tables[0];
            this.ddlMonth.DataBind();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();
            //this.ddlMonth.SelectedValue = System.DateTime.Today.Month.ToString().Trim();
        }
       


        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.ShowAbsCount();
           
        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;

            int j;

            secid = dt1.Rows[0]["secid"].ToString();
            for (j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }

            }

            return dt1;

        }

        private void ShowAbsCount()
        {
            Session.Remove("tblabscount");
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string compname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            //  string MonthId = this.ddlyearmon.Text.Trim();

            string Month = this.ddlMonth.SelectedItem.Text.Substring(0, 3);
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string yearmon = this.ddlMonth.SelectedValue.ToString(); ;
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(yearmon.Substring(4, 2))) + "-" + yearmon.Substring(0, 4);
                    cudate = Convert.ToDateTime(date).AddMonths(-1).ToString("dd-MMM-yyyy");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(yearmon.Substring(4, 2))) + "-" + yearmon.Substring(0, 4);
                    cudate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                    break;
            }


            // string date = Convert.ToDateTime(ASTUtility.Right(this.ddlyearmon.Text.Trim(), 2) + "/01/" + this.ddlyearmon.Text.Trim().Substring(0, 4)).ToString("dd-MMM-yyyy");
            string Empcode ="%"+ this.txtSrcEmployee.Text.Trim() + "%";
            string section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "SHOWEMPABSENTSSPECIAL", compname, cudate, deptname, Empcode, section, "","", "", "");
            if (ds2 == null)
            {
                this.gvabsspecialcount.DataSource = null;
                this.gvabsspecialcount.DataBind();
                return;
            }
            Session["tblabscount"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }


        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblabscount"];
            this.gvabsspecialcount.DataSource = dt;
            this.gvabsspecialcount.DataBind();
            



        }

        

        protected void ibtnFindDepartment_Click(object sender, EventArgs e)
        {
            this.GetCompName();

        }

        private void GetCompName()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompanyName.DataTextField = "actdesc";
            this.ddlCompanyName.DataValueField = "actcode";
            this.ddlCompanyName.DataSource = ds1.Tables[0];
            this.ddlCompanyName.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlCompanyName_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }


        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void imgbtnDeptSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            if (this.ddlCompanyName.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompanyName.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string txtCompanyname = (this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlCompanyName.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            // string txtCompanyname =(this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) =="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }


        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        private void SectionName()
        {

            string comcod = this.GetComeCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }


       


        protected void imgbtnSearchEmployee_Click(object sender, EventArgs e)
        {
            this.ShowAbsCount();

        }
        //private void SaveValue()
        //{

        //    DataTable dt = (DataTable)Session["tblabscount"];
        //    int rowindex;

        //    for (int i = 0; i < this.gvabsspecialcount.Rows.Count; i++)
        //    {

        //        double absday = Convert.ToDouble("0" + ((TextBox)this.gvabsspecialcount.Rows[i].FindControl("txtabsday")).Text.Trim());
        //        rowindex = (this.gvabsspecialcount.PageSize) * (this.gvabsspecialcount.PageIndex) + i;
        //        dt.Rows[rowindex]["absday"] = absday;
        //    }

        //    Session["tblabscount"] = dt;
        //}


        protected void lnkBtnEmployeeShow_Click(object sender, EventArgs e)
        {

            GridViewRow gvr = (GridViewRow)((LinkButton)sender).NamingContainer;
            int RowIndex = gvr.RowIndex;
            int rownumber = this.gvabsspecialcount.PageSize * this.gvabsspecialcount.PageIndex + RowIndex;
            string empid = ((DataTable)Session["tblabscount"]).Rows[RowIndex]["empid"].ToString();
            string empName = ((DataTable)Session["tblabscount"]).Rows[RowIndex]["empname"].ToString();
            this.lblselectempid.Text = empid;
            this.lblSelectEmpName.Text = "Employee Name : "+ empName;

            this.GetEmployeeAbs(empid);          
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "openModal();", true);

            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
           
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetEmployeeAbs(string empid)
        {
         
            //
            this.chkDate.Items.Clear();          
            string comcod = this.GetCompCode();
            string yearmon = this.ddlMonth.SelectedValue.ToString(); ;
            // string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);


            //  DateTime date2 = DateTime.ParseExact(date1, "dd-MMM-yyyy", CultureInfo.InvariantCulture);



            //DateTime date1 = DateTime.Parse(this.ddlMonth.SelectedValue.ToString());
            string cudate = "";
            string date = "";
            switch (comcod)
            {
                case "3365":
                case "3101":
                    date = "26-" + ASTUtility.Month3digit(Convert.ToInt32(yearmon.Substring(4, 2))) + "-" + yearmon.Substring(0, 4);
                    cudate = Convert.ToDateTime(date).AddMonths(-1).ToString("dd-MMM-yyyy");
                    //cudate = date1.AddMonths(-1).ToString("dd-MMM-yyyy");
                    break;

                default:
                    date = "01-" + ASTUtility.Month3digit(Convert.ToInt32(yearmon.Substring(4, 2))) + "-" + yearmon.Substring(0, 4);
                    cudate = Convert.ToDateTime(date).ToString("dd-MMM-yyyy");
                    break;
            }

            //string date = "01-" + Month + "-" + year;                                
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "ABSENT_DATE", cudate, empid, "", "", "", "", "", "", "");

            if (ds4 == null)
            {
                return;
            }
            this.chkDate.DataTextField = "sdate1";
            this.chkDate.DataValueField = "sdate";
            this.chkDate.DataSource = ds4.Tables[0];
            this.chkDate.DataBind();
            DataTable dt = ds4.Tables[0];


            for (int i = 0; i < dt.Rows.Count; i++)
            {

                string flg = dt.Rows[i]["absfl"].ToString().Trim();
                this.chkDate.Items[i].Text = Convert.ToDateTime(dt.Rows[i]["sdate1"]).ToString("dd-MMM-yyyy (ddd)");
                if (flg == "1")
                    this.chkDate.Items[i].Selected = true;

            }

        }


        protected void btnabsUpdate_Click(object sender, EventArgs e)
        {
           this.Master.FindControl("lblmsg").Visible = true;
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string msg = "";
           string empid = this.lblselectempid.Text.ToString();
            string month = this.ddlMonth.SelectedValue.ToString();
            string month1 = month;
            string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            //string month = this.ddlMonth.SelectedValue.ToString().Trim();
            //string month1 = month.PadLeft(2, '0');
            //string year = ASTUtility.Right(this.ddlMonth.SelectedItem.Text.Trim(), 4);
            string monyr = this.ddlMonth.SelectedValue.ToString();
            bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "DELETEABSCT", empid, monyr, "", "", "", "", "", "", "", "", "", "", "", "", "");
            if (result == false)
            {
                ((Label)this.Master.FindControl("lblmsg")).Text = "Data was Noted Updated";
                msg = "Update Failed!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);

                return;

            }


            for (int i = 0; i < this.chkDate.Items.Count; i++)
            {
                if (this.chkDate.Items[i].Selected)
                {

                    string absdat = Convert.ToDateTime(this.chkDate.Items[i].Value).ToString("dd-MMM-yyyy");
                    string absfl = "1";
                    bool result1 = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPABSENT", "INORUPDATEABSENTCT", empid, absdat, absfl, monyr, "", "", "", "", "", "", "", "", "", "", "");

                }
            }

            ((Label)this.Master.FindControl("lblmsg")).Text = "Updated Successfully";
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseModal_AlrtMsg();", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "CloseMOdal();", true);

            this.ShowAbsCount();
        }
      

        protected void gvabscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
            this.gvabsspecialcount.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            this.Data_Bind();


        }

        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void chkactocopy_CheckedChanged(object sender, EventArgs e)
        {

        }
        


     

        
    }
}