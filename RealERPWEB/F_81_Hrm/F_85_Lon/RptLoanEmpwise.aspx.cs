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

namespace RealERPWEB.F_81_Hrm.F_85_Lon
{
    public partial class RptLoanEmpwise : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
     
                this.GetCompany();
            }


        }

        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            if (this.lnkbtnShow.Text == "New")
                return;

            Session.Remove("tblcompany");
            string comcod = this.GetComeCode();
            string txtCompany = "%%";
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");

            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");

            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            Session["tblcompany"] = ds1.Tables[0];
            this.GetDeptName();
            ds1.Dispose();
        }

        private void GetDeptName()
        {
            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
            string txtCompany = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETPROJECTNAME", Company, txtCompany, "", "", "", "", "", "", "");
            this.ddlDept.DataTextField = "actdesc";
            this.ddlDept.DataValueField = "actcode";
            this.ddlDept.DataSource = ds1.Tables[0];
            this.ddlDept.DataBind();
            this.GetSection();
            ds1.Dispose();
        }

        private void GetSection()
        {

            if (this.lnkbtnShow.Text == "New")
                return;
            string comcod = this.GetComeCode();
            int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";

            string DeptName = ((this.ddlDept.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDept.SelectedValue.ToString().Substring(0, 9)) + "%";
            string SrchSection = "%%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETSECTION", Company, DeptName, SrchSection, "", "", "", "", "", "");
            this.ddlSection.DataTextField = "section";
            this.ddlSection.DataValueField = "seccode";
            this.ddlSection.DataSource = ds1.Tables[0];
            this.ddlSection.DataBind();
            this.ddlSection_SelectedIndexChanged(null, null);
            ds1.Dispose();


        }



        private void GetEmployeeName()
        {
            try
            {

                Hashtable hst = (Hashtable)Session["tblLogin"];
                string comcod = this.GetComeCode();
                int hrcomln = Convert.ToInt32((((DataTable)Session["tblcompany"]).Select("actcode='" + this.ddlCompany.SelectedValue.ToString() + "'"))[0]["hrcomln"]);
                string compcode = this.ddlCompany.SelectedValue.ToString().Substring(0, hrcomln) + "%";
                string deptcode = (this.ddlDept.SelectedValue.ToString().Substring(0, 2) == "00") ? "%" : this.ddlDept.SelectedValue.ToString().Substring(0, 9) + "%";
                string Section = (this.ddlSection.SelectedValue.ToString() == "000000000000") ? "%" : this.ddlSection.SelectedValue.ToString() + "%";

                string txtSProject = "%";
                DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_ENTRY_ANNUAL_INCREMENT", "GETEMPLOYEENAME", compcode, deptcode, Section, txtSProject, "", "", "", "", "");
                Session["tblempdsg"] = ds3.Tables[0];
                this.ddlEmployee.DataTextField = "empname";
                this.ddlEmployee.DataValueField = "empid";
                this.ddlEmployee.DataSource = ds3.Tables[0];
                this.ddlEmployee.DataBind();
                this.ddlEmployee_SelectedIndexChanged(null, null);
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);


            }


        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDeptName();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSection();
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEmployeeName();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string empid = this.ddlEmployee.SelectedValue.ToString().Trim();
                DataTable dt = (DataTable)Session["tblempdsg"];
                DataRow[] dr = dt.Select("empid = '" + empid + "'");
                if (dr.Length > 0)
                {
                    string errMsg = ((DataTable)Session["tblempdsg"]).Select("empid='" + empid + "'")[0]["desig"].ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContent('" + errMsg + "');", true);

                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + ex.Message + "');", true);

            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            //((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);
            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }
        private string GetComeCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }


        //private void Data_Bind()
        //{
        //    string comcod = this.GetComeCode();
        //    DataTable dt = (DataTable)Session["tbloan"];
        //    this.gvEmpLoanStatus.DataSource = dt;
        //    this.gvEmpLoanStatus.DataBind();
        //}


        //protected void lbtnPrint_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = (DataTable)Session["tbloan"];
        //    Hashtable hst = (Hashtable)Session["tblLogin"];
        //    var lst = dt.DataTableToList<RealEntity.C_81_Hrm.C_92_mgt.EmpSettlmnt.EmpLoanStatus>();
        //    string comnam = hst["comnam"].ToString();
        //    string comcod = hst["comcod"].ToString();
        //    string comadd = hst["comadd1"].ToString();
        //    string session = hst["session"].ToString();
        //    string compname = hst["compname"].ToString();
        //    string username = hst["username"].ToString();

        //    string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
        //    string frmdate = Convert.ToDateTime(this.txtDate.Text).ToString("dd-MMM-yyyy");
        //    string txtuserinfo = "Printed from Computer Address :" + compname + " ,Session: " + session + " ,User: " + username + " ,Time: " + printdate;
        //    string ComLogo = new Uri(Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg")).AbsoluteUri;
        //    string EmpId = lst[0].idcard.ToString();
        //    string EmpDpt = lst[0].desig.ToString();
        //    string EmpName = lst[0].empname.ToString();
        //    string EmpDesg = lst[0].secdesc.ToString();


        //    LocalReport Rpt1 = new LocalReport();
        //    Rpt1 = RealERPRDLC.RptSetupClass1.GetLocalReport("R_81_Hrm.R_85_Lon.rptEmpLoanInsDetails", lst, null, null);
        //    Rpt1.EnableExternalImages = true;
        //    Rpt1.SetParameters(new ReportParameter("companyname", comnam));
        //    Rpt1.SetParameters(new ReportParameter("comadd", comadd));
        //    Rpt1.SetParameters(new ReportParameter("ComLogo", ComLogo));
        //    Rpt1.SetParameters(new ReportParameter("rptTitle", "Employee Loan Installment Details"));
        //    Rpt1.SetParameters(new ReportParameter("txtDate", "Date: " + frmdate));
        //    Rpt1.SetParameters(new ReportParameter("EmpId", EmpId));
        //    Rpt1.SetParameters(new ReportParameter("EmpDpt", EmpDpt));
        //    Rpt1.SetParameters(new ReportParameter("EmpName", EmpName));
        //    Rpt1.SetParameters(new ReportParameter("EmpDesg", EmpDesg));
        //    Rpt1.SetParameters(new ReportParameter("txtuserinfo", txtuserinfo));


        //    Session["Report1"] = Rpt1;
        //    ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewerWin.aspx?PrintOpt=" +
        //        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";


        //}

        //    protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        //    {
        //        this.Data_Bind();
        //    }
        //}
    }
}