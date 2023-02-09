using RealERPLIB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WinForms;

namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptEmpAbsCount : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE ABSENT COUNT LIST";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetDate();
                this.GetCompName();
                this.GetYearMonth();

            }

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lnkPrint_Click);

        }
        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)            
            return;

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            string date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtfodate.Text = startdate + date.Substring(2);
            this.txttodate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
        }

        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());

        }
        private void GetYearMonth()
        {
            string comcod = this.GetComeCode();

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETYEARMON", "", "", "", "", "", "", "", "", "");
            if (ds1 == null)
                return;
            //this.ddlyearmon.DataTextField = "yearmon";
            //this.ddlyearmon.DataValueField = "ymon";
            //this.ddlyearmon.DataSource = ds1.Tables[0];

            //this.ddlyearmon.SelectedValue = System.DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            //this.ddlyearmon.DataBind();
            //this.ddlyearmon.DataBind();
            //string txtdate = Convert.ToDateTime(this.txtDate.Text.Trim()).ToString("dd-MMMM-yyyy");
            ds1.Dispose();
        }

        private void GetCompName()
        {
            Session.Remove("tblcompany");
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = this.GetComeCode();
            string txtCompany =  "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETCOMPANYNAME1", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlcomp.DataTextField = "actdesc";
            this.ddlcomp.DataValueField = "actcode";
            this.ddlcomp.DataSource = ds1.Tables[0];
            this.ddlcomp.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.ddlcomp_SelectedIndexChanged(null, null);
            ds1.Dispose();
        }

        protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }
        private void GetDepartment()
        {
            if (this.ddlcomp.Items.Count == 0)
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlcomp.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            string txtCompanyname = (this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() + "%";

            // string txtCompanyname =(this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) =="00")?"%":this.ddlCompanyName.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept =  "%%";
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

            string comcod = this.GetComeCode();
            string projectcode = this.ddldept.SelectedValue.ToString();
            string txtSSec = "%%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlsec.DataTextField = "sectionname";
            this.ddlsec.DataValueField = "section";
            this.ddlsec.DataSource = ds2.Tables[0];
            this.ddlsec.DataBind();

        }
        protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmpName();
        }
        private void GetEmpName()
        {
            string comcod = this.GetComeCode();
            string ProjectCode = this.ddlsec.SelectedValue.ToString() + "%";
            string txtSProject = "%";
            DataSet ds5 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE", "GETPAYSLIPEMPNAMEALL", ProjectCode, txtSProject, "", "", "", "", "", "", "");
            this.ddlEmp.DataTextField = "empname";
            this.ddlEmp.DataValueField = "empid";
            this.ddlEmp.DataSource = ds5.Tables[0];
            this.ddlEmp.DataBind();
            ViewState["tblemp"] = ds5.Tables[0];

        }

        private void ShowAbsCount()
        {
            Session.Remove("tblabscount");
            string comcod = this.GetComeCode();
            string fromdate = this.txtfodate.Text.ToString();
            string tdate = this.txttodate.Text.ToString();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlcomp.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string nozero = (hrcomln == 4) ? "0000" : "00";
            //string compname = (this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() == nozero) ? "%" : this.ddlcomp.SelectedValue.Substring(0, hrcomln).ToString() + "%";
            string deptname = (this.ddldept.SelectedValue.ToString() == "000000000000") ? "%" : this.ddldept.SelectedValue.ToString() + "%";
            string section = (this.ddlsec.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlsec.SelectedValue.ToString() + "%";
            string Empcode = (this.ddlEmp.SelectedValue.ToString() == "000000000000") ? "93%" : this.ddlEmp.SelectedValue.ToString() + "%";
            string year="2020";

            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPABSCOUNTINFO", year, deptname, section, Empcode, fromdate, tdate);
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPABSCOUNTINFO", Empcode, fromdate, tdate, deptname, section );

            if (ds2 == null)
            {
                this.gvabscount.DataSource = null;
                this.gvabscount.DataBind();
                return;
            }
            Session["tblabscount"] = this.HiddenSameData(ds2.Tables[0]);
            this.Data_Bind();

        }
        protected void lnkok_Click(object sender, EventArgs e)
        {
            this.ShowAbsCount();
        }
        protected void gvabscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvabscount.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }
        private void Data_Bind()
        {
            string comcod = this.GetComeCode();
            DataTable dt = (DataTable)Session["tblabscount"];
            this.gvabscount.DataSource = dt;
            this.gvabscount.DataBind();
            this.FooterCal();
        }
        private void FooterCal()
        {
            DataTable dt = (DataTable)Session["tblabscount"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvabscount.FooterRow.FindControl("lgvFabsday")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(abscount)", "")) ? 0.00
                        : dt.Compute("sum(abscount)", ""))).ToString("#,##0;(#,##0); ");
        }

        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string deptcode;

            int j;

            secid = dt1.Rows[0]["secid"].ToString();
            deptcode = dt1.Rows[0]["deptcode"].ToString();
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
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptname"] = "";
                }

                else
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                }

            }

            return dt1;

        }

        protected void lbltotalabs_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            int index = row.RowIndex;
            string empid = ((Label)this.gvabscount.Rows[index].FindControl("lgvEmpId")).Text.ToString();
            string comcod = this.GetComeCode();
            string fromdate = this.txtfodate.Text.ToString();
            string todate = this.txttodate.Text.ToString();

            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTEMPLOYEEABSENTDETAILSCOUNT", empid, fromdate, todate);
            if (ds2 == null)
            {
                this.GvEmpDetails.DataSource = null;
                this.GvEmpDetails.DataBind();
                return;
            }
            this.GvEmpDetails.DataSource = this.HiddenSameDataDetails(ds2.Tables[0]);
            this.GvEmpDetails.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "OpenModalDeails();", true);      
        }


        private DataTable HiddenSameDataDetails(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;

            string secid;
            string deptcode;
            string design;
            string empid;

            int j;

            secid = dt1.Rows[0]["secid"].ToString();
            deptcode = dt1.Rows[0]["deptcode"].ToString();
            empid = dt1.Rows[0]["empid"].ToString();
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
                if (dt1.Rows[j]["deptcode"].ToString() == deptcode)
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                    dt1.Rows[j]["deptname"] = "";
                }

                else
                {
                    deptcode = dt1.Rows[j]["deptcode"].ToString();
                }

                if (dt1.Rows[j]["empid"].ToString() == empid)
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                    dt1.Rows[j]["empname"] = "";
                    dt1.Rows[j]["desig"] = "";
                }

                else
                {
                    empid = dt1.Rows[j]["empid"].ToString();
                }

            }

            return dt1;

        }
        private void lnkPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetComeCode();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string comsnam = hst["comsnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string session = hst["session"].ToString();
            string username = hst["username"].ToString();
            string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            DataTable dt = (DataTable)Session["tblabscount"];

            LocalReport Rpt1 = new LocalReport();

            var lst = dt.DataTableToList <RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpAbsCount> ();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_84_Lea.RptEmpAbsCount", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comnam", comnam));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("RptTitle", "Employee Absent Count"));
            Rpt1.SetParameters(new ReportParameter("printFooter", ASTUtility.Concat(compname, username, printdate)));
            Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
            Rpt1.SetParameters(new ReportParameter("printdate","Print Date : "+ printdate));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        }
        protected void ddlpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gvabscount.PageSize = Convert.ToInt32(this.ddlpage.SelectedValue.ToString());
            this.Data_Bind();

        }
    }
}