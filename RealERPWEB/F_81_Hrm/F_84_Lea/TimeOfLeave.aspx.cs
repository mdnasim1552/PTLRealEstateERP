using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class TimeOfLeave : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        SendNotifyForUsers UserNotify = new SendNotifyForUsers();
        private Hashtable _errObj;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nextday = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtaplydate.Text = nextday;
                GetRemaningTime();
            }
        }

        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }
        private void GetRemaningTime()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string empid = hst["empid"].ToString();
            string comcod = this.GetCompCode();
            string date = this.txtaplydate.Text.Trim();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETTIMEOFLEAVEHISTORY", empid, date, "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            this.txtTimeLVRem.Text = ds1.Tables[0].Rows[0][""].ToString();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}