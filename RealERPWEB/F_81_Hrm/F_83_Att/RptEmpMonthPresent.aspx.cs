using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;
using RealERPLIB;
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpMonthPresent : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int indexofamp = (HttpContext.Current.Request.Url.AbsoluteUri.ToString().Contains("&")) ? HttpContext.Current.Request.Url.AbsoluteUri.ToString().IndexOf('&') : HttpContext.Current.Request.Url.AbsoluteUri.ToString().Length;
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString().Substring(0, indexofamp), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();
                //((Label)this.Master.FindControl("lblTitle")).Text = "Employee Monthly Presence Report";
                ((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                this.GetCompany();
                this.GetDate();
            }
        }
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtFdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtFdate.Text = startdate + this.txtFdate.Text.Trim().Substring(2);
            this.txtTdate.Text = Convert.ToDateTime(this.txtFdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");


            //string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //this.txtFdate.Text = startdate + date.Substring(2);
            //this.txtTdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);



        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string txtCompName = "%%";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "GETCOMPANYNAME", txtCompName, "", "", "", "", "", "", "", "");
            if (ds == null)
                return;

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds.Tables[0];
            this.ddlCompany.DataBind();

        }

        private void GetPresence()
        {
            try
            {

                string comcod = this.GetCompCode();
                string refno = this.ddlCompany.SelectedValue.ToString();
                string sec = refno.Substring(0, 2) + "%";
                string sdate = this.txtFdate.Text;
                string edate = this.txtTdate.Text;

                DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_GROUP_ATTENDENCE", "MONTHLYGROUPATTN", sec, sdate, edate, "", "", "", "", "", "");
                if (ds == null)
                {
                    return;
                }

                Session["tblmonthlyPre"] = HiddenSameData(ds.Tables[0]);
                this.Data_Bind();

            }
            catch (Exception ex)
            {

            }


        }


        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;

            string secid = dt1.Rows[0]["secid"].ToString();
            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {

                    dt1.Rows[j]["section"] = "";
                }

                secid = dt1.Rows[j]["secid"].ToString();
            }

            return dt1;


        }


        private void Data_Bind()
        {
            DataTable dt = (DataTable)Session["tblmonthlyPre"];

            this.gvMonthlyPresence.DataSource = dt;
            this.gvMonthlyPresence.DataBind();
        }

        protected void lnkbtnShow_OnClick(object sender, EventArgs e)
        {
            this.GetPresence();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmdate = txtFdate.Text;
            string todate = txtTdate.Text;
            LocalReport Rpt1 = new LocalReport();
            DataTable dt1 = (DataTable)Session["tblmonthlyPre"];

            var lst = dt1.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.BO_ClassLate.MonthlyPresent>();

            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptEmpMonthPresence", lst, null, null);

            Rpt1.SetParameters(new ReportParameter("footer", printFooter));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("reprtdate", "Date: " + frmdate + " To " + todate));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Monthly Presence Report"));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

    }
}