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
    public partial class EmployeeListEpic : System.Web.UI.Page
    {
        Common compUtility = new Common();
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                Session.Remove("tblEmpstatus");
                //this.GetDate();
                this.GetCompany();
                //this.SelectView();
                //this.GetDesignation();
            }
        }
        protected void lbtnOk_Click(object sender, EventArgs e)
        {
           this.GetEmpList();
        }

        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDessignationTo();
        }

        private void GetDessignationTo()
        {

            DataTable dt = (DataTable)Session["tbldesig"];
            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();
            if (this.Request.QueryString["Type"].ToString() == "EmpGradeADesig")
            {
                this.ddlToDesig.SelectedValue = "0311000";
            }
            else
            {
                // this.ddlToDesig.SelectedValue = "0311001";
            }


        }


        private void GetEmpList()
        {
            Session.Remove("tblEmpstatus");
            string comcod = this.GetCompCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string Deptid = (this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + "%";
            string secid = (this.ddlProjectName.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlProjectName.SelectedValue.ToString() + "%";
            DataSet ds = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS", "GETALLEMPLIST", Company, Deptid, secid, "", "", "", "", "", "");
            if (ds == null)
            {
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;
            }
            //Session["tblEmpstatus"] = HiddenSameData(ds4.Tables[0]);
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
                //if (dt1.Rows[j]["depcod"].ToString() == depcod)
                //{
                //    dt1.Rows[j]["deptname"] = "";
                //}
                //&& dt1.Rows[j]["secid"].ToString() == secid
                else
                {
                    if (dt1.Rows[j]["company"].ToString() == company)
                        dt1.Rows[j]["companyname"] = "";

                    if (dt1.Rows[j]["secid"].ToString() == secid)
                        dt1.Rows[j]["section"] = "";
                }


                company = dt1.Rows[j]["company"].ToString();
                secid = dt1.Rows[j]["secid"].ToString();
                //depcod = dt1.Rows[j]["depcod"].ToString();
            }
            return dt1;

        }



        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = GetCompCode();

            string txtCompany = "%";
            //string callType = (this.Request.QueryString["Type"].ToString() == "Pabx") ? "GETCOMPANYNAMEW_WPERMISSION" : "GETCOMPANYNAME";


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


            //
            //string comcod = this.GetCompCode();
            //string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            //string txtSProject = this.txtSrcPro.Text + "%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS2", "DEPARTMENTNAME", Company, txtSProject, "", "", "", "", "", "", "");
            //this.ddlProjectName.DataTextField = "departmentname";
            //this.ddlProjectName.DataValueField = "department";
            //this.ddlProjectName.DataSource = ds1.Tables[0];
            //this.ddlProjectName.DataBind();
        }

        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            //int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            //string nozero = (hrcomln == 4) ? "0000" : "00";

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
            comcod = this.ddlComName.SelectedValue.Length > 0 ? this.ddlComName.SelectedValue.ToString() : comcod;
            return comcod;
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();
            //this.GetProjectName();
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

                //((Label)this.gvEmpList.FooterRow.FindControl("lgvFlblgvemplist2")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("Sum(salary)", "")) ? 0.00 : dt.Compute("Sum(salary)", ""))).ToString("#,##0;(#,##0); ");

                Session["Report1"] = gvEmpList;
                ((HyperLink)this.gvEmpList.HeaderRow.FindControl("hlbtntbCdataExcelemplist")).NavigateUrl = "../../RptViewer.aspx?PrintOpt=GRIDTOEXCEL";
            }
        }
    }
}