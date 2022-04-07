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
using Microsoft.Reporting.WinForms;
using RealERPRDLC;
namespace RealERPWEB.F_81_Hrm.F_84_Lea
{
    public partial class YearlyHoliday : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getHoliday();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }


        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        
 
        private void RefreshLeave()
        {


        }

        private void getHoliday()
        {
            string htype = this.ddlholidayType.SelectedValue.ToString() ?? "";
            string todate =Convert.ToDateTime( this.txttodate.Text).ToString();
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString(); ;
            DataSet ds1 = HRData.GetTransInfo("", "SP_REPORT_HR_MIS", "YEARLYHOLIDAY", htype, frmdate,todate);
            if (ds1 == null)
                return;

            this.gvholiday.DataSource = ds1.Tables[0];
            this.gvholiday.DataBind();
        }


        private void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void ddlholidayType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
