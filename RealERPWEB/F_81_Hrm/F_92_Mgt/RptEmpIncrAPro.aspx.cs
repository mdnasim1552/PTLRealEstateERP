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
    public partial class RptEmpIncrAPro : System.Web.UI.Page
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

                this.lblHtitle.Text = (this.Request.QueryString["Type"].ToString().Trim() == "GradeWiseEmp") ? "Grade Wise Employee Details" : "";
                this.txtfromdate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.txtfromdate.Text = "01" + this.txtfromdate.Text.Trim().Substring(2);
                this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                this.GetCompany();
                this.SelectType();
            }

        }

        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "Increment":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;




            }



        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = this.GetCompCode();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "GETCOMPANYNAME", txtCompany, "", "", "", "", "", "", "", "");
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
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "GETDEPARTMENT", Company, txtSProject, "", "", "", "", "", "", "");
            this.ddlDepartment.DataTextField = "actdesc";
            this.ddlDepartment.DataValueField = "actcode";
            this.ddlDepartment.DataSource = ds1.Tables[0];
            this.ddlDepartment.DataBind();
            this.ddlDepartment_SelectedIndexChanged(null, null);

        }
        private void GetSectionName()
        {

            string comcod = this.GetCompCode();
            string projectcode = this.ddlDepartment.SelectedValue.ToString();

            string txtSSec = "%" + this.txtSrcSec.Text.Trim() + "%";
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.ddlSection.DataTextField = "sectionname";
            this.ddlSection.DataValueField = "section";
            this.ddlSection.DataSource = ds2.Tables[0];
            this.ddlSection.DataBind();
        }





        private string GetCompCode()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            return (hst["comcod"].ToString());
        }

        protected void imgbtnCompany_Click(object sender, ImageClickEventArgs e)
        {
            this.GetCompany();
        }

        protected void imgbtnProSrch_Click(object sender, ImageClickEventArgs e)
        {
            this.GetDepartment();

        }
        protected void imgbtnSecSrch_Click(object sender, ImageClickEventArgs e)
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

                case "Increment":
                    this.ShowIncrement();
                    break;



            }
        }




        private void ShowIncrement()
        {
            ViewState.Remove("tblempstatus");
            string comcod = this.GetCompCode();

            string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string projectcode = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8)) + "%";
            string section = ((this.ddlSection.SelectedValue.ToString() == "000000000000") ? "" : this.ddlSection.SelectedValue.ToString()) + "%";
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_MIS", "RPTEMPLOYEEINCREMENT", CompanyName, projectcode, section, frmdate, todate, "", "", "", "");

            if (ds3 == null)
            {
                //this.gvgwemp.DataSource = null;
                //this.gvgwemp.DataBind();
                this.gvEmpinc.DataSource = null;
                this.gvEmpinc.DataBind();
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

                case "Increment":

                    string deptid = dt1.Rows[0]["deptid"].ToString();
                    string secid = dt1.Rows[0]["secid"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {

                        if (dt1.Rows[j]["deptid"].ToString() == deptid && dt1.Rows[j]["secid"].ToString() == secid)
                        {

                            dt1.Rows[j]["deptname"] = "";
                            dt1.Rows[j]["section"] = "";
                        }

                        else
                        {

                            if (dt1.Rows[j]["deptid"].ToString() == deptid)
                                dt1.Rows[j]["deptname"] = "";

                            if (dt1.Rows[j]["secid"].ToString() == secid)
                                dt1.Rows[j]["section"] = "";



                        }

                        deptid = dt1.Rows[j]["deptid"].ToString();
                        secid = dt1.Rows[j]["secid"].ToString();
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
                case "Increment":
                    //this.gvgwemp.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    //this.gvgwemp.DataSource = dt;
                    //this.gvgwemp.DataBind();

                    this.gvEmpinc.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvEmpinc.DataSource = dt;
                    this.gvEmpinc.DataBind();
                    this.FooterCalculation();

                    break;
            }



        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblempstatus"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {


                case "Increment":
                    ((Label)this.gvEmpinc.FooterRow.FindControl("lblgvFpresalary")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(presal)", "")) ? 0.00 : dt.Compute("sum(presal)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpinc.FooterRow.FindControl("lblgvFgvincam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(incam)", "")) ? 0.00 : dt.Compute("sum(incam)", ""))).ToString("#,##0;(#,##0); ");
                    ((Label)this.gvEmpinc.FooterRow.FindControl("lblgvFsalaincam")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(salaincmnt)", "")) ? 0.00 : dt.Compute("sum(salaincmnt)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "Increment":
                    this.PrintIncrement();
                    break;
            }




        }
        private void PrintIncrement()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");

            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_92_Mgt.RptEmpIncrement();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = comnam;
            TextObject txtDateRange = rptstate.ReportDefinition.ReportObjects["txtDateRange"] as TextObject;
            txtDateRange.Text = "(From " + this.txtfromdate.Text + " To " + this.txttodate.Text + ")";

            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource((DataTable)ViewState["tblempstatus"]);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            this.lbljavascript.Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                                this.DDPrintOpt.SelectedValue.Trim().ToString() + "', target='_blank');</script>";

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





        protected void gvEmpinc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvEmpinc.PageIndex = e.NewPageIndex;
            this.Data_Bind();

        }
    }
}