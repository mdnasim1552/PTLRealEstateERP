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
namespace RealERPWEB.F_81_Hrm.F_89_Pay
{
    public partial class EmpOverTimeSalary : System.Web.UI.Page
    {
        ProcessAccess HRData = new ProcessAccess();
        Common compUtility = new Common();
        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                if (!ASTUtility.PagePermission(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]))
                    Response.Redirect("../../AcceessError.aspx");

                //((Label)this.Master.FindControl("lblTitle")).Text = (this.Request.QueryString["Type"].ToString().Trim() == "OvertimeSalary") ? "EMPLOYEE OVERTIME SALARY" : "EMPLOYEE MONTHLY LATE ATTENDANCE INMROMATION";
                DataRow[] dr1 = ASTUtility.PagePermission1(HttpContext.Current.Request.Url.AbsoluteUri.ToString(), (DataSet)Session["tblusrlog"]);
                ((Label)this.Master.FindControl("lblTitle")).Text = dr1[0]["dscrption"].ToString();
                this.Master.Page.Title = dr1[0]["dscrption"].ToString();

                this.GetDate();
                this.GetCompany();
                this.GetDesignation();
                this.SelectType();

            }
        }

        private void GetDate()
        {
            DataSet datSetup = compUtility.GetCompUtility();
            if (datSetup == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "showContentFail('" + "Please Setup Start Date Firstly!" + "');", true);
                return;
            }

            string startdate = datSetup.Tables[0].Rows.Count == 0 ? "01" : Convert.ToString(datSetup.Tables[0].Rows[0]["HR_ATTSTART_DAT"]);
            this.txtfromdate.Text = System.DateTime.Today.AddMonths(-1).ToString("dd-MMM-yyyy");
            this.txtfromdate.Text = startdate + this.txtfromdate.Text.Trim().Substring(2);
            this.txttodate.Text = Convert.ToDateTime(this.txtfromdate.Text).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // Create an event handler for the master page's contentCallEvent event
            ((LinkButton)this.Master.FindControl("lnkPrint")).Click += new EventHandler(lbtnPrint_Click);

            //((Panel)this.Master.FindControl("pnlTitle")).Visible = true;

        }

        private void SelectType()
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {
                case "OvertimeSalary":
                    this.MultiView1.ActiveViewIndex = 0;
                    break;


                case "MonthlyLateAtten":
                    this.MultiView1.ActiveViewIndex = 1;

                    break;

                case "OvertimeSalary02":
                    this.MultiView1.ActiveViewIndex = 2;
                    break;
            }



        }
        private void GetCompany()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string userid = hst["usrid"].ToString();
            string comcod = hst["comcod"].ToString();
            string txtCompany = "%" + this.txtSrcCompany.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "GETCOMPANYNAME", txtCompany, userid, "", "", "", "", "", "", "");
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
            string Company = this.ddlCompany.SelectedValue.ToString().Substring(0, 2) + "%";
            string txtSProject = "%" + this.txtSrcPro.Text.Trim() + "%";
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "GETDEPARTMENT", Company, txtSProject, "", "", "", "", "", "", "");
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
            DataSet ds2 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "SECTIONNAME", projectcode, txtSSec, "", "", "", "", "", "", "");
            this.DropCheck1.DataTextField = "sectionname";
            this.DropCheck1.DataValueField = "sectionname";
            this.DropCheck1.DataSource = ds2.Tables[0];
            this.DropCheck1.DataBind();

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

                case "OvertimeSalary":
                    this.ShowEmpOvertimeSalary();
                    break;

                case "MonthlyLateAtten":

                    this.ShowMonthlyLateAtten();
                    break;
                case "OvertimeSalary02":
                    this.ShowEmpOvertimeSalary02();

                    break;

            }
        }

        private void GetDesignation()
        {

            string comcod = this.GetCompCode();
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "GETDESIGNATION", "", "", "", "", "", "", "", "", "");
            Session["tbldesig"] = ds1.Tables[0];
            if (ds1 == null)
                return;
            this.ddlfrmDesig.DataTextField = "designation";
            this.ddlfrmDesig.DataValueField = "desigcod";
            this.ddlfrmDesig.DataSource = ds1.Tables[0];
            this.ddlfrmDesig.DataBind();
            this.ddlfrmDesig.SelectedValue = "0345001";
            this.GetDessignationTo();
        }

        private void GetDessignationTo()
        {

            DataTable dt = (DataTable)Session["tbldesig"];
            //string desigcod = this.ddlfrmDesig.SelectedValue.ToString().Trim();
            //DataView dv1 = dt.DefaultView;
            //dv1.RowFilter = "desigcod not in ('" + desigcod + "')";
            this.ddlToDesig.DataTextField = "designation";
            this.ddlToDesig.DataValueField = "desigcod";
            this.ddlToDesig.DataSource = dt;
            this.ddlToDesig.DataBind();

        }

        private void ShowEmpOvertimeSalary()
        {
            ViewState.Remove("tblpay");
            string comcod = this.GetCompCode();

            string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string projectcode = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8)) + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 4) == "0000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            }


            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");

         
            //added khalil
            string FrmDesignation = "0399999";
            string ToDesignation = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    FrmDesignation = this.ddlfrmDesig.SelectedValue.ToString();
                    ToDesignation = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }
            //end Khalil

            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "RPTOVRTIMESALARY", frmdate, todate, CompanyName, projectcode, section, ToDesignation, FrmDesignation, "", "");

            if (ds3 == null)
            {
                this.gvOvertime.DataSource = null;
                this.gvOvertime.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            ViewState["tblpay"] = dt;

            this.Data_Bind();

        }

        private void ShowEmpOvertimeSalary02()
        {

            ViewState.Remove("tblpay");
            string comcod = this.GetCompCode();

            string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string projectcode = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 8)) + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 4) == "0000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 8) + s1.Substring(0, 4);

            }


            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //added khalil
            string FrmDesignation = "0399999";
            string ToDesignation = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    FrmDesignation = this.ddlfrmDesig.SelectedValue.ToString();
                    ToDesignation = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }
            //end Khalil
            DataSet ds3 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_OVRTIMESALARY", "RPTOVRTIMESALARY02", frmdate, todate, CompanyName, projectcode, section, ToDesignation, FrmDesignation, "", "");

            if (ds3 == null)
            {
                this.gvOvertime02.DataSource = null;
                this.gvOvertime02.DataBind();
                return;

            }

            DataTable dt = ds3.Tables[0];
            ViewState["tblpay"] = dt;

            this.Data_Bind();




        }

        private void ShowMonthlyLateAtten()
        {


            Session.Remove("tblpay");
            string comcod = this.GetCompCode();
            string CompanyName = ((this.ddlCompany.SelectedValue.ToString() == "000000000000") ? "" : this.ddlCompany.SelectedValue.ToString().Substring(0, 2)) + "%";
            string Department = ((this.ddlDepartment.SelectedValue.ToString() == "000000000000") ? "" : this.ddlDepartment.SelectedValue.ToString().Substring(0, 9)) + "%";

            string section = "";
            if ((this.ddlDepartment.SelectedValue.ToString() != "000000000000"))
            {
                string[] sec = this.DropCheck1.Text.Trim().Split(',');

                if (sec[0].Substring(0, 3) == "000")
                    section = "";
                else
                    foreach (string s1 in sec)
                        section = section + this.ddlDepartment.SelectedValue.ToString().Substring(0, 9) + s1.Substring(0, 3);

            }
            string frmdate = Convert.ToDateTime(this.txtfromdate.Text).ToString("dd-MMM-yyyy");
            string todate = Convert.ToDateTime(this.txttodate.Text).ToString("dd-MMM-yyyy");
            //added khalil
            string FrmDesignation = "0399999";
            string ToDesignation = "0300001";
            switch (comcod)
            {
                case "3102":
                    //pnlDesig.Visible = true;

                    FrmDesignation = this.ddlfrmDesig.SelectedValue.ToString();
                    ToDesignation = this.ddlToDesig.SelectedValue.ToString();
                    break;
                default:
                    //pnlDesig.Visible = false;
                    break;
            }
            //end Khalil
          
            DataSet ds1 = HRData.GetTransInfo(comcod, "dbo_hrm.SP_REPORT_HR_ATTENDENCE", "RPTMONTHLYLATEATTN02", CompanyName, Department, section, ToDesignation, FrmDesignation, frmdate, todate, "", "");
            if (ds1 == null)
            {
                this.gvMoLateAttn.DataSource = null;
                this.gvMoLateAttn.DataBind();
                return;

            }
            ViewState["tblpay"] = this.HiddenSameData(ds1.Tables[0]);
            this.Data_Bind();


        }


        private DataTable HiddenSameData(DataTable dt1)
        {
            if (dt1.Rows.Count == 0)
                return dt1;


            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {

                case "OvertimeSalary":

                    break;

                case "MonthlyLateAtten":
                    string company = dt1.Rows[0]["company"].ToString();
                    for (int j = 1; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j]["company"].ToString() == company)
                        {
                            company = dt1.Rows[j]["company"].ToString();
                            dt1.Rows[j]["companyname"] = "";
                        }

                        else
                            company = dt1.Rows[j]["company"].ToString();


                    }

                    break;
            }



            return dt1;

        }

        private void Data_Bind()
        {

            DataTable dt = (DataTable)ViewState["tblpay"];
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {
                case "OvertimeSalary":
                    this.gvOvertime.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOvertime.DataSource = dt;
                    this.gvOvertime.DataBind();
                    this.FooterCalculation();
                    break;

                case "MonthlyLateAtten":
                    this.gvMoLateAttn.PageSize = Convert.ToInt16(this.ddlpagesize.SelectedValue);
                    this.gvMoLateAttn.DataSource = (DataTable)ViewState["tblpay"];
                    this.gvMoLateAttn.DataBind();
                    break;
                case "OvertimeSalary02":
                    this.gvOvertime02.PageSize = Convert.ToInt32(this.ddlpagesize.SelectedValue.ToString());
                    this.gvOvertime02.DataSource = dt;
                    this.gvOvertime02.DataBind();
                    this.FooterCalculation();

                    break;




            }



        }

        private void FooterCalculation()
        {
            DataTable dt = (DataTable)ViewState["tblpay"];

            if (dt.Rows.Count == 0)
                return;
            string type = this.Request.QueryString["Type"].ToString().Trim();

            switch (type)
            {


                case "OvertimeSalary":
                    ((Label)this.gvOvertime.FooterRow.FindControl("lgvFoallows")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", ""))).ToString("#,##0;(#,##0); ");
                    break;


                case "OvertimeSalary02":
                    ((Label)this.gvOvertime02.FooterRow.FindControl("lgvFoallows02")).Text = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", ""))).ToString("#,##0;(#,##0); ");
                    break;

            }



        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            string type = this.Request.QueryString["Type"].ToString().Trim();
            switch (type)
            {


                case "OvertimeSalary":
                    this.PrintOvertimeSalary();
                    break;

                case "MonthlyLateAtten":
                    this.PrintMonthlyLateAtten();
                    break;
                case "OvertimeSalary02":
                    this.PrintOvertimeSalary02();
                    break;




            }
        }

        private void PrintOvertimeSalary()
        {


            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblpay"];
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", "")));
            string date = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_89_Pay.RptOvertimeSalary();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = this.ddlCompany.SelectedItem.Text;
            TextObject txtTitle = rptstate.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Allowance for Holiday/Friday Duties (H/O) - Month Of " + date;
            TextObject txttk = rptstate.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "Amount In Word: " + ASTUtility.Trans(Math.Round(netpay), 2);
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);
            //string ComLogo = Server.MapPath(@"~\Image\LOGO" + comcod + ".jpg");
            //rptstate.SetParameterValue("ComLogo", ComLogo);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                             ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }


        private void PrintMonthlyLateAtten()
        {

            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblpay"];

            LocalReport Rpt1 = new LocalReport();
            var list = dt.DataTableToList<RealEntity.C_81_Hrm.C_84_Lea.BO_ClassLeave.RptEmpInfoData>();
            Rpt1 = RptSetupClass1.GetLocalReport("R_81_Hrm.R_83_Att.RptMonthlyLateAttn03", list, null, null);
            Rpt1.SetParameters(new ReportParameter("compName", comnam));
            Rpt1.SetParameters(new ReportParameter("rptTitle", "Late Attendance"));
            Rpt1.SetParameters(new ReportParameter("txtMonth", "Month " + Convert.ToDateTime(this.txttodate.Text.Trim()).ToString("MMMM,yyyy")));
            Rpt1.SetParameters(new ReportParameter("txtUserInfo", ASTUtility.Concat(compname, username, printdate)));


            Session["Report1"] = Rpt1;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RDLCViewer.aspx?PrintOpt=" +
                        ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";
        }

        private void PrintOvertimeSalary02()
        {
            Hashtable hst = (Hashtable)Session["tblLogin"];
            string comcod = hst["comcod"].ToString();
            string comnam = hst["comnam"].ToString();
            string compname = hst["compname"].ToString();
            string username = hst["username"].ToString();
            string printdate = System.DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss tt");
            DataTable dt = (DataTable)ViewState["tblpay"];
            double netpay = Convert.ToDouble((Convert.IsDBNull(dt.Compute("sum(oallow)", "")) ? 0.00 : dt.Compute("sum(oallow)", "")));
            string date = Convert.ToDateTime(this.txttodate.Text).ToString("MMMM, yyyy");
            ReportDocument rptstate = new RealERPRPT.R_81_Hrm.R_89_Pay.RptOvertimeSalary02();
            TextObject rptCname = rptstate.ReportDefinition.ReportObjects["txtCompName"] as TextObject;
            rptCname.Text = this.ddlCompany.SelectedItem.Text;
            TextObject txtTitle = rptstate.ReportDefinition.ReportObjects["txtTitle"] as TextObject;
            txtTitle.Text = "Extra Overtime Allowance for 6:00 PM to 7:00 PM (1 Hour) - Month Of " + date;
            TextObject txttk = rptstate.ReportDefinition.ReportObjects["TkInWord"] as TextObject;
            txttk.Text = "Amount In Word: " + ASTUtility.Trans(Math.Round(netpay), 2);
            TextObject txtuserinfo = rptstate.ReportDefinition.ReportObjects["txtuserinfo"] as TextObject;
            txtuserinfo.Text = "Printed from Computer Name:" + compname + ", User:" + username + ", Dated:" + printdate;
            rptstate.SetDataSource(dt);
            Session["Report1"] = rptstate;
            ((Label)this.Master.FindControl("lblprintstk")).Text = @"<script>window.open('../../RptViewer.aspx?PrintOpt=" +
                              ((DropDownList)this.Master.FindControl("DDPrintOpt")).SelectedValue.Trim().ToString() + "', target='_blank');</script>";

        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDepartment();

        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSectionName();
        }
        protected void gvOvertime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvOvertime.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void ddlfrmDesig_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetDessignationTo();
        }
        protected void gvMoLateAttn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMoLateAttn.PageIndex = e.NewPageIndex;
            this.Data_Bind();
        }

        protected void gvOvertime02_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}