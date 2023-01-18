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
namespace RealERPWEB.F_81_Hrm.F_92_Mgt
{
    public partial class RptEmpStatus03 : System.Web.UI.Page
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

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "GradeWiseEmp") ? "Grade Wise Employee Details" : "";
                this.GetGrade();
                this.GetCompany();
                this.SelectType();
            }

        }

        private void SelectType()
        {
            //string type = this.Request.QueryString["Type"].ToString().Trim();

            //switch (type)
            //{
            //    case "GradeWiseEmp":
            //        this.MultiView1.ActiveViewIndex = 0;
            //        break;




            //}



        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            string userid = hst["usrid"].ToString();
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETCOMPANYNAMEIALL", txtCompany, userid, "", "", "", "", "", "", "");
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_BASIC_UTILITY_DATA", "GET_ACCESSED_COMPANYLIST", txtCompany, userid, "", "", "", "", "", "", "");
            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
            this.ddlCompany.DataTextField = "actdesc";
            this.ddlCompany.DataValueField = "actcode";
            this.ddlCompany.DataSource = ds1.Tables[0];
            this.ddlCompany.DataBind();
            this.ddlCompany_SelectedIndexChanged(null, null);
            ds1.Dispose();

        }

        private void GetDepartment()
        {

            string comcod = this.GetCompCode();
            if (this.ddlCompany.Items.Count == 0)
                return;
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETPROJECTNAME", Company, txtSProject, "", "", "", "", "", "", "");

            //DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "GETDEPARTMENT", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);

        }
        private void GetSectionName()
        {
            string comcod = this.GetCompCode();
            string Company = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";
            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "SECTIONNAMEDP", Company, Department, txtSSec, "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "sectionname";
            this.DropCheck1.DataValueField = "sectionname";
            this.DropCheck1.DataSource = ds2.Tables[0];
            this.DropCheck1.DataBind();
            //string comcod = this.GetCompCode();
            //string projectcode = this.ddlDepartment.SelectedValue.ToString();
            //string grade = ((this.ddlgrade.SelectedValue.ToString() == "0000") ? "03" : this.ddlgrade.SelectedValue.ToString()) + "%";
            //string txtSSec ="%"+this.txtSrcSec.Text.Trim()+"%" ;
            //DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "SECTIONNAME", projectcode,grade, txtSSec, "", "", "", "", "", "");
            //this.DropCheck1.DataTextField = "sectionname";
            //this.DropCheck1.DataValueField = "sectionname";
            //this.DropCheck1.DataSource = ds2.Tables[0];
            //this.DropCheck1.DataBind();
        }

        private void GetGrade()
        {

            string comcod = this.GetCompCode();
            string Srchgrade = "%" + this.txtSrcgrade.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "GETGRADE", Srchgrade, "", "", "", "", "", "", "", "");

            if (ds1 == null)
                return;
            this.ddlgrade.DataTextField = "gradedesc";
            this.ddlgrade.DataValueField = "gradecod";
            this.ddlgrade.DataSource = ds1.Tables[0];
            this.ddlgrade.DataBind();
        }



        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            this.GetCompany();
        }
        protected void imgbtngrade_Click(object sender, EventArgs e)
        {
            this.GetGrade();

        }

        protected void imgbtnProSrch_Click(object sender, EventArgs e)
        {
            this.GetDepartment();

        }
        protected void imgbtnSecSrch_Click(object sender, EventArgs e)
        {
            this.GetSectionName();
        }
        protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Data_Bind();
        }
        protected void lnkbtnShow_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "GradeWiseEmp":
                    this.ShowGradeWiseEmp();
                    break;



            }
        }




        private void ShowGradeWiseEmp()
        {
            ViewState.Remove("tblempstatus");
            string comcod = this.GetCompCode();

            string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string grade = ((this.ddlgrade.SelectedValue.ToString() == "0000") ? "03" : this.ddlgrade.SelectedValue.ToString()) + "%";
            string projectcode = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8)) + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 3) == "000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            }



            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_EMPSTATUS03", "RPTGRADEWISEMPSAL", CompanyName, grade, projectcode, section, "", "", "", "", "");

            if (ds3 == null)
            {
                //this.gvgwemp.DataSource = null;
                //this.gvgwemp.DataBind();
                this.gvEmpList.DataSource = null;
                this.gvEmpList.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            ViewState["tblempstatus"] = this.HiddenSameData(dt);
            this.Data_Bind();

        }



        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "GradeWiseEmp":

                    string company = dt1.Rows[0]["company"].ToString();
                    string grade = dt1.Rows[0]["grade"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company && dt1.Rows[j]["grade"].ToString() == grade)
                        {
                            //company = dt1.Rows[j]["company"].ToString();
                            //grade = dt1.Rows[j]["grade"].ToString();
                            dt1.Rows[j]["companyname"] = "";
                            dt1.Rows[j]["gradedesc"] = "";
                        }

                        else
                        {


                            if (dt1.Rows[j]["company"].ToString() == company)
                                dt1.Rows[j]["companyname"] = "";

                            //if (dt1.Rows[j]["grade"].ToString() == grade)
                            //    dt1.Rows[j]["gradedesc"] = "";                    
                        }

                        company = dt1.Rows[j]["company"].ToString();
                        grade = dt1.Rows[j]["grade"].ToString();



                    }
                    break;


            }



            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblempstatus"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "GradeWiseEmp":
                    //this.gvgwemp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.gvgwemp.DataSource = dt;
                    //this.gvgwemp.DataBind();

                    this.gvEmpList.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpList.DataSource = dt;
                    this.gvEmpList.DataBind();

                    break;
            }



        }

        private void FooterCalculation()
        {
            //DataTable dt = (DataTable)ViewState["tblpay"];

            //if (dt.Rows.Count == 0)
            //    return;
            //string type = this.Request.QueryString["Type"].ToString().Trim();

            //switch (type)
            //{


            //    case "GradeWiseEmp":
            //        ((Label)this.gvgwemp.FooterRow.FindControl("lgvFoallows")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", ""))).ToString("#,##0;(#,##0); ");
            //        break;

            //}



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptGradeWiseEmp();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;


            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)ViewState["tblempstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                           ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }

        private void PrintOvertimeSalary()
        {


            //Hashtable hst = (Hashtable)Session["tblLogin"];
            //string comcod = hst["comcod"].ToString();
            //string comnam = hst["comnam"].ToString();
            //string compname = hst["compname"].ToString();
            //string username = hst["username"].ToString();
            //string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            //DataTable dt = (DataTable)ViewState["tblpay"];
            //double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", "")));
            //string date = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            //ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_89_Pay.RptOvertimeSalary();
            //TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            //rptCname.Text = this.ddlCompany.SelectedItem.Text;
            //TextObject txtTitle = rptstate.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            //txtTitle.Text = "Allowance for Holiday/Friday Duties (H/O) - Month Of " + date;
            //TextObject txttk = rptstate.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            //txttk.Text = "Amount In Word: " + ASTUtility.Trans(Math.Round(netpay), 2);
            //TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            //txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            //rptstate.SetDataSource(dt);
            ////string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            ////rptstate.SetParameterValue("ComLogo", ComLogo);
            //Session["Report1"] = rptstate;
            //this.lblprint.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
            //                    this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }



        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
        }


        protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();

        }


        protected void gvgwemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.gvgwemp.PageIndex = e.NewPageIndex;
            //this.Data_Bind();

        }
        protected void gvEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            this.gvEmpList.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }

    }
}