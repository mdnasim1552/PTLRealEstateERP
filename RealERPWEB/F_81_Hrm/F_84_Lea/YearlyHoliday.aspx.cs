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
                this.txtfromdate.Text = "26-Dec-2021";
                this.txttodate.Text = "25-Dec-2022";

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
            Session.Remove("allHoliday");
            string htype = this.ddlholidayType.SelectedValue.ToString() ?? "";
            string todate =Convert.ToDateTime( this.txttodate.Text).ToString("dd-MMM-yyyy")??"";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") ??"" ;
            DataSet ds1 = HRData.GetTransInfo("", "dbo_hrm.[SP_REPORT_HR_MIS]", "YEARLYHOLIDAY", htype, frmdate,todate);
            if (ds1 == null)
                return;

           Session["allHoliday"] = ds1.Tables[0];
            this.Data_Bind();
        }

        private void Data_Bind()
        {
            this.gvholiday.DataSource = (DataTable)Session["allHoliday"];
            this.gvholiday.DataBind();
        }

        private void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string comcod = hst["comcod"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
          string printdate = System.DateTime.Now.ToString("dddd");
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string frmdate =Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") ?? "";
            string todate = this.txttodate.Text.ToString() ?? "";
            string type = this.ddlholidayType.SelectedValue.ToString();
            string curdate = System.DateTime.Now.ToString("yyyy");
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            DataTable dt = (DataTable)Session["allHoliday"];
            if (dt == null)
            {
                return;
            }

            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.yearlyholiday>();
            LocalReport Rpt1 = new LocalReport();
            if (type == "H")
            {
                Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.rptYearlyHolidayGov", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", curdate));
            }
            else
            {
                Rpt1 = RptHRSetup.GetLocalReport("R_81_Hrm.R_84_Lea.rptYearlyHoliday", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("rptTitle", curdate));
            }

            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlholidayType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            this.getHoliday();
        }

        protected void gvholiday_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvholiday.PageIndex = e.NewPageIndex;
            this.getHoliday();
        }
    }
}
