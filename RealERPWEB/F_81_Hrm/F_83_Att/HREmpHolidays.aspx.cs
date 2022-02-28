using RealERPLIB;
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
    public partial class HREmpHolidays : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.GetMonth();
               this. ddlMonth_SelectedIndexChanged(null,null);
            }
        }
        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        private void GetMonth()
        {

            string comcod = this.GetComCode();
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONTHFOROFFDAY", "", "", "", "", "", "", "", "", "");
            this.ddlMonth.DataTextField = "mnam";
            this.ddlMonth.DataValueField = "yearmon";
            this.ddlMonth.DataSource = ds2.Tables[0];
            this.ddlMonth.DataBind();
            //this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("dd-MM-yyyy").Trim();
            this.ddlMonth.SelectedValue = System.DateTime.Today.ToString("yyyyMM").Trim();


        }
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetMonCalender();
        }
        private string Getdatestart()
        {
            DataSet datSetup = compUtility.GetCompUtility();

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);

            return startdate;
        }
        private void GetMonCalender()
        {
            //this.chkDate.Items.Clear();
            string comcod = this.GetComCode();
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
            //string date = cudate1.ToString("dd-MMM-yyyy");
            //string date = Getdatestart() + "-" + Month + "-" + year;
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_HREMPOFFDAY", "GETMONDATE", cudate, "", "", "", "", "", "", "", "");

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
    }
}