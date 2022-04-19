using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RealERPLIB;
using System.Configuration;
using System.Data;
using System.Web.Script.Services;


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

                this.rblmonth_SelectedIndexChanged(null, null);
                //this.ShowHoliday();
            }
        }



        private void ShowHoliday()
        {
            string comcod = this.GetComCode();
            string monthid = this.rblmonth.SelectedValue.ToString();
            string year = System.DateTime.Now.ToString("yyyy");


            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETHOLIDAYINFO", monthid, year, "", "", "", "", "", "", "");

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

            bool result = false;

            result = HRData.UpdateTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "DELETEHOLIDAY", date.ToString(), "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            if (result)
            {
                dt.Rows[index].Delete();
            }
            ViewState["HolidayInfo"] = dt;



            Data_Bind();


        }
    }
}