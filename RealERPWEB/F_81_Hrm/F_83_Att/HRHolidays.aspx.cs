using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealERPLIB;
using System.Data;


namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class HRHolidays : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE HOLIDAY INFORMATION";
                ((Label)this.Master.FindControl("lblmsg")).Visible = false;
                int mon = System.DateTime.Now.Month;
                rblmonth.SelectedIndex = mon-1;
                this.rblmonth_SelectedIndexChanged(null, null);
                //this.ShowHoliday();
                this.GetCompany();
                this.GetHolidayType();
                this.GetDepartment();
                this.GetProjectName();
            }
        }


        private void GetHolidayType()
        {
            //string comcod = this.GetComCode();
            //string monthid = this.rblmonth.SelectedValue.ToString();
            //string year = System.DateTime.Now.ToString("yyyy");


            //DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETHOLIDAYTYPE", monthid, year, "", "", "", "", "", "", "");

            //this.gvholiday.DataSource = ds3.Tables[0];
            //this.gvholiday.DataBind();
            //ViewState["HolidayInfo"] = ds3.Tables[0];
            string comcod = this.GetComCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GETHOLIDAY", "", "", "", "", "", "", "", "", "");
            if (ds1 == null || ds1.Tables[0].Rows.Count==0)
                return;
            this.htype.DataSource = ds1.Tables[0];
            this.htype.DataBind();
            this.htype.DataTextField = "hrgdesc";
            this.htype.DataValueField = "unit";
            this.htype.DataBind();


        }


        private void ShowHoliday()
        {
            ViewState.Remove("HolidayInfo");
            string comcod = this.GetComCode();
            string monthid = this.rblmonth.SelectedValue.ToString();
            string year = System.DateTime.Now.ToString("yyyy");


            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETHOLIDAYINFO", monthid, year, "", "", "", "", "", "", "");
            if (ds3 == null || ds3.Tables[0].Rows.Count == 0)
                return;

            this.gvholiday.DataSource = ds3.Tables[0];
            this.gvholiday.DataBind();
            ViewState["HolidayInfo"] = ds3.Tables[0];

        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        //protected void gvholiday_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    string comcod = this.GetComCode();
        //    int index = Convert.ToInt32(e.RowIndex);
        //    DataTable dt = ViewState["HolidayInfo"] as DataTable;
        //    DateTime date = Convert.ToDateTime(dt.Rows[index]["hdate"].ToString());

        //    bool result = false;

        //    result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "DELETEHOLIDAY", date.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "");

        //    if (result)
        //    {
        //        dt.Rows[index].Delete();
        //    }
        //    ViewState["HolidayInfo"] = dt;



        //    Data_Bind();
        //}

        protected void Data_Bind()
        {
            gvholiday.DataSource = ViewState["HolidayInfo"] as DataTable;
            gvholiday.DataBind();
        }


        protected void lnkAddHoliday_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
        }

        protected void rblmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            int startyear = System.DateTime.Now.Year;
            this.lblmonth.Text =   this.rblmonth.SelectedItem.Text.ToString() + " - "  + startyear.ToString();
            this.ShowHoliday();
            string value = this.rblmonth.SelectedIndex.ToString();
            switch (value)
            {
                case "0":
                    this.rblmonth.Items[0].Attributes["style"] = "background:#5b9bd1; color:white; display:block";
                    break;
                case "1":
                    this.rblmonth.Items[1].Attributes["style"] = "background:#5b9bd1; color:white; display:block";
                    break;
                case "2":
                    this.rblmonth.Items[2].Attributes["style"] = "background:#5b9bd1; color:white; display:block";
                    break;
                case "3":
                    this.rblmonth.Items[3].Attributes["style"] = "background:#5b9bd1; color:white; display:block";
                    break;
                case "4":
                    this.rblmonth.Items[4].Attributes["style"] = "background:#5b9bd1; color:white;  display:block";
                    break;
                case "5":
                    this.rblmonth.Items[5].Attributes["style"] = "background:#5b9bd1; color:white; display:block";
                    break;
                case "6":
                    this.rblmonth.Items[6].Attributes["style"] = "background:#5b9bd1; color:white; display:block";
                    break;
                case "7":
                    this.rblmonth.Items[7].Attributes["style"] = "background:#5b9bd1; color:white; display:block";

                    break;
                case "8":
                    this.rblmonth.Items[8].Attributes["style"] = "background:#5b9bd1; color:white; display:block";

                    break;
                case "9":
                    this.rblmonth.Items[9].Attributes["style"] = "background:#5b9bd1; color:white; display:block";

                    break;
                case "10":
                    this.rblmonth.Items[10].Attributes["style"] = "background:#5b9bd1; color:white; display:block";

                    break;
                case "11":
                    this.rblmonth.Items[11].Attributes["style"] = "background:#5b9bd1; color:white; display:block";

                    break;
            }

        }

        public List<DateTime> getHolidayDayDates()
        {

            int startyear = System.DateTime.Now.Year;

            List<DateTime> holidays = new List<DateTime>();
            DateTime basedt = new DateTime(startyear, 1, 1);
            while ((basedt.Year == startyear))
            {
                if (basedt.DayOfWeek == (DayOfWeek.Friday))
                {
                    holidays.Add(new DateTime(basedt.Year, basedt.Month, basedt.Day));
                }
                basedt = basedt.AddDays(1);
            }
            return holidays;

        }



        protected void markallfriday_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
            int monthid = Convert.ToInt32(this.rblmonth.SelectedValue.ToString());
            int year = Convert.ToInt32(System.DateTime.Now.ToString("yyyy"));

            List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.Holiday> holidays = new List<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.Holiday>();
            List<DateTime> holidaydates = this.getHolidayDayDates();

            foreach (DateTime holiday in holidaydates)
            {
                RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.Holiday holiday1 = new RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.Holiday { holidayType = "W", HolidayDate = holiday, Occasion = "Weekend Day" };

                holidays.Add(holiday1);

            }

            bool result = false;
            foreach (RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.Holiday holiday in holidays)
            {

                result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTHOLIDAY", holiday.HolidayDate.ToString(), holiday.Occasion, holiday.holidayType, "", "", "", "", "", "", "", "", "", "", "", "");

            }

            if (result)
            {
                this.ShowHoliday();
            }


        }

        protected void gvholiday_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string hdate = e.Row.Cells[1].Text;
                foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete " + hdate + " as Holiday ?')){ return false; };";
                    }
                }
            }
        }

        protected void LnkbtnDelete_Click(object sender, EventArgs e)
        {
    
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;        
            int index = row.RowIndex;

            string comcod = this.GetComCode();
            DataTable dt = ViewState["HolidayInfo"] as DataTable;
            DateTime date = Convert.ToDateTime(dt.Rows[index]["hdate"].ToString());
            string date1 = Convert.ToDateTime(dt.Rows[index]["hdate"]).ToString("dd-MMM-yyyy");
             
            bool result = false;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "DELETEHOLIDAY", date1, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                //dt.Rows[index].Delete();
                this.ShowHoliday();
            }
            //ViewState["HolidayInfo"] = dt;



            Data_Bind();


        }
    
    
    
     private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            //string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            string txtCompany = "%%";

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
            //string txtSearchDept = this.txtSrcDepartment.Text.Trim() + "%";

            string txtSearchDept =  "%%";

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
            //string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";

            string txtSProject = "%%";

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETPROJECTNAME", Department, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "deptname";
            this.ddlProjectName.DataValueField = "deptid";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.GetSection();
            this.GetProjectName();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.GetDepartment();
            //this.GetProjectName();
        }

        protected void lnkApply_Click(object sender, EventArgs e)
        {
            string msg = "";
            try
            {
                string company = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00" ? "94%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
                string department = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? company : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
                string section = (this.ddlProjectName.SelectedValue.ToString() == "000000000000" ? "%%" : this.ddlProjectName.SelectedValue.ToString()) + "%";
                string comcod = this.GetComCode();
            
                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "ACTIVEEMPLEAVE", company, department, section, "", "", "", "", "", "");
                if (ds == null)
                    return;

                DataTable dt = ds.Tables[0];
                string empid = "";

                string rowCount = dt.Rows.Count.ToString();
                string year = Convert.ToDateTime(System.DateTime.Now).ToString("yyyy");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    empid = dt.Rows[i]["empid"].ToString();
                    bool result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "INSERTOFFDAY", empid, year, "", "", "", "", "", "");
                }

                msg = rowCount + " Rows affected!";
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + msg + "');", true);
            }
            catch
            {
                msg = "Update Failed";

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + msg + "');", true);
            }


            int mon = System.DateTime.Now.Month;
            rblmonth.SelectedIndex = mon - 1;


        }
    }

}