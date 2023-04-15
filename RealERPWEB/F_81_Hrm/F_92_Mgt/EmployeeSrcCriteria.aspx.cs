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
using RealERPRDLC;
using Microsoft.Reporting.WinForms;
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class EmployeeSrcCriteria : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Remove("tblEmpstatus");;
                this.GetCompany();
                this.txtfrmage.Text = "0";
                this.txttoage.Text = "100";

                this.txtfrmsal.Text = "0";
                this.txttosal.Text = "1000000";

            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
           this.GetEmpList();
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comname = hst["comnam"].ToString();
            string session = hst["session"].ToString();
            string comadd = hst["comadd1"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string comLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)Session["empinfo"];
            if (dt == null || dt.Rows.Count==0)
                return;

            LocalReport Rpt1 = new LocalReport();
            var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_81_Rec.BO_ClassManPower.EmpAllInfo>();
            Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_82_App.RptEmployeeAllInfo", lst, null, null);
            Rpt1.EnableExternalImages = true;
            Rpt1.SetParameters(new ReportParameter("comname", comname));
            Rpt1.SetParameters(new ReportParameter("comadd", comadd));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee's Leave Card"));
            //Rpt1.PrintToPrinter();
            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
                ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            string gender = this.ddlgender.SelectedValue.ToString() == "0000" ? "%%" : this.ddlgender.SelectedValue.ToString() + "%";
            string religion = this.ddlreligion.SelectedValue.ToString()== "0000"?"%%": this.ddlreligion.SelectedValue.ToString() + "%";


            string frmage = (this.txtfrmage.Text.ToString() == "" ? "0" : this.txtfrmage.Text.ToString());
            string toage = (this.txttoage.Text.ToString() == "" ? "0" : this.txttoage.Text.ToString());

            string fromsal = Convert.ToDecimal((this.txtfrmsal.Text.ToString() == "" ? "0" : this.txtfrmsal.Text.ToString())).ToString();
            string tosal = Convert.ToDecimal((this.txttosal.Text.ToString() == "" ? "0" : this.txttosal.Text.ToString())).ToString();

            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETEMPBYFILTER", Company, Deptid, secid, gender, religion, frmage, toage, fromsal, tosal);
            if (ds == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }
            Session["tblEmpstatus"] = HiddenSameData(ds.Tables[0]);
            this.LoadGrid();

        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;
            string company, secid;
            company = dt1.Rows[0]["company"].ToString();
            secid = dt1.Rows[0]["secid"].ToString();
            string depcod = dt1.Rows[0]["depcod"].ToString();

            for (int j = 1; j < dt1.Rows.Count; j++)
            {
                if (dt1.Rows[j]["company"].ToString() == company)
                {

                    dt1.Rows[j]["companyname"] = "";

                }
                if (dt1.Rows[j]["secid"].ToString() == secid)
                {
                    dt1.Rows[j]["section"] = "";
                }

                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                        dt1.Rows[j]["companyname"] = "";

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";
                }


                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
            }
            return dt1;

        }



        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();

            string txtCompany = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            ds1.Dispose();
            this.ddlCompany_SelectedIndexChanged(null, null);


        }

        protected void ibtnFindProject_Click(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        private void GetProjectName()
        {
            string comcod = this.GetCompCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();
            string txtSSec = "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_PAYROLL", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlProjectName.DataTextField = "sectionname";
            this.ddlProjectName.DataValueField = "section";
            this.ddlProjectName.DataSource = ds2.Tables[0];
            this.ddlProjectName.DataBind();

        }

        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            string txtCompanyname = (this.ddlCompany.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSearchDept = "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_EMPLOYEE01", "GETDEPARTMENT", txtCompanyname, txtSearchDept, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.GetProjectName();
        }


        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            return comcod;
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetProjectName();
        }

        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadGrid();
        }

        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.LoadGrid();
        }

        private void LoadGrid()
        {
            DataTable dt = (DataTable)Session["tblEmpstatus"];
            this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
            this.gvEmpList.DataSource = dt;
            this.gvEmpList.DataBind();
            if (dt.Rows.Count > 0)
            {
                Session["Report1"] = gvEmpList;
                //((HyperLink)this.gvEmpList.HeaderRow.FindControl("hlbtntbCdataExcelemplist")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }
    }
}