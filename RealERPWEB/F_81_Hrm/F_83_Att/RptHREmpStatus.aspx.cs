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
namespace RealERPWEB.F_81_Hrm.F_83_Att
{
    public partial class RptHREmpStatus : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.rbtnlst.SelectedIndex = 0;
                this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetCompanyName();

                //((LinkButton)this.Master.FindControl("lnkPrint")).Enabled = (Convert.ToBoolean(dr1[0]["printable"]));
                //((Label)this.Master.FindControl("lblTitle")).Text = "EMPLOYEE STATUS INFORMATION";


            }
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private string GetComCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        private void GetCompanyName()
        {
            string comcod = this.GetComCode();
            string txtSCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETCOMPANYNAME", txtSCompany, "", "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtSCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);

        }
        private void GetProjectName()
        {

            string comcod = this.GetComCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "actdesc";
            this.ddlProjectName.DataValueField = "actcode";
            this.ddlProjectName.DataSource = ds1.Tables[0];
            this.ddlProjectName.DataBind();
            this.ddlProjectName_SelectedIndexChanged(null, null);

        }
        private void SectionName()
        {

            string comcod = this.GetComCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "SECTIONNAME", Company, Department, txtSSec, "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();

        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompanyName();
        }
        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }
        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {

            Session.Remove("tblEmpst");
            this.lblPage.Visible = true;
            this.ddlpagesize.Visible = true;
            string comcod = this.GetComCode();


            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "" : this.ddlProjectName.SelectedValue.ToString().Substring(0, 9)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            string calltype = "";
            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 5:
                case 6:
                    calltype = "RPTEMPSTATUS";
                    break;


                case 4:
                    calltype = "RPTTEREMPSTATUS";
                    break;

                case 7:
                    calltype = "RPTCONFIRMATIONDUE";
                    break;


            }
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", calltype, Company, Department, section, frmdate, todate, "", "", "", "");

            if (ds3 == null)
            {
                this.gvEmpStatus.DataSource = null;
                this.gvEmpStatus.DataBind();
                return;

            }


            Session["tblEmpst"] = ds3.Tables[0];
            this.LoadGrid();

        }

        private DataTable HiddenSameData(DataTable dt1)
        {

            if (dt1.Rows.Count == 0)
                return dt1;
            string company = dt1.Rows[0]["company"].ToString();
            string department = dt1.Rows[0]["department"].ToString();
            string section = dt1.Rows[0]["section"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["department"].ToString() == department && dt1.Rows[j]["section"].ToString() == section)
                {
                    company = dt1.Rows[j]["company"].ToString();
                    department = dt1.Rows[j]["department"].ToString();
                    section = dt1.Rows[j]["section"].ToString();
                    dt1.Rows[j]["companyname"] = "";
                    dt1.Rows[j]["departmentname"] = "";
                    dt1.Rows[j]["sectionname"] = "";
                }

                else
                {

                    if (dt1.Rows[j]["company"].ToString() == company)
                    {
                        dt1.Rows[j]["companyname"] = "";
                    }

                    if (dt1.Rows[j]["department"].ToString() == department)
                    {
                        dt1.Rows[j]["departmentname"] = "";
                    }

                    if (dt1.Rows[j]["section"].ToString() == section)
                    {
                        dt1.Rows[j]["sectionname"] = "";

                    }
                    company = dt1.Rows[j]["company"].ToString();
                    department = dt1.Rows[j]["department"].ToString();
                    section = dt1.Rows[j]["section"].ToString();

                }

            }
            return dt1;


        }


        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblEmpst"];
            DataView dv;
            int index = Convert.ToInt32(this.rbtnlst.SelectedIndex.ToString());
            switch (index)
            {
                case 0:
                    break;

                case 1:
                    dv = dt.DefaultView;
                    dv.RowFilter = ("tecst='yes'");
                    dt = dv.ToTable();
                    break;
                case 2:
                    dv = dt.DefaultView;
                    dv.RowFilter = ("tecst='no' or tecst=''");
                    dt = dv.ToTable();
                    break;

                case 3:
                    string txtdeg = this.txtDegree.Text.Trim() + "%";
                    dv = dt.DefaultView;
                    dv.RowFilter = ("acadeg like '" + txtdeg + "'");
                    dt = dv.ToTable();
                    break;

                case 5:
                    DateTime frmdate = Convert.ToDateTime(this.txtfromdate.Text);
                    DateTime todate = Convert.ToDateTime(this.txttodate.Text);
                    dv = dt.DefaultView;
                    dv.RowFilter = ("joindate >= '" + frmdate + "' and joindate<= '" + todate + "'");
                    dt = dv.ToTable();
                    break;

                case 6:
                    string txtdesig = this.txtDesig.Text.Trim() + "%";
                    dv = dt.DefaultView;
                    dv.RowFilter = ("desig like '" + txtdesig + "'");
                    dt = dv.ToTable();
                    break;


                case 7:
                    break;


            }

            dt = this.HiddenSameData(dt);
            Session["tblEmpst"] = dt;
            this.gvEmpStatus.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpStatus.DataSource = dt;
            this.gvEmpStatus.DataBind();
            this.gvEmpStatus.Columns[1].Visible = (this.ddlProjectName.SelectedValue == "000000000000") ? true : false;
            this.gvEmpStatus.Columns[10].Visible = (this.rbtnlst.SelectedIndex == 4) ? true : false;
            this.gvEmpStatus.Columns[11].Visible = (this.rbtnlst.SelectedIndex == 7) ? true : false;
            this.FooterCalculation();

        }
        private void FooterCalculation()
        {
            DataTable dt = (DataTable)Session["tblEmpst"];
            if (dt.Rows.Count == 0)
                return;
            ((Label)this.gvEmpStatus.FooterRow.FindControl("lgvFNetSal")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(netpay)", "")) ? 0.00 : dt.Compute("sum(netpay)", ""))).ToString("#,##0;(#,##0); ");


        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comname = hst["comnam"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            string comcod = hst["comcod"].ToString();
            string prjname = this.ddlProjectName.SelectedItem.Text.Trim().Substring(13);
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            LocalReport Rpt1 = new LocalReport();

            if (this.rbtnlst.SelectedIndex == 0)
            {
                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptHRAllEmpStatus", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "All Employee List With Academic Qualification"));

            }
            else if (this.rbtnlst.SelectedIndex == 1)
            {
                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptHRAllEmpStatus", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee List-Technical Person"));

            }
            else if (this.rbtnlst.SelectedIndex == 2)
            {
                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptHRAllEmpStatus", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee List-Non Technical Person"));

            }
            else if (this.rbtnlst.SelectedIndex == 3)
            {

                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptHRAllEmpStatus", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee List Academic Degree Wise"));


            }


            else if (this.rbtnlst.SelectedIndex == 4)
            {

                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptRetiredEmployee", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Retired Employee List"));

            }

            else if (this.rbtnlst.SelectedIndex == 5)
            {

                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptHRAllEmpStatus", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee List Academic Degree Wise"));

            }

            else if (this.rbtnlst.SelectedIndex == 6)
            {
                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptHRAllEmpStatus", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + System.DateTime.Today.ToString("dd-MMM-yyyy")));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Designation Wise Employee List"));


            }

            else if (this.rbtnlst.SelectedIndex == 7)
            {

                DataTable dt = (DataTable)Session["tblEmpst"];
                var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_83_Att.EClassAttendance.RptEmployeeStatus>();
                Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptEmpConfirmation", list, null, null);
                Rpt1.EnableExternalImages = true;
                Rpt1.SetParameters(new ReportParameter("compName", comname));
                Rpt1.SetParameters(new ReportParameter("txtDate", "(From " + Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy") + " To " + Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy") + ")"));
                Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Due Confirmation"));


            }

            Rpt1.SetParameters(new ReportParameter("comLogo", comLogo));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));

            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SectionName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }
        protected void gvEmpStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpStatus.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }
        protected void rbtnlst_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblfrmdate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.txtfromdate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.lbltodate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.txttodate.Visible = (this.rbtnlst.SelectedIndex == 4) || (this.rbtnlst.SelectedIndex == 5) || (this.rbtnlst.SelectedIndex == 7);
            this.txtDegree.Visible = (this.rbtnlst.SelectedIndex == 3);
            this.txtDesig.Visible = (this.rbtnlst.SelectedIndex == 6);
            this.lblimg.Visible = (this.rbtnlst.SelectedIndex == 3) || (this.rbtnlst.SelectedIndex == 6);
        }


    }
}
