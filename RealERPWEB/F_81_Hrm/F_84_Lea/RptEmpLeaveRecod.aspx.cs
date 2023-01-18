using Microsoft.Reporting.WinForms;
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
    public partial class RptEmpLeaveRecod : System.Web.UI.Page
    {

        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE'S LEAVE RECORD";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetDate();
                this.GetCompName();
                this.ShowValue();
                
            }
          
        }

        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
                return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtfodate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtfodate.Text = startdate + this.txtfodate.Text.Trim().Substring(2);
            this.txttodate.Text = Convert.ToDateTime(this.txtfodate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");

            //string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            //this.txtfodate.Text = startdate + date.Substring(2);
            //this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);
            // ((LinkButton)this.Master.FindControl("lnkbtnRecalculate")).Click += new EventHandler(lbtnTotal_Click);
            //((LinkButton)this.Master.FindControl("lnkbtnSave")).Click += new EventHandler(lbtnUpdate_Click);
        }


        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetCompName()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComCode();
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlcomp.DataTextField = "actdesc";
            this.ddlcomp.DataValueField = "actcode";
            this.ddlcomp.DataSource = ds1.Tables[0];
            this.ddlcomp.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlcomp_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }
        

        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowValue();
        }

        protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            this.GetDepartment();
        }

        private void GetDepartment()
        {
            if (this.ddlcomp.Items.Count == 0)
                return;
            string comcod = this.GetComCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlcomp.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string txtCompanyname = (this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            // string txtCompanyname =(this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) =="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddldept.DataTextField = "actdesc";
            this.ddldept.DataValueField = "actcode";
            this.ddldept.DataSource = ds1.Tables[0];
            this.ddldept.DataBind();
            this.ddldept_SelectedIndexChanged(null, null);
        }

        protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        private void SectionName()
        {

            string comcod = this.GetComCode();
            string projectcode = this.ddldept.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlsec.DataTextField = "sectionname";
            this.ddlsec.DataValueField = "section";
            this.ddlsec.DataSource = ds2.Tables[0];
            this.ddlsec.DataBind();

        }
        protected void ddlsec_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        private void GetEmpName()
        {
            string comcod = this.GetComCode();
            string ProjectCode = this.ddlsec.SelectedValue.ToString() + "%";
            string txtSProject = "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPAYSLIPEMPNAMEALL", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmp.DataTextField = "empname";
            this.ddlEmp.DataValueField = "empid";
            this.ddlEmp.DataSource = ds5.Tables[0];
            this.ddlEmp.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];

        }

     
        private void ShowValue()
        {
            Session.Remove("YearLeav");

            string comcod = this.GetComCode();
            string fromdate =  this.txtfodate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlcomp.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            //string compname = (this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddldept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddldept.SelectedValue.ToString() + "%";
            string section = (this.ddlsec.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlsec.SelectedValue.ToString() + "%";
            string Empcode = (this.ddlEmp.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlEmp.SelectedValue.ToString() + "%";
            string year = Convert.ToDateTime(fromdate).ToString("yyyy");
            DataSet ds4 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_LEAVESTATUS", "GETEMPLEAVERECORD", year, deptname, section, Empcode, fromdate, tdate);
            if (ds4 == null)
            {
                this.gvLeavRecod.DataSource = null;
                this.gvLeavRecod.DataBind();
                return;
            }
            DataTable dt = HiddenSameData(ds4.Tables[0]);
            Session["YearLeav"] = dt;
            this.LoadGrid();

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
                    secid = dt1.Rows[j]["secid"].ToString();
                    dt1.Rows[j]["secid"] = "";
                    dt1.Rows[j]["secname"] = "";
                }
                else
                {
                    secid = dt1.Rows[j]["secid"].ToString();
                }
            }
            return dt1;
        }
        private void LoadGrid()
        {

            DataTable dt = (DataTable)Session["YearLeav"];
            this.gvLeavRecod.PageSize = 50;//Convert.ToInt32(this.dd.SelectedValue.ToString());
            this.gvLeavRecod.DataSource = dt;
            this.gvLeavRecod.DataBind();
        }

       
        protected void gvLeavRecod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvLeavRecod.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }


        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvLeavRecod.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.LoadGrid();
        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string printFooter = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
            string frmDate = Convert.ToDateTime(this.txtfodate.Text).ToString("dd-MMM-yyyy");
            string toDate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            string txtDate = "(From " + frmDate + " To " + toDate + ")";

            DataTable dt = (DataTable)Session["YearLeav"];
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.EmpLeaveRecord>();



            LocalReport Rpt1 = new LocalReport();

            if(comcod == "3370" || comcod =="3101")
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptEmployeeLeaveRecordCPDL", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            }
            else
            {
                Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptEmployeeLeaveRecord", list, null, null);
                Rpt1.EnableExternalImages = true;
            }
               
      
            //Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "ANNUAL LEAVE APPROVAL SHEET"));
            Rpt1.SetParameters(new ReportParameter("printFooter", printFooter));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("txtDate", txtDate));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }



    }
}