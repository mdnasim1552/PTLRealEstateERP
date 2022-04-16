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

            ((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE HOLIDAY INFORMATION";
            ((Label)this.Master.FindControl("lblmsg")).Visible = false;

            this.ShowHoliday();

        }

       

        private void ShowHoliday()
        {
           



        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }

        protected void gvholiday_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void lnkAddHoliday_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string comcod = this.GetComCode();
        }

       


      

    }
}